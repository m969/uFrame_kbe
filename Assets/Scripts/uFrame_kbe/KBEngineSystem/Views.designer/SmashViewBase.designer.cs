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

	public class SmashViewBase : uFrame.MVVM.Views.ViewBase
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


		[UnityEngine.SerializeField()]
		[uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
		[UnityEngine.HideInInspector()]
		public FLOAT _modelScale;

		[uFrame.MVVM.Attributes.UFToggleGroup("modelScale")]
		[UnityEngine.HideInInspector()]
		public bool _BindmodelScale = true;

		[uFrame.MVVM.Attributes.UFGroup("modelScale")]
		[UnityEngine.SerializeField()]
		[UnityEngine.HideInInspector()]
		[UnityEngine.Serialization.FormerlySerializedAsAttribute("_modelScaleonlyWhenChanged")]
		protected bool _modelScaleOnlyWhenChanged;


		[uFrame.MVVM.Attributes.UFToggleGroup("CreateCellClient")]
		[UnityEngine.HideInInspector()]
		public bool _BindCreateCellClient = true;

		public override string DefaultIdentifier { get { return base.DefaultIdentifier; } }
		public override System.Type ViewModelType { get { return typeof(SmashViewModel); } }
		public SmashViewModel Smash { get { return (SmashViewModel)ViewModelObject; } }

		public override void Bind() {
			base.Bind();
			if (_BindmodelID) { this.BindProperty(this.Smash.modelIDProperty, this.modelIDChanged, _modelIDOnlyWhenChanged); }
			if (_BindmodelScale) { this.BindProperty(this.Smash.modelScaleProperty, this.modelScaleChanged, _modelScaleOnlyWhenChanged); }
			if (_BindCreateCellClient) {  this.BindCommandExecuted(this.Smash.CreateCellClientCommand, this.CreateCellClientExecuted);  }

		}

		public virtual void modelIDChanged(Byte arg1) {  }
		public virtual void modelScaleChanged(float arg1) {  }

		public virtual void CreateCellClientExecuted(SmashCreateCellClientCommand command) {  }
	}
}