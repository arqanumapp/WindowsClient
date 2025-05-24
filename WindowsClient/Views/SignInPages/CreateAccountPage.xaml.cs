using CoreLib.Services.Account;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WindowsClient.Services;

namespace WindowsClient.Views.SignInPages
{
    public partial class CreateAccountPage : UserControl
    {
        private readonly INavigationService _navigationService;
        private readonly CreateAccountService createAccountService;

        public CreateAccountPage(INavigationService navigationService, CreateAccountService createAccountService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            this.createAccountService = createAccountService;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<WelcomePage>();
        }

        private async void Continue_Click(object sender, RoutedEventArgs e)
        {
            string nickname = NicknameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(nickname))
            {
                NicknameErrorTextBlock.Text = "Please enter a nickname.";
                NicknameErrorTextBlock.Visibility = Visibility.Visible;
                return;
            }

            ContinueButton.IsEnabled = false;
            BackButton.IsEnabled = false;
            NicknameErrorTextBlock.IsEnabled = false;

            var progress = new Progress<string>(message =>
            {
                StatusTextBlock.Foreground = Brushes.Gray;
                StatusTextBlock.Text = message;
            });

            try
            {
                bool result = await createAccountService.CreateAsync(nickname, progress);

                if (result)
                {
                    //TODO: Navigate to the next page after account creation
                }
                else
                {
                    StatusTextBlock.Foreground = Brushes.Red;
                    StatusTextBlock.Text = "Error creating account. Try again";
                }
            }
            catch
            {
                StatusTextBlock.Foreground = Brushes.Red;
                StatusTextBlock.Text = "Error creating account. Try again";
            }
            finally
            {
                ContinueButton.Content = "Try again";
                ContinueButton.IsEnabled = true;
                BackButton.IsEnabled = true;
                NicknameErrorTextBlock.IsEnabled = true;
            }
        }
        private void NicknameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NicknameTextBox.Text))
            {
                NicknameErrorTextBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                NicknameErrorTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
