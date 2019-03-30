using uFrame.Editor.Attributes;
using uFrame.Editor.Graphs.Data;
using uFrame.Json;

namespace uFrame.Editor.Compiling.CommonNodes
{
    public class ScreenshotNode : GenericNode
    {
        private int _width = 100;
        private int _height = 100;
        [InspectorProperty]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        [JsonProperty, InspectorProperty]
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        [JsonProperty, InspectorProperty]
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
    }
}