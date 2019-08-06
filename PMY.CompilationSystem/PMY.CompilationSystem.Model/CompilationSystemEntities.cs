using PMY.CompilationSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Model
{
    public class CompilationSystemEntities : DbContext
    {
        //在webConfig的 <connectionStrings />里配置name="CompilationSystemEntities" ,即可使用该实体模型
        public CompilationSystemEntities() : base("CompilationSystemEntities")
        {

        }

        public DbSet<Users> Users { get; set; }

        public DbSet<SVNFolders> SVNFolders { get; set; }

        public DbSet<Projects> Projects { get; set; }

        public DbSet<ProjectClass> ProjectClass { get; set; }

        public DbSet<TaskList> TaskList { get; set; }
        
    }
}
