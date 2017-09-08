// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace DoozyUI
{
    public partial class ControlPanelWindow : QWindow
    {
        void DrawUISounds()
        {
            QUI.DrawTexture(DUIResources.headerUISoundsDatabase.texture, 552, 64);
            float sectionWidth = PAGE_WIDTH - SIDE_BAR_SHADOW_WIDTH * 2;
            QUI.BeginHorizontal(sectionWidth);
            {
                #region New UISound
                if (!NewUISoundAnimBool.value)
                {
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonNewUISound), 100 * (1 - SearchPatternAnimBool.faded), 20))
                    {
                        NewUISoundName = "";
                        NewUISoundAnimBool.target = true;
                        SearchPatternAnimBool.target = false;
                    }
                }
                else
                {
                    SearchPatternAnimBool.target = false;
                    QUI.DrawTexture(DUIResources.pageButtonNewUISound.active, 100, 20);
                    QUI.Space(80);
                    SaveColors();
                    QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                    NewUISoundName = EditorGUILayout.TextField(NewUISoundName, GUILayout.Width((sectionWidth - 149) * NewUISoundAnimBool.faded));
                    RestoreColors();
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonOk), 20, 20))
                    {
                        if (string.IsNullOrEmpty(NewUISoundName))
                        {
                            EditorUtility.DisplayDialog("Info", "Cannot create an unnamed ui sound. Try again.", "Ok");
                        }
                        else if (DUI.UISoundNameExists(NewUISoundName))
                        {
                            EditorUtility.DisplayDialog("Info", "An ui sound named '" + NewUISoundName + "' already exists in the database. Try again.", "Ok");
                        }
                        else
                        {
                            DUI.CreateUISound(NewUISoundName, soundTypeFilter);
                            NewUISoundAnimBool.target = false;
                            RefreshUISoundsDatabase(true);
                        }
                    }
                    QUI.Space(1);
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonCancel), 20, 20))
                    {
                        NewUISoundName = "";
                        NewUISoundAnimBool.target = false;
                    }
                }
                #endregion
                QUI.Space(342 * (1 - NewUISoundAnimBool.faded) * (1 - SearchPatternAnimBool.faded));
                #region Search
                if (!SearchPatternAnimBool.value)
                {
                    QUI.FlexibleSpace();
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonSearch), 100 * (1 - NewUISoundAnimBool.faded), 20))
                    {
                        SearchPattern = string.Empty;
                        SearchPatternAnimBool.target = true;
                        NewUISoundAnimBool.target = false;
                    }
                }
                else
                {
                    NewUISoundAnimBool.target = false;
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonClearSearch), 100 * SearchPatternAnimBool.faded, 20))
                    {
                        SearchPattern = string.Empty;
                        SearchPatternAnimBool.target = false;
                    }
                    GUILayout.Space(1);
                    SaveColors();
                    QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                    SearchPattern = EditorGUILayout.TextField(SearchPattern, GUILayout.Width((sectionWidth - 210) * SearchPatternAnimBool.faded));
                    RestoreColors();
                    QUI.DrawTexture(DUIResources.pageButtonSearch.active, 100, 20);
                }
                #endregion
            }
            QUI.EndHorizontal();
            QUI.Space(16);
            #region Filter
            QUI.BeginHorizontal(sectionWidth);
            {
                QUI.Space(8);
                QUI.FlexibleSpace();
                QUI.DrawTexture(DUIResources.pageButtonFilterHeader.texture, 302, 10);
                QUI.Space(302);
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();
            QUI.Space(10);
            QUI.BeginHorizontal(sectionWidth);
            {
                QUI.Space(-2);
                QUI.FlexibleSpace();
                if (soundTypeFilter == SoundType.All)
                {
                    QUI.DrawTexture(DUIResources.pageButtonFilterAll.active, 100, 20);
                    QUI.Space(80);
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonFilterAll), 100, 20)) { soundTypeFilter = SoundType.All; }
                }
                QUI.Space(1);
                if (soundTypeFilter == SoundType.UIButtons)
                {
                    QUI.DrawTexture(DUIResources.pageButtonFilterUIButton.active, 100, 20);
                    QUI.Space(80);
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonFilterUIButton), 100, 20)) { soundTypeFilter = SoundType.UIButtons; }
                }
                QUI.Space(1);
                if (soundTypeFilter == SoundType.UIElements)
                {
                    QUI.DrawTexture(DUIResources.pageButtonFilterUIElement.active, 100, 20);
                    QUI.Space(80);
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonFilterUIElement), 100, 20)) { soundTypeFilter = SoundType.UIElements; }
                }
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();
            #endregion
            QUI.Space(16);
            if (DUI.UISoundsDatabase.Count == 0)
            {
                QUI.DrawTexture(DUIResources.pageImageEmptyDatabase.texture, 552, 256);
                return;
            }
            QUI.DrawTexture(DUIResources.pageUISoundsTableTop.texture, 552, 12);
            for (int i = 0; i < DUI.UISoundsDatabase.Count; i++)
            {
                if (DUI.UISoundsDatabase[i] == null) { continue; }
                if (soundTypeFilter == SoundType.UIButtons && DUI.UISoundsDatabase[i].soundType == SoundType.UIElements) { continue; }
                if (soundTypeFilter == SoundType.UIElements && DUI.UISoundsDatabase[i].soundType == SoundType.UIButtons) { continue; }

                QUI.BeginHorizontal(sectionWidth);
                {
                    if (SearchPatternAnimBool.target)//a search pattern has been entered in the search box
                    {
                        try
                        {
                            if (!Regex.IsMatch(DUI.UISoundsDatabase[i].soundName, SearchPattern, RegexOptions.IgnoreCase))
                            {
                                QUI.EndHorizontal();
                                continue; //this does not match the search pattern --> we do not show this name it
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    DUI.UISoundsDatabase[i].soundType = (SoundType)EditorGUILayout.EnumPopup(DUI.UISoundsDatabase[i].soundType, GUILayout.Width(80));
                    if (SearchPatternAnimBool.target)
                    {
                        QUI.Label(DUI.UISoundsDatabase[i].soundName, DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormalItalic), 174);
                    }
                    else
                    {
                        QUI.Label(DUI.UISoundsDatabase[i].soundName, DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), 174);
                    }
#if dUI_MasterAudio
                    QUI.Label("Controlled by MasterAudio", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), 140);
#else
                    QUI.BeginChangeCheck();
                    {
                        DUI.UISoundsDatabase[i].audioClip = (AudioClip)EditorGUILayout.ObjectField("", DUI.UISoundsDatabase[i].audioClip, typeof(AudioClip), false, GUILayout.Width(140));
                    }
                    if(QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(DUI.DUISettings, "Updated UISound");
                        EditorUtility.SetDirty(DUI.UISoundsDatabase[i]);
                        AssetDatabase.SaveAssets();
                    }
#endif
                    QUI.Space(50 * SearchPatternAnimBool.faded);
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonPlay), 20, 20))
                    {
                        DUIUtils.PreviewSound(DUI.UISoundsDatabase[i].soundName);
                    }
                    QUI.Space(1);
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonStop), 20, 20))
                    {
                        DUIUtils.StopSoundPreview(DUI.UISoundsDatabase[i].soundName);
                    }
                    QUI.Space(1);
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonDelete), 100 * (1 - SearchPatternAnimBool.faded), 20))
                    {
                        if (EditorUtility.DisplayDialog("Delete the '" + DUI.UISoundsDatabase[i].soundName + "' ui sound?", "Are you sure you want to proceed?\nOperation cannot be undone!", "Yes", "Cancel"))
                        {
                            DUI.DeleteUISound(DUI.UISoundsDatabase[i].soundName);
                            RefreshUISoundsDatabase(true);
                            QUI.EndHorizontal();
                            continue;
                        }
                    }
                }
                QUI.EndHorizontal();
                QUI.Space(2);
            }
            QUI.Space(-2);
            QUI.DrawTexture(DUIResources.pageUISoundsTableBottom.texture, 552, 12);
        }
    }
}
