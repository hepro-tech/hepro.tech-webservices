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
        private readonly EventHistory _eventHistory;
        public event EventHandler<DeviceEvent> DeviceEvent;

        public DeviceManager(DeviceFactory deviceFactory, EventHistory eventHistory)
        {
            _device = deviceFactory.GetDeviceAsync().Result;
            _eventHistory = eventHistory;
            ParticleCloud.SharedCloud.SubscribeToDeviceEventsWithPrefixAsync(OnParticleEvent, _device.Id).Wait();
        }

        public async Task ArmAsync()
        {
            await PublishEventAsync("ARM", "ARM");
        }

        public async Task DisarmAsync()
        {
            await PublishEventAsync("ARM", "DISARM");
        }

        public async Task ElevateSecurityAsync()
        {
            await PublishEventAsync("SECURITY", "ELEVATE");
        }

        public async Task ReduceSecurityAsync()
        {
            await PublishEventAsync("SECURITY", "REDUCE");
        }

        private async Task PublishEventAsync(string name, string data = "")
        {
            await ParticleCloud.SharedCloud.PublishEventAsync(name, data);
        }

        private void OnParticleEvent(object sender, ParticleEventResponse particeEvent)
        {
            var deviceEvent = new DeviceEvent
            {
                Type = particeEvent.Name,
                Data = particeEvent.Data,
                Timestamp = particeEvent.PublishedAt
            };

            _eventHistory.RecordEvent(deviceEvent);
            DeviceEvent?.Invoke(this, deviceEvent);
        }
    }
}