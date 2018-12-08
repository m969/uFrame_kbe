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

	public class HallsControllerBase : uFrame.MVVM.Controller
	{
		private uFrame.MVVM.ViewModels.IViewModelManager<HallsViewModel> _HallsViewModelManager;

		[uFrame.IOC.InjectAttribute("Halls")]
		public uFrame.MVVM.ViewModels.IViewModelManager<HallsViewModel> HallsViewModelManager {
			get { return _HallsViewModelManager; }
			set { _HallsViewModelManager = value; } }

		public IEnumerable<HallsViewModel> HallsViewModels {
			get { return HallsViewModelManager.ViewModels; } }

		public override void Setup() { base.Setup(); }

		public override void Initialize(uFrame.MVVM.ViewModels.ViewModel viewModel) {
			base.Initialize(viewModel);
			this.InitializeHalls(((HallsViewModel)(viewModel))); }

		public virtual HallsViewModel CreateHalls() {
			return ((HallsViewModel)(this.Create(Guid.NewGuid().ToString()))); }

		public override uFrame.MVVM.ViewModels.ViewModel CreateEmpty() {
			return new HallsViewModel(this.EventAggregator); }

		public virtual void InitializeHalls(HallsViewModel viewModel) {
			HallsViewModelManager.Add(viewModel); }

		public override void DisposingViewModel(uFrame.MVVM.ViewModels.ViewModel viewModel) {
			base.DisposingViewModel(viewModel);
			HallsViewModelManager.Remove(viewModel); }

	}
}