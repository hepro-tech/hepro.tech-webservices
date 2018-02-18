using System;
using System.Threading.Tasks;
using HeProTech.Webservices.Events;
using Particle.SDK;
using Particle.SDK.Models;

namespace HeProTech.Webservices.Device
{
    public class DeviceManager
    {
        private readonly ParticleDevice _device;
        public event EventHandler<DeviceEvent> DeviceEvent;

        public DeviceManager(DeviceFactory deviceFactory)
        {
            _device = deviceFactory.GetDeviceAsync().Result;
            ParticleCloud.SharedCloud.SubscribeToDeviceEventsWithPrefixAsync(OnParticleEvent, _device.Id).Wait();
        }

        public async Task ArmAsync()
        {
            await PublishEventAsync("ARM");
        }

        public async Task DisarmAsync()
        {
            await PublishEventAsync("DISARM");
        }

        public async Task ElevateSecurityAsync()
        {
            await PublishEventAsync("ELEVATE");
        }

        public async Task ReduceSecurityAsync()
        {
            await PublishEventAsync("REDUCE");
        }

        private async Task PublishEventAsync(string name, string data = "")
        {
            await ParticleCloud.SharedCloud.PublishEventAsync(name, data);
        }

        private void OnParticleEvent(object sender, ParticleEventResponse particeEvent)
        {
            DeviceEvent?.Invoke(this, new DeviceEvent
            {
                Type = particeEvent.Name,
                Data = particeEvent.Data
            });
        }
    }
}