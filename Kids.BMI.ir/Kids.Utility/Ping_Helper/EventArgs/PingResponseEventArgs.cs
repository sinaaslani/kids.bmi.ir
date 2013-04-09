using System.Net;
using Kids.Utility.Ping_Helper.Enums;

namespace Kids.Utility.Ping_Helper.EventArgs
{
    public delegate void PingResponseEventHandler(object sender, PingResponseEventArgs e);

    public class PingResponseEventArgs : System.EventArgs
    {
        #region Properties

        private int byteCount;
        private bool cancel;
        private int responseTime;
        private PingResponseType result;
        private IPAddress serverAddress;

        public IPAddress ServerAddress
        {
            get { return serverAddress; }
        }

        public PingResponseType Result
        {
            get { return result; }
        }

        public int ResponseTime
        {
            get { return responseTime; }
        }

        public int ByteCount
        {
            get { return byteCount; }
        }

        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }

        #endregion

        public PingResponseEventArgs(IPAddress serverAddress, PingResponseType result, int responseTime, int byteCount,
                                     bool cancel)
        {
            this.serverAddress = serverAddress;
            this.result = result;
            this.responseTime = responseTime;
            this.byteCount = byteCount;
            this.cancel = cancel;
        }
    }
}