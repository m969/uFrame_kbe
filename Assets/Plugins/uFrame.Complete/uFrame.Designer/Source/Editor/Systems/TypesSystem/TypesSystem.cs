﻿using System;
using System.Collections.Generic;
using System.Linq;
using uFrame.Editor.Core;
using uFrame.Editor.Database.Data;
using uFrame.Editor.Graphs.Data.Types;
using uFrame.Editor.GraphUI;
using uFrame.Editor.GraphUI.Events;
using uFrame.Editor.GraphUI.ViewModels;
using uFrame.Editor.Input;
using uFrame.Editor.Platform;
using uFrame.IOC;

namespace uFrame.Editor.TypesSystem
{
    public class TypesSystem : DiagramPlugin
        , IContextMenuQuery
        , IExecuteCommand<SelectTypeCommand>
        , IQueryTypes
    {
        public override void Loaded(UFrameContainer container)
        {
            base.Loaded(container);
            TypesInfo = InvertGraphEditor.TypesContainer.ResolveAll<GraphTypeInfo>().ToArray();
            Repository = container.Resolve<IRepository>();
        }

        public IRepository Repository { get; set; }

        public GraphTypeInfo[] TypesInfo { get; set; }

        public void QueryContextMenu(ContextMenuUI ui, MouseEvent evt, params object[] obj)
        {
            var typedItem = obj.FirstOrDefault() as TypedItemViewModel;

            if (typedItem != null)
            {
                foreach (var item in TypesInfo)
                {
                    var item1 = item;
                    ui.AddCommand(new ContextMenuItem()
                    {
                        Title = item1.Name,
                        Group = item.Group,
                        Command = new LambdaCommand("Change Type", () =>
                        {
                            typedItem.RelatedType = item1.Name;
                        })
                    });
                }
            }
            var nodeItem = obj.FirstOrDefault() as ItemViewModel;
            if (nodeItem != null)
            {
                ui.AddCommand(new ContextMenuItem()
                {
                    Title = "Delete",
                    Command = new DeleteCommand()
                    {
                        Title = "Delete Item",
                        Item = new[] {nodeItem.DataObject as IDataRecord }
                    }
                });
            }
        }

        public List<SelectionMenuItem> CachedItems = null;
        public void Execute(SelectTypeCommand command)
        {
            var menu = new SelectionMenu();
            if (command.AllowNoneType)
            {
                menu.AddItem(new SelectionMenuItem("", "None", () =>
                {
                    command.Item.RelatedType = null;
                }));
            }

           // var types = GetRelatedTypes(command).ToArray();
            foreach (var item in GetRelatedTypes(command))
            {
                var type1 = item;
                if (command.Filter == null || command.Filter(item))
                {
                    menu.AddItem(new SelectionMenuItem(item.Group, TypedItemViewModel.TypeAlias(item.TypeName), () =>
                    {
                        var record = type1 as IDataRecord;
                        if (record != null)
                        {
                            command.Item.RelatedType = record.Identifier;

                        }
                        else
                        {
                            // TODO: Change RelatedType from FullName to TypeName, not sure if there is any bug
                            command.Item.RelatedType = type1.TypeName;//type1.FullName;
                        }

                        if (command.OnSelectionFinished != null) command.OnSelectionFinished();
                    }));
                }

            }

            if (!command.AllowNoneType) {
                menu.OnCancel = () => {
                    if (String.IsNullOrEmpty(command.Item.RelatedType)) {
                        command.Item.Node.Repository.Remove(command.Item);
                    }
                };
            }

            Signal<IShowSelectionMenu>(_=>_.ShowSelectionMenu(menu));
        }
        public virtual IEnumerable<ITypeInfo> GetRelatedTypes(SelectTypeCommand command)
        {
            if (command.AllowNoneType)
            {
                yield return new SystemTypeInfo(typeof(void));
            }

            var queriedTypes = new List<ITypeInfo>();
            Signal<IQueryTypes>(_=>_.QueryTypes(queriedTypes));

            foreach (var item in queriedTypes)
                yield return item;
        }

        public void QueryTypes(List<ITypeInfo> typeInfo)
        {
            if (Repository != null)
            foreach (var item in Repository.AllOf<IDataRecord>().OfType<ITypeInfo>())
            {
                typeInfo.Add(item);
            }
        }
    }
}
