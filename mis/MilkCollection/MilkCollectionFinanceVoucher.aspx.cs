using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_MilkCollection_MilkCollectionFinanceVoucher : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!Page.IsPostBack)
            {
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();

                GetCCDetails();

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "34", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("All", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if(objdb.Office_ID()== "4")
            {
                GetVoucherforIDS();
            }
			if(objdb.Office_ID()== "3")
            {
                GetVoucherforGDS();
            }
            else
            {
                GetVoucher();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "CCWiseAdditionDeductionReport" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divprint.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetVoucherforIDS()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "Office_ID", "Ofiice_T", "FromDate", "ToDate" }, new string[] { "19", ddlccbmcdetail.SelectedValue, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='table' style='font-size:15px;'>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='text-align:center;'>" + Session["Office_Name"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='text-align:center;'>VOUCHER CC :- " + ddlccbmcdetail.SelectedItem.Text + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4'>To,</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2' style='padding-left:50px;'>Manager(Finance)</td>");
                    sb.Append("<td colspan='2'>prepared by</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'>" + Session["Office_Name"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4'>Subject : Details for Milk Bill Payment</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    if (ds.Tables[0].Rows[0]["MilkQuantity"].ToString() != "")
                    {
                        sb.Append("<td colspan='4'style='padding-left:50px;'>Milk Quantity :<span style='padding-left:20px;'> " + ds.Tables[0].Rows[0]["MilkQuantity"].ToString() + "</span></td>");
                    }
                    else
                    {
                        sb.Append("<td colspan='4'style='padding-left:50px;'>Milk Quantity :<span style='padding-left:20px;'></span></td>");
                    }
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    if (ds.Tables[0].Rows[0]["FatInKg"].ToString() != "")
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Fat : <span style='padding-left:30px;'>" + ds.Tables[0].Rows[0]["FatInKg"].ToString() + "</span></td>");
                    }
                    else
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Fat : <span style='padding-left:30px;'></span></td>");
                    }

                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    if (ds.Tables[0].Rows[0]["SnfInKg"].ToString() != "")
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Snf : <span style='padding-left:30px;'>" + ds.Tables[0].Rows[0]["SnfInKg"].ToString() + "</span></td>");
                    }
                    else
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Snf : <span style='padding-left:30px;'></span></td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'>Buffalo Rate : " + ds.Tables[1].Rows[0]["BuffRate"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'>Cow Rate : " + ds.Tables[1].Rows[0]["CowRate"].ToString() + "</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td colspan='2'></td>");
                    sb.Append("<td colspan='2'>Period :  " + txtFdt.Text + " - " + txtTdt.Text + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2' style='border-top:1px dashed black; border-bottom:1px dashed black'>Earning Details</td>");
                    sb.Append("<td colspan='2' style='border-top:1px dashed black; border-bottom:1px dashed black'>Deduction Details</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td  colspan='2' style='border-top:1px dashed black; width:50%'>");
                    sb.Append("<table class='table' style='width:100%'>");
                    int EarningCount = ds.Tables[2].Rows.Count;
                    for (int i = 0; i < EarningCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td >" + ds.Tables[2].Rows[i]["HeadName"].ToString() + "</td>");
                        sb.Append("<td style='text-align:right;'>" + ds.Tables[2].Rows[i]["Amount"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("<td  colspan='2' style='border-top:1px dashed black; width:50%'>");
                    sb.Append("<table class='table' style='width:100%'>");
                    int DeductionCount = ds.Tables[3].Rows.Count;
                    for (int i = 0; i < DeductionCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td style='border-left:1px dashed black; '>" + ds.Tables[3].Rows[i]["HeadName"].ToString() + "</td>");
                        sb.Append("<td style='text-align:right;'>" + ds.Tables[3].Rows[i]["Amount"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    decimal TotalDeduction = 0;
                    decimal TotalEarning = 0;
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        TotalDeduction = decimal.Parse(ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("Amount")).ToString());
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        TotalEarning = decimal.Parse(ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Amount")).ToString());
                    }

                    decimal NetAmount = 0;
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        NetAmount = decimal.Parse(ds.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("NetAmount")).ToString());
                    }
                    else
                    {

                    }
                    decimal BalanceAmount = decimal.Parse(ds.Tables[5].AsEnumerable().Sum(row => row.Field<decimal>("BalanceAmount")).ToString());
                    decimal MilkBillOutstanding = NetAmount + BalanceAmount;
                    TotalEarning = TotalEarning + BalanceAmount;
                   
                    sb.Append("<tr>");
                    sb.Append("<td style='border-bottom:1px dashed black;'>Negative Payment:</td>");
                    sb.Append("<td style='border-bottom:1px dashed black; text-align:right;'>" + BalanceAmount + "</td>");
                    sb.Append("<td style='border-bottom:1px dashed black;'>Milk Bill Outstanding:</td>");
                    sb.Append("<td style='border-bottom:1px dashed black; text-align:right;'>" + MilkBillOutstanding + "</td>");
                    sb.Append("</tr>");
                    
                    sb.Append("<tr>");
                    sb.Append("<td style='border-top:1px dashed black;'>Total :</td>");
                    sb.Append("<td style='text-align:right; border-top:1px dashed black;'>" + TotalEarning.ToString() + "</td>");
                    sb.Append("<td style='border-top:1px dashed black;'>Total :</td>");
                    sb.Append("<td style='text-align:right; border-top:1px dashed black;'>" + (TotalDeduction + MilkBillOutstanding) + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    divReport.InnerHtml = sb.ToString();
                    divprint.InnerHtml = sb.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetVoucher()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "Office_ID", "Ofiice_T", "FromDate", "ToDate" }, new string[] { "19", ddlccbmcdetail.SelectedValue, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='table' style='font-size:15px;'>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='text-align:center;'>" + Session["Office_Name"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='text-align:center;'>VOUCHER CC :- " + ddlccbmcdetail.SelectedItem.Text + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4'>To,</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2' style='padding-left:50px;'>Manager(Finance)</td>");
                    sb.Append("<td colspan='2'>prepared by</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'>" + Session["Office_Name"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4'>Subject : Details for Milk Bill Payment</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    if (ds.Tables[0].Rows[0]["MilkQuantity"].ToString() != "")
                    {
                        sb.Append("<td colspan='4'style='padding-left:50px;'>Milk Quantity :<span style='padding-left:20px;'> " + ds.Tables[0].Rows[0]["MilkQuantity"].ToString() + "</span></td>");
                    }
                    else
                    {
                        sb.Append("<td colspan='4'style='padding-left:50px;'>Milk Quantity :<span style='padding-left:20px;'></span></td>");
                    }
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    if (ds.Tables[0].Rows[0]["FatInKg"].ToString() != "")
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Fat : <span style='padding-left:30px;'>" + ds.Tables[0].Rows[0]["FatInKg"].ToString() + "</span></td>");
                    }
                    else
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Fat : <span style='padding-left:30px;'></span></td>");
                    }

                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    if (ds.Tables[0].Rows[0]["SnfInKg"].ToString() != "")
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Snf : <span style='padding-left:30px;'>" + ds.Tables[0].Rows[0]["SnfInKg"].ToString() + "</span></td>");
                    }
                    else
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Snf : <span style='padding-left:30px;'></span></td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'>Buffalo Rate : " + ds.Tables[1].Rows[0]["BuffRate"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'>Cow Rate : " + ds.Tables[1].Rows[0]["CowRate"].ToString() + "</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td colspan='2'></td>");
                    sb.Append("<td colspan='2'>Period :  " + txtFdt.Text + " - " + txtTdt.Text + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2' style='border-top:1px dashed black; border-bottom:1px dashed black'>Earning Details</td>");
                    sb.Append("<td colspan='2' style='border-top:1px dashed black; border-bottom:1px dashed black'>Deduction Details</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td  colspan='2' style='border-top:1px dashed black; width:50%'>");
                    sb.Append("<table class='table' style='width:100%'>");
                    int EarningCount = ds.Tables[2].Rows.Count;
                    for (int i = 0; i < EarningCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td >" + ds.Tables[2].Rows[i]["HeadName"].ToString() + "</td>");
                        sb.Append("<td style='text-align:right;'>" + ds.Tables[2].Rows[i]["Amount"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("<td  colspan='2' style='border-top:1px dashed black; width:50%'>");
                    sb.Append("<table class='table' style='width:100%'>");
                    int DeductionCount = ds.Tables[3].Rows.Count;
                    for (int i = 0; i < DeductionCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td style='border-left:1px dashed black; '>" + ds.Tables[3].Rows[i]["HeadName"].ToString() + "</td>");
                        sb.Append("<td style='text-align:right;'>" + ds.Tables[3].Rows[i]["Amount"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    decimal TotalDeduction = 0;
                    decimal TotalEarning = 0;
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        TotalDeduction = decimal.Parse(ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("Amount")).ToString());
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        TotalEarning = decimal.Parse(ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Amount")).ToString());
                    }

                    decimal NetAmount = 0;
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        NetAmount = decimal.Parse(ds.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("NetAmount")).ToString());
                    }
                    else
                    {

                    }                       
                    sb.Append("<tr>");
                    sb.Append("<td style='border-top:1px dashed black;'></td>");
                    sb.Append("<td style='text-align:right; border-top:1px dashed black;'></td>");
                    sb.Append("<td style='border-top:1px dashed black;'>Total Deductions :</td>");
                    sb.Append("<td style='text-align:right; border-top:1px dashed black;'>" + (TotalDeduction) + "</td>");
                    sb.Append("</tr>"); 
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td>Net Amount :</td>");
                    sb.Append("<td style='text-align:right;'>" + (NetAmount) + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>Gross:</td>");
                    sb.Append("<td style='text-align:right;'>" + (TotalEarning) + "</td>");
                    sb.Append("<td>Gross :</td>");
                    sb.Append("<td style='text-align:right;'>" + (NetAmount + TotalDeduction) + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    divReport.InnerHtml = sb.ToString();
                    divprint.InnerHtml = sb.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetVoucherforGDS()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "Office_ID", "Ofiice_T", "FromDate", "ToDate" }, new string[] { "23", ddlccbmcdetail.SelectedValue, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='table' style='font-size:15px;'>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='text-align:center;'>" + Session["Office_Name"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='text-align:center;'>VOUCHER CC :- " + ddlccbmcdetail.SelectedItem.Text + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4'>To,</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2' style='padding-left:50px;'>Manager(Finance)</td>");
                    sb.Append("<td colspan='2'>prepared by</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'>" + Session["Office_Name"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4'>Subject : Details for Milk Bill Payment</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    if (ds.Tables[0].Rows[0]["MilkQuantity"].ToString() != "")
                    {
                        sb.Append("<td colspan='4'style='padding-left:50px;'>Milk Quantity :<span style='padding-left:20px;'> " + ds.Tables[0].Rows[0]["MilkQuantity"].ToString() + "</span></td>");
                    }
                    else
                    {
                        sb.Append("<td colspan='4'style='padding-left:50px;'>Milk Quantity :<span style='padding-left:20px;'></span></td>");
                    }
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    if (ds.Tables[0].Rows[0]["FatInKg"].ToString() != "")
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Fat : <span style='padding-left:30px;'>" + ds.Tables[0].Rows[0]["FatInKg"].ToString() + "</span></td>");
                    }
                    else
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Fat : <span style='padding-left:30px;'></span></td>");
                    }

                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    if (ds.Tables[0].Rows[0]["SnfInKg"].ToString() != "")
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Snf : <span style='padding-left:30px;'>" + ds.Tables[0].Rows[0]["SnfInKg"].ToString() + "</span></td>");
                    }
                    else
                    {
                        sb.Append("<td colspan='4' style='padding-left:50px;'>Milk Kg.Snf : <span style='padding-left:30px;'></span></td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'>Buffalo Rate : " + ds.Tables[1].Rows[0]["BuffRate"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='padding-left:50px;'>Cow Rate : " + ds.Tables[1].Rows[0]["CowRate"].ToString() + "</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td colspan='2'></td>");
                    sb.Append("<td colspan='2'>Period :  " + txtFdt.Text + " - " + txtTdt.Text + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2' style='border-top:1px dashed black; border-bottom:1px dashed black'>Earning Details</td>");
                    sb.Append("<td colspan='2' style='border-top:1px dashed black; border-bottom:1px dashed black'>Deduction Details</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td  colspan='2' style='border-top:1px dashed black; width:50%'>");
                    sb.Append("<table class='table' style='width:100%'>");
                    int EarningCount = ds.Tables[2].Rows.Count;
                    for (int i = 0; i < EarningCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td >" + ds.Tables[2].Rows[i]["HeadName"].ToString() + "</td>");
                        sb.Append("<td style='text-align:right;'>" + ds.Tables[2].Rows[i]["Amount"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("<td  colspan='2' style='border-top:1px dashed black; width:50%'>");
                    sb.Append("<table class='table' style='width:100%'>");
                    int DeductionCount = ds.Tables[3].Rows.Count;
                    for (int i = 0; i < DeductionCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td style='border-left:1px dashed black; '>" + ds.Tables[3].Rows[i]["HeadName"].ToString() + "</td>");
                        sb.Append("<td style='text-align:right;'>" + ds.Tables[3].Rows[i]["Amount"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    decimal TotalDeduction = 0;
                    decimal TotalEarning = 0;
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        TotalDeduction = decimal.Parse(ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("Amount")).ToString());
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        TotalEarning = decimal.Parse(ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Amount")).ToString());
                    }

                    decimal NetAmount = 0;
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        NetAmount = decimal.Parse(ds.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("NetAmount")).ToString());
                    }
                    else
                    {

                    }
                    sb.Append("<tr>");
                    sb.Append("<td style='border-top:1px dashed black;'></td>");
                    sb.Append("<td style='text-align:right; border-top:1px dashed black;'></td>");
                    sb.Append("<td style='border-top:1px dashed black;'>Total Deductions :</td>");
                    sb.Append("<td style='text-align:right; border-top:1px dashed black;'>" + (TotalDeduction) + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td>Net Amount :</td>");
                    sb.Append("<td style='text-align:right;'>" + (NetAmount) + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>Gross:</td>");
                    sb.Append("<td style='text-align:right;'>" + (TotalEarning) + "</td>");
                    sb.Append("<td>Gross :</td>");
                    sb.Append("<td style='text-align:right;'>" + (NetAmount + TotalDeduction) + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    divReport.InnerHtml = sb.ToString();
                    divprint.InnerHtml = sb.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}