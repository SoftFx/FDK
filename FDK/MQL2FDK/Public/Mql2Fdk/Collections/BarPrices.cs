namespace Mql2Fdk
{
    /// <summary>
    /// 
    /// </summary>
    public class BarPrices
    {
        internal BarPrices(BarPriceType type, MqlAdapter adapter)
        {
            this.type = type;
            this.adapter = adapter;
        }

        /// <summary>
        /// Gets 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                var snapshot = this.adapter.CurrentSnapshot;
                var bar = snapshot.Bars[index];

                if (this.type == BarPriceType.Open)
                    return bar.Open;

                if (this.type == BarPriceType.Close)
                    return bar.Close;

                if (this.type == BarPriceType.Low)
                    return bar.Low;

                return bar.High;
            }
        }

        /// <summary>
        /// Bars count.
        /// </summary>
        public int Count
        {
            get
            {
                var snapshot = this.adapter.CurrentSnapshot;
                return snapshot.Bars.Length;
            }
        }

        #region Members

        readonly BarPriceType type;
        readonly MqlAdapter adapter;

        #endregion
    }
}
