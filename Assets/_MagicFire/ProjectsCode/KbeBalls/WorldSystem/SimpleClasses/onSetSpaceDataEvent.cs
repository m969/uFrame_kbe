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
    
    
    public class onSetSpaceDataEvent : onSetSpaceDataEventBase {
        public onSetSpaceDataEvent(UInt32 spaceId, string key, string value)
        {
            SpaceID = spaceId;
            Key = key;
            Value = value;
        }
    }
}
