using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_MilkToPowerConversion : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {

                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    string date = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate.Text = date.ToString();
                    txtDate.Attributes.Add("readonly", "readonly");
                    rbtnsaleto_SelectedIndexChanged(sender, e);
                    FillGrid();

                    if (Session["IsSuccess"] != null)
                    {
                        if ((Boolean)Session["IsSuccess"] == true)
                        {
                            lblMsg.Text = "";
                            Session["IsSuccess"] = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Milk To Power Conversion Entry Successfully Save');", true);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDS.Enabled = false;


            }
            else
            {
                ddlDS.Items.Clear();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void rbtnsaleto_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblUnionType.Text = "";

        if (rbtnTransferType.SelectedValue == "1")
        {
            lblUnionType.Text = "Union";

            DataSet ds1 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
             new string[] { "flag", "Office_ID" },
             new string[] { "1", objdb.Office_ID() }, "dataset");


            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ddlUnion.DataSource = ds1.Tables[0];
                ddlUnion.DataTextField = "Office_Name";
                ddlUnion.DataValueField = "Office_ID";
                ddlUnion.DataBind();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlUnion.Items.Clear();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));
                ddlUnion.DataBind();
            }
        }

        if (rbtnTransferType.SelectedValue == "2")
        {
            lblUnionType.Text = "Third Party";

            DataSet ds2 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
               new string[] { "flag", "Office_ID" },
               new string[] { "2", objdb.Office_ID() }, "dataset");

            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ddlUnion.DataSource = ds2.Tables[0];
                ddlUnion.DataTextField = "ThirdPartyUnion_Name";
                ddlUnion.DataValueField = "ThirdPartyUnion_Id";
                ddlUnion.DataBind();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlUnion.Items.Clear();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));
                ddlUnion.DataBind();
            }

        }
    }


    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                ds = null;
                ds = objdb.ByProcedure("USP_Trn_MilkToPowerConversionDetail",
                        new string[] {  "flag",  
                                        "Office_ID", 
                                        "Date", 
                                        "MilkToPowerConversionType", 
                                        "UnionOffice_ID", 
                                        "MilkType", 
                                        "MilkQuantity", 
                                        "MilkFat", 
                                        "MilkSnf", 
                                        "MilkValue", 
                                        "RecoveryPer", 
                                        "RecoveredSMPQuantityMT", 
                                        "RecoveredSMPQuantityKG", 
                                        "RecoveredSMPValue", 
                                        "Remark", 
                                        "I_CreatedBy", 
                                        "V_IPAddress", 
                                        "V_MacAddress", 
                                        "V_EntryFrom"

                                                 },

                                        new string[] { "2",  
                                                objdb.Office_ID(),
                                                Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"),
                                                rbtnTransferType.SelectedValue,
                                                ddlUnion.SelectedValue,
                                                ddlmilktype.SelectedItem.Text,
                                                txtMilkQuantity.Text,
                                                txtFAT.Text,
                                                txtSNF.Text,
                                                txtMilkValue.Text,
                                                txtrecoveryPer.Text,
                                                txtRecoveredApproxQty.Text,
                                                (Convert.ToDecimal(txtRecoveredApproxQty.Text)*1000).ToString(),
                                                txtsmpvalue.Text,
                                                txtRemark.Text, 
                                                objdb.createdBy(),
                                                objdb.GetLocalIPAddress(),
                                                objdb.GetMACAddress(),
                                                "Web"
                                                 
                                                },
                                         new string[] { },
                                         new DataTable[] { }, "TableSave");

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    Session["IsSuccess"] = true;
                    Response.Redirect("MilkToPowerConversion.aspx", false);
                    ClearText();

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    Session["IsSuccess"] = false;
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ClearText()
    {
        ddlUnion.ClearSelection();
        ddlmilktype.ClearSelection();
        txtMilkQuantity.Text = "";
        txtFAT.Text = "";
        txtSNF.Text = "";
        txtMilkValue.Text = "";
        txtrecoveryPer.Text = "";
        txtRecoveredApproxQty.Text = "";
        txtsmpvalue.Text = "";
        txtRemark.Text = "";
    }


    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = string.Empty;
            GridView1.DataBind();

            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkToPowerConversionDetail",
                new string[] { "flag", "Office_ID", "Date" },
                new string[] { "1", ddlDS.SelectedValue, 
                    Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

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

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        LinkButton lblRowNumber = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkEdit");
        Label lblDate = (Label)GridView1.Rows[selRowIndex].FindControl("lblDate");
        Label lblMilkToPowerConversionType = (Label)GridView1.Rows[selRowIndex].FindControl("lblMilkToPowerConversionType");
        Label lblConversionOfficeTo = (Label)GridView1.Rows[selRowIndex].FindControl("lblConversionOfficeTo");
        lbldate.Text = lblDate.Text;
        lblConversionType.Text = lblMilkToPowerConversionType.Text;
        lblConversionUnion.Text = lblConversionOfficeTo.Text;

        string date = System.DateTime.Now.ToString("dd/MM/yyyy");
        txtreceivedate.Text = date.ToString();
        txtreceivedate.Attributes.Add("readonly", "readonly");

        ViewState["MilkToPowerConversion_ID"] = lblRowNumber.CommandArgument;

        GridView2.DataSource = string.Empty;
        GridView2.DataBind();

        ds = null;
        ds = objdb.ByProcedure("USP_Trn_MilkToPowerConversionDetail",
            new string[] { "flag", "MilkToPowerConversion_ID" },
            new string[] { "3", lblRowNumber.CommandArgument }, "dataset");

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds.Tables[0];
                GridView2.DataBind();
            }

        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecieveModal()", true);

    }
    protected void btnYesT_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                ds = null;
                ds = objdb.ByProcedure("USP_Trn_MilkToPowerConversionDetail",
                        new string[] {  "flag",  
                                        "MilkToPowerConversion_ID", 
                                        "SMPRecieve_Date", 
                                        "SMPValueQty", 
                                        "SMPValue",  
                                        "SMPRemark", 
                                        "SMPRecieve_By"  

                                                 },

                                        new string[] { "4",   
                                                ViewState["MilkToPowerConversion_ID"].ToString(),
                                                Convert.ToDateTime(txtreceivedate.Text, cult).ToString("yyyy/MM/dd"), 
                                                txtsmpreceivedqty.Text,
                                                txtsmpreceivedvalue.Text,
                                                txtRemarks_R.Text, 
                                                objdb.createdBy() 
                                                },
                                         new string[] { },
                                         new DataTable[] { }, "TableSave");

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                { 
                    FillGrid();

                    ds = null;
                    ds = objdb.ByProcedure("USP_Trn_MilkToPowerConversionDetail",
                        new string[] { "flag", "MilkToPowerConversion_ID" },
                        new string[] { "3", ViewState["MilkToPowerConversion_ID"].ToString() }, "dataset");

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            GridView2.DataSource = ds.Tables[0];
                            GridView2.DataBind();
                        }

                    }
                    lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you! Record Save Successfully", "");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecieveModal()", true);
                    
                }
                else
                {
                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecieveModal()", true);
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecieveModal()", true);
        }
    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
}