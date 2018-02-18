using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Particle.SDK;

namespace HeProTech.Webservices.Device
{
    public class DeviceFactory
    {
        public DeviceFactory(IConfiguration configuration)
        {
            var username = configuration["PARTICLE_CLOUD_USER"];
            var passowrd = configuration["PARTICLE_CLOUD_PASSWORD"];
            ParticleCloud.SharedCloud.LoginAsync(username, passowrd).Wait(); // wapoz lets not follow async paradigms ¯\_(ツ)_/¯
        }

        public async Task<ParticleDevice> GetDeviceAsync()
        {
            var devices = await ParticleCloud.SharedCloud.GetDevicesAsync();
            return devices.First();
        }
    }
}