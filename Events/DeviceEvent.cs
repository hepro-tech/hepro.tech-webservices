using System;

namespace HeProTech.Webservices.Events
{
    public class DeviceEvent
    {
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }

        public static DeviceEvent Of(string type, string data = "")
        {
            return new DeviceEvent
            {
                Type = type,
                Data = data,
                Timestamp = DateTime.Now
            };
        }
    }
}