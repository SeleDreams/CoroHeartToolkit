using CoroHeartToolkitGUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CoroHeartToolkitGUI.ViewModels
{
    public class FilePropertiesViewModel : ViewModelBase
    {
        public FilePropertiesViewModel(FileProperty[] properties)
        {
            Properties = properties;
        }

        public FileProperty[] Properties { get; set; }
    }
}
