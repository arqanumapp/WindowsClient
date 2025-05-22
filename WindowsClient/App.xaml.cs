using CoreLib;
using CoreLib.Helpers;
using CoreLib.Notifications;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WindowsClient.CoreImplementations;

namespace WindowsClient;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        services.AddSingleton<IDeviceInfoProvider, DeviceInfoProvider>();
        services.AddSingleton<INotificationDisplayService, NotificationDisplayService>();
        services.AddArqanumCore();

        services.AddTransient<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }


}

