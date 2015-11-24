namespace RealTimeLevel2
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
			this.components = new System.ComponentModel.Container();
			this.m_connectionParameters = new System.Windows.Forms.GroupBox();
			this.m_port = new System.Windows.Forms.NumericUpDown();
			this.m_ssl = new System.Windows.Forms.CheckBox();
			this.m_portLabel = new System.Windows.Forms.Label();
			this.m_password = new System.Windows.Forms.TextBox();
			this.m_username = new System.Windows.Forms.TextBox();
			this.m_address = new System.Windows.Forms.TextBox();
			this.m_passwordLabel = new System.Windows.Forms.Label();
			this.m_usernameLabel = new System.Windows.Forms.Label();
			this.m_addressLabel = new System.Windows.Forms.Label();
			this.m_symbols = new System.Windows.Forms.ComboBox();
			this.m_grid = new System.Windows.Forms.DataGridView();
			this.BidVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.AskVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.m_stop = new System.Windows.Forms.Button();
			this.m_start = new System.Windows.Forms.Button();
			this.m_save = new System.Windows.Forms.Button();
			this.m_timer = new System.Windows.Forms.Timer(this.components);
			this.m_copy = new System.Windows.Forms.Button();
			this.m_connectionParameters.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_port)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_grid)).BeginInit();
			this.SuspendLayout();
			// 
			// m_connectionParameters
			// 
			this.m_connectionParameters.Controls.Add(this.m_port);
			this.m_connectionParameters.Controls.Add(this.m_ssl);
			this.m_connectionParameters.Controls.Add(this.m_portLabel);
			this.m_connectionParameters.Controls.Add(this.m_password);
			this.m_connectionParameters.Controls.Add(this.m_username);
			this.m_connectionParameters.Controls.Add(this.m_address);
			this.m_connectionParameters.Controls.Add(this.m_passwordLabel);
			this.m_connectionParameters.Controls.Add(this.m_usernameLabel);
			this.m_connectionParameters.Controls.Add(this.m_addressLabel);
			this.m_connectionParameters.Location = new System.Drawing.Point(12, 12);
			this.m_connectionParameters.Name = "m_connectionParameters";
			this.m_connectionParameters.Size = new System.Drawing.Size(424, 127);
			this.m_connectionParameters.TabIndex = 13;
			this.m_connectionParameters.TabStop = false;
			this.m_connectionParameters.Text = "Connection parameters";
			// 
			// m_port
			// 
			this.m_port.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::RealTimeLevel2.Settings.Default, "Port", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.m_port.Location = new System.Drawing.Point(108, 46);
			this.m_port.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.m_port.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.m_port.Name = "m_port";
			this.m_port.Size = new System.Drawing.Size(91, 20);
			this.m_port.TabIndex = 14;
			this.m_port.Value = global::RealTimeLevel2.Settings.Default.Port;
			// 
			// m_ssl
			// 
			this.m_ssl.AutoSize = true;
			this.m_ssl.Checked = global::RealTimeLevel2.Settings.Default.SSL;
			this.m_ssl.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_ssl.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::RealTimeLevel2.Settings.Default, "SSL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.m_ssl.Location = new System.Drawing.Point(224, 49);
			this.m_ssl.Name = "m_ssl";
			this.m_ssl.Size = new System.Drawing.Size(46, 17);
			this.m_ssl.TabIndex = 11;
			this.m_ssl.Text = "SSL";
			this.m_ssl.UseVisualStyleBackColor = true;
			// 
			// m_portLabel
			// 
			this.m_portLabel.AutoSize = true;
			this.m_portLabel.Location = new System.Drawing.Point(51, 53);
			this.m_portLabel.Name = "m_portLabel";
			this.m_portLabel.Size = new System.Drawing.Size(26, 13);
			this.m_portLabel.TabIndex = 9;
			this.m_portLabel.Text = "Port";
			// 
			// m_password
			// 
			this.m_password.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RealTimeLevel2.Settings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.m_password.Location = new System.Drawing.Point(108, 98);
			this.m_password.Name = "m_password";
			this.m_password.Size = new System.Drawing.Size(282, 20);
			this.m_password.TabIndex = 7;
			this.m_password.Text = global::RealTimeLevel2.Settings.Default.Password;
			this.m_password.UseSystemPasswordChar = true;
			// 
			// m_username
			// 
			this.m_username.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RealTimeLevel2.Settings.Default, "Username", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.m_username.Location = new System.Drawing.Point(108, 72);
			this.m_username.Name = "m_username";
			this.m_username.Size = new System.Drawing.Size(282, 20);
			this.m_username.TabIndex = 6;
			this.m_username.Text = global::RealTimeLevel2.Settings.Default.Username;
			// 
			// m_tradingPlatformAddress
			// 
			this.m_address.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RealTimeLevel2.Settings.Default, "Address", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.m_address.Location = new System.Drawing.Point(108, 20);
			this.m_address.Name = "m_tradingPlatformAddress";
			this.m_address.Size = new System.Drawing.Size(282, 20);
			this.m_address.TabIndex = 5;
			this.m_address.Text = global::RealTimeLevel2.Settings.Default.Address;
			// 
			// m_passwordLabel
			// 
			this.m_passwordLabel.AutoSize = true;
			this.m_passwordLabel.Location = new System.Drawing.Point(24, 105);
			this.m_passwordLabel.Name = "m_passwordLabel";
			this.m_passwordLabel.Size = new System.Drawing.Size(53, 13);
			this.m_passwordLabel.TabIndex = 3;
			this.m_passwordLabel.Text = "Password";
			// 
			// m_usernameLabel
			// 
			this.m_usernameLabel.AutoSize = true;
			this.m_usernameLabel.Location = new System.Drawing.Point(22, 79);
			this.m_usernameLabel.Name = "m_usernameLabel";
			this.m_usernameLabel.Size = new System.Drawing.Size(55, 13);
			this.m_usernameLabel.TabIndex = 2;
			this.m_usernameLabel.Text = "Username";
			// 
			// m_addressLabel
			// 
			this.m_addressLabel.AutoSize = true;
			this.m_addressLabel.Location = new System.Drawing.Point(32, 27);
			this.m_addressLabel.Name = "m_addressLabel";
			this.m_addressLabel.Size = new System.Drawing.Size(45, 13);
			this.m_addressLabel.TabIndex = 1;
			this.m_addressLabel.Text = "Address";
			// 
			// m_symbols
			// 
			this.m_symbols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_symbols.FormattingEnabled = true;
			this.m_symbols.Location = new System.Drawing.Point(13, 185);
			this.m_symbols.Name = "m_symbols";
			this.m_symbols.Size = new System.Drawing.Size(182, 21);
			this.m_symbols.TabIndex = 14;
			this.m_symbols.SelectedIndexChanged += new System.EventHandler(this.OnSymbolsSelectedIndexChanged);
			// 
			// m_grid
			// 
			this.m_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.m_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BidVolume,
            this.Price,
            this.AskVolume});
			this.m_grid.Enabled = false;
			this.m_grid.Location = new System.Drawing.Point(13, 227);
			this.m_grid.Name = "m_grid";
			this.m_grid.Size = new System.Drawing.Size(423, 324);
			this.m_grid.TabIndex = 15;
			// 
			// BidVolume
			// 
			this.BidVolume.HeaderText = "Bid Volume";
			this.BidVolume.Name = "BidVolume";
			this.BidVolume.Width = 127;
			// 
			// Price
			// 
			this.Price.HeaderText = "Price";
			this.Price.Name = "Price";
			this.Price.Width = 126;
			// 
			// AskVolume
			// 
			this.AskVolume.HeaderText = "Ask Volume";
			this.AskVolume.Name = "AskVolume";
			this.AskVolume.Width = 127;
			// 
			// m_stop
			// 
			this.m_stop.Enabled = false;
			this.m_stop.Location = new System.Drawing.Point(215, 145);
			this.m_stop.Name = "m_stop";
			this.m_stop.Size = new System.Drawing.Size(75, 23);
			this.m_stop.TabIndex = 17;
			this.m_stop.Text = "Stop";
			this.m_stop.UseVisualStyleBackColor = true;
			this.m_stop.Click += new System.EventHandler(this.OnStop);
			// 
			// m_start
			// 
			this.m_start.Location = new System.Drawing.Point(120, 145);
			this.m_start.Name = "m_start";
			this.m_start.Size = new System.Drawing.Size(75, 23);
			this.m_start.TabIndex = 16;
			this.m_start.Text = "Start";
			this.m_start.UseVisualStyleBackColor = true;
			this.m_start.Click += new System.EventHandler(this.OnStart);
			// 
			// m_save
			// 
			this.m_save.Location = new System.Drawing.Point(25, 145);
			this.m_save.Name = "m_save";
			this.m_save.Size = new System.Drawing.Size(75, 23);
			this.m_save.TabIndex = 15;
			this.m_save.Text = "Save";
			this.m_save.UseVisualStyleBackColor = true;
			this.m_save.Click += new System.EventHandler(this.OnSave);
			// 
			// m_timer
			// 
			this.m_timer.Enabled = true;
			this.m_timer.Interval = 250;
			this.m_timer.Tick += new System.EventHandler(this.OnRefresh);
			// 
			// m_copy
			// 
			this.m_copy.Location = new System.Drawing.Point(215, 183);
			this.m_copy.Name = "m_copy";
			this.m_copy.Size = new System.Drawing.Size(75, 23);
			this.m_copy.TabIndex = 18;
			this.m_copy.Text = "Copy JSON";
			this.m_copy.UseVisualStyleBackColor = true;
			this.m_copy.Click += new System.EventHandler(this.OnCopy);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(474, 563);
			this.Controls.Add(this.m_copy);
			this.Controls.Add(this.m_stop);
			this.Controls.Add(this.m_grid);
			this.Controls.Add(this.m_start);
			this.Controls.Add(this.m_save);
			this.Controls.Add(this.m_symbols);
			this.Controls.Add(this.m_connectionParameters);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Real Time Level2";
			this.m_connectionParameters.ResumeLayout(false);
			this.m_connectionParameters.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_port)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_grid)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox m_connectionParameters;
        private System.Windows.Forms.CheckBox m_ssl;
        private System.Windows.Forms.Label m_portLabel;
        private System.Windows.Forms.TextBox m_password;
        private System.Windows.Forms.TextBox m_username;
        private System.Windows.Forms.TextBox m_address;
        private System.Windows.Forms.Label m_passwordLabel;
        private System.Windows.Forms.Label m_usernameLabel;
        private System.Windows.Forms.Label m_addressLabel;
        private System.Windows.Forms.ComboBox m_symbols;
        private System.Windows.Forms.DataGridView m_grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn BidVolume;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn AskVolume;
        private System.Windows.Forms.NumericUpDown m_port;
        private System.Windows.Forms.Button m_stop;
        private System.Windows.Forms.Button m_start;
        private System.Windows.Forms.Button m_save;
		private System.Windows.Forms.Timer m_timer;
		private System.Windows.Forms.Button m_copy;
    }
}

