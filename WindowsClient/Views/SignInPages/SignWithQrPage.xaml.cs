using CoreLib.Services.Account;
using System.Windows;
using System.Windows.Controls;
using WindowsClient.Services;

namespace WindowsClient.Views.SignInPages
{
    public partial class SignWithQrPage : UserControl
    {
        private readonly IQrCodeService _qrCodeService;
        private readonly INavigationService _navigationService;
        private readonly IAddDeviceService _addDeviceService;

        private Task? _listeningTask;
        private CancellationTokenSource? _cts;

        public SignWithQrPage(INavigationService navigationService, IQrCodeService qrCodeService, IAddDeviceService addDeviceService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _qrCodeService = qrCodeService;
            _addDeviceService = addDeviceService;
        }
        private async void SignWithQrPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _cts = new CancellationTokenSource();

                var (qr, listeningTask) = await _addDeviceService.GetDeviceQrData(async result =>
                {
                    if (_cts?.IsCancellationRequested == false)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _navigationService.NavigateTo<SelectSignInMethodPage>();
                        });
                    }
                });

                _listeningTask = listeningTask;

                QrImage.Source = _qrCodeService.GenerateQrCode(qr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating QR code: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _cts?.Cancel();

            _ = StopListeningAsync();

            _navigationService.NavigateTo<SelectSignInMethodPage>();
        }
        private async Task StopListeningAsync()
        {
            if (_listeningTask != null)
            {
                try
                {
                    await _listeningTask;
                }
                catch
                {
                }
            }
        }
    }
}
