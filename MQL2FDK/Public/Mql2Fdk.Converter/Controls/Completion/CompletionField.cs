using System;
using System.Reflection;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace Mql2Fdk.Converter.Controls.Completion
{
    /// <summary>
    /// Implements AvalonEdit ICompletionData interface to provide the entries in the completion drop down.
    /// </summary>
    public class CompletionField : ICompletionData
    {
        readonly FieldInfo _kind;

        public CompletionField(FieldInfo kind)
        {
            _kind = kind;
            Text = kind.Name;
        }

        public System.Windows.Media.ImageSource Image
        {
            get
            {
                return "application-x-executable.png".GetImageSource();
            }
        }

        public string Text { get; private set; }

        // Use this property if you want to show a fancy UIElement in the drop down list.
        public object Content
        {
            get
            {
                return
                    _kind.GetFieldInfoDescription();
            }
        }

        public object Description
        {
            get
            {
                return
                    _kind.GetFieldInfoDescription();
            }
        }

        public double Priority { get { return 0; } }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            var area = CodeEditorControl.TextLeftCaret(textArea.Document.Text, completionSegment.EndOffset);
            textArea.Document.Replace(completionSegment.EndOffset - area.Length, area.Length, Text);
        }
    }
}