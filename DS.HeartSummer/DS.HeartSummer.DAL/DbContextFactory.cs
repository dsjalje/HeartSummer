using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.DAL
{
    /// <summary>
    /// 负责创建EF上下文对象、保证线程内唯一、多次请求只创建一个对象
    /// </summary>
    public class DbContextFactory
    {

        public static DbContext GetCurrentDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("DbContext");
            if (dbContext==null)
            {
                dbContext = new SummerModelContainer();
                CallContext.SetData("DbContext", dbContext);
            } 
            return dbContext;
        }

    }
}
