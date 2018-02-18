using Microsoft.Extensions.Configuration;
using OneSignal.CSharp.SDK;

namespace HeProTech.Webservices.Notifications
{
    public class NotificationManager
    {
        private readonly OneSignalClient _notificationClient;

        public NotificationManager(IConfiguration configuration)
        {
            _notificationClient = new OneSignalClient("ONE_CLIENT_API_KEY");
        }
    }
}