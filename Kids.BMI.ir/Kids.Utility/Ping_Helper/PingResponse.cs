using System.Net;
using Kids.Utility.Ping_Helper.Enums;

namespace Kids.Utility.Ping_Helper
{
    public class PingResponse
    {
        #region Properties

        private int averageTime = Constants.InvalidInt;
        private IPEndPoint clientEndPoint;
        private string errorMessage;
        private int maximumTime = Constants.InvalidInt;
        private int minimumTime = Constants.InvalidInt;
        private int packetsReceived = Constants.InvalidInt;
        private int packetsSent = Constants.InvalidInt;
        private PingResponseType pingResult;
        private int[] responseTimes;
        private IPEndPoint serverEndPoint;

        public IPEndPoint ServerEndPoint
        {
            get { return serverEndPoint; }
            set { serverEndPoint = value; }
        }

        public IPEndPoint ClientEndPoint
        {
            get { return clientEndPoint; }
            set { clientEndPoint = value; }
        }

        public PingResponseType PingResult
        {
            get { return pingResult; }
            set { pingResult = value; }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public int PacketsSent
        {
            get { return packetsSent; }
            set { packetsSent = value; }
        }

        public int PacketsReceived
        {
            get { return packetsReceived; }
            set { packetsReceived = value; }
        }

        public int Lost
        {
            get { return packetsSent - packetsReceived; }
        }

        public int MinimumTime
        {
            get
            {
                //Check to see if the minimum time has already been calculated
                if (minimumTime == Constants.InvalidInt)
                {
                    if (responseTimes == null || responseTimes.Length == 0)
                    {
                        minimumTime = -1;
                    }
                    else
                    {
                        minimumTime = responseTimes[0];
                        for (int i = 1; i < responseTimes.Length; i++)
                        {
                            if (responseTimes[i] != Constants.InvalidInt && responseTimes[i] < minimumTime)
                                minimumTime = responseTimes[i];
                        }

                        //Handle all ping responses failing (thus giving Contants.InvalidInt times)
                        if (minimumTime == Constants.InvalidInt)
                            minimumTime = -1;
                    }
                }

                if (minimumTime == -1)
                    return Constants.InvalidInt;
                else
                    return minimumTime;
            }
        }

        public int MaximumTime
        {
            get
            {
                //Check to see if the maximum time has already been calculated
                if (maximumTime == Constants.InvalidInt)
                {
                    if (responseTimes == null || responseTimes.Length == 0)
                    {
                        minimumTime = -1;
                    }
                    else
                    {
                        maximumTime = responseTimes[0];
                        for (int i = 1; i < responseTimes.Length; i++)
                        {
                            if (responseTimes[i] != Constants.InvalidInt && responseTimes[i] > maximumTime)
                                maximumTime = responseTimes[i];
                        }

                        //Handle all ping responses failing (thus giving Contants.InvalidInt times)
                        if (maximumTime == Constants.InvalidInt)
                            maximumTime = -1;
                    }
                }

                if (maximumTime == -1)
                    return Constants.InvalidInt;
                else
                    return maximumTime;
            }
        }

        public int AverageTime
        {
            get
            {
                //Check to see if the average time has already been calculated
                if (averageTime == Constants.InvalidInt)
                {
                    averageTime = 0;
                    int validPings = 0;
                    for (int i = 0; i < responseTimes.Length; i++)
                    {
                        if (responseTimes[i] != Constants.InvalidInt)
                        {
                            averageTime += responseTimes[i];
                            validPings++;
                        }
                    }

                    //Handle all ping responses failing
                    if (validPings == 0)
                        averageTime = -1;
                    else
                        averageTime = (int) (averageTime/validPings);
                }

                if (averageTime == -1)
                    return Constants.InvalidInt;
                else
                    return averageTime;
            }
        }

        public int[] ResponseTimes
        {
            get { return responseTimes; }
            set { responseTimes = value; }
        }

        #endregion

        public PingResponse()
        {
        }

        public PingResponse(int expectedResponses)
        {
            responseTimes = new int[expectedResponses];
        }

        public PingResponse(int packetsSent, int packetsReceived, int[] responseTimes)
        {
            this.packetsSent = packetsSent;
            this.packetsReceived = packetsReceived;
            this.responseTimes = responseTimes;
        }
    }
}