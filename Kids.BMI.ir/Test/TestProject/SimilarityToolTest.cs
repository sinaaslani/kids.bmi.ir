using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Kids.Utility;
using Kids.Utility.UtilExtension.StringExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject1
{


    /// <summary>
    ///This is a test class for SimilarityToolTest and is intended
    ///to contain all SimilarityToolTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SimilarityToolTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void ecntest()
        {

            try
            {
                var clientCert = new X509Certificate2(@"C:\Users\p0034-2114\Downloads\kids.cer");
                string Random_EncKey1 = "+989125307593";
                string RSAEnc_Random_EncKey1 = ASymetricCryptoHelper.Encrypt(Random_EncKey1, clientCert);
            }
            catch
            {
            }
        }
        /// <summary>
        ///A test for Similarity
        ///</summary>
        [TestMethod()]
        public void SimilarityTest()
        {
            string str1 = "محید جواد";
            string str2 = "محيدجواد";
            var actual = str1.Similarity(str2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var file = "C:\\27.jpg";
            string SavePath = "C:\\" + Path.GetFileNameWithoutExtension(file) + "_stamped" + Path.GetExtension(file);
            var watermarkText = "سلام\n تولدت مبارک\n ازطرف دوستت : جابر خدا بنده منشلو";
            int right = Convert.ToInt32(500);
            int top = Convert.ToInt32(80);
            string FontName = "B Zar";
            int fontSize = 30;
            Color fontColor = Color.Black;

            WriteStringOnImage(file, FontName, fontSize, fontColor, watermarkText, right, top, SavePath);
        }

        private static void WriteStringOnImage(string file, string FontName, int fontSize, Color fontColor, string watermarkText,
                                               int right, int top, string SavePath)
        {
            using (Bitmap myBitmap = new Bitmap(file))
            using (Graphics g = Graphics.FromImage(myBitmap))
            {
                StringFormat strFormat = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Far
                };

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                using (Font font = new Font(FontName, fontSize, FontStyle.Bold))
                using (SolidBrush brush = new SolidBrush(fontColor))
                {
                    SizeF measuredSize = g.MeasureString(watermarkText, font);
                    var r = new RectangleF(right, top, measuredSize.Width + 20, measuredSize.Height + 20);
                    g.DrawString(watermarkText, font, brush, r, strFormat);
                }
                myBitmap.Save(SavePath);
            }
        }
    }
}
