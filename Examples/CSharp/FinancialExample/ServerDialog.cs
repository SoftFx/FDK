namespace FinancialExample
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;
    using SoftFX.Extended;

    public partial class ServerDialog : Form
    {
        public ServerDialog()
        {
            this.InitializeComponent();
        }

        #region Events Handlers

        void OnSave(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        void OnGet(object sender, EventArgs e)
        {
            try
            {
                var builder = Settings.Default.ConnectionStrings;
                builder.Address = this.m_address.Text;
                builder.Port = int.Parse(this.m_port.Text);
                builder.SecureConnection = this.m_ssl.Checked;
                builder.Username = this.m_username.Text;
                builder.Password = this.m_password.Text;
                var connectionString = builder.ToString();

                var dt = this.m_dateTime.Value;
                this.timeStamp = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, DateTimeKind.Utc);

                this.thread = new Thread(this.ThreadMethod);
                this.Disable();
                this.continueMonitoring = true;
                this.symbols = null;
                this.quotes.Clear();
                this.thread.Start(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void OnCancel(object sender, EventArgs e)
        {
            this.Close();
        }

        void OnClosing(object sender, FormClosingEventArgs e)
        {
            this.continueMonitoring = false;
            this.Log("Canceling");
        }

        void Disable()
        {
            this.m_connectionParameters.Enabled = false;
            this.m_dateTime.Enabled = false;
            this.m_useContractSize.Enabled = false;
            this.m_get.Enabled = false;
            this.m_cancel.Enabled = true;
        }

        void Enable()
        {
            this.m_connectionParameters.Enabled = true;
            this.m_dateTime.Enabled = true;
            this.m_useContractSize.Enabled = true;
            this.m_get.Enabled = true;
            this.m_cancel.Enabled = false;
        }

        #endregion

        #region Thread Methods

        void ThreadMethod(object obj)
        {
            try
            {
                var connectionString = (string)obj;
                this.Log("Creating data feed");
                using (var feed = new DataFeed(connectionString))
                {
                    this.Log("Data feed is created");
                    this.Run(feed);
                    this.Log("Disposing data feed");
                }
                this.Log("Data feed is disposed");
            }
            catch(Exception ex)
            {
                this.Log(ex.Message);
            }
            this.Finish();
        }

        void Run(DataFeed feed)
        {
            this.CheckCancel();
            this.Log("Starting data feed");
            if (feed.Start(30000))
            {
                this.Log("Data feed is started");
                this.GetSymbols(feed);
                this.GetQuotes(feed);
            }
            else
            {
                this.Log("Couldn't start data feed");
            }
        }

        void GetSymbols(DataFeed feed)
        {
            this.CheckCancel();
            this.Log("Getting symbols");
            this.symbols = feed.Server.GetSymbols();
            this.Log("{0} symbols have been got", this.symbols.Length);
        }

        void GetQuotes(DataFeed feed)
        {
            foreach (var element in this.symbols)
            {
                this.GetQuotes(feed, element.Name);
            }
        }

        void GetQuotes(DataFeed feed, string symbol)
        {
            this.CheckCancel();
            this.Log("Getting quotes for {0}", symbol);
            var bars = new PairBars(feed, symbol, BarPeriod.S1, timeStamp, -1);
            foreach (var element in bars)
            {
                if ((null != element.Bid) && (null != element.Ask))
                {
                    var entry = new FullSymbolEntry(symbol)
                    {
                        Bid = element.Bid.Close,
                        Ask = element.Ask.Close,
                    };
                    this.quotes.Add(entry);
                    this.Log("Quotes for symbol {0} have been got: {1}/{2}", symbol, entry.Bid, entry.Ask);
                }
                else
                {
                    this.Log("Quotes for symbol {0} are not available", symbol);
                }
                break;
            }
        }

        void CheckCancel()
        {
            if (!this.continueMonitoring)
            {
                throw new Exception("Canceled by user");
            }
        }

        void Finish()
        {
            if (this.InvokeRequired)
            {
                Action func = this.Finish;
                this.Invoke(func);
            }
            else if (this.continueMonitoring)
            {
                this.UseContractSize = this.m_useContractSize.Checked;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.UseContractSize = this.m_useContractSize.Checked;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        void Log(string format, params object[] args)
        {
            var message = string.Format(format, args);
            this.Log(message);
        }

        void Log(string message)
        {
            if (this.InvokeRequired)
            {
                Action<string> func = this.Log;
                this.Invoke(func, message);
            }
            else
            {
                this.m_log.Items.Add(message);
                this.m_log.SelectedIndex = this.m_log.Items.Count - 1;
            }
        }
        #endregion

        internal bool UseContractSize { get; private set; }

        internal SymbolInfo[] Symbols
        {
            get
            {
                return this.symbols;
            }
        }

        internal List<FullSymbolEntry> Quotes
        {
            get
            {
                return this.quotes;
            }
        }

        #region Members

        volatile bool continueMonitoring;
        DateTime timeStamp;
        Thread thread;
        SymbolInfo[] symbols;
        readonly List<FullSymbolEntry> quotes = new List<FullSymbolEntry>();

        #endregion
    }
}
