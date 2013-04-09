using System;
using System.Net.Mail;
using System.Text;

namespace Kids.Utility.POP3
{
    /// <summary>
    /// A Class to parse the source of a message into a System.Net.Mail.Message
    /// </summary>
    public sealed class MessageParser
    {
        private string _lastHeaderAdded = "";
        private string _boundary = "";
        private string _contentType = "";
        private MailMessage _message = null;

        private enum PARSERLOCATION
        {
            PARSER_INHEADERS,
            PARSER_INBODY_SINGLEPART,
            PARSER_INBODY_MULTIPART_HEADER,
            PARSER_INBODY_MULTIPART_BODY,
            PARSER_INATTACHMENT
        }



        public enum CONTENTTYPES
        {
            NONE,
            UNKNOWN,
            TEXT,
            HTML
        }

        private PARSERLOCATION _loc = PARSERLOCATION.PARSER_INHEADERS;

        /// <summary>
        /// Constructor for a message parser class
        /// </summary>
        public MessageParser()
        {
        }


  
        /// <summary>
        /// Parse a string into a System.Net.Mail.Message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="id">The Unique Identifer of this message</param>
        /// <returns></returns>
        public MailMessage ParseMessage(string message, string id)
        {
            _message = new MailMessage();
            _loc = PARSERLOCATION.PARSER_INHEADERS; // Start by parsing the headers

            try /// Here we go"
            {


                // Split the message by each line for easier parsing
                string[] splitter ={"\r\n"};
                string[] lines = message.Split(splitter, StringSplitOptions.None);
                int lineCounter = 0;

                // Parse all of the headers
                ParseHeaderLines(lines, ref lineCounter);

                // Is this a multi part message?
                if (_boundary != "")
                {
                    this._loc = PARSERLOCATION.PARSER_INBODY_MULTIPART_HEADER;
                    ParseMultiPart(lines, ref lineCounter); // MultiPart Message
                }
                else
                {
                    this._loc = PARSERLOCATION.PARSER_INBODY_SINGLEPART; 
                    ParsePart(lines, ref lineCounter); // Single Part Message
                }

            }
            catch (Exception e)
            {
                throw new ApplicationException("Could not parse message data", e);
            }

            return _message;
        }


        private void ParsePart(string[] lines, ref int currentLine)
        {
            System.Text.StringBuilder part = new StringBuilder();

            string filename = "";

            bool bParsingComplete = false;


            CONTENTTYPES contentType = CONTENTTYPES.UNKNOWN;

            // Now Get the message
            while (currentLine < lines.Length)
            {
                switch (this._loc)
                {
                    case PARSERLOCATION.PARSER_INBODY_MULTIPART_HEADER:
                        if (contentType == CONTENTTYPES.UNKNOWN)
                        {
                            contentType = DetermineContentType(lines[currentLine]);
                        }
                        if (lines[currentLine].Contains("filename=") == true)
                        {
                            int iPos = lines[currentLine].IndexOf("filename=")+9;
                            int iChars = lines[currentLine].Length - iPos;
                            filename = lines[currentLine].Substring(iPos, iChars);
                            filename = filename.Replace("\"", "");
                            filename = filename.Trim();
                        }
                        else if (lines[currentLine] == "")
                        {
                            this._loc = PARSERLOCATION.PARSER_INBODY_MULTIPART_BODY;
                        }
                        break;

                    case PARSERLOCATION.PARSER_INBODY_MULTIPART_BODY:
                        // This looks for the boundary
                        if (lines[currentLine].Length > 0)
                        {

                            if (_boundary.Length > 0)
                            {
                                // Look for the boundary
                                if (lines[currentLine].Contains(_boundary))
                                {
                                    bParsingComplete = true;
                                }
                            }
                            if (bParsingComplete == false)
                            {
                                part.Append(lines[currentLine]);
                                part.Append("\r\n");
                            }
                        }
                        break;

                    case PARSERLOCATION.PARSER_INBODY_SINGLEPART:
                        if (lines[currentLine].Length > 0)
                        {

                            part.Append(lines[currentLine]);
                            part.Append("\r\n");
                        }
                        break;
                }
                if (bParsingComplete == true)
                {
                    break;
                }
                currentLine++;
            }
            if (this._loc == PARSERLOCATION.PARSER_INBODY_SINGLEPART)
            {
                contentType = DetermineContentType(this._contentType);
            }
            AddPart(part.ToString(), contentType, filename, "base64");
        }

