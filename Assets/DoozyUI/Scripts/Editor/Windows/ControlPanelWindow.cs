// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace DoozyUI
{
    public partial class ControlPanelWindow : QWindow
    {
        public static ControlPanelWindow Instance;

        public static bool Selected = false;
        private static bool needsRefresh = false;
        public static void NeedsRefresh(bool value = true) { needsRefresh = value; }

        private const float VERTICAL_SPACE_DIVIDER = 20f;
        private const float SIDE_BAR_WIDTH = 300f;
        private const float SIDE_BAR_SHADOW_WIDTH = 16f;
        private const float PAGE_WIDTH = SIDE_BAR_WIDTH * 2 - SIDE_BAR_SHADOW_WIDTH;
        private const float SIDE_BAR_LOGO_HEIGHT = 70f;
        private const float SIDE_BAR_HEADER_HEIGHT = 20f;
        private const float SIDE_BAR_BUTTON_HEIGHT = 32f;

        private enum AnimatorPreset { InAnimations, OutAnimations, Loops, Punches }

        public enum Section
        {
            None,
            ControlPanel,
            UIElements,
            UIButtons,
            UISounds,
            CanvasNames,
            AnimatorPresets,
            Settings,
            Help,
            About
        }

        public static Section CurrentSection = Section.ControlPanel;
        public static Section PreviousSection = Section.None;
        public static bool refreshData = true;

        private static Vector2 SectionScrollPosition = Vector2.zero;

        private static bool _utility = true;
        private static string _title = "DoozyUI - Control Panel";
        private static bool _focus = true;

        private static float _minWidth = 900;
        private static float _minHeight = 600;

        private static Dictionary<string, AnimBool> UIElementsDatabaseAnimBool;
        private static Dictionary<string, AnimBool> UIButtonsDatabaseAnimBool;
        private static Dictionary<string, AnimBool> InAnimationsAnimatorPresetsAnimBool;
        private static Dictionary<string, AnimBool> OutAnimationsAnimatorPresetsAnimBool;
        private static Dictionary<string, AnimBool> LoopsAnimatorPresetsAnimBool;
        private static Dictionary<string, AnimBool> PunchesAnimatorPresetsAnimBool;

        private static string NewCategoryName = "";
        private static AnimBool NewCategoryAnimBool = new AnimBool(false);

        private static string NewUISoundName = "";
        private static AnimBool NewUISoundAnimBool = new AnimBool(false);
        private static SoundType soundTypeFilter = SoundType.All;

        private static AnimatorPreset selectedAnimatorPreset = AnimatorPreset.InAnimations;
        private static string openedAnimatorPresetCategory = "";
        private static string selectedAnimatorPresetName = "";
        private static bool refreshSelectedAnimatorPresetDatabase = true;
        private static AnimData SelectedInAnimData;
        private static AnimData SelectedOutAnimData;
        private static LoopData SelectedLoopData;
        private static PunchData SelectedPunchData;
        private static AnimBool showPresetSettingsAnimBool = new AnimBool(false);
        private AnimBool showMoveIn = new AnimBool(false);
        private AnimBool showRotateIn = new AnimBool(false);
        private AnimBool showScaleIn = new AnimBool(false);
        private AnimBool showFadeIn = new AnimBool(false);
        private AnimBool showMoveOut = new AnimBool(false);
        private AnimBool showRotateOut = new AnimBool(false);
        private AnimBool showScaleOut = new AnimBool(false);
        private AnimBool showFadeOut = new AnimBool(false);
        private AnimBool showMoveLoop = new AnimBool(false);
        private AnimBool showRotateLoop = new AnimBool(false);
        private AnimBool showScaleLoop = new AnimBool(false);
        private AnimBool showFadeLoop = new AnimBool(false);
        private AnimBool showMovePunch = new AnimBool(false);
        private AnimBool showRotatePunch = new AnimBool(false);
        private AnimBool showScalePunch = new AnimBool(false);

        private string SearchPattern = string.Empty;
        private static AnimBool SearchPatternAnimBool = new AnimBool(false);


        private static GUIStyle m_newsTitleStyle;
        private static GUIStyle NewsTitleStyle
        {
            get
            {
                if(m_newsTitleStyle == null)
                {
                    m_newsTitleStyle = new GUIStyle(DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal));
                    m_newsTitleStyle.alignment = TextAnchor.UpperLeft;
                    m_newsTitleStyle.stretchWidth = true;
                    m_newsTitleStyle.wordWrap = true;
                }
                return m_newsTitleStyle;
            }
        }
        private static GUIStyle m_newsTextStyle;
        private static GUIStyle NewsTextStyle
        {
            get
            {
                if(m_newsTextStyle == null)
                {
                    m_newsTextStyle = new GUIStyle(DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmall)); ;
                    m_newsTextStyle.alignment = TextAnchor.UpperLeft;
                    m_newsTextStyle.stretchWidth = true;
                    m_newsTextStyle.wordWrap = true;
                }
                return m_newsTextStyle;
            }
        }


        [MenuItem("Tools/DoozyUI/Control Panel", false, 0)]
        static void Init()
        {
            Instance = GetWindow<ControlPanelWindow>(_utility, _title, _focus);
            Instance.SetupWindow();
        }

        public static void Open(Section section)
        {
            Init();
            CurrentSection = section;
            refreshData = true;
        }

        private void OnEnable()
        {
            autoRepaintOnSceneChange = true;
            requiresContantRepaint = true;
        }

        private void RefreshUIElementsDatabase(bool forceRefresh = false)
        {
            if (DUI.UIElementsDatabase == null || forceRefresh)
            {
                EditorUtility.DisplayProgressBar("Reloading Data", "Refreshing the UIElements Database...", 0.5f);
                DUI.RefreshUIElementsDatabase();
            }
            EditorUtility.DisplayProgressBar("Reloading Data", "Quick cleanup...", 0.75f);
            UIElementsDatabaseAnimBool = new Dictionary<string, AnimBool>();
            foreach (string category in DUI.UIElementsDatabase.Keys)
            {
                DUI.UIElementsDatabase[category].RemoveEmpty();
                DUI.UIElementsDatabase[category].Sort();
                UIElementsDatabaseAnimBool.Add(category, new AnimBool(false));
            }
            EditorUtility.ClearProgressBar();
        }
        private void RefreshUIButtonsDatabase(bool forcedRefresh = false)
        {
            if (DUI.UIButtonsDatabase == null || forcedRefresh)
            {
                EditorUtility.DisplayProgressBar("Reloading Data", "Refreshing the UIButtons Database...", 0.5f);
                DUI.RefreshUIButtonsDatabase();
            }
            EditorUtility.DisplayProgressBar("Reloading Data", "Quick cleanup...", 0.75f);
            UIButtonsDatabaseAnimBool = new Dictionary<string, AnimBool>();
            foreach (string category in DUI.UIButtonsDatabase.Keys)
            {
                DUI.UIButtonsDatabase[category].RemoveEmpty();
                DUI.UIButtonsDatabase[category].Sort();
                UIButtonsDatabaseAnimBool.Add(category, new AnimBool(false));
            }
            EditorUtility.ClearProgressBar();
        }
        private void RefreshUISoundsDatabase(bool forcedRefresh = false)
        {
            if (DUI.UISoundsDatabase == null || forcedRefresh)
            {
                EditorUtility.DisplayProgressBar("Reloading Data", "Refreshing the UISounds Database...", 0.5f);
                DUI.RefreshUISoundsDatabase();
            }
            EditorUtility.ClearProgressBar();
        }
        private void RefreshCanvasNamesDatabase(bool forcedRefresh = false)
        {
            if (DUI.CanvasNamesDatabase == null || forcedRefresh)
            {
                EditorUtility.DisplayProgressBar("Reloading Data", "Refreshing the UICanvases Database...", 0.5f);
                DUI.RefreshCanvasNamesDatabase();
            }
            EditorUtility.ClearProgressBar();
        }
        private void RefreshInAnimationsAnimatorPresets(bool forceRefresh = false)
        {
            if (UIAnimatorUtil.InAnimDataPresetsDatabase == null || forceRefresh)
            {
                EditorUtility.DisplayProgressBar("Reloading Data", "Refreshing the - In Animations - Animator Presets...", 0.5f);
                UIAnimatorUtil.RefreshInAnimDataPresetsDatabase();
            }
            EditorUtility.DisplayProgressBar("Reloading Data", "Quick cleanup...", 0.75f);
            InAnimationsAnimatorPresetsAnimBool = new Dictionary<string, AnimBool>();
            foreach (string category in UIAnimatorUtil.InAnimDataPresetsDatabase.Keys)
            {
                InAnimationsAnimatorPresetsAnimBool.Add(category, new AnimBool(selectedAnimatorPreset == AnimatorPreset.InAnimations && category.Equals(openedAnimatorPresetCategory)));
            }
            openedAnimatorPresetCategory = "";
            selectedAnimatorPresetName = "";
            EditorUtility.ClearProgressBar();
        }
        private void RefreshOutAnimationsAnimatorPresets(bool forcedRefresh = false)
        {
            if (UIAnimatorUtil.OutAnimDataPresetsDatabase == null || forcedRefresh)
            {
                EditorUtility.DisplayProgressBar("Reloading Data", "Refreshing the - Out Animations - Animator Presets...", 0.5f);
                UIAnimatorUtil.RefreshOutAnimDataPresetsDatabase();
            }
            EditorUtility.DisplayProgressBar("Reloading Data", "Quick cleanup...", 0.75f);
            OutAnimationsAnimatorPresetsAnimBool = new Dictionary<string, AnimBool>();
            foreach (string category in UIAnimatorUtil.OutAnimDataPresetsDatabase.Keys)
            {
                OutAnimationsAnimatorPresetsAnimBool.Add(category, new AnimBool(selectedAnimatorPreset == AnimatorPreset.OutAnimations && category.Equals(openedAnimatorPresetCategory)));
            }
            openedAnimatorPresetCategory = "";
            selectedAnimatorPresetName = "";
            EditorUtility.ClearProgressBar();
        }
        private void RefreshLoopsAnimatorPresets(bool forcedRefresh = false)
        {
            if (UIAnimatorUtil.LoopDataPresetsDatabase == null || forcedRefresh)
            {
                EditorUtility.DisplayProgressBar("Reloading Data", "Refreshing the - Loop - Animator Presets...", 0.5f);
                UIAnimatorUtil.RefreshLoopDataPresetsDatabase();
            }
            EditorUtility.DisplayProgressBar("Reloading Data", "Quick cleanup...", 0.75f);
            LoopsAnimatorPresetsAnimBool = new Dictionary<string, AnimBool>();
            foreach (string category in UIAnimatorUtil.LoopDataPresetsDatabase.Keys)
            {
                LoopsAnimatorPresetsAnimBool.Add(category, new AnimBool(selectedAnimatorPreset == AnimatorPreset.Loops && category.Equals(openedAnimatorPresetCategory)));
            }
            openedAnimatorPresetCategory = "";
            selectedAnimatorPresetName = "";
            EditorUtility.ClearProgressBar();
        }
        private void RefreshPunchesAnimatorPresets(bool forcedRefresh = false)
        {
            if (UIAnimatorUtil.PunchDataPresetsDatabase == null || forcedRefresh)
            {
                EditorUtility.DisplayProgressBar("Reloading Data", "Refreshing the - Punch - Animator Presets...", 0.5f);
                UIAnimatorUtil.RefreshPunchDataPresetsDatabase();
            }
            EditorUtility.DisplayProgressBar("Reloading Data", "Quick cleanup...", 0.75f);
            PunchesAnimatorPresetsAnimBool = new Dictionary<string, AnimBool>();
            foreach (string category in UIAnimatorUtil.PunchDataPresetsDatabase.Keys)
            {
                PunchesAnimatorPresetsAnimBool.Add(category, new AnimBool(selectedAnimatorPreset == AnimatorPreset.Punches && category.Equals(openedAnimatorPresetCategory)));
            }
            openedAnimatorPresetCategory = "";
            selectedAnimatorPresetName = "";
            EditorUtility.ClearProgressBar();
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

            QUI.BeginHorizontal(position.width);
            {
                DrawSideBar();
                QUI.Space(16);
                DrawPages();
            }
            QUI.EndHorizontal();

            Repaint();
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void OnFocus()
        {
            Selected = true;

            if (needsRefresh)
            {
                switch (CurrentSection)
                {
                    case Section.None: break;
                    case Section.ControlPanel: break;
                    case Section.UIElements: RefreshUIElementsDatabase(true); break;
                    case Section.UIButtons: RefreshUIButtonsDatabase(true); break;
                    case Section.UISounds: RefreshUISoundsDatabase(true); break;
                    case Section.CanvasNames: RefreshCanvasNamesDatabase(true); break;
                    case Section.AnimatorPresets:
                        switch (selectedAnimatorPreset)
                        {
                            case AnimatorPreset.InAnimations: RefreshInAnimationsAnimatorPresets(true); break;
                            case AnimatorPreset.OutAnimations: RefreshOutAnimationsAnimatorPresets(true); break;
                            case AnimatorPreset.Loops: RefreshLoopsAnimatorPresets(true); break;
                            case AnimatorPreset.Punches: RefreshPunchesAnimatorPresets(true); break;
                        }
                        break;
                    case Section.Settings: break;
                    case Section.Help: break;
                    case Section.About: break;
                }
                needsRefresh = false;
            }
            AssetDatabase.SaveAssets();
        }

        private void OnLostFocus()
        {
            Selected = false;
            AssetDatabase.SaveAssets();
        }

        private void OnDisable()
        {
            AssetDatabase.SaveAssets();
        }

        void DrawBackground()
        {
            QUI.BeginHorizontal();
            {
                QUI.DrawTexture(DUIResources.backgroundGrey230.texture, SIDE_BAR_WIDTH, position.height);
                QUI.Space(-SIDE_BAR_WIDTH);
                QUI.DrawTexture(DUIResources.backgroundGrey242ShadowLeft.texture, SIDE_BAR_SHADOW_WIDTH, position.height);
                QUI.Space(-SIDE_BAR_WIDTH * 2 + SIDE_BAR_SHADOW_WIDTH);
                QUI.DrawTexture(DUIResources.backgroundGrey242.texture, PAGE_WIDTH, position.height);
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();
        }

        void DrawSideBar()
        {
            QUI.BeginVertical(300);
            {
                DrawSideBarLogo();
                DrawSideBarGeneral();
                DrawSideBarDatabases();
                DrawSideBarSystem();
                DrawSideBarHelp();
                QUI.FlexibleSpace();
                DrawSideBarSocial();
            }
            QUI.EndVertical();
        }

        void DrawSideBarLogo()
        {
            QUI.DrawTexture(DUIResources.sideLogoDoozyUI.texture, SIDE_BAR_WIDTH, SIDE_BAR_LOGO_HEIGHT);
        }
        void DrawSideBarGeneral()
        {
            QUI.DrawTexture(DUIResources.sideTitleGeneral.texture, SIDE_BAR_WIDTH, SIDE_BAR_HEADER_HEIGHT);
            if (CurrentSection == Section.ControlPanel)
            {
                QUI.DrawTexture(DUIResources.sideButtonControlPanelSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.SideButtonControlPanel), SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.ControlPanel;
                    ResetPageView();
                }
            }
            QUI.Space(VERTICAL_SPACE_DIVIDER);
        }
        void DrawSideBarDatabases()
        {
            QUI.DrawTexture(DUIResources.sideTitleDatabases.texture, SIDE_BAR_WIDTH, SIDE_BAR_HEADER_HEIGHT);
            if (CurrentSection == Section.UIElements)
            {
                QUI.DrawTexture(DUIResources.sideButtonUIElementSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.SideButtonUIElement), SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.UIElements;
                    ResetPageView();
                }
            }

            if (CurrentSection == Section.UIButtons)
            {
                QUI.DrawTexture(DUIResources.sideButtonUIButtonSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.SideButtonUIButton), SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.UIButtons;
                    ResetPageView();
                }
            }

            if (CurrentSection == Section.UISounds)
            {
                QUI.DrawTexture(DUIResources.sideButtonUISoundsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.SideButtonUISounds), SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.UISounds;
                    ResetPageView();
                }
            }

            if (CurrentSection == Section.CanvasNames)
            {
                QUI.DrawTexture(DUIResources.sideButtonUICanvasesSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.SideButtonUICanvases), SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.CanvasNames;
                    ResetPageView();
                }
            }

            if (CurrentSection == Section.AnimatorPresets)
            {
                QUI.DrawTexture(DUIResources.sideButtonAnimatorPresetsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.SideButtonAnimatorPresets), SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.AnimatorPresets;
                    ResetPageView();
                }
            }
            QUI.Space(VERTICAL_SPACE_DIVIDER);
        }
        void DrawSideBarSystem()
        {
            QUI.DrawTexture(DUIResources.sideTitleSystem.texture, SIDE_BAR_WIDTH, SIDE_BAR_HEADER_HEIGHT);

            if (CurrentSection == Section.Settings)
            {
                QUI.DrawTexture(DUIResources.sideButtonSettingsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.SideButtonSettings), SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.Settings;
                    ResetPageView();
                }
            }

            QUI.Space(VERTICAL_SPACE_DIVIDER);
        }
        void DrawSideBarHelp()
        {
            QUI.DrawTexture(DUIResources.sideTitleHelp.texture, SIDE_BAR_WIDTH, SIDE_BAR_HEADER_HEIGHT);

            if (CurrentSection == Section.Help)
            {
                QUI.DrawTexture(DUIResources.sideButtonHelpSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.SideButtonHelp), SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.Help;
                    ResetPageView();
                }
            }

            if (CurrentSection == Section.About)
            {
                QUI.DrawTexture(DUIResources.sideButtonAboutSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.SideButtonAbout), SIDE_BAR_WIDTH, SIDE_BAR_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.About;
                    ResetPageView();
                }
            }
            QUI.Space(VERTICAL_SPACE_DIVIDER);
        }
        void DrawSideBarSocial()
        {
            QUI.BeginHorizontal(SIDE_BAR_WIDTH);
            {
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.ButtonTwitter), 100, 20))
                {
                    Application.OpenURL("https://twitter.com/doozyplay");
                }
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.ButtonFacebook), 100, 20))
                {
                    Application.OpenURL("https://www.facebook.com/doozyentertainment");
                }
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.ButtonYoutube), 100, 20))
                {
                    Application.OpenURL("http://www.youtube.com/c/DoozyEntertainment");
                }
            }
            QUI.EndHorizontal();
            QUI.Space(VERTICAL_SPACE_DIVIDER / 2);
        }

        void DrawPages()
        {
            SectionScrollPosition = QUI.BeginScrollView(SectionScrollPosition);
            {
                switch (CurrentSection)
                {
                    case Section.ControlPanel:
                        DrawControlPanel();
                        break;
                    case Section.UIElements:
                        if (PreviousSection != CurrentSection || refreshData)
                        {
                            RefreshUIElementsDatabase();
                        }
                        DrawUIElementsDatabase();
                        break;
                    case Section.UIButtons:
                        if (PreviousSection != CurrentSection || refreshData)
                        {
                            RefreshUIButtonsDatabase();
                        }
                        DrawUIButtonsDatabase();
                        break;
                    case Section.UISounds:
                        if (PreviousSection != CurrentSection || refreshData)
                        {
                            RefreshUISoundsDatabase();
                        }
                        DrawUISounds();
                        break;
                    case Section.CanvasNames:
                        if (PreviousSection != CurrentSection || refreshData)
                        {
                            RefreshCanvasNamesDatabase();
                        }
                        DrawUICanvases();
                        break;
                    case Section.AnimatorPresets:
                        if (PreviousSection != CurrentSection || refreshData)
                        {
                            openedAnimatorPresetCategory = "";
                            selectedAnimatorPresetName = "";
                        }
                        DrawAnimatorPresets();
                        break;
                    case Section.Settings:
                        DrawSettings();
                        break;
                    case Section.Help: DrawHelp(); break;
                    case Section.About: DrawAbout(); break;
                }
                QUI.Space(16);
            }
            QUI.EndScrollView();

            if (PreviousSection != CurrentSection || refreshData)
            {
                PreviousSection = CurrentSection;
                refreshData = false;
            }
        }

        void ResetPageView()
        {
            SectionScrollPosition = Vector2.zero; //reset scroll

            NewCategoryName = ""; //reset new category name
            NewCategoryAnimBool.value = false; //reset ui for new category

            SearchPattern = ""; //reset search pattern
            SearchPatternAnimBool.value = false; //reset ui for search pattern
        }


        bool ButtonBar(string text, AnimBool aBool, float width)
        {
            return ButtonBar(text, DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), aBool, width);
        }
        bool ButtonBar(string text, GUIStyle textStyle, AnimBool aBool, float width)
        {
            QUI.BeginVertical(width);
            {
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.ButtonBar), width, 20))
                {
                    return true; //button clicked
                }
                QUI.Space(-20);
                QUI.BeginHorizontal(width);
                {
                    if (aBool.faded == 0) { QUI.DrawTexture(DUIResources.caretRotate10.texture, 20, 20); }
                    else if (aBool.faded <= 0.1f) { QUI.DrawTexture(DUIResources.caretRotate9.texture, 20, 20); }
                    else if (aBool.faded <= 0.2f) { QUI.DrawTexture(DUIResources.caretRotate8.texture, 20, 20); }
                    else if (aBool.faded <= 0.3f) { QUI.DrawTexture(DUIResources.caretRotate7.texture, 20, 20); }
                    else if (aBool.faded <= 0.4f) { QUI.DrawTexture(DUIResources.caretRotate6.texture, 20, 20); }
                    else if (aBool.faded <= 0.5f) { QUI.DrawTexture(DUIResources.caretRotate5.texture, 20, 20); }
                    else if (aBool.faded <= 0.6f) { QUI.DrawTexture(DUIResources.caretRotate4.texture, 20, 20); }
                    else if (aBool.faded <= 0.7f) { QUI.DrawTexture(DUIResources.caretRotate3.texture, 20, 20); }
                    else if (aBool.faded <= 0.8f) { QUI.DrawTexture(DUIResources.caretRotate2.texture, 20, 20); }
                    else if (aBool.faded <= 0.9f) { QUI.DrawTexture(DUIResources.caretRotate1.texture, 20, 20); }
                    else { QUI.DrawTexture(DUIResources.caretRotate0.texture, 20, 20); }
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
                QUI.Space(-2);
                QUI.BeginHorizontal(width);
                {
                    QUI.Space(20);
                    QUI.Label(text, textStyle, width - 20, 20);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
            }
            QUI.EndVertical();
            return false; //button not clicked
        }
        void DrawNamesList(List<string> list, float width, string emptyMessage = "List is empty...")
        {
            QUI.Space(2);
            if (list.Count == 0)
            {
                if (SearchPatternAnimBool.value)
                {
                    QUI.Label("No matching names found in this category!", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic));
                }
                else
                {
                    QUI.BeginHorizontal(width);
                    {
                        QUI.Space(22);
                        QUI.Label(emptyMessage, DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), width - 60);
                        if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonPlus), 20, 20))
                        {
                            list.Add("");
                        }
                    }
                    QUI.EndHorizontal();
                }
                return;
            }

            QUI.BeginVertical(width);
            {
                bool matchFoundInThisCategory = false;
                for (int i = 0; i < list.Count; i++)
                {
                    QUI.BeginHorizontal(width);
                    {
                        if (SearchPatternAnimBool.target)//a search pattern has been entered in the search box
                        {
                            try
                            {
                                if (!Regex.IsMatch(list[i], SearchPattern, RegexOptions.IgnoreCase))
                                {
                                    QUI.EndHorizontal();
                                    continue; //this does not match the search pattern --> we do not show this name it
                                }
                            }
                            catch (Exception)
                            {

                            }
                            matchFoundInThisCategory = true;
                        }
                        QUI.Space(20);
                        QUI.Label(list[i], DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormalItalic), (width - 43) * SearchPatternAnimBool.faded);
                        if (!SearchPatternAnimBool.value)
                        {
                            list[i] = EditorGUILayout.TextField(list[i], GUILayout.Width((width - 59) * (1 - SearchPatternAnimBool.faded)));
                            QUI.Space(-3);
                            if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonMinus), 20 * (1 - SearchPatternAnimBool.faded), 20))
                            {
                                list.RemoveAt(i);
                            }
                        }
                        QUI.FlexibleSpace();
                    }
                    QUI.EndHorizontal();
                    QUI.Space(2);
                }

                if (SearchPatternAnimBool.target)
                {
                    if (!matchFoundInThisCategory) //if a search pattern is active and no valid names were found for this category we let the developer know
                    {
                        QUI.Label("No matching names found in this category!", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic));
                    }
                }
                else //because a search pattern is active, we do no give the developer the option to create a new name
                {
                    QUI.BeginHorizontal(width);
                    {
                        QUI.Space(width - 34);
                        if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonPlus), 20, 20))
                        {
                            list.Add("");
                        }
                        QUI.Space(2);
                    }
                    QUI.EndHorizontal();
                }
            }
            QUI.EndVertical();
        }
        void DrawPresetList(string category, AnimatorPreset presetType, List<string> list, float width, string emptyMessage = "List is empty...")
        {
            QUI.Space(2);
            if (list.Count == 0)
            {
                if (SearchPatternAnimBool.value)
                {
                    QUI.Label("No matching names found in this category!", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic));
                }
                else
                {
                    QUI.BeginHorizontal(width);
                    {
                        QUI.Space(22);
                        QUI.Label(emptyMessage, DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic), width - 60);
                        //if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonPlus), 20, 20))
                        //{
                        //    list.Add("");
                        //}
                    }
                    QUI.EndHorizontal();
                }
                return;
            }

            QUI.BeginVertical(width);
            {
                bool matchFoundInThisCategory = false;
                for (int i = 0; i < list.Count; i++)
                {
                    QUI.BeginHorizontal(width);
                    {
                        if (SearchPatternAnimBool.target)//a search pattern has been entered in the search box
                        {
                            try
                            {
                                if (!Regex.IsMatch(list[i], SearchPattern, RegexOptions.IgnoreCase))
                                {
                                    QUI.EndHorizontal();
                                    continue; //this does not match the search pattern --> we do not show this name it
                                }
                            }
                            catch (Exception)
                            {

                            }
                            matchFoundInThisCategory = true;
                        }
                        QUI.Space(20);
                        if (!SearchPatternAnimBool.value)
                        {
                            if (list[i].Equals(selectedAnimatorPresetName))
                            {
                                SaveColors();
                                QUI.SetGUIColor(DUIColors.BlueLight.Color);
                                QUI.DrawTexture(DUIResources.backgroundGrey230.texture, (width - 55 - 100), 20);
                                QUI.Space(-20);
                                QUI.Label(list[i], DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), (width - 55 - 100) * (1 - SearchPatternAnimBool.faded));
                                QUI.Space(-7);
                                QUI.DrawTexture(DUIResources.pageButtonSelect.active, 100, 20);
                                QUI.Space(-20);
                                QUI.Space(100);
                                RestoreColors();
                            }
                            else
                            {
                                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.ButtonBar), (width - 55 - 100), 20))
                                {
                                    SelectedAnimatorPreset(list[i]);
                                }
                                QUI.Space(-(width - 55 - 100));
                                QUI.Label(list[i], DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelNormal), (width - 55 - 100) * (1 - SearchPatternAnimBool.faded));
                                QUI.Space(-7);
                                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonSelect), 100 * (1 - SearchPatternAnimBool.faded), 20))
                                {
                                    SelectedAnimatorPreset(list[i]);
                                }
                            }
                            QUI.Space(1);
                            if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonMinus), 20 * (1 - SearchPatternAnimBool.faded), 20))
                            {
                                if (EditorUtility.DisplayDialog("Delete the '" + list[i] + "' preset?", "Are you sure you want to proceed?\nOperation cannot be undone!", "Yes", "Cancel"))
                                {
                                    switch (presetType)
                                    {
                                        case AnimatorPreset.InAnimations: UIAnimatorUtil.DeleteInAnimPreset(openedAnimatorPresetCategory, list[i]); RefreshInAnimationsAnimatorPresets(true); break;
                                        case AnimatorPreset.OutAnimations: UIAnimatorUtil.DeleteOutAnimPreset(openedAnimatorPresetCategory, list[i]); RefreshOutAnimationsAnimatorPresets(true); break;
                                        case AnimatorPreset.Loops: UIAnimatorUtil.DeleteLoopPreset(openedAnimatorPresetCategory, list[i]); RefreshLoopsAnimatorPresets(true); break;
                                        case AnimatorPreset.Punches: UIAnimatorUtil.DeletePunchPreset(openedAnimatorPresetCategory, list[i]); RefreshPunchesAnimatorPresets(true); break;
                                    }
                                    openedAnimatorPresetCategory = category;
                                }
                            }
                        }
                        QUI.FlexibleSpace();
                    }
                    QUI.EndHorizontal();
                    QUI.Space(2);
                }

                if (SearchPatternAnimBool.target)
                {
                    if (!matchFoundInThisCategory) //if a search pattern is active and no valid names were found for this category we let the developer know
                    {
                        QUI.Label("No matching names found in this category!", DUIStyles.GetStyle(DUIStyles.BlackTextStyle.BlackLabelSmallItalic));
                    }
                }
                //else //because a search pattern is active, we do no give the developer the option to create a new name
                //{
                //    QUI.BeginHorizontal(width);
                //    {
                //        QUI.Space(width - 34);
                //        if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ControlPanel.PageButtonPlus), 20, 20))
                //        {
                //            list.Add("");
                //        }
                //        QUI.Space(2);
                //    }
                //    QUI.EndHorizontal();
                //}
            }
            QUI.EndVertical();
        }
    }
}
