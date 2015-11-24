namespace Mql2Fdk
{
    using System;
    using System.Drawing;

    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region Color Constants

        /// <summary>
        /// Blue color constant.
        /// </summary>
        protected static readonly int Blue = KnownColor.Blue.KnownToArgb();

        /// <summary>
        /// White color constant.
        /// </summary>
        protected static readonly int White = CastUtils.KnownToArgb(KnownColor.White);

        /// <summary>
        /// Red color constant.
        /// </summary>
        protected static readonly int Red = CastUtils.KnownToArgb(KnownColor.Red);

        /// <summary>
        /// DarkGray constant.
        /// </summary>
        protected static readonly int DarkGray = CastUtils.KnownToArgb(KnownColor.DarkGray);

        /// <summary>
        /// Orange constant.
        /// </summary>
        protected static readonly int Orange = CastUtils.KnownToArgb(KnownColor.Orange);

        /// <summary>
        /// Violet constant.
        /// </summary>
        protected static readonly int Violet = CastUtils.KnownToArgb(KnownColor.Violet);

        /// <summary>
        /// Green constant.
        /// </summary>
        protected static readonly int Green = CastUtils.KnownToArgb(KnownColor.Green);

        /// <summary>
        /// Yellow color.
        /// </summary>
        protected static readonly int Yellow = CastUtils.KnownToArgb(KnownColor.Yellow);

        /// <summary>
        /// Purple color.
        /// </summary>
        protected static readonly int Purple = CastUtils.KnownToArgb(KnownColor.Purple);

        /// <summary>
        /// Lime color
        /// </summary>
        protected static readonly int Lime = CastUtils.KnownToArgb(KnownColor.Lime);

        /// <summary>
        /// Pink color
        /// </summary>
        protected static readonly int Pink = KnownColor.Pink.KnownToArgb();

        /// <summary>
        /// Aqua color
        /// </summary>
        protected static readonly int Aqua = KnownColor.Aqua.KnownToArgb();

        /// <summary>
        /// DarkOrange
        /// </summary>
        protected static readonly int DarkOrange = KnownColor.DarkOrange.KnownToArgb();

        /// <summary>
        /// DarkBlue
        /// </summary>
        protected static readonly int DarkBlue = KnownColor.DarkBlue.KnownToArgb();

        /// <summary>
        /// Chocolate
        /// </summary>
        protected static readonly int Chocolate = KnownColor.Chocolate.KnownToArgb();

        /// <summary>
        /// Maroon
        /// </summary>
        protected static readonly int Maroon = KnownColor.Maroon.KnownToArgb();

        /// <summary>
        /// Teal
        /// </summary>
        protected static readonly int Teal = KnownColor.Teal.KnownToArgb();

        /// <summary>
        /// LimeGreen
        /// </summary>
        protected static readonly int LimeGreen = KnownColor.LimeGreen.KnownToArgb();

        /// <summary>
        /// LightGreen
        /// </summary>
        protected static readonly int LightGreen = KnownColor.LightGreen.KnownToArgb();

        /// <summary>
        /// Tomato
        /// </summary>
        protected static readonly int Tomato = KnownColor.Tomato.KnownToArgb();

        /// <summary>
        /// Black
        /// </summary>
        protected static readonly int Black = KnownColor.Black.KnownToArgb();

        /// <summary>
        /// Brown
        /// </summary>
        protected static readonly int Brown = KnownColor.Brown.KnownToArgb();

        /// <summary>
        /// 
        /// </summary>
        protected static readonly int DarkViolet = KnownColor.Tomato.KnownToArgb();

        /// <summary>
        /// 
        /// </summary>
        protected static readonly int DeepPink = KnownColor.Tomato.KnownToArgb();

        /// <summary>
        /// DodgerBlue
        /// </summary>
        protected static readonly int DodgerBlue = KnownColor.DodgerBlue.KnownToArgb();

        /// <summary>
        /// Gold
        /// </summary>
        protected static readonly int Gold = KnownColor.Gold.KnownToArgb();

        /// <summary>
        /// LawnGreen
        /// </summary>
        protected static readonly int LawnGreen = KnownColor.LawnGreen.KnownToArgb();

        /// <summary>
        /// MediumBlue
        /// </summary>
        protected static readonly int MediumBlue = KnownColor.MediumBlue.KnownToArgb();

        /// <summary>
        /// MediumSeaGreen
        /// </summary>
        protected static readonly int MediumSeaGreen = KnownColor.MediumSeaGreen.KnownToArgb();

        /// <summary>
        /// OrangeRed
        /// </summary>
        protected static readonly int OrangeRed = KnownColor.OrangeRed.KnownToArgb();

        /// <summary>
        /// RoyalBlue
        /// </summary>
        protected static readonly int RoyalBlue = KnownColor.RoyalBlue.KnownToArgb();


        /// <summary>
        /// No color constant.
        /// </summary>
        protected const int CLR_NONE = -1;

        #endregion

        #region Price Constants

        /// <summary>
        /// Close price.
        /// </summary>
        protected const int PRICE_CLOSE = 0;

        /// <summary>
        /// Open price.
        /// </summary>
        protected const int PRICE_OPEN = 1;

        /// <summary>
        /// High price.
        /// </summary>
        protected const int PRICE_HIGH = 2;

        /// <summary>
        /// Low price.
        /// </summary>
        protected const int PRICE_LOW = 3;

        /// <summary>
        /// Median price, (high+low)/2. 
        /// </summary>
        protected const int PRICE_MEDIAN = 4;

        /// <summary>
        ///  Typical price, (high+low+close)/3.
        /// </summary>
        protected const int PRICE_TYPICAL = 5;

        /// <summary>
        ///  Weighted close price, (high+low+close+close)/4. 
        /// </summary>
        protected const int PRICE_WEIGHTED = 6;

        #endregion

        #region Time Frame Constants

        /// <summary>
        /// 1 minute
        /// </summary>
        protected const int PERIOD_M1 = 1;

        /// <summary>
        /// 5 minutes
        /// </summary>
        protected const int PERIOD_M5 = 5;

        /// <summary>
        /// 15 minutes
        /// </summary>
        protected const int PERIOD_M15 = 15;

        /// <summary>
        /// 30 minutes
        /// </summary>
        protected const int PERIOD_M30 = 30;

        /// <summary>
        /// 1 hour
        /// </summary>
        protected const int PERIOD_H1 = 60;

        /// <summary>
        /// 4 hour
        /// </summary>
        protected const int PERIOD_H4 = 240;

        /// <summary>
        /// Daily
        /// </summary>
        protected const int PERIOD_D1 = 1440;

        /// <summary>
        /// Weekly
        /// </summary>
        protected const int PERIOD_W1 = 10080;

        /// <summary>
        /// Monthly
        /// </summary>
        protected const int PERIOD_MN1 = 43200;

        #endregion

        #region Moving Average methods

        /// <summary>
        /// Simple moving average.
        /// </summary>
        protected const int MODE_SMA = 0;

        /// <summary>
        /// Exponential moving average.
        /// </summary>
        protected const int MODE_EMA = 1;

        /// <summary>
        /// Smoothed moving average.
        /// </summary>
        protected const int MODE_SMMA = 2;

        /// <summary>
        /// Linear weighted moving average.
        /// </summary>
        protected const int MODE_LWMA = 3;

        /// <summary>
        /// MODE_GATORTEETH constant
        /// </summary>
        protected const int MODE_GATORTEETH = 2;

        #endregion

        #region Market Info Constants

        /// <summary>
        /// Last incoming bid price. For the current symbol, it is stored in the predefined variable Bid
        /// </summary>
        protected const int MODE_BID = 9;

        /// <summary>
        /// Last incoming ask price. For the current symbol, it is stored in the predefined variable Ask
        /// </summary>
        protected const int MODE_ASK = 10;

        /// <summary>
        /// Point size in the quote currency. For the current symbol, it is stored in the predefined variable Point
        /// </summary>
        protected const int MODE_POINT = 11;

        /// <summary>
        /// Count of digits after decimal point in the symbol prices. For the current symbol, it is stored in the predefined variable Digits
        /// </summary>
        protected const int MODE_DIGITS = 12;

        /// <summary>
        ///  Spread value in points
        /// </summary>
        protected const int MODE_SPREAD = 13;

        /// <summary>
        /// Lot size in the base currency.
        /// </summary>
        protected const int MODE_LOTSIZE = 15;

        /// <summary>
        /// Tick size in the quote currency.
        /// </summary>
        protected const int MODE_TICKSIZE = 17;

        /// <summary>
        ///  Swap of the long position.
        /// </summary>
        protected const int MODE_SWAPLONG = 18;

        /// <summary>
        /// Swap of the short position.
        /// </summary>
        protected const int MODE_SWAPSHORT = 19;

        /// <summary>
        /// Minimum permitted amount of a lot
        /// </summary>
        protected const int MODE_MINLOT = 23;

        /// <summary>
        /// Step for changing lots
        /// </summary>
        protected const int MODE_LOTSTEP = 24;

        /// <summary>
        /// Maximum permitted amount of a lot
        /// </summary>
        protected const int MODE_MAXLOT = 25;

        /// <summary>
        /// Margin calculation mode. 0 - Forex; 1 - CFD; 2 - Futures; 3 - CFD for indices.
        /// </summary>
        protected const int MODE_MARGINCALCMODE = 28;

        /// <summary>
        /// Initial margin requirements for 1 lot.
        /// </summary>
        protected const int MODE_MARGININIT = 29;

        /// <summary>
        /// Stop level
        /// </summary>
        protected const int MODE_STOPLEVEL = 30;

        /// <summary>
        /// Tick value
        /// </summary>
        protected const int MODE_TICKVALUE = 31;

        /// <summary>
        /// Starting mode
        /// </summary>
        protected const int MODE_STARTING = 32;

        /// <summary>
        /// Expiration mode
        /// </summary>
        protected const int MODE_EXPIRATION = 33;

        /// <summary>
        /// Trading is allowed
        /// </summary>
        protected const int MODE_TRADEALLOWED = 34;

        /// <summary>
        /// Trading is allowed
        /// </summary>
        protected const int MODE_MAIN = 35;

        #endregion

        #region Object Properties

        /// <summary>
        /// Datetime value to set/get first coordinate time part.
        /// </summary>
        protected datetime OBJPROP_TIME1 = new datetime(0);

        /// <summary>
        /// Double value to set/get first coordinate price part.
        /// </summary>
        protected double OBJPROP_PRICE1;

        /// <summary>
        /// Datetime value to set/get second coordinate time part.
        /// </summary>
        protected datetime OBJPROP_TIME2 = new datetime(0);

        /// <summary>
        /// Double value to set/get second coordinate price part.
        /// </summary>
        protected double OBJPROP_PRICE2 = new datetime(0);

        /// <summary>
        /// Datetime value to set/get third coordinate time part.
        /// </summary>
        protected datetime OBJPROP_TIME3 = new datetime(0);

        /// <summary>
        /// Double value to set/get third coordinate price part.
        /// </summary>
        protected double OBJPROP_PRICE3 = new datetime(0);

        /// <summary>
        /// Color value to set/get object color.
        /// </summary>
        protected color OBJPROP_COLOR = new color(0);

        /// <summary>
        /// Value is one of STYLE_SOLID, STYLE_DASH, STYLE_DOT, STYLE_DASHDOT, STYLE_DASHDOTDOT constants to set/get object line style.
        /// </summary>
        protected int OBJPROP_STYLE = new datetime(0);

        /// <summary>
        /// Integer value to set/get object line width. Can be from 1 to 5.
        /// </summary>
        protected int OBJPROP_WIDTH = new datetime(0);

        /// <summary>
        /// Boolean value to set/get background drawing flag for object.
        /// </summary>
        protected bool OBJPROP_BACK;

        /// <summary>
        /// Boolean value to set/get ray flag of object.
        /// </summary>
        protected bool OBJPROP_RAY;

        /// <summary>
        /// Boolean value to set/get ellipse flag for fibo arcs.
        /// </summary>
        protected bool OBJPROP_ELLIPSE;

        /// <summary>
        /// Double value to set/get scale object property.
        /// </summary>
        protected datetime OBJPROP_SCALE = new datetime(0);

        /// <summary>
        /// Double value to set/get angle object property in degrees.
        /// </summary>
        protected datetime OBJPROP_ANGLE = new datetime(0);

        /// <summary>
        /// Integer value or arrow enumeration to set/get arrow code object property.
        /// </summary>
        protected datetime OBJPROP_ARROWCODE = new datetime(0);

        /// <summary>
        /// Value can be one or combination (bitwise addition) of object visibility constants to set/get timeframe object property.
        /// </summary>
        protected datetime OBJPROP_TIMEFRAMES = new datetime(0);

        /// <summary>
        /// Double value to set/get deviation property for Standard deviation objects.
        /// </summary>
        protected datetime OBJPROP_DEVIATION = new datetime(0);

        /// <summary>
        /// Integer value to set/get font size for text objects.
        /// </summary>
        protected datetime OBJPROP_FONTSIZE = new datetime(0);

        /// <summary>
        /// Integer value to set/get anchor corner property for label objects. Must be from 0-3.
        /// </summary>
        protected datetime OBJPROP_CORNER = new datetime(0);

        /// <summary>
        /// Integer value to set/get anchor X distance object property in pixels.
        /// </summary>
        protected datetime OBJPROP_XDISTANCE = new datetime(0);

        /// <summary>
        /// Integer value is to set/get anchor Y distance object property in pixels.
        /// </summary>
        protected datetime OBJPROP_YDISTANCE = new datetime(0);


        /// <summary>
        /// Vertical line. Uses price part of first coordinate..
        /// </summary>
        protected int OBJ_VLINE = 0;

        /// <summary>
        /// Horizontal line. Uses price part of first coordinate..
        /// </summary>
        protected int OBJ_HLINE = 1;

        /// <summary>
        /// Text label. Uses 1 coordinate in pixels. 
        /// </summary>
        protected int OBJ_LABEL = 23;

        /// <summary>
        /// Text. Uses 1 coordinate.
        /// </summary>
        protected int OBJ_TEXT = 21;

        /// <summary>
        /// Arrows. Uses 1 coordinate.
        /// </summary>
        protected int OBJ_ARROW = 22;


        #endregion

        #region Constants

        /// <summary>
        /// index in the order pool
        /// </summary>
        protected const int SELECT_BY_POS = 0;

        /// <summary>
        /// index is order ticket
        /// </summary>
        protected const int SELECT_BY_TICKET = 1;

        /// <summary>
        /// order selected from trading pool(opened and pending orders)
        /// </summary>
        protected const int MODE_TRADES = 0;

        /// <summary>
        /// order selected from history pool (closed and canceled order)
        /// </summary>
        protected const int MODE_HISTORY = 1;

        /// <summary>
        /// buying position
        /// </summary>
        protected const int OP_BUY = 0;

        /// <summary>
        /// selling position
        /// </summary>
        protected const int OP_SELL = 1;

        /// <summary>
        /// buy limit pending position
        /// </summary>
        protected const int OP_BUYLIMIT = 2;

        /// <summary>
        /// buy stop pending position
        /// </summary>
        protected const int OP_BUYSTOP = 3;

        /// <summary>
        /// sell limit pending position
        /// </summary>
        protected const int OP_SELLLIMIT = 4;

        /// <summary>
        /// sell stop pending position.
        /// </summary>
        protected const int OP_SELLSTOP = 5;

        #endregion

        #region Common Constants

        /// <summary>
        /// Used with array functions. Indicates that all array elements will be processed.
        /// </summary>
        protected const int WHOLE_ARRAY = 0;

        #endregion

        #region Mode Constants

        /// <summary>
        /// Open price.
        /// </summary>
        protected const int MODE_OPEN = 0;

        /// <summary>
        /// Low price.
        /// </summary>
        protected const int MODE_LOW = 1;

        /// <summary>
        /// High price.
        /// </summary>
        protected const int MODE_HIGH = 2;

        /// <summary>
        /// Close price.
        /// </summary>
        protected const int MODE_CLOSE = 3;

        /// <summary>
        /// Volume, used in iLowest() and iHighest() functions.
        /// </summary>
        protected const int MODE_VOLUME = 4;

        /// <summary>
        /// Bar open time, used in ArrayCopySeries() function.
        /// </summary>
        protected const int MODE_TIME = 5;

        /// <summary>
        /// MODE_FREEZELEVEL constant
        /// </summary>
        protected const int MODE_FREEZELEVEL = 33;

        /// <summary>
        /// MODE_GATORJAW constant
        /// </summary>
        protected const int MODE_GATORJAW = 1;

        /// <summary>
        /// MODE_LOWER constant
        /// </summary>
        protected const int MODE_LOWER = 2;

        /// <summary>
        /// MODE_GATORLIPS constant
        /// </summary>
        protected const int MODE_GATORLIPS = 3;

        /// <summary>
        /// MODE_MARGINREQUIRED constant
        /// </summary>
        protected const int MODE_MARGINREQUIRED = 32;

        /// <summary>
        /// MODE_PLUSDI constant
        /// </summary>
        protected const int MODE_PLUSDI = 1;


        /// <summary>
        /// MODE_MINUSDI constant
        /// </summary>
        protected const int MODE_MINUSDI = 2;

        /// <summary>
        /// MODE_SIGNAL constant
        /// </summary>
        protected const int MODE_SIGNAL = 2;

        /// <summary>
        /// MODE_UPPER constant
        /// </summary>
        protected const int MODE_UPPER = 1;

        /// <summary>
        /// OBJ_RECTANGLE constant
        /// </summary>
        protected const int OBJ_RECTANGLE = 16;

        /// <summary>
        /// REASON_CHARTCHANGE constant
        /// </summary>
        protected const int REASON_CHARTCHANGE = 3;




        /// <summary>
        /// Draw line constant
        /// </summary>
        protected const int DRAW_LINE = 0;

        /// <summary>
        /// Empty value constant
        /// </summary>
        protected const int EMPTY_VALUE = int.MaxValue;

        #endregion

        #region Constants for Files

        /// <summary>
        /// File write constant
        /// </summary>
        protected const int FILE_READ = 1;

        /// <summary>
        /// File write constant
        /// </summary>
        protected const int FILE_WRITE = 2;

        /// <summary>
        /// File write constant
        /// </summary>
        protected const int FILE_BIN = 4;

        /// <summary>
        /// File write constant
        /// </summary>
        protected const int FILE_CSV = 8;

        /// <summary>
        /// SEEK_END constant
        /// </summary>
        protected int SEEK_END = 2;

        /// <summary>
        /// OBJ_TREND constant
        /// </summary>
        protected int OBJ_TREND = 2;

        #endregion

        #region Styles

        /// <summary>
        /// The pen is solid.
        /// </summary>
        protected const int STYLE_SOLID = 0;

        /// <summary>
        /// The pen is dashed.
        /// </summary>
        protected const int STYLE_DASH = 1;

        /// <summary>
        /// The pen is dotted.
        /// </summary>
        protected const int STYLE_DOT = 2;

        /// <summary>
        /// The pen has alternating dashes and dots.
        /// </summary>
        protected const int STYLE_DASHDOT = 3;

        /// <summary>
        /// The pen has alternating dashes and double dots.
        /// </summary>
        protected const int STYLE_DASHDOTDOT = 4;

        #endregion
    }
}
