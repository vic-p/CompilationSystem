using PMY.CompilationSystem.Model;
using PMY.MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Service
{
    /// <summary>
    /// 创建EF数据上下文实例，保证线程内唯一（无用）:现改为交给Unity容器创建
    /// </summary>
    public class DbContextFactory
    {
        public static DbContext CreateMyBlogEntities()
        {
            DbContext myBlogEntities = (DbContext)CallContext.GetData("MyBlogEntities");
            if (myBlogEntities == null)
            {
                myBlogEntities = new CompilationSystemEntities();
                myBlogEntities.Database.CreateIfNotExists();
                CallContext.SetData("MyBlogEntities", myBlogEntities);
            }
            return myBlogEntities;
        }
    }
}
