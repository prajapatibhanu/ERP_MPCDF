using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_mcms_reports_Next_Tanker_Required_Rpt : System.Web.UI.Page
{
    APIProcedure apiprocedure = new APIProcedure();
    IFormatProvider cult = new CultureInfo("en-IN", true);
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.SessionID != null)
        {
            if (!IsPostBack)
            {

                txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                GetDS(sender, e);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }


    protected void GetDS(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1 = apiprocedure.ByProcedure("SpAdminOffice",
                                  new string[] { "flag", "OfficeType_ID" },
                                  new string[] { "5", "2" }, "dataset");

            ddlDSName3.DataSource = ds1;
            ddlDSName3.DataTextField = "Office_Name";
            ddlDSName3.DataValueField = "Office_ID";
            ddlDSName3.DataBind();
            ddlDSName3.Items.Insert(0, new ListItem("Select", "0"));

            ddlDSName3.SelectedValue = apiprocedure.Office_ID();
            ddlDSName3.Enabled = false;
            ddlDSName3_SelectedIndexChanged(sender, e);


        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ex.Message.ToString());
        }
    }

    protected void ddlDSName3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDSName3.SelectedIndex != 0)
            {
                ddlCCName3.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                              new string[] { "flag", "Office_Parant_ID" },
                              new string[] { "22", ddlDSName3.SelectedValue }, "dataset");
                ddlCCName3.DataTextField = "Office_Name";
                ddlCCName3.DataValueField = "Office_ID";
                ddlCCName3.DataBind();
                ddlCCName3.Items.Insert(0, new ListItem("Select", "0"));
                 

            }
            else
            {

                ddlCCName3.DataSource = string.Empty;
                ddlCCName3.DataBind();
                ddlCCName3.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7:" + ex.Message.ToString());
        }
    }

    protected void btnSearchRpt_Click(object sender, EventArgs e)
    {
        try
        {

            ds = null;
            ds = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                  new string[] { "flag", "OfficeID ", "fromDate", "Todate" },
                                  new string[] { "9", ddlCCName3.SelectedValue, 
                                      Convert.ToDateTime(txtfromdate.Text, cult).ToString("yyyy/MM/dd"),
                                      Convert.ToDateTime(txtTodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_CCWiseNextTankerReport.DataSource = ds;
                gv_CCWiseNextTankerReport.DataBind();
            }
            else
            {
                gv_CCWiseNextTankerReport.DataSource = null;
                gv_CCWiseNextTankerReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 6:" + ex.Message.ToString());
        }
    }


}