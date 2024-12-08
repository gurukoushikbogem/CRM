using MigrationDemo.Models;
using MigrationDemo.Repositories;

namespace MigrationDemo.Services
{
    public class CommunicationHistoryService
    {
        private readonly ICommunicationHistoryRepository _historyRepository;

        public CommunicationHistoryService(ICommunicationHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task<List<CommunicationHistory>> GetAllInteractions()
        {
            return await _historyRepository.GetAllInteractions();
        }

        public async Task<CommunicationHistory> GetInteractionById(int interactionId)
        {
            return await _historyRepository.GetInteractionById(interactionId)
                   ?? throw new KeyNotFoundException("Interaction not found.");
        }

        public async Task<List<CommunicationHistory>> GetInteractionsByCustomerId(int customerId)
        {
            return await _historyRepository.GetInteractionsByCustomerId(customerId);
        }

        public async Task<List<CommunicationHistory>> GetPendingFollowUps(DateTime currentDate)
        {
            return await _historyRepository.GetPendingFollowUps(currentDate);
        }

        public async Task AddInteraction(CommunicationHistory interaction)
        {
            if (string.IsNullOrWhiteSpace(interaction.Notes))
                throw new ArgumentException("Interaction notes cannot be empty.");
            await _historyRepository.AddInteraction(interaction);
        }

        public async Task<bool> UpdateInteraction(CommunicationHistory interaction)
        {
            return await _historyRepository.UpdateInteraction(interaction);
        }

        public async Task<bool> DeleteInteraction(int interactionId)
        {
            return await _historyRepository.DeleteInteraction(interactionId);
        }

    }
}
