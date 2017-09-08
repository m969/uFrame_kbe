// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;

namespace DoozyUI
{
    [CustomEditor(typeof(UIElement), true)]
    [DisallowMultipleComponent]
    [CanEditMultipleObjects]
    public class UIElementEditor : QEditor
    {
        UIElement uiElement { get { return (UIElement)target; } }

        SerializedProperty
            linkedToNotification, autoRegister,
            elementCategory,
            elementName,
            LANDSCAPE, PORTRAIT,
            startHidden, animateAtStart, disableWhenHidden,
            useCustomStartAnchoredPosition, customStartAnchoredPosition,
            executeLayoutFix,
            selectedButton,

            inAnimations,
            OnInAnimationsStart, OnInAnimationsFinish,
            inAnimationsSoundAtStart, customInAnimationsSoundAtStart,
            inAnimationsSoundAtFinish, customInAnimationsSoundAtFinish,
            inAnimationsPresetCategoryName, inAnimationsPresetName, loadInAnimationsPresetAtRuntime,
            moveInEnabled, moveInMoveDirection, moveInCustomPosition, moveInEaseType, moveInEase, moveInAnimationCurve, moveInStartDelay, moveInDuration,
            rotateInEnabled, rotateInRotation, rotateInRotateMode, rotateInEaseType, rotateInEase, rotateInAnimationCurve, rotateInStartDelay, rotateInDuration,
            scaleInEnabled, scaleInScale, scaleInEaseType, scaleInEase, scaleInAnimationCurve, scaleInStartDelay, scaleInDuration,
            fadeInEnabled, fadeInAlpha, fadeInEaseType, fadeInEase, fadeInAnimationCurve, fadeInStartDelay, fadeInDuration,

            outAnimations,
            OnOutAnimationsStart, OnOutAnimationsFinish,
            outAnimationsSoundAtStart, customOutAnimationsSoundAtStart,
            outAnimationsSoundAtFinish, customOutAnimationsSoundAtFinish,
            outAnimationsPresetCategoryName, outAnimationsPresetName, loadOutAnimationsPresetAtRuntime,
            moveOutEnabled, moveOutMoveDirection, moveOutCustomPosition, moveOutEaseType, moveOutEase, moveOutAnimationCurve, moveOutStartDelay, moveOutDuration,
            rotateOutEnabled, rotateOutRotation, rotateOutRotateMode, rotateOutEaseType, rotateOutEase, rotateOutAnimationCurve, rotateOutStartDelay, rotateOutDuration,
            scaleOutEnabled, scaleOutScale, scaleOutEaseType, scaleOutEase, scaleOutAnimationCurve, scaleOutStartDelay, scaleOutDuration,
            fadeOutEnabled, fadeOutAlpha, fadeOutEaseType, fadeOutEase, fadeOutAnimationCurve, fadeOutStartDelay, fadeOutDuration,

            loopAnimations,
            loopAnimationsAutoStart,
            loopAnimationsPresetCategoryName, loopAnimationsPresetName, loadLoopAnimationsPresetAtRuntime,
            moveLoopEnabled, moveLoopMovement, moveLoopEaseType, moveLoopEase, moveLoopAnimationCurve, moveLoopLoops, moveLoopLoopType, moveLoopStartDelay, moveLoopDuration,
            rotateLoopEnabled, rotateLoopRotation, rotateLoopEaseType, rotateLoopEase, rotateLoopAnimationCurve, rotateLoopLoops, rotateLoopLoopType, rotateLoopStartDelay, rotateLoopDuration,
            scaleLoopEnabled, scaleLoopMin, scaleLoopMax, scaleLoopEaseType, scaleLoopEase, scaleLoopAnimationCurve, scaleLoopLoops, scaleLoopLoopType, scaleLoopStartDelay, scaleLoopDuration,
            fadeLoopEnabled, fadeLoopMin, fadeLoopMax, fadeLoopEaseType, fadeLoopEase, fadeLoopAnimationCurve, fadeLoopLoops, fadeLoopLoopType, fadeLoopStartDelay, fadeLoopDuration;

        AnimBool
            showPlayModeSettings,
            showCustomStartPosition,
            showInAnimations, showInAnimationsEvents, showInAnimationsPreset, showMoveIn, showRotateIn, showScaleIn, showFadeIn,
            showOutAnimations, showOutAnimationsEvents, showOutAnimationsPreset, showMoveOut, showRotateOut, showScaleOut, showFadeOut,
            showLoopAnimations, showLoopAnimationsPreset, showMoveLoop, showRotateLoop, showScaleLoop, showFadeLoop;

        bool localShowHide = false;

        int elementNameIndex = 0;
        int elementCategoryIndex = 0;

        int inAnimationsSoundAtStartIndex, inAnimationsSoundAtFinishIndex;
        int outAnimationsSoundAtStartIndex, outAnimationsSoundAtFinishIndex;

        string newPresetCategoryName = "";
        string newPresetName = "";

        int inAnimationsPresetCategoryNameIndex;
        int inAnimationsPresetNameIndex;
        bool inAnimationsNewPreset = false;
        bool inAnimationsNewCategoryName = false;

        int outAnimationsPresetCategoryNameIndex;
        int outAnimationsPresetNameIndex;
        bool outAnimationsNewPreset = false;
        bool outAnimationsNewCategoryName = false;

        int loopAnimationsPresetCategoryNameIndex;
        int loopAnimationsPresetNameIndex;
        bool loopAnimationsNewPreset = false;
        bool loopAnimationsNewCategoryName = false;

        bool ControlPanelSelected = false;
        bool refreshData = true;

