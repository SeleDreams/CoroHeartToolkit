using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;

namespace CoroHeartToolkitGUI.ViewModels
{
    public class TopMenuViewModel : ViewModelBase
    {
        public TopMenuViewModel(IReadOnlyList<TopMenuItemViewModel> items)
        {
            MenuItems = items;
        }

        public IReadOnlyList<TopMenuItemViewModel> MenuItems { get; set; }

        public ReactiveCommand<Unit, Unit> OpenCommand { get; }
        public ReactiveCommand<Unit, Unit> CloseCommand { get; }
        public ReactiveCommand<string, Unit> OpenRecentCommand { get; }
    }
}
