namespace SoftFX.Lrp.Implementation
{
    using System;

    struct Transaction : IDisposable
    {
        public Transaction(IDisposable owner)
            : this()
        {
            this.owner = owner;
        }

        public void Commit()
        {
            this.committed = true;
        }

        public void Dispose()
        {
            if (!this.committed && this.owner != null)
                this.owner.Dispose();
        }

        #region Fields

        bool committed;
        readonly IDisposable owner;

        #endregion
    }
}
