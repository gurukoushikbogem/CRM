using MigrationDemo.Models;

namespace MigrationDemo.Repositories
{
    public interface ILeadRepository
    {
        Task<List<Lead>> GetAllLeads();
        Task<Lead> GetLeadById(int leadId);
        Task<List<Lead>> GetLeadsByStatus(string status);
        Task AddLead(Lead lead);
        Task<bool> UpdateLead(Lead lead);
        Task<bool> UpdateLeadStatus(int leadId, string status);
        Task<bool> DeleteLead(int leadId);
        Task<List<Lead>> GetUnassignedLeads();

        Task<Lead>GetLeadByName(string name);

    }
}
