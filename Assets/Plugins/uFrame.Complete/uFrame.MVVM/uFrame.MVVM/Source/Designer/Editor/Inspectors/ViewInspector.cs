using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using uFrame.Editor.GraphUI;
using uFrame.Editor.GraphUI.ViewModels;
using uFrame.Editor.Unity;
using uFrame.MVVM.StateMachines;
using uFrame.MVVM.ViewModels;
using uFrame.MVVM.Views;
using UnityEditor;
using UnityEngine;

namespace uFrame.MVVM.Editor
{
    [CustomEditor(typeof(ViewBase), true)]
    public class ViewInspector : uFrameInspector
    {
        private List<ViewModelCommandInfo> _commands;

        public static bool ShowInfoLabels
        {
            get { return false; }
        }

        //[MenuItem("uFrame/Info Labels")]
        //public static void ShowHideInfoLabels()
        //{
        //    ShowInfoLabels = !ShowInfoLabels;
        //}
        public bool ShowIdentifierSettings
        {
            get
            {
                return EditorPrefs.GetBool("UFRAME_ShowIdentifierSettings", true);
            }
            set
            {
                EditorPrefs.SetBool("UFRAME_ShowIdentifierSettings", value);
            }
        }
        public bool ShowDefaultSettings
        {
            get
            {
                return EditorPrefs.GetBool("UFRAME_ShowDefaultSettings", true);
            }
            set
            {
                EditorPrefs.SetBool("UFRAME_ShowDefaultSettings", value);
            }
        }
        public bool ShowViewModelSettings
        {
            get
            {
                return EditorPrefs.GetBool("UFRAME_ShowViewModelSettings", true);
            }
            set
            {
                EditorPrefs.SetBool("UFRAME_ShowViewModelSettings", value);
            }
        }

        public bool ShowViewSettings
        {
            get
            {
                return EditorPrefs.GetBool("UFRAME_ShowViewSettings", true);
            }
            set
            {
                EditorPrefs.SetBool("UFRAME_ShowViewSettings", value);
            }
        }

        public void OnSceneGUI()
        {
            Handles.BeginGUI();
            var padding = 10f;
            var titleContent = new GUIContent(target.name);
            var subTitleContent = new GUIContent(target.GetType().Name);
            var titleSize = ElementDesignerStyles.ViewBarTitleStyle.CalcSize(titleContent);
            var subTitleSize = ElementDesignerStyles.ViewBarSubTitleStyle.CalcSize(subTitleContent);
            var maxTextWidth = Mathf.Max(titleSize.x, subTitleSize.x);
            var barWidth = (padding * 4f) + maxTextWidth + (36 * 1);
            var rect = new Rect(15f, 15f, barWidth, 48f);
            ElementDesignerStyles.DrawExpandableBox(rect, ElementDesignerStyles.SceneViewBar, "");
            GUILayout.BeginArea(rect);

            GUILayout.BeginHorizontal();
            GUILayout.Space(padding);
            if (GUILayout.Button(new GUIContent("", "View " + subTitleContent.text + " in Element Designer"), ElementDesignerStyles.EyeBall))
            {
                //uFrameEditorSceneManager.NavigateBack(target as ViewBase);
            }
            GUILayout.Space(padding);
            GUILayout.BeginVertical();
            GUILayout.Space(6f);
            GUILayout.Label(titleContent, ElementDesignerStyles.ViewBarTitleStyle, GUILayout.Width(maxTextWidth));
            GUILayout.Label(subTitleContent, ElementDesignerStyles.ViewBarSubTitleStyle, GUILayout.Width(maxTextWidth));

            GUILayout.EndVertical();
            //GUILayout.Space(padding);
            //if (GUILayout.Button(new GUIContent("", "Move to the previous " + subTitleContent.text), ElementDesignerStyles.NavigatePreviousStyle))
            //{

            //    uFrameEditorSceneManager.NavigatePrevious();
            //}
            //if (GUILayout.Button(new GUIContent("","Move to the next " + subTitleContent.text), ElementDesignerStyles.NavigateNextStyle))
            //{
            //    uFrameEditorSceneManager.NavigateNext();
            //}
            //GUILayout.Space(padding);
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            Handles.EndGUI();
        }

