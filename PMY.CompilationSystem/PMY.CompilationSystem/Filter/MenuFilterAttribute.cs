using PMY.CompilationSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMY.CompilationSystem.Filter
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class MenuFilterAttribute : ActionFilterAttribute
    {
        private string _LoginPath = "";
        public MenuFilterAttribute()
        {
            this._LoginPath = "/Login/Index";
        }

        public MenuFilterAttribute(string loginPath)
        {
            this._LoginPath = loginPath;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isSucess = false;
            if (HttpContext.Current.Request.Cookies["sessionId"] != null)
            {
                string sessionId = HttpContext.Current.Request.Cookies["sessionId"].Value;
                //根据该值查Memcache.
                object obj = Common.MemcacheHelper.Get(sessionId);
                if (obj != null)
                {
                    string strURI = HttpContext.Current.Request.RawUrl;
                    Users LoginUser = Common.SerializeHelper.DeserializeToObject<Users>(obj.ToString());
                    IService.IMenuService menuService = PMY.CommonUnity.UnitySingleton.CreateUnitySingleton("MyContainer", "Config\\unity.Config").GetInstance<IService.IMenuService>();
                    Dictionary<string, string> dicMenu = menuService.GetMenuByRight(LoginUser.Right);
                    if (dicMenu.Values.Contains(strURI))
                    {
                        isSucess = true;
                    }                   

                }

            }
            if (!isSucess)
            {
                filterContext.Result = new RedirectResult(this._LoginPath);//注意.
            }
        }
    }
}