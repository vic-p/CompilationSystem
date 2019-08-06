using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PMY.CompilationSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.DefaultBinder = new PMY.CompilationSystem.Extend.JQueryModelBundler();
            ControllerBuilder.Current.SetControllerFactory(new PMY.CommonUnity.UnityControllerFactory("MyContainer", "Config\\unity.Config"));

            ThreadPool.QueueUserWorkItem((state) => {
                IService.ITaskListService TaskListService = PMY.CommonUnity.UnitySingleton.CreateUnitySingleton("MyContainer", "Config\\unity.Config").GetInstance<PMY.CompilationSystem.IService.ITaskListService>();
                var signalrContext = GlobalHost.ConnectionManager.GetHubContext<PMY.CompilationSystem.SignalR.CMDMessageHub>();
                while (true)
                {
                    //signalrContext.Clients.All.addNewMessageToPage("开始处理");
                    Model.Models.TaskList task = TaskListService.LoadEntities(s => s.TaskStatus == Model.TaskStatus.未处理).FirstOrDefault();
                    if (task!=null)
                    {
                        task.TaskStatus = Model.TaskStatus.处理中;
                        TaskListService.EditEntity(task);
                        try
                        {
                            bool isQuitTask = false;
                            //string result = PMY.Common.ExecuteCMDHelper.ExecuteCMD(task.CMDCommand,(s,e)=> {                              
                            //    if (!string.IsNullOrEmpty(e.Data))
                            //    {
                            //        signalrContext.Clients.All.addNewMessageToPage(e.Data);
                            //        if (e.Data.Contains(" 个错误")&& e.Data!= "0 个错误")
                            //        {
                            //            isQuitTask = true;
                            //        }
                            //    }                             
                            //});
                            IRemoteService.ICmdService cmdService = PMY.CommonUnity.UnitySingleton.CreateUnitySingleton("MyContainer", "Config\\unity.Config").GetInstance<PMY.CompilationSystem.IRemoteService.ICmdService>();
                            cmdService.ExecuteCMD(task.CMDCommand, (message) =>
                            {
                                if (!string.IsNullOrEmpty(message))
                                {
                                    signalrContext.Clients.All.addNewMessageToPage(message);
                                    if (message.Contains(" 个错误") && message.Trim() != "0 个错误")
                                    {
                                        isQuitTask = true;
                                        task.TaskStatus = Model.TaskStatus.出现错误;
                                        TaskListService.EditEntity(task);
                                        signalrContext.Clients.All.addNewMessageToPage(task.ActionPath + task.TaskType.ToString() + "出错");
                                        //取消任务
                                        TaskListService.QuitTaskByCreator(task.Creator);
                                        signalrContext.Clients.All.addNewMessageToPage(task.Creator + "的任务已取消");
                                    }
                                }
                                else
                                {
                                    //当回调信息返回所有信息时，最后会返回null

                                    if (message == null && isQuitTask == false)
                                    {
                                        task.TaskStatus = Model.TaskStatus.已完成;
                                        TaskListService.EditEntity(task);
                                        signalrContext.Clients.All.addNewMessageToPage(task.ActionPath + task.TaskType.ToString() + "完成");
                                    }
                                }
                            });
                            
                        }
                        catch (Exception e)
                        {
                            signalrContext.Clients.All.addNewMessageToPage(e.Message);
                            task.TaskStatus = Model.TaskStatus.出现错误;
                            TaskListService.EditEntity(task);
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
            });

        }
    }
}
