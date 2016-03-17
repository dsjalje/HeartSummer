using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.DAL
{
    public class BaseDAL<T> where T : class,new()
    {
        DbContext db = DbContextFactory.GetCurrentDbContext();
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where<T>(whereLambda).AsQueryable();
        }

        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderLambda, bool isAsc)
        {
            var temp = db.Set<T>().Where<T>(whereLambda).AsQueryable();
            totalCount = temp.Count(); 
            ///升序
            if (isAsc)
            {
                temp = temp.OrderBy(orderLambda).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            }
            else
            {

                temp = temp.OrderByDescending(orderLambda).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            }
            return temp;

        }

        public T AddEntity(T entity)
        {
            db.Set<T>().Add(entity);
            return entity;
        }

        public bool DeleteEntity(T entity)
        {
            db.Entry(entity).State = System.Data.EntityState.Deleted;
            return true;
        }

        public bool UpdateEntity(T entity)
        {
            db.Entry(entity).State = System.Data.EntityState.Modified;
            return true;
        }
    }
}
