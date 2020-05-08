using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CoroHeartToolkitGUI.ViewModels;
using System;
using System.Collections.Generic;

namespace CoroHeartToolkitGUI.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
