// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using UnityEngine;
using UnityEditor;
using QuickEditor;

namespace DoozyUI
{
    public partial class DUIResources
    {
        private static Font m_Sansation;
        public static Font Sansation { get { if (m_Sansation == null) { m_Sansation = AssetDatabase.LoadAssetAtPath<Font>(DUI.DOOZYUI_PATH + "/Fonts/" + "Sansation-Regular.ttf"); } return m_Sansation; } }

        private static string m_ImagesPath;
        public static string ImagesPath { get { if (string.IsNullOrEmpty(m_ImagesPath)) { m_ImagesPath = DUI.DOOZYUI_PATH + "/Images/"; } return m_ImagesPath; } }

        public static Texture GetTexture(string fileName)
        {
            return AssetDatabase.LoadAssetAtPath<Texture>(ImagesPath + fileName + ".png");
        }

        //ICONS
        private static string m_ImagesIconsPath;
        public static string ImagesIconsPath { get { if (string.IsNullOrEmpty(m_ImagesIconsPath)) { m_ImagesIconsPath = ImagesPath + "Icons/"; } return m_ImagesIconsPath; } }

        public static QTexture iconOrientationManager128x128 = new QTexture(ImagesIconsPath, "iconOrientationManager128x128");
        public static QTexture iconPlayMakerEventDispatcher128x128 = new QTexture(ImagesIconsPath, "iconPlayMakerEventDispatcher128x128");
        public static QTexture iconSceneLoader128x128 = new QTexture(ImagesIconsPath, "iconSceneLoader128x128");
        public static QTexture iconSoundy128x128 = new QTexture(ImagesIconsPath, "iconSoundy128x128");
        public static QTexture iconUIAnimator128x128 = new QTexture(ImagesIconsPath, "iconUIAnimator128x128");
        public static QTexture iconUIButton128x128 = new QTexture(ImagesIconsPath, "iconUIButton128x128");
        public static QTexture iconUICanvas128x128 = new QTexture(ImagesIconsPath, "iconUICanvas128x128");
        public static QTexture iconUIEffect128x128 = new QTexture(ImagesIconsPath, "iconUIEffect128x128");
        public static QTexture iconUIElement128x128 = new QTexture(ImagesIconsPath, "iconUIElement128x128");
        public static QTexture iconUIManager128x128 = new QTexture(ImagesIconsPath, "iconUIManager128x128");
        public static QTexture iconUINotification128x128 = new QTexture(ImagesIconsPath, "iconUINotification128x128");
        public static QTexture iconUINotificationManager128x128 = new QTexture(ImagesIconsPath, "iconUINotificationManager128x128");
        public static QTexture iconUIToggle128x128 = new QTexture(ImagesIconsPath, "iconUIToggle128x128");
        public static QTexture iconUITrigger128x128 = new QTexture(ImagesIconsPath, "iconUITrigger128x128");

        //BUTTONS
        private static string m_ImagesButtonsPath;
        public static string ImagesButtonsPath { get { if (string.IsNullOrEmpty(m_ImagesButtonsPath)) { m_ImagesButtonsPath = ImagesPath + "Buttons/"; } return m_ImagesButtonsPath; } }

        public static QTexture buttonPlay = new QTexture(ImagesButtonsPath, "buttonPlay");
        public static QTexture buttonStop = new QTexture(ImagesButtonsPath, "buttonStop");

        //HEADERS
        private static string m_ImagesHeadersPath;
        public static string ImagesHeadersPath { get { if (string.IsNullOrEmpty(m_ImagesHeadersPath)) { m_ImagesHeadersPath = ImagesPath + "Headers/"; } return m_ImagesHeadersPath; } }

