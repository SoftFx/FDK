namespace SoftFX.Extended.Storage
{
    using System;
    using System.Collections.Generic;
    using SoftFX.Extended.Core;
    using TickTrader.Server.Monitoring;
    using TickTrader.Server.QuoteHistory.Store;
    using TickTrader.Server.QuoteHistory.Store.Ntfs;
    
	/// <summary>
	/// The class contains all supported storage adapter types.
	/// </summary>
	public static class StorageProvider
	{
		#region Types

		/// <summary>
		/// Gets NtfsMultiMetaFiles.
		/// </summary>
		public static Type Ntfs { get; private set; }

		/// <summary>
		/// SQLite storage provider type.
		/// </summary>
		public static Type SQLite { get; private set; }

        ///// <summary>
        ///// Berkeley DB storage provider type.
        ///// </summary>
        //public static Type BerkeleyDB { get; private set; }

		/// <summary>
		/// Gets list of all possible storage providers.
		/// </summary>
		public static Dictionary<string, Type> Providers { get; private set; }

		static StorageProvider()
		{
            Native.Initialize();
            Construct();
		}

		static void Construct()
		{
			Ntfs = typeof(NtfsHistoryStore);
            Providers = new Dictionary<string, Type>
            {
                {"Ntfs",       Ntfs       }
            };
		}

		#endregion

		internal static IHistoryStore CreateStore(Type type, string location, IMonitoringService service)
		{
			var parameters = new object[] { location, service };
            var store = (IHistoryStore)Activator.CreateInstance(type, parameters);
            return store;
		}
	}
}
