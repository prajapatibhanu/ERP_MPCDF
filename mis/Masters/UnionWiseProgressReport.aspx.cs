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
                
                //var objects = JObject.Parse(jsonString);
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void lnkbtnPlantOperation_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag" }, new string[] { "2" }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GrdDS.DataSource = ds.Tables[0];
                GrdDS.DataBind();
                spnHeader.InnerText = "Plant Operation Details";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DSModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkbtnMCMS_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag"}, new string[] { "3"}, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GrdDS.DataSource = ds.Tables[0];
                GrdDS.DataBind();
                spnHeader.InnerText = "MCMS Details";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DSModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkbtnMarketing_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag"}, new string[] { "1"}, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GrdDS.DataSource = ds.Tables[0];
                GrdDS.DataBind();
                spnHeader.InnerText = "Marketing Details";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DSModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkbtnFinance_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag"}, new string[] { "6" }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GrdDS.DataSource = ds.Tables[0];
                GrdDS.DataBind();
                spnHeader.InnerText = "Finance Details";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DSModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkbtnFO_Society_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag"}, new string[] { "4" }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GrdDS.DataSource = ds.Tables[0];
                GrdDS.DataBind();
                spnHeader.InnerText = "FO(Society) Details";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DSModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkbtnFO_RMRD_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag"}, new string[] { "5" }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GrdDS.DataSource = ds.Tables[0];
                GrdDS.DataBind();
                spnHeader.InnerText = "FO(RMRD) Details";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DSModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkbtnPayroll_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_UnionwiseProgressReport", new string[] { "flag"}, new string[] { "7" }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GrdDS.DataSource = ds.Tables[0];
                GrdDS.DataBind();
                spnHeader.InnerText = "Payroll Details";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DSModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void lbInventory_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

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
            dtGrd.Columns.Add("LastDate", typeof(string));
            int Count = DT.Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                DataRow dr = DT.Rows[i];
                dtGrd.Rows.Add(dr["Office_Name"].ToString(), dr["Indent_Date"].ToString());
            }
            GrdDS.DataSource = dtGrd;
            GrdDS.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DSModelF()", true);
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
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
   
}