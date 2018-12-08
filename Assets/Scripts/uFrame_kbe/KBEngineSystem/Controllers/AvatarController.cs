namespace KBEngine
{
	using UnityEngine;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using uFrame.IOC;
	using uFrame.Kernel;
	using uFrame.Kernel.Serialization;
	using uFrame.MVVM;
	using uFrame.MVVM.Bindings;
	using uFrame.MVVM.ViewModels;
	using UniRx;

	public class AvatarController : AvatarControllerBase
	{
		public override void InitializeAvatar(AvatarViewModel viewModel) {
			base.InitializeAvatar(viewModel); }

        public override void Setup()
        {
            base.Setup();
            //
        }
    }
}