using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.Captcha;
using Kids.Utility.RandomGenerator;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.Classes.FileUploadManagement;

namespace Site.Kids.bmi.ir
{
    public partial class JpegImage : FormBaseClass
    {

        private struct PixelUnitFactor
        {
            public const double Px = 1.0;
            public const double Inch = 96.0;
            public const double Cm = 37.7952755905512;
            public const double Pt = 1.33333333333333;
        }

        public static double CmToPx(double cm)
        {
            return cm * PixelUnitFactor.Cm;
        }

        public static double PxToCm(double px)
        {
            return px / PixelUnitFactor.Cm;
        }

        public enum ImageActType
        {
            Captcha = 2,
            ChildImage_Main = 1,
            ChildPic = 300,
            ChildIdentityPic = 301,
            ChildNationalCardFaceUPPic = 302,
            ChildNationalCardFaceDownPic = 303,
            ChildPic_Temp = 30,
            ChildIdentityPic_Temp = 31,
            ChildNationalCardFaceUPPic_Temp = 32,
            ChildNationalCardFaceDownPic_Temp = 33
        }

        private static String GenerateIntegerRandomCode()
        {
            IntegerRandomGenerator ig = new IntegerRandomGenerator(111111, 999999);
            return ig.GetRandom().ToString();
        }

        private static String GenerateStringRandomCode()
        {
            StringRandomGenerator ig = new StringRandomGenerator(6, 6, CharacterType.Digit | CharacterType.LowerCase);
            return ig.GetRandom();
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            var act = (ImageActType)Request["act"].ToInt32();
            switch (act)
            {
                case ImageActType.ChildImage_Main:
                case ImageActType.ChildPic:
                case ImageActType.ChildIdentityPic:
                case ImageActType.ChildNationalCardFaceUPPic:
                case ImageActType.ChildNationalCardFaceDownPic:
                    GetPicFromDB(act);
                    break;

                case ImageActType.Captcha:
                    SessionItems.CaptchaImageTextForRegister = GenerateStringRandomCode();
                    string RandomText = SessionItems.CaptchaImageTextForRegister;
                    SaveTextImage(RandomText);
                    break;

                case ImageActType.ChildPic_Temp:

                    if (SessionItems.ChildPic != null)
                    {
                        var u = KidsSecureFormBaseClass.OnlineKidsUser.SSOUser;
                        SaveImage(Image.FromFile(MapPath(SystemConfigs.UrlKidsPicFilesPath(u.UserID) + SessionItems.ChildPic)));
                    }
                    break;

                case ImageActType.ChildIdentityPic_Temp:

                    if (SessionItems.ChildIdentityPic != null)
                    {
                        var u = KidsSecureFormBaseClass.OnlineKidsUser.SSOUser;
                        SaveImage(Image.FromFile(MapPath(SystemConfigs.UrlKidsPicFilesPath(u.UserID) + SessionItems.ChildIdentityPic)));
                    }
                    break;


                case ImageActType.ChildNationalCardFaceUPPic_Temp:

                    if (SessionItems.ChildNationalCardFaceUpPic != null)
                    {
                        var u = KidsSecureFormBaseClass.OnlineKidsUser.SSOUser;
                        SaveImage(Image.FromFile(MapPath(SystemConfigs.UrlKidsPicFilesPath(u.UserID) + SessionItems.ChildNationalCardFaceUpPic)));
                    }
                    break;

                case ImageActType.ChildNationalCardFaceDownPic_Temp:

                    if (SessionItems.ChildNationalCardFaceDownPic != null)
                    {
                        var u = KidsSecureFormBaseClass.OnlineKidsUser.SSOUser;
                        SaveImage(Image.FromFile(MapPath(SystemConfigs.UrlKidsPicFilesPath(u.UserID) + SessionItems.ChildNationalCardFaceDownPic)));
                    }
                    break;

                default:
                    throw new InvalidOperationException();
            }

        }



        private void GetPicFromDB(ImageActType Act)
        {
            Image img;
            if (KidsSecureFormBaseClass.OnlineKidsUser != null && KidsSecureFormBaseClass.OnlineKidsUser.Kids_UserInfo != null)
            {
                var u = KidsSecureFormBaseClass.OnlineKidsUser.Kids_UserInfo;
                string BasdeFolder = MapPath(SystemConfigs.UrlKidsPicFilesPath(u.SSOUserName));

                int? width = null, hight = null;
                if (Request["w"].IsInt32() && Request["h"].IsInt32())
                {
                    width = Request["w"].ToInt32();
                    hight = Request["h"].ToInt32();
                }
                img = GetPicFromDB(Act, u, BasdeFolder, true, width, hight);
            }
            else
            {
                img = Image.FromFile(MapPath("~/App_Themes/Default/images/unknown_user.jpg"));
            }
            SaveImage(img);
        }

        public static Image GetPicFromDB(KidsUser u, ImageActType Act, bool ReplaceEmptyPicture, int? width, int? hight)
        {
            var c = HttpContext.Current.Server;

            string BasdeFolder = c.MapPath(SystemConfigs.UrlKidsPicFilesPath(u.SSOUserName));
            Image img = GetPicFromDB(Act, u, BasdeFolder, ReplaceEmptyPicture, width, hight);

            return img;
        }

