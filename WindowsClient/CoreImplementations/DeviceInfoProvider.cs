using CoreLib.Interfaces;
using System.Runtime.InteropServices;

namespace WindowsClient.CoreImplementations
{
    public class DeviceInfoProvider : IDeviceInfoProvider
    {
        public Task<string> GetDeviceName()
        {
            string machineName = Environment.MachineName;
            string osDescription = RuntimeInformation.OSDescription;

            string result = $"{machineName} ({osDescription})";
            return Task.FromResult(result);
        }
    }
}
