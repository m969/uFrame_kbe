// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using System.Collections.Generic;
using UnityEngine;

namespace DoozyUI
{
    public class DUIStyles
    {
        public enum TextStyle
        {
            LabelSmall,
            LabelSmallItalic,
            LabelNormal,
            LabelNormalItalic,
            LabelLarge
        }

        public enum BlackTextStyle
        {
            BlackLabelSmall,
            BlackLabelSmallItalic,
            BlackLabelNormal,
            BlackLabelNormalItalic,
            BlackLabelLarge
        }

        public enum ButtonStyle
        {
            Unlink,

            InAnimations,
            InAnimationsCollapsed,
            LoopAnimations,
            LoopAnimationsCollapsed,
            OutAnimations,
            OutAnimationsCollapsed,

            Events,
            EventsCollapsed,

            barAnimationPreset,
            barAnimationPresetCollapsed,
            barLoopPreset,
            barLoopPresetCollapsed,
            barPunchPreset,
            barPunchPresetCollapsed,

            Move,
            MoveDisabled,
            Rotate,
            RotateDisabled,
            Scale,
            ScaleDisabled,
            Fade,
            FadeDisabled,

            BarButtonMove,
            BarButtonMoveDisabled,
            BarButtonRotate,
            BarButtonRotateDisabled,
            BarButtonScale,
            BarButtonScaleDisabled,
            BarButtonFade,
            BarButtonFadeDisabled,
            BarButtonEnabled,
            BarButtonDisabled,

            OnPointerEnter,
            OnPointerEnterCollapsed,

            OnPointerExit,
            OnPointerExitCollapsed,

            OnPointerDown,
            OnPointerDownCollapsed,

            OnPointerUp,
            OnPointerUpCollapsed,

            OnClick,
            OnClickCollapsed,

            OnDoubleClick,
            OnDoubleClickCollapsed,

            OnLongClick,
            OnLongClickCollapsed,

            PunchMove,
            PunchMoveDisabled,
            PunchRotate,
            PunchRotateDisabled,
            PunchScale,
            PunchScaleDisabled,

            NormalAnimation,
            NormalAnimationCollapsed,
            SelectedAnimation,
            SelectedAnimationCollapsed,

            GameEvents,
            GameEventsCollapsed,

            OrientationLandscape,
            OrientationPortrait,
            OrientationUnknown,

            Navigation,
            NavigationCollapsed,

            ButtonPlay,
            ButtonStop
        }

        public enum ControlPanel
        {
            SideButtonControlPanel,
            SideButtonUIElement,
            SideButtonUIButton,
            SideButtonUISounds,
            SideButtonUICanvases,
            SideButtonAnimatorPresets,
            SideButtonSettings,
            SideButtonHelp,
            SideButtonAbout,
            ButtonTwitter,
            ButtonFacebook,
            ButtonYoutube,
            PageButtonOk,
            PageButtonCancel,
            PageButtonSearch,
            PageButtonClearSearch,
            PageButtonNewCategory,
            PageButtonSortAtoZ,
            PageButtonDelete,
            PageButtonSelect,
            PageButtonNewUISound,
            PageButtonFilterAll,
            PageButtonFilterUIButton,
            PageButtonFilterUIElement,
            PageButtonFilterInAnimations,
            PageButtonFilterOutAnimations,
            PageButtonFilterLoops,
            PageButtonFilterPunches,
            PageButtonPlus,
            PageButtonMinus,
            PageButtonPlay,
            PageButtonStop,
            ButtonBar,
            PageControlPanelButtonExternalLink,
            PageControlPanelButtonPlaymakerEnabled,
            PageControlPanelButtonPlaymakerDisabled,
            PageControlPanelButtonMasterAudioEnabled,
            PageControlPanelButtonMasterAudioDisabled,
            PageControlPanelButtonEnergyBarToolkitEnabled,
            PageControlPanelButtonEnergyBarToolkitDisabled,
            PageControlPanelButtonOrientationManagerEnabled,
            PageControlPanelButtonOrientationManagerDisabled,
            PageControlPanelButtonNavigationSystemEnabled,
            PageControlPanelButtonNavigationSystemDisabled,
            PageControlPanelButtonEzDefineSymbols,
            PageControlPanelButtonEzBind,
            PageControlPanelButtonEzDataManager,
            PageControlPanelButtonEzAds,
            PageControlPanelButtonPooly,

