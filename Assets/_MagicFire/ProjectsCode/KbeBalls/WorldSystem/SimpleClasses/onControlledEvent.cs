using KBEngine;

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
    
    
    public class onControlledEvent : onControlledEventBase {
        public onControlledEvent(Entity entity, bool isControlled)
        {
            Entity = entity;
            this.isControlled = isControlled;
        }
    }
}
