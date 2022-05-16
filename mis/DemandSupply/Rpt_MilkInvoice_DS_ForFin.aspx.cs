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


public partial class mis_DemandSupply_Rpt_MilkInvoice_DS_ForFin : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds1, ds2, ds6, ds7, dsInvo = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
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
                ViewState["Office_Address"] = ds2.Tables[0].Rows[0]["Office_Address"].ToString();
                ViewState["Office_Address1"] = ds2.Tables[0].Rows[0]["Office_Address1"].ToString();
                ViewState["Office_Pincode"] = ds2.Tables[0].Rows[0]["Office_Pincode"].ToString();
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

            if (ddlInvoiceFor.SelectedValue == "1")
            {
                pnlSSorDist.Visible = true;
                GetSS();
            }
            else
            {
                pnlInstitution.Visible = true;
                GetInstitution();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetInstitution()
    {
        try
        {

            ddlInstitution.DataSource = objdb.ByProcedure("USP_Mst_BoothReg",
                 new string[] { "flag", "Office_ID", "AreaId" },
                 new string[] { "20", objdb.Office_ID(), ddlLocation.SelectedValue }, "dataset");
            ddlInstitution.DataTextField = "BName";
            ddlInstitution.DataValueField = "InstNRId";
            ddlInstitution.DataBind();
            ddlInstitution.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
        }
    }
    private void GetSS()
    {
        try
        {
            if (objdb.Office_ID() == "4" || objdb.Office_ID() == "6" | objdb.Office_ID() == "7")
            {


                ds7 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                  new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                    new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() }, "dataset");
            }
            else // 2,3,5
            {
                ds7 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                  new string[] { "11", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() }, "dataset");
            }
            ddlSuperStockist.Items.Clear();
            if (ds7.Tables[0].Rows.Count > 0)
            {
                if (objdb.Office_ID() == "4" || objdb.Office_ID() == "6" | objdb.Office_ID() == "7")
                {
                    ddlSuperStockist.DataTextField = "SSName";
                    ddlSuperStockist.DataValueField = "SSRDId";
                }
                else
                {
                    ddlSuperStockist.DataTextField = "DTName";
                    ddlSuperStockist.DataValueField = "DistributorId";
                }
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
            if (objdb.Office_ID() == "4" || objdb.Office_ID() == "6" | objdb.Office_ID() == "7")
            {
                if (ddlInvoiceFor.SelectedValue == "1")
                {
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
                    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                             new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "AreaId", "SuperStockistId", "Office_ID" },
                               new string[] { "7", fromdat, todat, objdb.GetMilkCatId(), ddlLocation.SelectedValue, ssid.ToString(), objdb.Office_ID() }, "dataset");
                }
                else
                {
                    string InstId = "";
                    if (ddlInstitution.SelectedValue == "0")
                    {
                        InstId = "0";
                    }
                    else
                    {
                        string[] InstRDId = ddlInstitution.SelectedValue.Split('-');
                        InstId = InstRDId[0].ToString();
                    }
                    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                             new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "AreaId", "OrganizationId", "Office_ID" },
                               new string[] { "9", fromdat, todat, objdb.GetMilkCatId(), ddlLocation.SelectedValue, InstId.ToString(), objdb.Office_ID() }, "dataset");
                }
            }
            else
            {
                if (ddlInvoiceFor.SelectedValue == "1")
                {
                    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                            new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "AreaId", "DistributorId", "Office_ID" },
                              new string[] { "12", fromdat, todat, objdb.GetMilkCatId(), ddlLocation.SelectedValue, ddlSuperStockist.SelectedValue, objdb.Office_ID() }, "dataset");
                }
                else
                {
                    string InstId = "";
                    if (ddlInstitution.SelectedValue == "0")
                    {
                        InstId = "0";
                    }
                    else
                    {
                        string[] InstRDId = ddlInstitution.SelectedValue.Split('-');
                        InstId = InstRDId[0].ToString();
                    }
                    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                             new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "AreaId", "OrganizationId", "Office_ID" },
                               new string[] { "9", fromdat, todat, objdb.GetMilkCatId(), ddlLocation.SelectedValue, InstId.ToString(), objdb.Office_ID() }, "dataset");
                }
            }
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
                    if (objdb.Office_ID() == "4" || objdb.Office_ID() == "6" | objdb.Office_ID() == "7")
                    {
                        if (ddlInvoiceFor.SelectedValue == "1")
                        {
                            PrintInvoiceCumBillDetails_IUS_DS(e.CommandArgument.ToString());
                        }
                        else
                        {
                            PrintInvoiceCumBillDetails_Inst_IUS_DS(e.CommandArgument.ToString());
                        }
                    }
                    else if (objdb.Office_ID() == "2")
                    {
                        if (ddlInvoiceFor.SelectedValue == "1")
                        {
                            PrintInvoice_BDS_Dist(e.CommandArgument.ToString());
                        }
                        else
                        {
                            MilkInvoice_BDS_Inst(e.CommandArgument.ToString());
                        }
                    }
                    else if (objdb.Office_ID() == "5")
                    {
                        if (ddlInvoiceFor.SelectedValue == "1")
                        {
                            MilkInvoice_JDS(e.CommandArgument.ToString());
                        }
                        else
                        {
                            MilkInvoice_BDS_Inst(e.CommandArgument.ToString());
                        }
                    }
                    else if (objdb.Office_ID() == "3")
                    {
                        if (ddlInvoiceFor.SelectedValue == "1")
                        {
                            MilkInvoice_GDS(e.CommandArgument.ToString());
                        }
                        else
                        {
                            MilkInvoice_BDS_Inst(e.CommandArgument.ToString());
                        }
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
        ddlInvoiceFor.SelectedIndex = 0;
        pnlInstitution.Visible = false;
        pnlSSorDist.Visible = true;
        ddlSuperStockist.SelectedIndex = 0;
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
        if (ddlInvoiceFor.SelectedValue == "1")
        {
            GetSS();
        }
        else
        {
            GetInstitution();
        }
        GetDatatableHeaderDesign();
    }
    protected void ddlInvoiceFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (ddlInvoiceFor.SelectedValue == "1")
        {
            pnlSSorDist.Visible = true;
            pnlInstitution.Visible = false;
            ddlInstitution.SelectedIndex = 0;
            GetSS();

        }
        else
        {
            pnlInstitution.Visible = true;
            pnlSSorDist.Visible = false;
            ddlSuperStockist.SelectedIndex = 0;
            GetInstitution();
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
    private void PrintInvoiceCumBillDetails_IUS_DS(string dmid)
    {
        try
        {
            if (objdb.Office_ID() == "6")
            {
                dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
                     new string[] { "11", objdb.Office_ID(), dmid }, "dataset");
            }
            else
            {
                dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
                     new string[] { "6", objdb.Office_ID(), dmid }, "dataset");
            }


            if (dsInvo.Tables[0].Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<h2 style='text-align:center;text-align: center;font-size: 20px; margin-bottom: 0px;'>Invoice-cum-Bill of Supply</h2>");
                sb.Append("<table class='table1' style='width:100%'>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "2")
                {
                    sb.Append("<td colspan='5' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                else
                {
                    sb.Append("<td colspan='5' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</b></td>");
                sb.Append("<td colspan='6'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='5'></td>");
                    //sb.Append("<td colspan='6'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Delivery Note</td>");
                    sb.Append("<td colspan='6'>Mode/Terms of Payment</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='5'></td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Supplier's Ref</td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='6'</td>");
                }
                else
                {
                    sb.Append("<td colspan='6'>Other Reference(s)</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='5'></td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Buyer's Order No.</td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='6'></td>");
                }
                else
                {
                    sb.Append("<td colspan='6'>Dated</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='5'>Delivery Note</td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Dispatch Document No.</td>");
                }

                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='6'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");

                }
                else
                {
                    sb.Append("<td colspan='6'>Delivery Note Date</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='5'></br></td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Dispatched through</br></td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='6'></td>");
                }
                else
                {
                    sb.Append("<td colspan='6'>Destination</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='11'>Gate Pass No <b>" + dsInvo.Tables[2].Rows[0]["VDChallanNo"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='11'>Terms of Delivery</td>");
                }


                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center'>SN</td>");
                sb.Append("<td style='text-align:center;width:120px !important;'>Description of Goods</td>");
                sb.Append("<td style='text-align:center'>HSN / SAC</td>");
                sb.Append("<td style='text-align:center'>Qty(In Pkt)</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Qty(In Pkt.).</td>");
                sb.Append("<td style='text-align:center'>Return Qty (In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Inst Qty (In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Qty(In Ltr).</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Margin(In Ltr).</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Margin Amount(D)</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Amount</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Qty(In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Qty(In Ltr.)</td>");
                sb.Append("<td style='text-align:center'>Rate (Per Ltr.)</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Amount(A)</td>");
                sb.Append("<td style='text-align:center'>Payble Amount(A-D)</td>");

                sb.Append("</tr>");

                int TCount = dsInvo.Tables[0].Rows.Count;
                for (int i = 0; i < TCount; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["IName"].ToString() + "</b></td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSN_Code"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + " Pkt" + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalAdvCardQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalReturnQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalInstSupplyQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardComm"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardAmt"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardTotalAmount"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["BillingQty"].ToString() + " Pkt" + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["BillingQtyInLtr"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["RatePerLtr"].ToString() + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + Convert.ToDecimal(dsInvo.Tables[0].Rows[i]["BillingAmount"]).ToString("0.00") + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + Convert.ToDecimal(dsInvo.Tables[0].Rows[i]["PaybleAmount"]).ToString("0.00") + "</b></td>");
                    sb.Append("</tr>");
                }
                decimal TSupplyTotalQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("SupplyTotalQty"));
                decimal TReturnQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalReturnQty"));
                decimal TAdvCardQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalAdvCardQty"));
                decimal TInstQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalInstSupplyQty"));
                decimal TAdvCardQtyInLtr = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalAdvCardQtyInLtr"));
                decimal TAdvCardAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AdvCardAmt"));
                //decimal TAdvCardTotalAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AdvCardTotalAmount"));
                decimal TAdvCardTotalAmt = dsInvo.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("AdvCardTotalAmount") ?? 0);
                decimal TCashQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("BillingQty"));
                decimal TCashQtyInLtr = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingQtyInLtr"));
                decimal TAamt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingAmount"));
                decimal TPaybleAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaybleAmount"));

                decimal tcstaxamt = ((TPaybleAmt * Convert.ToDecimal(dsInvo.Tables[0].Rows[0]["TcsTaxPer"]) / 100));
                decimal FTPaybleAmt = TPaybleAmt + tcstaxamt;
                string Amount = GenerateWordsinRs(FTPaybleAmt.ToString("0.00"));

                decimal tcstaxamt_AdvCard = ((TAdvCardTotalAmt * Convert.ToDecimal(dsInvo.Tables[0].Rows[0]["TcsTaxPer"]) / 100));
                decimal FAdvCardwithtcstax = TAdvCardTotalAmt + tcstaxamt_AdvCard;
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2'></td>");
                sb.Append("<td style='text-align:center'><b>" + TSupplyTotalQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TAdvCardQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TReturnQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TInstQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TAdvCardQtyInLtr.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + TAdvCardAmt.ToString() + "</b></td>");
                sb.Append("<td style='text-align:center'><b>" + TAdvCardTotalAmt.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TCashQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TCashQtyInLtr.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + TAamt.ToString("0.00") + "</b></td>");
                sb.Append("<td style='text-align:center'><b>" + TPaybleAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Tcs on Sales @</b></td>");
                sb.Append("<td>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</td>");
                sb.Append("<td colspan='7' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? tcstaxamt_AdvCard.ToString("0.00") : "NA") + "</b></td>");
                sb.Append("<td colspan='12' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? tcstaxamt.ToString("0.00") : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total<b></td>");
                sb.Append("<td colspan='8' style='text-align:right'><b>" + FAdvCardwithtcstax.ToString("0.00") + "</b></td>");
                sb.Append("<td colspan='13' style='text-align:right'><b>" + FTPaybleAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='16'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='text-align:center'>HSN/SAC</td>");
                sb.Append("<td colspan='7' style='text-align:center'>Taxable Value</td>");
                sb.Append("<td colspan='7' style='text-align:center'>Total Tax Amount</td>");
                sb.Append("</tr>");
                int TCount1 = dsInvo.Tables[1].Rows.Count;
                for (int i = 0; i < TCount1; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2' style='text-align:center'>" + dsInvo.Tables[1].Rows[i]["HSN_Code"].ToString() + "</td>");
                    sb.Append("<td colspan='7' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TaxableValue"]).ToString("0.00") + "</td>");
                    sb.Append("<td colspan='7' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TotalTaxAmount"]).ToString("0.00") + "</td>");

                    sb.Append("</tr>");
                }
                decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
                string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString("0.00"));
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total</b></td>");
                sb.Append("<td colspan='7' style='text-align:right'><b>" + TTaxableValue.ToString("0.00") + "</b></td>");

                sb.Append("<td colspan='7' style='text-align:right'><b>" + TotalTaxAmount.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td  rowspan ='2' colspan='9' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");

                //sb.Append("<td colspan='6' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='7' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'></br>for " + ViewState["Office_Name"].ToString() + "</br>Bank Name :<b>" + dsInvo.Tables[0].Rows[0]["BankName"].ToString() + "</b></br>Branch  :<b> " + dsInvo.Tables[0].Rows[0]["Branch"].ToString() + "</b></br>IFSC Code : <b> " + dsInvo.Tables[0].Rows[0]["IFSC"].ToString() + "</b></br>A/C No : <b> " + dsInvo.Tables[0].Rows[0]["BankAccountNo"].ToString() + "</b></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }
                else
                {
                    sb.Append("<td colspan='7' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
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
    private void PrintInvoiceCumBillDetails_Inst_IUS_DS(string dmid)
    {
        try
        {
            if (objdb.Office_ID() == "6")
            {
                dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
                     new string[] { "11", objdb.Office_ID(), dmid }, "dataset");
            }
            else
            {
                dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
                     new string[] { "6", objdb.Office_ID(), dmid }, "dataset");
            }

            if (dsInvo.Tables[0].Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<h2 style='text-align:center;text-align: center;font-size: 20px; margin-bottom: 0px;'>Invoice-cum-Bill of Supply</h2>");
                sb.Append("<table class='table1' style='width:100%'>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "2")
                {
                    sb.Append("<td colspan='4' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                else
                {
                    sb.Append("<td colspan='4' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</b></td>");
                sb.Append("<td colspan='4'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='3'>Gate Pass No <b>" + dsInvo.Tables[0].Rows[0]["VDChallanNo"].ToString() + "</b></td>");
                    //sb.Append("<td colspan='4'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Delivery Note</td>");
                    sb.Append("<td colspan='4'>Mode/Terms of Payment</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='5'></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Supplier's Ref</td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='6'</td>");
                }
                else
                {
                    sb.Append("<td colspan='4'>Other Reference(s)</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["BName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["BAddress"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='3'></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Buyer's Order No.</td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='4'></td>");
                }
                else
                {
                    sb.Append("<td colspan='4'>Dated</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>Delivery Note</td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Dispatch Document No.</td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='4'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");

                }
                else
                {
                    sb.Append("<td colspan='4'>Delivery Note Date</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //   sb.Append("<td colspan='3'></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Dispatched through</br></td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    // sb.Append("<td colspan='4'></td>");
                }
                else
                {
                    sb.Append("<td colspan='4'>Destination</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='5'>Gate Pass No <b>" + dsInvo.Tables[2].Rows[0]["VDChallanNo"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Terms of Delivery</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center'>SN</td>");
                sb.Append("<td style='text-align:center;width:120px !important;'>Description of Goods</td>");
                sb.Append("<td style='text-align:center'>HSN / SAC</td>");
                sb.Append("<td style='text-align:center'>Qty(In Pkt)</td>");
                sb.Append("<td style='text-align:center'>Return Qty (In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Qty(In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Qty(In Ltr.)</td>");
                sb.Append("<td style='text-align:center'>Rate (Per Ltr.)</td>");
                sb.Append("<td style='text-align:center'>Payble Amount</td>");

                sb.Append("</tr>");

                int TCount = dsInvo.Tables[0].Rows.Count;
                for (int i = 0; i < TCount; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["IName"].ToString() + "</b></td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSN_Code"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + " Pkt" + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalReturnQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["BillingQty"].ToString() + " Pkt" + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["BillingQtyInLtr"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["RatePerLtr"].ToString() + "</b></td>");
                    sb.Append("<td style='text-align:center'><b>" + Convert.ToDecimal(dsInvo.Tables[0].Rows[i]["PaybleAmount"]).ToString("0.00") + "</b></td>");
                    sb.Append("</tr>");
                }
                decimal TSupplyTotalQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("SupplyTotalQty"));
                decimal TReturnQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalReturnQty"));
                decimal TCashQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("BillingQty"));
                decimal TCashQtyInLtr = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingQtyInLtr"));
                decimal TPaybleAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaybleAmount"));

                decimal FTPaybleAmt = TPaybleAmt;
                string Amount = GenerateWordsinRs(FTPaybleAmt.ToString("0.00"));


                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2'></td>");
                sb.Append("<td style='text-align:center'><b>" + TSupplyTotalQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TReturnQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TCashQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TCashQtyInLtr.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + TPaybleAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total<b></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b></b></td>");
                sb.Append("<td colspan='4' style='text-align:right'><b>" + FTPaybleAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>"); ;
                sb.Append("<tr>");
                sb.Append("<td colspan='9'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='text-align:center'>HSN/SAC</td>");
                sb.Append("<td colspan='3' style='text-align:center'>Taxable Value</td>");
                sb.Append("<td colspan='3' style='text-align:center'>Total Tax Amount</td>");
                sb.Append("</tr>");
                int TCount1 = dsInvo.Tables[1].Rows.Count;
                for (int i = 0; i < TCount1; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td colspan='3' style='text-align:center'>" + dsInvo.Tables[1].Rows[i]["HSN_Code"].ToString() + "</td>");
                    sb.Append("<td colspan='3' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TaxableValue"]).ToString("0.00") + "</td>");
                    sb.Append("<td colspan='3' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TotalTaxAmount"]).ToString("0.00") + "</td>");

                    sb.Append("</tr>");
                }
                decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
                string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString("0.00"));
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='text-align:right'><b>Total</b></td>");
                sb.Append("<td colspan='3' style='text-align:right'><b>" + TTaxableValue.ToString("0.00") + "</b></td>");

                sb.Append("<td colspan='3' style='text-align:right'><b>" + TotalTaxAmount.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td  rowspan ='2' colspan='6' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");

                //sb.Append("<td colspan='6' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'></br>for " + ViewState["Office_Name"].ToString() + "</br>Bank Name :<b>" + dsInvo.Tables[0].Rows[0]["BankName"].ToString() + "</b></br>Branch  :<b> " + dsInvo.Tables[0].Rows[0]["Branch"].ToString() + "</b></br>IFSC Code : <b> " + dsInvo.Tables[0].Rows[0]["IFSC"].ToString() + "</b></br>A/C No :  <b>" + dsInvo.Tables[0].Rows[0]["BankAccountNo"].ToString() + "</b></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }
                else
                {
                    sb.Append("<td colspan='3' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
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

    private void PrintInvoice_BDS_Dist(string dmid)
    {
        try
        {
            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                                           new string[] { "Flag", "MilkOrProductInvoiceId", "Office_ID" },
                                           new string[] { "3", dmid, objdb.Office_ID() }, "dataset");

            if (ds6.Tables[0].Rows.Count > 0)
            {


                if (ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString() == "")
                {
                    ViewState["Tval"] = "0.000";
                }
                else
                {
                    ViewState["Tval"] = ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString();
                }



                ////////////////Start Of Route Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();
                decimal totalamt = 0, paybleAmt = 0, totalAdvCrdAmt = 0, tcstamt = 0, fpaybleamt = 0;
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sb.Append("<td colspan='2' class='text-left'><b>" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='padding-left:50px;'><b>Bill Book</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2'><b>G.S.T/U.I.N NO:-" + ds6.Tables[0].Rows[0]["Office_Gst"].ToString() + "<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='3'>No" + ds6.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='3'>M/s  :-" + ds6.Tables[0].Rows[0]["DName"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='3'>" + ds6.Tables[0].Rows[0]["ShiftName"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left'>" + ds6.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</td>");
                sb.Append("<td colspan='2' class='text-right'>" + ds6.Tables[0].Rows[0]["RName"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table1'>");
                int Count = ds6.Tables[0].Rows.Count;
                int ColCount = ds6.Tables[0].Columns.Count;
                sb.Append("<thead>");
                sb.Append("<td>Particulars</td>");
                sb.Append("<td>Qty(In Pkt)</td>");
                sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
                sb.Append("<td>Return Qty (In Pkt.)</td>");
                sb.Append("<td>Inst Qty (In Pkt.)</td>");
                sb.Append("<td>Adv. Card Qty (In Ltr.)</td>");
                sb.Append("<td>Adv. Card Margin.</td>");
                sb.Append("<td>Adv. Card Amount</td>");
                sb.Append("<td>Billing Qty(In Pkt.)</td>");
                sb.Append("<td>Billing Qty(In Ltr.)</td>");
                sb.Append("<td>Rate (Per Ltr.)</td>");
                sb.Append("<td>Amount</td>");
                sb.Append("<td>Payble Amount</td>");
                sb.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalInstSupplyQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardComm"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardAmt"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingAmount"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["PaybleAmount"] + "</td>");
                    sb.Append("</tr>");

                    totalAdvCrdAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]);
                    totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["BillingAmount"]);
                    paybleAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["PaybleAmount"]);

                }
                sb.Append("<tr>");
                int ColumnCount = ds6.Tables[0].Columns.Count;
                for (int i = 0; i < ColumnCount - 41; i++)
                {
                    if (i == 0)
                    {
                        sb.Append("<td><b>Total</b></td>");
                    }
                    else if (i == ColumnCount - 47)
                    {
                        sb.Append("<td><b>" + totalAdvCrdAmt + "</b></td>");
                    }
                    else if (i == ColumnCount - 43)
                    {
                        sb.Append("<td><b>" + totalamt + "</b></td>");
                    }
                    else if (i == ColumnCount - 42)
                    {
                        sb.Append("<td><b>" + paybleAmt + "</b></td>");
                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }



                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                tcstamt = ((paybleAmt * Convert.ToDecimal(ViewState["Tval"].ToString()) / 100));
                fpaybleamt = paybleAmt + tcstamt;
                sb.Append("<td><b>Tcs on Sales @</b>" + (ViewState["Tval"].ToString() != "0.000" ? ViewState["Tval"].ToString() : "NA") + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td><b>" + (ViewState["Tval"].ToString() != "0.000" ? tcstamt.ToString("0.000") : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>Grand Total</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td><b>" + fpaybleamt.ToString("0.000") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td style='text-left'></br>Prepared & Checked by</td>");
                sb.Append("<td style='padding-left:270px;'>For :-" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<td colspan='2' style='padding-left:270;'></td>");
                sb.Append("<td class='text-center' colspan='2'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>Note:</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td class='text-left' colspan='2'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>2 . Please quote our Bill No. while remiting the amount.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>3 . All Payment to be made by Bank Draft payable to  :-" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                Print.InnerHtml = sb.ToString();

                ClientScriptManager CSM = Page.ClientScript;
                string strScript = "<script>";
                strScript += "window.print();";

                strScript += "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
                ////////////////End Of Route Wise Print Code   ///////////////////////

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : BDS Distributor ", ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }

    private void MilkInvoice_BDS_Inst(string dmid)
    {
        try
        {
            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                                           new string[] { "Flag", "MilkOrProductInvoiceId", "Office_ID" },
                                           new string[] { "3", dmid, objdb.Office_ID() }, "dataset");

            if (ds6.Tables[0].Rows.Count > 0)
            {
                ////////////////Start Of Route Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();
                decimal totalamt = 0;

                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sb.Append("<td class='text-left'><b>" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='padding-left:50px;'><b>Bill Book</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>G.S.T/U.I.N NO:-" + ds6.Tables[0].Rows[0]["Office_Gst"].ToString() + "<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>No" + ds6.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>M/s  :-" + ds6.Tables[0].Rows[0]["BName"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>" + ds6.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left'>" + ds6.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</td>");
                sb.Append("<td class='text-right'>" + ds6.Tables[0].Rows[0]["RName"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table table1-bordered'>");
                int Count = ds6.Tables[0].Rows.Count;
                int ColCount = ds6.Tables[0].Columns.Count;
                sb.Append("<thead>");
                sb.Append("<td>Particulars</td>");
                sb.Append("<td>Qty(In Pkt)</td>");
                sb.Append("<td>Return Qty (In Pkt.)</td>");
                sb.Append("<td>Billing Qty(In Pkt.)</td>");
                sb.Append("<td>Billing Qty(In Ltr.)</td>");
                sb.Append("<td>Rate (Per Ltr)</td>");
                sb.Append("<td>Payble Amount</td>");
                sb.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["PaybleAmount"] + "</td>");
                    sb.Append("</tr>");

                    totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["PaybleAmount"]);
                }
                sb.Append("<tr>");
                int ColumnCount = ds6.Tables[0].Columns.Count;
                for (int i = 0; i < ColumnCount - 47; i++)
                {
                    if (i == 0)
                    {
                        sb.Append("<td><b>Total</b></td>");
                    }
                    else if (i == ColumnCount - 48)
                    {
                        sb.Append("<td><b>" + totalamt + "</b></td>");
                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }



                }
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td style='text-left'>Prepared & Checked by</td>");
                sb.Append("<td style='padding-left:270px;'>For :-" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");
                //sb.Append("<td class='text-center' colspan='2'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='padding-left:270;'></td>");
                sb.Append("<td class=;text-left;' colspan='2'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>Note:</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td class='text-left' colspan='2'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>2 . Please quote our Bill No. while remiting the amount.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>3 . All Payment to be made by Bank Draft payable to  :-" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : BDS Distributor ", ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }


    }

    private void MilkInvoice_JDS(string dmid)
    {
        try
        {
            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                                           new string[] { "Flag", "MilkOrProductInvoiceId", "Office_ID" },
                                           new string[] { "3", dmid, objdb.Office_ID() }, "dataset");

            if (ds6.Tables[0].Rows.Count > 0)
            {
               
                if (ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString() == "")
                {
                    ViewState["Tval"] = "0.000";
                }
                else
                {
                    ViewState["Tval"] = ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString();
                }
              


                ////////////////Start Of Route Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();
                decimal totalamt = 0, paybleAmt = 0, totalAdvCrdAmt = 0, totalAdvCrdCmmAmt = 0, tcstamt = 0, fpaybleamt = 0, totaladvAmt = 0, totalInstAmt = 0, totalInstTranCommAmt = 0;
                decimal totaldiscomamt = 0, totaltranscomamt = 0, totalcashsale = 0, totalbillingqtyinltr = 0, totalsupplyqtyinltr = 0;
                int totalbillingqty = 0, totalsupplyqty = 0, totaladvancecardqty = 0, totalinstituteqty = 0, totalreturnqty = 0;

                sb.Append("<div class='table-responsive'");
                sb.Append("<div class='content' style='border: 0px solid black'>");

                sb.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");
                sb.Append("<tr>");
                sb.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sb.Append("<td colspan='2' class='text-left'><b>" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (ds6.Tables[0].Rows[0]["InvoiceEditStatus"].ToString() == "")
                {
                    sb.Append("<td colspan='3' style='padding-left:50px;'><b>Bill</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3' style='padding-left:50px;'><b>Revised Bill</b></td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'><b>G.S.T/U.I.N NO:-" + ds6.Tables[0].Rows[0]["Office_Gst"].ToString() + "<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='3'>Invoice No. :" + ds6.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='3'>M/s  :-" + ds6.Tables[0].Rows[0]["DName"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='3'>" + ds6.Tables[0].Rows[0]["ShiftName"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left;'>" + ds6.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</td>");
                sb.Append("<td class='text-right;' colspan='2'>" + ds6.Tables[0].Rows[0]["RName"].ToString() + "</td>");
                sb.Append("</table>");
                sb.Append("<table class='table table1-bordered'>");
                int Count = ds6.Tables[0].Rows.Count;
                int ColCount = ds6.Tables[0].Columns.Count;
                sb.Append("<thead style='padding-left:0px;'>");
                sb.Append("<td style='width:120px'>Particulars</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Qty(In Pkt)</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Qty(In Ltr.)</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Return Qty (In Pkt.)</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Adv. Card Qty(In Pkt.)</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Adv. Card Price</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Total Adv. Card Amt</td>");
                sb.Append("<td style='display:none'>Adv. Card Margin</td>");
                sb.Append("<td style='display:none'>Adv. Card Margin Amt</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Inst. Qty</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Inst. Total Amt</td>");
                sb.Append("<td style='display:none'>Inst. Margin</td>");
                sb.Append("<td style='display:none'>Inst. Tran Margin Amt</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Billing Qty(In Pkt.)</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Billing Qty(In Ltr.)</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Rate (Per Ltr.)</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Amount</td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Payble Amount</td>");
                sb.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["SupplyTotalQtyInLtr"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + (ds6.Tables[0].Rows[i]["TotalAdvCardQty"].ToString() == "0" ? "0.000" : ds6.Tables[0].Rows[i]["RatePerLtrAdCard"]) + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["RatePerLtrAdCard"])).ToString("0.000") + "</td>");
                    sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["AdvCardComm"] + "</td>");
                    sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["AdvCardAmt"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["TotalInstSupplyQty"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstRatePerLtr"])).ToString("0.000") + "</td>");
                    sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["InstTransComm"] + "</td>");
                    sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["InstTransCommAmt"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["BillingAmount"] + "</td>");
                    sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["PaybleAmount"] + "</td>");
                    sb.Append("</tr>");



                    totaladvAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["RatePerLtrAdCard"]));
                    totalInstAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstRatePerLtr"]));
                    totalInstTranCommAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstTransCommAmt"]);
                    totalAdvCrdCmmAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]);
                    totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["BillingAmount"]);
                    paybleAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["PaybleAmount"]);


                    totaldiscomamt += Convert.ToDecimal((double.Parse(ds6.Tables[0].Rows[i]["DistOrSSComm"].ToString()) + double.Parse(ds6.Tables[0].Rows[i]["TransComm"].ToString())) * double.Parse(ds6.Tables[0].Rows[i]["BillingQty"].ToString()));
                    totalbillingqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["BillingQty"]);
                    totalsupplyqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["SupplyTotalQty"]);
                    totalsupplyqtyinltr += Convert.ToDecimal(ds6.Tables[0].Rows[i]["SupplyTotalQtyInLtr"]);
                    totalbillingqtyinltr += Convert.ToInt32(ds6.Tables[0].Rows[i]["BillingQtyInLtr"]);
                    totaladvancecardqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalAdvCardQty"]);
                    totalinstituteqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalInstSupplyQty"]);
                    totalreturnqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalReturnQty"]);
                    totalcashsale += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["BillingQty"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["FinalRetailerrate"]));

                }
                sb.Append("<tr>");
                // int ColumnCount = ds6.Tables[0].Rows.Count;
                int ColumnCount = ds6.Tables[0].Columns.Count;
                for (int i = 0; i < ColumnCount-37; i++)
                {
                    if (i == 0)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>Total</b></td>");
                    }
                    else if (i == 1)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalsupplyqty.ToString() + "</b></td>");
                    }
                    else if (i == 2)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalsupplyqtyinltr.ToString() + "</b></td>");
                    }
                    else if (i == 3)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalreturnqty.ToString() + "</b></td>");
                    }
                    else if (i == 4)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totaladvancecardqty.ToString() + "</b></td>");
                    }
                    else if (i == 5)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'></td>");
                    }
                    else if (i == 6)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totaladvAmt.ToString("0.000") + "</b></td>");
                    }

                    else if (i == 7)
                    {
                        sb.Append("<td style='display:none'><b>" + totalAdvCrdCmmAmt.ToString("0.000") + "</b></td>");
                    }
                    else if (i == 8)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalinstituteqty.ToString() + "</b></td>");
                    }
                    else if (i == 9)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalInstAmt.ToString("0.000") + "</b></td>");
                    }
                    else if (i == 10)
                    {
                        sb.Append("<td style='display:none'><b>" + totalInstTranCommAmt.ToString("0.000") + "</b></td>");
                    }
                    else if (i == 11)
                    {
                        sb.Append("<td style='display:none'><b>" + totaldiscomamt.ToString("0.000") + "</b></td>");
                    }
                    else if (i == 12)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalbillingqty.ToString() + "</b></td>");
                    }
                    else if (i == 13)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalbillingqtyinltr.ToString() + "</b></td>");
                    }
                    //else if (i == ColumnCount - 17)
                    //{
                    //    sb.Append("<td><b>" + totaltranscomamt.ToString("0.000") + "</b></td>");
                    //}
                    //else if (i == ColumnCount - 17)
                    //{
                    //    sb.Append("<td></td>");
                    //}

                    //else if (i == ColumnCount - 16)
                    //{
                    //    sb.Append("<td></td>");
                    //}
                    else if (i == 15)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalamt.ToString("0.000") + "</b></td>");
                    }

                    else if (i == 16)
                    {
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + paybleAmt.ToString("0.000") + "</b></td>");
                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }



                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                tcstamt = ((paybleAmt * Convert.ToDecimal(ViewState["Tval"].ToString()) / 100));
                fpaybleamt = paybleAmt + tcstamt;
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>Tcs on Sales @</b>" + (ViewState["Tval"].ToString() != "0.000" ? ViewState["Tval"].ToString() : "NA") + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                //sb.Append("<td></td>");
                //sb.Append("<td></td>");
                //sb.Append("<td></td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + (ViewState["Tval"].ToString() != "0.000" ? tcstamt.ToString("0.000") : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>Grand Total</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                //sb.Append("<td></td>");
                //sb.Append("<td></td>");
                //sb.Append("<td></td>");
                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + fpaybleamt.ToString("0.000") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table>");

                sb.Append("<tr>");
                sb.Append("<td style='text-left;width:250px'>Prepared by</td>");
                sb.Append("<td style='text-right'><b>Advance Card Total</b></td>");
                sb.Append("<td style='text-right'><b>" + totaladvAmt.ToString("0.00") + "</b></td>");
                sb.Append("<td style='display:none' class='text-right'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-left'></td>");
                sb.Append("<td style='text-right'><b>Cash Milk Sale Total</b></td>");
                sb.Append("<td style='text-right'><b>" + totalcashsale.ToString("0.00") + "</b></td>");
                sb.Append("<td style='display:none' class='text-right' ><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)</b></td>");
                sb.Append("</tr>");


                sb.Append("<tr>");
                sb.Append("<td style='text-left'></td>");
                sb.Append("<td style='text-right'><b>Total Milk Sale Amount</b></td>");
                sb.Append("<td style='text-right'><b>" + (totaladvAmt + totalcashsale).ToString("0.00") + "</b></td>");
                sb.Append("<td class='text-center' ></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-left;width:250px'>Checked By</td>");
                sb.Append("<td style='text-right'><b>Transportation Charges(-)</b></td>");
                sb.Append("<td style='text-right'><b>" + (totaldiscomamt + totalAdvCrdCmmAmt + totalInstTranCommAmt).ToString("0.00") + "</b></td>");
                sb.Append("<td class='text-center' ></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-left;width:250px'></td>");
                sb.Append("<td style='text-right'><b>Net Milk Sale Amount</b></td>");
                sb.Append("<td style='text-right'><b>" + ((totaladvAmt + totalcashsale) - (totaldiscomamt + totalAdvCrdCmmAmt + totalInstTranCommAmt)).ToString("0.00") + "</b></td>");
                sb.Append("<td ></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-left'></td>");
                sb.Append("<td style='text-right'><b>Total Product Sale Amount(+)</b></td>");
                sb.Append("<td style='text-right'><b>" + 0.00 + "</b></td>");
                sb.Append("<td class='text-right' ><b></b></td>");
                sb.Append("</tr>");

                //Net Receivable Amount=Net Milk Sale Amount+Total Product Sale Amount
                sb.Append("<tr>");
                sb.Append("<td style='text-left'></td>");
                sb.Append("<td style='text-right'><b>Net Receivable Amount</b></td>");
                sb.Append("<td style='text-right'><b>" + ((totaladvAmt + totalcashsale) - (totaldiscomamt + totalAdvCrdCmmAmt + totalInstTranCommAmt)).ToString("0.00") + "</b></td>");
                sb.Append("<td class='text-center' ></td>");
                sb.Append("</tr>");
                sb.Append("</br><tr>");
                sb.Append("<td class='text-left' >Remark :" + ds6.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</td>");


                sb.Append("</tr></br>");

                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='4'>Note:</td>");
                sb.Append("<td class='text-right' colspan='4'>For :-" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");

                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td class='text-left' colspan='4'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                sb.Append("<td class='text-right' colspan='4'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='4'>2 . Please quote our Bill No. while remiting the amount.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='4'>3 . All Payment to be made by Bank Draft payable to  :-" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");
                sb.Append("</div>");

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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : JDS Distributor ", ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }

    private void MilkInvoice_GDS(string dmid)
    {
        try
        {
            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                                           new string[] { "Flag", "MilkOrProductInvoiceId", "Office_ID" },
                                           new string[] { "3", dmid, objdb.Office_ID() }, "dataset");

            if (ds6.Tables[0].Rows.Count > 0)
            {
                if (ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString() == "")
                {
                    ViewState["Tval"] = "0.000";
                }
                else
                {
                    ViewState["Tval"] = ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString();
                }

                ////////////////Start Of Route Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();
                decimal totalamt = 0, paybleAmt = 0, totalAdvCrdAmt = 0, tcstamt = 0, fpaybleamt = 0, totaladvAmt = 0, totalInstAmt = 0, totalInstTranCommAmt = 0;
             
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td rowspan='4' class='text-left'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sb.Append("<td colspan='2' class='text-center' style='font-size:16px;'><b>" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' class='text-center' style='font-size:12px;'><b>Office : </b>" + ViewState["Office_Address"].ToString() + " - " + ViewState["Office_Pincode"].ToString() + "</br><b>Plant : </b>" + ViewState["Office_Address1"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' class='text-center' style='font-size:13px;'><b>Bill Book</b></td><td class='blank_td' style='width: 250px;'></td>");

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' class='text-center' style='font-size:13px;'><b>G.S.T/U.I.N NO:-" + ds6.Tables[0].Rows[0]["Office_Gst"].ToString() + "<b></td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='3'>Invoice No" + ds6.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='3'>M/s  :-" + ds6.Tables[0].Rows[0]["DName"].ToString() + "<td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='3'>Add:" + ds6.Tables[0].Rows[0]["DAddress"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='3'>GST No.: " + ds6.Tables[0].Rows[0]["GSTNo"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left;'>" + ds6.Tables[0].Rows[0]["ShiftName"].ToString() + "</td>");
                sb.Append("<td class='text-left' colspan='3'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left'>" + ds6.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</td>");
                sb.Append("<td colspan='3' class='text-right'>" + ds6.Tables[0].Rows[0]["RName"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table table1-bordered'>");
                int Count = ds6.Tables[0].Rows.Count;
                int ColCount = ds6.Tables[0].Columns.Count;
                sb.Append("<thead>");
                sb.Append("<td>Particulars</td>");
                sb.Append("<td>Qty(In Pkt)</td>");
                sb.Append("<td>Return Qty (In Pkt.)</td>");
                sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
                sb.Append("<td>Adv. Card Qty(In Ltr.)</td>");
                sb.Append("<td>Adv. Card Margin</td>");
                sb.Append("<td>Adv. Card Margin Amt</td>");
                sb.Append("<td>Inst. Qty</td>");
                sb.Append("<td>Inst. Margin</td>");
                sb.Append("<td>Inst. Tran Margin Amt</td>");
                sb.Append("<td>Billing Qty(In Pkt.)</td>");
                sb.Append("<td>Billing Qty(In Ltr.)</td>");
                sb.Append("<td>Rate (Per Ltr.)</td>");
                sb.Append("<td>Amount</td>");
                sb.Append("<td>Net Payble Amount</td>");
                sb.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardComm"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardAmt"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalInstSupplyQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["InstTransComm"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["InstTransCommAmt"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingAmount"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["PaybleAmount"] + "</td>");
                    sb.Append("</tr>");



                    totaladvAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["RatePerLtrAdCard"]));
                    //totalInstAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstRatePerLtr"]));
                    totalInstTranCommAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstTransCommAmt"]);
                    totalAdvCrdAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]);
                    totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["BillingAmount"]);
                    paybleAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["PaybleAmount"]);

                }
                sb.Append("<tr>");
                int ColumnCount = ds6.Tables[0].Columns.Count;
                for (int i = 0; i < ColumnCount-39; i++)
                {
                    if (i == 0)
                    {
                        sb.Append("<td><b>Total</b></td>");
                    }
                    else if (i == ColumnCount - 48)
                    {
                        sb.Append("<td><b>" + totalAdvCrdAmt + "</b></td>");
                    }
                    else if (i == ColumnCount - 45)
                    {
                        sb.Append("<td><b>" + totalInstTranCommAmt + "</b></td>");
                    }
                    else if (i == ColumnCount - 41)
                    {
                        sb.Append("<td><b>" + totalamt + "</b></td>");
                    }
                    else if (i == ColumnCount - 40)
                    {
                        sb.Append("<td><b>" + paybleAmt + "</b></td>");
                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }



                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                tcstamt = ((paybleAmt * Convert.ToDecimal(ViewState["Tval"].ToString()) / 100));
                fpaybleamt = paybleAmt + tcstamt;
                sb.Append("<td><b>Tcs on Sales @</b>" + (ViewState["Tval"].ToString() != "0.000" ? ViewState["Tval"].ToString() : "NA") + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td><b>" + (ViewState["Tval"].ToString() != "0.000" ? tcstamt.ToString("0.000") : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>Grand Total</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td><b>" + fpaybleamt.ToString("0.000") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td style='text-left'>Prepared & Checked by</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>Note:</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td class='text-left' colspan='2'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='1'>2 . Please quote our Bill No. while remiting the amount.</td>");
                sb.Append("<td class='text-right'>For :-" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='1'>3 . All Payment to be made by Bank Draft payable to  :-" + ds6.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");
                sb.Append("<td class='text-right'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
               

                Print.InnerHtml = sb.ToString();
                ClientScriptManager CSM = Page.ClientScript;
                string strScript = "<script>";
                strScript += "window.print();";

                strScript += "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
                ////////////////End Of Route Wise Print Code   ///////////////////////
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : GDS Distributor ", ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }
}