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

	public class FoodViewBase : uFrame.MVVM.Views.ViewBase
	{
		[UnityEngine.SerializeField()]
		[uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
		[UnityEngine.HideInInspector()]
		public UINT8 _modelID;

		[uFrame.MVVM.Attributes.UFToggleGroup("modelID")]
		[UnityEngine.HideInInspector()]
		public bool _BindmodelID = true;

		[uFrame.MVVM.Attributes.UFGroup("modelID")]
		[UnityEngine.SerializeField()]
		[UnityEngine.HideInInspector()]
		[UnityEngine.Serialization.FormerlySerializedAsAttribute("_modelIDonlyWhenChanged")]
		protected bool _modelIDOnlyWhenChanged;


		[uFrame.MVVM.Attributes.UFToggleGroup("CreateCellClient")]
		[UnityEngine.HideInInspector()]
		public bool _BindCreateCellClient = true;

		public override string DefaultIdentifier { get { return base.DefaultIdentifier; } }
		public override System.Type ViewModelType { get { return typeof(FoodViewModel); } }
		public FoodViewModel Food { get { return (FoodViewModel)ViewModelObject; } }

		public override void Bind() {
			base.Bind();
			if (_BindmodelID) { this.BindProperty(this.Food.modelIDProperty, this.modelIDChanged, _modelIDOnlyWhenChanged); }
			if (_BindCreateCellClient) {  this.BindCommandExecuted(this.Food.CreateCellClientCommand, this.CreateCellClientExecuted);  }

		}

		public virtual void modelIDChanged(Byte arg1) {  }

		public virtual void CreateCellClientExecuted(FoodCreateCellClientCommand command) {  }
	}
}