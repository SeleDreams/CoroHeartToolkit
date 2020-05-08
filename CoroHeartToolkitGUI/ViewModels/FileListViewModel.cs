
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CoroHeartFormats;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace CoroHeartToolkitGUI.ViewModels
{
    public class FileListViewModel : ViewModelBase
    {
        public FileListViewModel()
        {
            Files = new ObservableCollection<string>();
        }

        public void OnSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("Selection Changed");

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
            IClassicDesktopStyleApplicationLifetime classicDesktopLifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
            string[] results = await dialog.ShowAsync(classicDesktopLifetime.MainWindow);
            if (results == null || (results.Length < 1 || !File.Exists(results[0])))
            {
                return;
            }
            PopulateList(results[0]);
        }

        public void CloseFile()
        {
            Files.Clear();
            if (GDAT != null)
            {
                GDAT.Dispose();
                GDAT = null;
            }
        }

        public void PopulateList(string path)
        {
            GDAT = new GDAT(path);
            List<GDATFile> files = GDAT.Files;
            Files.Clear();
            for (int i = 0; i < files.Count; i++)
            {
                char[] magic = Encoding.UTF8.GetChars(GDAT.Files[i].Magic);
                Array.Reverse(magic);
                string hexOffset = $"[0x{files[i].Offset:X8}]";
                string reversedMagic = string.Concat(new string(magic).Split(Path.GetInvalidFileNameChars()));
                if (string.IsNullOrEmpty(reversedMagic))
                {
                    Files.Add($"{hexOffset} {i}");
                    continue;
                }
                Files.Add($"{hexOffset} {i}.{reversedMagic}");
            }
        }

        private string SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
                Console.WriteLine("Value was set");
            }
        }
        private string _selectedFile;
        public ObservableCollection<string> Files { get; set; }
        public FilePropertiesViewModel PropertiesViewModel { get; set; }
        public ListBox List { get; set; }
        private GDAT GDAT;
    }
}