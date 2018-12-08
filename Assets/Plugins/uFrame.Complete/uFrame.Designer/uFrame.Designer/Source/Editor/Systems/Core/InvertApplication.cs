using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using uFrame.Editor.Core.MultiThreading;
using uFrame.IOC;
using UnityEngine;

namespace uFrame.Editor.Core
{
    public static class InvertApplication
    {
        public static bool IsTestMode { get; set; }
        public static IDebugLogger Logger
        {
            get { return _logger ?? (_logger = new DefaultLogger()); }
            set { _logger = value; }
        }

        public static IEnumerable<Assembly> TypeAssemblies {
            get {
                return _typeAssemblies.GetAssemblies();
            }
        }
        
        public static IEnumerable<Assembly> CachedAssemblies {
            get {
                return _cachedAssemblies.GetAssemblies();
            }
        }

        private static UFrameContainer _container;
        private static ICorePlugin[] _plugins;
        private static IDebugLogger _logger;
        private static Dictionary<Type, IEventManager> _eventManagers;

        private static AssembliesTypesCache _cachedAssemblies;
        private static AssembliesTypesCache _typeAssemblies;

        static InvertApplication()
        {
            _typeAssemblies = new AssembliesTypesCache();

            _cachedAssemblies = new AssembliesTypesCache();
            _cachedAssemblies.AddAssembly(typeof(int).Assembly);
            _cachedAssemblies.AddAssembly(typeof(List<>).Assembly);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Debug.WriteLine(assembly.FullName);
                if (assembly.FullName.StartsWith("Invert"))
                {
                    CacheAssembly(assembly);
                }
            }
        }

        public static void LoadPluginsFolder(string pluginsFolder)
        {
            if (!Directory.Exists(pluginsFolder))
            {
                Directory.CreateDirectory(pluginsFolder);
            }
            foreach (var plugin in Directory.GetFiles(pluginsFolder, "*.dll"))
            {
                var assembly = Assembly.LoadFrom(plugin);
                assembly = AppDomain.CurrentDomain.Load(assembly.GetName());
                InvertApplication.CacheAssembly(assembly);
            }
        }
        public static UFrameContainer Container
        {
            get
            {
                if (_container != null) return _container;
                _container = new UFrameContainer();
                InitializeContainer(_container);
                return _container;
            }
            set
            {
                _container = value;
                if (_container == null)
                {
                    IEventManager eventManager;
                    EventManagers.TryGetValue(typeof (ISystemResetEvents), out eventManager);
                    EventManagers.Clear();
                    var events = eventManager as EventManager<ISystemResetEvents>;
                    if (events != null)
                    {
                        events.Signal(_=>_.SystemResetting());
                    }
                }
            }
        }

        public static IEnumerable<Type> GetDerivedTypes<T>(bool includeAbstract = false, bool includeBase = true)
        {
            var type = typeof(T);
            if (includeBase)
                yield return type;
            if (includeAbstract)
            {
                foreach (var assembly in _cachedAssemblies.AssemblyTypesInfos)
                {
                    //if (!assembly.FullName.StartsWith("Invert")) continue;
                    foreach (var t in assembly
                        .Types
                        .Where(x => type.IsAssignableFrom(x)))
                    {
                        yield return t;
                    }
                }
            }
            else
            {
                var items = new List<Type>();
                foreach (var assembly in _cachedAssemblies.AssemblyTypesInfos)
                {
                    //if (!assembly.FullName.StartsWith("Invert")) continue;
                    try
                    {
                        foreach (var t in assembly
                            .Types
                            .Where(x => type.IsAssignableFrom(x) && !x.IsAbstract))
                        {
                            items.Add(t);
                        }
                    }
                    catch (Exception ex)
                    {
                        InvertApplication.Log(ex.Message);
                    }
                }
                foreach (var item in items)
                    yield return item;
            }
        }

        public static Type FindTypeByName(string name)
        {
            return _cachedAssemblies.FindTypeByName(name);
        }

        public static Type FindTypeByNameExternal(string name)
        {
            return _cachedAssemblies.FindTypeByName(name);
        }

        public static Type FindRuntimeTypeByName(string name)
        {
            return _typeAssemblies.FindTypeByName(name);
        }

        public static ICorePlugin[] Plugins
        {
            get
            {
                return _plugins ?? (_plugins = Container.ResolveAll<ICorePlugin>().ToArray());
            }
            set { _plugins = value; }
        }

