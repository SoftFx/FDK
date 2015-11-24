namespace SoftFX.Extended.Financial
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents container for 
    /// </summary>
    /// <typeparam name="TEntry"></typeparam>
    public class FinancialEntries<TEntry> : ICollection<TEntry>
        where TEntry : FinancialEntry
    {
        #region Construction

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner">Owner object.</param>
        public FinancialEntries(object owner)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            this.owner = owner;
            this.entries = new List<TEntry>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TEntry this[int index]
        {
            get
            {
                return this.entries[index];
            }
        }

        /// <summary>
        /// Returns number of symbol entries.
        /// </summary>
        public int Count
        {
            get
            {
                return this.entries.Count;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry">Financial entry.</param>
        public virtual void Add(TEntry entry)
        {
            this.CheckNullAndOwner(entry);
            if (entry.ArrayIndex >= 0)
                throw new ArgumentException("The entry is already added to the collection");

            this.entries.Add(entry);
            entry.ArrayIndex = this.entries.Count - 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry">Financial entry.</param>
        public virtual bool Remove(TEntry entry)
        {
            this.CheckNullAndOwner(entry);
            if (entry.ArrayIndex < 0)
                throw new ArgumentException("The entry is not added to collection");

            var lastIndex = this.entries.Count - 1;
            var lastEntry = this.entries[lastIndex];
            lastEntry.ArrayIndex = entry.ArrayIndex;

            this.entries[entry.ArrayIndex] = lastEntry;
            this.entries.RemoveAt(lastIndex);
            entry.ArrayIndex = -1;

            return true;
        }

        /// <summary>
        /// Clears collection.
        /// </summary>
        public virtual void Clear()
        {
            foreach (var element in this.entries)
            {
                element.ArrayIndex = -1;
            }
            this.entries.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(TEntry item)
        {
            return this.entries.Contains(item);
        }

        void CheckNullAndOwner(TEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException("entry");

            if (this.owner != entry.Owner)
                throw new ArgumentException("Invalid owner");
        }

        #endregion

        #region ICollection

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        void ICollection<TEntry>.CopyTo(TEntry[] array, int arrayIndex)
        {
            this.entries.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        bool ICollection<TEntry>.IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region IEnumerable

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TEntry> GetEnumerator()
        {
            return this.entries.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.entries.GetEnumerator();
        }

        #endregion

        #region Fields

        readonly IList<TEntry> entries;
        readonly object owner;

        #endregion
    }
}
