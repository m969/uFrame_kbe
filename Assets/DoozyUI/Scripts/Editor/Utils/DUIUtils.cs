// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using UnityEngine;

namespace DoozyUI
{
    public class DUIUtils
    {
        public static bool PreviewSound(string soundName)
        {
            if (string.IsNullOrEmpty(soundName.Trim()) || soundName.Equals(DUI.DEFAULT_SOUND_NAME)) { return false; }
#if dUI_MasterAudio
            DTGUIHelper.PreviewSoundGroup(soundName);
            return true;
#else
            if (DUI.UISoundsDatabase == null) { DUI.RefreshUISoundsDatabase(); }
            if (!DUI.UISoundNameExists(soundName)) { return false; }

            AudioClip audioClip = DUI.GetUISound(soundName).audioClip;
            if (audioClip != null) //there is an AudioClip reference -> play it
            {
                QUtils.PlayAudioClip(audioClip);
                return true;
            }


            audioClip = Resources.Load(DUI.GetUISound(soundName).soundName) as AudioClip;
            if (audioClip != null) //a sound file with the soundName was found in Resources -> play it
            {
                QUtils.PlayAudioClip(audioClip);
                return true;
            }

            return false;
#endif
        }

        public static void StopSoundPreview(string soundName)
        {
#if dUI_MasterAudio
            DTGUIHelper.StopPreview(soundName);
#else
            QUtils.StopAllClips();
#endif
        }
    }
}
