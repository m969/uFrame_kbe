// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace DoozyUI
{
    [CustomEditor(typeof(UIButton), true)]
    [DisallowMultipleComponent]
    [CanEditMultipleObjects]
    public class UIButtonEditor : QEditor
    {
        UIButton uiButton { get { return (UIButton)target; } }

        SerializedProperty
        #region ButtonCategory
            buttonCategory,
        #endregion
        #region ButtonName
            buttonName,
        #endregion
        #region Settings
            allowMultipleClicks, disableButtonInterval,
            deselectButtonOnClick,
        #endregion
        #region PointerEnter
            useOnPointerEnter, onPointerEnterDisableInterval, onPointerEnterSound, customOnPointerEnterSound, OnPointerEnter,
            onPointerEnterPunchPresetCategory, onPointerEnterPunchPresetName, loadOnPointerEnterPunchPresetAtRuntime,
            onPointerEnterPunch,
            onPointerEnterPunchMove, onPointerEnterPunchMoveEnabled, onPointerEnterPunchMovePunch, onPointerEnterPunchMoveStartDelay, onPointerEnterPunchMoveDuration, onPointerEnterPunchMoveVibrato, onPointerEnterPunchMoveElasticity,
            onPointerEnterPunchRotate, onPointerEnterPunchRotateEnabled, onPointerEnterPunchRotatePunch, onPointerEnterPunchRotateStartDelay, onPointerEnterPunchRotateDuration, onPointerEnterPunchRotateVibrato, onPointerEnterPunchRotateElasticity,
            onPointerEnterPunchScale, onPointerEnterPunchScaleEnabled, onPointerEnterPunchScalePunch, onPointerEnterPunchScaleStartDelay, onPointerEnterPunchScaleDuration, onPointerEnterPunchScaleVibrato, onPointerEnterPunchScaleElasticity,
            onPointerEnterGameEvents,
        #endregion
        #region PointerExit
            useOnPointerExit, onPointerExitDisableInterval, onPointerExitSound, customOnPointerExitSound, OnPointerExit,
            onPointerExitPunchPresetCategory, onPointerExitPunchPresetName, loadOnPointerExitPunchPresetAtRuntime,
            onPointerExitPunch,
            onPointerExitPunchMove, onPointerExitPunchMoveEnabled, onPointerExitPunchMovePunch, onPointerExitPunchMoveStartDelay, onPointerExitPunchMoveDuration, onPointerExitPunchMoveVibrato, onPointerExitPunchMoveElasticity,
            onPointerExitPunchRotate, onPointerExitPunchRotateEnabled, onPointerExitPunchRotatePunch, onPointerExitPunchRotateStartDelay, onPointerExitPunchRotateDuration, onPointerExitPunchRotateVibrato, onPointerExitPunchRotateElasticity,
            onPointerExitPunchScale, onPointerExitPunchScaleEnabled, onPointerExitPunchScalePunch, onPointerExitPunchScaleStartDelay, onPointerExitPunchScaleDuration, onPointerExitPunchScaleVibrato, onPointerExitPunchScaleElasticity,
            onPointerExitGameEvents,
        #endregion
        #region PointerDown
            useOnPointerDown, onPointerDownSound, customOnPointerDownSound, OnPointerDown,
            onPointerDownPunchPresetCategory, onPointerDownPunchPresetName, loadOnPointerDownPunchPresetAtRuntime,
            onPointerDownPunch,
            onPointerDownPunchMove, onPointerDownPunchMoveEnabled, onPointerDownPunchMovePunch, onPointerDownPunchMoveStartDelay, onPointerDownPunchMoveDuration, onPointerDownPunchMoveVibrato, onPointerDownPunchMoveElasticity,
            onPointerDownPunchRotate, onPointerDownPunchRotateEnabled, onPointerDownPunchRotatePunch, onPointerDownPunchRotateStartDelay, onPointerDownPunchRotateDuration, onPointerDownPunchRotateVibrato, onPointerDownPunchRotateElasticity,
            onPointerDownPunchScale, onPointerDownPunchScaleEnabled, onPointerDownPunchScalePunch, onPointerDownPunchScaleStartDelay, onPointerDownPunchScaleDuration, onPointerDownPunchScaleVibrato, onPointerDownPunchScaleElasticity,
            onPointerDownGameEvents,
        #endregion
        #region PointerUp
            useOnPointerUp, onPointerUpSound, customOnPointerUpSound, OnPointerUp,
            onPointerUpPunchPresetCategory, onPointerUpPunchPresetName, loadOnPointerUpPunchPresetAtRuntime,
            onPointerUpPunch,
            onPointerUpPunchMove, onPointerUpPunchMoveEnabled, onPointerUpPunchMovePunch, onPointerUpPunchMoveStartDelay, onPointerUpPunchMoveDuration, onPointerUpPunchMoveVibrato, onPointerUpPunchMoveElasticity,
            onPointerUpPunchRotate, onPointerUpPunchRotateEnabled, onPointerUpPunchRotatePunch, onPointerUpPunchRotateStartDelay, onPointerUpPunchRotateDuration, onPointerUpPunchRotateVibrato, onPointerUpPunchRotateElasticity,
            onPointerUpPunchScale, onPointerUpPunchScaleEnabled, onPointerUpPunchScalePunch, onPointerUpPunchScaleStartDelay, onPointerUpPunchScaleDuration, onPointerUpPunchScaleVibrato, onPointerUpPunchScaleElasticity,
            onPointerUpGameEvents,
        #endregion
        #region OnClick
            useOnClickAnimations, waitForOnClickAnimation, singleClickMode, onClickSound, customOnClickSound, OnClick,
            onClickPunchPresetCategory, onClickPunchPresetName, loadOnClickPunchPresetAtRuntime,
            onClickPunch,
            onClickPunchMove, onClickPunchMoveEnabled, onClickPunchMovePunch, onClickPunchMoveStartDelay, onClickPunchMoveDuration, onClickPunchMoveVibrato, onClickPunchMoveElasticity,
            onClickPunchRotate, onClickPunchRotateEnabled, onClickPunchRotatePunch, onClickPunchRotateStartDelay, onClickPunchRotateDuration, onClickPunchRotateVibrato, onClickPunchRotateElasticity,
            onClickPunchScale, onClickPunchScaleEnabled, onClickPunchScalePunch, onClickPunchScaleStartDelay, onClickPunchScaleDuration, onClickPunchScaleVibrato, onClickPunchScaleElasticity,
            onClickGameEvents,
        #endregion
        #region OnDoubleClick
            useOnDoubleClick, waitForOnDoubleClickAnimation, doubleClickRegisterInterval, onDoubleClickSound, customOnDoubleClickSound, OnDoubleClick,
            onDoubleClickPunchPresetCategory, onDoubleClickPunchPresetName, loadOnDoubleClickPunchPresetAtRuntime,
            onDoubleClickPunch,
            onDoubleClickPunchMove, onDoubleClickPunchMoveEnabled, onDoubleClickPunchMovePunch, onDoubleClickPunchMoveStartDelay, onDoubleClickPunchMoveDuration, onDoubleClickPunchMoveVibrato, onDoubleClickPunchMoveElasticity,
            onDoubleClickPunchRotate, onDoubleClickPunchRotateEnabled, onDoubleClickPunchRotatePunch, onDoubleClickPunchRotateStartDelay, onDoubleClickPunchRotateDuration, onDoubleClickPunchRotateVibrato, onDoubleClickPunchRotateElasticity,
            onDoubleClickPunchScale, onDoubleClickPunchScaleEnabled, onDoubleClickPunchScalePunch, onDoubleClickPunchScaleStartDelay, onDoubleClickPunchScaleDuration, onDoubleClickPunchScaleVibrato, onDoubleClickPunchScaleElasticity,
            onDoubleClickGameEvents,
        #endregion
        #region OnLongClick
            useOnLongClick, waitForOnLongClickAnimation, longClickRegisterInterval, onLongClickSound, customOnLongClickSound, OnLongClick,
            onLongClickPunchPresetCategory, onLongClickPunchPresetName, loadOnLongClickPunchPresetAtRuntime,
            onLongClickPunch,
            onLongClickPunchMove, onLongClickPunchMoveEnabled, onLongClickPunchMovePunch, onLongClickPunchMoveStartDelay, onLongClickPunchMoveDuration, onLongClickPunchMoveVibrato, onLongClickPunchMoveElasticity,
            onLongClickPunchRotate, onLongClickPunchRotateEnabled, onLongClickPunchRotatePunch, onLongClickPunchRotateStartDelay, onLongClickPunchRotateDuration, onLongClickPunchRotateVibrato, onLongClickPunchRotateElasticity,
            onLongClickPunchScale, onLongClickPunchScaleEnabled, onLongClickPunchScalePunch, onLongClickPunchScaleStartDelay, onLongClickPunchScaleDuration, onLongClickPunchScaleVibrato, onLongClickPunchScaleElasticity,
            onLongClickGameEvents,
        #endregion
        #region NormalLoop
            normalLoopPresetCategory, normalLoopPresetName, loadNormalLoopPresetAtRuntime,
            normalLoop,
            normalLoopMove,
            normalLoopMoveEnabled, normalLoopMoveMovement, normalLoopMoveEaseType, normalLoopMoveEase, normalLoopMoveAnimationCurve, normalLoopMoveLoops, normalLoopMoveLoopType, normalLoopMoveStartDelay, normalLoopMoveDuration,
            normalLoopRotate,
            normalLoopRotateEnabled, normalLoopRotateRotation, normalLoopRotateEaseType, normalLoopRotateEase, normalLoopRotateAnimationCurve, normalLoopRotateLoops, normalLoopRotateLoopType, normalLoopRotateStartDelay, normalLoopRotateDuration,
            normalLoopScale,
            normalLoopScaleEnabled, normalLoopScaleMin, normalLoopScaleMax, normalLoopScaleEaseType, normalLoopScaleEase, normalLoopScaleAnimationCurve, normalLoopScaleLoops, normalLoopScaleLoopType, normalLoopScaleStartDelay, normalLoopScaleDuration,
            normalLoopFade,
            normalLoopFadeEnabled, normalLoopFadeMin, normalLoopFadeMax, normalLoopFadeEaseType, normalLoopFadeEase, normalLoopFadeAnimationCurve, normalLoopFadeLoops, normalLoopFadeLoopType, normalLoopFadeStartDelay, normalLoopFadeDuration,
        #endregion
        #region SelectedLoop
            selectedLoopPresetCategory, selectedLoopPresetName, loadSelectedLoopPresetAtRuntime,
            selectedLoop,
            selectedLoopMove,
            selectedLoopMoveEnabled, selectedLoopMoveMovement, selectedLoopMoveEaseType, selectedLoopMoveEase, selectedLoopMoveAnimationCurve, selectedLoopMoveLoops, selectedLoopMoveLoopType, selectedLoopMoveStartDelay, selectedLoopMoveDuration,
            selectedLoopRotate,
            selectedLoopRotateEnabled, selectedLoopRotateRotation, selectedLoopRotateEaseType, selectedLoopRotateEase, selectedLoopRotateAnimationCurve, selectedLoopRotateLoops, selectedLoopRotateLoopType, selectedLoopRotateStartDelay, selectedLoopRotateDuration,
            selectedLoopScale,
            selectedLoopScaleEnabled, selectedLoopScaleMin, selectedLoopScaleMax, selectedLoopScaleEaseType, selectedLoopScaleEase, selectedLoopScaleAnimationCurve, selectedLoopScaleLoops, selectedLoopScaleLoopType, selectedLoopScaleStartDelay, selectedLoopScaleDuration,
            selectedLoopFade,
            selectedLoopFadeEnabled, selectedLoopFadeMin, selectedLoopFadeMax, selectedLoopFadeEaseType, selectedLoopFadeEase, selectedLoopFadeAnimationCurve, selectedLoopFadeLoops, selectedLoopFadeLoopType, selectedLoopFadeStartDelay, selectedLoopFadeDuration;
        #endregion

        AnimBool
            showOnPointerEnter, showOnPointerEnterPreset, showOnPointerEnterPunchMove, showOnPointerEnterPunchRotate, showOnPointerEnterPunchScale, showOnPointerEnterEvents, showOnPointerEnterGameEvents, showOnPointerEnterNavigation,
            showOnPointerExit, showOnPointerExitPreset, showOnPointerExitPunchMove, showOnPointerExitPunchRotate, showOnPointerExitPunchScale, showOnPointerExitEvents, showOnPointerExitGameEvents, showOnPointerExitNavigation,
            showOnPointerDown, showOnPointerDownPreset, showOnPointerDownPunchMove, showOnPointerDownPunchRotate, showOnPointerDownPunchScale, showOnPointerDownEvents, showOnPointerDownGameEvents, showOnPointerDownNavigation,
            showOnPointerUp, showOnPointerUpPreset, showOnPointerUpPunchMove, showOnPointerUpPunchRotate, showOnPointerUpPunchScale, showOnPointerUpEvents, showOnPointerUpGameEvents, showOnPointerUpNavigation,
            showOnClick, showOnClickPreset, showOnClickPunchMove, showOnClickPunchRotate, showOnClickPunchScale, showOnClickEvents, showOnClickGameEvents, showOnClickNavigation,
            showOnDoubleClick, showOnDoubleClickPreset, showOnDoubleClickPunchMove, showOnDoubleClickPunchRotate, showOnDoubleClickPunchScale, showOnDoubleClickEvents, showOnDoubleClickGameEvents, showOnDoubleClickNavigation,
            showOnLongClick, showOnLongClickPreset, showOnLongClickPunchMove, showOnLongClickPunchRotate, showOnLongClickPunchScale, showOnLongClickEvents, showOnLongClickGameEvents, showOnLongClickNavigation,
            showNormalAnimation, showNormalAnimationPreset, showNormalAnimationMove, showNormalAnimationRotate, showNormalAnimationScale, showNormalAnimationFade,
            showSelectedAnimation, showSelectedAnimationPreset, showSelectedAnimationMove, showSelectedAnimationRotate, showSelectedAnimationScale, showSelectedAnimationFade;

        int buttonNameIndex = 0;
        int buttonCategoryIndex = 0;

        int onPointerEnterSoundIndex;
        int onPointerExitSoundIndex;
        int onPointerDownSoundIndex;
        int onPointerUpSoundIndex;
        int onClickSoundIndex;
        int onDoubleClickSoundIndex;
        int onLongClickSoundIndex;

        string newPresetCategoryName = "";
        string newPresetName = "";

        int onPointerEnterPunchPresetCategoryNameIndex;
        int onPointerEnterPunchPresetNameIndex;
        bool onPointerEnterPunchNewPreset = false;
        bool onPointerEnterPunchNewCategoryName = false;

        int onPointerExitPunchPresetCategoryNameIndex;
        int onPointerExitPunchPresetNameIndex;
        bool onPointerExitPunchNewPreset = false;
        bool onPointerExitPunchNewCategoryName = false;

        int onPointerDownPunchPresetCategoryNameIndex;
        int onPointerDownPunchPresetNameIndex;
        bool onPointerDownPunchNewPreset = false;
        bool onPointerDownPunchNewCategoryName = false;

        int onPointerUpPunchPresetCategoryNameIndex;
        int onPointerUpPunchPresetNameIndex;
        bool onPointerUpPunchNewPreset = false;
        bool onPointerUpPunchNewCategoryName = false;

        int onClickPunchPresetCategoryNameIndex;
        int onClickPunchPresetNameIndex;
        bool onClickPunchNewPreset = false;
        bool onClickPunchNewCategoryName = false;

        int onDoubleClickPunchPresetCategoryNameIndex;
        int onDoubleClickPunchPresetNameIndex;
        bool onDoubleClickPunchNewPreset = false;
        bool onDoubleClickPunchNewCategoryName = false;

        int onLongClickPunchPresetCategoryNameIndex;
        int onLongClickPunchPresetNameIndex;
        bool onLongClickPunchNewPreset = false;
        bool onLongClickPunchNewCategoryName = false;

        int normalLoopPresetCategoryIndex;
        int normalLoopPresetNameIndex;
        bool normalLoopNewPreset = false;
        bool normalLoopNewCategoryName = false;

        int selectedLoopPresetCategoryIndex;
        int selectedLoopPresetNameIndex;
        bool selectedLoopNewPreset = false;
        bool selectedLoopNewCategoryName = false;

        EditorNavigationPointerData onPointerEnterEditorNavigationData = new EditorNavigationPointerData();
        EditorNavigationPointerData onPointerExitEditorNavigationData = new EditorNavigationPointerData();
        EditorNavigationPointerData onPointerDownEditorNavigationData = new EditorNavigationPointerData();
        EditorNavigationPointerData onPointerUpEditorNavigationData = new EditorNavigationPointerData();
        EditorNavigationPointerData onClickEditorNavigationData = new EditorNavigationPointerData();
        EditorNavigationPointerData onDoubleClickEditorNavigationData = new EditorNavigationPointerData();
        EditorNavigationPointerData onLongClickEditorNavigationData = new EditorNavigationPointerData();


        bool ControlPanelSelected = false;
        bool refreshData = true;

        void SerializedObjectFindProperties()
        {
            #region ButtonCategory
            buttonCategory = serializedObject.FindProperty("buttonCategory");
            #endregion
            #region ButtonName
            buttonName = serializedObject.FindProperty("buttonName");
            #endregion
            #region Settings
            allowMultipleClicks = serializedObject.FindProperty("allowMultipleClicks");
            disableButtonInterval = serializedObject.FindProperty("disableButtonInterval");
            deselectButtonOnClick = serializedObject.FindProperty("deselectButtonOnClick");
            #endregion
            #region PointerEnter
            useOnPointerEnter = serializedObject.FindProperty("useOnPointerEnter");
            onPointerEnterDisableInterval = serializedObject.FindProperty("onPointerEnterDisableInterval");
            onPointerEnterSound = serializedObject.FindProperty("onPointerEnterSound");
            customOnPointerEnterSound = serializedObject.FindProperty("customOnPointerEnterSound");
            OnPointerEnter = serializedObject.FindProperty("OnPointerEnter");
            onPointerEnterPunchPresetCategory = serializedObject.FindProperty("onPointerEnterPunchPresetCategory");
            onPointerEnterPunchPresetName = serializedObject.FindProperty("onPointerEnterPunchPresetName");
            loadOnPointerEnterPunchPresetAtRuntime = serializedObject.FindProperty("loadOnPointerEnterPunchPresetAtRuntime");
            onPointerEnterPunch = serializedObject.FindProperty("onPointerEnterPunch");
            onPointerEnterPunchMove = onPointerEnterPunch.FindPropertyRelative("move");
            onPointerEnterPunchMoveEnabled = onPointerEnterPunchMove.FindPropertyRelative("enabled");
            onPointerEnterPunchMovePunch = onPointerEnterPunchMove.FindPropertyRelative("punch");
            onPointerEnterPunchMoveStartDelay = onPointerEnterPunchMove.FindPropertyRelative("startDelay");
            onPointerEnterPunchMoveDuration = onPointerEnterPunchMove.FindPropertyRelative("duration");
            onPointerEnterPunchMoveVibrato = onPointerEnterPunchMove.FindPropertyRelative("vibrato");
            onPointerEnterPunchMoveElasticity = onPointerEnterPunchMove.FindPropertyRelative("elasticity");
            onPointerEnterPunchRotate = onPointerEnterPunch.FindPropertyRelative("rotate");
            onPointerEnterPunchRotateEnabled = onPointerEnterPunchRotate.FindPropertyRelative("enabled");
            onPointerEnterPunchRotatePunch = onPointerEnterPunchRotate.FindPropertyRelative("punch");
            onPointerEnterPunchRotateStartDelay = onPointerEnterPunchRotate.FindPropertyRelative("startDelay");
            onPointerEnterPunchRotateDuration = onPointerEnterPunchRotate.FindPropertyRelative("duration");
            onPointerEnterPunchRotateVibrato = onPointerEnterPunchRotate.FindPropertyRelative("vibrato");
            onPointerEnterPunchRotateElasticity = onPointerEnterPunchRotate.FindPropertyRelative("elasticity");
            onPointerEnterPunchScale = onPointerEnterPunch.FindPropertyRelative("scale");
            onPointerEnterPunchScaleEnabled = onPointerEnterPunchScale.FindPropertyRelative("enabled");
            onPointerEnterPunchScalePunch = onPointerEnterPunchScale.FindPropertyRelative("punch");
            onPointerEnterPunchScaleStartDelay = onPointerEnterPunchScale.FindPropertyRelative("startDelay");
            onPointerEnterPunchScaleDuration = onPointerEnterPunchScale.FindPropertyRelative("duration");
            onPointerEnterPunchScaleVibrato = onPointerEnterPunchScale.FindPropertyRelative("vibrato");
            onPointerEnterPunchScaleElasticity = onPointerEnterPunchScale.FindPropertyRelative("elasticity");
            onPointerEnterGameEvents = serializedObject.FindProperty("onPointerEnterGameEvents");
            #endregion
            #region PointerExit
            useOnPointerExit = serializedObject.FindProperty("useOnPointerExit");
            onPointerExitDisableInterval = serializedObject.FindProperty("onPointerExitDisableInterval");
            onPointerExitSound = serializedObject.FindProperty("onPointerExitSound");
            customOnPointerExitSound = serializedObject.FindProperty("customOnPointerExitSound");
            OnPointerExit = serializedObject.FindProperty("OnPointerExit");
            onPointerExitPunchPresetCategory = serializedObject.FindProperty("onPointerExitPunchPresetCategory");
            onPointerExitPunchPresetName = serializedObject.FindProperty("onPointerExitPunchPresetName");
            loadOnPointerExitPunchPresetAtRuntime = serializedObject.FindProperty("loadOnPointerExitPunchPresetAtRuntime");
            onPointerExitPunch = serializedObject.FindProperty("onPointerExitPunch");
            onPointerExitPunchMove = onPointerExitPunch.FindPropertyRelative("move");
            onPointerExitPunchMoveEnabled = onPointerExitPunchMove.FindPropertyRelative("enabled");
            onPointerExitPunchMovePunch = onPointerExitPunchMove.FindPropertyRelative("punch");
            onPointerExitPunchMoveStartDelay = onPointerExitPunchMove.FindPropertyRelative("startDelay");
            onPointerExitPunchMoveDuration = onPointerExitPunchMove.FindPropertyRelative("duration");
            onPointerExitPunchMoveVibrato = onPointerExitPunchMove.FindPropertyRelative("vibrato");
            onPointerExitPunchMoveElasticity = onPointerExitPunchMove.FindPropertyRelative("elasticity");
            onPointerExitPunchRotate = onPointerExitPunch.FindPropertyRelative("rotate");
            onPointerExitPunchRotateEnabled = onPointerExitPunchRotate.FindPropertyRelative("enabled");
            onPointerExitPunchRotatePunch = onPointerExitPunchRotate.FindPropertyRelative("punch");
            onPointerExitPunchRotateStartDelay = onPointerExitPunchRotate.FindPropertyRelative("startDelay");
            onPointerExitPunchRotateDuration = onPointerExitPunchRotate.FindPropertyRelative("duration");
            onPointerExitPunchRotateVibrato = onPointerExitPunchRotate.FindPropertyRelative("vibrato");
            onPointerExitPunchRotateElasticity = onPointerExitPunchRotate.FindPropertyRelative("elasticity");
            onPointerExitPunchScale = onPointerExitPunch.FindPropertyRelative("scale");
            onPointerExitPunchScaleEnabled = onPointerExitPunchScale.FindPropertyRelative("enabled");
            onPointerExitPunchScalePunch = onPointerExitPunchScale.FindPropertyRelative("punch");
            onPointerExitPunchScaleStartDelay = onPointerExitPunchScale.FindPropertyRelative("startDelay");
            onPointerExitPunchScaleDuration = onPointerExitPunchScale.FindPropertyRelative("duration");
            onPointerExitPunchScaleVibrato = onPointerExitPunchScale.FindPropertyRelative("vibrato");
            onPointerExitPunchScaleElasticity = onPointerExitPunchScale.FindPropertyRelative("elasticity");
            onPointerExitGameEvents = serializedObject.FindProperty("onPointerExitGameEvents");
            #endregion
            #region PointerDown
            useOnPointerDown = serializedObject.FindProperty("useOnPointerDown");
            onPointerDownSound = serializedObject.FindProperty("onPointerDownSound");
            customOnPointerDownSound = serializedObject.FindProperty("customOnPointerDownSound");
            OnPointerDown = serializedObject.FindProperty("OnPointerDown");
            onPointerDownPunchPresetCategory = serializedObject.FindProperty("onPointerDownPunchPresetCategory");
            onPointerDownPunchPresetName = serializedObject.FindProperty("onPointerDownPunchPresetName");
            loadOnPointerDownPunchPresetAtRuntime = serializedObject.FindProperty("loadOnPointerDownPunchPresetAtRuntime");
            onPointerDownPunch = serializedObject.FindProperty("onPointerDownPunch");
            onPointerDownPunchMove = onPointerDownPunch.FindPropertyRelative("move");
            onPointerDownPunchMoveEnabled = onPointerDownPunchMove.FindPropertyRelative("enabled");
            onPointerDownPunchMovePunch = onPointerDownPunchMove.FindPropertyRelative("punch");
            onPointerDownPunchMoveStartDelay = onPointerDownPunchMove.FindPropertyRelative("startDelay");
            onPointerDownPunchMoveDuration = onPointerDownPunchMove.FindPropertyRelative("duration");
            onPointerDownPunchMoveVibrato = onPointerDownPunchMove.FindPropertyRelative("vibrato");
            onPointerDownPunchMoveElasticity = onPointerDownPunchMove.FindPropertyRelative("elasticity");
            onPointerDownPunchRotate = onPointerDownPunch.FindPropertyRelative("rotate");
            onPointerDownPunchRotateEnabled = onPointerDownPunchRotate.FindPropertyRelative("enabled");
            onPointerDownPunchRotatePunch = onPointerDownPunchRotate.FindPropertyRelative("punch");
            onPointerDownPunchRotateStartDelay = onPointerDownPunchRotate.FindPropertyRelative("startDelay");
            onPointerDownPunchRotateDuration = onPointerDownPunchRotate.FindPropertyRelative("duration");
            onPointerDownPunchRotateVibrato = onPointerDownPunchRotate.FindPropertyRelative("vibrato");
            onPointerDownPunchRotateElasticity = onPointerDownPunchRotate.FindPropertyRelative("elasticity");
            onPointerDownPunchScale = onPointerDownPunch.FindPropertyRelative("scale");
            onPointerDownPunchScaleEnabled = onPointerDownPunchScale.FindPropertyRelative("enabled");
            onPointerDownPunchScalePunch = onPointerDownPunchScale.FindPropertyRelative("punch");
            onPointerDownPunchScaleStartDelay = onPointerDownPunchScale.FindPropertyRelative("startDelay");
            onPointerDownPunchScaleDuration = onPointerDownPunchScale.FindPropertyRelative("duration");
            onPointerDownPunchScaleVibrato = onPointerDownPunchScale.FindPropertyRelative("vibrato");
            onPointerDownPunchScaleElasticity = onPointerDownPunchScale.FindPropertyRelative("elasticity");
            onPointerDownGameEvents = serializedObject.FindProperty("onPointerDownGameEvents");
            #endregion
            #region PointerUp
            useOnPointerUp = serializedObject.FindProperty("useOnPointerUp");
            onPointerUpSound = serializedObject.FindProperty("onPointerUpSound");
            customOnPointerUpSound = serializedObject.FindProperty("customOnPointerUpSound");
            OnPointerUp = serializedObject.FindProperty("OnPointerUp");
            onPointerUpPunchPresetCategory = serializedObject.FindProperty("onPointerUpPunchPresetCategory");
            onPointerUpPunchPresetName = serializedObject.FindProperty("onPointerUpPunchPresetName");
            loadOnPointerUpPunchPresetAtRuntime = serializedObject.FindProperty("loadOnPointerUpPunchPresetAtRuntime");
            onPointerUpPunch = serializedObject.FindProperty("onPointerUpPunch");
            onPointerUpPunchMove = onPointerUpPunch.FindPropertyRelative("move");
            onPointerUpPunchMoveEnabled = onPointerUpPunchMove.FindPropertyRelative("enabled");
            onPointerUpPunchMovePunch = onPointerUpPunchMove.FindPropertyRelative("punch");
            onPointerUpPunchMoveStartDelay = onPointerUpPunchMove.FindPropertyRelative("startDelay");
            onPointerUpPunchMoveDuration = onPointerUpPunchMove.FindPropertyRelative("duration");
            onPointerUpPunchMoveVibrato = onPointerUpPunchMove.FindPropertyRelative("vibrato");
            onPointerUpPunchMoveElasticity = onPointerUpPunchMove.FindPropertyRelative("elasticity");
            onPointerUpPunchRotate = onPointerUpPunch.FindPropertyRelative("rotate");
            onPointerUpPunchRotateEnabled = onPointerUpPunchRotate.FindPropertyRelative("enabled");
            onPointerUpPunchRotatePunch = onPointerUpPunchRotate.FindPropertyRelative("punch");
            onPointerUpPunchRotateStartDelay = onPointerUpPunchRotate.FindPropertyRelative("startDelay");
            onPointerUpPunchRotateDuration = onPointerUpPunchRotate.FindPropertyRelative("duration");
            onPointerUpPunchRotateVibrato = onPointerUpPunchRotate.FindPropertyRelative("vibrato");
            onPointerUpPunchRotateElasticity = onPointerUpPunchRotate.FindPropertyRelative("elasticity");
            onPointerUpPunchScale = onPointerUpPunch.FindPropertyRelative("scale");
            onPointerUpPunchScaleEnabled = onPointerUpPunchScale.FindPropertyRelative("enabled");
            onPointerUpPunchScalePunch = onPointerUpPunchScale.FindPropertyRelative("punch");
            onPointerUpPunchScaleStartDelay = onPointerUpPunchScale.FindPropertyRelative("startDelay");
            onPointerUpPunchScaleDuration = onPointerUpPunchScale.FindPropertyRelative("duration");
            onPointerUpPunchScaleVibrato = onPointerUpPunchScale.FindPropertyRelative("vibrato");
            onPointerUpPunchScaleElasticity = onPointerUpPunchScale.FindPropertyRelative("elasticity");
            onPointerUpGameEvents = serializedObject.FindProperty("onPointerUpGameEvents");
            #endregion
            #region OnClick
            useOnClickAnimations = serializedObject.FindProperty("useOnClickAnimations");
            waitForOnClickAnimation = serializedObject.FindProperty("waitForOnClickAnimation");
            singleClickMode = serializedObject.FindProperty("singleClickMode");
            onClickSound = serializedObject.FindProperty("onClickSound");
            customOnClickSound = serializedObject.FindProperty("customOnClickSound");
            OnClick = serializedObject.FindProperty("OnClick");
            onClickPunchPresetCategory = serializedObject.FindProperty("onClickPunchPresetCategory");
            onClickPunchPresetName = serializedObject.FindProperty("onClickPunchPresetName");
            loadOnClickPunchPresetAtRuntime = serializedObject.FindProperty("loadOnClickPunchPresetAtRuntime");
            onClickPunch = serializedObject.FindProperty("onClickPunch");
            onClickPunchMove = onClickPunch.FindPropertyRelative("move");
            onClickPunchMoveEnabled = onClickPunchMove.FindPropertyRelative("enabled");
            onClickPunchMovePunch = onClickPunchMove.FindPropertyRelative("punch");
            onClickPunchMoveStartDelay = onClickPunchMove.FindPropertyRelative("startDelay");
            onClickPunchMoveDuration = onClickPunchMove.FindPropertyRelative("duration");
            onClickPunchMoveVibrato = onClickPunchMove.FindPropertyRelative("vibrato");
            onClickPunchMoveElasticity = onClickPunchMove.FindPropertyRelative("elasticity");
            onClickPunchRotate = onClickPunch.FindPropertyRelative("rotate");
            onClickPunchRotateEnabled = onClickPunchRotate.FindPropertyRelative("enabled");
            onClickPunchRotatePunch = onClickPunchRotate.FindPropertyRelative("punch");
            onClickPunchRotateStartDelay = onClickPunchRotate.FindPropertyRelative("startDelay");
            onClickPunchRotateDuration = onClickPunchRotate.FindPropertyRelative("duration");
            onClickPunchRotateVibrato = onClickPunchRotate.FindPropertyRelative("vibrato");
            onClickPunchRotateElasticity = onClickPunchRotate.FindPropertyRelative("elasticity");
            onClickPunchScale = onClickPunch.FindPropertyRelative("scale");
            onClickPunchScaleEnabled = onClickPunchScale.FindPropertyRelative("enabled");
            onClickPunchScalePunch = onClickPunchScale.FindPropertyRelative("punch");
            onClickPunchScaleStartDelay = onClickPunchScale.FindPropertyRelative("startDelay");
            onClickPunchScaleDuration = onClickPunchScale.FindPropertyRelative("duration");
            onClickPunchScaleVibrato = onClickPunchScale.FindPropertyRelative("vibrato");
            onClickPunchScaleElasticity = onClickPunchScale.FindPropertyRelative("elasticity");
            onClickGameEvents = serializedObject.FindProperty("onClickGameEvents");
            #endregion
            #region OnDoubleClick
            useOnDoubleClick = serializedObject.FindProperty("useOnDoubleClick");
            waitForOnDoubleClickAnimation = serializedObject.FindProperty("waitForOnDoubleClickAnimation");
            doubleClickRegisterInterval = serializedObject.FindProperty("doubleClickRegisterInterval");
            onDoubleClickSound = serializedObject.FindProperty("onDoubleClickSound");
            customOnDoubleClickSound = serializedObject.FindProperty("customOnDoubleClickSound");
            OnDoubleClick = serializedObject.FindProperty("OnDoubleClick");
            onDoubleClickPunchPresetCategory = serializedObject.FindProperty("onDoubleClickPunchPresetCategory");
            onDoubleClickPunchPresetName = serializedObject.FindProperty("onDoubleClickPunchPresetName");
            loadOnDoubleClickPunchPresetAtRuntime = serializedObject.FindProperty("loadOnDoubleClickPunchPresetAtRuntime");
            onDoubleClickPunch = serializedObject.FindProperty("onDoubleClickPunch");
            onDoubleClickPunchMove = onDoubleClickPunch.FindPropertyRelative("move");
            onDoubleClickPunchMoveEnabled = onDoubleClickPunchMove.FindPropertyRelative("enabled");
            onDoubleClickPunchMovePunch = onDoubleClickPunchMove.FindPropertyRelative("punch");
            onDoubleClickPunchMoveStartDelay = onDoubleClickPunchMove.FindPropertyRelative("startDelay");
            onDoubleClickPunchMoveDuration = onDoubleClickPunchMove.FindPropertyRelative("duration");
            onDoubleClickPunchMoveVibrato = onDoubleClickPunchMove.FindPropertyRelative("vibrato");
            onDoubleClickPunchMoveElasticity = onDoubleClickPunchMove.FindPropertyRelative("elasticity");
            onDoubleClickPunchRotate = onDoubleClickPunch.FindPropertyRelative("rotate");
            onDoubleClickPunchRotateEnabled = onDoubleClickPunchRotate.FindPropertyRelative("enabled");
            onDoubleClickPunchRotatePunch = onDoubleClickPunchRotate.FindPropertyRelative("punch");
            onDoubleClickPunchRotateStartDelay = onDoubleClickPunchRotate.FindPropertyRelative("startDelay");
            onDoubleClickPunchRotateDuration = onDoubleClickPunchRotate.FindPropertyRelative("duration");
            onDoubleClickPunchRotateVibrato = onDoubleClickPunchRotate.FindPropertyRelative("vibrato");
            onDoubleClickPunchRotateElasticity = onDoubleClickPunchRotate.FindPropertyRelative("elasticity");
            onDoubleClickPunchScale = onDoubleClickPunch.FindPropertyRelative("scale");
            onDoubleClickPunchScaleEnabled = onDoubleClickPunchScale.FindPropertyRelative("enabled");
            onDoubleClickPunchScalePunch = onDoubleClickPunchScale.FindPropertyRelative("punch");
            onDoubleClickPunchScaleStartDelay = onDoubleClickPunchScale.FindPropertyRelative("startDelay");
            onDoubleClickPunchScaleDuration = onDoubleClickPunchScale.FindPropertyRelative("duration");
            onDoubleClickPunchScaleVibrato = onDoubleClickPunchScale.FindPropertyRelative("vibrato");
            onDoubleClickPunchScaleElasticity = onDoubleClickPunchScale.FindPropertyRelative("elasticity");
            onDoubleClickGameEvents = serializedObject.FindProperty("onDoubleClickGameEvents");
            #endregion
            #region OnLongClick
            useOnLongClick = serializedObject.FindProperty("useOnLongClick");
            waitForOnLongClickAnimation = serializedObject.FindProperty("waitForOnLongClickAnimation");
            longClickRegisterInterval = serializedObject.FindProperty("longClickRegisterInterval");
            onLongClickSound = serializedObject.FindProperty("onLongClickSound");
            customOnLongClickSound = serializedObject.FindProperty("customOnLongClickSound");
            OnLongClick = serializedObject.FindProperty("OnLongClick");
            onLongClickPunchPresetCategory = serializedObject.FindProperty("onLongClickPunchPresetCategory");
            onLongClickPunchPresetName = serializedObject.FindProperty("onLongClickPunchPresetName");
            loadOnLongClickPunchPresetAtRuntime = serializedObject.FindProperty("loadOnLongClickPunchPresetAtRuntime");
            onLongClickPunch = serializedObject.FindProperty("onLongClickPunch");
            onLongClickPunchMove = onLongClickPunch.FindPropertyRelative("move");
            onLongClickPunchMoveEnabled = onLongClickPunchMove.FindPropertyRelative("enabled");
            onLongClickPunchMovePunch = onLongClickPunchMove.FindPropertyRelative("punch");
            onLongClickPunchMoveStartDelay = onLongClickPunchMove.FindPropertyRelative("startDelay");
            onLongClickPunchMoveDuration = onLongClickPunchMove.FindPropertyRelative("duration");
            onLongClickPunchMoveVibrato = onLongClickPunchMove.FindPropertyRelative("vibrato");
            onLongClickPunchMoveElasticity = onLongClickPunchMove.FindPropertyRelative("elasticity");
            onLongClickPunchRotate = onLongClickPunch.FindPropertyRelative("rotate");
            onLongClickPunchRotateEnabled = onLongClickPunchRotate.FindPropertyRelative("enabled");
            onLongClickPunchRotatePunch = onLongClickPunchRotate.FindPropertyRelative("punch");
            onLongClickPunchRotateStartDelay = onLongClickPunchRotate.FindPropertyRelative("startDelay");
            onLongClickPunchRotateDuration = onLongClickPunchRotate.FindPropertyRelative("duration");
            onLongClickPunchRotateVibrato = onLongClickPunchRotate.FindPropertyRelative("vibrato");
            onLongClickPunchRotateElasticity = onLongClickPunchRotate.FindPropertyRelative("elasticity");
            onLongClickPunchScale = onLongClickPunch.FindPropertyRelative("scale");
            onLongClickPunchScaleEnabled = onLongClickPunchScale.FindPropertyRelative("enabled");
            onLongClickPunchScalePunch = onLongClickPunchScale.FindPropertyRelative("punch");
            onLongClickPunchScaleStartDelay = onLongClickPunchScale.FindPropertyRelative("startDelay");
            onLongClickPunchScaleDuration = onLongClickPunchScale.FindPropertyRelative("duration");
            onLongClickPunchScaleVibrato = onLongClickPunchScale.FindPropertyRelative("vibrato");
            onLongClickPunchScaleElasticity = onLongClickPunchScale.FindPropertyRelative("elasticity");
            onLongClickGameEvents = serializedObject.FindProperty("onLongClickGameEvents");
            #endregion
            #region NormalLoop
            normalLoopPresetCategory = serializedObject.FindProperty("normalLoopPresetCategory");
            normalLoopPresetName = serializedObject.FindProperty("normalLoopPresetName");
            loadNormalLoopPresetAtRuntime = serializedObject.FindProperty("loadNormalLoopPresetAtRuntime");
            normalLoop = serializedObject.FindProperty("normalLoop");
            normalLoopMove = normalLoop.FindPropertyRelative("move");
            normalLoopMoveEnabled = normalLoopMove.FindPropertyRelative("enabled");
            normalLoopMoveMovement = normalLoopMove.FindPropertyRelative("movement");
            normalLoopMoveEaseType = normalLoopMove.FindPropertyRelative("easeType");
            normalLoopMoveEase = normalLoopMove.FindPropertyRelative("ease");
            normalLoopMoveAnimationCurve = normalLoopMove.FindPropertyRelative("animationCurve");
            normalLoopMoveLoops = normalLoopMove.FindPropertyRelative("loops");
            normalLoopMoveLoopType = normalLoopMove.FindPropertyRelative("loopType");
            normalLoopMoveStartDelay = normalLoopMove.FindPropertyRelative("startDelay");
            normalLoopMoveDuration = normalLoopMove.FindPropertyRelative("duration");
            normalLoopRotate = normalLoop.FindPropertyRelative("rotate");
            normalLoopRotateEnabled = normalLoopRotate.FindPropertyRelative("enabled");
            normalLoopRotateRotation = normalLoopRotate.FindPropertyRelative("rotation");
            normalLoopRotateEaseType = normalLoopRotate.FindPropertyRelative("easeType");
            normalLoopRotateEase = normalLoopRotate.FindPropertyRelative("ease");
            normalLoopRotateAnimationCurve = normalLoopRotate.FindPropertyRelative("animationCurve");
            normalLoopRotateLoops = normalLoopRotate.FindPropertyRelative("loops");
            normalLoopRotateLoopType = normalLoopRotate.FindPropertyRelative("loopType");
            normalLoopRotateStartDelay = normalLoopRotate.FindPropertyRelative("startDelay");
            normalLoopRotateDuration = normalLoopRotate.FindPropertyRelative("duration");
            normalLoopScale = normalLoop.FindPropertyRelative("scale");
            normalLoopScaleEnabled = normalLoopScale.FindPropertyRelative("enabled");
            normalLoopScaleMin = normalLoopScale.FindPropertyRelative("min");
            normalLoopScaleMax = normalLoopScale.FindPropertyRelative("max");
            normalLoopScaleEaseType = normalLoopScale.FindPropertyRelative("easeType");
            normalLoopScaleEase = normalLoopScale.FindPropertyRelative("ease");
            normalLoopScaleAnimationCurve = normalLoopScale.FindPropertyRelative("animationCurve");
            normalLoopScaleLoops = normalLoopScale.FindPropertyRelative("loops");
            normalLoopScaleLoopType = normalLoopScale.FindPropertyRelative("loopType");
            normalLoopScaleStartDelay = normalLoopScale.FindPropertyRelative("startDelay");
            normalLoopScaleDuration = normalLoopScale.FindPropertyRelative("duration");
            normalLoopFade = normalLoop.FindPropertyRelative("fade");
            normalLoopFadeEnabled = normalLoopFade.FindPropertyRelative("enabled");
            normalLoopFadeMin = normalLoopFade.FindPropertyRelative("min");
            normalLoopFadeMax = normalLoopFade.FindPropertyRelative("max");
            normalLoopFadeEaseType = normalLoopFade.FindPropertyRelative("easeType");
            normalLoopFadeEase = normalLoopFade.FindPropertyRelative("ease");
            normalLoopFadeAnimationCurve = normalLoopFade.FindPropertyRelative("animationCurve");
            normalLoopFadeLoops = normalLoopFade.FindPropertyRelative("loops");
            normalLoopFadeLoopType = normalLoopFade.FindPropertyRelative("loopType");
            normalLoopFadeStartDelay = normalLoopFade.FindPropertyRelative("startDelay");
            normalLoopFadeDuration = normalLoopFade.FindPropertyRelative("duration");
            #endregion
            #region SelectedLoop
            selectedLoopPresetCategory = serializedObject.FindProperty("selectedLoopPresetCategory");
            selectedLoopPresetName = serializedObject.FindProperty("selectedLoopPresetName");
            loadSelectedLoopPresetAtRuntime = serializedObject.FindProperty("loadSelectedLoopPresetAtRuntime");
            selectedLoop = serializedObject.FindProperty("selectedLoop");
            selectedLoopMove = selectedLoop.FindPropertyRelative("move");
            selectedLoopMoveEnabled = selectedLoopMove.FindPropertyRelative("enabled");
            selectedLoopMoveMovement = selectedLoopMove.FindPropertyRelative("movement");
            selectedLoopMoveEaseType = selectedLoopMove.FindPropertyRelative("easeType");
            selectedLoopMoveEase = selectedLoopMove.FindPropertyRelative("ease");
            selectedLoopMoveAnimationCurve = selectedLoopMove.FindPropertyRelative("animationCurve");
            selectedLoopMoveLoops = selectedLoopMove.FindPropertyRelative("loops");
            selectedLoopMoveLoopType = selectedLoopMove.FindPropertyRelative("loopType");
            selectedLoopMoveStartDelay = selectedLoopMove.FindPropertyRelative("startDelay");
            selectedLoopMoveDuration = selectedLoopMove.FindPropertyRelative("duration");
            selectedLoopRotate = selectedLoop.FindPropertyRelative("rotate");
            selectedLoopRotateEnabled = selectedLoopRotate.FindPropertyRelative("enabled");
            selectedLoopRotateRotation = selectedLoopRotate.FindPropertyRelative("rotation");
            selectedLoopRotateEaseType = selectedLoopRotate.FindPropertyRelative("easeType");
            selectedLoopRotateEase = selectedLoopRotate.FindPropertyRelative("ease");
            selectedLoopRotateAnimationCurve = selectedLoopRotate.FindPropertyRelative("animationCurve");
            selectedLoopRotateLoops = selectedLoopRotate.FindPropertyRelative("loops");
            selectedLoopRotateLoopType = selectedLoopRotate.FindPropertyRelative("loopType");
            selectedLoopRotateStartDelay = selectedLoopRotate.FindPropertyRelative("startDelay");
            selectedLoopRotateDuration = selectedLoopRotate.FindPropertyRelative("duration");
            selectedLoopScale = selectedLoop.FindPropertyRelative("scale");
            selectedLoopScaleEnabled = selectedLoopScale.FindPropertyRelative("enabled");
            selectedLoopScaleMin = selectedLoopScale.FindPropertyRelative("min");
            selectedLoopScaleMax = selectedLoopScale.FindPropertyRelative("max");
            selectedLoopScaleEaseType = selectedLoopScale.FindPropertyRelative("easeType");
            selectedLoopScaleEase = selectedLoopScale.FindPropertyRelative("ease");
            selectedLoopScaleAnimationCurve = selectedLoopScale.FindPropertyRelative("animationCurve");
            selectedLoopScaleLoops = selectedLoopScale.FindPropertyRelative("loops");
            selectedLoopScaleLoopType = selectedLoopScale.FindPropertyRelative("loopType");
            selectedLoopScaleStartDelay = selectedLoopScale.FindPropertyRelative("startDelay");
            selectedLoopScaleDuration = selectedLoopScale.FindPropertyRelative("duration");
            selectedLoopFade = selectedLoop.FindPropertyRelative("fade");
            selectedLoopFadeEnabled = selectedLoopFade.FindPropertyRelative("enabled");
            selectedLoopFadeMin = selectedLoopFade.FindPropertyRelative("min");
            selectedLoopFadeMax = selectedLoopFade.FindPropertyRelative("max");
            selectedLoopFadeEaseType = selectedLoopFade.FindPropertyRelative("easeType");
            selectedLoopFadeEase = selectedLoopFade.FindPropertyRelative("ease");
            selectedLoopFadeAnimationCurve = selectedLoopFade.FindPropertyRelative("animationCurve");
            selectedLoopFadeLoops = selectedLoopFade.FindPropertyRelative("loops");
            selectedLoopFadeLoopType = selectedLoopFade.FindPropertyRelative("loopType");
            selectedLoopFadeStartDelay = selectedLoopFade.FindPropertyRelative("startDelay");
            selectedLoopFadeDuration = selectedLoopFade.FindPropertyRelative("duration");
            #endregion
        }

        void GenerateInfoMessages()
        {
            infoMessage = new Dictionary<string, InfoMessage>();
            infoMessage.Add("OnPointerEnterLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = onPointerEnterPunchPresetCategory.stringValue + " / " + onPointerEnterPunchPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadOnPointerEnterPunchPresetAtRuntime.boolValue, Repaint) });
            infoMessage.Add("OnPointerExitLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = onPointerExitPunchPresetCategory.stringValue + " / " + onPointerExitPunchPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadOnPointerExitPunchPresetAtRuntime.boolValue, Repaint) });
            infoMessage.Add("OnPointerDownLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = onPointerDownPunchPresetCategory.stringValue + " / " + onPointerDownPunchPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadOnPointerDownPunchPresetAtRuntime.boolValue, Repaint) });
            infoMessage.Add("OnPointerUpLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = onPointerUpPunchPresetCategory.stringValue + " / " + onPointerUpPunchPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadOnPointerUpPunchPresetAtRuntime.boolValue, Repaint) });
            infoMessage.Add("OnClickLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = onClickPunchPresetCategory.stringValue + " / " + onClickPunchPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadOnClickPunchPresetAtRuntime.boolValue, Repaint) });
            infoMessage.Add("OnDoubleClickLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = onDoubleClickPunchPresetCategory.stringValue + " / " + onDoubleClickPunchPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadOnDoubleClickPunchPresetAtRuntime.boolValue, Repaint) });
            infoMessage.Add("OnLongClickLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = onLongClickPunchPresetCategory.stringValue + " / " + onLongClickPunchPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadOnLongClickPunchPresetAtRuntime.boolValue, Repaint) });
            infoMessage.Add("NormalLoopLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = normalLoopPresetCategory.stringValue + " / " + normalLoopPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadNormalLoopPresetAtRuntime.boolValue, Repaint) });
            infoMessage.Add("SelectedLoopLoadPresetAtRuntime", new InfoMessage() { title = "Runtime Preset", message = selectedLoopPresetCategory.stringValue + " / " + selectedLoopPresetName.stringValue, type = InfoMessageType.Info, show = new AnimBool(loadSelectedLoopPresetAtRuntime.boolValue, Repaint) });
        }

        void InitAnimBools()
        {
            showOnPointerEnter = new AnimBool(false, Repaint);
            showOnPointerEnterPreset = new AnimBool(false, Repaint);
            showOnPointerEnterPunchMove = new AnimBool(false, Repaint);
            showOnPointerEnterPunchRotate = new AnimBool(false, Repaint);
            showOnPointerEnterPunchScale = new AnimBool(false, Repaint);
            showOnPointerEnterEvents = new AnimBool(false, Repaint);
            showOnPointerEnterGameEvents = new AnimBool(false, Repaint);
            showOnPointerEnterNavigation = new AnimBool(false, Repaint);

            showOnPointerExit = new AnimBool(false, Repaint);
            showOnPointerExitPreset = new AnimBool(false, Repaint);
            showOnPointerExitPunchMove = new AnimBool(false, Repaint);
            showOnPointerExitPunchRotate = new AnimBool(false, Repaint);
            showOnPointerExitPunchScale = new AnimBool(false, Repaint);
            showOnPointerExitEvents = new AnimBool(false, Repaint);
            showOnPointerExitGameEvents = new AnimBool(false, Repaint);
            showOnPointerExitNavigation = new AnimBool(false, Repaint);

            showOnPointerDown = new AnimBool(false, Repaint);
            showOnPointerDownPreset = new AnimBool(false, Repaint);
            showOnPointerDownPunchMove = new AnimBool(false, Repaint);
            showOnPointerDownPunchRotate = new AnimBool(false, Repaint);
            showOnPointerDownPunchScale = new AnimBool(false, Repaint);
            showOnPointerDownEvents = new AnimBool(false, Repaint);
            showOnPointerDownGameEvents = new AnimBool(false, Repaint);
            showOnPointerDownNavigation = new AnimBool(false, Repaint);

            showOnPointerUp = new AnimBool(false, Repaint);
            showOnPointerUpPreset = new AnimBool(false, Repaint);
            showOnPointerUpPunchMove = new AnimBool(false, Repaint);
            showOnPointerUpPunchRotate = new AnimBool(false, Repaint);
            showOnPointerUpPunchScale = new AnimBool(false, Repaint);
            showOnPointerUpEvents = new AnimBool(false, Repaint);
            showOnPointerUpGameEvents = new AnimBool(false, Repaint);
            showOnPointerUpNavigation = new AnimBool(false, Repaint);

            showOnClick = new AnimBool(false, Repaint);
            showOnClickPreset = new AnimBool(false, Repaint);
            showOnClickPunchMove = new AnimBool(false, Repaint);
            showOnClickPunchRotate = new AnimBool(false, Repaint);
            showOnClickPunchScale = new AnimBool(false, Repaint);
            showOnClickEvents = new AnimBool(false, Repaint);
            showOnClickGameEvents = new AnimBool(false, Repaint);
            showOnClickNavigation = new AnimBool(false, Repaint);

            showOnDoubleClick = new AnimBool(false, Repaint);
            showOnDoubleClickPreset = new AnimBool(false, Repaint);
            showOnDoubleClickPunchMove = new AnimBool(false, Repaint);
            showOnDoubleClickPunchRotate = new AnimBool(false, Repaint);
            showOnDoubleClickPunchScale = new AnimBool(false, Repaint);
            showOnDoubleClickEvents = new AnimBool(false, Repaint);
            showOnDoubleClickGameEvents = new AnimBool(false, Repaint);
            showOnDoubleClickNavigation = new AnimBool(false, Repaint);

            showOnLongClick = new AnimBool(false, Repaint);
            showOnLongClickPreset = new AnimBool(false, Repaint);
            showOnLongClickPunchMove = new AnimBool(false, Repaint);
            showOnLongClickPunchRotate = new AnimBool(false, Repaint);
            showOnLongClickPunchScale = new AnimBool(false, Repaint);
            showOnLongClickEvents = new AnimBool(false, Repaint);
            showOnLongClickGameEvents = new AnimBool(false, Repaint);
            showOnLongClickNavigation = new AnimBool(false, Repaint);

            showNormalAnimation = new AnimBool(false, Repaint);
            showNormalAnimationPreset = new AnimBool(false, Repaint);
            showNormalAnimationMove = new AnimBool(false, Repaint);
            showNormalAnimationRotate = new AnimBool(false, Repaint);
            showNormalAnimationScale = new AnimBool(false, Repaint);
            showNormalAnimationFade = new AnimBool(false, Repaint);

            showSelectedAnimation = new AnimBool(false, Repaint);
            showSelectedAnimationPreset = new AnimBool(false, Repaint);
            showSelectedAnimationMove = new AnimBool(false, Repaint);
            showSelectedAnimationRotate = new AnimBool(false, Repaint);
            showSelectedAnimationScale = new AnimBool(false, Repaint);
            showSelectedAnimationFade = new AnimBool(false, Repaint);
        }

        protected override void OnEnable()
        {
            requiresContantRepaint = true;
            SerializedObjectFindProperties();
            GenerateInfoMessages();
            InitAnimBools();
        }

        void RefreshData(bool forcedRefresh = false)
        {
            serializedObject.Update();
            RefreshButtonNameAndCategory(forcedRefresh);
            RefreshUISounds(forcedRefresh);
            RefreshPunchAnimations(forcedRefresh);
            RefreshLoopAnimations(forcedRefresh);
            RefreshNavigationData(forcedRefresh);
            serializedObject.ApplyModifiedProperties();
        }
        void RefreshButtonNameAndCategory(bool forcedRefresh)
        {
            RefreshUIButtonsDatabase(forcedRefresh);
            ValiateUIButtonNameAndCategory();
        }
        void RefreshUISounds(bool forcedRefresh)
        {
            RefreshUISoundsDatabase(forcedRefresh);
            ValidateUISounds();
        }
        void RefreshPunchAnimations(bool forcedRefresh)
        {
            RefreshPunchAnimationsPresets(forcedRefresh);
            ValidatePunchAnimationsPresets();
        }
        void RefreshLoopAnimations(bool forcedRefresh)
        {
            RefreshLoopAnimationsPresets(forcedRefresh);
            ValidateLoopAnimationsPresets();
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerUIButton.texture, WIDTH_420, HEIGHT_42);
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
            DrawTopButtons();
            DrawButtonCategory();
            DrawButtonName();
            DrawSettings();
            DrawOnPointerEnter();
            DrawOnPointerExit();
            DrawOnPointerDown();
            DrawOnPointerUp();
            DrawOnClick();
            DrawOnDoubleClick();
            DrawOnLongClick();
            QUI.Space(SPACE_8);
            DrawNormalLoop();
            DrawSelectedLoop();
            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }

        void DrawTopButtons()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button("UIButtons Database"))
                {
                    ControlPanelWindow.Open(ControlPanelWindow.Section.UIButtons);
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
            if (DUI.DUISettings.UIButton_Inspector_ShowButtonRenameGameObject)
            {
                QUI.BeginHorizontal(WIDTH_420);
                {
                    if (QUI.Button("Rename GameObject to Button Name"))
                    {
                        if (serializedObject.isEditingMultipleObjects)
                        {
                            Undo.RecordObjects(targets, "Renamed Multiple Objects");
                            for (int i = 0; i < targets.Length; i++)
                            {
                                UIButton iTarget = (UIButton)targets[i];
                                iTarget.gameObject.name = DUI.DUISettings.UIButton_Inspector_RenameGameObjectPrefix + iTarget.buttonName + DUI.DUISettings.UIButton_Inspector_RenameGameObjectSuffix;
                            }
                        }
                        else
                        {
                            uiButton.gameObject.name = DUI.DUISettings.UIButton_Inspector_RenameGameObjectPrefix + buttonName.stringValue + DUI.DUISettings.UIButton_Inspector_RenameGameObjectSuffix;
                        }
                    }
                }
                QUI.EndHorizontal();
            }
        }

        void DrawButtonCategory()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Button Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    QUI.Label(buttonCategory.stringValue, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic));
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        buttonCategoryIndex = EditorGUILayout.Popup(buttonCategoryIndex, DUI.UIButtonCategories.ToArray());
                    }
                    if (QUI.EndChangeCheck())
                    {
                        if (!DUI.UIButtonCategories[buttonCategoryIndex].Equals(DUI.CUSTOM_NAME)) //not custom name category?
                        {
                            if (DUI.UIButtonNameExists(DUI.UIButtonCategories[buttonCategoryIndex], buttonName.stringValue)) //does the new category have the button name?
                            {
                                buttonCategory.stringValue = DUI.UIButtonCategories[buttonCategoryIndex];
                                buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                            }
                            else if (buttonName.stringValue.Equals(DUI.DEFAULT_BUTTON_NAME) && DUI.GetUIButtonNames(DUI.UIButtonCategories[buttonCategoryIndex]).Count > 0) //is the button name the default value? && is the new category not empty? -> set button name as the first value
                            {
                                buttonCategory.stringValue = DUI.UIButtonCategories[buttonCategoryIndex];
                                buttonName.stringValue = DUI.GetUIButtonNames(buttonCategory.stringValue)[0];
                            }
                            else if (!buttonName.stringValue.Equals(DUI.DEFAULT_BUTTON_NAME) && !string.IsNullOrEmpty(buttonName.stringValue.Trim()) && EditorUtility.DisplayDialog("Action Required", "The '" + buttonName.stringValue + "' button name does not exist in the '" + DUI.UIButtonCategories[buttonCategoryIndex] + "' category database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.AddUIButtonName(DUI.UIButtonCategories[buttonCategoryIndex], buttonName.stringValue);
                                buttonCategory.stringValue = DUI.UIButtonCategories[buttonCategoryIndex];
                                buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                            }
                            else if (DUI.GetUIButtonNames(DUI.UIButtonCategories[buttonCategoryIndex]).Count == 0)
                            {
                                if (EditorUtility.DisplayDialog("Information", "The '" + DUI.UIButtonCategories[buttonCategoryIndex] + "' category is empty.\n\nOpen the UIButtons Database and add some button names to it or delete it.\n\nThe button name and category will now be reset to the default values.", "Ok"))
                                {
                                    buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                                    buttonCategoryIndex = DUI.UIButtonCategories.IndexOf(buttonCategory.stringValue);
                                    buttonName.stringValue = DUI.DEFAULT_BUTTON_NAME;
                                    buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                                }
                            }
                            else
                            {
                                buttonCategory.stringValue = DUI.UIButtonCategories[buttonCategoryIndex];
                                buttonName.stringValue = DUI.GetUIButtonNames(buttonCategory.stringValue)[0];
                                buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                            }
                        }
                        else
                        {
                            buttonCategory.stringValue = DUI.UIButtonCategories[buttonCategoryIndex];
                        }
                    }
                }
            }
            QUI.EndHorizontal();
        }
        void DrawButtonName()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Button Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    QUI.Label(buttonName.stringValue, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic));
                }
                else
                {
                    if (buttonCategory.stringValue.Equals(DUI.CUSTOM_NAME))
                    {
                        QUI.PropertyField(buttonName);
                    }
                    else
                    {
                        if (DUI.GetUIButtonNames(DUI.UIButtonCategories[buttonCategoryIndex]).Count == 0)
                        {
                            QUI.Label(buttonName.stringValue, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic), 90);
                            QUI.FlexibleSpace();
                            QUI.Label("(Empty Category)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmallItalic), 86);
                        }
                        else
                        {
                            QUI.BeginChangeCheck();
                            {
                                if (!DUI.UIButtonCategoryExists(buttonCategory.stringValue)) { RefreshButtonNameAndCategory(true); }
                                buttonNameIndex = EditorGUILayout.Popup(buttonNameIndex, DUI.GetUIButtonNames(buttonCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                buttonName.stringValue = DUI.GetUIButtonNames(buttonCategory.stringValue)[buttonNameIndex];
                            }
                        }
                    }
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
        }

        void DrawSettings()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.PropertyField(allowMultipleClicks, 12);
                QUI.Label("allow multiple clicks", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 120);
                if (!allowMultipleClicks.boolValue)
                {
                    QUI.Space(55);
                    QUI.Label("disable button interval", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 126);
                    QUI.PropertyField(disableButtonInterval, 38);
                    QUI.Label("seconds", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 50);
                }
            }
            QUI.EndHorizontal();
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.PropertyField(deselectButtonOnClick, 12);
                QUI.Label("deselect button on click", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 140);
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
        }

        void DrawOnPointerEnter()
        {
            if (DUI.DUISettings == null || DUI.DUISettings.UIButton_Inspector_HideOnPointerEnter) { useOnPointerEnter.boolValue = false; return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (!useOnPointerEnter.boolValue)
                {
                    QUI.DrawTexture(DUIResources.barOnPointerEnterDisabled.texture, 336, 21);
                    if (showOnPointerEnter.target) { showOnPointerEnter.target = false; }
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(showOnPointerEnter.target ? DUIStyles.ButtonStyle.OnPointerEnter : DUIStyles.ButtonStyle.OnPointerEnterCollapsed), 336, 21)) { showOnPointerEnter.target = !showOnPointerEnter.target; }
                }
                if (QUI.Button(DUIStyles.GetStyle(useOnPointerEnter.boolValue ? DUIStyles.ButtonStyle.BarButtonEnabled : DUIStyles.ButtonStyle.BarButtonDisabled), 84, 21)) { useOnPointerEnter.boolValue = !useOnPointerEnter.boolValue; if (useOnPointerEnter.boolValue) { showOnPointerEnter.target = true; } }
            }
            QUI.EndHorizontal();
            infoMessage["OnPointerEnterLoadPresetAtRuntime"].show.target = loadOnPointerEnterPunchPresetAtRuntime.boolValue;
            infoMessage["OnPointerEnterLoadPresetAtRuntime"].message = onPointerEnterPunchPresetCategory.stringValue + " / " + onPointerEnterPunchPresetName.stringValue;
            DrawInfoMessage("OnPointerEnterLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (!useOnPointerEnter.boolValue) { return; }
            if (QUI.BeginFadeGroup(showOnPointerEnter.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    DrawOnPointerEnterSettings();
                    QUI.Space(SPACE_2);
                    DrawOnPointerEnterSound();
                    QUI.Space(SPACE_2);
                    DrawOnPointerEnterPreset();
                    QUI.Space(SPACE_2);
                    DrawOnPointerEnterPunchMove();
                    QUI.Space(SPACE_2);
                    DrawOnPointerEnterPunchRotate();
                    QUI.Space(SPACE_2);
                    DrawOnPointerEnterPunchScale();
                    QUI.Space(SPACE_4);
                    DrawOnPointerEnterEvents();
                    QUI.Space(SPACE_2);
                    DrawOnPointerEnterGameEvents();
                    QUI.Space(SPACE_2);
                    DrawOnPointerEnterNavigation();
                    QUI.Space(SPACE_16);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerEnterSettings()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("disable interval", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                QUI.PropertyField(onPointerEnterDisableInterval, 38);
                QUI.Label("seconds", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 50);
            }
            QUI.EndHorizontal();
        }
        void DrawOnPointerEnterSound()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("play sound", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 65);
                if (customOnPointerEnterSound.boolValue)
                {
                    QUI.PropertyField(onPointerEnterSound, 230);
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        onPointerEnterSoundIndex = EditorGUILayout.Popup(onPointerEnterSoundIndex, DUI.UISoundNamesUIButtons.ToArray(), GUILayout.Width(230));
                    }
                    if (QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onPointerEnterSound.stringValue = DUI.UISoundNamesUIButtons[onPointerEnterSoundIndex];
                    }
                }
                QUI.Space(SPACE_4);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(customOnPointerEnterSound, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (!customOnPointerEnterSound.boolValue)
                    {
                        if (!DUI.UISoundNamesUIButtons.Contains(onPointerEnterSound.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "The '" + onPointerEnterSound.stringValue + "' ui sound does not exist in the UISounds database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.CreateUISound(onPointerEnterSound.stringValue, SoundType.UIButtons, null);
                            }
                            else
                            {
                                onPointerEnterSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                            }
                        }
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onPointerEnterSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onPointerEnterSound.stringValue);
                        RefreshUISounds(false);
                    }
                }
                QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonPlay), 18, 18)) { DUIUtils.PreviewSound(onPointerEnterSound.stringValue); }
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonStop), 18, 18)) { DUIUtils.StopSoundPreview(onPointerEnterSound.stringValue); }
            }
            QUI.EndHorizontal();
        }
        void DrawOnPointerEnterPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerEnterPreset.target ? DUIStyles.ButtonStyle.barPunchPreset : DUIStyles.ButtonStyle.barPunchPresetCollapsed), WIDTH_420, 18)) { showOnPointerEnterPreset.target = !showOnPointerEnterPreset.target; }
            if (QUI.BeginFadeGroup(showOnPointerEnterPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!onPointerEnterPunchNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Punch iPunch = UIAnimatorUtil.GetPunch(onPointerEnterPunchPresetCategory.stringValue, onPointerEnterPunchPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIButton iTarget = (UIButton)targets[i];
                                        iTarget.onPointerEnterPunch = iPunch.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiButton, "Load Preset");
                                    uiButton.onPointerEnterPunch = UIAnimatorUtil.GetPunch(onPointerEnterPunchPresetCategory.stringValue, onPointerEnterPunchPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                onPointerEnterPunchNewPreset = true;
                                onPointerEnterPunchNewCategoryName = false;
                                newPresetCategoryName = onPointerEnterPunchPresetCategory.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + onPointerEnterPunchPresetName.stringValue + "' preset from the '" + onPointerEnterPunchPresetCategory.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeletePunchPreset(onPointerEnterPunchPresetCategory.stringValue, onPointerEnterPunchPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            if (iTarget.onPointerEnterPunchPresetCategory.Equals(onPointerEnterPunchPresetCategory.stringValue) ||
                                                iTarget.onPointerEnterPunchPresetName.Equals(onPointerEnterPunchPresetName.stringValue))
                                            {
                                                iTarget.onPointerEnterPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.onPointerEnterPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    onPointerEnterPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    onPointerEnterPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
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
                                onPointerEnterPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onPointerEnterPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                onPointerEnterPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onPointerEnterPunchPresetCategoryNameIndex];
                                onPointerEnterPunchPresetNameIndex = 0;
                                onPointerEnterPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onPointerEnterPunchPresetCategory.stringValue)[onPointerEnterPunchPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                onPointerEnterPunchPresetNameIndex = EditorGUILayout.Popup(onPointerEnterPunchPresetNameIndex, UIAnimatorUtil.GetPunchPresetNames(onPointerEnterPunchPresetCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                onPointerEnterPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onPointerEnterPunchPresetCategory.stringValue)[onPointerEnterPunchPresetNameIndex];
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
                                if (onPointerEnterPunchNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset category name cannot be an empty string.", "Ok");
                                }
                                else if (string.IsNullOrEmpty(newPresetName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset name cannot be an empty string.", "Ok");
                                }
                                else if (UIAnimatorUtil.PunchPresetExists(newPresetCategoryName, newPresetName))
                                {
                                    EditorUtility.DisplayDialog("Information", "There is another preset with the '" + newPresetName + "' preset name in the '" + newPresetCategoryName + "' preset category. Try a different preset name maybe?", "Ok");
                                }
                                else
                                {
                                    UIAnimatorUtil.CreatePunchPreset(newPresetCategoryName, newPresetName, uiButton.onPointerEnterPunch.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            iTarget.onPointerEnterPunchPresetCategory = newPresetCategoryName;
                                            iTarget.onPointerEnterPunchPresetName = newPresetName;
                                        }
                                    }
                                    onPointerEnterPunchPresetCategory.stringValue = newPresetCategoryName;
                                    onPointerEnterPunchPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
                                    onPointerEnterPunchNewPreset = false;
                                    onPointerEnterPunchNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                onPointerEnterPunchNewPreset = false;
                                onPointerEnterPunchNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!onPointerEnterPunchNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    onPointerEnterPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onPointerEnterPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    onPointerEnterPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onPointerEnterPunchPresetCategoryNameIndex];
                                    onPointerEnterPunchPresetNameIndex = 0;
                                    newPresetCategoryName = onPointerEnterPunchPresetCategory.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    onPointerEnterPunchNewCategoryName = QUI.Toggle(onPointerEnterPunchNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (onPointerEnterPunchNewCategoryName) { newPresetCategoryName = ""; }
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
                                onPointerEnterPunchNewCategoryName = QUI.Toggle(onPointerEnterPunchNewCategoryName);
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
                        QUI.Toggle(loadOnPointerEnterPunchPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerEnterPunchMove()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerEnterPunchMoveEnabled.boolValue ? DUIStyles.ButtonStyle.PunchMove : DUIStyles.ButtonStyle.PunchMoveDisabled), WIDTH_420, 18)) { onPointerEnterPunchMoveEnabled.boolValue = !onPointerEnterPunchMoveEnabled.boolValue; }
            showOnPointerEnterPunchMove.target = onPointerEnterPunchMoveEnabled.boolValue;
            DrawPunch(DUIColors.GreenLight.Color, showOnPointerEnterPunchMove, onPointerEnterPunchMovePunch, onPointerEnterPunchMoveStartDelay, onPointerEnterPunchMoveDuration, onPointerEnterPunchMoveVibrato, onPointerEnterPunchMoveElasticity);
        }
        void DrawOnPointerEnterPunchRotate()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerEnterPunchRotateEnabled.boolValue ? DUIStyles.ButtonStyle.PunchRotate : DUIStyles.ButtonStyle.PunchRotateDisabled), WIDTH_420, 18)) { onPointerEnterPunchRotateEnabled.boolValue = !onPointerEnterPunchRotateEnabled.boolValue; }
            showOnPointerEnterPunchRotate.target = onPointerEnterPunchRotateEnabled.boolValue;
            DrawPunch(DUIColors.OrangeLight.Color, showOnPointerEnterPunchRotate, onPointerEnterPunchRotatePunch, onPointerEnterPunchRotateStartDelay, onPointerEnterPunchRotateDuration, onPointerEnterPunchRotateVibrato, onPointerEnterPunchRotateElasticity);
        }
        void DrawOnPointerEnterPunchScale()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerEnterPunchScaleEnabled.boolValue ? DUIStyles.ButtonStyle.PunchScale : DUIStyles.ButtonStyle.PunchScaleDisabled), WIDTH_420, 18)) { onPointerEnterPunchScaleEnabled.boolValue = !onPointerEnterPunchScaleEnabled.boolValue; }
            showOnPointerEnterPunchScale.target = onPointerEnterPunchScaleEnabled.boolValue;
            DrawPunch(DUIColors.RedLight.Color, showOnPointerEnterPunchScale, onPointerEnterPunchScalePunch, onPointerEnterPunchScaleStartDelay, onPointerEnterPunchScaleDuration, onPointerEnterPunchScaleVibrato, onPointerEnterPunchScaleElasticity);
        }
        void DrawOnPointerEnterEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerEnterEvents.target ? DUIStyles.ButtonStyle.Events : DUIStyles.ButtonStyle.EventsCollapsed), WIDTH_420, 18)) { showOnPointerEnterEvents.target = !showOnPointerEnterEvents.target; }
            if (QUI.BeginFadeGroup(showOnPointerEnterEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.PropertyField(OnPointerEnter, new GUIContent() { text = "OnPointerEnter" }, WIDTH_420 - 6);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerEnterGameEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerEnterGameEvents.target ? DUIStyles.ButtonStyle.GameEvents : DUIStyles.ButtonStyle.GameEventsCollapsed), WIDTH_420, 18)) { showOnPointerEnterGameEvents.target = !showOnPointerEnterGameEvents.target; }
            if (QUI.BeginFadeGroup(showOnPointerEnterGameEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.DrawList(onPointerEnterGameEvents, WIDTH_420, "No Game Events are sent OnPointerEnter... Click [+] to start...");
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerEnterNavigation()
        {
            if (!UIManager.IsNavigationEnabled) { QUI.Space(-SPACE_2); return; }
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerEnterNavigation.target ? DUIStyles.ButtonStyle.Navigation : DUIStyles.ButtonStyle.NavigationCollapsed), WIDTH_420, 18)) { showOnPointerEnterNavigation.target = !showOnPointerEnterNavigation.target; }
            if (QUI.BeginFadeGroup(showOnPointerEnterNavigation.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    DrawNavigationData(uiButton.onPointerEnterNavigation, onPointerEnterEditorNavigationData);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                QUI.ResetColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawOnPointerExit()
        {
            if (DUI.DUISettings == null || DUI.DUISettings.UIButton_Inspector_HideOnPointerExit) { useOnPointerExit.boolValue = false; return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (!useOnPointerExit.boolValue)
                {
                    QUI.DrawTexture(DUIResources.barOnPointerExitDisabled.texture, 336, 21);
                    if (showOnPointerExit.target) { showOnPointerExit.target = false; }
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(showOnPointerExit.target ? DUIStyles.ButtonStyle.OnPointerExit : DUIStyles.ButtonStyle.OnPointerExitCollapsed), 336, 21)) { showOnPointerExit.target = !showOnPointerExit.target; }
                }
                if (QUI.Button(DUIStyles.GetStyle(useOnPointerExit.boolValue ? DUIStyles.ButtonStyle.BarButtonEnabled : DUIStyles.ButtonStyle.BarButtonDisabled), 84, 21)) { useOnPointerExit.boolValue = !useOnPointerExit.boolValue; if (useOnPointerExit.boolValue) { showOnPointerExit.target = true; } }
            }
            QUI.EndHorizontal();
            infoMessage["OnPointerExitLoadPresetAtRuntime"].show.target = loadOnPointerExitPunchPresetAtRuntime.boolValue;
            infoMessage["OnPointerExitLoadPresetAtRuntime"].message = onPointerExitPunchPresetCategory.stringValue + " / " + onPointerExitPunchPresetName.stringValue;
            DrawInfoMessage("OnPointerExitLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (!useOnPointerExit.boolValue) { return; }
            if (QUI.BeginFadeGroup(showOnPointerExit.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    DrawOnPointerExitSettings();
                    QUI.Space(SPACE_2);
                    DrawOnPointerExitSound();
                    QUI.Space(SPACE_2);
                    DrawOnPointerExitPreset();
                    QUI.Space(SPACE_2);
                    DrawOnPointerExitPunchMove();
                    QUI.Space(SPACE_2);
                    DrawOnPointerExitPunchRotate();
                    QUI.Space(SPACE_2);
                    DrawOnPointerExitPunchScale();
                    QUI.Space(SPACE_4);
                    DrawOnPointerExitEvents();
                    QUI.Space(SPACE_2);
                    DrawOnPointerExitGameEvents();
                    QUI.Space(SPACE_2);
                    DrawOnPointerExitNavigation();
                    QUI.Space(SPACE_16);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerExitSettings()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("disable interval", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                QUI.PropertyField(onPointerExitDisableInterval, 38);
                QUI.Label("seconds", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 50);
            }
            QUI.EndHorizontal();
        }
        void DrawOnPointerExitSound()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("play sound", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 65);
                if (customOnPointerExitSound.boolValue)
                {
                    QUI.PropertyField(onPointerExitSound, 230);
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        onPointerExitSoundIndex = EditorGUILayout.Popup(onPointerExitSoundIndex, DUI.UISoundNamesUIButtons.ToArray(), GUILayout.Width(230));
                    }
                    if (QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onPointerExitSound.stringValue = DUI.UISoundNamesUIButtons[onPointerExitSoundIndex];
                    }
                }
                QUI.Space(SPACE_4);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(customOnPointerExitSound, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (!customOnPointerExitSound.boolValue)
                    {
                        if (!DUI.UISoundNamesUIButtons.Contains(onPointerExitSound.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "The '" + onPointerExitSound.stringValue + "' ui sound does not exist in the UISounds database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.CreateUISound(onPointerExitSound.stringValue, SoundType.UIButtons, null);
                            }
                            else
                            {
                                onPointerExitSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                            }
                        }
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onPointerExitSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onPointerExitSound.stringValue);
                        RefreshUISounds(false);
                    }
                }
                QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonPlay), 18, 18)) { DUIUtils.PreviewSound(onPointerExitSound.stringValue); }
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonStop), 18, 18)) { DUIUtils.StopSoundPreview(onPointerExitSound.stringValue); }
            }
            QUI.EndHorizontal();
        }
        void DrawOnPointerExitPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerExitPreset.target ? DUIStyles.ButtonStyle.barPunchPreset : DUIStyles.ButtonStyle.barPunchPresetCollapsed), WIDTH_420, 18)) { showOnPointerExitPreset.target = !showOnPointerExitPreset.target; }
            if (QUI.BeginFadeGroup(showOnPointerExitPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!onPointerExitPunchNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Punch iPunch = UIAnimatorUtil.GetPunch(onPointerExitPunchPresetCategory.stringValue, onPointerExitPunchPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIButton iTarget = (UIButton)targets[i];
                                        iTarget.onPointerExitPunch = iPunch.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiButton, "Load Preset");
                                    uiButton.onPointerExitPunch = UIAnimatorUtil.GetPunch(onPointerExitPunchPresetCategory.stringValue, onPointerExitPunchPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                onPointerExitPunchNewPreset = true;
                                onPointerExitPunchNewCategoryName = false;
                                newPresetCategoryName = onPointerExitPunchPresetCategory.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + onPointerExitPunchPresetName.stringValue + "' preset from the '" + onPointerExitPunchPresetCategory.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeletePunchPreset(onPointerExitPunchPresetCategory.stringValue, onPointerExitPunchPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            if (iTarget.onPointerExitPunchPresetCategory.Equals(onPointerExitPunchPresetCategory.stringValue) ||
                                                iTarget.onPointerExitPunchPresetName.Equals(onPointerExitPunchPresetName.stringValue))
                                            {
                                                iTarget.onPointerExitPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.onPointerExitPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    onPointerExitPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    onPointerExitPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
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
                                onPointerExitPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onPointerExitPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                onPointerExitPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onPointerExitPunchPresetCategoryNameIndex];
                                onPointerExitPunchPresetNameIndex = 0;
                                onPointerExitPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onPointerExitPunchPresetCategory.stringValue)[onPointerExitPunchPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                onPointerExitPunchPresetNameIndex = EditorGUILayout.Popup(onPointerExitPunchPresetNameIndex, UIAnimatorUtil.GetPunchPresetNames(onPointerExitPunchPresetCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                onPointerExitPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onPointerExitPunchPresetCategory.stringValue)[onPointerExitPunchPresetNameIndex];
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
                                if (onPointerExitPunchNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset category name cannot be an empty string.", "Ok");
                                }
                                else if (string.IsNullOrEmpty(newPresetName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset name cannot be an empty string.", "Ok");
                                }
                                else if (UIAnimatorUtil.PunchPresetExists(newPresetCategoryName, newPresetName))
                                {
                                    EditorUtility.DisplayDialog("Information", "There is another preset with the '" + newPresetName + "' preset name in the '" + newPresetCategoryName + "' preset category. Try a different preset name maybe?", "Ok");
                                }
                                else
                                {
                                    UIAnimatorUtil.CreatePunchPreset(newPresetCategoryName, newPresetName, uiButton.onPointerExitPunch.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            iTarget.onPointerExitPunchPresetCategory = newPresetCategoryName;
                                            iTarget.onPointerExitPunchPresetName = newPresetName;
                                        }
                                    }
                                    onPointerExitPunchPresetCategory.stringValue = newPresetCategoryName;
                                    onPointerExitPunchPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
                                    onPointerExitPunchNewPreset = false;
                                    onPointerExitPunchNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                onPointerExitPunchNewPreset = false;
                                onPointerExitPunchNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!onPointerExitPunchNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    onPointerExitPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onPointerExitPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    onPointerExitPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onPointerExitPunchPresetCategoryNameIndex];
                                    onPointerExitPunchPresetNameIndex = 0;
                                    newPresetCategoryName = onPointerExitPunchPresetCategory.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    onPointerExitPunchNewCategoryName = QUI.Toggle(onPointerExitPunchNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (onPointerExitPunchNewCategoryName) { newPresetCategoryName = ""; }
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
                                onPointerExitPunchNewCategoryName = QUI.Toggle(onPointerExitPunchNewCategoryName);
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
                        QUI.Toggle(loadOnPointerExitPunchPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerExitPunchMove()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerExitPunchMoveEnabled.boolValue ? DUIStyles.ButtonStyle.PunchMove : DUIStyles.ButtonStyle.PunchMoveDisabled), WIDTH_420, 18)) { onPointerExitPunchMoveEnabled.boolValue = !onPointerExitPunchMoveEnabled.boolValue; }
            showOnPointerExitPunchMove.target = onPointerExitPunchMoveEnabled.boolValue;
            DrawPunch(DUIColors.GreenLight.Color, showOnPointerExitPunchMove, onPointerExitPunchMovePunch, onPointerExitPunchMoveStartDelay, onPointerExitPunchMoveDuration, onPointerExitPunchMoveVibrato, onPointerExitPunchMoveElasticity);
        }
        void DrawOnPointerExitPunchRotate()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerExitPunchRotateEnabled.boolValue ? DUIStyles.ButtonStyle.PunchRotate : DUIStyles.ButtonStyle.PunchRotateDisabled), WIDTH_420, 18)) { onPointerExitPunchRotateEnabled.boolValue = !onPointerExitPunchRotateEnabled.boolValue; }
            showOnPointerExitPunchRotate.target = onPointerExitPunchRotateEnabled.boolValue;
            DrawPunch(DUIColors.OrangeLight.Color, showOnPointerExitPunchRotate, onPointerExitPunchRotatePunch, onPointerExitPunchRotateStartDelay, onPointerExitPunchRotateDuration, onPointerExitPunchRotateVibrato, onPointerExitPunchRotateElasticity);

        }
        void DrawOnPointerExitPunchScale()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerExitPunchScaleEnabled.boolValue ? DUIStyles.ButtonStyle.PunchScale : DUIStyles.ButtonStyle.PunchScaleDisabled), WIDTH_420, 18)) { onPointerExitPunchScaleEnabled.boolValue = !onPointerExitPunchScaleEnabled.boolValue; }
            showOnPointerExitPunchScale.target = onPointerExitPunchScaleEnabled.boolValue;
            DrawPunch(DUIColors.RedLight.Color, showOnPointerExitPunchScale, onPointerExitPunchScalePunch, onPointerExitPunchScaleStartDelay, onPointerExitPunchScaleDuration, onPointerExitPunchScaleVibrato, onPointerExitPunchScaleElasticity);
        }
        void DrawOnPointerExitEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerExitEvents.target ? DUIStyles.ButtonStyle.Events : DUIStyles.ButtonStyle.EventsCollapsed), WIDTH_420, 18)) { showOnPointerExitEvents.target = !showOnPointerExitEvents.target; }
            if (QUI.BeginFadeGroup(showOnPointerExitEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.PropertyField(OnPointerExit, new GUIContent() { text = "OnPointerExit" }, WIDTH_420 - 6);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerExitGameEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerExitGameEvents.target ? DUIStyles.ButtonStyle.GameEvents : DUIStyles.ButtonStyle.GameEventsCollapsed), WIDTH_420, 18)) { showOnPointerExitGameEvents.target = !showOnPointerExitGameEvents.target; }
            if (QUI.BeginFadeGroup(showOnPointerExitGameEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.DrawList(onPointerExitGameEvents, WIDTH_420, "No Game Events are sent OnPointerExit... Click [+] to start...");
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerExitNavigation()
        {
            if (!UIManager.IsNavigationEnabled) { QUI.Space(-SPACE_2); return; }
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerExitNavigation.target ? DUIStyles.ButtonStyle.Navigation : DUIStyles.ButtonStyle.NavigationCollapsed), WIDTH_420, 18)) { showOnPointerExitNavigation.target = !showOnPointerExitNavigation.target; }
            if (QUI.BeginFadeGroup(showOnPointerExitNavigation.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    DrawNavigationData(uiButton.onPointerExitNavigation, onPointerExitEditorNavigationData);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                QUI.ResetColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawOnPointerDown()
        {
            if (DUI.DUISettings == null || DUI.DUISettings.UIButton_Inspector_HideOnPointerDown) { useOnPointerDown.boolValue = false; return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (!useOnPointerDown.boolValue)
                {
                    QUI.DrawTexture(DUIResources.barOnPointerDownDisabled.texture, 336, 21);
                    if (showOnPointerDown.target) { showOnPointerDown.target = false; }
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(showOnPointerDown.target ? DUIStyles.ButtonStyle.OnPointerDown : DUIStyles.ButtonStyle.OnPointerDownCollapsed), 336, 21)) { showOnPointerDown.target = !showOnPointerDown.target; }
                }
                if (QUI.Button(DUIStyles.GetStyle(useOnPointerDown.boolValue ? DUIStyles.ButtonStyle.BarButtonEnabled : DUIStyles.ButtonStyle.BarButtonDisabled), 84, 21)) { useOnPointerDown.boolValue = !useOnPointerDown.boolValue; if (useOnPointerDown.boolValue) { showOnPointerDown.target = true; } }
            }
            QUI.EndHorizontal();
            infoMessage["OnPointerDownLoadPresetAtRuntime"].show.target = loadOnPointerDownPunchPresetAtRuntime.boolValue;
            infoMessage["OnPointerDownLoadPresetAtRuntime"].message = onPointerDownPunchPresetCategory.stringValue + " / " + onPointerDownPunchPresetName.stringValue;
            DrawInfoMessage("OnPointerDownLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (!useOnPointerDown.boolValue) { return; }
            if (QUI.BeginFadeGroup(showOnPointerDown.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    DrawOnPointerDownSound();
                    QUI.Space(SPACE_2);
                    DrawOnPointerDownPreset();
                    QUI.Space(SPACE_2);
                    DrawOnPointerDownPunchMove();
                    QUI.Space(SPACE_2);
                    DrawOnPointerDownPunchRotate();
                    QUI.Space(SPACE_2);
                    DrawOnPointerDownPunchScale();
                    QUI.Space(SPACE_4);
                    DrawOnPointerDownEvents();
                    QUI.Space(SPACE_2);
                    DrawOnPointerDownGameEvents();
                    QUI.Space(SPACE_2);
                    DrawOnPointerDownNavigation();
                    QUI.Space(SPACE_16);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerDownSound()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("play sound", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 65);
                if (customOnPointerDownSound.boolValue)
                {
                    QUI.PropertyField(onPointerDownSound, 230);
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        onPointerDownSoundIndex = EditorGUILayout.Popup(onPointerDownSoundIndex, DUI.UISoundNamesUIButtons.ToArray(), GUILayout.Width(230));
                    }
                    if (QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onPointerDownSound.stringValue = DUI.UISoundNamesUIButtons[onPointerDownSoundIndex];
                    }
                }
                QUI.Space(SPACE_4);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(customOnPointerDownSound, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (!customOnPointerDownSound.boolValue)
                    {
                        if (!DUI.UISoundNamesUIButtons.Contains(onPointerDownSound.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "The '" + onPointerDownSound.stringValue + "' ui sound does not exist in the UISounds database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.CreateUISound(onPointerDownSound.stringValue, SoundType.UIButtons, null);
                            }
                            else
                            {
                                onPointerDownSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                            }
                        }
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onPointerDownSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onPointerDownSound.stringValue);
                        RefreshUISounds(false);
                    }
                }
                QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonPlay), 18, 18)) { DUIUtils.PreviewSound(onPointerDownSound.stringValue); }
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonStop), 18, 18)) { DUIUtils.StopSoundPreview(onPointerDownSound.stringValue); }
            }
            QUI.EndHorizontal();
        }
        void DrawOnPointerDownPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerDownPreset.target ? DUIStyles.ButtonStyle.barPunchPreset : DUIStyles.ButtonStyle.barPunchPresetCollapsed), WIDTH_420, 18)) { showOnPointerDownPreset.target = !showOnPointerDownPreset.target; }
            if (QUI.BeginFadeGroup(showOnPointerDownPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!onPointerDownPunchNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Punch iPunch = UIAnimatorUtil.GetPunch(onPointerDownPunchPresetCategory.stringValue, onPointerDownPunchPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIButton iTarget = (UIButton)targets[i];
                                        iTarget.onPointerDownPunch = iPunch.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiButton, "Load Preset");
                                    uiButton.onPointerDownPunch = UIAnimatorUtil.GetPunch(onPointerDownPunchPresetCategory.stringValue, onPointerDownPunchPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                onPointerDownPunchNewPreset = true;
                                onPointerDownPunchNewCategoryName = false;
                                newPresetCategoryName = onPointerDownPunchPresetCategory.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + onPointerDownPunchPresetName.stringValue + "' preset from the '" + onPointerDownPunchPresetCategory.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeletePunchPreset(onPointerDownPunchPresetCategory.stringValue, onPointerDownPunchPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            if (iTarget.onPointerDownPunchPresetCategory.Equals(onPointerDownPunchPresetCategory.stringValue) ||
                                                iTarget.onPointerDownPunchPresetName.Equals(onPointerDownPunchPresetName.stringValue))
                                            {
                                                iTarget.onPointerDownPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.onPointerDownPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    onPointerDownPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    onPointerDownPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
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
                                onPointerDownPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onPointerDownPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                onPointerDownPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onPointerDownPunchPresetCategoryNameIndex];
                                onPointerDownPunchPresetNameIndex = 0;
                                onPointerDownPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onPointerDownPunchPresetCategory.stringValue)[onPointerDownPunchPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                onPointerDownPunchPresetNameIndex = EditorGUILayout.Popup(onPointerDownPunchPresetNameIndex, UIAnimatorUtil.GetPunchPresetNames(onPointerDownPunchPresetCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                onPointerDownPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onPointerDownPunchPresetCategory.stringValue)[onPointerDownPunchPresetNameIndex];
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
                                if (onPointerDownPunchNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset category name cannot be an empty string.", "Ok");
                                }
                                else if (string.IsNullOrEmpty(newPresetName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset name cannot be an empty string.", "Ok");
                                }
                                else if (UIAnimatorUtil.PunchPresetExists(newPresetCategoryName, newPresetName))
                                {
                                    EditorUtility.DisplayDialog("Information", "There is another preset with the '" + newPresetName + "' preset name in the '" + newPresetCategoryName + "' preset category. Try a different preset name maybe?", "Ok");
                                }
                                else
                                {
                                    UIAnimatorUtil.CreatePunchPreset(newPresetCategoryName, newPresetName, uiButton.onPointerDownPunch.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            iTarget.onPointerDownPunchPresetCategory = newPresetCategoryName;
                                            iTarget.onPointerDownPunchPresetName = newPresetName;
                                        }
                                    }
                                    onPointerDownPunchPresetCategory.stringValue = newPresetCategoryName;
                                    onPointerDownPunchPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
                                    onPointerDownPunchNewPreset = false;
                                    onPointerDownPunchNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                onPointerDownPunchNewPreset = false;
                                onPointerDownPunchNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!onPointerDownPunchNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    onPointerDownPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onPointerDownPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    onPointerDownPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onPointerDownPunchPresetCategoryNameIndex];
                                    onPointerDownPunchPresetNameIndex = 0;
                                    newPresetCategoryName = onPointerDownPunchPresetCategory.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    onPointerDownPunchNewCategoryName = QUI.Toggle(onPointerDownPunchNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (onPointerDownPunchNewCategoryName) { newPresetCategoryName = ""; }
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
                                onPointerDownPunchNewCategoryName = QUI.Toggle(onPointerDownPunchNewCategoryName);
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
                        QUI.Toggle(loadOnPointerDownPunchPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerDownPunchMove()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerDownPunchMoveEnabled.boolValue ? DUIStyles.ButtonStyle.PunchMove : DUIStyles.ButtonStyle.PunchMoveDisabled), WIDTH_420, 18)) { onPointerDownPunchMoveEnabled.boolValue = !onPointerDownPunchMoveEnabled.boolValue; }
            showOnPointerDownPunchMove.target = onPointerDownPunchMoveEnabled.boolValue;
            DrawPunch(DUIColors.GreenLight.Color, showOnPointerDownPunchMove, onPointerDownPunchMovePunch, onPointerDownPunchMoveStartDelay, onPointerDownPunchMoveDuration, onPointerDownPunchMoveVibrato, onPointerDownPunchMoveElasticity);
        }
        void DrawOnPointerDownPunchRotate()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerDownPunchRotateEnabled.boolValue ? DUIStyles.ButtonStyle.PunchRotate : DUIStyles.ButtonStyle.PunchRotateDisabled), WIDTH_420, 18)) { onPointerDownPunchRotateEnabled.boolValue = !onPointerDownPunchRotateEnabled.boolValue; }
            showOnPointerDownPunchRotate.target = onPointerDownPunchRotateEnabled.boolValue;
            DrawPunch(DUIColors.OrangeLight.Color, showOnPointerDownPunchRotate, onPointerDownPunchRotatePunch, onPointerDownPunchRotateStartDelay, onPointerDownPunchRotateDuration, onPointerDownPunchRotateVibrato, onPointerDownPunchRotateElasticity);
        }
        void DrawOnPointerDownPunchScale()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerDownPunchScaleEnabled.boolValue ? DUIStyles.ButtonStyle.PunchScale : DUIStyles.ButtonStyle.PunchScaleDisabled), WIDTH_420, 18)) { onPointerDownPunchScaleEnabled.boolValue = !onPointerDownPunchScaleEnabled.boolValue; }
            showOnPointerDownPunchScale.target = onPointerDownPunchScaleEnabled.boolValue;
            DrawPunch(DUIColors.RedLight.Color, showOnPointerDownPunchScale, onPointerDownPunchScalePunch, onPointerDownPunchScaleStartDelay, onPointerDownPunchScaleDuration, onPointerDownPunchScaleVibrato, onPointerDownPunchScaleElasticity);
        }
        void DrawOnPointerDownEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerDownEvents.target ? DUIStyles.ButtonStyle.Events : DUIStyles.ButtonStyle.EventsCollapsed), WIDTH_420, 18)) { showOnPointerDownEvents.target = !showOnPointerDownEvents.target; }
            if (QUI.BeginFadeGroup(showOnPointerDownEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.PropertyField(OnPointerDown, new GUIContent() { text = "OnPointerDown" }, WIDTH_420 - 6);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerDownGameEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerDownGameEvents.target ? DUIStyles.ButtonStyle.GameEvents : DUIStyles.ButtonStyle.GameEventsCollapsed), WIDTH_420, 18)) { showOnPointerDownGameEvents.target = !showOnPointerDownGameEvents.target; }
            if (QUI.BeginFadeGroup(showOnPointerDownGameEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.DrawList(onPointerDownGameEvents, WIDTH_420, "No Game Events are sent OnPointerDown... Click [+] to start...");
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerDownNavigation()
        {
            if (!UIManager.IsNavigationEnabled) { QUI.Space(-SPACE_2); return; }
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerDownNavigation.target ? DUIStyles.ButtonStyle.Navigation : DUIStyles.ButtonStyle.NavigationCollapsed), WIDTH_420, 18)) { showOnPointerDownNavigation.target = !showOnPointerDownNavigation.target; }
            if (QUI.BeginFadeGroup(showOnPointerDownNavigation.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    DrawNavigationData(uiButton.onPointerDownNavigation, onPointerDownEditorNavigationData);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                QUI.ResetColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawOnPointerUp()
        {
            if (DUI.DUISettings == null || DUI.DUISettings.UIButton_Inspector_HideOnPointerUp) { useOnPointerUp.boolValue = false; return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (!useOnPointerUp.boolValue)
                {
                    QUI.DrawTexture(DUIResources.barOnPointerUpDisabled.texture, 336, 21);
                    if (showOnPointerUp.target) { showOnPointerUp.target = false; }
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(showOnPointerUp.target ? DUIStyles.ButtonStyle.OnPointerUp : DUIStyles.ButtonStyle.OnPointerUpCollapsed), 336, 21)) { showOnPointerUp.target = !showOnPointerUp.target; }
                }
                if (QUI.Button(DUIStyles.GetStyle(useOnPointerUp.boolValue ? DUIStyles.ButtonStyle.BarButtonEnabled : DUIStyles.ButtonStyle.BarButtonDisabled), 84, 21)) { useOnPointerUp.boolValue = !useOnPointerUp.boolValue; if (useOnPointerUp.boolValue) { showOnPointerUp.target = true; } }
            }
            QUI.EndHorizontal();
            infoMessage["OnPointerUpLoadPresetAtRuntime"].show.target = loadOnPointerUpPunchPresetAtRuntime.boolValue;
            infoMessage["OnPointerUpLoadPresetAtRuntime"].message = onPointerUpPunchPresetCategory.stringValue + " / " + onPointerUpPunchPresetName.stringValue;
            DrawInfoMessage("OnPointerUpLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (!useOnPointerUp.boolValue) { return; }
            if (QUI.BeginFadeGroup(showOnPointerUp.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    DrawOnPointerUpSound();
                    QUI.Space(SPACE_2);
                    DrawOnPointerUpPreset();
                    QUI.Space(SPACE_2);
                    DrawOnPointerUpPunchMove();
                    QUI.Space(SPACE_2);
                    DrawOnPointerUpPunchRotate();
                    QUI.Space(SPACE_2);
                    DrawOnPointerUpPunchScale();
                    QUI.Space(SPACE_4);
                    DrawOnPointerUpEvents();
                    QUI.Space(SPACE_2);
                    DrawOnPointerUpGameEvents();
                    QUI.Space(SPACE_2);
                    DrawOnPointerUpNavigation();
                    QUI.Space(SPACE_16);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerUpSound()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("play sound", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 65);
                if (customOnPointerUpSound.boolValue)
                {
                    QUI.PropertyField(onPointerUpSound, 230);
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        onPointerUpSoundIndex = EditorGUILayout.Popup(onPointerUpSoundIndex, DUI.UISoundNamesUIButtons.ToArray(), GUILayout.Width(230));
                    }
                    if (QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onPointerUpSound.stringValue = DUI.UISoundNamesUIButtons[onPointerUpSoundIndex];
                    }
                }
                QUI.Space(SPACE_4);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(customOnPointerUpSound, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (!customOnPointerUpSound.boolValue)
                    {
                        if (!DUI.UISoundNamesUIButtons.Contains(onPointerUpSound.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "The '" + onPointerUpSound.stringValue + "' ui sound does not exist in the UISounds database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.CreateUISound(onPointerUpSound.stringValue, SoundType.UIButtons, null);
                            }
                            else
                            {
                                onPointerUpSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                            }
                        }
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onPointerUpSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onPointerUpSound.stringValue);
                        RefreshUISounds(false);
                    }
                }
                QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonPlay), 18, 18)) { DUIUtils.PreviewSound(onPointerUpSound.stringValue); }
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonStop), 18, 18)) { DUIUtils.StopSoundPreview(onPointerUpSound.stringValue); }
            }
            QUI.EndHorizontal();
        }
        void DrawOnPointerUpPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerUpPreset.target ? DUIStyles.ButtonStyle.barPunchPreset : DUIStyles.ButtonStyle.barPunchPresetCollapsed), WIDTH_420, 18)) { showOnPointerUpPreset.target = !showOnPointerUpPreset.target; }
            if (QUI.BeginFadeGroup(showOnPointerUpPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!onPointerUpPunchNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Punch iPunch = UIAnimatorUtil.GetPunch(onPointerUpPunchPresetCategory.stringValue, onPointerUpPunchPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIButton iTarget = (UIButton)targets[i];
                                        iTarget.onPointerUpPunch = iPunch.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiButton, "Load Preset");
                                    uiButton.onPointerUpPunch = UIAnimatorUtil.GetPunch(onPointerUpPunchPresetCategory.stringValue, onPointerUpPunchPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                onPointerUpPunchNewPreset = true;
                                onPointerUpPunchNewCategoryName = false;
                                newPresetCategoryName = onPointerUpPunchPresetCategory.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + onPointerUpPunchPresetName.stringValue + "' preset from the '" + onPointerUpPunchPresetCategory.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeletePunchPreset(onPointerUpPunchPresetCategory.stringValue, onPointerUpPunchPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            if (iTarget.onPointerUpPunchPresetCategory.Equals(onPointerUpPunchPresetCategory.stringValue) ||
                                                iTarget.onPointerUpPunchPresetName.Equals(onPointerUpPunchPresetName.stringValue))
                                            {
                                                iTarget.onPointerUpPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.onPointerUpPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    onPointerUpPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    onPointerUpPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
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
                                onPointerUpPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onPointerUpPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                onPointerUpPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onPointerUpPunchPresetCategoryNameIndex];
                                onPointerUpPunchPresetNameIndex = 0;
                                onPointerUpPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onPointerUpPunchPresetCategory.stringValue)[onPointerUpPunchPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                onPointerUpPunchPresetNameIndex = EditorGUILayout.Popup(onPointerUpPunchPresetNameIndex, UIAnimatorUtil.GetPunchPresetNames(onPointerUpPunchPresetCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                onPointerUpPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onPointerUpPunchPresetCategory.stringValue)[onPointerUpPunchPresetNameIndex];
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
                                if (onPointerUpPunchNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset category name cannot be an empty string.", "Ok");
                                }
                                else if (string.IsNullOrEmpty(newPresetName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset name cannot be an empty string.", "Ok");
                                }
                                else if (UIAnimatorUtil.PunchPresetExists(newPresetCategoryName, newPresetName))
                                {
                                    EditorUtility.DisplayDialog("Information", "There is another preset with the '" + newPresetName + "' preset name in the '" + newPresetCategoryName + "' preset category. Try a different preset name maybe?", "Ok");
                                }
                                else
                                {
                                    UIAnimatorUtil.CreatePunchPreset(newPresetCategoryName, newPresetName, uiButton.onPointerUpPunch.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            iTarget.onPointerUpPunchPresetCategory = newPresetCategoryName;
                                            iTarget.onPointerUpPunchPresetName = newPresetName;
                                        }
                                    }
                                    onPointerUpPunchPresetCategory.stringValue = newPresetCategoryName;
                                    onPointerUpPunchPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
                                    onPointerUpPunchNewPreset = false;
                                    onPointerUpPunchNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                onPointerUpPunchNewPreset = false;
                                onPointerUpPunchNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!onPointerUpPunchNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    onPointerUpPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onPointerUpPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    onPointerUpPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onPointerUpPunchPresetCategoryNameIndex];
                                    onPointerUpPunchPresetNameIndex = 0;
                                    newPresetCategoryName = onPointerUpPunchPresetCategory.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    onPointerUpPunchNewCategoryName = QUI.Toggle(onPointerUpPunchNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (onPointerUpPunchNewCategoryName) { newPresetCategoryName = ""; }
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
                                onPointerUpPunchNewCategoryName = QUI.Toggle(onPointerUpPunchNewCategoryName);
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
                        QUI.Toggle(loadOnPointerUpPunchPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerUpPunchMove()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerUpPunchMoveEnabled.boolValue ? DUIStyles.ButtonStyle.PunchMove : DUIStyles.ButtonStyle.PunchMoveDisabled), WIDTH_420, 18)) { onPointerUpPunchMoveEnabled.boolValue = !onPointerUpPunchMoveEnabled.boolValue; }
            showOnPointerUpPunchMove.target = onPointerUpPunchMoveEnabled.boolValue;
            DrawPunch(DUIColors.GreenLight.Color, showOnPointerUpPunchMove, onPointerUpPunchMovePunch, onPointerUpPunchMoveStartDelay, onPointerUpPunchMoveDuration, onPointerUpPunchMoveVibrato, onPointerUpPunchMoveElasticity);
        }
        void DrawOnPointerUpPunchRotate()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerUpPunchRotateEnabled.boolValue ? DUIStyles.ButtonStyle.PunchRotate : DUIStyles.ButtonStyle.PunchRotateDisabled), WIDTH_420, 18)) { onPointerUpPunchRotateEnabled.boolValue = !onPointerUpPunchRotateEnabled.boolValue; }
            showOnPointerUpPunchRotate.target = onPointerUpPunchRotateEnabled.boolValue;
            DrawPunch(DUIColors.OrangeLight.Color, showOnPointerUpPunchRotate, onPointerUpPunchRotatePunch, onPointerUpPunchRotateStartDelay, onPointerUpPunchRotateDuration, onPointerUpPunchRotateVibrato, onPointerUpPunchRotateElasticity);
        }
        void DrawOnPointerUpPunchScale()
        {
            if (QUI.Button(DUIStyles.GetStyle(onPointerUpPunchScaleEnabled.boolValue ? DUIStyles.ButtonStyle.PunchScale : DUIStyles.ButtonStyle.PunchScaleDisabled), WIDTH_420, 18)) { onPointerUpPunchScaleEnabled.boolValue = !onPointerUpPunchScaleEnabled.boolValue; }
            showOnPointerUpPunchScale.target = onPointerUpPunchScaleEnabled.boolValue;
            DrawPunch(DUIColors.RedLight.Color, showOnPointerUpPunchScale, onPointerUpPunchScalePunch, onPointerUpPunchScaleStartDelay, onPointerUpPunchScaleDuration, onPointerUpPunchScaleVibrato, onPointerUpPunchScaleElasticity);
        }
        void DrawOnPointerUpEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerUpEvents.target ? DUIStyles.ButtonStyle.Events : DUIStyles.ButtonStyle.EventsCollapsed), WIDTH_420, 18)) { showOnPointerUpEvents.target = !showOnPointerUpEvents.target; }
            if (QUI.BeginFadeGroup(showOnPointerUpEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.PropertyField(OnPointerUp, new GUIContent() { text = "OnPointerUp" }, WIDTH_420 - 6);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerUpGameEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerUpGameEvents.target ? DUIStyles.ButtonStyle.GameEvents : DUIStyles.ButtonStyle.GameEventsCollapsed), WIDTH_420, 18)) { showOnPointerUpGameEvents.target = !showOnPointerUpGameEvents.target; }
            if (QUI.BeginFadeGroup(showOnPointerUpGameEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.DrawList(onPointerUpGameEvents, WIDTH_420, "No Game Events are sent OnPointerUp... Click [+] to start...");
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnPointerUpNavigation()
        {
            if (!UIManager.IsNavigationEnabled) { QUI.Space(-SPACE_2); return; }
            if (QUI.Button(DUIStyles.GetStyle(showOnPointerUpNavigation.target ? DUIStyles.ButtonStyle.Navigation : DUIStyles.ButtonStyle.NavigationCollapsed), WIDTH_420, 18)) { showOnPointerUpNavigation.target = !showOnPointerUpNavigation.target; }
            if (QUI.BeginFadeGroup(showOnPointerUpNavigation.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    DrawNavigationData(uiButton.onPointerUpNavigation, onPointerUpEditorNavigationData);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                QUI.ResetColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawOnClick()
        {
            if (DUI.DUISettings == null || DUI.DUISettings.UIButton_Inspector_HideOnClick) { useOnClickAnimations.boolValue = false; return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (!useOnClickAnimations.boolValue)
                {
                    QUI.DrawTexture(DUIResources.barOnClickDisabled.texture, 336, 21);
                    if (showOnClick.target) { showOnClick.target = false; }
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(showOnClick.target ? DUIStyles.ButtonStyle.OnClick : DUIStyles.ButtonStyle.OnClickCollapsed), 336, 21)) { showOnClick.target = !showOnClick.target; }
                }
                if (QUI.Button(DUIStyles.GetStyle(useOnClickAnimations.boolValue ? DUIStyles.ButtonStyle.BarButtonEnabled : DUIStyles.ButtonStyle.BarButtonDisabled), 84, 21)) { useOnClickAnimations.boolValue = !useOnClickAnimations.boolValue; if (useOnClickAnimations.boolValue) { showOnClick.target = true; } }
            }
            QUI.EndHorizontal();
            infoMessage["OnClickLoadPresetAtRuntime"].show.target = loadOnClickPunchPresetAtRuntime.boolValue;
            infoMessage["OnClickLoadPresetAtRuntime"].message = onClickPunchPresetCategory.stringValue + " / " + onClickPunchPresetName.stringValue;
            DrawInfoMessage("OnClickLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (!useOnClickAnimations.boolValue) { return; }
            if (QUI.BeginFadeGroup(showOnClick.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    DrawOnClickSettings();
                    QUI.Space(SPACE_2);
                    DrawOnClickSound();
                    QUI.Space(SPACE_4);
                    DrawOnClickPreset();
                    QUI.Space(SPACE_2);
                    DrawOnClickPunchMove();
                    QUI.Space(SPACE_2);
                    DrawOnClickPunchRotate();
                    QUI.Space(SPACE_2);
                    DrawOnClickPunchScale();
                    QUI.Space(SPACE_4);
                    DrawOnClickEvents();
                    QUI.Space(SPACE_2);
                    DrawOnClickGameEvents();
                    QUI.Space(SPACE_2);
                    DrawOnClickNavigation();
                    QUI.Space(SPACE_16);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnClickSound()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("play sound", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 65);
                if (customOnClickSound.boolValue)
                {
                    QUI.PropertyField(onClickSound, 230);
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        onClickSoundIndex = EditorGUILayout.Popup(onClickSoundIndex, DUI.UISoundNamesUIButtons.ToArray(), GUILayout.Width(230));
                    }
                    if (QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onClickSound.stringValue = DUI.UISoundNamesUIButtons[onClickSoundIndex];
                    }
                }
                QUI.Space(SPACE_4);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(customOnClickSound, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (!customOnClickSound.boolValue)
                    {
                        if (!DUI.UISoundNamesUIButtons.Contains(onClickSound.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "The '" + onClickSound.stringValue + "' ui sound does not exist in the UISounds database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.CreateUISound(onClickSound.stringValue, SoundType.UIButtons, null);
                            }
                            else
                            {
                                onClickSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                            }
                        }
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onClickSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onClickSound.stringValue);
                        RefreshUISounds(false);
                    }
                }
                QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonPlay), 18, 18)) { DUIUtils.PreviewSound(onClickSound.stringValue); }
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonStop), 18, 18)) { DUIUtils.StopSoundPreview(onClickSound.stringValue); }
            }
            QUI.EndHorizontal();
        }
        void DrawOnClickSettings()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.PropertyField(waitForOnClickAnimation, 12);
                QUI.Label("wait for animation", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 120);
                QUI.Space(92);
                QUI.Label("single click mode", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 100);
                QUI.PropertyField(singleClickMode, 80);
            }
            QUI.EndHorizontal();
        }
        void DrawOnClickPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnClickPreset.target ? DUIStyles.ButtonStyle.barPunchPreset : DUIStyles.ButtonStyle.barPunchPresetCollapsed), WIDTH_420, 18)) { showOnClickPreset.target = !showOnClickPreset.target; }
            if (QUI.BeginFadeGroup(showOnClickPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!onClickPunchNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Punch iPunch = UIAnimatorUtil.GetPunch(onClickPunchPresetCategory.stringValue, onClickPunchPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIButton iTarget = (UIButton)targets[i];
                                        iTarget.onClickPunch = iPunch.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiButton, "Load Preset");
                                    uiButton.onClickPunch = UIAnimatorUtil.GetPunch(onClickPunchPresetCategory.stringValue, onClickPunchPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                onClickPunchNewPreset = true;
                                onClickPunchNewCategoryName = false;
                                newPresetCategoryName = onClickPunchPresetCategory.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + onClickPunchPresetName.stringValue + "' preset from the '" + onClickPunchPresetCategory.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeletePunchPreset(onClickPunchPresetCategory.stringValue, onClickPunchPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            if (iTarget.onClickPunchPresetCategory.Equals(onClickPunchPresetCategory.stringValue) ||
                                                iTarget.onClickPunchPresetName.Equals(onClickPunchPresetName.stringValue))
                                            {
                                                iTarget.onClickPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.onClickPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    onClickPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    onClickPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
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
                                onClickPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onClickPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                onClickPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onClickPunchPresetCategoryNameIndex];
                                onClickPunchPresetNameIndex = 0;
                                onClickPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onClickPunchPresetCategory.stringValue)[onClickPunchPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                onClickPunchPresetNameIndex = EditorGUILayout.Popup(onClickPunchPresetNameIndex, UIAnimatorUtil.GetPunchPresetNames(onClickPunchPresetCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                onClickPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onClickPunchPresetCategory.stringValue)[onClickPunchPresetNameIndex];
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
                                if (onClickPunchNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset category name cannot be an empty string.", "Ok");
                                }
                                else if (string.IsNullOrEmpty(newPresetName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset name cannot be an empty string.", "Ok");
                                }
                                else if (UIAnimatorUtil.PunchPresetExists(newPresetCategoryName, newPresetName))
                                {
                                    EditorUtility.DisplayDialog("Information", "There is another preset with the '" + newPresetName + "' preset name in the '" + newPresetCategoryName + "' preset category. Try a different preset name maybe?", "Ok");
                                }
                                else
                                {
                                    UIAnimatorUtil.CreatePunchPreset(newPresetCategoryName, newPresetName, uiButton.onClickPunch.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            iTarget.onClickPunchPresetCategory = newPresetCategoryName;
                                            iTarget.onClickPunchPresetName = newPresetName;
                                        }
                                    }
                                    onClickPunchPresetCategory.stringValue = newPresetCategoryName;
                                    onClickPunchPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
                                    onClickPunchNewPreset = false;
                                    onClickPunchNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                onClickPunchNewPreset = false;
                                onClickPunchNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!onClickPunchNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    onClickPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onClickPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    onClickPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onClickPunchPresetCategoryNameIndex];
                                    onClickPunchPresetNameIndex = 0;
                                    newPresetCategoryName = onClickPunchPresetCategory.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    onClickPunchNewCategoryName = QUI.Toggle(onClickPunchNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (onClickPunchNewCategoryName) { newPresetCategoryName = ""; }
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
                                onClickPunchNewCategoryName = QUI.Toggle(onClickPunchNewCategoryName);
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
                        QUI.Toggle(loadOnClickPunchPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnClickPunchMove()
        {
            if (QUI.Button(DUIStyles.GetStyle(onClickPunchMoveEnabled.boolValue ? DUIStyles.ButtonStyle.PunchMove : DUIStyles.ButtonStyle.PunchMoveDisabled), WIDTH_420, 18)) { onClickPunchMoveEnabled.boolValue = !onClickPunchMoveEnabled.boolValue; }
            showOnClickPunchMove.target = onClickPunchMoveEnabled.boolValue;
            DrawPunch(DUIColors.GreenLight.Color, showOnClickPunchMove, onClickPunchMovePunch, onClickPunchMoveStartDelay, onClickPunchMoveDuration, onClickPunchMoveVibrato, onClickPunchMoveElasticity);
        }
        void DrawOnClickPunchRotate()
        {
            if (QUI.Button(DUIStyles.GetStyle(onClickPunchRotateEnabled.boolValue ? DUIStyles.ButtonStyle.PunchRotate : DUIStyles.ButtonStyle.PunchRotateDisabled), WIDTH_420, 18)) { onClickPunchRotateEnabled.boolValue = !onClickPunchRotateEnabled.boolValue; }
            showOnClickPunchRotate.target = onClickPunchRotateEnabled.boolValue;
            DrawPunch(DUIColors.OrangeLight.Color, showOnClickPunchRotate, onClickPunchRotatePunch, onClickPunchRotateStartDelay, onClickPunchRotateDuration, onClickPunchRotateVibrato, onClickPunchRotateElasticity);
        }
        void DrawOnClickPunchScale()
        {
            if (QUI.Button(DUIStyles.GetStyle(onClickPunchScaleEnabled.boolValue ? DUIStyles.ButtonStyle.PunchScale : DUIStyles.ButtonStyle.PunchScaleDisabled), WIDTH_420, 18)) { onClickPunchScaleEnabled.boolValue = !onClickPunchScaleEnabled.boolValue; }
            showOnClickPunchScale.target = onClickPunchScaleEnabled.boolValue;
            DrawPunch(DUIColors.RedLight.Color, showOnClickPunchScale, onClickPunchScalePunch, onClickPunchScaleStartDelay, onClickPunchScaleDuration, onClickPunchScaleVibrato, onClickPunchScaleElasticity);
        }
        void DrawOnClickEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnClickEvents.target ? DUIStyles.ButtonStyle.Events : DUIStyles.ButtonStyle.EventsCollapsed), WIDTH_420, 18)) { showOnClickEvents.target = !showOnClickEvents.target; }
            if (QUI.BeginFadeGroup(showOnClickEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.PropertyField(OnClick, new GUIContent() { text = "OnClick" }, WIDTH_420 - 6);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnClickGameEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnClickGameEvents.target ? DUIStyles.ButtonStyle.GameEvents : DUIStyles.ButtonStyle.GameEventsCollapsed), WIDTH_420, 18)) { showOnClickGameEvents.target = !showOnClickGameEvents.target; }
            if (QUI.BeginFadeGroup(showOnClickGameEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.DrawList(onClickGameEvents, WIDTH_420, "No Game Events are sent OnClick... Click [+] to start...");
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnClickNavigation()
        {
            if (!UIManager.IsNavigationEnabled) { QUI.Space(-SPACE_2); return; }
            if (QUI.Button(DUIStyles.GetStyle(showOnClickNavigation.target ? DUIStyles.ButtonStyle.Navigation : DUIStyles.ButtonStyle.NavigationCollapsed), WIDTH_420, 18)) { showOnClickNavigation.target = !showOnClickNavigation.target; }
            if (QUI.BeginFadeGroup(showOnClickNavigation.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    DrawNavigationData(uiButton.onClickNavigation, onClickEditorNavigationData);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                QUI.ResetColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawOnDoubleClick()
        {
            if (DUI.DUISettings == null || DUI.DUISettings.UIButton_Inspector_HideOnDoubleClick) { useOnDoubleClick.boolValue = false; return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (!useOnDoubleClick.boolValue)
                {
                    QUI.DrawTexture(DUIResources.barOnDoubleClickDisabled.texture, 336, 21);
                    if (showOnDoubleClick.target) { showOnDoubleClick.target = false; }
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(showOnDoubleClick.target ? DUIStyles.ButtonStyle.OnDoubleClick : DUIStyles.ButtonStyle.OnDoubleClickCollapsed), 336, 21)) { showOnDoubleClick.target = !showOnDoubleClick.target; }
                    useOnClickAnimations.boolValue = true;
                }
                if (QUI.Button(DUIStyles.GetStyle(useOnDoubleClick.boolValue ? DUIStyles.ButtonStyle.BarButtonEnabled : DUIStyles.ButtonStyle.BarButtonDisabled), 84, 21)) { useOnDoubleClick.boolValue = !useOnDoubleClick.boolValue; if (useOnDoubleClick.boolValue) { showOnDoubleClick.target = true; } }
            }
            QUI.EndHorizontal();
            infoMessage["OnDoubleClickLoadPresetAtRuntime"].show.target = loadOnDoubleClickPunchPresetAtRuntime.boolValue;
            infoMessage["OnDoubleClickLoadPresetAtRuntime"].message = onDoubleClickPunchPresetCategory.stringValue + " / " + onDoubleClickPunchPresetName.stringValue;
            DrawInfoMessage("OnDoubleClickLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (!useOnDoubleClick.boolValue) { return; }
            if (QUI.BeginFadeGroup(showOnDoubleClick.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {

                    DrawOnDoubleClickSettings();
                    QUI.Space(SPACE_2);
                    DrawOnDoubleClickSound();
                    QUI.Space(SPACE_2);
                    DrawOnDoubleClickPreset();
                    QUI.Space(SPACE_2);
                    DrawOnDoubleClickPunchMove();
                    QUI.Space(SPACE_2);
                    DrawOnDoubleClickPunchRotate();
                    QUI.Space(SPACE_2);
                    DrawOnDoubleClickPunchScale();
                    QUI.Space(SPACE_4);
                    DrawOnDoubleClickEvents();
                    QUI.Space(SPACE_2);
                    DrawOnDoubleClickGameEvents();
                    QUI.Space(SPACE_2);
                    DrawOnDoubleClickNavigation();
                    QUI.Space(SPACE_16);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnDoubleClickSettings()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.PropertyField(waitForOnDoubleClickAnimation, 12);
                QUI.Label("wait for animation", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 120);
                QUI.Space(122);
                QUI.Label("register interval", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 90);
                QUI.PropertyField(doubleClickRegisterInterval, 60);
            }
            QUI.EndHorizontal();
        }
        void DrawOnDoubleClickSound()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("play sound", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 65);
                if (customOnDoubleClickSound.boolValue)
                {
                    QUI.PropertyField(onDoubleClickSound, 230);
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        onDoubleClickSoundIndex = EditorGUILayout.Popup(onDoubleClickSoundIndex, DUI.UISoundNamesUIButtons.ToArray(), GUILayout.Width(230));
                    }
                    if (QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onDoubleClickSound.stringValue = DUI.UISoundNamesUIButtons[onDoubleClickSoundIndex];
                    }
                }
                QUI.Space(SPACE_4);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(customOnDoubleClickSound, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (!customOnDoubleClickSound.boolValue)
                    {
                        if (!DUI.UISoundNamesUIButtons.Contains(onDoubleClickSound.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "The '" + onDoubleClickSound.stringValue + "' ui sound does not exist in the UISounds database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.CreateUISound(onDoubleClickSound.stringValue, SoundType.UIButtons, null);
                            }
                            else
                            {
                                onDoubleClickSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                            }
                        }
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onDoubleClickSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onDoubleClickSound.stringValue);
                        RefreshUISounds(false);
                    }
                }
                QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonPlay), 18, 18)) { DUIUtils.PreviewSound(onDoubleClickSound.stringValue); }
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonStop), 18, 18)) { DUIUtils.StopSoundPreview(onDoubleClickSound.stringValue); }
            }
            QUI.EndHorizontal();
        }
        void DrawOnDoubleClickPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnDoubleClickPreset.target ? DUIStyles.ButtonStyle.barPunchPreset : DUIStyles.ButtonStyle.barPunchPresetCollapsed), WIDTH_420, 18)) { showOnDoubleClickPreset.target = !showOnDoubleClickPreset.target; }
            if (QUI.BeginFadeGroup(showOnDoubleClickPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!onDoubleClickPunchNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Punch iPunch = UIAnimatorUtil.GetPunch(onDoubleClickPunchPresetCategory.stringValue, onDoubleClickPunchPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIButton iTarget = (UIButton)targets[i];
                                        iTarget.onDoubleClickPunch = iPunch.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiButton, "Load Preset");
                                    uiButton.onDoubleClickPunch = UIAnimatorUtil.GetPunch(onDoubleClickPunchPresetCategory.stringValue, onDoubleClickPunchPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                onDoubleClickPunchNewPreset = true;
                                onDoubleClickPunchNewCategoryName = false;
                                newPresetCategoryName = onDoubleClickPunchPresetCategory.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + onDoubleClickPunchPresetName.stringValue + "' preset from the '" + onDoubleClickPunchPresetCategory.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeletePunchPreset(onDoubleClickPunchPresetCategory.stringValue, onDoubleClickPunchPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            if (iTarget.onDoubleClickPunchPresetCategory.Equals(onDoubleClickPunchPresetCategory.stringValue) ||
                                                iTarget.onDoubleClickPunchPresetName.Equals(onDoubleClickPunchPresetName.stringValue))
                                            {
                                                iTarget.onDoubleClickPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.onDoubleClickPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    onDoubleClickPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    onDoubleClickPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
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
                                onDoubleClickPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onDoubleClickPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                onDoubleClickPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onDoubleClickPunchPresetCategoryNameIndex];
                                onDoubleClickPunchPresetNameIndex = 0;
                                onDoubleClickPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onDoubleClickPunchPresetCategory.stringValue)[onDoubleClickPunchPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                onDoubleClickPunchPresetNameIndex = EditorGUILayout.Popup(onDoubleClickPunchPresetNameIndex, UIAnimatorUtil.GetPunchPresetNames(onDoubleClickPunchPresetCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                onDoubleClickPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onDoubleClickPunchPresetCategory.stringValue)[onDoubleClickPunchPresetNameIndex];
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
                                if (onDoubleClickPunchNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset category name cannot be an empty string.", "Ok");
                                }
                                else if (string.IsNullOrEmpty(newPresetName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset name cannot be an empty string.", "Ok");
                                }
                                else if (UIAnimatorUtil.PunchPresetExists(newPresetCategoryName, newPresetName))
                                {
                                    EditorUtility.DisplayDialog("Information", "There is another preset with the '" + newPresetName + "' preset name in the '" + newPresetCategoryName + "' preset category. Try a different preset name maybe?", "Ok");
                                }
                                else
                                {
                                    UIAnimatorUtil.CreatePunchPreset(newPresetCategoryName, newPresetName, uiButton.onDoubleClickPunch.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            iTarget.onDoubleClickPunchPresetCategory = newPresetCategoryName;
                                            iTarget.onDoubleClickPunchPresetName = newPresetName;
                                        }
                                    }
                                    onDoubleClickPunchPresetCategory.stringValue = newPresetCategoryName;
                                    onDoubleClickPunchPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
                                    onDoubleClickPunchNewPreset = false;
                                    onDoubleClickPunchNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                onDoubleClickPunchNewPreset = false;
                                onDoubleClickPunchNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!onDoubleClickPunchNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    onDoubleClickPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onDoubleClickPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    onDoubleClickPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onDoubleClickPunchPresetCategoryNameIndex];
                                    onDoubleClickPunchPresetNameIndex = 0;
                                    newPresetCategoryName = onDoubleClickPunchPresetCategory.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    onDoubleClickPunchNewCategoryName = QUI.Toggle(onDoubleClickPunchNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (onDoubleClickPunchNewCategoryName) { newPresetCategoryName = ""; }
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
                                onDoubleClickPunchNewCategoryName = QUI.Toggle(onDoubleClickPunchNewCategoryName);
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
                        QUI.Toggle(loadOnDoubleClickPunchPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnDoubleClickPunchMove()
        {
            if (QUI.Button(DUIStyles.GetStyle(onDoubleClickPunchMoveEnabled.boolValue ? DUIStyles.ButtonStyle.PunchMove : DUIStyles.ButtonStyle.PunchMoveDisabled), WIDTH_420, 18)) { onDoubleClickPunchMoveEnabled.boolValue = !onDoubleClickPunchMoveEnabled.boolValue; }
            showOnDoubleClickPunchMove.target = onDoubleClickPunchMoveEnabled.boolValue;
            DrawPunch(DUIColors.GreenLight.Color, showOnDoubleClickPunchMove, onDoubleClickPunchMovePunch, onDoubleClickPunchMoveStartDelay, onDoubleClickPunchMoveDuration, onDoubleClickPunchMoveVibrato, onDoubleClickPunchMoveElasticity);
        }
        void DrawOnDoubleClickPunchRotate()
        {
            if (QUI.Button(DUIStyles.GetStyle(onDoubleClickPunchRotateEnabled.boolValue ? DUIStyles.ButtonStyle.PunchRotate : DUIStyles.ButtonStyle.PunchRotateDisabled), WIDTH_420, 18)) { onDoubleClickPunchRotateEnabled.boolValue = !onDoubleClickPunchRotateEnabled.boolValue; }
            showOnDoubleClickPunchRotate.target = onDoubleClickPunchRotateEnabled.boolValue;
            DrawPunch(DUIColors.OrangeLight.Color, showOnDoubleClickPunchRotate, onDoubleClickPunchRotatePunch, onDoubleClickPunchRotateStartDelay, onDoubleClickPunchRotateDuration, onDoubleClickPunchRotateVibrato, onDoubleClickPunchRotateElasticity);
        }
        void DrawOnDoubleClickPunchScale()
        {
            if (QUI.Button(DUIStyles.GetStyle(onDoubleClickPunchScaleEnabled.boolValue ? DUIStyles.ButtonStyle.PunchScale : DUIStyles.ButtonStyle.PunchScaleDisabled), WIDTH_420, 18)) { onDoubleClickPunchScaleEnabled.boolValue = !onDoubleClickPunchScaleEnabled.boolValue; }
            showOnDoubleClickPunchScale.target = onDoubleClickPunchScaleEnabled.boolValue;
            DrawPunch(DUIColors.RedLight.Color, showOnDoubleClickPunchScale, onDoubleClickPunchScalePunch, onDoubleClickPunchScaleStartDelay, onDoubleClickPunchScaleDuration, onDoubleClickPunchScaleVibrato, onDoubleClickPunchScaleElasticity);
        }
        void DrawOnDoubleClickEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnDoubleClickEvents.target ? DUIStyles.ButtonStyle.Events : DUIStyles.ButtonStyle.EventsCollapsed), WIDTH_420, 18)) { showOnDoubleClickEvents.target = !showOnDoubleClickEvents.target; }
            if (QUI.BeginFadeGroup(showOnDoubleClickEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.PropertyField(OnDoubleClick, new GUIContent() { text = "OnDoubleClick" }, WIDTH_420 - 6);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnDoubleClickGameEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnDoubleClickGameEvents.target ? DUIStyles.ButtonStyle.GameEvents : DUIStyles.ButtonStyle.GameEventsCollapsed), WIDTH_420, 18)) { showOnDoubleClickGameEvents.target = !showOnDoubleClickGameEvents.target; }
            if (QUI.BeginFadeGroup(showOnDoubleClickGameEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.DrawList(onDoubleClickGameEvents, WIDTH_420, "No Game Events are sent OnDoubleClick... Click [+] to start...");
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnDoubleClickNavigation()
        {
            if (!UIManager.IsNavigationEnabled) { QUI.Space(-SPACE_2); return; }
            if (QUI.Button(DUIStyles.GetStyle(showOnDoubleClickNavigation.target ? DUIStyles.ButtonStyle.Navigation : DUIStyles.ButtonStyle.NavigationCollapsed), WIDTH_420, 18)) { showOnDoubleClickNavigation.target = !showOnDoubleClickNavigation.target; }
            if (QUI.BeginFadeGroup(showOnDoubleClickNavigation.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    DrawNavigationData(uiButton.onDoubleClickNavigation, onDoubleClickEditorNavigationData);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                QUI.ResetColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawOnLongClick()
        {
            if (DUI.DUISettings == null || DUI.DUISettings.UIButton_Inspector_HideOnLongClick) { useOnLongClick.boolValue = false; return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (!useOnLongClick.boolValue)
                {
                    QUI.DrawTexture(DUIResources.barOnLongClickDisabled.texture, 336, 21);
                    if (showOnLongClick.target) { showOnLongClick.target = false; }
                }
                else
                {
                    if (QUI.Button(DUIStyles.GetStyle(showOnLongClick.target ? DUIStyles.ButtonStyle.OnLongClick : DUIStyles.ButtonStyle.OnLongClickCollapsed), 336, 21)) { showOnLongClick.target = !showOnLongClick.target; }
                    useOnClickAnimations.boolValue = true;
                }
                if (QUI.Button(DUIStyles.GetStyle(useOnLongClick.boolValue ? DUIStyles.ButtonStyle.BarButtonEnabled : DUIStyles.ButtonStyle.BarButtonDisabled), 84, 21)) { useOnLongClick.boolValue = !useOnLongClick.boolValue; if (useOnLongClick.boolValue) { showOnLongClick.target = true; } }
            }
            QUI.EndHorizontal();
            infoMessage["OnLongClickLoadPresetAtRuntime"].show.target = loadOnLongClickPunchPresetAtRuntime.boolValue;
            infoMessage["OnLongClickLoadPresetAtRuntime"].message = onLongClickPunchPresetCategory.stringValue + " / " + onLongClickPunchPresetName.stringValue;
            DrawInfoMessage("OnLongClickLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (!useOnLongClick.boolValue) { return; }
            if (QUI.BeginFadeGroup(showOnLongClick.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {

                    DrawOnLongClickSettings();
                    QUI.Space(SPACE_2);
                    DrawOnLongClickSound();
                    QUI.Space(SPACE_2);
                    DrawOnLongClickPreset();
                    QUI.Space(SPACE_2);
                    DrawOnLongClickPunchMove();
                    QUI.Space(SPACE_2);
                    DrawOnLongClickPunchRotate();
                    QUI.Space(SPACE_2);
                    DrawOnLongClickPunchScale();
                    QUI.Space(SPACE_4);
                    DrawOnLongClickEvents();
                    QUI.Space(SPACE_2);
                    DrawOnLongClickGameEvents();
                    QUI.Space(SPACE_2);
                    DrawOnLongClickNavigation();
                    QUI.Space(SPACE_16);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnLongClickSettings()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.PropertyField(waitForOnLongClickAnimation, 12);
                QUI.Label("wait for animation", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 120);
                QUI.Space(122);
                QUI.Label("register interval", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 90);
                QUI.PropertyField(longClickRegisterInterval, 60);
            }
            QUI.EndHorizontal();
        }
        void DrawOnLongClickSound()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("play sound", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 65);
                if (customOnLongClickSound.boolValue)
                {
                    QUI.PropertyField(onLongClickSound, 230);
                }
                else
                {
                    QUI.BeginChangeCheck();
                    {
                        onLongClickSoundIndex = EditorGUILayout.Popup(onLongClickSoundIndex, DUI.UISoundNamesUIButtons.ToArray(), GUILayout.Width(230));
                    }
                    if (QUI.EndChangeCheck())
                    {
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onLongClickSound.stringValue = DUI.UISoundNamesUIButtons[onLongClickSoundIndex];
                    }
                }
                QUI.Space(SPACE_4);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(customOnLongClickSound, 12);
                }
                if (QUI.EndChangeCheck())
                {
                    if (!customOnLongClickSound.boolValue)
                    {
                        if (!DUI.UISoundNamesUIButtons.Contains(onLongClickSound.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "The '" + onLongClickSound.stringValue + "' ui sound does not exist in the UISounds database.\nDo you want to add it now?", "Yes", "No"))
                            {
                                DUI.CreateUISound(onLongClickSound.stringValue, SoundType.UIButtons, null);
                            }
                            else
                            {
                                onLongClickSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                            }
                        }
                        Undo.RecordObject(uiButton, "Updated Play Sound");
                        onLongClickSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onLongClickSound.stringValue);
                        RefreshUISounds(false);
                    }
                }
                QUI.Label("custom", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 48);
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonPlay), 18, 18)) { DUIUtils.PreviewSound(onLongClickSound.stringValue); }
                QUI.Space(SPACE_2);
                if (QUI.Button(DUIStyles.GetStyle(DUIStyles.ButtonStyle.ButtonStop), 18, 18)) { DUIUtils.StopSoundPreview(onLongClickSound.stringValue); }
            }
            QUI.EndHorizontal();
        }
        void DrawOnLongClickPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnLongClickPreset.target ? DUIStyles.ButtonStyle.barPunchPreset : DUIStyles.ButtonStyle.barPunchPresetCollapsed), WIDTH_420, 18)) { showOnLongClickPreset.target = !showOnLongClickPreset.target; }
            if (QUI.BeginFadeGroup(showOnLongClickPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!onLongClickPunchNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Punch iPunch = UIAnimatorUtil.GetPunch(onLongClickPunchPresetCategory.stringValue, onLongClickPunchPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIButton iTarget = (UIButton)targets[i];
                                        iTarget.onLongClickPunch = iPunch.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiButton, "Load Preset");
                                    uiButton.onLongClickPunch = UIAnimatorUtil.GetPunch(onLongClickPunchPresetCategory.stringValue, onLongClickPunchPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                onLongClickPunchNewPreset = true;
                                onLongClickPunchNewCategoryName = false;
                                newPresetCategoryName = onLongClickPunchPresetCategory.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + onLongClickPunchPresetName.stringValue + "' preset from the '" + onLongClickPunchPresetCategory.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeletePunchPreset(onLongClickPunchPresetCategory.stringValue, onLongClickPunchPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            if (iTarget.onLongClickPunchPresetCategory.Equals(onLongClickPunchPresetCategory.stringValue) ||
                                                iTarget.onLongClickPunchPresetName.Equals(onLongClickPunchPresetName.stringValue))
                                            {
                                                iTarget.onLongClickPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.onLongClickPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    onLongClickPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    onLongClickPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
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
                                onLongClickPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onLongClickPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                onLongClickPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onLongClickPunchPresetCategoryNameIndex];
                                onLongClickPunchPresetNameIndex = 0;
                                onLongClickPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onLongClickPunchPresetCategory.stringValue)[onLongClickPunchPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                onLongClickPunchPresetNameIndex = EditorGUILayout.Popup(onLongClickPunchPresetNameIndex, UIAnimatorUtil.GetPunchPresetNames(onLongClickPunchPresetCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                onLongClickPunchPresetName.stringValue = UIAnimatorUtil.GetPunchPresetNames(onLongClickPunchPresetCategory.stringValue)[onLongClickPunchPresetNameIndex];
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
                                if (onLongClickPunchNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset category name cannot be an empty string.", "Ok");
                                }
                                else if (string.IsNullOrEmpty(newPresetName.Trim()))
                                {
                                    EditorUtility.DisplayDialog("Information", "The new preset name cannot be an empty string.", "Ok");
                                }
                                else if (UIAnimatorUtil.PunchPresetExists(newPresetCategoryName, newPresetName))
                                {
                                    EditorUtility.DisplayDialog("Information", "There is another preset with the '" + newPresetName + "' preset name in the '" + newPresetCategoryName + "' preset category. Try a different preset name maybe?", "Ok");
                                }
                                else
                                {
                                    UIAnimatorUtil.CreatePunchPreset(newPresetCategoryName, newPresetName, uiButton.onLongClickPunch.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            iTarget.onLongClickPunchPresetCategory = newPresetCategoryName;
                                            iTarget.onLongClickPunchPresetName = newPresetName;
                                        }
                                    }
                                    onLongClickPunchPresetCategory.stringValue = newPresetCategoryName;
                                    onLongClickPunchPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshPunchAnimations(true);
                                    onLongClickPunchNewPreset = false;
                                    onLongClickPunchNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                onLongClickPunchNewPreset = false;
                                onLongClickPunchNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!onLongClickPunchNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    onLongClickPunchPresetCategoryNameIndex = EditorGUILayout.Popup(onLongClickPunchPresetCategoryNameIndex, UIAnimatorUtil.PunchPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    onLongClickPunchPresetCategory.stringValue = UIAnimatorUtil.PunchPresetCategories[onLongClickPunchPresetCategoryNameIndex];
                                    onLongClickPunchPresetNameIndex = 0;
                                    newPresetCategoryName = onLongClickPunchPresetCategory.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    onLongClickPunchNewCategoryName = QUI.Toggle(onLongClickPunchNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (onLongClickPunchNewCategoryName) { newPresetCategoryName = ""; }
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
                                onLongClickPunchNewCategoryName = QUI.Toggle(onLongClickPunchNewCategoryName);
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
                        QUI.Toggle(loadOnLongClickPunchPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnLongClickPunchMove()
        {
            if (QUI.Button(DUIStyles.GetStyle(onLongClickPunchMoveEnabled.boolValue ? DUIStyles.ButtonStyle.PunchMove : DUIStyles.ButtonStyle.PunchMoveDisabled), WIDTH_420, 18)) { onLongClickPunchMoveEnabled.boolValue = !onLongClickPunchMoveEnabled.boolValue; }
            showOnLongClickPunchMove.target = onLongClickPunchMoveEnabled.boolValue;
            DrawPunch(DUIColors.GreenLight.Color, showOnLongClickPunchMove, onLongClickPunchMovePunch, onLongClickPunchMoveStartDelay, onLongClickPunchMoveDuration, onLongClickPunchMoveVibrato, onLongClickPunchMoveElasticity);
        }
        void DrawOnLongClickPunchRotate()
        {
            if (QUI.Button(DUIStyles.GetStyle(onLongClickPunchRotateEnabled.boolValue ? DUIStyles.ButtonStyle.PunchRotate : DUIStyles.ButtonStyle.PunchRotateDisabled), WIDTH_420, 18)) { onLongClickPunchRotateEnabled.boolValue = !onLongClickPunchRotateEnabled.boolValue; }
            showOnLongClickPunchRotate.target = onLongClickPunchRotateEnabled.boolValue;
            DrawPunch(DUIColors.OrangeLight.Color, showOnLongClickPunchRotate, onLongClickPunchRotatePunch, onLongClickPunchRotateStartDelay, onLongClickPunchRotateDuration, onLongClickPunchRotateVibrato, onLongClickPunchRotateElasticity);
        }
        void DrawOnLongClickPunchScale()
        {
            if (QUI.Button(DUIStyles.GetStyle(onLongClickPunchScaleEnabled.boolValue ? DUIStyles.ButtonStyle.PunchScale : DUIStyles.ButtonStyle.PunchScaleDisabled), WIDTH_420, 18)) { onLongClickPunchScaleEnabled.boolValue = !onLongClickPunchScaleEnabled.boolValue; }
            showOnLongClickPunchScale.target = onLongClickPunchScaleEnabled.boolValue;
            DrawPunch(DUIColors.RedLight.Color, showOnLongClickPunchScale, onLongClickPunchScalePunch, onLongClickPunchScaleStartDelay, onLongClickPunchScaleDuration, onLongClickPunchScaleVibrato, onLongClickPunchScaleElasticity);
        }
        void DrawOnLongClickEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnLongClickEvents.target ? DUIStyles.ButtonStyle.Events : DUIStyles.ButtonStyle.EventsCollapsed), WIDTH_420, 18)) { showOnLongClickEvents.target = !showOnLongClickEvents.target; }
            if (QUI.BeginFadeGroup(showOnLongClickEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.PropertyField(OnLongClick, new GUIContent() { text = "OnLongClick" }, WIDTH_420 - 6);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnLongClickGameEvents()
        {
            if (QUI.Button(DUIStyles.GetStyle(showOnLongClickGameEvents.target ? DUIStyles.ButtonStyle.GameEvents : DUIStyles.ButtonStyle.GameEventsCollapsed), WIDTH_420, 18)) { showOnLongClickGameEvents.target = !showOnLongClickGameEvents.target; }
            if (QUI.BeginFadeGroup(showOnLongClickGameEvents.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    QUI.DrawList(onLongClickGameEvents, WIDTH_420, "No Game Events are sent OnLongClick... Click [+] to start...");
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }
        void DrawOnLongClickNavigation()
        {
            if (!UIManager.IsNavigationEnabled) { QUI.Space(-SPACE_2); return; }
            if (QUI.Button(DUIStyles.GetStyle(showOnLongClickNavigation.target ? DUIStyles.ButtonStyle.Navigation : DUIStyles.ButtonStyle.NavigationCollapsed), WIDTH_420, 18)) { showOnLongClickNavigation.target = !showOnLongClickNavigation.target; }
            if (QUI.BeginFadeGroup(showOnLongClickNavigation.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    DrawNavigationData(uiButton.onLongClickNavigation, onLongClickEditorNavigationData);
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                QUI.ResetColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawPunch(Color color, AnimBool show, SerializedProperty punch, SerializedProperty startDelay, SerializedProperty duration, SerializedProperty vibrato, SerializedProperty elasicity)
        {
            if (QUI.BeginFadeGroup(show.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("punch", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 40);
                        QUI.PropertyField(punch, 240);
                        QUI.Space(SPACE_4);
                        QUI.Label("elasticity", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 54);
                        QUI.PropertyField(elasicity, 66);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 50);
                        QUI.PropertyField(duration, 78);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(startDelay, 78);
                        QUI.Space(SPACE_4);
                        QUI.Label("vibrato", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 42);
                        QUI.PropertyField(vibrato, 78);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawNormalLoop()
        {
            if (DUI.DUISettings == null || DUI.DUISettings.UIButton_Inspector_HideNormalLoop) { return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button(DUIStyles.GetStyle(showNormalAnimation.target ? DUIStyles.ButtonStyle.NormalAnimation : DUIStyles.ButtonStyle.NormalAnimationCollapsed), 336, 21)) { showNormalAnimation.target = !showNormalAnimation.target; }
                if (QUI.Button(DUIStyles.GetStyle(normalLoopMoveEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonMove : DUIStyles.ButtonStyle.BarButtonMoveDisabled), 21, 21)) { normalLoopMoveEnabled.boolValue = !normalLoopMoveEnabled.boolValue; if (normalLoopMoveEnabled.boolValue) { showNormalAnimation.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(normalLoopRotateEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonRotate : DUIStyles.ButtonStyle.BarButtonRotateDisabled), 21, 21)) { normalLoopRotateEnabled.boolValue = !normalLoopRotateEnabled.boolValue; if (normalLoopRotateEnabled.boolValue) { showNormalAnimation.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(normalLoopScaleEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonScale : DUIStyles.ButtonStyle.BarButtonScaleDisabled), 21, 21)) { normalLoopScaleEnabled.boolValue = !normalLoopScaleEnabled.boolValue; if (normalLoopScaleEnabled.boolValue) { showNormalAnimation.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(normalLoopFadeEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonFade : DUIStyles.ButtonStyle.BarButtonFadeDisabled), 21, 21)) { normalLoopFadeEnabled.boolValue = !normalLoopFadeEnabled.boolValue; if (normalLoopFadeEnabled.boolValue) { showNormalAnimation.target = true; } }
            }
            QUI.EndHorizontal();
            infoMessage["NormalLoopLoadPresetAtRuntime"].show.target = loadNormalLoopPresetAtRuntime.boolValue;
            infoMessage["NormalLoopLoadPresetAtRuntime"].message = normalLoopPresetCategory.stringValue + " / " + normalLoopPresetName.stringValue;
            DrawInfoMessage("NormalLoopLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (QUI.BeginFadeGroup(showNormalAnimation.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    DrawNormalLoopPreset();
                    QUI.Space(SPACE_2);
                    DrawNormalLoopMoveLoop();
                    QUI.Space(SPACE_2);
                    DrawNormalLoopRotateLoop();
                    QUI.Space(SPACE_2);
                    DrawNormalLoopScaleLoop();
                    QUI.Space(SPACE_2);
                    DrawNormalLoopFadeLoop();
                    QUI.Space(SPACE_16);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawNormalLoopPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showNormalAnimationPreset.target ? DUIStyles.ButtonStyle.barLoopPreset : DUIStyles.ButtonStyle.barLoopPresetCollapsed), WIDTH_420, 18)) { showNormalAnimationPreset.target = !showNormalAnimationPreset.target; }
            if (QUI.BeginFadeGroup(showNormalAnimationPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!normalLoopNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Loop iLoop = UIAnimatorUtil.GetLoop(normalLoopPresetCategory.stringValue, normalLoopPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIButton iTarget = (UIButton)targets[i];
                                        iTarget.normalLoop = iLoop.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiButton, "Load Preset");
                                    uiButton.normalLoop = UIAnimatorUtil.GetLoop(normalLoopPresetCategory.stringValue, normalLoopPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                normalLoopNewPreset = true;
                                normalLoopNewCategoryName = false;
                                newPresetCategoryName = normalLoopPresetCategory.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + normalLoopPresetName.stringValue + "' preset from the '" + normalLoopPresetCategory.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeleteLoopPreset(normalLoopPresetCategory.stringValue, normalLoopPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            if (iTarget.normalLoopPresetCategory.Equals(normalLoopPresetCategory.stringValue) ||
                                                iTarget.normalLoopPresetName.Equals(normalLoopPresetName.stringValue))
                                            {
                                                iTarget.normalLoopPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.normalLoopPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    normalLoopPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    normalLoopPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
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
                                normalLoopPresetCategoryIndex = EditorGUILayout.Popup(normalLoopPresetCategoryIndex, UIAnimatorUtil.LoopPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                normalLoopPresetCategory.stringValue = UIAnimatorUtil.LoopPresetCategories[normalLoopPresetCategoryIndex];
                                normalLoopPresetNameIndex = 0;
                                normalLoopPresetName.stringValue = UIAnimatorUtil.GetLoopPresetNames(normalLoopPresetCategory.stringValue)[normalLoopPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                normalLoopPresetNameIndex = EditorGUILayout.Popup(normalLoopPresetNameIndex, UIAnimatorUtil.GetLoopPresetNames(normalLoopPresetCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                normalLoopPresetName.stringValue = UIAnimatorUtil.GetLoopPresetNames(normalLoopPresetCategory.stringValue)[normalLoopPresetNameIndex];
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
                                if (normalLoopNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
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
                                    UIAnimatorUtil.CreateLoopPreset(newPresetCategoryName, newPresetName, uiButton.normalLoop.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            iTarget.normalLoopPresetCategory = newPresetCategoryName;
                                            iTarget.normalLoopPresetName = newPresetName;
                                        }
                                    }
                                    normalLoopPresetCategory.stringValue = newPresetCategoryName;
                                    normalLoopPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshLoopAnimations(true);
                                    normalLoopNewPreset = false;
                                    normalLoopNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                normalLoopNewPreset = false;
                                normalLoopNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!normalLoopNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    normalLoopPresetCategoryIndex = EditorGUILayout.Popup(normalLoopPresetCategoryIndex, UIAnimatorUtil.LoopPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    normalLoopPresetCategory.stringValue = UIAnimatorUtil.LoopPresetCategories[normalLoopPresetCategoryIndex];
                                    normalLoopPresetNameIndex = 0;
                                    newPresetCategoryName = normalLoopPresetCategory.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    normalLoopNewCategoryName = QUI.Toggle(normalLoopNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (normalLoopNewCategoryName) { newPresetCategoryName = ""; }
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
                                normalLoopNewCategoryName = QUI.Toggle(normalLoopNewCategoryName);
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
                        QUI.Toggle(loadNormalLoopPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawNormalLoopMoveLoop()
        {
            if (QUI.Button(DUIStyles.GetStyle(normalLoopMoveEnabled.boolValue ? DUIStyles.ButtonStyle.Move : DUIStyles.ButtonStyle.MoveDisabled), WIDTH_420, 18)) { normalLoopMoveEnabled.boolValue = !normalLoopMoveEnabled.boolValue; }
            showNormalAnimationMove.target = normalLoopMoveEnabled.boolValue;
            DrawMoveLoop(showNormalAnimationMove, normalLoopMoveStartDelay, normalLoopMoveDuration, normalLoopMoveMovement, normalLoopMoveLoops, normalLoopMoveLoopType, normalLoopMoveEaseType, normalLoopMoveEase, normalLoopMoveAnimationCurve);

        }
        void DrawNormalLoopRotateLoop()
        {
            if (QUI.Button(DUIStyles.GetStyle(normalLoopRotateEnabled.boolValue ? DUIStyles.ButtonStyle.Rotate : DUIStyles.ButtonStyle.RotateDisabled), WIDTH_420, 18)) { normalLoopRotateEnabled.boolValue = !normalLoopRotateEnabled.boolValue; }
            showNormalAnimationRotate.target = normalLoopRotateEnabled.boolValue;
            DrawRotateLoop(showNormalAnimationRotate, normalLoopRotateStartDelay, normalLoopRotateDuration, normalLoopRotateRotation, normalLoopRotateLoops, normalLoopRotateLoopType, normalLoopRotateEaseType, normalLoopRotateEase, normalLoopRotateAnimationCurve);
        }
        void DrawNormalLoopScaleLoop()
        {
            if (QUI.Button(DUIStyles.GetStyle(normalLoopScaleEnabled.boolValue ? DUIStyles.ButtonStyle.Scale : DUIStyles.ButtonStyle.ScaleDisabled), WIDTH_420, 18)) { normalLoopScaleEnabled.boolValue = !normalLoopScaleEnabled.boolValue; }
            showNormalAnimationScale.target = normalLoopScaleEnabled.boolValue;
            DrawScaleLoop(showNormalAnimationScale, normalLoopScaleStartDelay, normalLoopScaleDuration, normalLoopScaleMin, normalLoopScaleMax, normalLoopScaleLoops, normalLoopScaleLoopType, normalLoopScaleEaseType, normalLoopScaleEase, normalLoopScaleAnimationCurve);
        }
        void DrawNormalLoopFadeLoop()
        {
            if (QUI.Button(DUIStyles.GetStyle(normalLoopFadeEnabled.boolValue ? DUIStyles.ButtonStyle.Fade : DUIStyles.ButtonStyle.FadeDisabled), WIDTH_420, 18)) { normalLoopFadeEnabled.boolValue = !normalLoopFadeEnabled.boolValue; }
            showNormalAnimationFade.target = normalLoopFadeEnabled.boolValue;
            DrawFadeLoop(showNormalAnimationFade, normalLoopFadeStartDelay, normalLoopFadeDuration, normalLoopFadeMin, normalLoopFadeMax, normalLoopFadeLoops, normalLoopFadeLoopType, normalLoopFadeEaseType, normalLoopFadeEase, normalLoopFadeAnimationCurve);
        }

        void DrawSelectedLoop()
        {
            if (DUI.DUISettings == null || DUI.DUISettings.UIButton_Inspector_HideSelectedLoop) { return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button(DUIStyles.GetStyle(showSelectedAnimation.target ? DUIStyles.ButtonStyle.SelectedAnimation : DUIStyles.ButtonStyle.SelectedAnimationCollapsed), 336, 21)) { showSelectedAnimation.target = !showSelectedAnimation.target; }
                if (QUI.Button(DUIStyles.GetStyle(selectedLoopMoveEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonMove : DUIStyles.ButtonStyle.BarButtonMoveDisabled), 21, 21)) { selectedLoopMoveEnabled.boolValue = !selectedLoopMoveEnabled.boolValue; if (selectedLoopMoveEnabled.boolValue) { showSelectedAnimation.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(selectedLoopRotateEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonRotate : DUIStyles.ButtonStyle.BarButtonRotateDisabled), 21, 21)) { selectedLoopRotateEnabled.boolValue = !selectedLoopRotateEnabled.boolValue; if (selectedLoopRotateEnabled.boolValue) { showSelectedAnimation.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(selectedLoopScaleEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonScale : DUIStyles.ButtonStyle.BarButtonScaleDisabled), 21, 21)) { selectedLoopScaleEnabled.boolValue = !selectedLoopScaleEnabled.boolValue; if (selectedLoopScaleEnabled.boolValue) { showSelectedAnimation.target = true; } }
                if (QUI.Button(DUIStyles.GetStyle(selectedLoopFadeEnabled.boolValue ? DUIStyles.ButtonStyle.BarButtonFade : DUIStyles.ButtonStyle.BarButtonFadeDisabled), 21, 21)) { selectedLoopFadeEnabled.boolValue = !selectedLoopFadeEnabled.boolValue; if (selectedLoopFadeEnabled.boolValue) { showSelectedAnimation.target = true; } }
            }
            QUI.EndHorizontal();
            infoMessage["SelectedLoopLoadPresetAtRuntime"].show.target = loadSelectedLoopPresetAtRuntime.boolValue;
            infoMessage["SelectedLoopLoadPresetAtRuntime"].message = selectedLoopPresetCategory.stringValue + " / " + selectedLoopPresetName.stringValue;
            DrawInfoMessage("SelectedLoopLoadPresetAtRuntime", WIDTH_420);
            QUI.Space(SPACE_4);
            if (QUI.BeginFadeGroup(showSelectedAnimation.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    DrawSelectedLoopPreset();
                    QUI.Space(SPACE_2);
                    DrawSelectedLoopMoveLoop();
                    QUI.Space(SPACE_2);
                    DrawSelectedLoopRotateLoop();
                    QUI.Space(SPACE_2);
                    DrawSelectedLoopScaleLoop();
                    QUI.Space(SPACE_2);
                    DrawSelectedLoopFadeLoop();
                    QUI.Space(SPACE_16);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawSelectedLoopPreset()
        {
            if (QUI.Button(DUIStyles.GetStyle(showSelectedAnimationPreset.target ? DUIStyles.ButtonStyle.barLoopPreset : DUIStyles.ButtonStyle.barLoopPresetCollapsed), WIDTH_420, 18)) { showSelectedAnimationPreset.target = !showSelectedAnimationPreset.target; }
            if (QUI.BeginFadeGroup(showSelectedAnimationPreset.faded))
            {
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.Space(SPACE_2);
                    if (!selectedLoopNewPreset)
                    {
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            SaveColors();
                            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
                            if (QUI.Button("Load Preset"))
                            {
                                if (serializedObject.isEditingMultipleObjects)
                                {
                                    Loop iLoop = UIAnimatorUtil.GetLoop(selectedLoopPresetCategory.stringValue, selectedLoopPresetName.stringValue);
                                    Undo.RecordObjects(targets, "Load Preset to Multiple Targets");
                                    for (int i = 0; i < targets.Length; i++)
                                    {
                                        UIButton iTarget = (UIButton)targets[i];
                                        iTarget.selectedLoop = iLoop.Copy();
                                    }
                                }
                                else
                                {
                                    Undo.RecordObject(uiButton, "Load Preset");
                                    uiButton.selectedLoop = UIAnimatorUtil.GetLoop(selectedLoopPresetCategory.stringValue, selectedLoopPresetName.stringValue);
                                }
                                QUI.ExitGUI();
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                            if (QUI.Button("New Preset"))
                            {
                                selectedLoopNewPreset = true;
                                selectedLoopNewCategoryName = false;
                                newPresetCategoryName = selectedLoopPresetCategory.stringValue;
                                newPresetName = "";
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Delete Preset"))
                            {
                                if (EditorUtility.DisplayDialog("Delete Preset", "Are you sure you want to delete the '" + selectedLoopPresetName.stringValue + "' preset from the '" + selectedLoopPresetCategory.stringValue + "' preset category?", "Yes", "No"))
                                {
                                    UIAnimatorUtil.DeleteLoopPreset(selectedLoopPresetCategory.stringValue, selectedLoopPresetName.stringValue);
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            if (iTarget.selectedLoopPresetCategory.Equals(selectedLoopPresetCategory.stringValue) ||
                                                iTarget.selectedLoopPresetName.Equals(selectedLoopPresetName.stringValue))
                                            {
                                                iTarget.selectedLoopPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                                iTarget.selectedLoopPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
                                            }
                                        }
                                    }
                                    selectedLoopPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                                    selectedLoopPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
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
                                selectedLoopPresetCategoryIndex = EditorGUILayout.Popup(selectedLoopPresetCategoryIndex, UIAnimatorUtil.LoopPresetCategories.ToArray());

                            }
                            if (QUI.EndChangeCheck())
                            {
                                selectedLoopPresetCategory.stringValue = UIAnimatorUtil.LoopPresetCategories[selectedLoopPresetCategoryIndex];
                                selectedLoopPresetNameIndex = 0;
                                selectedLoopPresetName.stringValue = UIAnimatorUtil.GetLoopPresetNames(selectedLoopPresetCategory.stringValue)[selectedLoopPresetNameIndex];
                            }

                        }
                        QUI.EndHorizontal();
                        QUI.BeginHorizontal(WIDTH_420 - 8);
                        {
                            QUI.Label("Preset Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                            QUI.BeginChangeCheck();
                            {
                                selectedLoopPresetNameIndex = EditorGUILayout.Popup(selectedLoopPresetNameIndex, UIAnimatorUtil.GetLoopPresetNames(selectedLoopPresetCategory.stringValue).ToArray());
                            }
                            if (QUI.EndChangeCheck())
                            {
                                selectedLoopPresetName.stringValue = UIAnimatorUtil.GetLoopPresetNames(selectedLoopPresetCategory.stringValue)[selectedLoopPresetNameIndex];
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
                                if (selectedLoopNewCategoryName && string.IsNullOrEmpty(newPresetCategoryName.Trim()))
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
                                    UIAnimatorUtil.CreateLoopPreset(newPresetCategoryName, newPresetName, uiButton.selectedLoop.Copy());
                                    if (serializedObject.isEditingMultipleObjects)
                                    {
                                        for (int i = 0; i < targets.Length; i++)
                                        {
                                            UIButton iTarget = (UIButton)targets[i];
                                            iTarget.selectedLoopPresetCategory = newPresetCategoryName;
                                            iTarget.selectedLoopPresetName = newPresetName;
                                        }
                                    }
                                    selectedLoopPresetCategory.stringValue = newPresetCategoryName;
                                    selectedLoopPresetName.stringValue = newPresetName;
                                    serializedObject.ApplyModifiedProperties();
                                    RefreshLoopAnimations(true);
                                    selectedLoopNewPreset = false;
                                    selectedLoopNewCategoryName = false;
                                    newPresetCategoryName = "";
                                    newPresetName = "";
                                }
                            }
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            if (QUI.Button("Cancel"))
                            {
                                selectedLoopNewPreset = false;
                                selectedLoopNewCategoryName = false;
                                newPresetCategoryName = "";
                                newPresetName = "";
                            }
                            RestoreColors();
                        }
                        QUI.EndHorizontal();
                        if (!selectedLoopNewCategoryName)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 8);
                            {
                                QUI.Label("Select Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_105);
                                QUI.BeginChangeCheck();
                                {
                                    selectedLoopPresetCategoryIndex = EditorGUILayout.Popup(selectedLoopPresetCategoryIndex, UIAnimatorUtil.LoopPresetCategories.ToArray(), GUILayout.Width(200));
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    selectedLoopPresetCategory.stringValue = UIAnimatorUtil.LoopPresetCategories[selectedLoopPresetCategoryIndex];
                                    selectedLoopPresetNameIndex = 0;
                                    newPresetCategoryName = selectedLoopPresetCategory.stringValue;
                                }
                                QUI.BeginChangeCheck();
                                {
                                    selectedLoopNewCategoryName = QUI.Toggle(selectedLoopNewCategoryName);
                                }
                                if (QUI.EndChangeCheck())
                                {
                                    if (selectedLoopNewCategoryName) { newPresetCategoryName = ""; }
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
                                selectedLoopNewCategoryName = QUI.Toggle(selectedLoopNewCategoryName);
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
                        QUI.Toggle(loadSelectedLoopPresetAtRuntime);
                        QUI.Label("Load preset at runtime (overrides current settings)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420 - 12);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();
        }
        void DrawSelectedLoopMoveLoop()
        {
            if (QUI.Button(DUIStyles.GetStyle(selectedLoopMoveEnabled.boolValue ? DUIStyles.ButtonStyle.Move : DUIStyles.ButtonStyle.MoveDisabled), WIDTH_420, 18)) { selectedLoopMoveEnabled.boolValue = !selectedLoopMoveEnabled.boolValue; }
            showSelectedAnimationMove.target = selectedLoopMoveEnabled.boolValue;
            DrawMoveLoop(showSelectedAnimationMove, selectedLoopMoveStartDelay, selectedLoopMoveDuration, selectedLoopMoveMovement, selectedLoopMoveLoops, selectedLoopMoveLoopType, selectedLoopMoveEaseType, selectedLoopMoveEase, selectedLoopMoveAnimationCurve);
        }
        void DrawSelectedLoopRotateLoop()
        {
            if (QUI.Button(DUIStyles.GetStyle(selectedLoopRotateEnabled.boolValue ? DUIStyles.ButtonStyle.Rotate : DUIStyles.ButtonStyle.RotateDisabled), WIDTH_420, 18)) { selectedLoopRotateEnabled.boolValue = !selectedLoopRotateEnabled.boolValue; }
            showSelectedAnimationRotate.target = selectedLoopRotateEnabled.boolValue;
            DrawRotateLoop(showSelectedAnimationRotate, selectedLoopRotateStartDelay, selectedLoopRotateDuration, selectedLoopRotateRotation, selectedLoopRotateLoops, selectedLoopRotateLoopType, selectedLoopRotateEaseType, selectedLoopRotateEase, selectedLoopRotateAnimationCurve);
        }
        void DrawSelectedLoopScaleLoop()
        {
            if (QUI.Button(DUIStyles.GetStyle(selectedLoopScaleEnabled.boolValue ? DUIStyles.ButtonStyle.Scale : DUIStyles.ButtonStyle.ScaleDisabled), WIDTH_420, 18)) { selectedLoopScaleEnabled.boolValue = !selectedLoopScaleEnabled.boolValue; }
            showSelectedAnimationScale.target = selectedLoopScaleEnabled.boolValue;
            DrawScaleLoop(showSelectedAnimationScale, selectedLoopScaleStartDelay, selectedLoopScaleDuration, selectedLoopScaleMin, selectedLoopScaleMax, selectedLoopScaleLoops, selectedLoopScaleLoopType, selectedLoopScaleEaseType, selectedLoopScaleEase, selectedLoopScaleAnimationCurve);
        }
        void DrawSelectedLoopFadeLoop()
        {
            if (QUI.Button(DUIStyles.GetStyle(selectedLoopFadeEnabled.boolValue ? DUIStyles.ButtonStyle.Fade : DUIStyles.ButtonStyle.FadeDisabled), WIDTH_420, 18)) { selectedLoopFadeEnabled.boolValue = !selectedLoopFadeEnabled.boolValue; }
            showSelectedAnimationFade.target = selectedLoopFadeEnabled.boolValue;
            DrawFadeLoop(showSelectedAnimationFade, selectedLoopFadeStartDelay, selectedLoopFadeDuration, selectedLoopFadeMin, selectedLoopFadeMax, selectedLoopFadeLoops, selectedLoopFadeLoopType, selectedLoopFadeEaseType, selectedLoopFadeEase, selectedLoopFadeAnimationCurve);
        }


        void DrawMoveLoop(AnimBool show,
                                  SerializedProperty startDelay, SerializedProperty duration,
                                  SerializedProperty movement, SerializedProperty loops, SerializedProperty loopType,
                                  SerializedProperty easeType, SerializedProperty ease, SerializedProperty animationCurve)
        {
            if (QUI.BeginFadeGroup(show.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.GreenLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("movement", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
                        QUI.PropertyField(movement, 348);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(duration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(startDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        QUI.PropertyField(loops, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        QUI.PropertyField(loopType, 56);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(easeType, WIDTH_105);
                        QUI.PropertyField(easeType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? ease : animationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawRotateLoop(AnimBool show,
                                  SerializedProperty startDelay, SerializedProperty duration,
                                  SerializedProperty rotation, SerializedProperty loops, SerializedProperty loopType,
                                  SerializedProperty easeType, SerializedProperty ease, SerializedProperty animationCurve)
        {
            if (QUI.BeginFadeGroup(show.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.OrangeLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("rotation", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 64);
                        QUI.PropertyField(rotation, 348);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(duration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(startDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        QUI.PropertyField(loops, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        QUI.PropertyField(loopType, 56);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(easeType, WIDTH_105);
                        QUI.PropertyField(easeType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? ease : animationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawScaleLoop(AnimBool show,
                          SerializedProperty startDelay, SerializedProperty duration,
                          SerializedProperty min, SerializedProperty max, SerializedProperty loops, SerializedProperty loopType,
                          SerializedProperty easeType, SerializedProperty ease, SerializedProperty animationCurve)
        {
            if (QUI.BeginFadeGroup(show.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("min", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 24);
                        QUI.PropertyField(min, 178);
                        QUI.Space(SPACE_4);
                        QUI.Label("max", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 28);
                        QUI.PropertyField(max, 168);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(duration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(startDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        QUI.PropertyField(loops, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        QUI.PropertyField(loopType, 56);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(easeType, WIDTH_105);
                        QUI.PropertyField(easeType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? ease : animationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawFadeLoop(AnimBool show,
                       SerializedProperty startDelay, SerializedProperty duration,
                       SerializedProperty min, SerializedProperty max, SerializedProperty loops, SerializedProperty loopType,
                       SerializedProperty easeType, SerializedProperty ease, SerializedProperty animationCurve)
        {
            if (QUI.BeginFadeGroup(show.faded))
            {
                SaveColors();
                QUI.SetGUIBackgroundColor(DUIColors.PurpleLight.Color);
                QUI.BeginVertical(WIDTH_420);
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("min", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 24);
                        QUI.PropertyField(min, 36);
                        QUI.Space(SPACE_4);
                        QUI.Label("max", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 28);
                        QUI.PropertyField(max, 36);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Label("duration", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 52);
                        QUI.PropertyField(duration, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("start delay", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 62);
                        QUI.PropertyField(startDelay, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loops", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 36);
                        QUI.PropertyField(loops, 38);
                        QUI.Space(SPACE_4);
                        QUI.Label("loop type", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 56);
                        QUI.PropertyField(loopType, 56);
                    }
                    QUI.EndHorizontal();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.PropertyField(easeType, WIDTH_105);
                        QUI.PropertyField(easeType.enumValueIndex == (int)UIAnimator.EaseType.Ease ? ease : animationCurve, 307);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_8);
                }
                QUI.EndVertical();
                RestoreColors();
            }
            QUI.EndFadeGroup();
        }

        void DrawNavigationData(NavigationPointerData navData, EditorNavigationPointerData editorNavData)
        {
            if (DUI.UIElementsDatabase == null) { DUI.RefreshUIElementsDatabase(); }
            if (uiButton.IsBackButton) { navData.addToNavigationHistory = false; }

            SaveColors();
            QUI.SetGUIBackgroundColor(DUIColors.BlueLight.Color);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(30);
                bool tempBool = navData.addToNavigationHistory;
                QUI.BeginChangeCheck();
                tempBool = QUI.Toggle(tempBool);
                if (QUI.EndChangeCheck())
                {
                    if (!uiButton.IsBackButton)
                    {
                        Undo.RecordObject(uiButton, "Updated NavigationPointer AddToNavigationHistory");
                        navData.addToNavigationHistory = tempBool;
                    }
                }
                QUI.Label("Add To Navigation History", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 370);
            }
            QUI.EndHorizontal();
            RestoreColors();
            QUI.Space(SPACE_2);
            DrawNavigationDataList(DUIResources.barShow.texture, navData.show, editorNavData.showIndex, DUIColors.GreenLight.Color);
            QUI.Space(SPACE_4);
            DrawNavigationDataList(DUIResources.barHide.texture, navData.hide, editorNavData.hideIndex, DUIColors.RedLight.Color);
        }
        void DrawNavigationDataList(Texture header, List<NavigationPointer> list, List<EditorNavigationPointer> listIndex, Color color)
        {
            if (listIndex.Count != list.Count) { RefreshNavigationData(); }
            QUI.DrawTexture(header, 420, 18);
            SaveColors();
            QUI.SetGUIBackgroundColor(color);

            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(30);
                QUI.BeginVertical(WIDTH_420 - 30);
                {
                    if (uiButton.IsBackButton)
                    {
                        list.Clear();
                        listIndex.Clear();
                        QUI.BeginHorizontal(WIDTH_420 - 32);
                        {
                            QUI.Label("This is a 'Back' button...", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal));
                            QUI.FlexibleSpace();
                        }
                        QUI.EndHorizontal();
                    }
                    else
                    {
                        QUI.Space(-SPACE_2);
                        if (list.Count > 0)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 32);
                            {
                                QUI.Label("Element Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 179);
                                QUI.Label("Element Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 179);
                                QUI.FlexibleSpace();
                            }
                            QUI.EndHorizontal();
                            QUI.Space(-SPACE_4);
                        }
                        string customName = "";
                        for (int i = 0; i < list.Count; i++)
                        {
                            QUI.BeginHorizontal(WIDTH_420 - 32);
                            {
                                QUI.BeginChangeCheck();
                                listIndex[i].categoryIndex = EditorGUILayout.Popup(listIndex[i].categoryIndex, DUI.UIElementCategories.ToArray(), GUILayout.Width(179));
                                if (QUI.EndChangeCheck())
                                {
                                    Undo.RecordObject(uiButton, "Updated NavigationPointer Category");
                                    list[i].category = DUI.UIElementCategories[listIndex[i].categoryIndex];

                                    if (list[i].category.Equals(DUI.CUSTOM_NAME))
                                    {
                                        listIndex[i].nameIndex = 0;
                                        list[i].name = "";
                                    }
                                    else if (DUI.UIElementNameExists(list[i].category, list[i].name))
                                    {
                                        listIndex[i].nameIndex = DUI.UIElementsDatabase[list[i].category].data.IndexOf(list[i].name);
                                    }
                                    else
                                    {
                                        if (!list[i].name.Equals(DUI.DEFAULT_ELEMENT_NAME) && !string.IsNullOrEmpty(list[i].name.Trim()) && EditorUtility.DisplayDialog("Action Required", "The '" + list[i].name + "' element name does not exist in the '" + DUI.UIElementCategories[listIndex[i].categoryIndex] + "' category database.\nDo you want to add it now?", "Yes", "No"))
                                        {
                                            DUI.AddUIElementName(DUI.UIElementCategories[listIndex[i].categoryIndex], list[i].name);
                                            RefreshNavigationDataList(list, listIndex);
                                            listIndex[i].nameIndex = DUI.GetUIElementNames(list[i].category).IndexOf(list[i].name);
                                        }
                                        else
                                        {
                                            listIndex[i].nameIndex = 0;
                                            list[i].name = DUI.UIElementsDatabase[list[i].category].data[0];
                                        }
                                    }
                                }

                                if (!list[i].category.Equals(DUI.CUSTOM_NAME))
                                {
                                    QUI.BeginChangeCheck();
                                    listIndex[i].nameIndex = EditorGUILayout.Popup(listIndex[i].nameIndex, DUI.UIElementsDatabase[list[i].category].ToArray(), GUILayout.Width(179));
                                    if (QUI.EndChangeCheck())
                                    {
                                        Undo.RecordObject(uiButton, "Updated NavigationPointer Name");
                                        list[i].name = DUI.UIElementsDatabase[list[i].category].data[listIndex[i].nameIndex];
                                    }
                                }
                                else
                                {
                                    customName = list[i].name;
                                    QUI.BeginChangeCheck();
                                    customName = EditorGUILayout.TextField(customName, GUILayout.Width(179));
                                    if (QUI.EndChangeCheck())
                                    {
                                        Undo.RecordObject(uiButton, "Updated NavigationPointer Name");
                                        list[i].name = customName;
                                    }
                                }
                                QUI.BeginVertical(18);
                                {
                                    QUI.Space(-1);
                                    if (QUI.ButtonMinus())
                                    {
                                        Undo.RecordObject(uiButton, "Removed NavigationPointer");
                                        list.RemoveAt(i);
                                        listIndex.RemoveAt(i);
                                        RefreshNavigationDataList(list, listIndex);
                                        QUI.ExitGUI();
                                    }
                                }
                                QUI.EndVertical();
                            }
                            QUI.EndHorizontal();
                        }
                        if (list.Count > 0)
                        {
                            QUI.Space(-SPACE_4);
                        }

                        QUI.BeginHorizontal(WIDTH_420 - 32);
                        {
                            if (list.Count == 0)
                            {
                                QUI.Label("List is empty... Click [+] to start...", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal));
                            }
                            QUI.FlexibleSpace();
                            if (QUI.ButtonPlus())
                            {
                                Undo.RecordObject(uiButton, "Added NavigationPointer");
                                list.Add(new NavigationPointer(DUI.DEFAULT_CATEGORY_NAME, DUI.DEFAULT_ELEMENT_NAME));
                                listIndex.Add(new EditorNavigationPointer(DUI.UIElementCategories.IndexOf(DUI.DEFAULT_CATEGORY_NAME), DUI.UIElementsDatabase[DUI.DEFAULT_CATEGORY_NAME].data.IndexOf(DUI.DEFAULT_ELEMENT_NAME)));
                                RefreshNavigationDataList(list, listIndex);
                            }
                        }
                        QUI.EndHorizontal();
                    }
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
            if (list.Count > 0) { QUI.Space(SPACE_8); }
            QUI.ResetColors();
        }

        void RefreshUIButtonsDatabase(bool forcedRefresh = false)
        {
            if (DUI.UIButtonsDatabase == null || forcedRefresh)
            {
                DUI.RefreshUIButtonsDatabase();
            }
        }
        void RefreshUISoundsDatabase(bool forcedRefresh = false)
        {
            if (DUI.UISoundsDatabase == null || forcedRefresh)
            {
                DUI.RefreshUISoundsDatabase();
            }
        }

        void ValiateUIButtonNameAndCategory()
        {
            if (string.IsNullOrEmpty(buttonName.stringValue)) //buttonName is empty? -> reset button name to default
            {
                buttonName.stringValue = DUI.DEFAULT_BUTTON_NAME;
            }
            if (buttonCategory.stringValue != DUI.CUSTOM_NAME)
            {
                if (!DUI.UIButtonCategoryExists(buttonCategory.stringValue)) //category does not exist -> reset button category to default
                {
                    EditorUtility.DisplayDialog("Information", "This button's category is set to '" + buttonCategory.stringValue + "', but this category was not found in the UIButtons Database.\nResetting this button's category to the default value (" + DUI.DEFAULT_CATEGORY_NAME + ").", "Ok");
                    buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                }
                if (!DUI.UIButtonNameExists(buttonCategory.stringValue, buttonName.stringValue)) //button name does not exist in the set category -> change category to default & add the button name to the database
                {
                    if (EditorUtility.DisplayDialog("Action Required", "This button's name is set to '" + buttonName.stringValue + "', but it was not found in the '" + buttonCategory.stringValue + "' category.\nDo you want to add the name to the set category?", "Yes", "No"))
                    {
                        DUI.AddUIButtonName(buttonCategory.stringValue, buttonName.stringValue);
                        buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Information", "This button's category was reset to the default value (" + DUI.DEFAULT_CATEGORY_NAME + ").", "Ok");
                        buttonCategory.stringValue = DUI.DEFAULT_CATEGORY_NAME;
                        if (!DUI.UIButtonNameExists(buttonCategory.stringValue, buttonName.stringValue))
                        {
                            if (EditorUtility.DisplayDialog("Action Required", "This button's name is set to '" + buttonName.stringValue + "', but it was not found in the '" + buttonCategory.stringValue + "' category.\nDo you want to add the name to the set category?", "Yes", "No"))
                            {
                                DUI.AddUIButtonName(buttonCategory.stringValue, buttonName.stringValue);
                                buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                            }
                            else
                            {
                                EditorUtility.DisplayDialog("Information", "This button's name was reset to the default value (" + DUI.DEFAULT_BUTTON_NAME + ").", "Ok");
                                buttonName.stringValue = DUI.DEFAULT_BUTTON_NAME;
                                buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                            }
                        }
                        else
                        {
                            buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                        }
                    }
                }
                else
                {
                    buttonNameIndex = DUI.GetUIButtonNames(buttonCategory.stringValue).IndexOf(buttonName.stringValue);
                }
            }
            else
            {
                buttonNameIndex = DUI.GetUIButtonNames(DUI.DEFAULT_CATEGORY_NAME).IndexOf(DUI.DEFAULT_BUTTON_NAME);
            }
            buttonCategoryIndex = DUI.UIButtonCategories.IndexOf(buttonCategory.stringValue);
        }
        void ValidateUISounds()
        {
            if (!customOnPointerEnterSound.boolValue)
            {
                if (string.IsNullOrEmpty(onPointerEnterSound.stringValue) ||
                   onPointerEnterSound.stringValue.Equals(DUI.DEFAULT_SOUND_NAME) ||
                   !DUI.UISoundNameExists(onPointerEnterSound.stringValue, SoundType.UIButtons))
                {
                    onPointerEnterSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                }
                onPointerEnterSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onPointerEnterSound.stringValue);
            }
            if (!customOnPointerExitSound.boolValue)
            {
                if (string.IsNullOrEmpty(onPointerExitSound.stringValue) ||
                   onPointerExitSound.stringValue.Equals(DUI.DEFAULT_SOUND_NAME) ||
                   !DUI.UISoundNameExists(onPointerExitSound.stringValue, SoundType.UIButtons))
                {
                    onPointerExitSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                }
                onPointerExitSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onPointerExitSound.stringValue);
            }
            if (!customOnPointerDownSound.boolValue)
            {
                if (string.IsNullOrEmpty(onPointerDownSound.stringValue) ||
                   onPointerDownSound.stringValue.Equals(DUI.DEFAULT_SOUND_NAME) ||
                   !DUI.UISoundNameExists(onPointerDownSound.stringValue, SoundType.UIButtons))
                {
                    onPointerDownSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                }
                onPointerDownSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onPointerDownSound.stringValue);
            }
            if (!customOnPointerUpSound.boolValue)
            {
                if (string.IsNullOrEmpty(onPointerUpSound.stringValue) ||
                   onPointerUpSound.stringValue.Equals(DUI.DEFAULT_SOUND_NAME) ||
                   !DUI.UISoundNameExists(onPointerUpSound.stringValue, SoundType.UIButtons))
                {
                    onPointerUpSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                }
                onPointerUpSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onPointerUpSound.stringValue);
            }
            if (!customOnClickSound.boolValue)
            {
                if (string.IsNullOrEmpty(onClickSound.stringValue) ||
                   onClickSound.stringValue.Equals(DUI.DEFAULT_SOUND_NAME) ||
                   !DUI.UISoundNameExists(onClickSound.stringValue, SoundType.UIButtons))
                {
                    onClickSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                }
                onClickSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onClickSound.stringValue);
            }
            if (!customOnDoubleClickSound.boolValue)
            {
                if (string.IsNullOrEmpty(onDoubleClickSound.stringValue) ||
                   onDoubleClickSound.stringValue.Equals(DUI.DEFAULT_SOUND_NAME) ||
                   !DUI.UISoundNameExists(onDoubleClickSound.stringValue, SoundType.UIButtons))
                {
                    onDoubleClickSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                }
                onDoubleClickSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onDoubleClickSound.stringValue);
            }
            if (!customOnLongClickSound.boolValue)
            {
                if (string.IsNullOrEmpty(onLongClickSound.stringValue) ||
                   onLongClickSound.stringValue.Equals(DUI.DEFAULT_SOUND_NAME) ||
                   !DUI.UISoundNameExists(onLongClickSound.stringValue, SoundType.UIButtons))
                {
                    onLongClickSound.stringValue = DUI.DEFAULT_SOUND_NAME;
                }
                onLongClickSoundIndex = DUI.UISoundNamesUIButtons.IndexOf(onLongClickSound.stringValue);
            }
        }

        void RefreshPunchAnimationsPresets(bool forcedRefresh = false)
        {
            if (UIAnimatorUtil.PunchDataPresetsDatabase == null || forcedRefresh)
            {
                UIAnimatorUtil.RefreshPunchDataPresetsDatabase();
            }
        }
        void RefreshLoopAnimationsPresets(bool forcedRefresh = false)
        {
            if (UIAnimatorUtil.LoopDataPresetsDatabase == null || forcedRefresh)
            {
                UIAnimatorUtil.RefreshLoopDataPresetsDatabase();
            }
        }

        void ValidatePunchAnimationsPresets()
        {
            //preset category is empty or preset category does not exist -> reset to default
            if (string.IsNullOrEmpty(onPointerEnterPunchPresetCategory.stringValue) ||
                !UIAnimatorUtil.PunchPresetCategoryExists(onPointerEnterPunchPresetCategory.stringValue))
            {
                onPointerEnterPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }
            if (string.IsNullOrEmpty(onPointerExitPunchPresetCategory.stringValue) ||
                !UIAnimatorUtil.PunchPresetCategoryExists(onPointerExitPunchPresetCategory.stringValue))
            {
                onPointerExitPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }
            if (string.IsNullOrEmpty(onPointerDownPunchPresetCategory.stringValue) ||
                !UIAnimatorUtil.PunchPresetCategoryExists(onPointerDownPunchPresetCategory.stringValue))
            {
                onPointerDownPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }
            if (string.IsNullOrEmpty(onPointerUpPunchPresetCategory.stringValue) ||
                !UIAnimatorUtil.PunchPresetCategoryExists(onPointerUpPunchPresetCategory.stringValue))
            {
                onPointerUpPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }
            if (string.IsNullOrEmpty(onClickPunchPresetCategory.stringValue) ||
                !UIAnimatorUtil.PunchPresetCategoryExists(onClickPunchPresetCategory.stringValue))
            {
                onClickPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }
            if (string.IsNullOrEmpty(onDoubleClickPunchPresetCategory.stringValue) ||
                !UIAnimatorUtil.PunchPresetCategoryExists(onDoubleClickPunchPresetCategory.stringValue))
            {
                onDoubleClickPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }
            if (string.IsNullOrEmpty(onLongClickPunchPresetCategory.stringValue) ||
                !UIAnimatorUtil.PunchPresetCategoryExists(onLongClickPunchPresetCategory.stringValue))
            {
                onLongClickPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }

            //preset name is empty or preset name does not exist in set category -> reset to default preset category and preset name
            if (string.IsNullOrEmpty(onPointerEnterPunchPresetName.stringValue) ||
                !UIAnimatorUtil.PunchPresetExists(onPointerEnterPunchPresetCategory.stringValue, onPointerEnterPunchPresetName.stringValue))
            {
                onPointerEnterPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                onPointerEnterPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }
            if (string.IsNullOrEmpty(onPointerExitPunchPresetName.stringValue) ||
              !UIAnimatorUtil.PunchPresetExists(onPointerExitPunchPresetCategory.stringValue, onPointerExitPunchPresetName.stringValue))
            {
                onPointerExitPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                onPointerExitPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }
            if (string.IsNullOrEmpty(onPointerDownPunchPresetName.stringValue) ||
              !UIAnimatorUtil.PunchPresetExists(onPointerDownPunchPresetCategory.stringValue, onPointerDownPunchPresetName.stringValue))
            {
                onPointerDownPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                onPointerDownPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }
            if (string.IsNullOrEmpty(onPointerUpPunchPresetName.stringValue) ||
              !UIAnimatorUtil.PunchPresetExists(onPointerUpPunchPresetCategory.stringValue, onPointerUpPunchPresetName.stringValue))
            {
                onPointerUpPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                onPointerUpPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }
            if (string.IsNullOrEmpty(onClickPunchPresetName.stringValue) ||
              !UIAnimatorUtil.PunchPresetExists(onClickPunchPresetCategory.stringValue, onClickPunchPresetName.stringValue))
            {
                onClickPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                onClickPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }
            if (string.IsNullOrEmpty(onDoubleClickPunchPresetName.stringValue) ||
              !UIAnimatorUtil.PunchPresetExists(onDoubleClickPunchPresetCategory.stringValue, onDoubleClickPunchPresetName.stringValue))
            {
                onDoubleClickPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                onDoubleClickPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }
            if (string.IsNullOrEmpty(onLongClickPunchPresetName.stringValue) ||
              !UIAnimatorUtil.PunchPresetExists(onLongClickPunchPresetCategory.stringValue, onLongClickPunchPresetName.stringValue))
            {
                onLongClickPunchPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                onLongClickPunchPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }

            onPointerEnterPunchPresetCategoryNameIndex = UIAnimatorUtil.PunchPresetCategories.IndexOf(onPointerEnterPunchPresetCategory.stringValue);
            onPointerEnterPunchPresetNameIndex = UIAnimatorUtil.GetPunchPresetNames(onPointerEnterPunchPresetCategory.stringValue).IndexOf(onPointerEnterPunchPresetName.stringValue);
            onPointerExitPunchPresetCategoryNameIndex = UIAnimatorUtil.PunchPresetCategories.IndexOf(onPointerExitPunchPresetCategory.stringValue);
            onPointerExitPunchPresetNameIndex = UIAnimatorUtil.GetPunchPresetNames(onPointerExitPunchPresetCategory.stringValue).IndexOf(onPointerExitPunchPresetName.stringValue);
            onPointerDownPunchPresetCategoryNameIndex = UIAnimatorUtil.PunchPresetCategories.IndexOf(onPointerDownPunchPresetCategory.stringValue);
            onPointerDownPunchPresetNameIndex = UIAnimatorUtil.GetPunchPresetNames(onPointerDownPunchPresetCategory.stringValue).IndexOf(onPointerDownPunchPresetName.stringValue);
            onPointerUpPunchPresetCategoryNameIndex = UIAnimatorUtil.PunchPresetCategories.IndexOf(onPointerUpPunchPresetCategory.stringValue);
            onPointerUpPunchPresetNameIndex = UIAnimatorUtil.GetPunchPresetNames(onPointerUpPunchPresetCategory.stringValue).IndexOf(onPointerUpPunchPresetName.stringValue);
            onClickPunchPresetCategoryNameIndex = UIAnimatorUtil.PunchPresetCategories.IndexOf(onClickPunchPresetCategory.stringValue);
            onClickPunchPresetNameIndex = UIAnimatorUtil.GetPunchPresetNames(onClickPunchPresetCategory.stringValue).IndexOf(onClickPunchPresetName.stringValue);
            onDoubleClickPunchPresetCategoryNameIndex = UIAnimatorUtil.PunchPresetCategories.IndexOf(onDoubleClickPunchPresetCategory.stringValue);
            onDoubleClickPunchPresetNameIndex = UIAnimatorUtil.GetPunchPresetNames(onDoubleClickPunchPresetCategory.stringValue).IndexOf(onDoubleClickPunchPresetName.stringValue);
            onLongClickPunchPresetCategoryNameIndex = UIAnimatorUtil.PunchPresetCategories.IndexOf(onLongClickPunchPresetCategory.stringValue);
            onLongClickPunchPresetNameIndex = UIAnimatorUtil.GetPunchPresetNames(onLongClickPunchPresetCategory.stringValue).IndexOf(onLongClickPunchPresetName.stringValue);
        }
        void ValidateLoopAnimationsPresets()
        {
            //preset category is empty or preset category does not exist -> reset to default
            if (string.IsNullOrEmpty(normalLoopPresetCategory.stringValue) ||
                !UIAnimatorUtil.LoopPresetCategoryExists(normalLoopPresetCategory.stringValue))
            {
                normalLoopPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }
            if (string.IsNullOrEmpty(selectedLoopPresetCategory.stringValue) ||
                !UIAnimatorUtil.LoopPresetCategoryExists(selectedLoopPresetCategory.stringValue))
            {
                selectedLoopPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            }

            //preset name is empty or preset name does not exist in set category -> reset to default preset category and preset name
            if (string.IsNullOrEmpty(normalLoopPresetName.stringValue) ||
                !UIAnimatorUtil.LoopPresetExists(normalLoopPresetCategory.stringValue, normalLoopPresetName.stringValue))
            {
                normalLoopPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                normalLoopPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }
            if (string.IsNullOrEmpty(selectedLoopPresetName.stringValue) ||
                !UIAnimatorUtil.LoopPresetExists(selectedLoopPresetCategory.stringValue, selectedLoopPresetName.stringValue))
            {
                selectedLoopPresetCategory.stringValue = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
                selectedLoopPresetName.stringValue = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            }

            normalLoopPresetCategoryIndex = UIAnimatorUtil.LoopPresetCategories.IndexOf(normalLoopPresetCategory.stringValue);
            normalLoopPresetNameIndex = UIAnimatorUtil.GetLoopPresetNames(normalLoopPresetCategory.stringValue).IndexOf(normalLoopPresetName.stringValue);
            selectedLoopPresetCategoryIndex = UIAnimatorUtil.LoopPresetCategories.IndexOf(selectedLoopPresetCategory.stringValue);
            selectedLoopPresetNameIndex = UIAnimatorUtil.GetLoopPresetNames(selectedLoopPresetCategory.stringValue).IndexOf(selectedLoopPresetName.stringValue);
        }

        void RefreshNavigationData(bool forcedRefresh = false)
        {
            if (!UIManager.IsNavigationEnabled) { return; }
            if (DUI.UIElementsDatabase == null || forcedRefresh) { DUI.RefreshUIElementsDatabase(); }
            if (useOnPointerEnter.boolValue || forcedRefresh)
            {
                RefreshNavigationDataList(uiButton.onPointerEnterNavigation.show, onPointerEnterEditorNavigationData.showIndex);
                RefreshNavigationDataList(uiButton.onPointerEnterNavigation.hide, onPointerEnterEditorNavigationData.hideIndex);
            }
            if (useOnPointerExit.boolValue || forcedRefresh)
            {
                RefreshNavigationDataList(uiButton.onPointerExitNavigation.show, onPointerExitEditorNavigationData.showIndex);
                RefreshNavigationDataList(uiButton.onPointerExitNavigation.hide, onPointerExitEditorNavigationData.hideIndex);
            }
            if (useOnPointerDown.boolValue || forcedRefresh)
            {
                RefreshNavigationDataList(uiButton.onPointerDownNavigation.show, onPointerDownEditorNavigationData.showIndex);
                RefreshNavigationDataList(uiButton.onPointerDownNavigation.hide, onPointerDownEditorNavigationData.hideIndex);
            }
            if (useOnPointerUp.boolValue || forcedRefresh)
            {
                RefreshNavigationDataList(uiButton.onPointerUpNavigation.show, onPointerUpEditorNavigationData.showIndex);
                RefreshNavigationDataList(uiButton.onPointerUpNavigation.hide, onPointerUpEditorNavigationData.hideIndex);
            }
            if (useOnClickAnimations.boolValue || forcedRefresh)
            {
                RefreshNavigationDataList(uiButton.onClickNavigation.show, onClickEditorNavigationData.showIndex);
                RefreshNavigationDataList(uiButton.onClickNavigation.hide, onClickEditorNavigationData.hideIndex);
            }
            if (useOnDoubleClick.boolValue || forcedRefresh)
            {
                RefreshNavigationDataList(uiButton.onDoubleClickNavigation.show, onDoubleClickEditorNavigationData.showIndex);
                RefreshNavigationDataList(uiButton.onDoubleClickNavigation.hide, onDoubleClickEditorNavigationData.hideIndex);
            }
            if (useOnLongClick.boolValue || forcedRefresh)
            {
                RefreshNavigationDataList(uiButton.onLongClickNavigation.show, onLongClickEditorNavigationData.showIndex);
                RefreshNavigationDataList(uiButton.onLongClickNavigation.hide, onLongClickEditorNavigationData.hideIndex);
            }
        }
        void RefreshNavigationDataList(List<NavigationPointer> list, List<EditorNavigationPointer> listIndex)
        {
            listIndex.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                listIndex.Add(new EditorNavigationPointer(0, 0));
                if (list[i].category.Equals(DUI.CUSTOM_NAME))
                {
                    listIndex[i].categoryIndex = DUI.UIElementCategories.IndexOf(list[i].category);
                    listIndex[i].nameIndex = 0;
                    continue;
                }
                else if (DUI.UIElementCategoryExists(list[i].category))
                {
                    listIndex[i].categoryIndex = DUI.UIElementCategories.IndexOf(list[i].category);
                    if (DUI.UIElementNameExists(list[i].category, list[i].name) && !list[i].category.Equals(DUI.CUSTOM_NAME))
                    {
                        listIndex[i].nameIndex = DUI.UIElementsDatabase[list[i].category].IndexOf(list[i].name);
                        continue;
                    }
                    else if (list[i].category.Equals(DUI.CUSTOM_NAME))
                    {
                        listIndex[i].nameIndex = 0;
                        continue;
                    }
                }

                if (!DUI.UIElementsDatabase.ContainsKey(DUI.DEFAULT_CATEGORY_NAME) ||
                   !DUI.UIElementNameExists(DUI.DEFAULT_CATEGORY_NAME, DUI.DEFAULT_ELEMENT_NAME))
                {
                    DUI.RefreshUIElementsDatabase();
                }
                list[i].category = DUI.DEFAULT_CATEGORY_NAME;
                listIndex[i].categoryIndex = DUI.UIElementCategories.IndexOf(DUI.DEFAULT_CATEGORY_NAME);
                list[i].name = DUI.DEFAULT_ELEMENT_NAME;
                listIndex[i].nameIndex = DUI.UIElementsDatabase[list[i].category].IndexOf(DUI.DEFAULT_ELEMENT_NAME);
            }
        }
    }
}
