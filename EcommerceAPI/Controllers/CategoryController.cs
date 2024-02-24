using EcommerceAPI.Data;
using EcommerceAPI.Interfaces.Service;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            try
            {
                _categoryService.AddCategory(category);
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
            var categories = _categoryService.GetAllCategories();
            return StatusCode(StatusCodes.Status200OK, categories);
        }

        [HttpDelete("{name}")]

        public IActionResult DeleteCategory(string name)
        {
            try
            {
                _categoryService.DeleteCategoryByName(name);
                return StatusCode(StatusCodes.Status204NoContent, new { message = "category has been deleted" });
            }
            catch (CustomErrorException Ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, Ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }
    }
}
