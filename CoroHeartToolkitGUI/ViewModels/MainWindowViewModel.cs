
using System.Collections.Generic;
using ReactiveUI;
using Avalonia.Controls;
using CoroHeartToolkitGUI.Models;
using CoroHeartToolkitGUI.Views;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;

namespace CoroHeartToolkitGUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {

            FileList = new FileListViewModel();
            FileProperties = new FilePropertiesViewModel();
            FileList.PropertiesViewModel = FileProperties;
            _initTopMenuItems();
            TopMenu = new TopMenuViewModel(TopMenuItems);
        }

        void TriggerAboutWindow()
        {
            AboutWindow window = new AboutWindow();
            IClassicDesktopStyleApplicationLifetime classicDesktopLifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
            
            window.ShowDialog(classicDesktopLifetime.MainWindow);
        }

        private void _initTopMenuItems()
        {
            TopMenuItems = new List<TopMenuItem>
                {
                    new TopMenuItem()
                    {
                        Header = "_File",
                        Items = new TopMenuItem[]
                {
                    new TopMenuItem()
                    {
                        Header = "_Open",
                        Command = ReactiveCommand.Create(FileList.OpenFile)
                    },
                    new TopMenuItem()
                    {
                        Header = "Close",
                        Command = ReactiveCommand.Create(FileList.CloseFile)
                    }
                }
                    },
                    new TopMenuItem()
                    {
                        Header = "Help",
                        Items = new TopMenuItem[]
                    {
                        new TopMenuItem()
                        {
                            Header = "About",
                            Command= ReactiveCommand.Create(TriggerAboutWindow)
                        }
                    } } };
        }
        private List<TopMenuItem> TopMenuItems { get => _topMenuItems; set => _topMenuItems = value; }

        public FileListViewModel FileList { get; set; }
        public TopMenuViewModel TopMenu { get; set; }
        public FilePropertiesViewModel FileProperties { get; set; }

        private List<TopMenuItem> _topMenuItems;
    }
}
