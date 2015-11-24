namespace LrpServer.Net
{
    public struct LrpQuoteEntry
    {
        public double Price;
        public double Volume;

        public LrpQuoteEntry(decimal price, double volume)
            : this()
        {
            this.Price = (double)price;
            this.Volume = volume;
        }
    }
}
