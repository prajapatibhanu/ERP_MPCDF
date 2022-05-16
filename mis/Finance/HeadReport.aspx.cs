﻿using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;


public partial class mis_Finance_HeadReport : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ds = objdb.ByProcedure("SpFinHeadMaster",
                    new string[] { "flag" },
                    new string[] { "4" }, "dataset");


            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/mis/Finance/HeadReportRpt.rdlc");

                ReportDataSource datasource = new ReportDataSource("dsFinHeadMaster", ds.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }

        }
    }
}