// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DoozyUI
{
    [AddComponentMenu(DUI.COMPONENT_MENU_UIELEMENT, DUI.MENU_PRIORITY_UIELEMENT)]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [DisallowMultipleComponent]
    public class UIElement : MonoBehaviour
    {
        #region Context Menu
#if UNITY_EDITOR
        [UnityEditor.MenuItem(DUI.GAMEOBJECT_MENU_UIELEMENT, false, DUI.MENU_PRIORITY_UIELEMENT)]
        static void CreateElement(UnityEditor.MenuCommand menuCommand)
        {
            UICanvas targetCanvas = null;
            GameObject selectedGO = menuCommand.context as GameObject;
            if (selectedGO != null) //check that a gameObject is selected
            {
                targetCanvas = selectedGO.GetComponent<UICanvas>(); //check if the selected gameObject is an UICanvas, otherwise get the root and check
                if (targetCanvas == null)
                {
                    targetCanvas = selectedGO.transform.root.GetComponent<UICanvas>(); //check if there is an UICanvas on the root of the selected gameOhject
                }
            }
            if (targetCanvas == null) //because we did not find any UICanvas on the selected gameObject (or on it's root transform), we get the MasterCanvas; if the MasterCanvas does not exist, it will be created automatically by the system
            {
                targetCanvas = UIManager.GetMasterCanvas();
            }
            GameObject go = new GameObject("New UIElement", typeof(RectTransform), typeof(UIElement));
            UnityEditor.GameObjectUtility.SetParentAndAlign(go, targetCanvas.gameObject);
            UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            go.GetComponent<UIElement>().Reset();
            UnityEditor.Selection.activeObject = go;
        }
#endif
        #endregion

        #region Obsolete
        [Serializable] [Obsolete] public class ElementName { public string elementName = DUI.DEFAULT_ELEMENT_NAME; }
        [Obsolete]
        public UIAnimator.InitialData InitialData
        {
            get
            {
                UIAnimator.InitialData initialData = new UIAnimator.InitialData();
                initialData.startPosition = useCustomStartAnchoredPosition ? customStartAnchoredPosition : startPosition;
                initialData.startRotation = startRotation;
                initialData.startScale = startScale;
                initialData.startAlpha = 1f;
                initialData.soundOn = UIManager.isSoundOn;

                return initialData;
            }
        }

        [Obsolete] public UIAnimator.MoveIn moveIn;
        [Obsolete] public UIAnimator.RotationIn rotationIn;
        [Obsolete] public UIAnimator.ScaleIn scaleIn;
        [Obsolete] public UIAnimator.FadeIn fadeIn;
        [Obsolete] public UIAnimator.MoveLoop moveLoop;
        [Obsolete] public UIAnimator.RotationLoop rotationLoop;
        [Obsolete] public UIAnimator.ScaleLoop scaleLoop;
        [Obsolete] public UIAnimator.FadeLoop fadeLoop;
        [Obsolete] public UIAnimator.MoveOut moveOut;
        [Obsolete] public UIAnimator.RotationOut rotationOut;
        [Obsolete] public UIAnimator.ScaleOut scaleOut;
        [Obsolete] public UIAnimator.FadeOut fadeOut;
        #endregion

        /// <summary>
        /// This is an extra id tag given to the tweener in order to locate the proper tween that manages the loop animations.
        /// </summary>
        /// 
        public const string LOOP_ANIMATIONS_ID = "UIElementLoopAnimations";

        /// <summary>
        /// The category this element name belongs to. The category is important when showing or hiding an UIElement as it is taken into account.
        /// </summary>
        public string elementCategory = DUI.DEFAULT_CATEGORY_NAME;
        /// <summary>
        /// The name of this element. The name is important when showing or hiding an UIElement as it is taken into account.
        /// </summary>
        public string elementName = DUI.DEFAULT_ELEMENT_NAME;

        /// <summary>
        /// Use this UIElement for LANDSCAPE orientation. Default is true.
        /// <para>If Orientation Manager is disabled, this setting does nothing.</para>
        /// </summary>
        public bool LANDSCAPE = true;
        /// <summary>
        /// Use this UIElement for PORTRAIT orientation. Default is true.
        /// <para>If Orientation Manager is disabled, this setting does nothing.</para>
        /// </summary>
        public bool PORTRAIT = true;

        /// <summary>
        /// Should this UIElement come from or go to a set custom position every time an In or Out animation is played? Default is set to false.
        /// </summary>
        public bool useCustomStartAnchoredPosition = false;
        /// <summary>
        /// The custom anchored position that this UIElement comes from or goes to when an In or Out animation is played. You can use this in code to cusomize on the fly this positon.
        /// </summary>
        public Vector3 customStartAnchoredPosition = Vector3.zero;

        /// <summary>
        /// Hide the UIElement at runtime at start. Initiates an instant Hide. Default is set to false.
        /// </summary>
        public bool startHidden = false;
        /// <summary>
        /// Animate the UIElement at runtime at start. Initiates a Show, thus playing an In animation. Default is set to false.
        /// </summary>
        public bool animateAtStart = false;
        /// <summary>
        /// Disables this UIElement when it is not visible (it is hidden) by setting it's active state to false.
        /// <para>Use this only if you have scripts that you need to disable. Otherwise you don't need it as the system handles the drawcalls in an effecient manner.</para>
        /// </summary>
        public bool disableWhenHidden = false;
        /// <summary>
        /// This fixes a very strange issue inside Unity. When setting a VerticalLayoutGroup or a HorizontalLayoutGroup, the Image bounds get moved (the image appeares in a different place).
        /// <para>If you have this issue, just set this to true. Default is set to false.</para>
        /// <para>If you are curious about what this does, look at the ExecuteLayoutFix method.</para>
        /// </summary>
        public bool executeLayoutFix = false;

        /// <summary>
        /// The button that gets selected when this UIElement gets shown; if null then no button will get auto selected. Default is set to null.
        /// </summary>
        public GameObject selectedButton = null;

        /// <summary>
        /// If this UIElement is linked to an UINotification then it will have an auto-generated element name. Do not change this value yourself.
        /// </summary>
        public bool linkedToNotification = false;
        /// <summary>
        /// Used by the UINotification. If this element is linked to a notification, then the notification should handle it's registration process in order to use an auto generated name. Do not change this value yourself.
        /// </summary>
        public bool autoRegister = true;
        /// <summary>
        /// Keeps track if this UIElement is visible or not. Do not change this value yourself.
        /// </summary>
        public bool isVisible = true;
        /// <summary>
        /// Internal variable that is set to true if this UIElement has other child UIElements. This is uesd by the system in order to handle special Show and Hide use case scenarios.
        /// </summary>
        private bool containsChildUIElements = false;

        #region IN ANIMATIONS
        /// <summary>
        /// In Animation Settings
        /// </summary>
        public Anim inAnimations = new Anim(Anim.AnimationType.In);

        /// <summary>
        /// UnityEvent invoked when In animations start.
        /// </summary>
        public UnityEvent OnInAnimationsStart = new UnityEvent();
        /// <summary>
        /// UnityEvent invoked when In animations finished.
        /// </summary>
        public UnityEvent OnInAnimationsFinish = new UnityEvent();

        /// <summary>
        /// Out Animations Preset Category Name
        /// </summary>
        public string inAnimationsPresetCategoryName = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        /// <summary>
        /// Out Animations Preset Name
        /// </summary>
        public string inAnimationsPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        /// <summary>
        /// Should the system load, at runtime, the Animation Preset with the set Preset Category and Preset Name. This overrides any values set in the inspector.
        /// </summary>
        public bool loadInAnimationsPresetAtRuntime = false;

        /// <summary>
        /// The sound name of the sound that gets played when the in animations start.
        /// </summary>
        public string inAnimationsSoundAtStart = DUI.DEFAULT_SOUND_NAME;
        /// <summary>
        /// Used by the custom inspector to allow you to type a sound name instead of selecting it from the UISounds Database.
        /// </summary>
        public bool customInAnimationsSoundAtStart = false;
        /// <summary>
        /// The sound name of the sound that gets played when the in animations finished.
        /// </summary>
        public string inAnimationsSoundAtFinish = DUI.DEFAULT_SOUND_NAME;
        /// <summary>
        /// Used by the custom inspector to allow you to type a sound name instead of selecting it from the UISounds Database.
        /// </summary>
        public bool customInAnimationsSoundAtFinish = false;
        #endregion
        #region OUT ANIMATIONS
        /// <summary>
        /// Out Animation Settings
        /// </summary>
        public Anim outAnimations = new Anim(Anim.AnimationType.Out);

        /// <summary>
        /// UnityEvent invoked when Out animations start.
        /// </summary>
        public UnityEvent OnOutAnimationsStart = new UnityEvent();
        /// <summary>
        /// UnityEvent invoked when Out animations finished.
        /// </summary>
        public UnityEvent OnOutAnimationsFinish = new UnityEvent();

        /// <summary>
        /// Out Animations Preset Category Name
        /// </summary>
        public string outAnimationsPresetCategoryName = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        /// <summary>
        /// Out Animations Preset Name
        /// </summary>
        public string outAnimationsPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        /// <summary>
        /// Should the system load, at runtime, the Animation Preset with the set Preset Category and Preset Name. This overrides any values set in the inspector.
        /// </summary>
        public bool loadOutAnimationsPresetAtRuntime = false;

        /// <summary>
        /// The sound name of the sound that gets played when the out animations start.
        /// </summary>
        public string outAnimationsSoundAtStart = DUI.DEFAULT_SOUND_NAME;
        /// <summary>
        /// Used by the custom inspector to allow you to type a sound name instead of selecting it from the UISounds Database.
        /// </summary>
        public bool customOutAnimationsSoundAtStart = false;
        /// <summary>
        /// The sound name of the sound that gets played when the out animations finished.
        /// </summary>
        public string outAnimationsSoundAtFinish = DUI.DEFAULT_SOUND_NAME;
        /// <summary>
        /// Used by the custom inspector to allow you to type a sound name instead of selecting it from the UISounds Database.
        /// </summary>
        public bool customOutAnimationsSoundAtFinish = false;
        #endregion
        #region LOOP ANIMATIONS
        /// <summary>
        /// Loop Animation Settings
        /// </summary>
        public Loop loopAnimations = new Loop();
        private bool loopAnimationsArePlaying = false;

        /// <summary>
        /// Loop Animations Preset Category Name
        /// </summary>
        public string loopAnimationsPresetCategoryName = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        /// <summary>
        /// Loop Animations Preset Name
        /// </summary>
        public string loopAnimationsPresetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        /// <summary>
        /// Should the system load, at runtime, the Loop Preset with the set Preset Category and Preset Name. This overrides any values set in the inspector.
        /// </summary>
        public bool loadLoopAnimationsPresetAtRuntime = false;
        #endregion

        /// <summary>
        /// Internal variable that holds a reference to the RectTransform component.
        /// </summary>
        private RectTransform m_rectTransform;
        /// <summary>
        /// Returns the RectTransform component.
        /// </summary>
        public RectTransform RectTransform { get { if (m_rectTransform == null) { m_rectTransform = GetComponent<RectTransform>() == null ? gameObject.AddComponent<RectTransform>() : GetComponent<RectTransform>(); } return m_rectTransform; } }
        /// <summary>
        /// Internal variable that holds a reference to the Canvas component.
        /// </summary>
        private Canvas m_canvas;
        /// <summary>
        /// Returns the Canvas component.
        /// </summary>
        public Canvas Canvas { get { if (m_canvas == null) { m_canvas = GetComponent<Canvas>() == null ? gameObject.AddComponent<Canvas>() : GetComponent<Canvas>(); } return m_canvas; } }
        /// <summary>
        /// Internal variable that holds a reference to the GraphicRaycaster component.
        /// </summary>
        private GraphicRaycaster m_graphicRaycaster;
        /// <summary>
        /// Returns the GraphicRaycaster component.
        /// </summary>
        public GraphicRaycaster GraphicRaycaster { get { if (m_graphicRaycaster == null) { m_graphicRaycaster = GetComponent<GraphicRaycaster>() == null ? gameObject.AddComponent<GraphicRaycaster>() : GetComponent<GraphicRaycaster>(); } return m_graphicRaycaster; } }
        /// <summary>
        /// Internal variable that holds a reference to the CanvasGroup component.
        /// </summary>
        private CanvasGroup m_canvasGroup;
        /// <summary>
        /// Returns the CanvasGroup component.
        /// </summary>
        public CanvasGroup CanvasGroup { get { if (m_canvasGroup == null) { m_canvasGroup = GetComponent<CanvasGroup>() == null ? gameObject.AddComponent<CanvasGroup>() : GetComponent<CanvasGroup>(); } return m_canvasGroup; } }

        /// <summary>
        /// Internal variable that holds the start RectTransform.anchoredPosition3D.
        /// </summary>
        private Vector3 startPosition;
        /// <summary>
        /// Internal variable that holds the start RectTransform.localEulerAngles
        /// </summary>
        private Vector3 startRotation;
        /// <summary>
        /// Internal variable that holds the start RectTransform.localScale
        /// </summary>
        private Vector3 startScale;
        /// <summary>
        /// Internal variable that holds the start alpha. It does that by checking if a CanvasGroup component is attached (holding the alpha value) or it just rememebers 1 (as in 100% visibility)
        /// </summary>
        private float startAlpha;

        /// <summary>
        /// Internal variable used when disableWhenHidden is set to true. After the element has been hidden (the out animations finished), the system waits for an additional disableTimeBuffer before it sets this gameObject's active state to false. This is a failsafe mesure and fixes a small bug on iOS.
        /// </summary>
        private float disableTimeBuffer = 0.05f;

        /// <summary>
        /// Internal variable that holds a reference to the coroutine that shows the element.
        /// </summary>
        private Coroutine cShow;
        /// <summary>
        /// Internal variable that holds a reference to the coroutine that hides the element.
        /// </summary>
        private Coroutine cHide;
        /// <summary>
        /// Internal variable that holds a reference to the coroutine that disables button clicks when in transit (an In or Out animation is running).
        /// </summary>
        private Coroutine cDisableButtonClicks;

        /// <summary>
        /// Internal variable that is set to true whenever either an In or an Out animation is running.
        /// </summary>
        private bool inTransition = false;

        /// <summary>
        /// Internal array that holds the references to all the child Canvases.
        /// </summary>
        private Canvas[] childCanvas;
        /// <summary>
        /// Internal array the holds the references to all the child UIButtons.
        /// </summary>
        private UIButton[] childButtons;

        /// <summary>
        /// Retruns true if at least one In animation is enabled. This means that if either move or rotate or scale or fade are enabled it will return true and false otherwise.
        /// </summary>
        public bool InAnimationsEnabled { get { return inAnimations.Enabled; } }
        /// <summary>
        /// Retruns true if at least one Loop animation is enabled. This means that if either move or rotate or scale or fade are enabled it will return true and false otherwise.
        /// </summary>
        public bool LoopAnimationsEnabled { get { return loopAnimations.Enabled; } }
        /// <summary>
        /// Retruns true if at least one Out animation is enabled. This means that if either move or rotate or scale or fade are enabled it will return true and false otherwise.
        /// </summary>
        public bool OutAnimationsEnabled { get { return outAnimations.Enabled; } }

        public void Reset()
        {
            RectTransform.localScale = Vector3.one;
            RectTransform.anchorMin = Vector2.zero;
            RectTransform.anchorMax = Vector2.one;
            RectTransform.sizeDelta = Vector2.zero;
            RectTransform.pivot = new Vector2(0.5f, 0.5f);
            Canvas.overrideSorting = true;

            if (DUI.DUISettings == null) { return; }
            LANDSCAPE = DUI.DUISettings.UIElement_LANDSCAPE;
            PORTRAIT = DUI.DUISettings.UIElement_PORTRAIT;

            startHidden = DUI.DUISettings.UIElement_startHidden;
            animateAtStart = DUI.DUISettings.UIElement_animateAtStart;
            disableWhenHidden = DUI.DUISettings.UIElement_disableWhenHidden;

            useCustomStartAnchoredPosition = DUI.DUISettings.UIElement_useCustomStartAnchoredPosition;
            customStartAnchoredPosition = DUI.DUISettings.UIElement_customStartAnchoredPosition;

            executeLayoutFix = DUI.DUISettings.UIElement_executeLayoutFix;

            inAnimationsPresetCategoryName = DUI.DUISettings.UIElement_inAnimationsPresetCategoryName;
            inAnimationsPresetName = DUI.DUISettings.UIElement_inAnimationsPresetName;
            loadInAnimationsPresetAtRuntime = DUI.DUISettings.UIElement_loadInAnimationsPresetAtRuntime;

            outAnimationsPresetCategoryName = DUI.DUISettings.UIElement_outAnimationsPresetCategoryName;
            outAnimationsPresetName = DUI.DUISettings.UIElement_outAnimationsPresetName;
            loadOutAnimationsPresetAtRuntime = DUI.DUISettings.UIElement_loadOutAnimationsPresetAtRuntime;

            loopAnimationsPresetCategoryName = DUI.DUISettings.UIElement_loopAnimationsPresetCategoryName;
            loopAnimationsPresetName = DUI.DUISettings.UIElement_loopAnimationsPresetName;
            loadLoopAnimationsPresetAtRuntime = DUI.DUISettings.UIElement_loadLoopAnimationsPresetAtRuntime;
        }

        void Awake()
        {
            m_rectTransform = RectTransform;
            m_canvas = Canvas;
            m_canvasGroup = CanvasGroup;
            m_graphicRaycaster = GraphicRaycaster;

            startPosition = useCustomStartAnchoredPosition ? customStartAnchoredPosition : RectTransform.anchoredPosition3D;
            startRotation = RectTransform.localEulerAngles;
            startScale = RectTransform.localScale;
            startAlpha = CanvasGroup == null ? 1 : CanvasGroup.alpha;

            childCanvas = GetComponentsInChildren<Canvas>();
            childButtons = GetComponentsInChildren<UIButton>();

            LoadRuntimeInAnimationsPreset();
            LoadRuntimeOutAnimationsPreset();
        }

        void OnEnable()
        {
            ExecuteLayoutFix();
            MoveToCustomStartPosition();
        }

        private void Start()
        {
            if (autoRegister) { RegisterToUIManager(); }

            OnInAnimationsStart.AddListener(InAnimationsStart);
            OnInAnimationsFinish.AddListener(InAnimationsFinish);
            OnOutAnimationsStart.AddListener(OutAnimationsStart);
            OnOutAnimationsFinish.AddListener(OutAnimationsFinish);

            SetupElement();
            InitLoopAnimations();
        }

        void OnDisable()
        {
            if (inTransition && UIManager.Instance != null && UIManager.Instance.autoDisableButtonClicks) { EnableButtonClicks(); }
        }

        void OnDestroy()
        {
            UnregisterFromUIManager();

            OnInAnimationsStart.RemoveListener(InAnimationsStart);
            OnInAnimationsFinish.RemoveListener(InAnimationsFinish);
            OnOutAnimationsStart.RemoveListener(OutAnimationsStart);
            OnOutAnimationsFinish.RemoveListener(OutAnimationsFinish);
        }

        /// <summary>
        /// Moves the UIElement to the set custom start position.
        /// </summary>
        void MoveToCustomStartPosition()
        {
            if (useCustomStartAnchoredPosition) { RectTransform.anchoredPosition3D = customStartAnchoredPosition; }
        }

        #region RegisterToUIManager / UnregisterFromUIManager
        /// <summary>
        /// Registers this UIElement to the UIManager.
        /// </summary>
        public void RegisterToUIManager()
        {
            if (elementCategory.Equals(DUI.CUSTOM_NAME)) { elementCategory = DUI.DEFAULT_CATEGORY_NAME; } //at runtime the category is reset to the DEFAULT_CATEGORY_NAME so that the system will be able to show/hide this UIElements

            if (UIManager.ElementDatabase.ContainsKey(elementName))
            {
                if (UIManager.ElementDatabase[elementName] == null) { UIManager.ElementDatabase[elementName] = new List<UIElement>(); }
                if (UIManager.ElementDatabase[elementName].Contains(this)) { return; }
                UIManager.ElementDatabase[elementName].Add(this);
            }
            else
            {
                UIManager.ElementDatabase.Add(elementName, new List<UIElement>() { this });
            }
        }
        /// <summary>
        /// Unregisters this UIElement from the UIManager.
        /// </summary>
        public void UnregisterFromUIManager()
        {
            if (UIManager.ElementDatabase == null) { return; }
            if (UIManager.ElementDatabase.ContainsKey(elementName))
            {
                UIManager.ElementDatabase[elementName].Remove(this);
                if (UIManager.ElementDatabase[elementName].Count == 0) { UIManager.ElementDatabase.Remove(elementName); }
            }
        }
        #endregion

        /// <summary>
        /// Executes the inital setup for this UIElement.
        /// </summary>
        void SetupElement()
        {
            if (GetComponentsInChildren<UIElement>().Length > 1)
            {
                containsChildUIElements = true;
            }

            if (animateAtStart)
            {
                if (linkedToNotification)
                {
                    Hide(true, false);
                    Show(false);
                }
                else
                {
                    if (UIManager.useOrientationManager)
                    {
                        if (UIManager.currentOrientation == UIManager.Orientation.Unknown)
                        {
                            StartCoroutine(GetOrientation());
                        }
                        else
                        {
                            if (LANDSCAPE && UIManager.currentOrientation == UIManager.Orientation.Landscape)
                            {
                                UIManager.HideUiElement(elementName, elementCategory, true);
                                UIManager.ShowUiElement(elementName, elementCategory, false);
                                if (disableWhenHidden && containsChildUIElements) StartCoroutine(TriggerShowInTheNextFrame(false));
                            }
                            else if (PORTRAIT && UIManager.currentOrientation == UIManager.Orientation.Portrait)
                            {
                                UIManager.HideUiElement(elementName, elementCategory, true);
                                UIManager.ShowUiElement(elementName, elementCategory, false);
                                if (disableWhenHidden && containsChildUIElements) StartCoroutine(TriggerShowInTheNextFrame(false));
                            }
                            else
                            {
                                Hide(true, disableWhenHidden);
                            }
                        }
                    }
                    else
                    {
                        UIManager.HideUiElement(elementName, elementCategory, true);
                        UIManager.ShowUiElement(elementName, elementCategory, false);
                        if (disableWhenHidden && containsChildUIElements) StartCoroutine(TriggerShowInTheNextFrame(false));
                    }
                }
            }
            else if (startHidden)
            {
                UIManager.HideUiElement(elementName, elementCategory, true);
            }
            else
            {

            }
        }

        /// <summary>
        /// Resets the UIElement's RectTransfrom to the start values.
        /// </summary>
        void ResetRectTransform()
        {
            UIAnimator.ResetTarget(RectTransform, startPosition, startRotation, startScale, startAlpha);
        }

        #region Show Methods (IN Animations)
        /// <summary>
        /// Loads the In Animations Preset that is set to load at runtime.
        /// </summary>
        void LoadRuntimeInAnimationsPreset()
        {
            if (loadInAnimationsPresetAtRuntime)
            {
                Anim presetAnimation = UIAnimatorUtil.GetInAnim(inAnimationsPresetCategoryName, inAnimationsPresetName);
                if (presetAnimation != null) { inAnimations = presetAnimation.Copy(); }
            }
        }

        /// <summary>
        /// Special reset for the UIElement's RectTransfrom that is executed before every Show.
        /// </summary>
        void ResetBeforeShow(bool resetPosition, bool resetAlpha)
        {
            if (resetPosition)
            {
                RectTransform.anchoredPosition3D = useCustomStartAnchoredPosition ? customStartAnchoredPosition : startPosition;
            }
            RectTransform.eulerAngles = startRotation;
            RectTransform.localScale = startScale;
            if (resetAlpha)
            {
                if (CanvasGroup == null) { return; }
                CanvasGroup.alpha = 1f;
            }
        }

        /// <summary>
        /// Shows the element.
        /// </summary>
        /// <param name="instantAction">If set to <c>true</c> it will execute the animations in 0 seconds and with 0 delay</param>
        public void Show(bool instantAction)
        {
            if (cHide != null)
            {
                isVisible = false;
                StopCoroutine(cHide);
                cHide = null;
            }

            if (!InAnimationsEnabled)
            {
                Debug.LogWarning("[DoozyUI] [" + name + "] You are trying to SHOW the " + elementName + " UIElement, but you didn't enable any IN animations. To fix this warning you should enable at least one IN animation.");
                return;
            }

            if (isVisible == false)
            {
                TriggerInAnimationsEvents();
                UIAnimator.StopAnimations(RectTransform, Anim.AnimationType.Out);
                cShow = StartCoroutine(iShow(instantAction));
                isVisible = true;
                if (instantAction == false) { DisableButtonClicks(inAnimations.TotalDuration); }
                if (disableWhenHidden && containsChildUIElements) StartCoroutine(TriggerShowInTheNextFrame(instantAction));
                ExecuteLayoutFix();
            }
        }
        /// <summary>
        /// Executes the UIElement Show command in realtime.
        /// </summary>
        IEnumerator iShow(bool instantAction)
        {
            yield return null;
            if (loopAnimationsArePlaying) { UIAnimator.StopLoops(RectTransform, LOOP_ANIMATIONS_ID); }
            ToggleCanvasAndGraphicRaycaster(true);
            ResetBeforeShow(!inAnimations.move.enabled, !inAnimations.fade.enabled);
            UIAnimator.Move(RectTransform, useCustomStartAnchoredPosition ? customStartAnchoredPosition : startPosition, inAnimations, StartMoveIn, FinishMoveIn, instantAction, false);
            UIAnimator.Rotate(RectTransform, startRotation, inAnimations, StartRotateIn, FinishRotateIn, instantAction, false);
            UIAnimator.Scale(RectTransform, startScale, inAnimations, StartScaleIn, FinishScaleIn, instantAction, false);
            UIAnimator.Fade(RectTransform, startAlpha, inAnimations, StartFadeIn, FinishFadeIn, instantAction, false);
            StartCoroutine(iSetSelectedGameObject());
            if (inAnimations.TotalDuration >= 0 && !instantAction)
            {
                yield return new WaitForSecondsRealtime(inAnimations.TotalDuration);
            }
            if (childButtons != null && childButtons.Length > 0)
            {
                for (int i = 0; i < childButtons.Length; i++)
                {
                    childButtons[i].ResetAnimations();
                }
            }
            ResetRectTransform();
            PlayLoopAnimations();
            cShow = null;
        }

        void StartMoveIn() { }
        void FinishMoveIn() { }
        void StartRotateIn() { }
        void FinishRotateIn() { }
        void StartScaleIn() { }
        void FinishScaleIn() { }
        void StartFadeIn() { }
        void FinishFadeIn() { }

        /// <summary>
        /// Executes the selection of the selectedButton after Show finished.
        /// </summary>
        /// <returns></returns>
        IEnumerator iSetSelectedGameObject()
        {
            yield return null;
            if (EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(selectedButton);
            }
        }
        #endregion

        #region Hide Methods (OUT Animations)
        /// <summary>
        /// Loads the Out Animations Preset that is set to load at runtime.
        /// </summary>
        void LoadRuntimeOutAnimationsPreset()
        {
            if (loadOutAnimationsPresetAtRuntime)
            {
                Anim presetAnimation = UIAnimatorUtil.GetOutAnim(outAnimationsPresetCategoryName, outAnimationsPresetName);
                if (presetAnimation != null) { outAnimations = presetAnimation.Copy(); }
            }
        }

        /// <summary>
        /// Hides the element.
        /// </summary>
        /// <param name="instantAction">If set to <c>true</c> it will execute the animations in 0 seconds and with 0 delay</param>
        public void Hide(bool instantAction)
        {
            Hide(instantAction, disableWhenHidden);
        }
        /// <summary>
        /// Hides the element.
        /// </summary>
        /// <param name="instantAction">If set to <c>true</c> it will execute the animations in 0 seconds and with 0 delay</param>
        public void Hide(bool instantAction, bool shouldDisable)
        {
            if (cShow != null)
            {
                isVisible = true;
                StopCoroutine(cShow);
                cShow = null;
            }

            if (!OutAnimationsEnabled)
            {
                Debug.LogWarning("[DoozyUI] [" + name + "] You are trying to HIDE the " + elementName + " UIElement, but you didn't enable any OUT animations. To fix this warning you should enable at least one OUT animation.");
                return;
            }

            if (isVisible)
            {
                if (!instantAction) { TriggerOutAnimationsEvents(); } //we do this check so that the events are not triggered onEnable when we have startHidden set as true
                UIAnimator.StopAnimations(RectTransform, Anim.AnimationType.In);
                cHide = StartCoroutine(iHide(instantAction, shouldDisable));
                isVisible = false;
                if (!instantAction) { DisableButtonClicks(outAnimations.TotalDuration); }
            }
        }
        /// <summary>
        /// Executes the UIElement Hide command in realtime.
        /// </summary>
        IEnumerator iHide(bool instantAction, bool shouldDisable = true)
        {
            float start = Time.realtimeSinceStartup;
            UIAnimator.StopLoops(RectTransform, LOOP_ANIMATIONS_ID);
            UIAnimator.Move(RectTransform, useCustomStartAnchoredPosition ? customStartAnchoredPosition : startPosition, outAnimations, StartMoveOut, FinishMoveOut, instantAction, false);
            UIAnimator.Rotate(RectTransform, startRotation, outAnimations, StartRotateOut, FinishRotateOut, instantAction, false);
            UIAnimator.Scale(RectTransform, startScale, outAnimations, StartScaleOut, FinishScaleOut, instantAction, false);
            UIAnimator.Fade(RectTransform, startAlpha, outAnimations, StartFadeOut, FinishFadeOut, instantAction, false);


            if (shouldDisable)
            {
                while (Time.realtimeSinceStartup < start + disableTimeBuffer)
                {
                    yield return null;
                }
                if (!instantAction)
                {
                    while (Time.realtimeSinceStartup < start + outAnimations.TotalDuration + disableTimeBuffer)
                    {
                        yield return null;
                    }
                }
                ToggleCanvasAndGraphicRaycaster(false);
                gameObject.SetActive(false);
            }
            else
            {
                if (!instantAction)
                {
                    while (Time.realtimeSinceStartup < start + outAnimations.TotalDuration + disableTimeBuffer)
                    {
                        yield return null;
                    }
                }
                ToggleCanvasAndGraphicRaycaster(false);
            }
            cHide = null;
        }

        void StartMoveOut() { }
        void FinishMoveOut() { }
        void StartRotateOut() { }
        void FinishRotateOut() { }
        void StartScaleOut() { }
        void FinishScaleOut() { }
        void StartFadeOut() { }
        void FinishFadeOut() { }
        #endregion                                                                               

        #region Loop Methods (LOOP Animations)
        /// <summary>
        /// Loads the Loop Animations Preset that is set to load at runtime and performs the inital setup of the loop animations.
        /// </summary>
        /// <param name="forced"></param>
        void InitLoopAnimations(bool forced = false)
        {
            if (loopAnimationsArePlaying) { return; }
            if (loadLoopAnimationsPresetAtRuntime)
            {
                Loop presetLoop = UIAnimatorUtil.GetLoop(loopAnimationsPresetCategoryName, loopAnimationsPresetName);
                if (presetLoop != null) { loopAnimations = presetLoop.Copy(); }
            }
            if (loopAnimations == null || !loopAnimations.Enabled) { return; }
            ResetRectTransform();
            UIAnimator.SetupLoops(RectTransform, startPosition, startRotation, startScale, startAlpha, loopAnimations,
                     null, null,
                     null, null,
                     null, null,
                     null, null,
                     LOOP_ANIMATIONS_ID, true, forced);
            if (loopAnimations.autoStart) { loopAnimationsArePlaying = true; }
        }
        /// <summary>
        /// Plays the loop animations.
        /// </summary>
        void PlayLoopAnimations()
        {
            UIAnimator.SetupLoops(RectTransform, startPosition, startRotation, startScale, startAlpha, loopAnimations,
                    null, null,
                    null, null,
                    null, null,
                    null, null,
                    LOOP_ANIMATIONS_ID, true, false);
            UIAnimator.PlayLoops(RectTransform, LOOP_ANIMATIONS_ID);
            loopAnimationsArePlaying = true;
        }
        /// <summary>
        /// Stops playing the loop animations and then resets the RectTransform to the start values.
        /// </summary>
        void StopLoopAnimations()
        {
            if (!loopAnimationsArePlaying) { return; }
            UIAnimator.StopLoops(RectTransform, LOOP_ANIMATIONS_ID);
            loopAnimationsArePlaying = false;
            ResetRectTransform();
        }
        #endregion

        #region Events
        /// <summary>
        /// Helper method that invokes an UnityEvent after a set delay. This happens in realtime.
        /// </summary>
        private Coroutine InvokeEvent(UnityEvent @event, float delay)
        {
            return StartCoroutine(InvokeEventAfterDelay(@event, delay));
        }
        /// <summary>
        /// Executes invoke for the UnityEvent after the set delay. This happens in realtime.
        /// </summary>
        /// <param name="event"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        IEnumerator InvokeEventAfterDelay(UnityEvent @event, float delay)
        {
            if (delay >= 0)
            {
                yield return new WaitForSecondsRealtime(delay);
                if (@event != null) { @event.Invoke(); }
            }
        }

        /// <summary>
        /// Internal method that invokes the In Animations UnityEvents whenever an In Animations starts.
        /// </summary>
        private void TriggerInAnimationsEvents()
        {
            InvokeEvent(OnInAnimationsStart, inAnimations.StartDelay);
            InvokeEvent(OnInAnimationsFinish, inAnimations.TotalDuration);
        }
        /// <summary>
        /// Internal method that invokes the Out Animations UnityEvents whenever an Out Animations starts.
        /// </summary>
        private void TriggerOutAnimationsEvents()
        {
            InvokeEvent(OnOutAnimationsStart, outAnimations.StartDelay);
            InvokeEvent(OnOutAnimationsFinish, outAnimations.TotalDuration);
        }

        #endregion

        void InAnimationsStart() { UIManager.PlaySound(inAnimationsSoundAtStart); }
        void InAnimationsFinish() { UIManager.PlaySound(inAnimationsSoundAtFinish); }
        void OutAnimationsStart() { UIManager.PlaySound(outAnimationsSoundAtStart); }
        void OutAnimationsFinish() { UIManager.PlaySound(outAnimationsSoundAtFinish); }

        /// <summary>
        /// Internal method that enables button clicks by calling UIManager.EnableButtonClicks() at proper times.
        /// </summary>
        void EnableButtonClicks()
        {
            if (!UIManager.Instance.autoDisableButtonClicks) { return; }

            if (inTransition)
            {
                inTransition = false;
                UIManager.EnableButtonClicks();
            }

            if (cDisableButtonClicks != null)
            {
                StopCoroutine(cDisableButtonClicks);
                cDisableButtonClicks = null;
            }
        }

        /// <summary>
        /// Internal method that disables the button clicks while an In or an Out animations is running (is in transition).
        /// </summary>
        void DisableButtonClicks(float time)
        {
            if (!UIManager.Instance.autoDisableButtonClicks) { return; }

            EnableButtonClicks();
            cDisableButtonClicks = StartCoroutine(DisableButtonClicksForTime(time));
        }
        /// <summary>
        /// Executes the disabling of the button clicks in realtime.
        /// </summary>
        IEnumerator DisableButtonClicksForTime(float delay)
        {
            UIManager.DisableButtonClicks();
            inTransition = true;
            yield return new WaitForSecondsRealtime(delay);
            inTransition = false;
            UIManager.EnableButtonClicks();
            cDisableButtonClicks = null;
        }

        /// <summary>
        /// Internal method that is used to disable the Canvas and the GraphicRaycaster when the UIElement is hidden and to enable them when the UIElement is visible. This manages the draw calls without setting the gameObject's active state to false.
        /// </summary>
        /// <param name="isEnabled"></param>
        void ToggleCanvasAndGraphicRaycaster(bool isEnabled)
        {
            Canvas.enabled = isEnabled;
            GraphicRaycaster.enabled = isEnabled;
        }

        /// <summary>
        /// Gets and Executes the orientation update for this UIElement.
        /// </summary>
        IEnumerator GetOrientation()
        {
            while (UIManager.currentOrientation == UIManager.Orientation.Unknown)
            {
                UIManager.Instance.OrientationManager.CheckDeviceOrientation();
                if (UIManager.currentOrientation != UIManager.Orientation.Unknown)
                    break;

                yield return null;
            }

            if (LANDSCAPE && UIManager.currentOrientation == UIManager.Orientation.Landscape)
            {
                UIManager.HideUiElement(elementName, elementCategory, true);
                UIManager.ShowUiElement(elementName, elementCategory, false);
            }
            else if (PORTRAIT && UIManager.currentOrientation == UIManager.Orientation.Portrait)
            {
                UIManager.HideUiElement(elementName, elementCategory, true);
                UIManager.ShowUiElement(elementName, elementCategory, false);
            }
        }

        /// <summary>
        /// This fixes a very strange issue inside Unity. When setting a VerticalLayoutGroup or a HorizontalLayoutGroup, the Image bounds get moved (the image appeares in a different place).
        /// We could not find a better solution, but this should work for now.
        /// </summary>
        void ExecuteLayoutFix()
        {
            if (!executeLayoutFix) { return; }
            childCanvas = GetComponentsInChildren<Canvas>();
            if (childCanvas != null && childCanvas.Length > 0)
            {
                for (int i = 0; i < childCanvas.Length; i++)
                {
                    childCanvas[i].enabled = !childCanvas[i].enabled;
                    childCanvas[i].enabled = !childCanvas[i].enabled;
                }
            }
        }

        /// <summary>
        /// If this UIElement has other child UIElements and was disabled when it was hidden, this comes into play. It calls again, in the next frame, the Show command so that the child UIElements will show up (provided they have the same element category and name).
        /// </summary>
        IEnumerator TriggerShowInTheNextFrame(bool instantAction)
        {
            yield return null;
            UIManager.ShowUiElement(elementName, elementCategory, instantAction);
        }
    }
}