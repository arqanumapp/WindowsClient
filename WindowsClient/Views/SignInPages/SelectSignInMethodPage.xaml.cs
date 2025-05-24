using System.Windows;
using System.Windows.Controls;
using WindowsClient.Services;

namespace WindowsClient.Views.SignInPages
{

    public partial class SelectSignInMethodPage : UserControl
    {
        private readonly INavigationService _navigationService;
        public SelectSignInMethodPage(INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<WelcomePage>();
        }

        private void QrContinue_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<SignWithQrPage>();
        }

    }
}
