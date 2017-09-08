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
        void DrawControlPanel()
        {
            QUI.DrawTexture(DUIResources.headerControlPanel.texture, 552, 64);
            float sectionWidth = PAGE_WIDTH - SIDE_BAR_SHADOW_WIDTH * 2;
            float leftColumnWidth = 242;
            float rightColumnWidth = 310;
            QUI.BeginHorizontal(sectionWidth);
            {
                QUI.BeginVertical(leftColumnWidth);
                {
                    DrawControlPanelSpportFor3RdPartyAssets(leftColumnWidth);
                    QUI.Space(8);
                    DrawControlPanelDoozyModules(leftColumnWidth);
                    QUI.Space(8);
                    DrawControlPanelRecomendedPlugins(leftColumnWidth);
                    QUI.FlexibleSpace();
                }
                QUI.EndVertical();
                QUI.Space(SPACE_16);
                QUI.BeginVertical(rightColumnWidth);
                {
                    DrawControlPanelNews(rightColumnWidth);
                    QUI.FlexibleSpace();
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
        }
        void DrawControlPanelSpportFor3RdPartyAssets(float width)
        {
            QUI.DrawTexture(DUIResources.pageControlPanelSeparatorSupport.texture, 242, 16);
            QUI.Space(2);
            #region Playmaker
            if (EditorApplication.isCompiling)
            {
                QUI.DrawTexture(DUIResources.pageControlPanelButtonMaskEditorIsCompiling.texture, 242, 40);
            }
            else if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                QUI.DrawTexture(DUIResources.pageControlPanelButtonMaskEditorInPlayMode.texture, 242, 40);
            }
            else
            {
                QUI.BeginHorizontal(width);
                {
#if dUI_PlayMaker
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonPlaymakerEnabled), 200, 40))
                    {
                        NotificationWindow.YesNo("Disable support for PlayMaker?",
                                                 "This will remove '" + DUI.SYMBOL_PLAYMAKER + "' from Scripting Define Symbols in Player Settings.",
                                                 () => { QUtils.RemoveScriptingDefineSymbol(DUI.SYMBOL_PLAYMAKER); },
                                                 null);
                    }
#else

                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonPlaymakerDisabled), 200, 40))
                    {
                        NotificationWindow.YesNo("Enable support for PlayMaker?",
                                                 "Enable this only if you have PlayMaker already installed." +
                                                 "\n\n" +
                                                 "This will add '" + DUI.SYMBOL_PLAYMAKER + "' to Scripting Define Symbols in Player Settings.",
                                                 () => { QUtils.AddScriptingDefineSymbol(DUI.SYMBOL_PLAYMAKER); },
                                                 null);
                    }
#endif
                    QUI.Space(2);
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonExternalLink), 40, 40))
                    {
                        Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/368");
                    }
                }
                QUI.EndHorizontal();
                #endregion
            }
            QUI.Space(2);
            #region Master Audio
            if (EditorApplication.isCompiling)
            {
                QUI.DrawTexture(DUIResources.pageControlPanelButtonMaskEditorIsCompiling.texture, 242, 40);
            }
            else if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                QUI.DrawTexture(DUIResources.pageControlPanelButtonMaskEditorInPlayMode.texture, 242, 40);
            }
            else
            {
                QUI.BeginHorizontal(width);
                {
#if dUI_MasterAudio
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonMasterAudioEnabled), 200, 40))
                {
                        NotificationWindow.YesNo("Disable support for Master Audio?",
                                                 "This will remove '" + DUI.SYMBOL_MASTER_AUDIO + "' from Scripting Define Symbols in Player Settings.",
                                                 () => { QUtils.RemoveScriptingDefineSymbol(DUI.SYMBOL_MASTER_AUDIO); },
                                                 null);
                }
#else
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonMasterAudioDisabled), 200, 40))
                    {
                        NotificationWindow.YesNo("Enable support for Master Audio?",
                                                 "Enable this only if you have Master Audio already installed." +
                                                 "\n\n" +
                                                 "This will add '" + DUI.SYMBOL_MASTER_AUDIO + "' to Scripting Define Symbols in Player Settings.",
                                                 () => { QUtils.AddScriptingDefineSymbol(DUI.SYMBOL_MASTER_AUDIO); },
                                                 null);
                    }
#endif
                    QUI.Space(2);
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonExternalLink), 40, 40))
                    {
                        Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/5607");
                    }
                }
                QUI.EndHorizontal();
            }
            #endregion
            QUI.Space(2);
            #region Energy Bar Toolkit
            if (EditorApplication.isCompiling)
            {
                QUI.DrawTexture(DUIResources.pageControlPanelButtonMaskEditorIsCompiling.texture, 242, 40);
            }
            else if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                QUI.DrawTexture(DUIResources.pageControlPanelButtonMaskEditorInPlayMode.texture, 242, 40);
            }
            else
            {
                QUI.BeginHorizontal(width);
                {
#if dUI_EnergyBarToolkit
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonEnergyBarToolkitEnabled), 200, 40))
                    {
                        NotificationWindow.YesNo("Disable support for Energy Bar Toolkit?",
                                                 "This will remove '" + DUI.SYMBOL_ENERGY_BAR_TOOLKIT + "' from Scripting Define Symbols in Player Settings.",
                                                 () => { QUtils.RemoveScriptingDefineSymbol(DUI.SYMBOL_ENERGY_BAR_TOOLKIT); },
                                                 null);
                    }
