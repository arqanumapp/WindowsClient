using CoreLib.Interfaces;
using System.Windows;
using WindowsClient.Windows;

namespace WindowsClient.CoreImplementations
{
    public class CaptchaTokenProvider : ICaptchaTokenProvider
    {
        public async Task<string> GetCaptchaTokenAsync()
        {
            var task = await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var captchaWindow = new CaptchaWindow();
                captchaWindow.ShowDialog();
                return captchaWindow.GetCaptchaTokenAsync();
            });

            return await task;
        }
    }
}
