using System;
using System.Net;

namespace Kids.Utility.Ping_Helper.EventArgs
{
    public delegate void PingStartedEventHandler(object sender, PingStartedEventArgs e);

    public class PingStartedEventArgs
    {
        private int byteCount;
        private IPEndPoint serverEndPoint;

        private DateTime startDateTime;

        public PingStartedEventArgs(IPEndPoint serverEndPoint, int byteCount, DateTime startDateTime)
        {
            this.serverEndPoint = serverEndPoint;
            this.startDateTime = startDateTime;
            this.byteCount = byteCount;
        }

        public IPEndPoint ServerEndPoint
        {
            get { return serverEndPoint; }
        }

        public DateTime StartDateTime
        {
            get { return startDateTime; }
        }

        public int ByteCount
        {
            get { return byteCount; }
        }
    }
}