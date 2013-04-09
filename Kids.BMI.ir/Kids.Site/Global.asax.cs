using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Kids.Common;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.Payment;
using Site.Kids.bmi.ir.Scores;

namespace Site.Kids.bmi.ir
{
    public class Global : HttpApplication
    {
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string PagePath;
            if (Request.Url.ToString().Contains("WebResource.axd"))
                PagePath = "~/WebResource.axd";
            else if (Request.Url.ToString().Contains("ScriptResource.axd"))
                PagePath = "~/ScriptResource.axd";
            else
                return;

            for (int i = 0; i < WebResourceResponseFilter.ReplacementExpression.Length; i++)
            {
                if (Request.Url.ToString().Contains(WebResourceResponseFilter.ReplacementExpression[i]))
                {
                    var q = Server.UrlDecode(Request.Url.Query).Replace(
                                                                        WebResourceResponseFilter.ReplacementExpression[i],
                                                                        WebResourceResponseFilter.FilterExpression[i]
                                                                        );
                    HttpContext.Current.RewritePath(PagePath + q);
                }
        }

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);

            RunCheckStatus();

            RunScoreCalculator();
        }

        static readonly object RunScoreCalculator_LockOBJ = new object();
        private void RunScoreCalculator()
        {

            Task t = Application["ScoreCalculatorTask"] as Task;
            if (t == null || t.Status != TaskStatus.Running)
            {
                lock (RunScoreCalculator_LockOBJ)
                {
                    if (t == null || t.Status != TaskStatus.Running)
                    {
                        t = Task.Factory.StartNew(ScoreCalculator.Instance(HttpContext.Current).Run);
                        Application["ScoreCalculatorTask"] = t;
                    }
                }
                Application.UnLock();
            }
        }

        static readonly object RunCheckStatus_LockOBJ = new object();
        private void RunCheckStatus()
        {

            Task t = Application["CheckStatusTask"] as Task;
            if (t == null || t.Status != TaskStatus.Running)
            {
                lock (RunCheckStatus_LockOBJ)
                {
                    if (t == null || t.Status != TaskStatus.Running)
                    {
                        t = Task.Factory.StartNew(CheckStatus_Auto.Run);
                        Application["CheckStatusTask"] = t;
                    }
                }
                Application.UnLock();
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            int RecourdCount;
            IEnumerable<DynamicPage> enumerable = DynamicPages_DataProvider.GetDynamicPage(out RecourdCount, PageTypeIds: SystemConfigs.DynamicPageTypesInRightMenu);

            foreach (DynamicPage page in enumerable)
            {
                RouteValueDictionary dictionary2 = new RouteValueDictionary { { "id", page.PageId } };
                RouteValueDictionary defaults = dictionary2;
                routes.MapPageRoute(page.PageId.ToString(), string.Format("{0}.aspx", page.PageName), "~/DynPage/DynamicPage.aspx", false, defaults);
            }


            routes.MapPageRoute("Javaneha", "کانون جوانه ها.aspx", "~/Entry.aspx");
            routes.MapPageRoute("HomeKids", "جزیره آرزوها.aspx", "~/DefaultKids.aspx");
            routes.MapPageRoute("Login", "ورود.aspx", "~/Login.aspx");


            routes.MapPageRoute("Game", "بازی.aspx", "~/KidsGame/GameHome.aspx");

            routes.MapPageRoute("NewsList", "InfoBox.aspx", "~/InfoBox/NewList.aspx");
            routes.MapPageRoute("ShowNews", "News.aspx", "~/InfoBox/ShowNews.aspx");


            routes.MapPageRoute("Poll", "Poll.aspx", "~/Poll/Poll.aspx");

            //routes.MapPageRoute("ShowFAQ", "FAQShow.aspx", "~/Contact/_FAQ/ShowFAQ.aspx");
            routes.MapPageRoute("FAQ", "FAQ.aspx", "~/_FAQ/FAQList.aspx");
            routes.MapPageRoute("ContactUs", "ContactUs.aspx", "~/Contact/ContactUs.aspx");

            routes.MapPageRoute("BankStory", "BankStory.aspx", "~/BankStory/BankStoryHome.aspx");
            routes.MapPageRoute("ExamList", "Exam.aspx", "~/BankStory/BankStoryExamList.aspx");
            routes.MapPageRoute("Exam", "AttendExam.aspx", "~/BankStory/BankStoryExam.aspx");

            routes.MapPageRoute("Registration", "Register.aspx", "~/Registration/Register.aspx");
            routes.MapPageRoute("WishAccRegistration", "RegisterAcc.aspx", "~/Registration/RegisterWishAccount.aspx");

            routes.MapPageRoute("Profile", "Profile.aspx", "~/Registration/Profile.aspx");
            routes.MapPageRoute("KidsBirthday", "KidsBirthday.aspx", "~/Registration/KidsBirthday.aspx");


            routes.MapPageRoute("Invite", "InviteFriend.aspx", "~/Registration/InviteFriend.aspx");
            routes.MapPageRoute("PostalCard", "PostalCardList.aspx", "~/WishAccount/PostalCardList.aspx");

            routes.MapPageRoute("WishAcc", "WishAcc.aspx", "~/WishAccount/WishAccHome.aspx");
            routes.MapPageRoute("GanjTunel", "Tunel.aspx", "~/WishAccount/GanjTunnel.aspx");




        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }
    }
}

