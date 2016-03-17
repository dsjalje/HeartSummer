using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.DALFactory
{
    public class DBSessionFactory
    {
        public static DBSession CreateDBSession()
        {
            DBSession dbSession = (DBSession)CallContext.GetData("dbSession");
            if (dbSession == null)
            {
                dbSession = new DBSession();
                CallContext.SetData("dbSession",dbSession);
            }
            return dbSession;
        }
    }
}
