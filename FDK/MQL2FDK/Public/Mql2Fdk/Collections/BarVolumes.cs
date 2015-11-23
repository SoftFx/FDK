namespace Mql2Fdk
{
    /// <summary>
    /// 
    /// </summary>
    public class BarVolumes
    {
        internal BarVolumes(MqlAdapter adapter)
        {
            this.adapter = adapter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                var snapshot = this.adapter.CurrentSnapshot;
                return snapshot.Bars[index].Volume;
            }
        }

        #region Members

        readonly MqlAdapter adapter;

        #endregion
    }
}
