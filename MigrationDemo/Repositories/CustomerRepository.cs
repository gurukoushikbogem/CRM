using MigrationDemo.Data;
using MigrationDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace MigrationDemo.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public CustomerRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await _dbContext.Customers.FindAsync(customerId);
        }

        public async Task<List<Customer>> SearchCustomers(string query)
        {
            return await _dbContext.Customers
                .Where(c => c.Name.Contains(query) || c.Company.Contains(query) || c.Industry.Contains(query))
                .ToListAsync();
        }

        public async Task AddCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            customer.UpdatedAt = DateTime.UtcNow;
            _dbContext.Customers.Update(customer);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<Customer>> GetCustomersByStatus(string status)
        {
            return await _dbContext.Customers.Where(c => c.AccountStatus == status).ToListAsync();
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Name == name);
        }

    }
}
