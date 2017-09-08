// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

#if dUI_PlayMaker
using UnityEngine;
using HutongGames.PlayMaker;
using DoozyUI;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("DoozyUI")]
    [Tooltip("[Deprecated] use Send Button Click instead.")]
    public class SendUIButtonMessage : FsmStateAction
    {
        [HutongGames.PlayMaker.ActionSection("[Deprecated] use Send Button Click")]
#region Variables
        [RequiredField]
        public FsmString buttonName;
        public UIButton.ButtonClickType clickType = UIButton.ButtonClickType.OnClick;
        public bool debugThis = false;
#endregion

        public override void Reset()
        {
            buttonName = new FsmString { UseVariable = false };
            clickType = UIButton.ButtonClickType.OnClick;
        }

        public override void OnEnter()
        {
            //UIButtonMessage m = new UIButtonMessage();
            //m.buttonName = buttonName.Value;
            //Message.Send<UIButtonMessage>(m);
            UIManager.Instance.SendButtonAction(buttonName.Value, clickType);

            if (debugThis) { Debug.Log("[DoozyUI] - Playmaker - State Name [" + State.Name + "] - Simulated an " + clickType + " button click - using the '" + buttonName + "' button name."); }

            Finish();
        }
    }
}
#endif