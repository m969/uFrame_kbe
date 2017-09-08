// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using UnityEditor;

namespace DoozyUI
{
    public partial class ControlPanelWindow : QWindow
    {
        void DrawUICanvases()
        {
            QUI.DrawTexture(DUIResources.headerUICanvasesDatabase.texture, 552, 64);
            float sectionWidth = PAGE_WIDTH - SIDE_BAR_SHADOW_WIDTH * 2;
            QUI.BeginHorizontal(sectionWidth);
            {
                QUI.Space(-22);
                QUI.BeginChangeCheck();
                {
                    DrawNamesList(DUI.CanvasNamesDatabase.data, sectionWidth + 22 + 12 - 100, "Add a new canvas name to get started...");
                }
                if(QUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(DUI.CanvasNamesDatabase);
                }
                QUI.Space(-12);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonSortAtoZ), 100, 20))
                {
                    DUI.RefreshCanvasNamesDatabase();
                }
            }
            QUI.EndHorizontal();
            QUI.Space(16);
            if (DUI.CanvasNamesDatabase.Count == 0)
            {
                QUI.DrawTexture(DUIResources.pageImageEmptyDatabase.texture, 552, 256);
                return;
            }
        }
    }
}