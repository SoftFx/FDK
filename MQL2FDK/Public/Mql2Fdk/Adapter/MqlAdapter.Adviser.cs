namespace Mql2Fdk
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region Chart Operations

        /// <summary>
        /// Returns a text string with the name of the current financial instrument.
        /// </summary>
        /// <returns>a current financial instrument</returns>
        protected string Symbol()
        {
            return this.symbol;
        }

        /// <summary>
        /// Returns name of the executed expert, script, custom indicator, or library, depending on the MQL4 program, from which this function has been called. 
        /// </summary>
        /// <returns></returns>
        protected string WindowExpertName()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// If indicator with name was found, the function returns the window index containing this specified indicator, otherwise it returns -1.
        /// Note: WindowFind() returns -1 if custom indicator searches itself when init() function works. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected int WindowFind(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns maximal value of the vertical scale of the specified subwindow of the current chart (0-main chart window, the indicators' subwindows are numbered starting from 1). If the subwindow index has not been specified, the maximal value of the price scale of the main chart window is returned.
        /// See also WindowPriceMin(), WindowFirstVisibleBar(), WindowBarsPerChart() 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected double WindowPriceMax(int index = 0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns minimal value of the vertical scale of the specified subwindow of the current chart (0-main chart window, the indicators' subwindows are numbered starting from 1). If the subwindow index has not been specified, the minimal value of the price scale of the main chart window is returned.
        /// See also WindowPriceMax(), WindowFirstVisibleBar(), WindowBarsPerChart() 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected double WindowPriceMin(int index = 0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves current chart screen shot as a GIF file. Returns FALSE if it fails. To get the error code, one has to use the GetLastError() function.
        /// The screen shot is saved in the terminal_dir\experts\files (terminal_dir\tester\files in case of testing) directory or its subdirectories. 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="size_x"></param>
        /// <param name="size_y"></param>
        /// <param name="start_bar"></param>
        /// <param name="chart_scale"></param>
        /// <param name="chart_mode"></param>
        /// <returns></returns>
        protected bool WindowScreenShot(string filename, int size_x, int size_y, int start_bar = -1, int chart_scale = -1, int chart_mode = -1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The method returns always zero.
        /// </summary>
        /// <param name="symbol">symbol name.</param>
        /// <param name="timeframe">Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe.</param>
        /// <returns></returns>
        protected int WindowHandle(string symbol, int timeframe)
        {
            return 0;
        }

        #endregion
    }
}
