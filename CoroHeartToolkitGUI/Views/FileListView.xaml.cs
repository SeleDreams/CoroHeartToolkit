using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CoroHeartToolkitGUI.Views
{
    public class FileListView : UserControl
    {
        public FileListView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }


    }
}
