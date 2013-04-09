using System.Web;
using System.Web.UI;

namespace Kids.Utility.WebMessageBox
{
    public enum MessageBoxType
    {
        Information = 1,
        Warning = 2,
        Error = 3
    }
    public static class MessageBoxHelper
    {
        public static void ShowMessageBox(Control p, string Message, string BoxTitle, MessageBoxType type)
        {
            Message = Message.Replace("\r\n", "");
            Message = Message.Replace("<BR>", "{BR}");
            Message = Message.Replace("<", "{START}");
            Message = Message.Replace(">", "{END}");
            Message = Message.Replace("\"", "{Q}");


            string ImagePath = string.Format(p.ResolveUrl("~/App_Themes/MessageBox/{0}.png"), type.GetEnumName());

            string script = string.Format(@"<script type=""text/javascript"">
                                 ShowJQMessageBox(' <fieldset style=""direction:rtl;width:400px;max-width:600px;min-width: 300px;"" align=""center""><legend><img  align=""middle"" src=""{0}"" Width=""32"" />{1} </legend>  <span style=""direction:rtl;text-align:right"">{2}</span></fieldset>');
                               </script>", ImagePath, BoxTitle, HttpContext.Current.Server.HtmlEncode(Message));



            script = script.Replace("{BR}", "<BR>");
            script = script.Replace("{START}", "<");
            script = script.Replace("{END}", ">");
            script = script.Replace("{Q}", "\"");

            ScriptManager.RegisterClientScriptBlock(p, p.GetType(), "message", script, false);

        }


    }
}
