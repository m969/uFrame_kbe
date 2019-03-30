﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using uFrame.Editor.Core;
using uFrame.Editor.Graphs.Data;
using uFrame.Editor.GraphUI.ViewModels;
using uFrame.Editor.Input;
using uFrame.Editor.Platform;
using uFrame.IOC;
using uFrame.Kernel.Collection;
using UnityEngine;

namespace uFrame.Editor.GraphUI.Drawers
{
    public abstract class DiagramNodeDrawer<TViewModel> : DiagramNodeDrawer where TViewModel : DiagramNodeViewModel
    {
        protected DiagramNodeDrawer()
        {
        }

        protected DiagramNodeDrawer(TViewModel viewModel)
        {
            this.ViewModelObject = viewModel;
        }

        public override Rect Bounds
        {
            get { return ViewModelObject.Bounds; }
            set { ViewModelObject.Bounds = value; }
        }

        public TViewModel NodeViewModel
        {
            get { return ViewModel as TViewModel; }
        }
    }

    public abstract class DiagramNodeDrawer : Drawer, INodeDrawer, IDisposable
    {
        //private string _cachedLabel;
        //private string[] _cachedTags;
        private string _cachedTag;
        private ErrorInfo[] _cachedIssues;
        private object _headerStyle;


        [Inject]
        public IUFrameContainer Container { get; set; }

        public DiagramNodeViewModel ViewModel
        {
            get { return DataContext as DiagramNodeViewModel; }
            set { DataContext = value; }
        }

        protected DiagramNodeDrawer()
        {

        }

        protected override void DataContextChanged()
        {
            base.DataContextChanged();
            _cachedTag = null;
            //_cachedTags = null;
            Children.Clear();
            // Anything after its initialized will be manually done
            //Refresh(InvertGraphEditor.Platform);
            RefreshContent();

            if (!ViewModel.IsCollapsed)
            foreach (var item in ViewModel.ContentItems)
            {
                var model = item as GraphItemViewModel;
                if (model != null)
                    Children.Add(CreateChild(model));
            }

            this.ViewModel.ContentItems.CollectionChanged += ContentItemsOnCollectionChangedWith;
        }

        private void ContentItemsOnCollectionChangedWith(object sender, NotifyCollectionChangedEventArgs changeArgs)
        {
            if (changeArgs.Action == NotifyCollectionChangedAction.Reset)
            {

                RefreshContent();
            }
            if (changeArgs.NewItems != null && !ViewModel.IsCollapsed)
            {
                foreach (var item in changeArgs.NewItems)
                {
                    var model = item as GraphItemViewModel;
                    if (model != null)
                        Children.Add(CreateChild(model));
                }
            }
            if (changeArgs.OldItems != null)
            {
                Children.RemoveAll(p => changeArgs.OldItems.Contains(p.ViewModelObject));
            }
            //_cachedTags = null;
            //_cachedTag = null;
            //_cachedTag = string.Join(" | ", ViewModel.Tags.ToArray());
            Refresh(InvertGraphEditor.PlatformDrawer);
        }

        void IDrawer.OnMouseDown(MouseEvent mouseEvent)
        {
            OnMouseDown(mouseEvent);
        }

        public float Scale
        {
            get { return InvertGraphEditor.DesignerWindow.Scale; }
        }

        public virtual object ItemStyle
        {
            get { return CachedStyles.Item4; }
        }

        public object SelectedItemStyle
        {
            get { return CachedStyles.SelectedItemStyle; }
        }

        public virtual float Width
        {
            get
            {
                var maxLengthItem = InvertGraphEditor.PlatformDrawer.CalculateTextSize(ViewModel.FullLabel, CachedStyles.DefaultLabelLarge);
                if (ViewModel.IsCollapsed)
                {
                    foreach (var item in ViewModel.Items)
                    {
                        var newSize = InvertGraphEditor.PlatformDrawer.CalculateTextSize(item.FullLabel, CachedStyles.DefaultLabelLarge);

                        if (maxLengthItem.x < newSize.x)
                        {
                            maxLengthItem = newSize;
                        }
                    }
                }
                if (ViewModel.ShowSubtitle)
                {
                    var subTitle = InvertGraphEditor.PlatformDrawer.CalculateTextSize(ViewModel.SubTitle, CachedStyles.DefaultLabelLarge);
                    if (subTitle.x > maxLengthItem.x)
                    {
                        maxLengthItem = subTitle;
                    }
                }


                return Math.Max(MinWidth, maxLengthItem.x + 40);
            }
        }

