using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using CommonUtility;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.EntitiesModel.Scores;
using Kids.Utility;
using Kids.Utility.UtilExtension.StringExtensions.Validators;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.WishAccount
{
    public partial class PostalCardShow : KidsSecureFormBaseClass
    {
        private PostalCard _CurrentCard;
        private PostalCard CurrentCard
        {
            get
            {
                return _CurrentCard ??
                       (_CurrentCard = SerializeHelper.DataContract_ToObject<PostalCard>(ViewState["CurrentCard"].ToString()));
            }
            set
            {
                _CurrentCard = value;
                ViewState["CurrentCard"] = SerializeHelper.DataContract_ToString(_CurrentCard);
            }
        }
        protected override void CheckKidsUser()
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DeleteTempFolder();

            if (!IsPostBack)
            {
                if (Request["id"].IsInt32())
                {
                    var cardid = UtilityMethod.GetRequestParameter("id", "0").ToInt32();
                    CurrentCard = PostalCard_DataProvider.GetPostalCard(cardid).FirstOrDefault();
                    if (CurrentCard != null)
                    {
                        //var user = OnlineKidsUser.Kids_UserInfo;
                        if (SessionItems.CurrentScore < CurrentCard.CardScore)
                        {
                            ShowMessageBox("شما امتیاز کافی جهت خرید این کارت پستال را ندارید");
                            return;
                        }
                        pnlMain.Visible = true;
                        imgPostalCardSmallPic.ImageUrl = string.Format("{0}/{1}", SystemConfigs.UrlPostalCardFilesPath, CurrentCard.CardPostalSmallPic);
                        txtColor.Text = ColorTranslator.ToHtml(Color.Black).Replace("#", "");
                        txtTop.Text = "10";
                        txtRight.Text = "10";
                        txtFontSize.Text = "30";
                        //txtRotationDegree.Text = "0";
                    }
                    else Response.Redirect("~/PostalCardList.aspx");
                }
                else
                    Response.Redirect("~/PostalCardList.aspx");
            }
        }

        private void DeleteTempFolder()
        {
            var FileList = new DirectoryInfo(MapPath(string.Format("{0}/Temp", SystemConfigs.UrlPostalCardFilesPath))).GetFiles().Where(o => o.CreationTime < DateTime.Now.AddMinutes(1));
            foreach (FileInfo f in FileList)
                try
                {
                    f.Delete();
                }
                catch { }

        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            int Top, Right, FontSize, RotationDegree;
            string FontName;

            Color c;
            if (!GetPostalCardInfo(out Top, out Right, out RotationDegree, out FontName, out FontSize, out c)) return;

            var path = CreateImage(Top, Right, RotationDegree, FontName, FontSize, c, CurrentCard, txtPostalCardText.Text);
            imgPreview.ImageUrl = path;
            pnlPreview.Visible = true;
            pnlSend.Visible = false;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            int Top, Right, FontSize, RotationDegree;
            string FontName;

            Color c;
            if (!GetPostalCardInfo(out Top, out Right, out RotationDegree, out FontName, out FontSize, out c)) return;

            var path = CreateImage(Top, Right, RotationDegree, FontName, FontSize, c, CurrentCard, txtPostalCardText.Text);
            imgSendPostalCard.ImageUrl = path;
            lblFriendInfo.Text = string.Format("{0}({1})", txtFriendName.Text, txtFriendEmailAddress.Text);
            lblPostalCardScore.Text = CurrentCard.CardScore.ToString() + " امتیاز ";
            pnlPreview.Visible = false;
            pnlSend.Visible = true;
            pnlCardInfo.Visible = false;
        }

        private bool GetPostalCardInfo(out int Top, out int Right, out int RotationDegree, out string FontName, out int FontSize, out Color c)
        {
            Top = 0;
            Right = 0;
            FontSize = 0;
            RotationDegree = 0;
            c = Color.Black;
            FontName = "Tahoma";
            if (!txtTop.Text.IsInt32())
            {
                ShowMessageBox("مقدار نقطه شروع بالای متن عکس نامعتبر است");
                return false;
            }
            if (!txtRight.Text.IsInt32())
            {
                ShowMessageBox("مقدار نقطه شروع راست متن عکس نامعتبر است");
                return false;
            }
            if (!txtColor.Text.IsColor())
            {
                ShowMessageBox("رنگ انتخاب شده نامعتبر است");
                return false;
            }
            if (!txtFontSize.Text.IsInt32())
            {
                ShowMessageBox("مقدار اندازه متن نامعتبر است");
                return false;
            }
            if (string.IsNullOrWhiteSpace(drpFontName.SelectedValue))
            {
                ShowMessageBox("نام فونت را انتخاب نمایید");
                return false;
            }
            //if (!txtRotationDegree.Text.IsInt32())
            //{
            //    ShowMessageBox("مقدار زاویه چرخش متن نامعتبر است");
            //    return false;
            //}


            Top = txtTop.Text.ToInt32();
            Right = txtRight.Text.ToInt32();
            RotationDegree = 0;//txtRotationDegree.Text.ToInt32();
            FontName = drpFontName.SelectedValue;
            FontSize = txtFontSize.Text.ToInt32();
            c = txtColor.Text.ToColor();
            return true;
        }


        private string CreateImage(int Top, int Right, int RotationDegree, string FontName, int fontSize, Color fontColor, PostalCard p, string BodyText)
        {
            var file = MapPath(string.Format("{0}/{1}", SystemConfigs.UrlPostalCardFilesPath, p.CardPostalPic));
            string SavePath = string.Format("{0}Temp/{1}_stamped{2}", SystemConfigs.UrlPostalCardFilesPath, Common.CreateTemporaryPassword(10), Path.GetExtension(file));

            WriteStringOnImage(file, FontName, fontSize, fontColor, BodyText, Right, Top, RotationDegree, MapPath(SavePath));
            return SavePath;
        }
        private static void WriteStringOnImage(string file, string FontName, int fontSize, Color fontColor, string watermarkText,
                                              int right, int top, int RotationDegree, string SavePath)
        {
            using (Bitmap myBitmap = new Bitmap(file))
            using (Graphics g = Graphics.FromImage(myBitmap))
            {
                StringFormat strFormat = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.DirectionRightToLeft,
                };

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                using (Font font = new Font(FontName, fontSize, FontStyle.Bold, GraphicsUnit.Point))
                using (SolidBrush brush = new SolidBrush(fontColor))
                {

                    var left = myBitmap.Width - right;

                    //left = left - myBitmap.Width / 2;
                    //top = top - myBitmap.Height / 2;

                    //float centerX = myBitmap.Width / 2f;
                    //float centerY = myBitmap.Height / 2f;
                    //g.FillEllipse(Brushes.Green, centerX - 5f, centerY - 5f, 10f, 10f);
                    //GraphicsState gs = g.Save();
                    //Matrix mat = new Matrix();
                    //mat.RotateAt(RotationDegree, new PointF(centerX, centerY), MatrixOrder.Append);
                    //g.Transform = mat;
                    //var r = new RectangleF(centerX / 2f, centerY / 2f, measuredSize.Width / 2, measuredSize.Height / 2);
                    //g.TranslateTransform(myBitmap.Width / 2f, myBitmap.Height / 2f);
                    //g.RotateTransform(RotationDegree);
                    //g.Restore(gs);
                    //g.ResetTransform();

                    SizeF measuredSize = g.MeasureString(watermarkText, font);
                    var r = new RectangleF(left - measuredSize.Width, top, measuredSize.Width, measuredSize.Height);
                    g.DrawString(watermarkText, font, brush, r, strFormat);

                }
                myBitmap.Save(SavePath, ImageFormat.Png);
            }
        }
        protected void SendTome_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSendTome.Checked)
            {
                var user = OnlineKidsUser.Kids_UserInfo;
                txtMyEmailAddress.Text = user.ChildEmailAddress;
                txtMyEmailAddress.Visible = true;
            }
            else
            {
                txtMyEmailAddress.Visible = false;
            }
        }

        protected void btnFinalSend_Click(object sender, EventArgs e)
        {
            try
            {
                int Top, Right, FontSize, RotationDegree;
                string FontName;

                Color c;
                if (!GetPostalCardInfo(out Top, out Right, out RotationDegree, out FontName, out FontSize, out c)) return;

                var path = CreateImage(Top, Right, RotationDegree, FontName, FontSize, c, CurrentCard, txtPostalCardText.Text);
                SendEmail(path);

                var user = OnlineKidsUser.Kids_UserInfo;
                ScoreHelper.AddScore(user, CurrentCard.ScoreType, CurrentCard.CardScore.ToDouble());

                ShowMessageBox(string.Format("کارت پستال با موفقیت ارسال شد و {0} امتیاز از مجموع امتیازات شما کسر گردید",
                                   CurrentCard.CardScore), "ارسال کارت تبریک", MessageBoxType.Information);
                pnlPreview.Visible = false;
                pnlSend.Visible = false;
                pnlMain.Visible = false;
            }
            catch (Exception ex)
            {
                ShowMessageBox(string.Format("خطا در ارسال ایمیل{0}", ex.Message), "ارسال دعوتنامه", MessageBoxType.Error);
            }
        }

        private void SendEmail(string ImagePath)
        {
            var user = OnlineKidsUser.Kids_UserInfo;
            string HTMLBody = File.ReadAllText(Server.MapPath("~/WishAccount/PostalCardEmailTemplate.htm"));
            HTMLBody = HTMLBody.Replace("@@CHILDNAME@@", user.ChildName + " " + user.ChildFamily);
            HTMLBody = HTMLBody.Replace("@@CHILDUSERID@@", user.KidsUserId.ToString());
#if(DEBUG)
            HTMLBody = HTMLBody.Replace("@@REGISTRATIONADDRESS@@", string.Format("http://localhost:7008/Register.aspx?inid={0}", user.KidsUserId));
#else
            HTMLBody = HTMLBody.Replace("@@REGISTRATIONADDRESS@@", string.Format("http://Kids.bmi.ir/Register.aspx?inid={0}", user.KidsUserId));
#endif

            string To = txtFriendEmailAddress.Text;
            string Bcc = chkSendTome.Checked && txtMyEmailAddress.Text.IsValidEmailAddress()
                             ? txtMyEmailAddress.Text
                             : "";

            HTMLBody = HTMLBody.Replace("@@EmbedImage1@@", Path.GetFileNameWithoutExtension(ImagePath));
            List<string> EmbedImageList = new List<string> { ImagePath };
            MailingHelper.SendEmail("Kids@BMI.IR", "کانون جوانه های بانک ملی ایران", To,
                                    string.Format("کارت تبریک : از طرف دوست شما{0} {1}", user.ChildName, user.ChildFamily), HTMLBody, EmbedImageList, Bcc);
            pnlMain.Visible = false;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlPreview.Visible = false;

        }

        protected void btnCancelSend_Click(object sender, EventArgs e)
        {
            pnlPreview.Visible = false;
            pnlSend.Visible = false;
            pnlCardInfo.Visible = true;
        }


    }
}