#else
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonEnergyBarToolkitDisabled), 200, 40))
                    {
                        NotificationWindow.YesNo("Enable support for Energy Bar Toolkit?",
                                                "Enable this only if you have Energy Bar Toolkit already installed." +
                                                "\n\n" +
                                                "This will add '" + DUI.SYMBOL_ENERGY_BAR_TOOLKIT + "' to Scripting Define Symbols in Player Settings.",
                                                () => { QUtils.AddScriptingDefineSymbol(DUI.SYMBOL_ENERGY_BAR_TOOLKIT); },
                                                null);
                    }
#endif
                    QUI.Space(2);
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonExternalLink), 40, 40))
                    {
                        Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/7515");
                    }
                }
                QUI.EndHorizontal();
            }
            #endregion
        }
        void DrawControlPanelDoozyModules(float width)
        {
            QUI.DrawTexture(DUIResources.pageControlPanelSeparatorDoozyModules.texture, 242, 16);
            QUI.Space(2);
            #region Orientation Manager
            if (EditorApplication.isCompiling)
            {
                QUI.DrawTexture(DUIResources.pageControlPanelButtonMaskEditorIsCompiling.texture, 242, 40);
            }
            else if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                QUI.DrawTexture(DUIResources.pageControlPanelButtonMaskEditorInPlayMode.texture, 242, 40);
            }
            else
            {
                QUI.BeginHorizontal(width);
                {
#if dUI_UseOrientationManager
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonOrientationManagerEnabled), 242, 40))
                    {
                        NotificationWindow.YesNo("Disable the Orientation Manager?",
                                                 "This will remove '" + DUI.SYMBOL_ORIENTATION_MANAGER + "' from Scripting Define Symbols in Player Settings.",
                                                 () => { QUtils.RemoveScriptingDefineSymbol(DUI.SYMBOL_ORIENTATION_MANAGER); },
                                                 null);
                    }
#else
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonOrientationManagerDisabled), 242, 40))
                    {
                        NotificationWindow.YesNo("Enable the Orientation Manager?",
                                                 "Enable this only if you want to create different UI's for each orientation." +
                                                 "\n\n" +
                                                 "This will add '" + DUI.SYMBOL_ORIENTATION_MANAGER + "' to Scripting Define Symbols in Player Settings.",
                                                 () => { QUtils.AddScriptingDefineSymbol(DUI.SYMBOL_ORIENTATION_MANAGER); },
                                                 null);
                    }
#endif
                }
                QUI.EndHorizontal();
            }
            #endregion
            QUI.Space(2);
            #region Navigation System
            if (EditorApplication.isCompiling)
            {
                QUI.DrawTexture(DUIResources.pageControlPanelButtonMaskEditorIsCompiling.texture, 242, 40);
            }
            else if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                QUI.DrawTexture(DUIResources.pageControlPanelButtonMaskEditorInPlayMode.texture, 242, 40);
            }
            else
            {
                QUI.BeginHorizontal(width);
                {
#if dUI_NavigationDisabled
                     if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonNavigationSystemDisabled), 242, 40))
                     {
                           NotificationWindow.YesNo("Enable the Navigation Manager?",
                                                    "This will remove '" + DUI.SYMBOL_NAVIGATION_SYSTEM + "' from Scripting Define Symbols in Player Settings.",
                                                    () => { QUtils.RemoveScriptingDefineSymbol(DUI.SYMBOL_NAVIGATION_SYSTEM); },
                                                    null);
                     }
#else
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonNavigationSystemEnabled), 242, 40))
                    {
                        NotificationWindow.YesNo("Disable the Navigation Manager?",
                                                 "Do this if you intend to handle the navigation yourself (maybe by using Playmaker?)." +
                                                 "\n\n" +
                                                 "This will add '" + DUI.SYMBOL_NAVIGATION_SYSTEM + "' to Scripting Define Symbols in Player Settings.",
                                                 () => { QUtils.AddScriptingDefineSymbol(DUI.SYMBOL_NAVIGATION_SYSTEM); },
                                                 null);
                    }
