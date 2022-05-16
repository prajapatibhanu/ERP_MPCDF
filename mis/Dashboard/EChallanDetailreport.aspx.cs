using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Dashboard_EChallanDetailreport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds;
    IFormatProvider cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        if (Request.QueryString["Rid"] !=null)
        {
            if (objdb.Decrypt(Request.QueryString["Rid"]) != null)
            {
                string Rid = objdb.Decrypt(Request.QueryString["Rid"].ToString());
                txtDate.Text = Rid;
                GetViewRefDetails();
            }
            else
                txtDate.Text = string.Empty;
        }
        else
            txtDate.Text = string.Empty;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetViewRefDetails();
    }
    protected void GetViewRefDetails()
    {
        try
        {
           
            ds = null;
            string date = "";

            if (txtDate.Text != "")
            {
                date = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                     new string[] { "flag", "I_OfficeID", "D_Date" },
                     new string[] { "28", objdb.Office_ID(), date }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_viewreferenceno.DataSource = ds;
                        gv_viewreferenceno.DataBind();

                    }
                    else
                    {
                        gv_viewreferenceno.DataSource = null;
                        gv_viewreferenceno.DataBind();
                    }
                }
                else
                {
                    gv_viewreferenceno.DataSource = null;
                    gv_viewreferenceno.DataBind();
                }
            }
            else
            {
                gv_viewreferenceno.DataSource = null;
                gv_viewreferenceno.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
}