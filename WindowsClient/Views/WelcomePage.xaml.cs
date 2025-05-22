using System.Windows;
using System.Windows.Controls;
using WindowsClient.Services;

namespace WindowsClient.Views
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
    }
}
