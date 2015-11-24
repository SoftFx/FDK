namespace Mql2Fdk
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region Conversion Functions

        /// <summary>
        /// Converts value containing time in seconds that has passed since January 1, 1970, into a string of "yyyy.mm.dd hh:mi" format. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        protected string TimeToStr(datetime value, int mode = TIME_DATE | TIME_MINUTES)
        {
            var result = new StringBuilder();

            var dateTime = value.DateTime;

            if ((mode & TIME_DATE) != 0)
                result.AppendFormat(dateTime.ToString("yyyy.MM.dd "));

            if ((mode & TIME_SECONDS) != 0)
                result.AppendFormat(dateTime.ToString("hh:mm:ss"));
            else if ((mode & TIME_MINUTES) != 0)
                result.AppendFormat(dateTime.ToString("hh:mm"));

            return result.ToString();
        }

        /// <summary>
        /// Converts string in the format "yyyy.mm.dd hh:mi" to datetime type (the amount of seconds that have passed since 1 Jan., 1970). 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected datetime StrToTime(string value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Rounds the floating point value to the given precision. Returns normalized value of the double type.
        /// </summary>
        /// <param name="value">a floating value</param>
        /// <param name="digits">must be from 0 to 8</param>
        /// <returns>normalized value</returns>
        protected static double NormalizeDouble(double value, int digits)
        {
            if (digits < 0 || digits > 8)
                throw new ArgumentOutOfRangeException("digits", digits, "Digits precision must be from 0 to 8.");

            var factor = Math.Pow(10, digits);
            var result = value * factor;
            result = Math.Round(result);
            result /= factor;
            return result;
        }

        /// <summary>
        /// Converts string representation of number to double type (double-precision format with floating point). 
        /// </summary>
        /// <param name="value">String containing the number character representation format..</param>
        /// <returns></returns>
        public static double StrToDouble(string value)
        {
            return Convert.ToDouble(value);
        }

        /// <summary>
        /// Converts string containing the value character representation into a value of the int (integer) type.  
        /// </summary>
        /// <param name="value">String containing the integer character representation format..</param>
        /// <returns></returns>
        protected static int StrToInteger(string value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Conversion of the symbol code into a one-character string. 
        /// </summary>
        /// <param name="charCode"></param>
        /// <returns></returns>
        protected static string CharToStr(int charCode)
        {
            var ch = (char)charCode;
            return ch.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns text string with the specified numerical value converted into a specified precision format. 
        /// </summary>
        /// <param name="d">Floating point value.</param>
        /// <param name="decimals">Precision format, number of digits after decimal point (0-8).</param>
        /// <returns></returns>
        protected static string DoubleToStr(double d, int decimals)
        {
            var zeroes = string.Empty;
            for (var i = 0; i < decimals; i++)
                zeroes += "0";
            var format = string.Format("{{0:0.{0}}}", zeroes);
            var s = string.Format(format, d);
            return s;
        }

        #endregion
    }
}
