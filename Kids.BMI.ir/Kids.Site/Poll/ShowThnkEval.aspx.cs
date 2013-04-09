using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Poll
{
    public partial class ShowThnkEval : FormBaseClass
    {
        private const string coockiPrefix = "KbmpS";
        protected void Page_Load(object sender, EventArgs e)
        {
            long? formId = null;
            if (UtilityMethod.GetRequestParameter("frmId").IsInt64())
                formId = Convert.ToInt64(UtilityMethod.GetRequestParameter("frmId"));



            if (!Page.IsPostBack)
            {

                QuestionnaireForm qForm = QuestionnaireForm_DataProvider.GetQuestionnaireForm(formId).FirstOrDefault();
                if (qForm == null || qForm.Status != (int)QuestionnaireStatusType.Confirmed)
                {
                    lblMessage.Text = "مقادیر ارسالی اشتباه میباشد";
                }
                Page.Title = qForm.Title;


                questionnaireFormLbl.Text = qForm.Title;


                qFormDate.Text = PersianDateTime.MiladiToPersian(qForm.FormDate).ToShortDateString();
                qFormIntroductionLbl.Text = qForm.Introduction;

                bool availableCookie = false;
                string cookName = coockiPrefix + formId;
                if (Request.Cookies.Get(cookName) != null)
                    availableCookie = true;
                if (availableCookie)
                {
                    dgQuestions.Visible = false;
                    lblMessage.Text = "كاربر گرامي شما قبلا به اين پرسشنامه پاسخ داده ايد. لذا امكان پاسخ دهي مجدد براي شما مقدور نمي باشد ";
                    SendBtn.Visible = false;
                    cancelBtn.Visible = false;
                    return;
                }

                List<QuestionnaireForm_Questions> questionList = qForm.QuestionnaireForm_Questions.ToList();
                questionCountLbl.Text = questionList.Count.ToString();
                ShowQuestionList(questionList);
             
            }
        }

        int qNo = 0;
        protected void dgQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                qNo++;
                QuestionnaireForm_Questions q = (QuestionnaireForm_Questions)e.Row.DataItem;

                HiddenField qHidden = e.Row.FindControl("hdQuestionId") as HiddenField;
                qHidden.Value = q.QuestionId.ToString();

                Label qlbl = e.Row.FindControl("lblQuestion") as Label;
                qlbl.Text = qNo.ToString() + ") " + q.Title;
                qlbl.CssClass = "normaltextSmall";

                Control ctrl = ResponseItemsTable.ShowUserResponseItem(q);
                e.Row.Cells[0].Controls.Add(ctrl);
            }
        }


        private void ShowQuestionList(List<QuestionnaireForm_Questions> questionList)
        {
            dgQuestions.DataSource = questionList;
            dgQuestions.DataBind();
        }

        //public static Control ShowUserResponseItem(QuestionnaireForm_Questions q)
        //{

        //    switch ((ResponseItemsType)q.ItemsType)
        //    {
        //        case ResponseItemsType.CheckBox:
        //            CheckBoxList ChkList = new CheckBoxList();
        //            foreach (QuestionnaireForms_ResponseItems Item in q.QuestionnaireForms_ResponseItems)
        //            {
        //                CheckBox chkbox = new CheckBox();
        //                chkbox.ID = Item.ItemId.ToString();
        //                chkbox.Text = Item.ItemText;
        //                chkbox.Checked = false;
        //                ChkList.Controls.Add(chkbox);

        //                if (Item.HasInputText)
        //                {
        //                    TextBox inputBox = new TextBox();
        //                    inputBox.TextMode = TextBoxMode.MultiLine;
        //                    inputBox.Rows = 2;
        //                    inputBox.Width = Unit.Pixel(330);
        //                    inputBox.ID = "txt" + Item.ItemId;
        //                    Label br = new Label();
        //                    br.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        //                    ChkList.Controls.Add(br);
        //                    ChkList.Controls.Add(inputBox);
        //                }
        //            }
        //            return ChkList as Control;

        //        case ResponseItemsType.RadioButton:
        //            return null;
        //        case ResponseItemsType.InputText:
        //            return null;
        //        default:
        //            throw new NotImplementedException();
        //    }

        //}




        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("thnkEvalList.aspx");
        }


        protected void SendBtn_Click(object sender, EventArgs e)
        {

            long userRandId = DateTime.Now.Ticks;

            foreach (GridViewRow QuestionRow in dgQuestions.Rows)
            {

                string idPrefix = QuestionRow.UniqueID + this.IdSeparator;

                HiddenField qHidden = QuestionRow.FindControl("hdQuestionId") as HiddenField;
                long questionId = Convert.ToInt64(qHidden.Value);


                QuestionnaireForm_Questions question = QuestionnaireForm_DataProvider.GetQuestion(questionId).FirstOrDefault();
                if (question.ItemsType == (int)ResponseItemsType.CheckBox)
                {
                    for (int j = 0; j < question.QuestionnaireForms_ResponseItems.Count; j++)
                    {
                        QuestionnaireForms_ResponseItems ritem = question.QuestionnaireForms_ResponseItems[j];
                        if (Page.Request.Form[idPrefix + ritem.ItemId] != null && Page.Request.Form[idPrefix + ritem.ItemId].ToLower() == "on")
                        {
                            ritem.ItemValue = ritem.ItemText;
                            if (ritem.HasInputText)
                            {
                                string itemTextBox = Page.Request.Form[idPrefix + "txt" + ritem.ItemId];
                                if (itemTextBox.Trim().Length > 0)
                                    ritem.ItemValue = itemTextBox.Trim();
                            }
                            QuestionnaireForm_UserResponses resp = new QuestionnaireForm_UserResponses
                            {
                                FormId = question.QuestionnaireForm.FormId,
                                QuestionId = questionId,
                                ItemId = ritem.ItemId,
                                ItemValue = ritem.ItemValue,
                                UserRandomId = userRandId
                            };
                            QuestionnaireForm_DataProvider.SaveUserResponse(resp);
                        }
                    }
                }
                else if (question.ItemsType == (int)ResponseItemsType.RadioButton)
                {
                    long selectedItemId = Convert.ToInt64(Page.Request.Form[idPrefix + "qroup" + questionId]);
                    //Page.Response.Write("<br> itemRadio : " + selectedItemId);
                    QuestionnaireForms_ResponseItems selectedItem = QuestionnaireForm_DataProvider.GetQuestionnaireForms_ResponseItem(selectedItemId).FirstOrDefault();
                    if (selectedItem != null)
                    {
                        selectedItem.ItemValue = selectedItem.ItemText;
                        if (selectedItem.HasInputText)
                        {
                            string itemTextBox = Page.Request.Form[idPrefix + "txt" + selectedItemId.ToString()];
                            if (itemTextBox.Trim().Length > 0)
                                selectedItem.ItemValue = itemTextBox.Trim();
                        }

                        QuestionnaireForm_UserResponses resp = new QuestionnaireForm_UserResponses
                        {
                            FormId = question.QuestionnaireForm.FormId,
                            QuestionId = questionId,
                            ItemId = selectedItem.ItemId,
                            ItemValue = selectedItem.ItemValue,
                            UserRandomId = userRandId
                        };
                        QuestionnaireForm_DataProvider.SaveUserResponse(resp);
                    }
                }
                else if (question.ItemsType ==(int) ResponseItemsType.InputText)
                {
                    for (int j = 0; j < question.QuestionnaireForm_UserResponses.Count; j++)
                    {
                        QuestionnaireForm_UserResponses ritem = question.QuestionnaireForm_UserResponses[j];
                        string itemTextBox = Page.Request.Form[idPrefix + ritem.ItemId];
                        ritem.ItemValue = itemTextBox;

                        QuestionnaireForm_UserResponses resp = new QuestionnaireForm_UserResponses
                        {
                            FormId = question.QuestionnaireForm.FormId,
                            QuestionId = questionId,
                            ItemId = ritem.Serial,
                            ItemValue = ritem.ItemValue,
                            UserRandomId = userRandId
                        };

                        QuestionnaireForm_DataProvider.SaveUserResponse(resp);
                    }
                }
            }

            lblMessage.Text = "پاسخ شما به فرم زير با موفقيت ثبت شد. از توجه شما متشكريم.";
            SendBtn.Visible = false;
            cancelBtn.Visible = false;
            //Page.Response.Redirect(Globals.UrlThnkEvalList);

            long formId = -1;
            if (UtilityMethod.GetRequestParameter( "frmId").IsInt64())
                formId = UtilityMethod.GetRequestParameter( "frmId").ToLong();
            HttpCookie cook = new HttpCookie(coockiPrefix + formId)
                {
                    Expires = DateTime.MaxValue,
                    Value = userRandId.ToString()
                };
            Response.Cookies.Add(cook);

        }
    }
}