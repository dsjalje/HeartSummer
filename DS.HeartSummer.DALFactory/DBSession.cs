using DS.HeartSummer.DAL;
using DS.HeartSummer.IDAL;
using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.DALFactory
{
    /// <summary>
    /// 数据访问会话层，负责创建所有的数据操作类对象，业务层制药获取到DBSession，就拿到了所有的数据操作类的实例，让业务层与数据层进行解耦。
    /// </summary>
    public partial class DBSession : IDBSession
    {
        DbContext db = DbContextFactory.GetCurrentDbContext();
        //private IUsersDAL _usersDAL;

        //public IUsersDAL UsersDAL
        //{
        //    get
        //    {
        //        if (_usersDAL == null)
        //        {
        //            _usersDAL = AbstractFactory.CreateUsers();//抽象工程

        //        }
        //        return _usersDAL;
        //    }
        //    set { _usersDAL = value; }
        //}
        /// <summary>
        /// 执行特殊sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql, params SqlParameter []  pars)
        {
            return db.Database.ExecuteSqlCommand(sql, pars);
        }

        /// <summary>
        /// 由于可能涉及多个数据操作、一次性保存
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
}
