using MigrationDemo.Models;
using MigrationDemo.Repositories;

namespace MigrationDemo.Services
{
    public class OpportunityService
    {
        private readonly IOpportunityRepository _opportunityRepository;

        private readonly ILeadRepository _leadRepository;

        private readonly ICustomerRepository _customerRepository;

        public OpportunityService(IOpportunityRepository opportunityRepository, ILeadRepository leadRepository, ICustomerRepository customerRepository)
        {
            _opportunityRepository = opportunityRepository;
            _leadRepository = leadRepository;
            _customerRepository = customerRepository;
        }

        public async Task<List<Oppurtunity>> GetAllOpportunities()
        {
            return await _opportunityRepository.GetAllOpportunities();
        }

        public async Task<List<Oppurtunity>> GetOpportunitiesByManager(int managerId)
        {
            return await _opportunityRepository.GetOpportunitiesByAccountManager(managerId);
        }

        public async Task<List<Oppurtunity>> GetOpportunitiesByCustomer(int customerId)
        {
            return await _opportunityRepository.GetOpportunitiesByCustomer(customerId);
        }

        public async Task<List<Oppurtunity>> GetOpportunitiesByStage(string stage)
        {
            return await _opportunityRepository.GetOpportunitiesByStage(stage);
        }

        public async Task<List<Oppurtunity>> GetOpportunitiesByHealth(string health)
        {
            return await _opportunityRepository.GetOpportunitiesByAccountHealth(health);
        }

        public async Task AddOpportunity(Oppurtunity opportunity)
        {
            if (opportunity.OpportunityValue <= 0)
                throw new ArgumentException("Opportunity value must be greater than zero.");

            await _opportunityRepository.AddOpportunity(opportunity);
        }

        public async Task<bool> UpdateOpportunity(Oppurtunity opportunity)
        {
            return await _opportunityRepository.UpdateOpportunity(opportunity);
        }

        public async Task<bool> DeleteOpportunity(int opportunityId)
        {
            return await _opportunityRepository.DeleteOpportunity(opportunityId);
        }

        public async Task<Oppurtunity> GetOpportunityById(int opportunityId)
        {
            return await _opportunityRepository.GetOpportunityById(opportunityId)
                   ?? throw new KeyNotFoundException("Opportunity not found.");
        }

        public async Task<Customer> ConvertOpportunityToCustomer(int opportunityId)
        {
            var opportunity = await _opportunityRepository.GetOpportunityById(opportunityId);

            if (opportunity == null)
                throw new KeyNotFoundException("Opportunity not found.");

            if (opportunity.Stage != "Open")
                throw new InvalidOperationException("Only open opportunities can be converted to customers.");

            if (!opportunity.CustomerId.HasValue)
                throw new InvalidOperationException("Opportunity must have a valid CustomerId (LeadId).");

            var lead = await _leadRepository.GetLeadById(opportunity.CustomerId.Value); 

            if (lead == null)
                throw new KeyNotFoundException("Lead associated with this opportunity not found.");

            var customer = new Customer
            {
                Name = lead.Name, 
                Company = "Not Defined", 
                Industry = "Sales", 
                ContactDetails = lead.ContactDetails, 
                AccountStatus = opportunity.Stage, 
                CreatedAt = DateTime.UtcNow 
            };

            await _customerRepository.AddCustomer(customer);

            return customer;
        }

    }
}
