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

public partial class mis_Demand_CityWiseMIS_IDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5 = new DataSet();
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
                GetCityName();
                GetCategory();
                GetSubSection();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetCategory()
    {
        try
        {
            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCat_id";
            ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlItemCategory.DataBind();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetCityName()
    {
        try
        {
            ddlCity.DataTextField = "RouteHeadName";
            ddlCity.DataValueField = "RouteHeadId";
            ddlCity.DataSource = objdb.ByProcedure("USP_Mst_RouteHead",
                 new string[] { "flag", "Office_ID" },
                   new string[] { "1", objdb.Office_ID() }, "dataset");
            ddlCity.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
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
    protected void GetSubSection()
    {
        try
        {

            ddlSubSection.DataTextField = "SubSectionName";
            ddlSubSection.DataValueField = "MOrPSubSection_id";
            ddlSubSection.DataSource = objdb.ByProcedure("USP_Mst_MilkOrProductSubSectionName",
                 new string[] { "Flag", "Office_ID", "ItemCat_id" },
                   new string[] { "6", objdb.Office_ID(), ddlItemCategory.SelectedValue }, "dataset");
            ddlSubSection.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetSubSection();
    }
    private void CityWiseMilkMIS_Report(string fdate,string tdate,string pdate, string subsid, string mhrid)
    {
        try
        {
            div_page_content.InnerHtml = "";
            Print.InnerHtml = "";
            int rr = 0;
            ds5 = objdb.ByProcedure("USP_Trn_MilkOrProduct_MISRpt_IDS",
                             new string[] { "Flag", "FromDate", "ToDate", "PDate", "ItemCat_id", "MultiMOrPSubSection_id", "MultiHeadRouteId", "Office_ID" },
                               new string[] { "1", fdate, tdate, pdate, objdb.GetMilkCatId(), subsid.ToString(), mhrid.ToString(), objdb.Office_ID() }, "dataset");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnExcel.Visible = true;
                rr++;
                StringBuilder sb1 = new StringBuilder();
                int Count1 = ds5.Tables[0].Rows.Count;
                int ColCount1 = ds5.Tables[0].Columns.Count;

                sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='" + (ColCount1) + "'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='" + (ColCount1) + "'><b>MIS REPORT</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount1) + "'><b>Date </b>" + txtFromDate.Text + "-" + txtToDate.Text + "</td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table1' style='width:100%;'>");
                sb1.Append("<thead>");
                sb1.Append("<tr>");
                // code for header hame
                string hname = "";
                int hrepatecount = 0;
                for (int j = 0; j < ColCount1; j++)
                {
                    string ColName = ds5.Tables[0].Columns[j].ToString();
                    string[] HName = ColName.Split(' ');
                    if (ColName == "City")
                    {
                        sb1.Append("<td style='border: 1px solid #000000;text-align:center;'");
                    }
                    else
                    {
                        if (HName[0] == hname)
                        {
                            ++hrepatecount;
                        }
                        else
                        {
                            if (hrepatecount == 0)
                            {
                                if (j == 1)
                                {
                                    sb1.Append("></td>");
                                }
                                else
                                {
                                    sb1.Append("><b>" + hname + "</b></td>");
                                }

                                sb1.Append("<td style='border: 1px solid #000000;text-align:center;'");
                            }
                            if (hrepatecount > 0)
                            {
                                sb1.Append(" style='border: 1px solid #000000;text-align:center;' colspan='" + (hrepatecount + 1) + "'><b>" + hname + "</b></td>");
                                sb1.Append("<td style='border: 1px solid #000000;text-align:center;'");
                                hrepatecount = 0;
                            }
                        }

                    }

                    hname = HName[0];
                }
                if (hrepatecount == 0)
                {
                    sb1.Append("><b>" + hname + "</b></td>");
                }
                if (hrepatecount > 0)
                {
                    sb1.Append(" style='border: 1px solid #000000;text-align:center;' colspan='" + (hrepatecount + 1) + "'><b>" + hname + "</b></td>");

                }
                sb1.Append("</tr>");

                // code for header hame
                sb1.Append("<tr>");
                for (int j = 0; j < ColCount1; j++)
                {
                    string ColName = ds5.Tables[0].Columns[j].ToString();
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                }
                sb1.Append("</tr>");
                sb1.Append("</thead>");
                //row print
                for (int i = 0; i < Count1; i++)
                {
                    sb1.Append("<tr>");
                    for (int K = 0; K < ColCount1; K++)
                    {
                        string ColName = ds5.Tables[0].Columns[K].ToString();
                        if (ColName == "City")
                        {
                            sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ((ds5.Tables[0].Rows[i][ColName]).ToString()) + "</td>");
                        }
                        else
                        {
                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + ((ds5.Tables[0].Rows[i][ColName]).ToString() == "" ? "00.00" : (ds5.Tables[0].Rows[i][ColName]).ToString()) + "</td>");
                        }


                    }
                    sb1.Append("</tr>");
                }
                // end of roprint

                // start of first footer sum
                DataTable dt1 = new DataTable();
                dt1 = ds5.Tables[0];
                decimal sum11 = 0;
                sb1.Append("<tr>");
                foreach (DataColumn column in dt1.Columns)
                {
                    if (column.ToString() == "City")
                    {
                        sb1.Append("<td style='border: 1px solid #000000 !important;'><b>Total</b></td>");

                    }
                    else
                    {
                        sum11 = dt1.AsEnumerable().Sum(r2 => r2.Field<decimal?>("" + column + "") ?? 0);
                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + sum11.ToString() + "</b></td>");
                    }
                }

                sb1.Append("</tr>");
                // end of first footer sum

                // start of second footer sum
                string hname1 = "";
                int hrepatecount1 = 0;
                DataTable dt2 = new DataTable();
                dt2 = ds5.Tables[0];
                decimal sum12 = 0;
                sb1.Append("<tr>");
                foreach (DataColumn column in dt2.Columns)
                {
                    string column1 = column.ToString();
                    string[] HName1 = column1.Split(' ');
                    var index = column.Ordinal;
                    if (column.ToString() == "City")
                    {
                        sb1.Append("<td style='border: 1px solid #000000 !important;'");

                    }
                    else
                    {
                        if (HName1[0] == hname1)
                        {
                            ++hrepatecount1;

                        }
                        else
                        {
                            if (hrepatecount1 == 0)
                            {
                                if (index == 1)
                                {
                                    sb1.Append("><b>Grand Total</b></td>");
                                }
                                else
                                {
                                    sb1.Append("><b>" + sum12 + "</b></td>");
                                    sum12 = 0;
                                }

                                sb1.Append("<td style='border: 1px solid #000000;text-align:center;'");
                            }
                            if (hrepatecount1 > 0)
                            {
                                sb1.Append(" style='border: 1px solid #000000;text-align:center;' colspan='" + (hrepatecount1 + 1) + "'><b>" + sum12 + "</b></td>");
                                sb1.Append("<td style='border: 1px solid #000000;text-align:center;'");
                                hrepatecount1 = 0;
                                sum12 = 0;
                            }
                        }

                    }

                    hname1 = HName1[0];
                    if (column.ToString() != "City")
                    {
                        sum12 += dt2.AsEnumerable().Sum(r2 => r2.Field<decimal?>("" + column + "") ?? 0);
                    }
                }
                if (hrepatecount1 == 0)
                {
                    sb1.Append("><b>" + sum12 + "</b></td>");
                }
                if (hrepatecount1 > 0)
                {
                    sb1.Append(" style='border: 1px solid #000000;text-align:center;' colspan='" + (hrepatecount1 + 1) + "'><b>" + sum12 + "</b></td>");

                }
                sb1.Append("</tr>");
                // end of Second footer sum

                // start of third footer sum
                string hname2 = "";
                int hrepatecount2 = 0;
                DataTable dt3 = new DataTable();
                dt3 = ds5.Tables[1];
                decimal sum13 = 0;
                sb1.Append("<tr>");
                foreach (DataColumn column1 in dt3.Columns)
                {
                    string column2 = column1.ToString();
                    string[] HName2 = column2.Split(' ');
                    var index1 = column1.Ordinal;
                    if (column1.ToString() == "City")
                    {
                        sb1.Append("<td style='border: 1px solid #000000 !important;'");

                    }
                    else
                    {
                        if (HName2[0] == hname2)
                        {
                            ++hrepatecount2;

                        }
                        else
                        {
                            if (hrepatecount2 == 0)
                            {
                                if (index1 == 1)
                                {
                                    sb1.Append("><b>Previous Total<b></td>");
                                }
                                else
                                {
                                    sb1.Append("><b>" + sum13 + "</b></td>");
                                    sum13 = 0;
                                }

                                sb1.Append("<td style='border: 1px solid #000000;text-align:center;'");
                            }
                            if (hrepatecount2 > 0)
                            {
                                sb1.Append(" style='border: 1px solid #000000;text-align:center;' colspan='" + (hrepatecount2 + 1) + "'><b>" + sum13 + "</b></td>");
                                sb1.Append("<td style='border: 1px solid #000000;text-align:center;'");
                                hrepatecount2 = 0;
                                sum13 = 0;
                            }
                        }

                    }

                    hname2 = HName2[0];
                    if (column1.ToString() != "City")
                    {
                        sum13 += dt3.AsEnumerable().Sum(r2 => r2.Field<decimal?>("" + column1 + "") ?? 0);
                    }
                }
                if (hrepatecount2 == 0)
                {
                    sb1.Append("><b>" + sum13 + "</b></td>");
                }
                if (hrepatecount2 > 0)
                {
                    sb1.Append(" style='border: 1px solid #000000;text-align:center;' colspan='" + (hrepatecount2 + 1) + "'><b>" + sum13 + "</b></td>");

                }
                sb1.Append("</tr>");
                // end of third footer sum
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                Print.InnerHtml = sb1.ToString();
                if (dt1 != null) { dt1.Dispose(); }
                if (dt2 != null) { dt2.Dispose(); }
                if (dt3 != null) { dt3.Dispose(); }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", " No Record Found.");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }


    }
    private void CityWiseProductMIS_Report(string fdate,string tdate, string subsid, string mhrid)
    {
        try
        {

            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProduct_MISRpt_IDS",
                             new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "MultiMOrPSubSection_id", "MultiHeadRouteId", "Office_ID" },
                               new string[] { "2", fdate, tdate, objdb.GetProductCatId(), subsid.ToString(), mhrid.ToString(), objdb.Office_ID() }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {

                btnPrint.Visible = true;
                btnExcel.Visible = true;

                StringBuilder sb1 = new StringBuilder();
                int Count1 = ds2.Tables[0].Rows.Count;
                int ColCount1 = ds2.Tables[0].Columns.Count;

                sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount1) + "'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount1) + "'><b>MIS REPORT</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount1) + "'><b>Date </b>" + txtFromDate.Text + "-" + txtToDate.Text + "</td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table1' style='width:100%;'>");
                sb1.Append("<thead>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Sub Section Name</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Item Name</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Qty (In Pkt)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Qty (In Ltr/KG)</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</thead>");
                string subsection = "";
                int qtytotal = 0;
                decimal litrkg = 0;
                int prowcount = 0;
                for (int i = 0; i < Count1; i++)
                {
                    
                    if (i == 0)
                    {
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + ds2.Tables[0].Rows[i]["SubSectionName"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["ItemName"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["PQty"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["PQtyInLtrKG"] + "</td>");
                        sb1.Append("</tr>");
                    }
                    else
                    {
                        if (subsection == ds2.Tables[0].Rows[i]["SubSectionName"].ToString())
                        {
                            ++prowcount;
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border-top:1px dashed black;'></td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["ItemName"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["PQty"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["PQtyInLtrKG"] + "</td>");
                            sb1.Append("</tr>");
                        }
                        else
                        {
                            if (prowcount == 0)
                            {
                                sb1.Append("<tr>");
                                sb1.Append("<td style='border:1px solid black;text-align:right;'colspan='2'><b>Total</b></td>");
                                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + qtytotal + "</b></td>");
                                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + litrkg.ToString("0.00") + "</b></td>");
                                sb1.Append("</tr>");

                                sb1.Append("<tr>");
                                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + ds2.Tables[0].Rows[i]["SubSectionName"] + "</b></td>");
                                sb1.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["ItemName"] + "</td>");
                                sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["PQty"] + "</td>");
                                sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["PQtyInLtrKG"] + "</td>");
                                sb1.Append("</tr>");
                                qtytotal = 0;
                                litrkg = 0;
                            }
                            if (prowcount > 0)
                            {
                                sb1.Append("<tr>");
                                sb1.Append("<td style='border:1px solid black;text-align:right;'colspan='2'><b>Total</b></td>");
                                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + qtytotal + "</b></td>");
                                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + litrkg.ToString("0.00") + "</b></td>");
                                sb1.Append("</tr>");
                                sb1.Append("<tr>");
                                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + ds2.Tables[0].Rows[i]["SubSectionName"] + "</b></td>");
                                sb1.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["ItemName"] + "</td>");
                                sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["PQty"] + "</td>");
                                sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["PQtyInLtrKG"] + "</td>");
                                sb1.Append("</tr>");
                                
                                prowcount = 0;
                                qtytotal = 0;
                                litrkg = 0;
                            }
                        }
                    }

                   

                    subsection = ds2.Tables[0].Rows[i]["SubSectionName"].ToString();
                    qtytotal += Convert.ToInt32(ds2.Tables[0].Rows[i]["PQty"]);
                    litrkg += Convert.ToDecimal(ds2.Tables[0].Rows[i]["PQtyInLtrKG"]);
                }
                if (prowcount == 0)
                {
                    sb1.Append("<tr>");
                    sb1.Append("<td style='border:1px solid black;text-align:right;'colspan='2'><b>Total</b></td>");
                    sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + qtytotal + "</b></td>");
                    sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + litrkg.ToString("0.00") + "</b></td>");
                    sb1.Append("</tr>");
                }
                if (prowcount > 0)
                {
                    sb1.Append("<tr>");
                    sb1.Append("<td style='border:1px solid black;text-align:right;'colspan='2'><b>Total</b></td>");
                    sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + qtytotal + "</b></td>");
                    sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + litrkg.ToString("0.00") + "</b></td>");
                    sb1.Append("</tr>");
                }
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                Print.InnerHtml = sb1.ToString();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", " No Record Found.");
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Product DM ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;

            string Headrouteid = "", MultiHeadRoutId = "", subsectionid = "", multisubsection = "";
            int rr = 0, iddata = 0, subsectindata = 0;

            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime Todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            DateTime minusday = fromdate.AddDays(-1);
            string fdate = fromdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string tdate = Todate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string pdat = minusday.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            foreach (ListItem itemss in ddlSubSection.Items)
            {
                if (itemss.Selected)
                {

                    subsectionid = itemss.Value;

                    ++subsectindata;
                    if (subsectindata == 1)
                    {
                        multisubsection = subsectionid;

                    }
                    else
                    {
                        multisubsection += "," + subsectionid;

                    }
                }
            }
            foreach (ListItem item in ddlCity.Items)
            {
                if (item.Selected)
                {

                    Headrouteid = item.Value;
                    ++iddata;
                    if (iddata == 1)
                    {
                        MultiHeadRoutId = Headrouteid;

                    }
                    else
                    {
                        MultiHeadRoutId += "," + Headrouteid;

                    }
                }
            }

            if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
            {
                CityWiseMilkMIS_Report(fdate,tdate, pdat, multisubsection, MultiHeadRoutId);
            }
            else
            {
                CityWiseProductMIS_Report(fdate,tdate, multisubsection, MultiHeadRoutId);
                
            }

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        btnPrint.Visible = false;
        btnExcel.Visible = false;
        div_page_content.InnerHtml = "";
        Print.InnerHtml = "";
        lblMsg.Text = string.Empty;
        ddlCity.ClearSelection();
        ddlSubSection.ClearSelection();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "CityWiseMISReport" + DateTime.Now + ".xls");
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
}