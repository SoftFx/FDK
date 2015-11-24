namespace Lrp.Engine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ListSet<T> : IList<T>
        where T : INamed
    {
        public int IndexOf(T item)
        {
            return this.data.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.ValidateItem(item);
            this.data.Insert(index, item);
            this.names.Add(item.Name);
        }

        public void RemoveAt(int index)
        {
            var item = this.data[index];
            this.names.Remove(item.Name);
            this.data.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return this.data[index];
            }
            set
            {
                var existingItem = this.data[index];
                if (existingItem.Name != value.Name)
                {
                    this.ValidateItem(value);
                    this.names.Remove(existingItem.Name);
                    this.names.Add(value.Name);
                }
                this.data[index] = value;
            }
        }

        public void Add(T item)
        {
            this.ValidateItem(item);
            this.names.Add(item.Name);
            this.data.Add(item);
        }

        public void Clear()
        {
            this.names.Clear();
            this.data.Clear();
        }

        public bool Contains(T item)
        {
            return this.data.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.data.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return this.data.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(T item)
        {
            if (this.names.Remove(item.Name))
            {
                this.data.Remove(item);
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        #region Private Methods

        void ValidateItem(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "An element can not be null");

            var name = item.Name;
            if (name == null)
                throw new ArgumentNullException("item", "An element name can not be null");

            if (this.names.Contains(name))
            {
                var message = string.Format("Duplicate element name = {0}", name);
                throw new ArgumentException(message, "item");
            }
        }

        #endregion

        #region Fields

        readonly List<T> data = new List<T>();
        readonly SortedSet<string> names = new SortedSet<string>();

        #endregion
    }
}
