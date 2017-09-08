// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using UnityEngine;
using System.Collections;

namespace DoozyUI
{
#if dUI_PlayMaker
    [AddComponentMenu(DUI.COMPONENT_MENU_PLAYMAKER_EVENT_DISPATCHER, DUI.MENU_PRIORITY_PLAYMAKER_EVENT_DISPATCHER)]
#endif
    public class PlaymakerEventDispatcher : MonoBehaviour
    {
#if dUI_PlayMaker

        #region Context Menu
#if UNITY_EDITOR
        [UnityEditor.MenuItem(DUI.GAMEOBJECT_MENU_PLAYMAKER_EVENT_DISPATCHER, false, DUI.MENU_PRIORITY_PLAYMAKER_EVENT_DISPATCHER)]
        static void CreateCustomGameObject(UnityEditor.MenuCommand menuCommand)
        {
            GameObject go = new GameObject("New PlaymakerEventDispatcher", typeof(PlaymakerEventDispatcher));
            UnityEditor.GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            UnityEditor.Selection.activeObject = go;
        }
#endif
        #endregion

        public bool debug = false;

        public bool overrideTargetFSM = false;
        public PlayMakerFSM targetFSM;

        public bool dispatchGameEvents = false;
        public bool dispatchButtonClicks = false;

        public bool Enabled { get { return targetFSM != null && (dispatchGameEvents || dispatchButtonClicks); } }

        private void Reset()
        {
            targetFSM = GetComponent<PlayMakerFSM>();
#if UNITY_EDITOR
            if (targetFSM == null)
            {
                if (UnityEditor.EditorUtility.DisplayDialog("Action Required", "The Playmaker Event Dispatcher attached to the '" + gameObject.name + "' gameObject did not find any PlayMakerFSM component attached as well." +
                                                                   "\n\n" +
                                                                   "Would you like to attach one now and reference it as the Target FSM for this dispatcher?", "Yes", "No"))
                {

                    targetFSM = gameObject.AddComponent<PlayMakerFSM>();
                    overrideTargetFSM = false;
                }
                else
                {
                    UnityEditor.EditorUtility.DisplayDialog("Info", "You chose not to attach a PlayMakerFSM." +
                                                        "\n\n" +
                                                        "Remember that you need to reference one in order for this dispatcher to do anything." +
                                                        "\n\n", "Ok");
                    overrideTargetFSM = true;
                }
            }
#endif
            dispatchGameEvents = false;
            dispatchButtonClicks = false;
        }

        void Awake()
        {
            if (targetFSM == null)
            {
                Debug.Log("[DoozyUI] The Target FSM, for the Playmaker Event Dispacher attached to the '" + gameObject.name + "' gameObject, is null. This dispatcher is disabled.");
            }
        }

        private void OnEnable()
        {
            RegisterToUIManager();
        }

        private void OnDisable()
        {
            UnregisterFromUIManager();
        }

        private void OnDestroy()
        {
            UnregisterFromUIManager();
        }

        protected void RegisterToUIManager()
        {
            if (!Enabled) { return; }
            if (UIManager.PlaymakerEventDispatcherDatabase.Contains(this)) { return; }
            UIManager.PlaymakerEventDispatcherDatabase.Add(this);
        }

        protected void UnregisterFromUIManager()
        {
            if (!UIManager.PlaymakerEventDispatcherDatabase.Contains(this)) { return; }
            UIManager.PlaymakerEventDispatcherDatabase.Remove(this);
        }

        public void DispatchEvent(string eventValue, DUI.EventType eventType)
        {
            switch (eventType)
            {
                case DUI.EventType.GameEvent: if (dispatchGameEvents) { StartCoroutine(WaitAndSendEvent(eventValue)); } break;
                case DUI.EventType.ButtonClick: if (dispatchButtonClicks) { StartCoroutine(WaitAndSendEvent(eventValue)); } break;
            }
        }

        private IEnumerator WaitAndSendEvent(string eventValue)
        {
            yield return new WaitForEndOfFrame();

            if (targetFSM == null)
            {
                Debug.LogWarning("[DoozyUI] The targetFSM for the Event Dispacher attached to [" + gameObject.name + "] gameObject is null. This should not happem.");
            }
            else
            {
                targetFSM.SendEvent(eventValue);
                if (debug) { Debug.Log("[DoozyUI] [Playmaker Event Dispatcher] attached to the '" + gameObject.name + "' gameObject - Sent the FSM Event '" + eventValue + "' to the '" + targetFSM.FsmName + "' target FSM that is attached to the '" + targetFSM.name + "' gameObject."); }
            }
        }
#endif
    }
}
