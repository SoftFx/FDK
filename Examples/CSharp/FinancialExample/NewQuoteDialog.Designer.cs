namespace FinancialExample
{
	partial class NewQuoteDialog
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
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Button button2;
			this.m_symbol = new System.Windows.Forms.TextBox();
			this.m_ok = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(117, 31);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(44, 13);
			label1.TabIndex = 0;
			label1.Text = "Symbol:";
			// 
			// button2
			// 
			button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			button2.Location = new System.Drawing.Point(189, 68);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(75, 23);
			button2.TabIndex = 3;
			button2.Text = "Cancel";
			button2.UseVisualStyleBackColor = true;
			// 
			// m_symbol
			// 
			this.m_symbol.Location = new System.Drawing.Point(189, 24);
			this.m_symbol.Name = "m_symbol";
			this.m_symbol.Size = new System.Drawing.Size(100, 20);
			this.m_symbol.TabIndex = 1;
			this.m_symbol.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// m_ok
			// 
			this.m_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_ok.Enabled = false;
			this.m_ok.Location = new System.Drawing.Point(86, 68);
			this.m_ok.Name = "m_ok";
			this.m_ok.Size = new System.Drawing.Size(75, 23);
			this.m_ok.TabIndex = 2;
			this.m_ok.Text = "OK";
			this.m_ok.UseVisualStyleBackColor = true;
			this.m_ok.Click += new System.EventHandler(this.OnOK);
			// 
			// NewQuoteDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(406, 119);
			this.Controls.Add(button2);
			this.Controls.Add(this.m_ok);
			this.Controls.Add(this.m_symbol);
			this.Controls.Add(label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewQuoteDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Quote Dialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox m_symbol;
		private System.Windows.Forms.Button m_ok;
	}
}