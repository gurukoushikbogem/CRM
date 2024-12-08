using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Filters;
using MigrationDemo.Models;
using MigrationDemo.Services;

namespace MigrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [JwtValidation]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [JwtValidation]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            return Ok(customer);
        }

        [HttpGet("search")]
        [JwtValidation]
        public async Task<IActionResult> SearchCustomers([FromQuery] string query)
        {
            var customers = await _customerService.SearchCustomers(query);
            return Ok(customers);
        }

        [HttpGet("status/{status}")]
        [JwtValidation]
        public async Task<IActionResult> GetCustomersByStatus(string status)
        {
            var customers = await _customerService.GetCustomersByStatus(status);
            return Ok(customers);
        }

        [HttpPost]
        [JwtValidation]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            await _customerService.AddCustomer(customer);
            return Ok(new { Message = "Customer added successfully." });
        }

        [HttpPut]
        [JwtValidation]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            var updated = await _customerService.UpdateCustomer(customer);
            if (updated)
                return Ok(new { Message = "Customer updated successfully." });
            return BadRequest(new { Message = "Failed to update customer." });
        }

        [HttpDelete("{id}")]
        [JwtValidation]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customerService.DeleteCustomer(id);
            if (deleted)
                return Ok(new { Message = "Customer deleted successfully." });
            return NotFound(new { Message = "Customer not found." });
        }

        [HttpGet("name/{name}")]
        [JwtValidation]
        public async Task<IActionResult> GetCustomerByName(string name)
        {
            var customer = await _customerService.GetCustomerByName(name);
            return Ok(customer);
        }

    }
}
