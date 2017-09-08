// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;

namespace DoozyUI
{
    [CustomEditor(typeof(UINotification))]
    [CanEditMultipleObjects]
    public class UINotificationEditor : QEditor
    {
        UINotification uiNotification { get { return (UINotification)target; } }

        SerializedProperty
            listenForBackButton,
            targetCanvasName, customTargetCanvasName,
            notificationContainer,
            overlay,
            title,
            message,
            icon,
            buttons,
            closeButton,
            specialElements,
            effects;

        int canvasNameIndex;

        bool ControlPanelSelected = false;
        bool refreshData = true;

        void SerializedObjectFindProperties()
        {
            listenForBackButton = serializedObject.FindProperty("listenForBackButton");
            targetCanvasName = serializedObject.FindProperty("targetCanvasName");
            customTargetCanvasName = serializedObject.FindProperty("customTargetCanvasName");
            notificationContainer = serializedObject.FindProperty("notificationContainer");
            overlay = serializedObject.FindProperty("overlay");
            title = serializedObject.FindProperty("title");
            message = serializedObject.FindProperty("message");
            icon = serializedObject.FindProperty("icon");
            buttons = serializedObject.FindProperty("buttons");
            closeButton = serializedObject.FindProperty("closeButton");
            specialElements = serializedObject.FindProperty("specialElements");
            effects = serializedObject.FindProperty("effects");
        }

        void GenerateInfoMessages()
        {
            infoMessage = new Dictionary<string, InfoMessage>();
            infoMessage.Add("EmptyTargetCanvasName", new InfoMessage() { title = "Empty Target Canvas Name", message = "You need to set a target canvas name so that they system will know the canvas you want this notification shown on. If left empty, the system will look for the '" + UICanvas.DEFAULT_CANVAS_NAME + "' automatically.", type = InfoMessageType.Info, show = new AnimBool(false, Repaint) });
            infoMessage.Add("MissingNotificationContainer", new InfoMessage() { title = "Missing Notification Container", message = "A notification needs an UIElement to act as a container. Fix this issue by adding a child UIElement to this gameObject and then reference it as the Notification Container.", type = InfoMessageType.Error, show = new AnimBool(false, Repaint) });
            infoMessage.Add("BadNotificationContainer", new InfoMessage() { title = "Bad Notification Container", message = "The UIElement referenced as the Notification Container is not a child of this gameObject. The UINotification may not behave as expected or it may not work at all.", type = InfoMessageType.Info, show = new AnimBool(false, Repaint) });
        }

        void InitAnimBools()
        {
        }

        protected override void OnEnable()
        {
            requiresContantRepaint = true;
            SerializedObjectFindProperties();
            GenerateInfoMessages();
            InitAnimBools();
            SetupAllChildUIElements();
        }

        void RefreshData(bool forcedRefresh = false)
        {
            serializedObject.Update();
            RefreshCanvasNames(forcedRefresh);
            serializedObject.ApplyModifiedProperties();
            EditorUtility.ClearProgressBar();
        }
        void RefreshCanvasNames(bool forcedRefresh)
        {
            RefreshCanvasNamesDatabase(forcedRefresh);
            ValidateUICanvasCanvasName();
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerUINotification.texture, WIDTH_420, HEIGHT_42);
            if (refreshData) //refresh needs to be executed this way because OnEnable is called 3 times when entering PlayMode, thus adding a lot of wait time for the developer (that is unacceptable); until we figure out why that happends, this solution will have to do.
            {
                RefreshData();
                refreshData = false;
            }
            if (!ControlPanelWindow.Selected && ControlPanelSelected)
            {
                RefreshData();
                ControlPanelSelected = false;
            }
            else if (ControlPanelWindow.Selected && !ControlPanelSelected)
            {
                ControlPanelSelected = true;
            }
            serializedObject.Update();
            SaveColors();
            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
            DrawListenForBackButton();
            DrawTargetCanvas();
            RestoreColors();
            infoMessage["EmptyTargetCanvasName"].show.target = string.IsNullOrEmpty(targetCanvasName.stringValue);
            DrawInfoMessage("EmptyTargetCanvasName", WIDTH_420);
            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
            DrawNotificationContainer();
            RestoreColors();
            infoMessage["MissingNotificationContainer"].show.target = notificationContainer.objectReferenceValue == null;
            DrawInfoMessage("MissingNotificationContainer", WIDTH_420);
            if (!infoMessage["MissingNotificationContainer"].show.value)
            {
                if (uiNotification.notificationContainer != null)
                {
                    infoMessage["BadNotificationContainer"].show.target = uiNotification.notificationContainer.transform.parent != uiNotification.transform;
                    DrawInfoMessage("BadNotificationContainer", WIDTH_420);
                }
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                DrawOverlay();
                QUI.Space(SPACE_4);
                DrawTitle();
                DrawMessage();
                DrawIcon();
                QUI.Space(SPACE_4);
                DrawButtons();
                DrawSpecialElements();
                DrawEffects();
                QUI.Space(SPACE_4);
                DrawCloseButton();
                RestoreColors();
            }
            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }

