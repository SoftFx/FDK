namespace FinancialExample
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
            System.Windows.Forms.TabControl m_tabs;
            System.Windows.Forms.TabPage calculatorTab;
            System.Windows.Forms.TabPage symbolsTab;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
            System.Windows.Forms.ContextMenuStrip m_currenciesContextMenu;
            System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem4;
            System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem4;
            System.Windows.Forms.ToolStripMenuItem addMajorCurrenciesToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
            System.Windows.Forms.TabPage accountsAndTradesTab;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            System.Windows.Forms.MenuStrip mainMenu;
            System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem getSymbolsAndQuotesToolStripMenuItem;
            System.Windows.Forms.Label label1;
            this.m_calculatorTabPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.m_symbols = new System.Windows.Forms.ListBox();
            this.m_symbolsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_symbolsTabPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.currenciesTab = new System.Windows.Forms.TabPage();
            this.m_currencies = new System.Windows.Forms.ListBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quotesTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.m_quotes = new System.Windows.Forms.ListBox();
            this.m_quotesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_quotesTabPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.m_accounts = new System.Windows.Forms.ListBox();
            this.m_acccountsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_trades = new System.Windows.Forms.ListBox();
            this.m_tradesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_accountsTabPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_calculate = new System.Windows.Forms.Button();
            this.m_clear = new System.Windows.Forms.Button();
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_calculationTime = new System.Windows.Forms.TextBox();
            m_tabs = new System.Windows.Forms.TabControl();
            calculatorTab = new System.Windows.Forms.TabPage();
            symbolsTab = new System.Windows.Forms.TabPage();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            m_currenciesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            addToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            removeToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            addMajorCurrenciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            accountsAndTradesTab = new System.Windows.Forms.TabPage();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            mainMenu = new System.Windows.Forms.MenuStrip();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            getSymbolsAndQuotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            label1 = new System.Windows.Forms.Label();
            m_tabs.SuspendLayout();
            calculatorTab.SuspendLayout();
            symbolsTab.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            this.m_symbolsContextMenu.SuspendLayout();
            this.currenciesTab.SuspendLayout();
            m_currenciesContextMenu.SuspendLayout();
            this.quotesTab.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.m_quotesContextMenu.SuspendLayout();
            accountsAndTradesTab.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            this.m_acccountsContextMenu.SuspendLayout();
            this.m_tradesContextMenu.SuspendLayout();
            mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabs
            // 
            m_tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_tabs.Controls.Add(calculatorTab);
            m_tabs.Controls.Add(symbolsTab);
            m_tabs.Controls.Add(this.currenciesTab);
            m_tabs.Controls.Add(this.quotesTab);
            m_tabs.Controls.Add(accountsAndTradesTab);
            m_tabs.Location = new System.Drawing.Point(0, 25);
            m_tabs.Name = "m_tabs";
            m_tabs.SelectedIndex = 0;
            m_tabs.Size = new System.Drawing.Size(784, 377);
            m_tabs.TabIndex = 0;
            // 
            // calculatorTab
            // 
            calculatorTab.Controls.Add(this.m_calculatorTabPropertyGrid);
            calculatorTab.Location = new System.Drawing.Point(4, 22);
            calculatorTab.Name = "calculatorTab";
            calculatorTab.Padding = new System.Windows.Forms.Padding(3);
            calculatorTab.Size = new System.Drawing.Size(776, 351);
            calculatorTab.TabIndex = 0;
            calculatorTab.Text = "Calculator";
            calculatorTab.UseVisualStyleBackColor = true;
            // 
            // m_calculatorTabPropertyGrid
            // 
            this.m_calculatorTabPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_calculatorTabPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.m_calculatorTabPropertyGrid.Name = "m_calculatorTabPropertyGrid";
            this.m_calculatorTabPropertyGrid.Size = new System.Drawing.Size(770, 345);
            this.m_calculatorTabPropertyGrid.TabIndex = 0;
            // 
            // symbolsTab
            // 
            symbolsTab.Controls.Add(tableLayoutPanel2);
            symbolsTab.Location = new System.Drawing.Point(4, 22);
            symbolsTab.Name = "symbolsTab";
            symbolsTab.Padding = new System.Windows.Forms.Padding(3);
            symbolsTab.Size = new System.Drawing.Size(776, 351);
            symbolsTab.TabIndex = 1;
            symbolsTab.Text = "Symbols";
            symbolsTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(this.m_symbols, 0, 0);
            tableLayoutPanel2.Controls.Add(this.m_symbolsTabPropertyGrid, 1, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new System.Drawing.Size(770, 345);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // m_symbols
            // 
            this.m_symbols.ContextMenuStrip = this.m_symbolsContextMenu;
            this.m_symbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_symbols.FormattingEnabled = true;
            this.m_symbols.Location = new System.Drawing.Point(3, 3);
            this.m_symbols.Name = "m_symbols";
            this.m_symbols.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.m_symbols.Size = new System.Drawing.Size(379, 339);
            this.m_symbols.TabIndex = 0;
            this.m_symbols.SelectedIndexChanged += new System.EventHandler(this.OnSelectedSymbolsChanged);
            // 
            // m_symbolsContextMenu
            // 
            this.m_symbolsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem2,
            this.removeToolStripMenuItem2});
            this.m_symbolsContextMenu.Name = "m_symbolsContextMenu";
            this.m_symbolsContextMenu.Size = new System.Drawing.Size(118, 48);
            // 
            // addToolStripMenuItem2
            // 
            this.addToolStripMenuItem2.Name = "addToolStripMenuItem2";
            this.addToolStripMenuItem2.Size = new System.Drawing.Size(117, 22);
            this.addToolStripMenuItem2.Text = "Add";
            this.addToolStripMenuItem2.Click += new System.EventHandler(this.OnAddSymbol);
            // 
            // removeToolStripMenuItem2
            // 
            this.removeToolStripMenuItem2.Name = "removeToolStripMenuItem2";
            this.removeToolStripMenuItem2.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem2.Text = "Remove";
            this.removeToolStripMenuItem2.Click += new System.EventHandler(this.OnRemoveSymbol);
            // 
            // m_symbolsTabPropertyGrid
            // 
            this.m_symbolsTabPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_symbolsTabPropertyGrid.Location = new System.Drawing.Point(388, 3);
            this.m_symbolsTabPropertyGrid.Name = "m_symbolsTabPropertyGrid";
            this.m_symbolsTabPropertyGrid.Size = new System.Drawing.Size(379, 339);
            this.m_symbolsTabPropertyGrid.TabIndex = 1;
            // 
            // currenciesTab
            // 
            this.currenciesTab.Controls.Add(this.m_currencies);
            this.currenciesTab.Location = new System.Drawing.Point(4, 22);
            this.currenciesTab.Name = "currenciesTab";
            this.currenciesTab.Padding = new System.Windows.Forms.Padding(3);
            this.currenciesTab.Size = new System.Drawing.Size(776, 351);
            this.currenciesTab.TabIndex = 4;
            this.currenciesTab.Text = "Currencies";
            this.currenciesTab.UseVisualStyleBackColor = true;
            // 
            // m_currencies
            // 
            this.m_currencies.ContextMenuStrip = m_currenciesContextMenu;
            this.m_currencies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_currencies.FormattingEnabled = true;
            this.m_currencies.Location = new System.Drawing.Point(3, 3);
            this.m_currencies.Name = "m_currencies";
            this.m_currencies.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.m_currencies.Size = new System.Drawing.Size(770, 345);
            this.m_currencies.TabIndex = 0;
            // 
            // m_currenciesContextMenu
            // 
            m_currenciesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            addToolStripMenuItem4,
            removeToolStripMenuItem4,
            addMajorCurrenciesToolStripMenuItem,
            this.toolStripSeparator1,
            moveUpToolStripMenuItem,
            moveDownToolStripMenuItem});
            m_currenciesContextMenu.Name = "m_currenciesContextMenu";
            m_currenciesContextMenu.Size = new System.Drawing.Size(190, 120);
            // 
            // addToolStripMenuItem4
            // 
            addToolStripMenuItem4.Name = "addToolStripMenuItem4";
            addToolStripMenuItem4.Size = new System.Drawing.Size(189, 22);
            addToolStripMenuItem4.Text = "Add";
            addToolStripMenuItem4.Click += new System.EventHandler(this.OnAddCurrency);
            // 
            // removeToolStripMenuItem4
            // 
            removeToolStripMenuItem4.Name = "removeToolStripMenuItem4";
            removeToolStripMenuItem4.Size = new System.Drawing.Size(189, 22);
            removeToolStripMenuItem4.Text = "Remove";
            removeToolStripMenuItem4.Click += new System.EventHandler(this.OnRemoveCurrency);
            // 
            // addMajorCurrenciesToolStripMenuItem
            // 
            addMajorCurrenciesToolStripMenuItem.Name = "addMajorCurrenciesToolStripMenuItem";
            addMajorCurrenciesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            addMajorCurrenciesToolStripMenuItem.Text = "Add Major Currencies";
            addMajorCurrenciesToolStripMenuItem.Click += new System.EventHandler(this.OnAddMajorCurrencies);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // moveUpToolStripMenuItem
            // 
            moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            moveUpToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            moveUpToolStripMenuItem.Text = "Move Up";
            moveUpToolStripMenuItem.Click += new System.EventHandler(this.OnMoveUpCurrency);
            // 
            // moveDownToolStripMenuItem
            // 
            moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            moveDownToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            moveDownToolStripMenuItem.Text = "Move Down";
            moveDownToolStripMenuItem.Click += new System.EventHandler(this.OnMoveDownCurrency);
            // 
            // quotesTab
            // 
            this.quotesTab.Controls.Add(this.tableLayoutPanel3);
            this.quotesTab.Location = new System.Drawing.Point(4, 22);
            this.quotesTab.Name = "quotesTab";
            this.quotesTab.Padding = new System.Windows.Forms.Padding(3);
            this.quotesTab.Size = new System.Drawing.Size(776, 351);
            this.quotesTab.TabIndex = 2;
            this.quotesTab.Text = "Quotes";
            this.quotesTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.m_quotes, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.m_quotesTabPropertyGrid, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(770, 345);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // m_quotes
            // 
            this.m_quotes.ContextMenuStrip = this.m_quotesContextMenu;
            this.m_quotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_quotes.FormattingEnabled = true;
            this.m_quotes.Location = new System.Drawing.Point(3, 3);
            this.m_quotes.Name = "m_quotes";
            this.m_quotes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.m_quotes.Size = new System.Drawing.Size(379, 339);
            this.m_quotes.Sorted = true;
            this.m_quotes.TabIndex = 0;
            this.m_quotes.SelectedIndexChanged += new System.EventHandler(this.OnSelectedQuotesChanged);
            // 
            // m_quotesContextMenu
            // 
            this.m_quotesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem3,
            this.removeToolStripMenuItem3});
            this.m_quotesContextMenu.Name = "m_quotesContextMenu";
            this.m_quotesContextMenu.Size = new System.Drawing.Size(118, 48);
            // 
            // addToolStripMenuItem3
            // 
            this.addToolStripMenuItem3.Name = "addToolStripMenuItem3";
            this.addToolStripMenuItem3.Size = new System.Drawing.Size(117, 22);
            this.addToolStripMenuItem3.Text = "Add";
            this.addToolStripMenuItem3.Click += new System.EventHandler(this.OnAddQuote);
            // 
            // removeToolStripMenuItem3
            // 
            this.removeToolStripMenuItem3.Name = "removeToolStripMenuItem3";
            this.removeToolStripMenuItem3.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem3.Text = "Remove";
            this.removeToolStripMenuItem3.Click += new System.EventHandler(this.OnRemoveQuote);
            // 
            // m_quotesTabPropertyGrid
            // 
            this.m_quotesTabPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_quotesTabPropertyGrid.Location = new System.Drawing.Point(388, 3);
            this.m_quotesTabPropertyGrid.Name = "m_quotesTabPropertyGrid";
            this.m_quotesTabPropertyGrid.Size = new System.Drawing.Size(379, 339);
            this.m_quotesTabPropertyGrid.TabIndex = 1;
            // 
            // accountsAndTradesTab
            // 
            accountsAndTradesTab.Controls.Add(tableLayoutPanel1);
            accountsAndTradesTab.Location = new System.Drawing.Point(4, 22);
            accountsAndTradesTab.Name = "accountsAndTradesTab";
            accountsAndTradesTab.Padding = new System.Windows.Forms.Padding(3);
            accountsAndTradesTab.Size = new System.Drawing.Size(776, 351);
            accountsAndTradesTab.TabIndex = 3;
            accountsAndTradesTab.Text = "Accounts & Trades";
            accountsAndTradesTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            tableLayoutPanel1.Controls.Add(this.m_accounts, 0, 0);
            tableLayoutPanel1.Controls.Add(this.m_trades, 1, 0);
            tableLayoutPanel1.Controls.Add(this.m_accountsTabPropertyGrid, 2, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(770, 345);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // m_accounts
            // 
            this.m_accounts.ContextMenuStrip = this.m_acccountsContextMenu;
            this.m_accounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_accounts.FormattingEnabled = true;
            this.m_accounts.Location = new System.Drawing.Point(3, 3);
            this.m_accounts.Name = "m_accounts";
            this.m_accounts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.m_accounts.Size = new System.Drawing.Size(248, 339);
            this.m_accounts.TabIndex = 0;
            this.m_accounts.SelectedIndexChanged += new System.EventHandler(this.OnSelectedAccountsChanged);
            // 
            // m_acccountsContextMenu
            // 
            this.m_acccountsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.m_acccountsContextMenu.Name = "m_acccountsContextMenu";
            this.m_acccountsContextMenu.Size = new System.Drawing.Size(118, 48);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.OnAddAccount);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.OnRemoveAccount);
            // 
            // m_trades
            // 
            this.m_trades.ContextMenuStrip = this.m_tradesContextMenu;
            this.m_trades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_trades.FormattingEnabled = true;
            this.m_trades.Location = new System.Drawing.Point(257, 3);
            this.m_trades.Name = "m_trades";
            this.m_trades.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.m_trades.Size = new System.Drawing.Size(248, 339);
            this.m_trades.TabIndex = 1;
            this.m_trades.SelectedIndexChanged += new System.EventHandler(this.OnSelectedTradesChanged);
            // 
            // m_tradesContextMenu
            // 
            this.m_tradesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.removeToolStripMenuItem1});
            this.m_tradesContextMenu.Name = "m_tradesContextMenu";
            this.m_tradesContextMenu.Size = new System.Drawing.Size(118, 48);
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.addToolStripMenuItem1.Text = "Add";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.OnAddTrade);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem1.Text = "Remove";
            this.removeToolStripMenuItem1.Click += new System.EventHandler(this.OnRemoveTrade);
            // 
            // m_accountsTabPropertyGrid
            // 
            this.m_accountsTabPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_accountsTabPropertyGrid.Location = new System.Drawing.Point(511, 3);
            this.m_accountsTabPropertyGrid.Name = "m_accountsTabPropertyGrid";
            this.m_accountsTabPropertyGrid.Size = new System.Drawing.Size(256, 339);
            this.m_accountsTabPropertyGrid.TabIndex = 2;
            // 
            // mainMenu
            // 
            mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            serverToolStripMenuItem});
            mainMenu.Location = new System.Drawing.Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Size = new System.Drawing.Size(784, 24);
            mainMenu.TabIndex = 4;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            openToolStripMenuItem,
            saveToolStripMenuItem,
            exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            openToolStripMenuItem.Text = "&Open";
            openToolStripMenuItem.Click += new System.EventHandler(this.OnOpen);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            saveToolStripMenuItem.Text = "&Save";
            saveToolStripMenuItem.Click += new System.EventHandler(this.OnSave);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // serverToolStripMenuItem
            // 
            serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            getSymbolsAndQuotesToolStripMenuItem});
            serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            serverToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            serverToolStripMenuItem.Text = "Server";
            // 
            // getSymbolsAndQuotesToolStripMenuItem
            // 
            getSymbolsAndQuotesToolStripMenuItem.Name = "getSymbolsAndQuotesToolStripMenuItem";
            getSymbolsAndQuotesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            getSymbolsAndQuotesToolStripMenuItem.Text = "Get Symbols and Quotes";
            getSymbolsAndQuotesToolStripMenuItem.Click += new System.EventHandler(this.OnGetSymbolsAndQuotes);
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(234, 423);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(84, 13);
            label1.TabIndex = 5;
            label1.Text = "Calculation time:";
            // 
            // m_calculate
            // 
            this.m_calculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_calculate.Location = new System.Drawing.Point(24, 418);
            this.m_calculate.Name = "m_calculate";
            this.m_calculate.Size = new System.Drawing.Size(75, 23);
            this.m_calculate.TabIndex = 2;
            this.m_calculate.Text = "Calculate";
            this.m_calculate.UseVisualStyleBackColor = true;
            this.m_calculate.Click += new System.EventHandler(this.OnCalculate);
            // 
            // m_clear
            // 
            this.m_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_clear.Location = new System.Drawing.Point(129, 418);
            this.m_clear.Name = "m_clear";
            this.m_clear.Size = new System.Drawing.Size(75, 23);
            this.m_clear.TabIndex = 3;
            this.m_clear.Text = "Clear";
            this.m_clear.UseVisualStyleBackColor = true;
            this.m_clear.Click += new System.EventHandler(this.OnClear);
            // 
            // m_openFileDialog
            // 
            this.m_openFileDialog.Filter = "TXT files|*.txt|All files|*.*";
            // 
            // m_saveFileDialog
            // 
            this.m_saveFileDialog.Filter = "TXT files|*.txt|All files|*.*";
            // 
            // m_calculationTime
            // 
            this.m_calculationTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_calculationTime.Location = new System.Drawing.Point(340, 421);
            this.m_calculationTime.Name = "m_calculationTime";
            this.m_calculationTime.ReadOnly = true;
            this.m_calculationTime.Size = new System.Drawing.Size(100, 20);
            this.m_calculationTime.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.m_calculationTime);
            this.Controls.Add(label1);
            this.Controls.Add(mainMenu);
            this.Controls.Add(this.m_clear);
            this.Controls.Add(this.m_calculate);
            this.Controls.Add(m_tabs);
            this.MainMenuStrip = mainMenu;
            this.MinimumSize = new System.Drawing.Size(800, 200);
            this.Name = "MainForm";
            this.Text = "Financial Example";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            m_tabs.ResumeLayout(false);
            calculatorTab.ResumeLayout(false);
            symbolsTab.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            this.m_symbolsContextMenu.ResumeLayout(false);
            this.currenciesTab.ResumeLayout(false);
            m_currenciesContextMenu.ResumeLayout(false);
            this.quotesTab.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.m_quotesContextMenu.ResumeLayout(false);
            accountsAndTradesTab.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            this.m_acccountsContextMenu.ResumeLayout(false);
            this.m_tradesContextMenu.ResumeLayout(false);
            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabPage quotesTab;
		private System.Windows.Forms.ListBox m_accounts;
		private System.Windows.Forms.ListBox m_trades;
		private System.Windows.Forms.PropertyGrid m_accountsTabPropertyGrid;
		private System.Windows.Forms.ContextMenuStrip m_acccountsContextMenu;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip m_tradesContextMenu;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
		private System.Windows.Forms.Button m_calculate;
		private System.Windows.Forms.Button m_clear;
		private System.Windows.Forms.ListBox m_symbols;
		private System.Windows.Forms.PropertyGrid m_symbolsTabPropertyGrid;
		private System.Windows.Forms.ContextMenuStrip m_symbolsContextMenu;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem2;
		private System.Windows.Forms.PropertyGrid m_calculatorTabPropertyGrid;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.ListBox m_quotes;
		private System.Windows.Forms.PropertyGrid m_quotesTabPropertyGrid;
		private System.Windows.Forms.ContextMenuStrip m_quotesContextMenu;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog m_openFileDialog;
		private System.Windows.Forms.SaveFileDialog m_saveFileDialog;
		private System.Windows.Forms.TabPage currenciesTab;
		private System.Windows.Forms.ListBox m_currencies;
		private System.Windows.Forms.TextBox m_calculationTime;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}

