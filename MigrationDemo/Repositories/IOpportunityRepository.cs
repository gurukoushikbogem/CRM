using MigrationDemo.Models;

namespace MigrationDemo.Repositories
{
    public interface IOpportunityRepository
    {
        Task<List<Oppurtunity>> GetAllOpportunities();
        Task<List<Oppurtunity>> GetOpportunitiesByAccountManager(int managerId);
        Task<List<Oppurtunity>> GetOpportunitiesByCustomer(int customerId);
        Task<List<Oppurtunity>> GetOpportunitiesByStage(string stage);
        Task<List<Oppurtunity>> GetOpportunitiesByAccountHealth(string health);
        Task AddOpportunity(Oppurtunity opportunity);
        Task<bool> UpdateOpportunity(Oppurtunity opportunity);
        Task<Oppurtunity> GetOpportunityById(int opportunityId);
        Task<bool> DeleteOpportunity(int opportunityId);
    }
}
