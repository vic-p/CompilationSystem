
using PMY.CompilationSystem.IService;
using PMY.CompilationSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Service
{
   
	
	public partial class ProjectClassService :BaseService<ProjectClass>, IProjectClassService
    {
	    public ProjectClassService(DbContext context, IDBSession dbSession) 
            : base(context, dbSession)
        {

        }     
    }
	
	public partial class ProjectsService :BaseService<Projects>, IProjectsService
    {
	    public ProjectsService(DbContext context, IDBSession dbSession) 
            : base(context, dbSession)
        {

        }     
    }
	
	public partial class SVNFoldersService :BaseService<SVNFolders>, ISVNFoldersService
    {
	    public SVNFoldersService(DbContext context, IDBSession dbSession) 
            : base(context, dbSession)
        {

        }     
    }
	
	public partial class TaskListService :BaseService<TaskList>, ITaskListService
    {
	    public TaskListService(DbContext context, IDBSession dbSession) 
            : base(context, dbSession)
        {

        }     
    }
	
	public partial class UsersService :BaseService<Users>, IUsersService
    {
	    public UsersService(DbContext context, IDBSession dbSession) 
            : base(context, dbSession)
        {

        }     
    }
	
}