using System.IO;
using System.Threading;

namespace Kids.Utility.Excell
{
    public static class ExcelHelper
    {
        
        public static bool SaveAs(System.Web.HttpResponse Response, string FullFilePath)
        {
            try
            {
                FileInfo myfile = new FileInfo(FullFilePath);

                if (myfile.Exists)
                {
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + myfile.Name);
                    Response.AddHeader("Content-Length", myfile.Length.ToString());
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.WriteFile(myfile.FullName);
                    Response.End();

                }
                return true;
            }
            catch (ThreadAbortException) { return false; }

        }
      
       

      
    }
}


