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

	public class HallsController : HallsControllerBase
	{
		public override void InitializeHalls(HallsViewModel viewModel) {
			base.InitializeHalls(viewModel); }
	}
}