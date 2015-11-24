namespace FinancialExample
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public partial class NewQuoteDialog : Form
    {
        public NewQuoteDialog(ListBox quotes)
        {
            this.quotes = quotes;

            foreach (var element in quotes.Items)
            {
                this.symbols.Add(element.ToString());
            }

            this.InitializeComponent();
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            this.m_ok.Enabled = this.IsValidSymbol();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Keys.Escape == keyData)
            {
                this.Close();
                return true;
            }
            if (Keys.Enter == keyData)
            {
                if (this.IsValidSymbol())
                {
                    this.CreateNewQuote();
                    this.Close();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void OnOK(object sender, EventArgs e)
        {
            this.CreateNewQuote();
        }

        bool IsValidSymbol()
        {
            var result = this.pattern.Match(m_symbol.Text).Success;
            if (result)
            {
                result = !this.symbols.Contains(this.m_symbol.Text);
            }

            return result;
        }

        void CreateNewQuote()
        {
            var entry = new FullSymbolEntry(this.m_symbol.Text);
            this.quotes.Items.Add(entry);
        }

        #region Members

        readonly ListBox quotes;
        readonly HashSet<string> symbols = new HashSet<string>();
        readonly Regex pattern = new Regex(@"^[a-zA-Z]\w*$");

        #endregion
    }
}
