using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using CommonUtility;
using Kids.Utility;
using Kids.Utility.WebMessageBox;
using Image = System.Drawing.Image;

namespace Site.Kids.bmi.ir.Classes.FileUploadManagement
{
    [Flags]
    public enum UploadFileType
    {
        Pictures = 1,
        Document = 2,
        Video = 8,
        Sounde = 16,
        Flash =32
    }

    public enum UploadFileSizeLimitation
    {
        _100K = 1,
        _200K = 2,
        _500K = 3,
        _1M = 4,
        _2M = 5,
        _5M = 6,
        Unlimited = 7
    }

    public enum UploadImageSize
    {
        _100_130 = 1,
        _1024_768 = 2
    }
    public static class FileUploadUtil
    {
        public static Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = size.Width / (float)originalWidth;
                float percentHeight = size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public static string SaveUploadeFile(AsyncFileUpload fup, string SaveFolder, UploadFileType filetype, UploadFileSizeLimitation filesizelimit, UploadImageSize? Imagesize = null)
        {

            string FileExtension = Path.GetExtension(fup.FileName);

            if (!IsValidFileType(FileExtension, filetype))
            {
                MessageBoxHelper.ShowMessageBox(fup.Page, "نوع فایل نامعتبر است", "", MessageBoxType.Information);
                return null;
            }
            if (!IsValidFileSize(fup.FileBytes.Length.ToLong(), filesizelimit))
            {
                MessageBoxHelper.ShowMessageBox(fup.Page, "حجم فایل نامعتبر است", "", MessageBoxType.Information);
                return null;
            }

            MemoryStream memStream = Imagesize.HasValue ? ResizeImage(fup.FileBytes, Imagesize.Value, FileExtension) : new MemoryStream(fup.FileBytes);


            string FileName = Path.GetFileNameWithoutExtension(fup.FileName);
            if (FileName != null) FileName = FileName.Replace(" ", "_");

            FileName = FileName + "__" + Common.CreateTemporaryPassword(8) + FileExtension;
            String AttchFilePath = HttpContext.Current.Server.MapPath(SaveFolder + FileName);
            File.WriteAllBytes(AttchFilePath, memStream.ToArray());
            return FileName;

        }

        public static string SaveUploadeFile(FileUpload fup, string SaveFolder, UploadFileType filetype, UploadFileSizeLimitation filesizelimit, UploadImageSize? Imagesize = null)
        {
            if (fup.HasFile)
            {
                string FileExtension = Path.GetExtension(fup.PostedFile.FileName);

                if (!IsValidFileType(FileExtension, filetype))
                {
                    MessageBoxHelper.ShowMessageBox(fup.Page, "نوع فایل نامعتبر است", "", MessageBoxType.Information);
                    return null;
                }
                if (!IsValidFileSize(fup, filesizelimit))
                {
                    MessageBoxHelper.ShowMessageBox(fup.Page, "حجم فایل نامعتبر است", "", MessageBoxType.Information);
                    return null;
                }

                MemoryStream memStream = Imagesize.HasValue ? ResizeImage(fup.FileBytes, Imagesize.Value, FileExtension) : new MemoryStream(fup.FileBytes);


                string FileName = Path.GetFileNameWithoutExtension(fup.PostedFile.FileName);
                if (FileName != null) FileName = FileName.Replace(" ", "_");

                FileName = FileName + "__" + Common.CreateTemporaryPassword(8) + FileExtension;
                String AttchFilePath = HttpContext.Current.Server.MapPath(SaveFolder + FileName);

                File.WriteAllBytes(AttchFilePath, memStream.ToArray());
                return FileName;
            }
            return null;
        }

        private static MemoryStream ResizeImage(byte[] filebytes, UploadImageSize Imagesize, string FileExtension)
        {
            Image original = Image.FromStream(new MemoryStream(filebytes));
            Size s = GetSize(Imagesize);
            Image resized = ResizeImage(original, s);
            MemoryStream memStream = new MemoryStream();

            if (FileExtension.ToUpper() == ".PNG")
                resized.Save(memStream, ImageFormat.Png);
            else if (FileExtension.ToUpper() == ".JPG" || FileExtension.ToUpper() == ".JPEG")
                resized.Save(memStream, ImageFormat.Jpeg);
            return memStream;
        }

        private static Size GetSize(UploadImageSize Imagesize)
        {
            switch (Imagesize)
            {
                case UploadImageSize._100_130:
                    return new Size(100, 130);
                case UploadImageSize._1024_768:
                    return new Size(1024, 768);
                default:
                    throw new ArgumentOutOfRangeException("Imagesize");
            }
        }

