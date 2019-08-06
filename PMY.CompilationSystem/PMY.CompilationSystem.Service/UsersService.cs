using PMY.CompilationSystem.IService;
using PMY.CompilationSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Service
{
    public partial class UsersService : BaseService<Users>, IUsersService
    {
        public Users GetCurrentUserBySessionId(string sessionId)
        {
            Users LoginUser = null;
            //根据该值查Memcache.
            object obj =  Common.MemcacheHelper.Get(sessionId);
            if (obj != null)
            {
                LoginUser = Common.SerializeHelper.DeserializeToObject<Users>(obj.ToString());

            }
            return LoginUser;
        }
    }
}
