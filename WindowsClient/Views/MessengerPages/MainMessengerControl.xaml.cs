using System.Windows;
using System.Windows.Controls;
using WindowsClient.Views.Controls;

namespace WindowsClient.Views.MessengerPages
{
    public partial class MainMessengerControl : UserControl
    {
        public MainMessengerControl()
        {
            InitializeComponent();
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
            ContentArea.Content = new AccountControl();
        }

        private void Contacts_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ContactControl();
        }

    }
}
