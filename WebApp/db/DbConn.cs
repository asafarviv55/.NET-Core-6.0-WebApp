namespace WebApp.db
{
    public class DbConn
    {

        public static List<Product> Get()
        {
            var products = new List<Product>();
            //to get the connection string 
            var connectionstring = "";
            connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //build the sqlconnection and execute the sql command
            if (connectionstring == null)
                return null;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                string commandtext = "select pcode, name , description , sell_date from products";

                SqlCommand cmd = new SqlCommand(commandtext, conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var product = new Product()
                    {
                        pcode = Convert.ToInt32(reader["pcode"]),
                        name = reader["name"].ToString(),
                        description = reader["description"].ToString(),
                        sell_date = DateTime.Parse(reader["sell_date"].ToString())
                    };
                    products.Add(product);
                }
            }
            return products;
        }

        /*   public static void ReadAllSettings()
           {
               try
               {
                   var appSettings = System.Configuration.ConfigurationManager.AppSettings;

                   if (appSettings.Count == 0)
                   {
                       Console.WriteLine("AppSettings is empty.");
                   }
                   else
                   {
                       foreach (var key in appSettings.AllKeys)
                       {
                           Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                       }
                   }
               }
               catch (ConfigurationErrorsException)
               {
                   Console.WriteLine("Error reading app settings");
               }
           }*/

    }
}
