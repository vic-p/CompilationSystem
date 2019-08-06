using PMY.CompilationSystem.Filter;
using PMY.CompilationSystem.Model.Models;
using PMY.CompilationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PMY.CompilationSystem.Controllers
{    
    public class ProjectOperationController : Controller
    {
        public IService.IProjectsService _ProjectsService = null;
        public IService.ITaskListService _TaskListService = null;
        public IService.IUsersService _UsersService = null;
        public ProjectOperationController(IService.IProjectsService ProjectsService, IService.ITaskListService TaskListService, IService.IUsersService UsersService)
        {
            _ProjectsService = ProjectsService;
            _TaskListService = TaskListService;
            _UsersService = UsersService;
        }
        // GET: ProjectOperation
        [MenuFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProjects(string projectClassName)
        {
            var listProjects = _ProjectsService.LoadEntities(s => s.ProjectClassName == projectClassName).OrderBy(s => s.Sort).ToList();
            //查询项目所在文件夹路径下的所有文件的最新时间赋予LatestModifiTime
            foreach (Model.Models.Projects project in listProjects)
            {
                project.LatestModifiTime = PMY.Common.IOHelper.GetFileLastWriteTimeOfFolder(project.ProjectParentPath);
            }
            var total = listProjects.Count;
            return Json(new { total = total, rows = listProjects }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Compily(Model.Models.Projects[] projectList)
        {
            string sessionId = Request.Cookies["sessionId"].Value;
            Users LoginUser = _UsersService.GetCurrentUserBySessionId(sessionId);
            bool isSucessful = false;
            string errMsg = string.Empty;
            foreach (Model.Models.Projects project in projectList)
            {
                StringBuilder compilyCommand = new StringBuilder();
                compilyCommand.Append("MSBuild");
                compilyCommand.Append(" " + project.ProjectPath);
                compilyCommand.Append(" " + @"/t:Build /p:Configuration=Debug;WarningLevel=2");
                //string result = PMY.Common.ExecuteCMDHelper.ExecuteCMD(compilyCommand.ToString());
                Model.Models.TaskList task = new Model.Models.TaskList()
                {
                    CMDCommand = compilyCommand.ToString(),
                    TaskType = Model.TaskType.编译,
                    TaskStatus = Model.TaskStatus.未处理,
                    ActionPath = project.ProjectPath,
                    CreateTime = DateTime.Now,
                    Creator = LoginUser.UserName
                };
                isSucessful = _TaskListService.AddEntity(task);
                if (isSucessful)
                {
                    project.CreateTime = DateTime.Now;//必须赋值，否则默认最小值0001.1.1，会出现“从 datetime2 数据类型到 datetime 数据类型的转换产生一个超出范围的值”
                    project.LastCompileTime = DateTime.Now; 
                    _ProjectsService.EditEntity(project);
                }
                else
                {
                    errMsg = project.ProjectName + "任务添加失败";
                    break;
                }
            }
            
            AjaxResult ajaxResult = null;
            if (isSucessful)
            {
                ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Success,
                    PromptMsg = "任务添加成功"
                };
            }
            else
            {
                ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Success,
                    PromptMsg = errMsg
                };
            }
            return Json(ajaxResult);
        }

        public ActionResult CompilyAndPubllish(Model.Models.Projects[] projectList)
        {

            return null;
        }
    }
}