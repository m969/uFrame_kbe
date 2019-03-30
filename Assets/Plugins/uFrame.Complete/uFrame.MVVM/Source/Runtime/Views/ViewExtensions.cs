using System;
using uFrame.Kernel;
using uFrame.MVVM.Events;
using uFrame.MVVM.Services;
using uFrame.MVVM.ViewModels;
using UnityEngine;
using Object = UnityEngine.Object;

namespace uFrame.MVVM.Views
{
    public static class ViewExtensions
    {
        public static ViewBase GetView(this GameObject go)
        {
            return go.GetComponent<ViewBase>();
        }

        public static T GetView<T>(this GameObject go) where T : class
        {
            return GetView(go) as T;
        }

        public static ViewBase GetView(this Collision go)
        {
            return GetView(go.collider);
        }

        public static T GetView<T>(this Collision go) where T : class
        {
            return GetView(go.collider) as T;
        }

        public static ViewBase GetView(this Transform go)
        {
            return go.GetComponent<ViewBase>();
        }

        public static T GetView<T>(this Transform go) where T : class
        {
            return GetView(go) as T;
        }

        public static ViewBase GetView(this Collider go)
        {
            return go.GetComponent<ViewBase>();
        }

        public static T GetView<T>(this Collider go) where T : class
        {
            return GetView(go) as T;
        }

        public static ViewBase GetView(this MonoBehaviour go)
        {
            if (go == null) return null;
            return go.GetComponent<ViewBase>();
        }

        public static T GetView<T>(this MonoBehaviour go) where T : class
        {
            return GetView(go) as T;
        }

        public static ViewModel GetViewModel(this GameObject go)
        {
            var view = GetView(go);
            if (view == null)
                return null;
            return view.ViewModelObject;
        }

        public static T GetViewModel<T>(this GameObject go) where T : class
        {
            return GetViewModel(go) as T;
        }

        public static T GetViewModel<T>(this Collision go) where T : class
        {
            return GetViewModel(go.collider) as T;
        }

        public static ViewModel GetViewModel(this Transform go)
        {
            var view = GetView(go);
            if (view == null)
                return null;
            return view.ViewModelObject;
        }

        public static T GetViewModel<T>(this Transform go) where T : class
        {
            return GetViewModel(go) as T;
        }

        public static ViewModel GetViewModel(this Collider go)
        {
            var view = GetView(go);
            if (view == null)
                return null;
            return view.ViewModelObject;
        }

        public static ViewModel GetViewModel(this Collision go)
        {
            var view = GetView(go.collider);
            if (view == null)
                return null;
            return view.ViewModelObject;
        }

        public static T GetViewModel<T>(this Collider go) where T : class
        {
            return GetViewModel(go) as T;
        }

        public static ViewModel GetViewModel(this MonoBehaviour go)
        {
            var view = GetView(go);
            if (view == null)
                return null;
            return view.ViewModelObject;
        }

        public static T GetViewModel<T>(this MonoBehaviour go) where T : class
        {
            return GetViewModel(go) as T;
        }

        public static ViewBase InstantiateView(this Transform parent, GameObject prefab, ViewModel model,
            string identifier = null)
        {
            return InstantiateView(parent, prefab, model, Vector3.zero, identifier);
        }

        public static ViewBase InstantiateView(this Transform parent, string viewName, ViewModel model,
            string identifier = null)
        {
            return InstantiateView(parent, viewName, model, Vector3.zero, identifier);
        }

        public static ViewBase InstantiateView(this Transform parent, string viewName, ViewModel model, Vector3 position,
            string identifier = null)
        {
            return InstantiateView(parent, viewName, model, position, Quaternion.identity, identifier);
        }

        public static ViewBase InstantiateView(this Transform parent, string viewName, ViewModel model, Vector3 position,
            Quaternion rotation, string identifier = null)
        {
            return InstantiateView(parent, ViewService.ViewResolver.FindView(viewName), model, position, rotation,
                identifier);
        }

        public static ViewBase InstantiateView(this Transform parent, GameObject prefab, ViewModel model,
            Vector3 position, string identifier = null)
        {
            return InstantiateView(parent, prefab, model, position, Quaternion.identity, identifier);
        }

        public static ViewBase InstantiateView(this Transform parent,
            GameObject prefab,
            ViewModel model,
            Vector3 position,
            Quaternion rotation,
            string identifier = null)
        {
            var parentScene = parent.GetComponent<Scene>();
            var command = new InstantiateViewCommand()
            {
                Identifier = identifier,
                ViewModelObject = model,
                Scene = parentScene,
                Prefab = prefab
            };
            uFrameKernel.EventAggregator.Publish(command);
            command.Result.transform.position = position;
            command.Result.transform.rotation = rotation;
            return command.Result;
        }

        public static ViewBase InstantiateView(this Transform parent, ViewModel model, string identifier = null)
        {
            return InstantiateView(parent, model, Vector3.zero, identifier);
        }

        public static ViewBase InstantiateView(this Transform parent, ViewModel model, Vector3 position,
            string identifier = null)
        {
            return InstantiateView(parent, model, position, Quaternion.identity, identifier);
        }

        public static ViewBase InstantiateView(this Transform parent, ViewModel model, Vector3 position,
            Quaternion rotation, string identifier = null)
        {
            return InstantiateView(parent, ViewService.ViewResolver.FindView(model), model, position, rotation,
                identifier);
        }

        public static bool IsView<TView>(this Transform go) where TView : ViewBase
        {
            if (go == null) return false;
            var c = go.GetComponent<TView>();
            return c != null;
        }

        public static bool IsView<TView>(this GameObject go) where TView : ViewBase
        {
            if (go == null) return false;
            var c = go.GetComponent<TView>();
            return c != null;
        }
    }
}