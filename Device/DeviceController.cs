using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeProTech.Webservices.Events;
using HeProTech.Webservices.Models;
using Microsoft.AspNetCore.Mvc;

namespace HeProTech.Webservices.Device
{
    public class DeviceController : Controller
    {
        private readonly DeviceManager _deviceManager;
        private readonly EventHistory _eventHistory;

        public DeviceController(DeviceManager deviceManager, EventHistory eventHistory)
        {
            _deviceManager = deviceManager;
            _eventHistory = eventHistory;
        }

        [HttpPost("/arm/")]
        public async Task ArmDeviceAsync()
        {
            await _deviceManager.ArmAsync();
        }

        [HttpPost("/disarm/")]
        public async Task DisarmDevice()
        {
            await _deviceManager.DisarmAsync();
        }

        [HttpGet("/status/")]
        public DeviceStatus GetStatus()
        {
            var armStatus = _eventHistory.GetLatestEventWhere(e => e.Type == "ARM")?.Data;
            var latestEvent = _eventHistory.GetLatestEvent();

            return new DeviceStatus
            {
                Name = "The Sensor Kit",
                Armed = armStatus == "ARM",
                Description = BuildDescriptionFrom(latestEvent),
                LatestEventTimestamp = latestEvent.Timestamp
            };
        }

        private string BuildDescriptionFrom(DeviceEvent deviceEvent)
        {
            switch (deviceEvent) {
                case DeviceEvent e when e.Type == "ARM" && e.Data == "ARM":
                    return "The device is armed!";
                case DeviceEvent e when e.Type == "ARM" && e.Data == "DISARM":
                    return "The device is not armed.";
                default:
                    return "Unknown";
            }
        }
    }
}
