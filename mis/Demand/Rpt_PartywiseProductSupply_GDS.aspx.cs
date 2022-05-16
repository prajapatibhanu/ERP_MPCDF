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

public partial class mis_Demand_Rpt_PartywiseProductSupply_GDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds3, ds5, ds6 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = Date;
                txtToDate.Attributes.Add("readonly", "readonly");
                GetOfficeDetails();
                GetLocation();
                GetDistributor();
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
    private void GetDistributor()
    {
        try
        {
            ddlDistributor.DataTextField = "DTName";
            ddlDistributor.DataValueField = "DistributorId";
            ddlDistributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "11", ddlLocation.SelectedValue, objdb.GetProductCatId(), objdb.Office_ID() }, "dataset");
            ddlDistributor.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetDistributor();
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
    private void DistWiseMilkSummaryACAC()
    {
        lblMsg.Text = string.Empty;
        div_page_content.InnerHtml = "";
        Print.InnerHtml = "";
        string routeid = "", routename = "", DDId = "";
        int fdays = 0, tdays = 0;
        int rr = 0, sddata = 0;
        StringBuilder sb1 = new StringBuilder();
        DateTime fmonth = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
        DateTime tmonth = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);

        string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        foreach (ListItem item in ddlDistributor.Items)
        {
            if (item.Selected)
            {
                ++sddata;
                if (sddata == 1)
                {
                    DDId = item.Value;
                }
                else
                {
                    DDId += "," + item.Value;
                }


            }
        }
        foreach (ListItem item in ddlDistributor.Items)
        {
            if (item.Selected)
            {

                routeid = item.Value;
                routename = item.Text;



                ds5 = objdb.ByProcedure("USP_Trn_ProductSupplyPartywise_GDS",
                         new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "DistributorId", "Office_ID" },
                           new string[] { "1", fromnonth, tomonth, objdb.GetProductCatId(), routeid, objdb.Office_ID() }, "dataset");
                if (ds5.Tables[0].Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;
                    rr++;
                    int Count1 = ds5.Tables[0].Rows.Count;
                    int ColCount1 = ds5.Tables[0].Columns.Count;
                    int totalpkt = 0;
                    decimal totalamt = 0, totalgst = 0;
                    if (rr == 1)
                    {
                        sb1.Append("<div class='table-responsive'>");
                        sb1.Append("<table class='table'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='" + (ColCount1 + 2) + "'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='" + (ColCount1 + 2) + "'><b>Party Wise Product Sale Report</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.No</b></td>");
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Distributor Name</td>");

                        for (int j = 0; j < ColCount1; j++)
                        {

                            string ColName = ds5.Tables[0].Columns[j].ToString();
                            if (ColName == "ColName")
                            {
                            }
                            else
                            {
                                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");
                            }
                        }
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Total</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");


                        for (int ii = 0; ii < Count1; ii++)
                        {
                            if (ii == 0) // Pkt
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            totalpkt += Convert.ToInt32(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                            if (ii == 1) // Amount
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            totalamt += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                            if (ii == 2)// In GST
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            totalgst += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                        }
                        //row print
                        for (int i = 0; i < Count1; i++)
                        {

                            // if (i != 2)
                            //  {
                            sb1.Append("<tr>");
                            if (i == 0)
                            {
                                sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;' rowspan='3'>" + rr + "</td>");
                                sb1.Append("<td style='border: 1px solid #000000 !important;' rowspan='3'>" + routename + "</td>");
                            }


                            for (int K = 0; K < ColCount1; K++)
                            {
                                string ColName = ds5.Tables[0].Columns[K].ToString();
                                if (ColName == "ColName")
                                {

                                }
                                else
                                {
                                    if (i == 0)
                                    {
                                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + Convert.ToInt32(ds5.Tables[0].Rows[i][ColName]).ToString() + "</td>");
                                    }
                                    else
                                    {
                                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i][ColName]).ToString() + "</td>");
                                    }

                                }



                            }
                            if (i == 0)
                            {
                                sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + totalpkt.ToString() + "</b></td>");
                            }
                            else if (i == 1)
                            {
                                sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + totalamt.ToString("0.00") + "</b></td>");
                            }
                            else
                            {
                                sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + totalgst.ToString("0.00") + "</b></td>");
                            }
                            sb1.Append("</tr>");


                            // }
                        }
                    }
                    else
                    {
                        for (int ii = 0; ii < Count1; ii++)
                        {
                            if (ii == 0) // Pkt
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            totalpkt += Convert.ToInt32(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                            if (ii == 1) // Anmount
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            totalamt += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                            if (ii == 2)// In GST
                            {
                                for (int KK = 0; KK < ColCount1; KK++)
                                {
                                    string ColName = ds5.Tables[0].Columns[KK].ToString();
                                    if (ColName != "ColName")
                                    {
                                        if (ds5.Tables[0].Rows[ii][ColName].ToString() != "")
                                        {
                                            totalgst += Convert.ToDecimal(ds5.Tables[0].Rows[ii][ColName]);
                                        }
                                    }

                                }
                            }
                        }
                        //row print
                        for (int i = 0; i < Count1; i++)
                        {
                            //if (i != 2)
                            //  {
                            sb1.Append("<tr>");
                            if (i == 0)
                            {
                                sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important' rowspan='3'>" + rr + "</td>");
                                sb1.Append("<td style='border: 1px solid #000000 !important;' rowspan='3'>" + routename + "</td>");

                            }


                            for (int K = 0; K < ColCount1; K++)
                            {
                                string ColName = ds5.Tables[0].Columns[K].ToString();
                                if (ColName == "ColName")
                                {
                                }
                                else
                                {
                                    if (i == 0)
                                    {
                                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + Convert.ToInt32(ds5.Tables[0].Rows[i][ColName]).ToString() + "</td>");
                                    }
                                    else
                                    {
                                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i][ColName]).ToString() + "</td>");
                                    }
                                }



                            }
                            if (i == 0)
                            {
                                sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + totalpkt.ToString() + "</b></td>");
                            }
                            else if (i == 1)
                            {
                                sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + totalamt.ToString("0.00") + "</b></td>");
                            }
                            else
                            {
                                sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + totalgst.ToString("0.00") + "</b></td>");
                            }
                            sb1.Append("</tr>");
                            // }
                        }
                    }

                }
                if (ds5 != null) { ds5.Dispose(); }
            }

        }
        ds6 = objdb.ByProcedure("USP_Trn_ProductSupplyPartywise_GDS",
                         new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "MultiDistributorId", "Office_ID" },
                           new string[] { "2", fromnonth, tomonth, objdb.GetProductCatId(), DDId, objdb.Office_ID() }, "dataset");
        if (ds6.Tables[0].Rows.Count > 0)
        {
            int Count2 = ds6.Tables[0].Rows.Count;
            int ColCount2 = ds6.Tables[0].Columns.Count;
            decimal totalamt_TT = 0, totalgst_TT = 0;
            int totalpkt_TT = 0;
            for (int ii = 0; ii < Count2; ii++)
            {
                if (ii == 0) // Pkt
                {
                    for (int KK = 0; KK < ColCount2; KK++)
                    {
                        string ColName = ds6.Tables[0].Columns[KK].ToString();
                        if (ColName != "ColName")
                        {
                            if (ds6.Tables[0].Rows[ii][ColName].ToString() != "")
                            {
                                totalpkt_TT += Convert.ToInt32(ds6.Tables[0].Rows[ii][ColName]);
                            }
                        }

                    }
                }
                if (ii == 1) // Amount
                {
                    for (int KK = 0; KK < ColCount2; KK++)
                    {
                        string ColName = ds6.Tables[0].Columns[KK].ToString();
                        if (ColName != "ColName")
                        {
                            if (ds6.Tables[0].Rows[ii][ColName].ToString() != "")
                            {
                                totalamt_TT += Convert.ToDecimal(ds6.Tables[0].Rows[ii][ColName]);
                            }
                        }

                    }
                }
                if (ii == 2)// In GST
                {
                    for (int KK = 0; KK < ColCount2; KK++)
                    {
                        string ColName = ds6.Tables[0].Columns[KK].ToString();
                        if (ColName != "ColName")
                        {
                            if (ds6.Tables[0].Rows[ii][ColName].ToString() != "")
                            {
                                totalgst_TT += Convert.ToDecimal(ds6.Tables[0].Rows[ii][ColName]);
                            }
                        }

                    }
                }
            }
            //row print
            for (int i = 0; i < Count2; i++)
            {

                //  if (i != 2)
                // {
                sb1.Append("<tr>");
                if (i == 0)
                {
                    sb1.Append("<td style='border: 1px solid #000000 !important;'></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + "Total Qty(In Pkt)" + "</b></td>");
                }
                if (i == 1)
                {
                    sb1.Append("<td style='border: 1px solid #000000 !important;'></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + "Total Amount" + "</b></td>");
                }
                if (i == 2)
                {
                    sb1.Append("<td style='border: 1px solid #000000 !important;'></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + "Total GST" + "</b></td>");
                }


                for (int K = 0; K < ColCount2; K++)
                {
                    string ColName = ds6.Tables[0].Columns[K].ToString();
                    if (ColName == "ColName")
                    {

                    }
                    else
                    {
                        if (i == 0)
                        {
                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + Convert.ToInt32(ds6.Tables[0].Rows[i][ColName]).ToString() + "</b></td>");
                        }
                        else
                        {
                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + Convert.ToDecimal(ds6.Tables[0].Rows[i][ColName]).ToString() + "</b></td>");
                        }

                    }



                }
                if (i == 0)
                {
                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + totalpkt_TT.ToString() + "</b></td>");
                }
                else if (i == 1)
                {
                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + totalamt_TT.ToString("0.00") + "</b></td>");
                }
                else
                {
                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + totalgst_TT.ToString("0.00") + "</b></td>");
                }
                sb1.Append("</tr>");


                // }
            }

        }
        sb1.Append("</table>");
        sb1.Append("<div>");
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
        ddlDistributor.ClearSelection();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "PartywiseProductSaleReport" + DateTime.Now + ".xls");
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
            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                DistWiseMilkSummaryACAC();
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