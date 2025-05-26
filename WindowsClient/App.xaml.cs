using CoreLib;
using CoreLib.Interfaces;
using CoreLib.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using WindowsClient.CoreImplementations;
using WindowsClient.Services;
using WindowsClient.Views.Controls;
using WindowsClient.Views.Controls.Settings;
using WindowsClient.Views.MessengerPages;
using WindowsClient.Views.SignInPages;

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
        services.AddSingleton<IDatabasePasswordProvider, DatabasePasswordProvider>();
        services.AddSingleton<ICaptchaTokenProvider, CaptchaTokenProvider>();
        services.AddArqanumCore();

        services.AddTransient<IQrCodeService, QrCodeService>();
        var contentControl = new ContentControl();

        services.AddSingleton(contentControl);

        services.AddSingleton<INavigationService>(sp =>
            new NavigationService(sp, sp.GetRequiredService<ContentControl>()));

        services.AddTransient<WelcomePage>();
        services.AddTransient<CreateAccountPage>();
        services.AddTransient<SignWithQrPage>();
        services.AddTransient<SelectSignInMethodPage>();

        services.AddSingleton<MainMessengerControl>();
        services.AddTransient<ChatsControl>();
        services.AddTransient<SettingsControl>();
        services.AddTransient<AccountControl>();


        services.AddSingleton<MainWindow>();

        var serviceProvider = services.BuildServiceProvider();
        var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }




}

