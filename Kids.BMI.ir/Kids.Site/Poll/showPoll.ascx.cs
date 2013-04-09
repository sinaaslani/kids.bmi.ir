using System.Linq;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Site.Kids.bmi.ir.Poll
{
    public partial class showPoll : UserControlBaseClass
    {
        private const string coockiPrefix = "Kbmp";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PollQuestion q = Poll_DataProvider.GetPoll().FirstOrDefault();
                if (q == null)
                {
                    Visible = false;
                    return;
                }

                questionLbl.Text = q.Title;

                //string UserIp = Page.Request.UserHostAddress;
                bool availablePollResponse = false;

                string cookName = coockiPrefix + q.QuestionId;
                if (Request.Cookies[cookName] != null)
                    availablePollResponse = true;


                if (!availablePollResponse)
                {
                    foreach (PollResponseItem ri in q.PollResponseItems)
                        rdoPollList.Items.Add(new ListItem(ri.ItemText, ri.ItemId.ToString()));

                    tdQuestions.Visible = true;
                    tdActions.Visible = true;
                    tdResult.Visible = false;
                }
                else
                {
                    tdQuestions.Visible = false;
                    tdActions.Visible = false;
                    tdResult.Visible = true;
                    showPostResult(q);
                }

            }
        }

        protected void sendResBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rdoPollList.SelectedValue))
            {
                PollQuestion question = Poll_DataProvider.GetPoll().FirstOrDefault();
                if (question == null)
                {
                    Visible = false;
                    return;
                }

                if (question.HasScore && KidsOnlineUser == null || KidsOnlineUser.Kids_UserInfo == null)
                {
                    Response.Redirect("~/ورود.aspx");
                    return;
                }

                string UserIp = Request.UserHostAddress;

                long selectedItemId = Convert.ToInt64(rdoPollList.SelectedValue);
                PollResponseItem selectedItem = question.PollResponseItems.FirstOrDefault(o => o.ItemId == selectedItemId);

                if (selectedItem != null)
                {
                    //selectedItem.ItemValue = selectedItem.ItemText;

                    string cookName = coockiPrefix + question.QuestionId;
                    if (Request.Cookies.Get(cookName) != null)
                        errorMsgLbl.Text = "شما و يا کاربري ديگري از پشت فايروال شما قبلا به اين سوال پاسخ داده است ";
                    else
                    {
                        long? KidsUserId=null;
                        if(KidsOnlineUser!=null && KidsOnlineUser.Kids_UserInfo!=null)
                            KidsUserId=KidsOnlineUser.Kids_UserInfo.KidsUserId;
                        PollUserResponse UserResp = new PollUserResponse { QuestionId = question.QuestionId, UserIp = UserIp, ResponseItemId = selectedItemId, KidsUserId = KidsUserId, CreateDateTime = DateTime.Now };
                        Poll_DataProvider.SavePollResponse(UserResp);
                        

                        HttpCookie cook = new HttpCookie(coockiPrefix + question.QuestionId)
                            {
                                Expires = DateTime.MaxValue,
                                Value = selectedItemId.ToString()
                            };
                        Response.Cookies.Add(cook);
                        rdoPollList.Visible = false;
                        sendResBtn.Visible = false;
                        errorMsgLbl.Visible = false;
                        showPostResult(question);
                    }
                }
                else
                {
                    errorMsgLbl.Visible = true;
                }

            }
        }

        private void showPostResult(PollQuestion qu)
        {
            if (qu != null)
            {
                string cookName = coockiPrefix + qu.QuestionId;
                if (Request.Cookies[cookName] != null)
                {
                    try
                    {
                        tdResult.Visible = true;
                        HttpCookie cook = Request.Cookies[cookName];

                        string QuestionResponse = qu.PollResponseItems.FirstOrDefault(o => o.ItemId == Convert.ToInt32(cook.Value)).ItemText;


                        lblrseult.CssClass = "normalTextSmall";
                        lblrseult.Text = string.Format("  پاسخ شما به سوال فوق گزينه  <font color=blue>{0}</font> مي باشد", QuestionResponse);
                        //tblrseult.Text += "</font>" ;
                        if (qu.UsersCanViewResult)
                        {
                            ucShowPollResults.ActiveQuestion = qu;
                            ucShowPollResults.Visible = true;
                        }
                    }
                    catch
                    {
                        lblrseult.Text += "<br><font class='normalTextSmall' >پاسخ شما به اين سوال مشخص نيست";
                        lblrseult.Text += "</font>";

                    }

                }

            }



        }

    }
}
