// Copyright (c) 2017 Doozy Entertainment / Marlink Trading SRL and Ez Entertainment / Ez Entertainment SRL. All Rights Reserved.
// This code is a collaboration between Doozy Entertainment and Ez Entertainment and is not to be used in any other assets other then the ones created by their respective companies.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace QuickEditor
{
    public class QWindow : EditorWindow
    {
        #region Colors
        private Color tempColor = Color.white;
        private Color tempContentColor = Color.white;
        private Color tempBackgroundColor = Color.white;

        public void SaveColors(bool resetColors = false)
        {
            tempColor = GUI.color;
            tempContentColor = GUI.contentColor;
            tempBackgroundColor = GUI.backgroundColor;
            if (resetColors) { QUI.ResetColors(); }
        }

        public void RestoreColors()
        {
            GUI.color = tempColor;
            GUI.contentColor = tempContentColor;
            GUI.backgroundColor = tempBackgroundColor;
        }
        #endregion

        #region Dimensions
        public const float WIDTH_512 = 512f;
        public const float WIDTH_256 = 256f;
        public const float WIDTH_128 = 128f;
        public const float WIDTH_64 = 64f;


        public const float HEIGHT_8 = 8f;
        /// <summary>
        /// This is the EditorGUIUtility.singleLineHeight value
        /// </summary> 
        public const float HEIGHT_16 = 16f;
        public const float HEIGHT_24 = 24f;
        public const float HEIGHT_36 = 36f;

        public const float INDENT_24 = 24f;

        /// <summary>
        /// This is the EditorGUIUtility.standardVerticalSpacing value
        /// </summary>
        public const float SPACE_2 = 2;
        public const float SPACE_4 = 4;
        public const float SPACE_8 = 8;
        /// <summary>
        /// This is the EditorGUIUtility.singleLineHeight value
        /// </summary> 
        public const float SPACE_16 = 16;
        #endregion

        public Dictionary<string, InfoMessage> infoMessage;
        protected void AddInfoMessage(string key, string title, string message, InfoMessageType type)
        {
            if (infoMessage == null) { infoMessage = new Dictionary<string, InfoMessage>(); }
            infoMessage.Add(key, new InfoMessage()
            {
                title = title,
                message = message,
                show = new AnimBool(false, Repaint),
                type = type
            });
        }
        protected void DrawInfoMessage(string key)
        {
            DrawInfoMessage(key, -1);
        }
        protected void DrawInfoMessage(string key, float width)
        {
            if (infoMessage == null) { Debug.Log("The infoMessage database is null."); return; }
            if (infoMessage.Count == 0) { Debug.Log("The infoMessage database is empty. Add the key to the database before you try to darw its message."); return; }
            if (!infoMessage.ContainsKey(key)) { Debug.Log("The infoMessage database does not contain any key named '" + key + "'. Check if it was added to the database or if you spelled the key wrong."); return; }
            QUI.DrawInfoMessage(infoMessage[key], width);
        }

        #region RequiresConstantRepaint
        public bool requiresContantRepaint = false;
        private void OnInspectorUpdate()
        {
            if (requiresContantRepaint) { Repaint(); }
        }
        #endregion

        public void CenterWindow()
        {
            var pos = position;
            pos.center = new Rect(0f, 0f, Screen.currentResolution.width, Screen.currentResolution.height).center;
            position = pos;
        }
    }
}
