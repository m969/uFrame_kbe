using uFrame.IOC;

namespace KbeBalls {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.Services;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    
    
    public class AvatarObjectView : AvatarObjectViewBase
    {
        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as AvatarViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Avatar to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

        public override void modelIDChanged(int arg1)
        {
            Debug.Log("modelIDChanged");
        }

        public override void nameChanged(String arg1)
        {
            Debug.Log("nameChanged");
            gameObject.name = arg1;
        }

        public override void moveSpeedChanged(double arg1)
        {
            Debug.Log("moveSpeedChanged");
            this._moveSpeed = (float)arg1;
            this.speed = (float)arg1;
        }

        public override void ExecuteReSpawn(ReSpawnCommand command)
        {
            Debug.Log("ExecuteReSpawn");
            base.ExecuteReSpawn(command);
        }

        public override void ReSpawnExecuted(ReSpawnCommand command)
        {
            Debug.Log("ReSpawnExecuted");
            base.ReSpawnExecuted(command);
        }
    }
}
