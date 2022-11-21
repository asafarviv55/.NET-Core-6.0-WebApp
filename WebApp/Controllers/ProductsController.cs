using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [HttpGet]
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            products.Add(new Product()
            {
                ProductID = 1,
                ProductName = "Shoe A",
                SKU = "SHOEAAA1"
            });
            products.Add(new Product()
            {
                ProductID =2,
                ProductName = "Shoe B",
                SKU = "SHOEBBB1"
            });
            return products;
        }
    }
}
