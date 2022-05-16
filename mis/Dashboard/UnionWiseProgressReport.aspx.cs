using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Web.Services;
using System.Web.Script.Services;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

public partial class mis_Dashboard_UnionWiseProgressReport : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillData();
                //var objects = JObject.Parse(jsonString);
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void FillData()
    {
        StringBuilder sb = new StringBuilder();
        Curr.InnerText = DateTime.Now.ToString("dd/MM/yyyy");
        ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag" }, new string[] { "1" }, "dataset");

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdMCMS.DataSource = ds.Tables[0];
                GrdMCMS.DataBind();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                GrdMarketing.DataSource = ds.Tables[1];
                GrdMarketing.DataBind();
            }
            //if (ds.Tables[2].Rows.Count > 0)
            //{
            //    GrdRMRD.DataSource = ds.Tables[2];
            //    GrdRMRD.DataBind();
            //}
            if (ds.Tables[3].Rows.Count > 0)
            {
                GrdPlantOperation.DataSource = ds.Tables[3];
                GrdPlantOperation.DataBind();
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                GrdFieldOperation.DataSource = ds.Tables[4];
                GrdFieldOperation.DataBind();
            }
            if (ds.Tables[5].Rows.Count > 0)
            {
                GrdFinance.DataSource = ds.Tables[5];
                GrdFinance.DataBind();
            }
            if (ds.Tables[6].Rows.Count > 0)
            {
                GrdPayroll.DataSource = ds.Tables[6];
                GrdPayroll.DataBind();
            } 
            //if (ds.Tables[7].Rows.Count > 0)
            //{
            //    GvTruckSheet.DataSource = ds.Tables[7];
            //    GvTruckSheet.DataBind();
            //}
			if (ds.Tables[8].Rows.Count > 0)
            {
                gvOnlineCollection.DataSource = ds.Tables[8];
                gvOnlineCollection.DataBind();
            }
        }
        string URL = "http://45.114.143.126:8222/WebService.asmx/WS_Office_Wise_Last_Indent_Date";
        URL = URL + "?key=SFA_MPCDFINV";
        var request = (HttpWebRequest)WebRequest.Create(URL);

        request.Method = "GET";

        var response = (HttpWebResponse)request.GetResponse();

        string jsonString = string.Empty;

        using (var stream = response.GetResponseStream())
        {

            using (var sr = new StreamReader(stream))
            {

                jsonString = sr.ReadToEnd();

            }

        }

        DataTable DT = JsonStringToDataTable(jsonString);
        DataTable dtGrd = new DataTable();
        dtGrd.Columns.Add("Office_Name", typeof(string));
        dtGrd.Columns.Add("indent_count", typeof(string));
        dtGrd.Columns.Add("LastDate", typeof(string));
        int Count = DT.Rows.Count;
        for (int i = 0; i < (Count - 5); i++)
        {
            DataRow dr = DT.Rows[i];
            dtGrd.Rows.Add(dr["Office_Name"].ToString(), dr["indent_count"].ToString(), dr["Indent_Date"].ToString());
        }
        GrdInventory.DataSource = dtGrd;
        GrdInventory.DataBind();
    }

    public DataTable JsonStringToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
        List<string> ColumnsName = new List<string>();
        foreach (string jSA in jsonStringArray)
        {
            string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            foreach (string ColumnsNameData in jsonStringData)
            {
                try
                {
                    int idx = ColumnsNameData.IndexOf(":");
                    string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                    if (!ColumnsName.Contains(ColumnsNameString))
                    {
                        ColumnsName.Add(ColumnsNameString);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                }
            }
            break;
        }
        foreach (string AddColumnName in ColumnsName)
        {
            dt.Columns.Add(AddColumnName);
        }
        foreach (string jSA in jsonStringArray)
        {
            string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in RowData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[RowColumns] = RowDataString;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dt.Rows.Add(nr);
        }
        return dt;
    }

    protected void GrdMCMS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            gvMCMSCC.DataSource = null;
            gvMCMSCC.DataBind();
            gvMCMSCC.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView2.Visible = false;
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView3.Visible = false;
            GridView4.DataSource = null;
            GridView4.DataBind();
            GridView4.Visible = false;
            GridView5.DataSource = null;
            GridView5.DataBind();
            GridView5.Visible = false;
            if (e.CommandName == "TotalCC")
            {
                ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag", "Office_Parant_ID" }, new string[] { "2", e.CommandArgument.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    gvMCMSCC.DataSource = ds.Tables[0];
                    gvMCMSCC.DataBind();
                    gvMCMSCC.Visible = true;
                    h5.InnerText = "MCMS Detail";
                    MCMS.InnerText = "Total CC";
                }
            }
            else if (e.CommandName == "TotalGP")
            {
                ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag", "Office_Parant_ID" }, new string[] { "2", e.CommandArgument.ToString() }, "dataset");
                if (ds != null && ds.Tables[1].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[1];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    h5.InnerText = "MCMS Detail";
                    MCMS.InnerText = "Total GatePass";
                }
            }
            else if (e.CommandName == "ReportedCC")
            {
                ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag", "Office_Parant_ID" }, new string[] { "2", e.CommandArgument.ToString() }, "dataset");
                if (ds != null && ds.Tables[2].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[2];
                    GridView2.DataBind();
                    GridView2.Visible = true;
                    h5.InnerText = "MCMS Detail";
                    MCMS.InnerText = "Reported CC";
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myFileModal()", true);
        }
        catch (Exception ex)
        {

        }
    }
    protected void GrdFieldOperation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            gvMCMSCC.DataSource = null;
            gvMCMSCC.DataBind();
            gvMCMSCC.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView2.Visible = false;
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView3.Visible = false;
            GridView4.DataSource = null;
            GridView4.DataBind();
            GridView4.Visible = false;
            GridView5.DataSource = null;
            GridView5.DataBind();
            GridView5.Visible = false;
            if (e.CommandName == "TotalSociety")
            {
                ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag", "Office_Parant_ID" }, new string[] { "3", e.CommandArgument.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView3.DataSource = ds.Tables[0];
                    GridView3.DataBind();
                    GridView3.Visible = true;
                    h5.InnerText = "Field Operation";
                    MCMS.InnerText = "TOTAL SOCIETY";
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myFileModal()", true);
        }
        catch (Exception ex)
        {

        }
    }
    //protected void GrdRMRD_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        GvTruckSheet.DataSource = null;
    //        GvTruckSheet.DataBind();
    //        gvMCMSCC.DataSource = null;
    //        gvMCMSCC.DataBind();
    //        gvMCMSCC.Visible = false;
    //        GridView1.DataSource = null;
    //        GridView1.DataBind();
    //        GridView1.Visible = false;
    //        GridView2.DataSource = null;
    //        GridView2.DataBind();
    //        GridView2.Visible = false;
    //        GridView3.DataSource = null;
    //        GridView3.DataBind();
    //        GridView3.Visible = false;
    //        GridView4.DataSource = null;
    //        GridView4.DataBind();
    //        GridView4.Visible = false;
    //        GridView5.DataSource = null;
    //        GridView5.DataBind();
    //        GridView5.Visible = false;
    //        if (e.CommandName == "RMRDMilkColl")
    //        {
    //            GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
    //            LinkButton lnkTnum = (LinkButton)gvr.FindControl("lblMilkCollection");
    //            Label Date = (Label)gvr.FindControl("lblLastDate");
    //            ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag", "Office_Parant_ID", "EntryDate", "TableNum" },
    //                new string[] { "4", e.CommandArgument.ToString(), Convert.ToDateTime(Date.Text, cult).ToString("yyyy/MM/dd"), lnkTnum.ToolTip.ToString() }, "dataset");
    //            if (ds != null && ds.Tables[0].Rows.Count > 0)
    //            {
    //                GridView4.DataSource = ds.Tables[0];
    //                GridView4.DataBind();
    //                GridView4.Visible = true;
    //                h5.InnerText = "RMRD";
    //                MCMS.InnerText = "TOTAL MILK COLLECTION";
    //            }
    //        }
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myFileModal()", true);
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    protected void GrdMarketing_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            gvMCMSCC.DataSource = null;
            gvMCMSCC.DataBind();
            gvMCMSCC.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView2.Visible = false;
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView3.Visible = false;
            GridView4.DataSource = null;
            GridView4.DataBind();
            GridView4.Visible = false;
            GridView5.DataSource = null;
            GridView5.DataBind();
            GridView5.Visible = false;
            if (e.CommandName == "Marketing")
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                LinkButton lnk = (LinkButton)gvr.FindControl("lblLastDate");
                ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag", "Office_Parant_ID", "EntryDate" },
                    new string[] { "5", e.CommandArgument.ToString(), Convert.ToDateTime(lnk.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView5.DataSource = ds.Tables[0];
                    GridView5.DataBind();
                    GridView5.Visible = true;
                    h5.InnerText = "Marketing";
                    MCMS.InnerText = "TOTAL DEMANDS";
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myFileModal()", true);
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void GvTruckSheet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            gvMCMSCC.DataSource = null;
            gvMCMSCC.DataBind();
            gvMCMSCC.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView2.Visible = false;
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView3.Visible = false;
            GridView4.DataSource = null;
            GridView4.DataBind();
            GridView4.Visible = false;
            GridView5.DataSource = null;
            GridView5.DataBind();
            GridView5.Visible = false;
            if (e.CommandName == "TruckMilkColl")
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label Date = (Label)gvr.FindControl("lblLastDate");
                ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag", "Office_Parant_ID", "EntryDate" },
                    new string[] { "6", e.CommandArgument.ToString(), Convert.ToDateTime(Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView4.DataSource = ds.Tables[0];
                    GridView4.DataBind();
                    GridView4.Visible = true;
                    h5.InnerText = "Truck Sheet";
                    MCMS.InnerText = "TOTAL MILK COLLECTION";
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myFileModal()", true);
        }
        catch (Exception ex)
        {

        }
    }
}