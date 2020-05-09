using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CoroHeartToolkitGUI.Views
{
    public class AboutWindow : Window
    {
        public AboutWindow()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
