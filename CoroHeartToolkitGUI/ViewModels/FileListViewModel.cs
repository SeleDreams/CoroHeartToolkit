
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CoroHeartFormats;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using ReactiveUI;
using System.Windows.Input;
using System.Reactive;
using CoroHeartToolkitGUI.Models;

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
            CloseFile();
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
            _currentGDAT = results[0];
            PopulateList(results[0]);
        }

        public async void ExportFile(){
            Console.WriteLine(_selectedIndex);
            string extension = FileProperties.GetExtension(GDAT.Files[_selectedIndex].Magic);
            SaveFileDialog dialog = new SaveFileDialog(){
                Title = "Where do you want to save this file !",
                DefaultExtension = extension,
                InitialFileName = _selectedIndex.ToString() + "." + extension
            };
             IClassicDesktopStyleApplicationLifetime classicDesktopLifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
            
            string results = await dialog.ShowAsync(classicDesktopLifetime.MainWindow);
            if (results == null){
                return;
            }
            else{
                Console.WriteLine("Test");
                GDATFile file = GDAT.Files[_selectedIndex];
                using (FileStream s = File.Create(results)){
                    file.Export(s);
                }
            }
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
                    Files.Add($"{i}");
            }
        }

        private string SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
            }
        }

        public int SelectedIndex{
            get => _selectedIndex;
            set{
                _selectedIndex = value;
                if (GDAT != null && GDAT.Files.Count > 0){
                    PropertiesViewModel.UpdateProperties(GDAT.Files[_selectedIndex]);
                }
                
            }
        }
        
        private int _selectedIndex;
        private string _selectedFile;

        private string _currentGDAT;
        public ObservableCollection<string> Files { get; set; }
        public FilePropertiesViewModel PropertiesViewModel { get; set; }
        public ListBox List { get; set; }
        private GDAT GDAT;
    }
}