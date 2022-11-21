using System.Data.SqlClient;
using WebApp.Models;

namespace WebApp.db
{
    public class DbConn
    {

        public static List<Product> Get()
        {
            var products = new List<Product>();
            //to get the connection string 
            var connectionstring = "Server=localhost,1433;Database=storedb;User Id=sa;Password=wvyf3691!";
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                string commandtext = "select code, name , description , sell_date from products";

                SqlCommand cmd = new SqlCommand(commandtext, conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var product = new Product()
                    {
                        code = Convert.ToInt32(reader["code"]),
                        name = reader["name"].ToString(),
                        description = reader["description"].ToString(),
                        sell_date = DateTime.Parse(reader["sell_date"].ToString())
                    };
                    products.Add(product);
                }
            }
            return products;
        }



    }
}
