namespace FinancialExample
{
    using System;
    using System.ComponentModel;

    class FullSymbolEntry
    {
        public FullSymbolEntry(string symbol)
        {
            if (symbol == null)
                throw new ArgumentNullException("symbol");

            this.Symbol = symbol;
        }

        #region Properties

        [Category("Parameters")]
        public string Symbol { get; private set; }

        [Category("Parameters")]
        public double Bid
        {
            get
            {
                return this.bid;
            }
            set
            {
                this.bid = ValidatePrice(value);
            }
        }
        [Category("Parameters")]
        public double Ask
        {
            get
            {
                return ask;
            }
            set
            {
                this.ask = ValidatePrice(value);
            }
        }

        static double ValidatePrice(double value)
        {
            if (value >= 0 && value <= float.MaxValue)
            {
                return value;
            }

            var message = string.Format("Price should be from [0, {0}]", float.MaxValue);
            throw new ArgumentOutOfRangeException("value", value, message);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return this.Symbol;
        }

        #endregion

        #region Members

        double bid;
        double ask;

        #endregion
    }
}
