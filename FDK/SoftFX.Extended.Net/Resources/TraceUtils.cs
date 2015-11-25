namespace SoftFX.Extended.Resources
{
    using System;
    using System.Diagnostics;
    static class TraceUtils
    {
        public static void WriteLine(String format, params object[] args)
        {
            Trace.WriteLine(string.Format(format, args));
            Console.WriteLine(string.Format(format, args));
        }
    }
}
