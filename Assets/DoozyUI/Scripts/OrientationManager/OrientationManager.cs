// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using UnityEngine;
using UnityEngine.Events;

namespace DoozyUI
{
    [RequireComponent(typeof(RectTransform), typeof(Canvas))]
    [DisallowMultipleComponent]
    public class OrientationManager : QuickEngine.Common.Singleton<OrientationManager>
    {
        protected OrientationManager() { }

        #region Context Menu
#if UNITY_EDITOR
        [UnityEditor.MenuItem(DUI.TOOLS_MENU_ORIENTATION_MANAGER, false, DUI.MENU_PRIORITY_ORIENTATION_MANAGER)]
        [UnityEditor.MenuItem(DUI.GAMEOBJECT_MENU_ORIENTATION_MANAGER, false, DUI.MENU_PRIORITY_ORIENTATION_MANAGER)]
        static void CreateOrientationManager()
        {
            AddOrientationManagerToScene();
        }
#endif
#endregion

        private RectTransform m_rectTransform;
        public RectTransform RectTransform { get { if (m_rectTransform == null) { m_rectTransform = GetComponent<RectTransform>(); } return m_rectTransform; } }

        private Canvas m_canvas;
        public Canvas Canvas { get { if (m_canvas == null) { m_canvas = GetComponent<Canvas>(); } return m_canvas; } }

        /// <summary>
        /// Prints to Debug.Log all the relevant functionality informations needed for debug purposes
        /// </summary>
        public bool debug = false;

        /// <summary>
        /// Orientation type
        /// </summary>
        public enum Orientation
        {
            /// <summary>
            /// Landscape mode
            /// </summary>
            Landscape,
            /// <summary>
            /// Portrait mode
            /// </summary>
            Portrait,
            /// <summary>
            /// Unknown mode. Used for calibration purposes
            /// </summary>
            Unknown
        }

        [System.Serializable]
        public class OrientationChange : UnityEvent<Orientation> { }
        /// <summary>
        /// UnityEvent that sends an OrientaionManager.Orientation parameter when the device's orientation changes.
        /// </summary>
        public OrientationChange onOrientationChange = new OrientationChange();

        private Orientation currentOrientation = Orientation.Unknown;
        /// <summary>
        /// Retruns the current orientation of the device.
        /// </summary>
        public Orientation CurrentOrientation { get { return currentOrientation; } }

        void Awake()
        {
            if (Instance != this) { Destroy(gameObject); }

            if (FindObjectsOfType(typeof(OrientationManager)).Length > 1)
            {
                Debug.LogError("[DoozyUI] There cannot be two OrientationManagers active at the same time");
                Destroy(this);
                return;
            }

            if (GetComponent<Canvas>().isRootCanvas == false)
            {
                GetComponent<RectTransform>().anchorMin = Vector2.zero;
                GetComponent<RectTransform>().anchorMax = Vector2.one;
            }
        }

        private void OnEnable()
        {
            CheckDeviceOrientation();
        }

        void OnRectTransformDimensionsChange()
        {
            CheckDeviceOrientation();
        }

        /// <summary>
        /// Checks the current orientation and updates it if it changed since the last check. You do not need to call this yourself as this is called automatically by the OrientationManager in the most efficient way. 
        /// </summary>
        public void CheckDeviceOrientation()
        {
#if UNITY_EDITOR
            //PORTRAIT
            if (Screen.width < Screen.height)
            {
                if (currentOrientation != Orientation.Portrait) //Orientation changed to PORTRAIT
                {
                    ChangeOrientation(Orientation.Portrait);
                }
            }

            //LANDSCAPE
            else
            {
                if (currentOrientation != Orientation.Landscape) //Orientation changed to LANDSCAPE
                {
                    ChangeOrientation(Orientation.Landscape);
                }
            }
#else
            //LANDSCAPE
            if (Screen.orientation == ScreenOrientation.Landscape ||
               Screen.orientation == ScreenOrientation.LandscapeLeft ||
               Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                if (currentOrientation != Orientation.Landscape) //Orientation changed to LANDSCAPE
                {
                    ChangeOrientation(Orientation.Landscape);
                }
            }

            //PORTRAIT
            else if (Screen.orientation == ScreenOrientation.Portrait ||
                     Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                if (currentOrientation != Orientation.Portrait) //Orientation changed to PORTRAIT
                {
                    ChangeOrientation(Orientation.Portrait);
                }
            }

            //FALLBACK option if we are in AutoRotate or if we are in Unknown
            else
            {
                ChangeOrientation(Orientation.Landscape);
            }
#endif
        }

        /// <summary>
        /// Updates the currentOrientation to the specified value and sends an UnityEvent to signal the change.
        /// </summary>
        public void ChangeOrientation(Orientation newOrientation)
        {
            currentOrientation = newOrientation;
            onOrientationChange.Invoke(currentOrientation);
            NotifyUIManager(newOrientation);
            if (debug) Debug.Log("[OrientationManager] currentOrientation: " + currentOrientation.ToString());
        }

        private void NotifyUIManager(Orientation newOrientation)
        {
#if dUI_DoozyUI
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying) { return; } //This fixes the issue InvalidOperationException when calling UIManager.Instance by invoking DontDestroyOnLoad. Issue is generated when manually updating the orientation for the OrientationManager while in EditMode.
#endif
            switch (newOrientation)
            {
                case Orientation.Landscape: UIManager.Instance.ChangeOrientation(UIManager.Orientation.Landscape); break;
                case Orientation.Portrait: UIManager.Instance.ChangeOrientation(UIManager.Orientation.Portrait); break;
                case Orientation.Unknown: UIManager.Instance.ChangeOrientation(UIManager.Orientation.Unknown); break;
            }
#endif
        }

        public static OrientationManager AddOrientationManagerToScene()
        {
            if (FindObjectOfType<OrientationManager>() != null)
            {
                Debug.Log("[Orientation Manager] Cannot add another Orientation Manager to this Scene because you don't need more than one.");
#if UNITY_EDITOR
                UnityEditor.Selection.activeObject = FindObjectOfType<OrientationManager>();
#endif
                return null;
            }

            string prefabName = "OrientationManager";
            string goName = "OrientationManager";
            GameObject prefab = Resources.Load<GameObject>("DUI/Prefabs/" + prefabName) as GameObject;
            if (prefab == null)
            {
                Debug.LogError("Could not find the " + prefabName + " prefab.");
                return null;
            }
            GameObject go = Instantiate(prefab);
            go.name = goName;
#if UNITY_EDITOR
            UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Added " + go.name);
            UnityEditor.Selection.activeObject = go;
#endif
            return go.GetComponent<OrientationManager>();
        }
    }
}
