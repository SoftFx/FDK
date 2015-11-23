using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mql2Fdk.Converter.Controls.Completion
{
    static class CompletionUtils
    {
        public static Uri GetUriToPicture(string genericPng)
        {
            return new Uri(
                @"pack://application:,,,/Mql2Fdk.CommonViews;component/Icons/Medium/" + genericPng);
        }

        public static ImageSource GetImageSource(this string genericPng)
        {
            return new BitmapImage(
                GetUriToPicture(genericPng));
        }


        public static object GetFieldInfoDescription(this FieldInfo fieldInfo)
        {
            var resBuilder = new CompletionTextBuilderWidget();

            resBuilder.AddTextWithColor(fieldInfo.Name, Colors.DarkMagenta);
            resBuilder.AddText(": ");
            resBuilder.AddTextWithColor(fieldInfo.FieldType.Name, Colors.MediumVioletRed);

            resBuilder.AddText("(field)");

            return resBuilder.ResultWidget;
        }

        public static object GetMethodInfoParams(this MethodInfo methodInfo)
        {
            var resBuilder = new CompletionTextBuilderWidget();
            var parameters = methodInfo.GetParameters();

            resBuilder.AddTextWithColor(methodInfo.Name, Colors.DarkMagenta);

            resBuilder.AddText("(");
            bool isFirst = true;
            foreach (var parameter in parameters)
            {
                if (!isFirst)
                    resBuilder.AddText(", ");
                else
                    isFirst = false;
                resBuilder.AddTextWithColor(parameter.Name, Colors.DarkSlateGray);
                resBuilder.AddText(": ");
                resBuilder.AddTextWithColor(parameter.ParameterType.Name,
                    Colors.SaddleBrown);
            }
            resBuilder.AddText(")");
            return resBuilder.ResultWidget;
        }
    }
}
