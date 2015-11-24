namespace FinancialExample
{
	partial class ServerDialog
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
			this.m_dateTime = new System.Windows.Forms.DateTimePicker();
			this.m_connectionParameters = new System.Windows.Forms.GroupBox();
			this.m_ssl = new System.Windows.Forms.CheckBox();
			this.m_port = new System.Windows.Forms.TextBox();
			this.m_portLabel = new System.Windows.Forms.Label();
			this.m_save = new System.Windows.Forms.Button();
			this.m_password = new System.Windows.Forms.TextBox();
			this.m_username = new System.Windows.Forms.TextBox();
			this.m_address = new System.Windows.Forms.TextBox();
			this.m_passwordLabel = new System.Windows.Forms.Label();
			this.m_usernameLabel = new System.Windows.Forms.Label();
			this.m_addressLabel = new System.Windows.Forms.Label();
			this.m_useContractSize = new System.Windows.Forms.CheckBox();
			this.m_get = new System.Windows.Forms.Button();
			this.m_cancel = new System.Windows.Forms.Button();
			this.m_log = new System.Windows.Forms.ListBox();
			this.m_connectionParameters.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_dateTime
			// 
			this.m_dateTime.CustomFormat = "yyyy-MM-dd  HH:mm:ss";
			this.m_dateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.m_dateTime.Location = new System.Drawing.Point(16, 184);
			this.m_dateTime.Name = "m_dateTime";
			this.m_dateTime.Size = new System.Drawing.Size(200, 20);
			this.m_dateTime.TabIndex = 0;
			// 
			// m_connectionParameters
			// 
			this.m_connectionParameters.Controls.Add(this.m_ssl);
			this.m_connectionParameters.Controls.Add(this.m_port);
			this.m_connectionParameters.Controls.Add(this.m_portLabel);
			this.m_connectionParameters.Controls.Add(this.m_save);
			this.m_connectionParameters.Controls.Add(this.m_password);
			this.m_connectionParameters.Controls.Add(this.m_username);
			this.m_connectionParameters.Controls.Add(this.m_address);
			this.m_connectionParameters.Controls.Add(this.m_passwordLabel);
			this.m_connectionParameters.Controls.Add(this.m_usernameLabel);
			this.m_connectionParameters.Controls.Add(this.m_addressLabel);
			this.m_connectionParameters.Location = new System.Drawing.Point(16, 12);
			this.m_connectionParameters.Name = "m_connectionParameters";
			this.m_connectionParameters.Size = new System.Drawing.Size(424, 166);
			this.m_connectionParameters.TabIndex = 13;
			this.m_connectionParameters.TabStop = false;
			this.m_connectionParameters.Text = "Connection parameters";
			// 
			// m_ssl
			// 
			this.m_ssl.AutoSize = true;
			this.m_ssl.Checked = global::FinancialExample.Settings.Default.SSL;
			this.m_ssl.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FinancialExample.Settings.Default, "SSL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.m_ssl.Location = new System.Drawing.Point(224, 49);
			this.m_ssl.Name = "m_ssl";
			this.m_ssl.Size = new System.Drawing.Size(46, 17);
			this.m_ssl.TabIndex = 11;
			this.m_ssl.Text = "SSL";
			this.m_ssl.UseVisualStyleBackColor = true;
			// 
			// m_port
			// 
			this.m_port.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FinancialExample.Settings.Default, "Port", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.m_port.Location = new System.Drawing.Point(108, 46);
			this.m_port.Name = "m_port";
			this.m_port.Size = new System.Drawing.Size(100, 20);
			this.m_port.TabIndex = 10;
			this.m_port.Text = global::FinancialExample.Settings.Default.Port;
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
			// m_save
			// 
			this.m_save.Location = new System.Drawing.Point(108, 127);
			this.m_save.Name = "m_save";
			this.m_save.Size = new System.Drawing.Size(75, 23);
			this.m_save.TabIndex = 8;
			this.m_save.Text = "Save";
			this.m_save.UseVisualStyleBackColor = true;
			this.m_save.Click += new System.EventHandler(this.OnSave);
			// 
			// m_password
			// 
			this.m_password.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FinancialExample.Settings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.m_password.Location = new System.Drawing.Point(108, 98);
			this.m_password.Name = "m_password";
			this.m_password.Size = new System.Drawing.Size(282, 20);
			this.m_password.TabIndex = 7;
			this.m_password.Text = global::FinancialExample.Settings.Default.Password;
			this.m_password.UseSystemPasswordChar = true;
			// 
			// m_username
			// 
			this.m_username.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FinancialExample.Settings.Default, "Username", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.m_username.Location = new System.Drawing.Point(108, 72);
			this.m_username.Name = "m_username";
			this.m_username.Size = new System.Drawing.Size(282, 20);
			this.m_username.TabIndex = 6;
			this.m_username.Text = global::FinancialExample.Settings.Default.Username;
			// 
			// m_address
			// 
			this.m_address.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FinancialExample.Settings.Default, "Address", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.m_address.Location = new System.Drawing.Point(108, 20);
			this.m_address.Name = "m_address";
			this.m_address.Size = new System.Drawing.Size(282, 20);
			this.m_address.TabIndex = 5;
			this.m_address.Text = global::FinancialExample.Settings.Default.Address;
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
			// m_useContractSize
			// 
			this.m_useContractSize.AutoSize = true;
			this.m_useContractSize.Location = new System.Drawing.Point(240, 184);
			this.m_useContractSize.Name = "m_useContractSize";
			this.m_useContractSize.Size = new System.Drawing.Size(108, 17);
			this.m_useContractSize.TabIndex = 14;
			this.m_useContractSize.Text = "Use contract size";
			this.m_useContractSize.UseVisualStyleBackColor = true;
			// 
			// m_get
			// 
			this.m_get.Location = new System.Drawing.Point(124, 225);
			this.m_get.Name = "m_get";
			this.m_get.Size = new System.Drawing.Size(75, 23);
			this.m_get.TabIndex = 15;
			this.m_get.Text = "Get";
			this.m_get.UseVisualStyleBackColor = true;
			this.m_get.Click += new System.EventHandler(this.OnGet);
			// 
			// m_cancel
			// 
			this.m_cancel.Location = new System.Drawing.Point(240, 225);
			this.m_cancel.Name = "m_cancel";
			this.m_cancel.Size = new System.Drawing.Size(75, 23);
			this.m_cancel.TabIndex = 16;
			this.m_cancel.Text = "Cancel";
			this.m_cancel.UseVisualStyleBackColor = true;
			this.m_cancel.Click += new System.EventHandler(this.OnCancel);
			// 
			// m_log
			// 
			this.m_log.FormattingEnabled = true;
			this.m_log.Location = new System.Drawing.Point(16, 278);
			this.m_log.Name = "m_log";
			this.m_log.Size = new System.Drawing.Size(424, 238);
			this.m_log.TabIndex = 17;
			// 
			// ServerDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 537);
			this.Controls.Add(this.m_log);
			this.Controls.Add(this.m_cancel);
			this.Controls.Add(this.m_get);
			this.Controls.Add(this.m_useContractSize);
			this.Controls.Add(this.m_connectionParameters);
			this.Controls.Add(this.m_dateTime);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ServerDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Get Symbols and Quotes from Server";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
			this.m_connectionParameters.ResumeLayout(false);
			this.m_connectionParameters.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker m_dateTime;
		private System.Windows.Forms.GroupBox m_connectionParameters;
		private System.Windows.Forms.CheckBox m_ssl;
		private System.Windows.Forms.TextBox m_port;
		private System.Windows.Forms.Label m_portLabel;
		private System.Windows.Forms.Button m_save;
		private System.Windows.Forms.TextBox m_password;
		private System.Windows.Forms.TextBox m_username;
		private System.Windows.Forms.TextBox m_address;
		private System.Windows.Forms.Label m_passwordLabel;
		private System.Windows.Forms.Label m_usernameLabel;
		private System.Windows.Forms.Label m_addressLabel;
		private System.Windows.Forms.CheckBox m_useContractSize;
		private System.Windows.Forms.Button m_get;
		private System.Windows.Forms.Button m_cancel;
		private System.Windows.Forms.ListBox m_log;
	}
}