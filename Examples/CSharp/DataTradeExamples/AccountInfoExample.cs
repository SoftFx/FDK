namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class AccountInfoExample : Example
    {
        public AccountInfoExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            var info = this.Trade.Server.GetAccountInfo();
            Console.WriteLine(info);
            Console.ReadKey();
        }
    }
}
