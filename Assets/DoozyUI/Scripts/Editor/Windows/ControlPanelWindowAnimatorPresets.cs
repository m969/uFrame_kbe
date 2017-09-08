// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using UnityEditor;
using UnityEngine;

namespace DoozyUI
{
    public partial class ControlPanelWindow : QWindow
    {
        void DrawAnimatorPresets()
        {
            QUI.DrawTexture(DUIResources.headerAnimatorPresets.texture, 552, 64);
            float sectionWidth = PAGE_WIDTH - SIDE_BAR_SHADOW_WIDTH * 2;
            QUI.Space(16);
            DrawAnimatorPresetsFilter(sectionWidth);
            QUI.Space(16);
            DrawSelectedAnimatorPreset(sectionWidth);
        }
        void DrawAnimatorPresetsFilter(float width)
        {
            QUI.BeginHorizontal(width);
            {
                QUI.Space(8);
                QUI.FlexibleSpace();
                QUI.DrawTexture(DUIResources.pageButtonFilterAnimatorPresetsHeader.texture, 423, 10);
                QUI.Space(423);
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();
            QUI.Space(10);
            QUI.BeginHorizontal(width);
            {

                QUI.Space(63);
                if (selectedAnimatorPreset == AnimatorPreset.InAnimations)
                {
                    QUI.DrawTexture(DUIResources.pageButtonFilterInAnimations.active, 124, 20);
                    QUI.Space(-20);
                    QUI.Space(124);
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonFilterInAnimations), 124, 20))
                    {
                        selectedAnimatorPreset = AnimatorPreset.InAnimations;
                        refreshSelectedAnimatorPresetDatabase = true;
                        ResetSelectedAnimatorPreset();
                    }
                }
                QUI.Space(1);
                if (selectedAnimatorPreset == AnimatorPreset.OutAnimations)
                {
                    QUI.DrawTexture(DUIResources.pageButtonFilterOutAnimations.active, 137, 20);
                    QUI.Space(-20);
                    QUI.Space(137);
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonFilterOutAnimations), 137, 20))
                    {
                        selectedAnimatorPreset = AnimatorPreset.OutAnimations;
                        refreshSelectedAnimatorPresetDatabase = true;
                        ResetSelectedAnimatorPreset();
                    }
                }
                QUI.Space(1);
                if (selectedAnimatorPreset == AnimatorPreset.Loops)
                {
                    QUI.DrawTexture(DUIResources.pageButtonFilterLoops.active, 71, 20);
                    QUI.Space(-20);
                    QUI.Space(71);
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonFilterLoops), 71, 20))
                    {
                        selectedAnimatorPreset = AnimatorPreset.Loops;
                        refreshSelectedAnimatorPresetDatabase = true;
                        ResetSelectedAnimatorPreset();
                    }
                }
                QUI.Space(1);
                if (selectedAnimatorPreset == AnimatorPreset.Punches)
                {
                    QUI.DrawTexture(DUIResources.pageButtonFilterPunches.active, 88, 20);
                    QUI.Space(-20);
                    QUI.Space(88);
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonFilterPunches), 88, 20))
                    {
                        selectedAnimatorPreset = AnimatorPreset.Punches;
                        refreshSelectedAnimatorPresetDatabase = true;
                        ResetSelectedAnimatorPreset();
                    }
                }
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();
        }

        void DrawSelectedAnimatorPreset(float width)
        {
            QUI.BeginHorizontal(width);
            {
                QUI.Space(64);
                QUI.Label("Selected Category:", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 90);
                QUI.Label(openedAnimatorPresetCategory == "" ? "---" : openedAnimatorPresetCategory, DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 120);
                QUI.Space(2);
                QUI.Label("Selected Preset:", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall), 76);
                QUI.Label(selectedAnimatorPresetName == "" ? "---" : selectedAnimatorPresetName, DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 160);
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();

            switch (selectedAnimatorPreset)
            {
                case AnimatorPreset.InAnimations:
                    if (refreshSelectedAnimatorPresetDatabase)
                    {
                        ResetSelectedAnimatorPreset();
                        RefreshInAnimationsAnimatorPresets();
                    }
                    DrawAnimatorPresetsInAnimations(width);
                    break;
                case AnimatorPreset.OutAnimations:
                    if (refreshSelectedAnimatorPresetDatabase)
                    {
                        ResetSelectedAnimatorPreset();
                        RefreshOutAnimationsAnimatorPresets();
                    }
                    DrawAnimatorPresetsOutAnimations(width);
                    break;
                case AnimatorPreset.Loops:
                    if (refreshSelectedAnimatorPresetDatabase)
                    {
                        ResetSelectedAnimatorPreset();
                        RefreshLoopsAnimatorPresets();
                    }
                    DrawAnimatorPresetsLoops(width);
                    break;
                case AnimatorPreset.Punches:
                    if (refreshSelectedAnimatorPresetDatabase)
                    {
                        ResetSelectedAnimatorPreset();
                        RefreshPunchesAnimatorPresets();
                    }
                    DrawAnimatorPresetsPunches(width);
                    break;
            }
            refreshSelectedAnimatorPresetDatabase = false;
        }

        void DrawAnimatorPresetsInAnimations(float width)
        {
            showPresetSettingsAnimBool.target = openedAnimatorPresetCategory != "" && selectedAnimatorPresetName != "";
            if (showPresetSettingsAnimBool.target)
            {
                QUI.Space(8 * showPresetSettingsAnimBool.faded);
                DrawAnimaorPresetsInAnimationsSettings(width);
            }

            QUI.Space(16);
            foreach (string category in UIAnimatorUtil.InAnimDataPresetsDatabase.Keys)
            {
                QUI.BeginHorizontal(width);
                {
                    #region Button Bar
                    if (ButtonBar(category, InAnimationsAnimatorPresetsAnimBool[category], (width - 103 * InAnimationsAnimatorPresetsAnimBool[category].faded * (1 - SearchPatternAnimBool.faded))))
                    {
                        ToggleOpenAnimatorPresetCategory(category);
                    }

                    QUI.Space(-7);

                    QUI.Space(1);

                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonDelete), 100 * InAnimationsAnimatorPresetsAnimBool[category].faded * (1 - SearchPatternAnimBool.faded), 20))
                    {
                        if (EditorUtility.DisplayDialog("Delete the '" + category + "' category?", "Are you sure you want to proceed?\nOperation cannot be undone!", "Yes", "Cancel"))
                        {
                            UIAnimatorUtil.DeleteInAnimCategory(category);
                            RefreshInAnimationsAnimatorPresets(true);
                            QUI.EndHorizontal();
                            continue;
                        }
                    }
                    #endregion
                }
                QUI.EndHorizontal();
                if (!InAnimationsAnimatorPresetsAnimBool.ContainsKey(category))
                {
                    RefreshInAnimationsAnimatorPresets(true);
                }
                else
                {
                    if (QUI.BeginFadeGroup(InAnimationsAnimatorPresetsAnimBool[category].faded))
                    {
                        DrawPresetList(category, selectedAnimatorPreset, UIAnimatorUtil.GetInAnimPresetNames(category), PAGE_WIDTH - 100, "Category is empty...");
                        QUI.Space(8);
                    }
                    QUI.EndFadeGroup();
                }
                QUI.Space(2);
            }
        }
        void DrawAnimaorPresetsInAnimationsSettings(float width)
        {
            QUI.BeginChangeCheck();
            {
                QUI.BeginHorizontal(width);
                {
                    QUI.Space(64);
                    QUI.BeginVertical(420);
                    {
                        if (QUI.Button(DUIStyles.GetStyle(SelectedInAnimData.data.move.enabled ? DUIStyles.ButtonStyle.Move : DUIStyles.ButtonStyle.MoveDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedInAnimData.data.move.enabled = !SelectedInAnimData.data.move.enabled; }
                        DrawMoveIn();
                        QUI.Space(2 * showPresetSettingsAnimBool.faded);
                        if (QUI.Button(DUIStyles.GetStyle(SelectedInAnimData.data.rotate.enabled ? DUIStyles.ButtonStyle.Rotate : DUIStyles.ButtonStyle.RotateDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedInAnimData.data.rotate.enabled = !SelectedInAnimData.data.rotate.enabled; }
                        DrawRotateIn();
                        QUI.Space(2 * showPresetSettingsAnimBool.faded);
                        if (QUI.Button(DUIStyles.GetStyle(SelectedInAnimData.data.scale.enabled ? DUIStyles.ButtonStyle.Scale : DUIStyles.ButtonStyle.ScaleDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedInAnimData.data.scale.enabled = !SelectedInAnimData.data.scale.enabled; }
                        DrawScaleIn();
                        QUI.Space(2 * showPresetSettingsAnimBool.faded);
                        if (QUI.Button(DUIStyles.GetStyle(SelectedInAnimData.data.fade.enabled ? DUIStyles.ButtonStyle.Fade : DUIStyles.ButtonStyle.FadeDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedInAnimData.data.fade.enabled = !SelectedInAnimData.data.fade.enabled; }
                        DrawFadeIn();
                    }
                    QUI.EndVertical();
                }
                QUI.EndHorizontal();
            }
            if (QUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(SelectedInAnimData);
            }
            QUI.Space(16 * showPresetSettingsAnimBool.faded);
        }
        void DrawMoveIn()
        {
            showMoveIn.target = SelectedInAnimData.data.move.enabled;
            if (QUI.BeginFadeGroup(showMoveIn.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedInAnimData.data.move.startDelay = EditorGUILayout.FloatField(SelectedInAnimData.data.move.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedInAnimData.data.move.duration = EditorGUILayout.FloatField(SelectedInAnimData.data.move.duration, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("move from", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
                        SelectedInAnimData.data.move.moveDirection = (Move.MoveDirection)EditorGUILayout.EnumPopup(SelectedInAnimData.data.move.moveDirection, GUILayout.Width(134));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedInAnimData.data.move.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedInAnimData.data.move.easeType, GUILayout.Width(105));
                        if ((SelectedInAnimData.data.move.moveDirection == Move.MoveDirection.CustomPosition))
                        {
                            if (SelectedInAnimData.data.move.easeType == UIAnimator.EaseType.Ease)
                            {
                                SelectedInAnimData.data.move.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedInAnimData.data.move.ease, GUILayout.Width(98));
                            }
                            else
                            {
                                SelectedInAnimData.data.move.animationCurve = EditorGUILayout.CurveField(SelectedInAnimData.data.move.animationCurve, GUILayout.Width(98));
                            }
                            QUI.Space(SPACE_2);
                            SelectedInAnimData.data.move.customPosition = EditorGUILayout.Vector3Field("", SelectedInAnimData.data.move.customPosition, GUILayout.Width(202));
                        }
                        else
                        {
                            if (SelectedInAnimData.data.move.easeType == UIAnimator.EaseType.Ease)
                            {
                                SelectedInAnimData.data.move.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedInAnimData.data.move.ease, GUILayout.Width(307));
                            }
                            else
                            {
                                SelectedInAnimData.data.move.animationCurve = EditorGUILayout.CurveField(SelectedInAnimData.data.move.animationCurve, GUILayout.Width(307));
                            }
                        }
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();

        }
        void DrawRotateIn()
        {
            showRotateIn.target = SelectedInAnimData.data.rotate.enabled;
            if (QUI.BeginFadeGroup(showRotateIn.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.OrangeLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedInAnimData.data.rotate.startDelay = EditorGUILayout.FloatField(SelectedInAnimData.data.rotate.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedInAnimData.data.rotate.duration = EditorGUILayout.FloatField(SelectedInAnimData.data.rotate.duration, GUILayout.Width(38));
                        QUI.Label("rotation", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        SelectedInAnimData.data.rotate.rotation = EditorGUILayout.Vector3Field("", SelectedInAnimData.data.rotate.rotation, GUILayout.Width(150));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedInAnimData.data.rotate.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedInAnimData.data.rotate.easeType, GUILayout.Width(105));
                        if (SelectedInAnimData.data.rotate.easeType == UIAnimator.EaseType.Ease)
                        {
                            SelectedInAnimData.data.rotate.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedInAnimData.data.rotate.ease, GUILayout.Width(307));
                        }
                        else
                        {
                            SelectedInAnimData.data.rotate.animationCurve = EditorGUILayout.CurveField(SelectedInAnimData.data.rotate.animationCurve, GUILayout.Width(307));
                        }
                    }
                    QUI.EndHorizontal();
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawScaleIn()
        {
            showScaleIn.target = SelectedInAnimData.data.scale.enabled;
            if (QUI.BeginFadeGroup(showScaleIn.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedInAnimData.data.scale.startDelay = EditorGUILayout.FloatField(SelectedInAnimData.data.scale.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedInAnimData.data.scale.duration = EditorGUILayout.FloatField(SelectedInAnimData.data.scale.duration, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("scale", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        SelectedInAnimData.data.scale.scale = EditorGUILayout.Vector3Field("", SelectedInAnimData.data.scale.scale, GUILayout.Width(150));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedInAnimData.data.scale.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedInAnimData.data.scale.easeType, GUILayout.Width(105));
                        if (SelectedInAnimData.data.scale.easeType == UIAnimator.EaseType.Ease)
                        {
                            SelectedInAnimData.data.scale.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedInAnimData.data.scale.ease, GUILayout.Width(307));
                        }
                        else
                        {
                            SelectedInAnimData.data.scale.animationCurve = EditorGUILayout.CurveField(SelectedInAnimData.data.scale.animationCurve, GUILayout.Width(307));
                        }
                    }
                    QUI.EndHorizontal();
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawFadeIn()
        {
            showFadeIn.target = SelectedInAnimData.data.fade.enabled;
            if (QUI.BeginFadeGroup(showFadeIn.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.PurpleLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedInAnimData.data.fade.startDelay = EditorGUILayout.FloatField(SelectedInAnimData.data.fade.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedInAnimData.data.fade.duration = EditorGUILayout.FloatField(SelectedInAnimData.data.fade.duration, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("alpha", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        SelectedInAnimData.data.fade.alpha = EditorGUILayout.FloatField(SelectedInAnimData.data.fade.alpha, GUILayout.Width(150));

                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedInAnimData.data.fade.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedInAnimData.data.fade.easeType, GUILayout.Width(105));
                        if (SelectedInAnimData.data.fade.easeType == UIAnimator.EaseType.Ease)
                        {
                            SelectedInAnimData.data.fade.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedInAnimData.data.fade.ease, GUILayout.Width(307));
                        }
                        else
                        {
                            SelectedInAnimData.data.fade.animationCurve = EditorGUILayout.CurveField(SelectedInAnimData.data.fade.animationCurve, GUILayout.Width(307));
                        }
                    }
                    QUI.EndHorizontal();
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawAnimatorPresetsOutAnimations(float width)
        {
            showPresetSettingsAnimBool.target = openedAnimatorPresetCategory != "" && selectedAnimatorPresetName != "";
            if (showPresetSettingsAnimBool.target)
            {
                QUI.Space(8 * showPresetSettingsAnimBool.faded);
                DrawAnimaorPresetsOutAnimationsSettings(width);
            }

            QUI.Space(16);
            foreach (string category in UIAnimatorUtil.OutAnimDataPresetsDatabase.Keys)
            {
                QUI.BeginHorizontal(width);
                {
                    #region Button Bar
                    if (ButtonBar(category, OutAnimationsAnimatorPresetsAnimBool[category], (width - 103 * OutAnimationsAnimatorPresetsAnimBool[category].faded * (1 - SearchPatternAnimBool.faded))))
                    {
                        ToggleOpenAnimatorPresetCategory(category);
                    }

                    QUI.Space(-7);

                    QUI.Space(1);

                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonDelete), 100 * OutAnimationsAnimatorPresetsAnimBool[category].faded * (1 - SearchPatternAnimBool.faded), 20))
                    {
                        if (EditorUtility.DisplayDialog("Delete the '" + category + "' category?", "Are you sure you want to proceed?\nOperation cannot be undone!", "Yes", "Cancel"))
                        {
                            UIAnimatorUtil.DeleteOutAnimCategory(category);
                            RefreshOutAnimationsAnimatorPresets(true);
                            QUI.EndHorizontal();
                            continue;
                        }
                    }
                    #endregion
                }
                QUI.EndHorizontal();
                if (!OutAnimationsAnimatorPresetsAnimBool.ContainsKey(category))
                {
                    RefreshOutAnimationsAnimatorPresets(true);
                }
                else
                {
                    if (QUI.BeginFadeGroup(OutAnimationsAnimatorPresetsAnimBool[category].faded))
                    {
                        DrawPresetList(category, selectedAnimatorPreset, UIAnimatorUtil.GetOutAnimPresetNames(category), PAGE_WIDTH - 100, "Category is empty...");
                        QUI.Space(8);
                    }
                    QUI.EndFadeGroup();
                }
                QUI.Space(2);
            }
        }
        void DrawAnimaorPresetsOutAnimationsSettings(float width)
        {
            QUI.BeginChangeCheck();
            {
                QUI.BeginHorizontal(width);
                {
                    QUI.Space(64);
                    QUI.BeginVertical(420);
                    {
                        if (QUI.Button(DUIStyles.GetStyle(SelectedOutAnimData.data.move.enabled ? DUIStyles.ButtonStyle.Move : DUIStyles.ButtonStyle.MoveDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedOutAnimData.data.move.enabled = !SelectedOutAnimData.data.move.enabled; }
                        DrawMoveOut();
                        QUI.Space(2 * showPresetSettingsAnimBool.faded);
                        if (QUI.Button(DUIStyles.GetStyle(SelectedOutAnimData.data.rotate.enabled ? DUIStyles.ButtonStyle.Rotate : DUIStyles.ButtonStyle.RotateDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedOutAnimData.data.rotate.enabled = !SelectedOutAnimData.data.rotate.enabled; }
                        DrawRotateOut();
                        QUI.Space(2 * showPresetSettingsAnimBool.faded);
                        if (QUI.Button(DUIStyles.GetStyle(SelectedOutAnimData.data.scale.enabled ? DUIStyles.ButtonStyle.Scale : DUIStyles.ButtonStyle.ScaleDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedOutAnimData.data.scale.enabled = !SelectedOutAnimData.data.scale.enabled; }
                        DrawScaleOut();
                        QUI.Space(2 * showPresetSettingsAnimBool.faded);
                        if (QUI.Button(DUIStyles.GetStyle(SelectedOutAnimData.data.fade.enabled ? DUIStyles.ButtonStyle.Fade : DUIStyles.ButtonStyle.FadeDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedOutAnimData.data.fade.enabled = !SelectedOutAnimData.data.fade.enabled; }
                        DrawFadeOut();
                    }
                    QUI.EndVertical();
                }
                QUI.EndHorizontal();
            }
            if (QUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(SelectedOutAnimData);
            }
            QUI.Space(16 * showPresetSettingsAnimBool.faded);
        }
        void DrawMoveOut()
        {
            showMoveOut.target = SelectedOutAnimData.data.move.enabled;
            if (QUI.BeginFadeGroup(showMoveOut.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedOutAnimData.data.move.startDelay = EditorGUILayout.FloatField(SelectedOutAnimData.data.move.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedOutAnimData.data.move.duration = EditorGUILayout.FloatField(SelectedOutAnimData.data.move.duration, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("move to", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
                        SelectedOutAnimData.data.move.moveDirection = (Move.MoveDirection)EditorGUILayout.EnumPopup(SelectedOutAnimData.data.move.moveDirection, GUILayout.Width(134));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedOutAnimData.data.move.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedOutAnimData.data.move.easeType, GUILayout.Width(105));
                        if ((SelectedOutAnimData.data.move.moveDirection == Move.MoveDirection.CustomPosition))
                        {
                            if (SelectedOutAnimData.data.move.easeType == UIAnimator.EaseType.Ease)
                            {
                                SelectedOutAnimData.data.move.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedOutAnimData.data.move.ease, GUILayout.Width(98));
                            }
                            else
                            {
                                SelectedOutAnimData.data.move.animationCurve = EditorGUILayout.CurveField(SelectedOutAnimData.data.move.animationCurve, GUILayout.Width(98));
                            }
                            QUI.Space(SPACE_2);
                            SelectedOutAnimData.data.move.customPosition = EditorGUILayout.Vector3Field("", SelectedOutAnimData.data.move.customPosition, GUILayout.Width(202));
                        }
                        else
                        {
                            if (SelectedOutAnimData.data.move.easeType == UIAnimator.EaseType.Ease)
                            {
                                SelectedOutAnimData.data.move.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedOutAnimData.data.move.ease, GUILayout.Width(307));
                            }
                            else
                            {
                                SelectedOutAnimData.data.move.animationCurve = EditorGUILayout.CurveField(SelectedOutAnimData.data.move.animationCurve, GUILayout.Width(307));
                            }
                        }
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();

        }
        void DrawRotateOut()
        {
            showRotateOut.target = SelectedOutAnimData.data.rotate.enabled;
            if (QUI.BeginFadeGroup(showRotateOut.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.OrangeLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedOutAnimData.data.rotate.startDelay = EditorGUILayout.FloatField(SelectedOutAnimData.data.rotate.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedOutAnimData.data.rotate.duration = EditorGUILayout.FloatField(SelectedOutAnimData.data.rotate.duration, GUILayout.Width(38));
                        QUI.Label("rotation", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        SelectedOutAnimData.data.rotate.rotation = EditorGUILayout.Vector3Field("", SelectedOutAnimData.data.rotate.rotation, GUILayout.Width(150));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedOutAnimData.data.rotate.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedOutAnimData.data.rotate.easeType, GUILayout.Width(105));
                        if (SelectedOutAnimData.data.rotate.easeType == UIAnimator.EaseType.Ease)
                        {
                            SelectedOutAnimData.data.rotate.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedOutAnimData.data.rotate.ease, GUILayout.Width(307));
                        }
                        else
                        {
                            SelectedOutAnimData.data.rotate.animationCurve = EditorGUILayout.CurveField(SelectedOutAnimData.data.rotate.animationCurve, GUILayout.Width(307));
                        }
                    }
                    QUI.EndHorizontal();
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawScaleOut()
        {
            showScaleOut.target = SelectedOutAnimData.data.scale.enabled;
            if (QUI.BeginFadeGroup(showScaleOut.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedOutAnimData.data.scale.startDelay = EditorGUILayout.FloatField(SelectedOutAnimData.data.scale.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedOutAnimData.data.scale.duration = EditorGUILayout.FloatField(SelectedOutAnimData.data.scale.duration, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("scale", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        SelectedOutAnimData.data.scale.scale = EditorGUILayout.Vector3Field("", SelectedOutAnimData.data.scale.scale, GUILayout.Width(150));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedOutAnimData.data.scale.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedOutAnimData.data.scale.easeType, GUILayout.Width(105));
                        if (SelectedOutAnimData.data.scale.easeType == UIAnimator.EaseType.Ease)
                        {
                            SelectedOutAnimData.data.scale.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedOutAnimData.data.scale.ease, GUILayout.Width(307));
                        }
                        else
                        {
                            SelectedOutAnimData.data.scale.animationCurve = EditorGUILayout.CurveField(SelectedOutAnimData.data.scale.animationCurve, GUILayout.Width(307));
                        }
                    }
                    QUI.EndHorizontal();
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawFadeOut()
        {
            showFadeOut.target = SelectedOutAnimData.data.fade.enabled;
            if (QUI.BeginFadeGroup(showFadeOut.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.PurpleLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedOutAnimData.data.fade.startDelay = EditorGUILayout.FloatField(SelectedOutAnimData.data.fade.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedOutAnimData.data.fade.duration = EditorGUILayout.FloatField(SelectedOutAnimData.data.fade.duration, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("alpha", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        SelectedOutAnimData.data.fade.alpha = EditorGUILayout.FloatField(SelectedOutAnimData.data.fade.alpha, GUILayout.Width(150));

                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedOutAnimData.data.fade.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedOutAnimData.data.fade.easeType, GUILayout.Width(105));
                        if (SelectedOutAnimData.data.fade.easeType == UIAnimator.EaseType.Ease)
                        {
                            SelectedOutAnimData.data.fade.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedOutAnimData.data.fade.ease, GUILayout.Width(307));
                        }
                        else
                        {
                            SelectedOutAnimData.data.fade.animationCurve = EditorGUILayout.CurveField(SelectedOutAnimData.data.fade.animationCurve, GUILayout.Width(307));
                        }
                    }
                    QUI.EndHorizontal();
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawAnimatorPresetsLoops(float width)
        {
            showPresetSettingsAnimBool.target = openedAnimatorPresetCategory != "" && selectedAnimatorPresetName != "";
            if (showPresetSettingsAnimBool.target)
            {
                QUI.Space(8 * showPresetSettingsAnimBool.faded);
                DrawAnimaorPresetsLoopAnimationsSettings(width);
            }

            QUI.Space(16);
            foreach (string category in UIAnimatorUtil.LoopDataPresetsDatabase.Keys)
            {
                QUI.BeginHorizontal(width);
                {
                    #region Button Bar
                    if (ButtonBar(category, LoopsAnimatorPresetsAnimBool[category], (width - 103 * LoopsAnimatorPresetsAnimBool[category].faded * (1 - SearchPatternAnimBool.faded))))
                    {
                        ToggleOpenAnimatorPresetCategory(category);
                    }

                    QUI.Space(-7);

                    QUI.Space(1);

                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonDelete), 100 * LoopsAnimatorPresetsAnimBool[category].faded * (1 - SearchPatternAnimBool.faded), 20))
                    {
                        if (EditorUtility.DisplayDialog("Delete the '" + category + "' category?", "Are you sure you want to proceed?\nOperation cannot be undone!", "Yes", "Cancel"))
                        {
                            UIAnimatorUtil.DeleteLoopCategory(category);
                            RefreshLoopsAnimatorPresets(true);
                            QUI.EndHorizontal();
                            continue;
                        }
                    }
                    #endregion
                }
                QUI.EndHorizontal();
                if (!LoopsAnimatorPresetsAnimBool.ContainsKey(category))
                {
                    RefreshLoopsAnimatorPresets(true);
                }
                else
                {
                    if (QUI.BeginFadeGroup(LoopsAnimatorPresetsAnimBool[category].faded))
                    {
                        DrawPresetList(category, selectedAnimatorPreset, UIAnimatorUtil.GetLoopPresetNames(category), PAGE_WIDTH - 100, "Category is empty...");
                        QUI.Space(8);
                    }
                    QUI.EndFadeGroup();
                }
                QUI.Space(2);
            }
        }
        void DrawAnimaorPresetsLoopAnimationsSettings(float width)
        {
            QUI.BeginChangeCheck();
            {
                QUI.BeginHorizontal(width);
                {
                    QUI.Space(64);
                    QUI.BeginVertical(420);
                    {
                        if (QUI.Button(DUIStyles.GetStyle(SelectedLoopData.data.move.enabled ? DUIStyles.ButtonStyle.Move : DUIStyles.ButtonStyle.MoveDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedLoopData.data.move.enabled = !SelectedLoopData.data.move.enabled; }
                        DrawMoveLoop();
                        QUI.Space(2 * showPresetSettingsAnimBool.faded);
                        if (QUI.Button(DUIStyles.GetStyle(SelectedLoopData.data.rotate.enabled ? DUIStyles.ButtonStyle.Rotate : DUIStyles.ButtonStyle.RotateDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedLoopData.data.rotate.enabled = !SelectedLoopData.data.rotate.enabled; }
                        DrawRotateLoop();
                        QUI.Space(2 * showPresetSettingsAnimBool.faded);
                        if (QUI.Button(DUIStyles.GetStyle(SelectedLoopData.data.scale.enabled ? DUIStyles.ButtonStyle.Scale : DUIStyles.ButtonStyle.ScaleDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedLoopData.data.scale.enabled = !SelectedLoopData.data.scale.enabled; }
                        DrawScaleLoop();
                        QUI.Space(2 * showPresetSettingsAnimBool.faded);
                        if (QUI.Button(DUIStyles.GetStyle(SelectedLoopData.data.fade.enabled ? DUIStyles.ButtonStyle.Fade : DUIStyles.ButtonStyle.FadeDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedLoopData.data.fade.enabled = !SelectedLoopData.data.fade.enabled; }
                        DrawFadeLoop();
                    }
                    QUI.EndVertical();
                }
                QUI.EndHorizontal();
            }
            if (QUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(SelectedLoopData);
            }
            QUI.Space(16 * showPresetSettingsAnimBool.faded);
        }
        void DrawMoveLoop()
        {
            showMoveLoop.target = SelectedLoopData.data.move.enabled;
            if (QUI.BeginFadeGroup(showMoveLoop.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedLoopData.data.move.startDelay = EditorGUILayout.FloatField(SelectedLoopData.data.move.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedLoopData.data.move.duration = EditorGUILayout.FloatField(SelectedLoopData.data.move.duration, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        SelectedLoopData.data.move.loops = EditorGUILayout.IntField(SelectedLoopData.data.move.loops, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        SelectedLoopData.data.move.loopType = (Loop.LoopType)EditorGUILayout.EnumPopup(SelectedLoopData.data.move.loopType, GUILayout.Width(56));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("movement", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
                        SelectedLoopData.data.move.movement = EditorGUILayout.Vector3Field("", SelectedLoopData.data.move.movement, GUILayout.Width(348));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedLoopData.data.move.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedLoopData.data.move.easeType, GUILayout.Width(105));
                        if (SelectedLoopData.data.move.easeType == UIAnimator.EaseType.Ease)
                        {
                            SelectedLoopData.data.move.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedLoopData.data.move.ease, GUILayout.Width(307));
                        }
                        else
                        {
                            SelectedLoopData.data.move.animationCurve = EditorGUILayout.CurveField(SelectedLoopData.data.move.animationCurve, GUILayout.Width(307));
                        }
                    }
                    QUI.EndHorizontal();
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawRotateLoop()
        {
            showRotateLoop.target = SelectedLoopData.data.rotate.enabled;
            if (QUI.BeginFadeGroup(showRotateLoop.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.OrangeLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedLoopData.data.rotate.startDelay = EditorGUILayout.FloatField(SelectedLoopData.data.rotate.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedLoopData.data.rotate.duration = EditorGUILayout.FloatField(SelectedLoopData.data.rotate.duration, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        SelectedLoopData.data.rotate.loops = EditorGUILayout.IntField(SelectedLoopData.data.rotate.loops, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        SelectedLoopData.data.rotate.loopType = (Loop.LoopType)EditorGUILayout.EnumPopup(SelectedLoopData.data.rotate.loopType, GUILayout.Width(56));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("rotation", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
                        SelectedLoopData.data.rotate.rotation = EditorGUILayout.Vector3Field("", SelectedLoopData.data.rotate.rotation, GUILayout.Width(348));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedLoopData.data.rotate.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedLoopData.data.rotate.easeType, GUILayout.Width(105));
                        if (SelectedLoopData.data.rotate.easeType == UIAnimator.EaseType.Ease)
                        {
                            SelectedLoopData.data.rotate.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedLoopData.data.rotate.ease, GUILayout.Width(307));
                        }
                        else
                        {
                            SelectedLoopData.data.rotate.animationCurve = EditorGUILayout.CurveField(SelectedLoopData.data.rotate.animationCurve, GUILayout.Width(307));
                        }
                    }
                    QUI.EndHorizontal();
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawScaleLoop()
        {
            showScaleLoop.target = SelectedLoopData.data.scale.enabled;
            if (QUI.BeginFadeGroup(showScaleLoop.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedLoopData.data.scale.startDelay = EditorGUILayout.FloatField(SelectedLoopData.data.scale.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedLoopData.data.scale.duration = EditorGUILayout.FloatField(SelectedLoopData.data.scale.duration, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        SelectedLoopData.data.scale.loops = EditorGUILayout.IntField(SelectedLoopData.data.scale.loops, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        SelectedLoopData.data.scale.loopType = (Loop.LoopType)EditorGUILayout.EnumPopup(SelectedLoopData.data.scale.loopType, GUILayout.Width(56));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("min", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 24);
                        SelectedLoopData.data.scale.min = EditorGUILayout.Vector3Field("", SelectedLoopData.data.scale.min, GUILayout.Width(178));
                        QUI.Space(SPACE_4);
                        QUI.Label("max", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 28);
                        SelectedLoopData.data.scale.max = EditorGUILayout.Vector3Field("", SelectedLoopData.data.scale.max, GUILayout.Width(168));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedLoopData.data.scale.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedLoopData.data.scale.easeType, GUILayout.Width(105));
                        if (SelectedLoopData.data.scale.easeType == UIAnimator.EaseType.Ease)
                        {
                            SelectedLoopData.data.scale.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedLoopData.data.scale.ease, GUILayout.Width(307));
                        }
                        else
                        {
                            SelectedLoopData.data.scale.animationCurve = EditorGUILayout.CurveField(SelectedLoopData.data.scale.animationCurve, GUILayout.Width(307));
                        }
                    }
                    QUI.EndHorizontal();
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();

        }
        void DrawFadeLoop()
        {
            showFadeLoop.target = SelectedLoopData.data.fade.enabled;
            if (QUI.BeginFadeGroup(showFadeLoop.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.PurpleLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedLoopData.data.fade.startDelay = EditorGUILayout.FloatField(SelectedLoopData.data.fade.startDelay, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        SelectedLoopData.data.fade.duration = EditorGUILayout.FloatField(SelectedLoopData.data.fade.duration, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        SelectedLoopData.data.fade.loops = EditorGUILayout.IntField(SelectedLoopData.data.fade.loops, GUILayout.Width(38));
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        SelectedLoopData.data.fade.loopType = (Loop.LoopType)EditorGUILayout.EnumPopup(SelectedLoopData.data.fade.loopType, GUILayout.Width(56));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("min", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 24);
                        SelectedLoopData.data.fade.min = EditorGUILayout.FloatField(SelectedLoopData.data.fade.min, GUILayout.Width(36));
                        QUI.Space(SPACE_4);
                        QUI.Label("max", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 28);
                        SelectedLoopData.data.fade.max = EditorGUILayout.FloatField(SelectedLoopData.data.fade.max, GUILayout.Width(36));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        SelectedLoopData.data.fade.easeType = (UIAnimator.EaseType)EditorGUILayout.EnumPopup(SelectedLoopData.data.fade.easeType, GUILayout.Width(105));
                        if (SelectedLoopData.data.fade.easeType == UIAnimator.EaseType.Ease)
                        {
                            SelectedLoopData.data.fade.ease = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(SelectedLoopData.data.fade.ease, GUILayout.Width(307));
                        }
                        else
                        {
                            SelectedLoopData.data.fade.animationCurve = EditorGUILayout.CurveField(SelectedLoopData.data.fade.animationCurve, GUILayout.Width(307));
                        }
                    }
                    QUI.EndHorizontal();
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawAnimatorPresetsPunches(float width)
        {
            showPresetSettingsAnimBool.target = openedAnimatorPresetCategory != "" && selectedAnimatorPresetName != "";
            if (showPresetSettingsAnimBool.target)
            {
                QUI.Space(8 * showPresetSettingsAnimBool.faded);
                DrawAnimaorPresetsPunchAnimationsSettings(width);
            }

            QUI.Space(16);
            foreach (string category in UIAnimatorUtil.PunchDataPresetsDatabase.Keys)
            {
                QUI.BeginHorizontal(width);
                {
                    #region Button Bar
                    if (ButtonBar(category, PunchesAnimatorPresetsAnimBool[category], (width - 103 * PunchesAnimatorPresetsAnimBool[category].faded * (1 - SearchPatternAnimBool.faded))))
                    {
                        ToggleOpenAnimatorPresetCategory(category);
                    }

                    QUI.Space(-7);

                    QUI.Space(1);

                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonDelete), 100 * PunchesAnimatorPresetsAnimBool[category].faded * (1 - SearchPatternAnimBool.faded), 20))
                    {
                        if (EditorUtility.DisplayDialog("Delete the '" + category + "' category?", "Are you sure you want to proceed?\nOperation cannot be undone!", "Yes", "Cancel"))
                        {
                            UIAnimatorUtil.DeletePunchCategory(category);
                            RefreshPunchesAnimatorPresets(true);
                            QUI.EndHorizontal();
                            continue;
                        }
                    }
                    #endregion
                }
                QUI.EndHorizontal();
                if (!PunchesAnimatorPresetsAnimBool.ContainsKey(category))
                {
                    RefreshPunchesAnimatorPresets(true);
                }
                else
                {
                    if (QUI.BeginFadeGroup(PunchesAnimatorPresetsAnimBool[category].faded))
                    {
                        DrawPresetList(category, selectedAnimatorPreset, UIAnimatorUtil.GetPunchPresetNames(category), PAGE_WIDTH - 100, "Category is empty...");
                        QUI.Space(8);
                    }
                    QUI.EndFadeGroup();
                }
                QUI.Space(2);
            }
        }
        void DrawAnimaorPresetsPunchAnimationsSettings(float width)
        {
            QUI.BeginChangeCheck();
            {
                QUI.BeginHorizontal(width);
                {
                    QUI.Space(64);
                    QUI.BeginVertical(420);
                    {
                        if (QUI.Button(DUIStyles.GetStyle(SelectedPunchData.data.move.enabled ? DUIStyles.ButtonStyle.Move : DUIStyles.ButtonStyle.MoveDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedPunchData.data.move.enabled = !SelectedPunchData.data.move.enabled; }
                        DrawMovePunch();
                        QUI.Space(2 * showPresetSettingsAnimBool.faded);
                        if (QUI.Button(DUIStyles.GetStyle(SelectedPunchData.data.rotate.enabled ? DUIStyles.ButtonStyle.Rotate : DUIStyles.ButtonStyle.RotateDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedPunchData.data.rotate.enabled = !SelectedPunchData.data.rotate.enabled; }
                        DrawRotatePunch();
                        QUI.Space(2 * showPresetSettingsAnimBool.faded);
                        if (QUI.Button(DUIStyles.GetStyle(SelectedPunchData.data.scale.enabled ? DUIStyles.ButtonStyle.Scale : DUIStyles.ButtonStyle.ScaleDisabled), 420, 18 * showPresetSettingsAnimBool.faded)) { SelectedPunchData.data.scale.enabled = !SelectedPunchData.data.scale.enabled; }
                        DrawScalePunch();
                    }
                    QUI.EndVertical();
                }
                QUI.EndHorizontal();
            }
            if(QUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(SelectedPunchData);
            }
            QUI.Space(16 * showPresetSettingsAnimBool.faded);
        }
        void DrawMovePunch()
        {
            showMovePunch.target = SelectedPunchData.data.move.enabled;
            if (QUI.BeginFadeGroup(showMovePunch.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedPunchData.data.move.startDelay = EditorGUILayout.FloatField(SelectedPunchData.data.move.startDelay, GUILayout.Width(78));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 50);
                        SelectedPunchData.data.move.duration = EditorGUILayout.FloatField(SelectedPunchData.data.move.duration, GUILayout.Width(78));
                        QUI.Space(SPACE_4);
                        QUI.Label("vibrato", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 42);
                        SelectedPunchData.data.move.vibrato = EditorGUILayout.IntField(SelectedPunchData.data.move.vibrato, GUILayout.Width(78));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("punch", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 40);
                        SelectedPunchData.data.move.punch = EditorGUILayout.Vector3Field("", SelectedPunchData.data.move.punch, GUILayout.Width(240));
                        QUI.Space(SPACE_4);
                        QUI.Label("elasticity", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 54);
                        SelectedPunchData.data.move.elasticity = EditorGUILayout.FloatField(SelectedPunchData.data.move.elasticity, GUILayout.Width(66));
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawRotatePunch()
        {
            showRotatePunch.target = SelectedPunchData.data.rotate.enabled;
            if (QUI.BeginFadeGroup(showRotatePunch.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.OrangeLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedPunchData.data.rotate.startDelay = EditorGUILayout.FloatField(SelectedPunchData.data.rotate.startDelay, GUILayout.Width(78));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 50);
                        SelectedPunchData.data.rotate.duration = EditorGUILayout.FloatField(SelectedPunchData.data.rotate.duration, GUILayout.Width(78));
                        QUI.Space(SPACE_4);
                        QUI.Label("vibrato", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 42);
                        SelectedPunchData.data.rotate.vibrato = EditorGUILayout.IntField(SelectedPunchData.data.rotate.vibrato, GUILayout.Width(78));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("punch", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 40);
                        SelectedPunchData.data.rotate.punch = EditorGUILayout.Vector3Field("", SelectedPunchData.data.rotate.punch, GUILayout.Width(240));
                        QUI.Space(SPACE_4);
                        QUI.Label("elasticity", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 54);
                        SelectedPunchData.data.rotate.elasticity = EditorGUILayout.FloatField(SelectedPunchData.data.rotate.elasticity, GUILayout.Width(66));
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawScalePunch()
        {
            showScalePunch.target = SelectedPunchData.data.scale.enabled;
            if (QUI.BeginFadeGroup(showScalePunch.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                QUI.BeginVertical(420);
                {
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        SelectedPunchData.data.scale.startDelay = EditorGUILayout.FloatField(SelectedPunchData.data.scale.startDelay, GUILayout.Width(78));
                        QUI.Space(SPACE_4);
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 50);
                        SelectedPunchData.data.scale.duration = EditorGUILayout.FloatField(SelectedPunchData.data.scale.duration, GUILayout.Width(78));
                        QUI.Space(SPACE_4);
                        QUI.Label("vibrato", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 42);
                        SelectedPunchData.data.scale.vibrato = EditorGUILayout.IntField(SelectedPunchData.data.scale.vibrato, GUILayout.Width(78));
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(420);
                    {
                        QUI.Label("punch", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 40);
                        SelectedPunchData.data.scale.punch = EditorGUILayout.Vector3Field("", SelectedPunchData.data.scale.punch, GUILayout.Width(240));
                        QUI.Space(SPACE_4);
                        QUI.Label("elasticity", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 54);
                        SelectedPunchData.data.scale.elasticity = EditorGUILayout.FloatField(SelectedPunchData.data.scale.elasticity, GUILayout.Width(66));
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void ToggleOpenAnimatorPresetCategory(string presetCategory)
        {
            openedAnimatorPresetCategory = openedAnimatorPresetCategory == presetCategory ? "" : presetCategory;
            selectedAnimatorPresetName = "";
            switch (selectedAnimatorPreset)
            {
                case AnimatorPreset.InAnimations: foreach (var category in InAnimationsAnimatorPresetsAnimBool.Keys) { InAnimationsAnimatorPresetsAnimBool[category].target = category.Equals(openedAnimatorPresetCategory); } break;
                case AnimatorPreset.OutAnimations: foreach (var category in OutAnimationsAnimatorPresetsAnimBool.Keys) { OutAnimationsAnimatorPresetsAnimBool[category].target = category.Equals(openedAnimatorPresetCategory); } break;
                case AnimatorPreset.Loops: foreach (var category in LoopsAnimatorPresetsAnimBool.Keys) { LoopsAnimatorPresetsAnimBool[category].target = category.Equals(openedAnimatorPresetCategory); } break;
                case AnimatorPreset.Punches: foreach (var category in PunchesAnimatorPresetsAnimBool.Keys) { PunchesAnimatorPresetsAnimBool[category].target = category.Equals(openedAnimatorPresetCategory); } break;
            }
        }
        void SelectedAnimatorPreset(string presetName)
        {
            selectedAnimatorPresetName = presetName;
            switch (selectedAnimatorPreset)
            {
                case AnimatorPreset.InAnimations: SelectedInAnimData = UIAnimatorUtil.GetInAnimData(openedAnimatorPresetCategory, presetName); break;
                case AnimatorPreset.OutAnimations: SelectedOutAnimData = UIAnimatorUtil.GetOutAnimData(openedAnimatorPresetCategory, presetName); break;
                case AnimatorPreset.Loops: SelectedLoopData = UIAnimatorUtil.GetLoopData(openedAnimatorPresetCategory, presetName); break;
                case AnimatorPreset.Punches: SelectedPunchData = UIAnimatorUtil.GetPunchData(openedAnimatorPresetCategory, presetName); break;
            }
        }
        void ResetSelectedAnimatorPreset()
        {
            openedAnimatorPresetCategory = "";
            selectedAnimatorPresetName = "";
            showPresetSettingsAnimBool.value = false;
        }
    }
}
