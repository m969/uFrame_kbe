namespace KbeBalls {
    using KbeBalls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    using KBEngine;
    
    
    public partial class AvatarViewModel : AvatarViewModelBase {
        public override void Bind()
        {
            base.Bind();
        }

        public override void __init__()
        {
            base.__init__();
            if (isPlayer())
            {
                KBEngine.Event.registerIn("relive", this, "relive");
                KBEngine.Event.registerIn("updatePlayer", this, "updatePlayer");

                // 触发登陆成功事件
                //KBEngine.Event.fireOut("onLoginSuccessfully", new object[] { KBEngineApp.app.entity_uuid, id, this });
                Aggregator.Publish(new OnLoginSuccessfullyEvent());
            }
        }

        public override void OnPropertyChanged(object sender, string propertyName)
        {
            base.OnPropertyChanged(sender, propertyName);
            this.Execute(new ReSpawnCommand());
        }

        public void relive(Byte type)
        {
            cellCall("relive", type);
        }

        public void updatePlayer(float x, float y, float z, float yaw)
        {
            position.x = x;
            position.y = y;
            position.z = z;

            direction.z = yaw;
        }
    }
}
