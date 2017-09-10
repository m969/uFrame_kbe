using System;
using uFrame.Kernel;
using uFrame.MVVM.ViewModels;

namespace uFrame.MVVM.Events
{
    public class ViewModelCommandExcuteEvent //uFrame_kbe
    {
        public object Action { get; set; }
        public IViewModelCommand Command { get; set; }
    }
}
