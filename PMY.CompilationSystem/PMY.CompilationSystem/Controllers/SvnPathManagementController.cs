using PMY.CompilationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PMY.CompilationSystem.Controllers
{
    public class SvnPathManagementController : Controller
    {
        public IService.ISVNFoldersService _SVNFoldersService = null;
        public SvnPathManagementController(IService.ISVNFoldersService SVNFoldersService)
        {
            _SVNFoldersService = SVNFoldersService;
        }
        // GET: SvnPathManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSVNFolders()
        {
            var listSVNFolders = _SVNFoldersService.LoadEntities(s => s.Id > 0).OrderBy(s=>s.Sort).ToList();
            var total = listSVNFolders.Count;
            return Json(new { total = total, rows = listSVNFolders }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrEdit([Bind(Include = "Id,FolderName,FolderPath,Sort,CreateTime")]Model.Models.SVNFolders svnFolders)
        {
            svnFolders.CreateTime = DateTime.Now;
            bool issucessful = false;
            //Id值为0则是新增，其他为修改
            if (svnFolders.Id == 0)
            {
                Model.Models.SVNFolders addsvnFolders = new Model.Models.SVNFolders()
                {
                    FolderName = svnFolders.FolderName,
                    FolderPath = svnFolders.FolderPath,
                    Sort = svnFolders.Sort,
                    CreateTime = svnFolders.CreateTime
                };
                issucessful = _SVNFoldersService.AddEntity(addsvnFolders);
            }
            else
            {
                issucessful = _SVNFoldersService.EditEntity(svnFolders);
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
        public ActionResult Delete([Bind(Include = "Id,FolderName,FolderPath,Sort,CreateTime")]Model.Models.SVNFolders[] svnFolderList)
        {
            foreach (Model.Models.SVNFolders svnfolder in svnFolderList)
            {
                _SVNFoldersService.DeleteEntity(svnfolder);
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