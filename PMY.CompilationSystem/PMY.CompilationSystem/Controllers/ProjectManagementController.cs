using PMY.CompilationSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PMY.CompilationSystem.Controllers
{
    public class ProjectManagementController : Controller
    {
        public IService.IProjectsService _ProjectsService = null;
        public ProjectManagementController(IService.IProjectsService ProjectsService)
        {
            _ProjectsService = ProjectsService;
        }
        // GET: ProjectManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProjects()
        {
            var listProjects = _ProjectsService.LoadEntities(s => s.Id > 0).OrderBy(s => s.Sort).ToList();
            //查询项目所在文件夹路径下的所有文件的最新时间赋予LatestModifiTime
            var total = listProjects.Count;
            return Json(new { total = total, rows = listProjects }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrEdit([Bind(Include = "Id,Sort,ProjectClassName,ProjectName,ProjectPath,LastCompileTime,LatestModifiTime,PublishPath,CreateTime")]Model.Models.Projects projects)
        {
            projects.CreateTime = DateTime.Now;
            projects.ProjectParentPath = Directory.GetParent(projects.ProjectPath).FullName;
            bool issucessful = false;
            //Id值为0则是新增，其他为修改
            if (projects.Id == 0)
            {
                Model.Models.Projects addprojects = new Model.Models.Projects()
                {
                    ProjectClassName = projects.ProjectClassName,
                    ProjectName = projects.ProjectName,
                    ProjectPath = projects.ProjectPath,
                    ProjectParentPath = projects.ProjectParentPath,
                    PublishPath=projects.PublishPath,
                    Sort = projects.Sort,
                    CreateTime = projects.CreateTime
                };
                issucessful = _ProjectsService.AddEntity(addprojects);
            }
            else
            {
                issucessful = _ProjectsService.EditEntity(projects);
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
        public ActionResult Delete([Bind(Include = "Id,Sort,ProjectClassName,ProjectName,ProjectPath,LastCompileTime,LatestModifiTime,PublishPath,CreateTime")]Model.Models.Projects[] projectsList)
        {
            foreach (Model.Models.Projects projects in projectsList)
            {
                _ProjectsService.DeleteEntity(projects);
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