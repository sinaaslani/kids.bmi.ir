using System;
using System.Web.UI;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir
{
    public partial class XMLProvider : Page
    {
        private enum XMLType
        {
            Profile_BankStory = 1,258
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Id"].IsInt64())
            {

                XMLType xmltyp = (XMLType)(Request["Id"].ToInt32());
                String XMLText;

                switch (xmltyp)
                {
                    case XMLType.Profile_BankStory:

                        var user = KidsSecureFormBaseClass.OnlineKidsUser.Kids_UserInfo;
                        XMLText = string.Format(
                            @"<?xml version='1.0' encoding='utf-8' ?>
                                             <arabic>
                                                <![CDATA[
                                                کاربر : {0} {1}
                                                <br><br>امتیاز : {2:0.00} 
                                                ]]>
                                           </arabic>",
                           user.ChildName, user.ChildFamily,
                           SessionItems.CurrentScore.HasValue ? SessionItems.CurrentScore : 0);

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }


                Response.Clear();
                Response.ContentType = "text/xml";
                Response.Write(XMLText);
                Response.End();
            }
        }

    }
}


//                long PageId = Request["Id"].ToLong();
//                DynamicPage page = DynamicPages_DataProvider.GetDynamicPageById(PageId);

//                if (page != null)
//                {
//                    Response.Clear();
//                    Response.ContentType = "text/xml";
//                    String XMLText =
//                        string.Format(
//                            @"<?xml version='1.0' encoding='utf-8' ?>
//                             <DynPage>
//                                <Id>{0}</Id>
//                                <Title>{1}</Title>
//                                <PageName>{2}</PageName>
//                                <Body>{3}</Body>
//                           </DynPage>",
//                        page.PageId, page.Title, page.PageName, page.Body);
//                    Response.Write(XMLText);
//                    Response.End();

//                }
//            }
//            else
//            {
//                int recordCount;
//                List<DynamicPage> dynamicPagelist = DynamicPages_DataProvider.GetDynamicPage(out recordCount, PageTypeId: DynamicPageTypes.XMLPage);


//                StringBuilder XMLText = new StringBuilder(@"<?xml version='1.0' encoding='utf-8' ?><DynPageList>");
//                foreach (var page in dynamicPagelist)
//                {
//                    if (page != null)
//                    {

//                        XMLText.AppendFormat(
//                            @"
//                           <DynPage>
//                                <Id>{0}</Id>
//                                <Title>{1}</Title>
//                                <PageName>{2}</PageName>
//                                <Body>{3}</Body>
//                           </DynPage>",
//                        page.PageId, page.Title, page.PageName, page.Body);

//                    }
//                }
//                XMLText.AppendFormat(@"</DynPageList>");