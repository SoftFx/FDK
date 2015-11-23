namespace FinancialExample
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using SoftFX.Extended.Financial;

    public partial class NewSymbolDialog : Form
    {
        public NewSymbolDialog(ListBox symbols, FinancialCalculator calcualtor)
        {
            this.symbols = symbols;
            this.calculator = calcualtor;
            this.InitializeComponent();
            this.UpdatePreview();
        }

        void OnOK(object sender, EventArgs e)
        {
            this.CreateSymbol();
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            this.m_ok.Enabled = this.IsValid();
            this.UpdatePreview();
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
                if (this.IsValid())
                {
                    this.CreateSymbol();
                    this.Close();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        bool IsValid()
        {
            var isSymbolValid = this.pattern.Match(this.m_symbol.Text).Success;
            var isFromValid = this.pattern.Match(this.m_from.Text).Success;
            var isToValid = this.pattern.Match(this.m_to.Text).Success;
            var result = isSymbolValid && isFromValid && isToValid;
            return result;
        }

        void CreateSymbol()
        {
            var symbol = this.m_symbol.Text;
            var from = this.m_from.Text;
            var to = this.m_to.Text;

            var entry = new SymbolEntry(calculator, symbol, from, to);
            this.calculator.Symbols.Add(entry);
            this.symbols.Items.Add(entry);
        }

        void UpdatePreview()
        {
            var preview = string.Format("{0} = {1}/{2}", this.m_symbol.Text, this.m_to.Text, this.m_from.Text);
            this.m_preview.Text = preview;
        }

        #region Members

        readonly ListBox symbols;
        readonly FinancialCalculator calculator;
        readonly Regex pattern = new Regex(@"^[a-zA-Z][\w/]*$");

        #endregion
    }
}
