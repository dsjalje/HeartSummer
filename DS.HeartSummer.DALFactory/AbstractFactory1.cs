 

using DS.HeartSummer.DAL; 
using System.Configuration; 
using System.Reflection; 

namespace DS.HeartSummer.DALFactory
{
    public partial class AbstractFactory
    {
	
		 
		public static DefinedsDAL CreateDefineds()
        { 
            string fullname = nameSpace + ".DefinedsDAL"; 
            return CreateInstans(assembly, fullname) as DefinedsDAL;
        }



	
		 
		public static FontAwesomeDAL CreateFontAwesome()
        { 
            string fullname = nameSpace + ".FontAwesomeDAL"; 
            return CreateInstans(assembly, fullname) as FontAwesomeDAL;
        }



	
		 
		public static SettingsDAL CreateSettings()
        { 
            string fullname = nameSpace + ".SettingsDAL"; 
            return CreateInstans(assembly, fullname) as SettingsDAL;
        }



	
		 
		public static StudentGradeDAL CreateStudentGrade()
        { 
            string fullname = nameSpace + ".StudentGradeDAL"; 
            return CreateInstans(assembly, fullname) as StudentGradeDAL;
        }



	
		 
		public static StudentGradesDAL CreateStudentGrades()
        { 
            string fullname = nameSpace + ".StudentGradesDAL"; 
            return CreateInstans(assembly, fullname) as StudentGradesDAL;
        }



	
		 
		public static StudentImageTableDAL CreateStudentImageTable()
        { 
            string fullname = nameSpace + ".StudentImageTableDAL"; 
            return CreateInstans(assembly, fullname) as StudentImageTableDAL;
        }



	
		 
		public static StudentInfoDAL CreateStudentInfo()
        { 
            string fullname = nameSpace + ".StudentInfoDAL"; 
            return CreateInstans(assembly, fullname) as StudentInfoDAL;
        }



	
		 
		public static TermsDAL CreateTerms()
        { 
            string fullname = nameSpace + ".TermsDAL"; 
            return CreateInstans(assembly, fullname) as TermsDAL;
        }



	
		 
		public static TimelineDAL CreateTimeline()
        { 
            string fullname = nameSpace + ".TimelineDAL"; 
            return CreateInstans(assembly, fullname) as TimelineDAL;
        }



	
		 
		public static UsersDAL CreateUsers()
        { 
            string fullname = nameSpace + ".UsersDAL"; 
            return CreateInstans(assembly, fullname) as UsersDAL;
        }



	}	
}