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

        public IEnumerable<Customer> GetCustomers() 
        { 
            return _context.Customers.Include(x => x.Products);   
        }

        [HttpPost]

        public IActionResult AddCustomer (Customer customer) 
        {
            try
            {
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
                var customer = _context.Customers.Where(x => x.Id == Id).Include(x => x.Products);
                return StatusCode(StatusCodes.Status200OK, customer);
            }
            catch (Exception Ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }
    }
}
