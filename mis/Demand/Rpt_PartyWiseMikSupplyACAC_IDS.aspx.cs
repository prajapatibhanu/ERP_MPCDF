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

public partial class mis_Demand_Rpt_PartyWiseMikSupplyACAC_IDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds3, ds5, ds7 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetOfficeDetails();
                GetLocation();
                GetSS();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtToDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
            }
        }
        else
        {
            objdb.redirectToHome();
        }
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
                  new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() }, "dataset");
            ddlDistributor.Items.Clear();
            if (ds7.Tables[0].Rows.Count > 0)
            {
                ddlDistributor.DataTextField = "SSName";
                ddlDistributor.DataValueField = "SSRDId";
                ddlDistributor.DataSource = ds7.Tables[0];
                ddlDistributor.DataBind();
            }
            else
            {
                ddlDistributor.Items.Insert(0, new ListItem("No Record Found.", "0"));
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
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetSS();
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds3.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null)
            {
                ds3.Dispose();
            }
        }
    }
    private void DistWiseMilkSummaryACAC_IDS()
    {
        lblMsg.Text = string.Empty;
        div_page_content.InnerHtml = "";
        Print.InnerHtml = "";
        string SSID = "", routename = "";
        int fdays = 0, tdays = 0;
        int rr = 0;
        StringBuilder sb1 = new StringBuilder();
        foreach (ListItem item in ddlDistributor.Items)
        {
            if (item.Selected)
            {

                SSID = item.Value;
                routename = item.Text;
                string[] SSRDId = SSID.Split('-');
                SSID = SSRDId[0];
                string[] DRNAme = routename.Split('-');



                DateTime fmonth = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
                DateTime tmonth = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
                DateTime enddate = tmonth.AddDays(1);
                string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                TimeSpan ts = (enddate - fmonth);

                int differenceInDays = ts.Days; // This is in int

                fdays = fmonth.Day;
                tdays = tmonth.Day;

                ds5 = objdb.ByProcedure("USP_Trn_PartywiseMilkyACAC_IDS",
                         new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "SuperStockistId", "Office_ID", "DifferenceInDays", "fdays", "tdays" },
                           new string[] { "1", fromnonth, tomonth, objdb.GetMilkCatId(), SSID.ToString(), objdb.Office_ID(), differenceInDays.ToString(), fdays.ToString(), tdays.ToString() }, "dataset");
                if (ds5.Tables[0].Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;
                    rr++;
                    int Count1 = ds5.Tables[0].Rows.Count;
                    int ColCount1 = ds5.Tables[0].Columns.Count;
                    decimal inltr = 0, inltrAdv = 0, inltrcast = 0, inltrInst = 0;
                    if (rr == 1)
                    {
                        sb1.Append("<table class='table1'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='" + (ColCount1 + 5) + "'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='" + (ColCount1 + 5) + "'><b>PARTY WISE MILK SUPPLY WITH INSTITUTE</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border: 1px solid #000000;text-align:left;' colspan='3'>I ACTUAL MILK SUPPLY</td>");
                        sb1.Append("<td style='border: 1px solid #000000;text-align:left;' colspan='" + (ColCount1 + 4) + "'><b>From </b>" + txtFromDate.Text + " <b>To </b>" + txtToDate.Text + "<b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border: 1px solid #000000;text-align:left' colspan='3'>II CASH MILK SUPPLY</td>");
                        sb1.Append("<td style='border: 1px solid #000000;padding: 2px 5px;' colspan='" + (ColCount1 + 4) + "'></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border: 1px solid #000000;text-align:left' colspan='3'>III ADVANCE CARD SUPPLY</td>");
                        sb1.Append("<td style='border: 1px solid #000000;padding: 2px 5px;' colspan='" + (ColCount1 + 4) + "'></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border: 1px solid #000000;text-align:left' colspan='3'>IV CREDIT SALE</td>");
                        sb1.Append("<td style='border: 1px solid #000000;padding: 2px 5px;' colspan='" + (ColCount1 + 4) + "'></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.No</b></td>");
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Distributor Name</td>");
                        for (int j = 0; j < ColCount1; j++)
                        {

                            string ColName = ds5.Tables[0].Columns[j].ToString();
                            if (ColName == "ColName")
                            {
                                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + "" + "</b></td>");
                            }
                            else
                            {
                                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");
                            }
                        }
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Sale In Litre</b></td>");
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Avg Sale In Litre</td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");


                        for (int ii = 0; ii < Count1; ii++)
                        {
                            if (ii == 4) // In Ltr
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            inltr += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                            if (ii == 5)// In Adv Ltr
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            inltrAdv += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                            if (ii == 6)// In Ltr Cash
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            inltrcast += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                            if (ii == 7)// In Ltr Institute
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            inltrInst += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                        }
                        //row print
                        for (int i = 0; i < Count1 - 3; i++)
                        {

                            if (i != 4)
                            {
                                sb1.Append("<tr>");
                                if (i == 0)
                                {
                                    sb1.Append("<td style='border: 1px solid #000000 !important;' rowspan='4'>" + rr + "</td>");
                                    sb1.Append("<td style='border: 1px solid #000000 !important;' rowspan='4'>" + DRNAme[0] + "</td>");
                                }


                                for (int K = 0; K < ColCount1; K++)
                                {
                                    string ColName = ds5.Tables[0].Columns[K].ToString();
                                    if (ColName == "ColName")
                                    {
                                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i][ColName].ToString() + "</td>");
                                    }
                                    else
                                    {
                                        if (i == 0)
                                        {
                                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + Convert.ToInt32(ds5.Tables[0].Rows[i][ColName]).ToString() + "</b></td>");
                                        }
                                        else
                                        {
                                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + Convert.ToInt32(ds5.Tables[0].Rows[i][ColName]).ToString() + "</td>");
                                        }

                                    }



                                }
                                if (i == 0)
                                {
                                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + inltr.ToString("0.00") + "</b></td>");
                                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (differenceInDays == 0 ? inltr : (inltr / differenceInDays)).ToString("0.00") + "</b></td>");
                                }
                                else if (i == 1)
                                {
                                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + inltrAdv.ToString("0.00") + "</b></td>");
                                    sb1.Append("<td style='border: 1px solid #000000 !important;'></td>");
                                }
                                else if (i == 2)
                                {
                                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + inltrcast.ToString("0.00") + "</b></td>");
                                    sb1.Append("<td style='border: 1px solid #000000 !important;'></td>");
                                }
                                else
                                {
                                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + inltrInst.ToString("0.00") + "</b></td>");
                                    sb1.Append("<td style='border: 1px solid #000000 !important;'></td>");
                                }
                                sb1.Append("</tr>");


                            }
                        }
                    }
                    else
                    {
                        for (int ii = 0; ii < Count1; ii++)
                        {
                            if (ii == 4)
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            inltr += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                            if (ii == 5)// In Adv Ltr
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            inltrAdv += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                            if (ii == 6)// In Ltr Cash
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            inltrcast += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                            if (ii == 7)// In Ltr Institute
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            inltrInst += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                        }
                        //row print
                        for (int i = 0; i < Count1 - 3; i++)
                        {
                            if (i != 4)
                            {
                                sb1.Append("<tr>");
                                if (i == 0)
                                {
                                    sb1.Append("<td style='border: 1px solid #000000 !important;' rowspan='4'>" + rr + "</td>");
                                    sb1.Append("<td style='border: 1px solid #000000 !important;' rowspan='4'>" + DRNAme[0] + "</td>");
                                }


                                for (int K = 0; K < ColCount1; K++)
                                {
                                    string ColName = ds5.Tables[0].Columns[K].ToString();
                                    if (ColName == "ColName")
                                    {
                                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i][ColName].ToString() + "</td>");
                                    }
                                    else
                                    {
                                        if (i == 0)
                                        {
                                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + Convert.ToInt32(ds5.Tables[0].Rows[i][ColName]).ToString() + "</b></td>");
                                        }
                                        else
                                        {
                                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + Convert.ToInt32(ds5.Tables[0].Rows[i][ColName]).ToString() + "</td>");
                                        }
                                    }



                                }
                                if (i == 0)
                                {
                                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + inltr.ToString("0.00") + "</b></td>");
                                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (differenceInDays == 0 ? inltr : (inltr / differenceInDays)).ToString("0.00") + "</b></td>");
                                }
                                else if (i == 1)
                                {
                                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + inltrAdv.ToString("0.00") + "</b></td>");
                                    sb1.Append("<td style='border: 1px solid #000000 !important;'></td>");
                                }
                                else if (i == 2)
                                {
                                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + inltrcast.ToString("0.00") + "</b></td>");
                                    sb1.Append("<td style='border: 1px solid #000000 !important;'></td>");
                                }
                                else
                                {
                                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + inltrInst.ToString("0.00") + "</b></td>");
                                    sb1.Append("<td style='border: 1px solid #000000 !important;'></td>");
                                }
                                sb1.Append("</tr>");
                            }
                        }
                    }

                }
                if (ds5 != null) { ds5.Dispose(); }
            }

        }
        sb1.Append("</table>");
        div_page_content.InnerHtml = sb1.ToString();
        Print.InnerHtml = sb1.ToString();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        btnPrint.Visible = false;
        btnExcel.Visible = false;
        div_page_content.InnerHtml = "";
        lblMsg.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "RoutwwiseSummary" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            div_page_content.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
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
            int Fmonth = fdate.Month;
            int Tmonth = tdate.Month;
            if ((fdate <= tdate) && (Fmonth == Tmonth))
            {
                lblMsg.Text = string.Empty;
                DistWiseMilkSummaryACAC_IDS();
            }
            else
            {
                txtToDate.Text = string.Empty;
                Fmonth = 0;
                Tmonth = 0;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate and Month must be same(ex : 01/01/2021 - 31/01/2021).");
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                btnPrint.Visible = false;
                btnExcel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
}