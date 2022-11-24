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
            List<Product> products = DbConn.GetProducts();

            return products;
        }



        [HttpGet]
        public Product EditProduct(Product product)
        {
            Console.WriteLine(product.id);
            Console.WriteLine(product.name);
            Console.WriteLine(product.description);
            Console.WriteLine(product.sell_date);

            return product;
        }



        [HttpPost]
        public Product DeleteProduct(Product product)
        {
            Console.WriteLine(product.id);
            Console.WriteLine(product.name);
            Console.WriteLine(product.description);
            Console.WriteLine(product.sell_date);

            return product;
        }


    }
}
