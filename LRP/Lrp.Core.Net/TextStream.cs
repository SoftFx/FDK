namespace SoftFX.Lrp
{
    using System;
    using System.Text;

    /// <summary>
    /// The class provides methods for reading embedded types.
    /// </summary>
    public class TextStream
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void Initialize(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text", "Text can not be null");

            this.text = text;
            this.position = 0;
        }

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public bool EntryWasFound { get; private set; }

        #endregion

        #region Reading Methods

        #region Reading of int8

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optioanal name of reading value</param>
        /// <returns>A read value.</returns>
        public sbyte ReadInt8(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }
            return this.DoReadInt8(position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public sbyte ReadInt8(String name, sbyte defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadInt8(this.position);
            }
            return defaultValue;
        }

        sbyte DoReadInt8(int position)
        {
            int value;
            position = ParseInt32(this.text, position, out value);
            var result = checked((sbyte)value);
            this.position = position;
            return result;
        }

        #endregion

        #region Rading of int16

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optioanal name of reading value</param>
        /// <returns>A read value.</returns>
        public short ReadInt16(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }
            return this.DoReadInt16(position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public short ReadInt16(String name, short defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadInt16(this.position);
            }
            return defaultValue;
        }

        short DoReadInt16(int position)
        {
            int value;
            position = ParseInt32(this.text, position, out value);
            var result = checked((short)value);
            this.position = position;
            return result;
        }

        #endregion

        #region Reading of int32

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optioanal name of reading value</param>
        /// <returns>A read value.</returns>
        public int ReadInt32(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }
            return this.DoReadInt32(position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public int ReadInt32(string name, int defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadInt32(this.position);
            }
            return defaultValue;
        }

        int DoReadInt32(int position)
        {
            int value;
            position = ParseInt32(this.text, position, out value);
            this.position = position;
            return value;
        }

        #endregion

        #region Reading of int64

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optioanal name of reading value</param>
        /// <returns>A read value.</returns>
        public long ReadInt64(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }

            return this.DoReadInt64(position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public long ReadInt64(string name, long defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadInt64(this.position);
            }
            return defaultValue;
        }

        private long DoReadInt64(int position)
        {
            long value;
            position = ParseInt64(this.text, position, out value);
            this.position = position;
            return value;
        }

        #endregion

        #region Reading of uint8

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optioanal name of reading value</param>
        /// <returns>A read value.</returns>
        public byte ReadUInt8(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }
            return this.DoReadUInt8(position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public byte ReadUInt8(string name, byte defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadUInt8(this.position);
            }
            return defaultValue;
        }

        byte DoReadUInt8(int position)
        {
            uint value;
            position = ParseUInt32(this.text, position, out value);
            var result = checked((byte)value);
            this.position = position;
            return result;
        }

        #endregion

        #region Reading of uint16

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optioanal name of reading value</param>
        /// <returns>A read value.</returns>
        public ushort ReadUInt16(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }
            return this.DoReadUInt16(position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public ushort ReadUInt16(string name, ushort defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadUInt16(this.position);
            }
            return defaultValue;
        }

        ushort DoReadUInt16(int position)
        {
            uint value;
            position = ParseUInt32(this.text, position, out value);
            var result = checked((ushort)value);
            this.position = position;
            return result;
        }

        #endregion

        #region Reading of uint32
        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optioanal name of reading value</param>
        /// <returns>A read value.</returns>
        public uint ReadUInt32(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }

            return this.DoReadUInt32(position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public uint ReadInt32(string name, uint defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadUInt32(this.position);
            }
            return defaultValue;
        }

        uint DoReadUInt32(int position)
        {
            uint value;
            position = ParseUInt32(this.text, position, out value);
            this.position = position;
            return value;
        }

        #endregion

        #region Reading of uint64

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optioanal name of reading value</param>
        /// <returns>A read value.</returns>
        public ulong ReadUInt64(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }
            return this.DoReadUInt64(position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public ulong ReadUInt64(string name, ulong defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadUInt64(this.position);
            }
            return defaultValue;
        }

        ulong DoReadUInt64(int position)
        {
            ulong value;
            position = ParseUInt64(this.text, position, out value);
            this.position = position;
            return value;
        }

        #endregion

        #region Reading of bool

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optional name of reading value</param>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public bool ReadBoolean(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }
            return this.DoReadBoolean(position);
        }
        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public bool ReadBoolean(string name, bool defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadBoolean(this.position);
            }
            return defaultValue;
        }

        bool DoReadBoolean(int position)
        {
            var ch = this.text[this.position];
            if (ch == 't')
            {
                position = ValidateVerbatimText(this.text, 1 + position, "rue");
                this.position = position;
                return true;
            }
            else if (ch == 'f')
            {
                position = ValidateVerbatimText(this.text, 1 + position, "alse");
                this.position = position;
                return false;
            }
            else
            {
                var message = string.Format("bool type can not start from {0}; position = {1}", ch, position);
                throw new FormatException(message);
            }
        }

        #endregion

        #region Reading of float

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optional name of reading value</param>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public float ReadSingle(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }
            return this.DoReadSingle(position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public float ReadSingle(string name, float defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadSingle(this.position);
            }
            return defaultValue;
        }

        unsafe float DoReadSingle(int position)
        {
            position = ReadForCharacter(this.text, position, '(');
            position = ValidateVerbatimText(this.text, position, "0x");
            float result = 0;
            var begin = (Byte*)&result;
            var end = begin + sizeof(float);
            position = ReadBinary(this.text, position, begin, end);
            position = ValidateVerbatimText(this.text, position, ')');
            this.position = position;
            return result;
        }

        #endregion

        #region Reading of double

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optional name of reading value</param>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public double ReadDouble(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }
            return this.DoReadDouble(position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public double ReadDouble(string name, double defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadDouble(this.position);
            }
            return defaultValue;
        }

        unsafe double DoReadDouble(int position)
        {
            position = ReadForCharacter(this.text, this.position, '(');
            position = ValidateVerbatimText(this.text, position, "0x");
            double result = 0;
            var begin = (Byte*)&result;
            var end = begin + sizeof(double);
            position = ReadBinary(this.text, position, begin, end);
            position = ValidateVerbatimText(this.text, position, ')');
            this.position = position;
            return result;
        }

        #endregion

        #region Reading of astring

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optional name of reading value</param>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <exception cref="System.OutOfMemoryException">if not enough memory for a new string creation.</exception>
        /// <returns>A read value.</returns>
        public string ReadAString(string name = null)
        {
            var text = this.text;
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(text, position, name);
                position = ValidateVerbatimText(text, position, " = ");
            }
            return this.DoReadAString(text, position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public string ReadAString(string name, string defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadAString(this.text, this.position);
            }
            return defaultValue;
        }

        string DoReadAString(string text, int position)
        {
            position = ValidateVerbatimText(text, position, '"');

            var builder = this.builder;
            builder.Remove(0, builder.Length);

            for (; ; )
            {
                var ch = text[position++];
                if ('\\' == ch)
                {
                    ch = text[position++];
                    if ('n' == ch)
                    {
                        builder.Append('\n');
                    }
                    else if ('t' == ch)
                    {
                        builder.Append('\t');
                    }
                    else if ('r' == ch)
                    {
                        builder.Append('\r');
                    }
                    else if ('"' == ch)
                    {
                        builder.Append('\"');
                    }
                    else if ('0' == ch)
                    {
                        builder.Append('\0');
                    }
                    else if ('0' == ch)
                    {
                        builder.Append('\0');
                    }
                    else if ('\\' == ch)
                    {
                        builder.Append('\\');
                    }
                    else
                    {
                        var message = string.Format("Incorrect escape character {0}; position = {1}", ch, position);
                        throw new FormatException(message);
                    }
                }
                else if ('"' == ch)
                {
                    break;
                }
                else
                {
                    builder.Append(ch);
                }
            }
            var result = builder.ToString();
            this.position = position;
            return result;
        }

        #endregion

        #region Reading of time

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">optional name of reading value</param>
        /// <exception cref="System.IndexOutOfRangeException">if end of the buffer has been reached.</exception>
        /// <returns>A read value.</returns>
        public DateTime ReadTime(string name = null)
        {
            var position = this.position;
            if (name != null)
            {
                position = ValidateVerbatimText(this.text, position, name);
                position = ValidateVerbatimText(this.text, position, " = ");
            }
            return this.DoReadTime(position);
        }

        /// <summary>
        /// Read a value from text stream.
        /// </summary>
        /// <param name="name">name of reading value</param>
        /// <param name="defaultValue">default value of the value</param>
        /// <returns>A read value.</returns>
        public DateTime ReadDouble(string name, DateTime defaultValue)
        {
            if (this.TryValidateName(name))
            {
                return this.DoReadTime(this.position);
            }
            return defaultValue;
        }

        unsafe DateTime DoReadTime(int position)
        {
            position = ReadForCharacter(this.text, this.position, '(');
            position = ValidateVerbatimText(this.text, position, "0x");
            long ticks = 0;
            var begin = (Byte*)&ticks;
            var end = begin + sizeof(long);
            position = ReadBinary(this.text, position, begin, end);
            position = ValidateVerbatimText(this.text, position, ')');
            this.position = position;

            ticks += FxDateTimeStartTicks;
            ticks *= 10000;
            if (ticks < 0)
                return new DateTime(1970, 1, 1);
            var result = DateTime.FromFileTimeUtc(ticks);
            return result;
        }

        #endregion

        /// <summary>
        /// Validate a text in stream.
        /// </summary>
        /// <param name="text">can not be null.</param>
        public void ValidateVerbatimText(string text)
        {
            this.position = ValidateVerbatimText(this.text, this.position, text);
        }

        /// <summary>
        /// Validate a text in stream.
        /// </summary>
        /// <param name="text">can not be null.</param>
        public void ValidateVerbatimText(char text)
        {
            this.position = ValidateVerbatimText(this.text, this.position, text);
        }
        /// <summary>
        /// Reads a text from the stream until ch is appeared.
        /// </summary>
        /// <param name="value">any character</param>
        public void ReadForCharacter(char value)
        {
            var position = this.position;
            var text = this.text;
            for (; ; )
            {
                var ch = text[position++];
                if (ch == value)
                {
                    break;
                }
            }
            this.position = position;
        }

        /// <summary>
        /// Reads a text from the stream until ch is appeared.
        /// </summary>
        /// <param name="value">any character</param>
        public string ReadAStringForCharacter(Char value)
        {
            var position = this.position;
            var text = this.text;
            for (; ; )
            {
                var ch = text[position];
                if (ch == value)
                {
                    break;
                }
                position++;
            }
            var result = text.Substring(this.position, position - this.position);
            this.position = position;
            return result;
        }
        #endregion

        #region Overrides

        /// <summary>
        /// Returns a text stream from current position to end.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = this.text.Substring(this.position, this.text.Length - this.position);
            return result;
        }

        #endregion

        #region Parsing Methods

        #region Parsing of int

        static int ParseInt32(string text, int position, out int value)
        {
            var ch = text[position];
            if (ch == '-')
                return ParseNegativeInt32(text, 1 + position, out value);
            else
                return ParsePositiveInt32(text, position, out value);
        }

        static int ParsePositiveInt32(string text, int position, out int value)
        {
            if (position >= text.Length)
            {
                var message = string.Format("Invalid position = {0} > text.Length = {1}", position, text.Length);
                throw new ArgumentException(message, "position");
            }
            {
                var ch = text[position];
                if (ch > '9' || ch < '0')
                {
                    var message = string.Format("Integer value can not strart from {0}; position = {1}", ch, position);
                    throw new FormatException(message);
                }
                value = (ch - '0'); 
            }

            for (++position; (position < text.Length); ++position)
            {
                var ch = text[position];
                if (ch > '9' || ch < '0')
                {
                    break;
                }
                checked
                {
                    value *= 10;
                    value += (ch - '0');
                }
            }
            return position;
        }

        static int ParseNegativeInt32(string text, int position, out int value)
        {
            if (position >= text.Length)
            {
                var message = string.Format("Invalid position = {0} > text.Length = {1}", position, text.Length);
                throw new ArgumentException(message, "position");
            }
            {
                var ch = text[position];
                if ((ch > '9') || (ch < '0'))
                {
                    var message = string.Format("Integer value can not strart from {0}; position = {1}", ch, position);
                    throw new FormatException(message);
                }
                value = ('0' - ch);
            }

            for (++position; (position < text.Length); ++position)
            {
                var ch = text[position];
                if (ch > '9' || ch < '0')
                {
                    break;
                }
                checked
                {
                    value *= 10;
                    value += ('0' - ch);
                }
            }
            return position;
        }

        #endregion

        #region Parsing of Int64

        static int ParseInt64(string text, int position, out long value)
        {
            var ch = text[position];
            if (ch == '-')
                return ParseNegativeInt64(text, 1 + position, out value);
            else
                return ParsePositiveInt64(text, position, out value);
        }

        static int ParsePositiveInt64(string text, int position, out long value)
        {
            if (position >= text.Length)
            {
                var message = string.Format("Invalid position = {0} > text.Length = {1}", position, text.Length);
                throw new ArgumentException(message, "position");
            }
            {
                var ch = text[position];
                if (ch > '9' || ch < '0')
                {
                    var message = string.Format("Integer value can not strart from {0}; position = {1}", ch, position);
                    throw new FormatException(message);
                }
                value = (ch - '0');
            }

            for (++position; (position < text.Length); ++position)
            {
                var ch = text[position];
                if (ch > '9' || ch < '0')
                {
                    break;
                }
                checked
                {
                    value *= 10;
                    value += (ch - '0');
                }
            }
            return position;
        }

        static int ParseNegativeInt64(string text, int position, out long value)
        {
            if (position >= text.Length)
            {
                var message = string.Format("Invalid position = {0} > text.Length = {1}", position, text.Length);
                throw new ArgumentException(message, "position");
            }
            {
                var ch = text[position];
                if (ch > '9' || ch < '0')
                {
                    var message = string.Format("Integer value can not strart from {0}; position = {1}", ch, position);
                    throw new FormatException(message);
                }
                value = ('0' - ch);
            }

            for (++position; (position < text.Length); ++position)
            {
                var ch = text[position];
                if (ch > '9' || ch < '0')
                {
                    break;
                }
                checked
                {
                    value *= 10;
                    value += ('0' - ch);
                }
            }
            return position;
        }

        #endregion

        static int ParseUInt32(string text, int position, out uint value)
        {
            if (position >= text.Length)
            {
                var message = string.Format("Invalid position = {0} > text.Length = {1}", position, text.Length);
                throw new ArgumentException(message, "position");
            }
            {
                var ch = text[position];
                if (ch > '9' || ch < '0')
                {
                    var message = string.Format("Integer value can not strart from {0}; position = {1}", ch, position);
                    throw new FormatException(message);
                }
                value = (uint)(ch - '0');
            }

            for (++position; (position < text.Length); ++position)
            {
                var ch = text[position];
                if (ch > '9' || ch < '0')
                {
                    break;
                }
                checked
                {
                    value *= 10;
                    value += (uint)(ch - '0');
                }
            }
            return position;
        }

        static int ParseUInt64(string text, int position, out ulong value)
        {
            if (position >= text.Length)
            {
                var message = string.Format("Invalid position = {0} > text.Length = {1}", position, text.Length);
                throw new ArgumentException(message, "position");
            }
            {
                var ch = text[position];
                if (ch > '9' || ch < '0')
                {
                    var message = string.Format("Integer value can not strart from {0}; position = {1}", ch, position);
                    throw new FormatException(message);
                }
                value = (uint)(ch - '0');
            }

            for (++position; (position < text.Length); ++position)
            {
                var ch = text[position];
                if (ch > '9' || ch < '0')
                {
                    break;
                }
                checked
                {
                    value *= 10;
                    value += (uint)(ch - '0');
                }
            }
            return position;
        }

        #endregion

        #region Validating Method

        static int ValidateVerbatimText(string text, int position, string value)
        {
            for (var index = 0; index < value.Length; ++index)
            {
                var expected = value[index];
                var read = text[position++];
                if (expected != read)
                {
                    var message = string.Format("Validate verbatim text is failed; expected {0}, but read {1}; position = {2}", expected, read, position);
                    throw new FormatException(message);
                }
            }
            return position;
        }

        bool TryValidateName(string name)
        {
            var position = this.position;
            var newPosition = TryValidateVerbatimText(this.text, position, name);
            if (position == newPosition)
            {
                this.EntryWasFound = false;
                return false;
            }
            position = newPosition;
            newPosition = ValidateVerbatimText(this.text, position, " = ");
            if (newPosition > position)
            {
                this.position = newPosition;
                this.EntryWasFound = true;
                return true;
            }
            this.EntryWasFound = false;
            return false;
        }

        static int TryValidateVerbatimText(string text, int position, string value)
        {
            var originalPosition = position;

            for (var index = 0; index < value.Length; ++index)
            {
                var expected = value[index];
                if (position >= text.Length)
                {
                    return originalPosition;
                }
                var read = text[position++];
                if (expected != read)
                {
                    return originalPosition;
                }
            }
            return position;
        }

        static int ValidateVerbatimText(string text, int position, char value)
        {
            var read = text[position++];
            if (value != read)
            {
                var message = string.Format("Validate verbatim text is failed; expected {0}, but read {1}; position = {2}", value, read, position);
                throw new FormatException(message);
            }
            return position;
        }

        static int ReadForCharacter(string text, int position, char value)
        {
            for (var ch = text[position]; ch != value; ch = text[++position])
            {
            }
            return 1 + position;
        }

        #region Reading Binary Data

        static unsafe int ReadBinary(string text, int position, byte* begin, byte* end)
        {
            for (var current = begin; current < end; ++current)
            {
                var ch = text[position++];
                var first = Int32FromHexChar(ch);

                ch = text[position++];
                var second = Int32FromHexChar(ch);
                *current = (Byte)(16 * first + second);
            }
            return position;
        }

        static int Int32FromHexChar(Char ch)
        {
            int result = (ch - '0');
            if (result < 0)
            {
                var message = string.Format("Binary data contains invalid character = {0}; position = {1}", ch);
                throw new FormatException(message);
            }
            if (result > 9)
            {
                result -= 7;
                if ((result < 10) || (result > 15))
                {
                    var message = string.Format("Binary data contains invalid character = {0}; position = {1}", ch);
                    throw new FormatException(message);
                }
            }
            return result;
        }

        #endregion

        #endregion

        #region members

        string text = string.Empty;
        int position = 0;
        readonly StringBuilder builder = new StringBuilder();

        #endregion

        #region Constants

        const long FxDateTimeStartTicks = 11644473600000;

        #endregion
    }
}
