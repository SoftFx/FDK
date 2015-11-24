using System;
using System.Collections.Generic;
using System.Linq;

namespace RHost
{
    public static class FdkVars
    {
        static FdkVars()
        {

            DiffFromUtc = DateTime.Now - DateTime.UtcNow;
        }

        private static readonly Dictionary<string, object> Vars = new Dictionary<string, object>();

        static TimeSpan DiffFromUtc;


        public static DateTime AddUtc(this DateTime utcTime)
        {
            var result = TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, TimeZoneInfo.Local);
            return result;
        }

        public static string RegisterVariable(object data, string prefix)
        {
            var pos = 0;
            if (string.IsNullOrEmpty(prefix))
                prefix = "fdk_";
            while (Vars.ContainsKey(string.Format("{0}_{1}", prefix, pos)))
            {
                pos++;
            }
            var varName = string.Format("{0}_{1}", prefix, pos);
            Vars[varName] = data;
            return varName;
        }
        
        public static string[] GetVarNames()
        {
			return Vars.Keys.ToArray();
		}

        public static void Unregister(string varName)
        {
            Vars.Remove(varName);
            //Pushes full GC
            GC.Collect(2);
        }

        public static void ClearAll()
        {
            Vars.Clear();
        }

        public static T GetValue<T>(string varName)
        {
            object result;
            if (!Vars.TryGetValue(varName, out result))
                return default(T);
            return (T) result;
        }
    }
}