using DS.HeartSummer.IBLL;
using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.BLL
{
    public partial class StudentGradeService : BaseService<StudentGrade>, IStudentGradeService
    {


        //批量删除

        public bool DeleteEntities(System.Linq.Expressions.Expression<Func<StudentGrade, bool>> whereLambda)
        {
            if (CurrentDAL.LoadEntities(whereLambda).Count() > 0)
            {
                List<StudentGrade> stus = CurrentDAL.LoadEntities(whereLambda).ToList();
                foreach (StudentGrade stu in stus)
                {
                    CurrentDAL.DeleteEntity(stu);
                }

            }
            return DbSession.SaveChanges() > 0;
        }

    }

}