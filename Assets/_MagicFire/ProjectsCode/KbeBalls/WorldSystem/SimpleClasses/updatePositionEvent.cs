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
    
    
    public class updatePositionEvent : updatePositionEventBase {
        public updatePositionEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}