        public static QTexture headerOrientationManager = new QTexture(ImagesHeadersPath, "headerOrientationManager");
        public static QTexture headerPlayMakerEventDispatcher = new QTexture(ImagesHeadersPath, "headerPlayMakerEventDispatcher");
        public static QTexture headerSceneLoader = new QTexture(ImagesHeadersPath, "headerSceneLoader");
        public static QTexture headerSoundy = new QTexture(ImagesHeadersPath, "headerSoundy");
        public static QTexture headerUINotificationManager = new QTexture(ImagesHeadersPath, "headerUINotificationManager");
        public static QTexture headerUICanvas = new QTexture(ImagesHeadersPath, "headerUICanvas");
        public static QTexture headerUIButton = new QTexture(ImagesHeadersPath, "headerUIButton");
        public static QTexture headerUIEffect = new QTexture(ImagesHeadersPath, "headerUIEffect");
        public static QTexture headerUIElement = new QTexture(ImagesHeadersPath, "headerUIElement");
        public static QTexture headerUIManager = new QTexture(ImagesHeadersPath, "headerUIManager");
        public static QTexture headerUINotification = new QTexture(ImagesHeadersPath, "headerUINotification");
        public static QTexture headerUIToggle = new QTexture(ImagesHeadersPath, "headerUIToggle");
        public static QTexture headerUITrigger = new QTexture(ImagesHeadersPath, "headerUITrigger");

        //BARS
        private static string m_ImagesBarsPath;
        public static string ImagesBarsPath { get { if (string.IsNullOrEmpty(m_ImagesBarsPath)) { m_ImagesBarsPath = ImagesPath + "Bars/"; } return m_ImagesBarsPath; } }

        public static QTexture barLinkedToUINotification = new QTexture(ImagesBarsPath, "barLinkedToUINotification");
        public static QTexture barButtonUnlink = new QTexture(ImagesBarsPath, "barButtonUnlink");

        public static QTexture barInAnimations = new QTexture(ImagesBarsPath, "barInAnimations");
        public static QTexture barInAnimationsCollapsed = new QTexture(ImagesBarsPath, "barInAnimationsCollapsed");
        public static QTexture barLoopAnimations = new QTexture(ImagesBarsPath, "barLoopAnimations");
        public static QTexture barLoopAnimationsCollapsed = new QTexture(ImagesBarsPath, "barLoopAnimationsCollapsed");
        public static QTexture barOutAnimations = new QTexture(ImagesBarsPath, "barOutAnimations");
        public static QTexture barOutAnimationsCollapsed = new QTexture(ImagesBarsPath, "barOutAnimationsCollapsed");

        public static QTexture barEvents = new QTexture(ImagesBarsPath, "barEvents");
        public static QTexture barEventsCollapsed = new QTexture(ImagesBarsPath, "barEventsCollapsed");

        public static QTexture barAnimationPreset = new QTexture(ImagesBarsPath, "barAnimationPreset");
        public static QTexture barAnimationPresetCollapsed = new QTexture(ImagesBarsPath, "barAnimationPresetCollapsed");
        public static QTexture barLoopPreset = new QTexture(ImagesBarsPath, "barLoopPreset");
        public static QTexture barLoopPresetCollapsed = new QTexture(ImagesBarsPath, "barLoopPresetCollapsed");
        public static QTexture barPunchPreset = new QTexture(ImagesBarsPath, "barPunchPreset");
        public static QTexture barPunchPresetCollapsed = new QTexture(ImagesBarsPath, "barPunchPresetCollapsed");

        public static QTexture barMove = new QTexture(ImagesBarsPath, "barMove");
        public static QTexture barMoveDisabled = new QTexture(ImagesBarsPath, "barMoveDisabled");
        public static QTexture barRotate = new QTexture(ImagesBarsPath, "barRotate");
        public static QTexture barRotateDisabled = new QTexture(ImagesBarsPath, "barRotateDisabled");
        public static QTexture barScale = new QTexture(ImagesBarsPath, "barScale");
        public static QTexture barScaleDisabled = new QTexture(ImagesBarsPath, "barScaleDisabled");
        public static QTexture barFade = new QTexture(ImagesBarsPath, "barFade");
        public static QTexture barFadeDisabled = new QTexture(ImagesBarsPath, "barFadeDisabled");

        public static QTexture barButtonMove = new QTexture(ImagesBarsPath, "barButtonMove");
        public static QTexture barButtonMoveDisabled = new QTexture(ImagesBarsPath, "barButtonMoveDisabled");
        public static QTexture barButtonRotate = new QTexture(ImagesBarsPath, "barButtonRotate");
        public static QTexture barButtonRotateDisabled = new QTexture(ImagesBarsPath, "barButtonRotateDisabled");
        public static QTexture barButtonScale = new QTexture(ImagesBarsPath, "barButtonScale");
        public static QTexture barButtonScaleDisabled = new QTexture(ImagesBarsPath, "barButtonScaleDisabled");
        public static QTexture barButtonFade = new QTexture(ImagesBarsPath, "barButtonFade");
        public static QTexture barButtonFadeDisabled = new QTexture(ImagesBarsPath, "barButtonFadeDisabled");

