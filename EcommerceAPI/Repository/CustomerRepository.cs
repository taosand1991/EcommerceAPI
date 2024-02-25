using EcommerceAPI.Data;
using EcommerceAPI.Dto;
using EcommerceAPI.Interfaces.Respository;
using EcommerceAPI.Models;

namespace EcommerceAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<CustomerDto> GetCustomers()
        {
            using var _context = new EcommerceContext();

            return _context.Customers.Select(customer => new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,

                Products = customer.Products.Select(product => new Product
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                }).ToList()
            }).ToList();
        }

        public Customer AddCustomer(Customer customer)
        {
            using var _context = new EcommerceContext();
            try
            {
                var hasedPassWord = PasswordEncrypt.EncodePasswordToBase64(customer.Password);
                customer.Password = hasedPassWord;
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return customer;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public CustomerDto GetCustomerById(int Id)
        {
            using var _context = new EcommerceContext();

            var customer = _context.Customers.Where(x => x.Id == Id).Select(customer => new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Admin = customer.Admin,
                PhoneNumber = customer.PhoneNumber
            }).FirstOrDefault();
            return customer ?? throw new CustomErrorException("Customer not found!");
        }

        public Customer GetCustomerByEmail(string Email)
        {
            using var _context = new EcommerceContext();

            return _context.Customers.Where(x => x.Email == Email).FirstOrDefault();
        }

        public void SaveNewPassword(string email, string password)
        {
            using var _context = new EcommerceContext();
            var userEmail = GetCustomerByEmail(email);

            if (userEmail != null)
            {
                var hashedPassword = PasswordEncrypt.EncodePasswordToBase64(password);

                userEmail.Password = hashedPassword;
                _context.SaveChanges();
            }
            else
            {
                throw new CustomErrorException("Email does not exist!!!");
            }
        }
    }
}
