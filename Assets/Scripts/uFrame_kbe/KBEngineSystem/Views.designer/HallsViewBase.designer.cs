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
	using uFrame.MVVM.Views;
	using UniRx;

	public class HallsViewBase : uFrame.MVVM.Views.ViewBase
	{
		public override string DefaultIdentifier { get { return base.DefaultIdentifier; } }
		public override System.Type ViewModelType { get { return typeof(HallsViewModel); } }
		public HallsViewModel Halls { get { return (HallsViewModel)ViewModelObject; } }

		public override void Bind() {
			base.Bind();
		}


	}
}