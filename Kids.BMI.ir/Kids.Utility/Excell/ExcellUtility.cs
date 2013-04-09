using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kids.Utility.Excell
{
    public class ExcellUtility
    {
        private static OleDbConnection ReturnConnection(string fileName, bool IsFirstRowHeader)
        {
            string cnnStr;
            if (IsFirstRowHeader)
            {
                cnnStr =
                    string.Format(
                        "Provider=Microsoft.JET.OLEDB.4.0;{0}; Jet OLEDB:Engine Type=5;Extended Properties=\"Excel 4.0;HDR=YES;IMEX=1\"",
                        fileName);
            }
            else
            {
                cnnStr =
                    string.Format(
                        "Provider=Microsoft.Jet.OLEDB.4.0;{0}; Jet OLEDB:Engine Type=5;Extended Properties=\"Excel 12.0;HDR=NO\"",
                        fileName);
            }
            return new OleDbConnection(cnnStr);
        }

        public static DataTable LoadFromFile(string fileName, string SheetName, bool IsFirstRowHeader)
        {
            OleDbConnection conn = ReturnConnection(fileName, IsFirstRowHeader);
            try
            {
                conn.Open();

                OleDbDataAdapter SheetAdapter = new OleDbDataAdapter("select * from [" + SheetName + "$]", conn);
                DataTable SheetData = new DataTable();
                SheetAdapter.Fill(SheetData);
                return SheetData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }



        public static void TOExcel(HttpResponse Response, IList DataSource, string FileName)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", FileName));
            Response.ContentEncoding = Encoding.UTF8;
            Response.Charset = "";

            // If you want the option to open the Excel file without saving than
            // comment out the line below
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "excel/ms-excel";
            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            DataGrid grid = new DataGrid();
            grid.HeaderStyle.Font.Bold = true;
            grid.AllowPaging = false;
            grid.DataSource = DataSource;
            grid.DataBind();

            grid.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        public static void TOExcel(HttpResponse Response, Control grid, string FileName)
        {
            bool VisibleFlag = false;

            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", FileName));
            Response.ContentEncoding = Encoding.UTF8;
            Response.Charset = "";

            // If you want the option to open the Excel file without saving than
            // comment out the line below
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "excel/ms-excel";// "application/vnd.ms-excel";
            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            //save State
            //Control ctrl_Parent = null;
            //if (grid.Parent != null)
            //{
            //    ctrl_Parent = grid.Parent;
            //    ctrl_Parent.Controls.Remove(grid);
            //}
            if (!grid.Visible)
            {
                grid.Visible = true;
                VisibleFlag = true;
            }
            ////////////////////////////////
            Panel pnl = new Panel();
            pnl.Controls.Add(grid);
            pnl.Direction = ContentDirection.RightToLeft;
            pnl.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.Flush();
            Response.End();

            //////restore State////////////////////////////////////////////Z//

            //if (ctrl_Parent != null)
            //    ctrl_Parent.Controls.Add(grid.Parent);

            if (VisibleFlag)
                grid.Visible = false;
        }
    }
}