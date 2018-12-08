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

	public class AvatarControllerBase : uFrame.MVVM.Controller
	{
		private uFrame.MVVM.ViewModels.IViewModelManager<AvatarViewModel> _AvatarViewModelManager;

		[uFrame.IOC.InjectAttribute("Avatar")]
		public uFrame.MVVM.ViewModels.IViewModelManager<AvatarViewModel> AvatarViewModelManager {
			get { return _AvatarViewModelManager; }
			set { _AvatarViewModelManager = value; } }

		public IEnumerable<AvatarViewModel> AvatarViewModels {
			get { return AvatarViewModelManager.ViewModels; } }

		public override void Setup() { base.Setup(); }

		public override void Initialize(uFrame.MVVM.ViewModels.ViewModel viewModel) {
			base.Initialize(viewModel);
			this.InitializeAvatar(((AvatarViewModel)(viewModel))); }

		public virtual AvatarViewModel CreateAvatar() {
			return ((AvatarViewModel)(this.Create(Guid.NewGuid().ToString()))); }

		public override uFrame.MVVM.ViewModels.ViewModel CreateEmpty() {
			return new AvatarViewModel(this.EventAggregator); }

		public virtual void InitializeAvatar(AvatarViewModel viewModel) {
			AvatarViewModelManager.Add(viewModel); }

		public override void DisposingViewModel(uFrame.MVVM.ViewModels.ViewModel viewModel) {
			base.DisposingViewModel(viewModel);
			AvatarViewModelManager.Remove(viewModel); }

	}
}