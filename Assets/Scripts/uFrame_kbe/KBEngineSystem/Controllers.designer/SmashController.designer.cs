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

	public class SmashControllerBase : uFrame.MVVM.Controller
	{
		private uFrame.MVVM.ViewModels.IViewModelManager<SmashViewModel> _SmashViewModelManager;

		[uFrame.IOC.InjectAttribute("Smash")]
		public uFrame.MVVM.ViewModels.IViewModelManager<SmashViewModel> SmashViewModelManager {
			get { return _SmashViewModelManager; }
			set { _SmashViewModelManager = value; } }

		public IEnumerable<SmashViewModel> SmashViewModels {
			get { return SmashViewModelManager.ViewModels; } }

		public override void Setup() { base.Setup(); }

		public override void Initialize(uFrame.MVVM.ViewModels.ViewModel viewModel) {
			base.Initialize(viewModel);
			this.InitializeSmash(((SmashViewModel)(viewModel))); }

		public virtual SmashViewModel CreateSmash() {
			return ((SmashViewModel)(this.Create(Guid.NewGuid().ToString()))); }

		public override uFrame.MVVM.ViewModels.ViewModel CreateEmpty() {
			return new SmashViewModel(this.EventAggregator); }

		public virtual void InitializeSmash(SmashViewModel viewModel) {
			SmashViewModelManager.Add(viewModel); }

		public override void DisposingViewModel(uFrame.MVVM.ViewModels.ViewModel viewModel) {
			base.DisposingViewModel(viewModel);
			SmashViewModelManager.Remove(viewModel); }

	}
}