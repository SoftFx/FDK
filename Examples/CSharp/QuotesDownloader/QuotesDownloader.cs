namespace QuotesDownloader
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;
    using SoftFX.Extended.Storage;

    public partial class QuotesDownloader : Form
    {
        #region Members

        readonly FixConnectionStringBuilder fixConnectionStringBuilder = Settings.Default.ConnectionParams;
        DataFeed datafeed;
        Downloader downloader;

        #endregion

        public QuotesDownloader()
        {
            if (this.fixConnectionStringBuilder == null)
            {
                this.fixConnectionStringBuilder = new FixConnectionStringBuilder();
            }
            this.InitializeComponent();

            this.Text = string.Format("{0} (FDK {1})", this.Text, Library.Version);

            foreach (var element in StorageProvider.Providers)
            {
                this.m_storageType.Items.Add(element.Key);
            }
            this.m_storageType.SelectedIndex = 0;

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, "Quotes");
            Directory.CreateDirectory(path);
            this.m_location.Text = path;
            this.m_quotesType.SelectedIndex = 0;

            this.m_address.Text = this.fixConnectionStringBuilder.Address;
            this.m_username.Text = this.fixConnectionStringBuilder.Username;
            this.m_password.Text = this.fixConnectionStringBuilder.Password;
            this.m_port.Text = this.fixConnectionStringBuilder.Port.ToString();
            this.m_ssl.Checked = this.fixConnectionStringBuilder.SecureConnection;


            var utcNow = DateTime.UtcNow;
            var to = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day);
            to = to.AddDays(-7);
            var from = to.AddDays(-7);
            this.m_dateAndTimeFrom.Value = from;
            this.m_dateAndTimeTo.Value = to;
            this.ApplyDisconnectedState();
        }

        void OnBrowse(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var status = dialog.ShowDialog();
            if (status == DialogResult.OK)
            {
                this.m_location.Text = dialog.SelectedPath;
            }
        }

        void OnPortValidating(object sender, CancelEventArgs e)
        {
            var st = this.m_port.Text;
            var port = (short)0;

            if (!short.TryParse(st, out port) || (port <= 0))
            {
                var message = string.Format("You should enter a positive number from 1 to {0}", short.MaxValue);
                this.m_toolTip.Show(message, this.m_port);
                e.Cancel = true;
            }
        }

        void OnPortKeyDown(object sender, KeyEventArgs e)
        {
            this.m_toolTip.Hide(this.m_port);
        }

        void OnLogClear(object sender, EventArgs e)
        {
            this.m_log.Items.Clear();
        }

        void OnLogSave(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "Log files|*.log"
            };

            var status = dialog.ShowDialog();
            if (status == DialogResult.OK)
            {
                using (var stream = new StreamWriter(dialog.FileName, false))
                {
                    foreach (var element in m_log.Items)
                    {
                        stream.WriteLine(element);
                    }
                }
            }
        }
        
        void OnConnection(object sender, EventArgs e)
        {
            if (this.datafeed == null)
                this.Connect();
            else
                this.Disconnect();
        }

        void Connect()
        {
            try
            {
                this.Log("Data feed initialization");
                this.fixConnectionStringBuilder.Address = this.m_address.Text;
                this.fixConnectionStringBuilder.Username = this.m_username.Text;
                this.fixConnectionStringBuilder.Password = this.m_password.Text;
                this.fixConnectionStringBuilder.Port = Convert.ToInt32(this.m_port.Text);
                this.fixConnectionStringBuilder.SecureConnection = this.m_ssl.Checked;
                var connectionString = this.fixConnectionStringBuilder.ToString();
                var dataFeed = new DataFeed(connectionString);
                dataFeed.Logon += this.OnLogon;
                dataFeed.Logout += this.OnLogout;
                dataFeed.SymbolInfo += this.OnSymbolInfo;
                this.Log("Connecting...");
                dataFeed.Start();
                this.datafeed = dataFeed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.ApplyConnectingState();
        }

        void ApplyConnectingState()
        {
            this.m_connection.Text = "Disconnect";
            this.m_connectionParameters.Enabled = false;
        }

        void ApplyConnectedState()
        {
            this.m_download.Enabled = true;
        }

        void Disconnect()
        {
            try
            {
                this.Log("Disconnecting...");
                if (datafeed != null)
                {
                    this.datafeed.Stop();
                    this.datafeed.Dispose();
                    this.datafeed = null;
                }

                this.Log("Data feed is disconnected");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.ApplyDisconnectedState();
        }

        void ApplyDisconnectedState()
        {
            this.m_connection.Text = "Connect";
            this.m_connectionParameters.Enabled = true;
            this.m_browse.Enabled = true;
            this.m_download.Enabled = false;
            this.m_symbols.Items.Clear();
        }

        #region Data Feed Events

        void OnLogon(object sender, LogonEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.InvokeInPrimaryThread(this.OnLogon, sender, e);
                return;
            }
            this.Log("Data feed is connected");
        }

        void OnSymbolInfo(object sender, SymbolInfoEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.InvokeInPrimaryThread(OnSymbolInfo, sender, e);
                return;
            }

            this.m_symbols.Items.Clear();
            foreach (var element in e.Information)
            {
                this.m_symbols.Items.Add(element.Name);
            }
            if (e.Information.Length > 0)
            {
                this.m_symbols.SelectedIndex = 0;
            }
            this.Log("Symbols information is received");
            this.ApplyConnectedState();
        }

        void OnLogout(object sender, LogoutEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.InvokeInPrimaryThread(OnLogout, sender, e);
                return;
            }

            if (string.IsNullOrEmpty(e.Text))
                this.Log("Logout event is received; reason = {0}", e.Reason);
            else
                this.Log("Logout event is received; reason = {0}; Text = {1}", e.Reason, e.Text);
        }

        #endregion

        #region Form Events

        void OnDownload(object sender, EventArgs e)
        {
            if (this.downloader == null)
            {
                var storageType = StorageProvider.Providers[m_storageType.SelectedItem.ToString()];
                var location = this.m_location.Text;
                var symbol = this.m_symbols.Text;
                var from = this.m_dateAndTimeFrom.Value;
                var to = this.m_dateAndTimeTo.Value;

                if (this.m_quotesType.SelectedIndex == 0)
                {
                    this.downloader = new Downloader(this.datafeed, storageType, location, symbol, from, to, false);
                }
                else if (this.m_quotesType.SelectedIndex == 1)
                {
                    this.downloader = new Downloader(this.datafeed, storageType, location, symbol, from, to, true);
                }
                else
                {
                    var st = this.m_quotesType.SelectedItem.ToString();
                    var match = Regex.Match(st, "^(Bid|Ask) ([^ ]+)$");
                    if (!match.Success)
                    {
                        var message = string.Format("Unexpected string format = {0}", st);
                        throw new ArgumentException(message);
                    }
                    var stPriceType = match.Groups[1].Value;
                    var priceType = ("Bid" == stPriceType) ? PriceType.Bid : PriceType.Ask;

                    var stBarPeriod = match.Groups[2].Value;

                    var barPeriod = new BarPeriod(stBarPeriod);
                    this.downloader = new Downloader(this.datafeed, storageType, location, symbol, from, to, priceType, barPeriod);

                }

                this.downloader.Message += this.OnMessage;
                this.downloader.Finish += this.OnFinish;
                this.downloader.Start();
                this.m_download.Text = "Break";
                this.m_browse.Enabled = false;
                this.m_symbols.Enabled = false;
                this.m_quotesType.Enabled = false;
                this.m_storageType.Enabled = false;
            }
            else
            {
                this.downloader.Stop();
                this.downloader = null;
                this.m_download.Text = "Download";
                this.m_browse.Enabled = true;
                this.m_symbols.Enabled = true;
                this.m_quotesType.Enabled = true;
                this.m_storageType.Enabled = false;
            }
        }

        void OnFinish(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.InvokeInPrimaryThread(OnFinish, sender, e);
                return;
            }
            if (this.downloader != null)
            {
                this.downloader.Join();
                this.downloader = null;
            }
            this.m_download.Text = "Download";
            this.m_browse.Enabled = true;
            this.m_symbols.Enabled = true;
            this.m_quotesType.Enabled = true;
        }

        void OnMessage(object sender, EventArgs<string> e)
        {
            if (this.InvokeRequired)
            {
                this.InvokeInPrimaryThread(OnMessage, sender, e);
                return;
            }

            this.Log(e.Value);
        }

        void OnClosed(object sender, FormClosedEventArgs e)
        {
            if (this.datafeed != null)
            {
                this.datafeed.Stop();
                this.datafeed.Dispose();
            }
        }

        void OnClosing(object sender, FormClosingEventArgs e)
        {
            if (this.downloader != null)
            {
                if (!this.downloader.IsFinished)
                {
                    e.Cancel = true;
                    MessageBox.Show("You should stop quotes downloading and wait for finish of background synchronization");
                }
            }
        }

        void OnSave(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        #endregion

        #region Helper Functions

        void Log(string text)
        {
            if (this.InvokeRequired)
            {
                this.InvokeInPrimaryThread(Log, text);
                return;
            }
            var st = string.Format("{0} - {1}", DateTime.Now, text);
            this.m_log.Items.Add(st);
            this.m_log.SelectedIndex = m_log.Items.Count - 1;
        }

        void Log(string format, params object[] arguments)
        {
            var text = string.Format(format, arguments);
            this.Log(text);
        }

        void InvokeInPrimaryThread(Action func)
        {
            this.DoInvokeInPrimaryThread(func);
        }

        void InvokeInPrimaryThread<A0>(Action<A0> func, A0 a0)
        {
            this.DoInvokeInPrimaryThread(func, a0);
        }

        void InvokeInPrimaryThread<A0, A1>(Action<A0, A1> func, A0 a0, A1 a1)
        {
            this.DoInvokeInPrimaryThread(func, a0, a1);
        }

        void DoInvokeInPrimaryThread(Delegate func, params object[] arguments)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(func, arguments);
            }
            else
            {
                func.DynamicInvoke(arguments);
            }
        }

        #endregion
    }
}
