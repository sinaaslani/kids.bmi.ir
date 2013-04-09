using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Site.Kids.bmi.ir.Classes
{
    public class WebResourceResponseFilter : Stream
    {
        private readonly Stream baseStream;

        public WebResourceResponseFilter(Stream responseStream)
        {
            if (responseStream == null)
                throw new ArgumentNullException("responseStream");
            baseStream = responseStream;
        }

        public override bool CanRead
        {
            get { return baseStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return baseStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return baseStream.CanWrite; }
        }

        public override void Flush()
        {
            baseStream.Flush();
        }

        public override long Length
        {
            get { return baseStream.Length; }
        }

        public override long Position
        {
            get { return baseStream.Position; }
            set { baseStream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return baseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return baseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            baseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            string originalText = Encoding.UTF8.GetString(buffer, offset, count);


            const string patt = @"<script\b[^>]*WebResource.axd[^?]*\?[.d]*d=(.*)t=(.*)[^>]*>";
            var r = new Regex(patt, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var newtext = r.Replace(originalText, MatchEvaluator);

            const string patt2 = @"<script\b[^>]*ScriptResource.axd[^?]*\?[.d]*d=(.*)t=(.*)[^>]*>";
            r = new Regex(patt2, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            newtext = r.Replace(newtext, MatchEvaluator);


            //const string patt3 = @"\|ScriptPath\|/ScriptResource.axd\?d=([^&]*)&t=([^|]*)";
            //r = new Regex(patt3, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //newtext = r.Replace(newtext, MatchEvaluator);

            //const string patt4 = @"'/WebResource.axd\?d=([^&]*)&t=([^']*)'";
            //r = new Regex(patt4, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //newtext = r.Replace(newtext, MatchEvaluator);
            

            buffer = Encoding.UTF8.GetBytes(newtext);
            baseStream.Write(buffer, 0, buffer.Length);


        }

        public static readonly string[] FilterExpression = new[] { "--" };
        public static readonly string[] ReplacementExpression = new[] { "_DOUBLEDASH_" };

        private string MatchEvaluator(Match m)
        {
            string inputValue = m.Groups[1].Value;

            string Result = m.Groups[0].Value;
            for (int i = 0; i < FilterExpression.Length; i++)
            {
                if (inputValue.IndexOf(FilterExpression[i], StringComparison.Ordinal) >= 0)
                    Result = Result.Replace(m.Groups[1].Value, m.Groups[1].Value.Replace(FilterExpression[i], ReplacementExpression[i]));
            }

            return Result;
        }

    }
}