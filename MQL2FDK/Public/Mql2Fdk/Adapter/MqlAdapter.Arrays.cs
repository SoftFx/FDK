namespace Mql2Fdk
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region Array Functions

        /// <summary>
        /// Sets a new size for the first dimension.
        /// If executed successfully, it returns count of all elements contained in the array after resizing, 
        /// otherwise, returns -1, and array is not resized.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="new_size"></param>
        /// <returns></returns>
        protected static int ArrayResize<T>(ref T[] array, int new_size)
        {
            Array.Resize(ref array, new_size);
            return new_size;
        }

        /// <summary>
        /// Returns the count of elements contained in the array. For a one-dimensional array, 
        /// the value to be returned by the ArraySize function is equal to that of ArrayRange(array,0). 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        protected static int ArraySize(Array array)
        {
            return array.Length;
        }

        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        protected static void ArraySort<T>(T[] array)
        {
            Array.Sort(array);
        }

        #endregion
    }
}
