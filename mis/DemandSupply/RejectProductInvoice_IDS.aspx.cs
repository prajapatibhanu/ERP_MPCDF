using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.IO;
using System.Data;

public partial class mis_DemandSupply_RejectProductInvoice_IDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds1, ds2, ds7, dsInvo, ds4 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetSS();
                GetOfficeDetails();

                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = Date;
                txtToDate.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            objdb.redirectToHome();
        }

    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_GST"] = ds2.Tables[0].Rows[0]["Office_Gst"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }
    private void CClear()
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
        pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void GetLocation()
    {
        try
        {
            ddlLocation.DataTextField = "AreaName";
            ddlLocation.DataValueField = "AreaId";
            ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetSS()
    {
        try
        {
            ds7 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                  new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetProductCatId() }, "dataset");
            ddlSuperStockist.Items.Clear();
            if (ds7.Tables[0].Rows.Count > 0)
            {
                ddlSuperStockist.DataTextField = "SSName";
                ddlSuperStockist.DataValueField = "SSRDId";
                ddlSuperStockist.DataSource = ds7.Tables[0];
                ddlSuperStockist.DataBind();
                ddlSuperStockist.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlSuperStockist.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds7 != null) { ds7.Dispose(); }
        }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtToDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                GetInvoiceDetails();
                GetDatatableHeaderDesign();
            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
    private void GetInvoiceDetails()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string ssid = "";
            if (ddlSuperStockist.SelectedValue == "0")
            {
                ssid = "0";
            }
            else
            {
                string[] SSRDId = ddlSuperStockist.SelectedValue.Split('-');
                ssid = SSRDId[0].ToString();
            }
            ds1 = objdb.ByProcedure("USP_Trn_ProductDM",
                     new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "AreaId", "SuperStockistId", "Office_ID", "IsActive" },
                       new string[] { "15", fromdat, todat, objdb.GetProductCatId(), ddlLocation.SelectedValue, ssid.ToString(), objdb.Office_ID(), ddlStatus.SelectedValue }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {

                pnldata.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                pnldata.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordPrintInvoice")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;

                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    PrintInvoiceCumBillDetails(e.CommandArgument.ToString());
                    GetDatatableHeaderDesign();

                }
            }
            if (e.CommandName == "RecordReject")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds4 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
                                    new string[] { "flag", "ProductDispDeliveryChallanId", "CreatedBy", "CreatedByIP" },
                                    new string[] { "7", e.CommandArgument.ToString(), objdb.createdBy(), IPAddress }, "TableSave");

                    if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                      
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        GetCompareDate();
                    }
                    else
                    {
                        string error = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    GetDatatableHeaderDesign();

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        ddlLocation.SelectedIndex = 0;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSS();
        GetDatatableHeaderDesign();
    }
    private void PrintInvoiceCumBillDetails(string dmid)
    {
        try
        {
            dsInvo = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
                     new string[] { "flag", "Office_ID", "ProductDispDeliveryChallanId" },
                     new string[] { "4", objdb.Office_ID(), dmid }, "dataset");

            if (dsInvo.Tables[0].Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<h2 style='text-align:center;text-align: center;font-size: 20px; margin-bottom: 0px;'>Invoice-cum-Bill of Supply</h2>");
                sb.Append("<table class='table1' style='width:100%'>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "2")
                {
                    sb.Append("<td colspan='3' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                else
                {
                    sb.Append("<td colspan='3' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["DMChallanNo"].ToString() + "</b></td>");
                sb.Append("<td colspan='3'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Delivery Note</td>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Mode/Terms of Payment</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Supplier's Ref</td>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>Gate Pass No <b>" + dsInvo.Tables[0].Rows[0]["GatePassNo"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Other Reference(s)</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["BCName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                else
                {
                    sb.Append("<td colspan='3' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>DM No. <b>" + dsInvo.Tables[0].Rows[0]["OrderId"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Buyer's Order No.</td>");
                }

                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>Vehicle Out Time <b>" + dsInvo.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Dated</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Dispatch Document No.</td>");
                sb.Append("<td colspan='2'>Delivery Note Date</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>Vehicle No.</br><b>" + dsInvo.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Dispatched through</br><b>" + dsInvo.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b></td>");
                }

                sb.Append("<td colspan='3'>Destination</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6'>Terms of Delivery</td>");

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center'>S.No</td>");
                sb.Append("<td colspan='2' style='text-align:center;width:120px !important;'>Description of Goods</td>");
                sb.Append("<td style='text-align:center'>HSN / SAC</td>");
                sb.Append("<td style='text-align:center'>Quantity</td>");
                sb.Append("<td style='text-align:center'>Rate</td>");
                sb.Append("<td style='text-align:center'>Per</td>");
                sb.Append("<td style='text-align:center'>Amount</td>");
                sb.Append("</tr>");

                int TCount = dsInvo.Tables[0].Rows.Count;
                for (int i = 0; i < TCount; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td colspan='2'><b>" + dsInvo.Tables[0].Rows[i]["ItemName"].ToString() + "</b></td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSNCode"].ToString() + "</td>");
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["SupplyQty"].ToString() + "&nbsp;&nbsp;&nbsp;" + dsInvo.Tables[0].Rows[i]["PackagingModeName"].ToString() + "</b></td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["ItemRate"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["PackagingModeName"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'><b>" + dsInvo.Tables[0].Rows[i]["Amount"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                }
                decimal TAmount = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                decimal TCSTAX = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TCSTaxAmt"));
                decimal TCGST = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CentralTax"));
                decimal TSGST = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("StateTax"));

                decimal Total = TAmount + TCGST + TSGST + TCSTAX;
                string Amount = GenerateWordsinRs(Total.ToString("0.00"));
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2'></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'>" + TAmount.ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>CGST</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>" + TCGST.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>SGST</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>" + TSGST.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Tcs on Sales @</b></td>");
                sb.Append("<td>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? TCSTAX.ToString() : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total<b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'><b>" + Total.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='8'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' rowspan='2' style='text-align:center'>HSN/SAC</td>");
                sb.Append("<td rowspan='2' style='text-align:center'>Taxable Value</td>");
                sb.Append("<td colspan='2' style='text-align:center'>Central Tax</td>");
                sb.Append("<td colspan='2' style='text-align:center'>State Tax</td>");
                sb.Append("<td rowspan='2' style='text-align:center'>Total Tax Amount</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center'>Rate</td>");
                sb.Append("<td style='text-align:center'>Amount</td>");
                sb.Append("<td style='text-align:center'>Rate</td>");
                sb.Append("<td style='text-align:center'>Amount</td>");
                sb.Append("</tr>");
                int TCount1 = dsInvo.Tables[1].Rows.Count;
                for (int i = 0; i < TCount1; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2'>" + dsInvo.Tables[1].Rows[i]["HSNCode"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["TaxableValue"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[1].Rows[i]["CGST"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["CentralTax"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[1].Rows[i]["SGST"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["StateTax"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["TotalTaxAmount"].ToString() + "</td>");

                    sb.Append("</tr>");
                }
                decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
                string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString("0.00"));
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total</b></td>");
                sb.Append("<td style='text-align:right'><b>" + TTaxableValue.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'><b>" + TCGST.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'><b>" + TSGST.ToString() + "</b></td>");
                sb.Append("<td style='text-align:right'><b>" + TotalTaxAmount.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td  rowspan ='2' colspan='7' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "2")
                {
                    sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }
                else if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'></br></br>for " + ViewState["Office_Name"].ToString() + "</br>Bank Name :<b>" + dsInvo.Tables[0].Rows[0]["BankName"].ToString() + "</b></br>Branch  :<b> " + dsInvo.Tables[0].Rows[0]["Branch"].ToString() + "</b></br>IFSC Code : <b> " + dsInvo.Tables[0].Rows[0]["IFSC"].ToString() + "</b></br>A/C No :  <b>" + dsInvo.Tables[0].Rows[0]["BankAccountNo"].ToString() + "</b></br></br></br><span style='padding-left:150px; padding-top:1100px;text-align:left;'>Authorised Signatory</td>");
                }
                else
                {
                    sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }
                sb.Append("</tr>");

                sb.Append("</table>");
                Print.InnerHtml = sb.ToString();
                ClientScriptManager CSM = Page.ClientScript;
                string strScript = "<script>";
                strScript += "window.print();";

                strScript += "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
        finally
        {
            if (dsInvo != null) { dsInvo.Dispose(); }
        }

    }
    private string GenerateWordsinRs(string value)
    {
        decimal numberrs = Convert.ToDecimal(value);
        CultureInfo ci = new CultureInfo("en-IN");
        string aaa = String.Format("{0:#,##0.##}", numberrs);
        aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
        // label6.Text = aaa;


        string input = value;
        string a = "";
        string b = "";

        // take decimal part of input. convert it to word. add it at the end of method.
        string decimals = "";

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            // remove decimal part from input
            input = input.Remove(input.IndexOf("."));

        }
        string strWords = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(input));

        if (!value.Contains("."))
        {
            a = strWords + " Rupees Only";
        }
        else
        {
            a = strWords + " Rupees";
        }

        if (decimals.Length > 0)
        {
            // if there is any decimal part convert it to words and add it to strWords.
            string strwords2 = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(decimals));
            b = " and " + strwords2 + " Paisa Only ";
        }

        return a + b;
    }
}