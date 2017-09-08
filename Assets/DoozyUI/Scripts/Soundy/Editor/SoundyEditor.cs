// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DoozyUI
{
    [CustomEditor(typeof(Soundy), true)]
    [DisallowMultipleComponent]
    public class SoundyEditor : QEditor
    {
        Soundy soundy { get { return (Soundy)target; } }

        SerializedProperty
            masterVolume, numberOfChannels;

        void SerializedObjectFindProperties()
        {
            masterVolume = serializedObject.FindProperty("masterVolume");
            numberOfChannels = serializedObject.FindProperty("numberOfChannels");
        }

        void GenerateInfoMessages()
        {
            infoMessage = new Dictionary<string, InfoMessage>();
        }

        protected override void OnEnable()
        {
            requiresContantRepaint = true;
            SerializedObjectFindProperties();
            GenerateInfoMessages();
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerSoundy.normal, WIDTH_420, HEIGHT_42);
            serializedObject.Update();
            DrawSettings();
            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }

        private void DrawSettings()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Master Volume", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 90);
                EditorGUILayout.Slider(masterVolume, 0, 1, GUIContent.none, GUILayout.Width(160));
                QUI.Space(SPACE_4);
                QUI.Label("Sound Channels", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 100);
                QUI.PropertyField(numberOfChannels, 52);
            }
            QUI.EndHorizontal();

            if(numberOfChannels.intValue < 1) { numberOfChannels.intValue = 1; }
        }
    }
}