        public static QTexture barButtonEnabled = new QTexture(ImagesBarsPath, "barButtonEnabled");
        public static QTexture barButtonDisabled = new QTexture(ImagesBarsPath, "barButtonDisabled");

        public static QTexture barShow = new QTexture(ImagesBarsPath, "barShow");
        public static QTexture barHide = new QTexture(ImagesBarsPath, "barHide");

        public static QTexture barOnPointerEnter = new QTexture(ImagesBarsPath, "barOnPointerEnter");
        public static QTexture barOnPointerEnterCollapsed = new QTexture(ImagesBarsPath, "barOnPointerEnterCollapsed");
        public static QTexture barOnPointerEnterDisabled = new QTexture(ImagesBarsPath, "barOnPointerEnterDisabled");

        public static QTexture barOnPointerExit = new QTexture(ImagesBarsPath, "barOnPointerExit");
        public static QTexture barOnPointerExitCollapsed = new QTexture(ImagesBarsPath, "barOnPointerExitCollapsed");
        public static QTexture barOnPointerExitDisabled = new QTexture(ImagesBarsPath, "barOnPointerExitDisabled");

        public static QTexture barOnPointerDown = new QTexture(ImagesBarsPath, "barOnPointerDown");
        public static QTexture barOnPointerDownCollapsed = new QTexture(ImagesBarsPath, "barOnPointerDownCollapsed");
        public static QTexture barOnPointerDownDisabled = new QTexture(ImagesBarsPath, "barOnPointerDownDisabled");

        public static QTexture barOnPointerUp = new QTexture(ImagesBarsPath, "barOnPointerUp");
        public static QTexture barOnPointerUpCollapsed = new QTexture(ImagesBarsPath, "barOnPointerUpCollapsed");
        public static QTexture barOnPointerUpDisabled = new QTexture(ImagesBarsPath, "barOnPointerUpDisabled");

        public static QTexture barOnClick = new QTexture(ImagesBarsPath, "barOnClick");
        public static QTexture barOnClickCollapsed = new QTexture(ImagesBarsPath, "barOnClickCollapsed");
        public static QTexture barOnClickDisabled = new QTexture(ImagesBarsPath, "barOnClickDisabled");

        public static QTexture barOnDoubleClick = new QTexture(ImagesBarsPath, "barOnDoubleClick");
        public static QTexture barOnDoubleClickCollapsed = new QTexture(ImagesBarsPath, "barOnDoubleClickCollapsed");
        public static QTexture barOnDoubleClickDisabled = new QTexture(ImagesBarsPath, "barOnDoubleClickDisabled");

        public static QTexture barOnLongClick = new QTexture(ImagesBarsPath, "barOnLongClick");
        public static QTexture barOnLongClickCollapsed = new QTexture(ImagesBarsPath, "barOnLongClickCollapsed");
        public static QTexture barOnLongClickDisabled = new QTexture(ImagesBarsPath, "barOnLongClickDisabled");

        public static QTexture barPunchMove = new QTexture(ImagesBarsPath, "barPunchMove");
        public static QTexture barPunchMoveDisabled = new QTexture(ImagesBarsPath, "barPunchMoveDisabled");
        public static QTexture barPunchRotate = new QTexture(ImagesBarsPath, "barPunchRotate");
        public static QTexture barPunchRotateDisabled = new QTexture(ImagesBarsPath, "barPunchRotateDisabled");
        public static QTexture barPunchScale = new QTexture(ImagesBarsPath, "barPunchScale");
        public static QTexture barPunchScaleDisabled = new QTexture(ImagesBarsPath, "barPunchScaleDisabled");

