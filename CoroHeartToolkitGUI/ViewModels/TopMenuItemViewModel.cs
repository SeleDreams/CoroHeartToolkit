using CoroHeartToolkitGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CoroHeartToolkitGUI.ViewModels
{
    public class TopMenuItemViewModel : ViewModelBase
    {
        public string Header { get; set; }
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
        public IList<TopMenuItemViewModel> Items { get; set; }

    }
}
