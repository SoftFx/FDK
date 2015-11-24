namespace SoftFX.Lrp
{
    using System;

    /// <summary>
    /// Local pointer type.
    /// </summary>
    public struct LPtr : IEquatable<LPtr>
    {
        #region Construction

        /// <summary>
        /// Creates a new instance of local pointer type.
        /// </summary>
        /// <param name="handle">a 64-bit integer valuer</param>
        public LPtr(long handle)
            : this()
        {
            this.handle = handle;
        }

        /// <summary>
        /// Creates a new instance of local pointer type.
        /// </summary>
        /// <param name="handle">a 32-bit integer valuer</param>
        public LPtr(int handle)
            : this()
        {
            this.handle = handle;
        }

        /// <summary>
        /// Creates a new instance of local pointer type.
        /// </summary>
        /// <param name="handle">a native pointer</param>
        public LPtr(IntPtr handle)
            : this()
        {
            this.handle = handle.ToInt64();
        }

        #endregion

        /// <summary>
        /// Returns true, if it's not null
        /// </summary>
        public bool IsZero
        {
            get
            {
                return this.handle == 0;
            }
        }
        /// <summary>
        /// Gets zero local pointer.
        /// </summary>
        public static LPtr Zero
        {
            get
            {
                return new LPtr();
            }
        }
        /// <summary>
        /// Gets a wrapped pointer as 32-bit integer value.
        /// </summary>
        public int ToInt32()
        {
            return (int)this.handle;
        }

        /// <summary>
        /// Gets a wrapped pointer as 64-bit integer value.
        /// </summary>
        public long ToInt64()
        {
            return this.handle;
        }

        /// <summary>
        /// Reset the local pointer
        /// </summary>
        public void Clear()
        {
            this.handle = 0;
        }

        /// <summary>
        /// Determines whether two specified instances of RPtr are not equal.
        /// </summary>
        /// <param name="first">an LPtr instance</param>
        /// <param name="second">an LPtr instance</param>
        /// <returns>true if first does not equal second; otherwise, false.</returns>
        public static bool operator ==(LPtr first, LPtr second)
        {
            return first.handle == second.handle;
        }
        /// <summary>
        /// Determines whether two specified instances of RPtr are equal.
        /// </summary>
        /// <param name="first">an LPtr instance</param>
        /// <param name="second">an LPtr instance</param>
        /// <returns>true if first equals second; otherwise, false.</returns>
        public static bool operator !=(LPtr first, LPtr second)
        {
            return first.handle != second.handle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj != null && typeof(LPtr) == obj.GetType())
            {
                var other = (LPtr)obj;
                return this.handle == other.handle;
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var result = this.handle.GetHashCode();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(LPtr other)
        {
            return this.handle == other.handle;
        }

        #region Fields

        long handle;

        #endregion
    }
}
