using System;
using Microsoft.Reporting.WebForms;
using System.Data;

public partial class mis_Warehouse_WarehouseItemWiseRpt : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ds = objdb.ByProcedure("Proc_tblSpItemStock", new string[] { "flag" }, new string[] { "12" }, "dataset");

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/mis/Warehouse/ItemWiseReport.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
    }
}