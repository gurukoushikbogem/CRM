using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Filters;
using MigrationDemo.Models;
using MigrationDemo.Services;

namespace MigrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{userId}")]
        [JwtValidation]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var notifications = await _notificationService.GetUserNotifications(userId);
            return Ok(notifications);
        }

        [HttpGet("{userId}/unread")]
        [JwtValidation]
        public async Task<IActionResult> GetUnreadNotifications(int userId)
        {
            var notifications = await _notificationService.GetUnreadUserNotifications(userId);
            return Ok(notifications);
        }

        [HttpPost]
        [JwtValidation]
        public async Task<IActionResult> AddNotification([FromBody] Notification notification)
        {
            await _notificationService.AddNotification(notification);
            return Ok(new { Message = "Notification created successfully." });
        }

        [HttpPatch("{notificationId}/read")]
        [JwtValidation]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            var updated = await _notificationService.MarkNotificationAsRead(notificationId);
            if (updated)
                return Ok(new { Message = "Notification marked as read." });
            return NotFound(new { Message = "Notification not found." });
        }

    }
}
