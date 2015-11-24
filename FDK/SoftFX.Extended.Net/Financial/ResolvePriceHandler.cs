namespace SoftFX.Extended.Financial
{
    /// <summary>
    /// Delegate for price resolving request by financial calculator.
    /// </summary>
    /// <param name="symbol">resolving symbol, can not be null</param>
    /// <returns></returns>
    public delegate PriceEntry? ResolvePriceHandler(string symbol);
}
