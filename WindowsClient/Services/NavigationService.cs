using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace WindowsClient.Services
{
    public interface INavigationService
    {
        void NavigateTo<TPage>() where TPage : UserControl;
        void GoBack();
        bool CanGoBack { get; }
    }
    public class NavigationService(IServiceProvider serviceProvider, ContentControl contentControl) : INavigationService
    {
        private readonly Stack<UserControl> _history = new();

        public bool CanGoBack => _history.Count > 1;

        public void NavigateTo<TPage>() where TPage : UserControl
        {
            if (contentControl.Content is UserControl current)
            {
                _history.Push(current);
            }

            var page = serviceProvider.GetRequiredService<TPage>();
            contentControl.Content = page;
        }

        public void GoBack()
        {
            if (CanGoBack)
            {
                _history.Pop();
                var previous = _history.Peek();
                contentControl.Content = previous;
            }
        }
    }
}
