using MigrationDemo.Models;

namespace MigrationDemo.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetNotificationsByUser(int userId);
        Task<List<Notification>> GetUnreadNotificationsByUser(int userId);
        Task AddNotification(Notification notification);
        Task<bool> MarkAsRead(int notificationId);

    }
}