        void SerializedObjectFindProperties()
        {
            linkedToNotification = serializedObject.FindProperty("linkedToNotification");
            autoRegister = serializedObject.FindProperty("autoRegister");

            elementCategory = serializedObject.FindProperty("elementCategory");

            elementName = serializedObject.FindProperty("elementName");

            LANDSCAPE = serializedObject.FindProperty("LANDSCAPE");
            PORTRAIT = serializedObject.FindProperty("PORTRAIT");

            startHidden = serializedObject.FindProperty("startHidden");
            animateAtStart = serializedObject.FindProperty("animateAtStart");
            disableWhenHidden = serializedObject.FindProperty("disableWhenHidden");
            useCustomStartAnchoredPosition = serializedObject.FindProperty("useCustomStartAnchoredPosition");
            customStartAnchoredPosition = serializedObject.FindProperty("customStartAnchoredPosition");
            executeLayoutFix = serializedObject.FindProperty("executeLayoutFix");
            selectedButton = serializedObject.FindProperty("selectedButton");

            inAnimations = serializedObject.FindProperty("inAnimations");

            OnInAnimationsStart = serializedObject.FindProperty("OnInAnimationsStart");
            OnInAnimationsFinish = serializedObject.FindProperty("OnInAnimationsFinish");

            inAnimationsSoundAtStart = serializedObject.FindProperty("inAnimationsSoundAtStart");
            customInAnimationsSoundAtStart = serializedObject.FindProperty("customInAnimationsSoundAtStart");
            inAnimationsSoundAtFinish = serializedObject.FindProperty("inAnimationsSoundAtFinish");
            customInAnimationsSoundAtFinish = serializedObject.FindProperty("customInAnimationsSoundAtFinish");

            inAnimationsPresetCategoryName = serializedObject.FindProperty("inAnimationsPresetCategoryName");
            inAnimationsPresetName = serializedObject.FindProperty("inAnimationsPresetName");
            loadInAnimationsPresetAtRuntime = serializedObject.FindProperty("loadInAnimationsPresetAtRuntime");
            //Move IN
            moveInEnabled = inAnimations.FindPropertyRelative("move").FindPropertyRelative("enabled");
            moveInMoveDirection = inAnimations.FindPropertyRelative("move").FindPropertyRelative("moveDirection");
            moveInCustomPosition = inAnimations.FindPropertyRelative("move").FindPropertyRelative("customPosition");
            moveInEaseType = inAnimations.FindPropertyRelative("move").FindPropertyRelative("easeType");
            moveInEase = inAnimations.FindPropertyRelative("move").FindPropertyRelative("ease");
            moveInAnimationCurve = inAnimations.FindPropertyRelative("move").FindPropertyRelative("animationCurve");
            moveInStartDelay = inAnimations.FindPropertyRelative("move").FindPropertyRelative("startDelay");
            moveInDuration = inAnimations.FindPropertyRelative("move").FindPropertyRelative("duration");
            //Rotate IN
            rotateInEnabled = inAnimations.FindPropertyRelative("rotate").FindPropertyRelative("enabled");
            rotateInRotation = inAnimations.FindPropertyRelative("rotate").FindPropertyRelative("rotation");
            rotateInRotateMode = inAnimations.FindPropertyRelative("rotate").FindPropertyRelative("rotateMode");
            rotateInEaseType = inAnimations.FindPropertyRelative("rotate").FindPropertyRelative("easeType");
            rotateInEase = inAnimations.FindPropertyRelative("rotate").FindPropertyRelative("ease");
            rotateInAnimationCurve = inAnimations.FindPropertyRelative("rotate").FindPropertyRelative("animationCurve");
            rotateInStartDelay = inAnimations.FindPropertyRelative("rotate").FindPropertyRelative("startDelay");
            rotateInDuration = inAnimations.FindPropertyRelative("rotate").FindPropertyRelative("duration");
            //Scale IN
            scaleInEnabled = inAnimations.FindPropertyRelative("scale").FindPropertyRelative("enabled");
            scaleInScale = inAnimations.FindPropertyRelative("scale").FindPropertyRelative("scale");
            scaleInEaseType = inAnimations.FindPropertyRelative("scale").FindPropertyRelative("easeType");
            scaleInEase = inAnimations.FindPropertyRelative("scale").FindPropertyRelative("ease");
            scaleInAnimationCurve = inAnimations.FindPropertyRelative("scale").FindPropertyRelative("animationCurve");
            scaleInStartDelay = inAnimations.FindPropertyRelative("scale").FindPropertyRelative("startDelay");
            scaleInDuration = inAnimations.FindPropertyRelative("scale").FindPropertyRelative("duration");
            //Fade IN
            fadeInEnabled = inAnimations.FindPropertyRelative("fade").FindPropertyRelative("enabled");
            fadeInAlpha = inAnimations.FindPropertyRelative("fade").FindPropertyRelative("alpha");
            fadeInEaseType = inAnimations.FindPropertyRelative("fade").FindPropertyRelative("easeType");
            fadeInEase = inAnimations.FindPropertyRelative("fade").FindPropertyRelative("ease");
            fadeInAnimationCurve = inAnimations.FindPropertyRelative("fade").FindPropertyRelative("animationCurve");
            fadeInStartDelay = inAnimations.FindPropertyRelative("fade").FindPropertyRelative("startDelay");
            fadeInDuration = inAnimations.FindPropertyRelative("fade").FindPropertyRelative("duration");

            outAnimations = serializedObject.FindProperty("outAnimations");

            OnOutAnimationsStart = serializedObject.FindProperty("OnOutAnimationsStart");
            OnOutAnimationsFinish = serializedObject.FindProperty("OnOutAnimationsFinish");

            outAnimationsSoundAtStart = serializedObject.FindProperty("outAnimationsSoundAtStart");
            customOutAnimationsSoundAtStart = serializedObject.FindProperty("customOutAnimationsSoundAtStart");
            outAnimationsSoundAtFinish = serializedObject.FindProperty("outAnimationsSoundAtFinish");
            customOutAnimationsSoundAtFinish = serializedObject.FindProperty("customOutAnimationsSoundAtFinish");

            outAnimationsPresetCategoryName = serializedObject.FindProperty("outAnimationsPresetCategoryName");
            outAnimationsPresetName = serializedObject.FindProperty("outAnimationsPresetName");
            loadOutAnimationsPresetAtRuntime = serializedObject.FindProperty("loadOutAnimationsPresetAtRuntime");
            //Move OUT
            moveOutEnabled = outAnimations.FindPropertyRelative("move").FindPropertyRelative("enabled");
            moveOutMoveDirection = outAnimations.FindPropertyRelative("move").FindPropertyRelative("moveDirection");
            moveOutCustomPosition = outAnimations.FindPropertyRelative("move").FindPropertyRelative("customPosition");
            moveOutEaseType = outAnimations.FindPropertyRelative("move").FindPropertyRelative("easeType");
            moveOutEase = outAnimations.FindPropertyRelative("move").FindPropertyRelative("ease");
            moveOutAnimationCurve = outAnimations.FindPropertyRelative("move").FindPropertyRelative("animationCurve");
            moveOutStartDelay = outAnimations.FindPropertyRelative("move").FindPropertyRelative("startDelay");
            moveOutDuration = outAnimations.FindPropertyRelative("move").FindPropertyRelative("duration");
            //Rotate OUT
            rotateOutEnabled = outAnimations.FindPropertyRelative("rotate").FindPropertyRelative("enabled");
            rotateOutRotation = outAnimations.FindPropertyRelative("rotate").FindPropertyRelative("rotation");
            rotateOutRotateMode = inAnimations.FindPropertyRelative("rotate").FindPropertyRelative("rotateMode");
            rotateOutEaseType = outAnimations.FindPropertyRelative("rotate").FindPropertyRelative("easeType");
            rotateOutEase = outAnimations.FindPropertyRelative("rotate").FindPropertyRelative("ease");
            rotateOutAnimationCurve = outAnimations.FindPropertyRelative("rotate").FindPropertyRelative("animationCurve");
            rotateOutStartDelay = outAnimations.FindPropertyRelative("rotate").FindPropertyRelative("startDelay");
            rotateOutDuration = outAnimations.FindPropertyRelative("rotate").FindPropertyRelative("duration");
            //Scale OUT
            scaleOutEnabled = outAnimations.FindPropertyRelative("scale").FindPropertyRelative("enabled");
            scaleOutScale = outAnimations.FindPropertyRelative("scale").FindPropertyRelative("scale");
            scaleOutEaseType = outAnimations.FindPropertyRelative("scale").FindPropertyRelative("easeType");
            scaleOutEase = outAnimations.FindPropertyRelative("scale").FindPropertyRelative("ease");
            scaleOutAnimationCurve = outAnimations.FindPropertyRelative("scale").FindPropertyRelative("animationCurve");
            scaleOutStartDelay = outAnimations.FindPropertyRelative("scale").FindPropertyRelative("startDelay");
            scaleOutDuration = outAnimations.FindPropertyRelative("scale").FindPropertyRelative("duration");
            //Fade OUT
            fadeOutEnabled = outAnimations.FindPropertyRelative("fade").FindPropertyRelative("enabled");
            fadeOutAlpha = outAnimations.FindPropertyRelative("fade").FindPropertyRelative("alpha");
            fadeOutEaseType = outAnimations.FindPropertyRelative("fade").FindPropertyRelative("easeType");
            fadeOutEase = outAnimations.FindPropertyRelative("fade").FindPropertyRelative("ease");
            fadeOutAnimationCurve = outAnimations.FindPropertyRelative("fade").FindPropertyRelative("animationCurve");
            fadeOutStartDelay = outAnimations.FindPropertyRelative("fade").FindPropertyRelative("startDelay");
            fadeOutDuration = outAnimations.FindPropertyRelative("fade").FindPropertyRelative("duration");

            loopAnimations = serializedObject.FindProperty("loopAnimations");
            loopAnimationsAutoStart = loopAnimations.FindPropertyRelative("autoStart");
            loopAnimationsPresetCategoryName = serializedObject.FindProperty("loopAnimationsPresetCategoryName");
            loopAnimationsPresetName = serializedObject.FindProperty("loopAnimationsPresetName");
            loadLoopAnimationsPresetAtRuntime = serializedObject.FindProperty("loadLoopAnimationsPresetAtRuntime");
            //Move LOOP
            moveLoopEnabled = loopAnimations.FindPropertyRelative("move").FindPropertyRelative("enabled");
            moveLoopMovement = loopAnimations.FindPropertyRelative("move").FindPropertyRelative("movement");
            moveLoopEaseType = loopAnimations.FindPropertyRelative("move").FindPropertyRelative("easeType");
            moveLoopEase = loopAnimations.FindPropertyRelative("move").FindPropertyRelative("ease");
            moveLoopAnimationCurve = loopAnimations.FindPropertyRelative("move").FindPropertyRelative("animationCurve");
            moveLoopLoops = loopAnimations.FindPropertyRelative("move").FindPropertyRelative("loops");
            moveLoopLoopType = loopAnimations.FindPropertyRelative("move").FindPropertyRelative("loopType");
            moveLoopStartDelay = loopAnimations.FindPropertyRelative("move").FindPropertyRelative("startDelay");
            moveLoopDuration = loopAnimations.FindPropertyRelative("move").FindPropertyRelative("duration");
            //Rotate LOOP
            rotateLoopEnabled = loopAnimations.FindPropertyRelative("rotate").FindPropertyRelative("enabled");
            rotateLoopRotation = loopAnimations.FindPropertyRelative("rotate").FindPropertyRelative("rotation");
            rotateLoopEaseType = loopAnimations.FindPropertyRelative("rotate").FindPropertyRelative("easeType");
            rotateLoopEase = loopAnimations.FindPropertyRelative("rotate").FindPropertyRelative("ease");
            rotateLoopAnimationCurve = loopAnimations.FindPropertyRelative("rotate").FindPropertyRelative("animationCurve");
            rotateLoopLoops = loopAnimations.FindPropertyRelative("rotate").FindPropertyRelative("loops");
            rotateLoopLoopType = loopAnimations.FindPropertyRelative("rotate").FindPropertyRelative("loopType");
            rotateLoopStartDelay = loopAnimations.FindPropertyRelative("rotate").FindPropertyRelative("startDelay");
            rotateLoopDuration = loopAnimations.FindPropertyRelative("rotate").FindPropertyRelative("duration");
            //Scale LOOP
            scaleLoopEnabled = loopAnimations.FindPropertyRelative("scale").FindPropertyRelative("enabled");
            scaleLoopMin = loopAnimations.FindPropertyRelative("scale").FindPropertyRelative("min");
            scaleLoopMax = loopAnimations.FindPropertyRelative("scale").FindPropertyRelative("max");
            scaleLoopEaseType = loopAnimations.FindPropertyRelative("scale").FindPropertyRelative("easeType");
            scaleLoopEase = loopAnimations.FindPropertyRelative("scale").FindPropertyRelative("ease");
            scaleLoopAnimationCurve = loopAnimations.FindPropertyRelative("scale").FindPropertyRelative("animationCurve");
            scaleLoopLoops = loopAnimations.FindPropertyRelative("scale").FindPropertyRelative("loops");
            scaleLoopLoopType = loopAnimations.FindPropertyRelative("scale").FindPropertyRelative("loopType");
            scaleLoopStartDelay = loopAnimations.FindPropertyRelative("scale").FindPropertyRelative("startDelay");
            scaleLoopDuration = loopAnimations.FindPropertyRelative("scale").FindPropertyRelative("duration");
            //Fade LOOP
            fadeLoopEnabled = loopAnimations.FindPropertyRelative("fade").FindPropertyRelative("enabled");
            fadeLoopMin = loopAnimations.FindPropertyRelative("fade").FindPropertyRelative("min");
            fadeLoopMax = loopAnimations.FindPropertyRelative("fade").FindPropertyRelative("max");
            fadeLoopEaseType = loopAnimations.FindPropertyRelative("fade").FindPropertyRelative("easeType");
            fadeLoopEase = loopAnimations.FindPropertyRelative("fade").FindPropertyRelative("ease");
            fadeLoopAnimationCurve = loopAnimations.FindPropertyRelative("fade").FindPropertyRelative("animationCurve");
            fadeLoopLoops = loopAnimations.FindPropertyRelative("fade").FindPropertyRelative("loops");
            fadeLoopLoopType = loopAnimations.FindPropertyRelative("fade").FindPropertyRelative("loopType");
            fadeLoopStartDelay = loopAnimations.FindPropertyRelative("fade").FindPropertyRelative("startDelay");
            fadeLoopDuration = loopAnimations.FindPropertyRelative("fade").FindPropertyRelative("duration");
        }

