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
        public event EventHandler<DeviceEvent> DeviceHadEvent;

        public DeviceManager(DeviceFactory deviceFactory, EventHistory eventHistory)
        {
            _device = deviceFactory.GetDeviceAsync().Result;
            _eventHistory = eventHistory;
            ParticleCloud.SharedCloud.SubscribeToDeviceEventsWithPrefixAsync(OnParticleEvent, _device.Id).Wait();
        }

        public async Task ArmAsync()
        {
            await PublishEventAsync("ARM", "ARM");
            _eventHistory.RecordEvent(DeviceEvent.Of("ARM", "ARM"));
        }

        public async Task DisarmAsync()
        {
            await PublishEventAsync("ARM", "DISARM");
            _eventHistory.RecordEvent(DeviceEvent.Of("ARM", "DISARM"));
        }

        public async Task ElevateSecurityAsync()
        {
            await PublishEventAsync("SECURITY", "ELEVATE");
            _eventHistory.RecordEvent(DeviceEvent.Of("SECURITY", "ELEVATE"));
        }

        public async Task ReduceSecurityAsync()
        {
            await PublishEventAsync("SECURITY", "REDUCE");
            _eventHistory.RecordEvent(DeviceEvent.Of("SECURITY", "REDUCE"));
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
            DeviceHadEvent?.Invoke(this, deviceEvent);
        }
    }
}