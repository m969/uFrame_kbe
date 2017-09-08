// Copyright (c) 2017 Doozy Entertainment / Marlink Trading SRL and Ez Entertainment / Ez Entertainment SRL. All Rights Reserved.
// This code is a collaboration between Doozy Entertainment and Ez Entertainment and is not to be used in any other assets other then the ones created by their respective companies.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuickEditor
{
    public class QStyles
    {
        private static GUISkin skin;
        public static GUISkin Skin { get { if (skin == null) { skin = GetSkin(); } return skin; } }

        public static GUIStyle GetStyle(string styleName)
        {
            return Skin.GetStyle(styleName);
        }

        private static GUISkin GetSkin()
        {
            GUISkin skin = ScriptableObject.CreateInstance<GUISkin>();
            List<GUIStyle> styles = new List<GUIStyle>();
            styles.AddRange(DefaultStyles());
            skin.customStyles = styles.ToArray();
            return skin;
        }

        private static void UpdateSkin()
        {
            skin = null;
            skin = GetSkin();
        }

        public static void AddStyle(GUIStyle style)
        {
            if (style == null) { return; }
            List<GUIStyle> customStyles = new List<GUIStyle>();
            customStyles.AddRange(Skin.customStyles);
            if (customStyles.Contains(style)) { return; }
            customStyles.Add(style);
            Skin.customStyles = customStyles.ToArray();
        }

        public static void RemoveStyle(GUIStyle style)
        {
            if (style == null) { return; }
            List<GUIStyle> customStyles = new List<GUIStyle>();
            customStyles.AddRange(Skin.customStyles);
            if (!customStyles.Contains(style)) { return; }
            customStyles.Remove(style);
            Skin.customStyles = customStyles.ToArray();
        }

        private static List<GUIStyle> DefaultStyles()
        {
            List<GUIStyle> styles = new List<GUIStyle>();
            styles.Add(TextStyleWithBackground("Help", QResources.WhiteBackground.Texture2D, QColors.Help, 12, FontStyle.Normal, TextAnchor.MiddleLeft, true, true, QuickEngine.QResources.FontAwesome));
            styles.Add(TextStyleWithBackground("Info", QResources.WhiteBackground.Texture2D, QColors.Info, 12, FontStyle.Normal, TextAnchor.MiddleLeft, true, true, QuickEngine.QResources.FontAwesome));
            styles.Add(TextStyleWithBackground("Warning", QResources.WhiteBackground.Texture2D, QColors.Warning, 12, FontStyle.Normal, TextAnchor.MiddleLeft, true, true, QuickEngine.QResources.FontAwesome));
            styles.Add(TextStyleWithBackground("Error", QResources.WhiteBackground.Texture2D, QColors.Error, 12, FontStyle.Normal, TextAnchor.MiddleLeft, true, true, QuickEngine.QResources.FontAwesome));
            return styles;
        }

        private static GUIStyle TextStyleWithBackground(string name, Texture2D background, QColor textColor, int fontSize, FontStyle fontStyle, TextAnchor alignment, bool richText = true, bool wordWrap = true, Font font = null)
        {
            return new GUIStyle()
            {
                name = name,
                normal =
                {
                    background = background,
                    textColor = EditorGUIUtility.isProSkin ? textColor.Dark : textColor.Light
                },
                fontSize = fontSize,
                fontStyle = fontStyle,
                alignment = alignment,
                richText = richText,
                wordWrap = wordWrap,
                padding = new RectOffset(8, 8, 8, 8),
                border = new RectOffset(2, 2, 2, 2),
                font = font
            };
        }
    }
}
