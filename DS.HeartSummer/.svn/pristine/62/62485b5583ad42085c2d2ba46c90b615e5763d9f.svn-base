using DS.HeartSummer.DALFactory;
using DS.HeartSummer.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.BLL
{
    public abstract class BaseService<T> where T : class,new()
    {
        public DBSession DbSession
        {
            get
            {
                return new DBSession();
            }
        }
        public IBaseDAL<T> CurrentDAL { get; set; } //表示当前数据操作类的实例
        public abstract void SetCurrentDAL();//定义一个抽象方法
        public BaseService()
        {
            SetCurrentDAL();//保重子类重写父类中的抽象方法
        }
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDAL.LoadEntities(whereLambda);

        }
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderLambda, bool isAsc)
        {
            return CurrentDAL.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLambda, orderLambda, isAsc);
        }
        public T AddEntity(T entity)
        {
            CurrentDAL.AddEntity(entity);
            DbSession.SaveChanges();
            return entity;
        }
        public bool DeleteEntity(T entity)
        {
            CurrentDAL.DeleteEntity(entity);
            return DbSession.SaveChanges() > 0;
        }
        public bool UpdateEntity(T entity)
        {
            CurrentDAL.UpdateEntity(entity);

            return DbSession.SaveChanges() > 0;

        }

    }
}
