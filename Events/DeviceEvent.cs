using System;

namespace HeProTech.Webservices.Events
{
    public class DeviceEvent
    {
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }
    }
}