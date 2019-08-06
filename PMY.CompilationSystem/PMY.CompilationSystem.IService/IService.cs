

using PMY.CompilationSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.IService
{
   
	
	public partial interface IProjectClassService :IBaseService<ProjectClass>
    {
      
    }
	
	public partial interface IProjectsService :IBaseService<Projects>
    {
      
    }
	
	public partial interface ISVNFoldersService :IBaseService<SVNFolders>
    {
      
    }
	
	public partial interface ITaskListService :IBaseService<TaskList>
    {
      
    }
	
	public partial interface IUsersService :IBaseService<Users>
    {
      
    }
	
}