            PageHelpApi,
            PageHelpForum,
            PageHelpManual,
            PageHelpMail
        }

        public enum UpgradeManager
        {
            ButtonUpgradeScene,
            ButtonCleanFiles,
            ButtonDeleteExamples
        }

        public enum NotificationWindow
        {
            NotificationTitleBlack,
            NotificationMessageBlack,
            NotificationButtonOk,
            NotificationButtonCancel,
            NotificationButtonYes,
            NotificationButtonNo,
            NotificationButtonContinue
        }

        private static GUISkin skin;
        public static GUISkin Skin { get { if (skin == null) { skin = GetSkin(); } return skin; } }

        public static GUIStyle GetStyle(TextStyle styleName) { return Skin.GetStyle(styleName.ToString()); }
        public static GUIStyle GetStyle(BlackTextStyle styleName) { return Skin.GetStyle(styleName.ToString()); }
        public static GUIStyle GetStyle(ButtonStyle styleName) { return Skin.GetStyle(styleName.ToString()); }
        public static GUIStyle GetStyle(ControlPanel styleName) { return Skin.GetStyle(styleName.ToString()); }
        public static GUIStyle GetStyle(UpgradeManager styleName) { return Skin.GetStyle(styleName.ToString()); }
        public static GUIStyle GetStyle(NotificationWindow styleName) { return Skin.GetStyle(styleName.ToString()); }

        private static GUISkin GetSkin()
        {
            GUISkin skin = ScriptableObject.CreateInstance<GUISkin>();
            List<GUIStyle> styles = new List<GUIStyle>();
            styles.AddRange(TextStyles());
            styles.AddRange(BlackTextStyles());
            styles.AddRange(ButtonStyles());
            styles.AddRange(ControlPanelStyles());
            styles.AddRange(UpgradeManagerStyles());
            styles.AddRange(NotificationWindowStyles());
            skin.customStyles = styles.ToArray();
            return skin;
        }

        private static void UpdateSkin()
        {
            skin = null;
            skin = GetSkin();
        }

        public static void AddStyle(GUIStyle style)
        {
            if (style == null) { return; }
            List<GUIStyle> customStyles = new List<GUIStyle>();
            customStyles.AddRange(Skin.customStyles);
            if (customStyles.Contains(style)) { return; }
            customStyles.Add(style);
            Skin.customStyles = customStyles.ToArray();
        }

        public static void RemoveStyle(GUIStyle style)
        {
            if (style == null) { return; }
            List<GUIStyle> customStyles = new List<GUIStyle>();
            customStyles.AddRange(Skin.customStyles);
            if (!customStyles.Contains(style)) { return; }
            customStyles.Remove(style);
            Skin.customStyles = customStyles.ToArray();
        }

        private static List<GUIStyle> TextStyles()
        {
            List<GUIStyle> styles = new List<GUIStyle>
            {
                GetTextStyle(TextStyle.LabelSmall, TextAnchor.MiddleLeft, FontStyle.Normal, 10, DUIResources.Sansation),
                GetTextStyle(TextStyle.LabelSmallItalic, TextAnchor.MiddleLeft, FontStyle.Italic, 10, DUIResources.Sansation),
                GetTextStyle(TextStyle.LabelNormal, TextAnchor.MiddleLeft, FontStyle.Normal, 12, DUIResources.Sansation),
                GetTextStyle(TextStyle.LabelNormalItalic, TextAnchor.MiddleLeft, FontStyle.Italic, 12, DUIResources.Sansation),
                GetTextStyle(TextStyle.LabelLarge, TextAnchor.MiddleLeft, FontStyle.Normal, 14, DUIResources.Sansation)
            };
            return styles;
        }
        private static GUIStyle GetTextStyle(TextStyle styleName, TextAnchor alignment, FontStyle fontStyle, int fontSize, Font font = null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.name = styleName.ToString();
            style.alignment = alignment;
            style.fontStyle = fontStyle;
            style.fontSize = fontSize;
            style.font = font;
            return style;
        }

