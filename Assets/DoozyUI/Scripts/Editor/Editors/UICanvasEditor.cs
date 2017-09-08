// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using UnityEngine;
using System.Collections;
using QuickEditor;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;

namespace DoozyUI
{
    [CustomEditor(typeof(UICanvas))]
    [CanEditMultipleObjects]
    public class UICanvasEditor : QEditor
    {
        UICanvas uiCanvas { get { return (UICanvas)target; } }

        SerializedProperty
            canvasName, customCanvasName;

        int canvasNameIndex;

        bool ControlPanelSelected = false;
        bool refreshData = true;

        void SerializedObjectFindProperties()
        {
            canvasName = serializedObject.FindProperty("canvasName");
            customCanvasName = serializedObject.FindProperty("customCanvasName");
        }

        void GenerateInfoMessages()
        {
            infoMessage = new Dictionary<string, InfoMessage>
            {
                { "NotRootCanvas", new InfoMessage() { title = "Disabled", message = "The UICanvas is not attached to a RootCanvas. This component should be attached to a top canvas in the Hierarchy.", type = InfoMessageType.Error, show = new AnimBool(false, Repaint) } },
                { "MasterCanvasInfo", new InfoMessage() { title = UICanvas.DEFAULT_CANVAS_NAME, message = "This UICanvas is your main/default canvas.\n\nYou should NOT have, in a scene, more than one '"+UICanvas.DEFAULT_CANVAS_NAME+"' at any given time.", type = InfoMessageType.Info, show = new AnimBool(false, Repaint) } }
            };
        }

        protected override void OnEnable()
        {
            requiresContantRepaint = true;
            SerializedObjectFindProperties();
            GenerateInfoMessages();
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
            DrawHeader(DUIResources.headerUICanvas.texture, WIDTH_420, HEIGHT_42);
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
            if (!infoMessage["NotRootCanvas"].show.value)
            {
                QUI.Space(-SPACE_4);
                infoMessage["MasterCanvasInfo"].show.target = canvasName.stringValue.Equals(UICanvas.DEFAULT_CANVAS_NAME);
                DrawInfoMessage("MasterCanvasInfo", WIDTH_420);

                DrawTopButtons();
                DrawCanvasName();
                DrawUpdateSortingLayerButton();
            }
            else
            {
                DrawRemoveComponentButton();
            }

            infoMessage["NotRootCanvas"].show.target = !uiCanvas.Canvas.isRootCanvas;
            DrawInfoMessage("NotRootCanvas", WIDTH_420);

            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }

        void DrawTopButtons()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button("UICanvases Database"))
                {
                    ControlPanelWindow.Open(ControlPanelWindow.Section.CanvasNames);
                }
                if (QUI.Button("Refresh Data"))
                {
                    RefreshData(true);
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_2);
        }
        void DrawCanvasName()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Canvas Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    QUI.Label(canvasName.stringValue, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic));
                }
                else
                {
                    if (customCanvasName.boolValue)
                    {
                        QUI.PropertyField(canvasName, 240);
                    }
                    else
                    {
                        QUI.BeginChangeCheck();
                        {
                            if (DUI.CanvasNamesDatabase == null || !DUI.CanvasNamesDatabase.Contains(canvasName.stringValue)) { RefreshCanvasNames(true); }
                            canvasNameIndex = EditorGUILayout.Popup(canvasNameIndex, DUI.CanvasNamesDatabase.ToArray(), GUILayout.Width(240));
                        }
                        if (QUI.EndChangeCheck())
                        {
                            canvasName.stringValue = DUI.CanvasNamesDatabase.data[canvasNameIndex];
                        }
                    }
                    QUI.Space(SPACE_4);
                    QUI.BeginChangeCheck();
                    {
                        QUI.PropertyField(customCanvasName, 12);
                    }
                    if (QUI.EndChangeCheck())
                    {
                        if (!customCanvasName.boolValue)
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
        void DrawUpdateSortingLayerButton()
        {
            if (QUI.Button("Update Sorting", WIDTH_420, HEIGHT_24))
            {
                if (!uiCanvas.Canvas.isRootCanvas)
                {
                    EditorUtility.DisplayDialog("UICanvas Issue",
                                                "The UICanvas, on the " + uiCanvas.name + " gameObject, is not attached to a root canvas. This component should be attached to a top canvas in the Hierarchy.",
                                                "Ok");
                    return;
                }
                if (EditorUtility.DisplayDialog("Update Sorting",
                                                "You are about to change the Sorting Layer Name of all the Canvases and Renderers, under this gameObject, to '" + uiCanvas.Canvas.sortingLayerName + "'." +
                                                "\n" + "\n" +
                                                "'" + uiCanvas.Canvas.sortingLayerName + "' is the Sorting Layer set to the Canvas component attached to this gameObject. (root canvas)" +
                                                "\n" + "\n" +
                                                "Are you sure you want to do that?" +
                                                "\n" +
                                                "(operation cannot be undone)",
                                                "Ok",
                                                "Cancel"))
                {
                    UIManager.UpdateCanvasSortingLayerName(uiCanvas.gameObject, uiCanvas.Canvas.sortingLayerName);
                    UIManager.UpdateRendererSortingLayerName(uiCanvas.gameObject, uiCanvas.Canvas.sortingLayerName);
                }
            }
            QUI.Space(SPACE_4);
        }
        void DrawRemoveComponentButton()
        {
            if (QUI.Button("Remove Component", WIDTH_420, HEIGHT_24))
            {
                DestroyImmediate(uiCanvas);
                QUI.ExitGUI();
            }
            QUI.Space(SPACE_4);
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
            if (canvasName.stringValue.Equals(UICanvas.DEFAULT_CANVAS_NAME)) { customCanvasName.boolValue = false; }
            if (customCanvasName.boolValue) { return; }
            if (!DUI.CanvasNamesDatabase.Contains(canvasName.stringValue)) //canvas name does not exist in canvas datatabase -> ask it it should be added
            {
                if (!string.IsNullOrEmpty(canvasName.stringValue.Trim()) && EditorUtility.DisplayDialog("Action Required", "The '" + canvasName.stringValue + "' canvas name does not exist in the canvas names database.\nDo you want to add it now?", "Yes", "No"))
                {
                    DUI.AddCanvasName(canvasName.stringValue);
                    customCanvasName.boolValue = false;
                }
                else
                {
                    EditorUtility.DisplayDialog("Information", "The canvas name was left unchanged and this UICanvas was set to use a custom canvas name." +
                                                               "\n\n" +
                                                               "Having a custom canvas name means that the name is not in the Canvas Database.", "Ok");
                    customCanvasName.boolValue = true;
                    return;
                }
            }
            canvasNameIndex = DUI.CanvasNamesDatabase.IndexOf(canvasName.stringValue);
        }
    }
}
