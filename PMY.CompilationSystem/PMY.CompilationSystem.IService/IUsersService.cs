using PMY.CompilationSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.IService
{
    public partial interface IUsersService : IBaseService<Users>
    {
        Users GetCurrentUserBySessionId(string sessionId);
    }
}