        private static List<GUIStyle> BlackTextStyles()
        {
            List<GUIStyle> styles = new List<GUIStyle>
            {
                GetBlackTextStyle(BlackTextStyle.BlackLabelSmall, TextAnchor.MiddleLeft, FontStyle.Normal, 10, DUIResources.Sansation),
                GetBlackTextStyle(BlackTextStyle.BlackLabelSmallItalic, TextAnchor.MiddleLeft, FontStyle.Italic, 10, DUIResources.Sansation),
                GetBlackTextStyle(BlackTextStyle.BlackLabelNormal, TextAnchor.MiddleLeft, FontStyle.Normal, 12, DUIResources.Sansation),
                GetBlackTextStyle(BlackTextStyle.BlackLabelNormalItalic, TextAnchor.MiddleLeft, FontStyle.Italic, 12, DUIResources.Sansation),
                GetBlackTextStyle(BlackTextStyle.BlackLabelLarge, TextAnchor.MiddleLeft, FontStyle.Normal, 14, DUIResources.Sansation)
            };
            return styles;
        }
        private static GUIStyle GetBlackTextStyle(BlackTextStyle styleName, TextAnchor alignment, FontStyle fontStyle, int fontSize, Font font = null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.name = styleName.ToString();
            style.normal.textColor = Color.black;
            style.alignment = alignment;
            style.fontStyle = fontStyle;
            style.fontSize = fontSize;
            style.font = font;
            return style;
        }

