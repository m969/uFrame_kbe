namespace uFrame.Editor.Configurations
{
    public class ConfigProxy : GraphItemAttribute
    {
        public SectionVisibility Visibility { get; set; }

        public ConfigProxy(SectionVisibility visibility)
        {
            Visibility = visibility;
        }
    }
}