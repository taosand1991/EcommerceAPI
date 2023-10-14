using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;


namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet]

        public IActionResult GetProducts()
        {
            using var _context = new EcommerceContext();

            try
            {
                var products = _context.Products.Select(product => new
                {
                    id = product.Id,
                    productPrice = product.ProductPrice,
                    productName = product.ProductName,
                    productDescription = product.ProductDescription,
                    customerId = product.CustomerId,

                    Categories = product.Categories.Select(category => new
                    {
                        id = category.Id,
                        name = category.Name,
                        type = category.Type,
                    })
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, products);
            }
            catch (Exception Ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
            

        }

        [HttpPost]

        public IActionResult AddProduct([FromBody]ProductCategory product)
        {
            using var _context = new EcommerceContext();

            
            try
            {

                Product ProductData = new()
                {
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                    CustomerId = product.CustomerId,
                };
                foreach (var category in product.Categories)
                {
                    var existingCategory = _context.Categories.FirstOrDefault(x => x.Name == category.Name);

                    Console.WriteLine($"  ExistingCategory: {existingCategory}");

                    if (existingCategory != null)
                    {
                        ProductData.Categories.Add(existingCategory);

                    }
                }



                _context.Products.Add(ProductData);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, new {message = "Product has been created"});
            }
            catch (Exception Ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Ex.InnerException?.Message);
            }
        }

        [HttpGet("{id}")]

        public IActionResult GetProduct(int Id)
        {
            using var _context = new EcommerceContext();
            try
            {
                var product = _context.Products.Where(x => x.Id == Id).Select(product => new
                {
                    product.Id,
                    product.ProductName,
                    product.ProductDescription,
                    product.ProductPrice,
                    product.CustomerId,

                    Categories = product.Categories.Select(category => new
                    {
                        category.Name,
                        category.Type,
                    })
                }).FirstOrDefault();
                if(product != null)
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
            Console.WriteLine($"Name: {Id}");
            using var _context = new EcommerceContext();
            try
            {
                var product = _context.Products.Where(x => x.ProductName == Id).FirstOrDefault();

                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                return StatusCode(StatusCodes.Status404NotFound, new {message = "Product not found"});

            }
            catch (Exception Ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpPut("{id}")]

        public IActionResult UpdateProduct([FromBody] Product product, int Id)
        {
            using var _context = new EcommerceContext();
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"productName: {product.Categories.Count}");
            try
            {
                var productData = _context.Products.Find(Id);

                if(productData != null)
                {
                    productData.ProductName = product.ProductName;
                    productData.ProductDescription = product.ProductDescription;
                    productData.ProductPrice = product.ProductPrice;
                    productData.CustomerId = product.CustomerId;
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK, "Product has been updated");
                }
                return StatusCode(StatusCodes.Status404NotFound, "Product could not be found");
            }
            catch (Exception Ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest, Ex.InnerException?.Message);
            }
        }
    }
}
