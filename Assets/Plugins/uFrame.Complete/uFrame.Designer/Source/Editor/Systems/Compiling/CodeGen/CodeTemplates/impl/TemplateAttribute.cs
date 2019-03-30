using System;
using System.Reflection;

namespace uFrame.Editor.Compiling.CodeGen
{
    public abstract class TemplateAttribute : Attribute
    {
        public virtual int Priority
        {
            get { return 0; }
        }
        public virtual bool CanGenerate(object templateInstance, MemberInfo info, TemplateContext ctx)
        {
            return true;
        }

        public virtual void Modify(object templateInstance, MemberInfo info, TemplateContext ctx)
        {
            
        }
    }
}