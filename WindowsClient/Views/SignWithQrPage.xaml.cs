using System.Windows;
using System.Windows.Controls;
using WindowsClient.Services;

namespace WindowsClient.Views
{
    public partial class SignWithQrPage : UserControl
    {
        private readonly IQrCodeService _qrCodeService;
        private readonly INavigationService _navigationService;
        public SignWithQrPage(INavigationService navigationService, IQrCodeService qrCodeService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _qrCodeService = qrCodeService;
            QrImage.Source = _qrCodeService.GenerateQrCode("Привет");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<WelcomePage>();
        }
    }
}
