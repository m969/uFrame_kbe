using System;
using uFrame.Editor.Documentation;

namespace uFrame.Editor.GraphUI
{
    public class TutorialStep : ITutorialStep
    {
        public TutorialStep( string name, Func<string> isDone)
            : this( name, isDone, null)
        {
        }

        public TutorialStep( string name, Func<string> isDone, Action doIt)
        {
         
            Name = name;
            IsDone = isDone;
            DoIt = doIt;
        }

        public string Name { get; set; }
        public Action DoIt { get; set; }
        public Func<string> IsDone { get; set; }
        public Action<IDocumentationBuilder> StepContent { get; set; }
        public string IsComplete { get; set; }
    }
}