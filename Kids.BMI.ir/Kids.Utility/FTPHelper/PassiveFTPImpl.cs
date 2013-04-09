using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Kids.Utility.FTPHelper
{

    #region "FTP file info class"
    /// <summary>
    /// Represents a file or directory entry from an FTP listing
    /// </summary>
    /// <remarks>
    /// This class is used to parse the results from a detailed
    /// directory list from FTP. It supports most formats of
    /// </remarks>
    public class FTPfileInfo
    {

        //Stores extended info about FTP file

        #region "Properties"
        public string FullName
        {
            get
            {
                return Path.EndsWith("/") ? Path + Filename : string.Format("{0}/{1}", Path, Filename);
            }
        }
        public string Filename
        {
            get
            {
                return _filename;
            }
        }
        public string Path
        {
            get
            {
                return _path;
            }
        }
        public DirectoryEntryTypes FileType
        {
            get
            {
                return _fileType;
            }
        }
        public long Size
        {
            get
            {
                return _size;
            }
        }
        public DateTime FileDateTime
        {
            get
            {
                return _fileDateTime;
            }
        }
        public string Permission
        {
            get
            {
                return _permission;
            }
        }
        public string Extension
        {
            get
            {
                int i = this.Filename.LastIndexOf(".");
                if (i >= 0 && i < (this.Filename.Length - 1))
                {
                    return this.Filename.Substring(i + 1);
                }
                else
                {
                    return "";
                }
            }
        }
        public string NameOnly
        {
            get
            {
                int i = this.Filename.LastIndexOf(".");
                if (i > 0)
                {
                    return this.Filename.Substring(0, i);
                }
                else
                {
                    return this.Filename;
                }
            }
        }
        private string _filename;
        private string _path;
        private DirectoryEntryTypes _fileType;
        private long _size;
        private DateTime _fileDateTime;
        private string _permission;

        #endregion

        /// <summary>
        /// Identifies entry as either File or Directory
        /// </summary>
        public enum DirectoryEntryTypes
        {
            File,
            Directory
        }

        /// <summary>
        /// Constructor taking a directory listing line and path
        /// </summary>
        /// <param name="line">The line returned from the detailed directory list</param>
        /// <param name="path">Path of the directory</param>
        /// <remarks></remarks>
        public FTPfileInfo(string line, string path)
        {
            //parse line
            Match m = GetMatchingRegex(line);
            if (m == null)
            {
                //failed
                throw (new ApplicationException("Unable to parse line: " + line));
            }
            else
            {
                _filename = m.Groups["name"].Value;
                _path = path;

                Int64.TryParse(m.Groups["size"].Value, out _size);
                //_size = System.Convert.ToInt32(m.Groups["size"].Value);

                _permission = m.Groups["permission"].Value;
                string _dir = m.Groups["dir"].Value;
                if (_dir != "" && _dir != "-")
                {
                    _fileType = DirectoryEntryTypes.Directory;
                }
                else
                {
                    _fileType = DirectoryEntryTypes.File;
                }

                try
                {
                    _fileDateTime = DateTime.ParseExact(m.Groups["timestamp"].Value, "dd-MM-yy  hh:mmtt", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    _fileDateTime = Convert.ToDateTime(null);
                }

            }
        }

        private Match GetMatchingRegex(string line)
        {
            Regex rx;
            Match m;
            for (int i = 0; i <= _ParseFormats.Length - 1; i++)
            {
                rx = new Regex(_ParseFormats[i]);
                m = rx.Match(line);
                if (m.Success)
                {
                    return m;
                }
            }
            return null;
        }

        #region "Regular expressions for parsing LIST results"
        /// <summary>
        /// List of REGEX formats for different FTP server listing formats
        /// </summary>
        /// <remarks>
        /// The first three are various UNIX/LINUX formats, fourth is for MS FTP
        /// in detailed mode and the last for MS FTP in 'DOS' mode.
        /// I wish VB.NET had support for Const arrays like C# but there you go
        /// </remarks>
        private static string[] _ParseFormats = new string[] { 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)", 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)", 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)", 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)", 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})(\\s+)(?<size>(\\d+))(\\s+)(?<ctbit>(\\w+\\s\\w+))(\\s+)(?<size2>(\\d+))\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{2}:\\d{2})\\s+(?<name>.+)", 
            "(?<timestamp>\\d{2}\\-\\d{2}\\-\\d{2}\\s+\\d{2}:\\d{2}[Aa|Pp][mM])\\s+(?<dir>\\<\\w+\\>){0,1}(?<size>\\d+){0,1}\\s+(?<name>.+)" };
        #endregion
    }
    #endregion

    #region "FTP Directory class"
    /// <summary>
    /// Stores a list of files and directories from an FTP result
    /// </summary>
    /// <remarks></remarks>
    public class FTPdirectory : List<FTPfileInfo>
    {


        public FTPdirectory()
        {
            //creates a blank directory listing
        }

        /// <summary>
        /// Constructor: create list from a (detailed) directory string
        /// </summary>
        /// <param name="dir">directory listing string</param>
        /// <param name="path"></param>
        /// <remarks></remarks>
        public FTPdirectory(string dir, string path)
        {
            foreach (string line in dir.Replace("\n", "").Split(System.Convert.ToChar('\r')))
            {
                //parse
                if (line != "")
                {
                    this.Add(new FTPfileInfo(line, path));
                }
            }
        }

        /// <summary>
        /// Filter out only files from directory listing
        /// </summary>
        /// <param name="ext">optional file extension filter</param>
        /// <returns>FTPdirectory listing</returns>
        public FTPdirectory GetFiles(string ext = "")
        {
            return this.GetFileOrDir(FTPfileInfo.DirectoryEntryTypes.File, ext);
        }

        /// <summary>
        /// Returns a list of only subdirectories
        /// </summary>
        /// <returns>FTPDirectory list</returns>
        /// <remarks></remarks>
        public FTPdirectory GetDirectories()
        {
            return this.GetFileOrDir(FTPfileInfo.DirectoryEntryTypes.Directory, "");
        }

        //internal: share use function for GetDirectories/Files
        private FTPdirectory GetFileOrDir(FTPfileInfo.DirectoryEntryTypes type, string ext)
        {
            FTPdirectory result = new FTPdirectory();
            foreach (FTPfileInfo fi in this)
            {
                if (fi.FileType == type)
                {
                    if (ext == "")
                    {
                        result.Add(fi);
                    }
                    else if (ext == fi.Extension)
                    {
                        result.Add(fi);
                    }
                }
            }
            return result;

        }

        public bool FileExists(string filename)
        {
            foreach (FTPfileInfo ftpfile in this)
            {
                if (ftpfile.Filename == filename)
                {
                    return true;
                }
            }
            return false;
        }

        private const char slash = '/';

        public static string GetParentDirectory(string dir)
        {
            string tmp = dir.TrimEnd(slash);
            int i = tmp.LastIndexOf(slash);
            if (i > 0)
            {
                return tmp.Substring(0, i - 1);
            }
            else
            {
                throw (new ApplicationException("No parent for root"));
            }
        }
    }
    #endregion
    public class PassiveFTPImpl : IFTPClient
    {
        private class SendFileStream : System.IO.Stream
        {
            public delegate void OnClose();

            private System.IO.Stream m_DataStream;
            private readonly OnClose m_onclose;
            private long m_lPos;

            public SendFileStream(System.IO.Stream DataStream, OnClose onclose)
            {
                m_DataStream = DataStream;
                m_onclose = onclose;
                m_lPos = 0;
            }

            public override bool CanRead
            {
                get { return false; }
            }

            public override bool CanSeek
            {
                get { return false; }
            }

            public override bool CanWrite
            {
                get { return true; }
            }

            public override void Flush()
            {
            }

            public override long Length
            {
                get { throw new NotImplementedException(); }
            }

            public override long Position
            {
                get
                {
                    return m_lPos;
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }

            public override long Seek(long offset, System.IO.SeekOrigin origin)
            {
                throw new NotImplementedException();
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                m_DataStream.Write(buffer, offset, count);
                m_lPos += count;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_DataStream != null)
                {
                    m_DataStream = null;
                    m_onclose();
                }

                base.Dispose(disposing);
            }
        }

        static readonly char[] ms_acEPSVSeps = new char[] { '|' };

        static private readonly System.Diagnostics.TraceSource m_TraceSource;

        private System.Net.Sockets.TcpClient m_ControlClient;
        private System.IO.Stream m_ControlStream;
        private System.Net.Security.SslStream m_SSLControlStream;
        private System.IO.StreamReader m_RawStreamReader;
        private System.IO.StreamWriter m_RawStreamWriter;
        private System.IO.StreamReader m_SSLStreamReader;
        private System.IO.StreamWriter m_SSLStreamWriter;

        private string m_sHost;
        private string m_sDataHost;
        private int m_iDataPort;
        private System.Net.Sockets.TcpClient m_DataClient;
        private System.IO.Stream m_DataStream;


        bool m_bSSLEnabled;
        private bool m_bSupportsEPSV;
        private System.IO.StreamReader m_StreamReader;
        private System.IO.StreamWriter m_StreamWriter;
        int? m_niLocalPortStart;
        int? m_niLocalPortEnd;
        private string m_dir = "";

        static PassiveFTPImpl()
        {
            m_TraceSource = new System.Diagnostics.TraceSource("Happen.Net.FTP.FTPClient");
        }

        public void UseLocalPortRange(int? niStartPort, int? niEndPort)
        {
            m_niLocalPortStart = niStartPort;
            m_niLocalPortEnd = niEndPort;
        }

        /// <summary>
        /// Opens the control session with the specified host.  If EnableSSL is set then the SSL
        /// session negotiation happens and the control session will be encrypted, and PROT P is
        /// also sent so the data connections will be encrypted.
        /// </summary>
        public void Open(string sHost, bool bEnableSSL)
        {
            m_bSupportsEPSV = true;     // assume server supports EPSV command till we know otherwise

            m_ControlClient = new System.Net.Sockets.TcpClient(sHost, 21);
            m_sHost = sHost;
            m_ControlClient.ReceiveTimeout = 30000;
            m_ControlStream = m_ControlClient.GetStream();
            m_RawStreamReader = new System.IO.StreamReader(m_ControlStream, Encoding.ASCII);
            m_RawStreamWriter = new System.IO.StreamWriter(m_ControlStream, Encoding.ASCII);

            m_StreamReader = m_RawStreamReader;
            m_StreamWriter = m_RawStreamWriter;

            pv_CheckResponse(2, 2);

            if (bEnableSSL)
            {
                pv_SendCmd("AUTH", "TLS");

                pv_CheckResponse(2, 3);

                m_SSLControlStream = new System.Net.Security.SslStream(m_ControlStream, true,
                    new System.Net.Security.RemoteCertificateValidationCallback(pv_CertValidationCallback));
                m_SSLControlStream.AuthenticateAsClient(sHost);

                m_SSLStreamReader = new System.IO.StreamReader(m_SSLControlStream, Encoding.ASCII);
                m_SSLStreamWriter = new System.IO.StreamWriter(m_SSLControlStream, Encoding.ASCII);

                m_StreamReader = m_SSLStreamReader;
                m_StreamWriter = m_SSLStreamWriter;

                pv_SendCmd("PBSZ", "0");
                pv_CheckResponse(2);

                pv_SendCmd("PROT", "P");
                pv_CheckResponse(2);

                m_bSSLEnabled = true;
            }
        }

        public string GetCurrentDirectory()
        {
            pv_SendCmd("PWD");

            int iCode;
            string sResponse;
            pv_GetResponse(out iCode, out sResponse);

            char[] aSeparators = { '\"' };
            string[] Strings = sResponse.Split(aSeparators);

            return Strings[1];
        }

        public void ChangeDirectory(string sDir)
        {
            m_dir = sDir;
            pv_SendCmd("CWD", sDir);
            pv_CheckResponse(2, 5);
        }

        public bool SSLEnabled
        {
            get { return m_bSSLEnabled; }
        }

        public void SetControlClear()
        {
            if (!m_bSSLEnabled)
                throw new Exception("CCC not supported without SSL enabled");

            pv_SendCmd("CCC");
            pv_CheckResponse(2);
            m_SSLControlStream.Dispose();
            m_SSLControlStream = null;
            m_SSLStreamReader.Dispose();
            m_SSLStreamWriter.Dispose();

            m_StreamReader = m_RawStreamReader;
            m_StreamWriter = m_RawStreamWriter;
        }

        public void DeleteFile(string sName, bool fIgnoreNotExist)
        {
            pv_SendCmd("DELE", sName);

            int iCode;
            string sResponse;
            pv_GetResponse(out iCode, out sResponse);

            if (iCode == 250)
                return; // all OK

            if (iCode == 550)
                // file does not exist
                if (fIgnoreNotExist)
                    return;

            throw new Exception("Unable to delete file: " + sResponse);
        }

        public void CreateDirectory(string sDirectory)
        {
            pv_SendCmd("MKD", sDirectory);
            pv_CheckResponse(2, 5);
        }

        public void Rename(string sFrom, string sTo)
        {
            pv_SendCmd("RNFR", sFrom);
            pv_CheckResponse(3, 5);
            pv_SendCmd("RNTO", sTo);
            pv_CheckResponse(2, 5);
        }

        public void GetFile(string NetworkPath, string LocalPath)
        {
            pv_EnterPassiveMode();

            pv_SendCmd("RETR", NetworkPath);

            pv_EstablishDataConnection();


            FileInfo targetFI = new FileInfo(LocalPath);

            //loop to read & write to file
            using (FileStream fs = targetFI.OpenWrite())
            {
                try
                {
                    byte[] buffer = new byte[2048];
                    int read;
                    do
                    {
                        read = m_DataStream.Read(buffer, 0, buffer.Length);
                        fs.Write(buffer, 0, read);
                    } while (read != 0);
                    fs.Flush();
                    fs.Close();
                }
                catch (Exception)
                {
                    //catch error and delete file only partially downloaded
                    fs.Close();
                    //delete target file as it's incomplete
                    targetFI.Delete();
                    throw;
                }
            }



            pv_CloseDataConnectionAndCheckResponse();
        }

        public System.IO.Stream GetSendFileStream(string sName)
        {
            pv_EnterPassiveMode();

            pv_SendCmd("STOR", sName);

            pv_EstablishDataConnection();

            return new SendFileStream(m_DataStream, new SendFileStream.OnClose(pv_CloseDataConnectionAndCheckResponse));
        }

        public void SendFile(string LocalFileName, string NetworkFileName)
        {
            FileInfo fi = new FileInfo(LocalFileName);

            pv_EnterPassiveMode();

            pv_SendCmd("STOR", NetworkFileName);

            pv_EstablishDataConnection();

            using (FileStream fs = fi.OpenRead())
            {
                byte[] abBuffer = new byte[4096];
                int iBytes = fs.Read(abBuffer, 0, abBuffer.Length);
                while (iBytes != 0)
                {
                    m_DataStream.Write(abBuffer, 0, iBytes);
                    iBytes = fs.Read(abBuffer, 0, abBuffer.Length);
                }
            }
            pv_CloseDataConnectionAndCheckResponse();
        }

        public string[] GetDirectoryList()
        {
            pv_EnterPassiveMode();

            //            pv_SendCmd( "LIST" );
            pv_SendCmd("NLST");
            //            pv_CheckResponse( 200 );

            pv_EstablishDataConnection();

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            byte[] aBuffer = new byte[1024];
            int iBytes = m_DataStream.Read(aBuffer, 0, aBuffer.Length);
            while (iBytes > 0)
            {
                ms.Write(aBuffer, 0, iBytes);
                iBytes = m_DataStream.Read(aBuffer, 0, aBuffer.Length);
            }

            pv_CloseDataConnectionAndCheckResponse();

            ms.Position = 0;
            System.IO.StreamReader reader = new System.IO.StreamReader(ms, Encoding.ASCII);
            string s = reader.ReadToEnd();

            string[] asDelimiters = new string[4];
            asDelimiters[0] = "\r\n";
            asDelimiters[1] = "\n";
            asDelimiters[2] = "\n\r";
            asDelimiters[3] = "\r";
            return s.Split(asDelimiters, StringSplitOptions.RemoveEmptyEntries);
        }

        public IEnumerable<FTPfileInfo> GetFullDirectoryList()
        {
            pv_EnterPassiveMode();

            pv_SendCmd("LIST");

            pv_EstablishDataConnection();

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            byte[] aBuffer = new byte[1024];
            int iBytes = m_DataStream.Read(aBuffer, 0, aBuffer.Length);
            while (iBytes > 0)
            {
                ms.Write(aBuffer, 0, iBytes);
                iBytes = m_DataStream.Read(aBuffer, 0, aBuffer.Length);
            }

            pv_CloseDataConnectionAndCheckResponse();

            ms.Position = 0;
            System.IO.StreamReader reader = new System.IO.StreamReader(ms, Encoding.ASCII);
            string Datastring = reader.ReadToEnd();

            //string[] asDelimiters = new string[4];
            //asDelimiters[0] = "\r\n";
            //asDelimiters[1] = "\n";
            //asDelimiters[2] = "\n\r";
            //asDelimiters[3] = "\r";
            //return s.Split(asDelimiters, StringSplitOptions.RemoveEmptyEntries);
            Datastring = Datastring.Replace("\r\n", "\r").TrimEnd('\r');
            //split the string into a list
            return new FTPdirectory(Datastring, m_dir);
        }

        private void pv_CloseDataConnection()
        {
            if ((m_DataClient != null)
                && (m_DataClient.Connected)
                )
                m_DataClient.Client.Shutdown(System.Net.Sockets.SocketShutdown.Both);

            if (m_DataStream != null)
                m_DataStream.Dispose();
            if (m_DataClient != null)
            {
                if (m_DataClient.Connected)
                {
                    m_DataClient.Close();
                }
            }
            m_DataClient = null;
            m_DataStream = null;
        }

        private void pv_CloseDataConnectionAndCheckResponse()
        {
            pv_CloseDataConnection();
            pv_CheckResponse(2, 2, 6);    // transfer complete
        }

        private void pv_EnterPassiveMode()
        {
            bool bInEPSV = false;
            bool bTriedEPSV = false;
            int iCode;
            string sResponse;
            string sEPSVResponse = null;

            if (m_bSupportsEPSV)
            {
                bTriedEPSV = true;
                pv_SendCmd("EPSV");
                pv_GetResponse(out iCode, out sEPSVResponse);
                if (iCode == 229)
                {
                    bInEPSV = true;

                    // scan response for where to connect to 
                    string[] asEParams = sEPSVResponse.Split(ms_acEPSVSeps);

                    if (asEParams.Length < 3)
                        throw new Exception("Could not parse EPSV resposne: " + sEPSVResponse);

                    m_sDataHost = asEParams[1];
                    if (string.IsNullOrEmpty(m_sDataHost))
                        m_sDataHost = m_sHost;
                    //                    System.Console.WriteLine( "Response {0}", sEPSVResponse );
                    //                    System.Console.WriteLine( "Response port is {0}", asEParams[3] );
                    if (!System.Int32.TryParse(asEParams[3], out m_iDataPort))
                        throw new Exception("Could not determine port from EPSV response: " + sEPSVResponse);
                }
                else
                    m_bSupportsEPSV = false;
            }

            if (!bInEPSV)
            {
                pv_SendCmd("PASV");
                pv_GetResponse(out iCode, out sResponse);
                if (iCode != 227)
                {
                    if (bTriedEPSV)
                    {
                        throw new Exception(string.Format("EPSV mode not supported: {0}{1}PASV mode not supported: {2}",
                            sEPSVResponse,
                            System.Environment.NewLine,
                            sResponse));
                    }
                    else
                        throw new Exception("PASV mode not supported: " + sResponse);
                }

                // scan the response for the host and port to which we connect
                int iStartHost = 0;
                while (iStartHost < sResponse.Length)
                {
                    if (System.Char.IsDigit(sResponse[iStartHost]))
                        break;
                    iStartHost++;
                }

                char[] aDelimiters = new char[2];
                aDelimiters[0] = ',';
                aDelimiters[1] = ')';
                string[] asNumbers = sResponse.Substring(iStartHost).Split(aDelimiters);
                if (asNumbers.Length < 6)
                    throw new Exception("Could not determine host IP from PASV response");

                int[] aNumbers = new int[6];
                try
                {
                    for (int iNumber = 0; iNumber < 6; iNumber++)
                    {
                        aNumbers[iNumber] = System.Convert.ToInt32(asNumbers[iNumber]);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Could not determine host IP from PASV response", e);
                }

                object[] aObjects = new Object[6];
                for (int i = 0; i < 6; i++)
                    aObjects[i] = aNumbers[i];
                m_sDataHost = System.String.Format("{0}.{1}.{2}.{3}", aObjects);
                m_iDataPort = (aNumbers[4] << 8) + aNumbers[5];
            }
        }

        private System.Net.Sockets.TcpClient pv_NewTCPClient(string sHost, int iHostPort)
        {
            System.Net.Sockets.TcpClient rc = null;

            if ((!m_niLocalPortStart.HasValue)
                || (!m_niLocalPortEnd.HasValue)
                )
            {
                // if non port range specified, then just use default TCP from host
                rc = new System.Net.Sockets.TcpClient(sHost, iHostPort);
            }
            else
            {
                // else try and use a port within the set range

                int iPort = m_niLocalPortStart.Value;
                bool bRetry = true;
                while (bRetry)
                {
                    try
                    {
                        rc = new System.Net.Sockets.TcpClient(new System.Net.IPEndPoint(System.Net.IPAddress.Any, iPort));

                        rc.Connect(sHost, iHostPort);

                        bRetry = false;
                    }
                    catch (System.Net.Sockets.SocketException ex)
                    {
                        if (rc != null)
                        {
                            rc.Close();
                            rc = null;
                        }

                        if (ex.ErrorCode == 10048)
                        {
                            iPort++;
                            if (iPort > m_niLocalPortEnd.Value)
                                throw new Exception("No IP port within allowable range is avaiable for use");
                        }
                        else
                            throw ex;
                    }
                }
            }

            return rc;
        }

        private void pv_EstablishDataConnection()
        {
            m_DataClient = pv_NewTCPClient(m_sDataHost, m_iDataPort);

            if (m_bSSLEnabled)
            {
                System.Net.Security.SslStream ss = new System.Net.Security.SslStream(m_DataClient.GetStream(), false,
                    new System.Net.Security.RemoteCertificateValidationCallback(pv_CertValidationCallback));
                ss.AuthenticateAsClient(m_sDataHost);
                m_DataStream = ss;
            }
            else
                m_DataStream = m_DataClient.GetStream();

            pv_CheckResponse(1);
        }

        // logs
        public void Logon(string sUser, string sPwd)
        {
            pv_SendCmd("USER", sUser);
            pv_CheckResponse(3, 3);        // note, should also check for 232 (pwd not required)

            pv_SendCmd("PASS", sPwd);
            pv_CheckResponse(2, 3);
        }

        private void pv_SendCmd(string sCommand)
        {
            pv_SendCmd(sCommand, null);
        }

        private void pv_SendCmd(string sCommand, string sParam)
        {
            m_TraceSource.TraceInformation("Cmd: {0}{1}", sCommand, sParam);

            m_StreamWriter.Write(sCommand);
            if (sParam != null)
            {
                m_StreamWriter.Write(' ');
                m_StreamWriter.WriteLine(sParam);
            }
            else
                m_StreamWriter.WriteLine();
            m_StreamWriter.Flush();
        }

        private void pv_CheckResponse(int? niDigit1)
        {
            pv_CheckResponse(niDigit1, null, null);
        }

        private void pv_CheckResponse(int? niDigit1, int? niDigit2)
        {
            pv_CheckResponse(niDigit1, niDigit2, null);
        }

        private void pv_CheckResponse(int? niDigit1, int? niDigit2, int? niDigit3)
        {
            int iResponseCode;
            string sResponse;

            pv_GetResponse(out iResponseCode, out sResponse);

            int iResp1, iResp2, iResp3;
            iResp1 = iResponseCode / 100;
            int iTemp = iResponseCode % 100;
            iResp2 = iTemp / 10;
            iTemp %= 10;
            iResp3 = iTemp;

            //            System.Console.WriteLine( "Response code {0}", iResponseCode );
            //            System.Console.WriteLine( "Expecting {0} {1} {2}", niDigit1, niDigit2, niDigit3 );

            if ((niDigit1.HasValue && (niDigit1.Value != iResp1))
                || (niDigit2.HasValue && (niDigit2.Value != iResp2))
                || (niDigit3.HasValue && (niDigit3.Value != iResp3))
                )
            {
                System.Text.StringBuilder msg = new StringBuilder();

                msg.AppendFormat("Unexpected resposne code {0} from ftp server.  Expected ", iResponseCode);
                if (niDigit1.HasValue)
                    msg.Append(niDigit1.Value);
                else
                    msg.Append('x');
                if (niDigit2.HasValue)
                    msg.Append(niDigit2.Value);
                else
                    msg.Append('x');
                if (niDigit3.HasValue)
                    msg.Append(niDigit3.Value);
                else
                    msg.Append('x');

                throw new Exception(msg.ToString());
            }
        }

        private void pv_GetResponse(out int iCode, out string sResponse)
        {
            string sLine = m_StreamReader.ReadLine();

            m_TraceSource.TraceInformation("Resp: {0}", sLine);

            // response should be of format 'nnn-', or 'nnn ' for multiline response
            if (sLine.Length < 4)
                throw new Exception("Unexpected response from ftp server: " + sLine);

            try
            {
                string sCode = sLine.Substring(0, 3);
                iCode = System.Convert.ToInt32(sCode);
                sResponse = sLine.Substring(4);

                if (sLine[3] == '-')
                {
                    while ((sLine.Length < 4)
                           || (sLine[0] == ' ')
                           || (sLine[3] != '-'))
                    {
                        // multi-line respone, keep reading till we get final response
                        sLine = m_StreamReader.ReadLine();
                    }

                    if (sLine.Length < 4)
                        throw new Exception("Unexpected response from ftp server: " + sLine);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unexpected response from ftp server", e);
            }
        }

        private bool pv_CertValidationCallback(Object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public bool SendFile(string sName, byte[] file)
        {
            throw new NotImplementedException();
        }

        public bool SendFileAsByte()
        {
            return false;
        }

        #region IDisposable Members

        public void Dispose()
        {
            pv_CloseDataConnection();

            if (m_SSLControlStream != null)
            {
                m_SSLControlStream.Dispose();
                m_SSLControlStream = null;
            }

            if (m_ControlClient != null)
            {
                if (m_ControlClient.Connected)
                    m_ControlClient.Close();
                m_ControlClient = null;
            }
        }

        #endregion
    }
}
