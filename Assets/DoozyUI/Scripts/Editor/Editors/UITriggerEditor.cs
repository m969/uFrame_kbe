// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace DoozyUI
{
    [CustomEditor(typeof(UITrigger), true)]
    [CanEditMultipleObjects]
    public class UITriggerEditor : QEditor
    {
        UITrigger uiTrigger { get { return (UITrigger)target; } }

        SerializedProperty
            triggerOnGameEvent, triggerOnButtonClick, triggerOnButtonDoubleClick, triggerOnButtonLongClick,
            dispatchAll,
            gameEvent, buttonCategory, buttonName,
            onTriggerEvent,
            gameEvents;

        AnimBool
            showGameEvents;

        int buttonNameIndex = 0;
        int buttonCategoryIndex = 0;

        bool ControlPanelSelected = false;
        bool refreshData = true;

        void SerializedObjectFindProperties()
        {
            triggerOnGameEvent = serializedObject.FindProperty("triggerOnGameEvent");
            triggerOnButtonClick = serializedObject.FindProperty("triggerOnButtonClick");
            triggerOnButtonDoubleClick = serializedObject.FindProperty("triggerOnButtonDoubleClick");
            triggerOnButtonLongClick = serializedObject.FindProperty("triggerOnButtonLongClick");
            dispatchAll = serializedObject.FindProperty("dispatchAll");
            gameEvent = serializedObject.FindProperty("gameEvent");
            buttonCategory = serializedObject.FindProperty("buttonCategory");
            buttonName = serializedObject.FindProperty("buttonName");
            onTriggerEvent = serializedObject.FindProperty("onTriggerEvent");
            gameEvents = serializedObject.FindProperty("gameEvents");
        }

        void GenerateInfoMessages()
        {
            infoMessage = new Dictionary<string, InfoMessage>();
            infoMessage.Add("Disabled", new InfoMessage() { title = "Disabled", message = "Select a trigger", type = InfoMessageType.Error, show = new AnimBool(false, Repaint) });
            infoMessage.Add("SetGameEvent", new InfoMessage() { title = "Disabled", message = "Set a game event to listen for or enable 'dispatch all' game events.", type = InfoMessageType.Error, show = new AnimBool(false, Repaint) });
            infoMessage.Add("SetOnClickButtonName", new InfoMessage() { title = "Disabled", message = "Set a button name to listen for, on click, or enable 'dispatch all' button clicks.", type = InfoMessageType.Error, show = new AnimBool(false, Repaint) });
            infoMessage.Add("SetOnDoubleClickButtonName", new InfoMessage() { title = "Disabled", message = "Set a button name to listen for, on double click, or enable 'dispatch all' button double clicks.", type = InfoMessageType.Error, show = new AnimBool(false, Repaint) });
            infoMessage.Add("SetOnLongClickButtonName", new InfoMessage() { title = "Disabled", message = "Set a button name to listen for, on long click, or enable 'dispatch all' button long clicks.", type = InfoMessageType.Error, show = new AnimBool(false, Repaint) });
        }

        void InitAnimBools()
        {
            showGameEvents = new AnimBool(gameEvents.arraySize > 0, Repaint);
        }

        protected override void OnEnable()
        {
            requiresContantRepaint = true;
            SerializedObjectFindProperties();
            GenerateInfoMessages();
            InitAnimBools();
        }

        void RefreshData(bool forcedRefresh = false)
        {
            serializedObject.Update();
            RefreshButtonNameAndCategory(forcedRefresh);
            serializedObject.ApplyModifiedProperties();
        }
        void RefreshButtonNameAndCategory(bool forcedRefresh)
        {
            RefreshUIButtonsDatabase(forcedRefresh);
            ValiateUIButtonNameAndCategory();
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerUITrigger.texture, WIDTH_420, HEIGHT_42);
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
            QUI.Space(-SPACE_4);

            infoMessage["Disabled"].show.target = !triggerOnGameEvent.boolValue && !triggerOnButtonClick.boolValue && !triggerOnButtonDoubleClick.boolValue && !triggerOnButtonLongClick.boolValue;
            DrawInfoMessage("Disabled", WIDTH_420);

            infoMessage["SetGameEvent"].show.target = triggerOnGameEvent.boolValue && string.IsNullOrEmpty(gameEvent.stringValue) && !dispatchAll.boolValue;
            DrawInfoMessage("SetGameEvent", WIDTH_420);

            infoMessage["SetOnClickButtonName"].show.target = triggerOnButtonClick.boolValue && (string.IsNullOrEmpty(buttonName.stringValue) || buttonName.stringValue.Equals(DUI.DEFAULT_BUTTON_NAME)) && !dispatchAll.boolValue;
            DrawInfoMessage("SetOnClickButtonName", WIDTH_420);

            infoMessage["SetOnDoubleClickButtonName"].show.target = triggerOnButtonDoubleClick.boolValue && (string.IsNullOrEmpty(buttonName.stringValue) || buttonName.stringValue.Equals(DUI.DEFAULT_BUTTON_NAME)) && !dispatchAll.boolValue;
            DrawInfoMessage("SetOnDoubleClickButtonName", WIDTH_420);

            infoMessage["SetOnLongClickButtonName"].show.target = triggerOnButtonLongClick.boolValue && (string.IsNullOrEmpty(buttonName.stringValue) || buttonName.stringValue.Equals(DUI.DEFAULT_BUTTON_NAME)) && !dispatchAll.boolValue;
            DrawInfoMessage("SetOnLongClickButtonName", WIDTH_420);

            DrawListenSelector();
            DrawGameEventOptions();
            DrawButtonNameOptions();
            DrawEvents();
            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }

        void DrawTopButtons()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button("UIButtons Database"))
                {
                    ControlPanelWindow.Open(ControlPanelWindow.Section.UIButtons);
                }
                if (QUI.Button("Refresh Data"))
                {
                    RefreshData(true);
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_2);
        }

        void DrawListenSelector()
        {
            if (triggerOnGameEvent.boolValue || triggerOnButtonClick.boolValue || triggerOnButtonDoubleClick.boolValue || triggerOnButtonLongClick.boolValue) { return; }
            SaveColors();
            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Listen for", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 58);
                QUI.Space(SPACE_4);
                QUI.PropertyField(triggerOnGameEvent, 12);
                QUI.Label("game event", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 72);
                QUI.Space(SPACE_4);
                QUI.PropertyField(triggerOnButtonClick, 12);
                QUI.Label("button click", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 68);
                QUI.Space(SPACE_4);
                QUI.PropertyField(triggerOnButtonDoubleClick, 12);
                QUI.Label("double click", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 70);
                QUI.Space(SPACE_4);
                QUI.PropertyField(triggerOnButtonLongClick, 12);
                QUI.Label("long click", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
            }
            QUI.EndHorizontal();
            RestoreColors();

        }

        void DrawGameEventOptions()
        {
            if (!triggerOnGameEvent.boolValue) { return; }
            buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
            buttonName.stringValue = DUI.DEFAULT_BUTTON_NAME;
            ValiateUIButtonNameAndCategory();
            SaveColors();
            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Listen for", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 58);
                QUI.PropertyField(triggerOnGameEvent, 12);
                QUI.Label("game event", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 70);
                QUI.Space(SPACE_8);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(dispatchAll, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (triggerOnGameEvent.boolValue)
                    {
                        gameEvent.stringValue = dispatchAll.boolValue ? DUI.DISPATCH_ALL : "";
                    }
                    else if (triggerOnButtonClick.boolValue || triggerOnButtonDoubleClick.boolValue || triggerOnButtonLongClick.boolValue)
                    {
                        buttonName.stringValue = dispatchAll.boolValue ? DUI.DISPATCH_ALL : DUI.DEFAULT_BUTTON_NAME;
                        if (dispatchAll.boolValue)
                        {
                            buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                        }
                        else
                        {
                            buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                            buttonName.stringValue = DUI.DEFAULT_BUTTON_NAME;
                            ValiateUIButtonNameAndCategory();
                        }
                    }
                }
                QUI.Label("dispatch all", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 142);
            }
            QUI.EndHorizontal();
            if (dispatchAll.boolValue) { gameEvent.stringValue = ""; return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Game Event", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 72);
                EditorGUILayout.DelayedTextField(gameEvent, GUIContent.none, GUILayout.Width(344));

            }
            QUI.EndHorizontal();
            RestoreColors();
        }

        void DrawButtonNameOptions()
        {
            if (!triggerOnButtonClick.boolValue && !triggerOnButtonDoubleClick.boolValue && !triggerOnButtonLongClick.boolValue) { return; }
            gameEvent.stringValue = "";

            DrawTopButtons();

            SaveColors();
            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Listen for", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 58);
                if (triggerOnButtonClick.boolValue)
                {
                    QUI.PropertyField(triggerOnButtonClick, 12);
                    QUI.Label("button click", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 68);
                }
                else if (triggerOnButtonDoubleClick.boolValue)
                {
                    QUI.PropertyField(triggerOnButtonDoubleClick, 12);
                    QUI.Label("double click", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 70);
                }
                else if (triggerOnButtonLongClick.boolValue)
                {
                    QUI.PropertyField(triggerOnButtonLongClick, 12);
                    QUI.Label("long click", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
                }
                QUI.Space(SPACE_8);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(dispatchAll, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (triggerOnGameEvent.boolValue)
                    {
                        gameEvent.stringValue = dispatchAll.boolValue ? DUI.DISPATCH_ALL : "";
                    }
                    else if (triggerOnButtonClick.boolValue || triggerOnButtonDoubleClick.boolValue || triggerOnButtonLongClick.boolValue)
                    {
                        buttonName.stringValue = dispatchAll.boolValue ? DUI.DISPATCH_ALL : DUI.DEFAULT_BUTTON_NAME;
                        if (dispatchAll.boolValue)
                        {
                            buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                        }
                        else
                        {
                            buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                            buttonName.stringValue = DUI.DEFAULT_BUTTON_NAME;
                            ValiateUIButtonNameAndCategory();
                        }
                    }
                }
                QUI.Label("dispatch all", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 142);
            }
            QUI.EndHorizontal();

            if (dispatchAll.boolValue) { gameEvent.stringValue = ""; return; }
            DrawButtonCategory();
            DrawButtonName();
            RestoreColors();
        }
        void DrawButtonCategory()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Button Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    QUI.Label(buttonCategory.stringValue, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic));
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        buttonCategoryIndex = EditorGUILayout.Popup(buttonCategoryIndex, DUI.UIButtonCategories.ToArray());
                    }
                    if (QUI.EndChangeCheck())
                    {
                        if (!DUI.UIButtonCategories[buttonCategoryIndex].Equals(DUI.CUSTOM_NAME)) //not custom name category?
                        {
                            if (DUI.UIButtonNameExists(DUI.UIButtonCategories[buttonCategoryIndex], buttonName.stringValue)) //does the new category have the button name?
                            {
                                buttonCategory.stringValue = DUI.UIButtonCategories[buttonCategoryIndex];
                                buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                            }
                            else if (buttonName.stringValue.Equals(DUI.DEFAULT_BUTTON_NAME) && DUI.GetUIButtonNames(DUI.UIButtonCategories[buttonCategoryIndex]).Count > 0) //is the button name the default value? && is the new category not empty? -> set button name as the first value
                            {
                                buttonCategory.stringValue = DUI.UIButtonCategories[buttonCategoryIndex];
                                buttonName.stringValue = DUI.GetUIButtonNames(buttonCategory.stringValue)[0];
                            }
                            else if (!buttonName.stringValue.Equals(DUI.DEFAULT_BUTTON_NAME) && !string.IsNullOrEmpty(buttonName.stringValue.Trim()) && EditorUtility.DisplayDialog("Action Required", "The '" + buttonName.stringValue + "' button name does not exist in the '" + DUI.UIButtonCategories[buttonCategoryIndex] + "' category database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.AddUIButtonName(DUI.UIButtonCategories[buttonCategoryIndex], buttonName.stringValue);
                                buttonCategory.stringValue = DUI.UIButtonCategories[buttonCategoryIndex];
                                buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                            }
                            else if (DUI.GetUIButtonNames(DUI.UIButtonCategories[buttonCategoryIndex]).Count == 0)
                            {
                                if (EditorUtility.DisplayDialog("Information", "The '" + DUI.UIButtonCategories[buttonCategoryIndex] + "' category is empty.\n\nOpen the UIButtons Database and add some button names to it or delete it.\n\nThe button name and category will now be reset to the default values.", "Ok"))
                                {
                                    buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                                    buttonCategoryIndex = DUI.UIButtonCategories.IndexOf(buttonCategory.stringValue);
                                    buttonName.stringValue = DUI.DEFAULT_BUTTON_NAME;
                                    buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                                }
                            }
                            else
                            {
                                buttonCategory.stringValue = DUI.UIButtonCategories[buttonCategoryIndex];
                                buttonName.stringValue = DUI.GetUIButtonNames(buttonCategory.stringValue)[0];
                                buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                            }
                        }
                        else
                        {
                            buttonCategory.stringValue = DUI.UIButtonCategories[buttonCategoryIndex];
                        }
                    }
                }
            }
            QUI.EndHorizontal();
        }
        void DrawButtonName()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Button Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    QUI.Label(buttonName.stringValue, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic));
                }
                else
                {
                    if (buttonCategory.stringValue.Equals(DUI.CUSTOM_NAME))
                    {
                        EditorGUILayout.DelayedTextField(buttonName, GUIContent.none);
                    }
                    else
                    {
                        if (DUI.GetUIButtonNames(DUI.UIButtonCategories[buttonCategoryIndex]).Count == 0)
                        {
                            QUI.Label(buttonName.stringValue, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic), 90);
                            QUI.FlexibleSpace();
                            QUI.Label("(Empty Category)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmallItalic), 86);
                        }
                        else
                        {
                            QUI.BeginChangeCheck();
                            {
                                if (!DUI.UIButtonCategoryExists(buttonCategory.stringValue)) { RefreshButtonNameAndCategory(true); }
                                buttonNameIndex = EditorGUILayout.Popup(buttonNameIndex, DUI.GetUIButtonNames(buttonCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                buttonName.stringValue = DUI.GetUIButtonNames(buttonCategory.stringValue)[buttonNameIndex];
                            }
                        }
                    }
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
        }

        void DrawEvents()
        {
            if (!triggerOnGameEvent.boolValue && !triggerOnButtonClick.boolValue && !triggerOnButtonDoubleClick.boolValue && !triggerOnButtonLongClick.boolValue) { return; }
            if (triggerOnGameEvent.boolValue && (!dispatchAll.boolValue && string.IsNullOrEmpty(gameEvent.stringValue))) { return; }
            if ((triggerOnButtonClick.boolValue || triggerOnButtonDoubleClick.boolValue || triggerOnButtonLongClick.boolValue) && (!dispatchAll.boolValue && string.IsNullOrEmpty(buttonName.stringValue))) { return; }
            SaveColors();
            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
            QUI.PropertyField(onTriggerEvent, new GUIContent("On Trigger Event"), WIDTH_420);
            RestoreColors();
            QUI.ResetColors();
            if (QUI.Button(DUIStyles.GetStyle(showGameEvents.target ? DUIStyles.ButtonStyle.GameEvents : DUIStyles.ButtonStyle.GameEventsCollapsed), WIDTH_420, 18)) { showGameEvents.target = !showGameEvents.target; }
            if (QUI.BeginFadeGroup(showGameEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.DrawList(gameEvents, WIDTH_420, "Not sending any Game Event on trigger... Click [+] to start...");
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void RefreshUIButtonsDatabase(bool forcedRefresh = false)
        {
            if (DUI.UIButtonsDatabase == null || forcedRefresh)
            {
                DUI.RefreshUIButtonsDatabase();
            }
        }

        void ValiateUIButtonNameAndCategory()
        {
            if (dispatchAll.boolValue)
            {
                buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                buttonName.stringValue = DUI.DISPATCH_ALL;
                return;
            }
            if (string.IsNullOrEmpty(buttonName.stringValue)) //buttonName is empty? -> reset button name to default
            {
                buttonName.stringValue = DUI.DEFAULT_BUTTON_NAME;
            }
            if (buttonCategory.stringValue != DUI.CUSTOM_NAME)
            {
                if (!DUI.UIButtonCategoryExists(buttonCategory.stringValue)) //category does not exist -> reset button category to default
                {
                    EditorUtility.DisplayDialog("Information", "This button's category is set to '" + buttonCategory.stringValue + "', but this category was not found in the UIButtons Database.\nResetting this button's category to the default value (" + DUI.DEFAULT_CATEGORY_NAME + ").", "Ok");
                    buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                }
                if (!DUI.UIButtonNameExists(buttonCategory.stringValue, buttonName.stringValue)) //button name does not exist in the set category -> change category to default & add the button name to the database
                {
                    if (EditorUtility.DisplayDialog("Action Required", "This button's name is set to '" + buttonName.stringValue + "', but it was not found in the '" + buttonCategory.stringValue + "' category.\nDo you want to add the name to the set category?", "Yes", "No"))
                    {
                        DUI.AddUIButtonName(buttonCategory.stringValue, buttonName.stringValue);
                        buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Information", "This button's category was reset to the default value (" + DUI.DEFAULT_CATEGORY_NAME + ").", "Ok");
                        buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                        if (!DUI.UIButtonNameExists(buttonCategory.stringValue, buttonName.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "This button's name is set to '" + buttonName.stringValue + "', but it was not found in the '" + buttonCategory.stringValue + "' category.\nDo you want to add the name to the set category?", "Yes", "No"))
                            {
                                DUI.AddUIButtonName(buttonCategory.stringValue, buttonName.stringValue);
                                buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                            }
                            else
                            {
                                EditorUtility.DisplayDialog("Information", "This button's name was reset to the default value (" + DUI.DEFAULT_BUTTON_NAME + ").", "Ok");
                                buttonName.stringValue = DUI.DEFAULT_BUTTON_NAME;
                                buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                            }
                        }
                        else
                        {
                            buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                        }
                    }
                }
                else
                {
                    buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                }
            }
            else
            {
                buttonNameIndex = DUI.GetUIButtonNames(DUI.DEFAULT_CATEGORY_NAME).IndexOf(DUI.DEFAULT_BUTTON_NAME);
            }
            buttonCategoryIndex = DUI.UIButtonCategories.IndexOf(buttonCategory.stringValue);
        }
    }
}
