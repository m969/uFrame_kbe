// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace KbeBalls {
    using KbeBalls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    
    
    public class AvatarControllerBase : EntityCommonController {
        
        private uFrame.MVVM.ViewModels.IViewModelManager _AvatarViewModelManager;
        
        [uFrame.IOC.InjectAttribute("Avatar")]
        public uFrame.MVVM.ViewModels.IViewModelManager AvatarViewModelManager {
            get {
                return _AvatarViewModelManager;
            }
            set {
                _AvatarViewModelManager = value;
            }
        }
        
        public IEnumerable<AvatarViewModel> AvatarViewModels {
            get {
                return AvatarViewModelManager.OfType<AvatarViewModel>();
            }
        }
        
        public override void Setup() {
            base.Setup();
            // This is called when the controller is created
        }
        
        public override void Initialize(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.Initialize(viewModel);
            // This is called when a viewmodel is created
            this.InitializeAvatar(((AvatarViewModel)(viewModel)));
        }
        
        public virtual AvatarViewModel CreateAvatar() {
            return ((AvatarViewModel)(this.Create(Guid.NewGuid().ToString())));
        }
        
        public override uFrame.MVVM.ViewModels.ViewModel CreateEmpty() {
            return new AvatarViewModel(this.EventAggregator);
        }
        
        public virtual void InitializeAvatar(AvatarViewModel viewModel) {
            // This is called when a AvatarViewModel is created
            viewModel.ReSpawn.Action = this.ReSpawnHandler;
            AvatarViewModelManager.Add(viewModel);
        }
        
        public override void DisposingViewModel(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.DisposingViewModel(viewModel);
            AvatarViewModelManager.Remove(viewModel);
        }
        
        public virtual void ReSpawnHandler(ReSpawnCommand command) {
            this.ReSpawn(command.Sender as AvatarViewModel, command);
        }
        
        public virtual void ReSpawn(AvatarViewModel viewModel, ReSpawnCommand arg) {
        }
    }
}
