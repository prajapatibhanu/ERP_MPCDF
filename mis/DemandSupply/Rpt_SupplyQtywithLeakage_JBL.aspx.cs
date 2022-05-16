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

public partial class mis_DemandSupply_Rpt_SupplyQtywithLeakage_JBL : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3 = new DataSet();
    decimal Totalvalue = 0, ClosingBalance = 0;
    decimal TotalMvalue = 0, TotalPvalue = 0, TotalMPay = 0, TotalPPay = 0, transpotttotal = 0;
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
                GetRoute();
                GetShift();

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetShift()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds1.Tables[0];
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("All", "0"));

            }
            else
            {
                ddlShift.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
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
    private void GetRoute()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                new string[] { "Flag", "AreaId", "ItemCat_id", "Office_ID" },
                  new string[] { "9",ddlLocation.SelectedValue,objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
            ddlRoute.Items.Clear();
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataSource = ds1.Tables[0];
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlRoute.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRoute();
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
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                GetLeakageReprt();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                GetCompareDate();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Search: ", ex.Message.ToString());
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
    private void GetLeakageReprt()
    {
        try
        {
            DataTable dt1 = new DataTable();
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds2 = objdb.ByProcedure("USP_Trn_Milk_LeakageReport",
                     new string[] { "Flag", "FromDate", "ToDate", "Office_ID", "Shift_id", "ItemCat_id", "AreaId", "RouteId" },
                       new string[] { "1", fromdat, todat, objdb.Office_ID(),ddlShift.SelectedValue,objdb.GetMilkCatId(),ddlLocation.SelectedValue, ddlRoute.SelectedValue }, "dataset");

            if (ds2.Tables[0].Rows.Count > 0)
            {
                dt1 = ds2.Tables[0];
                foreach (DataRow drow in dt1.Rows)
                {
                    foreach (DataColumn column in dt1.Columns)
                    {
                        if (column.ToString() != "BoothId" && column.ToString() != "Retailer Name" && column.ToString() != "Total Supply(In Pkt)" && column.ToString() != "Total Leakage(In Pkt)" && column.ToString() != "Total Supply(In Ltr)" && column.ToString() != "Total Leakage(In Ltr)")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0 + " [" + "0" + "]";
                            }

                        }

                    }
                }

                pnlData.Visible = true;
               // btnExportAll.Visible = true;
                int totalrow = dt1.Rows.Count;
                int totalcolumn = dt1.Columns.Count;
                StringBuilder sb = new StringBuilder();
                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='" + (totalcolumn - 3) + "' style='text-align: center;border:1px solid black;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='" + (totalcolumn - 3) + "' style='text-align: left;border:1px solid black;'>Date <b>  :" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td ccolspan='" + (totalcolumn - 3) + "' style='text-align: left;border:1px solid black;'>Route : <b>" + ddlRoute.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>S.No</b></td>");
                for (int j = 0; j < totalcolumn; j++)
                {
                    if (j == 0 || j == (totalcolumn - 3) || j == (totalcolumn - 1))
                    {
                        sb.Append("<td style='display:none;'>" + dt1.Columns[j].ColumnName.ToString() + "</td>");
                    }
                    else
                    {
                        sb.Append("<td style='text-align: left;border:1px solid black;'>" + dt1.Columns[j].ColumnName.ToString() + "</td>");
                    }
                }
                sb.Append("</tr>");
                for (int i = 0; i < totalrow; i++)
                {
                    sb.Append("<tr><td style='text-align: center;border:1px solid black;'>" + (i + 1) + "</td>");
                    for (int j = 0; j < totalcolumn; j++)
                    {
                        if (j == 0 || j == (totalcolumn - 3) || j == (totalcolumn - 1))
                        {
                            sb.Append("<td style='display:none;'>" + dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString() + "</td>");
                        }
                        else if (j == (totalcolumn - 4))
                        {
                            Int32 checkvalue = Convert.ToInt32(dt1.Rows[i][dt1.Columns[j + 1].ColumnName.ToString()].ToString());

                            if (checkvalue > 0)
                            {
                                //sb.Append("<td style='text-align: center;border:1px solid black;'>" + dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString() + " [-<span style='color:red'>" + checkvalue.ToString() + "</span>] </td>");
                                sb.Append("<td style='text-align: center;border:1px solid black;'>" + dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString() + " [<span style='color:red'>" + checkvalue.ToString() + "</span>] </td>");
                            }
                            else
                            {
                                sb.Append("<td style='text-align: center;border:1px solid black;'>" + dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString() + " [" + checkvalue.ToString() + "] </td>");
                            }

                        }
                        else if (j == (totalcolumn - 2))
                        {
                            decimal checkvalue1 = Convert.ToDecimal(dt1.Rows[i][dt1.Columns[j + 1].ColumnName.ToString()].ToString());

                            if (checkvalue1 > 0)
                            {
                                //sb.Append("<td style='text-align: center;border:1px solid black;'>" + dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString() + " [-<span style='color:red'>" + checkvalue1.ToString() + "</span>] </td>");
                                sb.Append("<td style='text-align: center;border:1px solid black;'>" + dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString() + " [<span style='color:red'>" + checkvalue1.ToString() + "</span>] </td>");

                            }
                            else
                            {
                                sb.Append("<td style='text-align: center;border:1px solid black;'>" + dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString() + " [" + checkvalue1.ToString() + "] </td>");
                            }

                        }
                        else
                        {
                            sb.Append("<td style='text-align: center;border:1px solid black;'>" + dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString() + "</td>");
                        }
                    }
                    sb.Append("</tr>");

                }
                string bindtr = "", bindtr1 = "";
                sb.Append("<tr><td style='text-align: center;border:1px solid black;'></td><td style='text-align: center;border:1px solid black;'><b>Total</b></td>");
                for (int j = 0; j < totalcolumn; j++)
                {
                    int totalmorp = 0;
                    int totalmorpbracket = 0;
                    decimal totalmorLtr = 0;
                    decimal totalmorLtrbracket = 0;
                    string Showtotal = "0";
                    string ShowNetSupply = "0";
                    if (dt1.Columns[j].ColumnName.ToString() != "BoothId" && dt1.Columns[j].ColumnName.ToString() != "Retailer Name")
                    {
                        for (int i = 0; i < totalrow; i++)
                        {
                            string colm = "";
                            if (dt1.Columns[j].ColumnName.ToString() == "Total Supply(In Pkt)")
                            {
                                //colm = dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt1.Rows[i][dt1.Columns[j + 1].ColumnName.ToString()].ToString().ToString() + "]";
                                colm = dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString().ToString() + " [" + dt1.Rows[i][dt1.Columns[j + 1].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt1.Columns[j].ColumnName.ToString() == "Total Leakage(In Pkt)")
                            {
                                colm = dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt1.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)")
                            {
                                //colm = dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt1.Rows[i][dt1.Columns[j + 1].ColumnName.ToString()].ToString().ToString() + "]";
                                colm = dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString().ToString() + " [" + dt1.Rows[i][dt1.Columns[j + 1].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt1.Columns[j].ColumnName.ToString() == "Total Leakage(In Ltr)")
                            {
                                colm = dt1.Rows[i][dt1.Columns[j - 1].ColumnName.ToString()].ToString().ToString() + " [" + dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else
                            {
                                colm = dt1.Rows[i][dt1.Columns[j].ColumnName.ToString()].ToString().ToString();
                            }

                            string[] s = colm.Split(' ');
                            string[] sa1;
                            string ABC = "0";
                            if (dt1.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)" || dt1.Columns[j].ColumnName.ToString() == "Total Leakage(In Ltr)")
                            {
                                totalmorLtr += Convert.ToDecimal(s[0]);
                            }
                            else
                            {
                                totalmorp += Convert.ToInt32(s[0]);
                            }

                            if (s[1].Contains("["))
                           // if (s[1].Contains("-"))
                            {
                                string[] sa = colm.Split('[');
                                //string[] sa = colm.Split('-');
                                sa1 = sa[1].Split(']');
                                ABC = sa1[0];
                            }
                            else
                            {
                                ABC = "0";
                            }

                            if (dt1.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)" || dt1.Columns[j].ColumnName.ToString() == "Total Leakage(In Ltr)")
                            {
                                totalmorLtrbracket += Convert.ToDecimal(ABC);
                                Showtotal = "<span style='color:green;'>" + totalmorLtr.ToString() + "</span>" + " [<span style='color:red;'>" + totalmorLtrbracket.ToString() + "</span>]";
                                ShowNetSupply = (totalmorLtr + totalmorLtrbracket).ToString();
                            }
                            else
                            {
                                totalmorpbracket += Convert.ToInt32(ABC);
                                Showtotal = "<span style='color:green;'>" + totalmorp.ToString() + "</span>" + " [<span style='color:red;'>" + totalmorpbracket.ToString() + "</span>]";
                                ShowNetSupply = (totalmorp + totalmorpbracket).ToString();
                            }




                        }
                        if (j == totalcolumn - 3)
                        {
                            sb.Append("<td  style='display:none;'>" + Showtotal + "</td>");
                            bindtr += "<td style='display:none;'>" + totalmorpbracket.ToString() + "</td>";
                            bindtr1 += "<td style='display:none;'>" + ShowNetSupply.ToString() + "</td>";
                        }
                        else if (j == totalcolumn - 1)
                        {
                            sb.Append("<td  style='display:none;'>" + Showtotal + "</td>");
                            bindtr += "<td style='display:none;'>" + totalmorLtrbracket.ToString() + "</td>";
                            bindtr1 += "<td style='display:none;'>" + ShowNetSupply.ToString() + "</td>";
                        }
                        else
                        {
                            sb.Append("<td style='text-align: center;border:1px solid black;'><b>" + Showtotal + "</b></td>");
                            if (dt1.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)")
                            {
                                bindtr += "<td style='color:red;text-align: center;border:1px solid black;'><b>" + totalmorLtrbracket.ToString() + "</b></td>";
                            }
                            else
                            {
                                bindtr += "<td style='color:red;text-align: center;border:1px solid black;'><b>" + totalmorpbracket.ToString() + "</b></td>";
                            }
                           
                            bindtr1 += "<td style='color:green;text-align: center;border:1px solid black;'><b>" + ShowNetSupply.ToString() + "</b></td>";
                        }
                    }
                }
                sb.Append("</tr>");
                sb.Append("<tr><td style='text-align: left;border:1px solid black;'></td><td style='text-align: center;border:1px solid black;'><b>Total Leakage</b></td>" + bindtr + "</tr>");
                sb.Append("<tr><td style='text-align: left;border:1px solid black;'></td><td style='text-align: center;border:1px solid black;'><b>Total Net Supply</b></td>" + bindtr1 + "</tr>");

                sb.Append("</table>");
                div_page_content.Visible = true;
                div_page_content.InnerHtml = sb.ToString();
                Print.InnerHtml = sb.ToString();

            }
            else
            {
                pnlData.Visible = false;
                div_page_content.Visible = false;
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ","No Recore Found.");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }


    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + txtFromDate.Text + "-" + txtToDate.Text + "-" + ddlRoute.SelectedItem.Text + "-" + "SupplyWithLeakage.xls");
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}