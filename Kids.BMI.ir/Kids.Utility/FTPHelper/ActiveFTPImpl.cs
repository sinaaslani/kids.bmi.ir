using System;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace Kids.Utility.FTPHelper
{

    public class ActiveFTPImpl : IFTPClient
    {
        #region IFTPClient Members

        string m_host;
        bool m_ssl;
        NetworkCredential m_creds;
        string m_dir;

        public void ChangeDirectory(string sDir)
        {
            if (string.IsNullOrEmpty(sDir))
                m_dir = "/";
            else if (sDir[0] == '/')
                m_dir = sDir;
            else
            {
                if (m_dir[m_dir.Length - 1] != '/')
                    m_dir += "/";
                m_dir += sDir;
            }
        }

        public void CreateDirectory(string sDirectory)
        {
            FtpWebRequest reqFTP = pv_GetFTPRequest("ftp://" + m_host + "/" + m_dir + "/" + sDirectory);

            reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;

            try
            {
                FtpWebResponse response = reqFTP.GetResponse() as FtpWebResponse;
                StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);
                string Datastring = sr.ReadToEnd();
                response.Close();
            }
            catch// (WebException ex)
            { }


        }

        public void DeleteFile(string sName, bool fIgnoreNotExist)
        {
            FtpWebRequest reqFTP = pv_GetFTPRequest("ftp://" + m_host + "/" + m_dir + "/" + sName);

            reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

            try
            {
                FtpWebResponse response = reqFTP.GetResponse() as FtpWebResponse;
                StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);
                string Datastring = sr.ReadToEnd();
                response.Close();
            }
            catch (WebException )
            { }

        }

        public void Dispose()
        {
            return;//Nothing to dispose
        }

        public string[] GetDirectoryList()
        {
            throw new NotImplementedException();
        }

        public void GetFile(string NetworkPath, string LocalPath)
        {
            FtpWebRequest reqFTP = pv_GetFTPRequest("ftp://" + m_host + "/" + m_dir + "/" + NetworkPath);

            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = reqFTP.GetResponse() as FtpWebResponse;

            Stream dataStream = response.GetResponseStream();

            FileInfo targetFI = new FileInfo(LocalPath);


            using (FileStream fs = targetFI.OpenWrite())
            {
                byte[] abBuffer = new byte[4096];
                int iBytes = dataStream.Read(abBuffer, 0, abBuffer.Length);
                while (iBytes != 0)
                {
                    fs.Write(abBuffer, 0, iBytes);
                    iBytes = dataStream.Read(abBuffer, 0, abBuffer.Length);
                }
            }

        }

        public IEnumerable<FTPfileInfo> GetFullDirectoryList()
        {
            FtpWebRequest reqFTP = pv_GetFTPRequest("ftp://" + m_host + m_dir);

            reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            FtpWebResponse response = reqFTP.GetResponse() as FtpWebResponse;
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);
            string Datastring = sr.ReadToEnd();
            response.Close();


            Datastring = Datastring.Replace("\r\n", "\r").TrimEnd('\r');
            //split the string into a list
            return new FTPdirectory(Datastring, m_dir);

        }


        public System.IO.Stream GetSendFileStream(string sName)
        {
            throw new NotImplementedException();
        }

        public void SendFile(string LocalFileName, string NetworkFileName)
        {
            FileInfo fi = new FileInfo(LocalFileName);
            byte[] LocalFileContent = File.ReadAllBytes(LocalFileName);

            FtpWebRequest reqFTP = pv_GetFTPRequest("ftp://" + m_host + "/" + m_dir + "/" + NetworkFileName);

            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.ContentLength = fi.Length;


            using (Stream reqStr = reqFTP.GetRequestStream())
            {
                reqStr.Write(LocalFileContent, 0, (int)fi.Length);
            }

        }

        public bool SendFileAsByte()
        {
            return true;
        }

        public void Logon(string sUser, string sPwd)
        {
            m_creds = new NetworkCredential(sUser, sPwd);
        }

        public void Open(string sHost, bool bEnableSSL)
        {
            m_host = sHost;
            m_ssl = bEnableSSL;
        }

        public void Rename(string sFrom, string sTo)
        {
            throw new NotImplementedException();
        }

        //public void SendFile(string sName, System.IO.Stream stream)
        //{
        //    byte[] fileByte = new byte[stream.Length];
        //    stream.Read(fileByte, 0, (int)stream.Length);
        //    SendFile(sName, fileByte);
        //}

        public void SetControlClear()
        {
            throw new NotImplementedException();
        }

        public bool SSLEnabled
        {
            get { throw new NotImplementedException(); }
        }

        private FtpWebRequest pv_GetFTPRequest(string url)
        {
            FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
            reqFTP.UseBinary = true;
            reqFTP.UsePassive = false;
            reqFTP.Credentials = m_creds;
            reqFTP.KeepAlive = false;
            return reqFTP;
        }

        #endregion
    }
}