        void GenerateInfoMessages()
        {
            infoMessage = new Dictionary<string, InfoMessage>();
            infoMessage.Add("InAnimationsDisabled", new InfoMessage() { title = "Disabled", message = "Enable at least one In Animation for SHOW to work.", type = InfoMessageType.Warning, show = new AnimBool(false, Repaint) });
            infoMessage.Add("OutAnimationsDisabled", new InfoMessage() { title = "Disabled", message = "Enable at least one Out Animation for HIDE to work.", type = InfoMessageType.Warning, show = new AnimBool(false, Repaint) });
            infoMessage.Add("InAnimationsLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = inAnimationsPresetCategoryName.stringValue + " / " + inAnimationsPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadInAnimationsPresetAtRuntime.boolValue, Repaint) });
            infoMessage.Add("OutAnimationsLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = outAnimationsPresetCategoryName.stringValue + " / " + outAnimationsPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadOutAnimationsPresetAtRuntime.boolValue, Repaint) });
            infoMessage.Add("LoopAnimationsLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = loopAnimationsPresetCategoryName.stringValue + " / " + loopAnimationsPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadLoopAnimationsPresetAtRuntime.boolValue, Repaint) });
        }

        void InitAnimBools()
        {
            showPlayModeSettings = new AnimBool(false, Repaint);

            showCustomStartPosition = new AnimBool(useCustomStartAnchoredPosition.boolValue, Repaint);

            showInAnimations = new AnimBool(false, Repaint);
            showInAnimationsEvents = new AnimBool(false, Repaint);
            showInAnimationsPreset = new AnimBool(loadInAnimationsPresetAtRuntime.boolValue, Repaint);
            showMoveIn = new AnimBool(moveInEnabled.boolValue, Repaint);
            showRotateIn = new AnimBool(rotateInEnabled.boolValue, Repaint);
            showScaleIn = new AnimBool(scaleInEnabled.boolValue, Repaint);
            showFadeIn = new AnimBool(fadeInEnabled.boolValue, Repaint);

            showOutAnimations = new AnimBool(false, Repaint);
            showOutAnimationsEvents = new AnimBool(false, Repaint);
            showOutAnimationsPreset = new AnimBool(loadOutAnimationsPresetAtRuntime.boolValue, Repaint);
            showMoveOut = new AnimBool(moveOutEnabled.boolValue, Repaint);
            showRotateOut = new AnimBool(rotateOutEnabled.boolValue, Repaint);
            showScaleOut = new AnimBool(scaleOutEnabled.boolValue, Repaint);
            showFadeOut = new AnimBool(fadeOutEnabled.boolValue, Repaint);

            showLoopAnimations = new AnimBool(false, Repaint);
            showLoopAnimationsPreset = new AnimBool(loadLoopAnimationsPresetAtRuntime.boolValue, Repaint);
            showMoveLoop = new AnimBool(moveLoopEnabled.boolValue, Repaint);
            showRotateLoop = new AnimBool(rotateLoopEnabled.boolValue, Repaint);
            showScaleLoop = new AnimBool(scaleLoopEnabled.boolValue, Repaint);
            showFadeLoop = new AnimBool(fadeLoopEnabled.boolValue, Repaint);
        }

        protected override void OnEnable()
        {
            requiresContantRepaint = true;
            SerializedObjectFindProperties();
            GenerateInfoMessages();
            InitAnimBools();
            AddMissingComponents();
            CheckIfLinkedToNotification();
        }

        void AddMissingComponents()
        {
            if (uiElement.GetComponent<Canvas>() == null) { uiElement.gameObject.AddComponent<Canvas>(); }
            if (uiElement.GetComponent<CanvasGroup>() == null) { uiElement.gameObject.AddComponent<CanvasGroup>(); }
            if (uiElement.GetComponent<GraphicRaycaster>() == null) { uiElement.gameObject.AddComponent<GraphicRaycaster>(); }
        }

        void RefreshData(bool forcedRefresh = false)
        {
            serializedObject.Update();
            RefreshElementNameAndCategory(forcedRefresh);
            RefreshUISounds(forcedRefresh);
            RefreshInAnimations(forcedRefresh);
            RefreshOutAnimations(forcedRefresh);
            RefreshLoopAnimations(forcedRefresh);
            serializedObject.ApplyModifiedProperties();
        }
        void RefreshElementNameAndCategory(bool forcedRefresh)
        {
            RefreshUIElementsDatabase(forcedRefresh);
            ValidateElementCategoryAndElementName();
        }
        void RefreshUISounds(bool forcedRefresh)
        {
            RefreshUISoundsDatabase(forcedRefresh);
            ValidateInAnimationsSounds();
            ValidateOutAnimationsSounds();
        }
        void RefreshInAnimations(bool forcedRefresh)
        {
            RefreshInAnimationsPresets(forcedRefresh);
            ValidateInAnimationsPreset();
        }
        void RefreshOutAnimations(bool forcedRefresh)
        {
            RefreshOutAnimationsPresets(forcedRefresh);
            ValidateOutAnimationsPreset();
        }
        void RefreshLoopAnimations(bool forcedRefresh)
        {
            RefreshLoopAnimationsPresets(forcedRefresh);
            ValidateLoopAnimationsPreset();
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerUIElement.normal, WIDTH_420, HEIGHT_42);
            if (refreshData) //refresh needs to be executed this way because OnEnable is called 3 times when entering PlayMode, thus adding a lot of wait time for the developer (that is unacceptable); until we figure out why that happends, this solution will have to do.
            {
                RefreshData();
                refreshData = false;
            }
            if (!ControlPanelWindow.Selected && ControlPanelSelected)
            {
                RefreshData();
                ControlPanelSelected = false;
            }
            else if (ControlPanelWindow.Selected && !ControlPanelSelected)
            {
                ControlPanelSelected = true;
            }
            serializedObject.Update();
            DrawPlayModeSettings();
            if (linkedToNotification.boolValue)
            {
                if (!showPlayModeSettings.value)
                {
                    QUI.Space(-SPACE_4);
                }
                else
                {
                    QUI.Space(SPACE_2);
                }
                DrawLinkedToNotification();
                QUI.Space(SPACE_2);
                DrawTopButtons();
            }
            else
            {
                DrawTopButtons();
                DrawCategory();
                DrawElementName();
            }
            DrawOrientation();
            DrawSettings();
            DrawInAnimations();
            //DrawSpecialAnimationsButtons();
            DrawOutAnimations();
            QUI.Space(-0.5f);
            DrawLoopAnimations();
            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }

        void DrawTopButtons()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button("UIElements Database"))
                {
                    ControlPanelWindow.Open(ControlPanelWindow.Section.UIElements);
                }
                if (QUI.Button("UISounds Database"))
                {
                    ControlPanelWindow.Open(ControlPanelWindow.Section.UISounds);
                }
                if (QUI.Button("Refresh Data"))
                {
                    RefreshData(true);
                }
            }
            QUI.EndHorizontal();
            DrawRenameGameObjectButton();
            QUI.Space(SPACE_2);
        }
        void DrawRenameGameObjectButton()
        {
            if (DUI.DUISettings.UIElement_Inspector_ShowButtonRenameGameObject && !linkedToNotification.boolValue)
            {
                QUI.BeginHorizontal(WIDTH_420);
                {
                    if (QUI.Button("Rename GameObject to Element Name"))
                    {
                        if (serializedObject.isEditingMultipleObjects)
                        {
                            Undo.RecordObjects(targets, "Renamed Multiple Objects");
                            for (int i = 0; i < targets.Length; i++)
                            {
                                UIElement iTarget = (UIElement)targets[i];
                                iTarget.gameObject.name = DUI.DUISettings.UIElement_Inspector_RenameGameObjectPrefix + iTarget.elementName + DUI.DUISettings.UIElement_Inspector_RenameGameObjectSuffix;
                            }
                        }
                        else
                        {
                            uiElement.gameObject.name = DUI.DUISettings.UIElement_Inspector_RenameGameObjectPrefix + elementName.stringValue + DUI.DUISettings.UIElement_Inspector_RenameGameObjectSuffix;
                        }
                    }
                }
                QUI.EndHorizontal();
            }
        }
        void DrawPlayModeSettings()
        {
            showPlayModeSettings.target = EditorApplication.isPlayingOrWillChangePlaymode;
            if (QUI.BeginFadeGroup(showPlayModeSettings.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_4);
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.BeginVertical(280);
                        {
                            QUI.Space(-0.5f);
                            QUI.BeginHorizontal(280);
                            {
                                if (QUI.Button("SHOW", 140, 20)) { if (localShowHide) { uiElement.Show(false); } else { UIManager.ShowUiElement(elementName.stringValue, elementCategory.stringValue, false); } }
                                if (QUI.Button("Hide", 140, 20)) { if (localShowHide) { uiElement.Hide(false); } else { UIManager.HideUiElement(elementName.stringValue, elementCategory.stringValue, false); } }
                            }
                            QUI.EndHorizontal();
                        }
                        QUI.EndVertical();
                        QUI.BeginVertical(112);
                        {
                            QUI.Space(1f);
                            QUI.BeginHorizontal(112);
                            {
                                localShowHide = QUI.Toggle(localShowHide);
                                QUI.Label("local Show/Hide", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 100);
                            }
                            QUI.EndHorizontal();
                        }
                        QUI.EndVertical();
                    }
                    QUI.EndVertical();
                    QUI.Space(SPACE_4);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }

