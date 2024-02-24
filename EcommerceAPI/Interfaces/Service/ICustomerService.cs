using EcommerceAPI.Dto;
using EcommerceAPI.Models;

namespace EcommerceAPI.Interfaces.Service
{
    public interface ICustomerService
    {
        Customer GetCustomerByEmail(string Email);
        CustomerDto GetCustomerById(int Id);
        Customer AddCustomer(Customer customer);
        List<CustomerDto> GetCustomers();
        void SaveNewPassword(string email, string password);
    }
}