        public static QTexture barNormalAnimation = new QTexture(ImagesBarsPath, "barNormalAnimation");
        public static QTexture barNormalAnimationCollapsed = new QTexture(ImagesBarsPath, "barNormalAnimationCollapsed");
        public static QTexture barSelectedAnimation = new QTexture(ImagesBarsPath, "barSelectedAnimation");
        public static QTexture barSelectedAnimationCollapsed = new QTexture(ImagesBarsPath, "barSelectedAnimationCollapsed");

        public static QTexture barGameEvents = new QTexture(ImagesBarsPath, "barGameEvents");
        public static QTexture barGameEventsCollapsed = new QTexture(ImagesBarsPath, "barGameEventsCollapsed");

        public static QTexture barOrientationLandscape = new QTexture(ImagesBarsPath, "barOrientationLandscape");
        public static QTexture barOrientationPortrait = new QTexture(ImagesBarsPath, "barOrientationPortrait");
        public static QTexture barOrientationUnknown = new QTexture(ImagesBarsPath, "barOrientationUnknown");

        public static QTexture barNavigation = new QTexture(ImagesBarsPath, "barNavigation");
        public static QTexture barNavigationCollapsed = new QTexture(ImagesBarsPath, "barNavigationCollapsed");

        public static QTexture barEnergyBars = new QTexture(ImagesBarsPath, "barEnergyBars");
        public static QTexture barCustomGameEvents = new QTexture(ImagesBarsPath, "barCustomGameEvents");

        //CONTROL PANEL
        private static string m_ImagesControlPanelPath;
        public static string ImagesControlPanelPath { get { if (string.IsNullOrEmpty(m_ImagesControlPanelPath)) { m_ImagesControlPanelPath = ImagesPath + "ControlPanel/"; } return m_ImagesControlPanelPath; } }

        public static QTexture backgroundGrey230 = new QTexture(ImagesControlPanelPath, "backgroundGrey230");
        public static QTexture backgroundGrey242 = new QTexture(ImagesControlPanelPath, "backgroundGrey242");
        public static QTexture backgroundGrey242ShadowLeft = new QTexture(ImagesControlPanelPath, "backgroundGrey242ShadowLeft");

        public static QTexture sideLogoDoozyUI = new QTexture(ImagesControlPanelPath, "sideLogoDoozyUI");

        public static QTexture sideTitleGeneral = new QTexture(ImagesControlPanelPath, "sideTitleGeneral");
        public static QTexture sideButtonControlPanel = new QTexture(ImagesControlPanelPath, "sideButtonControlPanel");
        public static QTexture sideButtonControlPanelSelected = new QTexture(ImagesControlPanelPath, "sideButtonControlPanelSelected");

        public static QTexture sideTitleDatabases = new QTexture(ImagesControlPanelPath, "sideTitleDatabases");
        public static QTexture sideButtonUIElement = new QTexture(ImagesControlPanelPath, "sideButtonUIElement");
        public static QTexture sideButtonUIElementSelected = new QTexture(ImagesControlPanelPath, "sideButtonUIElementSelected");
        public static QTexture sideButtonUIButton = new QTexture(ImagesControlPanelPath, "sideButtonUIButton");
        public static QTexture sideButtonUIButtonSelected = new QTexture(ImagesControlPanelPath, "sideButtonUIButtonSelected");
        public static QTexture sideButtonUISounds = new QTexture(ImagesControlPanelPath, "sideButtonUISounds");
        public static QTexture sideButtonUISoundsSelected = new QTexture(ImagesControlPanelPath, "sideButtonUISoundsSelected");
        public static QTexture sideButtonUICanvases = new QTexture(ImagesControlPanelPath, "sideButtonUICanvases");
        public static QTexture sideButtonUICanvasesSelected = new QTexture(ImagesControlPanelPath, "sideButtonUICanvasesSelected");
        public static QTexture sideButtonAnimatorPresets = new QTexture(ImagesControlPanelPath, "sideButtonAnimatorPresets");
        public static QTexture sideButtonAnimatorPresetsSelected = new QTexture(ImagesControlPanelPath, "sideButtonAnimatorPresetsSelected");

        public static QTexture sideTitleSystem = new QTexture(ImagesControlPanelPath, "sideTitleSystem");
        public static QTexture sideButtonSettings = new QTexture(ImagesControlPanelPath, "sideButtonSettings");
        public static QTexture sideButtonSettingsSelected = new QTexture(ImagesControlPanelPath, "sideButtonSettingsSelected");

