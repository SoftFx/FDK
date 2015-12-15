namespace Mql2Fdk
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region Account Information

        /// <summary>
        /// Returns free margin value of the current account.
        /// </summary>
        /// <returns>free margin value</returns>
        protected double AccountFreeMargin()
        {
            var freeMargin = 0D;

            var snapshot = this.currentSnapshot;
            if (snapshot != null)
                freeMargin = snapshot.AccountInfo.Equity - snapshot.AccountInfo.Margin;

            return freeMargin;
        }

        /// <summary>
        /// Returns balance value of the current account (the amount of money on the account).
        /// </summary>
        /// <returns>equity value</returns>
        protected double AccountEquity()
        {
            var equity = 0D;

            var snapshot = this.currentSnapshot;
            if (snapshot != null)
                equity = snapshot.AccountInfo.Equity;

            return equity;
        }

        /// <summary>
        /// Returns margin value of the current account.
        /// </summary>
        /// <returns>margin value</returns>
        protected double AccountMargin()
        {
            var margin = 0D;

            var snapshot = this.currentSnapshot;
            if (snapshot != null)
                margin = snapshot.AccountInfo.Margin;

            return margin;
        }

        /// <summary>
        /// Returns profit value of the current account.
        /// </summary>
        /// <returns>profit value</returns>
        protected double AccountProfit()
        {
            var profit = 0D;

            var snapshot = this.currentSnapshot;
            if (snapshot != null)
                profit = snapshot.AccountInfo.Equity - snapshot.AccountInfo.Balance;

            return profit;
        }

        /// <summary>
        /// Returns balance value of the current account (the amount of money on the account).
        /// </summary>
        /// <returns>balance value</returns>
        protected double AccountBalance()
        {
            var balance = 0D;
            var snapshot = this.currentSnapshot;
            if (snapshot != null)
                balance = snapshot.AccountInfo.Balance;

            return balance;
        }

        /// <summary>
        /// Returns the brokerage company name where the current account was registered.
        /// </summary>
        /// <returns>account company</returns>
        protected string AccountCompany()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the current account name.
        /// </summary>
        /// <returns>account name</returns>
        protected string AccountName()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the current account number.
        /// </summary>
        /// <returns>account number</returns>
        protected int AccountNumber()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The method returns address of trade server.
        /// </summary>
        /// <returns></returns>
        protected string AccountServer()
        {
            return string.Empty;
        }

        /// <summary>
        /// Returns currency name of the current account.
        /// </summary>
        /// <returns></returns>
        protected string AccountCurrency()
        {
            var snapshot = this.currentSnapshot;
            if (snapshot != null)
                return snapshot.AccountInfo.Currency;

            return null;
        }

        /// <summary>
        /// Returns credit value of the current account. 
        /// </summary>
        /// <returns>Credit value</returns>
        protected double AccountCredit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns leverage of the current account.
        /// </summary>
        /// <returns>leverage value</returns>
        protected int AccountLeverage()
        {
            var leverage = 0;

            var snapshot = this.currentSnapshot;
            if (snapshot != null)
                leverage = snapshot.AccountInfo.Leverage;

            return leverage;
        }

        /// <summary>
        /// Returns the value of the Stop Out level.
        /// </summary>
        /// <returns>Stop Out level value</returns>
        protected int AccountStopoutLevel()
        {
            var level = 0;
            var snapshot = this.currentSnapshot;
            if (snapshot != null)
                level = (int)Math.Round(snapshot.AccountInfo.StopOutLevel * 100);

            return level;
        }

        /// <summary>
        /// Returns free margin that remains after the specified position has been opened at the current price on the current account. If the free margin is insufficient, an error 134 (ERR_NOT_ENOUGH_MONEY) will be generated. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="cmd"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        protected double AccountFreeMarginCheck(string symbol, int cmd, double volume)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}