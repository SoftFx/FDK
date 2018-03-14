using System.Collections.Generic;
using System.Runtime.Serialization;
using TickTrader.BusinessObjects;

namespace SoftFX.Extended.Reports
{
    using System;

    /// <summary>
    /// Daily account snapshot report
    /// </summary>
    public class DailyAccountSnapshotReport
    {
        internal DailyAccountSnapshotReport()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Timestamp { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string AccountId { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public AccountType Type { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public int Leverage { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double Balance { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string BalanceCurrency { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double Profit { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double Commission { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double AgentCommission { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double TotalCommission => Commission + AgentCommission;

        /// <summary>
        /// 
        /// </summary>
        public double Swap { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double TotalProfitLoss => Profit + TotalCommission + Swap;

        /// <summary>
        /// 
        /// </summary>
        public double Equity { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double Margin { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double MarginLevel { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBlocked { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double? BalanceCurrencyToUsdConversionRate { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double? UsdToBalanceCurrencyConversionRate { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double? ProfitCurrencyToUsdConversionRate { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double? UsdToProfitCurrencyConversionRate { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public AssetInfo[] Assets { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public string NextStreamPositionId { get; internal set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            return string.Format("AccountId = {0}; Type = {1}; Readonly = {2}; BalanceCurrency = {3}; Leverage = {4}; Balance = {5}; Equity = {6}; Margin = {7}", this.AccountId, this.Type, this.IsReadOnly, this.BalanceCurrency, this.Leverage, this.Balance, this.Equity, this.Margin);
        }
    }
}
