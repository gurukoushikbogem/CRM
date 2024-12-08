using MigrationDemo.Models;

namespace MigrationDemo.Repositories
{
    public interface ICommunicationHistoryRepository
    {
        Task<List<CommunicationHistory>> GetAllInteractions();
        Task<CommunicationHistory> GetInteractionById(int interactionId);
        Task<List<CommunicationHistory>> GetInteractionsByCustomerId(int customerId);
        Task<List<CommunicationHistory>> GetPendingFollowUps(DateTime currentDate);
        Task AddInteraction(CommunicationHistory interaction);
        Task<bool> UpdateInteraction(CommunicationHistory interaction);
        Task<bool> DeleteInteraction(int interactionId);
    }
}
