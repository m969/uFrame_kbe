using System;
using System.Collections.Generic;
using System.Reflection;
using uFrame.IOC;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.MVVM.Bindings;
using uFrame.MVVM.ViewModels;
using UnityEngine;

namespace uFrame.MVVM
{
    public static class MVVMKernelExtensions
    {
        public static void RegisterViewModel<TViewModel>(this IUFrameContainer container, TViewModel viewModel,
            string identifier) where TViewModel : ViewModel
        {
            container.Register<TViewModel, TViewModel>();
            container.RegisterInstance<ViewModel>(viewModel, identifier);
            container.RegisterInstance(typeof (TViewModel), viewModel, identifier);
            container.RegisterInstance(viewModel.GetType(), viewModel, identifier);
        }

        public static void RegisterController<TController>(this IUFrameContainer container, TController controller)
            where TController : Controller
        {
            container.RegisterInstance<Controller>(controller, controller.GetType().Name, false);
            container.RegisterInstance<ISystemService>(controller, controller.GetType().Name, false);
            container.RegisterInstance<TController>(controller, false);

            // Todo Convention hack make it prettier :)
            container.RegisterInstance<Controller>(controller, typeof(TController).Name.ReplaceLast("Controller","ViewModel"));
        }

        public static void RegisterViewModelManager<TViewModel>(this IUFrameContainer container,
            IViewModelManager<TViewModel> manager)
            where TViewModel : ViewModel
        {
            container.RegisterInstance<IViewModelManager>(manager, typeof (TViewModel).Name.Replace("ViewModel", ""));
            container.RegisterInstance<IViewModelManager>(manager, typeof (TViewModel).Name);
            container.RegisterInstance(manager,
                typeof (TViewModel).Name.Replace("ViewModel", ""));
            container.RegisterInstance(manager);
        }

        public static void RegisterViewModelController<TController, TViewModel>(this IUFrameContainer container,
            TController controller) where TController : Controller
        {

        }

        public static T CreateViewModel<T>(this ISystemService s, string identifier = null) where T : ViewModel
        {
            return (T) s.CreateViewModel(typeof (T), identifier);
        }

        public static ViewModel CreateViewModel(this ISystemService s, Type type, string identifier = null)
        {
            var controller = uFrameKernel.Container.Resolve<Controller>(type.Name);
            if (controller == null)
            {
                throw new Exception(
                    "Controller could not be found.  Make sure your subsystem loader has been attached to the kernel.");

            }
            return controller.Create(identifier ?? Guid.NewGuid().ToString());
        }

        public static T CreateViewModel<T>() where T : ViewModel
        {
            return (T) CreateViewModel(typeof (T));
        }

        public static ViewModel CreateViewModel(Type type, string identifier = null)
        {
            var controller = uFrameKernel.Container.Resolve<Controller>(type.Name);
            if (controller == null)
            {
                throw new Exception(
                    "Controller could not be found.  Make sure your subsystem loader has been attached to the kernel.");
            }
            return controller.Create(identifier ?? Guid.NewGuid().ToString());
        }

        public static TViewModel CreateViewModel<TViewModel>(this ISystemLoader loader, string identifier = null)
            where TViewModel : ViewModel
        {
            var controller = uFrameKernel.Container.Resolve<Controller>(typeof (TViewModel).Name);
            return (TViewModel) controller.Create(identifier ?? Guid.NewGuid().ToString());
        }
        public static IDisposable DisposeWith(this IDisposable disposable, ViewModel container)
        {
            return container.AddBinding(disposable);
        }

        public static string ReplaceLast(this string source, string expr, string replacement)
        {
            int ndx = source.LastIndexOf(expr);
            if (ndx < 0) return source;
            string result = source.Remove(ndx, expr.Length).Insert(ndx, replacement);
            return result;
        }

    }
}
