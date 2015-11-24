namespace Mql2Fdk.SharedLogic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public static class CommonExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> items, IEnumerable<T> toBeAdded)
        {
            foreach (var item in toBeAdded)
                items.Add(item);
        }

        public static string[] SplitByChar(this string text, char splittingChar)
        {
            return text.Split(new[] { splittingChar }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string RemoveIncluding(this string text, string textToSearch)
        {
            var index = text.IndexOf(textToSearch);
            if (index < 0)
                return string.Empty;
            return text.Remove(0, index + textToSearch.Length);
        }
    }
}
