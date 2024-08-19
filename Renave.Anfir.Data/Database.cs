using System.Configuration;

namespace Renave.Anfir.Data
{
    public class Database
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Renave.Anfir"].ConnectionString;
            }
        }
    }
}
