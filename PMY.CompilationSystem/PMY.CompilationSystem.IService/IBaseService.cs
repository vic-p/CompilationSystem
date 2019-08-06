using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.IService
{
    public interface IBaseService<T> where T:class,new()
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 查询单页数据
        /// </summary>
        /// <typeparam name="orderEntity">按orderEntity排序</typeparam>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="totalCount">总数据条数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否顺序</param>
        /// <returns></returns>
        IQueryable<T> LoadPageEntities<orderEntity>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, orderEntity>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteEntity(T entity);

        /// <summary>
        /// 编辑一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool EditEntity(T entity);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool AddEntity(T entity);

    }
}
