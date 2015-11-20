namespace SoftFX.Lrp.Implementation
{
    static class HResult
    {
        public const int S_OK = 0;
        public const int S_FALSE = 1;

        public static bool Succeeded(int status)
        {
            return status >= 0;
        }

        public static bool Failed(int status)
        {
            return status < 0;
        }
    }
}
