using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_MilkCollection_DailyMilkProductEntry : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Attributes.Add("readonly", "readonly");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                FillDetails();
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Updated Successfully');", true);
                    }
                }
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void FillDetails()
    {
        try
        {
            ds = objdb.ByProcedure("USP_DailyMilkProductEntry", new string[] { "flag" }, new string[] { "4" }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                gvMilkProcurement.DataSource = ds.Tables[0];
                gvMilkProcurement.DataBind();

                gvPacketMilkSale.DataSource = ds.Tables[0];
                gvPacketMilkSale.DataBind();

                gvMainProduct.DataSource = ds.Tables[1];
                gvMainProduct.DataBind();

                gvBulkMilkSale.DataSource = ds.Tables[0];
                gvBulkMilkSale.DataBind();

                GvClosingStock.DataSource = ds.Tables[0];
                GvClosingStock.DataBind();

                gvSMPProduction.DataSource = ds.Tables[0];
                gvSMPProduction.DataBind();

                gvKisanCredit.DataSource = ds.Tables[0];
                gvKisanCredit.DataBind();
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
            DataTable dtMilkProcurement = new DataTable();
            DataTable dtPacketMilkSale = new DataTable();
            DataTable dtMainProduct = new DataTable();
            DataTable dtBulkMilkSale = new DataTable();
            DataTable dtClosingStock = new DataTable();
            DataTable dtSMPProduction = new DataTable();
            DataTable dtKisanCredit = new DataTable();

            dtMilkProcurement.Columns.Add("OfficeId", typeof(string));
            dtMilkProcurement.Columns.Add("MProQuantity", typeof(string));
            dtMilkProcurement.Columns.Add("MProCharge1", typeof(string));
            dtMilkProcurement.Columns.Add("MProCharge2", typeof(string));

            ////
            dtPacketMilkSale.Columns.Add("OfficeId", typeof(string));
            dtPacketMilkSale.Columns.Add("PMQuantity", typeof(string));
            dtPacketMilkSale.Columns.Add("PMCharge1", typeof(string));
            dtPacketMilkSale.Columns.Add("PMCharge2", typeof(string));


            ////
            dtMainProduct.Columns.Add("ProductID", typeof(string));
            dtMainProduct.Columns.Add("MPQuantity", typeof(string));
            dtMainProduct.Columns.Add("MPCharge1", typeof(string));
            dtMainProduct.Columns.Add("MPCharge2", typeof(string));

            ////
            dtBulkMilkSale.Columns.Add("OfficeId", typeof(string));
            dtBulkMilkSale.Columns.Add("BMQuantity", typeof(string));
            dtBulkMilkSale.Columns.Add("Remark", typeof(string));

            ////
            dtClosingStock.Columns.Add("OfficeId", typeof(string));
            dtClosingStock.Columns.Add("SMP", typeof(string));
            dtClosingStock.Columns.Add("WhiteButter", typeof(string));
            dtClosingStock.Columns.Add("Ghee", typeof(string));

            //// 
            dtSMPProduction.Columns.Add("OfficeId", typeof(string));
            dtSMPProduction.Columns.Add("Capacity", typeof(string));
            dtSMPProduction.Columns.Add("Production", typeof(string));
            dtSMPProduction.Columns.Add("WMPProd", typeof(string));


            ////
            dtKisanCredit.Columns.Add("OfficeId", typeof(string));
            dtKisanCredit.Columns.Add("NoOfFarmerMembers", typeof(string));
            dtKisanCredit.Columns.Add("NoOfFormsFilledByMember", typeof(string));
            dtKisanCredit.Columns.Add("Per_UnfilledForms", typeof(string));
            dtKisanCredit.Columns.Add("NofFormCertified", typeof(string));
            dtKisanCredit.Columns.Add("NoOfFormSubmitted", typeof(string));
            dtKisanCredit.Columns.Add("NoOfAcknowledgement", typeof(string));
            dtKisanCredit.Columns.Add("NewCardIssued", typeof(string));
            dtKisanCredit.Columns.Add("LimitExtended", typeof(string));


            //gvMilkProcurement
            foreach (GridViewRow row in gvMilkProcurement.Rows)
            {
                Label lblOfficeId = (Label)row.FindControl("lblOfficeId");
                TextBox txtMProQuantity = (TextBox)row.FindControl("txtMProQuantity");
                TextBox txtMProCharge1 = (TextBox)row.FindControl("txtMProCharge1");
                TextBox txtMProCharge2 = (TextBox)row.FindControl("txtMProCharge2");

                dtMilkProcurement.Rows.Add(lblOfficeId.Text, txtMProQuantity.Text, txtMProCharge1.Text, txtMProCharge2.Text);
            }

            //gvMilkProcurement
            foreach (GridViewRow row in gvPacketMilkSale.Rows)
            {
                Label lblOfficeId = (Label)row.FindControl("lblOfficeId");
                TextBox txtPMQuantity = (TextBox)row.FindControl("txtPMQuantity");
                TextBox txtPMCharge1 = (TextBox)row.FindControl("txtPMCharge1");
                TextBox txtPMCharge2 = (TextBox)row.FindControl("txtPMCharge2");

                dtPacketMilkSale.Rows.Add(lblOfficeId.Text, txtPMQuantity.Text, txtPMCharge1.Text, txtPMCharge2.Text);
            }

            //gvMainProduct
            foreach (GridViewRow row in gvMainProduct.Rows)
            {
                Label lblProductID = (Label)row.FindControl("lblProductID");
                TextBox txtMPQuantity = (TextBox)row.FindControl("txtMPQuantity");
                TextBox txtMPCharge1 = (TextBox)row.FindControl("txtMPCharge1");
                TextBox txtMPCharge2 = (TextBox)row.FindControl("txtMPCharge2");

                dtMainProduct.Rows.Add(lblProductID.Text, txtMPQuantity.Text, txtMPCharge1.Text, txtMPCharge2.Text);
            }

            //gvBulkMilkSale
            foreach (GridViewRow row in gvBulkMilkSale.Rows)
            {
                Label lblOfficeId = (Label)row.FindControl("lblOfficeId");
                TextBox txtBMQuantity = (TextBox)row.FindControl("txtBMQuantity");
                TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

                dtBulkMilkSale.Rows.Add(lblOfficeId.Text, txtBMQuantity.Text, txtRemark.Text);
            }

            //GvClosingStock
            foreach (GridViewRow row in GvClosingStock.Rows)
            {
                Label lblOfficeId = (Label)row.FindControl("lblOfficeId");
                TextBox txtSMP = (TextBox)row.FindControl("txtSMP");
                TextBox txtWhiteButter = (TextBox)row.FindControl("txtWhiteButter");
                TextBox txtGhee = (TextBox)row.FindControl("txtGhee");

                dtClosingStock.Rows.Add(lblOfficeId.Text, txtSMP.Text, txtWhiteButter.Text, txtGhee.Text);
            }

            //gvSMPProduction
            foreach (GridViewRow row in gvSMPProduction.Rows)
            {
                Label lblOfficeId = (Label)row.FindControl("lblOfficeId");
                TextBox txtCapacity = (TextBox)row.FindControl("txtCapacity");
                TextBox txtProduction = (TextBox)row.FindControl("txtProduction");
                TextBox txtWMPProd = (TextBox)row.FindControl("txtWMPProd");

                dtSMPProduction.Rows.Add(lblOfficeId.Text, txtCapacity.Text, txtProduction.Text, txtWMPProd.Text);
            }

            //gvKisanCredit
            foreach (GridViewRow row in gvKisanCredit.Rows)
            {
                Label lblOfficeId = (Label)row.FindControl("lblOfficeId");
                TextBox txtNoOfFarmerMembers = (TextBox)row.FindControl("txtNoOfFarmerMembers");
                TextBox txtNoOfFormsFilledByMember = (TextBox)row.FindControl("txtNoOfFormsFilledByMember");
                TextBox txtPer_UnfilledForms = (TextBox)row.FindControl("txtPer_UnfilledForms");
                TextBox txtNofFormCertified = (TextBox)row.FindControl("txtNofFormCertified");
                TextBox txtNoOfFormSubmitted = (TextBox)row.FindControl("txtNoOfFormSubmitted");
                TextBox txtNoOfAcknowledgement = (TextBox)row.FindControl("txtNoOfAcknowledgement");
                TextBox txtNewCardIssued = (TextBox)row.FindControl("txtNewCardIssued");
                TextBox txtLimitExtended = (TextBox)row.FindControl("txtLimitExtended");

                dtKisanCredit.Rows.Add(lblOfficeId.Text, txtNoOfFarmerMembers.Text, txtNoOfFormsFilledByMember.Text, txtPer_UnfilledForms.Text, txtNofFormCertified.Text, txtNoOfFormSubmitted.Text, txtNoOfAcknowledgement.Text, txtNewCardIssued.Text, txtLimitExtended.Text);
            }

            ds = objdb.ByProcedure("USP_DailyMilkProductEntry", new string[] { "flag", "Date", "CreatedBy", "CreatedIP" },
                new string[] { "1", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");
            if (ds != null)
            {
                string MilkProductId = ds.Tables[0].Rows[0]["MilkProductId"].ToString();
                 ds = objdb.ByProcedure("USP_DailyMilkProductEntry", new string[] { "flag", "MilkProductId" },
                new string[] { "2", MilkProductId }, 
                new string[] { "type_DailyMilkProductEntryMilkProcurementChild", "type_DailyMilkProductEntryProductMilkSale", "type_DailyMilkProductEntryMilkProductSale", "type_DailyMilkProductEntryBulkMilkSale", "type_DailyMilkProductEntryClosingStockUnionWise", "type_DailyMilkProductEntrySMPProduction","type_DailyMilkProductEntryKisanCreditCardProgress" },
                new DataTable[] { dtMilkProcurement, dtPacketMilkSale, dtMainProduct, dtBulkMilkSale, dtClosingStock,dtSMPProduction, dtKisanCredit}, "TableSave");
                if(ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}