        private static List<GUIStyle> ButtonStyles()
        {
            List<GUIStyle> styles = new List<GUIStyle>
            {
                GetButtonStyle(ButtonStyle.Unlink, DUIResources.barButtonUnlink),

                GetButtonStyle(ButtonStyle.InAnimations, DUIResources.barInAnimations),
                GetButtonStyle(ButtonStyle.InAnimationsCollapsed, DUIResources.barInAnimationsCollapsed),
                GetButtonStyle(ButtonStyle.LoopAnimations, DUIResources.barLoopAnimations),
                GetButtonStyle(ButtonStyle.LoopAnimationsCollapsed, DUIResources.barLoopAnimationsCollapsed),
                GetButtonStyle(ButtonStyle.OutAnimations, DUIResources.barOutAnimations),
                GetButtonStyle(ButtonStyle.OutAnimationsCollapsed, DUIResources.barOutAnimationsCollapsed),

                GetButtonStyle(ButtonStyle.Events, DUIResources.barEvents),
                GetButtonStyle(ButtonStyle.EventsCollapsed, DUIResources.barEventsCollapsed),

                GetButtonStyle(ButtonStyle.barAnimationPreset, DUIResources.barAnimationPreset),
                GetButtonStyle(ButtonStyle.barAnimationPresetCollapsed, DUIResources.barAnimationPresetCollapsed),
                GetButtonStyle(ButtonStyle.barLoopPreset, DUIResources.barLoopPreset),
                GetButtonStyle(ButtonStyle.barLoopPresetCollapsed, DUIResources.barLoopPresetCollapsed),
                GetButtonStyle(ButtonStyle.barPunchPreset, DUIResources.barPunchPreset),
                GetButtonStyle(ButtonStyle.barPunchPresetCollapsed, DUIResources.barPunchPresetCollapsed),

                GetButtonStyle(ButtonStyle.Move, DUIResources.barMove),
                GetButtonStyle(ButtonStyle.MoveDisabled, DUIResources.barMoveDisabled),
                GetButtonStyle(ButtonStyle.Rotate, DUIResources.barRotate),
                GetButtonStyle(ButtonStyle.RotateDisabled, DUIResources.barRotateDisabled),
                GetButtonStyle(ButtonStyle.Scale, DUIResources.barScale),
                GetButtonStyle(ButtonStyle.ScaleDisabled, DUIResources.barScaleDisabled),
                GetButtonStyle(ButtonStyle.Fade, DUIResources.barFade),
                GetButtonStyle(ButtonStyle.FadeDisabled, DUIResources.barFadeDisabled),

                GetButtonStyle(ButtonStyle.BarButtonMove, DUIResources.barButtonMove),
                GetButtonStyle(ButtonStyle.BarButtonMoveDisabled, DUIResources.barButtonMoveDisabled),
                GetButtonStyle(ButtonStyle.BarButtonRotate, DUIResources.barButtonRotate),
                GetButtonStyle(ButtonStyle.BarButtonRotateDisabled, DUIResources.barButtonRotateDisabled),
                GetButtonStyle(ButtonStyle.BarButtonScale, DUIResources.barButtonScale),
                GetButtonStyle(ButtonStyle.BarButtonScaleDisabled, DUIResources.barButtonScaleDisabled),
                GetButtonStyle(ButtonStyle.BarButtonFade, DUIResources.barButtonFade),
                GetButtonStyle(ButtonStyle.BarButtonFadeDisabled, DUIResources.barButtonFadeDisabled),

                GetButtonStyle(ButtonStyle.BarButtonEnabled, DUIResources.barButtonEnabled),
                GetButtonStyle(ButtonStyle.BarButtonDisabled, DUIResources.barButtonDisabled),

                GetButtonStyle(ButtonStyle.OnPointerEnter, DUIResources.barOnPointerEnter),
                GetButtonStyle(ButtonStyle.OnPointerEnterCollapsed, DUIResources.barOnPointerEnterCollapsed),

                GetButtonStyle(ButtonStyle.OnPointerExit, DUIResources.barOnPointerExit),
                GetButtonStyle(ButtonStyle.OnPointerExitCollapsed, DUIResources.barOnPointerExitCollapsed),

                GetButtonStyle(ButtonStyle.OnPointerDown, DUIResources.barOnPointerDown),
                GetButtonStyle(ButtonStyle.OnPointerDownCollapsed, DUIResources.barOnPointerDownCollapsed),

                GetButtonStyle(ButtonStyle.OnPointerUp, DUIResources.barOnPointerUp),
                GetButtonStyle(ButtonStyle.OnPointerUpCollapsed, DUIResources.barOnPointerUpCollapsed),

                GetButtonStyle(ButtonStyle.OnClick, DUIResources.barOnClick),
                GetButtonStyle(ButtonStyle.OnClickCollapsed, DUIResources.barOnClickCollapsed),

                GetButtonStyle(ButtonStyle.OnDoubleClick, DUIResources.barOnDoubleClick),
                GetButtonStyle(ButtonStyle.OnDoubleClickCollapsed, DUIResources.barOnDoubleClickCollapsed),

                GetButtonStyle(ButtonStyle.OnLongClick, DUIResources.barOnLongClick),
                GetButtonStyle(ButtonStyle.OnLongClickCollapsed, DUIResources.barOnLongClickCollapsed),

                GetButtonStyle(ButtonStyle.PunchMove, DUIResources.barPunchMove),
                GetButtonStyle(ButtonStyle.PunchMoveDisabled, DUIResources.barPunchMoveDisabled),
                GetButtonStyle(ButtonStyle.PunchRotate, DUIResources.barPunchRotate),
                GetButtonStyle(ButtonStyle.PunchRotateDisabled, DUIResources.barPunchRotateDisabled),
                GetButtonStyle(ButtonStyle.PunchScale, DUIResources.barPunchScale),
                GetButtonStyle(ButtonStyle.PunchScaleDisabled, DUIResources.barPunchScaleDisabled),

                GetButtonStyle(ButtonStyle.NormalAnimation, DUIResources.barNormalAnimation),
                GetButtonStyle(ButtonStyle.NormalAnimationCollapsed, DUIResources.barNormalAnimationCollapsed),
                GetButtonStyle(ButtonStyle.SelectedAnimation, DUIResources.barSelectedAnimation),
                GetButtonStyle(ButtonStyle.SelectedAnimationCollapsed, DUIResources.barSelectedAnimationCollapsed),

                GetButtonStyle(ButtonStyle.GameEvents, DUIResources.barGameEvents),
                GetButtonStyle(ButtonStyle.GameEventsCollapsed, DUIResources.barGameEventsCollapsed),

                GetButtonStyle(ButtonStyle.OrientationLandscape, DUIResources.barOrientationLandscape),
                GetButtonStyle(ButtonStyle.OrientationPortrait, DUIResources.barOrientationPortrait),
                GetButtonStyle(ButtonStyle.OrientationUnknown, DUIResources.barOrientationUnknown),

                GetButtonStyle(ButtonStyle.Navigation, DUIResources.barNavigation),
                GetButtonStyle(ButtonStyle.NavigationCollapsed, DUIResources.barNavigationCollapsed),

                GetButtonStyle(ButtonStyle.ButtonPlay, DUIResources.buttonPlay),
                GetButtonStyle(ButtonStyle.ButtonStop, DUIResources.buttonStop)
            };
            return styles;
        }
        private static GUIStyle GetButtonStyle(ButtonStyle styleName, QTexture qTexture)
        {
            return new GUIStyle()
            {
                name = styleName.ToString(),
                normal = { background = qTexture.normal2D },
                onNormal = { background = qTexture.normal2D },
                hover = { background = qTexture.hover2D },
                onHover = { background = qTexture.hover2D },
                active = { background = qTexture.active2D },
                onActive = { background = qTexture.active2D }
            };
        }

