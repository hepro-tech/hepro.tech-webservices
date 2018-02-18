using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HeProTech.Webservices.Notifications
{
    public class NotifcationTestContoller : Controller
    {
        private readonly NotificationManager _notificationManager;

        public NotifcationTestContoller(NotificationManager notifcationManager)
        {
            _notificationManager = notifcationManager;
        }

        [HttpPost("notify/{message}")]
        public async Task TestNotification(string message)
        {
            await _notificationManager.BroadcastNotificationAsync(message);
        }
    }
}