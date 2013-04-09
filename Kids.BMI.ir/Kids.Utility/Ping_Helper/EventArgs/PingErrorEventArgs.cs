using System;
using Kids.Utility.Ping_Helper.Enums;

namespace Kids.Utility.Ping_Helper.EventArgs
{
    public delegate void PingErrorEventHandler(object sender, PingErrorEventArgs e);

    public class PingErrorEventArgs
    {
        #region Properties

        private DateTime errorDateTime;
        private PingResponseType errorType;
        private string message;

        public PingResponseType ErrorType
        {
            get { return errorType; }
        }

        public DateTime ErrorDateTime
        {
            get { return errorDateTime; }
        }

        public string Message
        {
            get { return message; }
        }

        #endregion

        public PingErrorEventArgs(PingResponseType errorType, string message, DateTime errorDateTime)
        {
            this.errorType = errorType;
            this.message = message;
            this.errorDateTime = errorDateTime;
        }
    }
}