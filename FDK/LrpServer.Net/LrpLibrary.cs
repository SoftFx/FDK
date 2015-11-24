namespace LrpServer.Net
{
    using System;
    using IOPath = System.IO.Path;

    /// <summary>
    /// 
    /// </summary>
    public static class LrpLibrary
    {
        /// <summary>
        /// 
        /// </summary>
        public static string Path { get; set; }

        static LrpLibrary()
        {

            var path = typeof(Native).Assembly.Location;
            path = IOPath.GetDirectoryName(path);
            path = IOPath.Combine(path, CppDllName);

#if DEBUG
            var value = Environment.GetEnvironmentVariable("FRE");
            if (null != value)
            {
                path = IOPath.Combine(value, CppDllName);
            }
#endif

            LrpLibrary.Path = path;
        }

        static string CppDllName
        {
            get
            {
                if (!Environment.Is64BitProcess)
                    return "LrpServer.x86.dll";
                else
                    return "LrpServer.x64.dll";
            }
        }
    }
}
