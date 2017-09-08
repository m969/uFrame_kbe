// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System;
using UnityEngine;

namespace DoozyUI
{
    [CreateAssetMenu]
    [Serializable]
    public class DUISettings : ScriptableObject
    {
        //Internal Settings
        public bool InternalSettings_ExecutedUpgrade = false;

        //Hierarchy Manager
        public bool HierarchyManager_Enabled = true;
        public bool HierarchyManager_PlaymakerEventDispatcher_ShowIcon = false;
        public bool HierarchyManager_UITrigger_ShowIcon = true;
        public bool HierarchyManager_UIManager_ShowIcon = true;
        public bool HierarchyManager_Soundy_ShowIcon = false;
        public bool HierarchyManager_UINotificationManager_ShowIcon = false;
        public bool HierarchyManager_OrientationManager_ShowIcon = true;
        public bool HierarchyManager_SceneLoader_ShowIcon = true;

        public bool HierarchyManager_UICanvas_Enabled { get { return HierarchyManager_UICanvas_ShowIcon || HierarchyManager_UICanvas_ShowCanvasName || HierarchyManager_UICanvas_ShowSortingLayerNameAndOrder; } }
        public bool HierarchyManager_UICanvas_ShowIcon = true;
        public bool HierarchyManager_UICanvas_ShowCanvasName = true;
        public bool HierarchyManager_UICanvas_ShowSortingLayerNameAndOrder = true;

        public bool HierarchyManager_UINotification_ShowIcon = true;

        public bool HierarchyManager_UIElement_Enabled { get { return HierarchyManager_UIElement_ShowIcon || HierarchyManager_UIElement_ShowElementCategory || HierarchyManager_UIElement_ShowElementName || HierarchyManager_UIElement_ShowSortingLayerNameAndOrder; } }
        public bool HierarchyManager_UIElement_ShowIcon = true;
        public bool HierarchyManager_UIElement_ShowElementCategory = false;
        public bool HierarchyManager_UIElement_ShowElementName = true;
        public bool HierarchyManager_UIElement_ShowSortingLayerNameAndOrder = true;

        public bool HierarchyManager_UIButton_Enabled { get { return HierarchyManager_UIButton_ShowIcon || HierarchyManager_UIButton_ShowButtonCategory || HierarchyManager_UIButton_ShowButtonName; } }
        public bool HierarchyManager_UIButton_ShowIcon = true;
        public bool HierarchyManager_UIButton_ShowButtonCategory = false;
        public bool HierarchyManager_UIButton_ShowButtonName = true;

        public bool HierarchyManager_UIEffect_Enabled { get { return HierarchyManager_UIEffect_ShowIcon || HierarchyManager_UIEffect_ShowSortingLayerNameAndOrder; } }
        public bool HierarchyManager_UIEffect_ShowIcon = true;
        public bool HierarchyManager_UIEffect_ShowSortingLayerNameAndOrder = true;


        //UIElement Settings
        public bool UIElement_Inspector_ShowButtonRenameGameObject = true;
        public string UIElement_Inspector_RenameGameObjectPrefix = "UIE - ";
        public string UIElement_Inspector_RenameGameObjectSuffix = "";

        public bool UIElement_LANDSCAPE = true;
        public bool UIElement_PORTRAIT = true;

        public bool UIElement_startHidden = false;
        public bool UIElement_animateAtStart = false;
        public bool UIElement_disableWhenHidden = false;

        public bool UIElement_useCustomStartAnchoredPosition = false;
        public Vector3 UIElement_customStartAnchoredPosition = Vector3.zero;

        public bool UIElement_executeLayoutFix = false;

        public string UIElement_inAnimationsPresetCategoryName = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIElement_inAnimationsPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIElement_loadInAnimationsPresetAtRuntime = false;

        public string UIElement_outAnimationsPresetCategoryName = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIElement_outAnimationsPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIElement_loadOutAnimationsPresetAtRuntime = false;

        public bool UIElement_Inspector_HideLoopAnimations = false;
        public string UIElement_loopAnimationsPresetCategoryName = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIElement_loopAnimationsPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIElement_loadLoopAnimationsPresetAtRuntime = false;


        //UIButton Settings
        public bool UIButton_Inspector_ShowButtonRenameGameObject = true;
        public string UIButton_Inspector_RenameGameObjectPrefix = "UIB - ";
        public string UIButton_Inspector_RenameGameObjectSuffix = "";

        public bool UIButton_allowMultipleClicks = true;
        public float UIButton_disableButtonInterval = UIButton.BETWEEN_CLICKS_DISABLE_INTERVAL;
        public bool UIButton_deselectButtonOnClick = true;

        public bool UIButton_Inspector_HideOnPointerEnter = false;
        public bool UIButton_useOnPointerEnter = false;
        public float UIButton_onPointerEnterDisableInterval = UIButton.ON_POINTER_ENTER_DISABLE_INTERVAL;
        public string UIButton_onPointerEnterSound = DUI.DEFAULT_SOUND_NAME;
        public bool UIButton_customOnPointerEnterSound = false;
        public string UIButton_onPointerEnterPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIButton_onPointerEnterPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIButton_loadOnPointerEnterPunchPresetAtRuntime = false;

