using PMY.CompilationSystem.Model.Models;
using PMY.CompilationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMY.CompilationSystem.Controllers
{
    public class UserManagementController : Controller
    {
        public IService.IUsersService _UsersService = null;
        public UserManagementController(IService.IUsersService UsersService)
        {
            _UsersService = UsersService;
        }
        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsers()
        {
            var listUser = _UsersService.LoadEntities(s => s.Id > 0).ToList();
            var total = listUser.Count;
            return Json(new { total = total, rows = listUser }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCurrentUser()
        {
            string sessionId = Request.Cookies["sessionId"].Value;
            Users LoginUser = _UsersService.GetCurrentUserBySessionId(sessionId);
            return Json(LoginUser, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrEdit([Bind(Include = "Id,UserName,PassWord,Right,CreateTime")]Model.Models.Users users)
        {
            users.CreateTime = DateTime.Now;
            bool issucessful = false;
            //Id值为0则是新增，其他为修改
            if (users.Id == 0)
            {
                Model.Models.Users addusers = new Model.Models.Users()
                {
                    UserName = users.UserName,
                    PassWord = users.PassWord,
                    Right = users.Right,
                    CreateTime = users.CreateTime
                };
                issucessful = _UsersService.AddEntity(addusers);
            }
            else
            {
                issucessful = _UsersService.EditEntity(users);
            }

            AjaxResult ajaxResult = null;
            if (issucessful)
            {
                ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Success,
                    PromptMsg = "插入成功"
                };
            }
            else
            {
                ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Failed,
                    PromptMsg = "插入失败"
                };
            }

            return Json(ajaxResult);
        }

        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id,UserName,PassWord,Right,CreateTime")]Model.Models.Users[] userList)
        {
            foreach (Model.Models.Users user in userList)
            {
                _UsersService.DeleteEntity(user);
            }
            AjaxResult ajaxResult = new AjaxResult()
            {
                Result = DoResult.Success,
                PromptMsg = "删除成功"
            };
            return Json(ajaxResult);
        }
    }
}