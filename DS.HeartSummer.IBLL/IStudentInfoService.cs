using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.IBLL
{
    public partial interface IStudentInfoService : IBaseService<StudentInfo>
    {
        StudentInfo GetExistOrCreate(StudentInfo sti);
    }
}
