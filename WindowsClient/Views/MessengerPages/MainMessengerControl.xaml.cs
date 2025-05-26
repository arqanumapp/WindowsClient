using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using WindowsClient.Views.Controls;
using WindowsClient.Views.Controls.Settings;

namespace WindowsClient.Views.MessengerPages
{
    public partial class MainMessengerControl : UserControl
    {
        private readonly IServiceProvider _serviceProvider;

        public MainMessengerControl(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            ContentArea.Content = new ChatsControl();
        }

        private void Chats_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ContentArea.Content = new ChatsControl();
        }

        private void Settings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ContentArea.Content = new SettingsControl();
        }

        private void Account_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ContentArea.Content = _serviceProvider.GetRequiredService<AccountControl>();
        }

        private void Contacts_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ContactControl();
        }

    }
}
