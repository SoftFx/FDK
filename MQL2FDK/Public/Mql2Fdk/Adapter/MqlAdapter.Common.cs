namespace Mql2Fdk
{
    using System;
    using System.Media;
    using System.Threading;
    using System.Windows.Forms;
    using SoftFX.Extended;

    using WinFormsMessageBox = System.Windows.Forms.MessageBox;

    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region Common Functions

        /// <summary>
        /// The function outputs the comment defined by the user in the left top corner of the chart. Parameters can be of any type. Amount of passed parameters cannot exceed 64
        /// Arrays cannot be passed to the Comment() function. Arrays should be output element wise.
        /// Data of double type output with 4 digits after the decimal point. To output with more precision, use the DoubleToStr() function.
        /// Data of bool, datetime and color types will be output as their numeric presentation.
        /// To output values of datetime type as strings, convert them with the TimeToStr() function.
        /// See also Alert() and Print() functions.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected void Comment(params object[] args)
        {
            this.log.Comment(args);
        }

        /// <summary>
        /// Prints a message to the console window. Parameters can be of any type.
        /// </summary>
        /// <param name="args">list of parameters, which should be printed</param>
        protected void Print(params object[] args)
        {
            this.log.Print(args);
        }

        /// <summary>
        /// Prints a message to the console window. Parameters can be of any type.
        /// </summary>
        /// <param name="args">list of parameters, which should be printed</param>
        protected void Alert(params object[] args)
        {
            this.log.Alert(args);
        }

        /// <summary>
        /// The MessageBox function creates, displays, and operates message box. The message box contains an application-defined message and header, as well as a random combination of predefined icons and push buttons. If the function succeeds, the returned value is one of the MessageBox return code values.
        /// The function cannot be called from custom indicators since they are executed within interface thread and may not decelerate it. 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        protected int MessageBox(string text = "", int flags = 0)
        {
            return (int)WinFormsMessageBox.Show(text, "FDK", (MessageBoxButtons)flags);
        }

        /// <summary>
        /// Sends a message to the e-mail set in the Tools->Options->EMail tab.
        /// The sending can be disabled in settings, or it can be omitted to specify the e-mail address. To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="someText"></param>
        protected void SendMail(string subject, string someText)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends the file to the FTP server set in the Tools->Options->Publisher tab. If the attempt fails, it retuns FALSE.
        /// The function does not operate in the testing mode. This function cannot be called from custom indicators, either.
        /// The file to be sent must be stored in the terminal_directory\experts\files folder or in its sub-folders.
        /// It will not be sent if there is no FTP address and/or access password specified in settings. 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="ftpPath"></param>
        /// <returns></returns>
        protected bool SendFTP(string filename, string ftpPath = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends Push notification to mobile terminals whose MetaQuotes IDs are specified on the "Notifications" tab in options window.
        /// Sending notifications can be disabled in the settings. The ID can also be omitted.
        /// In case of error, the function returns false. To get information about the error, call the GetLastError() function.
        /// 
        /// Note: The SendNotification() function has strict limitations on its usage: no more than 2 calls per second and no more than 10 calls per minute. Frequency of calls is controlled dynamically, and the function can be blocked in case of violation. 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool SendNotification(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the number of milliseconds elapsed since the system started.
        /// </summary>
        /// <returns></returns>
        protected int GetTickCount()
        {
            return Environment.TickCount;
        }

        /// <summary>
        /// The Sleep() function suspends execution of the current expert within the specified interval.
        /// </summary>
        /// <param name="milliseconds"></param>
        protected void Sleep(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        /// <summary>
        /// Function plays a sound file. The file must be located in the terminal_dir\sounds directory or in its subdirectory. 
        /// </summary>
        /// <param name="filename">Path to the sound file.</param>
        protected void PlaySound(string filename)
        {
            using (var soundPlayer = new SoundPlayer(filename))
            {
                soundPlayer.Play();
            }
        }

        #endregion

        #region Checkup

        /// <summary>
        /// The method always returns true.
        /// </summary>
        protected bool IsDllsAllowed()
        {
            return true;
        }

        /// <summary>
        /// The method always returns true.
        /// </summary>
        protected bool IsExpertEnabled()
        {
            return true;
        }

        /// <summary>
        /// The method always returns true.
        /// </summary>
        /// <returns></returns>
        protected bool IsDemo()
        {
            return true;
        }

        /// <summary>
        /// Returns TRUE if expert runs in the testing mode, otherwise returns FALSE. 
        /// </summary>
        /// <returns></returns>
        protected bool IsTesting()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns TRUE if expert runs in the strategy tester optimization mode, otherwise returns FALSE. 
        /// </summary>
        /// <returns></returns>
        protected bool IsOptimization()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The method always returns zero.
        /// </summary>
        /// <returns></returns>
        protected int GetLastError()
        {
            return 0;
        }

        /// <summary>
        /// Returns the amount of minutes determining the used period (chart timeframe).
        /// </summary>
        /// <returns></returns>
        protected int Period()
        {
            if (this.periodicity == BarPeriod.M1)
                return PERIOD_M1;

            if (this.periodicity == BarPeriod.M5)
                return PERIOD_M5;

            if (this.periodicity == BarPeriod.M15)
                return PERIOD_M15;

            if (this.periodicity == BarPeriod.M30)
                return PERIOD_M30;

            if (this.periodicity == BarPeriod.H1)
                return PERIOD_H1;

            if (this.periodicity == BarPeriod.H4)
                return PERIOD_H4;

            if (this.periodicity == BarPeriod.D1)
                return PERIOD_D1;

            if (this.periodicity == BarPeriod.W1)
                return PERIOD_W1;

            if (this.periodicity == BarPeriod.MN1)
                return PERIOD_MN1;

            var message = string.Format("Unsupported periodicity = {0}", this.periodicity);
            throw new Exception(message);
        }

        /// <summary>
        /// Returns TRUE if a thread for trading is occupied by another expert advisor, otherwise returns FALSE.
        /// See also IsTradeAllowed(). 
        /// </summary>
        /// <returns></returns>
        protected bool IsTradeContextBusy()
        {
            if (this.currentSnapshot == null)
                return true;

            return !this.currentSnapshot.IsTradeLoggedOn;
        }

        /// <summary>
        /// Returns the code of the uninitialization reason for the experts, custom indicators, and scripts. The returned values can be ones of Uninitialize reason codes. This function can also be called in function init() to analyze the reasons for deinitialization of the previour launch. 
        /// </summary>
        /// <returns></returns>
        protected int UninitializeReason()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns true if the expert is allowed to trade, otherwise returns false.
        /// </summary>
        /// <returns></returns>
        protected bool IsTradeAllowed()
        {
            return true;
        }

        /// <summary>
        /// The method returns status of trade and feed connections.
        /// </summary>
        /// <returns>true, if the feed and trade are connected, otherwise false</returns>
        protected bool IsConnected()
        {
            var result = false;

            var snapshot = this.currentSnapshot;
            if (snapshot != null)
                result = snapshot.IsFeedLoggedOn && snapshot.IsTradeLoggedOn;

            return result;
        }

        /// <summary>
        /// Returns true if the program (an expert or a script) has been commanded to stop its operation, otherwise returns false.
        /// </summary>
        /// <returns></returns>
        protected bool IsStopped()
        {
            return this.isStopped;
        }

        #endregion

        #region File Functions

        /// <summary>
        /// Closes file previously opened by the FileOpen() function. 
        /// </summary>
        /// <param name="handle"></param>
        protected void FileClose(int handle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes specified file name. To get the detailed error information, call GetLastError().
        /// Files can only be deleted if they are in the terminal_dir\experts\files directory (terminal_directory\tester\files, in case of testing) or its subdirectories. 
        /// </summary>
        /// <param name="filename"></param>
        protected void FileDelete(string filename)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Flushes all data stored in the file buffer to the disk.
        /// Notes: The FileFlush() function must be called between operations of file reading and writing in the file.
        /// At file closing, the data are flushed to the disk automatically, so there is no need to call the FileFlush() function before calling of the FileClose() function. 
        /// </summary>
        /// <param name="handle"></param>
        protected void FileFlush(int handle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns logical true if file pointer is at the end of the file, otherwise returns false. To get the detailed error information, call GetLastError() function. If the file end is reached during reading, the GetLastError() function will return error ERR_END_OF_FILE (4099). 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        protected bool FileIsEnding(int handle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// For CSV file returns logical true if file pointer is at the end of the line, otherwise returns false. To get the detailed error information, call GetLastError() function. 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        protected bool FileIsLineEnding(int handle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens file for input and/or output. Returns a file handle for the opened file or -1 (if the function fails). To get the detailed error information, call GetLastError() function.
        /// Notes: Files can only be opened in the terminal_directory\experts\files folder (terminal_directory\tester\files if for expert testing) or in its subfolders.
        /// FILE_BIN and FILE_CSV modes cannot be used simultaneously.
        /// If FILE_WRITE does not combine with FILE_READ, a zero-length file will be opened. If even the file containd some data, they will be deleted. If there is a need to add data to an existing file, it must be opened using combination of FILE_READ | FILE_WRITE.
        /// If FILE_READ does not combine with FILE_WRITE, the file will be opened only if it already exists. If the file does not exist, it can be created using the FILE_WRITE mode.
        /// No more than 32 files can be opened within an executable module simultaneously. Handles of files opened in the same module cannot be passed to other modules (libraries). 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="mode"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        protected int FileOpen(string filename, int mode, int delimiter = ';')
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens file in the current history directory (terminal_directory\history\server_name) or in its subfolders. Returns the file handle for the opened file. If the function fails, the returned value is -1. To get the detailed error information, call the GetLastError() function.
        /// Notes: Client terminal can connect to servers of different brokerage companies. History data (HST files) for each brokerage company are stored in the corresponding subfolder of the terminal_directory\history folder.
        /// The function can be useful to form own history data for a non-standard symbol and/or period. The file formed in the history folder can be opened offline, not data pumping is needed to chart it. 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="mode"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        protected int FileOpenHistory(string filename, int mode, int delimiter = ';')
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the specified amount of elements from the binary file into array. Before reading, make sure that the array is large enough. Returns the amount of actually read elements.
        /// To get the detailed error information, call the GetLastError() function. 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        protected int FileReadArray(int handle, ref object[] array, int start, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sizeof(char)
        /// </summary>
        protected const int CHAR_VALUE = 1;

        /// <summary>
        /// sizeof(shart)
        /// </summary>
        protected const int SHORT_VALUE = 2;

        /// <summary>
        /// sizeof(int32)
        /// </summary>
        protected const int LONG_VALUE = 4;

        /// <summary>
        /// sizeof(double)
        /// </summary>
        protected const int DOUBLE_VALUE = 8;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        protected double FileReadDouble(int handle, int size = DOUBLE_VALUE)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        protected int FileReadInteger(int handle, int size = DOUBLE_VALUE)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read the number from the current file position before the delimiter. Only for CSV files.
        /// To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        protected double FileReadNumber(int handle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function reads the string from the current file position. Applies to both CSV and binary files. For text files, the string will be read before the delimiter. For binary file, the given count of characters will be read to the string. To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected string FileReadString(int handle, int length = 0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function moves the file pointer to a new position that is an offset, in bytes, from the beginning, the end or the current file position. The next reading or writing are made at a new position.
        /// If file pointer has been moved successfully, the function returns TRUE, otherwise, it returns FALSE. To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        protected bool FileSeek(int handle, int offset, int origin)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function returns file size in bytes. To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        protected int FileSize(int handle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        protected int FileTell(int handle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function writes a double value with floating point to a binary file. If the format is specified as FLOAT_VALUE, the value will be written as a 4-bytes floating point number (of the float type), otherwise, it will be written in the 8-bytes floating point format (of the double type).
        /// Returns the actually written bytes count or a negative value if an error occurs.
        /// To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        protected int FileWriteDouble(int handle, double value, int size = DOUBLE_VALUE)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function writes the integer value to a binary file. If the size is SHORT_VALUE, the value will be written as a 2-byte integer (the short type), if the size is CHAR_VALUE, the value will be written as a 1-byte integer (the char type), and if the size is LONG_VALUE, the value will be written as a 4-byte integer (the long int type).
        /// Returns the actually written bytes count or a negative value if an error occurs.
        /// To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        protected int FileWriteInteger(int handle, int value, int size = LONG_VALUE)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// The function writes the string to a binary file from the current file position.
        /// Returns the actually written bytes count or a negative value if an error occurs.
        /// To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        int FileWriteString(int handle, string value, int length)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        protected int FileWrite(int handle, params object[] items)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Global Variables of the Terminal

        /// <summary>
        /// Returns TRUE if the global variable exists, otherwise, returns FALSE. To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected bool GlobalVariableCheck(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the global variable. If the function succeeds, the returned value will be TRUE, otherwise, it will be FALSE. To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected bool GlobalVariableDel(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the value of an existing global variable or 0 if an error occurs. To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected double GlobalVariableGet(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function returns the name of a global variable by its index in the list of global variables. To get the detailed error information, one has to call the GetLastError(). 
        /// </summary>
        /// <param name="index">Index in the list of global variables. It must exceed or be equal to 0 and be less than GlobalVariablesTotal().</param>
        /// <returns></returns>
        protected string GlobalVariableName(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets a new value of the global variable. If it does not exist, the system creates a new gloabl variable. If the function succeeds, the returned value will be the last access time. Otherwise, the returned value will be 0. To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected datetime GlobalVariableSet(string name, double value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the new value of the existing global variable if the current value equals to the third parameter check_value. If there is no global variable, the function will generate error ERR_GLOBAL_VARIABLE_NOT_FOUND (4058) and return FALSE. When successfully executed, the function returns TRUE, otherwise, it returns FALSE. To get the detailed error information, one has to call the GetLastError() function.
        /// If the current value of the global variable differs from the check_value, the function will return FALSE.
        /// The function provides atomic access to the global variable, this is why it can be used for providing of a semaphore at interaction of several experts working simultaneously within one client terminal. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="check_value"></param>
        /// <returns></returns>
        protected bool GlobalVariableSetOnCondition(string name, double value, double check_value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes global variables. If the name prefix is not specified, all global variables will be deleted. Otherwise, only those variables will be deleted, the names of which begin with the specified prefix. The function returns the count of deleted variables. 
        /// </summary>
        /// <param name="prefix_name"></param>
        /// <returns></returns>
        protected int GlobalVariablesDeleteAll(string prefix_name = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function returns the total count of global variables. 
        /// </summary>
        /// <returns></returns>
        protected int GlobalVariablesTotal()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Object Functions

        /// <summary>
        /// Count all objects with any types
        /// </summary>
        protected const int EMPTY = -1;

        /// <summary>
        /// Changes the object description. For objects of OBJ_TEXT and OBJ_LABEL, this description is shown as a text line in the chart. If the function succeeds, the returned value will be TRUE. Otherwise, it is FALSE. To get the detailed error information, one has to call the GetLastError() function.
        /// Parameters of font_size, font_name and text_color are used for objects of OBJ_TEXT and OBJ_LABEL only. For objects of other types, these parameters are ignored
        /// </summary>
        /// <param name="name">Object name.</param>
        /// <param name="text">A text describing the object.</param>
        /// <param name="font_size">Font size in points.</param>
        /// <param name="font">Font name.</param>
        /// <param name="text_color">Text color.</param>
        /// <returns></returns>
        protected bool ObjectSetText(string name, string text, int font_size, string font = null, color text_color = new color())
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns total amount of objects of the specified type in the chart. 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected int ObjectsTotal(int type = EMPTY)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes object having the specified name. If the function succeeds, the returned value will be TRUE. Otherwise, it will be FALSE.
        /// To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected bool ObjectDelete(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Search for an object having the specified name. The function returns index of the windows that contains the object to be found. If it fails, the returned value will be -1. To get the detailed error information, one has to call the GetLastError() function. The chart sub-windows (if there are sub-windows with indicators in the chart) are numbered starting from 1. The chart main window always exists and has the 0 index. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected int ObjectFind(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function moves an object coordinate in the chart. Objects can have from one to three coordinates depending on their types. If the function succeeds, the returned value will be TRUE. Otherwise, it will be FALSE. To get the detailed error information, one has to call the GetLastError() function. The object coordinates are numbered starting from 0. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="point"></param>
        /// <param name="time1"></param>
        /// <param name="price1"></param>
        /// <returns></returns>
        protected bool ObjectMove(string name, int point, datetime time1, double price1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function returns the object name by its index in the objects list. To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="point"></param>
        /// <param name="time1"></param>
        /// <param name="price1"></param>
        /// <returns></returns>
        protected string ObjectName(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function returns the object type value. To get the detailed error information, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected int ObjectType(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes all objects of the specified type and in the specified sub-window of the chart. The function returns the count of removed objects. To get the detailed error information, one has to call the GetLastError() function.
        /// Notes: The chart sub-windows (if there are sub-windows with indicators in the chart) are numbered starting from 1. The chart main window always exists and has the 0 index. If the window index is missing or it has the value of -1, the objects will be removed from the entire chart.
        /// If the type value equals to -1 or this parameter is missing, all objects will be removed from the specified sub-window. 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected int ObjectsDeleteAll(int window = 0, int type = 0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The MessageBox function creates, displays, and operates message box. The message box contains an application-defined message and header, as well as a random combination of predefined icons and push buttons. If the function succeeds, the returned value is one of the MessageBox return code values.
        /// The function cannot be called from custom indicators since they are executed within interface thread and may not decelerate it. 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        protected int MessageBox(string text = "", string caption = "", int flags = 0)
        {
            return (int)WinFormsMessageBox.Show(text, caption, (MessageBoxButtons)flags);
        }

        /// <summary>
        /// Creation of an object with the specified name, type and initial coordinates in the specified window. Count of coordinates related to the object can be from 1 to 3 depending on the object type. If the function succeeds, the returned value will be TRUE. Otherwise, it will be FALSE. To get the detailed error information, one has to call the GetLastError() function. Objects of the OBJ_LABEL type ignore the coordinates. Use the function of ObjectSet() to set up the OBJPROP_XDISTANCE and OBJPROP_YDISTANCE properties.
        /// Notes: The chart sub-windows (if there are sub-windows with indicators in the chart) are numbered starting from 1. The chart main window always exists and has the 0 index.
        /// Coordinates must be passed in pairs: time and price. For example, the OBJ_VLINE object needs only time, but price (any value) must be passed, as well. 
        /// </summary>
        /// <param name="name">Object unique name.</param>
        /// <param name="type">Object type. It can be any of the Object type enumeration values.</param>
        /// <param name="window">Index of the window where the object will be added. Window index must exceed or equal to 0 and be less than WindowsTotal().</param>
        /// <param name="time1">Time part of the first point.</param>
        /// <param name="price1">Price part of the first point.</param>
        /// <param name="time2">Time part of the second point.</param>
        /// <param name="price2">Price part of the second point.</param>
        /// <param name="time3">Time part of the third point.</param>
        /// <param name="price3">Price part of the third point.</param>
        /// <returns></returns>
        protected bool ObjectCreate(string name, int type, int window, datetime time1, double price1,
                                    datetime time2 = new datetime(),
                                    double price2 = 0, datetime time3 = new datetime(), double price3 = 0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function returns the value of the specified object property. To check errors, one has to call the GetLastError() function. 
        /// See also ObjectSet() function. 
        /// </summary>
        /// <param name="name">Object name.</param>
        /// <param name="index">Object property index. It can be any of the Object properties enumeration values.</param>
        /// <returns></returns>
        protected double ObjectGet(string name, int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Changes the value of the specified object property. If the function succeeds, the returned value will be TRUE. Otherwise, it will be FALSE. To get the detailed error information, one has to call the GetLastError() function. 
        /// See also ObjectGet() function. 
        /// </summary>
        /// <param name="name">Object name.</param>
        /// <param name="index">Object value index. It can be any of Object properties enumeration values.</param>
        /// <param name="value">New value of the given property.</param>
        /// <returns></returns>
        protected bool ObjectSet(string name, int index, double value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}