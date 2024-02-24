using EcommerceAPI.Dto;
using EcommerceAPI.Models;

namespace EcommerceAPI.Interfaces.Respository
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByEmail(string Email);
        CustomerDto GetCustomerById(int Id);
        Customer AddCustomer(Customer customer);
        List<CustomerDto> GetCustomers();
        void SaveNewPassword(string email, string password);
    }
}
