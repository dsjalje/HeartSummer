using DS.HeartSummer.DALFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.IBLL
{
    public interface IBaseService<T> where T : class,new()
    {
        DBSession DbSession { get; }
  
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderLambda, bool isAsc);
        T AddEntity(T entity);
        bool DeleteEntity(T entity);
        bool UpdateEntity(T entity);
        bool DeleteEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);
    }
}
