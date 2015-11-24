namespace Mql2Fdk
{
    using System;
    using System.Drawing;
    using System.Globalization;

    public static class CastUtils
    {
        const double Epsilon = 1e-6;

        public static int ToInt(this bool value)
        {
            return value ? 1 : 0;
        }

        public static double ToDouble(this bool value)
        {
            return value ? 1 : 0;
        }

        public static bool ToBool(this int value)
        {
            return value != 0;
        }

        public static bool ToBool(this double value)
        {
            return Math.Abs(value) > Epsilon;
        }

        public static string ConvStr(this bool val)
        {
            return val.ToInt().ToString(CultureInfo.InvariantCulture);
        }

        public static string ConvStr(this int val)
        {
            return val.ToString(CultureInfo.InvariantCulture);
        }

        public static string ConvStr(this double val)
        {
            return val.ToString(CultureInfo.InvariantCulture);
        }

        public static int ColorFromString(this string value)
        {
            var typeHostOfColors = typeof(KnownColor);
            var getColorFromStr = (KnownColor)typeHostOfColors.GetField(value).GetValue(null);

            var knownToArgb = getColorFromStr.KnownToArgb();
            return knownToArgb;
        }

        /// <summary>
        /// Convert known colors to their Argb int32 format
        /// </summary>
        /// <param name="color">Color to be converted</param>
        /// <returns></returns>
        public static int KnownToArgb(this KnownColor color)
        {
            return Color.FromKnownColor(color).ToArgb();
        }
    }
}