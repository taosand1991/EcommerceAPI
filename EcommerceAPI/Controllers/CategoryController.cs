using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
       
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            using var _context = new EcommerceContext();

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
            using var _context = new EcommerceContext();

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
            }).ToList();
            return StatusCode(StatusCodes.Status200OK, categories);
        }

        [HttpDelete("{name}")]

        public IActionResult DeleteCategory(string name) 
        {
            using var context = new EcommerceContext();
            try
            {
                var category = context.Categories.FirstOrDefault(cat => cat.Name == name);
                if(category != null) 
                {
                    context.Categories.Remove(category);
                    context.SaveChanges();
                    return StatusCode(StatusCodes.Status204NoContent, new {message = "category has been deleted"});
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);

            }
        }
    }
}