        public static QTexture sideTitleHelp = new QTexture(ImagesControlPanelPath, "sideTitleHelp");
        public static QTexture sideButtonHelp = new QTexture(ImagesControlPanelPath, "sideButtonHelp");
        public static QTexture sideButtonHelpSelected = new QTexture(ImagesControlPanelPath, "sideButtonHelpSelected");
        public static QTexture sideButtonAbout = new QTexture(ImagesControlPanelPath, "sideButtonAbout");
        public static QTexture sideButtonAboutSelected = new QTexture(ImagesControlPanelPath, "sideButtonAboutSelected");

        public static QTexture buttonTwitter = new QTexture(ImagesControlPanelPath, "buttonTwitter");
        public static QTexture buttonFacebook = new QTexture(ImagesControlPanelPath, "buttonFacebook");
        public static QTexture buttonYoutube = new QTexture(ImagesControlPanelPath, "buttonYoutube");

        public static QTexture headerControlPanel = new QTexture(ImagesControlPanelPath, "headerControlPanel");
        public static QTexture headerUIElementsDatabase = new QTexture(ImagesControlPanelPath, "headerUIElementsDatabase");
        public static QTexture headerUIButtonsDatabase = new QTexture(ImagesControlPanelPath, "headerUIButtonsDatabase");
        public static QTexture headerUISoundsDatabase = new QTexture(ImagesControlPanelPath, "headerUISoundsDatabase");
        public static QTexture headerUICanvasesDatabase = new QTexture(ImagesControlPanelPath, "headerUICanvasesDatabase");
        public static QTexture headerAnimatorPresets = new QTexture(ImagesControlPanelPath, "headerAnimatorPresets");
        public static QTexture headerSettings = new QTexture(ImagesControlPanelPath, "headerSettings");
        public static QTexture headerHelp = new QTexture(ImagesControlPanelPath, "headerHelp");
        public static QTexture headerAbout = new QTexture(ImagesControlPanelPath, "headerAbout");

        public static QTexture pageControlPanelSeparatorSupport = new QTexture(ImagesControlPanelPath, "pageControlPanelSeparatorSupport");
        public static QTexture pageControlPanelSeparatorDoozyModules = new QTexture(ImagesControlPanelPath, "pageControlPanelSeparatorDoozyModules");
        public static QTexture pageControlPanelSeparatorRecomended = new QTexture(ImagesControlPanelPath, "pageControlPanelSeparatorRecomended");
        public static QTexture pageControlPanelSeparatorSceneInfo = new QTexture(ImagesControlPanelPath, "pageControlPanelSeparatorSceneInfo");
        public static QTexture pageControlPanelSeparatorNews = new QTexture(ImagesControlPanelPath, "pageControlPanelSeparatorNews");
        public static QTexture pageControlPanelSeparatorReleaseNotes = new QTexture(ImagesControlPanelPath, "pageControlPanelSeparatorReleaseNotes");
        public static QTexture pageControlPanelSeparatorRoadmap = new QTexture(ImagesControlPanelPath, "pageControlPanelSeparatorRoadmap");

        public static QTexture pageControlPanelButtonExternalLink = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonExternalLink");
        public static QTexture pageControlPanelButtonMaskEditorIsCompiling = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonMaskEditorIsCompiling");
        public static QTexture pageControlPanelButtonMaskEditorInPlayMode = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonMaskEditorInPlayMode");

        public static QTexture pageControlPanelButtonPlaymakerEnabled = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonPlaymakerEnabled");
        public static QTexture pageControlPanelButtonPlaymakerDisabled = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonPlaymakerDisabled");
        public static QTexture pageControlPanelButtonMasterAudioEnabled = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonMasterAudioEnabled");
        public static QTexture pageControlPanelButtonMasterAudioDisabled = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonMasterAudioDisabled");
        public static QTexture pageControlPanelButtonEnergyBarToolkitEnabled = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonEnergyBarToolkitEnabled");
        public static QTexture pageControlPanelButtonEnergyBarToolkitDisabled = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonEnergyBarToolkitDisabled");

