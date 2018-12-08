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

	public class AvatarViewBase : uFrame.MVVM.Views.ViewBase
	{
		[UnityEngine.SerializeField()]
		[uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
		[UnityEngine.HideInInspector()]
		public UINT8 _level;

		[uFrame.MVVM.Attributes.UFToggleGroup("level")]
		[UnityEngine.HideInInspector()]
		public bool _Bindlevel = true;

		[uFrame.MVVM.Attributes.UFGroup("level")]
		[UnityEngine.SerializeField()]
		[UnityEngine.HideInInspector()]
		[UnityEngine.Serialization.FormerlySerializedAsAttribute("_levelonlyWhenChanged")]
		protected bool _levelOnlyWhenChanged;


		[UnityEngine.SerializeField()]
		[uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
		[UnityEngine.HideInInspector()]
		public INT32 _mass;

		[uFrame.MVVM.Attributes.UFToggleGroup("mass")]
		[UnityEngine.HideInInspector()]
		public bool _Bindmass = true;

		[uFrame.MVVM.Attributes.UFGroup("mass")]
		[UnityEngine.SerializeField()]
		[UnityEngine.HideInInspector()]
		[UnityEngine.Serialization.FormerlySerializedAsAttribute("_massonlyWhenChanged")]
		protected bool _massOnlyWhenChanged;


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


		[UnityEngine.SerializeField()]
		[uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
		[UnityEngine.HideInInspector()]
		public FLOAT _moveSpeed;

		[uFrame.MVVM.Attributes.UFToggleGroup("moveSpeed")]
		[UnityEngine.HideInInspector()]
		public bool _BindmoveSpeed = true;

		[uFrame.MVVM.Attributes.UFGroup("moveSpeed")]
		[UnityEngine.SerializeField()]
		[UnityEngine.HideInInspector()]
		[UnityEngine.Serialization.FormerlySerializedAsAttribute("_moveSpeedonlyWhenChanged")]
		protected bool _moveSpeedOnlyWhenChanged;


		[UnityEngine.SerializeField()]
		[uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
		[UnityEngine.HideInInspector()]
		public UNICODE _name;

		[uFrame.MVVM.Attributes.UFToggleGroup("name")]
		[UnityEngine.HideInInspector()]
		public bool _Bindname = true;

		[uFrame.MVVM.Attributes.UFGroup("name")]
		[UnityEngine.SerializeField()]
		[UnityEngine.HideInInspector()]
		[UnityEngine.Serialization.FormerlySerializedAsAttribute("_nameonlyWhenChanged")]
		protected bool _nameOnlyWhenChanged;


		[UnityEngine.SerializeField()]
		[uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
		[UnityEngine.HideInInspector()]
		public INT8 _state;

		[uFrame.MVVM.Attributes.UFToggleGroup("state")]
		[UnityEngine.HideInInspector()]
		public bool _Bindstate = true;

		[uFrame.MVVM.Attributes.UFGroup("state")]
		[UnityEngine.SerializeField()]
		[UnityEngine.HideInInspector()]
		[UnityEngine.Serialization.FormerlySerializedAsAttribute("_stateonlyWhenChanged")]
		protected bool _stateOnlyWhenChanged;


		[uFrame.MVVM.Attributes.UFToggleGroup("CreateCellClient")]
		[UnityEngine.HideInInspector()]
		public bool _BindCreateCellClient = true;

		public override string DefaultIdentifier { get { return base.DefaultIdentifier; } }
		public override System.Type ViewModelType { get { return typeof(AvatarViewModel); } }
		public AvatarViewModel Avatar { get { return (AvatarViewModel)ViewModelObject; } }

		public override void Bind() {
			base.Bind();
			if (_Bindlevel) { this.BindProperty(this.Avatar.levelProperty, this.levelChanged, _levelOnlyWhenChanged); }
			if (_Bindmass) { this.BindProperty(this.Avatar.massProperty, this.massChanged, _massOnlyWhenChanged); }
			if (_BindmodelID) { this.BindProperty(this.Avatar.modelIDProperty, this.modelIDChanged, _modelIDOnlyWhenChanged); }
			if (_BindmodelScale) { this.BindProperty(this.Avatar.modelScaleProperty, this.modelScaleChanged, _modelScaleOnlyWhenChanged); }
			if (_BindmoveSpeed) { this.BindProperty(this.Avatar.moveSpeedProperty, this.moveSpeedChanged, _moveSpeedOnlyWhenChanged); }
			if (_Bindname) { this.BindProperty(this.Avatar.nameProperty, this.nameChanged, _nameOnlyWhenChanged); }
			if (_Bindstate) { this.BindProperty(this.Avatar.stateProperty, this.stateChanged, _stateOnlyWhenChanged); }
			if (_BindCreateCellClient) {  this.BindCommandExecuted(this.Avatar.CreateCellClientCommand, this.CreateCellClientExecuted);  }

		}

		public virtual void levelChanged(Byte arg1) {  }
		public virtual void massChanged(Int32 arg1) {  }
		public virtual void modelIDChanged(Byte arg1) {  }
		public virtual void modelScaleChanged(float arg1) {  }
		public virtual void moveSpeedChanged(float arg1) {  }
		public virtual void nameChanged(string arg1) {  }
		public virtual void stateChanged(SByte arg1) {  }

		public virtual void CreateCellClientExecuted(AvatarCreateCellClientCommand command) {  }
	}
}