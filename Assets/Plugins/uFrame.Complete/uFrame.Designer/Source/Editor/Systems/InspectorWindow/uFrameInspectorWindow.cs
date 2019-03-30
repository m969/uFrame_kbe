﻿using uFrame.Editor.Core;
using uFrame.Editor.GraphUI.Drawers;
using UnityEditor;
using UnityEngine;

namespace uFrame.Editor.InspectorWindow
{
    public class uFrameInspectorWindow : EditorWindow {
        private Vector2 _scrollPosition;

        [MenuItem("Window/uFrame/Inspector #&i")]
        internal static void ShowWindow()
        {
            var window = GetWindow<uFrameInspectorWindow>();
            window.titleContent.text = "Inspector";
            Instance = window;
            window.Show();
        }

        public static uFrameInspectorWindow Instance { get; set; }

        public void OnGUI()
        {
            Instance = this;
            var rect = new Rect(0f, 0f, Screen.width, Screen.height).Pad(0,0,4,20);

            GUILayout.BeginArea(rect);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            InvertApplication.SignalEvent<IDrawInspector>(_ => _.DrawInspector(rect));
            GUILayout.EndScrollView();
            GUILayout.EndArea();
    
        }

        public void Update()
        {
            Repaint();
        }

    }
}