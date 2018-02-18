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

            _deviceManager.DeviceHadEvent += async (s, e) => await OnDeviceEvent(s, e);
        }

        private async Task OnDeviceEvent(object sender, DeviceEvent deviceEvent)
        {
            switch(deviceEvent)
            {
                case var e when e.Type == EventTypes.MOTION_EVENT && int.Parse(e.Data) < 100:
                    await _deviceManager.ElevateSecurityAsync();
                    break;
                default:
                    return;
            }
        }
    }
}