        public void Info(string message)
        {
            if (!ShowInfoLabels) return;
            EditorGUILayout.HelpBox(message, MessageType.Info);
        }
        public void Warning(string message)
        {

            EditorGUILayout.HelpBox(message, MessageType.Warning);
        }
        public override void OnInspectorGUI()
        {
            GUIHelpers.IsInspector = true;
            var t = target as ViewBase;
            if (EditorApplication.isPlaying)
            {

                ShowDefaultSettings = Toggle("Default", ShowDefaultSettings);
                if (ShowDefaultSettings)
                {
                    EditorGUILayout.Space();
                    base.OnInspectorGUI();
                }
                DrawPlayModeGui();
                return;
            }

            ShowDefaultSettings = Toggle("Default", ShowDefaultSettings);
            if (ShowDefaultSettings)
            {

                base.OnInspectorGUI();

            }

            serializedObject.Update();
            ShowIdentifierSettings = Toggle("Initialization", ShowIdentifierSettings);
            if (ShowIdentifierSettings)
            {
                DoInitializationSection(t);
            }

            if (_groupFields == null)
                GetFieldInformation(t);

            if (_groupFields != null)
            {

                foreach (var groupField in _groupFields)
                {
                    if (_toggleGroups.ContainsKey(groupField.Key)) continue;

                    EditorPrefs.SetBool(groupField.Key, Toggle(groupField.Key, EditorPrefs.GetBool(groupField.Key, false)));


                    DoGroupField(groupField, t);
                }
                EditorPrefs.SetBool("UFRAME_BindingsOpen", Toggle("Bindings", EditorPrefs.GetBool("UFRAME_BindingsOpen", false)));

                if (EditorPrefs.GetBool("UFRAME_BindingsOpen", false))
                {

                    DoBindingsSection();
                }

                //var btnContent = new GUIContent("Show In Designer");
                //if (GUI.Button(GUIHelpers.GetRect(ElementDesignerStyles.ButtonStyle), btnContent, ElementDesignerStyles.ButtonStyle))
                //{
                //    uFrameEditorSceneManager.NavigateBack(target as ViewBase);
                //}
            }


            serializedObject.ApplyModifiedProperties();
            GUIHelpers.IsInspector = false;
        }

