// Copyright (c) 2017 Doozy Entertainment / Marlink Trading SRL and Ez Entertainment / Ez Entertainment SRL. All Rights Reserved.
// This code is a collaboration between Doozy Entertainment and Ez Entertainment and is not to be used in any other assets other then the ones created by their respective companies.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using UnityEngine;

namespace QuickEngine
{
    public partial class QResources
    {
        private static Font fontAwesome;
        public static Font FontAwesome { get { if (fontAwesome == null) { fontAwesome = Resources.Load("Quick/Fonts/FontAwesome") as Font; } return fontAwesome; } }
    }
}
