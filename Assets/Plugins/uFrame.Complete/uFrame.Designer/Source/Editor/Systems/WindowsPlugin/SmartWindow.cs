﻿using System;
using System.Collections.Generic;
using uFrame.Editor.Windows;
using UnityEditor;
using UnityEngine;

namespace uFrame.Editor.WindowsPlugin
{


    public class SmartWindow : EditorWindow, IWindowDrawer
    {

        [SerializeField] private string _windowId;
        [SerializeField] private string _windowFactoryId; 
        [SerializeField] private string _persistedData;
        private List<Area> _drawers;

        public string WindowId
        {
            get { return _windowId; }
            set { _windowId = value; }
        }

        public bool RepaintOnUpdate { get; set; }

        public string PersistedData
        {
            get { return _persistedData; }
            set { _persistedData = value; }
        }

        public string WindowFactoryId
        {
            get { return _windowFactoryId; }
            set { _windowFactoryId = value; }
        }

        public IWindow ViewModel { get; set; }

        public List<Area> Drawers
        {
            get { return _drawers ?? (_drawers = new List<Area>()); }
            set { _drawers = value; }
        }

        public void OnGUI()
        {
            if (ViewModel == null && _windowFactoryId != null)   
            {
                WindowsPlugin.BindDrawerToWindow(this,_windowFactoryId);
            }

            if (ViewModel != null)
            {
                foreach (var ufArea in Drawers)
                {
                    DrawArea(ufArea);
                }
            }
        }

        private int _gridSystemUnits = 16;

        public int GridSystemUnits
        {
            get { return _gridSystemUnits; }
            set { _gridSystemUnits = value; }
        }

#pragma warning disable 67
        public event Action<object> Bound;
#pragma warning restore 67

        private void DrawArea(Area area)
        {
            var layout = area.Layout;
            var gridUnitWidth = this.position.width / GridSystemUnits;
            var gridUnitHeight = this.position.height / GridSystemUnits;
            GUILayout.BeginArea(new Rect(layout.GridOffsetLeft * gridUnitWidth, layout.GridOffsetTop * gridUnitHeight, layout.GridWidth * gridUnitWidth, layout.GridHeight * gridUnitHeight));
            area.Draw();
            GUILayout.EndArea();
        }

        void Update()
        {
            if (RepaintOnUpdate)
            {
                Repaint();
            }
        }

        void OnDestroy()
        {
            WindowsPlugin.WindowDrawers.Remove(this);
        }
    }
}