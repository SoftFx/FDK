namespace Mql2Fdk.Translator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    static class ExtensionUtils
    {
        public static void ReverseEach<T>(this IEnumerable<T> itemSource, Action<T> action)
        {
            itemSource.ReverseEachWithIndex((item, id) => action(item));
        }

        public static void ReverseEachWithIndex<T>(this IEnumerable<T> itemSource, Action<T, int> action)
        {
            var items = itemSource.ToArray();
            for (var index = items.Length - 1; index >= 0; index--)
            {
                var item = items[index];
                action(item, index);
            }
        }

        public static void Each<T>(this IEnumerable<T> itemSource, Action<T> action)
        {
            itemSource.EachWithIndex((item, id) => action(item));
        }

        public static void EachWithIndex<T>(this IEnumerable<T> itemSource, Action<T, int> action)
        {
            var items = itemSource.ToArray();
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                action(item, index);
            }
        }
    }
}