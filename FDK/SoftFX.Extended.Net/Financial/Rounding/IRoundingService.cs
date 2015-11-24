namespace SoftFX.Extended.Financial
{
    interface IRoundingService
    {
        decimal RoundProfit(string currency, decimal profit);

        double RoundProfit(string currency, double profit);

        decimal RoundMargin(string currency, decimal margin);

        double RoundMargin(string currency, double margin);
    }
}
