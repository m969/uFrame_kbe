﻿using uFrame.Editor.Core;
using UnityEditor;
using UnityEngine;

namespace uFrame.Editor.InspectorWindow
{
    public class uFrameIssuesWindow : EditorWindow
    {
        private Vector2 _scrollPosition;

        [MenuItem("Window/uFrame/Issues #&u")]
        internal static void ShowWindow()
        {
            var window = GetWindow<uFrameIssuesWindow>();
            window.titleContent.text = "Issues";
            Instance = window;
            window.Show();
        }

        public static uFrameIssuesWindow Instance { get; set; }

        public void OnGUI()
        {
            Instance = this;
            var rect = new Rect(0f, 0f, Screen.width, Screen.height);

            GUILayout.BeginArea(rect);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            InvertApplication.SignalEvent<IDrawErrorsList>(_ => _.DrawErrors(rect));
            GUILayout.EndScrollView();
            GUILayout.EndArea();

        }

        public void Update()
        {
            Repaint();
        }

    }
}