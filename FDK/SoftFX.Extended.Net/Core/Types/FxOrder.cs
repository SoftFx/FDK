namespace SoftFX.Extended.Data
{
    using System;

    class FxOrder
    {
        public string OrderId { get; set; }
        public string ClientOrderId { get; set; }
        public string Symbol { get; set; }
        public int InitialType { get; set; }
        public int Type { get; set; }
        public TradeRecordSide Side { get; set; }
        public double InitialVolume { get; set; }
        public double? Volume { get; set; }
        public double? MaxVisibleVolume { get; set; }
        public double? Price { get; set; }
        public double? StopPrice { get; set; }
        public double? StopLoss { get; set; }
        public double? TakeProfit { get; set; }
        public double Commission { get; set; }
        public double AgentCommission { get; set; }
        public double Swap { get; set; }
        public double? Profit { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public string Comment { get; set; }
        public string Tag { get; set; }
        public int? Magic { get; set; }
        public bool IsReducedOpenCommission { get; set; }
        public bool IsReducedCloseCommission { get; set; }
        public bool ImmediateOrCancel { get; set; }
        public bool MarketWithSlippage { get; set; }
        public bool? IOCOverride { get; set; }
        public bool? IFMOverride { get; set; }

        public FxOrder()
        {
            this.OrderId = string.Empty;
            this.ClientOrderId = string.Empty;
            this.Symbol = string.Empty;
            this.Comment = string.Empty;
            this.Tag = string.Empty;
        }

        public FxOrder(string symbol, int type, TradeRecordSide side, double volume, double? maxVisibleVolume, double? price, double? stopPrice, double? stopLoss, double? takeProfit, DateTime? expiration, string comment, string tag, int? magic, bool? IOCOverride, bool? IFMOverride)
            : this()
        {
            this.Symbol = symbol;
            this.Type = type;
            this.Side = side;
            this.Volume = volume;
            this.MaxVisibleVolume = maxVisibleVolume;
            this.Price = price;
            this.StopPrice = stopPrice;
            this.StopLoss = stopLoss;
            this.TakeProfit = takeProfit;
            this.Expiration = expiration;
            this.Comment = comment;
            this.Tag = tag;
            this.Magic = magic;
            this.IOCOverride = IOCOverride;
            this.IFMOverride = IFMOverride;
        }

        public FxOrder(string id, string clientId, string symbol, int type, TradeRecordSide side, double? newVolume, double? newMaxVisibleVolume, double? newPrice, double? newStopPrice, double? stopLoss, double? takeProfit, DateTime? expiration, string comment, string tag, int? magic, bool? IOCOverride, bool? IFMOverride)
            : this()
        {
            this.OrderId = id;
            this.ClientOrderId = clientId;
            this.Symbol = symbol;
            this.Type = type;
            this.Side = side;
            this.Volume = newVolume;
            this.MaxVisibleVolume = newMaxVisibleVolume;
            this.Price = newPrice;
            this.StopPrice = newStopPrice;
            this.StopLoss = stopLoss;
            this.TakeProfit = takeProfit;
            this.Expiration = expiration;
            this.Comment = comment;
            this.Tag = tag;
            this.Magic = magic;
            this.IOCOverride = IOCOverride;
            this.IFMOverride = IFMOverride;
        }
    }
}
