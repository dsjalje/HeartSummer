 

using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.IDAL
{
    public partial interface IDBSession
    {

	
		IDefinedsDAL DefinedsDAL{get;set;}
	
		IFontAwesomeDAL FontAwesomeDAL{get;set;}
	
		ISettingsDAL SettingsDAL{get;set;}
	
		IStudentGradeDAL StudentGradeDAL{get;set;}
	
		IStudentGradesDAL StudentGradesDAL{get;set;}
	
		IStudentImageTableDAL StudentImageTableDAL{get;set;}
	
		IStudentInfoDAL StudentInfoDAL{get;set;}
	
		ITermsDAL TermsDAL{get;set;}
	
		ITimelineDAL TimelineDAL{get;set;}
	
		IUsersDAL UsersDAL{get;set;}
	}	
}