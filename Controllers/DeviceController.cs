using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hepro.tech.webservices.events;
using Microsoft.AspNetCore.Mvc;

namespace hepro.tech.webservices.Controllers
{
    public class DeviceController : Controller
    {
        [HttpPost("/arm/")]
        public void ArmDevice()
        {

        }

        [HttpPost("/disarm/")]
        public void DisarmDevice()
        {

        }

        [HttpGet("/status/")]
        public void GetStatus()
        {

        }
    }
}
