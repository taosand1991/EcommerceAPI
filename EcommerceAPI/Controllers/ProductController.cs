using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public ProductController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        [HttpPost]

        public IActionResult AddProduct(Product product)
        {
            try
            {
                _context.Customers.Single(x => x.Id == product.CustomerId).Products.Add(product);
                _context.Products.Add(product);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, product);
            }
            catch (Exception Ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Ex.InnerException?.Message);
            }
        }

        [HttpGet("{id}")]

        public IActionResult GetProduct(int Id)
        {
            try
            {
                var product = _context.Products.Where(x => x.Id == Id).Include(x => x.CustomerId);
                return StatusCode(StatusCodes.Status200OK, product);
            }
            catch (Exception Ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }
    }
}
