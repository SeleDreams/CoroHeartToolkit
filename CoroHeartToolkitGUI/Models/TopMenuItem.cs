using CoroHeartToolkitGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CoroHeartToolkitGUI.Models
{
    public class TopMenuItem
    {
        public string Header { get; set; }
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
        public IList<TopMenuItem> Items { get; set; }
    }
}
