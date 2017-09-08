// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Example {
    using Example;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    
    
    public class SettingScreenControllerBase : SubScreenController {
        
        private uFrame.MVVM.ViewModels.IViewModelManager _SettingScreenViewModelManager;
        
        [uFrame.IOC.InjectAttribute("SettingScreen")]
        public uFrame.MVVM.ViewModels.IViewModelManager SettingScreenViewModelManager {
            get {
                return _SettingScreenViewModelManager;
            }
            set {
                _SettingScreenViewModelManager = value;
            }
        }
        
        public IEnumerable<SettingScreenViewModel> SettingScreenViewModels {
            get {
                return SettingScreenViewModelManager.OfType<SettingScreenViewModel>();
            }
        }
        
        public override void Setup() {
            base.Setup();
            // This is called when the controller is created
        }
        
        public override void Initialize(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.Initialize(viewModel);
            // This is called when a viewmodel is created
            this.InitializeSettingScreen(((SettingScreenViewModel)(viewModel)));
        }
        
        public virtual SettingScreenViewModel CreateSettingScreen() {
            return ((SettingScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
        }
        
        public override uFrame.MVVM.ViewModels.ViewModel CreateEmpty() {
            return new SettingScreenViewModel(this.EventAggregator);
        }
        
        public virtual void InitializeSettingScreen(SettingScreenViewModel viewModel) {
            // This is called when a SettingScreenViewModel is created
            viewModel.Default.Action = this.DefaultHandler;
            viewModel.Apply.Action = this.ApplyHandler;
            SettingScreenViewModelManager.Add(viewModel);
        }
        
        public override void DisposingViewModel(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.DisposingViewModel(viewModel);
            SettingScreenViewModelManager.Remove(viewModel);
        }
        
        public virtual void Default(SettingScreenViewModel viewModel) {
        }
        
        public virtual void Apply(SettingScreenViewModel viewModel) {
        }
        
        public virtual void DefaultHandler(DefaultCommand command) {
            this.Default(command.Sender as SettingScreenViewModel);
        }
        
        public virtual void ApplyHandler(ApplyCommand command) {
            this.Apply(command.Sender as SettingScreenViewModel);
        }
    }
}