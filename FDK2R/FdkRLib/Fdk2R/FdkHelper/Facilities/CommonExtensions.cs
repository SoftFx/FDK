using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace FdkMinimal.Facilities
{
	public static class CommonExtensions
{	
		public static void Each<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (var it in items)
				action (it);
		}

        public static void RemoveAll<K,V>(this Dictionary<K,V> dictionary, Func<KeyValuePair<K,V>, bool> predicate)
        {
            var toRemove = new List<K>();
            foreach (var item in dictionary)
            {
                if(predicate(item))
                {
                    toRemove.Add(item.Key);
                }
            }
            foreach (var item in toRemove)
            {
                dictionary.Remove(item);
            }
        }

        public static List<Tuple<string, string>> GetTupleOfValues(string command)
        {
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(command);
            var listValues = new List<Tuple<string, string>>(values.Count);
            listValues.AddRange(values.Select(val => new Tuple<string, string>(val.Key, val.Value)));
            return listValues;
        }
        public static List<Tuple<string, string>> ReadNodeAsPairs(YamlMappingNode mappingNode)
		{
			var result = new List<Tuple<string, string>>();

			foreach (var criteriaParamNode in mappingNode.Children)
			{
				var keyNode = (YamlScalarNode)criteriaParamNode.Key;
				var valueNode = (YamlScalarNode)criteriaParamNode.Value;
                var item = new Tuple<string, string>(keyNode.Value, valueNode.Value);
				result.Add (item);
			}
			return result;
		}
        public static string[] SplitTrim(this string text, char splitChar)
        {
            var tokens = text.Split(new[] { splitChar }, StringSplitOptions.RemoveEmptyEntries)
                .Select(tok => tok.Trim())
                .ToArray();
            return tokens;
        }
        
        public static bool StartsWithOrdinal(this string text, string textToStart)
        {
			return text.StartsWith(textToStart, StringComparison.Ordinal);
        }
        
		public static bool EndsWithOrdinal(this string text, string textToStart)
        {
			return text.EndsWith(textToStart, StringComparison.Ordinal);
        }
        
        public static YamlSequenceNode GetNodeByName(YamlMappingNode mapping, string childName)
        {
            var criteriaRootConfigNode = (YamlSequenceNode)mapping.Children.FirstOrDefault(it =>
            {
                var scalar = it.Key as YamlScalarNode;
                if (scalar == null)
                    return false;

                return scalar.Value == childName;
            }).Value;
            return criteriaRootConfigNode;
        }
        public static YamlMappingNode GetMappingNodeByName(YamlMappingNode mapping, string childName)
        {
            var criteriaRootConfigNode = mapping.Children.FirstOrDefault(it =>
            {
                var scalar = it.Key as YamlScalarNode;
                if (scalar == null)
                    return false;

                return scalar.Value == childName;
            }).Value;
            return (YamlMappingNode)criteriaRootConfigNode;
        }
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime PreviousSunday(this DateTime startingDate)
        {
            DateTime previousWeekStart = startingDate.AddDays(-(int)startingDate.DayOfWeek -7); ;
            DateTime previousWeekEnd = startingDate.AddDays(-1);
            return previousWeekStart;
        }
        
        public static DateTime StartOfMonth(this DateTime now)
        {
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            return startOfMonth;
        }
        public static DateTime StartOfYear(this DateTime now)
        {
            var startOfMonth = new DateTime(now.Year, 1, 1);
            return startOfMonth;
        }
        public static DateTime StartOfDay(this DateTime now)
        {
            var startOfMonth = new DateTime(now.Year, now.Month, now.Day);
            return startOfMonth;
        }
    }
}

