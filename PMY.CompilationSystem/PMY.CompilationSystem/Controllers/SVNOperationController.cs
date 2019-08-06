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
    public class SVNOperationController : Controller
    {
        public IService.ITaskListService _TaskListService = null;
        public IService.IUsersService _UsersService = null;
        public SVNOperationController(IService.ITaskListService TaskListService, IService.IUsersService UsersService)
        {
            _TaskListService = TaskListService;
            _UsersService = UsersService;
        }
        // GET: SVNOperation
        [MenuFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update([Bind(Include = "Id, FolderName, FolderPath, Sort, CreateTime")]Model.Models.SVNFolders[] svnFoldersList)
        {
            string sessionId = Request.Cookies["sessionId"].Value;
            Users LoginUser = _UsersService.GetCurrentUserBySessionId(sessionId);
            StringBuilder updateCommand = new StringBuilder();
            updateCommand.Append("svn update");
            foreach (Model.Models.SVNFolders folder in svnFoldersList)
            {
                updateCommand.Append(" " + folder.FolderPath);
            }
            Model.Models.TaskList task = new Model.Models.TaskList()
            {
                CMDCommand = updateCommand.ToString(),
                TaskType = Model.TaskType.更新,
                TaskStatus = Model.TaskStatus.未处理,
                ActionPath = "",
                CreateTime = DateTime.Now,
                Creator = LoginUser.UserName
            };
            bool isSucessful = _TaskListService.AddEntity(task);
            //string result = PMY.Common.ExecuteCMDHelper.ExecuteCMD(updateCommand.ToString());
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
                    Result = DoResult.Failed,
                    PromptMsg = "任务添加失败"
                };
            }
            return Json(ajaxResult);
        }
    }
}