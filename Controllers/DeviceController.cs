using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hepro.tech.webservices.events;
using hepro.tech.webservices.models;
using Microsoft.AspNetCore.Mvc;

namespace hepro.tech.webservices.Controllers
{
    public class DeviceController : Controller
    {
        private readonly DeviceManager _deviceManager;

        public DeviceController(DeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
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
            return new DeviceStatus
            {
                Name = "Cookie Jar",
                Armed = true
            };
        }
    }
}
