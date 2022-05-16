using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_SupplyListRouteOrDistWise_New : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3, ds4 = new DataSet();
    double sum1, sum2 = 0, sum3 = 0;
    int sum11 = 0, sum22 = 0, sum33 = 0;
    int dsum11 = 0, dsum22 = 0, dsum33 = 0;
    int csum11 = 0, csum22 = 0, csum33 = 0;
    int cellIndex = 2;
    string deliverydat = "",delishit="";
    int recordyn = 0;

    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetBackRecord();
            }

            if (txtOrderDate.Text != "" && ddlShift.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0")
            {
                if(rblReportType.SelectedValue=="1")
                {
                    BindGrid();
                }
              
            }

        }
        else
        {
            objdb.redirectToHome();
        }
    }


    private void BindGrid()
    {

        //GVCMNEW.Columns.Clear();


        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
        string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        DataSet dSNew = objdb.ByProcedure("USP_Trn_MilkOrProductDmandAprovedListForSuply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                       new string[] { "1", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate }, "dataset");



        if (ViewState["DynamicGridBind"] == null)
        {
            foreach (DataColumn column in dSNew.Tables[0].Columns)
            {
                TemplateField tfield = new TemplateField();
                tfield.HeaderText = column.ColumnName;
                GVCMNEW.Columns.Add(tfield);
            }
        }

        if (dSNew.Tables[0].Rows.Count>0)
        {
            GVCMNEW.DataSource = dSNew;
            GVCMNEW.DataBind();
            GetApprovedDemandRouteWise_New();
            ViewState["DynamicGridBind"] = "1";
            btnsave.Visible = true;
        }
        else
        {
            btnsave.Visible = false;
        }
        

    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DataSet dSNews = objdb.ByProcedure("USP_Trn_MilkOrProductDmandAprovedListForSuply",
                         new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                           new string[] { "1", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate }, "dataset");


            for (int i = 0; i < dSNews.Tables[0].Columns.Count + 1; i++)
            {
                if (i > 2)
                {
                    string strcolval = dSNews.Tables[0].Columns[i - 1].ColumnName;

                    if (strcolval == "Total Crate")
                    {
                        TextBox txtboxid = new TextBox();
                        txtboxid.MaxLength = 5;
                        txtboxid.Width = 80;
                        txtboxid.ID = "txtboxid" + i;
                        txtboxid.Text = (e.Row.DataItem as DataRowView).Row[strcolval].ToString();
                        e.Row.Cells[i + 1].Controls.Add(txtboxid);

                    }
                    else
                    {
                        Label txtboxid = new Label();
                        txtboxid.ID = "txtboxid" + i;
                        txtboxid.Text = (e.Row.DataItem as DataRowView).Row[strcolval].ToString();
                        e.Row.Cells[i + 1].Controls.Add(txtboxid);

                        if (txtboxid.Text == "")
                        {
                            txtboxid.Text = "0";
                        }

                    }

                }
            }

        }

        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;

    }
    private void GetApprovedDemandRouteWise_New()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DataSet ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDmandAprovedListForSuply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                       new string[] { "1", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate }, "dataset");


            if (ds1.Tables[0].Rows.Count != 0)
            {
                DataTable dt = new DataTable();
                dt = ds1.Tables[0];
                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Demand" && column.ToString() != "Total Crate")
                        {
                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }

                GVCMNEW.FooterRow.Cells[1].Text = "Total Demand";
                GVCMNEW.FooterRow.Cells[1].Font.Bold = true;

                int A = 4;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "RouteId" && column.ToString() != "Route")
                    {
                        if (column.ToString() == "Total Crate")
                        {
                            sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));
                            GVCMNEW.FooterRow.Cells[A].Text = "0";
                            GVCMNEW.FooterRow.Cells[A].Font.Bold = true;
                            //GVCMNEW.FooterRow.Cells[A].Visible = false;
                            A = A + 1;
                        }
                        else
                        {
                            sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));
                            GVCMNEW.FooterRow.Cells[A].Text = sum11.ToString();
                            GVCMNEW.FooterRow.Cells[A].Font.Bold = true;
                            A = A + 1;
                            lblTotalDemandValue.Text = sum11.ToString();
                        }

                    }
                }


            }
            else
            {
                lblTotalDemandValue.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Routewise Approved demand ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                string myStringfromdat = txtOrderDate.Text; // for delivary datewise
                DateTime demanddate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
               

                if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text=="Milk")
                {
                    ViewState["DelivaryDate"] = deliverydat;
                    ViewState["DelivaryShift"] = "2";
                }
                else if(ddlShift.SelectedItem.Text == "Evening" && ddlItemCategory.SelectedItem.Text=="Milk")
                {
                    demanddate = demanddate.AddDays(1);
                    deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    ViewState["DelivaryDate"] = deliverydat;
                    ViewState["DelivaryShift"] = "1";
                }
                else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Product")
                {
                    demanddate = demanddate.AddDays(1);
                    deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    ViewState["DelivaryDate"] = deliverydat;
                    ViewState["DelivaryShift"] = "1";
                }
                else
                {

                }

                DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
                string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DataSet dSNews = objdb.ByProcedure("USP_Trn_MilkOrProductDmandAprovedListForSuply",
                             new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                               new string[] { "1", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate }, "dataset");

                for (int i = 0; i <= GVCMNEW.Rows.Count - 1; i++)
                {
                    GridViewRow row1 = GVCMNEW.Rows[i];

                    LinkButton lblRouteId = (LinkButton)row1.FindControl("lnkbtnRoute");

                    int j = 0;
                    int k = 2;

                    foreach (DataColumn column in dSNews.Tables[0].Columns)
                    {
                        if (j > 1)
                        {
                            string columnname = column.ColumnName;
                            k = k + 1;

                            if (columnname == "Total Crate")
                            {
                                string txtboxval = (row1.FindControl("txtboxid" + k) as TextBox).Text;

                                if (txtboxval != "" && txtboxval != "0")
                                {

                                    objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                                           new string[] { "flag","TotalQty","Demand_Date","Shift_id" ,"RouteId", "ItemCat_id", "ItemType_id", "Item_id",
                                   "CreatedBy", "Office_ID", "CreatedByIP","MPItemCat_id","Delivary_Date","DelivaryShift_id" },
                                           new string[] { "21" ,txtboxval,orderedate,ddlShift.SelectedValue, lblRouteId.CommandArgument,objdb.GetCrateItemCat_Id(), objdb.GetCrateType_Id(), objdb.GetCrateItem_Id(),
                                      objdb.createdBy(),objdb.Office_ID(), objdb.GetLocalIPAddress(),ddlItemCategory.SelectedValue,ViewState["DelivaryDate"].ToString(),ViewState["DelivaryShift"].ToString() }, "dataset");

                                    recordyn = recordyn + 1;
                                }
                            }

                        }

                        j = j + 1;

                    }

                }
                if (recordyn!=0)
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Crate Successfully Updated");
                }

               

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
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
    //private void DisplayCrateLabel()
    //{
    //    if (ddlItemCategory.SelectedValue == "2")
    //    {
    //        pnltotalcrate.Visible = false;
    //    }
    //    else
    //    {
    //        pnltotalcrate.Visible = true;
    //    }
    //}


    private void GetApprovedDemandDistributorWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDmandAprovedListForSuply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                       new string[] { "2", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {
                DataTable dt = new DataTable();
                dt = ds2.Tables[0];
                //dt2 = ds2.Tables[1];
                //ViewState["RoutOrDisOrInstWiseTable"] = dt2;
                //BindStringbuilderDistWise();
                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //if (column.ToString() != "S.No." && column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist Name" && column.ToString() != "Total Demand" && column.ToString() != "Total Crate" && column.ToString() != "Total Demand in Litre")
                        if (column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist Name" && column.ToString() != "Total Demand" && column.ToString() != "Total Crate")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                GridView2.DataSource = dt;
                GridView2.DataBind();

                GridView2.FooterRow.Cells[1].Text = "Total Demand";
                GridView2.FooterRow.Cells[1].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {
                    //if (column.ToString() != "S.No." && column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist Name" && column.ToString() != "Total Demand" && column.ToString() != "Total Crate" && column.ToString() != "Total Demand in Litre")
                    if (column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist Name" && column.ToString() != "Total Demand" && column.ToString() != "Total Crate")
                    {

                        sum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView2.FooterRow.Cells[cellIndex].Text = sum22.ToString();
                        GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Total Demand")
                    {

                        dsum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView2.FooterRow.Cells[cellIndex].Text = dsum22.ToString();
                        GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                        lblTotalDemandValue.Text = dsum22.ToString();
                    }
                }
                if (ddlItemCategory.SelectedValue != "2") // for milk category
                {

                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() == "Total Crate")
                        {

                            csum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                            GridView2.FooterRow.Cells[cellIndex].Text = csum22.ToString();
                            GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                            cellIndex = cellIndex + 1;
                            // lblTotalCrateValue.Text = csum22.ToString();
                        }
                    }
                    //foreach (DataColumn column in dt.Columns)
                    //{
                    //    if (column.ToString() == "Total Demand in Litre")
                    //    {

                    //        sum2 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

                    //        GridView2.FooterRow.Cells[cellIndex].Text = sum2.ToString("N2");
                    //        GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                    //        cellIndex = cellIndex + 1;
                    //    }
                    //}
                }
                // DisplayCrateLabel();
                if (ddlItemCategory.SelectedValue != "2")
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
                    //  GridView2.FooterRow.Cells[rowcount + 3].Visible = false;
                }

                //ViewState["RoutOrDisOrInstWiseTable"] = "";
                //if (dt2 != null) { dt2.Dispose(); }

            }
            else
            {
                lblTotalDemandValue.Text = "";
                // lblTotalCrateValue.Text = "";
                // DisplayCrateLabel();
                // divStringBuilder.InnerHtml = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error DistributorWise Approved demand ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    private void GetApprovedDemandOrganizationWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDmandAprovedListForSuply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                       new string[] { "3", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate }, "dataset");

            if (ds3.Tables[0].Rows.Count != 0)
            {
                DataTable dt = new DataTable();
                dt = ds3.Tables[0];
                //dt3 = ds3.Tables[1];
                //ViewState["RoutOrDisOrInstWiseTable"] = dt3;
                //BindStringbuilderInstWise();
                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //if (column.ToString() != "S.No." && column.ToString() != "OrganizationId" && column.ToString() != "Organization Name" && column.ToString() != "Total Demand" && column.ToString() != "Total Crate" && column.ToString() != "Total Demand in Litre")
                        if (column.ToString() != "OrganizationId" && column.ToString() != "Organization Name" && column.ToString() != "Total Demand" && column.ToString() != "Total Crate")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                GridView3.DataSource = dt;
                GridView3.DataBind();

                GridView3.FooterRow.Cells[1].Text = "Total Demand";
                GridView3.FooterRow.Cells[1].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {
                    //if (column.ToString() != "S.No." && column.ToString() != "OrganizationId" && column.ToString() != "Organization Name" && column.ToString() != "Total Demand" && column.ToString() != "Total Crate" && column.ToString() != "Total Demand in Litre")
                    if (column.ToString() != "S.No." && column.ToString() != "OrganizationId" && column.ToString() != "Organization Name" && column.ToString() != "Total Demand" && column.ToString() != "Total Crate")
                    {

                        sum33 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView3.FooterRow.Cells[cellIndex].Text = sum33.ToString();
                        GridView3.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Total Demand")
                    {

                        dsum33 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView3.FooterRow.Cells[cellIndex].Text = dsum33.ToString();
                        GridView3.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                        lblTotalDemandValue.Text = dsum33.ToString();
                    }
                }
                if (ddlItemCategory.SelectedValue != "2") // for milk category
                {

                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() == "Total Crate")
                        {

                            csum33 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                            GridView3.FooterRow.Cells[cellIndex].Text = csum33.ToString();
                            GridView3.FooterRow.Cells[cellIndex].Font.Bold = true;
                            cellIndex = cellIndex + 1;
                            //  lblTotalCrateValue.Text = csum33.ToString();
                        }

                    }
                    //foreach (DataColumn column in dt.Columns)
                    //{
                    //    if (column.ToString() == "Total Demand in Litre")
                    //    {

                    //        sum3 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

                    //        GridView3.FooterRow.Cells[cellIndex].Text = sum3.ToString("N2");
                    //        GridView3.FooterRow.Cells[cellIndex].Font.Bold = true;
                    //        cellIndex = cellIndex + 1;
                    //    }
                    //}
                }
                // DisplayCrateLabel();
                if (ddlItemCategory.SelectedValue != "2")
                {
                    int rowcount = GridView3.FooterRow.Cells.Count - 2;
                    GridView3.FooterRow.Cells[rowcount].Visible = false;
                    GridView3.FooterRow.Cells[rowcount + 1].Visible = false;
                }
                else
                {
                    int rowcount = GridView3.FooterRow.Cells.Count - 3; // previous default is : 4
                    GridView3.FooterRow.Cells[rowcount].Visible = false;
                    GridView3.FooterRow.Cells[rowcount + 1].Visible = false;
                    GridView3.FooterRow.Cells[rowcount + 2].Visible = false;
                    // GridView3.FooterRow.Cells[rowcount + 3].Visible = false;
                }
                //ViewState["RoutOrDisOrInstWiseTable"] = "";
                //if (dt3 != null) { dt3.Dispose(); }
            }
            else
            {
                lblTotalDemandValue.Text = "";
                // lblTotalCrateValue.Text = "";
                //  DisplayCrateLabel();
                // divStringBuilder.InnerHtml = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Institution wise Approved demand ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }

    private void GetDemandStatusByRoute()
    {
        try
        {
            GVCMNEW.Visible = true;
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();

            GridView2.Visible = false;
            GridView3.Visible = false;
            pnlData.Visible = true;
            pnlrouteOrDistOrInstwisedata.Visible = true;
            this.BindGrid();

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

    private void GetDemandStatusByDistributor()
    {
        try
        {
            GVCMNEW.Visible = false;
            btnsave.Visible = false;

            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView2.Visible = true;

            GridView3.Visible = false;
            pnlData.Visible = true;
            pnlrouteOrDistOrInstwisedata.Visible = true;
            GetApprovedDemandDistributorWise();
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
    private void GetDemandStatusByInstitute()
    {
        try
        {
            GVCMNEW.Visible = false;
            btnsave.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();

            GridView2.Visible = false;
            GridView3.Visible = true;
            pnlData.Visible = true;
            pnlrouteOrDistOrInstwisedata.Visible = true;
            GetApprovedDemandOrganizationWise();
            

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
    private void callrblReportType()
    {
        if (txtOrderDate.Text != "" && ddlShift.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0")
        {
            if (rblReportType.SelectedValue == "1")
            {
                pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details";
                GetDemandStatusByRoute();
            }
            else if (rblReportType.SelectedValue == "2")
            {
                pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details";
                GetDemandStatusByDistributor();
                btnsave.Visible = false;
            }
            else if (rblReportType.SelectedValue == "3")
            {
                pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details";
                GetDemandStatusByInstitute();
                btnsave.Visible = false;

            }
            else
            {
                pnlData.Visible = false;
                pnlrouteOrDistOrInstwisedata.Visible = false;
                btnsave.Visible = false;

            }
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
    //    pnlrouteOrDistOrInstwisedata.Visible = false;
    //}
    //protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (txtOrderDate.Text != "" || ddlShift.SelectedValue != "0")
    //    {
    //        ddlItemCategory.SelectedIndex = 0;
    //        pnlSearchBy.Visible = false;
    //        rblReportType.ClearSelection();
    //        pnlData.Visible = false;
    //        pnlrouteOrDistOrInstwisedata.Visible = false;
    //    }

    //}
    //protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (txtOrderDate.Text != "" && ddlShift.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0")
    //    {
    //        if(ddlShift.SelectedItem.Text=="Evening" && ddlItemCategory.SelectedItem.Text=="Product")
    //        {
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " list can show only in Morning shift.");
    //            ddlItemCategory.SelectedIndex = 0;
    //            rblReportType.ClearSelection();
    //            pnlSearchBy.Visible = false;
    //            pnlData.Visible = false;
    //            pnlrouteOrDistOrInstwisedata.Visible = false;
    //        }
    //        else
    //        {
    //            lblMsg.Text = string.Empty;
    //            pnlSearchBy.Visible = true;
    //            rblReportType.ClearSelection();
    //            pnlData.Visible = false;
    //            pnlrouteOrDistOrInstwisedata.Visible = false;
    //        }

    //    }
    //    else
    //    {
    //        rblReportType.ClearSelection();
    //        pnlSearchBy.Visible = false;
    //        pnlData.Visible = false;
    //        pnlrouteOrDistOrInstwisedata.Visible = false;
    //    }
    //}
    //protected void rblReportType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    callrblReportType();
    //}
    #endregion=====================================================


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int maxrowcell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

            if (ddlItemCategory.SelectedValue != "3")
            {
                e.Row.Cells[maxrowcell1].Visible = false;
                //  e.Row.Cells[maxrowcell1 + 1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            int maxheadercell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            if (ddlItemCategory.SelectedValue != "3")
            {
                e.Row.Cells[maxheadercell1].Visible = false;
                //  e.Row.Cells[maxheadercell1 + 1].Visible = false;
            }
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int maxrowcell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

            if (ddlItemCategory.SelectedValue != "3")
            {
                e.Row.Cells[maxrowcell1].Visible = false;
                // e.Row.Cells[maxrowcell1 + 1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            int maxheadercell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            if (ddlItemCategory.SelectedValue != "3")
            {
                e.Row.Cells[maxheadercell1].Visible = false;
                //   e.Row.Cells[maxheadercell1 + 1].Visible = false;
            }
        }

        //===========================new code


    }
    protected void GVCMNEW_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    Response.Redirect("ApprovalofSupplyatDS.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
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
                    Session["D_DistributorId"] = e.CommandArgument.ToString(); ;
                    Session["D_OrganizationId"] = "0";
                    Response.Redirect("ApprovalofSupplyatDS.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
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
                    Session["D_OrganizationId"] = e.CommandArgument.ToString(); ;
                    Response.Redirect("ApprovalofSupplyatDS.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
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
            txtOrderDate.Text = string.Empty;
            ddlShift.SelectedIndex = 0;
            ddlItemCategory.SelectedIndex = 0;
            rblReportType.ClearSelection();
            pnlData.Visible = false;
            pnlrouteOrDistOrInstwisedata.Visible = false;
        }
    }
    //protected void BindStringbuilderRouteWise()
    //{
    //    try
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        if (ViewState["RoutOrDisOrInstWiseTable"] != null)
    //        {
    //            DataTable dt = (DataTable)ViewState["RoutOrDisOrInstWiseTable"];
    //            int totalrow = dt.Rows.Count;
    //            int totalcolumn = dt.Columns.Count;

    //            sb.Append("<table class='table table-striped table-bordered'><tr>");
    //            sb.Append("<th style='display:none;'>S.No.</th>");
    //            for (int j = 0; j < totalcolumn; j++)
    //            {
    //                if (j == 0)
    //                {
    //                    sb.Append("<th style='display:none;'>" + dt.Columns[j].ColumnName.ToString() + "</th>");
    //                }
    //                else
    //                {
    //                    if (dt.Columns[j].ColumnName.ToString() == "Route")
    //                    {
    //                        sb.Append("<th>Total Crate</th>");
    //                    }
    //                    else
    //                    {
    //                        sb.Append("<th>" + dt.Columns[j].ColumnName.ToString() + "</th>");
    //                    }
    //                }
    //            }
    //            sb.Append("</tr>");
    //            for (int i = 0; i < totalrow; i++)
    //            {
    //                sb.Append("<tr style='display:none;'><td>" + (i + 1) + "</td>");
    //                for (int j = 0; j < totalcolumn; j++)
    //                {
    //                    if (j == 0)
    //                    {
    //                        sb.Append("<td style='display:none;'>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
    //                    }
    //                    else
    //                    {
    //                        sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
    //                    }
    //                }
    //                sb.Append("</tr>");

    //            }
    //            string bindtr = "";
    //            sb.Append("<tr style='display:none;'><td></td><td></td>");
    //            int Showtotal = 0;
    //            for (int j = 0; j < totalcolumn; j++)
    //            {

    //                int totalmorpbracket = 0;


    //                if (dt.Columns[j].ColumnName.ToString() != "RouteId" && dt.Columns[j].ColumnName.ToString() != "Route")
    //                {
    //                    for (int i = 0; i < totalrow; i++)
    //                    {
    //                        string colm = "";

    //                        colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString();
    //                        if (colm=="")
    //                        {
    //                            colm="0";
    //                        }

    //                        totalmorpbracket += Convert.ToInt32(colm);

    //                        Showtotal += Convert.ToInt32(colm);


    //                    }
    //                    bindtr += "<td>" + totalmorpbracket.ToString() + "</td>";
    //                }
    //            }
    //            sb.Append("</tr>");
    //            sb.Append("<tr><td><b>"+ Showtotal +"</b></td>" + bindtr + "</tr>");
    //            sb.Append("</table> ");
    //           // divStringBuilder.InnerHtml = sb.ToString();
    //            dt.Dispose();
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 9 ", ex.Message.ToString());
    //    }
    //}
    //protected void BindStringbuilderDistWise()
    //{
    //    try
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        if (ViewState["RoutOrDisOrInstWiseTable"] != null)
    //        {
    //            DataTable dt = (DataTable)ViewState["RoutOrDisOrInstWiseTable"];
    //            int totalrow = dt.Rows.Count;
    //            int totalcolumn = dt.Columns.Count;

    //            sb.Append("<table class='table table-striped table-bordered'><tr>");
    //            sb.Append("<th style='display:none;'>S.No.</th>");
    //            for (int j = 0; j < totalcolumn; j++)
    //            {
    //                if (j == 0)
    //                {
    //                    sb.Append("<th style='display:none;'>" + dt.Columns[j].ColumnName.ToString() + "</th>");
    //                }
    //                else
    //                {
    //                    if (dt.Columns[j].ColumnName.ToString() == "Distributor/Superstockist Name")
    //                    {
    //                        sb.Append("<th>Total Crate</th>");
    //                    }
    //                    else
    //                    {
    //                        sb.Append("<th>" + dt.Columns[j].ColumnName.ToString() + "</th>");
    //                    }
    //                }
    //            }
    //            sb.Append("</tr>");
    //            for (int i = 0; i < totalrow; i++)
    //            {
    //                sb.Append("<tr style='display:none;'><td>" + (i + 1) + "</td>");
    //                for (int j = 0; j < totalcolumn; j++)
    //                {
    //                    if (j == 0)
    //                    {
    //                        sb.Append("<td style='display:none;'>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
    //                    }
    //                    else
    //                    {
    //                        sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
    //                    }
    //                }
    //                sb.Append("</tr>");

    //            }
    //            string bindtr = "";
    //            sb.Append("<tr style='display:none;'><td></td><td></td>");
    //            int Showtotal = 0;
    //            for (int j = 0; j < totalcolumn; j++)
    //            {
    //                int totalmorpbracket = 0;
    //                if (dt.Columns[j].ColumnName.ToString() != "DistributorId" && dt.Columns[j].ColumnName.ToString() != "Distributor/Superstockist Name")
    //                {
    //                    for (int i = 0; i < totalrow; i++)
    //                    {
    //                        string colm = "";

    //                        colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString();
    //                        if (colm == "")
    //                        {
    //                            colm = "0";
    //                        }

    //                        totalmorpbracket += Convert.ToInt32(colm);

    //                        Showtotal += Convert.ToInt32(colm);


    //                    }
    //                    bindtr += "<td>" + totalmorpbracket.ToString() + "</td>";
    //                }
    //            }
    //            sb.Append("</tr>");
    //            sb.Append("<tr><td><b>" + Showtotal + "</b></td>" + bindtr + "</tr>");
    //            sb.Append("</table><br/><br/><br/> ");
    //            divStringBuilder.InnerHtml = sb.ToString();
    //            dt.Dispose();
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
    //    }
    //}

    //protected void BindStringbuilderInstWise()
    //{
    //    try
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        if (ViewState["RoutOrDisOrInstWiseTable"] != null)
    //        {
    //            DataTable dt = (DataTable)ViewState["RoutOrDisOrInstWiseTable"];
    //            int totalrow = dt.Rows.Count;
    //            int totalcolumn = dt.Columns.Count;

    //            sb.Append("<table class='table table-striped table-bordered'><tr>");
    //            sb.Append("<th style='display:none;'>S.No.</th>");
    //            for (int j = 0; j < totalcolumn; j++)
    //            {
    //                if (j == 0)
    //                {
    //                    sb.Append("<th style='display:none;'>" + dt.Columns[j].ColumnName.ToString() + "</th>");
    //                }
    //                else
    //                {
    //                    if (dt.Columns[j].ColumnName.ToString() == "Organization Name")
    //                    {
    //                        sb.Append("<th>Total Crate</th>");
    //                    }
    //                    else
    //                    {
    //                        sb.Append("<th>" + dt.Columns[j].ColumnName.ToString() + "</th>");
    //                    }
    //                }
    //            }
    //            sb.Append("</tr>");
    //            for (int i = 0; i < totalrow; i++)
    //            {
    //                sb.Append("<tr style='display:none;'><td>" + (i + 1) + "</td>");
    //                for (int j = 0; j < totalcolumn; j++)
    //                {
    //                    if (j == 0)
    //                    {
    //                        sb.Append("<td style='display:none;'>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
    //                    }
    //                    else
    //                    {
    //                        sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
    //                    }
    //                }
    //                sb.Append("</tr>");

    //            }
    //            string bindtr = "";
    //            sb.Append("<tr style='display:none;'><td></td><td></td>");
    //            int Showtotal = 0;
    //            for (int j = 0; j < totalcolumn; j++)
    //            {
    //                int totalmorpbracket = 0;
    //                if (dt.Columns[j].ColumnName.ToString() != "OrganizationId" && dt.Columns[j].ColumnName.ToString() != "Organization Name")
    //                {
    //                    for (int i = 0; i < totalrow; i++)
    //                    {
    //                        string colm = "";

    //                        colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString();
    //                        if (colm == "")
    //                        {
    //                            colm = "0";
    //                        }

    //                        totalmorpbracket += Convert.ToInt32(colm);

    //                        Showtotal += Convert.ToInt32(colm);


    //                    }
    //                    bindtr += "<td>" + totalmorpbracket.ToString() + "</td>";
    //                }
    //            }
    //            sb.Append("</tr>");
    //            sb.Append("<tr><td><b>" + Showtotal + "</b></td>" + bindtr + "</tr>");
    //            sb.Append("</table><br/><br/><br/> ");
    //            divStringBuilder.InnerHtml = sb.ToString();
    //            dt.Dispose();
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 11 ", ex.Message.ToString());
    //    }
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            callrblReportType();

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtOrderDate.Text = string.Empty;
        ddlShift.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
        rblReportType.ClearSelection();
        pnlData.Visible = false;
        pnlrouteOrDistOrInstwisedata.Visible = false;
    }

}