        public virtual float MinWidth
        {
            get { return 150f; }
        }
        public float ItemHeight
        {
            get { return 20; }
        }

        public virtual float Padding
        {
            get { return 8; }
        }

        public virtual object BackgroundStyle
        {
            get { return CachedStyles.NodeHeader1; }
        }

        public float ItemExpandedHeight
        {
            get { return 0; }
        }

        string IDrawer.ShouldFocus { get; set; }



        public DiagramDrawer Diagram { get; set; }


        protected virtual void GetContentDrawers(List<IDrawer> drawers)
        {

            foreach (var item in ViewModel.ContentItems)
            {
                var drawer = CreateChild(item);

                if (drawer == null)
                {
                    InvertApplication.Log(string.Format("Couldn't create drawer for {0} make sure it is registered.",
                        item.GetType().Name));
                    continue;
                }


                drawers.Add(drawer);
            }
        }

        private IDrawer CreateChild(GraphItemViewModel item)
        {

            var drawer = InvertGraphEditor.Container.CreateDrawer(item);
            if (drawer != null)
            {
                drawer.ParentDrawer = this;
            }

            return drawer;
        }


        public override void Draw(IPlatformDrawer platform, float scale) {
            if (Event.current.type == EventType.Layout)
                return;


            var width = platform.CalculateTextSize(_cachedTag, CachedStyles.Tag1).x;
            var labelRect =
                new Rect((Bounds.x + (Bounds.width / 2)) - (width / 2), Bounds.y - (16f), width, 15f).Scale(Scale);
            if (!string.IsNullOrEmpty(_cachedTag))
            {
                platform.DrawLabel(labelRect, _cachedTag, CachedStyles.Tag1, DrawingAlignment.MiddleCenter);
            }

#if UNITY_EDITOR || USE_IMAGES
            var adjustedBounds = new Rect(Bounds.x - 9, Bounds.y + 1, Bounds.width + 19, Bounds.height + 9);
#else
            var adjustedBounds = Bounds; //new Rect(Bounds.x - 9, Bounds.y + 1, Bounds.width + 19, Bounds.height + 9);
#endif
            var boxRect = adjustedBounds.Scale(Scale);

            DrawBeforeBackground(platform,boxRect);

            platform.DrawStretchBox(boxRect, CachedStyles.NodeBackground, 18);

            //if (ViewModel.IsSelected || ViewModel.IsMouseOver)
            //{
            //    platform.DrawStretchBox(boxRect, CachedStyles.NodeBackground, 20);

            //}
            if (ViewModel.AllowCollapsing && ShowHeader)
            {

                var rect = new Rect((Bounds.x + (Bounds.width / 2f)) - 21f,
                    Bounds.y + Bounds.height - 1f, 42f, 18f);
                var style = ViewModel.IsCollapsed
                    ? CachedStyles.NodeExpand
                    : CachedStyles.NodeCollapse;

                platform.DoButton(rect.Scale(scale), string.Empty, style, () =>
                {
                    InvertApplication.Execute(new LambdaCommand("Toggle Collapse",() =>
                    {
                        ViewModel.IsCollapsed = !ViewModel.IsCollapsed;
                        Dirty = true;
                    }) );
                });
            }


            DrawChildren(platform, scale);
            bool hasErrors = ViewModel.Issues != null && ViewModel.Issues.Length > 0;
            if (hasErrors)
            {
                platform.DrawStretchBox(boxRect, CachedStyles.BoxHighlighter6, 20);

                if (ViewModel.IsSelected)
                {
                    platform.DrawStretchBox(boxRect, CachedStyles.BoxHighlighter2, 20);
                }
            }
            else
            {

                if (ViewModel.IsMouseOver)
                {
                    platform.DrawStretchBox(boxRect, CachedStyles.BoxHighlighter3, 20);
                }
                if (ViewModel.IsSelected)
                {
                    platform.DrawStretchBox(boxRect, CachedStyles.BoxHighlighter2, 20);
                }
            }
            if (!ViewModel.Enabled)
            {
                platform.DrawStretchBox(boxRect, CachedStyles.BoxHighlighter5, 20);
            }

            if ( ViewModel.Issues != null)
            {
                for (int index = 0; index < ViewModel.Issues.Length; index++)
                {
                    var keyValuePair = ViewModel.Issues[index];
                    var w = platform.CalculateTextSize(keyValuePair.Message, CachedStyles.DefaultLabel).x;//EditorStyles.label.CalcSize(new GUIContent(keyValuePair.Key)).x);
                    var x = (Bounds.x + (Bounds.width / 2f)) - (w / 2f);
                    var rect = new Rect(x, (Bounds.y + Bounds.height + 18) + (40f * (index)), w + 20f, 40);
                    platform.DrawWarning(rect, keyValuePair.Message);
                    if (keyValuePair.AutoFix != null)
                    {
                        platform.DoButton(new Rect(rect.x + rect.width + 5, rect.y, 75, 25).Scale(Scale), "Auto Fix", null,
                            () =>
                            {
                                InvertApplication.Execute(new LambdaCommand("Auto Fix",() =>
                                {
                                    keyValuePair.AutoFix();
                                }));

                            });
                    }
                    hasErrors = true;
                }
            }

        }

