using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.IService
{
    public interface IDBSession
    {
        bool SaveChanges();

        int ExecuteSqlComman(string sql, params SqlParameter[] pars);

        List<T> ExecuteQuery<T>(string sql, params SqlParameter[] pars);
    }
}
