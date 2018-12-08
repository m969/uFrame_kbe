using System.Collections.Generic;
using System.Linq;
using uFrame.Editor.Attributes;
using uFrame.Editor.Core;
using uFrame.Editor.Graphs.Data;
using uFrame.Json;

namespace uFrame.MVVM
{
    public class SubSystemNode : SubSystemNodeBase
    {
        [JsonProperty, InspectorProperty]
        public string SubSystemNamespace { get; set; }

        public override bool AllowInputs
        {
            get { return false; }
        }

        public override bool AllowOutputs
        {
            get { return false; }
        }

        public virtual IEnumerable<InstancesReference> AvailableInstances
        {
            get
            {
                foreach (var item in Instances)
                {
                    yield return item;
                }
            }
        }

        public override IEnumerable<IItem> PossibleInstances
        {
            get
            {
                foreach (var item in this.Repository.AllOf<IInstancesConnectable>())
                {
                    yield return new InstancesReference()
                    {
                        Repository = Repository,
                        Node = this,
                        Name = item.Name,
                        SourceIdentifier = item.Identifier
                    };
                }
            }
        }
    }

    public partial interface ISubSystemConnectable : IDiagramNodeItem, IConnectable
    {
    }
}