        protected virtual void DrawBeforeBackground(IPlatformDrawer platform, Rect boxRect)
        {

        }

        protected virtual void DrawChildren(IPlatformDrawer platform, float scale)
        {
            for (int index = 0; index < Children.Count; index++)
            {
                var item = Children[index];
//if (item.Dirty)
                //{
                //    Refresh((IPlatformDrawer)platform,item.Bounds.position,false);
                //    item.Dirty = false;
                //}
                item.Draw(platform, scale);
            }
        }

        public object GetNodeColorStyle()
        {

            switch (ViewModel.Color)
            {
                case NodeColor.DarkGray:
                    return CachedStyles.NodeHeader1;
                case NodeColor.Blue:
                    return CachedStyles.NodeHeader2;
                case NodeColor.Gray:
                    return CachedStyles.NodeHeader3;
                case NodeColor.LightGray:
                    return CachedStyles.NodeHeader4;
                case NodeColor.Black:
                    return CachedStyles.NodeHeader5;
                case NodeColor.DarkDarkGray:
                    return CachedStyles.NodeHeader6;
                case NodeColor.Orange:
                    return CachedStyles.NodeHeader7;
                case NodeColor.Red:
                    return CachedStyles.NodeHeader8;
                case NodeColor.YellowGreen:
                    return CachedStyles.NodeHeader9;
                case NodeColor.Green:
                    return CachedStyles.NodeHeader10;
                case NodeColor.Purple:
                    return CachedStyles.NodeHeader11;
                case NodeColor.Pink:
                    return CachedStyles.NodeHeader12;
                case NodeColor.Yellow:
                    return CachedStyles.NodeHeader13;

            }
            return CachedStyles.NodeHeader1;

        }

        protected virtual object HeaderStyle
        {
            get { return _headerStyle ?? (_headerStyle = GetNodeColorStyle()); }
        }

        protected virtual object GetHighlighter()
        {
            return CachedStyles.BoxHighlighter4;
        }

        //protected virtual void DrawItem(IDiagramNodeItem item, ElementsDiagram diagram, bool importOnly)
        //{
        //    if (item.IsSelected && item.IsSelectable && !importOnly)
        //    {
        //        var rect = new Rect(item.Position).Scale(Scale);
        //        //rect.y += ItemHeight;
        //        //rect.height -= ItemHeight;
        //        //rect.height += ItemExpandedHeight;
        //        GUI.Box(rect, string.Empty, SelectedItemStyle);
        //        GUILayout.BeginArea(rect);
        //        EditorGUI.BeginChangeCheck();
        //        EditorGUILayout.BeginHorizontal();

