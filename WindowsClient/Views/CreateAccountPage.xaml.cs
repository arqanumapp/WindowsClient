using System.Windows;
using System.Windows.Controls;
using WindowsClient.Services;

namespace WindowsClient.Views
{
    public partial class CreateAccountPage : UserControl
    {
        private readonly INavigationService _navigationService;

        public CreateAccountPage(INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<WelcomePage>();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            string nickname = NicknameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(nickname))
            {
                MessageBox.Show("Please enter a nickname.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}
