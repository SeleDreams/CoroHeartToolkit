using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CoroHeartToolkitGUI.Views
{
    public class FilePropertiesView : UserControl
    {
        public FilePropertiesView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
