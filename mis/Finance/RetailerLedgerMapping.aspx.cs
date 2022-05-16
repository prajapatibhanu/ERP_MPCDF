using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class mis_Finance_RetailerLedgerMapping : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["RowNo"] = "0";
                    FillRetailer();
                    FillPartyLedger();
                    FillGrid();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                   
                                      
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    //Fill Item DropDown
    

    //Fill PatryLedger DropDown
    protected void FillRetailer()
    {
        try
        {
            ds = objdb.ByProcedure("Usp_FinPartyWiseItemRateMaping",
                new string[] { "flag" },
                new string[] { "3"}, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlRetailerType.DataTextField = "RetailerTypeName";
                ddlRetailerType.DataValueField = "RetailerTypeID";
                ddlRetailerType.DataSource = ds.Tables[0];
                ddlRetailerType.DataBind();
                ddlRetailerType.Items.Insert(0, new ListItem("Select", "0"));


                ddlRetailerTypeflt.DataTextField = "RetailerTypeName";
                ddlRetailerTypeflt.DataValueField = "RetailerTypeID";
                ddlRetailerTypeflt.DataSource = ds.Tables[0];
                ddlRetailerTypeflt.DataBind();
                ddlRetailerTypeflt.Items.Insert(0, new ListItem("All", "0"));

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillPartyLedger()
    {
        try
        {
            ds = objdb.ByProcedure("Usp_FinRetailerLedgerMapping",
                new string[] { "flag", "Office_ID" },
                new string[] { "5", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedger.DataTextField = "Ledger_Name";
                ddlLedger.DataValueField = "Ledger_ID";
                ddlLedger.DataSource = ds.Tables[0];
                ddlLedger.DataBind();
                //ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Office = "";
            string Status = "0";
            foreach (ListItem item in ddlLedger.Items)
            {
                
                if (item.Selected == true)
                {
                   Status = "1";
                   objdb.ByProcedure("Usp_FinRetailerLedgerMapping",
                                    new string[] { "flag", 
                                                  "RetailerTypeID",
                                                  "Ledger_ID", 
                                                  "Office_ID", 
                                                  "IsActive", 
                                                  "CreatedBy",
                                                  "CreatedByIP" },
                                    new string[] {"1",
                                                  ddlRetailerType.SelectedValue,
                                                  item.Value,
                                                  ViewState["Office_ID"].ToString(),
                                                  "1",
                                                  ViewState["Emp_ID"].ToString(),
                                                  objdb.GetLocalIPAddress()}, "dataset");
                }
            }
            if(Status == "1")
            {
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                FillGrid();
                ddlRetailerType.ClearSelection();
                FillPartyLedger();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select atleast one Ledger for Mapping');", true);
            }
            
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            gvRetailerLedgerMappingDetail.DataSource = string.Empty;
            gvRetailerLedgerMappingDetail.DataBind();
            ds = objdb.ByProcedure("Usp_FinRetailerLedgerMapping", new string[] { "flag", "RetailerTypeID", "Office_ID" }, new string[] { "3", ddlRetailerTypeflt.SelectedValue, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvRetailerLedgerMappingDetail.DataSource = ds.Tables[0];
                    gvRetailerLedgerMappingDetail.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlRetailerTypeflt_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        string IsActive = "0";
        int index = row.RowIndex;
        CheckBox cb1 = (CheckBox)gvRetailerLedgerMappingDetail.Rows[index].FindControl("chkActive");
        Label RetailerLedgerMapping_ID = (Label)gvRetailerLedgerMappingDetail.Rows[index].FindControl("RetailerLedgerMapping_ID");
        if (cb1.Checked == true)
        {
            IsActive = "1";
        }
        objdb.ByProcedure("Usp_FinRetailerLedgerMapping", new string[] { "flag", "RetailerLedgerMapping_ID", "IsActive" }, new string[] { "4", RetailerLedgerMapping_ID.Text, IsActive }, "dataset");
        FillGrid();
    }
}