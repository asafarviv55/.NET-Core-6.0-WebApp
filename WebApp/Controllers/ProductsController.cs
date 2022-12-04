using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [EnableCors("http://localhost:8080")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {




        [HttpGet]
        public AProducts InitialProducts()
        {
            ProductService.RecreateDB();
            ProductService.InitialDB();
            return ProductService.GetAllProducts();
        }


        [HttpGet]
        public AProducts GetAllProducts()
        {
            AProducts aProducts = ProductService.GetAllProducts();

            return aProducts;
        }


        [HttpGet]
        public AProducts RefreshProducts()
        {
            AProducts aProducts = ProductService.GetAllProducts();

            return aProducts;
        }




        [HttpPost]
        public void FileUpload(IFormCollection formData)
        {
            ProductService.FileUpload(formData);
        }



        [HttpGet]
        public List<Product> Paging(int offset, int rowsPerPage)
        {
            List<Product> products = ProductService.Paging(offset, rowsPerPage);
            return products;
        }


        [HttpGet]
        public List<Product> GetAllProductsOrderBy(int orderCol, int orderDirection)
        {
            List<Product> products = ProductService.GetAllProductsOrderBy(orderCol, orderDirection);


            return products;
        }


        [HttpGet]
        public void AddNewProduct(int code, String name, String description, String imagePath)
        {
            ProductService.AddNewProduct(code, name, description, imagePath);
        }


        [HttpGet]
        public void UpdateProduct(int id, int code, String name, String description, String imagePath)
        {
            ProductService.UpdateProduct(id, code, name, description, imagePath);
        }


        [HttpGet]
        public List<Product> SearchProducts(String str)
        {
            List<Product> products = ProductService.searchProduct(str);


            return products;
        }




        [HttpPost]
        public void DeleteProducts(IFormCollection form)
        {
            form.TryGetValue("id", out StringValues ids);
            ProductService.DeleteProducts(ids);
        }








    }
}
