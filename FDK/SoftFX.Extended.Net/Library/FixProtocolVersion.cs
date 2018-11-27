namespace SoftFX.Extended
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents protocol specification.
    /// </summary>
    public class FixProtocolVersion : IComparable<FixProtocolVersion>
    {
        static readonly FixProtocolVersion CurrentVersion = new FixProtocolVersion("ext", 1, 69);

        #region Predefined known protocol versions

        const string ExtendedType = "ext";
        internal static readonly FixProtocolVersion Initial = new FixProtocolVersion(ExtendedType, 0, 0);
        internal static readonly FixProtocolVersion Versioning = new FixProtocolVersion(ExtendedType, 1, 0);
        internal static readonly FixProtocolVersion SymbolExtending = new FixProtocolVersion(ExtendedType, 1, 1);
        internal static readonly FixProtocolVersion SymbolColorExtending = new FixProtocolVersion(ExtendedType, 1, 2);
        internal static readonly FixProtocolVersion TradeTransactionReportExtending = new FixProtocolVersion(ExtendedType, 1, 3);
        internal static readonly FixProtocolVersion InitialTrading = new FixProtocolVersion(ExtendedType, 1, 4);
        internal static readonly FixProtocolVersion NewLogoutReasons = new FixProtocolVersion(ExtendedType, 1, 5);
        internal static readonly FixProtocolVersion NewNotification = new FixProtocolVersion(ExtendedType, 1, 6);
        internal static readonly FixProtocolVersion PositionReportBalanceForRollover = new FixProtocolVersion(ExtendedType, 1, 7);
        internal static readonly FixProtocolVersion NewPositionReport = new FixProtocolVersion(ExtendedType, 1, 9);

        internal static readonly FixProtocolVersion Version10 = new FixProtocolVersion(ExtendedType, 1, 10);
        internal static readonly FixProtocolVersion Version11 = new FixProtocolVersion(ExtendedType, 1, 11);
        internal static readonly FixProtocolVersion Version12 = new FixProtocolVersion(ExtendedType, 1, 12);
        internal static readonly FixProtocolVersion Version13 = new FixProtocolVersion(ExtendedType, 1, 13);
        internal static readonly FixProtocolVersion Version14 = new FixProtocolVersion(ExtendedType, 1, 14);
        internal static readonly FixProtocolVersion Version15 = new FixProtocolVersion(ExtendedType, 1, 15);
        internal static readonly FixProtocolVersion Version16 = new FixProtocolVersion(ExtendedType, 1, 16);
        internal static readonly FixProtocolVersion Version17 = new FixProtocolVersion(ExtendedType, 1, 17);
        internal static readonly FixProtocolVersion Version18 = new FixProtocolVersion(ExtendedType, 1, 18);
        internal static readonly FixProtocolVersion Version19 = new FixProtocolVersion(ExtendedType, 1, 19);
        internal static readonly FixProtocolVersion Version20 = new FixProtocolVersion(ExtendedType, 1, 20);
        internal static readonly FixProtocolVersion Version21 = new FixProtocolVersion(ExtendedType, 1, 21);
        internal static readonly FixProtocolVersion Version22 = new FixProtocolVersion(ExtendedType, 1, 22);
        internal static readonly FixProtocolVersion Version23 = new FixProtocolVersion(ExtendedType, 1, 23);
        internal static readonly FixProtocolVersion Version24 = new FixProtocolVersion(ExtendedType, 1, 24);
        internal static readonly FixProtocolVersion Version25 = new FixProtocolVersion(ExtendedType, 1, 25);
        internal static readonly FixProtocolVersion Version26 = new FixProtocolVersion(ExtendedType, 1, 26);
        internal static readonly FixProtocolVersion Version27 = new FixProtocolVersion(ExtendedType, 1, 27);
        internal static readonly FixProtocolVersion Version28 = new FixProtocolVersion(ExtendedType, 1, 28);
        internal static readonly FixProtocolVersion Version29 = new FixProtocolVersion(ExtendedType, 1, 29);
        internal static readonly FixProtocolVersion Version30 = new FixProtocolVersion(ExtendedType, 1, 30);
        internal static readonly FixProtocolVersion Version31 = new FixProtocolVersion(ExtendedType, 1, 31);
        internal static readonly FixProtocolVersion Version32 = new FixProtocolVersion(ExtendedType, 1, 32);
        internal static readonly FixProtocolVersion Version33 = new FixProtocolVersion(ExtendedType, 1, 33);
        internal static readonly FixProtocolVersion Version36 = new FixProtocolVersion(ExtendedType, 1, 36);
        internal static readonly FixProtocolVersion Version38 = new FixProtocolVersion(ExtendedType, 1, 38);
        internal static readonly FixProtocolVersion Version40 = new FixProtocolVersion(ExtendedType, 1, 40);
        internal static readonly FixProtocolVersion Version41 = new FixProtocolVersion(ExtendedType, 1, 41);
        internal static readonly FixProtocolVersion Version42 = new FixProtocolVersion(ExtendedType, 1, 42);
        internal static readonly FixProtocolVersion Version43 = new FixProtocolVersion(ExtendedType, 1, 43);
        internal static readonly FixProtocolVersion Version44 = new FixProtocolVersion(ExtendedType, 1, 44);
        internal static readonly FixProtocolVersion Version45 = new FixProtocolVersion(ExtendedType, 1, 45);
        internal static readonly FixProtocolVersion Version46 = new FixProtocolVersion(ExtendedType, 1, 46);
        internal static readonly FixProtocolVersion Version47 = new FixProtocolVersion(ExtendedType, 1, 47);
        internal static readonly FixProtocolVersion Version57 = new FixProtocolVersion(ExtendedType, 1, 57);
        internal static readonly FixProtocolVersion Version61 = new FixProtocolVersion(ExtendedType, 1, 61);
        internal static readonly FixProtocolVersion Version62 = new FixProtocolVersion(ExtendedType, 1, 62);
        internal static readonly FixProtocolVersion Version63 = new FixProtocolVersion(ExtendedType, 1, 63);
        internal static readonly FixProtocolVersion Version64 = new FixProtocolVersion(ExtendedType, 1, 64);
        internal static readonly FixProtocolVersion Version65 = new FixProtocolVersion(ExtendedType, 1, 65);
        internal static readonly FixProtocolVersion Version66 = new FixProtocolVersion(ExtendedType, 1, 66);
        internal static readonly FixProtocolVersion Version67 = new FixProtocolVersion(ExtendedType, 1, 67);
        internal static readonly FixProtocolVersion Version68 = new FixProtocolVersion(ExtendedType, 1, 68);
        internal static readonly FixProtocolVersion Version69 = new FixProtocolVersion(ExtendedType, 1, 69);

        #endregion

        /// <summary>
        /// Creates empty protocol version instance.
        /// </summary>
        public FixProtocolVersion()
        {
            this.Type = string.Empty;
        }

        /// <summary>
        /// Creates protocol version instance from type, major and minor versions.
        /// </summary>
        /// <param name="type">Protocol type; can not be null.</param>
        /// <param name="majorVersion">Protocol major version; can not be negative.</param>
        /// <param name="minorVersion">Protocol minor version; can not be negative.</param>
        /// <exception cref="System.ArgumentNullException">If protocol type is null.</exception>
        /// <exception cref="System.ArgumentException">If protocol major/minor version is negative.</exception>
        public FixProtocolVersion(string type, int majorVersion, int minorVersion)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), "Protocol type can not be null.");

            if (majorVersion < 0)
                throw new ArgumentOutOfRangeException(nameof(majorVersion), "Major version can not be negative.");

            if (minorVersion < 0)
                throw new ArgumentOutOfRangeException(nameof(minorVersion), "Minor version can not be negative.");

            this.Type = type;
            this.MajorVersion = majorVersion;
            this.MinorVersion = minorVersion;
        }

        /// <summary>
        /// Creates protocol version instance from string.
        /// </summary>
        /// <param name="text">Can not be null.</param>
        /// <exception cref="System.ArgumentNullException">If text is null.</exception>
        /// <exception cref="System.ArgumentException">If text is not valid protocol version string.</exception>
        public FixProtocolVersion(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text), "Protocol version string can not be null.");

            var pattern = new Regex(@"([a-zA-Z]+)\.(\d+)\.(\d+)");
            var match = pattern.Match(text);

            if (!match.Success)
                throw new ArgumentException("Protocol version string has incorrect format.");

            if (text != match.Value)
                throw new ArgumentException("Protocol version string has incorrect format.");


            this.Type = match.Groups[1].Value;
            this.MajorVersion = Convert.ToInt32(match.Groups[2].Value);
            if (this.MajorVersion < 0)
                throw new ArgumentException("Major version can not be negative.");

            this.MinorVersion = Convert.ToInt32(match.Groups[3].Value);
            if (this.MinorVersion < 0)
                throw new ArgumentException("Minor version can not be negative.");
        }

        #region Methods

        /// <summary>
        /// Returns a string, which contains protocol specification.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", this.Type, this.MajorVersion, this.MinorVersion);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">Can not be null; should be ProtocolVersion instance.</param>
        /// <exception cref="System.InvalidCastException">If obj type is different than ProtocolVersion.</exception>
        /// <exception cref="System.ArgumentNullException">If obj is null.</exception>
        /// <returns>True if obj is an instance of ProtocolVersion and equals the value of this instance; otherwise, false. </returns>
        public override bool Equals(object obj)
        {
            var other = (FixProtocolVersion)obj;
            var result = this.Equals(other);
            return result;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            var hashCode = this.Type.GetHashCode() ^ this.MajorVersion.GetHashCode() ^ this.MinorVersion.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="other">Can not be null; should be ProtocolVersion instance.</param>
        /// <exception cref="System.InvalidCastException">If obj type is different than ProtocolVersion.</exception>
        /// <exception cref="System.ArgumentNullException">If obj is null.</exception>
        /// <returns>True if other equals the value of this instance; otherwise, false. </returns>
        public bool Equals(FixProtocolVersion other)
        {
            return Equals(this, other);
        }

        static bool Equals(FixProtocolVersion first, FixProtocolVersion second)
        {
            if (object.ReferenceEquals(first, second))
                return true;

            if (object.ReferenceEquals(null, first) || object.ReferenceEquals(null, second))
                return false;

            if (first.Type != second.Type)
                return false;

            if (first.MajorVersion != second.MajorVersion)
                return false;

            return first.MinorVersion == second.MinorVersion;
        }

        /// <summary>
        /// Compares the current instance with another instance of the protocol version type.
        /// </summary>
        /// <param name="other">A protocol version instance; can not be null; must have the same type.</param>
        /// <returns>
        /// Less than zero if this instance is less than value.
        /// Zero if this instance is equal to value.
        /// Greater than zero if this instance is greater than value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">If 'other' is null.</exception>
        /// <exception cref="System.ArgumentException">If 'other' protocol version has the different type.</exception>
        public int CompareTo(FixProtocolVersion other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), "Protocol version can not be null.");

            if (this.Type != other.Type)
                throw new ArgumentException("Can not compare two protocol versions, which have different types.");

            var result = this.MajorVersion.CompareTo(other.MajorVersion);
            if (result == 0)
                result = this.MinorVersion.CompareTo(other.MinorVersion);


            return result;
        }

        /// <summary>
        /// Returns a value indicating whether two protocol version instances are equal.
        /// </summary>
        /// <param name="first">A protocol version instance; can not be null.</param>
        /// <param name="second">A protocol version instance; can not be null.</param>
        /// <exception cref="System.ArgumentNullException">If first or second argument is null.</exception>
        /// <returns>True, if two protocol version instances are equal, otherwise false.</returns>
        public static bool operator == (FixProtocolVersion first, FixProtocolVersion second)
        {
            var result = Equals(first, second);
            return result;
        }

        /// <summary>
        /// Returns a value indicating whether two protocol version instances are equal.
        /// </summary>
        /// <param name="first">A protocol version instance; can not be null.</param>
        /// <param name="second">A protocol version instance; can not be null.</param>
        /// <exception cref="System.ArgumentNullException">If first or second argument is null.</exception>
        /// <returns>True, if two protocol version instances are different, otherwise false.</returns>
        public static bool operator != (FixProtocolVersion first, FixProtocolVersion second)
        {
            var result = !Equals(first, second);
            return result;
        }

        /// <summary>
        /// Returns a value indicating whether the first protocol version instance is less than second.
        /// </summary>
        /// <param name="first">A protocol version instance; can not be null.</param>
        /// <param name="second">A protocol version instance; can not be null.</param>
        /// <exception cref="System.ArgumentNullException">If first or second argument is null.</exception>
        /// <returns>True, if the first protocol version instance is less than second, otherwise false.</returns>
        public static bool operator < (FixProtocolVersion first, FixProtocolVersion second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first), "Protocol version can not be null.");

            if (second == null)
                throw new ArgumentNullException(nameof(second), "Protocol version can not be null.");

            var status = first.CompareTo(second);
            return (status < 0);
        }

        /// <summary>
        /// Returns a value indicating whether the first protocol version instance is less or equal than second.
        /// </summary>
        /// <param name="first">A protocol version instance; can not be null.</param>
        /// <param name="second">A protocol version instance; can not be null.</param>
        /// <exception cref="System.ArgumentNullException">If first or second argument is null.</exception>
        /// <returns>True, if the first protocol version instance is less or equal than second, otherwise false.</returns>
        public static bool operator <= (FixProtocolVersion first, FixProtocolVersion second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first), "Protocol version can not be null.");

            if (second == null)
                throw new ArgumentNullException(nameof(second), "Protocol version can not be null.");

            var status = first.CompareTo(second);
            return (status <= 0);
        }

        /// <summary>
        /// Returns a value indicating whether the first protocol version instance is more than second.
        /// </summary>
        /// <param name="first">A protocol version instance; can not be null.</param>
        /// <param name="second">A protocol version instance; can not be null.</param>
        /// <exception cref="System.ArgumentNullException">If first or second argument is null.</exception>
        /// <returns>True, if the first protocol version instance is more than second, otherwise false.</returns>
        public static bool operator > (FixProtocolVersion first, FixProtocolVersion second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first), "Protocol version can not be null.");

            if (second == null)
                throw new ArgumentNullException(nameof(second), "Protocol version can not be null.");

            var status = first.CompareTo(second);
            return (status > 0);
        }

        /// <summary>
        /// Returns a value indicating whether the first protocol version instance is more or equal than second.
        /// </summary>
        /// <param name="first">A protocol version instance; can not be null.</param>
        /// <param name="second">A protocol version instance; can not be null.</param>
        /// <exception cref="System.ArgumentNullException">If first or second argument is null.</exception>
        /// <returns>True, if the first protocol version instance is more or equal than second, otherwise false.</returns>
        public static bool operator >= (FixProtocolVersion first, FixProtocolVersion second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first), "Protocol version can not be null.");

            if (second == null)
                throw new ArgumentNullException(nameof(second), "Protocol version can not be null.");

            var status = first.CompareTo(second);
            return (status >= 0);
        }

        /// <summary>
        /// Converts protocol type, major version and minor version to string.
        /// </summary>
        /// <param name="type">Protocol type; can not be null.</param>
        /// <param name="majorVersion">Protocol major version; can not be negative.</param>
        /// <param name="minorVersion">Protocol minor version; can not be negative.</param>
        /// <exception cref="System.ArgumentNullException">If protocol type is null.</exception>
        /// <exception cref="System.ArgumentException">If protocol major/minor version is negative.</exception>
        /// <returns>Can not be null.</returns>
        public static string ToString(string type, int majorVersion, int minorVersion)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), "Protocol type can not be null.");

            if (majorVersion < 0)
                throw new ArgumentException("Major version can not be negative.");

            if (minorVersion < 0)
                throw new ArgumentException("Minor version can not be negative.");

            return string.Format("{0}.{1}.{2}", type, majorVersion, minorVersion);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get up-to-date protocol version.
        /// </summary>
        public static FixProtocolVersion TheLatestVersion
        {
            get
            {
                return CurrentVersion;
            }
        }

        /// <summary>
        /// Gets protocol type.
        /// </summary>
        /// <value>A new protocol type; can not be null.</value>
        /// <exception cref="System.ArgumentNullException">If protocol type is null.</exception>
        public string Type { get; private set; }

        /// <summary>
        /// Gets protocol major version.
        /// </summary>
        /// <value>A new protocol major version; can not be negative.</value>
        /// <exception cref="System.ArgumentException">If protocol major version is negative.</exception>
        public int MajorVersion { get; private set; }

        /// <summary>
        /// Gets protocol minor version.
        /// </summary>
        /// <value>A new protocol minor version; can not be negative.</value>
        /// <exception cref="System.ArgumentException">If protocol minor version is negative.</exception>
        public int MinorVersion { get; private set; }

        #endregion
    }
}
