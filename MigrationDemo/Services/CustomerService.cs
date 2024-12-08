using MigrationDemo.Models;
using MigrationDemo.Repositories;

namespace MigrationDemo.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customerRepository.GetAllCustomers();
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await _customerRepository.GetCustomerById(customerId)
                   ?? throw new KeyNotFoundException("Customer not found.");
        }

        public async Task<List<Customer>> SearchCustomers(string query)
        {
            return await _customerRepository.SearchCustomers(query);
        }

        public async Task AddCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name) || string.IsNullOrWhiteSpace(customer.Company))
                throw new ArgumentException("Name and Company are required fields.");

            await _customerRepository.AddCustomer(customer);
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            return await _customerRepository.UpdateCustomer(customer);
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            return await _customerRepository.DeleteCustomer(customerId);
        }

        public async Task<List<Customer>> GetCustomersByStatus(string status)
        {
            return await _customerRepository.GetCustomersByStatus(status);
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            return await _customerRepository.GetCustomerByName(name);
        }
    }
}
