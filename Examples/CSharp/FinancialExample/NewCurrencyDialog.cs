namespace FinancialExample
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using SoftFX.Extended.Financial;

    public partial class NewCurrencyDialog : Form
    {
        public NewCurrencyDialog(ListBox currencies, FinancialCalculator calcualtor)
        {
            this.currencies = currencies;
            this.calculator = calcualtor;
            this.InitializeComponent();
            this.UpdateState();
        }

        void OnOK(object sender, EventArgs e)
        {
            this.CreateCurrency();
            this.Close();
        }

        void CreateCurrency()
        {
            var currency = this.m_currency.Text;
            this.calculator.Currencies.Add(currency);
            this.currencies.Items.Add(currency);
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            this.UpdateState();
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
                    this.CreateCurrency();
                    this.Close();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void UpdateState()
        {
            this.m_ok.Enabled = this.IsValid();
        }

        bool IsValid()
        {
            var currency = this.m_currency.Text;
            if (!this.pattern.Match(currency).Success)
                return false;

            if (this.calculator.Currencies.Contains(currency))
                return false;

            return true;
        }

        #region Members

        readonly ListBox currencies;
        readonly FinancialCalculator calculator;
        readonly Regex pattern = new Regex(@"^[a-zA-Z]\w*$");

        #endregion
    }
}
