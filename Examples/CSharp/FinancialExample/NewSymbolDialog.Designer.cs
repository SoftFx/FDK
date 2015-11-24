namespace FinancialExample
{
	partial class NewSymbolDialog
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
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Button button2;
			this.m_symbol = new System.Windows.Forms.TextBox();
			this.m_from = new System.Windows.Forms.TextBox();
			this.m_to = new System.Windows.Forms.TextBox();
			this.m_ok = new System.Windows.Forms.Button();
			this.m_preview = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(95, 41);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(44, 13);
			label1.TabIndex = 3;
			label1.Text = "Symbol:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(60, 67);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(79, 13);
			label2.TabIndex = 4;
			label2.Text = "Profit Currency:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(52, 97);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(87, 13);
			label3.TabIndex = 5;
			label3.Text = "Margin Currency:";
			// 
			// button2
			// 
			button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			button2.Location = new System.Drawing.Point(166, 183);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(75, 23);
			button2.TabIndex = 7;
			button2.Text = "Cancel";
			button2.UseVisualStyleBackColor = true;
			// 
			// m_symbol
			// 
			this.m_symbol.Location = new System.Drawing.Point(166, 34);
			this.m_symbol.Name = "m_symbol";
			this.m_symbol.Size = new System.Drawing.Size(168, 20);
			this.m_symbol.TabIndex = 0;
			this.m_symbol.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// m_from
			// 
			this.m_from.Location = new System.Drawing.Point(166, 64);
			this.m_from.Name = "m_from";
			this.m_from.Size = new System.Drawing.Size(168, 20);
			this.m_from.TabIndex = 1;
			this.m_from.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// m_to
			// 
			this.m_to.Location = new System.Drawing.Point(166, 94);
			this.m_to.Name = "m_to";
			this.m_to.Size = new System.Drawing.Size(168, 20);
			this.m_to.TabIndex = 2;
			this.m_to.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// m_ok
			// 
			this.m_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_ok.Enabled = false;
			this.m_ok.Location = new System.Drawing.Point(64, 183);
			this.m_ok.Name = "m_ok";
			this.m_ok.Size = new System.Drawing.Size(75, 23);
			this.m_ok.TabIndex = 6;
			this.m_ok.Text = "OK";
			this.m_ok.UseVisualStyleBackColor = true;
			this.m_ok.Click += new System.EventHandler(this.OnOK);
			// 
			// m_preview
			// 
			this.m_preview.AutoSize = true;
			this.m_preview.Location = new System.Drawing.Point(163, 134);
			this.m_preview.Name = "m_preview";
			this.m_preview.Size = new System.Drawing.Size(45, 13);
			this.m_preview.TabIndex = 8;
			this.m_preview.Text = "Preview";
			// 
			// NewSymbolDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(428, 218);
			this.Controls.Add(this.m_preview);
			this.Controls.Add(button2);
			this.Controls.Add(this.m_ok);
			this.Controls.Add(label3);
			this.Controls.Add(label2);
			this.Controls.Add(label1);
			this.Controls.Add(this.m_to);
			this.Controls.Add(this.m_from);
			this.Controls.Add(this.m_symbol);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewSymbolDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Symbol Dialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox m_symbol;
		private System.Windows.Forms.TextBox m_from;
		private System.Windows.Forms.TextBox m_to;
		private System.Windows.Forms.Button m_ok;
		private System.Windows.Forms.Label m_preview;
	}
}