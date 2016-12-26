using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftFX.Extended
{
    public class TradeServerInfo
    {
        internal TradeServerInfo()
        {
        }

        public string CompanyName { get; internal set; }
        public string CompanyFullName { get; internal set; }
        public string CompanyDescription { get; internal set; }
        public string CompanyAddress { get; internal set; }
        public string CompanyPhone { get; internal set; }
        public string CompanyEmail { get; internal set; }
        public string CompanyWebSite { get; internal set; }
        public string ServerName { get; internal set; }
        public string ServerFullName { get; internal set; }
        public string ServerDescription { get; internal set; }
        public string ServerAddress { get; internal set; }
        public int? ServerFixFeedSslPort { get; internal set; }
        public int? ServerFixTradeSslPort { get; internal set; }
        public int? ServerWebSocketFeedPort { get; internal set; }
        public int? ServerWebSocketTradePort { get; internal set; }
        public int? ServerRestPort { get; internal set; }

        public override string ToString()
        {
            return String.Format("CompanyName = {0}", CompanyName);
        }
    }
}
