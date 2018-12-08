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

	public class RoomControllerBase : uFrame.MVVM.Controller
	{
		private uFrame.MVVM.ViewModels.IViewModelManager<RoomViewModel> _RoomViewModelManager;

		[uFrame.IOC.InjectAttribute("Room")]
		public uFrame.MVVM.ViewModels.IViewModelManager<RoomViewModel> RoomViewModelManager {
			get { return _RoomViewModelManager; }
			set { _RoomViewModelManager = value; } }

		public IEnumerable<RoomViewModel> RoomViewModels {
			get { return RoomViewModelManager.ViewModels; } }

		public override void Setup() { base.Setup(); }

		public override void Initialize(uFrame.MVVM.ViewModels.ViewModel viewModel) {
			base.Initialize(viewModel);
			this.InitializeRoom(((RoomViewModel)(viewModel))); }

		public virtual RoomViewModel CreateRoom() {
			return ((RoomViewModel)(this.Create(Guid.NewGuid().ToString()))); }

		public override uFrame.MVVM.ViewModels.ViewModel CreateEmpty() {
			return new RoomViewModel(this.EventAggregator); }

		public virtual void InitializeRoom(RoomViewModel viewModel) {
			RoomViewModelManager.Add(viewModel); }

		public override void DisposingViewModel(uFrame.MVVM.ViewModels.ViewModel viewModel) {
			base.DisposingViewModel(viewModel);
			RoomViewModelManager.Remove(viewModel); }

	}
}