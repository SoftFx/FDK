namespace FdkImport
{
    using System;
    using System.Windows.Forms;
    using FdkImport.Engine;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
        }

        void OnConvert(object sender, EventArgs e)
        {
            try
            {
                var converter = new Converter();
                this.m_destination.Text = converter.Process(null, "MyAdviser", this.m_source.Text);
            }
            catch (Exception ex)
            {
                this.m_destination.Text = ex.ToString();
            }
        }

        void OnSourceKeyPress(object sender, KeyPressEventArgs e)
        {
            if (1 == (int)e.KeyChar)
            {
                this.m_source.SelectAll();
            }
        }

        void OnDestinationKeyPress(object sender, KeyPressEventArgs e)
        {
            if (1 == (int)e.KeyChar)
            {
                this.m_destination.SelectAll();
            }
        }
    }
}