        private CONTENTTYPES DetermineContentType(string str)
        {
            if (str.Contains("text/plain") == true)
            {
                return CONTENTTYPES.TEXT;
            }
            else if (str.Contains("html") == true)
            {
                return CONTENTTYPES.HTML;
            }
    
            return CONTENTTYPES.UNKNOWN;
        }

        private void ParseMultiPart(string[] lines, ref int currentLine)
        {
            StringBuilder part = new StringBuilder();

            while (currentLine < lines.Length)
            {
                this._loc = PARSERLOCATION.PARSER_INBODY_MULTIPART_HEADER;
                ParsePart(lines, ref currentLine);
                currentLine++;
            }
        }

  

        private void ParseHeaderLines(string[] lines, ref int currentLine)
        {
            string line = lines[currentLine];
            while ((line != "") && (currentLine < lines.Length))
            {
                if (line.Contains("+OK")==true)
                {
                    // This is a response to the top command! .. ignore this line
                }
                else
                {
                    ParseHeader(line);
                }
                currentLine++;
                line = lines[currentLine];
            }

            if (_boundary.Length > 0)
            {
                if (lines[currentLine + 1].Contains("This is a multi-part message in MIME format") == true)
                {
                    currentLine+=2;// Skip this line..
                }
            }
        }

        private void AddPart(string part, CONTENTTYPES contentType)
        {
            AddPart(part, contentType, "","");
        }

        private void AddPart(string part, CONTENTTYPES contentType, string filename, string encoding)
        {
            switch (contentType)
            {
                case CONTENTTYPES.HTML:
                case CONTENTTYPES.TEXT:
                case CONTENTTYPES.NONE:
                    if (_message.Body.Length > 0)
                    {
                        // Add this as an alternate view
                    }
                    else
                    {
                        _message.BodyEncoding = Encoding.ASCII;
                        _message.Body = part.ToString();
                    }
                    break;
                default:
                    // Add an Attachment
                    System.Net.Mail.Attachment a = System.Net.Mail.Attachment.CreateAttachmentFromString(part, filename);
                    a.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    
                    _message.Attachments.Add(a);
                    break;
       

            }
            
        }

        private bool ExplodeString(string str, out string strKey, out string strValue)
        {
            int loc = str.IndexOf(':');
            int locSpace = str.IndexOf(' ');

            bool bFoundHeaderKey = false;

            if (loc > -1)
            {
                if (loc < locSpace)
                {
                    bFoundHeaderKey = true;
                }
            }

            if (bFoundHeaderKey)
            {
                strKey = str.Substring(0, loc);
                strValue = str.Substring(loc + 2, str.Length - loc - 2);
                strValue = strValue.Trim();
            }
            else
            {
                strKey = "";
                strValue = str;
            }

            return bFoundHeaderKey;

        }

        private void ParseHeader(string str)
        {
            string strKey = "";
            string strValue = "";


            bool bFoundHeaderKey = ExplodeString(str, out strKey, out strValue);

   
            if (bFoundHeaderKey == true)
            {
                if ((strKey.Length > 0) && (strValue.Length > 0))
                {
                    _message.Headers.Add(strKey, strValue);
                }
                if (strKey.Length>0)
                    _lastHeaderAdded = strKey;
            }
            else
            {
                string oldData = _message.Headers[_lastHeaderAdded];
                strKey = _lastHeaderAdded;
                oldData += str;
                _message.Headers[_lastHeaderAdded] = oldData;
            }
            switch (strKey.ToUpper())
            {
                case "TO":
                    _message.To.Add(strValue);
                    break;
                case "FROM":
                    System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(strValue);
                    _message.From= addr;
                    break;
                case "CC":
                    _message.CC.Add(strValue);
                    break;
                case "SUBJECT":
                    _message.Subject = strValue;
                    break;
                case "CONTENT-TYPE":
                    _boundary = ParseBoundary(strValue);
                    break;
            }
        }

        private string ParseBoundary(string boundary)
        {
            string ret = "";
            try
            {
                int loc = boundary.IndexOf("boundary=");
                if (loc > 0)
                {
                    int iStart = loc + 10 + 2;
                    ret = boundary.Substring(iStart, boundary.Length - iStart - 1);
                }
                else
                {
                    _contentType = boundary;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.ToString());
            }
            return ret;
        }
    }
}