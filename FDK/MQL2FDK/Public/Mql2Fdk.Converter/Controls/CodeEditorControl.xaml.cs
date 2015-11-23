using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Highlighting;
using Mql2Fdk;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Converter.Common;
using Mql2Fdk.Converter.Controls.Completion;

namespace Mql2Fdk.Converter.Controls
{
    /// <summary>
    /// Interaction logic for CodeEditorControl.xaml
    /// </summary>
    public partial class CodeEditorControl
    {
        public CodeEditorControl()
        {
            InitializeComponent();
            TextEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C++");
            TextEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;
            TextEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            BuildTypeCache();


            _completionTimer.Elapsed += (s, ev) => UiService.InvokeMainThread(() =>
            {
                var text = TextLeftCaret(TextEditor.Text, TextEditor.CaretOffset);
                if (!string.IsNullOrEmpty(text))
                    BuildFunctionLists(text);

                _completionTimer.Enabled = false;
            });

            _completionTimer.AutoReset = false;
            _completionTimer.Enabled = false;

            _completionTimer.Interval = UserSettings.Default.TimeToCompletion;
        }

        readonly SortedDictionary<string, MethodInfo> _functions = new SortedDictionary<string, MethodInfo>();
        readonly SortedDictionary<string, FieldInfo> _fields = new SortedDictionary<string, FieldInfo>();
        void BuildTypeCache()
        {
            var scanTypes = typeof(MqlAdapter);
            var methodList = scanTypes.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).ToList();
            methodList.AddRange(scanTypes.GetMethods(BindingFlags.NonPublic | BindingFlags.Static));
            foreach (var info in methodList)
            {
                _functions[info.Name] = info;
            }


            var fieldList = scanTypes.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).ToList();
            fieldList.AddRange(scanTypes.GetFields(BindingFlags.NonPublic | BindingFlags.Static));
            foreach (var info in fieldList)
            {
                _fields[info.Name] = info;
            }
        }

        public UiEvent TextChangedEvent = new UiEvent();

        void OnTextChanged(object sender, EventArgs e)
        {
            TextChangedEvent.Notify(TextEditor.Text);
        }


        CompletionWindow _completionWindow;
        readonly Timer _completionTimer = new Timer();

        public static string TextLeftCaret(string text, int caretOffset)
        {
            try
            {
                if (text.Length == 0)
                    return "";
                var result = "";
                for (var pos = caretOffset - 1; pos >= 0; pos--)
                {
                    if (!UnicodeIdentifierMatcher.IsValidIdNotFirstChar(text[pos]))
                        return result;
                    result = text[pos] + result;
                }
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }

        void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            _completionTimer.Enabled = true;
        }

        void BuildFunctionLists(string text)
        {
            var completionData = BuildCompletionData(text);

            if (completionData.Count == 0)
                return;
            // open code completion after the user has pressed dot:
            _completionWindow = new CompletionWindow(TextEditor.TextArea);
            // provide AvalonEdit with the data:
            var data = _completionWindow.CompletionList.CompletionData;
            foreach (var itemData in completionData)
            {
                data.Add(itemData);
            }
            _completionWindow.Show();
            _completionWindow.Closed += delegate { _completionWindow = null; };
        }

        List<ICompletionData> BuildCompletionData(string text)
        {
            var completionData = new List<ICompletionData>();
            if (Mq4ReservedWords.GetReservedWordKind(text) != TokenKind.None)
                return completionData;

            var splitSearchForText = SplitIntoCamelHumps(text);

            completionData.AddRange(
                _functions
                .Where(fn => MatchCamelHumps(fn.Key, splitSearchForText))
                .Select(fnName => new CompletionFunction(fnName.Value)));

            completionData.AddRange(
                _fields
                .Where(fn => MatchCamelHumps(fn.Key, splitSearchForText))
                .Select(fldName => new CompletionField(fldName.Value)));
            return completionData;
        }

        static bool MatchCamelHumps(string bigText, string[] searchFor)
        {
            var splitBigText = SplitIntoCamelHumps(bigText);
            if (searchFor.Length > splitBigText.Length)
                return false;
            return !searchFor.Where((t, i) => !splitBigText[i].StartsWith(t)).Any();
        }

        static string[] SplitIntoCamelHumps(string bigText)
        {
            return Regex.Replace(bigText, "(\\B[A-Z])", " $1")
                .Split(' ')
                .Select(w => w.ToUpper()).ToArray();
        }


        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && _completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    _completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            // do not set e.Handled=true - we still want to insert the character that was typed
        }

        void HandleDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            // Note that you can have more than one file.
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0)
                return;
            TextEditor.Text = MainWindow.ReadFileContent(files[0]);
        }
    }
}