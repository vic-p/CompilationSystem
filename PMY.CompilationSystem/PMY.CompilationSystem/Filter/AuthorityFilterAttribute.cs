using PMY.CompilationSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMY.CompilationSystem.Filter
{
    /// <summary>
    /// ajax跟exception一致
    /// 检验登陆和权限的filter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class AuthorityFilterAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 未登录时返还的地址
        /// </summary>
        private string _LoginPath = "";
        public AuthorityFilterAttribute()
        {
            this._LoginPath = "/Login/Index";
        }

        public AuthorityFilterAttribute(string loginPath)
        {
            this._LoginPath = loginPath;
        }



        /// <summary>
        /// 检查用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;//表示支持控制器、action的AllowAnonymousAttribute
            }

            //var sessionUser = HttpContext.Current.Session["CurrentUser"];//使用session
            ////var memberValidation = HttpContext.Current.Request.Cookies.Get("CurrentUser");//使用cookie
            ////也可以使用数据库、nosql等介质
            //if (sessionUser == null || !(sessionUser is CurrentUser))
            //{
            //    HttpContext.Current.Session["CurrentUrl"] = filterContext.RequestContext.HttpContext.Request.RawUrl;
            //    filterContext.Result = new RedirectResult(this._LoginPath);
            //}

            bool isSucess = false;
            if (HttpContext.Current.Request.Cookies["sessionId"] != null)
            {
                string sessionId = HttpContext.Current.Request.Cookies["sessionId"].Value;
                //根据该值查Memcache.
                object obj = Common.MemcacheHelper.Get(sessionId);
                if (obj != null)
                {
                    //Users LoginUser = Common.SerializeHelper.DeserializeToObject<Users>(obj.ToString());
                    isSucess = true;
                    Common.MemcacheHelper.Set(sessionId, obj, DateTime.Now.AddMinutes(20));//模拟出滑动过期时间.

                }

            }
            if (!isSucess)
            {
                filterContext.Result = new RedirectResult(this._LoginPath);//注意.
            }

        }

    }
}