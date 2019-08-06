using PMY.CompilationSystem.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMY.CompilationSystem.Controllers
{   
    public class TaskOperationController : Controller
    {
        public PMY.CompilationSystem.IService.ITaskListService _TaskListService = null;
        public TaskOperationController(IService.ITaskListService TaskListService)
        {
            _TaskListService = TaskListService;
        }
        // GET: TaskOperation
        [MenuFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTaskList(int offset, int limit, DateTime createTime)
        {
            int totalCount;
            var taskList = _TaskListService.LoadPageEntities<int>(offset+1, limit, out totalCount, 
                s => s.CreateTime.Year==createTime.Year&&s.CreateTime.Month==createTime.Month&&s.CreateTime.Day==createTime.Day, 
                s => s.Id, false).ToList();
            var total = taskList.Count;
            return Json(new { total = totalCount, rows = taskList }, JsonRequestBehavior.AllowGet);
        }
    }
}