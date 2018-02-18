using System.Collections.Generic;
using System.Linq;

namespace HeProTech.Webservices.Events
{
    public class EventHistory
    {
        private const int HISTORY_SIZE = 10000;
        private readonly List<DeviceEvent> _deviceEvents;

        public EventHistory()
        {
            _deviceEvents = new List<DeviceEvent>();
        }

        public IEnumerable<DeviceEvent> GetEvents()
        {
            return _deviceEvents;
        }

        public DeviceEvent GetLatestEvent()
        {
            return GetEvents().First();
        }

        public void RecordEvent(DeviceEvent deviceEvent)
        {
            if (_deviceEvents.Count == HISTORY_SIZE)
            {
                _deviceEvents.RemoveAt(HISTORY_SIZE - 1);
            }
            _deviceEvents.Insert(0, deviceEvent);
        }
    }
}