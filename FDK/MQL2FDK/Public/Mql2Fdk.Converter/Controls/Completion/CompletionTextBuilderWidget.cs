using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mql2Fdk.Converter.Controls.Completion
{
    class CompletionTextBuilderWidget
    {
        public StackPanel ResultWidget { get; private set; }
        public CompletionTextBuilderWidget()
        {
            ResultWidget= new StackPanel {Orientation = Orientation.Horizontal};
        }
        public void AddText(string text)
        {
            AddWidget(new TextBlock { Text = text });
        }
        public void AddTextWithColor(string text, Color color)
        {
            AddWidget(new TextBlock { Text = text,
                Foreground = new SolidColorBrush(color)
            });
        }

        void AddWidget(UIElement textBlock)
        {
            ResultWidget.Children.Add(textBlock);
        }
    }
}