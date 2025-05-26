using CoreLib.Services.Account;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WindowsClient.Views.Controls
{
    public partial class AccountControl : UserControl
    {
        private readonly AccountService _accountService;
        public AccountControl(AccountService accountService)
        {
            InitializeComponent();
            _accountService = accountService;
            Loaded += AccountControl_Loaded;
        }
        private async void AccountControl_Loaded(object sender, RoutedEventArgs e)
        {
            var model = await _accountService.GetAccountInfoAsync();
            if (model != null)
            {
                NickNameTextBlock.Text = model.NickName;
                IdTextBlock.Text = model.AccountId;
            }
            else
            {
                NickNameTextBlock.Text = "Unknown";
                IdTextBlock.Text = "Unknown";
            }
        }
        private async void IdTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(IdTextBlock.Text);

            CopyNotification.Visibility = Visibility.Visible;

            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(200));
            CopyNotification.BeginAnimation(OpacityProperty, fadeIn);

            await Task.Delay(2000);

            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
            fadeOut.Completed += (s, _) => CopyNotification.Visibility = Visibility.Collapsed;
            CopyNotification.BeginAnimation(OpacityProperty, fadeOut);
        }

    }
}
