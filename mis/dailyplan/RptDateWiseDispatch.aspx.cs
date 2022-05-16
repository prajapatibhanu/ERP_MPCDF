using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
public partial class mis_dailyplan_RptDateWiseDispatch : System.Web.UI.Page
{
    DataSet ds, ds2;
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
                FillDropdown();
                //txtDispatchDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                //txtDispatchDate.Attributes.Add("ReadOnly", "ReadOnly");

                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("ReadOnly", "ReadOnly");
                txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtToDate.Attributes.Add("ReadOnly", "ReadOnly");
               
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void FillDropdown()
    {
        try
        {
            ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds;
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("All", "0"));

            }
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                new string[] { "flag", "Office_ID" },
                new string[] { "0", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlProductSection.DataSource = ds.Tables[0];
                ddlProductSection.DataTextField = "ProductSection_Name";
                ddlProductSection.DataValueField = "ProductSection_ID";
                ddlProductSection.DataBind();

            }
            ddlProductSection.Items.Insert(0, new ListItem("Select", "0"));
            // ddlProductSectionTo.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        lblMsg.Text = "";
        string msg = "";
        string Shiftid = "0";
        //if (txtDispatchDate.Text == "")
        //{
        //    msg += "Select Dispatch Date. \\n";
        //}
        if (txtFromDate.Text == "")
        {
            msg += "Select From Datee. \\n";
        }
        if (txtToDate.Text == "")
        {
            msg += "Select To Date. \\n";
        }
        //if (ddlShift.SelectedIndex < 0)
        //{
        //    msg += "Select Shift. \\n";
        //}
        if (ddlProductSection.SelectedIndex <= 0)
        {
            msg += "Select Product Section. \\n";
        }
        
        if (msg == "")
        {
           
            FillGrid();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }
    }

    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
           
            string SenderOffice_ID = ViewState["Office_ID"].ToString();
            string SenderSection_ID = ddlProductSection.SelectedValue.ToString();

            ds = objdb.ByProcedure("spProductionItemStock",
                new string[] { "flag", "SenderOffice_ID ", "SenderSection_ID", "FromDate", "ToDate", "Shift_id" },
                new string[] { "8", SenderOffice_ID, SenderSection_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlShift.SelectedValue.ToString() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   
  
}