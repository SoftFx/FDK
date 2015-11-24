namespace Lrp.Engine
{
    static class EmbeddedDataTypes
    {
        public static readonly DataType Int8 = new DataType("int8", "sbyte", "__int8", "Int8");
        public static readonly DataType Int16 = new DataType("int16", "short", "__int16", "Int16");
        public static readonly DataType Int32 = new DataType("int32", "int", "__int32", "Int32");
        public static readonly DataType Int64 = new DataType("int64", "long", "__int64", "Int64");


        public static readonly DataType UInt8 = new DataType("uint8", "byte", "unsigned __int8", "UInt8");
        public static readonly DataType UInt16 = new DataType("uint16", "ushort", "unsigned __int16", "UInt16");
        public static readonly DataType UInt32 = new DataType("uint32", "uint", "unsigned __int32", "UInt32");
        public static readonly DataType UInt64 = new DataType("uint64", "ulong", "unsigned __int64", "UInt64");

        public static readonly DataType Time = new DataType("time", "System.DateTime", "CDateTime", "Time");
        public static readonly DataType NullTime = new DataType("time?", "System.DateTime?", "NUllable<CDateTime>", "NullTime");

        public static readonly DataType Boolean = new DataType("bool", "bool", "bool", "Boolean");
        public static readonly DataType Single = new DataType("float", "float", "float", "Single");
        public static readonly DataType Double = new DataType("double", "double", "double", "Double");
        public static readonly DataType NullDouble = new DataType("double?", "double?", "NUllable<double>", "NullDouble");

        public static readonly DataType AString = new DataType("astring", "string", "std::string", "AString");
        public static readonly DataType WString = new DataType("wstring", "string", "std::wstring", "WString");

        public static readonly DataType AChar = new DataType("achar", "char", "char", "AChar");
        public static readonly DataType WChar = new DataType("wchar", "char", "wchar_t", "WChar");

        public static readonly DataType LocalPointer = new DataType("lptr", "SoftFX.Lrp.LPtr", "void*", "LocalPointer");
        public static readonly DataType RemotePointer = new DataType("rptr", "SoftFX.Lrp.RPtr", "void*", "RemotePointer");
        public static readonly DataType Void = new DataType("void", "void", "void", "Void");

        public static readonly DataType Raw = new DataType("raw", "Lrp.Core.MemoryBuffer", "MemoryBuffer", "Raw");


        public static readonly DataType AStringsArray = new DataType("astring[]", "string[]", "std::vector<std::string>", "AStringsArray");

    }
}
