using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Demand_DemandDashboard : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3 = new DataSet();
    string FromDate = "", ToDate = "";
    double sum1 = 0;
    int dsum11 = 0;
    int sum11 = 0;
    int cellIndex = 2;
    string itemcatid = "3";
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDemandFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDemandto.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtpayfromdt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtpaytodt.Text = DateTime.Now.ToString("dd/MM/yyyy");

                GetCompareDate();
                GetShift();
                GetDemand();
                //GetPayment();
            }
        }
        else
        {
            objdb.redirectToHome();
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    private void GetOrderCount()
    {
        try
        {
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            FromDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ToDate = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                  new string[] { "flag", "FromDate", "ToDate", "Office_ID" },
                    new string[] { "16", FromDate, ToDate, objdb.Office_ID() }, "dataset");

            if (ds2.Tables[0].Rows.Count > 0)
            {
                lblTotal.Text = ds2.Tables[0].Rows[0]["total"].ToString();
                lblApproved.Text = ds2.Tables[0].Rows[0]["approved"].ToString();
                lblPending.Text = ds2.Tables[0].Rows[0]["pending"].ToString();
                lblRejected.Text = ds2.Tables[0].Rows[0]["rejeced"].ToString();
            }
            else
            {
                lblTotal.Text = "0";
                lblApproved.Text = "0";
                lblPending.Text = "0";
                lblRejected.Text = "0";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Count: ", ex.Message.ToString());
        }

        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    private void GetOrderDetailsByStatus(string status)
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            FromDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ToDate = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Demand_Status" },
                  new string[] { "17", FromDate, ToDate, objdb.Office_ID(), status }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Count: ", ex.Message.ToString());
        }

        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
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
                GetOrderCount();
            }
            else
            {
                lblTotal.Text = "0";
                lblApproved.Text = "0";
                lblPending.Text = "0";
                lblRejected.Text = "0";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error D: ", ex.Message.ToString());
        }
    }
    protected void lnkbtnTotal_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            GetOrderDetailsByStatus("");
        }
    }
    protected void lnkbtnApproved_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            GetOrderDetailsByStatus("3");
        }
    }
    protected void lnkbtnPending_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            GetOrderDetailsByStatus("1");
        }
    }
    protected void lnkbtnRejected_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            GetOrderDetailsByStatus("2");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
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

            }
            else
            {
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetDemand()
    {
        try
        {
            string myStringfromdat = txtDemandFrom.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ds2 = new DataSet();
            string myStringtodate = txtDemandto.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                //DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
                //DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
                FromDate = fdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ToDate = tdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandDashboard",
                      new string[] { "flag", "FromDate", "ToDate", "Office_ID", "DelivaryShift_id", "ItemCat_id" },
                        new string[] { "0", FromDate, ToDate, objdb.Office_ID(), ddlShift.SelectedValue, "3" }, "dataset");

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in ds2.Tables[0].Rows)
                    {
                        foreach (DataColumn column in ds2.Tables[0].Columns)
                        {
                            if (column.ToString() == "Office_ID" && column.ToString() == "Name")
                            {

                                if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                                {
                                    drow[column] = 0;
                                }

                            }

                        }
                    }
                    grdDemand.DataSource = ds2;
                    grdDemand.DataBind();

                }
                else
                {
                    grdDemand.DataSource = null;
                    grdDemand.DataBind();
                }
            }
            else
            {
                grdDemand.DataSource = null;
                grdDemand.DataBind();
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error D: ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void btndemand_Click(object sender, EventArgs e)
    {
        GetDemand();
    }
    protected void grdDemand_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;

            e.Row.Cells[3].Visible = false;
        }


    }
    protected void grdDemand_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = string.Empty;
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        switch (e.CommandName)
        {
            case "RecordUpdate":
                FillRouteWise();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowReport();", true);
                break;
        }
    }


    private void FillRouteWise()
    {
        try
        {
            Session["Task"] = null;
            string myStringfromdat = txtDemandFrom.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ds3 = new DataSet();
            string myStringtodate = txtDemandto.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                FromDate = fdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ToDate = tdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandDashboard",
                      new string[] { "Flag", "FromDate", "ToDate", "Office_ID", "DelivaryShift_id", "ItemCat_id" },
                        new string[] { "1", FromDate, ToDate, hdnvalue.Value, ddlShift.SelectedValue, "3" }, "dataset");

                if (ds3.Tables[0].Rows.Count > 0)
                {
                    //ds3.Tables[0].Columns.RemoveAt(0);
                    //GridView2.DataSource = ds3;
                    //GridView2.DataBind();

                    DataTable dt1 = new DataTable();
                    dt1 = ds3.Tables[0];

                    foreach (DataRow drow in dt1.Rows)
                    {
                        foreach (DataColumn column in dt1.Columns)
                        {
                            if (column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Supply" && column.ToString() != "Total Supply in Litre")
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
                    Session["Task"] = dt1;
                    GridView2.FooterRow.Cells[1].Text = "Total Supply";
                    GridView2.FooterRow.Cells[1].Font.Bold = true;
                    foreach (DataColumn column in dt1.Columns)
                    {
                        if (column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Supply" && column.ToString() != "Total Supply in Litre")
                        {

                            sum11 = Convert.ToInt32(dt1.Compute("SUM([" + column + "])", string.Empty));

                            GridView2.FooterRow.Cells[cellIndex].Text = sum11.ToString();
                            GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                            cellIndex = cellIndex + 1;
                        }
                    }
                    foreach (DataColumn column in dt1.Columns)
                    {
                        if (column.ToString() == "Total Supply")
                        {

                            dsum11 = Convert.ToInt32(dt1.Compute("SUM([" + column + "])", string.Empty));

                            GridView2.FooterRow.Cells[cellIndex].Text = dsum11.ToString();
                            GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                            cellIndex = cellIndex + 1;
                        }
                    }
                    if (itemcatid != "2") // for milk category
                    {
                        foreach (DataColumn column in dt1.Columns)
                        {
                            if (column.ToString() == "Total Supply in Litre")
                            {

                                sum1 = Convert.ToDouble(dt1.Compute("SUM([" + column + "])", string.Empty));

                                GridView2.FooterRow.Cells[cellIndex].Text = sum1.ToString("N2");
                                GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                                cellIndex = cellIndex + 1;
                            }
                        }
                    }
                    if (itemcatid != "2")
                    {
                        int rowcount = GridView2.FooterRow.Cells.Count - 2;
                        GridView2.FooterRow.Cells[rowcount].Visible = false;
                        GridView2.FooterRow.Cells[rowcount + 1].Visible = false;
                    }
                    else
                    {
                        int rowcount = GridView2.FooterRow.Cells.Count - 3; // previous default is : 4
                        GridView2.FooterRow.Cells[rowcount].Visible = false;
                        GridView2.FooterRow.Cells[rowcount + 1].Visible = false;
                        GridView2.FooterRow.Cells[rowcount + 2].Visible = false;

                    }

                    if (dt1 != null) { dt1.Dispose(); }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error D: ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int maxrowcell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

            if (itemcatid != "3")
            {
                e.Row.Cells[maxrowcell1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            int maxheadercell1 = e.Row.Cells.Count - 1;  // previous default is : 2
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            if (itemcatid != "3")
            {
                e.Row.Cells[maxheadercell1].Visible = false;
            }
        }
    }
    //private void GetPayment()
    //{
    //    try
    //    {
    //        string myStringfromdat = txtpayfromdt.Text; // From Database
    //        DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //        ds2 = new DataSet();
    //        string myStringtodate = txtpaytodt.Text; // From Database
    //        DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //        if (fdate <= tdate)
    //        {
    //            lblMsg.Text = string.Empty;
    //            //DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
    //            //DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
    //            FromDate = fdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //            ToDate = tdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

    //            ds2 = objdb.ByProcedure("USP_Trn_DistributorPaymentSheet_OfficeID_List",
    //                  new string[] { "flag", "OfficeID", "FromDT", "ToDate" },
    //                    new string[] { "0", objdb.Office_ID(), FromDate, ToDate }, "dataset");


    //            grdPaydetail.DataSource = ds2;
    //            grdPaydetail.DataBind();

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error D: ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds2 != null) { ds2.Dispose(); }
    //    }
    //}
    //protected void btnpaydetail_Click(object sender, EventArgs e)
    //{
    //    GetPayment();

    //}
    //private void FillPayment()
    //{
    //    try
    //    {
    //        string myStringfromdat = txtpayfromdt.Text; // From Database
    //        DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //        ds3 = new DataSet();
    //        string myStringtodate = txtpaytodt.Text; // From Database
    //        DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //        if (fdate <= tdate)
    //        {
    //            lblMsg.Text = string.Empty;
    //            FromDate = fdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //            ToDate = tdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

    //            ds3 = objdb.ByProcedure("USP_Trn_DistributorPaymentSheet_OfficeID_RouteWise_List",
    //                  new string[] { "flag", "OfficeID", "FromDT", "ToDate" },
    //                    new string[] { "0", hdnofficeid.Value, FromDate, ToDate }, "dataset");


    //            GridView3.DataSource = ds3;
    //            GridView3.DataBind();
               
    //            //decimal MilkAmount = ds3.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
    //            //decimal PaidAmt = ds3.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaidAmt"));
    //            //decimal Balance = ds3.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Balance"));
    //           // GridView1.FooterRow.Cells[6].Text = "<b>Total</b>";
    //            //GridView1.Columns[6].FooterText = "<b>Total</b>";
    //            //GridView1.Columns[7].FooterText = "<b>" + MilkAmount.ToString() + "</b>";
    //            //GridView1.Columns[8].FooterText = "<b>" + PaidAmt.ToString() + "</b>";
    //            //GridView1.Columns[9].FooterText = "<b>" + Balance.ToString() + "</b>";
    //            //GridView1.FooterRow.Cells[7].Text = "<b>" + MilkAmount.ToString() + "</b>";
    //            //GridView1.FooterRow.Cells[8].Text = "<b>" + PaidAmt.ToString() + "</b>";
    //            //GridView1.FooterRow.Cells[9].Text = "<b>" + Balance.ToString() + "</b>";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error D: ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds3 != null) { ds3.Dispose(); }
    //    }
    //}
    //protected void grdPaydetail_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    lblMsg.Text = string.Empty;
    //    hdnofficeid.Value = Convert.ToString(e.CommandArgument);
    //    switch (e.CommandName)
    //    {
    //        case "RecordUpdate":
    //            FillPayment();
    //            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPayReport();", true);
    //            break;
    //    }
    //}
    protected void btnprint_Click(object sender, EventArgs e)
    {
          
        DataTable dt  =(DataTable)Session["Task"] ;
        Export(dt);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowReport();", true);
    }
    private void Export(DataTable dt)
    {
        string attachment = "attachment; filename=RouteWise.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "";
        foreach (DataColumn dc in dt.Columns)
        {
            Response.Write(tab + dc.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString());
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
}