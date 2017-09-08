// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using UnityEngine;
using System;

namespace DoozyUI
{
    [Serializable]
    public class AnimData : ScriptableObject
    {
        public string presetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string presetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public Anim data;
        public AnimData(Anim.AnimationType aType)
        {
            presetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            presetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            data = new Anim(aType);
        }
        public bool LoadDefaultValues { get { return presetName.Equals(UIAnimatorUtil.DEFAULT_PRESET_NAME) && presetCategory.Equals(UIAnimatorUtil.DEFAULT_PRESET_CATEGORY); } }
    }
}