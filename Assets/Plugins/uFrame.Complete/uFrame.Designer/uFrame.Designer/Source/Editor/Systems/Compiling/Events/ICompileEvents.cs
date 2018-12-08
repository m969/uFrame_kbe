using uFrame.Editor.Compiling.CodeGen;
using uFrame.Editor.Database;
using uFrame.Editor.Database.Data;

namespace uFrame.Editor.Compiling.Events
{
    /// <summary>
    /// Events that are fired during the compilation process.
    /// </summary>
    public interface ICompileEvents
    {
        /// <summary>
        /// When the "Save & Compile" button is clicked. This is called first
        /// </summary>

        void PreCompile(IGraphConfiguration configuration, IDataRecord[] compilingRecords);


        /// <summary>
        /// When saving & compiling is complete.
        /// </summary>
        void PostCompile(IGraphConfiguration configuration, IDataRecord[] compilingRecords);

        /// <summary>
        /// When a file is generated, this method is called.
        /// </summary>
        void FileGenerated(CodeFileGenerator generator);
        void FileSkipped(CodeFileGenerator codeFileGenerator);
    }
}