        public static QTexture pageControlPanelButtonOrientationManagerEnabled = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonOrientationManagerEnabled");
        public static QTexture pageControlPanelButtonOrientationManagerDisabled = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonOrientationManagerDisabled");
        public static QTexture pageControlPanelButtonNavigationSystemEnabled = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonNavigationSystemEnabled");
        public static QTexture pageControlPanelButtonNavigationSystemDisabled = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonNavigationSystemDisabled");

        public static QTexture pageControlPanelButtonEzDefineSymbols = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonEzDefineSymbols");
        public static QTexture pageControlPanelButtonEzBind = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonEzBind");
        public static QTexture pageControlPanelButtonEzDataManager = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonEzDataManager");
        public static QTexture pageControlPanelButtonEzAds = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonEzAds");
        public static QTexture pageControlPanelButtonPooly = new QTexture(ImagesControlPanelPath, "pageControlPanelButtonPooly");

        public static QTexture pageButtonOk = new QTexture(ImagesControlPanelPath, "pageButtonOk");
        public static QTexture pageButtonCancel = new QTexture(ImagesControlPanelPath, "pageButtonCancel");

        public static QTexture pageButtonSearch = new QTexture(ImagesControlPanelPath, "pageButtonSearch");
        public static QTexture pageButtonClearSearch = new QTexture(ImagesControlPanelPath, "pageButtonClearSearch");

        public static QTexture pageButtonNewCategory = new QTexture(ImagesControlPanelPath, "pageButtonNewCategory");
        public static QTexture pageButtonSortAtoZ = new QTexture(ImagesControlPanelPath, "pageButtonSortAtoZ");
        public static QTexture pageButtonDelete = new QTexture(ImagesControlPanelPath, "pageButtonDelete");
        public static QTexture pageButtonSelect = new QTexture(ImagesControlPanelPath, "pageButtonSelect");

        public static QTexture pageButtonNewUISound = new QTexture(ImagesControlPanelPath, "pageButtonNewUISound");

        public static QTexture pageButtonFilterHeader = new QTexture(ImagesControlPanelPath, "pageButtonFilterHeader");
        public static QTexture pageButtonFilterAll = new QTexture(ImagesControlPanelPath, "pageButtonFilterAll");
        public static QTexture pageButtonFilterUIButton = new QTexture(ImagesControlPanelPath, "pageButtonFilterUIButton");
        public static QTexture pageButtonFilterUIElement = new QTexture(ImagesControlPanelPath, "pageButtonFilterUIElement");

        public static QTexture pageUISoundsTableTop = new QTexture(ImagesControlPanelPath, "pageUISoundsTableTop");
        public static QTexture pageUISoundsTableBottom = new QTexture(ImagesControlPanelPath, "pageUISoundsTableBottom");

        public static QTexture pageButtonFilterAnimatorPresetsHeader = new QTexture(ImagesControlPanelPath, "pageButtonFilterAnimatorPresetsHeader");
        public static QTexture pageButtonFilterInAnimations = new QTexture(ImagesControlPanelPath, "pageButtonFilterInAnimations");
        public static QTexture pageButtonFilterOutAnimations = new QTexture(ImagesControlPanelPath, "pageButtonFilterOutAnimations");
        public static QTexture pageButtonFilterLoops = new QTexture(ImagesControlPanelPath, "pageButtonFilterLoops");
        public static QTexture pageButtonFilterPunches = new QTexture(ImagesControlPanelPath, "pageButtonFilterPunches");

        public static QTexture pageButtonPlus = new QTexture(ImagesControlPanelPath, "pageButtonPlus");
        public static QTexture pageButtonMinus = new QTexture(ImagesControlPanelPath, "pageButtonMinus");

        public static QTexture pageButtonPlay = new QTexture(ImagesControlPanelPath, "pageButtonPlay");
        public static QTexture pageButtonStop = new QTexture(ImagesControlPanelPath, "pageButtonStop");

        public static QTexture pageImageEmptyDatabase = new QTexture(ImagesControlPanelPath, "pageImageEmptyDatabase");

