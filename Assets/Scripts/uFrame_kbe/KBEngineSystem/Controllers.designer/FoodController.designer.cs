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

	public class FoodControllerBase : uFrame.MVVM.Controller
	{
		private uFrame.MVVM.ViewModels.IViewModelManager<FoodViewModel> _FoodViewModelManager;

		[uFrame.IOC.InjectAttribute("Food")]
		public uFrame.MVVM.ViewModels.IViewModelManager<FoodViewModel> FoodViewModelManager {
			get { return _FoodViewModelManager; }
			set { _FoodViewModelManager = value; } }

		public IEnumerable<FoodViewModel> FoodViewModels {
			get { return FoodViewModelManager.ViewModels; } }

		public override void Setup() { base.Setup(); }

		public override void Initialize(uFrame.MVVM.ViewModels.ViewModel viewModel) {
			base.Initialize(viewModel);
			this.InitializeFood(((FoodViewModel)(viewModel))); }

		public virtual FoodViewModel CreateFood() {
			return ((FoodViewModel)(this.Create(Guid.NewGuid().ToString()))); }

		public override uFrame.MVVM.ViewModels.ViewModel CreateEmpty() {
			return new FoodViewModel(this.EventAggregator); }

		public virtual void InitializeFood(FoodViewModel viewModel) {
			FoodViewModelManager.Add(viewModel); }

		public override void DisposingViewModel(uFrame.MVVM.ViewModels.ViewModel viewModel) {
			base.DisposingViewModel(viewModel);
			FoodViewModelManager.Remove(viewModel); }

	}
}