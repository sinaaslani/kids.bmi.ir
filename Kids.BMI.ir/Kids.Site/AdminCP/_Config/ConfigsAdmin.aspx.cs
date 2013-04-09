using System;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.Common;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP._Config
{
    public partial class ConfigsAdmin : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Bind();
        }

        private void Bind()
        {
            dgConfigs.DataSource = Config_DataProvider.GetConfig();
            dgConfigs.DataBind();
        }

        protected void dgConfigs_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgConfigs.EditIndex = e.NewEditIndex;
            Bind();
        }

        protected void dgConfigs_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var confName = dgConfigs.DataKeys[dgConfigs.EditIndex].Value.ToString();
            var c = string.IsNullOrWhiteSpace(confName) ? new Config() : Config_DataProvider.GetConfig().FirstOrDefault();

            c.ConfigName = e.NewValues["ConfigName"].ToString();
            c.ConfigValue = e.NewValues["ConfigValue"].ToString();
            Config_DataProvider.SaveConfig(c);
            Config_DataProvider.ClearCacheConfig();

            dgConfigs.EditIndex = -1;
            Bind();
        }

        protected void lnkAddNewConfig_Click(object sender, EventArgs e)
        {
            var src = Config_DataProvider.GetConfig();
            src.Insert(0, new Config { ConfigName = "", ConfigValue = "" });
            dgConfigs.DataSource = src;

            dgConfigs.EditIndex = 0;
            dgConfigs.DataBind();
        }

        protected void dgConfigs_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgConfigs.EditIndex = -1;
            Bind();
        }

        protected void dgConfigs_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var c = Config_DataProvider.GetConfig(e.Keys["ConfigName"].ToString()).FirstOrDefault();
            if (c != null)
                Config_DataProvider.DeleteConfig(c);
            Bind();
        }




    }
}