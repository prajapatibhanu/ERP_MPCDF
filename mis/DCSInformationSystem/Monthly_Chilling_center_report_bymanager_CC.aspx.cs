using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformationSystem_Monthly_Chilling_center_report_bymanager : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
               

                //txtMilk_Hand_variation_Q_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                //txtMilk_Hand_variation_Per_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                //txtProduct_mfg_variation_Q_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                //txtProduct_mfg_variation_Per_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                //txtTanker_milk_variation_Q_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                //txtTanker_milk_variation_Per_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                
                if (!IsPostBack)
                {
                    txtMilk_Hand_variation_Q_Qty.Text = "0";
                    txtMilk_Hand_variation_Per_Qty.Text = "0";
                    txtProduct_mfg_variation_Q_Qty.Text = "0";
                    txtProduct_mfg_variation_Per_Qty.Text = "0";
                    txtTanker_milk_variation_Q_Qty.Text = "0";
                    txtTanker_milk_variation_Per_Qty.Text = "0";
                    txtMilk_Hand_variation_Q_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                    txtMilk_Hand_variation_Per_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                    txtProduct_mfg_variation_Q_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                    txtProduct_mfg_variation_Per_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                    txtTanker_milk_variation_Q_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                    txtTanker_milk_variation_Per_Qty.Attributes.Add("ReadOnly", "ReadOnly");
                    ViewState["Otherexpenditure"] = "";
                    getyear();
                   // getdata();
                    //BindGridviewOtherexpenditure();

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
    protected void BindGridviewOtherexpenditure()
    {
        try
        {
            if (ViewState["Otherexpenditure"].ToString() == "")
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Sno", typeof(int));
                dt.Columns.Add("Expendditure_name", typeof(string));
                dt.Columns.Add("Unit", typeof(decimal));

                dt.Columns.Add("Amount", typeof(decimal));


                DataRow dr = dt.NewRow();
                dr["Sno"] = 1;
                dr["Expendditure_name"] = string.Empty;
                dr["Unit"] = 0;

                dr["Amount"] = 0;


                dt.Rows.Add(dr);
                ViewState["Otherexpenditure"] = dt;
                gvOtherexpenditure.DataSource = dt;
                gvOtherexpenditure.DataBind();
            }
            else
            {
                DataTable dt = (DataTable)ViewState["Otherexpenditure"];
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
                ViewState["Otherexpenditure"] = dt;
                gvOtherexpenditure.DataSource = dt;
                gvOtherexpenditure.DataBind();
            }

            


        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void getdata()
    {
        try
        {
            
            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Chilling_center_Report_By_manager",
               new string[] { "flag", "Office_ID", "CreatedBy" },
                 new string[] { "2", objdb.Office_ID(), Session["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvMCCRdetail.DataSource = ds;
                    gvMCCRdetail.DataBind();
                    gvMCCRdetail.Visible = true;
                }
                else
                {
                    gvMCCRdetail.DataSource = null;
                    gvMCCRdetail.DataBind();
                }
            }
            else
            {
                gvMCCRdetail.DataSource = null;
                gvMCCRdetail.DataBind();
               
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

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Chilling_center_Report_By_manager",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year" },
                 new string[] { "6", objdb.Office_ID(), Session["Emp_ID"].ToString(),DDlMonth2.SelectedValue
                           , ddlyear2.SelectedItem.Text }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvMCCRdetail.DataSource = ds;
                    gvMCCRdetail.DataBind();
                    gvMCCRdetail.Visible = true;
                    gvMCCRdetail.Rows[gvMCCRdetail.Rows.Count - 1].Focus();
                }
                else
                {
                    gvMCCRdetail.DataSource = null;
                    gvMCCRdetail.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                    lblMsg.Focus();
                }
            }
            else
            {
                gvMCCRdetail.DataSource = null;
                gvMCCRdetail.DataBind();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                lblMsg.Focus();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvOtherexpenditure_DataBound(object sender, EventArgs e)
    {

    }
    protected void btnAddrow_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (gvOtherexpenditure.Rows.Count == 0)
            {
                BindGridviewOtherexpenditure();
            }
            else if (gvOtherexpenditure.Rows.Count > 0)
            {
                AddNewRow();
            }
            //gvOtherexpenditure.Focus();
            gvOtherexpenditure.Rows[gvOtherexpenditure.Rows.Count - 1].Focus();
            
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void AddNewRow()
    {
        try
        {


            lblMsg.Text = "";
            int rowIndex = 0;
            DataTable dt = (DataTable)ViewState["Otherexpenditure"];
            int j = 0;
            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {

                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox txtExpendditure_name = (TextBox)gvOtherexpenditure.Rows[rowIndex].Cells[1].FindControl("txtExpendditure_name");
                    TextBox txtUnit = (TextBox)gvOtherexpenditure.Rows[rowIndex].Cells[2].FindControl("txtUnit");

                    TextBox txtAmount = (TextBox)gvOtherexpenditure.Rows[rowIndex].Cells[4].FindControl("txtAmount");

                    if (txtUnit.Text == "")
                    {
                        txtUnit.Text = "0";
                    }

                    if (txtAmount.Text == "")
                    {
                        txtAmount.Text = "0";
                    }

                    //  TextBox txtDateOfReturn = (TextBox)gvtanker.Rows[rowIndex].Cells[8].FindControl("txtDateOfReturn");

                    dr = dt.NewRow();

                    dt.Rows[i - 1]["Sno"] = i + 1;
                    dt.Rows[i - 1]["Expendditure_name"] = txtExpendditure_name.Text;
                    dt.Rows[i - 1]["Unit"] = txtUnit.Text;

                    dt.Rows[i - 1]["Amount"] = txtAmount.Text;

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
                    j++;
                }
            }
            //   DataRow dr = dt.NewRow();
            dr["Sno"] = j + 1;
            dr["Expendditure_name"] = string.Empty;
            dr["Unit"] = 0;

            dr["Amount"] = 0;

            dt.Rows.Add(dr);
            ViewState["Otherexpenditure"] = dt;
            gvOtherexpenditure.DataSource = dt;
            gvOtherexpenditure.DataBind();

        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvOtherexpenditure_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ViewState["Otherexpenditure"] != null)
            {
                DataTable dt = (DataTable)ViewState["Otherexpenditure"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Otherexpenditure"] = dt;
                    gvOtherexpenditure.DataSource = dt;
                    gvOtherexpenditure.DataBind();

                    for (int i = 0; i < gvOtherexpenditure.Rows.Count - 1; i++)
                    {
                        gvOtherexpenditure.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }


                }
            }
            //gvOtherexpenditure.OptionsNavigation.AutoFocusNewRow = true;
            gvOtherexpenditure.Rows[gvOtherexpenditure.Rows.Count-1].Focus();
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void gvMCCRdetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "select")
            {
                int MCCR_ID = Convert.ToInt32(e.CommandArgument.ToString());
                lblMCCR_ID.Text = e.CommandArgument.ToString();
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Chilling_center_Report_By_manager",
              new string[] { "flag", "Office_ID", "CreatedBy", "MCCR_ID" },
                new string[] { "3", objdb.Office_ID(), Session["Emp_ID"].ToString(), MCCR_ID.ToString() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DDlMonth.SelectedValue = ds.Tables[0].Rows[0]["Month"].ToString();
                        getyear();
                        ddlyear.SelectedValue = ds.Tables[0].Rows[0]["Year"].ToString();
                        txtOB_M_Qty.Text = ds.Tables[0].Rows[0]["OB_M_Qty"].ToString();
                        txtOB_M_Fat.Text = ds.Tables[0].Rows[0]["OB_M_Fat"].ToString();
                        txtOB_M_SNF.Text = ds.Tables[0].Rows[0]["OB_M_SNF"].ToString();
                        txtMilk_pur_good_Qty.Text = ds.Tables[0].Rows[0]["Milk_pur_good_Qty"].ToString();
                        txtMilk_pur_good_Fat.Text = ds.Tables[0].Rows[0]["Milk_pur_good_Fat"].ToString();
                        txtMilk_pur_good_SNF.Text = ds.Tables[0].Rows[0]["Milk_pur_good_SNF"].ToString();
                        txtMilk_pur_Sour_Qty.Text = ds.Tables[0].Rows[0]["Milk_pur_Sour_Qty"].ToString();
                        txtMilk_pur_Sour_Fat.Text = ds.Tables[0].Rows[0]["Milk_pur_Sour_Fat"].ToString();
                        txtMilk_pur_Sour_SNF.Text = ds.Tables[0].Rows[0]["Milk_pur_Sour_SNF"].ToString();
                        txtMilk_pur_Curdle_Qty.Text = ds.Tables[0].Rows[0]["Milk_pur_Curdle_Qty"].ToString();
                        txtMilk_pur_Curdle_Fat.Text = ds.Tables[0].Rows[0]["Milk_pur_Curdle_Fat"].ToString();
                        txtMilk_pur_Curdle_SNF.Text = ds.Tables[0].Rows[0]["Milk_pur_Curdle_SNF"].ToString();
                        txtSC_milk_for_prod_Qty.Text = ds.Tables[0].Rows[0]["SC_milk_for_prod_Qty"].ToString();
                        txtSC_milk_for_prod_Fat.Text = ds.Tables[0].Rows[0]["SC_milk_for_prod_Fat"].ToString();
                        txtSC_milk_for_prod_SNF.Text = ds.Tables[0].Rows[0]["SC_milk_for_prod_SNF"].ToString();
                        txtMilk_dispatch_dairy_Qty.Text = ds.Tables[0].Rows[0]["Milk_dispatch_dairy_Qty"].ToString();
                        txtMilk_dispatch_dairy_Fat.Text = ds.Tables[0].Rows[0]["Milk_dispatch_dairy_Fat"].ToString();
                        txtMilk_dispatch_dairy_SNF.Text = ds.Tables[0].Rows[0]["Milk_dispatch_dairy_SNF"].ToString();
                        txtCB_M_Qty.Text = ds.Tables[0].Rows[0]["CB_M_Qty"].ToString();
                        txtCB_M_Fat.Text = ds.Tables[0].Rows[0]["CB_M_Fat"].ToString();
                        txtCB_M_SNF.Text = ds.Tables[0].Rows[0]["CB_M_SNF"].ToString();
                        txtWhite_butter_SC_OB_Qty.Text = ds.Tables[0].Rows[0]["White_butter_SC_OB_Qty"].ToString();
                        txtWhite_butter_SC_OB_Fat.Text = ds.Tables[0].Rows[0]["White_butter_SC_OB_Fat"].ToString();
                        txtWhite_butter_SC_OB_SNF.Text = ds.Tables[0].Rows[0]["White_butter_SC_OB_SNF"].ToString();
                        txtWhite_butter_SC_WBM_Qty.Text = ds.Tables[0].Rows[0]["White_butter_SC_WBM_Qty"].ToString();
                        txtWhite_butter_SC_WBM_Fat.Text = ds.Tables[0].Rows[0]["White_butter_SC_WBM_Fat"].ToString();
                        txtWhite_butter_SC_WBM_SNF.Text = ds.Tables[0].Rows[0]["White_butter_SC_WBM_SNF"].ToString();
                        txtWhite_butter_SC_CB_Qty.Text = ds.Tables[0].Rows[0]["White_butter_SC_CB_Qty"].ToString();
                        txtWhite_butter_SC_CB_Fat.Text = ds.Tables[0].Rows[0]["White_butter_SC_CB_Fat"].ToString();
                        txtWhite_butter_SC_CB_SNF.Text = ds.Tables[0].Rows[0]["White_butter_SC_CB_SNF"].ToString();
                        txtMilk_Hand_variation_Q_Qty.Text = ds.Tables[0].Rows[0]["Milk_Hand_variation_Q_Qty"].ToString();
                        txtMilk_Hand_variation_Q_Fat.Text = ds.Tables[0].Rows[0]["Milk_Hand_variation_Q_Fat"].ToString();
                        txtMilk_Hand_variation_Q_SNF.Text = ds.Tables[0].Rows[0]["Milk_Hand_variation_Q_SNF"].ToString();
                        txtMilk_Hand_variation_Per_Qty.Text = ds.Tables[0].Rows[0]["Milk_Hand_variation_Per_Qty"].ToString();
                        txtMilk_Hand_variation_Per_Fat.Text = ds.Tables[0].Rows[0]["Milk_Hand_variation_Per_Fat"].ToString();
                        txtMilk_Hand_variation_Per_SNF.Text = ds.Tables[0].Rows[0]["Milk_Hand_variation_Per_SNF"].ToString();
                        txtProduct_mfg_variation_Q_Qty.Text = ds.Tables[0].Rows[0]["Product_mfg_variation_Q_Qty"].ToString();
                        txtProduct_mfg_variation_Q_Fat.Text = ds.Tables[0].Rows[0]["Product_mfg_variation_Q_Fat"].ToString();
                        txtProduct_mfg_variation_Q_SNF.Text = ds.Tables[0].Rows[0]["Product_mfg_variation_Q_SNF"].ToString();
                        txtProduct_mfg_variation_Per_Qty.Text = ds.Tables[0].Rows[0]["Product_mfg_variation_Per_Qty"].ToString();
                        txtProduct_mfg_variation_Per_Fat.Text = ds.Tables[0].Rows[0]["Product_mfg_variation_Per_Fat"].ToString();
                        txtProduct_mfg_variation_Per_SNF.Text = ds.Tables[0].Rows[0]["Product_mfg_variation_Per_SNF"].ToString();
                        txtTanker_milk_Rec_DP_Qty.Text = ds.Tables[0].Rows[0]["Tanker_milk_Rec_DP_Qty"].ToString();
                        txtTanker_milk_Rec_DP_Fat.Text = ds.Tables[0].Rows[0]["Tanker_milk_Rec_DP_Fat"].ToString();
                        txtTanker_milk_Rec_DP_SNF.Text = ds.Tables[0].Rows[0]["Tanker_milk_Rec_DP_SNF"].ToString();
                        txtTanker_milk_variation_Q_Qty.Text = ds.Tables[0].Rows[0]["Tanker_milk_variation_Q_Qty"].ToString();
                        txtTanker_milk_variation_Q_Fat.Text = ds.Tables[0].Rows[0]["Tanker_milk_variation_Q_Fat"].ToString();
                        txtTanker_milk_variation_Q_SNF.Text = ds.Tables[0].Rows[0]["Tanker_milk_variation_Q_SNF"].ToString();
                        txtTanker_milk_variation_Per_Qty.Text = ds.Tables[0].Rows[0]["Tanker_milk_variation_Per_Qty"].ToString();
                        txtTanker_milk_variation_Per_Fat.Text = ds.Tables[0].Rows[0]["Tanker_milk_variation_Per_Fat"].ToString();
                        txtTanker_milk_variation_Per_SNF.Text = ds.Tables[0].Rows[0]["Tanker_milk_variation_Per_SNF"].ToString();
                        txtStock_OB_WB.Text = ds.Tables[0].Rows[0]["Stock_OB_WB"].ToString();
                        txtStock_OB_Ghee.Text = ds.Tables[0].Rows[0]["Stock_OB_Ghee"].ToString();
                        txtStock_OB_Cattlefeed.Text = ds.Tables[0].Rows[0]["Stock_OB_Cattlefeed"].ToString();
                        txtStock_Manufa_WB.Text = ds.Tables[0].Rows[0]["Stock_Manufa_WB"].ToString();
                        txtStock_Manufa_Ghee.Text = ds.Tables[0].Rows[0]["Stock_Manufa_Ghee"].ToString();
                        txtStock_Manufa_Cattlefeed.Text = ds.Tables[0].Rows[0]["Stock_Manufa_Cattlefeed"].ToString();
                        txtStock_Rec_WB.Text = ds.Tables[0].Rows[0]["Stock_Rec_WB"].ToString();
                        txtStock_Rec_Ghee.Text = ds.Tables[0].Rows[0]["Stock_Rec_Ghee"].ToString();
                        txtStock_Rec_Cattlefeed.Text = ds.Tables[0].Rows[0]["Stock_Rec_Cattlefeed"].ToString();
                        txtStock_Sold_WB.Text = ds.Tables[0].Rows[0]["Stock_Sold_WB"].ToString();
                        txtStock_Sold_Ghee.Text = ds.Tables[0].Rows[0]["Stock_Sold_Ghee"].ToString();
                        txtStock_Sold_Cattlefeed.Text = ds.Tables[0].Rows[0]["Stock_Sold_Cattlefeed"].ToString();
                        txtStock_Dispatch_DP_WB.Text = ds.Tables[0].Rows[0]["Stock_Dispatch_DP_WB"].ToString();
                        txtStock_Dispatch_DP_Ghee.Text = ds.Tables[0].Rows[0]["Stock_Dispatch_DP_Ghee"].ToString();
                        txtStock_Dispatch_DP_Cattlefeed.Text = ds.Tables[0].Rows[0]["Stock_Dispatch_DP_Cattlefeed"].ToString();
                        txtStock_CB_WB.Text = ds.Tables[0].Rows[0]["Stock_CB_WB"].ToString();
                        txtStock_CB_Ghee.Text = ds.Tables[0].Rows[0]["Stock_CB_Ghee"].ToString();
                        txtStock_CB_Cattlefeed.Text = ds.Tables[0].Rows[0]["Stock_CB_Cattlefeed"].ToString();
                        txtDCS_CCTE_Headload_dcsnos_Unit.Text = ds.Tables[0].Rows[0]["DCS_CCTE_Headload_dcsnos_Unit"].ToString();
                        txtDCS_CCTE_Headload_dcsnos_AMT.Text = ds.Tables[0].Rows[0]["DCS_CCTE_Headload_dcsnos_AMT"].ToString();
                        txtDCS_CCTE_Vehicle_nos_Unit.Text = ds.Tables[0].Rows[0]["DCS_CCTE_Vehicle_nos_Unit"].ToString();
                        txtDCS_CCTE_Vehicle_nos_AMT.Text = ds.Tables[0].Rows[0]["DCS_CCTE_Vehicle_nos_AMT"].ToString();
                        txtCattle_FT_Vehicle_nos_Unit.Text = ds.Tables[0].Rows[0]["Cattle_FT_Vehicle_nos_Unit"].ToString();
                        txtCattle_FT_Vehicle_nos_AMT.Text = ds.Tables[0].Rows[0]["Cattle_FT_Vehicle_nos_AMT"].ToString();
                        txtCattle_FT_Loading_nos_Unit.Text = ds.Tables[0].Rows[0]["Cattle_FT_Loading_nos_Unit"].ToString();
                        txtCattle_FT_Loading_nos_AMT.Text = ds.Tables[0].Rows[0]["Cattle_FT_Loading_nos_AMT"].ToString();
                        txtCattle_FT_UnLoading_nos_Unit.Text = ds.Tables[0].Rows[0]["Cattle_FT_UnLoading_nos_Unit"].ToString();
                        txtCattle_FT_UnLoading_nos_AMT.Text = ds.Tables[0].Rows[0]["Cattle_FT_UnLoading_nos_AMT"].ToString();
                        txtExpend_Electricity_Unit.Text = ds.Tables[0].Rows[0]["Expend_Electricity_Unit"].ToString();
                        txtExpend_Electricity_AMT.Text = ds.Tables[0].Rows[0]["Expend_Electricity_AMT"].ToString();
                        txtExpend_Deisel_Unit.Text = ds.Tables[0].Rows[0]["Expend_Deisel_Unit"].ToString();
                        txtExpend_Deisel_AMT.Text = ds.Tables[0].Rows[0]["Expend_Deisel_AMT"].ToString();
                        txtExpend_Che_acid_Unit.Text = ds.Tables[0].Rows[0]["Expend_Che_acid_Unit"].ToString();
                        txtExpend_Che_acid_AMT.Text = ds.Tables[0].Rows[0]["Expend_Che_acid_AMT"].ToString();
                        txtExpend_Che_Alcohol_Unit.Text = ds.Tables[0].Rows[0]["Expend_Che_Alcohol_Unit"].ToString();
                        txtExpend_Che_Alcohol_AMT.Text = ds.Tables[0].Rows[0]["Expend_Che_Alcohol_AMT"].ToString();
                        txtExpend_Detgt_SS_Unit.Text = ds.Tables[0].Rows[0]["Expend_Detgt_SS_Unit"].ToString();
                        txtExpend_Detgt_SS_AMT.Text = ds.Tables[0].Rows[0]["Expend_Detgt_SS_AMT"].ToString();
                        txtExpend_Detgt_CS_Unit.Text = ds.Tables[0].Rows[0]["Expend_Detgt_CS_Unit"].ToString();
                        txtExpend_Detgt_CS_AMT.Text = ds.Tables[0].Rows[0]["Expend_Detgt_CS_AMT"].ToString();
                        txtExpend_Detgt_WS_Unit.Text = ds.Tables[0].Rows[0]["Expend_Detgt_WS_Unit"].ToString();
                        txtExpend_Detgt_WS_AMT.Text = ds.Tables[0].Rows[0]["Expend_Detgt_WS_AMT"].ToString();
                        txtExpend_CLabour_Unit.Text = ds.Tables[0].Rows[0]["Expend_CLabour_Unit"].ToString();
                        txtExpend_CLabour_AMT.Text = ds.Tables[0].Rows[0]["Expend_CLabour_AMT"].ToString();
                        txtExpend_Security_Unit.Text = ds.Tables[0].Rows[0]["Expend_Security_Unit"].ToString();
                        txtExpend_Security_AMT.Text = ds.Tables[0].Rows[0]["Expend_Security_AMT"].ToString();
                        txtExpend_Stationary_Unit.Text = ds.Tables[0].Rows[0]["Expend_Stationary_Unit"].ToString();
                        txtExpend_Stationary_AMT.Text = ds.Tables[0].Rows[0]["Expend_Stationary_AMT"].ToString();
                        // PDM_rowcount.ToString()
                        int Otherexpenditure_count= int.Parse(ds.Tables[0].Rows[0]["Other_Expend_Count"].ToString());
                        if (Otherexpenditure_count > 0)
                        {
                            DataTable dt = new DataTable();
                            dt = ds.Tables[1];
                            ViewState["Otherexpenditure"] = dt;
                            gvOtherexpenditure.DataSource = dt;
                            gvOtherexpenditure.DataBind();
                            btnSubmit.Visible = false;
                            btnupdate.Visible = true;
                            // txtnmUn_Skilled.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                        }

                    }
                }
            }

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
            //cal();
            lblMsg.Text = "";
            if (DDlMonth.SelectedIndex > 0)
            {
                int Otherexpenditure_rowcount = gvOtherexpenditure.Rows.Count;
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Chilling_center_Report_By_manager",
                    new string[] { "flag", "Office_ID","OB_M_Qty","OB_M_Fat","OB_M_SNF","Milk_pur_good_Qty","Milk_pur_good_Fat","Milk_pur_good_SNF","Milk_pur_Sour_Qty","Milk_pur_Sour_Fat","Milk_pur_Sour_SNF","Milk_pur_Curdle_Qty","Milk_pur_Curdle_Fat","Milk_pur_Curdle_SNF","SC_milk_for_prod_Qty","SC_milk_for_prod_Fat","SC_milk_for_prod_SNF","Milk_dispatch_dairy_Qty","Milk_dispatch_dairy_Fat","Milk_dispatch_dairy_SNF","CB_M_Qty","CB_M_Fat","CB_M_SNF","White_butter_SC_OB_Qty","White_butter_SC_OB_Fat","White_butter_SC_OB_SNF","White_butter_SC_WBM_Qty","White_butter_SC_WBM_Fat","White_butter_SC_WBM_SNF","White_butter_SC_CB_Qty","White_butter_SC_CB_Fat","White_butter_SC_CB_SNF","Milk_Hand_variation_Q_Qty","Milk_Hand_variation_Q_Fat","Milk_Hand_variation_Q_SNF","Milk_Hand_variation_Per_Qty","Milk_Hand_variation_Per_Fat","Milk_Hand_variation_Per_SNF","Product_mfg_variation_Q_Qty","Product_mfg_variation_Q_Fat","Product_mfg_variation_Q_SNF","Product_mfg_variation_Per_Qty","Product_mfg_variation_Per_Fat","Product_mfg_variation_Per_SNF","Tanker_milk_Rec_DP_Qty","Tanker_milk_Rec_DP_Fat","Tanker_milk_Rec_DP_SNF","Tanker_milk_variation_Q_Qty","Tanker_milk_variation_Q_Fat","Tanker_milk_variation_Q_SNF","Tanker_milk_variation_Per_Qty","Tanker_milk_variation_Per_Fat","Tanker_milk_variation_Per_SNF","Stock_OB_WB","Stock_OB_Ghee","Stock_OB_Cattlefeed","Stock_Manufa_WB","Stock_Manufa_Ghee","Stock_Manufa_Cattlefeed","Stock_Rec_WB","Stock_Rec_Ghee","Stock_Rec_Cattlefeed","Stock_Sold_WB","Stock_Sold_Ghee","Stock_Sold_Cattlefeed","Stock_Dispatch_DP_WB","Stock_Dispatch_DP_Ghee","Stock_Dispatch_DP_Cattlefeed","Stock_CB_WB","Stock_CB_Ghee","Stock_CB_Cattlefeed","DCS_CCTE_Headload_dcsnos_Unit","DCS_CCTE_Headload_dcsnos_AMT","DCS_CCTE_Vehicle_nos_Unit","DCS_CCTE_Vehicle_nos_AMT","Cattle_FT_Vehicle_nos_Unit","Cattle_FT_Vehicle_nos_AMT","Cattle_FT_Loading_nos_Unit","Cattle_FT_Loading_nos_AMT","Cattle_FT_UnLoading_nos_Unit","Cattle_FT_UnLoading_nos_AMT","Expend_Electricity_Unit","Expend_Electricity_AMT","Expend_Deisel_Unit","Expend_Deisel_AMT","Expend_Che_acid_Unit","Expend_Che_acid_AMT","Expend_Che_Alcohol_Unit","Expend_Che_Alcohol_AMT","Expend_Detgt_SS_Unit","Expend_Detgt_SS_AMT","Expend_Detgt_CS_Unit","Expend_Detgt_CS_AMT","Expend_Detgt_WS_Unit","Expend_Detgt_WS_AMT","Expend_CLabour_Unit","Expend_CLabour_AMT","Expend_Security_Unit","Expend_Security_AMT","Expend_Stationary_Unit","Expend_Stationary_AMT","Other_Expend_Count","Month",
                                     "Year", "CreatedBy" },
                      new string[] { "0", objdb.Office_ID(),txtOB_M_Qty.Text ,txtOB_M_Fat.Text ,txtOB_M_SNF.Text ,txtMilk_pur_good_Qty.Text ,txtMilk_pur_good_Fat.Text ,txtMilk_pur_good_SNF.Text ,txtMilk_pur_Sour_Qty.Text ,txtMilk_pur_Sour_Fat.Text ,txtMilk_pur_Sour_SNF.Text ,txtMilk_pur_Curdle_Qty.Text ,txtMilk_pur_Curdle_Fat.Text ,txtMilk_pur_Curdle_SNF.Text ,txtSC_milk_for_prod_Qty.Text ,txtSC_milk_for_prod_Fat.Text ,txtSC_milk_for_prod_SNF.Text ,txtMilk_dispatch_dairy_Qty.Text ,txtMilk_dispatch_dairy_Fat.Text ,txtMilk_dispatch_dairy_SNF.Text ,txtCB_M_Qty.Text ,txtCB_M_Fat.Text ,txtCB_M_SNF.Text ,txtWhite_butter_SC_OB_Qty.Text ,txtWhite_butter_SC_OB_Fat.Text ,txtWhite_butter_SC_OB_SNF.Text ,txtWhite_butter_SC_WBM_Qty.Text ,txtWhite_butter_SC_WBM_Fat.Text ,txtWhite_butter_SC_WBM_SNF.Text ,txtWhite_butter_SC_CB_Qty.Text ,txtWhite_butter_SC_CB_Fat.Text ,txtWhite_butter_SC_CB_SNF.Text ,txtMilk_Hand_variation_Q_Qty.Text ,txtMilk_Hand_variation_Q_Fat.Text ,txtMilk_Hand_variation_Q_SNF.Text ,txtMilk_Hand_variation_Per_Qty.Text ,txtMilk_Hand_variation_Per_Fat.Text ,txtMilk_Hand_variation_Per_SNF.Text ,txtProduct_mfg_variation_Q_Qty.Text ,txtProduct_mfg_variation_Q_Fat.Text ,txtProduct_mfg_variation_Q_SNF.Text ,txtProduct_mfg_variation_Per_Qty.Text ,txtProduct_mfg_variation_Per_Fat.Text ,txtProduct_mfg_variation_Per_SNF.Text ,txtTanker_milk_Rec_DP_Qty.Text ,txtTanker_milk_Rec_DP_Fat.Text ,txtTanker_milk_Rec_DP_SNF.Text ,txtTanker_milk_variation_Q_Qty.Text ,txtTanker_milk_variation_Q_Fat.Text ,txtTanker_milk_variation_Q_SNF.Text ,txtTanker_milk_variation_Per_Qty.Text ,txtTanker_milk_variation_Per_Fat.Text ,txtTanker_milk_variation_Per_SNF.Text ,txtStock_OB_WB.Text ,txtStock_OB_Ghee.Text ,txtStock_OB_Cattlefeed.Text ,txtStock_Manufa_WB.Text ,txtStock_Manufa_Ghee.Text ,txtStock_Manufa_Cattlefeed.Text ,txtStock_Rec_WB.Text ,txtStock_Rec_Ghee.Text ,txtStock_Rec_Cattlefeed.Text ,txtStock_Sold_WB.Text ,txtStock_Sold_Ghee.Text ,txtStock_Sold_Cattlefeed.Text ,txtStock_Dispatch_DP_WB.Text ,txtStock_Dispatch_DP_Ghee.Text ,txtStock_Dispatch_DP_Cattlefeed.Text ,txtStock_CB_WB.Text ,txtStock_CB_Ghee.Text ,txtStock_CB_Cattlefeed.Text ,txtDCS_CCTE_Headload_dcsnos_Unit.Text ,txtDCS_CCTE_Headload_dcsnos_AMT.Text ,txtDCS_CCTE_Vehicle_nos_Unit.Text ,txtDCS_CCTE_Vehicle_nos_AMT.Text ,txtCattle_FT_Vehicle_nos_Unit.Text ,txtCattle_FT_Vehicle_nos_AMT.Text ,txtCattle_FT_Loading_nos_Unit.Text ,txtCattle_FT_Loading_nos_AMT.Text ,txtCattle_FT_UnLoading_nos_Unit.Text ,txtCattle_FT_UnLoading_nos_AMT.Text ,txtExpend_Electricity_Unit.Text ,txtExpend_Electricity_AMT.Text ,txtExpend_Deisel_Unit.Text ,txtExpend_Deisel_AMT.Text ,txtExpend_Che_acid_Unit.Text ,txtExpend_Che_acid_AMT.Text ,txtExpend_Che_Alcohol_Unit.Text ,txtExpend_Che_Alcohol_AMT.Text ,txtExpend_Detgt_SS_Unit.Text ,txtExpend_Detgt_SS_AMT.Text ,txtExpend_Detgt_CS_Unit.Text ,txtExpend_Detgt_CS_AMT.Text ,txtExpend_Detgt_WS_Unit.Text ,txtExpend_Detgt_WS_AMT.Text ,txtExpend_CLabour_Unit.Text ,txtExpend_CLabour_AMT.Text ,txtExpend_Security_Unit.Text ,txtExpend_Security_AMT.Text ,txtExpend_Stationary_Unit.Text ,txtExpend_Stationary_AMT.Text ,Otherexpenditure_rowcount.ToString(),  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    lblMCCR_ID.Text = ds.Tables[0].Rows[0]["MCCR_ID"].ToString();



                    if (gvOtherexpenditure.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvOtherexpenditure.Rows)
                        {
                            TextBox txtExpendditure_name = (TextBox)row.FindControl("txtExpendditure_name");
                            TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                            //TextBox txtRate = (TextBox)row.FindControl("txtRate");
                            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");



                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_Chilling_center_Report_By_manager",
                          new string[] { "flag", "MCCR_ID", "Expendditure_name", "Unit",
                                     "Amount" },
                            new string[] { "1", lblMCCR_ID.Text, txtExpendditure_name.Text, txtUnit.Text
                           , txtAmount.Text}, "dataset");
                            count++;
                        }
                    }


                    //if (count > 0)
                    //{

                        lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Saved Successfully");
                        clear();
                        //getdata();

                    //}
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
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            //cal();
            lblMsg.Text = "";
            if (DDlMonth.SelectedIndex > 0)
            {
                int Otherexpenditure_rowcount = gvOtherexpenditure.Rows.Count;
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Chilling_center_Report_By_manager",
                    new string[] { "flag", "Office_ID","MCCR_ID","OB_M_Qty","OB_M_Fat","OB_M_SNF","Milk_pur_good_Qty","Milk_pur_good_Fat","Milk_pur_good_SNF","Milk_pur_Sour_Qty","Milk_pur_Sour_Fat","Milk_pur_Sour_SNF","Milk_pur_Curdle_Qty","Milk_pur_Curdle_Fat","Milk_pur_Curdle_SNF","SC_milk_for_prod_Qty","SC_milk_for_prod_Fat","SC_milk_for_prod_SNF","Milk_dispatch_dairy_Qty","Milk_dispatch_dairy_Fat","Milk_dispatch_dairy_SNF","CB_M_Qty","CB_M_Fat","CB_M_SNF","White_butter_SC_OB_Qty","White_butter_SC_OB_Fat","White_butter_SC_OB_SNF","White_butter_SC_WBM_Qty","White_butter_SC_WBM_Fat","White_butter_SC_WBM_SNF","White_butter_SC_CB_Qty","White_butter_SC_CB_Fat","White_butter_SC_CB_SNF","Milk_Hand_variation_Q_Qty","Milk_Hand_variation_Q_Fat","Milk_Hand_variation_Q_SNF","Milk_Hand_variation_Per_Qty","Milk_Hand_variation_Per_Fat","Milk_Hand_variation_Per_SNF","Product_mfg_variation_Q_Qty","Product_mfg_variation_Q_Fat","Product_mfg_variation_Q_SNF","Product_mfg_variation_Per_Qty","Product_mfg_variation_Per_Fat","Product_mfg_variation_Per_SNF","Tanker_milk_Rec_DP_Qty","Tanker_milk_Rec_DP_Fat","Tanker_milk_Rec_DP_SNF","Tanker_milk_variation_Q_Qty","Tanker_milk_variation_Q_Fat","Tanker_milk_variation_Q_SNF","Tanker_milk_variation_Per_Qty","Tanker_milk_variation_Per_Fat","Tanker_milk_variation_Per_SNF","Stock_OB_WB","Stock_OB_Ghee","Stock_OB_Cattlefeed","Stock_Manufa_WB","Stock_Manufa_Ghee","Stock_Manufa_Cattlefeed","Stock_Rec_WB","Stock_Rec_Ghee","Stock_Rec_Cattlefeed","Stock_Sold_WB","Stock_Sold_Ghee","Stock_Sold_Cattlefeed","Stock_Dispatch_DP_WB","Stock_Dispatch_DP_Ghee","Stock_Dispatch_DP_Cattlefeed","Stock_CB_WB","Stock_CB_Ghee","Stock_CB_Cattlefeed","DCS_CCTE_Headload_dcsnos_Unit","DCS_CCTE_Headload_dcsnos_AMT","DCS_CCTE_Vehicle_nos_Unit","DCS_CCTE_Vehicle_nos_AMT","Cattle_FT_Vehicle_nos_Unit","Cattle_FT_Vehicle_nos_AMT","Cattle_FT_Loading_nos_Unit","Cattle_FT_Loading_nos_AMT","Cattle_FT_UnLoading_nos_Unit","Cattle_FT_UnLoading_nos_AMT","Expend_Electricity_Unit","Expend_Electricity_AMT","Expend_Deisel_Unit","Expend_Deisel_AMT","Expend_Che_acid_Unit","Expend_Che_acid_AMT","Expend_Che_Alcohol_Unit","Expend_Che_Alcohol_AMT","Expend_Detgt_SS_Unit","Expend_Detgt_SS_AMT","Expend_Detgt_CS_Unit","Expend_Detgt_CS_AMT","Expend_Detgt_WS_Unit","Expend_Detgt_WS_AMT","Expend_CLabour_Unit","Expend_CLabour_AMT","Expend_Security_Unit","Expend_Security_AMT","Expend_Stationary_Unit","Expend_Stationary_AMT","Other_Expend_Count","Month",
                                     "Year", "CreatedBy" },
                      new string[] { "4", objdb.Office_ID(),lblMCCR_ID.Text,txtOB_M_Qty.Text ,txtOB_M_Fat.Text ,txtOB_M_SNF.Text ,txtMilk_pur_good_Qty.Text ,txtMilk_pur_good_Fat.Text ,txtMilk_pur_good_SNF.Text ,txtMilk_pur_Sour_Qty.Text ,txtMilk_pur_Sour_Fat.Text ,txtMilk_pur_Sour_SNF.Text ,txtMilk_pur_Curdle_Qty.Text ,txtMilk_pur_Curdle_Fat.Text ,txtMilk_pur_Curdle_SNF.Text ,txtSC_milk_for_prod_Qty.Text ,txtSC_milk_for_prod_Fat.Text ,txtSC_milk_for_prod_SNF.Text ,txtMilk_dispatch_dairy_Qty.Text ,txtMilk_dispatch_dairy_Fat.Text ,txtMilk_dispatch_dairy_SNF.Text ,txtCB_M_Qty.Text ,txtCB_M_Fat.Text ,txtCB_M_SNF.Text ,txtWhite_butter_SC_OB_Qty.Text ,txtWhite_butter_SC_OB_Fat.Text ,txtWhite_butter_SC_OB_SNF.Text ,txtWhite_butter_SC_WBM_Qty.Text ,txtWhite_butter_SC_WBM_Fat.Text ,txtWhite_butter_SC_WBM_SNF.Text ,txtWhite_butter_SC_CB_Qty.Text ,txtWhite_butter_SC_CB_Fat.Text ,txtWhite_butter_SC_CB_SNF.Text ,txtMilk_Hand_variation_Q_Qty.Text ,txtMilk_Hand_variation_Q_Fat.Text ,txtMilk_Hand_variation_Q_SNF.Text ,txtMilk_Hand_variation_Per_Qty.Text ,txtMilk_Hand_variation_Per_Fat.Text ,txtMilk_Hand_variation_Per_SNF.Text ,txtProduct_mfg_variation_Q_Qty.Text ,txtProduct_mfg_variation_Q_Fat.Text ,txtProduct_mfg_variation_Q_SNF.Text ,txtProduct_mfg_variation_Per_Qty.Text ,txtProduct_mfg_variation_Per_Fat.Text ,txtProduct_mfg_variation_Per_SNF.Text ,txtTanker_milk_Rec_DP_Qty.Text ,txtTanker_milk_Rec_DP_Fat.Text ,txtTanker_milk_Rec_DP_SNF.Text ,txtTanker_milk_variation_Q_Qty.Text ,txtTanker_milk_variation_Q_Fat.Text ,txtTanker_milk_variation_Q_SNF.Text ,txtTanker_milk_variation_Per_Qty.Text ,txtTanker_milk_variation_Per_Fat.Text ,txtTanker_milk_variation_Per_SNF.Text ,txtStock_OB_WB.Text ,txtStock_OB_Ghee.Text ,txtStock_OB_Cattlefeed.Text ,txtStock_Manufa_WB.Text ,txtStock_Manufa_Ghee.Text ,txtStock_Manufa_Cattlefeed.Text ,txtStock_Rec_WB.Text ,txtStock_Rec_Ghee.Text ,txtStock_Rec_Cattlefeed.Text ,txtStock_Sold_WB.Text ,txtStock_Sold_Ghee.Text ,txtStock_Sold_Cattlefeed.Text ,txtStock_Dispatch_DP_WB.Text ,txtStock_Dispatch_DP_Ghee.Text ,txtStock_Dispatch_DP_Cattlefeed.Text ,txtStock_CB_WB.Text ,txtStock_CB_Ghee.Text ,txtStock_CB_Cattlefeed.Text ,txtDCS_CCTE_Headload_dcsnos_Unit.Text ,txtDCS_CCTE_Headload_dcsnos_AMT.Text ,txtDCS_CCTE_Vehicle_nos_Unit.Text ,txtDCS_CCTE_Vehicle_nos_AMT.Text ,txtCattle_FT_Vehicle_nos_Unit.Text ,txtCattle_FT_Vehicle_nos_AMT.Text ,txtCattle_FT_Loading_nos_Unit.Text ,txtCattle_FT_Loading_nos_AMT.Text ,txtCattle_FT_UnLoading_nos_Unit.Text ,txtCattle_FT_UnLoading_nos_AMT.Text ,txtExpend_Electricity_Unit.Text ,txtExpend_Electricity_AMT.Text ,txtExpend_Deisel_Unit.Text ,txtExpend_Deisel_AMT.Text ,txtExpend_Che_acid_Unit.Text ,txtExpend_Che_acid_AMT.Text ,txtExpend_Che_Alcohol_Unit.Text ,txtExpend_Che_Alcohol_AMT.Text ,txtExpend_Detgt_SS_Unit.Text ,txtExpend_Detgt_SS_AMT.Text ,txtExpend_Detgt_CS_Unit.Text ,txtExpend_Detgt_CS_AMT.Text ,txtExpend_Detgt_WS_Unit.Text ,txtExpend_Detgt_WS_AMT.Text ,txtExpend_CLabour_Unit.Text ,txtExpend_CLabour_AMT.Text ,txtExpend_Security_Unit.Text ,txtExpend_Security_AMT.Text ,txtExpend_Stationary_Unit.Text ,txtExpend_Stationary_AMT.Text ,Otherexpenditure_rowcount.ToString(),  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    //lblMCCR_ID.Text = ds.Tables[0].Rows[0]["MCCR_ID"].ToString();

                    DataSet dsdetail_delete = objdb.ByProcedure("usp_insertupdate_monthly_Chilling_center_Report_By_manager",
                     new string[] { "flag", "MCCR_ID" },
                       new string[] { "5", lblMCCR_ID.Text }, "dataset");

                    if (gvOtherexpenditure.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvOtherexpenditure.Rows)
                        {
                            TextBox txtExpendditure_name = (TextBox)row.FindControl("txtExpendditure_name");
                            TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                            //TextBox txtRate = (TextBox)row.FindControl("txtRate");
                            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");



                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_Chilling_center_Report_By_manager",
                          new string[] { "flag", "MCCR_ID", "Expendditure_name", "Unit",
                                     "Amount" },
                            new string[] { "1", lblMCCR_ID.Text, txtExpendditure_name.Text, txtUnit.Text
                           , txtAmount.Text}, "dataset");
                            count++;
                        }
                    }

                    //if (count > 0)
                    //{

                        lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Updated Successfully");
                        clear();
                        //getdata();

                   // }
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
    public void clear()
    {
        try
        {
            lblMCCR_ID.Text = "0";
            txtOB_M_Qty.Text = "0";
            txtOB_M_Fat.Text = "0";
            txtOB_M_SNF.Text = "0";
            txtMilk_pur_good_Qty.Text = "0";
            txtMilk_pur_good_Fat.Text = "0";
            txtMilk_pur_good_SNF.Text = "0";
            txtMilk_pur_Sour_Qty.Text = "0";
            txtMilk_pur_Sour_Fat.Text = "0";
            txtMilk_pur_Sour_SNF.Text = "0";
            txtMilk_pur_Curdle_Qty.Text = "0";
            txtMilk_pur_Curdle_Fat.Text = "0";
            txtMilk_pur_Curdle_SNF.Text = "0";
            txtSC_milk_for_prod_Qty.Text = "0";
            txtSC_milk_for_prod_Fat.Text = "0";
            txtSC_milk_for_prod_SNF.Text = "0";
            txtMilk_dispatch_dairy_Qty.Text = "0";
            txtMilk_dispatch_dairy_Fat.Text = "0";
            txtMilk_dispatch_dairy_SNF.Text = "0";
            txtCB_M_Qty.Text = "0";
            txtCB_M_Fat.Text = "0";
            txtCB_M_SNF.Text = "0";
            txtWhite_butter_SC_OB_Qty.Text = "0";
            txtWhite_butter_SC_OB_Fat.Text = "0";
            txtWhite_butter_SC_OB_SNF.Text = "0";
            txtWhite_butter_SC_WBM_Qty.Text = "0";
            txtWhite_butter_SC_WBM_Fat.Text = "0";
            txtWhite_butter_SC_WBM_SNF.Text = "0";
            txtWhite_butter_SC_CB_Qty.Text = "0";
            txtWhite_butter_SC_CB_Fat.Text = "0";
            txtWhite_butter_SC_CB_SNF.Text = "0";
            txtMilk_Hand_variation_Q_Qty.Text = "0";
            txtMilk_Hand_variation_Q_Fat.Text = "0";
            txtMilk_Hand_variation_Q_SNF.Text = "0";
            txtMilk_Hand_variation_Per_Qty.Text = "0";
            txtMilk_Hand_variation_Per_Fat.Text = "0";
            txtMilk_Hand_variation_Per_SNF.Text = "0";
            txtProduct_mfg_variation_Q_Qty.Text = "0";
            txtProduct_mfg_variation_Q_Fat.Text = "0";
            txtProduct_mfg_variation_Q_SNF.Text = "0";
            txtProduct_mfg_variation_Per_Qty.Text = "0";
            txtProduct_mfg_variation_Per_Fat.Text = "0";
            txtProduct_mfg_variation_Per_SNF.Text = "0";
            txtTanker_milk_Rec_DP_Qty.Text = "0";
            txtTanker_milk_Rec_DP_Fat.Text = "0";
            txtTanker_milk_Rec_DP_SNF.Text = "0";
            txtTanker_milk_variation_Q_Qty.Text = "0";
            txtTanker_milk_variation_Q_Fat.Text = "0";
            txtTanker_milk_variation_Q_SNF.Text = "0";
            txtTanker_milk_variation_Per_Qty.Text = "0";
            txtTanker_milk_variation_Per_Fat.Text = "0";
            txtTanker_milk_variation_Per_SNF.Text = "0";
            txtStock_OB_WB.Text = "0";
            txtStock_OB_Ghee.Text = "0";
            txtStock_OB_Cattlefeed.Text = "0";
            txtStock_Manufa_WB.Text = "0";
            txtStock_Manufa_Ghee.Text = "0";
            txtStock_Manufa_Cattlefeed.Text = "0";
            txtStock_Rec_WB.Text = "0";
            txtStock_Rec_Ghee.Text = "0";
            txtStock_Rec_Cattlefeed.Text = "0";
            txtStock_Sold_WB.Text = "0";
            txtStock_Sold_Ghee.Text = "0";
            txtStock_Sold_Cattlefeed.Text = "0";
            txtStock_Dispatch_DP_WB.Text = "0";
            txtStock_Dispatch_DP_Ghee.Text = "0";
            txtStock_Dispatch_DP_Cattlefeed.Text = "0";
            txtStock_CB_WB.Text = "0";
            txtStock_CB_Ghee.Text = "0";
            txtStock_CB_Cattlefeed.Text = "0";
            txtDCS_CCTE_Headload_dcsnos_Unit.Text = "0";
            txtDCS_CCTE_Headload_dcsnos_AMT.Text = "0";
            txtDCS_CCTE_Vehicle_nos_Unit.Text = "0";
            txtDCS_CCTE_Vehicle_nos_AMT.Text = "0";
            txtCattle_FT_Vehicle_nos_Unit.Text = "0";
            txtCattle_FT_Vehicle_nos_AMT.Text = "0";
            txtCattle_FT_Loading_nos_Unit.Text = "0";
            txtCattle_FT_Loading_nos_AMT.Text = "0";
            txtCattle_FT_UnLoading_nos_Unit.Text = "0";
            txtCattle_FT_UnLoading_nos_AMT.Text = "0";
            txtExpend_Electricity_Unit.Text = "0";
            txtExpend_Electricity_AMT.Text = "0";
            txtExpend_Deisel_Unit.Text = "0";
            txtExpend_Deisel_AMT.Text = "0";
            txtExpend_Che_acid_Unit.Text = "0";
            txtExpend_Che_acid_AMT.Text = "0";
            txtExpend_Che_Alcohol_Unit.Text = "0";
            txtExpend_Che_Alcohol_AMT.Text = "0";
            txtExpend_Detgt_SS_Unit.Text = "0";
            txtExpend_Detgt_SS_AMT.Text = "0";
            txtExpend_Detgt_CS_Unit.Text = "0";
            txtExpend_Detgt_CS_AMT.Text = "0";
            txtExpend_Detgt_WS_Unit.Text = "0";
            txtExpend_Detgt_WS_AMT.Text = "0";
            txtExpend_CLabour_Unit.Text = "0";
            txtExpend_CLabour_AMT.Text = "0";
            txtExpend_Security_Unit.Text = "0";
            txtExpend_Security_AMT.Text = "0";
            txtExpend_Stationary_Unit.Text = "0";
            txtExpend_Stationary_AMT.Text = "0";


            DDlMonth.SelectedIndex = 0;
            ViewState["Otherexpenditure"] = "";

            getyear();
            gvOtherexpenditure.DataSource = null;
            gvOtherexpenditure.DataBind();
            ViewState["Otherexpenditure"] = "";
            //BindGridviewOtherexpenditure();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void cal()
    {
        try
        {

            string num3 = txtSC_milk_for_prod_Qty.Text;
            string num4 = txtMilk_dispatch_dairy_Qty.Text;
            string num5 = txtCB_M_Qty.Text;
            string num1 = txtOB_M_Qty.Text;
            string num21 = txtMilk_pur_good_Qty.Text;
            string num22 = txtMilk_pur_Sour_Qty.Text;
            string num23 = txtMilk_pur_Curdle_Qty.Text;

            string num61 = txtWhite_butter_SC_OB_Qty.Text;
            string num62 = txtWhite_butter_SC_WBM_Qty.Text;
            string num63 = txtWhite_butter_SC_CB_Qty.Text;

            
            var num9 = txtTanker_milk_Rec_DP_Qty.Text;


            txtMilk_Hand_variation_Q_Qty.Text =Convert.ToString( Math.Round((double.Parse(num3.ToString()) + double.Parse(num4.ToString()) + double.Parse(num5.ToString())) - (double.Parse(num1.ToString()) + double.Parse(num21.ToString()) + double.Parse(num22.ToString()) + double.Parse(num23.ToString())),2));
            txtMilk_Hand_variation_Per_Qty.Text = Convert.ToString(Math.Round((double.Parse(txtMilk_Hand_variation_Q_Qty.Text) / ((double.Parse(num1.ToString()) + double.Parse(num21.ToString()) + double.Parse(num22.ToString()) + double.Parse(num23.ToString())) * 100)), 2));
            txtProduct_mfg_variation_Q_Qty.Text = Convert.ToString(Math.Round(((double.Parse(num62.ToString()) + double.Parse(num63.ToString())) - (double.Parse(num3.ToString()) + double.Parse(num61.ToString()))), 2));
            txtProduct_mfg_variation_Per_Qty.Text = Convert.ToString(Math.Round((double.Parse(txtProduct_mfg_variation_Q_Qty.Text) / ((double.Parse(num3.ToString()) + double.Parse(num61.ToString())) * 100)), 2));
            txtTanker_milk_variation_Q_Qty.Text = Convert.ToString(Math.Round((double.Parse(num9.ToString()) - double.Parse(num4.ToString())), 2));
            txtTanker_milk_variation_Per_Qty.Text = Convert.ToString(Math.Round((double.Parse(txtTanker_milk_variation_Q_Qty.Text) / (double.Parse(num4.ToString()) * 100)), 2));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


  
}