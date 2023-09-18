using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public CategoryController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, category);
            }
            catch (Exception Ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Ex.InnerException?.Message);
            }
        }

        [HttpGet]

        public IActionResult GetCategories()
        {
            var categories = _context.Categories.Select(category => new 
            {
                id = category.Id,
                name = category.Name,
                type = category.Type,

                Products = category.Products.Select(product => new
                {
                    product.Id,
                    product.ProductName,
                    product.ProductDescription,
                    product.ProductPrice,
                })
            });
            return StatusCode(StatusCodes.Status200OK, categories);
        }
    }
}
