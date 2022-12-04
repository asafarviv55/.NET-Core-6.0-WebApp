namespace WebApp.Models
{
    public class Product
    {
        public int id { get; set; }
        public int code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string sell_date { get; set; }
        public string imagePath { get; set; }
    }
}