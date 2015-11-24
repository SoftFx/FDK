namespace QuotesCsvExporter
{
    partial class QuotesExporter
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
			this.label1 = new System.Windows.Forms.Label();
			this.m_storageLocation = new System.Windows.Forms.TextBox();
			this.m_open = new System.Windows.Forms.Button();
			this.m_dateFrom = new System.Windows.Forms.DateTimePicker();
			this.m_dateTo = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.m_start = new System.Windows.Forms.Button();
			this.m_stop = new System.Windows.Forms.Button();
			this.m_progress = new System.Windows.Forms.ProgressBar();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.m_symbols = new System.Windows.Forms.ListBox();
			this.m_browserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.label6 = new System.Windows.Forms.Label();
			this.m_save = new System.Windows.Forms.Button();
			this.m_outputFile = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.m_contractSize = new System.Windows.Forms.NumericUpDown();
			this.m_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.m_removeDuplicateEntries = new System.Windows.Forms.CheckBox();
			this.m_sourceType = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.m_contractSize)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Storage location";
			// 
			// m_storageLocation
			// 
			this.m_storageLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_storageLocation.Location = new System.Drawing.Point(12, 37);
			this.m_storageLocation.Name = "m_storageLocation";
			this.m_storageLocation.ReadOnly = true;
			this.m_storageLocation.Size = new System.Drawing.Size(291, 21);
			this.m_storageLocation.TabIndex = 1;
			// 
			// m_open
			// 
			this.m_open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_open.Location = new System.Drawing.Point(312, 37);
			this.m_open.Name = "m_open";
			this.m_open.Size = new System.Drawing.Size(75, 23);
			this.m_open.TabIndex = 2;
			this.m_open.Text = "Open";
			this.m_open.UseVisualStyleBackColor = true;
			this.m_open.Click += new System.EventHandler(this.OnOpen);
			// 
			// m_dateFrom
			// 
			this.m_dateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_dateFrom.Location = new System.Drawing.Point(115, 137);
			this.m_dateFrom.Name = "m_dateFrom";
			this.m_dateFrom.Size = new System.Drawing.Size(273, 21);
			this.m_dateFrom.TabIndex = 3;
			// 
			// m_dateTo
			// 
			this.m_dateTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_dateTo.Location = new System.Drawing.Point(115, 179);
			this.m_dateTo.Name = "m_dateTo";
			this.m_dateTo.Size = new System.Drawing.Size(272, 21);
			this.m_dateTo.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(112, 119);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "From:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(112, 161);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 15);
			this.label3.TabIndex = 6;
			this.label3.Text = "To:";
			// 
			// m_start
			// 
			this.m_start.Enabled = false;
			this.m_start.Location = new System.Drawing.Point(81, 405);
			this.m_start.Name = "m_start";
			this.m_start.Size = new System.Drawing.Size(75, 23);
			this.m_start.TabIndex = 7;
			this.m_start.Text = "Start";
			this.m_start.UseVisualStyleBackColor = true;
			this.m_start.Click += new System.EventHandler(this.OnStart);
			// 
			// m_stop
			// 
			this.m_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_stop.Enabled = false;
			this.m_stop.Location = new System.Drawing.Point(234, 405);
			this.m_stop.Name = "m_stop";
			this.m_stop.Size = new System.Drawing.Size(75, 23);
			this.m_stop.TabIndex = 8;
			this.m_stop.Text = "Stop";
			this.m_stop.UseVisualStyleBackColor = true;
			this.m_stop.Click += new System.EventHandler(this.OnStop);
			// 
			// m_progress
			// 
			this.m_progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_progress.Location = new System.Drawing.Point(11, 376);
			this.m_progress.Name = "m_progress";
			this.m_progress.Size = new System.Drawing.Size(376, 23);
			this.m_progress.TabIndex = 9;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 119);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(43, 15);
			this.label4.TabIndex = 11;
			this.label4.Text = "Period";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 206);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(54, 15);
			this.label5.TabIndex = 12;
			this.label5.Text = "Symbols";
			// 
			// m_symbols
			// 
			this.m_symbols.FormattingEnabled = true;
			this.m_symbols.ItemHeight = 15;
			this.m_symbols.Location = new System.Drawing.Point(115, 206);
			this.m_symbols.Name = "m_symbols";
			this.m_symbols.Size = new System.Drawing.Size(272, 94);
			this.m_symbols.TabIndex = 13;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 315);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(77, 15);
			this.label6.TabIndex = 14;
			this.label6.Text = "Contract size";
			// 
			// m_save
			// 
			this.m_save.Location = new System.Drawing.Point(312, 79);
			this.m_save.Name = "m_save";
			this.m_save.Size = new System.Drawing.Size(75, 23);
			this.m_save.TabIndex = 16;
			this.m_save.Text = "Save";
			this.m_save.UseVisualStyleBackColor = true;
			this.m_save.Click += new System.EventHandler(this.OnSave);
			// 
			// m_outputFile
			// 
			this.m_outputFile.Location = new System.Drawing.Point(15, 80);
			this.m_outputFile.Name = "m_outputFile";
			this.m_outputFile.ReadOnly = true;
			this.m_outputFile.Size = new System.Drawing.Size(291, 21);
			this.m_outputFile.TabIndex = 17;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(15, 65);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(62, 15);
			this.label7.TabIndex = 18;
			this.label7.Text = "Output file";
			// 
			// m_contractSize
			// 
			this.m_contractSize.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.m_contractSize.Location = new System.Drawing.Point(115, 314);
			this.m_contractSize.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.m_contractSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.m_contractSize.Name = "m_contractSize";
			this.m_contractSize.Size = new System.Drawing.Size(120, 21);
			this.m_contractSize.TabIndex = 19;
			this.m_contractSize.ThousandsSeparator = true;
			this.m_contractSize.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			// 
			// m_saveFileDialog
			// 
			this.m_saveFileDialog.Filter = "CSV files|*.csv|All files|*.*";
			// 
			// m_removeDuplicateEntries
			// 
			this.m_removeDuplicateEntries.AutoSize = true;
			this.m_removeDuplicateEntries.Checked = true;
			this.m_removeDuplicateEntries.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_removeDuplicateEntries.Location = new System.Drawing.Point(12, 347);
			this.m_removeDuplicateEntries.Name = "m_removeDuplicateEntries";
			this.m_removeDuplicateEntries.Size = new System.Drawing.Size(161, 19);
			this.m_removeDuplicateEntries.TabIndex = 20;
			this.m_removeDuplicateEntries.Text = "Remove duplicate prices";
			this.m_removeDuplicateEntries.UseVisualStyleBackColor = true;
			// 
			// m_sourceType
			// 
			this.m_sourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_sourceType.FormattingEnabled = true;
			this.m_sourceType.Items.AddRange(new object[] {
            "Ticks",
            "S1",
            "S10",
            "M1",
            "M5",
            "M15",
            "M30",
            "H1",
            "H4",
            "D1",
            "W1",
            "MN1"});
			this.m_sourceType.Location = new System.Drawing.Point(266, 312);
			this.m_sourceType.Name = "m_sourceType";
			this.m_sourceType.Size = new System.Drawing.Size(121, 23);
			this.m_sourceType.TabIndex = 21;
			// 
			// QuotesExporter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(402, 455);
			this.Controls.Add(this.m_sourceType);
			this.Controls.Add(this.m_removeDuplicateEntries);
			this.Controls.Add(this.m_contractSize);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.m_outputFile);
			this.Controls.Add(this.m_save);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.m_symbols);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.m_progress);
			this.Controls.Add(this.m_stop);
			this.Controls.Add(this.m_start);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.m_dateTo);
			this.Controls.Add(this.m_dateFrom);
			this.Controls.Add(this.m_open);
			this.Controls.Add(this.m_storageLocation);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "QuotesExporter";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SoftFX Quotes Exporter";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
			((System.ComponentModel.ISupportInitialize)(this.m_contractSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_storageLocation;
        private System.Windows.Forms.Button m_open;
        private System.Windows.Forms.DateTimePicker m_dateFrom;
        private System.Windows.Forms.DateTimePicker m_dateTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button m_start;
        private System.Windows.Forms.Button m_stop;
        private System.Windows.Forms.ProgressBar m_progress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox m_symbols;
		private System.Windows.Forms.FolderBrowserDialog m_browserDialog;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button m_save;
		private System.Windows.Forms.TextBox m_outputFile;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown m_contractSize;
		private System.Windows.Forms.SaveFileDialog m_saveFileDialog;
		private System.Windows.Forms.CheckBox m_removeDuplicateEntries;
		private System.Windows.Forms.ComboBox m_sourceType;
    }
}

