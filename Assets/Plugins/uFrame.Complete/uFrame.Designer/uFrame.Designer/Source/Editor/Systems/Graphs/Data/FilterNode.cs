using uFrame.Editor.Database.Data;
using uFrame.Json;
using UnityEngine;

namespace uFrame.Editor.Graphs.Data
{
    public partial class FilterNode : GenericNode, IFilterItem
    {
        string IFilterItem.NodeId
        {
            get { return NodeId; }
            set { NodeId = value; }
        }

        IDiagramNode IFilterItem.Node
        {
            get { return this; }
        }

        [JsonProperty]
        public bool Collapsed
        {
            get { return _collapsed; }
            set { this.Changed("Collapsed", ref _collapsed, value); }
        }

        [JsonProperty]
        public string FilterId
        {
            get { return _filterId; }
            set { this.Changed("FilterId", ref _filterId, value); }
        }

        public override IGraphFilter Filter
        {
            get { return Repository.GetById<IGraphFilter>(FilterId); }
        }

        private string _filterId;
        private bool _collapsed;
        private Vector2 _position1;
        [JsonProperty]
        public Vector2 Position
        {
            get { return _position1; }
            set
            {
                this.Changed("Position", ref _position1, new Vector2(value.x < 0 ? 0 : value.x, value.y < 0 ? 0 : value.y));
            }
        }

        public override void RecordRemoved(IDataRecord record)
        {
            base.RecordRemoved(record);
            if (record.Identifier == FilterId)
            {
                record.Repository.Remove(this);
            }
        }
    }
}