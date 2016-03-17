 

using DS.HeartSummer.DALFactory;
using DS.HeartSummer.IBLL;
using DS.HeartSummer.IDAL;
using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.BLL
{
	
	public partial class UsersService :BaseService<Users>,IUsersService
    {
        public override void SetCurrentDAL()
        {
            this.CurrentDAL  = this.DbSession.UsersDAL;
        }
    }   
	
}