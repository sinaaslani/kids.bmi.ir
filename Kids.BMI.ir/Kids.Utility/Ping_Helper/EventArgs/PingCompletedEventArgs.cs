using System;

namespace Kids.Utility.Ping_Helper.EventArgs
{
    public delegate void PingCompletedEventHandler(object sender, PingCompletedEventArgs e);

    public class PingCompletedEventArgs
    {
        #region Properties

        private DateTime endDateTime;
        private PingResponse pingResponse;

        public PingResponse PingResponse
        {
            get { return pingResponse; }
        }

        public DateTime EndDateTime
        {
            get { return endDateTime; }
        }

        #endregion

        public PingCompletedEventArgs(PingResponse pingResponse, DateTime endDateTime)
        {
            this.pingResponse = pingResponse;
            this.endDateTime = endDateTime;
        }
    }
}