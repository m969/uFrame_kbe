// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using UnityEngine;

namespace DoozyUI
{
    public partial class ControlPanelWindow : QWindow
    {
        void DrawHelp()
        {
            QUI.DrawTexture(DUIResources.headerHelp.texture, 552, 64);
            float sectionWidth = PAGE_WIDTH - SIDE_BAR_SHADOW_WIDTH * 2;
            float leftColumnWidth = 242;
            float rightColumnWidth = 310;
            QUI.BeginHorizontal(sectionWidth);
            {
                QUI.BeginVertical(leftColumnWidth);
                {
                    DrawHelpButtons(leftColumnWidth);
                    QUI.Space(8);
                    QUI.FlexibleSpace();
                }
                QUI.EndVertical();
                QUI.Space(SPACE_16);
                QUI.BeginVertical(rightColumnWidth);
                {
                    DrawFAQ(rightColumnWidth);
                    QUI.FlexibleSpace();
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
        }

        void DrawHelpButtons(float width)
        {
            QUI.DrawTexture(DUIResources.pageHelpSeparatorResources.texture, 242, 16);
            QUI.Space(2);
            DrawControlPanelExternalLinkButton(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageHelpForum), "https://goo.gl/vGMBrg");
            QUI.Space(2);
            DrawControlPanelExternalLinkButton(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageHelpManual), "https://goo.gl/LSHMRj");
            QUI.Space(2);
            DrawControlPanelExternalLinkButton(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageHelpApi), "https://goo.gl/Ybsf99");
            QUI.Space(2);
            QUI.DrawTexture(DUIResources.pageHelpButtonMail.texture, 242, 40);
        }

        void DrawFAQ(float width)
        {
            QUI.DrawTexture(DUIResources.pageHelpSeparatorFAQ.texture, 294, 16);
            DrawArticle("The FAQ section is currently under construction!",
                        "Coming Soon...",
                        width - 16);
        }

    }
}