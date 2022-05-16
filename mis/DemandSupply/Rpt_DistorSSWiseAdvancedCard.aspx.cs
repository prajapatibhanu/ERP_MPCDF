using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_Rpt_DistorSSWiseAdvancedCard : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3 = new DataSet();
    int sum11 = 0, sum22 = 0;
    int cellIndex = 2;
    int cellIndexbooth = 2;
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.GetItemCat_id()!=null)
        {
            if (!Page.IsPostBack)
            {
                GetShift();
                GetOfficeDetails();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void GetShift()
    {
        try
        {
            ddlShift.DataTextField = "ShiftName";
            ddlShift.DataValueField = "Shift_id";
            ddlShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlShift.DataBind();
            ddlShift.Items.Insert(0, new ListItem("All", "0"));
            if (objdb.GetItemCat_id() == objdb.GetProductCatId())
            {
                ddlShift.SelectedValue = objdb.GetShiftMorId();
                ddlShift.Enabled = false;
            }
            else
            {
                ddlShift.Enabled = true;
                ddlShift.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Shift ", ex.Message.ToString());
        }
    }
    private void GetAdvancedCardDetailsDistWise()
    {
        try
        {
            lblMsg.Text = "";
            string fm = "16/" + txtFromMonth.Text;
            string tm = "15/" + txtToMonth.Text;
            DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime tmonth = DateTime.ParseExact(tm, "dd/MM/yyyy", culture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds1 = objdb.ByProcedure("USP_Trn_MilkAdvancedCardDetails",
                     new string[] { "flag", "DistributorId", "Shift_id", "ItemCat_id", "FromDate", "ToDate", "Office_ID" },
                       new string[] { "3", objdb.createdBy(), ddlShift.SelectedValue, "3", fromnonth, tomonth, objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                GridView1.Visible = true;
                btnPrintRoutWise.Visible = true;
                GridView1.Visible = true;
                DataTable dt = new DataTable();
                dt = ds1.Tables[0];

                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();

                GridView1.FooterRow.Cells[1].Text = "Grand Total";
                GridView1.FooterRow.Cells[1].Font.Bold = true;

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist")
                    {

                        sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndex].Text = sum11.ToString();
                        GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;

                    }


                }
                //if (ddlItemCategory.SelectedValue != "2") // for milk category
                //{
                //    foreach (DataColumn column in dt.Columns)
                //    {
                //        if (column.ToString() == "Total Demand in Litre")
                //        {

                //            sum1 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

                //            GridView1.FooterRow.Cells[cellIndex].Text = sum1.ToString("N2");
                //            GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                //            cellIndex = cellIndex + 1;
                //        }
                //    }
                //}
                //if (ddlItemCategory.SelectedValue != "2")
                //{
                int rowcount = GridView1.FooterRow.Cells.Count - 2;
                GridView1.FooterRow.Cells[rowcount].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
                //}
                //else
                //{
                //    int rowcount = GridView1.FooterRow.Cells.Count - 3;
                //    GridView1.FooterRow.Cells[rowcount].Visible = false;
                //    GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
                //    GridView1.FooterRow.Cells[rowcount + 2].Visible = false;
                //}

                ////////////////Start Of Route Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();

                string fm1 = "16/" + txtFromMonth.Text;

                DateTime fmonth1 = DateTime.ParseExact(fm1, "dd/MM/yyyy", culture);
                string f1 = String.Format("{0:MMM-yyyy}", fmonth1);

                string tm1 = "15/" + txtToMonth.Text;
                DateTime tmonth1 = DateTime.ParseExact(tm1, "dd/MM/yyyy", culture);
                string t1 = String.Format("{0:MMM-yyyy}", tmonth1);

                string D = t1 + " To " + f1;
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td class='text-center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb.Append("<td class='text-right'>Month  :-" + D + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                //sb.Append("<td class='text-center'><b>DAY: " + s + "(" + ddlItemCategory.SelectedItem.Text + " SALE)<b></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table table1-bordered'>");
                int Count = ds1.Tables[0].Rows.Count;
                int ColCount = ds1.Tables[0].Columns.Count;
                sb.Append("<thead>");
                sb.Append("<td>S.No</td>");
                sb.Append("<td>NAME</td>");
                for (int j = 0; j < ColCount; j++)
                {


                    if (ds1.Tables[0].Columns[j].ToString() != "DistributorId" && ds1.Tables[0].Columns[j].ToString() != "Distributor/Superstockist")
                    {
                        string ColName = ds1.Tables[0].Columns[j].ToString();
                        sb.Append("<td>" + ColName + "</td>");

                    }

                }
                sb.Append("</thead>");




                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td>" + ds1.Tables[0].Rows[i]["Distributor/Superstockist"] + "</td>");
                    for (int K = 0; K < ColCount; K++)
                    {
                        //if (ds1.Tables[0].Columns[K].ToString() != "S.No." && ds1.Tables[0].Columns[K].ToString() != "RouteId" && ds1.Tables[0].Columns[K].ToString() != "Route" && ds1.Tables[0].Columns[K].ToString() != "Total Supply" && ds1.Tables[0].Columns[K].ToString() != "Total Crate" && ds1.Tables[0].Columns[K].ToString() != "Total Supply in Litre")
                        if (ds1.Tables[0].Columns[K].ToString() != "DistributorId" && ds1.Tables[0].Columns[K].ToString() != "Distributor/Superstockist")
                        {
                            string ColName = ds1.Tables[0].Columns[K].ToString();
                            sb.Append("<td>" + ds1.Tables[0].Rows[i][ColName].ToString() + "</td>");


                        }

                    }
                    sb.Append("</tr>");




                }
                sb.Append("<tr>");
                int ColumnCount = GridView1.FooterRow.Cells.Count - 2;
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (i == 1)
                    {
                        sb.Append("<td><b>" + GridView1.FooterRow.Cells[i].Text + "</b></td>");
                    }
                    else
                    {
                        sb.Append("<td>" + GridView1.FooterRow.Cells[i].Text + "</td>");
                    }



                }
                sb.Append("</tr>");
                sb.Append("</table>");



                ViewState["Sb"] = sb.ToString();

                ////////////////End Of Dist Wise Print Code   ///////////////////////

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnPrintRoutWise.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error DistWise Advanced Card Report ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }

        }
    }
    private void GetRetailerDetails(string did)
    {
        try
        {
            string fm = "16/" + txtFromMonth.Text;
            string tm = "15/" + txtToMonth.Text;
            DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime tmonth = DateTime.ParseExact(tm, "dd/MM/yyyy", culture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds2 = objdb.ByProcedure("USP_Trn_MilkAdvancedCardDetails",
                     new string[] { "flag", "DistributorId", "Shift_id", "ItemCat_id", "FromDate", "ToDate", "Office_ID" },
                       new string[] { "4", did, ddlShift.SelectedValue, "3", fromnonth, tomonth, objdb.Office_ID() }, "dataset");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                btnConsRoutePrint.Visible = true;
                pnlpopupdata.Visible = true;
                DataTable dt = new DataTable();
                dt = ds2.Tables[0];

                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "BoothId" && column.ToString() != "BName")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                GridView4.DataSource = dt;
                GridView4.DataBind();

                GridView4.FooterRow.Cells[1].Text = "Grand Total";
                GridView4.FooterRow.Cells[1].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "BoothId" && column.ToString() != "BName")
                    {

                        sum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView4.FooterRow.Cells[cellIndexbooth].Text = sum22.ToString();
                        GridView4.FooterRow.Cells[cellIndexbooth].Font.Bold = true;
                        cellIndexbooth = cellIndexbooth + 1;
                    }
                }

                //if (ddlItemCategory.SelectedValue != "2") // for milk category
                //{
                //    foreach (DataColumn column in dt.Columns)
                //    {
                //        if (column.ToString() == "Total Demand in Litre")
                //        {

                //            sum2 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

                //            GridView4.FooterRow.Cells[cellIndexbooth].Text = sum2.ToString("N2");
                //            GridView4.FooterRow.Cells[cellIndexbooth].Font.Bold = true;
                //            cellIndexbooth = cellIndexbooth + 1;
                //        }
                //    }
                //}
                //if (ddlItemCategory.SelectedValue != "2")
                //{
                int rowcount1 = GridView4.FooterRow.Cells.Count - 2;
                GridView4.FooterRow.Cells[rowcount1].Visible = false;
                GridView4.FooterRow.Cells[rowcount1 + 1].Visible = false;
                //}
                //else
                //{
                //    int rowcount1 = GridView4.FooterRow.Cells.Count - 4;
                //    GridView4.FooterRow.Cells[rowcount1].Visible = false;
                //    GridView4.FooterRow.Cells[rowcount1 + 1].Visible = false;
                //    GridView4.FooterRow.Cells[rowcount1 + 2].Visible = false;
                //    GridView4.FooterRow.Cells[rowcount1 + 3].Visible = false;
                //}
                ////////////////////
                StringBuilder sb1 = new StringBuilder();


                sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
                sb1.Append("<td></td>");
                sb1.Append("<td class='text-center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("<td class='text-right'>Month  :-" + modaldate.InnerHtml + "</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td>" + modalRootOrDistName.InnerHtml + "</td>");
                sb1.Append("<td></td>");
                sb1.Append("</tr>");

                sb1.Append("</table>");
                sb1.Append("<table class='table table1-bordered'>");
                int Count1 = ds2.Tables[0].Rows.Count;
                int ColCount1 = ds2.Tables[0].Columns.Count;
                sb1.Append("<thead>");
                sb1.Append("<td>S.no</td>");
                sb1.Append("<td>Retailer Name</td>");
                for (int j = 0; j < ColCount1; j++)
                {

                    //if (ds2.Tables[0].Columns[j].ToString() != "S.No." && ds2.Tables[0].Columns[j].ToString() != "tmp_MilkOrProductDemandId" && ds2.Tables[0].Columns[j].ToString() != "tmp_ChallanNo" && ds2.Tables[0].Columns[j].ToString() != "BandOName" && ds2.Tables[0].Columns[j].ToString() != "Total Supply" && ds2.Tables[0].Columns[j].ToString() != "Total Crate" && ds2.Tables[0].Columns[j].ToString() != "Total Supply in Litre")
                    if (ds2.Tables[0].Columns[j].ToString() != "BoothId" && ds2.Tables[0].Columns[j].ToString() != "BName")
                    {
                        string ColName = ds2.Tables[0].Columns[j].ToString();
                        sb1.Append("<td>" + ColName + "</td>");

                    }

                }

                sb1.Append("</thead>");




                for (int i = 0; i < Count1; i++)
                {

                    sb1.Append("<tr>");
                    sb1.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb1.Append("<td>" + ds2.Tables[0].Rows[i]["BName"] + "</td>");
                    for (int K = 0; K < ColCount1; K++)
                    {
                        //if (ds2.Tables[0].Columns[K].ToString() != "S.No." && ds2.Tables[0].Columns[K].ToString() != "tmp_MilkOrProductDemandId" && ds2.Tables[0].Columns[K].ToString() != "tmp_ChallanNo" && ds2.Tables[0].Columns[K].ToString() != "BandOName" && ds2.Tables[0].Columns[K].ToString() != "Total Supply" && ds2.Tables[0].Columns[K].ToString() != "Total Crate" && ds2.Tables[0].Columns[K].ToString() != "Total Supply in Litre")
                        if (ds2.Tables[0].Columns[K].ToString() != "BoothId" && ds2.Tables[0].Columns[K].ToString() != "BName")
                        {
                            string ColName = ds2.Tables[0].Columns[K].ToString();
                            sb1.Append("<td>" + ds2.Tables[0].Rows[i][ColName].ToString() + "</td>");


                        }

                    }



                    sb1.Append("</tr>");




                }
                sb1.Append("<tr>");
                int ColumnCount = GridView4.FooterRow.Cells.Count - 2;
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (i == 1)
                    {
                        sb1.Append("<td><b>" + GridView4.FooterRow.Cells[i].Text + "</b></td>");
                    }
                    else
                    {
                        sb1.Append("<td>" + GridView4.FooterRow.Cells[i].Text + "</td>");
                    }



                }
                sb1.Append("</tr>");
                sb1.Append("</table>");

                Print.InnerHtml = sb1.ToString();

                btnConsRoutePrint.Visible = true;
                //////////////////////////
            }
            else
            {
                pnlpopupdata.Visible = true;
                GridView4.DataSource = null;
                GridView4.DataBind();
                btnConsRoutePrint.Visible = false;
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error DistWise Advanced Card Report ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //e.Row.Cells[2].Controls.Add(lnkbuttton);

            //  int maxrowcell1 = e.Row.Cells.Count - 1;

            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

            //if (ddlItemCategory.SelectedValue != "3")
            //{
            //    e.Row.Cells[maxrowcell1].Visible = false;
            //}
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            // int maxheadercell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

            //if (ddlItemCategory.SelectedValue != "3")
            //{
            //    e.Row.Cells[maxheadercell1].Visible = false;
            //}
        }

    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // int maxrowcell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            //if (ddlItemCategory.SelectedValue != "3")
            //{
            //    e.Row.Cells[maxrowcell1].Visible = false;
            //}
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //int maxheadercell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            //if (ddlItemCategory.SelectedValue != "3")
            //{
            //    e.Row.Cells[maxheadercell1].Visible = false;
            //}
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DistwiseRetailer")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                    LinkButton lnkDistributorId = (LinkButton)row.FindControl("lnkDistributorId");

                    modalRootOrDistName.InnerHtml = lnkDistributorId.Text;

                    string fm1 = "16/" + txtFromMonth.Text;

                    DateTime fmonth1 = DateTime.ParseExact(fm1, "dd/MM/yyyy", culture);
                    string f1 = String.Format("{0:MMM-yyyy}", fmonth1);

                    string tm1 = "15/" + txtToMonth.Text;
                    DateTime tmonth1 = DateTime.ParseExact(tm1, "dd/MM/yyyy", culture);
                    string t1 = String.Format("{0:MMM-yyyy}", tmonth1);

                    modaldate.InnerHtml = t1 + " To " + f1;
                    modelShift.InnerHtml = ddlShift.SelectedItem.Text;


                    GetRetailerDetails(e.CommandArgument.ToString());

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetAdvancedCardDetailsDistWise();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtToMonth.Text = string.Empty;
        txtFromMonth.Text = string.Empty;
        ddlShift.SelectedIndex = 0;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView4.DataSource = null;
        GridView4.DataBind();
        btnPrintRoutWise.Visible = false;
        GridView1.Visible = false;
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {

        Print.InnerHtml = ViewState["Sb"].ToString();
        // Print.InnerHtml += ViewState["CratePrint"].ToString();
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

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
}