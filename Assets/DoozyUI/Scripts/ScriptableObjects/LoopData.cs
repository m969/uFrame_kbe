// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using UnityEngine;
using System;

namespace DoozyUI
{
    [Serializable]
    public class LoopData : ScriptableObject
    {
        public string presetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
        public string presetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public Loop data;
        public LoopData()
        {
            presetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            presetCategory = UIAnimatorUtil.DEFAULT_PRESET_CATEGORY;
            data = new Loop();
        }
        public bool LoadDefaultValues { get { return presetName.Equals(UIAnimatorUtil.DEFAULT_PRESET_NAME) && presetCategory.Equals(UIAnimatorUtil.DEFAULT_PRESET_CATEGORY); } }
    }
}