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
        public void InitialProducts()
        {
            ProductService.RecreateDB();
            ProductService.InitialDB();
        }


        [HttpGet]
        public List<Product> GetAllProducts()
        {
            List<Product> products = ProductService.GetAllProducts();

            return products;
        }


        [HttpGet]
        public List<Product> RefreshProducts()
        {
            List<Product> products = ProductService.GetAllProducts();


            return products;
        }


        [HttpGet]
        public List<Product> GetAllProductsOrderBy(int orderCol, int orderDirection)
        {
            List<Product> products = ProductService.GetAllProductsOrderBy(orderCol, orderDirection);


            return products;
        }


        [HttpGet]
        public List<Product> addNewProduct()
        {
            List<Product> products = ProductService.AddNewProduct();


            return products;
        }




        [HttpGet]
        public List<Product> SearchProducts(String str)
        {
            List<Product> products = ProductService.searchProduct(str);


            return products;
        }

        [HttpDelete]
        public void DeleteProduct(int id)
        {
            ProductService.DeleteProduct(id);
        }









    }
}
