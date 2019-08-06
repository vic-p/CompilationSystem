using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Model
{
    public enum UserRight
    {
        超级管理员 = 1,
        普通用户 = 2,
        游客 = 3
    }

    public enum TaskType
    {
        更新 = 1,
        编译 = 2,
        编译并发布 = 3,
        打包 = 4
    }

    public enum TaskStatus
    {
        未处理 = 1,
        处理中 = 2,
        已完成 = 3,
        出现错误 = 4,
        已取消
    }
}
