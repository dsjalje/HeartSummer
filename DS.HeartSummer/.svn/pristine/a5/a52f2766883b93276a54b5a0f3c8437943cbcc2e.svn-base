using DS.HeartSummer.DAL;
using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.DALFactory
{
    public class AbstractFactory
    {
        private static readonly string assembly = ConfigurationManager.AppSettings["AssemblyPath"];
        private static readonly string nameSpace = ConfigurationManager.AppSettings["NameSpace"];
        public static UsersDAL CreateUsers()
        {


            string fullname = nameSpace + ".UsersDAL";

            return CreateInstans(assembly, fullname) as UsersDAL;
        }

        private static object CreateInstans(string assembly, string fullname)
        {
            return Assembly.Load(assembly).CreateInstance(fullname);
        }
    }
}
