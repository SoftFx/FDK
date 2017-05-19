namespace SoftFX.Extended.Events
{
    using System;

    /// <summary>
    /// Represents the method that will handle logon event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Logon information.</param>
    public delegate void LogonHandler(object sender, LogonEventArgs e);

    /// <summary>
    /// Represents the method that will handle logout event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Logout information.</param>
    public delegate void LogoutHandler(object sender, LogoutEventArgs e);

    /// <summary>
    /// Represents the method that will handle two factor auth event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Two factor auth information.</param>
    public delegate void TwoFactorAuthHandler(object sender, TwoFactorAuthEventArgs e);

    /// <summary>
    /// Represents the method that will handle symbol subscribed event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Subscribed nformation.</param>
    public delegate void SubscribedHandler(object sender, SubscribedEventArgs e);

    /// <summary>
    /// Represents the method that will handle symbol unsubscribed event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Unsubscribed nformation.</param>
    public delegate void UnsubscribedHandler(object sender, UnsubscribedEventArgs e);

    /// <summary>
    /// Represents the method that will handle new tick event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Tick information.</param>
    public delegate void TickHandler(object sender, TickEventArgs e);

    /// <summary>
    /// Represents the method that will handle: successful login; session status is changed on server (opened to closed, closed to opened).
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Contains session information.</param>
    public delegate void SessionInfoHandler(object sender, SessionInfoEventArgs e);

    /// <summary>
    /// Represents the method that will handle: cache modification.
    /// </summary>
    /// <param name="sender">The source of the event; can be data feed or data trade instance.</param>
    /// <param name="e">Contains cache modification information.</param>
    public delegate void CacheHandler(object sender, CacheEventArgs e);

    /// <summary>
    /// Represents the method that will handle: account settings (Leverage, Currency, AccountingType) are changed.
    /// </summary>
    /// <param name="sender">The source of the event; can be data trade instance.</param>
    /// <param name="e">Contains account information.</param>
    public delegate void AccountInfoHandler(object sender, AccountInfoEventArgs e);

    /// <summary>
    /// Represents the method that will handle: any execution report.
    /// </summary>
    /// <param name="sender">The source of the event; can be data trade instance.</param>
    /// <param name="e">Contains trade report information.</param>
    public delegate void ExecutionReportHandler(object sender, ExecutionReportEventArgs e);

    /// <summary>
    /// Represents the method that will handle: initialization of currency information.
    /// </summary>
    /// <param name="sender">The source of the event; can be data trade instance.</param>
    /// <param name="e">Contains symbols information.</param>
    public delegate void CurrencyInfoHandler(object sender, CurrencyInfoEventArgs e);

    /// <summary>
    /// Represents the method that will handle: initialization of symbols information.
    /// </summary>
    /// <param name="sender">The source of the event; can be data trade instance.</param>
    /// <param name="e">Contains symbols information.</param>
    public delegate void SymbolInfoHandler(object sender, SymbolInfoEventArgs e);

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void TradeTransactionReportHandler(object sender, TradeTransactionReportEventArgs e);

    /// <summary>
    /// Represents the method that will handle new ticks event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Tick information.</param>
    public delegate void TicksHandler(object sender, TickEventArgs e);

    /// <summary>
    /// Represents the method that will handle: any position report update.
    /// </summary>
    /// <param name="sender">The source of the event; can be data trade instance.</param>
    /// <param name="e">Contains trade position report information.</param>
    public delegate void PositionReportHandler(object sender, PositionReportEventArgs e);

    /// <summary>
    /// Represents the method that will handle: margin call, margin call revocation, stop out.
    /// </summary>
    /// <param name="sender">The source of event; can be DataTrade instance.</param>
    /// <param name="e">Contains notification information.</param>
    public delegate void NotifyHandler(object sender, NotificationEventArgs e);

    /// <summary>
    /// Represents the method that will handle: margin call, margin call revocation, stop out.
    /// </summary>
    /// <param name="sender">The source of event; can be DataTrade instance.</param>
    /// <param name="e">Contains notification information.</param>
    public delegate void NotifyHandler<T>(object sender, NotificationEventArgs<T> e);
}