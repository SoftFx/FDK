namespace QuotesCsvExporter
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using SoftFX.Extended;

    public partial class QuotesExporter : Form
    {
        public QuotesExporter()
        {
            this.InitializeComponent();
            this.Text = string.Format("{0} (FDK {1})", this.Text, Library.Version);

            this.MaximumSize = new Size(this.Size.Width * 2, this.Size.Height);
            this.MinimumSize = this.Size;
            this.m_symbols.SelectionMode = SelectionMode.One;
            this.m_sourceType.SelectedIndex = 0;
        }

        #region Event Handlers

        void OnStart(object sender, EventArgs e)
        {
            var contractSize = (int)this.m_contractSize.Value;
            var symbol = m_symbols.SelectedItem.ToString();
            var from = this.GetFrom();
            var to = this.GetTo();
            if (this.m_sourceType.SelectedIndex == 0)
            {
                this.exporter = new Exporter(this.m_storageLocation.Text, this.m_outputFile.Text, symbol, null, from, to, contractSize, this.m_removeDuplicateEntries.Checked, this.m_progress.Maximum);
            }
            else
            {
                var period = new BarPeriod(this.m_sourceType.SelectedItem.ToString());
                this.exporter = new Exporter(this.m_storageLocation.Text, this.m_outputFile.Text, symbol, period, from, to, contractSize, this.m_removeDuplicateEntries.Checked, this.m_progress.Maximum);
            }

            this.exporter.Progress += this.OnProgress;
            this.exporter.Finish += this.OnFinish;

            this.m_start.Enabled = false;
            this.m_stop.Enabled = true;
            this.m_open.Enabled = false;
            this.m_save.Enabled = false;
            this.m_dateFrom.Enabled = false;
            this.m_dateTo.Enabled = false;
            this.m_contractSize.Enabled = false;
            this.m_symbols.Enabled = false;
            this.m_removeDuplicateEntries.Enabled = false;

            this.exporter.Start();
        }

        DateTime GetFrom()
        {
            var result = this.m_dateFrom.Value;
            result = new DateTime(result.Year, result.Month, result.Day, 0, 0, 0, 0, DateTimeKind.Utc);
            return result;
        }

        DateTime GetTo()
        {
            var result = this.m_dateTo.Value;
            result = new DateTime(result.Year, result.Month, result.Day, 23, 59, 59, 999, DateTimeKind.Utc);
            return result;
        }

        void OnProgress(object sender, ProgressEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.InvokeInPrimaryThread(this.OnProgress, sender, e);
            }
            else
            {
                this.m_progress.Value = e.Value;
            }
        }

        void OnFinish(object sender, FinishEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.InvokeInPrimaryThread(this.OnFinish, sender, e);
            }
            else
            {
                if (!e.Status)
                {
                    MessageBox.Show(e.Message, this.Text);
                }

                this.m_start.Enabled = true;
                this.m_stop.Enabled = false;
                this.m_open.Enabled = true;
                this.m_save.Enabled = true;
                this.m_dateFrom.Enabled = true;
                this.m_dateTo.Enabled = true;
                this.m_symbols.Enabled = true;
                this.m_contractSize.Enabled = true;
                this.m_removeDuplicateEntries.Enabled = true;

                this.exporter = null;
                this.m_progress.Value = 0;
            }
        }

        void OnStop(object sender, EventArgs e)
        {
            var exporter = this.exporter;
            if (exporter != null)
            {
                exporter.Stop();
                this.m_stop.Enabled = false;
            }
        }

        void OnOpen(object sender, EventArgs e)
        {
            var result = this.m_browserDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            this.m_storageLocation.Text = this.m_browserDialog.SelectedPath;

            var items = Directory.GetDirectories(m_storageLocation.Text);
            this.m_symbols.Items.Clear();

            foreach (var element in items)
            {
                var symbol = Path.GetFileName(element);
                symbol = symbol.Replace("%2F", "/");
                this.m_symbols.Items.Add(symbol);
            }
            this.m_symbols.SelectedIndex = 0;
            this.EnableStartIfPossible();
        }

        void OnSave(object sender, EventArgs e)
        {
            var result = this.m_saveFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            this.m_outputFile.Text = this.m_saveFileDialog.FileName;
            this.EnableStartIfPossible();
        }

        void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.exporter != null)
            {
                MessageBox.Show("To close the application you should stop quotes exporting", this.Text);
                e.Cancel = true;
            }
        }
        #endregion

        #region Private Methods

        void EnableStartIfPossible()
        {
            if (string.IsNullOrEmpty(this.m_storageLocation.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(this.m_outputFile.Text))
            {
                return;
            }
            if (m_symbols.Items.Count == 0)
            {
                return;
            }

            this.m_start.Enabled = true;
            this.m_stop.Enabled = false;
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

        #region members

        Exporter exporter;

        #endregion
    }
}
