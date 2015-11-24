using System;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace Mql2Fdk.Converter.Controls.Completion
{
    /// <summary>
    /// Implements AvalonEdit ICompletionData interface to provide the entries in the completion drop down.
    /// </summary>
    public class CompletionParameters : ICompletionData
    {
        readonly MethodInfo _kind;

        public CompletionParameters(MethodInfo kind)
        {
            _kind = kind;
        }

        public ImageSource Image
        {
            get
            {
                return "package-x-generic.png".GetImageSource();
            }
        }


        public string Text { get; private set; }

        // Use this property if you want to show a fancy UIElement in the drop down list.
        public object Content
        {
            get { return Description; }
        }

        public object Description
        {
            get
            {
                return _kind.GetMethodInfoParams();
            }
        }

        public double Priority
        {
            get { return 0; }
        }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            var area = CodeEditorControl.TextLeftCaret(textArea.Document.Text, completionSegment.EndOffset);
            textArea.Document.Replace(completionSegment.EndOffset - area.Length, area.Length, Text);
        }
    }
}