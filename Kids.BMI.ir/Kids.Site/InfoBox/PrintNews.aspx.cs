using System;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.InfoBox
{
    public partial class PrintNews : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (UtilityMethod.GetRequestParameter( "id").IsInt64())
            {
                long newsId = Convert.ToInt64(UtilityMethod.GetRequestParameter( "Id"));
                News news = News_DataProvider.GetNews(newsId);
                if (news != null)
                {
                    TitleLbl.Text = news.Title;
                    summaryLbl.Text = "<font size=1 color=gray>" + PersianDateTime.MiladiToPersian(news.CreateDateTime).ToLongDateTimeString() + "</font>";
                    summaryLbl.Text += "<br>" + news.Summary.Replace("\n", "<br>");
                    BodyLbl.Text = news.Body;


                    if (news.PicAddress != "")
                    {
                        newsImage.ImageUrl = SystemConfigs.UrlNewsFilesPath + news.PicAddress;
                        newsImage.Visible = true;

                    }
                    else
                        newsImage.Visible = false;
                    
                }
                
            }
            
        }
    }
}