        private void DoGroupField(KeyValuePair<string, List<FieldInfo>> groupField, ViewBase t)
        {
            if (EditorPrefs.GetBool(groupField.Key, false))
            {

                if (groupField.Key == "View Model Properties" &&
                    !(t.OverrideViewModel)) return;
                foreach (var field in groupField.Value)
                {
                    try
                    {
                        // serializedObject.GetIterator().Reset();
                        var property = serializedObject.FindProperty(field.Name);
                        if (property == null) continue;
                        if (property.propertyType == SerializedPropertyType.Vector2)
                        {
                            var newValue = EditorGUILayout.Vector2Field(property.name, property.vector2Value);
                            if (newValue != property.vector2Value)
                            {
                                property.vector2Value = newValue;
                            }
                        }
                        else if (property.propertyType == SerializedPropertyType.Vector3)
                        {
                            var newValue = EditorGUILayout.Vector3Field(property.name, property.vector3Value);
                            if (newValue != property.vector3Value)
                            {
                                property.vector3Value = newValue;
                            }
                        }
                        else
                        {
                            EditorGUILayout.PropertyField(property);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Log(field.Name + ex.Message);
                    }
                }
            }
        }

        private void DoBindingsSection()
        {
            foreach (var group in _toggleGroups)
            {
                var property = serializedObject.FindProperty(@group.Value.Name);
                EditorGUILayout.PropertyField(property, new GUIContent(property.name.Replace("_", "").Replace("Bind", "")));
                if (property.boolValue)
                {
                    EditorGUI.indentLevel++;
                    if (_groupFields != null)
                    {
                        if (_groupFields.ContainsKey(@group.Key))
                        {
                            foreach (var groupField in _groupFields[@group.Key])
                            {
                                var subProperty = serializedObject.FindProperty(groupField.Name);

                                if (subProperty != null)
                                {
                                    EditorGUILayout.PropertyField(subProperty,
                                        new GUIContent(subProperty.name.Replace(@group.Key, "").Replace("_", "")));
                                }
                            }
                        }
                    }
                    EditorGUI.indentLevel--;
                }
                property.Reset();
            }
        }

        private void DoInitializationSection(ViewBase t)
        {
            EditorGUILayout.Space();

            var resolveNameProperty = serializedObject.FindProperty("_viewModelId");
            var resolutionNameProperty = serializedObject.FindProperty("_viewModelId");
            var injectProperty = serializedObject.FindProperty("_InjectView");
            var bindOnStartProperty = serializedObject.FindProperty("_BindOnStart");
            var overrideProperty = serializedObject.FindProperty("_overrideViewModel");
            var disposeViewModelOnDestroy = serializedObject.FindProperty("_DisposeViewModelOnDestroy");


            if (!string.IsNullOrEmpty(t.DefaultIdentifier) && (resolveNameProperty.stringValue != t.DefaultIdentifier))
                if (GUILayout.Button(string.Format("Use Registered \"{0}\" Instance", t.DefaultIdentifier)))
                {
                    serializedObject.FindProperty("_viewModelId").stringValue = t.DefaultIdentifier;
                }

            Info("If you leave this empty, View will fetch a new viewmodel on awake.\n" +
                 "Otherwise it is going to always use the viewmodel with given identifier.\n" +
                 "If viewmodel with given id does not exist, one will be automatically created and registered.");
            EditorGUILayout.PropertyField(resolutionNameProperty, new GUIContent("ViewModel Identifier"));

            Info("Should this view be injected with Dependencies registered inside of system loaders.");
            EditorGUILayout.PropertyField(injectProperty, new GUIContent("Inject This View"));

            Info("Should this view try to bind on start.");
            EditorGUILayout.PropertyField(bindOnStartProperty, new GUIContent("Bind On Start"));

            Info("When the gameobject/view is destroyed should its dispose of the view-model as well?.");
            EditorGUILayout.PropertyField(disposeViewModelOnDestroy, new GUIContent("Dispose On Destroy"));


            Info("This should always be checked except when you are instantiating it manually, or its using a shared instance that is already being initialized.");
            EditorGUILayout.PropertyField(overrideProperty, new GUIContent("Initialize ViewModel"));
        }

        public ViewBase Target
        {
            get { return (ViewBase)target; }
        }
        public List<ViewModelCommandInfo> Commands
        {
            get
            {
                if (_commands == null)
                {
                    if (Target.ViewModelObject != null)
                    {
                        _commands = Target.ViewModelObject.GetViewModelCommands();
                    }
                }
                return _commands;
            }
        }
        private void DrawPlayModeGui()
        {
            if (EditorApplication.isPlaying)
            {
                var t = target as ViewBase;
                if (t != null && t.ViewModelObject != null)
                {
                    if (GUIHelpers.DoToolbarEx("View Model Properties"))
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.LabelField("Id", t.ViewModelObject.Identifier);
                        EditorGUILayout.LabelField("# References", t.ViewModelObject.References.ToString());
                        DoViewModelGUI(t.ViewModelObject);
                    }

                    if (Commands != null)
                    {
                        if (GUIHelpers.DoToolbarEx("Commands"))
                        {

                            foreach (var command in Commands)
                            {
                                var type = command.ParameterType;

                                if (type != null && type != typeof(void))
                                {
                                    // TODO REWRITE PARAMETERS
                                    //if (!(command.Command is IParameterCommand)) continue;
                                    //EditorGUI.BeginChangeCheck();
                                    //object newValue = null;
                                    //object currentValue = ((IParameterCommand)command.Command).Parameter ?? GetDefaultValue(type);//Activator.CreateInstance(type);

                                    //var propertyName = "Parameter";
                                    //var isEnum = false;
                                    //newValue = DoTypeInspector(type, propertyName, currentValue, isEnum);

                                    //if (EditorGUI.EndChangeCheck())
                                    //{
                                    //    ((IParameterCommand) command.Command).Parameter = newValue;
                                    //}
                                }
                                if (GUI.Button(GUIHelpers.GetRect(ElementDesignerStyles.ButtonStyle), command.Name,
                                    ElementDesignerStyles.ButtonStyle))
                                {
                                    command.Signal.Publish();
                                }
                            }
                            //foreach (var command in Commands)
                            //{
                            //    if (GUI.Button(GUIHelpers.GetRect(ElementDesignerStyles.ButtonStyle), command.Key,
                            //        ElementDesignerStyles.ButtonStyle))
                            //    {
                            //        var commandWith = command.Value as ICommandWith<object>;



                            //        Target.ExecuteCommand(command.Value);
                            //    }
                            //}
                        }


                    }
                    //if (t.ViewModelObject != null)
                    //{
                    //    foreach (var item in t.ViewModelObject.Bindings)
                    //    {
                    //        if (GUIHelpers.DoToolbarEx(item.Key == -1
                    //            ? "Controller"
                    //            : EditorUtility.InstanceIDToObject(item.Key).name))
                    //        {
                    //            foreach (var binding in item.Value)
                    //            {

                    //                if (GUIHelpers.DoTriggerButton(new UFStyle()
                    //                {
                    //                    Label = binding.GetType().Name + ": " + binding.ModelMemberName,
                    //                    //IconStyle = bi
                    //                }))
                    //                {

                    //                }
                    //            }

                    //        }


                    //    }
                    //}
                }
                else
                {
                    EditorGUILayout.HelpBox("View Model not initialized yet.", MessageType.Info);
                    Repaint();
                }

            }
            Repaint();
            return;
        }

