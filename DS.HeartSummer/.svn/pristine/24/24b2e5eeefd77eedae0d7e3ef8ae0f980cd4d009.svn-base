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
    public partial class UsersService : BaseService<Users>, IUsersService
    {

        public Users AddEntity(Users entity,out string msg)
        {
            if (!IsExistUser(entity.LoginId))
            {
                //没有相同的
                this.DbSession.UsersDAL.AddEntity(entity);
                this.DbSession.SaveChanges();
                msg = "添加成功！";
                return entity;
            }
            else
            {
                msg = "用户名重复";
                return null;
            } 
        }
        /// <summary>
        /// 用户名查同
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        private bool IsExistUser(string loginId)
        {
            return this.DbSession.UsersDAL.LoadEntities(c => c.LoginId == loginId).Count() > 0;  
        }
    }
}
