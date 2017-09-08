namespace KbeBalls {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    
    
    public class AvatarController : AvatarControllerBase {
        
        public override void InitializeAvatar(AvatarViewModel viewModel) {
            base.InitializeAvatar(viewModel);
            // This is called when a AvatarViewModel is created
        }

        public override void ReSpawn(AvatarViewModel viewModel, ReSpawnCommand arg)
        {
            base.ReSpawn(viewModel, arg);
        }
    }
}
