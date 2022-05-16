using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_MilkOrProductChallanDetailsByDistributor : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds,ds1 = new DataSet();
    string orderdate = "";
    double sum4 = 0;
    int sum44 = 0;
    int dsum44 = 0;
    int csum44 = 0;
    int cellIndexboothOrg = 1;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.GetItemCat_id()!=null)
        {
            if (!Page.IsPostBack)
            {
                GetShift();
                GetCategory();
                GetOfficeDetails();
                GetRouteIDByDistributor();
                GetRetailer();
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
    #region=======================user defined function========================
    private void GetRouteIDByDistributor()
    {
        try
        {
            // ds = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     // new string[] { "flag", "Office_ID", "DistributorId" },
                       // new string[] { "4", objdb.Office_ID(), objdb.createdBy() }, "dataset");
					   
			ds = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                    new string[] { "flag", "Office_ID", "DistributorId", "ItemCat_id" },
                      new string[] { "4", objdb.Office_ID(), objdb.createdBy(), ddlItemCategory.SelectedValue }, "dataset");		   

            if (ds.Tables[0].Rows.Count != 0)
            {
                ViewState["RouteId"] = ds.Tables[0].Rows[0]["RouteId"].ToString();
            }
            else
            {
                ViewState["RouteId"] = "0";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error route ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
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
    private void Clear()
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
       // ddlShift.SelectedIndex = 0;

    }
    protected void GetShift()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds;
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
            else
            {
                ddlShift.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void GetCategory()
    {
        try
        {
            
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds;
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
                ddlItemCategory.SelectedValue = objdb.GetItemCat_id();
            }
            else
            {
                ddlItemCategory.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetRetailer()
    {
        try
        {
            //ds = objdb.ByProcedure("USP_Mst_DistributorParlourMapping",
            //         new string[] { "flag", "DistributorId" },
            //           new string[] { "6", objdb.createdBy() }, "dataset");
            ddlRetailer.Items.Clear();
            
            ds = objdb.ByProcedure("USP_Mst_BoothReg",
                   new string[] { "flag", "RouteId", "ItemCat_id" },
                     new string[] { "12", ViewState["RouteId"].ToString(),ddlItemCategory.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlRetailer.DataTextField = "BoothName";
                ddlRetailer.DataValueField = "BoothId";
                ddlRetailer.DataSource = ds;
                ddlRetailer.DataBind();
                ddlRetailer.Items.Insert(0, new ListItem("All", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void GetItemDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                new string[] { "flag", "FromDate", "ToDate", "Shift_id", "ItemCat_id", "BoothId", "DistributorId" },
                       new string[] { "12", fromdat.ToString(), todat.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, ddlRetailer.SelectedValue, objdb.createdBy() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {

                pnldata.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
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
            if (ds != null) { ds.Dispose(); }
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
                GetItemDetails();
            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6: ", ex.Message.ToString());
        }
    }
    #endregion====================================end of user defined function

    #region=============== init or changed event for controls =================

    //protected void ddlShift_Init(object sender, EventArgs e)
    //{
    //    GetShift();
    //}
    //protected void ddlItemCategory_Init(object sender, EventArgs e)
    //{
    //    GetCategory();
    //}
    #endregion============ end of changed event for controls===========


    #region============ button click event & GridView Event ============================
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        Clear();
    }

    #endregion=============end of button click funciton==================


    //protected void ddlRetailer_Init(object sender, EventArgs e)
    //{
    //    GetRetailer();
    //}
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ChallanNo")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblBoothName = (Label)row.FindControl("lblBoothName");
                    LinkButton lnkChallanNo = (LinkButton)row.FindControl("lnkChallanNo");
                    Label lblDeliveryDate = (Label)row.FindControl("lblDeliveryDate");
                    Label lblDelivaryShift = (Label)row.FindControl("lblDelivaryShift");
                    Label lblItemCatid = (Label)row.FindControl("lblItemCatid");
                    Label lblDelivaryShiftid = (Label)row.FindControl("lblDelivaryShiftid");
                    Label lblItemCatName = (Label)row.FindControl("lblItemCatName");
                    ViewState["ItemCategory"] = lblItemCatid.Text;
                    modalcategory.InnerHtml = lblItemCatName.Text;
                    modalchallanno.InnerHtml = lnkChallanNo.Text;
                    modalBoothName.InnerHtml = lblBoothName.Text;
                    modaldate.InnerHtml = lblDeliveryDate.Text;
                    modelShift.InnerHtml = lblDelivaryShift.Text;
                    DateTime date3 = DateTime.ParseExact(lblDeliveryDate.Text, "dd/MM/yyyy", culture);
                    string deliverydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Delivery_Date", "MilkOrProductDemandId" },
                       new string[] { "11", objdb.Office_ID(), lblDelivaryShiftid.Text, lblItemCatid.Text.Trim(), deliverydate, e.CommandArgument.ToString() }, "dataset");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {

                        DataTable dt1 = new DataTable();
                        dt1 = ds1.Tables[0];

                        foreach (DataRow drow in dt1.Rows)
                        {
                            foreach (DataColumn column in dt1.Columns)
                            {
                                //if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_ChallanNo" && column.ToString() != "BName" && column.ToString() != "Total Supply" && column.ToString() != "Total Crate" && column.ToString() != "Total Supply in Litre")
                                if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_ChallanNo" && column.ToString() != "BName" && column.ToString() != "Total Supply")
                                {

                                    if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                                    {
                                        drow[column] = 0;
                                    }

                                }

                            }
                        }
                        GridView2.DataSource = dt1;
                        GridView2.DataBind();

                        GridView2.FooterRow.Cells[0].Text = "Total Supply";
                        GridView2.FooterRow.Cells[0].Font.Bold = true;
                        foreach (DataColumn column in dt1.Columns)
                        {
                            //if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_ChallanNo" && column.ToString() != "BName" && column.ToString() != "Total Supply" && column.ToString() != "Total Crate" && column.ToString() != "Total Supply in Litre")
                            if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_ChallanNo" && column.ToString() != "BName" && column.ToString() != "Total Supply")
                            {

                                sum44 = Convert.ToInt32(dt1.Compute("SUM([" + column + "])", string.Empty));

                                GridView2.FooterRow.Cells[cellIndexboothOrg].Text = sum44.ToString();
                                GridView2.FooterRow.Cells[cellIndexboothOrg].Font.Bold = true;
                                cellIndexboothOrg = cellIndexboothOrg + 1;
                            }
                        }
                        foreach (DataColumn column in dt1.Columns)
                        {
                            if (column.ToString() == "Total Supply")
                            {

                                dsum44 = Convert.ToInt32(dt1.Compute("SUM([" + column + "])", string.Empty));

                                GridView2.FooterRow.Cells[cellIndexboothOrg].Text = dsum44.ToString();
                                GridView2.FooterRow.Cells[cellIndexboothOrg].Font.Bold = true;
                                cellIndexboothOrg = cellIndexboothOrg + 1;
                            }
                        }
                       // if (lblItemCatid.Text != "2") // for milk category
                       // {
                            //foreach (DataColumn column in dt1.Columns)
                            //{
                            //    if (column.ToString() == "Total Crate")
                            //    {

                            //        csum44 = Convert.ToInt32(dt1.Compute("SUM([" + column + "])", string.Empty));

                            //        GridView2.FooterRow.Cells[cellIndexboothOrg].Text = csum44.ToString();
                            //        GridView2.FooterRow.Cells[cellIndexboothOrg].Font.Bold = true;
                            //        cellIndexboothOrg = cellIndexboothOrg + 1;
                            //    }
                            //}
                            //foreach (DataColumn column in dt1.Columns)
                            //{
                            //    if (column.ToString() == "Total Supply in Litre")
                            //    {

                            //        sum4 = Convert.ToDouble(dt1.Compute("SUM([" + column + "])", string.Empty));

                            //        GridView2.FooterRow.Cells[cellIndexboothOrg].Text = sum4.ToString("N2");
                            //        GridView2.FooterRow.Cells[cellIndexboothOrg].Font.Bold = true;
                            //        cellIndexboothOrg = cellIndexboothOrg + 1;
                            //    }
                            //}
                      //  }
                        //if (lblItemCatid.Text != "2")
                      //  {
                            int rowcount1 = GridView2.FooterRow.Cells.Count - 3;
                            GridView2.FooterRow.Cells[rowcount1].Visible = false;
                            GridView2.FooterRow.Cells[rowcount1 + 1].Visible = false;
                            GridView2.FooterRow.Cells[rowcount1 + 2].Visible = false;
                            dt1.Dispose();
                        //}
                        //else
                        //{
                        //    int rowcount1 = GridView2.FooterRow.Cells.Count - 4; //previous 5
                        //    GridView2.FooterRow.Cells[rowcount1].Visible = false;
                        //    GridView2.FooterRow.Cells[rowcount1 + 1].Visible = false;
                        //    GridView2.FooterRow.Cells[rowcount1 + 2].Visible = false;
                        //    GridView2.FooterRow.Cells[rowcount1 + 3].Visible = false;
                        //  //  GridView2.FooterRow.Cells[rowcount1 + 4].Visible = false;
                        //    dt1.Dispose();
                        //}
                        ////////////////Start Of Parlor Wise Print Code   ///////////////////////
                        StringBuilder sb = new StringBuilder();
                        int Count = ds1.Tables[0].Rows.Count;
                        int ColCount = ds1.Tables[0].Columns.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            string ChallNo = ds1.Tables[0].Rows[i]["tmp_ChallanNo"].ToString();
                            string[] OrderNo = ChallNo.Split('C', 'H');
                            sb.Append("<table class='table1' style='width:100%; height:100%'>");
                            sb.Append("<tr>");
                            sb.Append("<td>OrdNo: " + OrderNo[2].ToString() + "</td>");
                            sb.Append("<td><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                            sb.Append("<td>Challan: " + ds1.Tables[0].Rows[i]["tmp_ChallanNo"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td></td>");
                            sb.Append("<td><b>Phone: " + ViewState["Office_ContactNo"].ToString() + "<b></td>");
                            sb.Append("<td></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");


                            sb.Append("<td>Route No: " + modalBoothName.InnerText + "</td>");

                            sb.Append("<td>Date:  " + modaldate.InnerText + "</td>");
                            sb.Append("<td>Shift:  " + modelShift.InnerText + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            string BandOName = ds1.Tables[0].Rows[i]["BName"].ToString();
                            string[] Booth = BandOName.Split('[');
                            string[] BN0 = Booth[1].Split(']');
                            sb.Append("<td>Booth Name:  " + Booth[0].ToString() + "</td>");
                            sb.Append("<td></td>");
                            sb.Append("<td>(Bno): " + BN0[0].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            sb.Append("<table class='table' style='width:100%; height:100%'>");
                            sb.Append("<tr>");
                            sb.Append("<td style='border: 1px solid #000000 !important;'>" + modalcategory.InnerHtml + "</td>");
                            for (int j = 0; j < ColCount; j++)
                            {
                                //if (ds1.Tables[0].Columns[j].ToString() != "S.No." && ds1.Tables[0].Columns[j].ToString() != "tmp_MilkOrProductDemandId" && ds1.Tables[0].Columns[j].ToString() != "tmp_ChallanNo" && ds1.Tables[0].Columns[j].ToString() != "BName" && ds1.Tables[0].Columns[j].ToString() != "Total Supply" && ds1.Tables[0].Columns[j].ToString() != "Total Crate" && ds1.Tables[0].Columns[j].ToString() != "Total Supply in Litre")
                                if (ds1.Tables[0].Columns[j].ToString() != "S.No." && ds1.Tables[0].Columns[j].ToString() != "tmp_MilkOrProductDemandId" && ds1.Tables[0].Columns[j].ToString() != "tmp_ChallanNo" && ds1.Tables[0].Columns[j].ToString() != "BName" && ds1.Tables[0].Columns[j].ToString() != "Total Supply")
                                {
                                    string ColName = ds1.Tables[0].Columns[j].ToString();
                                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + ColName + "</td>");

                                }

                            }
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td style='border: 1px solid #000000 !important;'>Supply</td>");
                            for (int K = 0; K < ColCount; K++)
                            {
                                //if (ds1.Tables[0].Columns[K].ToString() != "S.No." && ds1.Tables[0].Columns[K].ToString() != "tmp_MilkOrProductDemandId" && ds1.Tables[0].Columns[K].ToString() != "tmp_ChallanNo" && ds1.Tables[0].Columns[K].ToString() != "BName" && ds1.Tables[0].Columns[K].ToString() != "Total Supply" && ds1.Tables[0].Columns[K].ToString() != "Total Crate" && ds1.Tables[0].Columns[K].ToString() != "Total Supply in Litre")
                                if (ds1.Tables[0].Columns[K].ToString() != "S.No." && ds1.Tables[0].Columns[K].ToString() != "tmp_MilkOrProductDemandId" && ds1.Tables[0].Columns[K].ToString() != "tmp_ChallanNo" && ds1.Tables[0].Columns[K].ToString() != "BName" && ds1.Tables[0].Columns[K].ToString() != "Total Supply")
                                {
                                    string ColName = ds1.Tables[0].Columns[K].ToString();
                                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds1.Tables[0].Rows[i][ColName].ToString() + "</td>");


                                }

                            }
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            //sb.Append("<span style='padding-top:20px;'>Crate Issue:  " + ds1.Tables[0].Rows[i]["Total Crate"].ToString() + "</span>");
                            sb.Append("<span style='padding-top:20px;'>Crate Issue:  " + "-----" + "</span>");
                            if (i == (Count - 1))
                            {

                            }
                            else
                            {
                                sb.Append("<div class='pagebreak'></div>");
                            }


                        }
                        Print.InnerHtml = sb.ToString();
                        btnParlorWisePrint.Visible = true;
                    }
                    else
                    {
                        GridView2.DataSource = null;
                        GridView2.DataBind();
                        btnParlorWisePrint.Visible = false;
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    GetDatatableHeaderDesign();

                }
            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7: " + ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               // int maxBrowcell1 = e.Row.Cells.Count - 1; // previous 2
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;


                //if (ViewState["ItemCategory"].ToString() != "3")
                //{
                //    e.Row.Cells[maxBrowcell1].Visible = false;
                //   // e.Row.Cells[maxBrowcell1 + 1].Visible = false;
                //}
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
               // int maxBheadercell1 = e.Row.Cells.Count - 1; // previous 2
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                //if (ViewState["ItemCategory"].ToString() != "3")
                //{
                //    e.Row.Cells[maxBheadercell1].Visible = false;
                //   // e.Row.Cells[maxBheadercell1 + 1].Visible = false;
                //}
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
    }    
    protected void GetOfficeDetails()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 9 ", ex.Message.ToString());
        }
    }
    //protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetRetailer();
    //}
}