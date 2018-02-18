using System.Threading.Tasks;
using Particle.SDK;

namespace HeProTech.Webservices.Device
{
    public class DeviceFactory
    {
        public async Task<ParticleDevice> GetDeviceAsync()
        {
            var devices = await ParticleCloud.SharedCloud.GetDevicesAsync();
            return devices.First();
        }
    }
}