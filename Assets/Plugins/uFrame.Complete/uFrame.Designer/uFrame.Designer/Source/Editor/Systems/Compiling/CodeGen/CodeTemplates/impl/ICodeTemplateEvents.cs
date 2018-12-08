using System.CodeDom;

namespace uFrame.Editor.Compiling.CodeGen
{
    public interface ICodeTemplateEvents
    {
        void PropertyAdded(object template, TemplateContext templateContext, CodeMemberProperty codeMemberProperty);
        void MethodAdded(object template, TemplateContext templateContext, CodeMemberMethod codeMemberMethod);

        void ConstructorAdded(object template, TemplateContext templateContext, CodeConstructor codeConstructor);
        void TemplateGenerating(object templateClass, TemplateContext templateContext);
    }
}