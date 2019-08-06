using PMY.CompilationSystem.Model;
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
    public class MenuController : Controller
    {
        public PMY.CompilationSystem.IService.IMenuService _MenuService = null;

        public MenuController(IService.IMenuService menuService)
        {
            this._MenuService = menuService;
        }
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMenuList()
        {
            Dictionary<string, string> dicMenu = new Dictionary<string, string>();
            string sessionId = Request.Cookies["sessionId"].Value;
            //根据该值查Memcache.
            object obj = Common.MemcacheHelper.Get(sessionId);
            if (obj != null)
            {
                Users LoginUser = Common.SerializeHelper.DeserializeToObject<Users>(obj.ToString());
                switch (LoginUser.Right)
                {
                    case UserRight.超级管理员:
                        dicMenu = _MenuService.GetMenuByRight(UserRight.超级管理员);
                        //strMenuList = @"<li><a href=""/SVNOperation/Index"">SVN操作</a></li>
                        //           <li><a href=""/ProjectOperation/Index"">编译</a></li>
                        //           <li><a href=""/Home/Index"">打包</a></li>
                        //           <li><a href=""/Home/Index"">打包记录</a></li>
                        //           <li><a href=""/TaskOperation/Index"">任务列表</a></li>
                        //           <li><a href=""/Background/Index"">后台管理</a></li>
                        //         ";
                        break;
                    case UserRight.普通用户:
                        dicMenu = _MenuService.GetMenuByRight(UserRight.普通用户);
                        //strMenuList = @"<li><a href=""/SVNOperation/Index"">SVN操作</a></li>
                        //           <li><a href=""/ProjectOperation/Index"">编译</a></li>
                        //           <li><a href=""/Home/Index"">打包</a></li>
                        //           <li><a href=""/Home/Index"">打包记录</a></li>
                        //           <li><a href=""/TaskOperation/Index"">任务列表</a></li>
                        //         ";
                        break;
                    case UserRight.游客:
                        dicMenu = _MenuService.GetMenuByRight(UserRight.游客);
                        //strMenuList = @"
                        //           <li><a href=""/Home/Index"">打包记录</a></li>
                        //           <li><a href=""/TaskOperation/Index"">任务列表</a></li>
                        //         ";
                        break;
                }
                
            }

            StringBuilder strMenu = new StringBuilder();
            AjaxResult ajaxResult = null;
            if (dicMenu.Count > 0)
            {
                foreach (var menu in dicMenu)
                {
                    strMenu.Append("<li>");
                    strMenu.Append("<a href=" + menu.Value + ">");
                    strMenu.Append(menu.Key);
                    strMenu.Append("</a></li>");

                }
                ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Success,
                    PromptMsg = "成功",
                    Tag = strMenu.ToString()
                };
            }
            else
            {
                ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Failed,
                    PromptMsg = "失败",
                };
            }

            return Json(ajaxResult, JsonRequestBehavior.AllowGet);
        }
    }
}