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
            //   List<Product> products = DbConn.Get();
            DbConn.ReadAllSettings();
            return null;
        }
    }
}