        private static void DoViewModelGUI(uFrame.MVVM.ViewModels.ViewModel t)
        {
            if (t == null) return;

            var properties = t.GetViewModelProperties();
            foreach (var property in properties)
            {
                var type = property.Property.ValueType;
                DoViewModelProperty(type, property);
            }
        }

        private static void DoViewModelProperty(Type type, ViewModelPropertyInfo property)
        {

            if (property.IsCollectionProperty)
            {
                if (property.Property != null && property.Property.ObjectValue != null)
                    EditorGUILayout.LabelField(property.Property.PropertyName, ((IList)property.Property.ObjectValue).Count.ToString());
                return;
            }
            if (property.IsComputed)
            {
                if (property.Property != null && property.Property.ObjectValue != null)
                    EditorGUILayout.LabelField(property.Property.PropertyName, property.Property.ObjectValue.ToString());
                return;
            }
            else if (property.Property.ValueType == typeof(State))
            {
                EditorGUILayout.LabelField(property.Property.PropertyName, property.Property.ObjectValue.ToString());
                var _designerWidnow = InvertGraphEditor.DesignerWindow;
                if (_designerWidnow != null && _designerWidnow.DiagramDrawer != null)
                {
                    var items = _designerWidnow.DiagramDrawer.DiagramViewModel.GraphItems;

                    var objectValue = property.Property.ObjectValue as State;
                    if (objectValue != null)
                    {
                        var stateMachine = objectValue.StateMachine;

                        foreach (var item in items)
                        {
                            var stateNode = item as StateNodeViewModel;
                            if (stateNode != null)
                            {
                                stateNode.IsCurrentState = item.Name == objectValue.Name;
                            }
                            else if (stateMachine.LastTransition != null)
                            {
                                var connectionNode = item as ConnectionViewModel;
                                if (connectionNode != null)
                                {

                                    connectionNode.IsActive = stateMachine.LastTransition.Name ==
                                                              connectionNode.ConnectorA.ConnectorFor.Name;
                                }
                            }


                        }
                    }

                    //EditorWindow.GetWindow<ElementsDesigner>().Repaint();
                }

                return;
            }
            EditorGUI.BeginChangeCheck();
            object newValue = null;
            object currentValue = property.Property.ObjectValue;
            var propertyName = property.Property.PropertyName;
            var isEnum = property.IsEnum;
            newValue = DoTypeInspector(type, propertyName, currentValue, isEnum);

            if (EditorGUI.EndChangeCheck())
            {

                property.Property.ObjectValue = newValue;
            }
        }

        private static object DoTypeInspector(Type type, string propertyName, object currentValue, bool isEnum)
        {
            if (currentValue == null)
            {
                return null;
            }
            object newValue = null;
            if (type == typeof(int))
            {
                newValue = EditorGUILayout.IntField(propertyName,
                    (int)currentValue);
            }
            else if (type == typeof(bool))
            {
                newValue = EditorGUILayout.Toggle(propertyName,
                    (bool)currentValue);
            }
            else if (type == typeof(string))
            {
                newValue = EditorGUILayout.TextField(propertyName,
                    (string)currentValue);
            }
            else if (type == typeof(decimal))
            {
                newValue = Convert.ToDecimal(EditorGUILayout.FloatField(propertyName,
                    Convert.ToSingle(currentValue)));
            }
            else if (type == typeof(float))
            {
                newValue = EditorGUILayout.FloatField(propertyName,
                    (float)currentValue);
            }
            else if (type == typeof(Vector2))
            {
                newValue = EditorGUILayout.Vector2Field(propertyName,
                    (Vector2)currentValue);
            }
            else if (type == typeof(Vector3))
            {
                newValue = EditorGUILayout.Vector3Field(propertyName,
                    (Vector3)currentValue);
            }
            else if (type == typeof(Rect))
            {
                newValue = EditorGUILayout.RectField(propertyName,
                    (Rect)currentValue);
            }
            else if (type == typeof(Color))
            {
                newValue = EditorGUILayout.ColorField(propertyName,
                    (Color)currentValue);
            }
            else if (isEnum)
            {
                newValue = EditorGUILayout.EnumPopup(propertyName,
                    (Enum)currentValue);
            }
            return newValue;
        }

        object GetDefaultValue(Type t)
        {
            if (t == typeof(string)) return String.Empty;
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }

    }
}