using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Web.Script.Services;
using System.Web.Services;
using BMISSOClientHelper;
using BMISSOClientHelper.BMISSOService;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.EntitiesModel.Scores;
using Kids.LoggingHelper;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.Scores;

namespace Site.Kids.bmi.ir.KidsGame
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class ScoreService : WebService
    {
        [WebMethod(EnableSession = true)]
        public void AddGameScore(int GameId, int ScoreId, long Value)
        {
            try
            {
                var u = new KidsSecureServiceBaseClass();

                if (u.OnlineKidsUser == null || u.OnlineKidsUser.Kids_UserInfo == null)
                    throw new ApplicationException("User not found");
                var user = u.OnlineKidsUser.Kids_UserInfo;

                SaveScore(GameId, ScoreId, Value, user);
            }
            catch (Exception ex)
            {
                LogUtility.WriteEntryEventLog("AddGameScore_WebService", ex, EventLogEntryType.Information);
                throw;
            }
        }

        [WebMethod(EnableSession = true)]
        public void AddGameScoreForTempUser(string TempUserId, int GameId, int ScoreId, long Value)
        {
            try
            {
                if (TempUserMapperManager.Instance[TempUserId] == null)
                    throw new ApplicationException("User not found");
                var user = TempUserMapperManager.Instance[TempUserId].User;

                SaveScore(GameId, ScoreId, Value, user);
            }
            catch (Exception ex)
            {
                LogUtility.WriteEntryEventLog("AddGameScore_WebService", ex, EventLogEntryType.Information);
                throw;
            }
        }

        private static void SaveScore(int GameId, int ScoreId, long Value, KidsUser user)
        {
            Game g = Game_DataProvider.GetGame(GameId).FirstOrDefault();
            if (g == null)
                throw new ApplicationException("Game not found");

            if (g.ScoreTypes.All(o => o.Id != ScoreId))
                throw new ApplicationException("Score not found or this score is not for this game");

            ScoreType scoreType = g.ScoreTypes.First(o => o.Id == ScoreId);

            if (scoreType.CoefficentValue < 0)
            {
                List<scoreListItem> DailyscoreList, MonthlyscoreList;
                var score = ScoreHelper.CalculateScore(user, true, out DailyscoreList,out MonthlyscoreList);
                if (score + (Value * scoreType.CoefficentValue) < 0)
                    throw new ApplicationException("there is not enoght score for use!Current Score Is :" + score);
            }
            
            ScoreHelper.AddScore(user, scoreType, Value);
        }


        [WebMethod(EnableSession = true)]
        public TempUser IsValidUser(string TempUserId)
        {
            try
            {
                var user = TempUserMapperManager.Instance[TempUserId];
                if (user != null)
                {
                    return new TempUser { Name = user.User.ChildName, Family = user.User.ChildFamily, Sex = user.User.ChildSex, };
                }
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.WriteEntryEventLog("IsValidUser_WebService", ex, EventLogEntryType.Information);
                throw;
            }
        }

        [WebMethod(EnableSession = true)]
        public bool KeepAliveTempUser(string TempUserId)
        {
            try
            {
                var userwrapper = TempUserMapperManager.Instance[TempUserId];
                if (userwrapper != null)
                {
                    userwrapper.CreateDateTime = DateTime.Now;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.WriteEntryEventLog("KeepAliveTempUser_WebService", ex, EventLogEntryType.Information);
                throw;
            }
        }

    }

    public class KidsSecureServiceBaseClass
    {
        public OnlineKidsUserInfo OnlineKidsUser { get; private set; }

        private static readonly string SSOUserName = SystemConfigs.SSOUserName;
        private static readonly string SSOPassword = SystemConfigs.SSOPassword;
        private static readonly string SSOWebSiteURL = SystemConfigs.SSOWebSiteURL;
        private static readonly string SSOWebServiceURL = SystemConfigs.SSOWebServiceURL;

        private readonly static SSOClientHelper BMIUserInteraction = new SSOClientHelper(SSOUserName, SSOPassword, SSOWebSiteURL, SSOWebServiceURL);



        public KidsSecureServiceBaseClass()
        {
            OnlineKidsUser = SetUserInfo();

        }

        private OnlineKidsUserInfo SetUserInfo()
        {
            OnlineKidsUserInfo outuser;
            if (!SSOClientHelper.IsBMIUserLoggedIn(out outuser, GetKidsUser, false, Serializaer, DeSerializaer))
                throw new SecurityException("Invalid User Info 1");

            return outuser;
        }

        private static OnlineKidsUserInfo GetKidsUser(UserProfile userinfo, bool RefreshSSOUser)
        {
            try
            {
                KidsUser u = KidsUser_DataProvider.GetKidsUser(SSOUserName: userinfo.UserID).FirstOrDefault();

                if (u == null)
                    throw new ApplicationException("Invalid User");

                if (RefreshSSOUser)
                    userinfo = BMIUserInteraction.GetBMIUserProfile(userinfo.UserID);

                return new OnlineKidsUserInfo { Kids_UserInfo = u, SSOUser = userinfo };
            }
            catch (ApplicationException)
            {
                throw new SecurityException("Invalid User Info 2");
            }
        }

        private string Serializaer(OnlineKidsUserInfo user)
        {
            return SerializeHelper.DataContract_ToString(user);
        }

        private OnlineKidsUserInfo DeSerializaer(string serialuser)
        {
            return SerializeHelper.DataContract_ToObject<OnlineKidsUserInfo>(serialuser);
        }
    }

}
