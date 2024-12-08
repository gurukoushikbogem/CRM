using MigrationDemo.Models;

namespace MigrationDemo.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task<List<Customer>> SearchCustomers(string query);
        Task AddCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int customerId);
        Task<List<Customer>> GetCustomersByStatus(string status);
        Task<Customer> GetCustomerByName(string name);
    }
}