        private static void InitializeContainer(IUFrameContainer container)
        {
            _plugins = null;
            container.RegisterInstance<IUFrameContainer>(container);
            var pluginTypes = GetDerivedTypes<ICorePlugin>(false, false).ToArray();
            // Load all plugins
            foreach (var diagramPlugin in pluginTypes)
            {
                if (pluginTypes.Any(p => p.BaseType == diagramPlugin)) continue;
                var pluginInstance = Activator.CreateInstance((Type) diagramPlugin) as ICorePlugin;
                if (pluginInstance == null) continue;
                container.RegisterInstance(pluginInstance, diagramPlugin.Name, false);
                container.RegisterInstance(pluginInstance.GetType(), pluginInstance);
                if (pluginInstance.Enabled)
                {

                    foreach (var item in diagramPlugin.GetInterfaces())
                    {
                        ListenFor(item, pluginInstance);
                    }
                }

            }

            container.InjectAll();

            foreach (var diagramPlugin in Plugins.OrderBy(p => p.LoadPriority).Where(p=>!p.Ignore))
            {

                if (diagramPlugin.Enabled)
                {
                    //var start = DateTime.Now;
                    diagramPlugin.Container = Container;
                    diagramPlugin.Initialize(Container);

                }


            }

            foreach (var diagramPlugin in Plugins.OrderBy(p => p.LoadPriority).Where(p => !p.Ignore))
            {

                if (diagramPlugin.Enabled)
                {
                    var start = DateTime.Now;
                    container.Inject(diagramPlugin);
                    diagramPlugin.Loaded(Container);
                    diagramPlugin.LoadTime = DateTime.Now.Subtract(start);
                }


            }
            SignalEvent<ISystemResetEvents>(_=>_.SystemRestarted());
        }

        private static Dictionary<Type, IEventManager> EventManagers
        {
            get { return _eventManagers ?? (_eventManagers = new Dictionary<Type, IEventManager>()); }
            set { _eventManagers = value; }
        }

