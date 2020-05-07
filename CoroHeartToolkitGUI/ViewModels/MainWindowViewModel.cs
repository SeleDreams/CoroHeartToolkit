using CoroHeartToolkitGUI.Services;
using System.Collections.Generic;
using ReactiveUI;
using Avalonia.Controls;
using System.Threading.Tasks;
using CoroHeartFormats;
using Kaitai;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using Avalonia.Controls.Shapes;
using System.Globalization;

namespace CoroHeartToolkitGUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(Window window, Database db)
        {
            this.window = window;
            FileList = new FileListViewModel(db.GetItems());
            TopMenu = new TopMenuViewModel(TopMenuItems);
            FileProperties = new FilePropertiesViewModel(new FileProperty[]
                {
                    new FileProperty("Type","Unknown"),
                    new FileProperty("Size","Unknown")
                });
        }


        public async void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                AllowMultiple = false,
                Filters = new List<FileDialogFilter>()
                {
                    new FileDialogFilter()
                    {
                        Extensions = new List<string> {"dat"},
                        Name = "GDA File"
                    }
                },
                Title = "Select the DAT file you want to open"
            };
            string[] results = await dialog.ShowAsync(window);
            Gdat = Gdat.FromFile(results[0]);
            Gdat.ReadData();
            List<Gdat.File> files = Gdat.Files;
            FileList.Files.Clear();
            for (int i = 0; i < files.Count(); i++)
            {
                char[] magic = Gdat.Files[i].Magic.ToCharArray();
                Array.Reverse(magic);
                CultureInfo info = CultureInfo.InvariantCulture;
                string hexOffset = $"[0x{ files[i].FileOffset.ToString("X8")}]";
                string reversedMagic = string.Concat(new string(magic).Split(System.IO.Path.GetInvalidFileNameChars()));
                if (string.IsNullOrEmpty(reversedMagic))
                {
                    FileList.Files.Add($"{hexOffset} {i}");
                    continue;
                }
                FileList.Files.Add($"{hexOffset} {i}.{reversedMagic}");
            }
        }

        void CloseFile()
        {

        }

        private List<TopMenuItemViewModel> TopMenuItems
        {
            get
            {
                if (_topMenuItems != null)
                {
                    return _topMenuItems;
                }
                return _topMenuItems = new List<TopMenuItemViewModel>
                {
                    new TopMenuItemViewModel()
                    {
                        Header = "_File",
                        Items = new TopMenuItemViewModel[]
                {
                    new TopMenuItemViewModel()
                    {
                        Header = "_Open",
                        Command = ReactiveCommand.Create(OpenFile)
                    },
                    new TopMenuItemViewModel()
                    {
                        Header = "Close",
                       Command = ReactiveCommand.Create(CloseFile)
                    }
                }
                    },
                    new TopMenuItemViewModel()
                    {
                        Header = "Edit",
                        Items = new TopMenuItemViewModel[]
                    {
                        new TopMenuItemViewModel()
                        {
                            Header = "Undo"
                        },
                        new TopMenuItemViewModel()
                        {
                            Header = "Redo"
                        }
                    } } };
            }
        }


        public FileListViewModel FileList { get; set; }
        public TopMenuViewModel TopMenu { get; set; }
        public FilePropertiesViewModel FileProperties { get; set; }

        Gdat Gdat;

        private List<TopMenuItemViewModel> _topMenuItems;
        private readonly Window window;
    }
}
