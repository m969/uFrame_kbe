﻿using System.Collections.Generic;
using uFrame.Editor.Core;
using uFrame.Editor.GraphUI;
using uFrame.Editor.GraphUI.ViewModels;
using uFrame.Editor.Windows;
using uFrame.IOC;
using UnityEngine;

namespace uFrame.Editor.QuickAccess
{
    public class QuickAccessWindowPlugin : DiagramPlugin, IQuickAccessEvents
    {
        public override decimal LoadPriority
        {
            get { return 20; }
        }
        public override bool Required
        {
            get { return true; }
        }

        public override void Initialize(UFrameContainer container)
        {
            Container = container;
        }

        //  [MenuItem("Window/uFrame/Quick Access #z")]
        public static void ShowQuickAccess()
        {
            InvertApplication.SignalEvent<IWindowsEvents>(_ => _.ShowWindow("QuickAccessWindowFactory", "Quick Access", null, new Vector2(Event.current.mousePosition.x, Event.current.mousePosition.y), new Vector2(150f, 250f)));
        
         
        }

        //  [MenuItem("Window/uFrame/Quick Access #z",true)]
        public static bool ShowQuickAccessValidation()
        {
            return InvertGraphEditor.DesignerWindow != null && InvertGraphEditor.DesignerWindow.DiagramViewModel != null;
        }

        public void SelectionChanged(GraphItemViewModel selected)
        {
            Debug.Log("Item selected: "+selected.GetType().Name);
        }

        public DiagramViewModel CurrentDiagramViewModel
        {
            get { return InvertGraphEditor.DesignerWindow.DiagramViewModel; }
        }

        public IEnumerable<GraphItemViewModel> CurrentSelectedNodeItems
        {
            get { return InvertGraphEditor.DesignerWindow.DiagramViewModel.SelectedNodeItems; }
        }

        public IEnumerable<GraphItemViewModel> CurrentSelectedGraphItems
        {
            get { return InvertGraphEditor.DesignerWindow.DiagramViewModel.SelectedGraphItems; }
        }

        public override UFrameContainer Container { get; set; }

        public void QuickAccessItemsEvents(QuickAccessContext context, List<IItem> items)
        {
        
        }
    }
}