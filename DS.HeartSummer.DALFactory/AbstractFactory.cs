using DS.HeartSummer.DAL; 
using System.Configuration; 
using System.Reflection; 

namespace DS.HeartSummer.DALFactory
{
    public partial class AbstractFactory
    {
        private static readonly string assembly = ConfigurationManager.AppSettings["AssemblyPath"];
        private static readonly string nameSpace = ConfigurationManager.AppSettings["NameSpace"];
    

        private static object CreateInstans(string assembly, string fullname)
        {
            return Assembly.Load(assembly).CreateInstance(fullname);
        }
    }
}
