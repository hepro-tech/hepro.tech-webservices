using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hepro.tech.webservices.events;
using Microsoft.AspNetCore.Mvc;

namespace hepro.tech.webservices.Controllers
{
    public class EventsController : Controller
    {
        [HttpPost("/events/")]
        public void PostEvent([FromBody] EventBase eventBase)
        {

        }
    }
}
