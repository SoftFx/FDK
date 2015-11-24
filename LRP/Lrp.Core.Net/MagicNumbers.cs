namespace SoftFX.Lrp
{
    /// <summary>
    /// 
    /// </summary>
    public static class MagicNumbers
    {
        /// <summary>
        /// customer's bit
        /// </summary>
        private const int LRP_CODE_CUSTOMER = unchecked((int)0x20000000);
        /// <summary>
        /// customer's successful mask
        /// </summary>
        private const int LRP_CODE_SUCCESS = unchecked((int)(0x00000000 | LRP_CODE_CUSTOMER));
        /// <summary>
        /// customer's information mask
        /// </summary>
        private const int LRP_CODE_INFORMATION = unchecked((int)(0x40000000 | LRP_CODE_CUSTOMER));
        /// <summary>
        /// customer's warning mask
        /// </summary>
        private const int LRP_CODE_WARNING = unchecked((int)(0x80000000 | LRP_CODE_CUSTOMER));
        /// <summary>
        /// customer's error mask
        /// </summary>
        private const int LRP_CODE_ERROR = unchecked((int)(0xC0000000 | LRP_CODE_CUSTOMER));
        /// <summary>
        /// successful operation
        /// </summary>
        public const int S_OK = 0;
        /// <summary>
        /// common error
        /// </summary>
        public const int E_FAIL = unchecked((int)0x80004005);
        /// <summary>
        /// Method of invalid unknown component has been called
        /// </summary>
        public const int LRP_INVALID_COMPONENT_ID = (0 | LRP_CODE_ERROR);
        /// <summary>
        /// Unknown method has been called
        /// </summary>
        public const int LRP_INVALID_METHOD_ID = (1 | LRP_CODE_ERROR);
        /// <summary>
        /// Exception has been encountered
        /// </summary>
        public const int LRP_EXCEPTION = (2 | LRP_CODE_ERROR);

    }
}
