using System;
using System.IO;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.KidsGame
{
    public partial class GameFileManager : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["lid"].IsGuid())
            {
                var urlInfo = TempLinkManager.Instanse[UtilityMethod.GetRequestParameter("lid", Guid.NewGuid().ToString()).ToGuid()];
                var FileContent = File.ReadAllBytes(urlInfo.RealFilePath);
                Response.Clear();
                Response.ContentType = "application/oc-stream";
                Response.AddHeader("Content-Length", FileContent.Length.ToString());
                Response.BinaryWrite(FileContent);
                Response.Flush();
                Response.End();
            }
        }
    }
}