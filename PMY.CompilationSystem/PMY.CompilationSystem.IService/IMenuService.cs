using PMY.CompilationSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.IService
{
    public interface IMenuService
    {
        Dictionary<string, string> GetMenuByRight(UserRight userRight);
    }
}
