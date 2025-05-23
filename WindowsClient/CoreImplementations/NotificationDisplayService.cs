using CoreLib.Interfaces;

namespace WindowsClient.CoreImplementations
{
    class NotificationDisplayService : INotificationDisplayService
    {
        public Task ShowNotificationAsync(string data)
        {
            Console.WriteLine("Hello");
            return Task.CompletedTask;
        }
    }
}
