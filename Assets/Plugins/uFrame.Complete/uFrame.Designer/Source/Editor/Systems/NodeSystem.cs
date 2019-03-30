﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using uFrame.Editor.Attributes;
using uFrame.Editor.Configurations;
using uFrame.Editor.Core;
using uFrame.Editor.Database.Data;
using uFrame.Editor.Graphs.Data;
using uFrame.Editor.GraphUI;
using uFrame.Editor.GraphUI.Drawers;
using uFrame.Editor.GraphUI.ViewModels;
using uFrame.Editor.Input;
using uFrame.Editor.Menus;
using uFrame.Editor.Nodes;
using uFrame.Editor.Platform;
using uFrame.IOC;
using UnityEditor;

namespace uFrame.Editor
{
    public class FlagConfig
    {
        public FlagConfig(Type @for, string flagName, NodeColor color)
        {
            For = @for;
            FlagName = flagName;
            Color = color;
        }

        public Type For { get; set; }

        public string FlagName { get; set; }

        public NodeColor Color { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public bool GetValue(IDiagramNodeItem obj)
        {
            if (PropertyInfo != null)
            {
                return (bool)PropertyInfo.GetValue(obj, null);
            }
            return obj[FlagName];
        }
        public void SetValue(IDiagramNodeItem obj, bool value)
        {
            if (PropertyInfo != null)
            {
                PropertyInfo.SetValue(obj,value, null);
            }
            else
            {
                obj[FlagName] = value;
            }

        }
    }

    public class FlagSystem : DiagramPlugin
        , IContextMenuQuery
    {
        //private static Dictionary<Type, FlagConfig> _flagConfigs = new Dictionary<Type, FlagConfig>();

        //public static Dictionary<Type, FlagConfig> FlagConfigs
        //{
        //    get { return _flagConfigs; }
        //    private set { _flagConfigs = value; }
        //}
        private static Dictionary<string, FlagConfig> _flagsByName = new Dictionary<string, FlagConfig>();

        public static Dictionary<string, FlagConfig> FlagByName
        {
            get { return _flagsByName; }
            private set { _flagsByName = value; }
        }
        public override decimal LoadPriority
        {
            get { return 500000; }
        }

        public override void Loaded(UFrameContainer container)
        {
            base.Loaded(container);
            FlagByName.Clear();
            foreach (var item in InvertApplication.GetDerivedTypes<IDiagramNodeItem>())
            {

                var flagProperties = item.GetProperties(BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance).Where(p => p.IsDefined(typeof (NodeFlag), true)).ToArray();
                foreach (var property in flagProperties)
                {
                    var attr = property.GetCustomAttributes(typeof (NodeFlag), true).OfType<NodeFlag>().FirstOrDefault();
                    FlagByName.Add(attr.Name, new FlagConfig(item,attr.Name,attr.Color)
                    {
                        PropertyInfo = property
                    });
                }
            }

            foreach (var item in container.ResolveAll<FlagConfig>())
            {
                FlagByName.Add(item.FlagName,item);
            }

        }

        public void QueryContextMenu(ContextMenuUI ui, MouseEvent evt, params object[] obj)
        {
            var item = obj.OfType<GraphItemViewModel>().FirstOrDefault();
            if (item == null) return;

            foreach (var flag in FlagByName.Values)
            {
                var flag1 = flag;
                if (flag.For.GetType().IsAssignableFrom(item.DataObject.GetType()) || flag.For == item.DataObject.GetType())
                {
                    var value = flag.GetValue(item.DataObject as IDiagramNodeItem);
                    ui.AddCommand(new ContextMenuItem()
                    {
                        Title = flag.FlagName,
                        Checked = value,
                        Group = "Flags",
                        Command = new LambdaCommand("Set Flag",() =>
                        {
                            flag1.SetValue(item.DataObject as IDiagramNodeItem, !value);
                        })
                    });
                }
            }
        }
    }


