using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        
        [HttpGet]

        public IActionResult GetCustomers()
        {
            using var _context = new EcommerceContext();

            var customers = _context.Customers.Select(customer => new
            {
                customer.Id,
                customer.FirstName,
                customer.LastName,
                customer.Email,

                Products = customer.Products.Select(product => new
                {
                    product.Id,
                    product.ProductName,
                    product.ProductDescription,
                    product.ProductPrice,
                })
            }).ToList();
            return StatusCode(StatusCodes.Status200OK, customers);
        }

        [HttpPost]

        public IActionResult AddCustomer(Customer customer)
        {
            using var _context = new EcommerceContext();
            try
            {
                var hasedPassWord = PasswordEncrypt.EncodePasswordToBase64(customer.Password);
                customer.Password = hasedPassWord;
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, customer);
            }
            catch (Exception Ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpGet("{id}")]

        public IActionResult GetCustomer(int Id)
        {
            using var _context = new EcommerceContext();

            try
            {
                var customer = _context.Customers.Where(x => x.Id == Id).Select(customer => new
                {
                    customer.Id,
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.Admin,
                }).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, customer);
            }
            catch (Exception Ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [Route("login")]
        [HttpPost]

        public IActionResult Login([FromBody] LoginData miniCustomer)
        {
            using var _context = new EcommerceContext();

            var user = _context.Customers.Where(x => x.Email == miniCustomer.Email).FirstOrDefault();

            if(user != null)
            {
                var password = PasswordEncrypt.DecodeFrom64(user.Password);
                if(password != miniCustomer.Password)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new {message = "Email or password is Incorrect!!!"});
                }
                return StatusCode(StatusCodes.Status200OK, user);
            }
            return StatusCode(StatusCodes.Status400BadRequest, new { message = "Email or password is Incorrect!!!" });
        }
    }
}
