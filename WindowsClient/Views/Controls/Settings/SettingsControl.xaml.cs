using System.Windows;
using System.Windows.Controls;

namespace WindowsClient.Views.Controls.Settings
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
            ContentArea.Content = new DevicesSettingsControl();
        }
        private void DevicesButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DevicesSettingsControl(); 
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AccountSettingsControl(); 
        }
    }
}
