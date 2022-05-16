using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformationSystem_MPR_Of_TS_Allocated_expenses : System.Web.UI.Page
{

    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {

                if (!IsPostBack)
                {
                    ViewState["detailtanker"] = "";
                    ViewState["detailtruck"] = "";
                    ViewState["detailjeepcar"] = "";
                    getyear();
                  //  getdata();
                    // AddNewRow();
                    //BindGridviewtanker();
                    //BindGridviewtruck();
                    //BindGridviewjeepcar();
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    public void getyear()
    {
        try
        {

            DataSet dsyear = new DataSet();
            dsyear = objdb.ByProcedure("Get_year_and_month"
                   , new string[] { "flag" }
                   , new string[] { "0" }, "dataset");
            if (dsyear != null)
            {
                if (dsyear.Tables.Count > 0)
                {
                    if (dsyear.Tables[0].Rows.Count > 0)
                    {
                        ddlyear.Items.Clear();
                        ddlyear.DataSource = dsyear.Tables[0];
                        ddlyear.DataTextField = "Year_sel";
                        ddlyear.DataValueField = "Year_sel";
                        ddlyear.DataBind();
                        //CODE CHANGES STARTED BY AJAY ON 13-JUN-2019
                        //ddlDOA.Items.Insert(0, "--Select DOA--");
                        //  ddlyear.Items.Insert(0, DateTime.Now.Year.ToString());
                        ddlyear.Enabled = true;

                        ddlyear2.Items.Clear();
                        ddlyear2.DataSource = dsyear.Tables[0];
                        ddlyear2.DataTextField = "Year_sel";
                        ddlyear2.DataValueField = "Year_sel";
                        ddlyear2.DataBind();
                        //CODE CHANGES STARTED BY AJAY ON 13-JUN-2019
                        //ddlDOA.Items.Insert(0, "--Select DOA--");
                        //  ddlyear.Items.Insert(0, DateTime.Now.Year.ToString());
                        ddlyear2.Enabled = true;
                    }

                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                        lblMsg.Focus();
                        //panelasset.Visible = false;

                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void getdata()
    {
        try
        {

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
               new string[] { "flag", "Office_ID", "CreatedBy" },
                 new string[] { "2", objdb.Office_ID(), Session["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvAEdetail.DataSource = ds;
                    gvAEdetail.DataBind();
                    gvAEdetail.Visible = true;
                }
                else
                {
                    gvAEdetail.DataSource = null;
                    gvAEdetail.DataBind();
                }
            }
            else
            {
                gvAEdetail.DataSource = null;
                gvAEdetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void DDlMonth2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (DDlMonth2.SelectedIndex > 0)
            {
                getdata_byfilter();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Select Month for search");
                lblMsg.Focus();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DDlMonth2_SelectedIndexChanged(sender, e);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void getdata_byfilter()
    {
        try
        {

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year" },
                 new string[] { "4", objdb.Office_ID(), Session["Emp_ID"].ToString(),DDlMonth2.SelectedValue
                           , ddlyear2.SelectedItem.Text }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvAEdetail.DataSource = ds;
                    gvAEdetail.DataBind();
                    gvAEdetail.Visible = true;
                    gvAEdetail.Rows[gvAEdetail.Rows.Count - 1].Focus();
                }
                else
                {
                    gvAEdetail.DataSource = null;
                    gvAEdetail.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                    lblMsg.Focus();
                }
            }
            else
            {
                gvAEdetail.DataSource = null;
                gvAEdetail.DataBind();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                lblMsg.Focus();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlVehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["detailtanker"] = "";
            ViewState["detailtruck"] = "";
            ViewState["detailjeepcar"] = "";
            if (ddlVehicleType.SelectedIndex > 0 && ddlVehicleType.SelectedItem.Text == "Tanker")
            {
                divtanker.Visible = true;
                divtruck.Visible = false;
                divjeepcar.Visible = false;
                divbutton.Visible = true;
                BindGridviewtanker();
            }
            else if (ddlVehicleType.SelectedIndex > 0 && ddlVehicleType.SelectedItem.Text == "Truck")
            {
                divtanker.Visible = false;
                divtruck.Visible = true;
                divjeepcar.Visible = false;
                divbutton.Visible = true;
                BindGridviewtruck();

            }
            else if (ddlVehicleType.SelectedIndex > 0 && ddlVehicleType.SelectedItem.Text == "Jeeps & Cars")
            {
                divtanker.Visible = false;
                divtruck.Visible = false;
                divjeepcar.Visible = true;
                divbutton.Visible = true;
                BindGridviewjeepcar();

            }
            else
            {
                divtanker.Visible = false;
                divtruck.Visible = false;
                divjeepcar.Visible = false;
                divbutton.Visible = false;

            }

        }
        catch
        {

        }
    }
    protected void btnAddtanker_Click(object sender, EventArgs e)
    {
        try
        {
            AddNewRowtanker();
            //gvtanker.Focus();
            gvtanker.Rows[gvtanker.Rows.Count - 1].Focus();
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    private void AddNewRowtanker()
    {
        try
        {



            int rowIndex = 0;
            DataTable dt = (DataTable)ViewState["detailtanker"];

            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox txtVehicleNo = (TextBox)gvtanker.Rows[rowIndex].Cells[1].FindControl("txtVehicleNo");
                    TextBox txtRoad = (TextBox)gvtanker.Rows[rowIndex].Cells[2].FindControl("txtRoad");
                    TextBox txtPermit = (TextBox)gvtanker.Rows[rowIndex].Cells[3].FindControl("txtPermit");
                    TextBox txtInsurance = (TextBox)gvtanker.Rows[rowIndex].Cells[4].FindControl("txtInsurance");
                    TextBox txtTyre = (TextBox)gvtanker.Rows[rowIndex].Cells[5].FindControl("txtTyre");
                    TextBox txtBattery = (TextBox)gvtanker.Rows[rowIndex].Cells[6].FindControl("txtBattery");
                    TextBox txtMajorRepairs = (TextBox)gvtanker.Rows[rowIndex].Cells[7].FindControl("txtMajorRepairs");
                    //  TextBox txtDateOfReturn = (TextBox)gvtanker.Rows[rowIndex].Cells[8].FindControl("txtDateOfReturn");

                    dr = dt.NewRow();

                    dt.Rows[i - 1]["Sno"] = i + 1;
                    dt.Rows[i - 1]["VehicleNo"] = txtVehicleNo.Text;
                    dt.Rows[i - 1]["Road"] = txtRoad.Text;
                    dt.Rows[i - 1]["Permit"] = txtPermit.Text;
                    dt.Rows[i - 1]["Insurance"] = txtInsurance.Text;
                    dt.Rows[i - 1]["Tyre"] = txtTyre.Text;
                    dt.Rows[i - 1]["Battery"] = txtBattery.Text;
                    dt.Rows[i - 1]["MajorRepairs"] = txtMajorRepairs.Text;
                    rowIndex++;
                    //dt.Rows.Add(dr);

                    //dr["Sno"] = dt.Rows.Count + 1;
                    //dr["VehicleNo"] = string.Empty;
                    //dr["Road"] = string.Empty;
                    //dr["Permit"] = string.Empty;
                    //dr["Insurance"] = string.Empty;
                    //dr["Tyre"] = string.Empty;
                    //dr["Battery"] = string.Empty;
                    //dr["MajorRepairs"] = string.Empty;
                    //dr["MarketPurchaseRs"] = string.Empty;
                    //dr["closingbal"] = string.Empty;
                }
            }

            dt.Rows.Add(dr);
            ViewState["detailtanker"] = dt;
            gvtanker.DataSource = dt;
            gvtanker.DataBind();

        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void AddNewRowtruck()
    {
        try
        {


            int rowIndex = 0;

            DataTable dt = (DataTable)ViewState["detailtruck"];

            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox txtVehicleNo = (TextBox)gvtruck.Rows[rowIndex].Cells[1].FindControl("txtVehicleNo");
                    TextBox txtRoad = (TextBox)gvtruck.Rows[rowIndex].Cells[2].FindControl("txtRoad");
                    TextBox txtPermit = (TextBox)gvtruck.Rows[rowIndex].Cells[3].FindControl("txtPermit");
                    TextBox txtInsurance = (TextBox)gvtruck.Rows[rowIndex].Cells[4].FindControl("txtInsurance");
                    TextBox txtTyre = (TextBox)gvtruck.Rows[rowIndex].Cells[5].FindControl("txtTyre");
                    TextBox txtBattery = (TextBox)gvtruck.Rows[rowIndex].Cells[6].FindControl("txtBattery");
                    TextBox txtMajorRepairs = (TextBox)gvtruck.Rows[rowIndex].Cells[7].FindControl("txtMajorRepairs");
                    // TextBox txtDateOfReturn = (TextBox)gvtruck.Rows[rowIndex].Cells[8].FindControl("txtDateOfReturn");

                    dr = dt.NewRow();

                    dt.Rows[i - 1]["Sno"] = i + 1;
                    dt.Rows[i - 1]["VehicleNo"] = txtVehicleNo.Text;
                    dt.Rows[i - 1]["Road"] = txtRoad.Text;
                    dt.Rows[i - 1]["Permit"] = txtPermit.Text;
                    dt.Rows[i - 1]["Insurance"] = txtInsurance.Text;
                    dt.Rows[i - 1]["Tyre"] = txtTyre.Text;
                    dt.Rows[i - 1]["Battery"] = txtBattery.Text;
                    dt.Rows[i - 1]["MajorRepairs"] = txtMajorRepairs.Text;
                    rowIndex++;
                    //dt.Rows.Add(dr);

                    //dr["Sno"] = dt.Rows.Count + 1;
                    //dr["VehicleNo"] = string.Empty;
                    //dr["Road"] = string.Empty;
                    //dr["Permit"] = string.Empty;
                    //dr["Insurance"] = string.Empty;
                    //dr["Tyre"] = string.Empty;
                    //dr["Battery"] = string.Empty;
                    //dr["MajorRepairs"] = string.Empty;
                    //dr["MarketPurchaseRs"] = string.Empty;
                    //dr["closingbal"] = string.Empty;
                }
            }


            dt.Rows.Add(dr);
            ViewState["detailtruck"] = dt;
            gvtruck.DataSource = dt;
            gvtruck.DataBind();

        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void AddNewRowjeepcar()
    {
        try
        {


            int rowIndex = 0;

            DataTable dt = (DataTable)ViewState["detailjeepcar"];

            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox txtVehicleNo = (TextBox)gvjeepcar.Rows[rowIndex].Cells[1].FindControl("txtVehicleNo");
                    TextBox txtRoad = (TextBox)gvjeepcar.Rows[rowIndex].Cells[2].FindControl("txtRoad");
                    TextBox txtPermit = (TextBox)gvjeepcar.Rows[rowIndex].Cells[3].FindControl("txtPermit");
                    TextBox txtInsurance = (TextBox)gvjeepcar.Rows[rowIndex].Cells[4].FindControl("txtInsurance");
                    TextBox txtTyre = (TextBox)gvjeepcar.Rows[rowIndex].Cells[5].FindControl("txtTyre");
                    TextBox txtBattery = (TextBox)gvjeepcar.Rows[rowIndex].Cells[6].FindControl("txtBattery");
                    TextBox txtMajorRepairs = (TextBox)gvjeepcar.Rows[rowIndex].Cells[7].FindControl("txtMajorRepairs");
                    //TextBox txtDateOfReturn = (TextBox)gvjeepcar.Rows[rowIndex].Cells[8].FindControl("txtDateOfReturn");

                    dr = dt.NewRow();

                    dt.Rows[i - 1]["Sno"] = i + 1;
                    dt.Rows[i - 1]["VehicleNo"] = txtVehicleNo.Text;
                    dt.Rows[i - 1]["Road"] = txtRoad.Text;
                    dt.Rows[i - 1]["Permit"] = txtPermit.Text;
                    dt.Rows[i - 1]["Insurance"] = txtInsurance.Text;
                    dt.Rows[i - 1]["Tyre"] = txtTyre.Text;
                    dt.Rows[i - 1]["Battery"] = txtBattery.Text;
                    dt.Rows[i - 1]["MajorRepairs"] = txtMajorRepairs.Text;
                    rowIndex++;
                    //dt.Rows.Add(dr);

                    //dr["Sno"] = dt.Rows.Count + 1;
                    //dr["VehicleNo"] = string.Empty;
                    //dr["Road"] = string.Empty;
                    //dr["Permit"] = string.Empty;
                    //dr["Insurance"] = string.Empty;
                    //dr["Tyre"] = string.Empty;
                    //dr["Battery"] = string.Empty;
                    //dr["MajorRepairs"] = string.Empty;
                    //dr["MarketPurchaseRs"] = string.Empty;
                    //dr["closingbal"] = string.Empty;
                }
            }

            dt.Rows.Add(dr);
            ViewState["detailjeepcar"] = dt;
            gvjeepcar.DataSource = dt;
            gvjeepcar.DataBind();

        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BindGridviewtanker()
    {
        try
        {
            if (ViewState["detailtanker"].ToString() == "")
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Sno", typeof(int));
                dt.Columns.Add("VehicleNo", typeof(string));
                dt.Columns.Add("Road", typeof(string));
                dt.Columns.Add("Permit", typeof(string));
                dt.Columns.Add("Insurance", typeof(string));
                dt.Columns.Add("Tyre", typeof(string));
                dt.Columns.Add("Battery", typeof(string));
                dt.Columns.Add("MajorRepairs", typeof(string));
                //dt.Columns.Add("MarketPurchaseRs", typeof(string));
                //dt.Columns.Add("closingbal", typeof(string));

                DataRow dr = dt.NewRow();
                dr["Sno"] = 1;
                dr["VehicleNo"] = string.Empty;
                dr["Road"] = string.Empty;
                dr["Permit"] = string.Empty;
                dr["Insurance"] = string.Empty;
                dr["Tyre"] = string.Empty;
                dr["Battery"] = string.Empty;
                dr["MajorRepairs"] = string.Empty;

                dt.Rows.Add(dr);
                ViewState["detailtanker"] = dt;
                gvtanker.DataSource = dt;
                gvtanker.DataBind();
            }
            else
            {
                DataTable dt = (DataTable)ViewState["detailtanker"];
                // dt.Columns.Add("rowid", typeof(int));
                //dt.Columns.Add("AssetNo", typeof(string));
                //dt.Columns.Add("AssetDescription", typeof(string));
                //dt.Columns.Add("Goods_Services", typeof(string));
                //dt.Columns.Add("Quantity", typeof(string));
                //dt.Columns.Add("UOM", typeof(string));
                //dt.Columns.Add("ValuePerUOM", typeof(string));
                //dt.Columns.Add("Reason", typeof(string));
                //dt.Columns.Add("DateOfReturn", typeof(string));
                //dt.Columns.Add("ID", typeof(string));
                //dt.Columns.Add("ReasonForDelay", typeof(string));
                //dt.Columns.Add("NextExpectedDeliveryDate", typeof(string));
                //dt.Columns.Add("fuAssetImage", typeof(string));
                DataRow dr = dt.NewRow();
                //dr["rowid"] = 1;
                //dr["AssetNo"] = string.Empty;
                //dr["AssetDescription"] = string.Empty;
                //dr["Goods_Services"] = string.Empty;
                //dr["Quantity"] = string.Empty;
                //dr["UOM"] = string.Empty;
                //dr["ValuePerUOM"] = string.Empty;
                //dr["Reason"] = string.Empty;
                //dr["DateOfReturn"] = string.Empty;
                //dr["ID"] = string.Empty;
                //dr["ReasonForDelay"] = string.Empty;
                //dr["NextExpectedDeliveryDate"] = string.Empty;
                dt.Rows.Add(dr);
                ViewState["detailtanker"] = dt;
                gvtanker.DataSource = dt;
                gvtanker.DataBind();
            }




        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BindGridviewtruck()
    {
        try
        {
            if (ViewState["detailtruck"].ToString() == "")
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Sno", typeof(int));
                dt.Columns.Add("VehicleNo", typeof(string));
                dt.Columns.Add("Road", typeof(string));
                dt.Columns.Add("Permit", typeof(string));
                dt.Columns.Add("Insurance", typeof(string));
                dt.Columns.Add("Tyre", typeof(string));
                dt.Columns.Add("Battery", typeof(string));
                dt.Columns.Add("MajorRepairs", typeof(string));
                //dt.Columns.Add("MarketPurchaseRs", typeof(string));
                //dt.Columns.Add("closingbal", typeof(string));

                DataRow dr = dt.NewRow();
                dr["Sno"] = 1;
                dr["VehicleNo"] = string.Empty;
                dr["Road"] = string.Empty;
                dr["Permit"] = string.Empty;
                dr["Insurance"] = string.Empty;
                dr["Tyre"] = string.Empty;
                dr["Battery"] = string.Empty;
                dr["MajorRepairs"] = string.Empty;

                dt.Rows.Add(dr);
                ViewState["detailtruck"] = dt;
                gvtruck.DataSource = dt;
                gvtruck.DataBind();
            }
            else
            {
                DataTable dt = (DataTable)ViewState["detailtruck"];
                // dt.Columns.Add("rowid", typeof(int));
                //dt.Columns.Add("AssetNo", typeof(string));
                //dt.Columns.Add("AssetDescription", typeof(string));
                //dt.Columns.Add("Goods_Services", typeof(string));
                //dt.Columns.Add("Quantity", typeof(string));
                //dt.Columns.Add("UOM", typeof(string));
                //dt.Columns.Add("ValuePerUOM", typeof(string));
                //dt.Columns.Add("Reason", typeof(string));
                //dt.Columns.Add("DateOfReturn", typeof(string));
                //dt.Columns.Add("ID", typeof(string));
                //dt.Columns.Add("ReasonForDelay", typeof(string));
                //dt.Columns.Add("NextExpectedDeliveryDate", typeof(string));
                //dt.Columns.Add("fuAssetImage", typeof(string));
                DataRow dr = dt.NewRow();
                //dr["rowid"] = 1;
                //dr["AssetNo"] = string.Empty;
                //dr["AssetDescription"] = string.Empty;
                //dr["Goods_Services"] = string.Empty;
                //dr["Quantity"] = string.Empty;
                //dr["UOM"] = string.Empty;
                //dr["ValuePerUOM"] = string.Empty;
                //dr["Reason"] = string.Empty;
                //dr["DateOfReturn"] = string.Empty;
                //dr["ID"] = string.Empty;
                //dr["ReasonForDelay"] = string.Empty;
                //dr["NextExpectedDeliveryDate"] = string.Empty;
                dt.Rows.Add(dr);
                ViewState["detailtruck"] = dt;
                gvtruck.DataSource = dt;
                gvtruck.DataBind();
            }




        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BindGridviewjeepcar()
    {
        try
        {
            if (ViewState["detailjeepcar"].ToString() == "")
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Sno", typeof(int));
                dt.Columns.Add("VehicleNo", typeof(string));
                dt.Columns.Add("Road", typeof(string));
                dt.Columns.Add("Permit", typeof(string));
                dt.Columns.Add("Insurance", typeof(string));
                dt.Columns.Add("Tyre", typeof(string));
                dt.Columns.Add("Battery", typeof(string));
                dt.Columns.Add("MajorRepairs", typeof(string));

                DataRow dr = dt.NewRow();
                dr["Sno"] = 1;
                dr["VehicleNo"] = string.Empty;
                dr["Road"] = string.Empty;
                dr["Permit"] = string.Empty;
                dr["Insurance"] = string.Empty;
                dr["Tyre"] = string.Empty;
                dr["Battery"] = string.Empty;
                dr["MajorRepairs"] = string.Empty;

                dt.Rows.Add(dr);
                ViewState["detailjeepcar"] = dt;
                gvjeepcar.DataSource = dt;
                gvjeepcar.DataBind();
            }
            else
            {
                DataTable dt = (DataTable)ViewState["detailjeepcar"];
                // dt.Columns.Add("rowid", typeof(int));
                //dt.Columns.Add("AssetNo", typeof(string));
                //dt.Columns.Add("AssetDescription", typeof(string));
                //dt.Columns.Add("Goods_Services", typeof(string));
                //dt.Columns.Add("Quantity", typeof(string));
                //dt.Columns.Add("UOM", typeof(string));
                //dt.Columns.Add("ValuePerUOM", typeof(string));
                //dt.Columns.Add("Reason", typeof(string));
                //dt.Columns.Add("DateOfReturn", typeof(string));
                //dt.Columns.Add("ID", typeof(string));
                //dt.Columns.Add("ReasonForDelay", typeof(string));
                //dt.Columns.Add("NextExpectedDeliveryDate", typeof(string));
                //dt.Columns.Add("fuAssetImage", typeof(string));
                DataRow dr = dt.NewRow();
                //dr["rowid"] = 1;
                //dr["AssetNo"] = string.Empty;
                //dr["AssetDescription"] = string.Empty;
                //dr["Goods_Services"] = string.Empty;
                //dr["Quantity"] = string.Empty;
                //dr["UOM"] = string.Empty;
                //dr["ValuePerUOM"] = string.Empty;
                //dr["Reason"] = string.Empty;
                //dr["DateOfReturn"] = string.Empty;
                //dr["ID"] = string.Empty;
                //dr["ReasonForDelay"] = string.Empty;
                //dr["NextExpectedDeliveryDate"] = string.Empty;
                dt.Rows.Add(dr);
                ViewState["detailjeepcar"] = dt;
                gvjeepcar.DataSource = dt;
                gvjeepcar.DataBind();
            }




        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void gvtanker_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
    //    TableHeaderCell cell = new TableHeaderCell();
    //    cell.Text = "Meter Reading";
    //    cell.ColumnSpan = 2;
    //    row.Controls.Add(cell);

    //    cell = new TableHeaderCell();
    //    cell.ColumnSpan = 2;
    //    cell.Text = "Diesel Information";
    //    row.Controls.Add(cell);

    //   // row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
    //    gvtanker.HeaderRow.Parent.Controls.AddAt(0, row);
    //}
    //protected void gvtanker_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{

    //}
    //protected void gvtanker_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.Header)
    //        {
    //            GridView HeaderGrid = (GridView)sender;
    //            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //            TableCell HeaderCell = new TableCell();
    //            HeaderCell.Text = "Vehicle No.";
    //            HeaderCell.RowSpan = 3;
    //            HeaderGridRow.Cells.Add(HeaderCell);

    //            HeaderCell = new TableCell();
    //            HeaderCell.Text = "Meter Reading";
    //            HeaderCell.ColumnSpan = 2;
    //            HeaderGridRow.Cells.Add(HeaderCell);


    //            HeaderCell = new TableCell();
    //            HeaderCell.Text = "Diesel Information";
    //            HeaderCell.ColumnSpan = 6;
    //            HeaderGridRow.Cells.Add(HeaderCell);

    //            //HeaderCell = new TableCell();
    //            //HeaderCell.Text = "HARDNESS OF WATER (PPM)";
    //            //HeaderCell.ColumnSpan = 4;
    //            //HeaderGridRow.Cells.Add(HeaderCell);

    //            //HeaderCell = new TableCell();
    //            //HeaderCell.Text = "TEMP OF COLD STORAGE ( °C )";
    //            //HeaderCell.ColumnSpan = 3;
    //            //HeaderGridRow.Cells.Add(HeaderCell);

    //            //HeaderCell = new TableCell();
    //            //HeaderCell.Text = "TEMP OF PRODUCT COLD STORAGE ( °C )";
    //            //HeaderCell.ColumnSpan = 4;
    //            //HeaderGridRow.Cells.Add(HeaderCell);

    //            //HeaderCell = new TableCell();
    //            //HeaderCell.Text = "TEMP OF BUFFER DEEP FREEZER ( °C )";
    //            //HeaderCell.ColumnSpan = 3;
    //            //HeaderGridRow.Cells.Add(HeaderCell);

    //            //HeaderCell = new TableCell();
    //            //HeaderCell.Text = "TEMP CHILLED WATER AT ( °C )";
    //            //HeaderCell.ColumnSpan = 5;
    //            //HeaderGridRow.Cells.Add(HeaderCell);

    //            gvtanker.Controls[0].Controls.AddAt(0, HeaderGridRow);

    //        }
    //    }
    //    catch
    //    {

    //    }
    //}
    protected void gvtanker_DataBound(object sender, EventArgs e)
    {
        try
        {

            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();
            //  cell.Text = "S No.";
            //  cell.RowSpan = 2;
            //  cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);


            cell = new TableHeaderCell();
            cell.Text = "Tax";
            cell.ColumnSpan = 2;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            // row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            gvtanker.HeaderRow.Parent.Controls.AddAt(0, row);
        }
        catch
        {

        }
    }

    protected void gvjeepcar_DataBound(object sender, EventArgs e)
    {
        try
        {
            //int strt1=2;
            //int strt2=4;
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();
            //  cell.Text = "S No.";
            //  cell.RowSpan = 2;
            //  cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            // cell.Text = "Vehicle No.";
            // cell.RowSpan = 2;
            // cell.ColumnSpan = 1;
            row.Controls.Add(cell);


            cell = new TableHeaderCell();
            cell.Text = "Tax";
            //cell.Text.HorizontalAlign=center
            cell.ColumnSpan = 2;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            // row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            gvjeepcar.HeaderRow.Parent.Controls.AddAt(0, row);
        }
        catch
        {

        }
    }
    protected void btnaddjeepcar_Click(object sender, EventArgs e)
    {
        try
        {
            AddNewRowjeepcar();
            // gvjeepcar.Focus();
            gvjeepcar.Rows[gvjeepcar.Rows.Count - 1].Focus();
        }
        catch
        {

        }

    }
    protected void btnaddtruck_Click(object sender, EventArgs e)
    {
        try
        {
            AddNewRowtruck();
            //  gvtruck.Focus();
            gvtruck.Rows[gvtruck.Rows.Count - 1].Focus();

        }
        catch
        {

        }

    }
    protected void gvtruck_DataBound(object sender, EventArgs e)
    {
        try
        {
            //int strt1=2;
            //int strt2=4;
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();
            //  cell.Text = "S No.";
            //  cell.RowSpan = 2;
            //  cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            // cell.Text = "Vehicle No.";
            // cell.RowSpan = 2;
            // cell.ColumnSpan = 1;
            row.Controls.Add(cell);


            cell = new TableHeaderCell();
            cell.Text = "Tax";
            cell.ColumnSpan = 2;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            row.Controls.Add(cell);

            // row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            gvtruck.HeaderRow.Parent.Controls.AddAt(0, row);
        }
        catch
        {

        }
    }

    protected void gvjeepcar_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["detailjeepcar"] != null)
            {
                DataTable dt = (DataTable)ViewState["detailjeepcar"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Capital_tbl"] = dt;
                    gvjeepcar.DataSource = dt;
                    gvjeepcar.DataBind();

                    for (int i = 0; i < gvjeepcar.Rows.Count - 1; i++)
                    {
                        gvjeepcar.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }


                }
            }
            // gvjeepcar.Focus();
            gvjeepcar.Rows[gvjeepcar.Rows.Count - 1].Focus();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvtruck_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["detailtruck"] != null)
            {
                DataTable dt = (DataTable)ViewState["detailtruck"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["detailtruck"] = dt;
                    gvtruck.DataSource = dt;
                    gvtruck.DataBind();

                    for (int i = 0; i < gvtruck.Rows.Count - 1; i++)
                    {
                        gvtruck.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }


                }

            }
            //gvtruck.Focus();
            gvtruck.Rows[gvtruck.Rows.Count - 1].Focus();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void gvtanker_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["detailtanker"] != null)
            {
                DataTable dt = (DataTable)ViewState["detailtanker"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["detailtanker"] = dt;
                    gvtanker.DataSource = dt;
                    gvtanker.DataBind();

                    for (int i = 0; i < gvtanker.Rows.Count - 1; i++)
                    {
                        gvtanker.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }


                }
            }
            //gvtanker.Focus();
            gvtanker.Rows[gvtanker.Rows.Count - 1].Focus();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (DDlMonth.SelectedIndex > 0)
            {
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
                    new string[] { "flag", "Office_ID", "Vehicle_Type_ID", "Vehicle_Type_Name", "Month",
                                     "Year", "CreatedBy" },
                      new string[] { "0", objdb.Office_ID(), ddlVehicleType.SelectedValue, ddlVehicleType.SelectedItem.Text, DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    lblAE_id.Text = ds.Tables[0].Rows[0]["AE_Id"].ToString();
                    if (ddlVehicleType.SelectedValue == "1")
                    {



                        foreach (GridViewRow row in gvtanker.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtRoad = (TextBox)row.FindControl("txtRoad");
                            TextBox txtPermit = (TextBox)row.FindControl("txtPermit");
                            TextBox txtInsurance = (TextBox)row.FindControl("txtInsurance");
                            TextBox txtTyre = (TextBox)row.FindControl("txtTyre");
                            TextBox txtBattery = (TextBox)row.FindControl("txtBattery");
                            TextBox txtMajorRepairs = (TextBox)row.FindControl("txtMajorRepairs");
                            //TextBox txtVehicleNo=(TextBox)row.FindControl("txtVehicleNo");


                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
                          new string[] { "flag", "AE_Id", "Vehicle_No", "Road_tax", "Permit_tax",
                                     "Insurance", "Tyre","Battery","Major_repaires" },
                            new string[] { "1", lblAE_id.Text, txtVehicleNo.Text, txtRoad.Text, txtPermit.Text
                           , txtInsurance.Text,txtTyre.Text,txtBattery.Text,txtMajorRepairs.Text }, "dataset");
                            count++;
                        }
                    }
                    else if (ddlVehicleType.SelectedValue == "2")
                    {



                        foreach (GridViewRow row in gvtruck.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtRoad = (TextBox)row.FindControl("txtRoad");
                            TextBox txtPermit = (TextBox)row.FindControl("txtPermit");
                            TextBox txtInsurance = (TextBox)row.FindControl("txtInsurance");
                            TextBox txtTyre = (TextBox)row.FindControl("txtTyre");
                            TextBox txtBattery = (TextBox)row.FindControl("txtBattery");
                            TextBox txtMajorRepairs = (TextBox)row.FindControl("txtMajorRepairs");
                            //TextBox txtVehicleNo=(TextBox)row.FindControl("txtVehicleNo");


                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
                          new string[] { "flag", "AE_Id", "Vehicle_No", "Road_tax", "Permit_tax",
                                     "Insurance", "Tyre","Battery","Major_repaires" },
                            new string[] { "1", lblAE_id.Text, txtVehicleNo.Text, txtRoad.Text, txtPermit.Text
                           , txtInsurance.Text,txtTyre.Text,txtBattery.Text,txtMajorRepairs.Text }, "dataset");
                            count++;
                        }
                    }
                    else if (ddlVehicleType.SelectedValue == "3")
                    {



                        foreach (GridViewRow row in gvjeepcar.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtRoad = (TextBox)row.FindControl("txtRoad");
                            TextBox txtPermit = (TextBox)row.FindControl("txtPermit");
                            TextBox txtInsurance = (TextBox)row.FindControl("txtInsurance");
                            TextBox txtTyre = (TextBox)row.FindControl("txtTyre");
                            TextBox txtBattery = (TextBox)row.FindControl("txtBattery");
                            TextBox txtMajorRepairs = (TextBox)row.FindControl("txtMajorRepairs");
                            //TextBox txtVehicleNo=(TextBox)row.FindControl("txtVehicleNo");


                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
                          new string[] { "flag", "AE_Id", "Vehicle_No", "Road_tax", "Permit_tax",
                                     "Insurance", "Tyre","Battery","Major_repaires" },
                            new string[] { "1", lblAE_id.Text, txtVehicleNo.Text, txtRoad.Text, txtPermit.Text
                           , txtInsurance.Text,txtTyre.Text,txtBattery.Text,txtMajorRepairs.Text }, "dataset");
                            count++;
                        }
                    }

                    if (count > 0)
                    {

                        lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Saved Successfully");
                        //getdata();
                        ddlVehicleType.SelectedIndex = 0;
                        DDlMonth.SelectedIndex = 0;
                        ddlVehicleType_SelectedIndexChanged(sender, e);
                    }
                }

            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Select Month");
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvAEdetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        if (e.CommandName == "select")
        {
            int MPR_AE_Id = Convert.ToInt32(e.CommandArgument.ToString());
            lblAE_id.Text = e.CommandArgument.ToString();
            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
          new string[] { "flag", "Office_ID", "CreatedBy", "AE_Id" },
            new string[] { "3", objdb.Office_ID(), Session["Emp_ID"].ToString(), MPR_AE_Id.ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDlMonth.SelectedValue = ds.Tables[0].Rows[0]["Month"].ToString();
                    getyear();
                    ddlyear.SelectedValue = ds.Tables[0].Rows[0]["Year"].ToString();
                    ddlVehicleType.SelectedValue = ds.Tables[0].Rows[0]["Vehicle_Type_ID"].ToString();
                    ddlVehicleType.SelectedItem.Text = ds.Tables[0].Rows[0]["Vehicle_Type_Name"].ToString();
                  
                    // PDM_rowcount.ToString()


                    DataTable dt = new DataTable();
                    dt = ds.Tables[1];
                    if (ddlVehicleType.SelectedValue == "1" && ddlVehicleType.SelectedItem.Text == "Tanker")
                    {
                        divtanker.Visible = true;
                        divtruck.Visible = false;
                        divjeepcar.Visible = false;
                        divbutton.Visible = true;

                        ViewState["detailtanker"] = dt;
                        gvtanker.DataSource = dt;
                        gvtanker.DataBind();
                    }
                    else if (ddlVehicleType.SelectedValue == "2" && ddlVehicleType.SelectedItem.Text == "Truck")
                    {
                        divtanker.Visible = false;
                        divtruck.Visible = true;
                        divjeepcar.Visible = false;
                        divbutton.Visible = true;

                        ViewState["detailtruck"] = dt;
                        gvtruck.DataSource = dt;
                        gvtruck.DataBind();
                    }
                    else if (ddlVehicleType.SelectedValue == "3" && ddlVehicleType.SelectedItem.Text == "Jeeps & Cars")
                    {
                        divtanker.Visible = false;
                        divtruck.Visible = false;
                        divjeepcar.Visible = true;
                        divbutton.Visible = true;

                        ViewState["detailjeepcar"] = dt;
                        gvjeepcar.DataSource = dt;
                        gvjeepcar.DataBind();
                    }

                    else
                    {
                        divtanker.Visible = false;
                        divtruck.Visible = false;
                        divjeepcar.Visible = false;
                        divbutton.Visible = false;

                    }
                    btnSubmit.Visible = false;
                    btnupdate.Visible = true;
                    // txtnmUn_Skilled.Text = ds.Tables[0].Rows[0]["Year"].ToString();

                }
            }
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (DDlMonth.SelectedIndex > 0)
            {
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
                    new string[] { "flag","AE_Id", "Office_ID", "Vehicle_Type_ID", "Vehicle_Type_Name", "Month",
                                     "Year", "CreatedBy" },
                      new string[] { "5",lblAE_id.Text, objdb.Office_ID(), ddlVehicleType.SelectedValue, ddlVehicleType.SelectedItem.Text, DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    //lblDP_id.Text = ds.Tables[0].Rows[0]["MPR_DP_Id"].ToString();


                    DataSet dsdetail_delete = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
                     new string[] { "flag", "AE_Id" },
                       new string[] { "6", lblAE_id.Text }, "dataset");
                    if (ddlVehicleType.SelectedValue == "1")
                    {



                        foreach (GridViewRow row in gvtanker.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtRoad = (TextBox)row.FindControl("txtRoad");
                            TextBox txtPermit = (TextBox)row.FindControl("txtPermit");
                            TextBox txtInsurance = (TextBox)row.FindControl("txtInsurance");
                            TextBox txtTyre = (TextBox)row.FindControl("txtTyre");
                            TextBox txtBattery = (TextBox)row.FindControl("txtBattery");
                            TextBox txtMajorRepairs = (TextBox)row.FindControl("txtMajorRepairs");
                            //TextBox txtVehicleNo=(TextBox)row.FindControl("txtVehicleNo");


                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
                          new string[] { "flag", "AE_Id", "Vehicle_No", "Road_tax", "Permit_tax",
                                     "Insurance", "Tyre","Battery","Major_repaires" },
                            new string[] { "1", lblAE_id.Text, txtVehicleNo.Text, txtRoad.Text, txtPermit.Text
                           , txtInsurance.Text,txtTyre.Text,txtBattery.Text,txtMajorRepairs.Text }, "dataset");
                            count++;
                        }
                    }
                    else if (ddlVehicleType.SelectedValue == "2")
                    {



                        foreach (GridViewRow row in gvtruck.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtRoad = (TextBox)row.FindControl("txtRoad");
                            TextBox txtPermit = (TextBox)row.FindControl("txtPermit");
                            TextBox txtInsurance = (TextBox)row.FindControl("txtInsurance");
                            TextBox txtTyre = (TextBox)row.FindControl("txtTyre");
                            TextBox txtBattery = (TextBox)row.FindControl("txtBattery");
                            TextBox txtMajorRepairs = (TextBox)row.FindControl("txtMajorRepairs");
                            //TextBox txtVehicleNo=(TextBox)row.FindControl("txtVehicleNo");


                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
                          new string[] { "flag", "AE_Id", "Vehicle_No", "Road_tax", "Permit_tax",
                                     "Insurance", "Tyre","Battery","Major_repaires" },
                            new string[] { "1", lblAE_id.Text, txtVehicleNo.Text, txtRoad.Text, txtPermit.Text
                           , txtInsurance.Text,txtTyre.Text,txtBattery.Text,txtMajorRepairs.Text }, "dataset");
                            count++;
                        }
                    }
                    else if (ddlVehicleType.SelectedValue == "3")
                    {



                        foreach (GridViewRow row in gvjeepcar.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtRoad = (TextBox)row.FindControl("txtRoad");
                            TextBox txtPermit = (TextBox)row.FindControl("txtPermit");
                            TextBox txtInsurance = (TextBox)row.FindControl("txtInsurance");
                            TextBox txtTyre = (TextBox)row.FindControl("txtTyre");
                            TextBox txtBattery = (TextBox)row.FindControl("txtBattery");
                            TextBox txtMajorRepairs = (TextBox)row.FindControl("txtMajorRepairs");
                            //TextBox txtVehicleNo=(TextBox)row.FindControl("txtVehicleNo");


                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses",
                          new string[] { "flag", "AE_Id", "Vehicle_No", "Road_tax", "Permit_tax",
                                     "Insurance", "Tyre","Battery","Major_repaires" },
                            new string[] { "1", lblAE_id.Text, txtVehicleNo.Text, txtRoad.Text, txtPermit.Text
                           , txtInsurance.Text,txtTyre.Text,txtBattery.Text,txtMajorRepairs.Text }, "dataset");
                            count++;
                        }
                    }

                    if (count > 0)
                    {

                       
                        //getdata();
                        ddlVehicleType.SelectedIndex = 0;
                        DDlMonth.SelectedIndex = 0;
                        ddlVehicleType_SelectedIndexChanged(sender, e);
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Updated Successfully");
                    }
                }

            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Select Month");
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}