        private static List<GUIStyle> ControlPanelStyles()
        {
            List<GUIStyle> styles = new List<GUIStyle>
            {
                GetButtonStyle(ControlPanel.SideButtonControlPanel, DUIResources.sideButtonControlPanel),
                GetButtonStyle(ControlPanel.SideButtonUIElement, DUIResources.sideButtonUIElement),
                GetButtonStyle(ControlPanel.SideButtonUIButton, DUIResources.sideButtonUIButton),
                GetButtonStyle(ControlPanel.SideButtonUISounds, DUIResources.sideButtonUISounds),
                GetButtonStyle(ControlPanel.SideButtonUICanvases, DUIResources.sideButtonUICanvases),
                GetButtonStyle(ControlPanel.SideButtonAnimatorPresets, DUIResources.sideButtonAnimatorPresets),
                GetButtonStyle(ControlPanel.SideButtonSettings, DUIResources.sideButtonSettings),
                GetButtonStyle(ControlPanel.SideButtonHelp, DUIResources.sideButtonHelp),
                GetButtonStyle(ControlPanel.SideButtonAbout, DUIResources.sideButtonAbout),
                GetButtonStyle(ControlPanel.ButtonTwitter, DUIResources.buttonTwitter),
                GetButtonStyle(ControlPanel.ButtonFacebook, DUIResources.buttonFacebook),
                GetButtonStyle(ControlPanel.ButtonYoutube, DUIResources.buttonYoutube),

                GetButtonStyle(ControlPanel.PageButtonOk, DUIResources.pageButtonOk),
                GetButtonStyle(ControlPanel.PageButtonCancel, DUIResources.pageButtonCancel),
                GetButtonStyle(ControlPanel.PageButtonSearch, DUIResources.pageButtonSearch),
                GetButtonStyle(ControlPanel.PageButtonClearSearch, DUIResources.pageButtonClearSearch),
                GetButtonStyle(ControlPanel.PageButtonNewCategory, DUIResources.pageButtonNewCategory),
                GetButtonStyle(ControlPanel.PageButtonSortAtoZ, DUIResources.pageButtonSortAtoZ),
                GetButtonStyle(ControlPanel.PageButtonDelete, DUIResources.pageButtonDelete),
                GetButtonStyle(ControlPanel.PageButtonSelect, DUIResources.pageButtonSelect),
                GetButtonStyle(ControlPanel.PageButtonNewUISound, DUIResources.pageButtonNewUISound),
                GetButtonStyle(ControlPanel.PageButtonFilterAll, DUIResources.pageButtonFilterAll),
                GetButtonStyle(ControlPanel.PageButtonFilterUIButton, DUIResources.pageButtonFilterUIButton),
                GetButtonStyle(ControlPanel.PageButtonFilterUIElement, DUIResources.pageButtonFilterUIElement),
                GetButtonStyle(ControlPanel.PageButtonFilterInAnimations, DUIResources.pageButtonFilterInAnimations),
                GetButtonStyle(ControlPanel.PageButtonFilterOutAnimations, DUIResources.pageButtonFilterOutAnimations),
                GetButtonStyle(ControlPanel.PageButtonFilterLoops, DUIResources.pageButtonFilterLoops),
                GetButtonStyle(ControlPanel.PageButtonFilterPunches, DUIResources.pageButtonFilterPunches),
                GetButtonStyle(ControlPanel.PageButtonPlus, DUIResources.pageButtonPlus),
                GetButtonStyle(ControlPanel.PageButtonMinus, DUIResources.pageButtonMinus),
                GetButtonStyle(ControlPanel.PageButtonPlay, DUIResources.pageButtonPlay),
                GetButtonStyle(ControlPanel.PageButtonStop, DUIResources.pageButtonStop),
                GetButtonStyle(ControlPanel.ButtonBar, DUIResources.buttonBar),

                GetButtonStyle(ControlPanel.PageControlPanelButtonExternalLink, DUIResources.pageControlPanelButtonExternalLink),
                GetButtonStyle(ControlPanel.PageControlPanelButtonPlaymakerEnabled, DUIResources.pageControlPanelButtonPlaymakerEnabled),
                GetButtonStyle(ControlPanel.PageControlPanelButtonPlaymakerDisabled, DUIResources.pageControlPanelButtonPlaymakerDisabled),
                GetButtonStyle(ControlPanel.PageControlPanelButtonMasterAudioEnabled, DUIResources.pageControlPanelButtonMasterAudioEnabled),
                GetButtonStyle(ControlPanel.PageControlPanelButtonMasterAudioDisabled, DUIResources.pageControlPanelButtonMasterAudioDisabled),
                GetButtonStyle(ControlPanel.PageControlPanelButtonEnergyBarToolkitEnabled, DUIResources.pageControlPanelButtonEnergyBarToolkitEnabled),
                GetButtonStyle(ControlPanel.PageControlPanelButtonEnergyBarToolkitDisabled, DUIResources.pageControlPanelButtonEnergyBarToolkitDisabled),

                GetButtonStyle(ControlPanel.PageControlPanelButtonOrientationManagerEnabled, DUIResources.pageControlPanelButtonOrientationManagerEnabled),
                GetButtonStyle(ControlPanel.PageControlPanelButtonOrientationManagerDisabled, DUIResources.pageControlPanelButtonOrientationManagerDisabled),
                GetButtonStyle(ControlPanel.PageControlPanelButtonNavigationSystemEnabled, DUIResources.pageControlPanelButtonNavigationSystemEnabled),
                GetButtonStyle(ControlPanel.PageControlPanelButtonNavigationSystemDisabled, DUIResources.pageControlPanelButtonNavigationSystemDisabled),

                GetButtonStyle(ControlPanel.PageControlPanelButtonEzDefineSymbols, DUIResources.pageControlPanelButtonEzDefineSymbols),
                GetButtonStyle(ControlPanel.PageControlPanelButtonEzBind, DUIResources.pageControlPanelButtonEzBind),
                GetButtonStyle(ControlPanel.PageControlPanelButtonEzDataManager, DUIResources.pageControlPanelButtonEzDataManager),
                GetButtonStyle(ControlPanel.PageControlPanelButtonEzAds, DUIResources.pageControlPanelButtonEzAds),
                GetButtonStyle(ControlPanel.PageControlPanelButtonPooly, DUIResources.pageControlPanelButtonPooly),

                GetButtonStyle(ControlPanel.PageHelpApi, DUIResources.pageHelpButtonApi),
                GetButtonStyle(ControlPanel.PageHelpForum, DUIResources.pageHelpButtonForum),
                GetButtonStyle(ControlPanel.PageHelpManual, DUIResources.pageHelpButtonManual),
                GetButtonStyle(ControlPanel.PageHelpMail, DUIResources.pageHelpButtonMail)
            };
            return styles;
        }
        private static GUIStyle GetButtonStyle(ControlPanel styleName, QTexture qTexture)
        {
            return new GUIStyle()
            {
                name = styleName.ToString(),
                normal = { background = qTexture.normal2D },
                onNormal = { background = qTexture.normal2D },
                hover = { background = qTexture.hover2D },
                onHover = { background = qTexture.hover2D },
                active = { background = qTexture.active2D },
                onActive = { background = qTexture.active2D }
            };
        }

