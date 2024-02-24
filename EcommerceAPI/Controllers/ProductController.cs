using EcommerceAPI.Data;
using EcommerceAPI.Interfaces.Service;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productService.GetProducts();
                return StatusCode(StatusCodes.Status200OK, products);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpPost]

        public IActionResult AddProduct([FromBody] ProductCategory product)
        {
            try
            {
                _productService.AddProduct(product);
                return StatusCode(StatusCodes.Status201Created, new { message = "Product has been created" });
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
                var product = _productService.GetProductById(Id);
                if (product != null)
                    return StatusCode(StatusCodes.Status200OK, product);
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Product is not found" });
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpDelete("{Id}")]

        public IActionResult DeleteProduct(string Id)
        {
            try
            {
                _productService.DeleteProductById(int.Parse(Id));
                return StatusCode(StatusCodes.Status204NoContent);
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

        [HttpPut("{id}")]

        public IActionResult UpdateProduct([FromBody] Product product, int Id)
        {
            try
            {
                _productService.UpdateProduct(Id, product);
                return StatusCode(StatusCodes.Status200OK, "Product has been updated");
            }
            catch (CustomErrorException Ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, Ex.Message);
            }
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return StatusCode(StatusCodes.Status200OK, $"product is Okay - {envName}");
        }
    }
}
