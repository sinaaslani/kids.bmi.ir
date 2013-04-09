namespace Kids.Utility.Ping_Helper.Enums
{
    public enum PingResponseType
    {
        Ok = 0,
        CouldNotResolveHost,
        RequestTimedOut,
        ConnectionError,
        InternalError,
        Canceled
    }
}