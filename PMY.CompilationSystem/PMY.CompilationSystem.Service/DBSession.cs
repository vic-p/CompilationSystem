using PMY.CompilationSystem.IService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Service
{
    public class DBSession : IDBSession
    {
        //DbContext CompilationSystemEntities = DbContextFactory.CreateCompilationSystemEntities();
        private DbContext CompilationSystemEntities { get; set; }
        public DBSession(DbContext context)
        {
            CompilationSystemEntities = context;
        }

        public List<T> ExecuteQuery<T>(string sql, params SqlParameter[] pars)
        {
            return CompilationSystemEntities.Database.SqlQuery<T>(sql, pars).ToList();
        }

        public int ExecuteSqlComman(string sql, params SqlParameter[] pars)
        {
            return CompilationSystemEntities.Database.ExecuteSqlCommand(sql, pars);
        }

        public bool SaveChanges()
        {
            return CompilationSystemEntities.SaveChanges() > 0;
        }
    }
}
