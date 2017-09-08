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
        void DrawUIButtonsDatabase()
        {
            QUI.DrawTexture(DUIResources.headerUIButtonsDatabase.texture, 552, 64);
            float sectionWidth = PAGE_WIDTH - SIDE_BAR_SHADOW_WIDTH * 2;
            QUI.BeginHorizontal(sectionWidth);
            {
                #region New Category
                if (!NewCategoryAnimBool.value)
                {
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonNewCategory), 100 * (1 - SearchPatternAnimBool.faded), 20))
                    {
                        NewCategoryName = "";
                        NewCategoryAnimBool.target = true;
                        SearchPatternAnimBool.target = false;
                        foreach (string category in UIButtonsDatabaseAnimBool.Keys)
                        {
                            UIButtonsDatabaseAnimBool[category].target = false;
                        }
                    }
                }
                else
                {
                    SearchPatternAnimBool.target = false;
                    QUI.DrawTexture(DUIResources.pageButtonNewCategory.active, 100, 20);
                    QUI.Space(80);
                    SaveColors();
                    QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                    NewCategoryName = EditorGUILayout.TextField(NewCategoryName, GUILayout.Width((sectionWidth - 149) * NewCategoryAnimBool.faded));
                    RestoreColors();
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonOk), 20, 20))
                    {
                        if (string.IsNullOrEmpty(NewCategoryName))
                        {
                            EditorUtility.DisplayDialog("Info", "Cannot create an unnamed category. Try again.", "Ok");
                        }
                        else if (DUI.UIButtonCategoryExists(NewCategoryName))
                        {
                            EditorUtility.DisplayDialog("Info", "A category named '" + NewCategoryName + "' already exists in the database. Try again.", "Ok");
                        }
                        else
                        {
                            DUI.CreateUIButtonsCategory(NewCategoryName);
                            NewCategoryAnimBool.target = false;
                            RefreshUIButtonsDatabase(true);
                        }
                    }
                    QUI.Space(1);
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonCancel), 20, 20))
                    {
                        NewCategoryName = "";
                        NewCategoryAnimBool.target = false;
                    }
                }
                #endregion
                QUI.Space(342 * (1 - NewCategoryAnimBool.faded) * (1 - SearchPatternAnimBool.faded));
                #region Search
                if (!SearchPatternAnimBool.value)
                {
                    QUI.FlexibleSpace();
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonSearch), 100 * (1 - NewCategoryAnimBool.faded), 20))
                    {
                        SearchPattern = string.Empty;
                        SearchPatternAnimBool.target = true;
                        NewCategoryAnimBool.target = false;
                        foreach (string category in UIButtonsDatabaseAnimBool.Keys)
                        {
                            UIButtonsDatabaseAnimBool[category].target = true;
                        }
                    }
                }
                else
                {
                    NewCategoryAnimBool.target = false;
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonClearSearch), 100 * SearchPatternAnimBool.faded, 20))
                    {
                        SearchPattern = string.Empty;
                        SearchPatternAnimBool.target = false;
                        foreach (string category in UIButtonsDatabaseAnimBool.Keys)
                        {
                            UIButtonsDatabaseAnimBool[category].target = false;
                        }
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
            if (DUI.UIButtonsDatabase.Keys.Count == 0)
            {
                QUI.DrawTexture(DUIResources.pageImageEmptyDatabase.texture, 552, 256);
                return;
            }

            foreach (string category in DUI.UIButtonsDatabase.Keys)
            {
                QUI.BeginHorizontal(sectionWidth);
                {
                    #region Button Bar
                    if (ButtonBar(category + (SearchPatternAnimBool.target ? "*" : ""), UIButtonsDatabaseAnimBool[category], (sectionWidth - 203 * UIButtonsDatabaseAnimBool[category].faded * (1 - SearchPatternAnimBool.faded))))
                    {
                        UIButtonsDatabaseAnimBool[category].target = !UIButtonsDatabaseAnimBool[category].target;
                    }

                    QUI.Space(-7);

                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonSortAtoZ), 100 * UIButtonsDatabaseAnimBool[category].faded * (1 - SearchPatternAnimBool.faded), 20))
                    {
                        DUI.UIButtonsDatabase[category].RemoveEmpty();
                        DUI.UIButtonsDatabase[category].Sort();
                    }

                    QUI.Space(1);

                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonDelete), 100 * UIButtonsDatabaseAnimBool[category].faded * (1 - SearchPatternAnimBool.faded), 20))
                    {
                        if (EditorUtility.DisplayDialog("Delete the '" + category + "' category?", "Are you sure you want to proceed?\nOperation cannot be undone!", "Yes", "Cancel"))
                        {
                            DUI.DeleteUIButtonsCategory(category);
                            RefreshUIButtonsDatabase(true);
                            QUI.EndHorizontal();
                            continue;
                        }
                    }
                    #endregion
                }
                QUI.EndHorizontal();
                if (!UIButtonsDatabaseAnimBool.ContainsKey(category))
                {
                    RefreshUIButtonsDatabase(true);
                }
                else
                {
                    if (QUI.BeginFadeGroup(UIButtonsDatabaseAnimBool[category].faded))
                    {
                        QUI.BeginChangeCheck();
                        {
                            DrawNamesList(DUI.UIButtonsDatabase[category].data, PAGE_WIDTH - 200, "Category is empty...");
                        }
                        if (QUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(DUI.UIButtonsDatabase[category]);
                        }
                        QUI.Space(8);
                    }
                    QUI.EndFadeGroup();
                }
                QUI.Space(2);
            }
        }
    }
}
