﻿using Avalonia.Controls;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;
using CoroHeartToolkitGUI.Models;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace CoroHeartToolkitGUI.ViewModels
{
    public class TopMenuViewModel : ViewModelBase
    {
        public TopMenuViewModel(IReadOnlyList<TopMenuItem> items)
        {
            
            MenuItems = items;
        }

        public IReadOnlyList<TopMenuItem> MenuItems { get; set; }
    }
}
