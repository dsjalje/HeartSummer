 

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
	public partial class DBSession : IDBSession
    {
	
		private IUsersDAL _UsersDAL;
        public IUsersDAL UsersDAL
        {
            get
            {
                if(_UsersDAL == null)
                {
                    _UsersDAL = new UsersDAL();
                }
                return _UsersDAL;
            }
            set { _UsersDAL = value; }
        }
	}	
}