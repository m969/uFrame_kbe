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
    [CustomEditor(typeof(UIManager), true)]
    [DisallowMultipleComponent]
    public class UIManagerEditor : QEditor
    {
        UIManager uiManager { get { return (UIManager)target; } }

        SerializedProperty
            debugGameEvents, debugUIButtons, debugUIElements, debugUINotifications, debugUICanvases,
            autoDisableButtonClicks;

        void SerializedObjectFindProperties()
        {
            debugGameEvents = serializedObject.FindProperty("debugGameEvents");
            debugUIButtons = serializedObject.FindProperty("debugUIButtons");
            debugUIElements = serializedObject.FindProperty("debugUIElements");
            debugUINotifications = serializedObject.FindProperty("debugUINotifications");
            autoDisableButtonClicks = serializedObject.FindProperty("autoDisableButtonClicks");
        }

        protected override void OnEnable()
        {
            requiresContantRepaint = true;
            SerializedObjectFindProperties();
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerUIManager.normal, WIDTH_420, HEIGHT_42);
            serializedObject.Update();
            DrawTopButtons();
            DrawOrientationManagerButton();
            DrawDebugOptions();
            DrawSettings();
            QUI.Space(SPACE_4);
            QUI.Label("General Info", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic));
            DrawBackButtonStatus();
            DrawButtonClicksStatus();
            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }

        void DrawTopButtons()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button("Control Panel"))
                {
                    ControlPanelWindow.Open(ControlPanelWindow.Section.ControlPanel);
                }
                if (QUI.Button("Editor Settings"))
                {
                    ControlPanelWindow.Open(ControlPanelWindow.Section.Settings);
                }
                if (QUI.Button("Help"))
                {
                    ControlPanelWindow.Open(ControlPanelWindow.Section.Help);
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_2);
        }

        void DrawOrientationManagerButton()
        {
            if(!UIManager.useOrientationManager) { return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button("Add Orientation Manager to Scene"))
                {
                    OrientationManager.AddOrientationManagerToScene();
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_2);
        }

        void DrawDebugOptions()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Debug", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 40, 18);
                QUI.Space(SPACE_8);
                QUI.Toggle(debugGameEvents);
                QUI.Label("GameEvents", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 64, 18);
                QUI.Space(SPACE_8);
                QUI.Toggle(debugUIButtons);
                QUI.Label("UIButtons", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 50, 18);
                QUI.Space(SPACE_8);
                QUI.Toggle(debugUIElements);
                QUI.Label("UIElements", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 58, 18);
                QUI.Space(SPACE_8);
                QUI.Toggle(debugUINotifications);
                QUI.Label("UINotifications", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 74, 18);
            }
            QUI.EndHorizontal();
        }
        void DrawSettings()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Toggle(autoDisableButtonClicks);
                QUI.Label("Auto disable Button Clicks when an UIElement is in trasition", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 408, 18);
            }
            QUI.EndHorizontal();
        }
        void DrawBackButtonStatus()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("The 'Back' button is " + (UIManager.BackButtonDisabled ? "DISABLED" : "ENABLED"), DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmallItalic));
            }
            QUI.EndHorizontal();
        }
        void DrawButtonClicksStatus()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Button clicks are " + (UIManager.ButtonClicksDisabled ? "DISABLED" : "ENABLED"), DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmallItalic));
            }
            QUI.EndHorizontal();
        }
    }
}
