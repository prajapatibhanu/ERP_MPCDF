using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformationSystem_MPR_Of_TS_Diesel_Pump : System.Web.UI.Page
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
                    txtOB_AMT.Text = "0";
                    txt_ITOV_AMT.Text = "0";
                    txt_ITO_AMT.Text = "0";
                    txtCB_AMT.Text = "0";
                    txtOB_AMT.Attributes.Add("readonly", "readonly");
                    txt_ITOV_AMT.Attributes.Add("readonly", "readonly");
                    txt_ITO_AMT.Attributes.Add("readonly", "readonly");
                    txtCB_AMT.Attributes.Add("readonly", "readonly");



                    ViewState["PurchaseDuringMonth"] = "";
                   
                    getyear();
                   // getdata();
                    BindGridviewPurchaseDuringMonth();


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
    public void getdata()
    {
        try
        {

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_diesel_Pump",
               new string[] { "flag", "Office_ID", "CreatedBy" },
                 new string[] { "2", objdb.Office_ID(), Session["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDPdetail.DataSource = ds;
                    gvDPdetail.DataBind();
                    gvDPdetail.Visible = true;
                }
                else
                {
                    gvDPdetail.DataSource = null;
                    gvDPdetail.DataBind();
                }
            }
            else
            {
                gvDPdetail.DataSource = null;
                gvDPdetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BindGridviewPurchaseDuringMonth()
    {
        try
        {
            if (ViewState["PurchaseDuringMonth"].ToString() == "")
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Sno", typeof(int));
                dt.Columns.Add("Purchase_date", typeof(string));
                dt.Columns.Add("Quantity", typeof(decimal));
                dt.Columns.Add("Rate", typeof(decimal));
                dt.Columns.Add("Amount", typeof(decimal));
               

                DataRow dr = dt.NewRow();
                dr["Sno"] = 1;
                dr["Purchase_date"] = string.Empty;
                dr["Quantity"] = 0;
                dr["Rate"] = 0;
                dr["Amount"] = 0;
                
                

                dt.Rows.Add(dr);
                ViewState["PurchaseDuringMonth"] = dt;
                gvPurchaseDuringMonth.DataSource = dt;
                gvPurchaseDuringMonth.DataBind();
            }
            else
            {
                DataTable dt = (DataTable)ViewState["PurchaseDuringMonth"];
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
                ViewState["PurchaseDuringMonth"] = dt;
                gvPurchaseDuringMonth.DataSource = dt;
                gvPurchaseDuringMonth.DataBind();
            }




        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_diesel_Pump",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year" },
                 new string[] { "6", objdb.Office_ID(), Session["Emp_ID"].ToString(),DDlMonth2.SelectedValue
                           , ddlyear2.SelectedItem.Text }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDPdetail.DataSource = ds;
                    gvDPdetail.DataBind();
                    gvDPdetail.Visible = true;
                    gvDPdetail.Rows[gvDPdetail.Rows.Count - 1].Focus();
                }
                else
                {
                    gvDPdetail.DataSource = null;
                    gvDPdetail.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                    lblMsg.Focus();
                }
            }
            else
            {
                gvDPdetail.DataSource = null;
                gvDPdetail.DataBind();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                lblMsg.Focus();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAddrow_Click(object sender, EventArgs e)
    {
        try
        {
            AddNewRow();
           // gvPurchaseDuringMonth.Focus();
            gvPurchaseDuringMonth.Rows[gvPurchaseDuringMonth.Rows.Count - 1].Focus();

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



            int rowIndex = 0;
            DataTable dt = (DataTable)ViewState["PurchaseDuringMonth"];
            int j = 0;
            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {
               
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox txtPurchase_date = (TextBox)gvPurchaseDuringMonth.Rows[rowIndex].Cells[1].FindControl("txtPurchase_date");
                    TextBox txtQuantity = (TextBox)gvPurchaseDuringMonth.Rows[rowIndex].Cells[2].FindControl("txtQuantity");
                    TextBox txtRate = (TextBox)gvPurchaseDuringMonth.Rows[rowIndex].Cells[3].FindControl("txtRate");
                    TextBox txtAmount = (TextBox)gvPurchaseDuringMonth.Rows[rowIndex].Cells[4].FindControl("txtAmount");

                    if (txtQuantity.Text == "")
                    {
                        txtQuantity.Text = "0";
                    }
                    if (txtRate.Text == "")
                    {
                        txtRate.Text = "0";
                    }
                    if (txtAmount.Text == "")
                    {
                        txtAmount.Text = "0";
                        txtAmount.Attributes.Add("readonly", "readonly");
                    }
                   
                    //  TextBox txtDateOfReturn = (TextBox)gvtanker.Rows[rowIndex].Cells[8].FindControl("txtDateOfReturn");

                    dr = dt.NewRow();

                    dt.Rows[i - 1]["Sno"] = i + 1;
                    dt.Rows[i - 1]["Purchase_date"] = txtPurchase_date.Text;
                    dt.Rows[i - 1]["Quantity"] = txtQuantity.Text;
                    dt.Rows[i - 1]["Rate"] = txtRate.Text;
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
            dr["Purchase_date"] = string.Empty;
            dr["Quantity"] = 0;
            dr["Rate"] = 0;
            dr["Amount"] = 0;

            dt.Rows.Add(dr);
            ViewState["PurchaseDuringMonth"] = dt;
            gvPurchaseDuringMonth.DataSource = dt;
            gvPurchaseDuringMonth.DataBind();

        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvPurchaseDuringMonth_DataBound(object sender, EventArgs e)
    {

    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (DDlMonth.SelectedIndex > 0)

            {
                int PDM_rowcount = gvPurchaseDuringMonth.Rows.Count;
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_diesel_Pump",
                    new string[] { "flag", "Office_ID", "Op_bal_qty" , "Op_bal_Rate" ,"Op_bal_AMT" ,"IOV_qty ","IOV_Rate" ,"IOV_AMT" ,"ITO_qty" ,"ITO_Rate" ,"ITO_AMT" ,"CL_bal_qty" ,"CL_bal_Rate" ,"CL_bal_AMT" ,"PDM_count", "Month",
                                     "Year", "CreatedBy" },
                      new string[] { "0", objdb.Office_ID(),txtOB_Quantity.Text,txtOB_rate.Text,txtOB_AMT.Text,txt_ITOV_Quantity.Text,txt_ITOV_Rate.Text,txt_ITOV_AMT.Text,txt_ITO_Quantity.Text,txt_ITO_Rate.Text,txt_ITO_AMT.Text,txtCB_Quantity.Text,txtCB_Rate.Text,txtCB_AMT.Text,PDM_rowcount.ToString(),  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    lblDP_id.Text = ds.Tables[0].Rows[0]["MPR_DP_Id"].ToString();




                    foreach (GridViewRow row in gvPurchaseDuringMonth.Rows)
                        {
                            TextBox txtPurchase_date = (TextBox)row.FindControl("txtPurchase_date");
                            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                            TextBox txtRate = (TextBox)row.FindControl("txtRate");
                            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
                            //TextBox txtTyre = (TextBox)row.FindControl("txtTyre");
                            //TextBox txtBattery = (TextBox)row.FindControl("txtBattery");
                            //TextBox txtMajorRepairs = (TextBox)row.FindControl("txtMajorRepairs");
                            //TextBox txtVehicleNo=(TextBox)row.FindControl("txtVehicleNo");

                          //  DateTime Purchase_date = txtPurchase_date.Text.ToString("DD-MM-YYYY");
                              // Purchase_date  DateTime.ParseExact(Purchase_date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                            DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_diesel_Pump",
                          new string[] { "flag", "MPR_DP_Id", "PDM_Date", "Quantity", "Rate",
                                     "Amount" },
                            new string[] { "1", lblDP_id.Text, txtPurchase_date.Text, txtQuantity.Text, txtRate.Text
                           , txtAmount.Text}, "dataset");
                            count++;
                        }
                   
                   
                    if (count > 0)
                    {

                        lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Saved Successfully");
                        clear();
                        //getdata();
                        
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
    protected void gvPurchaseDuringMonth_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ViewState["PurchaseDuringMonth"] != null)
            {
                DataTable dt = (DataTable)ViewState["PurchaseDuringMonth"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["PurchaseDuringMonth"] = dt;
                    gvPurchaseDuringMonth.DataSource = dt;
                    gvPurchaseDuringMonth.DataBind();

                    for (int i = 0; i < gvPurchaseDuringMonth.Rows.Count - 1; i++)
                    {
                        gvPurchaseDuringMonth.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }


                }
            }
            //gvPurchaseDuringMonth.Focus();
            gvPurchaseDuringMonth.Rows[gvPurchaseDuringMonth.Rows.Count - 1].Focus();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvDPdetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "select")
            {
                int MPR_DP_Id = Convert.ToInt32(e.CommandArgument.ToString());
                lblDP_id.Text = e.CommandArgument.ToString();
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_diesel_Pump",
              new string[] { "flag", "Office_ID", "CreatedBy", "MPR_DP_Id" },
                new string[] { "3", objdb.Office_ID(), Session["Emp_ID"].ToString(), MPR_DP_Id.ToString() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DDlMonth.SelectedValue = ds.Tables[0].Rows[0]["Month"].ToString();
                        getyear();
                        ddlyear.SelectedValue = ds.Tables[0].Rows[0]["Year"].ToString();
                        txtOB_Quantity.Text = ds.Tables[0].Rows[0]["Op_bal_qty"].ToString();
                        txtOB_rate.Text = ds.Tables[0].Rows[0]["Op_bal_Rate"].ToString();
                        txtOB_AMT.Text = ds.Tables[0].Rows[0]["Op_bal_AMT"].ToString();
                        txt_ITOV_Quantity.Text = ds.Tables[0].Rows[0]["IOV_qty"].ToString();
                        txt_ITOV_Rate.Text = ds.Tables[0].Rows[0]["IOV_Rate"].ToString();
                        txt_ITOV_AMT.Text = ds.Tables[0].Rows[0]["IOV_AMT"].ToString();
                        txt_ITO_Quantity.Text = ds.Tables[0].Rows[0]["ITO_qty"].ToString();
                        txt_ITO_Rate.Text = ds.Tables[0].Rows[0]["ITO_Rate"].ToString();
                        txt_ITO_AMT.Text = ds.Tables[0].Rows[0]["ITO_AMT"].ToString();
                        txtCB_Quantity.Text = ds.Tables[0].Rows[0]["CL_bal_qty"].ToString();
                        txtCB_Rate.Text = ds.Tables[0].Rows[0]["CL_bal_Rate"].ToString();
                        txtCB_AMT.Text = ds.Tables[0].Rows[0]["CL_bal_AMT"].ToString();
                       // PDM_rowcount.ToString()

                       
                        DataTable dt = new DataTable();
                        dt = ds.Tables[1]; 
                        ViewState["PurchaseDuringMonth"] = dt;
                        gvPurchaseDuringMonth.DataSource = dt;
                        gvPurchaseDuringMonth.DataBind();
                        btnSubmit.Visible = false;
                        btnupdate.Visible = true;
                        // txtnmUn_Skilled.Text = ds.Tables[0].Rows[0]["Year"].ToString();

                    }
                }
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
            lblMsg.Text = "";
            if (DDlMonth.SelectedIndex > 0)
            {
                int PDM_rowcount = gvPurchaseDuringMonth.Rows.Count;
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_diesel_Pump",
                    new string[] { "flag","MPR_DP_Id", "Office_ID", "Op_bal_qty" , "Op_bal_Rate" ,"Op_bal_AMT" ,"IOV_qty ","IOV_Rate" ,"IOV_AMT" ,"ITO_qty" ,"ITO_Rate" ,"ITO_AMT" ,"CL_bal_qty" ,"CL_bal_Rate" ,"CL_bal_AMT" ,"PDM_count", "Month",
                                     "Year", "CreatedBy" },
                      new string[] { "4",lblDP_id.Text, objdb.Office_ID(),txtOB_Quantity.Text,txtOB_rate.Text,txtOB_AMT.Text,txt_ITOV_Quantity.Text,txt_ITOV_Rate.Text,txt_ITOV_AMT.Text,txt_ITO_Quantity.Text,txt_ITO_Rate.Text,txt_ITO_AMT.Text,txtCB_Quantity.Text,txtCB_Rate.Text,txtCB_AMT.Text,PDM_rowcount.ToString(),  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    //lblDP_id.Text = ds.Tables[0].Rows[0]["MPR_DP_Id"].ToString();


                    DataSet dsdetail_delete = objdb.ByProcedure("usp_insertupdate_monthly_diesel_Pump",
                     new string[] { "flag", "MPR_DP_Id" },
                       new string[] { "5", lblDP_id.Text }, "dataset");

                    foreach (GridViewRow row in gvPurchaseDuringMonth.Rows)
                    {
                        TextBox txtPurchase_date = (TextBox)row.FindControl("txtPurchase_date");
                        TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                        TextBox txtRate = (TextBox)row.FindControl("txtRate");
                        TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
                        //TextBox txtTyre = (TextBox)row.FindControl("txtTyre");
                        //TextBox txtBattery = (TextBox)row.FindControl("txtBattery");
                        //TextBox txtMajorRepairs = (TextBox)row.FindControl("txtMajorRepairs");
                        //TextBox txtVehicleNo=(TextBox)row.FindControl("txtVehicleNo");

                        //  DateTime Purchase_date = txtPurchase_date.Text.ToString("DD-MM-YYYY");
                        // Purchase_date  DateTime.ParseExact(Purchase_date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                        DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_diesel_Pump",
                      new string[] { "flag", "MPR_DP_Id", "PDM_Date", "Quantity", "Rate",
                                     "Amount" },
                        new string[] { "1", lblDP_id.Text, txtPurchase_date.Text, txtQuantity.Text, txtRate.Text
                           , txtAmount.Text}, "dataset");
                        count++;
                    }


                    if (count > 0)
                    {

                        lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Updated Successfully");
                        clear();
                        //getdata();

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
    public void clear()
    {
        try
        {
            lblDP_id.Text = "0";
            txtOB_Quantity.Text = "0";
            txtOB_rate.Text = "0";
            txtOB_AMT.Text = "0";
            txt_ITOV_Quantity.Text = "0";
            txt_ITOV_Rate.Text = "0";
            txt_ITOV_AMT.Text = "0";
            txt_ITO_Quantity.Text = "0";
            txt_ITO_Rate.Text = "0";
            txt_ITO_AMT.Text = "0";
            
            txtCB_Quantity.Text = "0";
            txtCB_Rate.Text = "0";
           
            txtCB_AMT.Text = "0";
         
            DDlMonth.SelectedIndex = 0;
            ViewState["PurchaseDuringMonth"] = "";

            getyear();
            BindGridviewPurchaseDuringMonth();

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
            DDlMonth2_SelectedIndexChanged( sender,  e);
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}