        public static Action ListenFor(Type eventInterface, object listenerObject)
        {
            var listener = listenerObject;

            IEventManager manager;
            if (!EventManagers.TryGetValue(eventInterface, out manager))
            {
                EventManagers.Add(eventInterface, manager = (IEventManager) Activator.CreateInstance(typeof(EventManager<>).MakeGenericType(eventInterface)));
            }
            var m = manager as IEventManager;


            return m.AddListener(listener);
        }
        /// <summary>
        /// Subscribes to a series of related events defined by an interface.
        /// </summary>
        /// <typeparam name="TEvents">The interface type the describes the events.</typeparam>
        /// <param name="listener">The listener that implements the event interface TEvents.</param>
        public static Action ListenFor<TEvents>(TEvents listener) where TEvents : class
        {
            IEventManager manager;
            if (!EventManagers.TryGetValue(typeof (TEvents), out manager))
            {
                EventManagers.Add(typeof (TEvents), manager = new EventManager<TEvents>());
            }
            var m = manager as EventManager<TEvents>;
            if (m.Listeners.Contains(listener))
                return () => m.Listeners.Remove(listener);
            return m.Subscribe(listener);
        }
        /// <summary>
        /// Subscribes to a series of related events defined by an interface.
        /// </summary>
        /// <typeparam name="TEvents">The interface type the describes the events.</typeparam>
        /// <param name="listenerObject">The listener that implements the event interface TEvents.</param>
        public static Action ListenFor<TEvents>(object listenerObject) where TEvents : class
        {
            var listener = listenerObject as TEvents;
            if (listener == null)
            {
                throw new Exception(string.Format("Listener object is not of type {0}", typeof(TEvents).Name));
            }
            IEventManager manager;
            if (!EventManagers.TryGetValue(typeof(TEvents), out manager))
            {
                EventManagers.Add(typeof(TEvents), manager = new EventManager<TEvents>());
            }
            var m = manager as EventManager<TEvents>;
            if (m.Listeners.Contains(listener))
                return () => m.Listeners.Remove(listener);
            return m.Subscribe(listener);
        }
        /// <summary>
        /// Signals and event to all listeners
        /// </summary>
        /// <typeparam name="TEvents">The lambda that invokes the action.</typeparam>
        public static void SignalEvent<TEvents>(Action<TEvents> action) where TEvents : class
        {
            IEventManager manager;
            if (!EventManagers.TryGetValue(typeof(TEvents), out manager))
            {
                EventManagers.Add(typeof(TEvents), manager = new EventManager<TEvents>());
            }
            var m = manager as EventManager<TEvents>;
            m.Signal(action);
        }
        public static void Execute(Action action)
        {
            Execute(new LambdaCommand("Unknown Command", action));
        }
        public static void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            SignalEvent<ICommandExecuting>(_ => _.CommandExecuting(command));
            try
            {
                SignalEvent<IExecuteCommand<TCommand>>(_ => _.Execute(command));
            }
            catch (Exception ex)
            {
                SignalEvent<INotify>(_=>_.NotifyWithActions(ex.Message,NotificationIcon.Error,new NotifyActionItem()
                {
                    Title = "More...",
                    Action = () =>
                    {
                        SignalEvent<IShowExceptionDetails>(__ => __.ShowExceptionDetails(new Problem()
                        {
                            Exception = ex
                        }));
                    }
                }));
#if DEBUG
                LogException(ex);
#endif
            }
            SignalEvent<ICommandExecuted>(_ => _.CommandExecuted(command));
        }

        public static BackgroundTask ExecuteInBackground<TCommand>(TCommand command)
            where TCommand : IBackgroundCommand
        {
            SignalEvent<ICommandExecuting>(_ => _.CommandExecuting(command));

            var cmd = new BackgroundTaskCommand()
            {
                Command = command,
                Action = (c) =>
                {

                    SignalEvent<IExecuteCommand<TCommand>>(_ => _.Execute((TCommand)c));

                }
            };
            SignalEvent<ICommandExecuted>(_ => _.CommandExecuted(command));
            SignalEvent<IExecuteCommand<BackgroundTaskCommand>>(m=>
            {
                m.Execute(cmd);
            });

            return cmd.Task;
        }

        public static void Execute(ICommand command)
        {
            SignalEvent<ICommandExecuting>(_ => _.CommandExecuting(command));
            var type = typeof (IExecuteCommand<>).MakeGenericType(command.GetType());
            IEventManager manager;
            if (!EventManagers.TryGetValue(type, out manager))
            {
                EventManagers.Add(type, manager = Activator.CreateInstance(typeof(EventManager<>).MakeGenericType(type)) as IEventManager);
            }
            manager.Signal(listener => listener.GetType().GetMethod("Execute",new Type[] {command.GetType()}).Invoke(listener, new object[] { command }));
            SignalEvent<ICommandExecuted>(_ => _.CommandExecuted(command));
        }
        public static void Log(string s)
        {
#if DEBUG
            Logger.Log(s);
            //Debug.Log(s);
#endif
        }

        public static IEnumerable<KeyValuePair<PropertyInfo, TAttribute>> GetPropertiesWithAttribute<TAttribute>(this object obj) where TAttribute : Attribute
        {
            return GetPropertiesWithAttributeByType<TAttribute>(obj.GetType());
        }

        public static IEnumerable<KeyValuePair<PropertyInfo, TAttribute>> GetPropertiesWithAttributeByType<TAttribute>(this Type type, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance) where TAttribute : Attribute
        {
            foreach (var source in type.GetProperties(flags).ToArray())
            {
                var attribute = source.GetCustomAttributes(typeof (TAttribute), true).OfType<TAttribute>().FirstOrDefault();
                if (attribute == null) continue;
                yield return new KeyValuePair<PropertyInfo, TAttribute>(source, (TAttribute)attribute);
            }
        }
        public static IEnumerable<KeyValuePair<ConstructorInfo, TAttribute>> GetConstructorsWithAttribute<TAttribute>(this Type type, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance) where TAttribute : Attribute
        {
            foreach (var source in type.GetConstructors(flags))
            {
                var attribute = source.GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().FirstOrDefault();
                if (attribute == null) continue;
                yield return new KeyValuePair<ConstructorInfo, TAttribute>(source, (TAttribute)attribute);
            }
        }
        public static IEnumerable<KeyValuePair<MethodInfo, TAttribute>> GetMethodsWithAttribute<TAttribute>(this Type type, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance) where TAttribute : Attribute
        {
            foreach (var source in type.GetMethods(flags))
            {
                var attribute = source.GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().FirstOrDefault();
                if (attribute == null) continue;
                yield return new KeyValuePair<MethodInfo, TAttribute>(source, (TAttribute)attribute);
            }
        }
        public static IEnumerable<KeyValuePair<FieldInfo, TAttribute>> GetFieldsWithAttribute<TAttribute>(this Type type, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance) where TAttribute : Attribute
        {
            foreach (var source in type.GetFields(flags))
            {
                var attribute = source.GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().FirstOrDefault();
                if (attribute == null) continue;
                yield return new KeyValuePair<FieldInfo, TAttribute>(source, (TAttribute)attribute);
            }
        }
        public static IEnumerable<KeyValuePair<EventInfo, TAttribute>> GetEventsWithAttribute<TAttribute>(this Type type, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance) where TAttribute : Attribute
        {
            foreach (var source in type.GetEvents(flags))
            {
                var attribute = source.GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().FirstOrDefault();
                if (attribute == null) continue;
                yield return new KeyValuePair<EventInfo, TAttribute>(source, (TAttribute)attribute);
            }
        }
        public static IEnumerable<PropertyInfo> GetPropertiesByAttribute<TAttribute>(this object obj) where TAttribute : Attribute
        {
            return GetPropertiesByAttribute<TAttribute>(obj.GetType());
        }

        public static IEnumerable<PropertyInfo> GetPropertiesByAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.GetCustomAttributes(typeof(TAttribute), true).Length > 0);
        }



        public static Type GetGenericParameter(this Type type)
        {
            var t = type;
            while (t != null)
            {
                if (t.IsGenericType)
                {
                    return t.GetGenericArguments().FirstOrDefault();
                }
                t = t.BaseType;
            }
            return null;
        }

        public static void LogException(Exception exception)
        {
            Logger.LogException(exception);

        }

        public static void LogError(string format)
        {
            Logger.Log(format);
        }

        public static void LogIfNull(object obj, string s)
        {
            if (obj == null)
            {
                LogError(string.Format("{0} is NULL!!", s ?? "YUP it "));
            }
        }

        public static void CacheAssembly(Assembly assembly)
        {
            _cachedAssemblies.AddAssembly(assembly);
        }

        public static void CacheTypeAssembly(Assembly assembly)
        {

            _typeAssemblies.AddAssembly(assembly);
        }

        private class AssemblyTypesInfo {
            public readonly Assembly Assembly;
            public readonly Type[] Types;
            public readonly StringTypeTuple[] NameToType;
            public readonly Dictionary<string, Type> FullNameToType = new Dictionary<string, Type>();

            public AssemblyTypesInfo(Assembly assembly) {
                Assembly = assembly;

                Type[] types = assembly.GetTypes();
                NameToType = new StringTypeTuple[types.Length];
                for (int i = 0; i < types.Length; i++) {
                    Type type = types[i];
                    NameToType[i] = new StringTypeTuple(type.Name, type);
                    FullNameToType.Add(type.FullName, type);
                }

                Types = types;
            }

            public Type FindTypeByName(string name) {
                Type type;
                if (!FullNameToType.TryGetValue(name, out type)) {
                    for (int i = 0; i < NameToType.Length; i++) {
                        StringTypeTuple tuple = NameToType[i];
                        if (tuple.String == name)
                            return tuple.Type;
                    }
                }
                return type;
            }

            public struct StringTypeTuple {
                public readonly string String;
                public readonly Type Type;

                public StringTypeTuple(string s, Type type) {
                    String = s;
                    Type = type;
                }
            }
        }

        private class AssembliesTypesCache {
            public readonly List<AssemblyTypesInfo> AssemblyTypesInfos = new List<AssemblyTypesInfo>();

            public void AddAssembly(Assembly assembly) {
                if (assembly == null)
                    throw new ArgumentNullException("assembly");

                // Skip if already exists
                foreach (AssemblyTypesInfo assemblyTypesInfo in AssemblyTypesInfos) {
                    if (assemblyTypesInfo.Assembly == assembly)
                        return;
                }

                AssemblyTypesInfos.Add(new AssemblyTypesInfo(assembly));
            }

            public Type FindTypeByName(string name) {
                if (string.IsNullOrEmpty(name))
                    return null;

                foreach (AssemblyTypesInfo info in AssemblyTypesInfos) {
                    Type type = info.FindTypeByName(name);
                    if (type != null)
                        return type;
                }

                return null;
            }

            public IEnumerable<Assembly> GetAssemblies() {
                foreach (AssemblyTypesInfo assemblyTypesInfo in AssemblyTypesInfos) {
                    yield return assemblyTypesInfo.Assembly;
                }
            }
        }
    }
}