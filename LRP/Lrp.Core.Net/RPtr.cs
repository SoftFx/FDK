namespace SoftFX.Lrp
{
    /// <summary>
    /// Remote pointer type.
    /// </summary>
    public struct RPtr
    {
        /// <summary>
        /// Creates a new instance of remote pointer type.
        /// </summary>
        /// <param name="handle">a remote native pointer.</param>
        public RPtr(long handle)
            : this()
        {
            this.Handle = handle;
        }

        /// <summary>
        /// Returns true, if it's not zero
        /// </summary>
        public bool IsZero
        {
            get
            {
                return this.Handle == 0;
            }
        }

        /// <summary>
        /// Gets remote native pointer.
        /// </summary>
        public long Handle { get; private set; }

        /// <summary>
        /// Gets zero native remote pointer.
        /// </summary>
        public static RPtr Zero
        {
            get
            {
                return new RPtr();
            }
        }

        /// <summary>
        /// Reset the remote pointer
        /// </summary>
        public void Clear()
        {
            this.Handle = 0;
        }

        /// <summary>
        /// Returns hash code of this.Handle.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int result = this.Handle.GetHashCode();
            return result;
        }

        /// <summary>
        /// Determines whether two specified instances of RPtr are not equal.
        /// </summary>
        /// <param name="first">an RPtr instance</param>
        /// <param name="second">an RPtr instance</param>
        /// <returns>true if first does not equal second; otherwise, false.</returns>
        public static bool operator == (RPtr first, RPtr second)
        {
            return first.Handle == second.Handle;
        }

        /// <summary>
        /// Determines whether two specified instances of RPtr are equal.
        /// </summary>
        /// <param name="first">an RPtr instance</param>
        /// <param name="second">an RPtr instance</param>
        /// <returns>true if first equals second; otherwise, false.</returns>
        public static bool operator != (RPtr first, RPtr second)
        {
            return first.Handle != second.Handle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var second = obj as RPtr?;
            if (second == null)
                return false;

            return Handle == second.Value.Handle;
        }
    }
}
