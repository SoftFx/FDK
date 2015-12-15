namespace RealTimeLevel2
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            if (this.fixConnectionStringBuilder == null)
            {
                this.fixConnectionStringBuilder = new FixConnectionStringBuilder();
            }

            this.InitializeComponent();
            this.FormClosed += this.OnFormClosed;
        }

        #region Event Handlers

        void OnSave(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        void OnStart(object sender, EventArgs e)
        {
            this.m_connectionParameters.Enabled = false;

            this.m_save.Enabled = false;
            this.m_start.Enabled = false;
            this.m_stop.Enabled = true;

            try
            {
                this.fixConnectionStringBuilder.Address = this.m_address.Text;
                this.fixConnectionStringBuilder.Port = (int)this.m_port.Value;
                this.fixConnectionStringBuilder.Username = this.m_username.Text;
                this.fixConnectionStringBuilder.Password = this.m_password.Text;
                this.fixConnectionStringBuilder.SecureConnection = m_ssl.Checked;

                var connectionString = this.fixConnectionStringBuilder.ToString();
                this.datafeed = new DataFeed(connectionString);
                this.datafeed.SymbolInfo += this.OnSymbolInfo;
                this.datafeed.Tick += this.OnTick;
                this.datafeed.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.OnStop(sender, e);
            }
        }

        void OnTick(object sender, TickEventArgs e)
        {
            lock (this.synchronizer)
            {
                var quote = e.Tick;
                this.symbolToLevel2[quote.Symbol] = quote;
            }
        }

        void OnSymbolInfo(object sender, SymbolInfoEventArgs e)
        {
            var feed = (DataFeed)sender;
            lock (this.synchronizer)
            {
                this.symbolToInfo.Clear();
                this.symbolToLevel2.Clear();

                foreach (var element in e.Information)
                {
                    this.symbolToInfo[element.Name] = element;
                }
            }

            Action action = this.RefreshSymbols;
            this.Invoke(action);

            var symbols = new List<string>(e.Information.Length);
            foreach (var element in e.Information)
            {
                symbols.Add(element.Name);
            }
            feed.Server.SubscribeToQuotes(symbols, 5);
        }

        void OnStop(object sender, EventArgs e)
        {
            if (datafeed != null)
            {
                this.datafeed.Stop();
                this.datafeed.Dispose();
                this.datafeed = null;
            }
            this.m_connectionParameters.Enabled = true;
            this.m_save.Enabled = true;
            this.m_start.Enabled = true;
            this.m_stop.Enabled = false;
        }

        void OnSymbolsSelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnRefresh(sender, e);
        }

        void OnRefresh(object sender, EventArgs e)
        {
            var selectedItem = m_symbols.SelectedItem;
            if (selectedItem == null)
                return;

            var symbol = selectedItem.ToString();

            Quote quote = null;
            SymbolInfo info = null;

            lock (this.synchronizer)
            {
                this.symbolToInfo.TryGetValue(symbol, out info);
                this.symbolToLevel2.TryGetValue(symbol, out quote);
            }

            if ((null != quote) && (null != info))
            {
                this.DoRefresh(quote, info);
            }
        }

        void RefreshSymbols()
        {
            this.m_symbols.Items.Clear();

            foreach (var element in this.symbolToInfo)
            {
                this.m_symbols.Items.Add(element.Key);
            }
            if (this.m_symbols.Items.Count > 0)
            {
                this.m_symbols.SelectedIndex = 0;
            }
        }

        void DoRefresh(Quote quote, SymbolInfo info)
        {
            this.m_grid.Rows.Clear();
            this.DoRefreshAsks(quote, info);
            this.DoRefreshBids(quote, info);
        }

        void DoRefreshAsks(Quote quote, SymbolInfo info)
        {
            var count = quote.Asks.Length;
            for (var index = count - 1; index > -1; -- index)
            {
                var entry = quote.Asks[index];
                var price = entry.Price;
                var volume = (entry.Volume / info.RoundLot);
                this.m_grid.Rows.Add(string.Empty, price.ToString(), volume.ToString());
            }
        }

        void DoRefreshBids(Quote quote, SymbolInfo info)
        {
            var count = quote.Bids.Length;
            for (var index = 0; index < count; ++index)
            {
                var entry = quote.Bids[index];
                var price = entry.Price;
                var volume = (entry.Volume / info.RoundLot);
                this.m_grid.Rows.Add(volume.ToString(), price.ToString(), string.Empty);
            }
        }

        void OnCopy(object sender, EventArgs e)
        {
            var selectedItem = m_symbols.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            var symbol = selectedItem.ToString();

            Quote quote = null;
            SymbolInfo info = null;

            lock (this.synchronizer)
            {
                this.symbolToInfo.TryGetValue(symbol, out info);
                this.symbolToLevel2.TryGetValue(symbol, out quote);
            }

            if ((null != quote) && (null != info))
            {
                var level2 = new Level2(quote, info);
                var st = level2.ToJson();
                Clipboard.SetText(st);
            }
        }

        void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.datafeed != null)
            {
                this.datafeed.Stop();
                this.datafeed.Dispose();
                this.datafeed = null;
            }
        }

        #endregion

        #region Members

        readonly object synchronizer = new object();
        readonly Dictionary<string, SymbolInfo> symbolToInfo = new Dictionary<string, SymbolInfo>();
        readonly Dictionary<string, Quote> symbolToLevel2 = new Dictionary<string, Quote>();
        readonly FixConnectionStringBuilder fixConnectionStringBuilder = Settings.Default.ConnectionParams;
        DataFeed datafeed;

        #endregion
    }
}
