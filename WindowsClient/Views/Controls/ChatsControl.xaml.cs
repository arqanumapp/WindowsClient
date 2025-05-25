using System.Windows.Controls;

namespace WindowsClient.Views.Controls
{
    public partial class ChatsControl : UserControl
    {
        public ChatsControl()
        {
            InitializeComponent();
            ContentArea.Content = new DefaultChatContent();
        }
    }
}
