using EcommerceAPI.Dto;
using EcommerceAPI.Interfaces.Respository;
using EcommerceAPI.Interfaces.Service;
using EcommerceAPI.Models;

namespace EcommerceAPI.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public List<CustomerDto> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }
        public Customer AddCustomer(Customer customer)
        {
            return _customerRepository.AddCustomer(customer);
        }
        public Customer GetCustomerByEmail(string email)
        {
            return _customerRepository.GetCustomerByEmail(email);
        }
        public CustomerDto GetCustomerById(int id)
        {
            return _customerRepository.GetCustomerById(id);
        }

        public void SaveNewPassword(string email, string password)
        {
            _customerRepository.SaveNewPassword(email, password);
        }
    }
}
