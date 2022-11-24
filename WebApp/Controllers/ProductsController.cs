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
            List<Product> products = DbConn.GetAllProducts();

            return products;
        }


        [HttpGet]
        public List<Product> RefreshProducts(String str)
        {
            List<Product> products = DbConn.GetProductsBySearch(str);


            return products;
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
