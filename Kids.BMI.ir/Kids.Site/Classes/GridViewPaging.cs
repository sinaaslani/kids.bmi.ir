using System.Web.UI.WebControls;

namespace Site.Kids.bmi.ir.Classes
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

    public class GridViewFiller
    {
        //these fields are for storing the cute little datatable and the virtualitemcount
        private object _dt;
        private string _id;
        private int _VirtualItemCount;
        private GridView _gv;
        public void PagingGridView(GridView gv, object dt, int VirtualItemCount)
        {
            _dt = dt;
            _VirtualItemCount = VirtualItemCount;
            _gv = gv;
            _id = gv.ID;
            FillGridView();
        }


        private void FillGridView()
        {

            ObjectDataSource ods = new ObjectDataSource
                                       {
                                           ID = "ods" + _id,
                                           EnablePaging = _gv.AllowPaging,
                                           TypeName = "Site.Kids.bmi.ir.Classes.TableAdapter",
                                           SelectMethod = "GetData",
                                           SelectCountMethod = "VirtualItemCount",
                                           StartRowIndexParameterName = "startRow",
                                           MaximumRowsParameterName = "maxRows",
                                           EnableViewState = false
                                       };

            ods.ObjectCreating += new ObjectDataSourceObjectEventHandler(ods_ObjectCreating);

            _gv.DataSource = ods;
            _gv.DataBind();
        }

        private void ods_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = new TableAdapter(_dt, _VirtualItemCount);
        }
    }

}