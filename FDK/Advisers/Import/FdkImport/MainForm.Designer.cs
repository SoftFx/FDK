namespace FdkImport
{
	partial class MainForm
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
			this.m_menu = new System.Windows.Forms.MenuStrip();
			this.m_convert = new System.Windows.Forms.ToolStripMenuItem();
			this.m_split = new System.Windows.Forms.SplitContainer();
			this.m_source = new System.Windows.Forms.TextBox();
			this.m_destination = new System.Windows.Forms.TextBox();
			this.m_menu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_split)).BeginInit();
			this.m_split.Panel1.SuspendLayout();
			this.m_split.Panel2.SuspendLayout();
			this.m_split.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_menu
			// 
			this.m_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_convert});
			this.m_menu.Location = new System.Drawing.Point(0, 0);
			this.m_menu.Name = "m_menu";
			this.m_menu.Size = new System.Drawing.Size(784, 24);
			this.m_menu.TabIndex = 0;
			this.m_menu.Text = "menuStrip1";
			// 
			// m_convert
			// 
			this.m_convert.Name = "m_convert";
			this.m_convert.Size = new System.Drawing.Size(61, 20);
			this.m_convert.Text = "Convert";
			this.m_convert.Click += new System.EventHandler(this.OnConvert);
			// 
			// m_split
			// 
			this.m_split.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_split.Location = new System.Drawing.Point(0, 24);
			this.m_split.Name = "m_split";
			// 
			// m_split.Panel1
			// 
			this.m_split.Panel1.Controls.Add(this.m_source);
			// 
			// m_split.Panel2
			// 
			this.m_split.Panel2.Controls.Add(this.m_destination);
			this.m_split.Size = new System.Drawing.Size(784, 538);
			this.m_split.SplitterDistance = 390;
			this.m_split.TabIndex = 1;
			// 
			// m_source
			// 
			this.m_source.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_source.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.m_source.Location = new System.Drawing.Point(0, 0);
			this.m_source.Multiline = true;
			this.m_source.Name = "m_source";
			this.m_source.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.m_source.Size = new System.Drawing.Size(390, 538);
			this.m_source.TabIndex = 0;
			this.m_source.WordWrap = false;
			this.m_source.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnSourceKeyPress);
			// 
			// m_destination
			// 
			this.m_destination.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_destination.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.m_destination.Location = new System.Drawing.Point(0, 0);
			this.m_destination.Multiline = true;
			this.m_destination.Name = "m_destination";
			this.m_destination.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.m_destination.Size = new System.Drawing.Size(390, 538);
			this.m_destination.TabIndex = 0;
			this.m_destination.WordWrap = false;
			this.m_destination.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnDestinationKeyPress);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.m_split);
			this.Controls.Add(this.m_menu);
			this.MainMenuStrip = this.m_menu;
			this.Name = "MainForm";
			this.Text = "FDK Import";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.m_menu.ResumeLayout(false);
			this.m_menu.PerformLayout();
			this.m_split.Panel1.ResumeLayout(false);
			this.m_split.Panel1.PerformLayout();
			this.m_split.Panel2.ResumeLayout(false);
			this.m_split.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_split)).EndInit();
			this.m_split.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip m_menu;
		private System.Windows.Forms.ToolStripMenuItem m_convert;
		private System.Windows.Forms.SplitContainer m_split;
		private System.Windows.Forms.TextBox m_source;
		private System.Windows.Forms.TextBox m_destination;
	}
}