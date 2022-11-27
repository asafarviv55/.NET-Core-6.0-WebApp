using System.Data;
using System.Data.SqlClient;
using WebApp.Models;

namespace WebApp.db
{
    public class ProductService
    {


        public static void RecreateDB()
        {

            //to get the connection string 
            var connectionstring = "Server=localhost,1433;Database=storedb;User Id=sa;Password=wvyf3691!";

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    string commandtext = "RecreateDB";


                    SqlCommand cmd = new SqlCommand(commandtext, conn);


                    cmd.ExecuteNonQuery();

                }
                finally
                {
                    conn.Close();

                }

            }

        }

        public static List<Product> InitialDB()
        {
            var products = new List<Product>();
            //to get the connection string 
            var connectionstring = "Server=localhost,1433;Database=storedb;User Id=sa;Password=wvyf3691!";

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("code", typeof(int)));
                    dt.Columns.Add(new DataColumn("name", typeof(string)));
                    dt.Columns.Add(new DataColumn("description", typeof(string)));
                    dt.Columns.Add(new DataColumn("sell_date", typeof(DateTime)));

                    for (int i = 1; i <= 10; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["code"] = i;
                        dr["name"] = i + " name";
                        dr["description"] = "product description " + i;
                        dr["sell_date"] = DateTime.Now;

                        dt.Rows.Add(dr);
                    }
                    //create object of SqlBulkCopy which help to insert  
                    SqlBulkCopy objbulk = new SqlBulkCopy(conn);

                    //assign Destination table name  
                    objbulk.DestinationTableName = "products";


                    objbulk.ColumnMappings.Add("code", "code");
                    objbulk.ColumnMappings.Add("name", "name");
                    objbulk.ColumnMappings.Add("description", "description");
                    objbulk.ColumnMappings.Add("sell_date", "sell_date");


                    //insert bulk Records into DataBase.  
                    objbulk.WriteToServer(dt);
                    conn.Close();
                }
                finally
                {
                    conn.Close();
                }
            }
            return products;
        }



        public static List<Product> AddProduct(int code, String name, String description)
        {
            var products = new List<Product>();
            //to get the connection string 
            var connectionstring = "Server=localhost,1433;Database=storedb;User Id=sa;Password=wvyf3691!";
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string commandtext = "AddProduct " + code + " , '" + name + "' , '" + description + "' ,'" + DateTime.Now + "' ";

                    SqlCommand cmd = new SqlCommand(commandtext, conn);

                    var reader = cmd.ExecuteNonQuery();

                }
                finally
                {
                    conn.Close();
                }
            }
            return products;
        }

        public static List<Product> updateProduct()
        {
            var products = new List<Product>();
            //to get the connection string 
            var connectionstring = "Server=localhost,1433;Database=storedb;User Id=sa;Password=wvyf3691!";
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string commandtext = "updateProduct ";

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
                finally
                {
                    conn.Close();
                }
            }
            return products;
        }


        public static List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            //to get the connection string 
            var connectionstring = "Server=localhost,1433;Database=storedb;User Id=sa;Password=wvyf3691!";
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string commandtext = "getAllProducts";

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

                finally
                {
                    conn.Close();
                }
            }
            return products;
        }

        public static void DeleteProduct(int id)
        {
            var products = new List<Product>();
            //to get the connection string 
            var connectionstring = "Server=localhost,1433;Database=storedb;User Id=sa;Password=wvyf3691!";
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string commandtext = "DeleteProduct " + id;

                    SqlCommand cmd = new SqlCommand(commandtext, conn);

                    cmd.ExecuteNonQuery();

                }
                finally
                {
                    conn.Close();
                }

            }

        }

        public static List<Product> GetAllProductsOrderBy(int orderCol, int orderDirection)
        {
            var products = new List<Product>();
            var connectionstring = "Server=localhost,1433;Database=storedb;User Id=sa;Password=wvyf3691!";
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                string commandtext = "";
                commandtext = "getProductsOrderByColumn " + "'" + getColumnNameByNumber(orderCol) + "' , '" + getOrderDirectionByNumber(orderDirection) + "' ";



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

        public static List<Product> searchProduct(String searchStr)
        {
            var products = new List<Product>();
            //to get the connection string 
            var connectionstring = "Server=localhost,1433;Database=storedb;User Id=sa;Password=wvyf3691!";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {

                using (SqlCommand cmd = new SqlCommand("SearchByNameOrCode", con))
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

        private static String getOrderDirectionByNumber(int col)
        {
            return col == 0 ? "asc" : "desc";
        }

        private static String getColumnNameByNumber(int col)
        {
            switch (col)
            {
                case 1:
                    return "code";
                case 2:
                    return "name";
                case 3:
                    return "description";
                default:
                    return "sell_date";
            }
        }

    }
}
