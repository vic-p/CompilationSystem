using PMY.CompilationSystem.IService;
using PMY.CompilationSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Service
{
    public partial class TaskListService : BaseService<TaskList>, ITaskListService
    {
        public int QuitTaskByCreator(string creator)
        {
            //5为‘已取消’状态；
            string sql = $"update TaskLists set TaskStatus = {(int)Model.TaskStatus.已取消} where TaskStatus ={(int)Model.TaskStatus.处理中} and  Creator = '{creator}'";
            return DBSession.ExecuteSqlComman(sql);

        }
    }
}
