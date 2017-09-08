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
        void DrawUIElementsDatabase()
        {
            QUI.DrawTexture(DUIResources.headerUIElementsDatabase.texture, 552, 64);
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
                        foreach (string category in UIElementsDatabaseAnimBool.Keys)
                        {
                            UIElementsDatabaseAnimBool[category].target = false;
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
                        else if (DUI.UIElementCategoryExists(NewCategoryName))
                        {
                            EditorUtility.DisplayDialog("Info", "A category named '" + NewCategoryName + "' already exists in the database. Try again.", "Ok");
                        }
                        else
                        {
                            DUI.CreateUIElementsCategory(NewCategoryName);
                            NewCategoryAnimBool.target = false;
                            RefreshUIElementsDatabase(true);
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
                        foreach (string category in UIElementsDatabaseAnimBool.Keys)
                        {
                            UIElementsDatabaseAnimBool[category].target = true;
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
                        foreach (string category in UIElementsDatabaseAnimBool.Keys)
                        {
                            UIElementsDatabaseAnimBool[category].target = false;
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
            if (UIElementsDatabaseAnimBool.Keys.Count == 0)
            {
                QUI.DrawTexture(DUIResources.pageImageEmptyDatabase.texture, 552, 256);
                QUI.BeginHorizontal(sectionWidth);
                {
                    QUI.FlexibleSpace();
                    QUI.Label("Add a new category to get started...", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormalItalic));
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                return;
            }

            foreach (string category in DUI.UIElementsDatabase.Keys)
            {
                QUI.BeginHorizontal(sectionWidth);
                {
                    #region Button Bar
                    if (ButtonBar(category + (SearchPatternAnimBool.target ? "*" : ""), UIElementsDatabaseAnimBool[category], (sectionWidth - 203 * UIElementsDatabaseAnimBool[category].faded * (1 - SearchPatternAnimBool.faded))))
                    {
                        UIElementsDatabaseAnimBool[category].target = !UIElementsDatabaseAnimBool[category].target;
                    }

                    QUI.Space(-7);

                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonSortAtoZ), 100 * UIElementsDatabaseAnimBool[category].faded * (1 - SearchPatternAnimBool.faded), 20))
                    {
                        DUI.UIElementsDatabase[category].RemoveEmpty();
                        DUI.UIElementsDatabase[category].Sort();
                    }

                    QUI.Space(1);

                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonDelete), 100 * UIElementsDatabaseAnimBool[category].faded * (1 - SearchPatternAnimBool.faded), 20))
                    {
                        if (EditorUtility.DisplayDialog("Delete the '" + category + "' category?", "Are you sure you want to proceed?\nOperation cannot be undone!", "Yes", "Cancel"))
                        {
                            DUI.DeleteUIElementsCategory(category);
                            RefreshUIElementsDatabase(true);
                            QUI.EndHorizontal();
                            continue;
                        }
                    }
                    #endregion
                }
                QUI.EndHorizontal();
                if (!UIElementsDatabaseAnimBool.ContainsKey(category))
                {
                    RefreshUIElementsDatabase(true);
                }
                else
                {
                    if (QUI.BeginFadeGroup(UIElementsDatabaseAnimBool[category].faded))
                    {
                        QUI.BeginChangeCheck();
                        {
                            DrawNamesList(DUI.UIElementsDatabase[category].data, PAGE_WIDTH - 200, "Category is empty...");
                        }
                        if (QUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(DUI.UIElementsDatabase[category]);
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
