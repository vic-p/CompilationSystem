using PMY.CompilationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PMY.CompilationSystem.Controllers
{
    public class ProjectClassManagementController : Controller
    {
        
        public IService.IProjectClassService _ProjectClassService = null;
        public ProjectClassManagementController(IService.IProjectClassService ProjectClassService)
        {
            _ProjectClassService = ProjectClassService;
        }
        // GET: ProjectClassManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProjectClass()
        {
            var listProjectClass = _ProjectClassService.LoadEntities(s => s.Id > 0).ToList();
            var total = listProjectClass.Count;
            return Json(new { total = total, rows = listProjectClass }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrEdit([Bind(Include = "Id,ClassName,CreateTime")]Model.Models.ProjectClass projectclass)
        {
            projectclass.CreateTime = DateTime.Now;
            bool issucessful = false;
            //Id值为0则是新增，其他为修改
            if (projectclass.Id == 0)
            {
                Model.Models.ProjectClass addProjectClass = new Model.Models.ProjectClass()
                {
                    ClassName = projectclass.ClassName,
                    CreateTime = projectclass.CreateTime
                };
                issucessful = _ProjectClassService.AddEntity(addProjectClass);
            }
            else
            {
                issucessful = _ProjectClassService.EditEntity(projectclass);
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
        public ActionResult Delete([Bind(Include = "Id,ClassName,CreateTime")]Model.Models.ProjectClass[] projectclassList)
        {
            foreach (Model.Models.ProjectClass ProjectClass in projectclassList)
            {
                _ProjectClassService.DeleteEntity(ProjectClass);
            }
            AjaxResult ajaxResult = new AjaxResult()
            {
                Result = DoResult.Success,
                PromptMsg = "删除成功"
            };
            return Json(ajaxResult);
        }

        public ActionResult GetProjectClassOption()
        {
            var listProjectClass = _ProjectClassService.LoadEntities(s => s.Id > 0).ToList();
            StringBuilder str = new StringBuilder();
            str.Append("<option> </option>");
            foreach (Model.Models.ProjectClass projectClass in listProjectClass)
            {
                str.Append("<option>");
                str.Append(projectClass.ClassName.ToString());
                str.Append("</option>");
            }
            AjaxResult ajaxResult = new AjaxResult()
            {
                Result = DoResult.Success,
                PromptMsg = "成功",
                Tag = str.ToString()
            };
            return Json(ajaxResult, JsonRequestBehavior.AllowGet);
        }

    }
}