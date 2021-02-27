using System.Configuration;
using UaFDatabase;

namespace UaFootball.AppCode
{
    public class DBManager
    {
        public static UaFootball_DBDataContext GetDB()
        {
            return new UaFootball_DBDataContext(ConfigurationManager.ConnectionStrings["UaFDatabase"].ConnectionString);
        }
    }
}