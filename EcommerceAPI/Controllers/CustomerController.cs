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
        private readonly EcommerceContext _context;

        public CustomerController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetCustomers()
        {
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
            });
            return StatusCode(StatusCodes.Status200OK, customers);
        }

        [HttpPost]

        public IActionResult AddCustomer(Customer customer)
        {
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
            try
            {
                var customer = _context.Customers.Where(x => x.Id == Id).Select(customer => new
                {
                    customer.Id,
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                });
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
            var user = _context.Customers.Where(x => x.Email == miniCustomer.Email).FirstOrDefault();
            if(user != null)
            {
                var password = PasswordEncrypt.DecodeFrom64(user.Password);
                if(password != miniCustomer.Password)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new {message = "Email or password is Incorrect!!!"});
                }
                return StatusCode(StatusCodes.Status200OK, new {message = "You are logged in!!"});
            }
            return StatusCode(StatusCodes.Status400BadRequest, new { message = "Email or password is Incorrect!!!" });
        }
    }
}
