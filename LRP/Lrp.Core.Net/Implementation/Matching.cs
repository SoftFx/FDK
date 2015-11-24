namespace SoftFX.Lrp.Implementation
{
    using System;
    using System.Collections.Generic;

    class Matching<T>
        where T : class
    {
        public void Add(string name, T item)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (item == null)
                throw new ArgumentNullException("component");

            if (this.nameToItem.ContainsKey(name))
            {
                var message = string.Format("Duplicate item = {0}", name);
                throw new ArgumentException(message, "name");
            }

            this.nameToItem[name] = item;
        }

        public T Find(int id, string name)
        {
            for (var index = this.items.Count; index <= id; ++index)
            {
                var empty = new KeyValuePair<string, T>();
                this.items.Add(empty);
            }
            var entry = this.items[id];
            if (entry.Key == name)
                return entry.Value;

            T result;
            this.nameToItem.TryGetValue(name, out result);
            this.items[id] = new KeyValuePair<string, T>(name, result);
            return result;
        }

        #region Fields

        Dictionary<string, T> nameToItem = new Dictionary<string, T>();
        readonly List<KeyValuePair<string, T>> items = new List<KeyValuePair<string, T>>();

        #endregion
    }
}
