using EcommerceAPI.Data;
using EcommerceAPI.Interfaces.Service;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]

        public IActionResult GetCustomers()
        {
            try
            {
                var customers = _customerService.GetCustomers();
                return StatusCode(StatusCodes.Status200OK, customers);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }
        }

        [HttpPost]

        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            try
            {
                _customerService.AddCustomer(customer);
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
                var customer = _customerService.GetCustomerById(Id);
                return StatusCode(StatusCodes.Status200OK, customer);
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

        [Route("login")]
        [HttpPost]

        public IActionResult Login([FromBody] LoginData miniCustomer)
        {

            var user = _customerService.GetCustomerByEmail(miniCustomer.Email);

            if (user != null)
            {
                var password = PasswordEncrypt.DecodeFrom64(user.Password);
                if (password != miniCustomer.Password)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Email or password is Incorrect!!!" });
                }
                return StatusCode(StatusCodes.Status200OK, user);
            }
            return StatusCode(StatusCodes.Status400BadRequest, new { message = "Email or password is Incorrect!!!" });
        }

        [Route("account/reset-password")]
        [HttpPost]

        public IActionResult ResetPassword([FromBody] string email)
        {
            try
            {
                var userEmail = _customerService.GetCustomerByEmail(email);

                if (userEmail != null)
                {
                    var twilio = new TwilioAPI();

                    twilio.CreateService();
                    twilio.SendVerificationToken(userEmail.PhoneNumber!);

                    return StatusCode(StatusCodes.Status200OK, new { message = true });
                }

                return StatusCode(StatusCodes.Status200OK, new { message = false });

            }
            catch (Exception Ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }

        }

        [Route("account/verify-code")]
        [HttpPost]
        public IActionResult VerifyCode([FromBody] string tokenCode, string email)
        {

            try
            {
                var userEmail = _customerService.GetCustomerByEmail(email);

                if (userEmail != null)
                {
                    var twilio = new TwilioAPI();

                    twilio.VerifyToken(userEmail.PhoneNumber!, tokenCode);

                    var Status = Environment.GetEnvironmentVariable("Status")!;

                    if (Status == "approved")
                    {
                        return StatusCode(StatusCodes.Status200OK, new { message = true });
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { message = false });
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Ex.Message);
            }

        }

        [Route("account/new-password")]
        [HttpPost]
        public IActionResult NewPassword([FromBody] string password, [FromQuery] string email)
        {
            try
            {
                _customerService.SaveNewPassword(email, password);
                return StatusCode(StatusCodes.Status200OK, new { message = "password has been reset" });
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
