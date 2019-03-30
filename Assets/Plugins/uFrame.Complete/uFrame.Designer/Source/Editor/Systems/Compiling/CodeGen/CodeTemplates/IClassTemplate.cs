namespace uFrame.Editor.Compiling.CodeGen
{
    public interface IClassTemplate
    {
        string OutputPath { get; }
        bool CanGenerate { get; }
        void TemplateSetup();
    }

    public interface IClassTemplate<TData> : IClassTemplate
    {
        TemplateContext<TData> Ctx { get; set; }
        
    }
}