using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Mql2Fdk.EncodingTools;
using Microsoft.Win32;
using Mql2Fdk.Translator.CodeGenerator.Formatter;
using Mql2Fdk.Translator.Lexer.Preprocessor;
using Mql2Fdk.Translator.Translator;
using Mql2Fdk.Converter.Common;
using Mql2Fdk.Converter.Controls.FileTemplates;
using Mql2Fdk.Converter.Controls.Preferences;
using Mql2Fdk.SharedLogic;

namespace Mql2Fdk.Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        static Encoding _encoding = Encoding.Default;

        public MainWindow()
        {
            InitializeComponent();

            OutputEditor.OnGenerate.OnEvent += data => UpdateSource(EditorControl.TextEditor.Text);

            LoadSettings();
        }

        void LoadSettings()
        {
            Width = UserSettings.Default.Width;
            Height = UserSettings.Default.Height;
            Left = UserSettings.Default.StartX;
            Top = UserSettings.Default.StartY;

            IncludePaths.BlackListIncludeFile("stdlib.mqh");
            var includes = UserSettings.Default.IncludeDirectories.SplitByChar('|');
            foreach (var include in includes)
            {
                IncludePaths.AddDirectoryInclude(include);
            }
        
        }

        void UpdateSource(string text)
        {
            try
            {
                var translator = new Mq4Translator();
                translator.Parse(text);
                var code = translator.GenerateCode();
                var formattedCode = code.BeautifyCsFileSource();
                var errors = code.CsFileErrors().Select(err => err.ErrorPrettyPrinter());
                UiService.InvokeMainThread(() =>
                {
                    WriteError(string.Join(Environment.NewLine, errors));
                    OutputEditor.TextEditor.Text = formattedCode;
                });
            }
            catch (Exception ex)
            {
                UiService.InvokeMainThread(() =>
                {
                    var message = ex.Message;
                    WriteError(message);
                });
            }
        }

        void WriteError(string message)
        {
            ErrorList.Text = message;
        }

        void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void OnFileNew(object sender, RoutedEventArgs e)
        {
            var wizard = new NewFileWizard();
            wizard.ShowDialog();
            if(wizard.IsSuccessful)
            {
                EditorControl.TextEditor.Text = wizard.SelectedSource;
            }
        }

        void OnFileOpen(object sender, RoutedEventArgs e)
        {
            var fileOpenDialog = new OpenFileDialog
            {
                Filter = "Mq4 Files (*.mq4)|*.mq4|All files|*.*"
            };
            var result = fileOpenDialog.ShowDialog() ?? false;
            if (!result)
                return;
            var fileName = fileOpenDialog.FileName;
            IncludePaths.AddIncludeDirectoryOfFile(fileName);
            var task = new Thread(() => UpdateViewsFromFileName(fileName));
            task.Start();
        }

        void UpdateViewsFromFileName(string fileName)
        {
            // using automatic (better) detection of the encoding
            // http://www.codeproject.com/Articles/17201/Detect-Encoding-for-In-and-Outgoing-Text

            var fileContent = ReadFileContent(fileName);
			UiService.InvokeMainThread(UpdateContent, fileContent);
            UiService.InvokeMainThread(() => OutputEditor.OnGenerate.Notify(fileContent));
        }
		void UpdateContent(string fileContent)
		{
			EditorControl.TextEditor.Text = fileContent;
		}

        public static string ReadFileContent(string fileName)
        {
            var fileContent = _encoding == null
                                  ? fileName.ReadFileContent()
                                  : File.ReadAllText(fileName, _encoding);
            return fileContent;
        }

        void OnFileSave(object sender, RoutedEventArgs e)
        {
            var selectedFileName = UserSettings.Default.SelectedFileName;
            if(!string.IsNullOrEmpty(selectedFileName))
            {
                SaveFileNameToDisk(selectedFileName, OutputEditor.TextEditor.Text);
                return;
            }
            FileSaveAs();
        }
        #region Encodings
        void OnDefaultEncoding(object sender, RoutedEventArgs e)
        {
            _encoding = Encoding.Default;
        }

        void OnUtf8Encoding(object sender, RoutedEventArgs e)
        {
            _encoding = Encoding.UTF8;
        }

        void OnAutoDetectEncoding(object sender, RoutedEventArgs e)
        {
            _encoding = null;
        }

        void OnUtf32Encoding(object sender, RoutedEventArgs e)
        {
            _encoding = Encoding.UTF32;
        }

        void OnUnicodeEncoding(object sender, RoutedEventArgs e)
        {
            _encoding = Encoding.Unicode;
        }
        #endregion

        void OnPreferences(object sender, RoutedEventArgs e)
        {
            var preferencesWindow = new PreferencesWindow();
            preferencesWindow.ShowDialog();
        }

        void OnTabChange(object sender, SelectionChangedEventArgs e)
        {
            OutputEditor.OnGenerate.Notify();
        }

        void OnClosingMainWindow(object sender, CancelEventArgs e)
        {
            UserSettings.Default.StartX = (int)Left;
            UserSettings.Default.StartY = (int)Top;
            UserSettings.Default.Width = (int)Width;
            UserSettings.Default.Height = (int)Height;
            UserSettings.Default.SelectedFileName = string.Empty;
            UserSettings.Default.SelectedOutputFileName = string.Empty;
            UserSettings.Default.Save();
        }

        void OnFileSaveAs(object sender, ExecutedRoutedEventArgs e)
        {
            FileSaveAs();
        }

        void FileSaveAs()
        {
            var fileName = OpenFileDialogGetFile("C# Files (*.cs)|*.cs|All files|*.*");

            if (string.IsNullOrEmpty(fileName))
                return;
            SaveFileNameToDisk(fileName, OutputEditor.TextEditor.Text);
        }

        static string OpenFileDialogGetFile(string filter)
        {
            var fileOpenDialog = new SaveFileDialog
            {
                Filter = filter
            };
            var result = fileOpenDialog.ShowDialog() ?? false;
            if (!result) return string.Empty;
            var fileName = fileOpenDialog.FileName;
            return fileName;
        }
        void SaveFileNameToDisk(string fileName, string contents)
        {
            if (string.IsNullOrEmpty(fileName))
                return;
            if (_encoding == null)
                // using automatic (better) detection of the encoding
                // http://www.codeproject.com/Articles/17201/Detect-Encoding-for-In-and-Outgoing-Text
                fileName.WriteFileContent(contents);
            else
                File.WriteAllText(fileName, contents, _encoding);
        }

        void OnFileSaveMql(object sender, RoutedEventArgs routedEventArgs)
        {
            var outputFileName = UserSettings.Default.SelectedOutputFileName;
            if (string.IsNullOrEmpty(outputFileName))
            {
                outputFileName = SaveAsMql();
                if(string.IsNullOrEmpty(outputFileName))
                    return;
            }
            SaveFileNameToDisk(outputFileName, EditorControl.TextEditor.Text);
        }

        string SaveAsMql()
        {
            var fileName = OpenFileDialogGetFile("Mql Files (*.mql)|*.mql|All files|*.*");
            UserSettings.Default.SelectedOutputFileName = fileName;
            
            if(string.IsNullOrEmpty(fileName))
                return string.Empty;
            var outputFileName = fileName;
            SaveFileNameToDisk(outputFileName, EditorControl.TextEditor.Text);
            return outputFileName;
        }

        void OnFileSaveAsMql(object sender, RoutedEventArgs e)
        {
            SaveFileNameToDisk(SaveAsMql(), EditorControl.TextEditor.Text);            
        }
    }
}