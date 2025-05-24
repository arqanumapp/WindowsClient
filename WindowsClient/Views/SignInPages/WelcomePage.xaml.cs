using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WindowsClient.Services;

namespace WindowsClient.Views.SignInPages
{
    public partial class WelcomePage : UserControl
    {
        private readonly INavigationService _navigationService;

        public WelcomePage(INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<CreateAccountPage>();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<SelectSignInMethodPage>();
        }

        private void GitHubLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/arqanumapp", 
                UseShellExecute = true
            });
        }
    }
}