        private static List<GUIStyle> UpgradeManagerStyles()
        {
            List<GUIStyle> styles = new List<GUIStyle>
            {
                GetButtonStyle(UpgradeManager.ButtonUpgradeScene, DUIResources.upgradeManagerButtonUpgradeScene),
                GetButtonStyle(UpgradeManager.ButtonCleanFiles, DUIResources.upgradeManagerButtonCleanFiles),
                GetButtonStyle(UpgradeManager.ButtonDeleteExamples, DUIResources.upgradeManagerButtonDeleteExamples)
            };
            return styles;
        }
        private static GUIStyle GetButtonStyle(UpgradeManager styleName, QTexture qTexture)
        {
            return new GUIStyle()
            {
                name = styleName.ToString(),
                normal = { background = qTexture.normal2D },
                onNormal = { background = qTexture.normal2D },
                hover = { background = qTexture.hover2D },
                onHover = { background = qTexture.hover2D },
                active = { background = qTexture.active2D },
                onActive = { background = qTexture.active2D }
            };
        }

        private static List<GUIStyle> NotificationWindowStyles()
        {
            List<GUIStyle> styles = new List<GUIStyle>
            {
                GetBlackTextStyle(NotificationWindow.NotificationTitleBlack, TextAnchor.MiddleRight, FontStyle.Normal, 20, DUIResources.Sansation),
                GetBlackTextStyle(NotificationWindow.NotificationMessageBlack, TextAnchor.MiddleCenter, FontStyle.Normal, 12, DUIResources.Sansation),
                GetButtonStyle(NotificationWindow.NotificationButtonOk, DUIResources.notificationWindowButtonOk),
                GetButtonStyle(NotificationWindow.NotificationButtonCancel, DUIResources.notificationWindowButtonCancel),
                GetButtonStyle(NotificationWindow.NotificationButtonYes, DUIResources.notificationWindowButtonYes),
                GetButtonStyle(NotificationWindow.NotificationButtonNo, DUIResources.notificationWindowButtonNo),
                GetButtonStyle(NotificationWindow.NotificationButtonContinue, DUIResources.notificationWindowButtonContinue)
            };
            return styles;
        }
        private static GUIStyle GetButtonStyle(NotificationWindow styleName, QTexture qTexture)
        {
            return new GUIStyle()
            {
                name = styleName.ToString(),
                normal = { background = qTexture.normal2D },
                onNormal = { background = qTexture.normal2D },
                hover = { background = qTexture.hover2D },
                onHover = { background = qTexture.hover2D },
                active = { background = qTexture.active2D },
                onActive = { background = qTexture.active2D }
            };
        }
        private static GUIStyle GetBlackTextStyle(NotificationWindow styleName, TextAnchor alignment, FontStyle fontStyle, int fontSize, Font font = null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.name = styleName.ToString();
            style.normal.textColor = QuickEngine.Extensions.ColorExtensions.ColorFrom256(29, 24, 25);
            style.alignment = alignment;
            style.fontStyle = fontStyle;
            style.fontSize = fontSize;
            style.font = font;
            style.wordWrap = true;
            style.richText = true;
            return style;
        }
    }
}
