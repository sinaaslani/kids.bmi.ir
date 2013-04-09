using System.Web.UI.WebControls;

namespace Kids.Utility
{
    public class TableAdapter
    {
        private readonly object _dt;
        private readonly int _VirtualItemCount;

        //this is the constructor that takes as parameters a cute little datatable
        // with the right 10 records, an an integer vic = virtualitemcount
        public TableAdapter(object dt, int VirtualItemCount)
        {
            _dt = dt;
            _VirtualItemCount = VirtualItemCount;
        }


        //this returns the total number of records in the table (30.000)
        public int VirtualItemCount()
        {
            return _VirtualItemCount;
        }

        //this returns the datatable (10 records)
        public object GetData()
        {
            return _dt;
        }

        //this also returns the datatable (10 records) but the ODS needs it for paging purposes
        public object GetData(int startRow, int maxRows)
        {
            return _dt;
        }
    }

    public static class GridViewPager
    {
        public static void DoPaging(GridView gv, object dt, int VirtualItemCount)
        {
            ObjectDataSource ods = new ObjectDataSource
                                       {
                                           ID = "ods" + gv.ID,
                                           EnablePaging = gv.AllowPaging,
                                           TypeName = typeof (TableAdapter).FullName,
                                           SelectMethod = "GetData",
                                           SelectCountMethod = "VirtualItemCount",
                                           StartRowIndexParameterName = "startRow",
                                           MaximumRowsParameterName = "maxRows",
                                           EnableViewState = false
                                       };

            ods.ObjectCreating += new ObjectDataSourceObjectEventHandler(
                (sender, e) => { e.ObjectInstance = new TableAdapter(dt, VirtualItemCount); }
                );

            gv.DataSource = ods;
            gv.DataBind();
        }
    }

}