        //        DrawSelectedItem(item, diagram);
        //        EditorGUILayout.EndHorizontal();
        //        GUILayout.EndArea();
        //    }
        //    else
        //    {

        //        GUI.Box(item.Position.Scale(Scale), string.Empty, item.IsSelected ? SelectedItemStyle : ItemStyle);

        //        DrawItemLabel(item);

        //    }
        //    if (!string.IsNullOrEmpty(item.Highlighter))
        //    {
        //        var highlighterPosition = new Rect(item.Position);
        //        highlighterPosition.width = 4;
        //        highlighterPosition.y += 2;
        //        highlighterPosition.x += 2;
        //        highlighterPosition.height = ItemHeight - 6;
        //        GUI.Box(highlighterPosition.Scale(Scale), string.Empty, ElementDesignerStyles.GetHighlighter(item.Highlighter));
        //    }
        //}

        //protected virtual void DrawItemLabel(IDiagramNodeItem item)
        //{
        //    var style = new GUIStyle(ItemStyle);
        //    style.normal.textColor = BackgroundStyle.normal.textColor;
        //    GUI.Label(item.Position.Scale(Scale), item.Label, style);


        //}

        public override void OnMouseDown(MouseEvent mouseEvent)
        {
            ViewModelObject.Select();
            if (mouseEvent.ModifierKeyStates.Ctrl)
            {
                if (mouseEvent.ModifierKeyStates.Alt)
                {
                    this.ViewModel.CtrlShiftClicked();
                }
                else
                {
                    this.ViewModel.CtrlClicked();

                }

            }
        }

        public override void OnMouseDoubleClick(MouseEvent mouseEvent)
        {
            base.OnMouseDoubleClick(mouseEvent);


        }

        public override void OnMouseMove(MouseEvent e)
        {
            base.OnMouseMove(e);
            ViewModel.IsMouseOver = true;
        }


        public virtual bool ShowHeader
        {
            get { return true; }
        }

        public virtual void RefreshContent()
        {
            var drawers = new List<IDrawer>();
            if (ShowHeader)
            {
                drawers.Add(new HeaderDrawer()
                {
                    BackgroundStyle = HeaderStyle,
                    TextStyle = HeaderTextStyle,
                    ViewModelObject = ViewModelObject,
                    StyleSchema = NodeSystem.MinimalView && !ViewModel.IsCurrentFilter ? ViewModel.MinimalisticStyleSchema : ViewModel.StyleSchema,
                    IconTooltip = ViewModel.Comments,
                    ParentDrawer = this
                });
            }
            //if (!ViewModel.IsCollapsed)
            //{
            //    //GetContentDrawers(drawers);
            //}
            Children = drawers;
        }

        public virtual object HeaderTextStyle
        {
            get { return CachedStyles.ViewModelHeaderStyle; }
        }
        public virtual Vector2 HeaderPadding
        {
            get { return new Vector2(10, 6); }
        }

        public override void Refresh(IPlatformDrawer platform, Vector2 position, bool hardRefresh = true)
        {

            _headerStyle = null;
            if (_cachedIssues == null)
            {
                _cachedIssues = new ErrorInfo[] {};// ViewModel.Issues.ToArray();

            }
            _cachedTag = string.Join(" | ", ViewModel.Tags.ToArray());
            //if (Children == null || Children.Count < 1)
            //{
            // //Children.Clear();
            //   RefreshContent();

            //}

            var startY = ViewModel.Position.y;
            // Now lets stretch all the content drawers to the maximum width
            var minWidth = Math.Max(120f, platform.CalculateTextSize(_cachedTag, CachedStyles.Tag1).x);
            var height = LayoutChildren(platform, startY, ref minWidth, hardRefresh);

            //_cachedLabel = ViewModel.Label;

            if (!ViewModel.IsCollapsed)
            {
                Bounds = new Rect(ViewModel.Position.x, ViewModel.Position.y, minWidth, height + Padding);
            }
            else
            {
                Bounds = new Rect(ViewModel.Position.x, ViewModel.Position.y, minWidth, height);
            }

            if (Children != null && Children.Count > 0)
            {
                var cb = new Rect(Children[0].Bounds);
                cb.x += 15;
                cb.y += 1;
                cb.width -= 13;

                ViewModel.ConnectorBounds = cb;

            }

        }

