namespace KbeBalls {
    using KbeBalls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Kernel;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    using KBEngine;
    
    
    public partial class EntityCommonViewModel : EntityCommonViewModelBase {
        public override void Bind()
        {
            base.Bind();
        }

        public override void __init__()
        {
            base.__init__();
        }

        public override void onDestroy()
        {
            if (isPlayer())
                KBEngine.Event.deregisterIn(this);
        }

        public override void onEnterWorld()
        {
            base.onEnterWorld();
            if (isPlayer())
                Aggregator.Publish(new OnAvatarEnterWorldEvent() { Entity = this });
        }
    }
}
