
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CoroHeartToolkitGUI.ViewModels
{
    public class FileListViewModel : ViewModelBase
    {
        public FileListViewModel(IEnumerable<string> items)
        {
            Files = new ObservableCollection<string>(items);
        }
        public ObservableCollection<string> Files { get; set; }
    }
}