        protected virtual float LayoutChildren(IPlatformDrawer platform, float startY, ref float minWidth, bool hardRefresh)
        {
            var currentY = startY;
            var currentX = this.ViewModel.Position.x;
            var height = 0f;
            for (int index = 0; index < Children.Count; index++)
            {
                var item = Children[index];
                var previous = index > 0 ? Children[index - 1] : null;
                var next = index < Children.Count - 1 ? Children[index + 1] : null;

                // If its a new line adjust the positions
                if (item.ViewModelObject.IsNewLine)
                {

                    if (previous != null)
                    {
                        //var newWidth = currentX - this.ViewModel.Position.x;
                        //if (newWidth > minWidth)
                        //{
                        //    minWidth = newWidth;
                        //}

                        currentY += previous.Bounds.height;
                        height += previous.Bounds.height;
                    }
                    currentX = this.ViewModel.Position.x;
                }

                item.Refresh(platform, new Vector2(currentX, currentY), hardRefresh);

                currentX += item.Bounds.width;
                if (next == null)
                {

                    height += item.Bounds.height;
                }
                var newWidth = currentX - this.ViewModel.Position.x;
                if (newWidth > minWidth)
                {
                    minWidth = newWidth;
                }
            }
            for (int index = 0; index < Children.Count; index++)
            {
                var item = Children[index];
                //var previous = index > 0 ? Children[index - 1] : null;
                var next = index < Children.Count - 1 ? Children[index + 1] : null;

                if ((next != null && next.ViewModelObject.IsNewLine) || next == null)
                {
                    var widthAtItem = (item.Bounds.x + item.Bounds.width) - ViewModel.Position.x;
                    var adjustment = minWidth - widthAtItem;
                    var newRect = new Rect(item.Bounds);
                    newRect.width += adjustment;
                    item.Bounds = newRect;
                }
            }
            foreach (var item in Children)
            {
                item.Dirty = false;
                item.OnLayout();
            }

            return height;

        }

        public bool IsExternal { get; set; }


        public void Dispose()
        {
            ViewModel.ContentItems.CollectionChanged -= ContentItemsOnCollectionChangedWith;
        }
    }

    //public class InspectorDrawer : Drawer<DiagramNodeViewModel>
    //{

    //    public PropertyFieldDrawer[] Drawers { get; set; }
    //    public override void Refresh(IPlatformDrawer platform, Vector2 position, bool hardRefresh = true)
    //    {
    //        base.Refresh(platform, position, hardRefresh);
    //        InspectorOptions = new List<ViewModel>();
    //        ViewModel.GetInspectorOptions(InspectorOptions);
    //        Drawers = InspectorOptions.OfType<PropertyFieldViewModel>().Select(_ => new PropertyFieldDrawer(_)).ToArray();

    //        var height = 0f;
    //        var width = 0f;
    //        foreach (var drawer in Drawers)
    //        {
    //            drawer.Refresh(platform, position, hardRefresh);
    //            height += drawer.Bounds.height;
    //            if (width < drawer.Bounds.width)
    //            {
    //                width = drawer.Bounds.width;
    //            }
    //        }

    //        var x = position.x;
    //        var y = position.y;

    //        this.Bounds = new Rect(x,y,width,height);




    //    }

    //    public List<ViewModel> InspectorOptions { get; set; }

    //    public override void Draw(IPlatformDrawer platform, float scale)
    //    {
    //        base.Draw(platform, scale);

    //        foreach (var item in Drawers)
    //        {
    //            item.Draw(platform,1f);
    //        }

    //    }

    //    public InspectorDrawer(GraphItemViewModel viewModelObject) : base(viewModelObject)
    //    {
    //    }

    //    public InspectorDrawer(DiagramNodeViewModel viewModelObject) : base(viewModelObject)
    //    {
    //    }
    //}

    //public class BulletPointDrawer : Drawer<BulletPointViewModel>
    //{

    //}


}