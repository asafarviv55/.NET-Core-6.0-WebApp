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
        public void InitialProducts()
        {
            ProductService.RecreateDB();
            ProductService.InitialDB();
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
        public void AddNewProduct(int code, String name, String description)
        {
            ProductService.AddProduct(code, name, description);
        }


        [HttpGet]
        public void UpdateProduct(int id, int code, String name, String description)
        {
            ProductService.updateProduct(id, code, name, description);
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



        [HttpPost]
        public void DeleteProducts(IFormCollection form)
        {
            form.TryGetValue("id", out StringValues ids);
            ProductService.DeleteProducts(ids);
        }








    }
}
