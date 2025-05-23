using Microsoft.Web.WebView2.Core;
using System.Windows;

namespace WindowsClient.Windows
{
    public partial class CaptchaWindow : Window
    {
        private TaskCompletionSource<string> _tcs = new();

        public CaptchaWindow()
        {
            InitializeComponent();
            Loaded += async (_, _) =>
            {
                await WebView.EnsureCoreWebView2Async();
                WebView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

                WebView.Source = new Uri("https://enigram-001-site1.qtempurl.com/captcha");
            };
        }
        private void WebView_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                
            }
            else
            {
                MessageBox.Show($"Ошибка загрузки страницы: {e.WebErrorStatus}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string token = e.TryGetWebMessageAsString();
            if (!string.IsNullOrWhiteSpace(token))
            {
                _tcs.TrySetResult(token);
                Dispatcher.Invoke(Close);
            }
        }

        public Task<string> GetCaptchaTokenAsync() => _tcs.Task;
    }
}
