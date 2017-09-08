namespace KbeBalls {
    using KbeBalls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Json;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    
    
    public class setdirectionEvent : setdirectionEventBase {
        public setdirectionEvent()
        {
            
        }

        public setdirectionEvent(KBEngine.Entity entity)
        {
            Entity = entity;
        }
    }
}
