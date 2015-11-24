namespace SoftFX.Lrp.Implementation
{
    using System;

    unsafe static class Network
    {
        #region Private Methods

        static bool SendEx(IntPtr socket, Timeout timeout, void* data, int size)
        {
            var set = fd_set.Create(socket);
            var buffer = (byte*)data;

            for (; size > 0; )
            {
                var t = timeout.ToTime();
                var status = WinAPI.select(1, null, &set, null, &t);
                if (status != 1)
                    return false;

                status = WinAPI.send(socket, buffer, size, 0);
                if (status <= 0)
                    return false;

                buffer += status;
                size -= status;
            }

            return true;
        }

        static bool SendEx(IntPtr socket, void* data, int size)
        {
            var buffer = (byte*)data;
            for (; size > 0; )
            {
                var status = WinAPI.send(socket, buffer, size, 0);
                if (status <= 0)
                    return false;

                buffer += status;
                size -= status;
            }

            return true;
        }

        static bool ReceiveEx(IntPtr socket, Timeout timeout, void* data, int size)
        {
            var set = fd_set.Create(socket);
            var buffer = (byte*)data;
            for (; size > 0; )
            {
                var t = timeout.ToTime();
                var status = WinAPI.select(1, &set, null, null, &t);
                if (status != 1)
                    return false;

                status = WinAPI.recv(socket, buffer, size, 0);
                if (status <= 0)
                    return false;

                buffer += status;
                size -= status;
            }

            return true;
        }

        #endregion

        #region Internal Methods

        public static bool SendEx(IntPtr socket, Timeout timeout, string text)
        {
            var size = text.Length;
            if (!SendEx(socket, timeout, &size, sizeof(int)))
                return false;

            if (size == 0)
                return true;

            var buffer = new byte[size];
            for (var index = 0; index < size; ++index)
            {
                buffer[index] = (byte)text[index];
            }

            fixed (byte* ptr = &buffer[0])
            {
                return SendEx(socket, timeout, ptr, size);
            }
        }

        public static bool SendEx(IntPtr socket, string text)
        {
            var size = text.Length;
            if (!SendEx(socket, &size, sizeof(int)))
                return false;

            if (size == 0)
                return true;

            var buffer = new byte[size];
            for (var index = 0; index < size; ++index)
            {
                buffer[index] = (byte)text[index];
            }

            fixed (byte* ptr = &buffer[0])
            {
                return SendEx(socket, ptr, size);
            }
        }

        public static bool SendEx(IntPtr socket, Timeout timeout, int value)
        {
            return SendEx(socket, timeout, &value, sizeof(int));
        }

        public static bool ReceiveEx(IntPtr socket, Timeout timeout, out string text)
        {
            text = string.Empty;
            var size = 0;
            if (!ReceiveEx(socket, timeout, &size, sizeof(int)))
                return false;

            if (size < 0)
                return false;

            if (size == 0)
                return true;

            var buffer = new sbyte[size];
            var result = false;
            fixed (sbyte* ptr = &buffer[0])
            {
                result = ReceiveEx(socket, timeout, ptr, size);
                if (result)
                    text = new string(ptr, 0, size);
            }

            return result;
        }

        public static bool ReceiveEx(IntPtr socket, Timeout timeout, MemoryBuffer data)
        {
            var set = fd_set.Create(socket);

            data.Reset();

            var length = data.Capacity;
            var buffer = (byte*)data.Data;

            var total = 0;
            for (; total < 4; )
            {
                var interval = timeout.ToTime();

                var status = WinAPI.select(1, &set, null, null, &interval);
                if (status == 0)
                    return false;

                status = WinAPI.recv(socket, buffer, length, 0);
                if (status <= 0)
                    return false;

                buffer += status;
                total += status;
                length -= status;
            }
            data.Construct(total);
            var size = data.ReadInt32();
            data.Construct(sizeof(int) + size);

            length = size - total + sizeof(int); // remaining data length

            if (length < 0) // we read more than expected
                return false;

            if (length == 0) // we read all data
                 return true;
 
            buffer = (byte*)data.Data;
            buffer += total;
            for (; length > 0; )
            {
                var interval = timeout.ToTime();

                var status = WinAPI.select(1, &set, null, null, &interval);
                if (status == 0)
                    return false;

                status = WinAPI.recv(socket, buffer, length, 0);
                if (status <= 0)
                    return false;

                buffer += status;
                length -= status;
            }

            return true;
        }

        public static bool ReceiveEx(IntPtr socket, Timeout timeout, out int value)
        {
            var temporary = 0;
            var result = ReceiveEx(socket, timeout, &temporary, sizeof(int));
            value = temporary;
            return result;
        }

        #endregion
    }
}
