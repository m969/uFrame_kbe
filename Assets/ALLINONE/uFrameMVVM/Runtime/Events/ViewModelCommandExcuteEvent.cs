using System;
using uFrame.Kernel;
using uFrame.MVVM.ViewModels;

namespace uFrame.MVVM.Events
{
    public class ViewModelCommandExcuteEvent
    {
        public object Action { get; set; }
        public ViewModelCommand Command { get; set; }
    }
}
