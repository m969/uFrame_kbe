// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Example {
    using Example;
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
    
    
    public partial class LevelSelectScreenViewModelBase : SubScreenViewModel {
        
        private ModelCollection<LevelDescriptor> _AvailableLevels;
        
        private Signal<SelectLevelCommand> _SelectLevel;
        
        public LevelSelectScreenViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
        
        public virtual ModelCollection<LevelDescriptor> AvailableLevels {
            get {
                return _AvailableLevels;
            }
            set {
                _AvailableLevels = value;
            }
        }
        
        public virtual Signal<SelectLevelCommand> SelectLevel {
            get {
                return _SelectLevel;
            }
            set {
                _SelectLevel = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            this.SelectLevel = new Signal<SelectLevelCommand>(this);
            _AvailableLevels = new ModelCollection<LevelDescriptor>(this, "AvailableLevels");
        }
        
        public virtual void ExecuteSelectLevel(LevelDescriptor argument) {
            this.SelectLevel.OnNext(new SelectLevelCommand(){Argument = argument});
        }
        
        public override void Read(uFrame.Kernel.Serialization.ISerializerStream stream) {
            base.Read(stream);
        }
        
        public override void Write(uFrame.Kernel.Serialization.ISerializerStream stream) {
            base.Write(stream);
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModels.ViewModelCommandInfo> list) {
            base.FillCommands(list);
            list.Add(new ViewModelCommandInfo("SelectLevel", SelectLevel) { ParameterType = typeof(LevelDescriptor) });
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModels.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
            list.Add(new ViewModelPropertyInfo(_AvailableLevels, false, true, false, false));
        }
    }
    
    public partial class LevelSelectScreenViewModel {
        
        public LevelSelectScreenViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
}
