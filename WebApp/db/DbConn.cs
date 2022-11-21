using Azure;
using System.Data.SqlClient;

namespace WebApp.db
{
    public class DbConn
    {
        public void Get()
        {
            string connetionString;
            SqlConnection conn;

            connetionString = @"Data Source=localhost;Initial Catalog=storedb; User ID=sa; Password=Wvyf3691!";

            conn = new SqlConnection(connetionString);

            conn.Open();

            Response.Write("Connection MAde");
            conn.Close();
        }
        
    }
}
