using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Demand_UpdateApprovedDemand : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1 = new DataSet();
    double sum1, sum2 = 0, sum3 = 0;
    int sum11, sum22 = 0, sum33 = 0;
    int cellIndex = 2;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetBackRecord();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
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
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));

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
    private void GetApprovedDemandRouteWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductApprovedDemandRDIwise",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                       new string[] { "1", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //if (column.ToString() != "S.No." && column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Demand" && column.ToString() != "Total Demand in Litre'")
                        if (column.ToString() != "S.No." && column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Demand")
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

                GridView1.FooterRow.Cells[1].Text = "Total Demand";
                GridView1.FooterRow.Cells[1].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {
                    //if (column.ToString() != "S.No." && column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Demand in Litre")
                    if (column.ToString() != "S.No." && column.ToString() != "RouteId" && column.ToString() != "Route")
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
               // if (ddlItemCategory.SelectedValue != "2")
              //  {
                    int rowcount = GridView1.FooterRow.Cells.Count - 2;
                    GridView1.FooterRow.Cells[rowcount].Visible = false;
                    GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
              //  }
                //else
                //{
                //    int rowcount = GridView1.FooterRow.Cells.Count - 3;
                //    GridView1.FooterRow.Cells[rowcount].Visible = false;
                //    GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
                //    GridView1.FooterRow.Cells[rowcount + 2].Visible = false;
                //}
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Routewise demand Report ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetApprovedDemandDistributorWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductApprovedDemandRDIwise",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                       new string[] { "2", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //if (column.ToString() != "S.No." && column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist Name" && column.ToString() != "TotalSupply" && column.ToString() != "Total Demand in Litre")
                        if (column.ToString() != "S.No." && column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist Name" && column.ToString() != "Total Demand")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                // dt.Columns.Remove("DistributorId");
                GridView2.DataSource = dt;
                GridView2.DataBind();

                GridView2.FooterRow.Cells[1].Text = "Total Demand";
                GridView2.FooterRow.Cells[1].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {
                    //if (column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist Name" && column.ToString() != "Total Demand in Litre")
                    if (column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist Name")
                    {

                        sum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView2.FooterRow.Cells[cellIndex].Text = sum22.ToString();
                        GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }
                //if (ddlItemCategory.SelectedValue != "2") // for milk category
                //{
                //    foreach (DataColumn column in dt.Columns)
                //    {
                //        if (column.ToString() == "Total Demand in Litre")
                //        {

                //            sum2 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

                //            GridView2.FooterRow.Cells[cellIndex].Text = sum2.ToString("N2");
                //            GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                //            cellIndex = cellIndex + 1;
                //        }
                //    }
                //}
                //if (ddlItemCategory.SelectedValue != "2")
               // {
                    int rowcount = GridView2.FooterRow.Cells.Count - 2;
                    GridView2.FooterRow.Cells[rowcount].Visible = false;
                    GridView2.FooterRow.Cells[rowcount + 1].Visible = false;
               // }
                //else
                //{
                //    int rowcount = GridView2.FooterRow.Cells.Count - 3;
                //    GridView2.FooterRow.Cells[rowcount].Visible = false;
                //    GridView2.FooterRow.Cells[rowcount + 1].Visible = false;
                //    GridView2.FooterRow.Cells[rowcount + 2].Visible = false;
                //}

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Distwise demand Report ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void GetApprovedDemandInstWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductApprovedDemandRDIwise",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                       new string[] { "3", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //if (column.ToString() != "OrganizationId" && column.ToString() != "Organization Name" && column.ToString() != "Total Demand" && column.ToString() != "Total Demand in Litre")
                        if (column.ToString() != "OrganizationId" && column.ToString() != "Organization Name" && column.ToString() != "Total Demand")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                // dt.Columns.Remove("DistributorId");
                GridView3.DataSource = dt;
                GridView3.DataBind();

                GridView3.FooterRow.Cells[1].Text = "Total Demand";
                GridView3.FooterRow.Cells[1].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {
                    //if (column.ToString() != "OrganizationId" && column.ToString() != "Organization Name" && column.ToString() != "Total Demand in Litre")
                    if (column.ToString() != "OrganizationId" && column.ToString() != "Organization Name")
                    {

                        sum33 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView3.FooterRow.Cells[cellIndex].Text = sum33.ToString();
                        GridView3.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }
                //if (ddlItemCategory.SelectedValue != "2") // for milk category
                //{
                //    foreach (DataColumn column in dt.Columns)
                //    {
                //        if (column.ToString() == "Total Demand in Litre")
                //        {

                //            sum3 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

                //            GridView3.FooterRow.Cells[cellIndex].Text = sum3.ToString("N2");
                //            GridView3.FooterRow.Cells[cellIndex].Font.Bold = true;
                //            cellIndex = cellIndex + 1;
                //        }
                //    }
                //}
               // if (ddlItemCategory.SelectedValue != "2")
               // {
                    int rowcount = GridView3.FooterRow.Cells.Count - 2;
                    GridView3.FooterRow.Cells[rowcount].Visible = false;
                    GridView3.FooterRow.Cells[rowcount + 1].Visible = false;
               // }
               // else
                //{
                //    int rowcount = GridView3.FooterRow.Cells.Count - 3;
                //    GridView3.FooterRow.Cells[rowcount].Visible = false;
                //    GridView3.FooterRow.Cells[rowcount + 1].Visible = false;
                //    GridView3.FooterRow.Cells[rowcount + 2].Visible = false;
                //}

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Institution wise Approved demand Report ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetApprovedDemandStatusByRoute()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView1.Visible = true;
            GridView2.Visible = false;
            GridView3.Visible = false;
            pnlData.Visible = true;
            pnlrouteOrDistOrInstdata.Visible = true;
            GetApprovedDemandRouteWise();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void GetApprovedDemandStatusByDistributor()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView2.Visible = true;
            GridView1.Visible = false;
            GridView3.Visible = false;
            pnlData.Visible = true;
            pnlrouteOrDistOrInstdata.Visible = true;
            GetApprovedDemandDistributorWise();



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
    private void GetApprovedDemandStatusByInstitution()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView1.Visible = false;
            GridView2.Visible = false;
            GridView3.Visible = true;
            pnlData.Visible = true;
            pnlrouteOrDistOrInstdata.Visible = true;

            GetApprovedDemandInstWise();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    #endregion========================================================
    #region=========== init or changed even===========================
    protected void ddlShift_Init(object sender, EventArgs e)
    {
        GetShift();
    }
    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetCategory();
    }
    //protected void txtOrderDate_TextChanged(object sender, EventArgs e)
    //{
    //    ddlShift.SelectedIndex = 0;
    //    ddlItemCategory.SelectedIndex = 0;
    //    pnlSearchBy.Visible = false;
    //    rblReportType.ClearSelection();
    //    pnlData.Visible = false;
    //    pnlrouteOrDistOrInstdata.Visible = false;
    //}
    //protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (txtOrderDate.Text != "" || ddlShift.SelectedValue != "0")
    //    {
    //        ddlItemCategory.SelectedIndex = 0;
    //        pnlSearchBy.Visible = false;
    //        rblReportType.ClearSelection();
    //        pnlData.Visible = false;
    //        pnlrouteOrDistOrInstdata.Visible = false;
    //    }

    //}
    //protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (txtOrderDate.Text != "" && ddlShift.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0")
    //    {
    //        pnlSearchBy.Visible = true;
    //        rblReportType.ClearSelection();
    //        pnlData.Visible = false;
    //        pnlrouteOrDistOrInstdata.Visible = false;
    //        //pnlTotalFatSNF.Visible = true;
    //    }
    //    else
    //    {
    //        rblReportType.ClearSelection();
    //        pnlSearchBy.Visible = false;
    //        pnlData.Visible = false;
    //        pnlrouteOrDistOrInstdata.Visible = false;
    //    }
    //}
    private void callrblReportType()
    {
        if (rblReportType.SelectedValue == "1")
        {
            pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details";
            GetApprovedDemandStatusByRoute();
        }
        else if (rblReportType.SelectedValue == "2")
        {
            pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details";
            GetApprovedDemandStatusByDistributor();
        }
        else if (rblReportType.SelectedValue == "3")
        {
            pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details";
            GetApprovedDemandStatusByInstitution();

        }
        else
        {
            pnlData.Visible = false;
            pnlrouteOrDistOrInstdata.Visible = false;

        }
    }
    //protected void rblReportType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rblReportType.SelectedValue == "1")
    //    {
    //        pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details";
    //        GetApprovedDemandStatusByRoute();
    //    }
    //    else if (rblReportType.SelectedValue == "2")
    //    {
    //        pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details";
    //        GetApprovedDemandStatusByDistributor();
    //    }
    //    else if (rblReportType.SelectedValue == "3")
    //    {
    //        pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details";
    //        GetApprovedDemandStatusByInstitution();

    //    }
    //    else
    //    {
    //        pnlData.Visible = false;
    //        pnlrouteOrDistOrInstdata.Visible = false;

    //    }
    //}
    #endregion=====================================================


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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
           // int maxheadercell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

            //if (ddlItemCategory.SelectedValue != "3")
            //{
            //    e.Row.Cells[maxheadercell1].Visible = false;
            //}
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // int maxrowcell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

            //if (ddlItemCategory.SelectedValue != "3")
            //{
            //    e.Row.Cells[maxrowcell1].Visible = false;
            //    e.Row.Cells[maxrowcell1 + 1].Visible = false;
            //}
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
          //  int maxheadercell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            //if (ddlItemCategory.SelectedValue != "3")
            //{
            //    e.Row.Cells[maxheadercell1].Visible = false;
            //    e.Row.Cells[maxheadercell1 + 1].Visible = false;
            //}
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RoutwiseBooth")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    LinkButton lnkbtnRoute = (LinkButton)row.FindControl("lnkbtnRoute");
                    Session["DDate"] = txtOrderDate.Text;
                    Session["DShift"] = ddlShift.SelectedValue;
                    Session["DShiftName"] = ddlShift.SelectedItem.Text;
                    Session["DCategory"] = ddlItemCategory.SelectedValue;
                    Session["DCategoryName"] = ddlItemCategory.SelectedItem.Text;
                    Session["DRDIType"] = rblReportType.SelectedValue;
                    Session["D_RDIName"] = lnkbtnRoute.Text;
                    Session["D_RouteId"] = e.CommandArgument.ToString();
                    Session["D_DistributorId"] = "0";
                    Session["D_OrganizationId"] = "0";
                    Response.Redirect("UpdateApprovedDemandChild.aspx");

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DistwiseBooth")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                   
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    
                    LinkButton lnkbtnDistributor = (LinkButton)row.FindControl("lnkbtnDistributor");

                    Session["DDate"] = txtOrderDate.Text;
                    Session["DShift"] = ddlShift.SelectedValue;
                    Session["DShiftName"] = ddlShift.SelectedItem.Text;
                    Session["DCategory"] = ddlItemCategory.SelectedValue;
                    Session["DCategoryName"] = ddlItemCategory.SelectedItem.Text;
                    Session["DRDIType"] = rblReportType.SelectedValue;
                    Session["D_RDIName"] = lnkbtnDistributor.Text;
                    Session["D_RouteId"] = "0";
                    Session["D_DistributorId"] = e.CommandArgument.ToString();
                    Session["D_OrganizationId"] = "0";
                    Response.Redirect("UpdateApprovedDemandChild.aspx");
                }
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

    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Orgwise")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;

                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    LinkButton lnkbtnOrganization = (LinkButton)row.FindControl("lnkbtnOrganization");
                    Session["DDate"] = txtOrderDate.Text;
                    Session["DShift"] = ddlShift.SelectedValue;
                    Session["DShiftName"] = ddlShift.SelectedItem.Text;
                    Session["DCategory"] = ddlItemCategory.SelectedValue;
                    Session["DCategoryName"] = ddlItemCategory.SelectedItem.Text;
                    Session["DRDIType"] = rblReportType.SelectedValue;
                    Session["D_RDIName"] = lnkbtnOrganization.Text;
                    Session["D_RouteId"] = "0";
                    Session["D_DistributorId"] = "0";
                    Session["D_OrganizationId"] = e.CommandArgument.ToString();
                    Response.Redirect("UpdateApprovedDemandChild.aspx");
                }
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
    private void GetBackRecord()
    {
        if (Session["DDate"] != null && Session["DShift"] != null && Session["DShiftName"] != null && Session["DRDIType"] != null)
        {
            txtOrderDate.Text = Session["DDate"].ToString();
            ddlShift.SelectedValue = Session["DShift"].ToString();
            ddlItemCategory.SelectedValue = Session["DCategory"].ToString();
            rblReportType.SelectedValue = Session["DRDIType"].ToString();
            pnlSearchBy.Visible = true;
            callrblReportType();
            Session["DDate"] = null;
            Session["DShift"] = null;
            Session["DCategory"] = null;
            Session["DRDIType"] = null;
            Session["DCategoryName"] = null;
            Session["D_RDIName"] = null;
            Session["DShiftName"] = null;
            Session["D_RouteId"] = null;
            Session["D_DistributorId"] = null;
            Session["D_OrganizationId"] = null;
        }
        else
        {
            lblMsg.Text = string.Empty;
            txtOrderDate.Text = string.Empty;
            ddlShift.SelectedIndex = 0;
            ddlItemCategory.SelectedIndex = 0;
            rblReportType.ClearSelection();
            pnlData.Visible = false;
            pnlrouteOrDistOrInstdata.Visible = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            callrblReportType();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtOrderDate.Text = string.Empty;
        ddlShift.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
        rblReportType.ClearSelection();
        pnlData.Visible = false;
        pnlrouteOrDistOrInstdata.Visible = false;
    }
}