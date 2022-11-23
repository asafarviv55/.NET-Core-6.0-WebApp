using System.Data.SqlClient;
using WebApp.Models;

namespace WebApp.db
{
    public class DbConn
    {



        public static List<Product> GetProducts1()
        {

            return new List<Product>();
        }


        public static List<Product> GetProducts()
        {
            var products = new List<Product>();
            //to get the connection string 
            var connectionstring = "Server=localhost,1433;Database=storedb;User Id=sa;Password=wvyf3691!";
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                string commandtext = "select id, code, name , description , sell_date from products";

                SqlCommand cmd = new SqlCommand(commandtext, conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var product = new Product()
                    {
                        id = Convert.ToInt32(reader["id"]),
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