        void DrawListenForBackButton()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Toggle(listenForBackButton);
                QUI.Label("Listen for the 'Back' button (close when pressing 'Back' or ESC)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal));
            }
            QUI.EndHorizontal();
        }
        void DrawTargetCanvas()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {

                QUI.Label("Target Canvas", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 130);
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    QUI.Label(targetCanvasName.stringValue, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic));
                }
                else
                {
                    if (customTargetCanvasName.boolValue)
                    {
                        QUI.PropertyField(targetCanvasName, 215);
                    }
                    else
                    {
                        QUI.BeginChangeCheck();
                        {
                            if (DUI.CanvasNamesDatabase == null || !DUI.CanvasNamesDatabase.Contains(targetCanvasName.stringValue)) { RefreshCanvasNames(true); }
                            canvasNameIndex = EditorGUILayout.Popup(canvasNameIndex, DUI.CanvasNamesDatabase.ToArray(), GUILayout.Width(215));
                        }
                        if (QUI.EndChangeCheck())
                        {
                            targetCanvasName.stringValue = DUI.CanvasNamesDatabase.data[canvasNameIndex];
                        }
                    }
                    QUI.Space(SPACE_4);
                    QUI.BeginChangeCheck();
                    {
                        QUI.PropertyField(customTargetCanvasName, 12);
                    }
                    if (QUI.EndChangeCheck())
                    {
                        if (!customTargetCanvasName.boolValue)
                        {
                            ValidateUICanvasCanvasName();
                        }
                    }
                    QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 50);

                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
        }
        void DrawNotificationContainer()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Notification Container", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 130);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(notificationContainer, 281);
                }
                if (QUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    if (uiNotification.notificationContainer != null)
                    {
                        uiNotification.notificationContainer.transform.SetParent(uiNotification.transform);
                        uiNotification.notificationContainer.linkedToNotification = true;
                        uiNotification.notificationContainer.autoRegister = false;
                        uiNotification.notificationContainer.name = DUI.DUISettings.UIElement_Inspector_RenameGameObjectPrefix + "Notification Container" + DUI.DUISettings.UIElement_Inspector_RenameGameObjectSuffix;
                    }
                }
            }
            QUI.EndHorizontal();
        }
        void DrawOverlay()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Background Overlay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 130);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(overlay, 281);
                }
                if (QUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    if (uiNotification.overlay != null)
                    {
                        uiNotification.overlay.transform.SetParent(uiNotification.transform);
                        uiNotification.overlay.linkedToNotification = true;
                        uiNotification.overlay.autoRegister = false;
                        uiNotification.overlay.name = DUI.DUISettings.UIElement_Inspector_RenameGameObjectPrefix + "Background Overlay" + DUI.DUISettings.UIElement_Inspector_RenameGameObjectSuffix;
                    }
                }
            }
            QUI.EndHorizontal();
        }
        void DrawTitle()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Notification Title", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 130);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(title, 281);
                }
                if (QUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    if (uiNotification.title != null)
                    {
                        uiNotification.title.name = "Notification Title";
                    }
                }
            }
            QUI.EndHorizontal();
        }
        void DrawMessage()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Notification Message", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 130);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(message, 281);
                }
                if (QUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    if (uiNotification.message != null)
                    {
                        uiNotification.message.name = "Notification Message";
                    }
                }
            }
            QUI.EndHorizontal();
        }
        void DrawIcon()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Notification Icon", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 130);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(icon, 281);
                }
                if (QUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    if (uiNotification.icon != null)
                    {
                        uiNotification.icon.name = "Notification Icon";
                    }
                }
            }
            QUI.EndHorizontal();
        }
        void DrawButtons()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Notification Buttons", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 130);
                QUI.BeginChangeCheck();
                {
                    QUI.DrawList(buttons, 288, "No UIButton referenced...");
                }
                if (QUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    if (uiNotification.buttons != null && uiNotification.buttons.Length > 0)
                    {

                        for (int i = 0; i < uiNotification.buttons.Length; i++)
                        {
                            if (uiNotification.buttons[i] != null)
                            {
                                uiNotification.buttons[i].name = "Notification Button " + i;
                            }
                        }
                    }
                }
            }
            QUI.EndHorizontal();
            if (buttons.arraySize > 0) { QUI.Space(SPACE_8); }
        }
        void DrawSpecialElements()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Special Elements", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 130);
                QUI.BeginChangeCheck();
                {
                    QUI.DrawList(specialElements, 288, "No UIElement referenced...");
                }
                if (QUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    if (uiNotification.specialElements != null && uiNotification.specialElements.Length > 0)
                    {

                        for (int i = 0; i < uiNotification.specialElements.Length; i++)
                        {
                            if (uiNotification.specialElements[i] != null)
                            {
                                uiNotification.specialElements[i].transform.SetParent(uiNotification.transform);
                                uiNotification.specialElements[i].name = DUI.DUISettings.UIElement_Inspector_RenameGameObjectPrefix + "Special Element " + i + DUI.DUISettings.UIElement_Inspector_RenameGameObjectSuffix;
                            }
                        }
                    }
                }
            }
            QUI.EndHorizontal();
            if (specialElements.arraySize > 0) { QUI.Space(SPACE_8); }
        }
        void DrawEffects()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Notification Effects", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 130);
                QUI.BeginChangeCheck();
                {
                    QUI.DrawList(effects, 288, "No UIEffect referenced...");
                }
                if (QUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    if (uiNotification.effects != null && uiNotification.effects.Length > 0)
                    {

                        for (int i = 0; i < uiNotification.effects.Length; i++)
                        {
                            if (uiNotification.effects[i] != null)
                            {
                                uiNotification.effects[i].targetUIElement = uiNotification.notificationContainer;
                                uiNotification.effects[i].name = "Notification Effect " + i;
                            }
                        }
                    }
                }
            }
            QUI.EndHorizontal();
            if (effects.arraySize > 0) { QUI.Space(SPACE_8); }
        }
        void DrawCloseButton()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Close Button", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 130);
                QUI.PropertyField(closeButton, 281);
            }
            QUI.EndHorizontal();
        }

        void SetupAllChildUIElements()
        {
            uiNotification.Initialize();
        }

        void RefreshCanvasNamesDatabase(bool forcedRefresh)
        {
            if (DUI.CanvasNamesDatabase == null || forcedRefresh)
            {
                DUI.RefreshCanvasNamesDatabase();
            }
        }

        void ValidateUICanvasCanvasName()
        {
            if (!DUI.CanvasNamesDatabase.Contains(targetCanvasName.stringValue)) //canvas name does not exist in canvas datatabase -> ask it it should be added
            {
                if (!string.IsNullOrEmpty(targetCanvasName.stringValue.Trim()) && EditorUtility.DisplayDialog("Action Required", "The '" + targetCanvasName.stringValue + "' target canvas name does not exist in the canvas names database.\nDo you want to add it now?", "Yes", "No"))
                {
                    DUI.AddCanvasName(targetCanvasName.stringValue);
                }
                else
                {
                    EditorUtility.DisplayDialog("Information", "The canvas name was reset to the default value. (" + UICanvas.DEFAULT_CANVAS_NAME + ")", "Ok");
                    targetCanvasName.stringValue = UICanvas.DEFAULT_CANVAS_NAME;
                    if (!DUI.CanvasNamesDatabase.Contains(targetCanvasName.stringValue))
                    {
                        DUI.AddCanvasName(targetCanvasName.stringValue);
                    }
                }
            }
            canvasNameIndex = DUI.CanvasNamesDatabase.IndexOf(targetCanvasName.stringValue);
        }
    }
}
