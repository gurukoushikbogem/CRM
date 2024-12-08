using MigrationDemo.Models;
using MigrationDemo.Repositories;

namespace MigrationDemo.Services
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<Notification>> GetUserNotifications(int userId)
        {
            return await _notificationRepository.GetNotificationsByUser(userId);
        }

        public async Task<List<Notification>> GetUnreadUserNotifications(int userId)
        {
            return await _notificationRepository.GetUnreadNotificationsByUser(userId);
        }

        public async Task AddNotification(Notification notification)
        {
            notification.Timestamp = DateTime.UtcNow;
            await _notificationRepository.AddNotification(notification);
        }

        public async Task<bool> MarkNotificationAsRead(int notificationId)
        {
            return await _notificationRepository.MarkAsRead(notificationId);
        }

    }
}