        public bool UIButton_Inspector_HideOnPointerExit = false;
        public bool UIButton_useOnPointerExit = false;
        public float UIButton_onPointerExitDisableInterval = UIButton.ON_POINTER_EXIT_DISABLE_INTERVAL;
        public string UIButton_onPointerExitSound = DUI.DEFAULT_SOUND_NAME;
        public bool UIButton_customOnPointerExitSound = false;
        public string UIButton_onPointerExitPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIButton_onPointerExitPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIButton_loadOnPointerExitPunchPresetAtRuntime = false;

        public bool UIButton_Inspector_HideOnPointerDown = false;
        public bool UIButton_useOnPointerDown = false;
        public string UIButton_onPointerDownSound = DUI.DEFAULT_SOUND_NAME;
        public bool UIButton_customOnPointerDownSound = false;
        public string UIButton_onPointerDownPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIButton_onPointerDownPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIButton_loadOnPointerDownPunchPresetAtRuntime = false;

        public bool UIButton_Inspector_HideOnPointerUp = false;
        public bool UIButton_useOnPointerUp = false;
        public string UIButton_onPointerUpSound = DUI.DEFAULT_SOUND_NAME;
        public bool UIButton_customOnPointerUpSound = false;
        public string UIButton_onPointerUpPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIButton_onPointerUpPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIButton_loadOnPointerUpPunchPresetAtRuntime = false;

        public bool UIButton_Inspector_HideOnClick = false;
        public bool UIButton_useOnClickAnimations = true;
        public bool UIButton_waitForOnClickAnimation = true;
        public UIButton.SingleClickMode UIButton_singleClickMode = UIButton.SingleClickMode.Instant;
        public string UIButton_onClickSound = DUI.DEFAULT_SOUND_NAME;
        public bool UIButton_customOnClickSound = false;
        public string UIButton_onClickPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIButton_onClickPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIButton_loadOnClickPunchPresetAtRuntime = false;

        public bool UIButton_Inspector_HideOnDoubleClick = false;
        public bool UIButton_useOnDoubleClick = false;
        public bool UIButton_waitForOnDoubleClickAnimation = true;
        public float UIButton_doubleClickRegisterInterval = UIButton.DOUBLE_CLICK_REGISTER_INTERVAL;
        public string UIButton_onDoubleClickSound = DUI.DEFAULT_SOUND_NAME;
        public bool UIButton_customOnDoubleClickSound = false;
        public string UIButton_onDoubleClickPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIButton_onDoubleClickPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIButton_loadOnDoubleClickPunchPresetAtRuntime = false;

        public bool UIButton_Inspector_HideOnLongClick = false;
        public bool UIButton_useOnLongClick = false;
        public bool UIButton_waitForOnLongClickAnimation = true;
        public float UIButton_longClickRegisterInterval = UIButton.LONG_CLICK_REGISTER_INTERVAL;
        public string UIButton_onLongClickSound = DUI.DEFAULT_SOUND_NAME;
        public bool UIButton_customOnLongClickSound = false;
        public string UIButton_onLongClickPunchPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIButton_onLongClickPunchPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIButton_loadOnLongClickPunchPresetAtRuntime = false;

        public bool UIButton_Inspector_HideNormalLoop = false;
        public string UIButton_normalLoopPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIButton_normalLoopPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIButton_loadNormalLoopPresetAtRuntime = false;

        public bool UIButton_Inspector_HideSelectedLoop = false;
        public string UIButton_selectedLoopPresetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string UIButton_selectedLoopPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public bool UIButton_loadSelectedLoopPresetAtRuntime = false;

        public bool UIButton_addToNavigationHistory = false;


        //UIEffect Settings
        public bool UIEffect_Inspector_ShowButtonRenameGameObject = false;
        public string UIEffect_Inspector_RenameGameObjectPrefix = "UIEffect for ";
        public string UIEffect_Inspector_RenameGameObjectSuffix = "";

        public bool UIEffect_playOnAwake = false;
        public bool UIEffect_stopInstantly = false;

        public bool UIEffect_useCustomSortingLayerName = false;
        public string UIEffect_customSortingLayerName = UIEffect.DEFAULT_CUSTOM_SORTING_LAYER_NAME;

        public bool UIEffect_useCustomOrderInLayer = false;
        public int UIEffect_customOrderInLayer = UIEffect.DEFAULT_CUSTOM_ORDER_IN_LAYER;

        public UIEffect.EffectPosition UIEffect_effectPosition = UIEffect.EffectPosition.InFrontOfTarget;
        public int UIEffect_sortingOrderStep = UIEffect.DEFAULT_DEFAULT_SORTING_ORDER_STEP;
    }
}
