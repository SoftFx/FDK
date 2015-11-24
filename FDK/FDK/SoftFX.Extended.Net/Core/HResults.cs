namespace SoftFX.Extended.Core
{
    static class HResults
    {
        #region Standard Constants

        public const int E_FAIL =               unchecked((int)0x80000008);
        public const int S_OK =    0;
        public const int S_FALSE = 1;

        #endregion

        #region Standard Error Constants

        public const int E_POINTER =            unchecked((int)0x80000005);
        public const int E_INVALIDARG =         unchecked((int)0x80000003);
        public const int E_OUTOFMEMORY =        unchecked((int)0x8007000E);
        public const int E_NOTIMPL =            unchecked((int)0x80000001);

        #endregion

        #region Constants

        // customer's bit
        public const int FX_CODE_CUSTOMER =     unchecked((int)0x20000000);

        public const int FX_CODE_SUCCESS =      unchecked((int)(0x00000000 | FX_CODE_CUSTOMER));
        public const int FX_CODE_INFORMATION =  unchecked((int)(0x40000000 | FX_CODE_CUSTOMER));
        public const int FX_CODE_WARNING =      unchecked((int)(0x80000000 | FX_CODE_CUSTOMER));
        public const int FX_CODE_ERROR =        unchecked((int)(0xC0000000 | FX_CODE_CUSTOMER));

        public const int FX_CODE_START = 0;

        public const int FX_CODE_ERROR_SEND =           ((FX_CODE_START + 0) | FX_CODE_ERROR);
        public const int FX_CODE_ERROR_TIMEOUT =        ((FX_CODE_START + 1) | FX_CODE_ERROR);
        public const int FX_CODE_ERROR_RECEIVE =        ((FX_CODE_START + 2) | FX_CODE_ERROR);
        public const int FX_CODE_ERROR_EXCEPTION =      ((FX_CODE_START + 3) | FX_CODE_ERROR);
        public const int FX_CODE_ERROR_REJECT =         ((FX_CODE_START + 7) | FX_CODE_ERROR);
        public const int FX_CODE_ERROR_LOGOUT =         ((FX_CODE_START + 8) | FX_CODE_ERROR);
        public const int FX_CODE_ERROR_INVALID_HANDLE = ((FX_CODE_START + 9) | FX_CODE_ERROR);

        #endregion

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
