using PMY.CompilationSystem.IService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Service
{
    public class BaseService<T>: IBaseService<T> where T:class,new()
    {
        //DbContext CompilationSystemEntities = DbContextFactory.CreateCompilationSystemEntities();
        protected DbContext CompilationSystemEntities { get; private set; }
        public IDBSession DBSession { get; set; }
        public BaseService(DbContext context, IDBSession dbSession)
        {
            CompilationSystemEntities = context;
            DBSession = dbSession;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return CompilationSystemEntities.Set<T>().AsNoTracking<T>().Where<T>(whereLambda);
        }

        /// <summary>
        /// 查询单页数据
        /// </summary>
        /// <typeparam name="orderEntity">按orderEntity排序</typeparam>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="totalCount">总数据条数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<orderEntity>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, orderEntity>> orderbyLambda, bool isAsc)
        {
            var temp = CompilationSystemEntities.Set<T>().AsNoTracking<T>().Where<T>(whereLambda);
            totalCount = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy<T, orderEntity>(orderbyLambda).Skip<T>(pageIndex - 1).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, orderEntity>(orderbyLambda).Skip<T>(pageIndex - 1).Take<T>(pageSize);
            }
            return temp;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentException("entity");
                }
                if (CompilationSystemEntities.Entry(entity).State == EntityState.Detached)
                {
                    HandleDetached(entity);
                }
                CompilationSystemEntities.Set<T>().Attach(entity);
                CompilationSystemEntities.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
                //CompilationSystemEntities.Set<T>().Remove(entity);
                DBSession.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 编辑一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentException("entity");
                }
                if (CompilationSystemEntities.Entry(entity).State == EntityState.Detached)
                {
                    HandleDetached(entity);
                }
                CompilationSystemEntities.Set<T>().Attach(entity);
                CompilationSystemEntities.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
                DBSession.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddEntity(T entity)
        {
            try
            {
                CompilationSystemEntities.Set<T>().Add(entity);
                DBSession.SaveChanges();
                //return entity;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private bool HandleDetached(T entity)
        {
            var objectContext = ((IObjectContextAdapter)CompilationSystemEntities).ObjectContext;
            var entitySet = objectContext.CreateObjectSet<T>();
            var entityKey = objectContext.CreateEntityKey(entitySet.EntitySet.Name, entity);
            object foundSet;
            bool exists = objectContext.TryGetObjectByKey(entityKey, out foundSet);
            if (exists)
            {
                objectContext.Detach(foundSet); //从上下文中移除
            }
            return exists;
        }
    }
}
