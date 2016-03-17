 

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
	
		private IDefinedsDAL _DefinedsDAL;
        public IDefinedsDAL DefinedsDAL
        {
            get
            {
                if(_DefinedsDAL == null)
                {
				 
                    _DefinedsDAL = AbstractFactory.CreateDefineds();
                }
                return _DefinedsDAL;
            }
            set { _DefinedsDAL = value; }
        }
	
		private IFontAwesomeDAL _FontAwesomeDAL;
        public IFontAwesomeDAL FontAwesomeDAL
        {
            get
            {
                if(_FontAwesomeDAL == null)
                {
				 
                    _FontAwesomeDAL = AbstractFactory.CreateFontAwesome();
                }
                return _FontAwesomeDAL;
            }
            set { _FontAwesomeDAL = value; }
        }
	
		private ISettingsDAL _SettingsDAL;
        public ISettingsDAL SettingsDAL
        {
            get
            {
                if(_SettingsDAL == null)
                {
				 
                    _SettingsDAL = AbstractFactory.CreateSettings();
                }
                return _SettingsDAL;
            }
            set { _SettingsDAL = value; }
        }
	
		private IStudentGradeDAL _StudentGradeDAL;
        public IStudentGradeDAL StudentGradeDAL
        {
            get
            {
                if(_StudentGradeDAL == null)
                {
				 
                    _StudentGradeDAL = AbstractFactory.CreateStudentGrade();
                }
                return _StudentGradeDAL;
            }
            set { _StudentGradeDAL = value; }
        }
	
		private IStudentGradesDAL _StudentGradesDAL;
        public IStudentGradesDAL StudentGradesDAL
        {
            get
            {
                if(_StudentGradesDAL == null)
                {
				 
                    _StudentGradesDAL = AbstractFactory.CreateStudentGrades();
                }
                return _StudentGradesDAL;
            }
            set { _StudentGradesDAL = value; }
        }
	
		private IStudentImageTableDAL _StudentImageTableDAL;
        public IStudentImageTableDAL StudentImageTableDAL
        {
            get
            {
                if(_StudentImageTableDAL == null)
                {
				 
                    _StudentImageTableDAL = AbstractFactory.CreateStudentImageTable();
                }
                return _StudentImageTableDAL;
            }
            set { _StudentImageTableDAL = value; }
        }
	
		private IStudentInfoDAL _StudentInfoDAL;
        public IStudentInfoDAL StudentInfoDAL
        {
            get
            {
                if(_StudentInfoDAL == null)
                {
				 
                    _StudentInfoDAL = AbstractFactory.CreateStudentInfo();
                }
                return _StudentInfoDAL;
            }
            set { _StudentInfoDAL = value; }
        }
	
		private ITermsDAL _TermsDAL;
        public ITermsDAL TermsDAL
        {
            get
            {
                if(_TermsDAL == null)
                {
				 
                    _TermsDAL = AbstractFactory.CreateTerms();
                }
                return _TermsDAL;
            }
            set { _TermsDAL = value; }
        }
	
		private ITimelineDAL _TimelineDAL;
        public ITimelineDAL TimelineDAL
        {
            get
            {
                if(_TimelineDAL == null)
                {
				 
                    _TimelineDAL = AbstractFactory.CreateTimeline();
                }
                return _TimelineDAL;
            }
            set { _TimelineDAL = value; }
        }
	
		private IUsersDAL _UsersDAL;
        public IUsersDAL UsersDAL
        {
            get
            {
                if(_UsersDAL == null)
                {
				 
                    _UsersDAL = AbstractFactory.CreateUsers();
                }
                return _UsersDAL;
            }
            set { _UsersDAL = value; }
        }
	}	
}