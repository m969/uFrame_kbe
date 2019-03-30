using System.CodeDom;
using System.Linq;
using uFrame.Editor.Compiling.CodeGen;
using uFrame.Editor.Configurations;

namespace uFrame.Editor.Compiling.CommonNodes
{
    [TemplateClass(TemplateLocation.DesignerFile)]
    public class EnumNodeGenerator : IClassTemplate<EnumNode>, ITemplateCustomFilename
    {
        public string OutputPath
        {
            get { return Path2.Combine("Enums"); }
        }

        public bool CanGenerate
        {
            get { return true; }
        }

        public void TemplateSetup()
        {
            Ctx.CurrentDeclaration.IsEnum = true;
            Ctx.CurrentDeclaration.BaseTypes.Clear();
            foreach (var item in Ctx.Data.Items)
            {
                this.Ctx.CurrentDeclaration.Members.Add(new CodeMemberField(this.Ctx.CurrentDeclaration.Name, item.Name));
            }
        }

        public TemplateContext<EnumNode> Ctx { get; set; }
        public string Filename { get { return Path2.Combine(Ctx.Data.Graph.Name, "Enums", Ctx.Data.Name + ".cs"); } }
    }
}