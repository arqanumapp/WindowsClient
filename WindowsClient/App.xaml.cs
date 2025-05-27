using CoreLib;
using CoreLib.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using WindowsClient.CoreImplementations;
using WindowsClient.Services;
using WindowsClient.Utils;
using WindowsClient.Views.Controls;
using WindowsClient.Views.Controls.Settings;
using WindowsClient.Views.MessengerPages;
using WindowsClient.Views.SignInPages;

namespace WindowsClient;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;
    private static Mutex _appMutex;
    private static CancellationTokenSource _cts;

    [STAThread]
    public static void Main(string[] args)
    {
        ProtocolRegistrar.RegisterIfNeeded();

        if (SingleInstanceManager.IsFirstInstance(out _appMutex))
        {
            _cts = new CancellationTokenSource();
            InstanceCommunicator.StartListening(HandleIncomingMessage, _cts.Token);

            var app = new App();
            app.RunWithArgs(args);

            _cts.Cancel();
            _appMutex.ReleaseMutex();
            _appMutex.Dispose();
        }
        else
        {
            if (args.Length > 0)
            {
                InstanceCommunicator.SendMessageToMainInstance(args[0]);
            }
        }
    }

    private void LoadAppResources()
    {
        var dict = new ResourceDictionary
        {
            Source = new Uri("Resources/Styles/Styles.xaml", UriKind.Relative)
        };

        this.Resources.MergedDictionaries.Add(dict);
    }

    private void RunWithArgs(string[] args)
    {
        this.Startup += (_, _) =>
        {
            LoadAppResources();

            ConfigureServices();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            if (args.Length > 0)
            {
                HandleUri(args[0]);
            }
        };

        this.Run();
    }

    private void ConfigureServices()
    {
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

        _serviceProvider = services.BuildServiceProvider();
    }

    private static void HandleIncomingMessage(string message)
    {
        Current?.Dispatcher.Invoke(() => HandleUri(message));
    }

    private static void HandleUri(string uri)
    {
        MessageBox.Show("URI received: " + uri);

    }
}
