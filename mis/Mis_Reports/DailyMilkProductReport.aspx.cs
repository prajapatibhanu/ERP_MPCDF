using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_MilkCollection_DailyMilkProductReport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
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
                ReportDetail.Visible = false;
                btnPrint.Visible = false;
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
            gvMilkProcurement.DataSource = null;
            gvMilkProcurement.DataBind();
            ReportDetail.Visible = false;
            ds = objdb.ByProcedure("USP_DailyMilkProductEntry", new string[] { "flag", "Date" }, new string[] { "3", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                ReportDetail.Visible = true;
                btnPrint.Visible = true;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvMilkProcurement.DataSource = ds.Tables[0];
                    gvMilkProcurement.DataBind();
                    gvMilkProcurement.FooterRow.Cells[1].Text = "<b>Total : </b>";
                    decimal ProQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Quantity"));
                    decimal ProCharge1 = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Charge1"));
                    decimal ProCharge2 = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Charge2"));
                    gvMilkProcurement.FooterRow.Cells[2].Text = "<b>" + ProQuantity.ToString() + "</b>";
                    gvMilkProcurement.FooterRow.Cells[3].Text = "<b>" + ProCharge1.ToString() + "</b>";
                    gvMilkProcurement.FooterRow.Cells[4].Text = "<b>" + ProCharge2.ToString() + "</b>";
                }
                else
                {
                    ReportDetail.Visible = false;
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvPacketMilkSale.DataSource = ds.Tables[1];
                    gvPacketMilkSale.DataBind();
                    gvPacketMilkSale.FooterRow.Cells[1].Text = "<b>Total : </b>";
                    decimal PMQuantity = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Quantity"));
                    decimal PMCharge1 = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Charge1"));
                    decimal PMCharge2 = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Charge2"));
                    gvPacketMilkSale.FooterRow.Cells[2].Text = "<b>" + PMQuantity.ToString() + "</b>";
                    gvPacketMilkSale.FooterRow.Cells[3].Text = "<b>" + PMCharge1.ToString() + "</b>";
                    gvPacketMilkSale.FooterRow.Cells[4].Text = "<b>" + PMCharge2.ToString() + "</b>";
                }
                else
                {
                    ReportDetail.Visible = false;
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    gvMainProduct.DataSource = ds.Tables[2];
                    gvMainProduct.DataBind();
                }
                else
                {
                    ReportDetail.Visible = false;
                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    gvBulkMilkSale.DataSource = ds.Tables[3];
                    gvBulkMilkSale.DataBind();
                }
                else
                {
                    ReportDetail.Visible = false;
                }

                if (ds.Tables[4].Rows.Count > 0)
                {
                    GvClosingStock.DataSource = ds.Tables[4];
                    GvClosingStock.DataBind();
                    GvClosingStock.FooterRow.Cells[1].Text = "<b>MPCDF : </b>";
                    decimal SMP = ds.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("SMP"));
                    decimal WhiteButter = ds.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("WhiteButter"));
                    decimal Ghee = ds.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("Ghee"));
                    GvClosingStock.FooterRow.Cells[2].Text = "<b>" + SMP.ToString() + "</b>";
                    GvClosingStock.FooterRow.Cells[3].Text = "<b>" + WhiteButter.ToString() + "</b>";
                    GvClosingStock.FooterRow.Cells[4].Text = "<b>" + Ghee.ToString() + "</b>";
                }
                else
                {
                    ReportDetail.Visible = false;
                }

                if (ds.Tables[5].Rows.Count > 0)
                {
                    gvSMPProduction.DataSource = ds.Tables[5];
                    gvSMPProduction.DataBind();
                    gvSMPProduction.FooterRow.Cells[1].Text = "<b>Total : </b>";
                    decimal Capacity = ds.Tables[5].AsEnumerable().Sum(row => row.Field<decimal>("Capacity"));
                    decimal Production = ds.Tables[5].AsEnumerable().Sum(row => row.Field<decimal>("Production"));
                    decimal WMPProdKG = ds.Tables[5].AsEnumerable().Sum(row => row.Field<decimal>("WMPProdKG"));
                    gvSMPProduction.FooterRow.Cells[2].Text = "<b>" + Capacity.ToString() + "</b>";
                    gvSMPProduction.FooterRow.Cells[3].Text = "<b>" + Production.ToString() + "</b>";
                    gvSMPProduction.FooterRow.Cells[4].Text = "<b>" + WMPProdKG.ToString() + "</b>";
                }
                else
                {
                    ReportDetail.Visible = false;
                }

                if (ds.Tables[6].Rows.Count > 0)
                {
                    gvKisanCredit.DataSource = ds.Tables[6];
                    gvKisanCredit.DataBind();
                    gvKisanCredit.FooterRow.Cells[1].Text = "<b>MPCDF : </b>";
                    Int32 NoOfFarmerMembers = ds.Tables[6].AsEnumerable().Sum(row => row.Field<Int32>("NoOfFarmerMembers"));
                    Int32 NoOfFormsFilledByMember = ds.Tables[6].AsEnumerable().Sum(row => row.Field<Int32>("NoOfFormsFilledByMember"));
                    decimal PerOfUnfilledForms = ds.Tables[6].AsEnumerable().Sum(row => row.Field<decimal>("PerOfUnfilledForms"));
                    Int32 NoOfFormCertifiedByDCS = ds.Tables[6].AsEnumerable().Sum(row => row.Field<Int32>("NoOfFormCertifiedByDCS"));
                    Int32 NoOfFormSubmittedToBanks = ds.Tables[6].AsEnumerable().Sum(row => row.Field<Int32>("NoOfFormSubmittedToBanks"));
                    Int32 NoOfAcknowledgementReceivedFromBanks = ds.Tables[6].AsEnumerable().Sum(row => row.Field<Int32>("NoOfAcknowledgementReceivedFromBanks"));
                    Int32 NewCardsIssued = ds.Tables[6].AsEnumerable().Sum(row => row.Field<Int32>("NewCardsIssued"));
                    Int32 LimitExtended = ds.Tables[6].AsEnumerable().Sum(row => row.Field<Int32>("LimitExtended"));

                    gvKisanCredit.FooterRow.Cells[2].Text = "<b>" + NoOfFarmerMembers.ToString() + "</b>";
                    gvKisanCredit.FooterRow.Cells[3].Text = "<b>" + NoOfFormsFilledByMember.ToString() + "</b>";
                    gvKisanCredit.FooterRow.Cells[4].Text = "<b>" + PerOfUnfilledForms.ToString() + "</b>";
                    gvKisanCredit.FooterRow.Cells[5].Text = "<b>" + NoOfFormCertifiedByDCS.ToString() + "</b>";
                    gvKisanCredit.FooterRow.Cells[6].Text = "<b>" + NoOfFormSubmittedToBanks.ToString() + "</b>";
                    gvKisanCredit.FooterRow.Cells[7].Text = "<b>" + NoOfAcknowledgementReceivedFromBanks.ToString() + "</b>";
                    gvKisanCredit.FooterRow.Cells[8].Text = "<b>" + NewCardsIssued.ToString() + "</b>";
                    gvKisanCredit.FooterRow.Cells[9].Text = "<b>" + LimitExtended.ToString() + "</b>";
                }
                else
                {
                    ReportDetail.Visible = false;
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
        try
        {
            FillDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}