        void DrawLinkedToNotification()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) { return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.DrawTexture(DUIResources.barLinkedToUINotification.texture, 315, 42);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.Unlink), 105, 42))
                {
                    UnlinkFromNotification();
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
        }

        void DrawCategory()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Element Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    QUI.Label(elementCategory.stringValue, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic));
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        elementCategoryIndex = EditorGUILayout.Popup(elementCategoryIndex, DUI.UIElementCategories.ToArray());
                    }
                    if (QUI.EndChangeCheck())
                    {
                        if (!DUI.UIElementCategories[elementCategoryIndex].Equals(DUI.CUSTOM_NAME)) //not custom name category?
                        {
                            if (DUI.UIElementNameExists(DUI.UIElementCategories[elementCategoryIndex], elementName.stringValue)) //does the new category have the name?
                            {
                                elementCategory.stringValue = DUI.UIElementCategories[elementCategoryIndex];
                                elementNameIndex = DUI.GetUIElementNames(elementCategory.stringValue).IndexOf(elementName.stringValue);
                            }
                            else if (elementName.stringValue.Equals(DUI.DEFAULT_ELEMENT_NAME) && DUI.GetUIElementNames(DUI.UIElementCategories[elementCategoryIndex]).Count > 0) //is the element name the default value? || is the new category not empty? -> set element name as the first value
                            {
                                elementCategory.stringValue = DUI.UIElementCategories[elementCategoryIndex];
                                elementName.stringValue = DUI.GetUIElementNames(elementCategory.stringValue)[0];
                            }
                            else if (!elementName.stringValue.Equals(DUI.DEFAULT_ELEMENT_NAME) && !string.IsNullOrEmpty(elementName.stringValue.Trim()) && EditorUtility.DisplayDialog("Action Required", "The '" + elementName.stringValue + "' element name does not exist in the '" + DUI.UIElementCategories[elementCategoryIndex] + "' category database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.AddUIElementName(DUI.UIElementCategories[elementCategoryIndex], elementName.stringValue);
                                elementCategory.stringValue = DUI.UIElementCategories[elementCategoryIndex];
                                elementNameIndex = DUI.GetUIElementNames(elementCategory.stringValue).IndexOf(elementName.stringValue);
                            }
                            else if (DUI.GetUIElementNames(DUI.UIElementCategories[elementCategoryIndex]).Count == 0)
                            {
                                if (EditorUtility.DisplayDialog("Information", "The '" + DUI.UIElementCategories[elementCategoryIndex] + "' category is empty.\n\nOpen the UIElements Database and add some element names to it or delete it.\n\nThe element name and category will now be reset to the default values.", "Ok"))
                                {
                                    elementCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                                    elementCategoryIndex = DUI.UIElementCategories.IndexOf(elementCategory.stringValue);
                                    elementName.stringValue = DUI.DEFAULT_BUTTON_NAME;
                                    elementNameIndex = DUI.GetUIElementNames(elementCategory.stringValue).IndexOf(elementName.stringValue);
                                }
                            }
                            else
                            {
                                elementCategory.stringValue = DUI.UIElementCategories[elementCategoryIndex];
                                elementName.stringValue = DUI.GetUIElementNames(elementCategory.stringValue)[0];
                                elementNameIndex = DUI.GetUIElementNames(elementCategory.stringValue).IndexOf(elementName.stringValue);
                            }
                        }
                        else
                        {
                            elementCategory.stringValue = DUI.UIElementCategories[elementCategoryIndex];
                        }
                    }
                }
            }
            QUI.EndHorizontal();
        }
        void DrawElementName()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Element Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    QUI.Label(elementName.stringValue, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic));
                }
                else
                {
                    if (elementCategory.stringValue.Equals(DUI.CUSTOM_NAME))
                    {
                        QUI.PropertyField(elementName);

                    }
                    else
                    {
                        if (DUI.GetUIElementNames(DUI.UIElementCategories[elementCategoryIndex]).Count == 0)
                        {
                            QUI.Label(elementName.stringValue, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic), 90);
                            QUI.FlexibleSpace();
                            QUI.Label("(Empty Category)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmallItalic), 86);
                        }
                        else
                        {
                            QUI.BeginChangeCheck();
                            {
                                if (!DUI.UIElementCategoryExists(elementCategory.stringValue)) { RefreshElementNameAndCategory(true); }
                                elementNameIndex = EditorGUILayout.Popup(elementNameIndex, DUI.GetUIElementNames(elementCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                elementName.stringValue = DUI.GetUIElementNames(elementCategory.stringValue)[elementNameIndex];
                            }
                        }
                    }
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
        }
        void DrawOrientation()
        {
            if (!UIManager.useOrientationManager) { return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Orientation", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                QUI.PropertyField(LANDSCAPE, 12);
                QUI.Label("LANDSCAPE", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                QUI.Space(SPACE_4);
                QUI.PropertyField(PORTRAIT, 12);
                QUI.Label("PORTRAIT", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
        }
        void DrawSettings()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.PropertyField(startHidden, 12);
                QUI.Label("hide @START", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 88);
                QUI.PropertyField(animateAtStart, 12);
                QUI.Label("animate @START", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 110);
                QUI.PropertyField(disableWhenHidden, 12);
                QUI.Label("disable when hidden", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 210);
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.PropertyField(useCustomStartAnchoredPosition, 12);
                QUI.Label("custom start position", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 120);
                showCustomStartPosition.target = useCustomStartAnchoredPosition.boolValue;
                if (QUI.BeginFadeGroup(showCustomStartPosition.faded))
                {
                    QUI.BeginVertical(276);
                    {
                        QUI.Space(SPACE_2);
                        QUI.PropertyField(customStartAnchoredPosition, 276);
                        QUI.BeginHorizontal(276);
                        {
                            QUI.Space(16);
                            if (QUI.Button("GET", 80)) { customStartAnchoredPosition.vector3Value = uiElement.RectTransform.anchoredPosition3D; }
                            QUI.Space(10);
                            if (QUI.Button("SET", 78)) { uiElement.RectTransform.anchoredPosition3D = customStartAnchoredPosition.vector3Value; }
                            QUI.Space(10);
                            if (QUI.Button("RESET", 78)) { customStartAnchoredPosition.vector3Value = Vector3.zero; }
                        }
                        QUI.EndHorizontal();
                    }
                    QUI.EndVertical();
                }
                QUI.EndFadeGroup();
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.PropertyField(executeLayoutFix, 12);
                QUI.Label("execute layout fix (useful in some cases)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal));
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("auto selected button after show", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 180);
                QUI.PropertyField(selectedButton, 238);
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
        }

        void DrawInAnimations()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button(DUIStyles.GetStyle(showInAnimations.target ? DUIStyles.ButtonStyle.InAnimations : DUIStyles.ButtonStyle.InAnimationsCollapsed), 336, 21)) { showInAnimations.target = !showInAnimations.target; }
                if (QUI.Button(DUIStyles.GetStyle(moveInEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonMove : DUIStyles.ButtonStyle.BarButtonMoveDisabled), 21, 21)) { moveInEnabled.boolValue = !moveInEnabled.boolValue; if (moveInEnabled.boolValue) { showInAnimations.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(rotateInEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonRotate : DUIStyles.ButtonStyle.BarButtonRotateDisabled), 21, 21)) { rotateInEnabled.boolValue = !rotateInEnabled.boolValue; if (rotateInEnabled.boolValue) { showInAnimations.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(scaleInEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonScale : DUIStyles.ButtonStyle.BarButtonScaleDisabled), 21, 21)) { scaleInEnabled.boolValue = !scaleInEnabled.boolValue; if (scaleInEnabled.boolValue) { showInAnimations.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(fadeInEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonFade : DUIStyles.ButtonStyle.BarButtonFadeDisabled), 21, 21)) { fadeInEnabled.boolValue = !fadeInEnabled.boolValue; if (fadeInEnabled.boolValue) { showInAnimations.target = true; } }
            }
            QUI.EndHorizontal();
            infoMessage["InAnimationsDisabled"].show.target = !uiElement.InAnimationsEnabled && !loadInAnimationsPresetAtRuntime.boolValue;
            DrawInfoMessage("InAnimationsDisabled", WIDTH_420);
            infoMessage["InAnimationsLoadPresetAtRuntime"].show.target = loadInAnimationsPresetAtRuntime.boolValue;
            infoMessage["InAnimationsLoadPresetAtRuntime"].message = inAnimationsPresetCategoryName.stringValue + " / " + inAnimationsPresetName.stringValue;
            DrawInfoMessage("InAnimationsLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (QUI.BeginFadeGroup(showInAnimations.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    DrawInAnimationsEvents();
                    QUI.Space(SPACE_8);
                    DrawInAnimationsSounds();
                    QUI.Space(SPACE_8);
                    DrawInAnimationsPreset();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(moveInEnabled.boolValue ? DUIStyles.ButtonStyle.Move : DUIStyles.ButtonStyle.MoveDisabled), WIDTH_420, 18)) { moveInEnabled.boolValue = !moveInEnabled.boolValue; }
                    DrawMoveIn();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(rotateInEnabled.boolValue ? DUIStyles.ButtonStyle.Rotate : DUIStyles.ButtonStyle.RotateDisabled), WIDTH_420, 18)) { rotateInEnabled.boolValue = !rotateInEnabled.boolValue; }
                    DrawRotateIn();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(scaleInEnabled.boolValue ? DUIStyles.ButtonStyle.Scale : DUIStyles.ButtonStyle.ScaleDisabled), WIDTH_420, 18)) { scaleInEnabled.boolValue = !scaleInEnabled.boolValue; }
                    DrawScaleIn();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(fadeInEnabled.boolValue ? DUIStyles.ButtonStyle.Fade : DUIStyles.ButtonStyle.FadeDisabled), WIDTH_420, 18)) { fadeInEnabled.boolValue = !fadeInEnabled.boolValue; }
                    DrawFadeIn();
                    QUI.Space(SPACE_16);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawInAnimationsEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showInAnimationsEvents.target ? DUIStyles.ButtonStyle.Events : DUIStyles.ButtonStyle.EventsCollapsed), WIDTH_420, 18)) { showInAnimationsEvents.target = !showInAnimationsEvents.target; }
            if (QUI.BeginFadeGroup(showInAnimationsEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.PropertyField(OnInAnimationsStart, new GUIContent() { text = "OnInAnimationsStart" }, WIDTH_420 - 6);
                    QUI.Space(SPACE_2);
                    QUI.PropertyField(OnInAnimationsFinish, new GUIContent() { text = "OnInAnimationsFinish" }, WIDTH_420 - 6);
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawInAnimationsSounds()
        {
            //@START
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("sound @START", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 95);
                if (customInAnimationsSoundAtStart.boolValue)
                {
                    QUI.PropertyField(inAnimationsSoundAtStart, 200);
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        inAnimationsSoundAtStartIndex = EditorGUILayout.Popup(inAnimationsSoundAtStartIndex, DUI.UISoundNamesUIElements.ToArray(), GUILayout.Width(200));
                    }
                    if (QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(uiElement, "Updated Play Sound");
                        inAnimationsSoundAtStart.stringValue = DUI.UISoundNamesUIElements[inAnimationsSoundAtStartIndex];
                    }
                }
                QUI.Space(SPACE_4);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(customInAnimationsSoundAtStart, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (!customInAnimationsSoundAtStart.boolValue)
                    {
                        if (!DUI.UISoundNamesUIElements.Contains(inAnimationsSoundAtStart.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "The '" + inAnimationsSoundAtStart.stringValue + "' ui sound does not exist in the UISounds database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.CreateUISound(inAnimationsSoundAtStart.stringValue, SoundType.UIElements, null);
                            }
                            else
                            {
                                inAnimationsSoundAtStart.stringValue = DUI.DEFAULT_SOUND_NAME;
                            }
                        }
                        Undo.RecordObject(uiElement, "Updated Play Sound");
                        inAnimationsSoundAtStartIndex = DUI.UISoundNamesUIElements.IndexOf(inAnimationsSoundAtStart.stringValue);
                        RefreshUISounds(false);
                    }
                }
                QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonPlay), 18, 18)) { DUIUtils.PreviewSound(inAnimationsSoundAtStart.stringValue); }
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonStop), 18, 18)) { DUIUtils.StopSoundPreview(inAnimationsSoundAtStart.stringValue); }
            }
            QUI.EndHorizontal();

            //@FINISH
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("sound @FINISH", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 95);
                if (customInAnimationsSoundAtFinish.boolValue)
                {
                    QUI.PropertyField(inAnimationsSoundAtFinish, 200);
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        inAnimationsSoundAtFinishIndex = EditorGUILayout.Popup(inAnimationsSoundAtFinishIndex, DUI.UISoundNamesUIElements.ToArray(), GUILayout.Width(200));
                    }
                    if (QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(uiElement, "Updated Play Sound");
                        inAnimationsSoundAtFinish.stringValue = DUI.UISoundNamesUIElements[inAnimationsSoundAtFinishIndex];
                    }
                }
                QUI.Space(SPACE_4);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(customInAnimationsSoundAtFinish, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (!customInAnimationsSoundAtFinish.boolValue)
                    {
                        if (!DUI.UISoundNamesUIElements.Contains(inAnimationsSoundAtFinish.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "The '" + inAnimationsSoundAtFinish.stringValue + "' ui sound does not exist in the UISounds database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.CreateUISound(inAnimationsSoundAtFinish.stringValue, SoundType.UIElements, null);
                            }
                            else
                            {
                                inAnimationsSoundAtFinish.stringValue = DUI.DEFAULT_SOUND_NAME;
                            }
                        }
                        Undo.RecordObject(uiElement, "Updated Play Sound");
                        inAnimationsSoundAtFinishIndex = DUI.UISoundNamesUIElements.IndexOf(inAnimationsSoundAtFinish.stringValue);
                        RefreshUISounds(false);
                    }
                }
                QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonPlay), 18, 18)) { DUIUtils.PreviewSound(inAnimationsSoundAtFinish.stringValue); }
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonStop), 18, 18)) { DUIUtils.StopSoundPreview(inAnimationsSoundAtFinish.stringValue); }
            }
            QUI.EndHorizontal();
        }
        void DrawInAnimationsPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showInAnimationsPreset.target ? DUIStyles.ButtonStyle.barAnimationPreset : DUIStyles.ButtonStyle.barAnimationPresetCollapsed), WIDTH_420, 18)) { showInAnimationsPreset.target = !showInAnimationsPreset.target; }
            if (QUI.BeginFadeGroup(showInAnimationsPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!inAnimationsNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Anim iAnim = UIAnimatorUtil.GetInAnim(inAnimationsPresetCategoryName.stringValue, inAnimationsPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIElement iTarget = (UIElement)targets[i];
                                        iTarget.inAnimations = iAnim.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiElement, "Load Preset");
                                    uiElement.inAnimations = UIAnimatorUtil.GetInAnim(inAnimationsPresetCategoryName.stringValue, inAnimationsPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                inAnimationsNewPreset = true;
                                inAnimationsNewCategoryName = false;
                                newPresetCategoryName = inAnimationsPresetCategoryName.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + inAnimationsPresetName.stringValue + "' preset from the '" + inAnimationsPresetCategoryName.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeleteInAnimPreset(inAnimationsPresetCategoryName.stringValue, inAnimationsPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIElement iTarget = (UIElement)targets[i];
                                            if (iTarget.inAnimationsPresetCategoryName.Equals(inAnimationsPresetCategoryName.stringValue) ||
                                                iTarget.inAnimationsPresetName.Equals(inAnimationsPresetName.stringValue))
                                            {
                                                iTarget.inAnimationsPresetCategoryName = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.inAnimationsPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    inAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    inAnimationsPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshInAnimations(true);
                                }
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                inAnimationsPresetCategoryNameIndex = EditorGUILayout.Popup(inAnimationsPresetCategoryNameIndex, UIAnimatorUtil.InAnimPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                inAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.InAnimPresetCategories[inAnimationsPresetCategoryNameIndex];
                                inAnimationsPresetNameIndex = 0;
                                inAnimationsPresetName.stringValue = UIAnimatorUtil.GetInAnimPresetNames(inAnimationsPresetCategoryName.stringValue)[inAnimationsPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                inAnimationsPresetNameIndex = EditorGUILayout.Popup(inAnimationsPresetNameIndex, UIAnimatorUtil.GetInAnimPresetNames(inAnimationsPresetCategoryName.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                inAnimationsPresetName.stringValue = UIAnimatorUtil.GetInAnimPresetNames(inAnimationsPresetCategoryName.stringValue)[inAnimationsPresetNameIndex];
                            }
                        }
                        QUI.EndHorizontal();

                    }
                    else
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("Save Preset"))
                            {
                                if (inAnimationsNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset category name cannot be an empty string.", "Ok");
                                }
                                else if (string.IsNullOrEmpty(newPresetName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset name cannot be an empty string.", "Ok");
                                }
                                else if (UIAnimatorUtil.InAnimPresetExists(newPresetCategoryName, newPresetName))
                                {
                                    EditorUtility.DisplayDialog("Information", "There is another preset with the '" + newPresetName + "' preset name in the '" + newPresetCategoryName + "' preset category. Try a different preset name maybe?", "Ok");
                                }
                                else
                                {
                                    UIAnimatorUtil.CreateInAnimPreset(newPresetCategoryName, newPresetName, uiElement.inAnimations.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIElement iTarget = (UIElement)targets[i];
                                            iTarget.inAnimationsPresetCategoryName = newPresetCategoryName;
                                            iTarget.inAnimationsPresetName = newPresetName;
                                        }
                                    }
                                    inAnimationsPresetCategoryName.stringValue = newPresetCategoryName;
                                    inAnimationsPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshInAnimations(true);
                                    inAnimationsNewPreset = false;
                                    inAnimationsNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                inAnimationsNewPreset = false;
                                inAnimationsNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!inAnimationsNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    inAnimationsPresetCategoryNameIndex = EditorGUILayout.Popup(inAnimationsPresetCategoryNameIndex, UIAnimatorUtil.InAnimPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    inAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.InAnimPresetCategories[inAnimationsPresetCategoryNameIndex];
                                    inAnimationsPresetNameIndex = 0;
                                    newPresetCategoryName = inAnimationsPresetCategoryName.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    inAnimationsNewCategoryName = QUI.Toggle(inAnimationsNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (inAnimationsNewCategoryName) { newPresetCategoryName = ""; }
                                }
                                QUI.Label("new category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 80);
                            }
                            QUI.EndHorizontal();
                        }
                        else
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("New Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                newPresetCategoryName = EditorGUILayout.TextArea(newPresetCategoryName, GUILayout.Width(200));
                                inAnimationsNewCategoryName = QUI.Toggle(inAnimationsNewCategoryName);
                                QUI.Label("new category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 80);
                            }
                            QUI.EndHorizontal();
                        }
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("New Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            newPresetName = EditorGUILayout.TextArea(newPresetName);
                        }
                        QUI.EndHorizontal();
                    }
                    QUI.BeginHorizontal(WIDTH_420 - 8);
                    {
                        QUI.Toggle(loadInAnimationsPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
            QUI.Space(SPACE_2);
        }
        void DrawMoveIn()
        {
            showMoveIn.target = moveInEnabled.boolValue;
            if (QUI.BeginFadeGroup(showMoveIn.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(moveInDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(moveInStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("move from", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
                        QUI.PropertyField(moveInMoveDirection, 134);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(moveInEaseType, WIDTH_105);
                        if ((moveInMoveDirection.enumValueIndex == (int)Move.MoveDirection.CustomPosition))
                        {
                            QUI.PropertyField(moveInEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? moveInEase : moveInAnimationCurve, 98);
                            QUI.Space(SPACE_2);
                            QUI.PropertyField(moveInCustomPosition, 202);
                        }
                        else
                        {
                            QUI.PropertyField(moveInEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? moveInEase : moveInAnimationCurve, 307);
                        }
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawRotateIn()
        {
            showRotateIn.target = rotateInEnabled.boolValue;
            if (QUI.BeginFadeGroup(showRotateIn.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.OrangeLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(rotateInDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(rotateInStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("rotation", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        QUI.PropertyField(rotateInRotation, 150);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(rotateInEaseType, WIDTH_105);
                        QUI.PropertyField(rotateInEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? rotateInEase : rotateInAnimationCurve, 120);
                        QUI.Space(SPACE_2);
                        QUI.Label("rotate mode", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 72);
                        QUI.PropertyField(rotateInRotateMode, 104);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawScaleIn()
        {
            showScaleIn.target = scaleInEnabled.boolValue;
            if (QUI.BeginFadeGroup(showScaleIn.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(scaleInDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(scaleInStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("scale", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        QUI.PropertyField(scaleInScale, 150);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(scaleInEaseType, WIDTH_105);
                        QUI.PropertyField(scaleInEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? scaleInEase : scaleInAnimationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawFadeIn()
        {
            showFadeIn.target = fadeInEnabled.boolValue;
            if (QUI.BeginFadeGroup(showFadeIn.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.PurpleLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(fadeInDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(fadeInStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("alpha", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        QUI.PropertyField(fadeInAlpha, 150);

                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(fadeInEaseType, WIDTH_105);
                        QUI.PropertyField(fadeInEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? fadeInEase : fadeInAnimationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawSpecialAnimationsButtons()
        {
            QUI.Space(SPACE_4);
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button("IN -> OUT"))
                {
                    for (int i = 0; i < targets.Length; i++)
                    {
                        UIElement iTarget = (UIElement)targets[i];
                        iTarget.outAnimations = iTarget.inAnimations.Copy();
                        iTarget.outAnimations.animationType = Anim.AnimationType.Out;
                        iTarget.outAnimations.move.animationType = Anim.AnimationType.Out;
                        iTarget.outAnimations.rotate.animationType = Anim.AnimationType.Out;
                        iTarget.outAnimations.scale.animationType = Anim.AnimationType.Out;
                        iTarget.outAnimations.fade.animationType = Anim.AnimationType.Out;
                    }
                }
                if (QUI.Button("IN <- OUT"))
                {
                    for (int i = 0; i < targets.Length; i++)
                    {
                        UIElement iTarget = (UIElement)targets[i];
                        iTarget.inAnimations = iTarget.outAnimations.Copy();
                        iTarget.inAnimations.animationType = Anim.AnimationType.In;
                        iTarget.inAnimations.move.animationType = Anim.AnimationType.In;
                        iTarget.inAnimations.rotate.animationType = Anim.AnimationType.In;
                        iTarget.inAnimations.scale.animationType = Anim.AnimationType.In;
                        iTarget.inAnimations.fade.animationType = Anim.AnimationType.In;
                    }
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_8);
        }

        void DrawOutAnimations()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button(DUIStyles.GetStyle(showOutAnimations.target ? DUIStyles.ButtonStyle.OutAnimations : DUIStyles.ButtonStyle.OutAnimationsCollapsed), 336, 21)) { showOutAnimations.target = !showOutAnimations.target; }
                if (QUI.Button(DUIStyles.GetStyle(moveOutEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonMove : DUIStyles.ButtonStyle.BarButtonMoveDisabled), 21, 21)) { moveOutEnabled.boolValue = !moveOutEnabled.boolValue; if (moveOutEnabled.boolValue) { showOutAnimations.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(rotateOutEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonRotate : DUIStyles.ButtonStyle.BarButtonRotateDisabled), 21, 21)) { rotateOutEnabled.boolValue = !rotateOutEnabled.boolValue; if (rotateOutEnabled.boolValue) { showOutAnimations.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(scaleOutEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonScale : DUIStyles.ButtonStyle.BarButtonScaleDisabled), 21, 21)) { scaleOutEnabled.boolValue = !scaleOutEnabled.boolValue; if (scaleOutEnabled.boolValue) { showOutAnimations.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(fadeOutEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonFade : DUIStyles.ButtonStyle.BarButtonFadeDisabled), 21, 21)) { fadeOutEnabled.boolValue = !fadeOutEnabled.boolValue; if (fadeOutEnabled.boolValue) { showOutAnimations.target = true; } }
            }
            QUI.EndHorizontal();
            QUI.Space(-0.5f);
            infoMessage["OutAnimationsDisabled"].show.target = !uiElement.OutAnimationsEnabled && !loadOutAnimationsPresetAtRuntime.boolValue;
            DrawInfoMessage("OutAnimationsDisabled", WIDTH_420);
            infoMessage["OutAnimationsLoadPresetAtRuntime"].show.target = loadOutAnimationsPresetAtRuntime.boolValue;
            infoMessage["OutAnimationsLoadPresetAtRuntime"].message = outAnimationsPresetCategoryName.stringValue + " / " + outAnimationsPresetName.stringValue;
            DrawInfoMessage("OutAnimationsLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (QUI.BeginFadeGroup(showOutAnimations.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    DrawOutAnimationsEvents();
                    QUI.Space(SPACE_8);
                    DrawOutAnimationsSounds();
                    QUI.Space(SPACE_8);
                    DrawOutAnimationsPreset();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(moveOutEnabled.boolValue ? DUIStyles.ButtonStyle.Move : DUIStyles.ButtonStyle.MoveDisabled), WIDTH_420, 18)) { moveOutEnabled.boolValue = !moveOutEnabled.boolValue; }
                    DrawMoveOut();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(rotateOutEnabled.boolValue ? DUIStyles.ButtonStyle.Rotate : DUIStyles.ButtonStyle.RotateDisabled), WIDTH_420, 18)) { rotateOutEnabled.boolValue = !rotateOutEnabled.boolValue; }
                    DrawRotateOut();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(scaleOutEnabled.boolValue ? DUIStyles.ButtonStyle.Scale : DUIStyles.ButtonStyle.ScaleDisabled), WIDTH_420, 18)) { scaleOutEnabled.boolValue = !scaleOutEnabled.boolValue; }
                    DrawScaleOut();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(fadeOutEnabled.boolValue ? DUIStyles.ButtonStyle.Fade : DUIStyles.ButtonStyle.FadeDisabled), WIDTH_420, 18)) { fadeOutEnabled.boolValue = !fadeOutEnabled.boolValue; }
                    DrawFadeOut();
                    QUI.Space(SPACE_16);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOutAnimationsEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOutAnimationsEvents.target ? DUIStyles.ButtonStyle.Events : DUIStyles.ButtonStyle.EventsCollapsed), WIDTH_420, 18)) { showOutAnimationsEvents.target = !showOutAnimationsEvents.target; }
            if (QUI.BeginFadeGroup(showOutAnimationsEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.PropertyField(OnOutAnimationsStart, new GUIContent() { text = "OnOutAnimationsStart" }, WIDTH_420 - 6);
                    QUI.Space(SPACE_2);
                    QUI.PropertyField(OnOutAnimationsFinish, new GUIContent() { text = "OnOutAnimationsFinish" }, WIDTH_420 - 6);
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOutAnimationsSounds()
        {
            //@START
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("sound @START", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 95);
                if (customOutAnimationsSoundAtStart.boolValue)
                {
                    QUI.PropertyField(outAnimationsSoundAtStart, 200);
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        outAnimationsSoundAtStartIndex = EditorGUILayout.Popup(outAnimationsSoundAtStartIndex, DUI.UISoundNamesUIElements.ToArray(), GUILayout.Width(200));
                    }
                    if (QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(uiElement, "Updated Play Sound");
                        outAnimationsSoundAtStart.stringValue = DUI.UISoundNamesUIElements[outAnimationsSoundAtStartIndex];
                    }
                }
                QUI.Space(SPACE_4);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(customOutAnimationsSoundAtStart, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (!customOutAnimationsSoundAtStart.boolValue)
                    {
                        if (!DUI.UISoundNamesUIElements.Contains(outAnimationsSoundAtStart.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "The '" + outAnimationsSoundAtStart.stringValue + "' ui sound does not exist in the UISounds database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.CreateUISound(outAnimationsSoundAtStart.stringValue, SoundType.UIElements, null);
                            }
                            else
                            {
                                outAnimationsSoundAtStart.stringValue = DUI.DEFAULT_SOUND_NAME;
                            }
                        }
                        Undo.RecordObject(uiElement, "Updated Play Sound");
                        outAnimationsSoundAtStartIndex = DUI.UISoundNamesUIElements.IndexOf(outAnimationsSoundAtStart.stringValue);
                        RefreshUISounds(false);
                    }
                }
                QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonPlay), 18, 18)) { DUIUtils.PreviewSound(outAnimationsSoundAtStart.stringValue); }
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonStop), 18, 18)) { DUIUtils.StopSoundPreview(outAnimationsSoundAtStart.stringValue); }
            }
            QUI.EndHorizontal();

            //@FINISH
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("sound @FINISH", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 95);
                if (customOutAnimationsSoundAtFinish.boolValue)
                {
                    QUI.PropertyField(outAnimationsSoundAtFinish, 200);
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        outAnimationsSoundAtFinishIndex = EditorGUILayout.Popup(outAnimationsSoundAtFinishIndex, DUI.UISoundNamesUIElements.ToArray(), GUILayout.Width(200));
                    }
                    if (QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(uiElement, "Updated Play Sound");
                        outAnimationsSoundAtFinish.stringValue = DUI.UISoundNamesUIElements[outAnimationsSoundAtFinishIndex];
                    }
                }
                QUI.Space(SPACE_4);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(customOutAnimationsSoundAtFinish, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (!customOutAnimationsSoundAtFinish.boolValue)
                    {
                        if (!DUI.UISoundNamesUIElements.Contains(outAnimationsSoundAtFinish.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "The '" + outAnimationsSoundAtFinish.stringValue + "' ui sound does not exist in the UISounds database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.CreateUISound(outAnimationsSoundAtFinish.stringValue, SoundType.UIElements, null);
                            }
                            else
                            {
                                outAnimationsSoundAtFinish.stringValue = DUI.DEFAULT_SOUND_NAME;
                            }
                        }
                        Undo.RecordObject(uiElement, "Updated Play Sound");
                        outAnimationsSoundAtFinishIndex = DUI.UISoundNamesUIElements.IndexOf(outAnimationsSoundAtFinish.stringValue);
                        RefreshUISounds(false);
                    }
                }
                QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonPlay), 18, 18)) { DUIUtils.PreviewSound(outAnimationsSoundAtFinish.stringValue); }
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonStop), 18, 18)) { DUIUtils.StopSoundPreview(outAnimationsSoundAtFinish.stringValue); }
            }
            QUI.EndHorizontal();
        }
        void DrawOutAnimationsPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOutAnimationsPreset.target ? DUIStyles.ButtonStyle.barAnimationPreset : DUIStyles.ButtonStyle.barAnimationPresetCollapsed), WIDTH_420, 18)) { showOutAnimationsPreset.target = !showOutAnimationsPreset.target; }
            if (QUI.BeginFadeGroup(showOutAnimationsPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!outAnimationsNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Anim iAnim = UIAnimatorUtil.GetOutAnim(outAnimationsPresetCategoryName.stringValue, outAnimationsPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIElement iTarget = (UIElement)targets[i];
                                        iTarget.outAnimations = iAnim.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiElement, "Load Preset");
                                    uiElement.outAnimations = UIAnimatorUtil.GetOutAnim(outAnimationsPresetCategoryName.stringValue, outAnimationsPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                outAnimationsNewPreset = true;
                                outAnimationsNewCategoryName = false;
                                newPresetCategoryName = outAnimationsPresetCategoryName.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + outAnimationsPresetName.stringValue + "' preset from the '" + outAnimationsPresetCategoryName.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeleteOutAnimPreset(outAnimationsPresetCategoryName.stringValue, outAnimationsPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIElement iTarget = (UIElement)targets[i];
                                            if (iTarget.outAnimationsPresetCategoryName.Equals(outAnimationsPresetCategoryName.stringValue) ||
                                                iTarget.outAnimationsPresetName.Equals(outAnimationsPresetName.stringValue))
                                            {
                                                iTarget.outAnimationsPresetCategoryName = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.outAnimationsPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    outAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    outAnimationsPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshOutAnimations(true);
                                }
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                outAnimationsPresetCategoryNameIndex = EditorGUILayout.Popup(outAnimationsPresetCategoryNameIndex, UIAnimatorUtil.OutAnimPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                outAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.OutAnimPresetCategories[outAnimationsPresetCategoryNameIndex];
                                outAnimationsPresetNameIndex = 0;
                                outAnimationsPresetName.stringValue = UIAnimatorUtil.GetOutAnimPresetNames(outAnimationsPresetCategoryName.stringValue)[outAnimationsPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                outAnimationsPresetNameIndex = EditorGUILayout.Popup(outAnimationsPresetNameIndex, UIAnimatorUtil.GetOutAnimPresetNames(outAnimationsPresetCategoryName.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                outAnimationsPresetName.stringValue = UIAnimatorUtil.GetOutAnimPresetNames(outAnimationsPresetCategoryName.stringValue)[outAnimationsPresetNameIndex];
                            }
                        }
                        QUI.EndHorizontal();

                    }
                    else
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("Save Preset"))
                            {
                                if (outAnimationsNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset category name cannot be an empty string.", "Ok");
                                }
                                else if (string.IsNullOrEmpty(newPresetName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset name cannot be an empty string.", "Ok");
                                }
                                else if (UIAnimatorUtil.OutAnimPresetExists(newPresetCategoryName, newPresetName))
                                {
                                    EditorUtility.DisplayDialog("Information", "There is another preset with the '" + newPresetName + "' preset name in the '" + newPresetCategoryName + "' preset category. Try a different preset name maybe?", "Ok");
                                }
                                else
                                {
                                    UIAnimatorUtil.CreateOutAnimPreset(newPresetCategoryName, newPresetName, uiElement.outAnimations.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIElement iTarget = (UIElement)targets[i];
                                            iTarget.outAnimationsPresetCategoryName = newPresetCategoryName;
                                            iTarget.outAnimationsPresetName = newPresetName;
                                        }
                                    }
                                    outAnimationsPresetCategoryName.stringValue = newPresetCategoryName;
                                    outAnimationsPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshOutAnimations(true);
                                    outAnimationsNewPreset = false;
                                    outAnimationsNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                outAnimationsNewPreset = false;
                                outAnimationsNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!outAnimationsNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    outAnimationsPresetCategoryNameIndex = EditorGUILayout.Popup(outAnimationsPresetCategoryNameIndex, UIAnimatorUtil.OutAnimPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    outAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.OutAnimPresetCategories[outAnimationsPresetCategoryNameIndex];
                                    outAnimationsPresetNameIndex = 0;
                                    newPresetCategoryName = outAnimationsPresetCategoryName.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    outAnimationsNewCategoryName = QUI.Toggle(outAnimationsNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (outAnimationsNewCategoryName) { newPresetCategoryName = ""; }
                                }
                                QUI.Label("new category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 80);
                            }
                            QUI.EndHorizontal();
                        }
                        else
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("New Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                newPresetCategoryName = EditorGUILayout.TextArea(newPresetCategoryName, GUILayout.Width(200));
                                outAnimationsNewCategoryName = QUI.Toggle(outAnimationsNewCategoryName);
                                QUI.Label("new category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 80);
                            }
                            QUI.EndHorizontal();
                        }
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("New Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            newPresetName = EditorGUILayout.TextArea(newPresetName);
                        }
                        QUI.EndHorizontal();
                    }
                    QUI.BeginHorizontal(WIDTH_420 - 8);
                    {
                        QUI.Toggle(loadOutAnimationsPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
            QUI.Space(SPACE_2);
        }
        void DrawMoveOut()
        {
            showMoveOut.target = moveOutEnabled.boolValue;
            if (QUI.BeginFadeGroup(showMoveOut.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(moveOutDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(moveOutStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("move to", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
                        QUI.PropertyField(moveOutMoveDirection, 134);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(moveOutEaseType, WIDTH_105);
                        if ((moveOutMoveDirection.enumValueIndex == (int)Move.MoveDirection.CustomPosition))
                        {
                            QUI.PropertyField(moveOutEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? moveOutEase : moveOutAnimationCurve, 98);
                            QUI.Space(SPACE_2);
                            QUI.PropertyField(moveOutCustomPosition, 202);
                        }
                        else
                        {
                            QUI.PropertyField(moveOutEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? moveOutEase : moveOutAnimationCurve, 307);
                        }
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawRotateOut()
        {
            showRotateOut.target = rotateOutEnabled.boolValue;
            if (QUI.BeginFadeGroup(showRotateOut.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.OrangeLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(rotateOutDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(rotateOutStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("rotation", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        QUI.PropertyField(rotateOutRotation, 150);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(rotateOutEaseType, WIDTH_105);
                        QUI.PropertyField(rotateOutEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? rotateOutEase : rotateOutAnimationCurve, 120);
                        QUI.Space(SPACE_2);
                        QUI.Label("rotate mode", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 72);
                        QUI.PropertyField(rotateOutRotateMode, 104);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawScaleOut()
        {
            showScaleOut.target = scaleOutEnabled.boolValue;
            if (QUI.BeginFadeGroup(showScaleOut.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(scaleOutDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(scaleOutStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("scale", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        QUI.PropertyField(scaleOutScale, 150);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(scaleOutEaseType, WIDTH_105);
                        QUI.PropertyField(scaleOutEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? scaleOutEase : scaleOutAnimationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawFadeOut()
        {
            showFadeOut.target = fadeOutEnabled.boolValue;
            if (QUI.BeginFadeGroup(showFadeOut.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.PurpleLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(fadeOutDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(fadeOutStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("alpha", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                        QUI.PropertyField(fadeOutAlpha, 150);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(fadeOutEaseType, WIDTH_105);
                        QUI.PropertyField(fadeOutEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? fadeOutEase : fadeOutAnimationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawLoopAnimations()
        {
            if (DUI.DUISettings == null || DUI.DUISettings.UIElement_Inspector_HideLoopAnimations)
            {
                moveLoopEnabled.boolValue = false;
                rotateLoopEnabled.boolValue = false;
                scaleLoopEnabled.boolValue = false;
                fadeLoopEnabled.boolValue = false;
                return;
            }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button(DUIStyles.GetStyle(showLoopAnimations.target ? DUIStyles.ButtonStyle.LoopAnimations : DUIStyles.ButtonStyle.LoopAnimationsCollapsed), 336, 21)) { showLoopAnimations.target = !showLoopAnimations.target; }
                if (QUI.Button(DUIStyles.GetStyle(moveLoopEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonMove : DUIStyles.ButtonStyle.BarButtonMoveDisabled), 21, 21)) { moveLoopEnabled.boolValue = !moveLoopEnabled.boolValue; if (moveLoopEnabled.boolValue) { showLoopAnimations.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(rotateLoopEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonRotate : DUIStyles.ButtonStyle.BarButtonRotateDisabled), 21, 21)) { rotateLoopEnabled.boolValue = !rotateLoopEnabled.boolValue; if (rotateLoopEnabled.boolValue) { showLoopAnimations.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(scaleLoopEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonScale : DUIStyles.ButtonStyle.BarButtonScaleDisabled), 21, 21)) { scaleLoopEnabled.boolValue = !scaleLoopEnabled.boolValue; if (scaleLoopEnabled.boolValue) { showLoopAnimations.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(fadeLoopEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonFade : DUIStyles.ButtonStyle.BarButtonFadeDisabled), 21, 21)) { fadeLoopEnabled.boolValue = !fadeLoopEnabled.boolValue; if (fadeLoopEnabled.boolValue) { showLoopAnimations.target = true; } }
            }
            QUI.EndHorizontal();
            QUI.Space(-0.5f);
            infoMessage["LoopAnimationsLoadPresetAtRuntime"].show.target = loadLoopAnimationsPresetAtRuntime.boolValue;
            infoMessage["LoopAnimationsLoadPresetAtRuntime"].message = loopAnimationsPresetCategoryName.stringValue + " / " + loopAnimationsPresetName.stringValue;
            DrawInfoMessage("LoopAnimationsLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (QUI.BeginFadeGroup(showLoopAnimations.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(loopAnimationsAutoStart, 12);
                        QUI.Label("auto start", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 68);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_4);
                    DrawLoopAnimationsPreset();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(moveLoopEnabled.boolValue ? DUIStyles.ButtonStyle.Move : DUIStyles.ButtonStyle.MoveDisabled), WIDTH_420, 18)) { moveLoopEnabled.boolValue = !moveLoopEnabled.boolValue; }
                    showMoveLoop.target = moveLoopEnabled.boolValue;
                    DrawMoveLoop();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(rotateLoopEnabled.boolValue ? DUIStyles.ButtonStyle.Rotate : DUIStyles.ButtonStyle.RotateDisabled), WIDTH_420, 18)) { rotateLoopEnabled.boolValue = !rotateLoopEnabled.boolValue; }
                    showRotateLoop.target = rotateLoopEnabled.boolValue;
                    DrawRotateLoop();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(scaleLoopEnabled.boolValue ? DUIStyles.ButtonStyle.Scale : DUIStyles.ButtonStyle.ScaleDisabled), WIDTH_420, 18)) { scaleLoopEnabled.boolValue = !scaleLoopEnabled.boolValue; }
                    showScaleLoop.target = scaleLoopEnabled.boolValue;
                    DrawScaleLoop();
                    QUI.Space(SPACE_2);
                    if (QUI.Button(DUIStyles.GetStyle(fadeLoopEnabled.boolValue ? DUIStyles.ButtonStyle.Fade : DUIStyles.ButtonStyle.FadeDisabled), WIDTH_420, 18)) { fadeLoopEnabled.boolValue = !fadeLoopEnabled.boolValue; }
                    showFadeLoop.target = fadeLoopEnabled.boolValue;
                    DrawFadeLoop();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawLoopAnimationsPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showLoopAnimationsPreset.target ? DUIStyles.ButtonStyle.barLoopPreset : DUIStyles.ButtonStyle.barLoopPresetCollapsed), WIDTH_420, 18)) { showLoopAnimationsPreset.target = !showLoopAnimationsPreset.target; }
            if (QUI.BeginFadeGroup(showLoopAnimationsPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!loopAnimationsNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Loop iLoop = UIAnimatorUtil.GetLoop(loopAnimationsPresetCategoryName.stringValue, loopAnimationsPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIElement iTarget = (UIElement)targets[i];
                                        iTarget.loopAnimations = iLoop.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiElement, "Load Preset");
                                    uiElement.loopAnimations = UIAnimatorUtil.GetLoop(loopAnimationsPresetCategoryName.stringValue, loopAnimationsPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                loopAnimationsNewPreset = true;
                                loopAnimationsNewCategoryName = false;
                                newPresetCategoryName = loopAnimationsPresetCategoryName.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + loopAnimationsPresetName.stringValue + "' preset from the '" + loopAnimationsPresetCategoryName.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeleteLoopPreset(loopAnimationsPresetCategoryName.stringValue, loopAnimationsPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIElement iTarget = (UIElement)targets[i];
                                            if (iTarget.loopAnimationsPresetCategoryName.Equals(loopAnimationsPresetCategoryName.stringValue) ||
                                                iTarget.loopAnimationsPresetName.Equals(loopAnimationsPresetName.stringValue))
                                            {
                                                iTarget.loopAnimationsPresetCategoryName = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.loopAnimationsPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    loopAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    loopAnimationsPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshLoopAnimations(true);
                                }
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                loopAnimationsPresetCategoryNameIndex = EditorGUILayout.Popup(loopAnimationsPresetCategoryNameIndex, UIAnimatorUtil.LoopPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                loopAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.LoopPresetCategories[loopAnimationsPresetCategoryNameIndex];
                                loopAnimationsPresetNameIndex = 0;
                                loopAnimationsPresetName.stringValue = UIAnimatorUtil.GetLoopPresetNames(loopAnimationsPresetCategoryName.stringValue)[loopAnimationsPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                loopAnimationsPresetNameIndex = EditorGUILayout.Popup(loopAnimationsPresetNameIndex, UIAnimatorUtil.GetLoopPresetNames(loopAnimationsPresetCategoryName.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                loopAnimationsPresetName.stringValue = UIAnimatorUtil.GetLoopPresetNames(loopAnimationsPresetCategoryName.stringValue)[loopAnimationsPresetNameIndex];
                            }
                        }
                        QUI.EndHorizontal();

                    }
                    else
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("Save Preset"))
                            {
                                if (loopAnimationsNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset category name cannot be an empty string.", "Ok");
                                }
                                else if (string.IsNullOrEmpty(newPresetName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset name cannot be an empty string.", "Ok");
                                }
                                else if (UIAnimatorUtil.LoopPresetExists(newPresetCategoryName, newPresetName))
                                {
                                    EditorUtility.DisplayDialog("Information", "There is another preset with the '" + newPresetName + "' preset name in the '" + newPresetCategoryName + "' preset category. Try a different preset name maybe?", "Ok");
                                }
                                else
                                {
                                    UIAnimatorUtil.CreateLoopPreset(newPresetCategoryName, newPresetName, uiElement.loopAnimations.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIElement iTarget = (UIElement)targets[i];
                                            iTarget.loopAnimationsPresetCategoryName = newPresetCategoryName;
                                            iTarget.loopAnimationsPresetName = newPresetName;
                                        }
                                    }
                                    loopAnimationsPresetCategoryName.stringValue = newPresetCategoryName;
                                    loopAnimationsPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshLoopAnimations(true);
                                    loopAnimationsNewPreset = false;
                                    loopAnimationsNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                loopAnimationsNewPreset = false;
                                loopAnimationsNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!loopAnimationsNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    loopAnimationsPresetCategoryNameIndex = EditorGUILayout.Popup(loopAnimationsPresetCategoryNameIndex, UIAnimatorUtil.LoopPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    loopAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.LoopPresetCategories[loopAnimationsPresetCategoryNameIndex];
                                    loopAnimationsPresetNameIndex = 0;
                                    newPresetCategoryName = loopAnimationsPresetCategoryName.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    loopAnimationsNewCategoryName = QUI.Toggle(loopAnimationsNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (loopAnimationsNewCategoryName) { newPresetCategoryName = ""; }
                                }
                                QUI.Label("new category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 80);
                            }
                            QUI.EndHorizontal();
                        }
                        else
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("New Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                newPresetCategoryName = EditorGUILayout.TextArea(newPresetCategoryName, GUILayout.Width(200));
                                loopAnimationsNewCategoryName = QUI.Toggle(loopAnimationsNewCategoryName);
                                QUI.Label("new category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 80);
                            }
                            QUI.EndHorizontal();
                        }
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("New Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            newPresetName = EditorGUILayout.TextArea(newPresetName);
                        }
                        QUI.EndHorizontal();
                    }
                    QUI.BeginHorizontal(WIDTH_420 - 8);
                    {
                        QUI.Toggle(loadLoopAnimationsPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
            QUI.Space(SPACE_2);
        }
        void DrawMoveLoop()
        {
            if (QUI.BeginFadeGroup(showMoveLoop.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("movement", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
                        QUI.PropertyField(moveLoopMovement, 348);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(moveLoopDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(moveLoopStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        QUI.PropertyField(moveLoopLoops, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        QUI.PropertyField(moveLoopLoopType, 56);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(moveLoopEaseType, WIDTH_105);
                        QUI.PropertyField(moveLoopEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? moveLoopEase : moveLoopAnimationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawRotateLoop()
        {
            if (QUI.BeginFadeGroup(showRotateLoop.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.OrangeLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("rotation", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
                        QUI.PropertyField(rotateLoopRotation, 348);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(rotateLoopDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(rotateLoopStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        QUI.PropertyField(rotateLoopLoops, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        QUI.PropertyField(rotateLoopLoopType, 56);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(rotateLoopEaseType, WIDTH_105);
                        QUI.PropertyField(rotateLoopEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? rotateLoopEase : rotateLoopAnimationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawScaleLoop()
        {
            if (QUI.BeginFadeGroup(showScaleLoop.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("min", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 24);
                        QUI.PropertyField(scaleLoopMin, 178);
                        QUI.Space(SPACE_4);
                        QUI.Label("max", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 28);
                        QUI.PropertyField(scaleLoopMax, 168);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(scaleLoopDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(scaleLoopStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        QUI.PropertyField(scaleLoopLoops, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        QUI.PropertyField(scaleLoopLoopType, 56);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(scaleLoopEaseType, WIDTH_105);
                        QUI.PropertyField(scaleLoopEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? scaleLoopEase : scaleLoopAnimationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawFadeLoop()
        {
            if (QUI.BeginFadeGroup(showFadeLoop.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.PurpleLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("min", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 24);
                        QUI.PropertyField(fadeLoopMin, 36);
                        QUI.Space(SPACE_4);
                        QUI.Label("max", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 28);
                        QUI.PropertyField(fadeLoopMax, 36);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(fadeLoopDuration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(fadeLoopStartDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        QUI.PropertyField(fadeLoopLoops, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        QUI.PropertyField(fadeLoopLoopType, 56);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(fadeLoopEaseType, WIDTH_105);
                        QUI.PropertyField(fadeLoopEaseType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? fadeLoopEase : fadeLoopAnimationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void CheckIfLinkedToNotification()
        {
            uiElement.linkedToNotification = false;
            uiElement.autoRegister = true;

            Transform parent = uiElement.transform.parent;
            while (parent != null)
            {
                if (parent.GetComponent<UINotification>() != null)
                {
                    UINotification n = parent.GetComponent<UINotification>();
                    if (n.notificationContainer != null && n.notificationContainer == uiElement)
                    {
                        uiElement.linkedToNotification = true;
                        uiElement.autoRegister = false;
                        uiElement.name = DUI.DUISettings.UIElement_Inspector_RenameGameObjectPrefix + "Notification Container" + DUI.DUISettings.UIElement_Inspector_RenameGameObjectSuffix;
                    }
                    else if (n.overlay != null && n.overlay == uiElement)
                    {
                        uiElement.linkedToNotification = true;
                        uiElement.autoRegister = false;
                        uiElement.name = DUI.DUISettings.UIElement_Inspector_RenameGameObjectPrefix + "Background Overlay" + DUI.DUISettings.UIElement_Inspector_RenameGameObjectSuffix;
                    }
                    else if (n.specialElements != null && n.specialElements.Length > 0)
                    {
                        for (int i = 0; i < n.specialElements.Length; i++)
                        {
                            if (n.specialElements[i] == null) { continue; }
                            if (n.specialElements[i] != uiElement) { continue; }
                            uiElement.linkedToNotification = true;
                            uiElement.autoRegister = false;
                            uiElement.name = DUI.DUISettings.UIElement_Inspector_RenameGameObjectPrefix + "Special Element " + i + DUI.DUISettings.UIElement_Inspector_RenameGameObjectSuffix;
                        }
                    }
                    break;
                }
                if (parent != null) { parent = parent.transform.parent; }
            }
        }
        void UnlinkFromNotification()
        {
            elementCategory.stringValue = DUI.CUSTOM_NAME;

            Transform parent = uiElement.transform.parent;
            while (parent != null)
            {
                if (parent.GetComponent<UINotification>() != null)
                {
                    UINotification n = parent.GetComponent<UINotification>();
                    if (n.notificationContainer != null && n.notificationContainer == uiElement)
                    {
                        n.notificationContainer = null;
                        break;
                    }
                    else if (n.overlay != null && n.overlay == uiElement)
                    {
                        n.overlay = null;
                        break;
                    }
                    else if (n.specialElements != null && n.specialElements.Length > 0)
                    {
                        for (int i = 0; i < n.specialElements.Length; i++)
                        {
                            if (n.specialElements[i] == null) { continue; }
                            if (n.specialElements[i] != uiElement) { continue; }
                            n.specialElements[i] = null;
                            break;
                        }
                    }
                    break;
                }
                if (parent != null) { parent = parent.transform.parent; }
            }

            linkedToNotification.boolValue = false;
            autoRegister.boolValue = true;
            uiElement.name = DUI.DUISettings.UIElement_Inspector_RenameGameObjectPrefix + "Unlinked from Notification" + DUI.DUISettings.UIElement_Inspector_RenameGameObjectSuffix;
        }

        void RefreshUIElementsDatabase(bool forcedRefresh = false)
        {
            if (DUI.UIElementsDatabase == null || forcedRefresh)
            {
                DUI.RefreshUIElementsDatabase();
            }
        }
        void RefreshUISoundsDatabase(bool forcedRefresh = false)
        {
            if (DUI.UISoundsDatabase == null || forcedRefresh)
            {
                DUI.RefreshUISoundsDatabase();
            }
        }

        void ValidateElementCategoryAndElementName()
        {
            if (linkedToNotification.boolValue) { return; }
            if (string.IsNullOrEmpty(elementName.stringValue)) //element name is empty -> reset element name to default
            {
                elementCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                elementName.stringValue = DUI.DEFAULT_ELEMENT_NAME;
            }
            if (elementCategory.stringValue != DUI.CUSTOM_NAME)
            {
                if (!DUI.UIElementCategoryExists(elementCategory.stringValue)) //element category does not exist -> reset element category to default
                {
                    EditorUtility.DisplayDialog("Information", "This element's category is set to '" + elementCategory.stringValue + "', but this category was not found in the UIElements Database.\nResetting this element's category to the default value (" + DUI.DEFAULT_CATEGORY_NAME + ").", "Ok");
                    elementCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                }
                if (!DUI.UIElementNameExists(elementCategory.stringValue, elementName.stringValue)) //element name does not exist in the set category -> change category to default & ask if to add the element name to the database
                {
                    if (EditorUtility.DisplayDialog("Action Required", "This element's name is set to '" + elementName.stringValue + "', but it was not found in the '" + elementCategory.stringValue + "' category.\nDo you want to add the name to the set category?", "Yes", "No"))
                    {
                        DUI.AddUIElementName(elementCategory.stringValue, elementName.stringValue);
                        elementNameIndex = DUI.GetUIElementNames(elementCategory.stringValue).IndexOf(elementName.stringValue);
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Information", "This element's category was reset to the default value (" + DUI.DEFAULT_CATEGORY_NAME + ").", "Ok");
                        elementCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                        if (!DUI.UIElementNameExists(elementCategory.stringValue, elementName.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "This element's name is set to '" + elementName.stringValue + "', but it was not found in the '" + elementCategory.stringValue + "' category.\nDo you want to add the name to the set category?", "Yes", "No"))
                            {
                                DUI.AddUIElementName(elementCategory.stringValue, elementName.stringValue);
                                elementNameIndex = DUI.GetUIElementNames(elementCategory.stringValue).IndexOf(elementName.stringValue);
                            }
                            else
                            {
                                EditorUtility.DisplayDialog("Information", "This element's name was reset to the default value (" + DUI.DEFAULT_ELEMENT_NAME + ").", "Ok");
                                elementName.stringValue = DUI.DEFAULT_ELEMENT_NAME;
                                elementNameIndex = DUI.GetUIElementNames(elementCategory.stringValue).IndexOf(elementName.stringValue);
                            }
                        }
                        else
                        {
                            elementNameIndex = DUI.GetUIElementNames(elementCategory.stringValue).IndexOf(elementName.stringValue);
                        }
                    }
                }
                else
                {
                    elementNameIndex = DUI.GetUIElementNames(elementCategory.stringValue).IndexOf(elementName.stringValue);
                }
            }
            else
            {
                elementNameIndex = DUI.GetUIElementNames(DUI.DEFAULT_CATEGORY_NAME).IndexOf(DUI.DEFAULT_ELEMENT_NAME);
            }
            elementCategoryIndex = DUI.UIElementCategories.IndexOf(elementCategory.stringValue);
        }
        void ValidateInAnimationsSounds()
        {
            if (string.IsNullOrEmpty(inAnimationsSoundAtStart.stringValue) ||
                   inAnimationsSoundAtStart.stringValue.Equals(DUI.DEFAULT_SOUND_NAME) ||
                   !DUI.UISoundNameExists(inAnimationsSoundAtStart.stringValue, SoundType.UIElements))
            {
                inAnimationsSoundAtStart.stringValue = DUI.DEFAULT_SOUND_NAME;
            }
            inAnimationsSoundAtStartIndex = DUI.UISoundNamesUIElements.IndexOf(inAnimationsSoundAtStart.stringValue);

            if (string.IsNullOrEmpty(inAnimationsSoundAtFinish.stringValue) ||
                   inAnimationsSoundAtFinish.stringValue.Equals(DUI.DEFAULT_SOUND_NAME) ||
                   !DUI.UISoundNameExists(inAnimationsSoundAtFinish.stringValue, SoundType.UIElements))
            {
                inAnimationsSoundAtFinish.stringValue = DUI.DEFAULT_SOUND_NAME;
            }
            inAnimationsSoundAtFinishIndex = DUI.UISoundNamesUIElements.IndexOf(inAnimationsSoundAtFinish.stringValue);
        }
        void ValidateOutAnimationsSounds()
        {
            if (string.IsNullOrEmpty(outAnimationsSoundAtStart.stringValue) ||
                  outAnimationsSoundAtStart.stringValue.Equals(DUI.DEFAULT_SOUND_NAME) ||
                  !DUI.UISoundNameExists(outAnimationsSoundAtStart.stringValue, SoundType.UIElements))
            {
                outAnimationsSoundAtStart.stringValue = DUI.DEFAULT_SOUND_NAME;
            }
            outAnimationsSoundAtStartIndex = DUI.UISoundNamesUIElements.IndexOf(outAnimationsSoundAtStart.stringValue);

            if (string.IsNullOrEmpty(outAnimationsSoundAtFinish.stringValue) ||
                   outAnimationsSoundAtFinish.stringValue.Equals(DUI.DEFAULT_SOUND_NAME) ||
                   !DUI.UISoundNameExists(outAnimationsSoundAtFinish.stringValue, SoundType.UIElements))
            {
                outAnimationsSoundAtFinish.stringValue = DUI.DEFAULT_SOUND_NAME;
            }
            outAnimationsSoundAtFinishIndex = DUI.UISoundNamesUIElements.IndexOf(outAnimationsSoundAtFinish.stringValue);
        }

        void RefreshInAnimationsPresets(bool forcedRefresh = false)
        {
            if (UIAnimatorUtil.InAnimDataPresetsDatabase == null || forcedRefresh)
            {
                UIAnimatorUtil.RefreshInAnimDataPresetsDatabase();
            }
        }
        void RefreshOutAnimationsPresets(bool forcedRefresh = false)
        {
            if (UIAnimatorUtil.OutAnimDataPresetsDatabase == null || forcedRefresh)
            {
                UIAnimatorUtil.RefreshOutAnimDataPresetsDatabase();
            }
        }
        void RefreshLoopAnimationsPresets(bool forcedRefresh = false)
        {
            if (UIAnimatorUtil.LoopDataPresetsDatabase == null || forcedRefresh)
            {
                UIAnimatorUtil.RefreshLoopDataPresetsDatabase();
            }
        }

        void ValidateInAnimationsPreset()
        {
            if (string.IsNullOrEmpty(inAnimationsPresetCategoryName.stringValue) || !UIAnimatorUtil.InAnimPresetCategoryExists(inAnimationsPresetCategoryName.stringValue)) //preset category is empty or preset category does not exist -> reset to default
            {
                inAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }
            if (string.IsNullOrEmpty(inAnimationsPresetName.stringValue) || !UIAnimatorUtil.InAnimPresetExists(inAnimationsPresetCategoryName.stringValue, inAnimationsPresetName.stringValue)) //preset name is empty or preset name does not exist in set category -> reset to default preset category and preset name
            {
                inAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                inAnimationsPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }

            inAnimationsPresetCategoryNameIndex = UIAnimatorUtil.InAnimPresetCategories.IndexOf(inAnimationsPresetCategoryName.stringValue);
            inAnimationsPresetNameIndex = UIAnimatorUtil.GetInAnimPresetNames(inAnimationsPresetCategoryName.stringValue).IndexOf(inAnimationsPresetName.stringValue);
        }
        void ValidateOutAnimationsPreset()
        {
            if (string.IsNullOrEmpty(outAnimationsPresetCategoryName.stringValue) || !UIAnimatorUtil.OutAnimPresetCategoryExists(outAnimationsPresetCategoryName.stringValue)) //preset category is empty or preset category does not exist -> reset to default
            {
                outAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }
            if (string.IsNullOrEmpty(outAnimationsPresetName.stringValue) || !UIAnimatorUtil.OutAnimPresetExists(outAnimationsPresetCategoryName.stringValue, outAnimationsPresetName.stringValue)) //preset name is empty or preset name does not exist in set category -> reset to default preset category and preset name
            {
                outAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                outAnimationsPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }

            outAnimationsPresetCategoryNameIndex = UIAnimatorUtil.OutAnimPresetCategories.IndexOf(outAnimationsPresetCategoryName.stringValue);
            outAnimationsPresetNameIndex = UIAnimatorUtil.GetOutAnimPresetNames(outAnimationsPresetCategoryName.stringValue).IndexOf(outAnimationsPresetName.stringValue);
        }
        void ValidateLoopAnimationsPreset()
        {
            if (string.IsNullOrEmpty(loopAnimationsPresetCategoryName.stringValue) || !UIAnimatorUtil.LoopPresetCategoryExists(loopAnimationsPresetCategoryName.stringValue)) //preset category is empty or preset category does not exist -> reset to default
            {
                loopAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }
            if (string.IsNullOrEmpty(loopAnimationsPresetName.stringValue) || !UIAnimatorUtil.LoopPresetExists(loopAnimationsPresetCategoryName.stringValue, loopAnimationsPresetName.stringValue)) //preset name is empty or preset name does not exist in set category -> reset to default preset category and preset name
            {
                loopAnimationsPresetCategoryName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                loopAnimationsPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }

            loopAnimationsPresetCategoryNameIndex = UIAnimatorUtil.LoopPresetCategories.IndexOf(loopAnimationsPresetCategoryName.stringValue);
            loopAnimationsPresetNameIndex = UIAnimatorUtil.GetLoopPresetNames(loopAnimationsPresetCategoryName.stringValue).IndexOf(loopAnimationsPresetName.stringValue);
        }
    }
}
