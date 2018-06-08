using System;
using MySql.Data.MySqlClient;
namespace Gift4U.Util
{
    public class DBManager
    {
        private MySqlConnection connection;
        private static string connectionString = "server=127.0.0.1;uid=root;" +
            "pwd=mysqlroot;database=Gift4U";
        private static DBManager dBManager;
        private DBManager()
        {

        }
        public static DBManager getManager()
        {
            if (dBManager == null)
            {
                dBManager = new DBManager();
            }
            return dBManager;
        }
        public MySqlConnection getConnection(){
            if(connection==null){
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            return connection;
        }
    }
}
