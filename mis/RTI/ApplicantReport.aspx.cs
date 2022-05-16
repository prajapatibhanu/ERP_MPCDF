using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class mis_RTI_ApplicantReport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ds = objdb.ByProcedure("SpRtiApplication",
                    new string[] { "flag" },
                    new string[] { "8" }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/mis/RTI/ApplicantReportRpt.rdlc");

                ReportDataSource datasource = new ReportDataSource("dsRtiApplication", ds.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }

        }
    }
}