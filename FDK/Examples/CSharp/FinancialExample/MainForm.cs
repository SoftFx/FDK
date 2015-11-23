namespace FinancialExample
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Forms;
    using SoftFX.Extended;
    using SoftFX.Extended.Financial;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
            this.Text = string.Format("{0} (FDK {1})", this.Text, Library.Version);
            this.m_calculatorTabPropertyGrid.SelectedObject = this.calculator;
        }

        #region Accounts Tab

        void OnAddAccount(object sender, EventArgs e)
        {
            var entry = new AccountEntry(calculator)
            {
                Tag = ++accountsCounter
            };

            this.calculator.Accounts.Add(entry);
            this.m_accounts.Items.Add(entry);
        }

        void OnRemoveAccount(object sender, EventArgs e)
        {
            if (m_accounts.SelectedItems.Count == 0)
                return;

            var result = MessageBox.Show("Do you would like to remove selected accounts?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            var removing = new List<AccountEntry>();
            foreach (var element in this.m_accounts.SelectedItems)
            {
                var entry = (AccountEntry)element;
                removing.Add(entry);
            }

            foreach (var element in removing)
            {
                this.calculator.Accounts.Remove(element);
                this.m_accounts.Items.Remove(element);
            }
        }

        void OnSelectedAccountsChanged(object sender, EventArgs e)
        {
            var entries = new List<object>();
            foreach (var element in this.m_accounts.SelectedItems)
            {
                entries.Add(element);
            }

            this.m_accountsTabPropertyGrid.SelectedObjects = entries.ToArray();
            
            this.m_trades.Items.Clear();
            if (entries.Count != 1)
                return;

            var account = (AccountEntry)entries[0];
            foreach (var element in account.Trades)
            {
                this.m_trades.Items.Add(element);
            }
        }

        #endregion

        #region Trades Tab

        void OnAddTrade(object sender, EventArgs e)
        {
            var account = this.GetCurrentAccountEntry();
            if (account == null)
                return;

            var entry = new TradeEntry(account)
            {
                Tag = ++tradesCounter
            };

            account.Trades.Add(entry);
            this.m_trades.Items.Add(entry);
        }

        void OnRemoveTrade(object sender, EventArgs e)
        {
            if (this.m_trades.SelectedItems.Count == 0)
                return;

            var account = this.GetCurrentAccountEntry();
            if (account == null)
            {
                return;
            }

            var result = MessageBox.Show("Do you would like to remove selected trades?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            var removing = new List<TradeEntry>();
            foreach (var element in this.m_trades.SelectedItems)
            {
                var entry = (TradeEntry)element;
                removing.Add(entry);
            }

            foreach (var element in removing)
            {
                account.Trades.Remove(element);
                this.m_trades.Items.Remove(element);
            }
        }
        
        AccountEntry GetCurrentAccountEntry()
        {
            AccountEntry result = null;
            if (this.m_accounts.SelectedItems.Count != 1)
                MessageBox.Show("To add or remove trades you should select only one account", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                result = (AccountEntry)this.m_accounts.SelectedItems[0];

            return result;
        }

        void OnSelectedTradesChanged(object sender, EventArgs e)
        {
            var entries = new List<object>();

            foreach (var element in this.m_trades.SelectedItems)
            {
                entries.Add(element);
            }

            this.m_accountsTabPropertyGrid.SelectedObjects = entries.ToArray();
        }

        #endregion

        #region Symbols Tab

        void OnAddSymbol(object sender, EventArgs e)
        {
            var dialog = new NewSymbolDialog(this.m_symbols, this.calculator);
            dialog.ShowDialog();
        }

        void OnRemoveSymbol(object sender, EventArgs e)
        {
            if (m_symbols.SelectedItems.Count == 0)
                return;
            
            var result = MessageBox.Show("Do you would like to remove selected symbols?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            var removing = new List<SymbolEntry>();
            foreach (var element in this.m_symbols.SelectedItems)
            {
                var entry = (SymbolEntry)element;
                removing.Add(entry);
            }
            foreach (var element in removing)
            {
                this.calculator.Symbols.Remove(element);
                this.m_symbols.Items.Remove(element);
            }
        }

        void OnSelectedSymbolsChanged(object sender, EventArgs e)
        {
            var entries = new List<object>();
            foreach (var element in this.m_symbols.SelectedItems)
            {
                entries.Add(element);
            }

            this.m_symbolsTabPropertyGrid.SelectedObjects = entries.ToArray();
        }

        #endregion

        #region Currencies Tab
        
        void OnAddCurrency(object sender, EventArgs e)
        {
            var dialog = new NewCurrencyDialog(m_currencies, calculator);
            dialog.ShowDialog();
        }

        void OnRemoveCurrency(object sender, EventArgs e)
        {
            if (m_currencies.SelectedItems.Count == 0)
                return;

            var result = MessageBox.Show("Do you would like to remove selected currencies?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            var removing = new List<string>();
            foreach (var element in this.m_currencies.SelectedItems)
            {
                var entry = element.ToString();
                removing.Add(entry);
            }
            foreach (var element in removing)
            {
                this.m_currencies.Items.Remove(element);
                this.calculator.Currencies.Remove(element);
            }
        }

        void OnAddMajorCurrencies(object sender, EventArgs e)
        {
            var majorCurrencies = new string[] { "USD", "EUR", "GBP", "CHF", "JPY" };
            foreach (var element in majorCurrencies)
            {
                this.DoAddCurrency(element);
            }
        }

        void DoAddCurrency(string currency)
        {
            if (!this.calculator.Currencies.Contains(currency))
            {
                this.calculator.Currencies.Add(currency);
                this.m_currencies.Items.Add(currency);
            }
        }

        void OnMoveUpCurrency(object sender, EventArgs e)
        {
            var index = this.m_currencies.SelectedIndex;
            if (index <= 0)
            {
                return;
            }

            this.calculator.Currencies.Exchange(index - 1, index);
            var obj = m_currencies.Items[index - 1];
            this.m_currencies.Items[index - 1] = this.m_currencies.Items[index];
            this.m_currencies.Items[index] = obj;
        }

        void OnMoveDownCurrency(object sender, EventArgs e)
        {
            var index = this.m_currencies.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            if (index + 1 >= this.calculator.Currencies.Count)
            {
                return;
            }
            this.calculator.Currencies.Exchange(index, index + 1);
            var obj = m_currencies.Items[index];
            this.m_currencies.Items[index] = this.m_currencies.Items[index + 1];
            this.m_currencies.Items[index + 1] = obj;
        }
        #endregion

        #region Quotes Tab

        void OnAddQuote(object sender, EventArgs e)
        {
            var dialog = new NewQuoteDialog(this.m_quotes);
            dialog.ShowDialog();
        }

        void OnRemoveQuote(object sender, EventArgs e)
        {
            if (this.m_symbols.SelectedItems.Count == 0)
                return;

            var result = MessageBox.Show("Do you would like to remove selected quotes?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            var removing = new List<object>();
            foreach (var element in this.m_quotes.SelectedItems)
            {
                removing.Add(element);
            }
            foreach (var element in removing)
            {
                this.m_quotes.Items.Remove(element);
            }
        }

        void OnSelectedQuotesChanged(object sender, EventArgs e)
        {
            var entries = new List<object>();
            foreach (var element in this.m_quotes.SelectedItems)
            {
                entries.Add(element);
            }
            this.m_quotesTabPropertyGrid.SelectedObjects = entries.ToArray();
        }

        #endregion

        #region Main Form

        void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                var message = string.Format("Do you would like to close {0}?", this.Text);
                var result = MessageBox.Show(message, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        void OnCalculate(object sender, EventArgs e)
        {
            try
            {
                this.UpdateQuotes();

                var sw = Stopwatch.StartNew();

                this.calculator.Calculate();
                sw.Stop();
                this.m_calculationTime.Text = sw.Elapsed.ToString();
                this.RefreshProperties();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void UpdateQuotes()
        {
            this.calculator.Prices.Clear();
            foreach (var element in this.m_quotes.Items)
            {
                var entry = (FullSymbolEntry)element;
                this.calculator.Prices.Update(entry.Symbol, entry.Bid, entry.Ask);
            }
        }

        void OnClear(object sender, EventArgs e)
        {
            try
            {
                this.calculator.Clear();
                this.RefreshProperties();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void RefreshProperties()
        {
            this.m_calculatorTabPropertyGrid.Refresh();
            this.m_symbolsTabPropertyGrid.Refresh();
            this.m_quotesTabPropertyGrid.Refresh();
            this.m_accountsTabPropertyGrid.Refresh();
        }

        void OnOpen(object sender, EventArgs e)
        {
            var result = this.m_openFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            var path = this.m_openFileDialog.FileName;
            try
            {
                this.calculator = FinancialCalculator.Load(path);
                this.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void RefreshData()
        {
            this.m_calculatorTabPropertyGrid.SelectedObject = this.calculator;

            this.m_symbolsTabPropertyGrid.SelectedObject = null;
            this.m_quotesTabPropertyGrid.SelectedObject = null;
            this.m_accountsTabPropertyGrid.SelectedObject = null;

            this.m_trades.Items.Clear();
            this.m_accounts.Items.Clear();
            this.m_quotes.Items.Clear();
            this.m_symbols.Items.Clear();
            this.m_currencies.Items.Clear();

            foreach (var element in this.calculator.Symbols)
            {
                this.m_symbols.Items.Add(element);
            }

            foreach (var element in this.calculator.Prices)
            {
                var item = new FullSymbolEntry(element.Key)
                {
                    Bid = element.Value.Bid,
                    Ask = element.Value.Ask
                };
                this.m_quotes.Items.Add(item);
            }

            foreach (var element in calculator.Accounts)
            {
                this.m_accounts.Items.Add(element);
            }

            foreach (var element in this.calculator.Currencies)
            {
                this.m_currencies.Items.Add(element);
            }
        }

        void OnSave(object sender, EventArgs e)
        {
            var result = m_saveFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            var path = this.m_saveFileDialog.FileName;
            try
            {
                this.UpdateQuotes();
                this.calculator.Save(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void OnExit(object sender, EventArgs e)
        {
            this.Close();
        }

        void OnGetSymbolsAndQuotes(object sender, EventArgs e)
        {
            var dialog = new ServerDialog();
            var result = dialog.ShowDialog();
            if ((DialogResult.OK != result) || (null == dialog.Symbols))
                return;

            this.calculator.Symbols.Clear();
            this.m_symbols.Items.Clear();

            foreach (var element in dialog.Symbols)
            {
                var entry = new SymbolEntry(this.calculator, element.Name, element.SettlementCurrency, element.Currency);
                if (dialog.UseContractSize)
                {
                    entry.ContractSize = element.RoundLot;
                }
                this.calculator.Symbols.Add(entry);
                this.m_symbols.Items.Add(entry);
            }

            this.m_quotes.Items.Clear();

            foreach (var element in dialog.Quotes)
            {
                this.m_quotes.Items.Add(element);
            }
        }

        #endregion

        #region Members

        FinancialCalculator calculator = new FinancialCalculator();
        int accountsCounter;
        int tradesCounter;

        #endregion
    }
}