        private static Image GetPicFromDB(ImageActType Act, KidsUser u, string BasdeFolder, bool ReplaceEmptyPicture, int? width, int? hight)
        {
            var c = HttpContext.Current.Server;
            Image img = null;

            switch (Act)
            {
                case ImageActType.ChildImage_Main:
                    try
                    {
                        if (ReplaceEmptyPicture)
                            img = !string.IsNullOrWhiteSpace(u.ChildPic)
                                      ? Image.FromFile(BasdeFolder + u.ChildPic)
                                      : Image.FromFile(u.ChildSex
                                                       ? c.MapPath("~/App_Themes/Default/images/1.jpg")
                                                       : c.MapPath("~/App_Themes/Default/images/0.jpg")
                                                       );
                        else
                            img = !string.IsNullOrWhiteSpace(u.ChildPic)
                                 ? Image.FromFile(BasdeFolder + u.ChildPic)
                                 : new Bitmap(0, 0);
                    }
                    catch
                    {
                        img = Image.FromFile(c.MapPath("~/App_Themes/Default/images/unknown_user.jpg"));
                    }
                    break;

                case ImageActType.ChildPic:
                    try
                    {
                        if (ReplaceEmptyPicture)
                            img = !string.IsNullOrWhiteSpace(u.ChildPic)
                                      ? Image.FromFile(BasdeFolder + u.ChildPic)
                                      : Image.FromFile(c.MapPath("~/App_Themes/Default/images/Blank.jpg"));
                        else
                            img = !string.IsNullOrWhiteSpace(u.ChildPic)
                                 ? Image.FromFile(BasdeFolder + u.ChildPic)
                                 : new Bitmap(0, 0);
                    }
                    catch
                    {
                        img = Image.FromFile(c.MapPath("~/App_Themes/Default/images/unknown_user.jpg"));
                    }
                    break;

                case ImageActType.ChildIdentityPic:
                    try
                    {
                        if (ReplaceEmptyPicture)
                            img = !string.IsNullOrWhiteSpace(u.ChildIdentityPic)
                                     ? Image.FromFile(BasdeFolder + u.ChildIdentityPic)
                                     : Image.FromFile(c.MapPath("~/App_Themes/Default/images/Blank.jpg"));
                        else
                            img = !string.IsNullOrWhiteSpace(u.ChildIdentityPic)
                                 ? Image.FromFile(BasdeFolder + u.ChildIdentityPic)
                                 : new Bitmap(0, 0);
                    }
                    catch
                    {
                        img = Image.FromFile(c.MapPath("~/App_Themes/Default/images/unknown_user.jpg"));
                    }
                    break;

                case ImageActType.ChildNationalCardFaceUPPic:
                    try
                    {
                        if (ReplaceEmptyPicture)
                            img = !string.IsNullOrWhiteSpace(u.ChildNationalCardFaceUPPic)
                                     ? Image.FromFile(BasdeFolder + u.ChildNationalCardFaceUPPic)
                                     : Image.FromFile(c.MapPath("~/App_Themes/Default/images/Blank.jpg"));
                        else
                            img = !string.IsNullOrWhiteSpace(u.ChildNationalCardFaceUPPic)
                                 ? Image.FromFile(BasdeFolder + u.ChildNationalCardFaceUPPic)
                                 : new Bitmap(0, 0);
                    }
                    catch
                    {
                        img = Image.FromFile(c.MapPath("~/App_Themes/Default/images/unknown_user.jpg"));
                    }
                    break;

                case ImageActType.ChildNationalCardFaceDownPic:
                    try
                    {
                        if (ReplaceEmptyPicture)
                            img = !string.IsNullOrWhiteSpace(u.ChildNationalCardFaceDownPic)
                                     ? Image.FromFile(BasdeFolder + u.ChildNationalCardFaceDownPic)
                                     : Image.FromFile(c.MapPath("~/App_Themes/Default/images/Blank.jpg"));
                        else
                            img = !string.IsNullOrWhiteSpace(u.ChildNationalCardFaceDownPic)
                                 ? Image.FromFile(BasdeFolder + u.ChildNationalCardFaceDownPic)
                                 : new Bitmap(0, 0);
                    }
                    catch
                    {
                        img = Image.FromFile(c.MapPath("~/App_Themes/Default/images/unknown_user.jpg"));
                    }
                    break;
            }

            if (width.HasValue && hight.HasValue)
                img = FileUploadUtil.ResizeImage(img, new Size(width.Value, hight.Value));
            return img;
        }

        private void SaveTextImage(string RandomText)
        {
            if (!string.IsNullOrEmpty(RandomText))
            {
                CaptchaImage cImage = new CaptchaImage(RandomText, 160, 60, "Comic Sans MS");
                SaveImage(cImage.Image);
            }
        }

        private static void SaveImage(Image img)
        {
            var c = HttpContext.Current;
            c.Response.Clear();
            c.Response.ContentType = "image/jpeg";

            img.Save(c.Response.OutputStream, ImageFormat.Jpeg);
            img.Dispose();
        }

    }
}