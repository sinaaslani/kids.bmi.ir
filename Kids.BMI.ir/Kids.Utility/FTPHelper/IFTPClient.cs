using System;
using System.Collections.Generic;
using System.IO;

namespace Kids.Utility.FTPHelper
{
    public interface IFTPClient : IDisposable
    {
        void ChangeDirectory(string sDir);
        void CreateDirectory(string sDirectory);
        void DeleteFile(string Name, bool fIgnoreNotExist);
        string[] GetDirectoryList();
        void GetFile(string NetworkPath, string LocalPath);
        IEnumerable<FTPfileInfo> GetFullDirectoryList();
        Stream GetSendFileStream(string sName);
        void Logon(string sUser, string sPwd);
        void Open(string sHost, bool bEnableSSL);
        void Rename(string sFrom, string sTo);
        void SendFile(string LocalFileName, string NetworkFileName);
        void SetControlClear();
        bool SSLEnabled { get; }
        //bool SendFile(string sName, byte[] file);
        bool SendFileAsByte();
    }
}
