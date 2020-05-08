
using System.Collections.Generic;
using ReactiveUI;
using Avalonia.Controls;
using CoroHeartToolkitGUI.Models;

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
                        Header = "Edit",
                        Items = new TopMenuItem[]
                    {
                        new TopMenuItem()
                        {
                            Header = "Undo"
                        },
                        new TopMenuItem()
                        {
                            Header = "Redo"
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
