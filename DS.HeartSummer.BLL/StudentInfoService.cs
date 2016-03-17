using DS.HeartSummer.IBLL;
using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.BLL
{
    public partial class StudentInfoService : BaseService<StudentInfo>, IStudentInfoService
    {
        public StudentInfo GetExistOrCreate(StudentInfo sti)
        {
            var s = CurrentDAL.LoadEntities(c => c.UserId == sti.UserId && c.UserName == sti.UserName).FirstOrDefault();

            if (s != null)
            {
                return s;
            }
            else
            {
                //create
                CurrentDAL.AddEntity(sti);
                DbSession.SaveChanges();
                return sti;
            }

        }
    }
}