#endif
                }
                QUI.EndHorizontal();
            }
            #endregion
        }
        void DrawControlPanelRecomendedPlugins(float width)
        {
            QUI.DrawTexture(DUIResources.pageControlPanelSeparatorRecomended.texture, 242, 16);
            QUI.Space(2);
            DrawControlPanelExternalLinkButton(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonEzDefineSymbols), "https://www.assetstore.unity3d.com/en/#!/content/75541");
            QUI.Space(2);
            DrawControlPanelExternalLinkButton(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonEzBind), "https://www.assetstore.unity3d.com/en/#!/content/84939");
            QUI.Space(2);
            DrawControlPanelExternalLinkButton(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonEzDataManager), "https://www.assetstore.unity3d.com/en/#!/content/77057");
            QUI.Space(2);
            DrawControlPanelExternalLinkButton(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonEzAds), "https://www.assetstore.unity3d.com/en/#!/content/76283");
            QUI.Space(2);
            DrawControlPanelExternalLinkButton(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageControlPanelButtonPooly), "https://www.assetstore.unity3d.com/en/#!/content/82941");
        }
        void DrawControlPanelExternalLinkButton(GUIStyle style, string url)
        {
            if (QUI.Button(style, 242, 40))
            {
                Application.OpenURL(url);
            }
        }
        void DrawControlPanelNews(float width)
        {
            QUI.DrawTexture(DUIResources.pageControlPanelSeparatorNews.texture, 294, 16);
            DrawArticle("Hey lovely DoozyUI Users!",
                                        "We want to update you on a few things.",
                                        width - 16);

            QUI.Space(3);

            DrawArticle("Apologies",
                                        "First up, we’d like to apologize. " +
                                        "We’re a small team, and we’ve been trying to find the right balance between interacting with the community and getting work done. " +
                                        "If we spend too much time talking to folks, we don’t get as much done, and vice versa." +
                                        "\n\n" +
                                        "We fear we haven’t communicated enough, so we’ll be spending more time keeping you all informed and getting your thoughts. " +
                                        "Stay tuned.",
                                        width - 16);

            QUI.Space(3);

            DrawArticle("About DoozyUI",
                                        "We’ve been hard at work on this completely rewritten core for DoozyUI. " +
                                        "This means we should be able to crank out future releases much faster and with much less effort. " +
                                        "And the new code base is the best we’ve ever made — truly better design, better consistency, and better performance. " +
                                        "And honestly, DoozyUI is turning out better than we’d ever imagined." +
                                        "\n\n" +
                                        "The all-new features are crazy simple to use and they just work. " +
                                        "You get all the power of a complex UI management system with none of the hassle." +
                                        "\n\n" +
                                        "But we know it’s not perfect yet, and we’d love your feedback.",
                                        width - 16);

            QUI.Space(3);

            DrawArticle("The 2.8 Release",
                                        "This release is a huge milestone for us as it is the first big step towards the next major version." +
                                        "\n\n" +
                                        "The new custom editors were created with purpose in mind. " +
                                        "To this end, all the components went through several design iterations until we found a balance between functionality and ease of use." +
                                        "\n\n" +
                                        "New animation presets (over 400) were created for you so that you have a lot of 'out of the box' options." +
                                        "\n" +
                                        "All the components have new options and, for you coders out there, you will notice that new methods are available and that we added summaries to everything.",
                                        width - 16);

            QUI.Space(3);

            DrawArticle("Next Up",
                                        "We will be releasing minor versions 2.8.1, 2.8.2 and so on, where we will add some new options and components for the system. " +
                                        "Between them we will release quick patches for any issues that may arrise (so please tell us if you find any bugs)." +
                                        "\n\n" +
                                        "The next component will be the UIToggle. " +
                                        "Also, as soon as Font Awesome 5 is released to the general public, we'll add it as well (right now the alpha version looks incredible).",
                                        width - 16);

            QUI.Space(3);

            DrawArticle("The Future",
                                        "What we envision is installing DoozyUI, getting an UI pack (made for DoozyUI) from the Unity Asset Store and having a premade functional UI ready to go in your project in less than 5 minutes." +
                                        "\n\n" +
                                        "That is why we plan on giving you, all the UI designers out there, the option of creating premade UIs. " +
                                        "Right now we see a lot of UI graphic packs on the Unity Asset Store and we would love seeing those packs turned into 'out of the box' working UIs (not just static graphics). " +
                                        "This is one of the big features that we are working on for the 3.0 major version." +
                                        "\n\n" +
                                        "Another big feature will be a NodeGraph that will allow you create and visualize the UI flow.",
                                        width - 16);

            QUI.Space(3);

            DrawArticle("Thank You",
                                        "We would like to thank everyone that bought DoozyUI and helped us get this far." +
                                        "\n\n" +
                                        "We have even bigger plans for the system and we are glad to have your support. " +
                                        "Should you have any suggestions or find any bugs (or solutions) please let us know so that we can improve the system for you and anyone that uses it." +
                                        "\n\n" +
                                        "Thanks!" +
                                        "\n" +
                                        "Doozy Entertainment",
                                        width - 16);
        }
        void DrawArticle(string title, string text, float width)
        {
            QUI.Label(title, NewsTitleStyle, width);
            QUI.Space(-4);
            QUI.Label(text, NewsTextStyle, width);
        }
    }
}
