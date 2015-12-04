using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFX.Extended.Functional.Tests
{
    internal class TestContext
    {
        public TestContext(AccountType accType)
        {
            this.AccountType = accType;
            if (AccountType == AccountType.Gross)
                Symbol = "EURUSD";
            else
                Symbol = "EUR/USD";
            VeryLowPrice = 0.5;
            VeryHighPrice = 1.5;
            Volume = 10000;
            this.Comment = string.Format("UnitTest for {0} account", accType.ToString());
        }
        public AccountType  AccountType { get; private set; }
        public string       Symbol { get; private set; }   
        public double VeryLowPrice { get; private set; }
        public double VeryHighPrice { get; private set; }
        public double Volume { get; private set; }
        public string Comment { get; private set; }

    }
}
