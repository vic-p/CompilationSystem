using PMY.CompilationSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.IService
{
    public partial interface ITaskListService : IBaseService<TaskList>
    {
        /// <summary>
        /// 根据用户名取消用户任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int QuitTaskByCreator(string creator);
    }
}
