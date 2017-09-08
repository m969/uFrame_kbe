// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DoozyUI
{
    [CustomEditor(typeof(OrientationManager), true)]
    [DisallowMultipleComponent]
    public class OrientationManagerEditor : QEditor
    {
        OrientationManager orientationManager { get { return (OrientationManager)target; } }

        SerializedProperty
           onOrientationChange;

        void SerializedObjectFindProperties()
        {
            onOrientationChange = serializedObject.FindProperty("onOrientationChange");
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
            UpdateOrientationInEditMode();
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerOrientationManager.texture, WIDTH_420, HEIGHT_42);
            serializedObject.Update();
            DrawOrientation();
            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }

        void DrawOrientation()
        {
            QUI.Space(-SPACE_4);
            switch (orientationManager.CurrentOrientation)
            {
                case OrientationManager.Orientation.Landscape: if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.OrientationLandscape), 420, 28)) { UpdateOrientationInEditMode(); } break;
                case OrientationManager.Orientation.Portrait: if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.OrientationPortrait), 420, 28)) { UpdateOrientationInEditMode(); } break;
                case OrientationManager.Orientation.Unknown: if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.OrientationUnknown), 420, 28)) { UpdateOrientationInEditMode(); } break;
            }
            QUI.Space(SPACE_2);
            SaveColors();
            QUI.SetGUIBackgroundColor(DUIColors.PurpleLight.Color);
            QUI.PropertyField(onOrientationChange, true, new GUIContent("OnOrientationChange"), WIDTH_420);
            RestoreColors();
        }

        void UpdateOrientationInEditMode()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) //Update the orientation in EditMode only, not when in PlayMode
            {
                //Debug.Log("[OrientationManager] Cannot manually trigger an orientation update while in PlayMode. The system handles that for you automatically.");
                return;
            } 

            //PORTRAIT
            if (orientationManager.RectTransform.rect.width < orientationManager.RectTransform.rect.height)
            {
                if (orientationManager.CurrentOrientation != OrientationManager.Orientation.Portrait) //Orientation changed to PORTRAIT
                {
                    orientationManager.ChangeOrientation(OrientationManager.Orientation.Portrait);
                }
            }

            //LANDSCAPE
            else
            {
                if (orientationManager.CurrentOrientation != OrientationManager.Orientation.Landscape) //Orientation changed to LANDSCAPE
                {
                    orientationManager.ChangeOrientation(OrientationManager.Orientation.Landscape);
                }
            }
        }
    }
}
