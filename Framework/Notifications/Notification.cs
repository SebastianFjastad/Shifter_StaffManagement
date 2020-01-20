using System.Globalization;
using System.Runtime.Serialization;

namespace Framework.Notifications
{
    /// <summary>
    /// A notification or message.
    /// </summary>
    [DataContract]
    public class Notification
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public Notification(string text)
        {
            Guard.ArgumentNotEmpty(text, "text");
            Text = text;
            Severity = NotificationSeverity.Information;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="severity">The severity.</param>
        public Notification(string text, NotificationSeverity severity)
        {
            Guard.ArgumentNotEmpty(text, "text");
            Text = text;
            Severity = severity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="tag">The Tag.</param>
        public Notification(string text, NotificationSeverity severity, object tag, string hint)
        {
            Guard.ArgumentNotEmpty(text, "text");
            Text = text;
            Severity = severity;
            Tag = tag;
            Hint = hint;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// The <see cref="NotificationSeverity"/> of the message
        /// </summary>
        [DataMember]
        public NotificationSeverity Severity { get; set; }

        /// <summary>
        /// The <see cref="Notification"/> text.
        /// </summary>
        [DataMember]
        public string Text { get; set; }

        /// <summary>
        /// The <see cref="Notification"/> code.
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// Gets and sets the message hint
        /// </summary>
        [DataMember]
        public string Hint { get; set; }

        /// <summary>
        /// Gets and sets grouping data
        /// </summary>
        [DataMember]
        public string Grouping { get; set; }

        /// <summary>
        /// Used to apply additional taggable data to a notification
        /// </summary>
        [DataMember]
        public object Tag { get; set; }

        #endregion Properties

        #region Virtual Methods

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0} : {1}", Severity, Text);
        }

        #endregion Virtual Methods

        #region Static Methods

        /// <summary>
        /// Creates a new instance of <see cref="Framework.Notifications.Notification"/>.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="notificationSeverity">The notification severity.</param>
        /// <returns></returns>
        public static Notification Create(string message, NotificationSeverity notificationSeverity)
        {
            return new Notification(message, notificationSeverity);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Framework.Notifications.Notification"/>.
        /// </summary>
        public static Notification Create(string code, string message, NotificationSeverity notificationSeverity)
        {
            return new Notification(message, notificationSeverity) { Code = code };
        }

        #endregion Static Methods
    }
}