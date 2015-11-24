namespace SoftFX.Lrp
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The class is used by LRP core for C++ client initialization.
    /// </summary>
    public unsafe static class LocalServer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="signature"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static int Initialize(string argument, string signature, LocalServerInvokeHandler handler)
        {
            var parameters = Parse(argument);
            var param = new IntPtr(long.Parse(parameters[Param]));
            var callback = new IntPtr(long.Parse(parameters[Callback]));
            var func = (CallBackFunc)Marshal.GetDelegateForFunctionPointer(callback, typeof(CallBackFunc));
            var invoke = Marshal.GetFunctionPointerForDelegate(handler);

            var buffer = new MemoryBuffer();
            buffer.WriteAString(signature);
            buffer.WriteAChar('\0');
            byte* ptr = (byte*)buffer.Data;
            ptr += sizeof(int);
            func(ptr, invoke.ToPointer(), param.ToPointer());
            buffer.Free();
            return 0;
        }

        static Dictionary<string, string> Parse(string argument)
        {
            var result = new Dictionary<string, string>();
            var parts = argument.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var element in parts)
            {
                var match = Pattern.Match(element);
                if (match.Success)
                {
                    var key = match.Groups[1].Value;
                    var value = match.Groups[2].Value;
                    result[key] = value;
                }
            }
            return result;
        }

        #region Fields

        static readonly char[] Separators = new char[] { ';' };
        static readonly Regex Pattern = new Regex("^([^=]+)=(.*)$");

        const string Param = "param";
        const string Callback = "callback";

        #endregion
    }
}
