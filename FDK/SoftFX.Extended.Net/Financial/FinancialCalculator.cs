namespace SoftFX.Extended.Financial
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using SoftFX.Extended.Core;
    using SoftFX.Extended.Financial.Adapter;
    using SoftFX.Extended.Financial.Serialization;
    using TickTrader.BusinessLogic;

    /// <summary>
	/// Contains methods for offline calculation of profit and margin.
	/// </summary>
	public class FinancialCalculator
	{
		#region Construction and Serialization

		/// <summary>
		/// Creates a new financial calculator instance.
		/// </summary>
		public FinancialCalculator()
		{
            this.Prices = new PriceEntries();
            this.Accounts = new FinancialEntries<AccountEntry>(this);
            this.Symbols = new SymbolEntries(this);
            this.Currencies = new CurrencyEntries();
		}

		/// <summary>
		/// Creates a new financial calculator instance.
		/// </summary>
		/// <param name="handler">Price resolution handler.</param>
        [Obsolete("ResolvePriceHandler is not supported anymore, please use parameterless constructor.")]
		public FinancialCalculator(ResolvePriceHandler handler)
            : this()
		{
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

			this.handler = handler;
		}

		/// <summary>
		/// Load a financial calculator from a file.
		/// </summary>
		/// <param name="path"></param>
		public static FinancialCalculator Load(string path)
		{
			using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return Load(stream);
			}
		}

		/// <summary>
		/// Load a financial calculator from a stream.
		/// </summary>
		/// <param name="stream">Stream to load data from. Stream will not be closed by this method.</param>
		public static FinancialCalculator Load(Stream stream)
		{
			var reader = new StreamReader(stream);
			var text = reader.ReadToEnd();
			CalculatorData data = null;

			try
			{
				data = DoLoadAsXml(text);
			}
			catch
			{
			}

			if (data == null)
			{
				data = DoLoadAsTxt(text);
			}

			var result = data.CreateCalculator();
			return result;
		}

		static CalculatorData DoLoadAsXml(string text)
		{
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(text);
                    writer.Flush();
                    stream.Position = 0;

                    var serializer = new XmlSerializer(typeof(CalculatorData));
                    var obj = serializer.Deserialize(stream);
                    var result = (CalculatorData)obj;
                    return result;
                }
            }
		}

		static CalculatorData DoLoadAsTxt(string text)
		{
			var serializer = new Generated.Serializer(Native.LrpLlCommonClient);
			var result = serializer.Deserialize(text);
			return result;
		}

		/// <summary>
		/// Save the financial calculator to a file.
		/// </summary>
		/// <param name="path"></param>
		public void Save(string path)
		{
			using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				this.Save(stream);
			}
		}

		/// <summary>
		/// Save the financial calculator to a stream.
		/// </summary>
        /// <param name="stream">Stream to save data to. Stream will not be closed by this method.</param>
		public void Save(Stream stream)
		{
			var data = new CalculatorData(this);
			var serializer = new Generated.Serializer(Native.LrpLlCommonClient);
			var text = serializer.Serialize(data);
			var writer = new StreamWriter(stream);
			writer.Write(text);
			writer.Flush();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Recalculates margin and profit.
		/// </summary>
		public void Calculate()
		{
			this.Clear();

			this.Symbols.MakeIndex();

            this.MarketState = new MarketState(NettingCalculationTypes.OneByOne);

            this.MarketState.Set(this.Currencies.Select(CalculatorConvert.ToCurrencyInfo));
            this.MarketState.Set(this.Symbols.OrderBy(o => o.GroupSortOrder).ThenBy(o => o.SortOrder).Select(CalculatorConvert.ToSymbolInfo));
            foreach (var rate in this.Prices.Select(CalculatorConvert.ToSymbolRate))
                this.MarketState.Update(rate);

			foreach (var account in this.Accounts)
			{
                account.Calculate();
			}
		}

		/// <summary>
		/// Resets all calculated properties to null.
		/// </summary>
		public void Clear()
		{
			foreach (var element in this.Accounts)
			{
				element.Clear();
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets mode of margin calculation.
		/// </summary>
		[DisplayName("Margin Mode")]
		[DefaultValue(typeof(MarginMode), "Dynamic")]
        [Description("MarginMode is obsolete, only Dynamic margin mode supported.")]
        [Obsolete("MarginMode is obsolete, only Dynamic margin mode supported.")]
		public MarginMode MarginMode
		{
			get
			{
				return this.marginMode;
			}
			set
			{
                if (value != MarginMode.Dynamic && value != MarginMode.Static && value != MarginMode.StaticIfPossible)
				{
					var message = string.Format("Unsupported margin mode = {0}", value);
					throw new ArgumentException(message, nameof(value));
				}

				this.marginMode = value;
			}
		}

		/// <summary>
        /// Gets container, which manages all accounts.
		/// </summary>
		[Browsable(false)]
        public FinancialEntries<AccountEntry> Accounts { get; private set; }

		/// <summary>
        /// Gets container, which manages all prices.
		/// </summary>
		[Browsable(false)]
        public PriceEntries Prices { get; private set; }

		/// <summary>
		/// Gets container, which manages all symbols.
		/// </summary>
		[Browsable(false)]
		public SymbolEntries Symbols { get; private set; }

		/// <summary>
		/// Gets container, which manages list of major currencies.
		/// </summary>
		[Browsable(false)]
		public CurrencyEntries Currencies { get; private set; }

		#endregion

        #region Internal Properties

        internal MarketState MarketState { get; private set; }

        #endregion

        #region Fields

        MarginMode marginMode;
		readonly ResolvePriceHandler handler;

		#endregion
	}
}
