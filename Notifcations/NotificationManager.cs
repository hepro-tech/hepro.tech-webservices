using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OneSignal.CSharp.SDK;
using OneSignal.CSharp.SDK.Resources;
using OneSignal.CSharp.SDK.Resources.Notifications;

namespace HeProTech.Webservices.Notifications
{
    public class NotificationManager
    {
        private readonly string _appId;
        private readonly OneSignalClient _notificationClient;

        public NotificationManager(IConfiguration configuration)
        {
            _notificationClient = new OneSignalClient(configuration["ONE_CLIENT_API_KEY"]);
            _appId = configuration["ONE_CLIENT_APP_ID"];
        }

        public async Task BroadcastNotificationAsync(string notifcationMessage)
        {
            var options = new NotificationCreateOptions();
            options.AppId = Guid.Parse(_appId);
            options.IncludedSegments = new List<string> { "All" };
            options.Contents.Add(LanguageCodes.English, notifcationMessage);
            options.DeliverToAndroid = true;

            await Task.Run(() => _notificationClient.Notifications.Create(options));
        }
    }
}