using PMY.CompilationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMY.CompilationSystem.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public PMY.CompilationSystem.IService.IUsersService _UsersService = null;
        public LoginController(IService.IUsersService UsersService)
        {
            _UsersService = UsersService;

        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin()
        {
            //string validateCode = Session["validateCode"] != null ? Session["validateCode"].ToString() : string.Empty;
            //if (string.IsNullOrEmpty(validateCode))
            //{
            //    return Content("no:验证码错误!!");
            //}
            //Session["validateCode"] = null;
            //string txtCode = Request["vCode"];
            //if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
            //{
            //    return Content("no:验证码错误!!");
            //}
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            var userInfo = _UsersService.LoadEntities(u => u.UserName == userName && u.PassWord == userPwd).FirstOrDefault();//根据用户名找用户
            AjaxResult ajaxResult = null;
            if (userInfo != null)
            {
                // Session["userInfo"] = userInfo;
                //产生一个GUID值作为Memache的键.
                //  System.Web.Script.Serialization.JavaScriptSerializer
                string sessionId = Guid.NewGuid().ToString();
                Common.MemcacheHelper.Set(sessionId, Common.SerializeHelper.SerializeToString(userInfo)
                    , DateTime.Now.AddMinutes(20));//将登录用户信息存储到Memcache中。
                Response.Cookies["sessionId"].Value = sessionId;//将Memcache的key以Cookie的形式返回给浏览器。

                ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Success,
                    PromptMsg = "登录成功"
                };               
            }
            else
            {
                ajaxResult = new AjaxResult()
                {
                    Result = DoResult.Failed,
                    PromptMsg = "登录失败"
                };              
            }

            return Json(ajaxResult);
        }

        public ActionResult VisitorLogin()
        {
            Model.Models.Users visitor = new Model.Models.Users()
            {
                UserName = "游客",
                Right = Model.UserRight.游客
            };
            string sessionId = Guid.NewGuid().ToString();
            Common.MemcacheHelper.Set(sessionId, Common.SerializeHelper.SerializeToString(visitor)
                , DateTime.Now.AddMinutes(20));//将登录用户信息存储到Memcache中。
            Response.Cookies["sessionId"].Value = sessionId;//将Memcache的key以Cookie的形式返回给浏览器。

            AjaxResult ajaxResult = new AjaxResult()
            {
                Result = DoResult.Success,
                PromptMsg = "登录成功"
            };

            return Json(ajaxResult);
        }

        public ActionResult UserLoginout()
        {
            string sessionId = Request.Cookies["sessionId"].Value;
            if (!string.IsNullOrEmpty(sessionId))
            {
                Common.MemcacheHelper.Delete(sessionId);
                Response.Cookies["sessionId"].Value = string.Empty;
            }
            return RedirectToAction("Index");
        }
    }
}