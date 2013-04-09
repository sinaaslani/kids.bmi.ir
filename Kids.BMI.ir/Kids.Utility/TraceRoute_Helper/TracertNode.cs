using System.Net;
using System.Net.NetworkInformation;

namespace Kids.Utility.TraceRoute_Helper
{
    /// <summary>
    /// Contains data about a node encountered using Tracert
    /// </summary>
    public class TracertNode
    {
        private readonly IPAddress _address;

        private readonly long _roundTripTime;

        private readonly IPStatus _status;

        internal TracertNode(IPAddress address, long roundTripTime, IPStatus status)
        {
            _address = address;
            _roundTripTime = roundTripTime;
            _status = status;
        }

        /// <summary>
        /// The IPAddress of the node
        /// </summary>
        public IPAddress Address
        {
            get { return _address; }
        }

        /// <summary>
        /// The time taken to go to the node and come back to the originating node in milliseconds.
        /// </summary>
        public long RoundTripTime
        {
            get { return _roundTripTime; }
        }

        /// <summary>
        /// The IPStatus of request send to the node
        /// </summary>
        public IPStatus Status
        {
            get { return _status; }
        }
    }
}