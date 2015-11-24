namespace SoftFX.Extended.Financial
{
    interface IPrecisionProvider
    {
        int GetCurrencyPrecision(string currency);
    }
}
