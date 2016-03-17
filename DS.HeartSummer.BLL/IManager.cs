 

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
	
	public partial class DefinedsService :BaseService<Defineds>,IDefinedsService
    {
        public override void SetCurrentDAL()
        {
            this.CurrentDAL  = this.DbSession.DefinedsDAL;
        }
    }   
	
	public partial class FontAwesomeService :BaseService<FontAwesome>,IFontAwesomeService
    {
        public override void SetCurrentDAL()
        {
            this.CurrentDAL  = this.DbSession.FontAwesomeDAL;
        }
    }   
	
	public partial class SettingsService :BaseService<Settings>,ISettingsService
    {
        public override void SetCurrentDAL()
        {
            this.CurrentDAL  = this.DbSession.SettingsDAL;
        }
    }   
	
	public partial class StudentGradeService :BaseService<StudentGrade>,IStudentGradeService
    {
        public override void SetCurrentDAL()
        {
            this.CurrentDAL  = this.DbSession.StudentGradeDAL;
        }
    }   
	
	public partial class StudentGradesService :BaseService<StudentGrades>,IStudentGradesService
    {
        public override void SetCurrentDAL()
        {
            this.CurrentDAL  = this.DbSession.StudentGradesDAL;
        }
    }   
	
	public partial class StudentImageTableService :BaseService<StudentImageTable>,IStudentImageTableService
    {
        public override void SetCurrentDAL()
        {
            this.CurrentDAL  = this.DbSession.StudentImageTableDAL;
        }
    }   
	
	public partial class StudentInfoService :BaseService<StudentInfo>,IStudentInfoService
    {
        public override void SetCurrentDAL()
        {
            this.CurrentDAL  = this.DbSession.StudentInfoDAL;
        }
    }   
	
	public partial class TermsService :BaseService<Terms>,ITermsService
    {
        public override void SetCurrentDAL()
        {
            this.CurrentDAL  = this.DbSession.TermsDAL;
        }
    }   
	
	public partial class TimelineService :BaseService<Timeline>,ITimelineService
    {
        public override void SetCurrentDAL()
        {
            this.CurrentDAL  = this.DbSession.TimelineDAL;
        }
    }   
	
	public partial class UsersService :BaseService<Users>,IUsersService
    {
        public override void SetCurrentDAL()
        {
            this.CurrentDAL  = this.DbSession.UsersDAL;
        }
    }   
	
}