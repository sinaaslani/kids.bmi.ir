using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.LoggingHelper;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Registration
{
    public partial class KidsUserSateWidget : UserControlBaseClass
    {
        protected string TooltipScript;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public  void SetUserInfo(KidsUser User)
        {
            if (User == null)
                return;

            try
            {
                var States = User.StatusHistory.Split(",".ToCharArray(), StringSplitOptions.None);
                List<KidsUserState> AllStates = KidsUser_DataProvider.GetKidsUserStates();

                foreach (string s in States)
                {
                    Image img = new Image
                        {
                            ID = string.Format("imgState{0}", s),
                            ClientIDMode = ClientIDMode.Static,
                            ImageUrl = "~/App_Themes/Default/Images/KidsUserStatus/Green.png",
                            ImageAlign = ImageAlign.Middle,
                            ToolTip = AllStates.First(o => o.Id == s.ToInt32()).StateName,
                            Width = 24,
                            Height = 24
                        };
                    TooltipScript += string.Format(" $('#{0}').powerTip({{ placement: 'n' }});\r\n", img.ID);


                    Label lbl = new Label {Text = "--->"};
                    StatePlaceHolder.Controls.Add(img);
                    StatePlaceHolder.Controls.Add(lbl);
                }

                var LastItem = AllStates.First(o => o.Id == States.Last().ToInt32());
                lblLastStatus.Text = LastItem.StateName;

                if (LastItem.NextId.HasValue)
                {
                    Image imgLast = new Image
                        {
                            ID = string.Format("imgState{0}", LastItem.NextId),
                            ClientIDMode = ClientIDMode.Static,
                            ImageUrl = "~/App_Themes/Default/Images/KidsUserStatus/Gray.png",
                            ToolTip = AllStates.First(o => o.Id == LastItem.NextId).StateName,
                            ImageAlign = ImageAlign.Middle,
                            Width = 24,
                            Height = 24
                        };
                    TooltipScript += string.Format(" $('#{0}').powerTip({{ placement: 'n' }});\r\n", imgLast.ID);
                    StatePlaceHolder.Controls.Add(imgLast);
                }
                else
                {
                    StatePlaceHolder.Controls.RemoveAt(StatePlaceHolder.Controls.Count - 1);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteEntryEventLog("KidsUserSateWidget", ex, EventLogEntryType.Error);
            }
        }
    }
}