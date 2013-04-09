using System;
using System.Collections.Generic;
using Kids.Common;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Masters
{
    public partial class ucMenuSide : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
//            if (!IsPostBack)
//            {
//                int RecourdCount;

//                IEnumerable<DynamicPage> Pages = DynamicPages_DataProvider.GetDynamicPage(out RecourdCount, PageTypeIds: Configs.DynamicPageTypesInRightMenu);

//                foreach (DynamicPage page in Pages)
//                {
//                    mnuContent.Text += string.Format(@" <tr>
//                                                            <td valign='middle' align='right' style='background-image: url(/App_Themes/Default/images/Menu/menu1.png);
//                                                                width: 220px; height: 38px; background-repeat: no-repeat; padding-right: 12px'>
//                                                               <a href='{0}'> {1}</a>
//                                                            </td>
//                                                        </tr>",
//                                                        ResolveClientUrl(string.Format("~/{0}.aspx", page.PageName)), page.PageName
//                                                    );
//                }

//            }

        }
    }
}