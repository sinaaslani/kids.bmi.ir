using System;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.BankStory
{
    public partial class _BankStoryExam : KidsSecureFormBaseClass
    {
        private BankStoryExam _CurrentExam;
        private BankStoryExam CurrentExam
        {
            get
            {
                return _CurrentExam ??
                       (_CurrentExam = SerializeHelper.DataContract_ToObject<BankStoryExam>(ViewState["CurrentExam"].ToString()));
            }
            set
            {
                _CurrentExam = value;
                ViewState["CurrentExam"] = SerializeHelper.DataContract_ToString(_CurrentExam);
            }
        }


        private DateTime? ExamStartTime
        {
            get
            {
                return ViewState["ExamStartTime"] == null ? null : (DateTime?)ViewState["ExamStartTime"];
            }
            set
            {
                ViewState["ExamStartTime"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["exid"].IsInt32())
                {
                    int? exid = Request["exid"].ToInt32();
                    if (OnlineKidsUser.Kids_UserInfo.KidsUser_BankStoryExam.Any(o => o.ExamId == exid))
                    {
                        ShowMessageBox("کاربر عزیز :<BR>شما قبلا در این آزمون شرکت کرده اید ", "",
                                       MessageBoxType.Information);
                        return;
                    }
                    CurrentExam = BankStory_DataProvider.GetExams(exid).FirstOrDefault();

                    if (CurrentExam != null)
                    {
                        ExamStartTime = DateTime.Now;
                        lblExamTimer.Text = string.Format("مدت زمان باقی مانده :{0}:00", CurrentExam.DurationTime.ToString().PadLeft(2, '0'));
                        BindExam();
                        pnlMain.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("~/Exam.aspx");
                    return;
                }



            }
        }

        private void BindExam()
        {
            if (Request["exid"].IsInt32())
            {
                if (CurrentExam != null)
                {
                    dgExamQuestion.DataSource = CurrentExam.BankStoryExam_Question;
                    dgExamQuestion.DataBind();
                }
            }
        }



        protected override void CheckKidsUser()
        {

        }
        

        protected void ExamTimer_Tick(object sender, EventArgs e)
        {
            if (ExamStartTime != null)
            {
                var TimeLapsed = DateTime.Now.Subtract(ExamStartTime.Value);
                if (TimeLapsed.TotalMinutes < CurrentExam.DurationTime)
                    lblExamTimer.Text = string.Format("مدت زمان باقی مانده : {0}:{1}",
                                                      (CurrentExam.DurationTime - TimeLapsed.Minutes - 1).ToString()
                                                                                                     .PadLeft(2, '0'),
                                                      (60 - TimeLapsed.Seconds).ToString().PadLeft(2, '0')
                        );
                else
                {
                    lblExamTimer.Text = "مدت امتحان به پایان رسیده است";
                    SaveExamResult();
                }
            }
        }

        protected void btnFinishExam_Click(object sender, EventArgs e)
        {
            SaveExamResult();
        }

        private void SaveExamResult()
        {
            ExamTimer.Enabled = false;
            int CorrectAnswer = 0, InCorrectAnswer = 0, NoAnswer = 0;
            foreach (GridViewRow item in dgExamQuestion.Rows)
            {
                var QuestionId = (item.FindControl("QuestionId") as HiddenField).Value.ToInt32();
                var A = (item.FindControl("rdoAnswerA") as RadioButton).Checked;
                var B = (item.FindControl("rdoAnswerB") as RadioButton).Checked;
                var C = (item.FindControl("rdoAnswerC") as RadioButton).Checked;
                var D = (item.FindControl("rdoAnswerD") as RadioButton).Checked;

                var UserAnswer = GetAnswer(A, B, C, D);
                var q = CurrentExam.BankStoryExam_Question.FirstOrDefault(o => o.QuestionId == QuestionId);

                if (UserAnswer == -1)
                    NoAnswer++;
                else if (q.Answer == UserAnswer)
                    CorrectAnswer++;
                else
                    InCorrectAnswer++;
            }

            try
            {
                var user = OnlineKidsUser.Kids_UserInfo;
                var UserExam = new KidsUser_BankStoryExam
                    {
                        CorrectAnswer = CorrectAnswer,
                        InCorrectAnswer = InCorrectAnswer,
                        NoAnswer = NoAnswer,
                        ExamId = CurrentExam.ExamId,
                        CalculatedScore = CorrectAnswer / (CorrectAnswer + InCorrectAnswer + NoAnswer),
                        CreateDateTime = DateTime.Now
                    };
                user.KidsUser_BankStoryExam.Add(UserExam);
                KidsUser_DataProvider.SaveKidsUser(user, this, RefreshOnlineKidsUserInfo);

                ShowExamResult(UserExam);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PK_KidsUser_BankStoryExam"))
                {
                    RefreshOnlineKidsUserInfo();
                    ShowMessageBox("نتیجه آزمون شما قبلا ثبت شده است");
                    ShowExamResult(OnlineKidsUser.Kids_UserInfo.KidsUser_BankStoryExam.FirstOrDefault());
                }
            }
        }

        private void ShowExamResult(KidsUser_BankStoryExam UserExam)
        {
            pnlDetails.Visible = true;
            pnlMain.Visible = false;

            lblCorrectCount.Text = UserExam.CorrectAnswer.ToString();
            lblInCorectCount.Text = UserExam.InCorrectAnswer.ToString();
            lblNoAnswerCount.Text = UserExam.NoAnswer.ToString();
            lblScore.Text = UserExam.CalculatedScore.ToString();
        }

        private int GetAnswer(bool A, bool B, bool C, bool D)
        {
            if (A)
                return 1;
            if (B)
                return 2;
            if (C)
                return 3;
            if (D)
                return 4;
            return -1;
        }
    }
}