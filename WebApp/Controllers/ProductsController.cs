using Microsoft.AspNetCore.Mvc;
using WebApp.db;
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
            List<Product> products = DbConn.Get();
            /* var products = new List<Product>();
             products.Add(new Product()
             {
                 code     = 1,
                 name = "Shoe A",
                 description = "SHOEAAA1"
             });
             products.Add(new Product()
             {
                 code = 2,
                 name = "Shoe B",
                 description = "SHOEBBB1"
             });*/
            return products;
        }
    }
}
