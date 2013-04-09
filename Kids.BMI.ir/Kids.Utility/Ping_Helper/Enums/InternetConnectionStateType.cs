using System;

namespace Kids.Utility.Ping_Helper.Enums
{
    [Flags]
    public enum InternetConnectionStatesType
    {
        ModemConnection = 0x1,
        LANConnection = 0x2,
        ProxyConnection = 0x4,
        RASInstalled = 0x10,
        Offline = 0x20,
        ConnectionConfigured = 0x40
    }
}