    public class NodeSystem : DiagramPlugin,
        IContextMenuQuery,
        IExecuteCommand<CreateNodeCommand>,
        IExecuteCommand<RenameCommand>,
        IExecuteCommand<DeleteCommand>,
        IExecuteCommand<ShowCommand>,
        IExecuteCommand<HideCommand>,
        IExecuteCommand<PullNodeCommand>,
        IExecuteCommand<ApplyRenameCommand>,
        IExecuteCommand<MoveItemUpCommand>,
        IExecuteCommand<MoveItemDownCommand>,
        IOnMouseUpEvent,
        IToolbarQuery
    {

        public static bool MinimalView
        {
            get { return EditorPrefs.GetBool("MinimalView", false); }
            set { EditorPrefs.SetBool("MinimalView",value); }
        }

        public void QueryContextMenu(ContextMenuUI ui, MouseEvent evt, params object[] objs)
        {
            var diagramNodeItem = objs.FirstOrDefault() as ItemViewModel;
            if (diagramNodeItem != null)
            {
                var item = diagramNodeItem.DataObject as IDiagramNodeItem;
                if (item != null)
                {
                    ui.AddCommand(new ContextMenuItem()
                    {
                        Title = "Move Up",
                        Command = new MoveItemUpCommand()
                        {
                            Item = item
                        }
                    });
                    ui.AddCommand(new ContextMenuItem()
                    {
                        Title = "Move Down",
                        Command = new MoveItemDownCommand()
                        {
                            Item = item
                        }
                    });
                }
            }
            var diagramNode = objs.FirstOrDefault() as DiagramNodeViewModel;
            if (diagramNode != null)
            {
                ui.AddCommand(new ContextMenuItem()
                {
                    Title = "Rename",
                    Group = "Node",
                    Command = new RenameCommand() { ViewModel = diagramNode }
                });
                ui.AddCommand(new ContextMenuItem()
                {
                    Title = "Hide",
                    Group = "Node",
                    Command = new HideCommand()
                    {
                        Node = objs.OfType<DiagramNodeViewModel>().Select(p=>p.GraphItemObject).ToArray(),
                        Filter = diagramNode.DiagramViewModel.GraphData.CurrentFilter
                    }
                });
                ui.AddCommand(new ContextMenuItem()
                {
                    Title = "Delete",
                    Group = "Careful",
                    Command = new DeleteCommand()
                    {
                        Item = objs.OfType<DiagramNodeViewModel>().Select(p => p.GraphItemObject).ToArray()
                    }
                });
                if (diagramNode.IsExternal)
                {
                    ui.AddCommand(new ContextMenuItem()
                    {
                        Title = "Pull",
                        Group = "Node",
                        Command = new PullNodeCommand()
                        {
                            Node = objs.OfType<DiagramNodeViewModel>().Select(p => p.GraphItemObject).ToArray(),
                        }
                    });
                }

            }

            var diagram = objs.FirstOrDefault() as DiagramViewModel;
            if (diagram != null)
            {

                var filter = diagram.GraphData.CurrentFilter;
                foreach (var nodeType in FilterExtensions.AllowedFilterNodes[filter.GetType()].OrderBy(p=>p.FullName))
                {
                    if (nodeType.IsAbstract) continue;
                    var config = Container.GetNodeConfig(nodeType);
                    if (config.AllowAddingInMenu)
                    {
                        ui.AddCommand(new ContextMenuItem()
                        {
                            Title = "Create " + Container.GetNodeConfig(nodeType).Name,
                            Group = "Create",
                            Command = new CreateNodeCommand()
                            {
                                NodeType = nodeType,
                                GraphData = diagram.GraphData,
                                Position = diagram.LastMouseEvent.MouseDownPosition
                            }
                        });

                    }

                }

                if (filter.AllowExternalNodes)
                {

                    foreach (var item in filter.GetAllowedDiagramItems().OfType<GenericNode>().OrderBy(p => p.Name))
                    {
                        ui.AddCommand(new ContextMenuItem()
                        {
                            Title = "Show/" + item.Config.Name + "/" + item.Name,
                            Group = "Show",
                            Command = new ShowCommand() { Node = item, Filter = filter, Position = evt.MousePosition }
                        });
                    }
                }

            }



        }

        public void Execute(CreateNodeCommand command)
        {
            var node = Activator.CreateInstance(command.NodeType) as IDiagramNode;
            var repository = Container.Resolve<IRepository>();
            node.GraphId = command.GraphData.Identifier;
            repository.Add(node);
            if (string.IsNullOrEmpty(node.Name))
                node.Name =
                    repository.GetUniqueName("New" + node.GetType().Name.Replace("Data", ""));

            var filterItem = node as IFilterItem;
            if (filterItem != null)
            {
                filterItem.FilterId = command.GraphData.CurrentFilter.Identifier;
                filterItem.Position = command.Position;
            }
            else
            {
                command.GraphData.CurrentFilter.ShowInFilter(node, command.Position);
            }

            // Start name editing
            DiagramNodeViewModel diagramNodeViewModel = InvertGraphEditor.CurrentDiagramViewModel.GraphItems
                .OfType<DiagramNodeViewModel>()
                .First(nodeViewModel => nodeViewModel.DataObject == node);
            InvertApplication.Execute(new RenameCommand { ViewModel = diagramNodeViewModel});
        }

        public void Execute(RenameCommand command)
        {
            command.ViewModel.BeginEditing();
        }

        public void Execute(DeleteCommand command) {
            int nonGraphNodeCount = command.Item.Length - command.Item.OfType<IDiagramNodeItem>().Count();
            string graphNodesNames =
                String.Join(", ", command.Item.OfType<IDiagramNodeItem>().Select(item => item.FullLabel).ToArray());
            string deleteMessage;
            if (nonGraphNodeCount == 0) {
                deleteMessage = String.Format("Delete {0}? This can't be Undo", graphNodesNames);
            } else {
                deleteMessage = String.Format("Delete {0} (and {1} others)? This can't be Undo", graphNodesNames, nonGraphNodeCount);
            }
            if (InvertGraphEditor.Platform.MessageBox("Delete", deleteMessage, "Go Ahead", "Cancel"))
            {
                foreach (var item in command.Item)
                    item.Repository.Remove(item);
            }
        }

        public void Execute(ShowCommand command)
        {
            command.Filter.ShowInFilter(command.Node, command.Position);
        }

        public void Execute(HideCommand command)
        {
            foreach(var item in command.Node)
            command.Filter.HideInFilter(item);
        }

        public void Execute(PullNodeCommand command)
        {
            var workspaceService = Container.Resolve<WorkspaceService>();

            if (workspaceService != null && workspaceService.CurrentWorkspace != null)
            {
                foreach (var gn in command.Node.OfType<GraphNode>())
                {
                    gn.GraphId = workspaceService.CurrentWorkspace.CurrentGraphId;
                    foreach (var item in gn.FilterNodes)
                    {
                        if (gn == item) continue;
                        item.GraphId = workspaceService.CurrentWorkspace.CurrentGraphId;
                    }
                }


            }

        }

        public void OnMouseUp(Drawer drawer, MouseEvent mouseEvent)
        {
            Container.Resolve<IRepository>().Commit();
        }

        public void Execute(ApplyRenameCommand command)
        {
            if (string.IsNullOrEmpty(command.Item.Name)) {
                command.Item.Name = "RenameMe";
            } else {
                command.Item.Rename(command.Name);
            }

            command.Item.EndEditing();

            DesignerWindow dw = InvertApplication.Container.Resolve<DesignerWindow>();
            dw.RefreshContent();
        }

        public void Execute(MoveItemUpCommand command)
        {
            var items = command.Item.Node.PersistedItems.Where(p => p.GetType() == command.Item.GetType()).ToArray().ToList();
            var index = items.IndexOf(command.Item);
            items.Move(index,true);
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                item.Order = i;
            }
        }

        public void Execute(MoveItemDownCommand command)
        {
            var items = command.Item.Node.PersistedItems.Where(p => p.GetType() == command.Item.GetType()).ToArray().ToList();
            var index = items.IndexOf(command.Item);
            items.Move(index, false);
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                item.Order = i;
            }
        }


        public void QueryToolbarCommands(ToolbarUI ui)
        {
            ui.AddCommand(new ToolbarItem()
            {
                Checked = MinimalView,
                Title = "Minimal",
                Command = new LambdaCommand("minimal",()=>MinimalView = !MinimalView),
                Position = ToolbarPosition.BottomRight
            });
        }
    }
}
