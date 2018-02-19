using System;
using System.Linq;
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

        private async Task OnDeviceEvent(object sender, DeviceEvent e)
        {
            var latestMotionEvent = _eventHistory.GetLatestEventWhere(ev => ev.Type == EventTypes.MOTION_EVENT);
            var latestSecurityEvent = _eventHistory.GetLatestEventWhere(ev => ev.Type == "SECURITY");

            var latestReduce = _eventHistory.GetLatestEventWhere(ev => ev.Data == "REDUCE")?.Timestamp ?? DateTime.MinValue;
            var elevateCount = _eventHistory.GetEvents().Where(ev => ev.Data == "ELEVATE" && ev.Timestamp > latestReduce).Count();

            var latestElevate = _eventHistory.GetLatestEventWhere(ev => ev.Data == "ELEVATE")?.Timestamp ?? DateTime.MinValue;
            var reduceCount = _eventHistory.GetEvents().Where(ev => ev.Data == "REDUCE" && ev.Timestamp > latestElevate).Count();

            var latestNotifyTime = _eventHistory.GetLatestEventWhere(ev => ev.Type == "USER_NOTIFIED")?.Timestamp ?? DateTime.MinValue;

            if (e.Type == EventTypes.MOTION_EVENT && elevateCount <= 1) {
                _eventHistory.RecordEvent(DeviceEvent.Of(EventTypes.MOTION_EVENT));
                await _deviceManager.ElevateSecurityAsync();
            } else if (e.Type == EventTypes.PROXIMITY_EVENT) {
                var proximityLevel = int.Parse(e.Data);
                if (proximityLevel > 200 && reduceCount < 4) {
                    await _deviceManager.ReduceSecurityAsync();
                } else if (proximityLevel < 100 && elevateCount < 4) {
                    await _deviceManager.ElevateSecurityAsync();
                } else if (proximityLevel < 100 && latestNotifyTime < DateTime.Now.AddMinutes(-1)) {
                    _eventHistory.RecordEvent(DeviceEvent.Of("USER_NOTIFIED"));
                    await _notificationManager.BroadcastNotificationAsync("Someone is getting close to your Sensor Kit!");
                }
            }
        }
    }
}