// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace KbeBalls {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.Services;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    
    
    public class EntityCommonViewBase : uFrame.MVVM.Views.ViewBase {
        
        [UnityEngine.SerializeField()]
        [uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Vector3 _Position;
        
        [UnityEngine.SerializeField()]
        [uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Vector3 _Direction;
        
        [UnityEngine.SerializeField()]
        [uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Int32 _modelID;
        
        [uFrame.MVVM.Attributes.UFToggleGroup("Position")]
        [UnityEngine.HideInInspector()]
        public bool _BindPosition = true;
        
        [uFrame.MVVM.Attributes.UFGroup("Position")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_PositiononlyWhenChanged")]
        protected bool _PositionOnlyWhenChanged;
        
        [uFrame.MVVM.Attributes.UFToggleGroup("Direction")]
        [UnityEngine.HideInInspector()]
        public bool _BindDirection = true;
        
        [uFrame.MVVM.Attributes.UFGroup("Direction")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_DirectiononlyWhenChanged")]
        protected bool _DirectionOnlyWhenChanged;
        
        [uFrame.MVVM.Attributes.UFToggleGroup("modelID")]
        [UnityEngine.HideInInspector()]
        public bool _BindmodelID = true;
        
        [uFrame.MVVM.Attributes.UFGroup("modelID")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_modelIDonlyWhenChanged")]
        protected bool _modelIDOnlyWhenChanged;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(EntityCommonViewModel);
            }
        }
        
        public EntityCommonViewModel EntityCommon {
            get {
                return (EntityCommonViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as EntityCommonViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var entitycommonview = ((EntityCommonViewModel)model);
            entitycommonview.Position = this._Position;
            entitycommonview.Direction = this._Direction;
            entitycommonview.modelID = this._modelID;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.EntityCommon to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindPosition) {
                this.BindProperty(this.EntityCommon.PositionProperty, this.PositionChanged, _PositionOnlyWhenChanged);
            }
            if (_BindDirection) {
                this.BindProperty(this.EntityCommon.DirectionProperty, this.DirectionChanged, _DirectionOnlyWhenChanged);
            }
            if (_BindmodelID) {
                this.BindProperty(this.EntityCommon.modelIDProperty, this.modelIDChanged, _modelIDOnlyWhenChanged);
            }
        }
        
        public virtual void PositionChanged(Vector3 arg1) {
        }
        
        public virtual void DirectionChanged(Vector3 arg1) {
        }
        
        public virtual void modelIDChanged(Int32 arg1) {
        }
    }
}
