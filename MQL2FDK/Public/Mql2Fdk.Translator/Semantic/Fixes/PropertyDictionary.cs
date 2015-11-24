using System.Collections.Generic;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    public static class PropertyDictionary
    {
        public static Dictionary<string, string> TypesProperty = new Dictionary<string, string>();
        public static Dictionary<string, string> PropertyContents = new Dictionary<string, string>();

        public static void SetProperty(string name, string typeName, string value)
        {
            TypesProperty[name] = typeName;
            PropertyContents[name] = value;
        }
    }
}