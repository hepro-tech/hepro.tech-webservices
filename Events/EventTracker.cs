using System;
using System.Threading.Tasks;
using HeProTech.Webservices.Device;
using HeProTech.Webservices.Notifications;

namespace HeProTech.Webservices.Events
{
    public class EventTracker
    {
        private readonly DeviceManager _deviceManager;
        private readonly EventHistory _eventHistory;
        private readonly NotificationManager _notificationManager;

        public EventTracker(DeviceManager deviceManager, NotificationManager notificationManager, EventHistory eventHistory)
        {
            _deviceManager = deviceManager;
            _eventHistory = eventHistory;
            _notificationManager = notificationManager;

            _deviceManager.DeviceHadEvent += async (s, e) => await OnDeviceEvent(s, e);
        }

        private async Task OnDeviceEvent(object sender, DeviceEvent deviceEvent)
        {
            var latestSecurityEvent = _eventHistory.GetLatestEventWhere(ev => ev.Type == "SECURITY");

            switch(deviceEvent)
            {
                case var e when e.Type == EventTypes.MOTION_EVENT:
                    if (latestSecurityEvent?.Data == "ELEVATE") await _deviceManager.ElevateSecurityAsync();
                    break;
                case var e when e.Type == EventTypes.PROXIMITY_EVENT && int.Parse(e.Data) > 300:
                    if (latestSecurityEvent?.Data == "REDUCE") await _deviceManager.ReduceSecurityAsync();
                    break;
                default:
                    return;
            }
        }
    }
}