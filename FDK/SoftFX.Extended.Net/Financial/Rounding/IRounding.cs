namespace SoftFX.Extended.Financial
{
    interface IRounding
    {
        decimal RoundProfit(int precision, decimal profit);

        double RoundProfit(int precision, double profit);

        decimal RoundMargin(int precision, decimal margin);

        double RoundMargin(int precision, double margin);
    }
}
