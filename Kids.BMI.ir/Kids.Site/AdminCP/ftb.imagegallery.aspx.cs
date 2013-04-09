using System;
using System.Configuration;
using System.IO;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP
{
    public partial class WebForm1 : AdminSecureFormBaseClass
    {
        [ConfigurationProperty("maxRequestLength", DefaultValue = 10000)]
        public int MaxRequestLength { get; set; }

        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null)
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImageGallery1.AcceptedFileTypes = new[] { "jpeg", "png", "jpg", "gif", "rar", "zip", "ppt", "doc", "pptx","xlsx", "docx", "pdf", "txt","swf","flv" };




            //string currentFolder = ImageGallery1.CurrentImagesFolder;

            // modify the directories allowed
            //if (currentFolder == "~/images")
            //{
            //    string[] defaultDirectories = System.IO.Directory.GetDirectories(Server.MapPath(currentFolder), "*");
            //    //string[] customDirectories = new string[] { "folder1", "folder2" };
            //    ImageGallery1.CurrentDirectories = defaultDirectories;
            //}


            // modify the images allowed
            //if (currentFolder == "~/images")
            //{

            //System.IO.DirectoryInfo directoryInfo = new DirectoryInfo(Server.MapPath(currentFolder));

            // these are the default images FTB:ImageGallery will find
            //System.IO.FileInfo[] defaultImages = new DirectoryInfo(MapPath("~/AdminCP/Files")).GetFiles();

            // user defined custom images (here, we're just allowing the first two)
            //System.IO.FileInfo[] customImages = new FileInfo[2] { defaultImages[0], defaultImages[1] };

            // the gallery will use these images in this instance
            //ImageGallery1.CurrentImages = defaultImages;



        }


    }
}