        public static QTexture buttonBar = new QTexture(ImagesControlPanelPath, "buttonBar");
        public static QTexture caretRotate0 = new QTexture(ImagesControlPanelPath, "caretRotate0");
        public static QTexture caretRotate1 = new QTexture(ImagesControlPanelPath, "caretRotate1");
        public static QTexture caretRotate2 = new QTexture(ImagesControlPanelPath, "caretRotate2");
        public static QTexture caretRotate3 = new QTexture(ImagesControlPanelPath, "caretRotate3");
        public static QTexture caretRotate4 = new QTexture(ImagesControlPanelPath, "caretRotate4");
        public static QTexture caretRotate5 = new QTexture(ImagesControlPanelPath, "caretRotate5");
        public static QTexture caretRotate6 = new QTexture(ImagesControlPanelPath, "caretRotate6");
        public static QTexture caretRotate7 = new QTexture(ImagesControlPanelPath, "caretRotate7");
        public static QTexture caretRotate8 = new QTexture(ImagesControlPanelPath, "caretRotate8");
        public static QTexture caretRotate9 = new QTexture(ImagesControlPanelPath, "caretRotate9");
        public static QTexture caretRotate10 = new QTexture(ImagesControlPanelPath, "caretRotate10");

        public static QTexture pageHelpSeparatorFAQ = new QTexture(ImagesControlPanelPath, "pageHelpSeparatorFAQ");
        public static QTexture pageHelpSeparatorResources = new QTexture(ImagesControlPanelPath, "pageHelpSeparatorResources");

        public static QTexture pageHelpButtonApi = new QTexture(ImagesControlPanelPath, "pageHelpButtonApi");
        public static QTexture pageHelpButtonForum = new QTexture(ImagesControlPanelPath, "pageHelpButtonForum");
        public static QTexture pageHelpButtonManual = new QTexture(ImagesControlPanelPath, "pageHelpButtonManual");
        public static QTexture pageHelpButtonMail = new QTexture(ImagesControlPanelPath, "pageHelpButtonMail");

        public static QTexture pageAboutDoozyUIVersion = new QTexture(ImagesControlPanelPath, "pageAboutDoozyUIVersion");

        //UPGRADE MANAGER
        private static string m_ImagesUpgradeManagerPath;
        public static string ImagesUpgradeManagerPath { get { if (string.IsNullOrEmpty(m_ImagesUpgradeManagerPath)) { m_ImagesUpgradeManagerPath = ImagesPath + "UpgradeManager/"; } return m_ImagesUpgradeManagerPath; } }

        public static QTexture upgradeManagerBackground = new QTexture(ImagesUpgradeManagerPath, "upgradeManagerBackground");
        public static QTexture upgradeManagerButtonUpgradeScene = new QTexture(ImagesUpgradeManagerPath, "upgradeManagerButtonUpgradeScene");
        public static QTexture upgradeManagerButtonCleanFiles = new QTexture(ImagesUpgradeManagerPath, "upgradeManagerButtonCleanFiles");
        public static QTexture upgradeManagerButtonDeleteExamples = new QTexture(ImagesUpgradeManagerPath, "upgradeManagerButtonDeleteExamples");

        //NOTIFICATION WINDOW
        private static string m_ImagesNotificationWindowPath;
        public static string ImagesNotificationWindowPath { get { if (string.IsNullOrEmpty(m_ImagesNotificationWindowPath)) { m_ImagesNotificationWindowPath = ImagesPath + "NotificationWindow/"; } return m_ImagesNotificationWindowPath; } }

        public static QTexture notificationWindowBackground = new QTexture(ImagesNotificationWindowPath, "notificationWindowBackground");
        public static QTexture notificationWindowButtonOk = new QTexture(ImagesNotificationWindowPath, "notificationWindowButtonOk");
        public static QTexture notificationWindowButtonCancel = new QTexture(ImagesNotificationWindowPath, "notificationWindowButtonCancel");
        public static QTexture notificationWindowButtonYes = new QTexture(ImagesNotificationWindowPath, "notificationWindowButtonYes");
        public static QTexture notificationWindowButtonNo = new QTexture(ImagesNotificationWindowPath, "notificationWindowButtonNo");
        public static QTexture notificationWindowButtonContinue = new QTexture(ImagesNotificationWindowPath, "notificationWindowButtonContinue");
    }
}
