using System;

namespace HeProTech.Webservices.Models
{
    public class DeviceStatus
    {
        public string Name { get; set; }
        public bool Armed { get; set; }
        public string Description { get; set; }
        public DateTime LatestEventTimestamp { get; set;}
    }
}