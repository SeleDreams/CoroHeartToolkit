using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using CoroHeartToolkitGUI.ViewModels;
using System;
using System.Collections.Generic;

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
