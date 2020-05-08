using CoroHeartToolkitGUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CoroHeartToolkitGUI.ViewModels
{
    public class FilePropertiesViewModel : ViewModelBase
    {
        public FilePropertiesViewModel()
        {
            Properties = new ObservableCollection<FileProperty>();
        }

        public ObservableCollection<FileProperty> Properties { get; set; }
    }
}
