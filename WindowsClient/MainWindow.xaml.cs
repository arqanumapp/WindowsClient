using CoreLib.Storage;
using System.Windows;
using System.Windows.Controls;
using WindowsClient.Services;
using WindowsClient.Views.MessengerPages;
using WindowsClient.Views.SignInPages;

namespace WindowsClient
{
    public partial class MainWindow : Window
    {
        public MainWindow(ContentControl hostControl, INavigationService navigationService, IAccountStorage accountStorage)
        {
            InitializeComponent();

            MainContentControl.Content = hostControl;
            Loaded += async (_, _) =>
            {
                var account = await accountStorage.GetAccountAsync();

                if (account == null)
                {
                    navigationService.NavigateTo<WelcomePage>();
                }
                else
                {
                    navigationService.NavigateTo<MainMessengerControl>();
                }
            };
        }
    }
}
