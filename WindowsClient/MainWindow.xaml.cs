using System.Windows;
using System.Windows.Controls;
using WindowsClient.Services;
using WindowsClient.Views;

namespace WindowsClient
{
    public partial class MainWindow : Window
    {
        public MainWindow(ContentControl hostControl, INavigationService navigationService)
        {
            InitializeComponent();

            MainContentControl.Content = hostControl;

            navigationService.NavigateTo<WelcomePage>();
        }
    }
}
