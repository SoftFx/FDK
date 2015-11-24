namespace SoftFX.Extended
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using SoftFX.Extended.Events;

    sealed class ConnectionTester : IDisposable
    {
        public ConnectionTester(ConnectionStringBuilder builder)
        {
            this.Builder = builder;
        }

        #region Properties

        public ConnectionStringBuilder Builder { get; private set; }

        #endregion

        #region Methods

        public void Start(string username, string password)
        {
            if (this.dataClient != null)
                throw new InvalidOperationException("Start method can be called only once.");

            var builder = this.Builder;
            
            builder.Username = username;
            builder.Password = password;
            
            var connectionString = builder.ToString();
            
            builder.Username = string.Empty;
            builder.Password = string.Empty;

            this.dataClient = new DataFeed(connectionString);

            this.dataClient.Logout += this.OnLogout;
            this.dataClient.Start();
        }

        public void StartTrade()
        {
            if (this.dataClient != null)
                throw new InvalidOperationException("Start method can be called only once.");

            var connectionString = this.Builder.ToString();
            var trade = new DataTrade(connectionString);
            trade.AccountInfo += this.OnAccountInfo;
            this.dataClient = trade;
            this.dataClient.Start();
        }

        public void StartFeed()
        {
            if (this.dataClient != null)
                throw new InvalidOperationException("Start method can be called only once.");

            var connectionString = this.Builder.ToString();
            var feed = new DataFeed(connectionString);
            feed.SymbolInfo += this.OnSymbolInfo;
            this.dataClient = feed;
            this.dataClient.Start();
        }

        #endregion

        #region Event Handlers

        void OnLogout(object sender, LogoutEventArgs e)
        {
            var dataClient = (DataClient)sender;
            dataClient.Logout -= this.OnLogout;

            if (e.Reason == LogoutReason.InvalidCredentials)
                this.eventArgs = e;

            this.syncEvent.Set();
        }

        void OnAccountInfo(object sender, AccountInfoEventArgs e)
        {
            var trade = (DataTrade)sender;
            trade.AccountInfo -= this.OnAccountInfo;
            this.eventArgs = e;
            this.syncEvent.Set();
        }

        void OnSymbolInfo(object sender, SymbolInfoEventArgs e)
        {
            var feed = (DataFeed)sender;
            feed.SymbolInfo -= this.OnSymbolInfo;
            this.eventArgs = e;
            this.syncEvent.Set();
        }

        #endregion

        #region Other

        public void Dispose()
        {
            var connection = this.dataClient;
            if (connection != null)
            {
                this.dataClient = null;
                connection.Dispose();
            }
        }

        public void WaitFor(int timeoutInMs)
        {
            this.syncEvent.WaitOne(timeoutInMs);
        }

        public void CollectValidConnectionStrings(ICollection<ConnectionStringBuilder> builders)
        {
            if (this.eventArgs != null)
            {
                builders.Add(this.Builder);
            }
        }
        
        #endregion

        #region Members

        DataClient dataClient;
        EventArgs eventArgs;
        readonly AutoResetEvent syncEvent = new AutoResetEvent(false);

        #endregion
    }
}
