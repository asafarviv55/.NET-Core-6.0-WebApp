using System.Data;
using System.Data.SqlClient;
using WebApp.Models;

namespace WebApp.db
{
    public class DbConn
    {



        public static List<Product> GetProductById(Product product)
        {

            return new List<Product>();
        }


        public static List<Product> GetAllProducts()
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
                conn.Close();
            }
            return products;
        }


        public static List<Product> GetProductsBySearch(String searchStr)
        {
            var products = new List<Product>();
            //to get the connection string 
            var connectionstring = "Server=localhost,1433;Database=storedb;User Id=sa;Password=wvyf3691!";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("SearchByName", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@str", searchStr);
                    con.Open();
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
                con.Close();
            }
            return products;
        }



    }
}
