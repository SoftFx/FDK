namespace SoftFX.Extended.Data
{
    using System;

    class FxOrder
    {
        public string OrderId { get; set; }
        public string ClientOrderId { get; set; }
        public string Symbol { get; set; }
        public int Type { get; set; }
        public TradeRecordSide Side { get; set; }
        public double Price { get; set; }
        public double? NewPrice { get; set; }
        public double? StopLoss { get; set; }
        public double? TakeProfit { get; set; }
        public double InitialVolume { get; set; }
        public double Volume { get; set; }
        public double Commission { get; set; }
        public double AgentCommission { get; set; }
        public double Swap { get; set; }
        public double? Profit { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public string Comment { get; set; }

        public FxOrder()
        {
            this.OrderId = string.Empty;
            this.ClientOrderId = string.Empty;
            this.Symbol = string.Empty;
            this.Comment = string.Empty;
        }

        public FxOrder(string id, string clientId, string symbol, int type, TradeRecordSide side, double? newPrice, double volume, double? stopLoss, double? takeProfit, DateTime? expiration, string comment)
            : this()
        {
            this.OrderId = id;
            this.ClientOrderId = clientId;
            this.Symbol = symbol;

            this.Type = type;
            this.Side = side;
            this.Price = 0;
            this.NewPrice = newPrice;
            this.Volume = volume;
            this.StopLoss = stopLoss;
            this.TakeProfit = takeProfit;
            this.Expiration = expiration;
            this.Comment = comment;
        }

        public FxOrder(string symbol, int type, TradeRecordSide side, double price, double volume, double? stopLoss, double? takeProfit, DateTime? expiration, string comment)
            : this()
        {
            this.Symbol = symbol;
            this.Type = type;
            this.Side = side;
            this.Price = price;
            this.Volume = volume;
            this.StopLoss = stopLoss;
            this.TakeProfit = takeProfit;
            this.Expiration = expiration;
            this.Comment = comment;
        }
    }
}
