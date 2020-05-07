using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CoroHeartToolkitGUI.Views
{
    public class TopMenuView : UserControl
    {
        public TopMenuView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
