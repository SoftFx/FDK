namespace SoftFX.Basic
{
    using System;

    /// <summary>
    /// Provides atomic access to manager data.
    /// </summary>
    class SnapshotReader
    {
        /// <summary>
        /// Creates a new instance of Snapshot and locks manager instance to take snapshot data.
        /// </summary>
        /// <param name="manager"></param>
        public SnapshotReader(Manager manager)
        {
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));

            this.synchronizer = new object();

            manager.Acquire();
            this.manager = manager;
        }

        /// <summary>
        /// Unlocks manager.
        /// </summary>
        public void Dispose()
        {
            lock (this.synchronizer)
            {
                var m = this.manager;
                if (m == null)
                    return;

                this.manager = null;
                m.Release();
            }
        }

        #region Members

        readonly object synchronizer;
        Manager manager;

        #endregion
    }
}
