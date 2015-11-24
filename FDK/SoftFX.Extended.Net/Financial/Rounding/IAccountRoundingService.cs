namespace SoftFX.Extended.Financial
{
    interface IAccountRoundingService : IRoundingService
    {
        decimal RoundProfit(decimal profit);

        double RoundProfit(double profit);

        decimal RoundMargin(decimal margin);

        double RoundMargin(double margin);
    }
}
