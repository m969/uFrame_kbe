// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;

namespace DoozyUI
{
    public partial class ControlPanelWindow : QWindow
    {
        public const string VERSION = "Version 2.8.0p4";
        public const string COPYRIGHT = "Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.";

        void DrawAbout()
        {
            QUI.DrawTexture(DUIResources.headerAbout.texture, 552, 64);
            float sectionWidth = PAGE_WIDTH - SIDE_BAR_SHADOW_WIDTH * 2;
            QUI.Space(SPACE_16);
            QUI.DrawTexture(DUIResources.pageAboutDoozyUIVersion.texture, 552, 256);
            QUI.Space(SPACE_16);

            DrawArticle("About DoozyUI",
                                        "DoozyUI is a complete UI management system for Unity. " +
                                        "It manipulates native Unity components and takes full advantage of their intended usage. " +
                                        "This assures maximum compatibility with uGUI, best performance and makes the entire system have a predictable behaviour. " +
                                        "Also, by working only with native components, the system will be compaible with any ohter asset that uses uGUI correctly. " +
                                        "\n\n" +
                                        "Easy to use and understand, given the user has some basic knowledge of how Unity's native UI solution (uGUI) works, DoozyUI has flexible components that can be configured in a lot of ways. " +
                                        "Functionality and design go hand in hand in order to offer a pleasant user experience (UX) while using the system." +
                                        "\n\n" +
                                        "Starting with version 2.8, DoozyUI is officialy VR READY, being capable of handling with ease multiple Canvases set to World Space render mode. " +
                                        "The system has been redesigned, from the core up, in order to accomodate a higher degree of flexibility that was needed in order for it to handle a lot of different use case scenarios." +
                                        "\n\n" +
                                        "The asset 'DoozyUI' has been released on the Unity Asset Store under the 'Doozy Entertainment' brand, owned by the Marlink Trading SRL company.",
                                        sectionWidth);
        }
    }
}