        private static bool IsValidFileType(string FileExtension, UploadFileType filetype)
        {

            if ((filetype & UploadFileType.Pictures) == UploadFileType.Pictures)
                return new[] { ".JPG", ".GIF", ".PNG", ".BMP" }.Any(o => o == FileExtension.ToUpper());

            if ((filetype & UploadFileType.Document) == UploadFileType.Document)
                return new[] { ".DOC", ".DOCX", ".XLS", ".XLSX" }.Any(o => o == FileExtension.ToUpper());

            if ((filetype & UploadFileType.Video) == UploadFileType.Video)
                return new[] { ".AVI", ".MP4", ".3GP" }.Any(o => o == FileExtension.ToUpper());

            if ((filetype & UploadFileType.Sounde) == UploadFileType.Sounde)
                return new[] { ".MP3", ".WMA" }.Any(o => o == FileExtension.ToUpper());

            if ((filetype & UploadFileType.Flash) == UploadFileType.Flash)
                return new[] { ".SWF", ".FLV" }.Any(o => o == FileExtension.ToUpper());

            throw new ArgumentOutOfRangeException("filetype");
        }

        private static bool IsValidFileSize(FileUpload fup, UploadFileSizeLimitation filesize)
        {
            return IsValidFileSize(fup.PostedFile.ContentLength, filesize);
        }

        private static bool IsValidFileSize(long fupsize, UploadFileSizeLimitation filesize)
        {
            switch (filesize)
            {
                case UploadFileSizeLimitation._100K:
                    return fupsize / 1024.0 <= 100;

                case UploadFileSizeLimitation._200K:
                    return fupsize / 1024.0 <= 200;

                case UploadFileSizeLimitation._500K:
                    return fupsize / 1024.0 <= 500;

                case UploadFileSizeLimitation._1M:
                    return fupsize / 1024.0 <= 1000;

                case UploadFileSizeLimitation._2M:
                    return fupsize / 1024.0 <= 2000;

                case UploadFileSizeLimitation._5M:
                    return fupsize / 1024.0 <= 5000;

                case UploadFileSizeLimitation.Unlimited:
                    return fupsize / 1024.0 <= 10000;

                default:
                    throw new ArgumentOutOfRangeException("filesize");
            }
        }


        public static long GetFileSize(UploadFileSizeLimitation filesize)
        {
            switch (filesize)
            {
                case UploadFileSizeLimitation._100K:
                    return 100 * 1024;

                case UploadFileSizeLimitation._200K:
                    return 200 * 1024;

                case UploadFileSizeLimitation._500K:
                    return 500 * 1024;

                case UploadFileSizeLimitation._1M:
                    return 1000 * 1024;

                case UploadFileSizeLimitation._2M:
                    return 2000 * 1024;

                case UploadFileSizeLimitation._5M:
                    return 5000 * 1024;

                case UploadFileSizeLimitation.Unlimited:
                    return 1000000 * 1024;

                default:
                    throw new ArgumentOutOfRangeException("filesize");
            }
        }

        internal static string GetFileTypes(UploadFileType filetype)
        {
            if ((filetype & UploadFileType.Pictures) == UploadFileType.Pictures)
                return @"fileExtension.toUpperCase().indexOf('.JPG') ==  -1 &&
                 fileExtension.toUpperCase().indexOf('.JPG') ==  -1 && 
                 fileExtension.toUpperCase().indexOf('.GIF') ==  -1 && 
                 fileExtension.toUpperCase().indexOf('.PNG') ==  -1";


            if ((filetype & UploadFileType.Document) == UploadFileType.Document)
                return @"fileExtension.toUpperCase().indexOf('.DOC') ==  -1 &&
                 fileExtension.toUpperCase().indexOf('.DOCX') ==  -1 && 
                 fileExtension.toUpperCase().indexOf('.XLS') ==  -1 && 
                 fileExtension.toUpperCase().indexOf('.XLSX') ==  -1";


            if ((filetype & UploadFileType.Video) == UploadFileType.Video)
                return @"fileExtension.toUpperCase().indexOf('.AVI') ==  -1 &&
                 fileExtension.toUpperCase().indexOf('.MP4') ==  -1 && 
                 fileExtension.toUpperCase().indexOf('.3GP') ==  -1 ";

            if ((filetype & UploadFileType.Sounde) == UploadFileType.Sounde)
                return @"fileExtension.toUpperCase().indexOf('.MP3') ==  -1 &&
                 fileExtension.toUpperCase().indexOf('.WMA') ==  -1 ";

            if ((filetype & UploadFileType.Flash) == UploadFileType.Flash)
                return @"fileExtension.toUpperCase().indexOf('.SWF') ==  -1 &&
                         fileExtension.toUpperCase().indexOf('.FLV') ==  -1 &&
                 fileExtension.toUpperCase().indexOf('.WMA') ==  -1 ";

            throw new ArgumentOutOfRangeException("filetype");
        }
    }
}