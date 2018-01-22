using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SoftFX.Lrp.Implementation;

namespace SoftFX.Lrp
{
    /// <summary>
    ///
    /// </summary>
    public unsafe class MemoryBuffer : IDisposable
    {
        #region construction and destruction
        internal MemoryBuffer()
        {
            m_heap = s_heap.Handle;
            m_capacity = 2;
            m_data = (byte*)Alloc(m_capacity);
            if (null == m_data)
            {
                string message = string.Format("Could not allocate {0} bytes", m_capacity);
                throw new OutOfMemoryException(message);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="heap"></param>
        /// <param name="data"></param>
        /// <param name="size"></param>
        /// <param name="capacity"></param>
        public MemoryBuffer(void* heap, void* data, int size, int capacity)
        {
            m_heap = heap;
            m_data = (byte*)data;
            m_size = size;
            m_capacity = capacity;
            this.IsLPtr32Bit = MemoryBuffer.cIsLPtr32Bit;
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static MemoryBuffer CreateLocal()
        {
            MemoryBuffer result = s_localBuffer;
            if (null == result)
            {
                result = new MemoryBuffer();
                s_localBuffer = result;
            }
            result.Reset();
            result.IsLPtr32Bit = MemoryBuffer.cIsLPtr32Bit;
            return result;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static MemoryBuffer CreateLocal(int size)
        {
            MemoryBuffer result = s_localBuffer;
            if (null == result)
            {
                result = new MemoryBuffer();
                s_localBuffer = result;
            }
            result.Reset();
            if (result.Capacity < size)
            {
                result.ReAlloc(size);
            }
            result.m_size = size;
            result.IsLPtr32Bit = MemoryBuffer.cIsLPtr32Bit;
            return result;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public static MemoryBuffer CreateRemote(UInt16 componentId, UInt16 methodId)
        {
            MemoryBuffer result = null;
            UInt64 id = 0;
            lock (s_synchronizer)
            {
                id = ++s_counter;
                int index = s_buffers.Count - 1;
                if (index >= 0)
                {
                    result = s_buffers[index];
                    s_buffers.RemoveAt(index);
                }
            }
            if (null == result)
            {
                result = new MemoryBuffer();
            }
            result.Reset();
            result.WriteUInt32(0);
            result.WriteUInt64(id);
            result.WriteUInt16(componentId);
            result.WriteUInt16(methodId);
            result.IsLPtr32Bit = MemoryBuffer.cIsLPtr32Bit;
            return result;
        }
        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            if (this != s_localBuffer)
            {
                lock (s_synchronizer)
                {
                    s_buffers.Add(this);
                }
            }
        }
        /// <summary>
        ///
        /// </summary>
        ~MemoryBuffer()
        {
            Free();
        }
        internal void Free()
        {
            if (null != m_heap)
            {
                Free(m_data);
                m_heap = null;
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="offset"></param>
        public void Reset(int offset = 0)
        {
            m_position = offset;
            m_size = offset;
        }
        internal void Construct(int size)
        {
            if (m_capacity < size)
            {
                ReAlloc(size);
            }
            m_size = size;
            m_position = 0;
        }
        internal void ReInitialize(int newCapacity, int newSize, void* newData)
        {
            m_data = (byte*)newData;
            m_position = 0;
            m_size = newSize;
            m_capacity = newCapacity;
        }
        #endregion
        #region helper methods
        internal static void Swap(MemoryBuffer first, MemoryBuffer second)
        {
            byte* temporary = first.m_data;
            first.m_data = second.m_data;
            second.m_data = temporary;
            Swap(ref first.m_size, ref second.m_size);
            Swap(ref first.m_capacity, ref second.m_capacity);
            Swap(ref first.m_position, ref second.m_position);
        }
        internal void Swap(int* size, void** ppData, int* pCapacity)
        {
            byte* temporary = m_data;
            m_data = (byte*)(*ppData);
            *ppData = temporary;

            Swap(ref m_size, ref *size);
            Swap(ref m_capacity, ref *pCapacity);
        }
        private static void Swap<T>(ref T first, ref T second)
        {
            T temporary = first;
            first = second;
            second = temporary;
        }
        #endregion
        #region other methods
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            int count = this.Size;
            byte[] result = new byte[count];
            for (int index = 0; index < count; ++index)
            {
                result[index] = m_data[index];
            }
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public int ReadCount()
        {
            int newPosition = m_position + sizeof(int);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(int).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(int*)(m_data + m_position);
            if (result < 0)
            {
                string message = string.Format("Container size = {0} can not be negative", result);
                throw new ArgumentException(message, "result");
            }
            if (newPosition + result > m_size)
            {
                string message = string.Format("Container size = {0} can not be more than available size = {1}", result, newPosition - m_size);
                throw new ArgumentException(message, "result");
            }
            m_position = newPosition;
            return result;
        }
        #endregion
        #region writing methods
        internal void WriteData(int size, void* data)
        {
            int newPosition = m_position + size;
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var destination = (SByte*)(m_data + m_position);
            var current = (SByte*)data;
            var end = current + size;
            for (; current < end; ++current, ++destination)
            {
                *destination = *current;
            }
            m_position = newPosition;
        }
        /// <summary>
        /// Writes a raw data to memory buffer.
        /// </summary>
        /// <param name="data"></param>
        public void WriteData(byte[] data)
        {
            if ((null != data) && (0 != data.Length))
            {
                fixed (byte* ptr = &data[0])
                {
                    WriteData(data.Length, ptr);
                }
            }
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteInt8(SByte arg)
        {
            int newPosition = m_position + sizeof(SByte);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var pointer = (SByte*)(m_data + m_position);
            *pointer = arg;
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteInt16(Int16 arg)
        {
            int newPosition = m_position + sizeof(Int16);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var pointer = (Int16*)(m_data + m_position);
            *pointer = arg;
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteInt32(int arg)
        {
            int newPosition = m_position + sizeof(int);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var pointer = (int*)(m_data + m_position);
            *pointer = arg;
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteNullInt32(int? arg)
        {
            if (arg.HasValue)
            {
                WriteBoolean(true);
                WriteInt32(arg.Value);
            }
            else
            {
                WriteBoolean(false);
            }
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteInt64(long arg)
        {
            int newPosition = m_position + sizeof(long);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var pointer = (long*)(m_data + m_position);
            *pointer = arg;
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteNullInt64(int? arg)
        {
            if (arg.HasValue)
            {
                WriteBoolean(true);
                WriteInt64(arg.Value);
            }
            else
            {
                WriteBoolean(false);
            }
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteUInt8(byte arg)
        {
            int newPosition = m_position + sizeof(byte);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var pointer = (byte*)(m_data + m_position);
            *pointer = arg;
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteUInt16(UInt16 arg)
        {
            int newPosition = m_position + sizeof(UInt16);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var pointer = (UInt16*)(m_data + m_position);
            *pointer = arg;
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteUInt32(UInt32 arg)
        {
            int newPosition = m_position + sizeof(UInt32);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var pointer = (UInt32*)(m_data + m_position);
            *pointer = arg;
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteUInt64(UInt64 arg)
        {
            int newPosition = m_position + sizeof(UInt64);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var pointer = (UInt64*)(m_data + m_position);
            *pointer = arg;
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteBoolean(bool arg)
        {
            int newPosition = m_position + sizeof(bool);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var pointer = (bool*)(m_data + m_position);
            *pointer = arg;
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteNullBoolean(bool? arg)
        {
            if (arg.HasValue)
            {
                WriteBoolean(true);
                WriteBoolean(arg.Value);
            }
            else
            {
                WriteBoolean(false);
            }
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteSingle(float arg)
        {
            int newPosition = m_position + sizeof(float);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var pointer = (float*)(m_data + m_position);
            *pointer = arg;
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteDouble(double arg)
        {
            int newPosition = m_position + sizeof(double);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            var pointer = (double*)(m_data + m_position);
            *pointer = arg;
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteNullDouble(double? arg)
        {
            if (arg.HasValue)
            {
                WriteBoolean(true);
                WriteDouble(arg.Value);
            }
            else
            {
                WriteBoolean(false);
            }
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteAString(string arg)
        {
            int newPosition = m_position + arg.Length + sizeof(int);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            WriteInt32(arg.Length);
            fixed (char* source = arg)
            {
                char* end = source + arg.Length;
                byte* dest = (byte*)(m_data + m_position);
                for (char* current = source; current < end; ++current, ++dest)
                {
                    *dest = (byte)(*current);
                }
            }
            m_position = newPosition;
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteWString(string arg)
        {
            int newPosition = m_position + sizeof(char) * arg.Length + sizeof(int);
            if (newPosition > m_capacity)
            {
                ReAlloc(newPosition);
            }
            WriteInt32(arg.Length);
            fixed (char* source = arg)
            {
                char* end = source + arg.Length;
                char* dest = (char*)(m_data + m_position);
                for (char* current = source; current < end; ++current, ++dest)
                {
                    *dest = (*current);
                }
            }
            m_position = newPosition;
        }
        /// <summary>
        /// Write an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteAChar(Char arg)
        {
            WriteUInt8((byte)arg);
        }
        /// <summary>
        /// Write an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteWChar(Char arg)
        {
            WriteUInt16((UInt16)arg);
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public unsafe void WriteLocalPointer(LPtr arg)
        {
            if (IsLPtr32Bit)
            {
                int value = arg.ToInt32();
                WriteInt32(value);
            }
            else
            {
                long value = arg.ToInt64();
                WriteInt64(value);
            }
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public unsafe void WriteRemotePointer(RPtr arg)
        {
            WriteInt64(arg.Handle);
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteTime(DateTime arg)
        {
            long ticks = arg.ToFileTimeUtc();
            ticks /= 10000;
            ticks -= cFxDateTimeStartTicks;
            WriteInt64(ticks);
        }
        /// <summary>
        /// Writes an input value to memory buffer.
        /// </summary>
        /// <param name="arg">any value.</param>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for the buffer reallocation.</exception>
        public void WriteNullTime(DateTime? arg)
        {
            if (arg.HasValue)
            {
                WriteBoolean(true);
                WriteTime(arg.Value);
            }
            else
            {
                WriteBoolean(false);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        public void WriteAStringsArray(string[] args)
        {
            WriteInt32(args.Length);
            for (int index = 0; index < args.Length; ++index)
            {
                WriteAString(args[index]);
            }
        }
        #endregion
        #region reading methods
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public SByte ReadInt8()
        {
            int newPosition = m_position + sizeof(SByte);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(SByte).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(SByte*)(m_data + m_position);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public Int16 ReadInt16()
        {
            int newPosition = m_position + sizeof(Int16);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(Int16).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(Int16*)(m_data + m_position);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public int ReadInt32()
        {
            int newPosition = m_position + sizeof(int);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(int).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(int*)(m_data + m_position);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public int? ReadNullInt32()
        {
            bool hasValue = ReadBoolean();
            if (hasValue)
            {
                return ReadInt32();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public long ReadInt64()
        {
            int newPosition = m_position + sizeof(long);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(long).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(long*)(m_data + m_position);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public long? ReadNullInt64()
        {
            bool hasValue = ReadBoolean();
            if (hasValue)
            {
                return ReadInt64();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public byte ReadUInt8()
        {
            int newPosition = m_position + sizeof(byte);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(byte).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(byte*)(m_data + m_position);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public UInt16 ReadUInt16()
        {
            int newPosition = m_position + sizeof(UInt16);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(UInt16).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(UInt16*)(m_data + m_position);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public UInt32 ReadUInt32()
        {
            int newPosition = m_position + sizeof(UInt32);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(UInt32).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(UInt32*)(m_data + m_position);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public UInt64 ReadUInt64()
        {
            int newPosition = m_position + sizeof(UInt64);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(UInt64).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(UInt64*)(m_data + m_position);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public bool ReadBoolean()
        {
            int newPosition = m_position + sizeof(bool);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(bool).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(bool*)(m_data + m_position);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public bool? ReadNullBoolean()
        {
            bool hasValue = ReadBoolean();
            if (hasValue)
            {
                return ReadBoolean();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public float ReadSingle()
        {
            int newPosition = m_position + sizeof(float);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(float).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(float*)(m_data + m_position);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public double ReadDouble()
        {
            int newPosition = m_position + sizeof(double);
            if (newPosition > m_size)
            {
                string message = string.Format("End of stream has been reached for type = {0}", typeof(double).Name);
                throw new IndexOutOfRangeException(message);
            }
            var result = *(double*)(m_data + m_position);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public double? ReadNullDouble()
        {
            bool hasValue = ReadBoolean();
            if (hasValue)
            {
                return ReadDouble();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for a new string creation.</exception>
        /// <returns>A read value.</returns>
        public string ReadAString()
        {
            int length = ReadInt32();
            int newPosition = m_position + length;
            if (newPosition > m_size)
            {
                m_position -= sizeof(int); // restore position
                string message = string.Format("End of stream has been reached for type = {0}", typeof(string).Name);
                throw new IndexOutOfRangeException(message);
            }

            var data = (SByte*)(m_data + m_position);
            var result = new string(data, 0, length);
            m_position = newPosition;
            return result;
        }
            /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for a new string creation.</exception>
        /// <returns>A read value.</returns>
        public string ReadWString()
        {
            int length = ReadInt32();
            int newPosition = m_position + sizeof(char) * length;
            if (newPosition > m_size)
            {
                m_position -= sizeof(int); // restore position
                string message = string.Format("End of stream has been reached for type = {0}", typeof(string).Name);
                throw new IndexOutOfRangeException(message);
            }

            var data = (Char*)(m_data + m_position);
            var result = new string(data, 0, length);
            m_position = newPosition;
            return result;
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public LPtr ReadLocalPointer()
        {
            if (this.IsLPtr32Bit)
            {
                int value = ReadInt32();
                return new LPtr(value);
            }
            else
            {
                long value = ReadInt64();
                return new LPtr(value);
            }
        }
        /// <summary>
        /// Reads a value from memory buffer.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public RPtr ReadRemotePointer()
        {
            long value = ReadInt64();
            RPtr result = new RPtr(value);
            return result;
        }
        /// <summary>
        /// Reads a datetime object from the stream.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public DateTime ReadTime()
        {
            long ticks = ReadInt64();
            ticks += cFxDateTimeStartTicks;
            ticks *= 10000;
            if (ticks < 0)
                return new DateTime(1970,1,1);

            return DateTime.FromFileTimeUtc(ticks);
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public DateTime? ReadNullTime()
        {
            bool hasValue = ReadBoolean();
            if (hasValue)
            {
                return ReadTime();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        ///  Reads raw data.
        /// </summary>
        /// <returns>this</returns>
        public MemoryBuffer ReadRaw()
        {
            return this;
        }
        #endregion
        #region members
        private void ReAlloc(int requiredSize)
        {
            int newCapacity = m_capacity;
            do
            {
                newCapacity *= 2;
            } while (newCapacity < requiredSize);

            byte* newPointer = (byte*)Alloc(newCapacity);
            if (null == newPointer)
            {
                throw new OutOfMemoryException();
            }

            int count = (m_position > m_size) ? m_position : m_size;

            for (int index = 0; index < count; ++index)
            {
                newPointer[index] = m_data[index];
            }

            Free(m_data);
            m_data = newPointer;
            m_capacity = newCapacity;
        }

        private void* Alloc(Int32 size)
        {
            UIntPtr pSize = new UIntPtr((UInt32)size);
            IntPtr ptr = HeapAlloc(m_heap, 0, pSize);
            void* result = ptr.ToPointer();
            return result;
        }
        private void Free(void* pointer)
        {
            IntPtr ptr = new IntPtr(pointer);
            HeapFree(m_heap, 0, ptr);
        }
        [DllImport("kernel32.dll", SetLastError = false)]
        private static extern IntPtr HeapAlloc(void* hHeap, UInt32 dwFlags, UIntPtr dwBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool HeapFree(void* hHeap, UInt32 dwFlags, IntPtr lpMem);
        #endregion
        #region properties
        /// <summary>
        /// Get size of allocated memory in bytes.
        /// </summary>
        public int Capacity
        {
            get
            {
                return m_capacity;
            }
        }
        /// <summary>
        /// Gets current buffer position.
        /// </summary>
        public int Position
        {
            get
            {
                return m_position;
            }
            set
            {
                if (m_size < m_position)
                {
                    m_size = m_position;
                }
                if ((value < 0) || (value > m_size))
                {
                    string message = string.Format("Position is out of range; new position = {0}; epected range from 0 to {1}", value, m_size);
                    throw new ArgumentException(message, "value");
                }
                m_position = value;
            }
        }
        /// <summary>
        /// Gets size of the buffer in bytes.
        /// </summary>
        public int Size
        {
            get
            {
                int result = (m_position > m_size) ? m_position : m_size;
                return result;
            }
        }
        internal int MovePosition(int moving)
        {
            int result = this.Position;
            result += moving;
            this.Position = result;
            return result;
        }
        /// <summary>
        ///
        /// </summary>
        public void* Data
        {
            get
            {
                return m_data;
            }
        }
        /// <summary>
        ///
        /// </summary>
        public void* Heap
        {
            get
            {
                return m_heap;
            }
            set
            {
                m_heap = value;
            }
        }
        internal bool IsLPtr32Bit { get; set; }
        #endregion
        #region members
        private void* m_heap;
        private byte* m_data;
        private int m_size;
        private int m_capacity;
        private int m_position;
        #endregion
        #region static members
        private static UInt64 s_counter = 1;
        private static Heap s_heap = new Heap();
        private static readonly Object s_synchronizer = new Object();
        [ThreadStatic]
        private static MemoryBuffer s_localBuffer = null;
        private static readonly List<MemoryBuffer> s_buffers = new List<MemoryBuffer>();
        private static readonly bool cIsLPtr32Bit = (sizeof(int) == IntPtr.Size);
        #endregion
        #region constants
        private const long cFxDateTimeStartTicks = 11644473600000;
        #endregion
    }
}
