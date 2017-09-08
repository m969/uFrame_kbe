// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System;
using QuickEditor;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace DoozyUI
{
    public partial class ControlPanelWindow : QWindow
    {
        AnimBool HierarchyManagerAnimBool = new AnimBool(false);

        AnimBool UIElementAnimBool = new AnimBool(false);

        AnimBool UIButtonAnimBool = new AnimBool(false);
        AnimBool UIButtonOnPointerEnterAnimBool = new AnimBool(false);
        AnimBool UIButtonOnPointerExitAnimBool = new AnimBool(false);
        AnimBool UIButtonOnPointerDownAnimBool = new AnimBool(false);
        AnimBool UIButtonOnPointerUpAnimBool = new AnimBool(false);
        AnimBool UIButtonOnClickAnimBool = new AnimBool(false);
        AnimBool UIButtonOnDoubleClickAnimBool = new AnimBool(false);
        AnimBool UIButtonOnLongClickAnimBool = new AnimBool(false);
        AnimBool UIButtonNormalLoop = new AnimBool(false);
        AnimBool UIButtonSelectedLoop = new AnimBool(false);

        AnimBool UIEffectAnimBool = new AnimBool(false);

        void DrawSettings()
        {
            QUI.DrawTexture(DUIResources.headerSettings.texture, 552, 64);
            float sectionWidth = PAGE_WIDTH - SIDE_BAR_SHADOW_WIDTH * 2;
            QUI.BeginChangeCheck();
            {
                if (DUI.DUISettings == null)
                {
                    QUI.Space(16);
                    QUI.BeginHorizontal(sectionWidth);
                    {
                        QUI.FlexibleSpace();
                        QUI.Label("DUISettings not found...", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormalItalic));
                        QUI.FlexibleSpace();
                    }
                    QUI.EndHorizontal();
                    return;
                }
                QUI.Space(8);
                DrawSettingsHierarchyManager(sectionWidth);
                QUI.Space(4);
                DrawSettingsUIElement(sectionWidth);
                QUI.Space(4);
                DrawSettingsUIButton(sectionWidth);
                QUI.Space(4);
                DrawSettingsUIEffect(sectionWidth);
            }
            if(QUI.EndChangeCheck())
            {
                Undo.RecordObject(DUI.DUISettings, "Updated DUISettings");
                EditorUtility.SetDirty(DUI.DUISettings);
                AssetDatabase.SaveAssets();
            }
        }

        void DrawSettingsHierarchyManager(float width)
        {
            if (ButtonBar("Hierarchy Manager", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelLarge), HierarchyManagerAnimBool, width)) { HierarchyManagerAnimBool.target = !HierarchyManagerAnimBool.target; }
            if (QUI.BeginFadeGroup(HierarchyManagerAnimBool.faded))
            {
                QUI.BeginChangeCheck();
                QUI.BeginHorizontal(width);
                {
                    QUI.Space(INDENT_24);
                    QUI.BeginVertical(width - INDENT_24);
                    {
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            DUI.DUISettings.HierarchyManager_Enabled = QUI.Toggle(DUI.DUISettings.HierarchyManager_Enabled);
                            QUI.Label("Enabled", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 42, 18);
                            QUI.Space(2);
                            QUI.Label("- shows icons and other relevant info in the Hierarchy", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 266, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconUICanvas128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_UICanvas_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_UICanvas_ShowIcon);
                            QUI.Label("UI Canvas Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 76, 18);
                            QUI.Space(8);
                            DUI.DUISettings.HierarchyManager_UICanvas_ShowCanvasName = QUI.Toggle(DUI.DUISettings.HierarchyManager_UICanvas_ShowCanvasName);
                            QUI.Label("Canvas Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                            QUI.Space(8);
                            DUI.DUISettings.HierarchyManager_UICanvas_ShowSortingLayerNameAndOrder = QUI.Toggle(DUI.DUISettings.HierarchyManager_UICanvas_ShowSortingLayerNameAndOrder);
                            QUI.Label("Sorting Layer Name and Order", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 144, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconUIButton128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_UIButton_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_UIButton_ShowIcon);
                            QUI.Label("UI Button Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 76, 18);
                            QUI.Space(8);
                            DUI.DUISettings.HierarchyManager_UIButton_ShowButtonCategory = QUI.Toggle(DUI.DUISettings.HierarchyManager_UIButton_ShowButtonCategory);
                            QUI.Label("Button Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                            QUI.Space(8);
                            DUI.DUISettings.HierarchyManager_UIButton_ShowButtonName = QUI.Toggle(DUI.DUISettings.HierarchyManager_UIButton_ShowButtonName);
                            QUI.Label("Button Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconUIElement128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_UIElement_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_UIElement_ShowIcon);
                            QUI.Label("UI Element Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 76, 18);
                            QUI.Space(8);
                            DUI.DUISettings.HierarchyManager_UIElement_ShowElementCategory = QUI.Toggle(DUI.DUISettings.HierarchyManager_UIElement_ShowElementCategory);
                            QUI.Label("Element Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                            QUI.Space(8);
                            DUI.DUISettings.HierarchyManager_UIElement_ShowElementName = QUI.Toggle(DUI.DUISettings.HierarchyManager_UIElement_ShowElementName);
                            QUI.Label("Element Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                            QUI.Space(8);
                            DUI.DUISettings.HierarchyManager_UIElement_ShowSortingLayerNameAndOrder = QUI.Toggle(DUI.DUISettings.HierarchyManager_UIElement_ShowSortingLayerNameAndOrder);
                            QUI.Label("Sorting Layer Name and Order", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 144, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconUIEffect128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_UIEffect_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_UIEffect_ShowIcon);
                            QUI.Label("UI Effect Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 76, 18);
                            QUI.Space(8);
                            DUI.DUISettings.HierarchyManager_UIEffect_ShowSortingLayerNameAndOrder = QUI.Toggle(DUI.DUISettings.HierarchyManager_UIEffect_ShowSortingLayerNameAndOrder);
                            QUI.Label("Sorting Layer Name and Order", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 144, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconUITrigger128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_UITrigger_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_UITrigger_ShowIcon);
                            QUI.Label("UI Trigger Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 70, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconUINotification128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_UINotification_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_UINotification_ShowIcon);
                            QUI.Label("UI Notification Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 92, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconUIManager128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_UIManager_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_UIManager_ShowIcon);
                            QUI.Label("UI Manager Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconSoundy128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_Soundy_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_Soundy_ShowIcon);
                            QUI.Label("Soundy Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 60, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconUINotificationManager128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_UINotificationManager_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_UINotificationManager_ShowIcon);
                            QUI.Label("UI Notification Manager Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 136, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconOrientationManager128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_OrientationManager_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_OrientationManager_ShowIcon);
                            QUI.Label("Orientation Manager Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 122, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconSceneLoader128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_SceneLoader_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_SceneLoader_ShowIcon);
                            QUI.Label("Scene Loader Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 94, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.DrawTexture(DUIResources.iconPlayMakerEventDispatcher128x128.texture, 24, 24);
                            DUI.DUISettings.HierarchyManager_PlaymakerEventDispatcher_ShowIcon = QUI.Toggle(DUI.DUISettings.HierarchyManager_PlaymakerEventDispatcher_ShowIcon);
                            QUI.Label("Playmaker Event Dispatcher Icon", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 154, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(16);
                    }
                    QUI.EndVertical();
                }
                QUI.EndHorizontal();
                if (QUI.EndChangeCheck())
                {
                    DUIHierarchyManager.UpdateReferences();
                    EditorApplication.RepaintHierarchyWindow();
                }
            }
            QUI.EndFadeGroup();
        }

        void DrawSettingsUIElement(float width)
        {
            if (ButtonBar("UIElement", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelLarge), UIElementAnimBool, width)) { UIElementAnimBool.target = !UIElementAnimBool.target; }
            if (QUI.BeginFadeGroup(UIElementAnimBool.faded))
            {
                QUI.BeginHorizontal(width);
                {
                    QUI.Space(INDENT_24);
                    QUI.BeginVertical(width - INDENT_24);
                    {
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Label("Inspector Settings", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 88, 18);
                            QUI.Space(2);
                            DUI.DUISettings.UIElement_Inspector_ShowButtonRenameGameObject = QUI.Toggle(DUI.DUISettings.UIElement_Inspector_ShowButtonRenameGameObject);
                            QUI.Label("Show Button 'Rename GameObject to Element Name'", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 250, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(96);
                            QUI.Label("Name Prefix", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 62, 18);
                            DUI.DUISettings.UIElement_Inspector_RenameGameObjectPrefix = EditorGUILayout.DelayedTextField(DUI.DUISettings.UIElement_Inspector_RenameGameObjectPrefix, GUILayout.Width(147));
                            QUI.Space(2);
                            QUI.Label("Name Suffix", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 62, 18);
                            DUI.DUISettings.UIElement_Inspector_RenameGameObjectSuffix = EditorGUILayout.DelayedTextField(DUI.DUISettings.UIElement_Inspector_RenameGameObjectSuffix, GUILayout.Width(147));
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Label("Orientation", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 56, 18);
                            QUI.Space(2);
                            DUI.DUISettings.UIElement_LANDSCAPE = QUI.Toggle(DUI.DUISettings.UIElement_LANDSCAPE);
                            QUI.Label("LANDSCAPE", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 64, 18);
                            QUI.Space(2);
                            DUI.DUISettings.UIElement_PORTRAIT = QUI.Toggle(DUI.DUISettings.UIElement_PORTRAIT);
                            QUI.Label("PORTRAIT", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 56, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            DUI.DUISettings.UIElement_startHidden = QUI.Toggle(DUI.DUISettings.UIElement_startHidden);
                            QUI.Label("hide @Start", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 64, 18);
                            QUI.Space(16);
                            DUI.DUISettings.UIElement_animateAtStart = QUI.Toggle(DUI.DUISettings.UIElement_animateAtStart);
                            QUI.Label("animate @Start", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 78, 18);
                            QUI.Space(16);
                            DUI.DUISettings.UIElement_disableWhenHidden = QUI.Toggle(DUI.DUISettings.UIElement_disableWhenHidden);
                            QUI.Label("disable when hidden", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 100, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            DUI.DUISettings.UIElement_useCustomStartAnchoredPosition = QUI.Toggle(DUI.DUISettings.UIElement_useCustomStartAnchoredPosition);
                            QUI.Label("custom start position", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 100, 18);
                            QUI.Space(2);
                            DUI.DUISettings.UIElement_customStartAnchoredPosition = EditorGUILayout.Vector3Field("", DUI.DUISettings.UIElement_customStartAnchoredPosition, GUILayout.Width(150));
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            DUI.DUISettings.UIElement_executeLayoutFix = QUI.Toggle(DUI.DUISettings.UIElement_executeLayoutFix);
                            QUI.Label("execute layout fix (useful in some cases)", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 188, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(4);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(INDENT_24);
                            QUI.Label("IN ANIMATIONS", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), 100, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(INDENT_24);
                            QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                            DUI.DUISettings.UIElement_inAnimationsPresetCategoryName = EditorGUILayout.TextField(DUI.DUISettings.UIElement_inAnimationsPresetCategoryName, GUILayout.Width(200));
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(INDENT_24);
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                            DUI.DUISettings.UIElement_inAnimationsPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIElement_inAnimationsPresetName, GUILayout.Width(200));
                            QUI.Space(4);
                            DUI.DUISettings.UIElement_loadInAnimationsPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIElement_loadInAnimationsPresetAtRuntime);
                            QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(4);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(INDENT_24);
                            QUI.Label("OUT ANIMATIONS", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), 110, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(INDENT_24);
                            QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                            DUI.DUISettings.UIElement_outAnimationsPresetCategoryName = EditorGUILayout.TextField(DUI.DUISettings.UIElement_outAnimationsPresetCategoryName, GUILayout.Width(200));
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(INDENT_24);
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                            DUI.DUISettings.UIElement_outAnimationsPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIElement_outAnimationsPresetName, GUILayout.Width(200));
                            QUI.Space(4);
                            DUI.DUISettings.UIElement_loadOutAnimationsPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIElement_loadOutAnimationsPresetAtRuntime);
                            QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(4);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(INDENT_24);
                            QUI.Label("LOOP ANIMATIONS", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), 116, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(INDENT_24);
                            DUI.DUISettings.UIElement_Inspector_HideLoopAnimations = QUI.Toggle(DUI.DUISettings.UIElement_Inspector_HideLoopAnimations);
                            QUI.Label("Hide in Inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(INDENT_24);
                            QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                            DUI.DUISettings.UIElement_loopAnimationsPresetCategoryName = EditorGUILayout.TextField(DUI.DUISettings.UIElement_loopAnimationsPresetCategoryName, GUILayout.Width(200));
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(INDENT_24);
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                            DUI.DUISettings.UIElement_loopAnimationsPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIElement_loopAnimationsPresetName, GUILayout.Width(200));
                            QUI.Space(4);
                            DUI.DUISettings.UIElement_loadLoopAnimationsPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIElement_loadLoopAnimationsPresetAtRuntime);
                            QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(10);
                    }
                    QUI.EndVertical();
                }
                QUI.EndHorizontal();
            }
            QUI.EndFadeGroup();
        }

        void DrawSettingsUIButton(float width)
        {
            if (ButtonBar("UIButton", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelLarge), UIButtonAnimBool, width)) { UIButtonAnimBool.target = !UIButtonAnimBool.target; }
            if (QUI.BeginFadeGroup(UIButtonAnimBool.faded))
            {
                QUI.BeginHorizontal(width);
                {
                    QUI.Space(INDENT_24);
                    QUI.BeginVertical(width - INDENT_24);
                    {
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Label("Inspector Settings", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 88, 18);
                            QUI.Space(2);
                            DUI.DUISettings.UIButton_Inspector_ShowButtonRenameGameObject = QUI.Toggle(DUI.DUISettings.UIButton_Inspector_ShowButtonRenameGameObject);
                            QUI.Label("Show Button 'Rename GameObject to target Element Name'", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 280, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(96);
                            QUI.Label("Name Prefix", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 62, 18);
                            DUI.DUISettings.UIButton_Inspector_RenameGameObjectPrefix = EditorGUILayout.DelayedTextField(DUI.DUISettings.UIButton_Inspector_RenameGameObjectPrefix, GUILayout.Width(147));
                            QUI.Space(2);
                            QUI.Label("Name Suffix", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 62, 18);
                            DUI.DUISettings.UIButton_Inspector_RenameGameObjectSuffix = EditorGUILayout.DelayedTextField(DUI.DUISettings.UIButton_Inspector_RenameGameObjectSuffix, GUILayout.Width(147));
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            DUI.DUISettings.UIButton_allowMultipleClicks = QUI.Toggle(DUI.DUISettings.UIButton_allowMultipleClicks);
                            QUI.Label("Allow Multiple Clicks", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 98, 18);
                            QUI.Space(16);
                            QUI.Label("Disable Button Interval", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 108, 18);
                            DUI.DUISettings.UIButton_disableButtonInterval = EditorGUILayout.FloatField(DUI.DUISettings.UIButton_disableButtonInterval, GUILayout.Width(40));
                            QUI.Space(16);
                            DUI.DUISettings.UIButton_deselectButtonOnClick = QUI.Toggle(DUI.DUISettings.UIButton_deselectButtonOnClick);
                            QUI.Label("Deselect On Button Click", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 118, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        DrawSettingsUIButtonOnPointerEnter(width - INDENT_24);
                        QUI.Space(2);
                        DrawSettingsUIButtonOnPointerExit(width - INDENT_24);
                        QUI.Space(2);
                        DrawSettingsUIButtonOnPointerDown(width - INDENT_24);
                        QUI.Space(2);
                        DrawSettingsUIButtonOnPointerUp(width - INDENT_24);
                        QUI.Space(2);
                        DrawSettingsUIButtonOnClick(width - INDENT_24);
                        QUI.Space(2);
                        DrawSettingsUIButtonOnDoubleClick(width - INDENT_24);
                        QUI.Space(2);
                        DrawSettingsUIButtonOnLongClick(width - INDENT_24);
                        QUI.Space(2);
                        DrawSettingsUIButtonNormalLoop(width - INDENT_24);
                        QUI.Space(2);
                        DrawSettingsUIButtonSelectedLoop(width - INDENT_24);
                        QUI.Space(10);
                    }
                    QUI.EndVertical();
                }
                QUI.EndHorizontal();
            }
            QUI.EndFadeGroup();
        }
        void DrawSettingsUIButtonOnPointerEnter(float width)
        {
            if (ButtonBar("OnPointerEnter", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), UIButtonOnPointerEnterAnimBool, width)) { UIButtonOnPointerEnterAnimBool.target = !UIButtonOnPointerEnterAnimBool.target; }
            if (DUI.DUISettings.UIButton_Inspector_HideOnPointerEnter)
            {
                QUI.Space(-24);
                QUI.BeginHorizontal(width);
                {
                    QUI.FlexibleSpace();
                    QUI.Label("hidden in inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 100, 20);
                }
                QUI.EndHorizontal();
            }
            if (QUI.BeginFadeGroup(UIButtonOnPointerEnterAnimBool.faded))
            {
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    DUI.DUISettings.UIButton_Inspector_HideOnPointerEnter = QUI.Toggle(DUI.DUISettings.UIButton_Inspector_HideOnPointerEnter);
                    QUI.Label("Hide in Inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                    if (DUI.DUISettings.UIButton_Inspector_HideOnPointerEnter) { DUI.DUISettings.UIButton_useOnPointerEnter = false; }
                    QUI.Space(16);
                    DUI.DUISettings.UIButton_useOnPointerEnter = QUI.Toggle(DUI.DUISettings.UIButton_useOnPointerEnter);
                    QUI.Label("Enabled", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 42, 18);
                    QUI.Space(16);
                    QUI.Label("Disable Interval", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 76, 18);
                    DUI.DUISettings.UIButton_onPointerEnterDisableInterval = EditorGUILayout.FloatField(DUI.DUISettings.UIButton_onPointerEnterDisableInterval, GUILayout.Width(40));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Play Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerEnterSound = EditorGUILayout.TextArea(DUI.DUISettings.UIButton_onPointerEnterSound, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_customOnPointerEnterSound = QUI.Toggle(DUI.DUISettings.UIButton_customOnPointerEnterSound);
                    QUI.Label("Custom Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 72, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerEnterPunchPresetCategory = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onPointerEnterPunchPresetCategory, GUILayout.Width(200));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerEnterPunchPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onPointerEnterPunchPresetName, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_loadOnPointerEnterPunchPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIButton_loadOnPointerEnterPunchPresetAtRuntime);
                    QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.Space(6);
            }
            QUI.EndFadeGroup();
        }
        void DrawSettingsUIButtonOnPointerExit(float width)
        {
            if (ButtonBar("OnPointerExit", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), UIButtonOnPointerExitAnimBool, width)) { UIButtonOnPointerExitAnimBool.target = !UIButtonOnPointerExitAnimBool.target; }
            if (DUI.DUISettings.UIButton_Inspector_HideOnPointerExit)
            {
                QUI.Space(-24);
                QUI.BeginHorizontal(width);
                {
                    QUI.FlexibleSpace();
                    QUI.Label("hidden in inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 100, 20);
                }
                QUI.EndHorizontal();
            }
            if (QUI.BeginFadeGroup(UIButtonOnPointerExitAnimBool.faded))
            {
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    DUI.DUISettings.UIButton_Inspector_HideOnPointerExit = QUI.Toggle(DUI.DUISettings.UIButton_Inspector_HideOnPointerExit);
                    QUI.Label("Hide in Inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                    if (DUI.DUISettings.UIButton_Inspector_HideOnPointerExit) { DUI.DUISettings.UIButton_useOnPointerExit = false; }
                    QUI.Space(16);
                    DUI.DUISettings.UIButton_useOnPointerExit = QUI.Toggle(DUI.DUISettings.UIButton_useOnPointerExit);
                    QUI.Label("Enabled", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 42, 18);
                    QUI.Space(16);
                    QUI.Label("Disable Interval", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 76, 18);
                    DUI.DUISettings.UIButton_onPointerExitDisableInterval = EditorGUILayout.FloatField(DUI.DUISettings.UIButton_onPointerExitDisableInterval, GUILayout.Width(40));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Play Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerExitSound = EditorGUILayout.TextArea(DUI.DUISettings.UIButton_onPointerExitSound, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_customOnPointerExitSound = QUI.Toggle(DUI.DUISettings.UIButton_customOnPointerExitSound);
                    QUI.Label("Custom Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 72, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerExitPunchPresetCategory = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onPointerExitPunchPresetCategory, GUILayout.Width(200));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerExitPunchPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onPointerExitPunchPresetName, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_loadOnPointerExitPunchPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIButton_loadOnPointerExitPunchPresetAtRuntime);
                    QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.Space(6);
            }
            QUI.EndFadeGroup();
        }
        void DrawSettingsUIButtonOnPointerDown(float width)
        {
            if (ButtonBar("OnPointerDown", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), UIButtonOnPointerDownAnimBool, width)) { UIButtonOnPointerDownAnimBool.target = !UIButtonOnPointerDownAnimBool.target; }
            if (DUI.DUISettings.UIButton_Inspector_HideOnPointerDown)
            {
                QUI.Space(-24);
                QUI.BeginHorizontal(width);
                {
                    QUI.FlexibleSpace();
                    QUI.Label("hidden in inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 100, 20);
                }
                QUI.EndHorizontal();
            }
            if (QUI.BeginFadeGroup(UIButtonOnPointerDownAnimBool.faded))
            {
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    DUI.DUISettings.UIButton_Inspector_HideOnPointerDown = QUI.Toggle(DUI.DUISettings.UIButton_Inspector_HideOnPointerDown);
                    QUI.Label("Hide in Inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                    if (DUI.DUISettings.UIButton_Inspector_HideOnPointerDown) { DUI.DUISettings.UIButton_useOnPointerDown = false; }
                    QUI.Space(16);
                    DUI.DUISettings.UIButton_useOnPointerDown = QUI.Toggle(DUI.DUISettings.UIButton_useOnPointerDown);
                    QUI.Label("Enabled", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 42, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Play Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerDownSound = EditorGUILayout.TextArea(DUI.DUISettings.UIButton_onPointerDownSound, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_customOnPointerDownSound = QUI.Toggle(DUI.DUISettings.UIButton_customOnPointerDownSound);
                    QUI.Label("Custom Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 72, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerDownPunchPresetCategory = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onPointerDownPunchPresetCategory, GUILayout.Width(200));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerDownPunchPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onPointerDownPunchPresetName, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_loadOnPointerDownPunchPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIButton_loadOnPointerDownPunchPresetAtRuntime);
                    QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.Space(6);
            }
            QUI.EndFadeGroup();
        }
        void DrawSettingsUIButtonOnPointerUp(float width)
        {
            if (ButtonBar("OnPointerUp", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), UIButtonOnPointerUpAnimBool, width)) { UIButtonOnPointerUpAnimBool.target = !UIButtonOnPointerUpAnimBool.target; }
            if (DUI.DUISettings.UIButton_Inspector_HideOnPointerUp)
            {
                QUI.Space(-24);
                QUI.BeginHorizontal(width);
                {
                    QUI.FlexibleSpace();
                    QUI.Label("hidden in inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 100, 20);
                }
                QUI.EndHorizontal();
            }
            if (QUI.BeginFadeGroup(UIButtonOnPointerUpAnimBool.faded))
            {
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    DUI.DUISettings.UIButton_Inspector_HideOnPointerUp = QUI.Toggle(DUI.DUISettings.UIButton_Inspector_HideOnPointerUp);
                    QUI.Label("Hide in Inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                    if (DUI.DUISettings.UIButton_Inspector_HideOnPointerUp) { DUI.DUISettings.UIButton_useOnPointerUp = false; }
                    QUI.Space(16);
                    DUI.DUISettings.UIButton_useOnPointerUp = QUI.Toggle(DUI.DUISettings.UIButton_useOnPointerUp);
                    QUI.Label("Enabled", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 42, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Play Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerUpSound = EditorGUILayout.TextArea(DUI.DUISettings.UIButton_onPointerUpSound, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_customOnPointerUpSound = QUI.Toggle(DUI.DUISettings.UIButton_customOnPointerUpSound);
                    QUI.Label("Custom Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 72, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerUpPunchPresetCategory = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onPointerUpPunchPresetCategory, GUILayout.Width(200));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onPointerUpPunchPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onPointerUpPunchPresetName, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_loadOnPointerUpPunchPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIButton_loadOnPointerUpPunchPresetAtRuntime);
                    QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.Space(6);
            }
            QUI.EndFadeGroup();
        }
        void DrawSettingsUIButtonOnClick(float width)
        {
            if (ButtonBar("OnClick", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), UIButtonOnClickAnimBool, width)) { UIButtonOnClickAnimBool.target = !UIButtonOnClickAnimBool.target; }
            if (DUI.DUISettings.UIButton_Inspector_HideOnClick)
            {
                QUI.Space(-24);
                QUI.BeginHorizontal(width);
                {
                    QUI.FlexibleSpace();
                    QUI.Label("hidden in inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 100, 20);
                }
                QUI.EndHorizontal();
            }
            if (QUI.BeginFadeGroup(UIButtonOnClickAnimBool.faded))
            {
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    DUI.DUISettings.UIButton_Inspector_HideOnClick = QUI.Toggle(DUI.DUISettings.UIButton_Inspector_HideOnClick);
                    QUI.Label("Hide in Inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                    if (DUI.DUISettings.UIButton_Inspector_HideOnClick) { DUI.DUISettings.UIButton_useOnClickAnimations = false; }
                    QUI.Space(16);
                    DUI.DUISettings.UIButton_useOnClickAnimations = QUI.Toggle(DUI.DUISettings.UIButton_useOnClickAnimations);
                    QUI.Label("Enabled", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 42, 18);
                    QUI.Space(16);
                    DUI.DUISettings.UIButton_waitForOnClickAnimation = QUI.Toggle(DUI.DUISettings.UIButton_waitForOnClickAnimation);
                    QUI.Label("Wait for Animation", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 90, 18);
                    QUI.Space(16);
                    QUI.Label("Single Click Mode", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                    DUI.DUISettings.UIButton_singleClickMode = (UIButton.SingleClickMode)EditorGUILayout.EnumPopup(DUI.DUISettings.UIButton_singleClickMode, GUILayout.Width(80));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Play Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onClickSound = EditorGUILayout.TextArea(DUI.DUISettings.UIButton_onClickSound, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_customOnClickSound = QUI.Toggle(DUI.DUISettings.UIButton_customOnClickSound);
                    QUI.Label("Custom Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 72, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onClickPunchPresetCategory = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onClickPunchPresetCategory, GUILayout.Width(200));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onClickPunchPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onClickPunchPresetName, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_loadOnClickPunchPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIButton_loadOnClickPunchPresetAtRuntime);
                    QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.Space(6);
            }
            QUI.EndFadeGroup();
        }
        void DrawSettingsUIButtonOnDoubleClick(float width)
        {
            if (ButtonBar("OnDoubleClick", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), UIButtonOnDoubleClickAnimBool, width)) { UIButtonOnDoubleClickAnimBool.target = !UIButtonOnDoubleClickAnimBool.target; }
            if (DUI.DUISettings.UIButton_Inspector_HideOnDoubleClick)
            {
                QUI.Space(-24);
                QUI.BeginHorizontal(width);
                {
                    QUI.FlexibleSpace();
                    QUI.Label("hidden in inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 100, 20);
                }
                QUI.EndHorizontal();
            }
            if (QUI.BeginFadeGroup(UIButtonOnDoubleClickAnimBool.faded))
            {
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    DUI.DUISettings.UIButton_Inspector_HideOnDoubleClick = QUI.Toggle(DUI.DUISettings.UIButton_Inspector_HideOnDoubleClick);
                    QUI.Label("Hide in Inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                    if (DUI.DUISettings.UIButton_Inspector_HideOnDoubleClick) { DUI.DUISettings.UIButton_useOnDoubleClick = false; }
                    QUI.Space(16);
                    DUI.DUISettings.UIButton_useOnDoubleClick = QUI.Toggle(DUI.DUISettings.UIButton_useOnDoubleClick);
                    QUI.Label("Enabled", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 42, 18);
                    QUI.Space(16);
                    DUI.DUISettings.UIButton_waitForOnDoubleClickAnimation = QUI.Toggle(DUI.DUISettings.UIButton_waitForOnDoubleClickAnimation);
                    QUI.Label("Wait for Animation", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 90, 18);
                    QUI.Space(16);
                    QUI.Label("Register Interval", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_doubleClickRegisterInterval = EditorGUILayout.FloatField(DUI.DUISettings.UIButton_doubleClickRegisterInterval, GUILayout.Width(40));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Play Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onDoubleClickSound = EditorGUILayout.TextArea(DUI.DUISettings.UIButton_onDoubleClickSound, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_customOnDoubleClickSound = QUI.Toggle(DUI.DUISettings.UIButton_customOnDoubleClickSound);
                    QUI.Label("Custom Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 72, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onDoubleClickPunchPresetCategory = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onDoubleClickPunchPresetCategory, GUILayout.Width(200));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onDoubleClickPunchPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onDoubleClickPunchPresetName, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_loadOnDoubleClickPunchPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIButton_loadOnDoubleClickPunchPresetAtRuntime);
                    QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.Space(6);
            }
            QUI.EndFadeGroup();
        }
        void DrawSettingsUIButtonOnLongClick(float width)
        {
            if (ButtonBar("OnLongClick", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), UIButtonOnLongClickAnimBool, width)) { UIButtonOnLongClickAnimBool.target = !UIButtonOnLongClickAnimBool.target; }
            if (DUI.DUISettings.UIButton_Inspector_HideOnLongClick)
            {
                QUI.Space(-24);
                QUI.BeginHorizontal(width);
                {
                    QUI.FlexibleSpace();
                    QUI.Label("hidden in inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 100, 20);
                }
                QUI.EndHorizontal();
            }
            if (QUI.BeginFadeGroup(UIButtonOnLongClickAnimBool.faded))
            {
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    DUI.DUISettings.UIButton_Inspector_HideOnLongClick = QUI.Toggle(DUI.DUISettings.UIButton_Inspector_HideOnLongClick);
                    QUI.Label("Hide in Inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                    if (DUI.DUISettings.UIButton_Inspector_HideOnLongClick) { DUI.DUISettings.UIButton_useOnLongClick = false; }
                    QUI.Space(16);
                    DUI.DUISettings.UIButton_useOnLongClick = QUI.Toggle(DUI.DUISettings.UIButton_useOnLongClick);
                    QUI.Label("Enabled", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 42, 18);
                    QUI.Space(16);
                    DUI.DUISettings.UIButton_waitForOnLongClickAnimation = QUI.Toggle(DUI.DUISettings.UIButton_waitForOnLongClickAnimation);
                    QUI.Label("Wait for Animation", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 90, 18);
                    QUI.Space(16);
                    QUI.Label("Register Interval", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_longClickRegisterInterval = EditorGUILayout.FloatField(DUI.DUISettings.UIButton_longClickRegisterInterval, GUILayout.Width(40));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Play Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onLongClickSound = EditorGUILayout.TextArea(DUI.DUISettings.UIButton_onLongClickSound, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_customOnLongClickSound = QUI.Toggle(DUI.DUISettings.UIButton_customOnLongClickSound);
                    QUI.Label("Custom Sound", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 72, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onLongClickPunchPresetCategory = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onLongClickPunchPresetCategory, GUILayout.Width(200));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_onLongClickPunchPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIButton_onLongClickPunchPresetName, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_loadOnLongClickPunchPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIButton_loadOnLongClickPunchPresetAtRuntime);
                    QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.Space(6);
            }
            QUI.EndFadeGroup();
        }
        void DrawSettingsUIButtonNormalLoop(float width)
        {
            if (ButtonBar("Normal Animation", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), UIButtonNormalLoop, width)) { UIButtonNormalLoop.target = !UIButtonNormalLoop.target; }
            if (DUI.DUISettings.UIButton_Inspector_HideNormalLoop)
            {
                QUI.Space(-24);
                QUI.BeginHorizontal(width);
                {
                    QUI.FlexibleSpace();
                    QUI.Label("hidden in inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 100, 20);
                }
                QUI.EndHorizontal();
            }
            if (QUI.BeginFadeGroup(UIButtonNormalLoop.faded))
            {
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    DUI.DUISettings.UIButton_Inspector_HideNormalLoop = QUI.Toggle(DUI.DUISettings.UIButton_Inspector_HideNormalLoop);
                    QUI.Label("Hide in Inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_normalLoopPresetCategory = EditorGUILayout.TextField(DUI.DUISettings.UIButton_normalLoopPresetCategory, GUILayout.Width(200));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_normalLoopPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIButton_normalLoopPresetName, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_loadNormalLoopPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIButton_loadNormalLoopPresetAtRuntime);
                    QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.Space(6);
            }
            QUI.EndFadeGroup();
        }
        void DrawSettingsUIButtonSelectedLoop(float width)
        {
            if (ButtonBar("Selected Animation", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), UIButtonSelectedLoop, width)) { UIButtonSelectedLoop.target = !UIButtonSelectedLoop.target; }
            if (DUI.DUISettings.UIButton_Inspector_HideSelectedLoop)
            {
                QUI.Space(-24);
                QUI.BeginHorizontal(width);
                {
                    QUI.FlexibleSpace();
                    QUI.Label("hidden in inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 100, 20);
                }
                QUI.EndHorizontal();
            }
            if (QUI.BeginFadeGroup(UIButtonSelectedLoop.faded))
            {
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    DUI.DUISettings.UIButton_Inspector_HideSelectedLoop = QUI.Toggle(DUI.DUISettings.UIButton_Inspector_HideSelectedLoop);
                    QUI.Label("Hide in Inspector", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 86, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_selectedLoopPresetCategory = EditorGUILayout.TextField(DUI.DUISettings.UIButton_selectedLoopPresetCategory, GUILayout.Width(200));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.BeginHorizontal(width - INDENT_24);
                {
                    QUI.Space(INDENT_24);
                    QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 80, 18);
                    DUI.DUISettings.UIButton_selectedLoopPresetName = EditorGUILayout.TextField(DUI.DUISettings.UIButton_selectedLoopPresetName, GUILayout.Width(200));
                    QUI.Space(4);
                    DUI.DUISettings.UIButton_loadSelectedLoopPresetAtRuntime = QUI.Toggle(DUI.DUISettings.UIButton_loadSelectedLoopPresetAtRuntime);
                    QUI.Label("Load preset at runtime", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 110, 18);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.Space(6);
            }
            QUI.EndFadeGroup();
        }

        private void DrawSettingsUIEffect(float width)
        {
            if (ButtonBar("UIEffect", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelLarge), UIEffectAnimBool, width)) { UIEffectAnimBool.target = !UIEffectAnimBool.target; }
            if (QUI.BeginFadeGroup(UIEffectAnimBool.faded))
            {
                QUI.BeginHorizontal(width);
                {
                    QUI.Space(INDENT_24);
                    QUI.BeginVertical(width - INDENT_24);
                    {
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Label("Inspector Settings", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 88, 18);
                            QUI.Space(2);
                            DUI.DUISettings.UIEffect_Inspector_ShowButtonRenameGameObject = QUI.Toggle(DUI.DUISettings.UIEffect_Inspector_ShowButtonRenameGameObject);
                            QUI.Label("Show Button 'Rename GameObject to target Element Name'", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 280, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Space(96);
                            QUI.Label("Name Prefix", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 62, 18);
                            DUI.DUISettings.UIEffect_Inspector_RenameGameObjectPrefix = EditorGUILayout.DelayedTextField(DUI.DUISettings.UIEffect_Inspector_RenameGameObjectPrefix, GUILayout.Width(147));
                            QUI.Space(2);
                            QUI.Label("Name Suffix", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 62, 18);
                            DUI.DUISettings.UIEffect_Inspector_RenameGameObjectSuffix = EditorGUILayout.DelayedTextField(DUI.DUISettings.UIEffect_Inspector_RenameGameObjectSuffix, GUILayout.Width(147));
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(8);
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            DUI.DUISettings.UIEffect_playOnAwake = QUI.Toggle(DUI.DUISettings.UIEffect_playOnAwake);
                            QUI.Label("play on awake", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 70, 18);
                            QUI.Space(16);
                            DUI.DUISettings.UIEffect_stopInstantly = QUI.Toggle(DUI.DUISettings.UIEffect_stopInstantly);
                            QUI.Label("stop instantly on hide", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 102, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            DUI.DUISettings.UIEffect_useCustomSortingLayerName = QUI.Toggle(DUI.DUISettings.UIEffect_useCustomSortingLayerName);
                            QUI.Label("use custom Layer Name", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 116, 18);
                            QUI.Space(2);
                            DUI.DUISettings.UIEffect_customSortingLayerName = EditorGUILayout.TextField(DUI.DUISettings.UIEffect_customSortingLayerName, GUILayout.Width(150));
                            QUI.Space(16);
                            DUI.DUISettings.UIEffect_useCustomOrderInLayer = QUI.Toggle(DUI.DUISettings.UIEffect_useCustomOrderInLayer);
                            QUI.Label("use custom Order in Layer", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 124, 18);
                            QUI.Space(2);
                            DUI.DUISettings.UIEffect_customOrderInLayer = EditorGUILayout.IntField(DUI.DUISettings.UIEffect_customOrderInLayer, GUILayout.Width(40));
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(width - INDENT_24);
                        {
                            QUI.Label("Set the effect", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 66, 18);
                            DUI.DUISettings.UIEffect_effectPosition = (UIEffect.EffectPosition)EditorGUILayout.EnumPopup(DUI.DUISettings.UIEffect_effectPosition, GUILayout.Width(116));
                            QUI.Label("by", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 18, 18);
                            DUI.DUISettings.UIEffect_sortingOrderStep = EditorGUILayout.IntField(DUI.DUISettings.UIEffect_sortingOrderStep, GUILayout.Width(40));
                            QUI.Label("step" + (DUI.DUISettings.UIEffect_sortingOrderStep != 1 ? "s" : ""), DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 50, 18);
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                        QUI.Space(10);
                    }
                    QUI.EndVertical();
                }
                QUI.EndHorizontal();
            }
            QUI.EndFadeGroup();
        }
    }
}
