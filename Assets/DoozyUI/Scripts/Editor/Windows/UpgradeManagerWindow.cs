// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using UnityEditor;
using UnityEngine;

namespace DoozyUI
{
    public class UpgradeManagerWindow : QWindow
    {
        public static UpgradeManagerWindow Instance;

        private static bool _utility = true;
        private static string _title = "DoozyUI - Upgrade Manager";
        private static bool _focus = true;

        private static float _minWidth = 300;
        private static float _minHeight = 600;

        [MenuItem("Tools/DoozyUI/Upgrade Manager", false, 100)]
        static void Init()
        {
            Instance = GetWindow<UpgradeManagerWindow>(_utility, _title, _focus);
            Instance.SetupWindow();
        }

        private void OnEnable()
        {
            autoRepaintOnSceneChange = true;
            requiresContantRepaint = true;
        }

        private void SetupWindow()
        {
            titleContent = new GUIContent(_title);
            minSize = new Vector2(_minWidth, _minHeight);
            maxSize = minSize;
            CenterWindow();
        }

        private void OnGUI()
        {
            DrawBackground();
            QUI.Space(332);
            DrawButtons();
            Repaint();
        }

        void DrawBackground()
        {
            QUI.DrawTexture(DUIResources.upgradeManagerBackground.texture, position.width, position.height);
            QUI.Space(-position.height);
        }

        void DrawButtons()
        {
            QUI.BeginHorizontal(position.width);
            {
                QUI.FlexibleSpace();
                QUI.BeginVertical(240);
                {
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.UpgradeManager.ButtonUpgradeScene), 240, 40))
                    {
                        NotificationWindow.YesNo("Upgrade Current Scene",
                                                 "Are you sure you want to upgrade the current scene?" +
                                                 "\n\n" +
                                                 "This process will override all the values of DoozyUI components by getting the old version values and converting them to the new version values.",
                                                 UpgradeScene,
                                                 null);
                    }
                    QUI.Space(SPACE_8);
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.UpgradeManager.ButtonCleanFiles), 240, 40))
                    {
                        NotificationWindow.OkCancel("Clean Files",
                                                    "This process will delete all the files that are no longer needed by the system. These files were used by the old core and are no longer needed by the new one.",
                                                    CleanFiles,
                                                    null);
                    }
                    QUI.Space(SPACE_8);
                    if (QUI.Button(DUIStyles.GetStyle(DUIStyles.UpgradeManager.ButtonDeleteExamples), 240, 40))
                    {
                        NotificationWindow.OkCancel("Delete Old Examples",
                                                    "Are you sure you want to delete the old examples folder?" +
                                                    "\n\n" +
                                                    "If you are referencing anything from it, in your current project, it is recommended that you keep it." +
                                                    "\n\n" +
                                                    "The old examples folder is Assets/DoozyUI/_EXAMPLES",
                                                    DeleteExamples,
                                                    null);
                    }
                }
                QUI.EndVertical();
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();
        }

        void UpgradeScene()
        {
            DUIUpgradeManager.UpgradeScene();
        }

        void CleanFiles()
        {
            DUIUpgradeManager.CleanFiles();
        }

        void DeleteExamples()
        {
            DUIUpgradeManager.DeleteExamples();
        }
    }
}
