﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformationSystem_Diesel_Consumption : System.Web.UI.Page
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
                    // getdata();
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

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
               new string[] { "flag", "Office_ID", "CreatedBy" },
                 new string[] { "2", objdb.Office_ID(), Session["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDCdetail.DataSource = ds;
                    gvDCdetail.DataBind();
                    gvDCdetail.Visible = true;
                }
                else
                {
                    gvDCdetail.DataSource = null;
                    gvDCdetail.DataBind();
                }
            }
            else
            {
                gvDCdetail.DataSource = null;
                gvDCdetail.DataBind();
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
    public void getdata_byfilter()
    {
        try
        {

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year" },
                 new string[] { "4", objdb.Office_ID(), Session["Emp_ID"].ToString(),DDlMonth2.SelectedValue
                           , ddlyear2.SelectedItem.Text }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDCdetail.DataSource = ds;
                    gvDCdetail.DataBind();
                    gvDCdetail.Visible = true;
                    gvDCdetail.Rows[gvDCdetail.Rows.Count - 1].Focus();
                }
                else
                {
                    gvDCdetail.DataSource = null;
                    gvDCdetail.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                    lblMsg.Focus();
                }
            }
            else
            {
                gvDCdetail.DataSource = null;
                gvDCdetail.DataBind();
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
            // gvtanker.Focus();
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

            //DataRow dr = dt.NewRow();
            //dr["Sno"] = 1;
            //dr["VehicleNo"] = string.Empty;
            //dr["Opening"] = string.Empty;
            //dr["Closing"] = string.Empty;
            //dr["openbal"] = string.Empty;
            //dr["OwnPump"] = string.Empty;
            //dr["fromcc"] = string.Empty;
            //dr["MarketPurchaseLtr"] = string.Empty;
            //dr["MarketPurchaseRs"] = string.Empty;
            //dr["closingbal"] = string.Empty;

            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox txtVehicleNo = (TextBox)gvtanker.Rows[rowIndex].Cells[1].FindControl("txtVehicleNo");
                    TextBox txtOpening = (TextBox)gvtanker.Rows[rowIndex].Cells[2].FindControl("txtOpening");
                    TextBox txtClosing = (TextBox)gvtanker.Rows[rowIndex].Cells[3].FindControl("txtClosing");
                    TextBox txtopenbal = (TextBox)gvtanker.Rows[rowIndex].Cells[4].FindControl("txtopenbal");
                    TextBox txtOwnPump = (TextBox)gvtanker.Rows[rowIndex].Cells[5].FindControl("txtOwnPump");
                    TextBox txtfromcc = (TextBox)gvtanker.Rows[rowIndex].Cells[6].FindControl("txtfromcc");
                    TextBox txtMarketPurchaseLtr = (TextBox)gvtanker.Rows[rowIndex].Cells[7].FindControl("txtMarketPurchaseLtr");
                    TextBox txtMarketPurchaseRs = (TextBox)gvtanker.Rows[rowIndex].Cells[8].FindControl("txtMarketPurchaseRs");
                    TextBox txtclosingbal = (TextBox)gvtanker.Rows[rowIndex].Cells[9].FindControl("txtclosingbal");

                    dr = dt.NewRow();

                    dt.Rows[i - 1]["Sno"] = i + 1;
                    dt.Rows[i - 1]["VehicleNo"] = txtVehicleNo.Text;
                    dt.Rows[i - 1]["Opening"] = txtOpening.Text;
                    dt.Rows[i - 1]["Closing"] = txtClosing.Text;
                    dt.Rows[i - 1]["openbal"] = txtopenbal.Text;
                    dt.Rows[i - 1]["OwnPump"] = txtOwnPump.Text;
                    dt.Rows[i - 1]["fromcc"] = txtfromcc.Text;
                    dt.Rows[i - 1]["MarketPurchaseLtr"] = txtMarketPurchaseLtr.Text;
                    dt.Rows[i - 1]["MarketPurchaseRs"] = txtMarketPurchaseRs.Text;
                    dt.Rows[i - 1]["closingbal"] = txtclosingbal.Text;
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

            //DataRow dr = dt.NewRow();
            //dr["Sno"] = 1;
            //dr["VehicleNo"] = string.Empty;
            //dr["Opening"] = string.Empty;
            //dr["Closing"] = string.Empty;
            //dr["openbal"] = string.Empty;
            //dr["OwnPump"] = string.Empty;
            //dr["fromcc"] = string.Empty;
            //dr["MarketPurchaseLtr"] = string.Empty;
            //dr["MarketPurchaseRs"] = string.Empty;
            //dr["closingbal"] = string.Empty;

            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox txtVehicleNo = (TextBox)gvtruck.Rows[rowIndex].Cells[1].FindControl("txtVehicleNo");
                    TextBox txtOpening = (TextBox)gvtruck.Rows[rowIndex].Cells[2].FindControl("txtOpening");
                    TextBox txtClosing = (TextBox)gvtruck.Rows[rowIndex].Cells[3].FindControl("txtClosing");
                    TextBox txtopenbal = (TextBox)gvtruck.Rows[rowIndex].Cells[4].FindControl("txtopenbal");
                    TextBox txtOwnPump = (TextBox)gvtruck.Rows[rowIndex].Cells[5].FindControl("txtOwnPump");
                    TextBox txtfromcc = (TextBox)gvtruck.Rows[rowIndex].Cells[6].FindControl("txtfromcc");
                    TextBox txtMarketPurchaseLtr = (TextBox)gvtruck.Rows[rowIndex].Cells[7].FindControl("txtMarketPurchaseLtr");
                    TextBox txtMarketPurchaseRs = (TextBox)gvtruck.Rows[rowIndex].Cells[8].FindControl("txtMarketPurchaseRs");
                    TextBox txtclosingbal = (TextBox)gvtruck.Rows[rowIndex].Cells[9].FindControl("txtclosingbal");

                    dr = dt.NewRow();

                    dt.Rows[i - 1]["Sno"] = i + 1;
                    dt.Rows[i - 1]["VehicleNo"] = txtVehicleNo.Text;
                    dt.Rows[i - 1]["Opening"] = txtOpening.Text;
                    dt.Rows[i - 1]["Closing"] = txtClosing.Text;
                    dt.Rows[i - 1]["openbal"] = txtopenbal.Text;
                    dt.Rows[i - 1]["OwnPump"] = txtOwnPump.Text;
                    dt.Rows[i - 1]["fromcc"] = txtfromcc.Text;
                    dt.Rows[i - 1]["MarketPurchaseLtr"] = txtMarketPurchaseLtr.Text;
                    dt.Rows[i - 1]["MarketPurchaseRs"] = txtMarketPurchaseRs.Text;
                    dt.Rows[i - 1]["closingbal"] = txtclosingbal.Text;
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


            //DataRow dr = dt.NewRow();
            //dr["Sno"] = 1;
            //dr["VehicleNo"] = string.Empty;
            //dr["Opening"] = string.Empty;
            //dr["Closing"] = string.Empty;
            //dr["openbal"] = string.Empty;
            //dr["OwnPump"] = string.Empty;
            //dr["fromcc"] = string.Empty;
            //dr["MarketPurchaseLtr"] = string.Empty;
            //dr["MarketPurchaseRs"] = string.Empty;
            //dr["closingbal"] = string.Empty;

            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox txtVehicleNo = (TextBox)gvjeepcar.Rows[rowIndex].Cells[1].FindControl("txtVehicleNo");
                    TextBox txtOpening = (TextBox)gvjeepcar.Rows[rowIndex].Cells[2].FindControl("txtOpening");
                    TextBox txtClosing = (TextBox)gvjeepcar.Rows[rowIndex].Cells[3].FindControl("txtClosing");
                    TextBox txtopenbal = (TextBox)gvjeepcar.Rows[rowIndex].Cells[4].FindControl("txtopenbal");
                    TextBox txtOwnPump = (TextBox)gvjeepcar.Rows[rowIndex].Cells[5].FindControl("txtOwnPump");
                    TextBox txtfromcc = (TextBox)gvjeepcar.Rows[rowIndex].Cells[6].FindControl("txtfromcc");
                    TextBox txtMarketPurchaseLtr = (TextBox)gvjeepcar.Rows[rowIndex].Cells[7].FindControl("txtMarketPurchaseLtr");
                    TextBox txtMarketPurchaseRs = (TextBox)gvjeepcar.Rows[rowIndex].Cells[8].FindControl("txtMarketPurchaseRs");
                    TextBox txtclosingbal = (TextBox)gvjeepcar.Rows[rowIndex].Cells[9].FindControl("txtclosingbal");

                    dr = dt.NewRow();

                    dt.Rows[i - 1]["Sno"] = i + 1;
                    dt.Rows[i - 1]["VehicleNo"] = txtVehicleNo.Text;
                    dt.Rows[i - 1]["Opening"] = txtOpening.Text;
                    dt.Rows[i - 1]["Closing"] = txtClosing.Text;
                    dt.Rows[i - 1]["openbal"] = txtopenbal.Text;
                    dt.Rows[i - 1]["OwnPump"] = txtOwnPump.Text;
                    dt.Rows[i - 1]["fromcc"] = txtfromcc.Text;
                    dt.Rows[i - 1]["MarketPurchaseLtr"] = txtMarketPurchaseLtr.Text;
                    dt.Rows[i - 1]["MarketPurchaseRs"] = txtMarketPurchaseRs.Text;
                    dt.Rows[i - 1]["closingbal"] = txtclosingbal.Text;
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
                dt.Columns.Add("Opening", typeof(string));
                dt.Columns.Add("Closing", typeof(string));
                dt.Columns.Add("openbal", typeof(string));
                dt.Columns.Add("OwnPump", typeof(string));
                dt.Columns.Add("fromcc", typeof(string));
                dt.Columns.Add("MarketPurchaseLtr", typeof(string));
                dt.Columns.Add("MarketPurchaseRs", typeof(string));
                dt.Columns.Add("closingbal", typeof(string));

                DataRow dr = dt.NewRow();
                dr["Sno"] = 1;
                dr["VehicleNo"] = string.Empty;
                dr["Opening"] = string.Empty;
                dr["Closing"] = string.Empty;
                dr["openbal"] = string.Empty;
                dr["OwnPump"] = string.Empty;
                dr["fromcc"] = string.Empty;
                dr["MarketPurchaseLtr"] = string.Empty;
                dr["MarketPurchaseRs"] = string.Empty;
                dr["closingbal"] = string.Empty;

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
                dt.Columns.Add("Opening", typeof(string));
                dt.Columns.Add("Closing", typeof(string));
                dt.Columns.Add("openbal", typeof(string));
                dt.Columns.Add("OwnPump", typeof(string));
                dt.Columns.Add("fromcc", typeof(string));
                dt.Columns.Add("MarketPurchaseLtr", typeof(string));
                dt.Columns.Add("MarketPurchaseRs", typeof(string));
                dt.Columns.Add("closingbal", typeof(string));

                DataRow dr = dt.NewRow();
                dr["Sno"] = 1;
                dr["VehicleNo"] = string.Empty;
                dr["Opening"] = string.Empty;
                dr["Closing"] = string.Empty;
                dr["openbal"] = string.Empty;
                dr["OwnPump"] = string.Empty;
                dr["fromcc"] = string.Empty;
                dr["MarketPurchaseLtr"] = string.Empty;
                dr["MarketPurchaseRs"] = string.Empty;
                dr["closingbal"] = string.Empty;

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
                dt.Columns.Add("Opening", typeof(string));
                dt.Columns.Add("Closing", typeof(string));
                dt.Columns.Add("openbal", typeof(string));
                dt.Columns.Add("OwnPump", typeof(string));
                dt.Columns.Add("fromcc", typeof(string));
                dt.Columns.Add("MarketPurchaseLtr", typeof(string));
                dt.Columns.Add("MarketPurchaseRs", typeof(string));
                dt.Columns.Add("closingbal", typeof(string));

                DataRow dr = dt.NewRow();
                dr["Sno"] = 1;
                dr["VehicleNo"] = string.Empty;
                dr["Opening"] = string.Empty;
                dr["Closing"] = string.Empty;
                dr["openbal"] = string.Empty;
                dr["OwnPump"] = string.Empty;
                dr["fromcc"] = string.Empty;
                dr["MarketPurchaseLtr"] = string.Empty;
                dr["MarketPurchaseRs"] = string.Empty;
                dr["closingbal"] = string.Empty;

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
            // cell.Text = "Vehicle No.";
            // cell.RowSpan = 2;
            // cell.ColumnSpan = 1;
            row.Controls.Add(cell);


            cell = new TableHeaderCell();
            cell.Text = "Meter Reading";
            cell.ColumnSpan = 2;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Diesel Information";
            cell.ColumnSpan = 6;
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
            cell.Text = "Meter Reading";
            cell.ColumnSpan = 2;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Diesel Information";
            cell.ColumnSpan = 6;
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
            // gvtruck.Focus();
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
            cell.Text = "Meter Reading";
            cell.ColumnSpan = 2;

            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Diesel Information";
            cell.ColumnSpan = 6;
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
            // gvtruck.Focus();
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
            // gvtanker.Focus();
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
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
                    new string[] { "flag", "Office_ID", "Vehicle_Type_ID", "Vehicle_Type_Name", "Month",
                                     "Year", "CreatedBy" },
                      new string[] { "0", objdb.Office_ID(), ddlVehicleType.SelectedValue, ddlVehicleType.SelectedItem.Text, DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    lblDC_ID.Text = ds.Tables[0].Rows[0]["DC_Id"].ToString();
                    if (ddlVehicleType.SelectedValue == "1")
                    {



                        foreach (GridViewRow row in gvtanker.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtOpening = (TextBox)row.FindControl("txtOpening");
                            TextBox txtClosing = (TextBox)row.FindControl("txtClosing");
                            TextBox txtopenbal = (TextBox)row.FindControl("txtopenbal");
                            TextBox txtOwnPump = (TextBox)row.FindControl("txtOwnPump");
                            TextBox txtfromcc = (TextBox)row.FindControl("txtfromcc");
                            TextBox txtMarketPurchaseLtr = (TextBox)row.FindControl("txtMarketPurchaseLtr");
                            TextBox txtMarketPurchaseRs = (TextBox)row.FindControl("txtMarketPurchaseRs");
                            TextBox txtclosingbal = (TextBox)row.FindControl("txtclosingbal");



                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
                          new string[] { "flag", "DC_Id", "Vehicle_No", "Opening", "Closing",
                                     "Opbal", "Ownpump","FromCC","Market_purchase_Ltr","Market_purchase_Rs","Clobal" },
                            new string[] { "1", lblDC_ID.Text, txtVehicleNo.Text, txtOpening.Text, txtClosing.Text
                           , txtopenbal.Text,txtOwnPump.Text,txtfromcc.Text,txtMarketPurchaseLtr.Text,txtMarketPurchaseRs.Text,txtclosingbal.Text }, "dataset");
                            count++;
                        }
                    }
                    else if (ddlVehicleType.SelectedValue == "2")
                    {



                        foreach (GridViewRow row in gvtruck.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtOpening = (TextBox)row.FindControl("txtOpening");
                            TextBox txtClosing = (TextBox)row.FindControl("txtClosing");
                            TextBox txtopenbal = (TextBox)row.FindControl("txtopenbal");
                            TextBox txtOwnPump = (TextBox)row.FindControl("txtOwnPump");
                            TextBox txtfromcc = (TextBox)row.FindControl("txtfromcc");
                            TextBox txtMarketPurchaseLtr = (TextBox)row.FindControl("txtMarketPurchaseLtr");
                            TextBox txtMarketPurchaseRs = (TextBox)row.FindControl("txtMarketPurchaseRs");
                            TextBox txtclosingbal = (TextBox)row.FindControl("txtclosingbal");



                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
                          new string[] { "flag", "DC_Id", "Vehicle_No", "Opening", "Closing",
                                     "Opbal", "Ownpump","FromCC","Market_purchase_Ltr","Market_purchase_Rs","Clobal" },
                            new string[] { "1", lblDC_ID.Text, txtVehicleNo.Text, txtOpening.Text, txtClosing.Text
                           , txtopenbal.Text,txtOwnPump.Text,txtfromcc.Text,txtMarketPurchaseLtr.Text,txtMarketPurchaseRs.Text,txtclosingbal.Text }, "dataset");
                            count++;
                        }
                    }
                    else if (ddlVehicleType.SelectedValue == "3")
                    {



                        foreach (GridViewRow row in gvjeepcar.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtOpening = (TextBox)row.FindControl("txtOpening");
                            TextBox txtClosing = (TextBox)row.FindControl("txtClosing");
                            TextBox txtopenbal = (TextBox)row.FindControl("txtopenbal");
                            TextBox txtOwnPump = (TextBox)row.FindControl("txtOwnPump");
                            TextBox txtfromcc = (TextBox)row.FindControl("txtfromcc");
                            TextBox txtMarketPurchaseLtr = (TextBox)row.FindControl("txtMarketPurchaseLtr");
                            TextBox txtMarketPurchaseRs = (TextBox)row.FindControl("txtMarketPurchaseRs");
                            TextBox txtclosingbal = (TextBox)row.FindControl("txtclosingbal");



                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
                          new string[] { "flag", "DC_Id", "Vehicle_No", "Opening", "Closing",
                                     "Opbal", "Ownpump","FromCC","Market_purchase_Ltr","Market_purchase_Rs","Clobal" },
                            new string[] { "1", lblDC_ID.Text, txtVehicleNo.Text, txtOpening.Text, txtClosing.Text
                           , txtopenbal.Text,txtOwnPump.Text,txtfromcc.Text,txtMarketPurchaseLtr.Text,txtMarketPurchaseRs.Text,txtclosingbal.Text }, "dataset");
                            count++;
                        }
                    }

                    if (count > 0)
                    {

                      
                        //getdata();
                        DDlMonth.SelectedIndex = 0;
                        ddlVehicleType.SelectedIndex = 0;
                        ddlVehicleType_SelectedIndexChanged(sender, e);
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Saved Successfully");
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

    protected void gvDCdetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        if (e.CommandName == "select")
        {
            int MPR_DC_ID = Convert.ToInt32(e.CommandArgument.ToString());
            lblDC_ID.Text = e.CommandArgument.ToString();
            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
          new string[] { "flag", "Office_ID", "CreatedBy", "DC_ID" },
            new string[] { "3", objdb.Office_ID(), Session["Emp_ID"].ToString(), MPR_DC_ID.ToString() }, "dataset");
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
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
                    new string[] { "flag","DC_ID", "Office_ID", "Vehicle_Type_ID", "Vehicle_Type_Name", "Month",
                                     "Year", "CreatedBy" },
                      new string[] { "5",lblDC_ID.Text, objdb.Office_ID(), ddlVehicleType.SelectedValue, ddlVehicleType.SelectedItem.Text, DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    // lblDC_ID.Text = ds.Tables[0].Rows[0]["DC_Id"].ToString();

                    DataSet dsdetail_delete = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
                    new string[] { "flag", "DC_ID" },
                      new string[] { "6", lblDC_ID.Text }, "dataset");
                    if (ddlVehicleType.SelectedValue == "1")
                    {



                        foreach (GridViewRow row in gvtanker.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtOpening = (TextBox)row.FindControl("txtOpening");
                            TextBox txtClosing = (TextBox)row.FindControl("txtClosing");
                            TextBox txtopenbal = (TextBox)row.FindControl("txtopenbal");
                            TextBox txtOwnPump = (TextBox)row.FindControl("txtOwnPump");
                            TextBox txtfromcc = (TextBox)row.FindControl("txtfromcc");
                            TextBox txtMarketPurchaseLtr = (TextBox)row.FindControl("txtMarketPurchaseLtr");
                            TextBox txtMarketPurchaseRs = (TextBox)row.FindControl("txtMarketPurchaseRs");
                            TextBox txtclosingbal = (TextBox)row.FindControl("txtclosingbal");



                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
                          new string[] { "flag", "DC_Id", "Vehicle_No", "Opening", "Closing",
                                     "Opbal", "Ownpump","FromCC","Market_purchase_Ltr","Market_purchase_Rs","Clobal" },
                            new string[] { "1", lblDC_ID.Text, txtVehicleNo.Text, txtOpening.Text, txtClosing.Text
                           , txtopenbal.Text,txtOwnPump.Text,txtfromcc.Text,txtMarketPurchaseLtr.Text,txtMarketPurchaseRs.Text,txtclosingbal.Text }, "dataset");
                            count++;
                        }
                    }
                    else if (ddlVehicleType.SelectedValue == "2")
                    {



                        foreach (GridViewRow row in gvtruck.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtOpening = (TextBox)row.FindControl("txtOpening");
                            TextBox txtClosing = (TextBox)row.FindControl("txtClosing");
                            TextBox txtopenbal = (TextBox)row.FindControl("txtopenbal");
                            TextBox txtOwnPump = (TextBox)row.FindControl("txtOwnPump");
                            TextBox txtfromcc = (TextBox)row.FindControl("txtfromcc");
                            TextBox txtMarketPurchaseLtr = (TextBox)row.FindControl("txtMarketPurchaseLtr");
                            TextBox txtMarketPurchaseRs = (TextBox)row.FindControl("txtMarketPurchaseRs");
                            TextBox txtclosingbal = (TextBox)row.FindControl("txtclosingbal");



                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
                          new string[] { "flag", "DC_Id", "Vehicle_No", "Opening", "Closing",
                                     "Opbal", "Ownpump","FromCC","Market_purchase_Ltr","Market_purchase_Rs","Clobal" },
                            new string[] { "1", lblDC_ID.Text, txtVehicleNo.Text, txtOpening.Text, txtClosing.Text
                           , txtopenbal.Text,txtOwnPump.Text,txtfromcc.Text,txtMarketPurchaseLtr.Text,txtMarketPurchaseRs.Text,txtclosingbal.Text }, "dataset");
                            count++;
                        }
                    }
                    else if (ddlVehicleType.SelectedValue == "3")
                    {



                        foreach (GridViewRow row in gvjeepcar.Rows)
                        {
                            TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");
                            TextBox txtOpening = (TextBox)row.FindControl("txtOpening");
                            TextBox txtClosing = (TextBox)row.FindControl("txtClosing");
                            TextBox txtopenbal = (TextBox)row.FindControl("txtopenbal");
                            TextBox txtOwnPump = (TextBox)row.FindControl("txtOwnPump");
                            TextBox txtfromcc = (TextBox)row.FindControl("txtfromcc");
                            TextBox txtMarketPurchaseLtr = (TextBox)row.FindControl("txtMarketPurchaseLtr");
                            TextBox txtMarketPurchaseRs = (TextBox)row.FindControl("txtMarketPurchaseRs");
                            TextBox txtclosingbal = (TextBox)row.FindControl("txtclosingbal");



                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption",
                          new string[] { "flag", "DC_Id", "Vehicle_No", "Opening", "Closing",
                                     "Opbal", "Ownpump","FromCC","Market_purchase_Ltr","Market_purchase_Rs","Clobal" },
                            new string[] { "1", lblDC_ID.Text, txtVehicleNo.Text, txtOpening.Text, txtClosing.Text
                           , txtopenbal.Text,txtOwnPump.Text,txtfromcc.Text,txtMarketPurchaseLtr.Text,txtMarketPurchaseRs.Text,txtclosingbal.Text }, "dataset");
                            count++;
                        }
                    }

                    if (count > 0)
                    {

                        
                        //getdata();
                        DDlMonth.SelectedIndex = 0;
                        ddlVehicleType.SelectedIndex = 0;
                        ddlVehicleType_SelectedIndexChanged(sender, e);
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Updated Successfully");
                        //lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Updated Successfully");
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