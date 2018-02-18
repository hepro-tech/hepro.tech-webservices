using System;
using System.Threading.Tasks;
using HeProTech.Webservices.Device;
using HeProTech.Webservices.Notifications;

namespace HeProTech.Webservices.Events
{
    public class EventTracker
    {
        private readonly DeviceManager _deviceManager;
        private readonly NotificationManager _notificationManager;

        public EventTracker(DeviceManager deviceManager, NotificationManager notificationManager)
        {
            _deviceManager = deviceManager;
            _notificationManager = notificationManager;

            _deviceManager.DeviceEvent += async (s, e) => await OnDeviceEvent(s, e);
        }

        private async Task OnDeviceEvent(object sender, DeviceEvent e)
        {
            if (e.Data == "SOMEBODY TOUCHA MY SPAGHET")
            {
                await _notificationManager.BroadcastNotificationAsync("SOMEBODY TOUCHA MY SPAGHET");
            }
        }
    }
}