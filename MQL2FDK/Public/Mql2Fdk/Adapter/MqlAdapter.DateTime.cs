namespace Mql2Fdk
{
    using System;
    using System.Text;


    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region Date and Time

        /// <summary>
        /// TIME_DATE gets result as "yyyy.mm.dd",
        /// </summary>
        protected const int TIME_DATE = 0x1;

        /// <summary>
        /// TIME_MINUTES gets result as "hh:mi",
        /// </summary>
        protected const int TIME_MINUTES = 0x2;

        /// <summary>
        /// TIME_SECONDS gets result as "hh:mi:ss".
        /// </summary>
        protected const int TIME_SECONDS = 0x4;

        /// <summary>
        /// Returns the current day of the month, i.e., the day of month of the last known server time.
        /// </summary>
        /// <returns></returns>
        protected int Day()
        {
            return this.currentSnapshot.ServerDateTime.Day;
        }

        /// <summary>
        /// Returns the current zero-based day of the week (0-Sunday,1,2,3,4,5,6) of the last known server time.
        /// </summary>
        /// <returns></returns>
        protected int DayOfWeek()
        {
            return (int)this.currentSnapshot.ServerDateTime.DayOfWeek;
        }

        /// <summary>
        /// Returns the current day of the year (1 means 1 January,..,365(6) does 31 December), i.e., the day of year of the last known server time.
        /// </summary>
        /// <returns></returns>
        protected int DayOfYear()
        {
            return this.currentSnapshot.ServerDateTime.DayOfYear;
        }

        /// <summary>
        /// Returns the current month as number (1-January,2,3,4,5,6,7,8,9,10,11,12), i.e., the number of month of the last known server time.
        /// </summary>
        /// <returns></returns>
        protected int Month()
        {
            return this.currentSnapshot.ServerDateTime.Month;
        }

        /// <summary>
        /// Returns the hour (0,1,2,..23) of the last known server time by the moment of the program start (this value will not change within the time of the program execution).
        /// Note: At the testing, the last known server time is modelled. 
        /// </summary>
        /// <returns></returns>
        protected int Hour()
        {
            return this.currentSnapshot.ServerDateTime.Hour;
        }

        /// <summary>
        /// Returns the current minute (0,1,2,..59) of the last known server time by the moment of the program start (this value will not change within the time of the program execution). 
        /// </summary>
        /// <returns></returns>
        protected int Minute()
        {
            return this.currentSnapshot.ServerDateTime.Minute;
        }

        /// <summary>
        /// Returns the amount of seconds elapsed from the beginning of the current minute of the last known server time by the moment of the program start (this value will not change within the time of the program execution). 
        /// </summary>
        /// <returns></returns>
        protected int Seconds()
        {
            return this.currentSnapshot.ServerDateTime.Second;
        }

        /// <summary>
        /// Returns the last known server time (time of incoming of the latest quote) as number of seconds elapsed from 00:00 January 1, 1970.
        /// </summary>
        /// <returns></returns>
        protected datetime TimeCurrent()
        {
            var serverTime = this.currentSnapshot.ServerDateTime;
            return new datetime(serverTime);
        }

        /// <summary>
        /// Returns day of month (1 - 31) for the specified date.
        /// </summary>
        /// <param name="date">datetime as number of seconds elapsed since midnight (00:00:00), January 1, 1970</param>
        /// <returns></returns>
        protected int TimeDay(datetime date)
        {
            return date.DateTime.Day;
        }

        /// <summary>
        /// Returns the zero-based day of week (0 means Sunday,1,2,3,4,5,6) for the specified date.
        /// </summary>
        /// <param name="date">datetime as number of seconds elapsed since midnight (00:00:00), January 1, 1970.</param>
        /// <returns></returns>
        protected int TimeDayOfWeek(datetime date)
        {
            return (int)date.DateTime.DayOfWeek;
        }

        /// <summary>
        /// Returns day (1 means 1 January,..,365(6) does 31 December) of year for the specified date.
        /// </summary>
        /// <param name="date">datetime as number of seconds elapsed since midnight (00:00:00), January 1, 1970</param>
        /// <returns></returns>
        protected int TimeDayOfYear(datetime date)
        {
            return date.DateTime.DayOfYear;
        }

        /// <summary>
        /// Returns the hour for the specified time.
        /// </summary>
        /// <param name="time">datetime as number of seconds elapsed since midnight (00:00:00), January 1, 1970</param>
        /// <returns></returns>
        protected int TimeHour(datetime time)
        {
            return time.DateTime.Hour;
        }

        /// <summary>
        /// Returns local computer time as number of seconds elapsed from 00:00 January 1, 1970.
        /// </summary>
        /// <returns></returns>
        protected datetime TimeLocal()
        {
            return new datetime(DateTime.Now);
        }

        /// <summary>
        /// Returns the minute for the specified time. 
        /// </summary>
        /// <param name="time">datetime as number of seconds elapsed since midnight (00:00:00), January 1, 1970</param>
        /// <returns></returns>
        protected int TimeMinute(datetime time)
        {
            return time.DateTime.Minute;
        }

        /// <summary>
        /// Returns the month number for the specified time.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        protected int TimeMonth(datetime time)
        {
            return time.DateTime.Month;
        }

        /// <summary>
        /// Returns the amount of seconds elapsed from the beginning of the minute for the specified time.
        /// </summary>
        /// <param name="time">datetime as number of seconds elapsed since midnight (00:00:00), January 1, 1970</param>
        /// <returns></returns>
        protected int TimeSeconds(datetime time)
        {
            return time.DateTime.Second;
        }

        /// <summary>
        /// Returns year for the specified date.
        /// </summary>
        /// <param name="time">datetime as number of seconds elapsed since midnight (00:00:00), January 1, 1970</param>
        /// <returns></returns>
        protected int TimeYear(datetime time)
        {
            return time.DateTime.Year;
        }

        /// <summary>
        /// Returns the current year, i.e., the year of the last known server time.
        /// </summary>
        /// <returns></returns>
        protected int Year()
        {
            return this.currentSnapshot.ServerDateTime.Year;
        }

        #endregion
    }
}
