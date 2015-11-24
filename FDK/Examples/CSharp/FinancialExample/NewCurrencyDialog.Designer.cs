namespace FinancialExample
{
	partial class NewCurrencyDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_ok = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.m_currency = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// m_ok
			// 
			this.m_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_ok.Location = new System.Drawing.Point(58, 104);
			this.m_ok.Name = "m_ok";
			this.m_ok.Size = new System.Drawing.Size(75, 23);
			this.m_ok.TabIndex = 1;
			this.m_ok.Text = "OK";
			this.m_ok.UseVisualStyleBackColor = true;
			this.m_ok.Click += new System.EventHandler(this.OnOK);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(203, 104);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// m_currency
			// 
			this.m_currency.Location = new System.Drawing.Point(58, 38);
			this.m_currency.Name = "m_currency";
			this.m_currency.Size = new System.Drawing.Size(220, 20);
			this.m_currency.TabIndex = 0;
			this.m_currency.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// NewCurrencyDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(337, 153);
			this.Controls.Add(this.m_currency);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.m_ok);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewCurrencyDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Currency Dialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button m_ok;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox m_currency;
	}
}