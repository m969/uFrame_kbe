﻿using System.Collections.Generic;
using uFrame.Editor.Database.Data;
using UnityEngine;

namespace uFrame.Editor.Graphs.Data
{

    public interface IDiagramNode : IDiagramNodeItem, IDataHeirarchy
    {

    
        string SubTitle { get; }

        /// <summary>
        /// Any child list items of the node
        /// </summary>
        IEnumerable<IDiagramNodeItem> DisplayedItems { get; }

        /// <summary>
        /// Begins renaming the node.
        /// </summary>
        /// <param name="newName"></param>
        void Rename(string newName);

        /// <summary>
        /// Remove the node from the diagram.  Usually justs calls RemoveNode on the OwnerData
        /// </summary>
        void RemoveFromDiagram();



        /// <summary>
        /// The name that was used when the last save occured.
        /// </summary>
        string OldName { get; set; }

        /// <summary>
        /// The items that should be persisted with this diagram node.
        /// </summary>
        IEnumerable<IDiagramNodeItem> PersistedItems { get; }

        IEnumerable<IGraphItem> GraphItems { get; } 

        bool IsNewNode { get; set; }
        bool IsExternal { get; }

        /// <summary>
        /// The location that is used when entering a new filter or sub-diagram.
        /// </summary>
        Vector2 DefaultLocation { get; }
        string GraphId { get; set; }
   
        string FullName { get; set; }


        /// <summary>
        /// Begin the rename process
        /// </summary>
        void BeginEditing();
        /// <summary>
        /// Apply changes to the renaming of the node.
        /// </summary>
        /// <returns>Could it successfully rename the node.</returns>
        bool EndEditing();

        void NodeAddedInFilter(IDiagramNode newNodeData);
    
    }
}