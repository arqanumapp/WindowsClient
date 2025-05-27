using Microsoft.Win32;
using System.Diagnostics;

namespace WindowsClient.Utils
{
    public static class ProtocolRegistrar
    {
        private const string ProtocolName = "arqanum";

        public static void RegisterIfNeeded()
        {
            string keyPath = $@"Software\Classes\{ProtocolName}";
            using var key = Registry.CurrentUser.OpenSubKey(keyPath);

            if (key != null)
            {
                return;
            }

            string appPath = Process.GetCurrentProcess().MainModule?.FileName
                             ?? throw new InvalidOperationException("Failed to get path to application");

            using var baseKey = Registry.CurrentUser.CreateSubKey(keyPath);
            baseKey.SetValue("", "URL:Arqanum Protocol");
            baseKey.SetValue("URL Protocol", "");

            using var iconKey = baseKey.CreateSubKey("DefaultIcon");
            iconKey.SetValue("", $"\"{appPath}\",1");

            using var commandKey = baseKey.CreateSubKey(@"shell\open\command");
            commandKey.SetValue("", $"\"{appPath}\" \"%1\"");
        }
    }
}
