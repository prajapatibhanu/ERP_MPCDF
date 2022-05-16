using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformationSystem_DCS_master_information : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
               
                if (!IsPostBack)
                {
                    ViewState["AvailableElectronicFacility"] = "";

                    getyear();
                    GetCCDetails();
                    BindGridviewAvailableElectronicFacility();

                    int Employee_id = int.Parse(Session["Emp_ID"].ToString());
                    string Officeid = objdb.Office_ID();
                    //getdata();
                   // DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_DCS_Information_system",
                   //new string[] { "flag", "Office_ID" },
                   //  new string[] { "2", Officeid.ToString()}, "dataset");
                   // if(dsdetail.Tables.Count>0)
                   // {
                   //     if (dsdetail.Tables[0].Rows.Count > 0)
                   //     {
                   //         txtDCScode.Text = dsdetail.Tables[0].Rows[0]["DCS_CODE"].ToString();
                   //         txtvillagename.Text = dsdetail.Tables[0].Rows[0]["Village_Name"].ToString();
                   //         txtDistrict.Text = dsdetail.Tables[0].Rows[0]["District_Name"].ToString();
                   //         txtBlock.Text = dsdetail.Tables[0].Rows[0]["Block_Name"].ToString();
                   //         txtCR_Num.Text = dsdetail.Tables[0].Rows[0]["RegistrationNo"].ToString();
                   //         txtcrdate.Text = dsdetail.Tables[0].Rows[0]["RegistrationDate"].ToString();
                   //         if (dsdetail.Tables[0].Rows[0]["WomenDcs"].ToString() == "Yes")
                   //         {
                   //             chkrw.Checked = true;
                   //             chkrw.Enabled = false;
                   //         }
                   //         else
                   //         {
                   //             chkrw.Checked = false;
                   //             chkrw.Enabled = false;
                   //         }
                   //         txtDCScode.Enabled = false;
                   //         txtvillagename.Enabled = false;
                   //         txtDistrict.Enabled = false;
                   //         txtBlock.Enabled = false;
                            
                            
                   //     }
                   // }
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

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_DCS_Information_system",
               new string[] { "flag", "Office_ID", "CreatedBy" },
                 new string[] { "3", objdb.Office_ID(), Session["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDCS_detail.DataSource = ds;
                    gvDCS_detail.DataBind();
                    gvDCS_detail.Visible = true;
                }
                else
                {
                    gvDCS_detail.DataSource = null;
                    gvDCS_detail.DataBind();
                }
            }
            else
            {
                gvDCS_detail.DataSource = null;
                gvDCS_detail.DataBind();
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
            gvDCS_detail.DataSource = string.Empty;
            gvDCS_detail.DataBind();
            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_DCS_Information_system",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year","CC_Id" },
                 new string[] { "7", ddlSocietyflt.SelectedValue, Session["Emp_ID"].ToString(),DDlMonth2.SelectedValue
                           , ddlyear2.SelectedItem.Text,ddlccbmcdetailflt.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDCS_detail.DataSource = ds;
                    gvDCS_detail.DataBind();
                    gvDCS_detail.Visible = true;
                    gvDCS_detail.Rows[gvDCS_detail.Rows.Count - 1].Focus();
                }
                else
                {
                    gvDCS_detail.DataSource = null;
                    gvDCS_detail.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                    lblMsg.Focus();
                }
            }
            else
            {
                gvDCS_detail.DataSource = null;
                gvDCS_detail.DataBind();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                lblMsg.Focus();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BindGridviewAvailableElectronicFacility()
     {
        try
        {
            if (ViewState["AvailableElectronicFacility"].ToString() == "")
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Sno", typeof(int));
                dt.Columns.Add("AEFFacility_Name", typeof(string));
                dt.Columns.Add("AEF_OWN", typeof(string));
                dt.Columns.Add("AEF_Scheme", typeof(string));
                dt.Columns.Add("AEF_InstDate", typeof(string));


                DataRow dr = dt.NewRow();
                dr["Sno"] = 1;
                dr["AEFFacility_Name"] = string.Empty;
                dr["AEF_OWN"] = string.Empty;
                dr["AEF_Scheme"] = string.Empty;
                dr["AEF_InstDate"] = string.Empty;


                dt.Rows.Add(dr);
                ViewState["AvailableElectronicFacility"] = dt;
                gvAvailableElectronicFacility.DataSource = dt;
                gvAvailableElectronicFacility.DataBind();
            }
            else
            {
                DataTable dt = (DataTable)ViewState["AvailableElectronicFacility"];
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
                ViewState["AvailableElectronicFacility"] = dt;
                gvAvailableElectronicFacility.DataSource = dt;
                gvAvailableElectronicFacility.DataBind();
            }




        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void DDlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvAvailableElectronicFacility_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["AvailableElectronicFacility"] != null)
            {
                DataTable dt = (DataTable)ViewState["AvailableElectronicFacility"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["AvailableElectronicFacility"] = dt;
                    gvAvailableElectronicFacility.DataSource = dt;
                    gvAvailableElectronicFacility.DataBind();

                    for (int i = 0; i < gvAvailableElectronicFacility.Rows.Count - 1; i++)
                    {
                        gvAvailableElectronicFacility.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }


                }
            }
           // gvAvailableElectronicFacility.Focus();
            gvAvailableElectronicFacility.Rows[gvAvailableElectronicFacility.Rows.Count - 1].Focus();
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
           // gvAvailableElectronicFacility.Focus();
            gvAvailableElectronicFacility.Rows[gvAvailableElectronicFacility.Rows.Count - 1].Focus();
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
            DataTable dt = (DataTable)ViewState["AvailableElectronicFacility"];

            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox txtAEFFacility_Name = (TextBox)gvAvailableElectronicFacility.Rows[rowIndex].Cells[1].FindControl("txtAEFFacility_Name");
                    TextBox txtAEF_OWN = (TextBox)gvAvailableElectronicFacility.Rows[rowIndex].Cells[2].FindControl("txtAEF_OWN");
                    TextBox txtAEF_Scheme = (TextBox)gvAvailableElectronicFacility.Rows[rowIndex].Cells[3].FindControl("txtAEF_Scheme");
                    TextBox txtAEF_InstDate = (TextBox)gvAvailableElectronicFacility.Rows[rowIndex].Cells[4].FindControl("txtAEF_InstDate");

                    //  TextBox txtDateOfReturn = (TextBox)gvtanker.Rows[rowIndex].Cells[8].FindControl("txtDateOfReturn");

                    dr = dt.NewRow();

                    dt.Rows[i - 1]["Sno"] = i + 1;
                    dt.Rows[i - 1]["AEFFacility_Name"] = txtAEFFacility_Name.Text;
                    dt.Rows[i - 1]["AEF_OWN"] = txtAEF_OWN.Text;
                    dt.Rows[i - 1]["AEF_Scheme"] = txtAEF_Scheme.Text;
                    dt.Rows[i - 1]["AEF_InstDate"] = txtAEF_InstDate.Text;

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
            ViewState["AvailableElectronicFacility"] = dt;
            gvAvailableElectronicFacility.DataSource = dt;
            gvAvailableElectronicFacility.DataBind();

        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvAvailableElectronicFacility_DataBound(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (DDlMonth.SelectedIndex > 0)
            {
                string Officeid = objdb.Office_ID();
                string Registered_As_Women = "";
                if(chkrw.Checked==true)
                {
                    Registered_As_Women = "Yes";
                }
                else
                {
                    Registered_As_Women = "No";
                }

                int PDM_rowcount = gvAvailableElectronicFacility.Rows.Count;
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_DCS_Information_system",
                    new string[] { "flag", "Office_ID", "DCS_Code" , "Village_name" ,"Village_Gis_code" ,"Milk_route ","Affiliated_chilling_center" ,"District" ,"Tehsil" ,"Block" ,"Field_officer" ,"VEO" ,"Scheme_in_which_DCS_Open" ,"Corporate_reg_number" ,"Corporate_reg_Date","Resistered_as_woman","Union_mem_number","Union_mem_Date","Ai_center_type","B_M_C_capecity","B_M_C_Scheme","B_M_C_date","Affiliated_with_BMC","Assembly_Area","Parliament_Area", "Month",
                                     "Year", "CreatedBy","CC_Id","DCS_Id" },
                      new string[] { "0", objdb.Office_ID(),txtDCScode.Text,txtvillagename.Text,txtvgiscode.Text,txtMilkRoute.Text,txtacc.Text,txtDistrict.Text,txtTehsil.Text,txtBlock.Text,txtFieldOfficer.Text,txtVEO.Text,txtsiwdcso.Text,txtCR_Num.Text,txtcrdate.Text,Registered_As_Women.ToString(),txtum_num.Text,txtumdate.Text,ddlaic.SelectedValue,txtBMCcapacity.Text,txtBMCScheme.Text,txtBMCInstDate.Text,txtAWB.Text,txtAssemblyArea.Text,txtParliamentArea.Text,  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString(),ddlccbmcdetail.SelectedValue,ddlSociety.SelectedValue }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    lblDCS_IS_ID.Text = ds.Tables[0].Rows[0]["DCS_IS_ID"].ToString();




                    foreach (GridViewRow row in gvAvailableElectronicFacility.Rows)
                    {
                        TextBox txtAEFFacility_Name = (TextBox)row.FindControl("txtAEFFacility_Name");
                        TextBox txtAEF_OWN = (TextBox)row.FindControl("txtAEF_OWN");
                        TextBox txtAEF_Scheme = (TextBox)row.FindControl("txtAEF_Scheme");
                        TextBox txtAEF_InstDate = (TextBox)row.FindControl("txtAEF_InstDate");
                        //TextBox txtTyre = (TextBox)row.FindControl("txtTyre");
                        //TextBox txtBattery = (TextBox)row.FindControl("txtBattery");
                        //TextBox txtMajorRepairs = (TextBox)row.FindControl("txtMajorRepairs");
                        //TextBox txtVehicleNo=(TextBox)row.FindControl("txtVehicleNo");

                        //  DateTime Purchase_date = txtPurchase_date.Text.ToString("DD-MM-YYYY");
                        // Purchase_date  DateTime.ParseExact(Purchase_date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                        DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_DCS_Information_system",
                      new string[] { "flag", "DCS_IS_ID", "AEFFacility_Name", "AEF_OWN", "AEF_Scheme",
                                     "AEF_InstDate" },
                        new string[] { "1", lblDCS_IS_ID.Text, txtAEFFacility_Name.Text, txtAEF_OWN.Text, txtAEF_Scheme.Text
                           , txtAEF_InstDate.Text}, "dataset");
                        count++;
                    }


                    if (count > 0)
                    {

                        lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Saved Successfully");
                        clear();
                       // getdata();

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
            lblDCS_IS_ID.Text = "";
            txtvgiscode.Text="";
            txtMilkRoute.Text="";
            txtacc.Text="";
            txtTehsil.Text="";
            txtFieldOfficer.Text="";
            txtVEO.Text="";
            txtsiwdcso.Text="";
            txtCR_Num.Text="";
            txtcrdate.Text="";
            //chkrw.Checked=false;
            txtum_num.Text="";
            txtumdate.Text="";
            ddlaic.SelectedIndex=0;txtBMCcapacity.Text="";
            txtBMCScheme.Text="";
            txtBMCInstDate.Text="";
            txtAWB.Text="";
            txtAssemblyArea.Text="";
            txtParliamentArea.Text="";
            DDlMonth.SelectedIndex = 0;
            ViewState["AvailableElectronicFacility"] = "";
            ddlccbmcdetail.ClearSelection();
            FillSociety();
            txtDCScode.Text = "";
            txtDCScode.Enabled = true;
            txtDistrict.Enabled = true;
            txtvillagename.Text = "";
            txtvillagename.Enabled = true;
            txtBlock.Enabled = true;
            chkrw.Enabled = true;
            btnSubmit.Visible = true;
            btnupdate.Visible = false;
            getyear();
            BindGridviewAvailableElectronicFacility();

        }
         catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        ddlccbmcdetailflt.DataTextField = "Office_Name";
                        ddlccbmcdetailflt.DataValueField = "Office_ID";


                        ddlccbmcdetailflt.DataSource = ds;
                        ddlccbmcdetailflt.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            //ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                            //ddlccbmcdetailflt.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {

                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            

                            ddlccbmcdetail.Enabled = false;

                            ddlccbmcdetailflt.SelectedValue = objdb.Office_ID();


                            ddlccbmcdetailflt.Enabled = false;
                            // FillSociety();
                        }


                    }
                }
               
            }
            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
            ddlccbmcdetailflt.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlccbmcdetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDCScode.Text = "";
        txtvillagename.Text = "";
        txtDistrict.Text = "";
        txtBlock.Text = "";
        txtCR_Num.Text = "";
        txtcrdate.Text = "";        
        chkrw.Checked = true;
        chkrw.Enabled = true;

        txtDCScode.Enabled = true;
        txtvillagename.Enabled = true;
        txtDistrict.Enabled = true;
        txtBlock.Enabled = true;
        FillSociety();
    }
    protected void FillSociety()
    {
        try
        {
            ddlSociety.Items.Clear();
            if (ddlccbmcdetail.SelectedValue != "0")
            {
                //ds = null;
                DataSet ds1 = objdb.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
                                  new string[] { "flag", "Supplyunitparant_ID" },
                                  new string[] { "16", ddlccbmcdetail.SelectedValue }, "dataset");

                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds1.Tables[0];
                        ddlSociety.DataBind();
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                //Response.Redirect("RMRD_Challan_EntryFromCanes.aspx", false);
            }
            ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvDCS_detail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "select")
            {
                int DCS_IS_ID = Convert.ToInt32(e.CommandArgument.ToString());
                lblDCS_IS_ID.Text = e.CommandArgument.ToString();
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_DCS_Information_system",
              new string[] { "flag", "Office_ID", "CreatedBy", "DCS_IS_ID" },
                new string[] { "4", objdb.Office_ID(), Session["Emp_ID"].ToString(), DCS_IS_ID.ToString() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DDlMonth.SelectedValue = ds.Tables[0].Rows[0]["Month"].ToString();
                        getyear();
                        ddlyear.SelectedValue = ds.Tables[0].Rows[0]["Year"].ToString();
                        ddlccbmcdetail.ClearSelection();
                        ddlccbmcdetail.Items.FindByValue(ds.Tables[0].Rows[0]["CC_Id"].ToString()).Selected = true;
                        FillSociety();
                        ddlSociety.ClearSelection();
                        ddlSociety.Items.FindByValue(ds.Tables[0].Rows[0]["DCS_Id"].ToString()).Selected = true;
                        ddlSociety_SelectedIndexChanged(sender, e);
                        txtvgiscode.Text = ds.Tables[0].Rows[0]["Village_Gis_code"].ToString();
                        txtMilkRoute.Text = ds.Tables[0].Rows[0]["Milk_route"].ToString();
                        txtacc.Text = ds.Tables[0].Rows[0]["Affiliated_chilling_center"].ToString();

                        txtTehsil.Text = ds.Tables[0].Rows[0]["Tehsil"].ToString();
                        txtFieldOfficer.Text = ds.Tables[0].Rows[0]["Field_officer"].ToString();
                        txtVEO.Text = ds.Tables[0].Rows[0]["VEO"].ToString();
                        txtsiwdcso.Text = ds.Tables[0].Rows[0]["Scheme_in_which_DCS_Open"].ToString();
                        txtCR_Num.Text = ds.Tables[0].Rows[0]["Corporate_reg_number"].ToString();
                        txtcrdate.Text = ds.Tables[0].Rows[0]["Corporate_reg_Date"].ToString();
                        if (ds.Tables[0].Rows[0]["Resistered_as_woman"].ToString() == "Yes")
                        {
                            chkrw.Checked = true;
                        }
                        else if (ds.Tables[0].Rows[0]["Resistered_as_woman"].ToString() == "No")
                        {
                            chkrw.Checked = false;
                        }
                        txtum_num.Text = ds.Tables[0].Rows[0]["Union_mem_number"].ToString();
                        txtumdate.Text = ds.Tables[0].Rows[0]["Union_mem_Date"].ToString();

                        ddlaic.SelectedValue = ds.Tables[0].Rows[0]["Ai_center_type"].ToString();
                        ddlaic.SelectedItem.Text = ds.Tables[0].Rows[0]["Ai_center_type"].ToString();
                        txtBMCcapacity.Text = ds.Tables[0].Rows[0]["B_M_C_capecity"].ToString();
                        txtBMCScheme.Text = ds.Tables[0].Rows[0]["B_M_C_Scheme"].ToString();
                        txtBMCInstDate.Text = ds.Tables[0].Rows[0]["B_M_C_date"].ToString();
                        txtAWB.Text = ds.Tables[0].Rows[0]["Affiliated_with_BMC"].ToString();
                        txtAssemblyArea.Text = ds.Tables[0].Rows[0]["Assembly_Area"].ToString();
                        txtParliamentArea.Text = ds.Tables[0].Rows[0]["Parliament_Area"].ToString();
                        DataTable dt = new DataTable();
                        dt = ds.Tables[1];
                        ViewState["AvailableElectronicFacility"] = dt;
                        gvAvailableElectronicFacility.DataSource = dt;
                        gvAvailableElectronicFacility.DataBind();
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
                string Officeid = objdb.Office_ID();
                string Registered_As_Women = "";
                if (chkrw.Checked == true)
                {
                    Registered_As_Women = "Yes";
                }
                else
                {
                    Registered_As_Women = "No";
                }

                int PDM_rowcount = gvAvailableElectronicFacility.Rows.Count;
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_DCS_Information_system",
                    new string[] { "flag","DCS_IS_ID", "Office_ID", "DCS_Code" , "Village_name" ,"Village_Gis_code" ,"Milk_route ","Affiliated_chilling_center" ,"District" ,"Tehsil" ,"Block" ,"Field_officer" ,"VEO" ,"Scheme_in_which_DCS_Open" ,"Corporate_reg_number" ,"Corporate_reg_Date","Resistered_as_woman","Union_mem_number","Union_mem_Date","Ai_center_type","B_M_C_capecity","B_M_C_Scheme","B_M_C_date","Affiliated_with_BMC","Assembly_Area","Parliament_Area", "Month",
                                     "Year", "CreatedBy","CC_Id","DCS_Id"  },
                      new string[] { "5",lblDCS_IS_ID.Text, objdb.Office_ID(),txtDCScode.Text,txtvillagename.Text,txtvgiscode.Text,txtMilkRoute.Text,txtacc.Text,txtDistrict.Text,txtTehsil.Text,txtBlock.Text,txtFieldOfficer.Text,txtVEO.Text,txtsiwdcso.Text,txtCR_Num.Text,txtcrdate.Text,Registered_As_Women.ToString(),txtum_num.Text,txtumdate.Text,ddlaic.SelectedValue,txtBMCcapacity.Text,txtBMCScheme.Text,txtBMCInstDate.Text,txtAWB.Text,txtAssemblyArea.Text,txtParliamentArea.Text,  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString(),ddlccbmcdetail.SelectedValue,ddlSociety.SelectedValue }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                   // lblDCS_IS_ID.Text = ds.Tables[0].Rows[0]["DCS_IS_ID"].ToString();


                    DataSet dsdetail_delete = objdb.ByProcedure("usp_insertupdate_monthly_DCS_Information_system",
                      new string[] { "flag", "DCS_IS_ID" },
                        new string[] { "6", lblDCS_IS_ID.Text}, "dataset");

                    foreach (GridViewRow row in gvAvailableElectronicFacility.Rows)
                    {
                        TextBox txtAEFFacility_Name = (TextBox)row.FindControl("txtAEFFacility_Name");
                        TextBox txtAEF_OWN = (TextBox)row.FindControl("txtAEF_OWN");
                        TextBox txtAEF_Scheme = (TextBox)row.FindControl("txtAEF_Scheme");
                        TextBox txtAEF_InstDate = (TextBox)row.FindControl("txtAEF_InstDate");
                        //TextBox txtTyre = (TextBox)row.FindControl("txtTyre");
                        //TextBox txtBattery = (TextBox)row.FindControl("txtBattery");
                        //TextBox txtMajorRepairs = (TextBox)row.FindControl("txtMajorRepairs");
                        //TextBox txtVehicleNo=(TextBox)row.FindControl("txtVehicleNo");

                        //  DateTime Purchase_date = txtPurchase_date.Text.ToString("DD-MM-YYYY");
                        // Purchase_date  DateTime.ParseExact(Purchase_date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                        DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_DCS_Information_system",
                      new string[] { "flag", "DCS_IS_ID", "AEFFacility_Name", "AEF_OWN", "AEF_Scheme",
                                     "AEF_InstDate" },
                        new string[] { "1", lblDCS_IS_ID.Text, txtAEFFacility_Name.Text, txtAEF_OWN.Text, txtAEF_Scheme.Text
                           , txtAEF_InstDate.Text}, "dataset");
                        count++;
                    }


                    if (count > 0)
                    {

                        lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "updated Successfully");
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
    protected void ddlSociety_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDCScode.Text = "";
        txtvillagename.Text = "";
        txtDistrict.Text = "";
        txtBlock.Text = "";
        txtCR_Num.Text = "";
        txtcrdate.Text = "";
        chkrw.Checked = false;
        chkrw.Enabled = true;

        txtDCScode.Enabled = true;
        txtvillagename.Enabled = true;
        txtDistrict.Enabled = true;
        txtBlock.Enabled = true;
        DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_DCS_Information_system",
       new string[] { "flag", "Office_ID" },
         new string[] { "2", ddlSociety.SelectedValue.ToString() }, "dataset");
        if (dsdetail.Tables.Count > 0)
        {
            if (dsdetail.Tables[0].Rows.Count > 0)
            {
                txtDCScode.Text = dsdetail.Tables[0].Rows[0]["DCS_CODE"].ToString();
                txtvillagename.Text = dsdetail.Tables[0].Rows[0]["Village_Name"].ToString();
                txtDistrict.Text = dsdetail.Tables[0].Rows[0]["District_Name"].ToString();
                txtBlock.Text = dsdetail.Tables[0].Rows[0]["Block_Name"].ToString();
                txtCR_Num.Text = dsdetail.Tables[0].Rows[0]["RegistrationNo"].ToString();
                txtcrdate.Text = dsdetail.Tables[0].Rows[0]["RegistrationDate"].ToString();
                if (dsdetail.Tables[0].Rows[0]["WomenDcs"].ToString() == "Yes")
                {
                    chkrw.Checked = true;
                    chkrw.Enabled = false;
                }
                else
                {
                    chkrw.Checked = false;
                    chkrw.Enabled = false;
                }
                txtDCScode.Enabled = false;
                txtvillagename.Enabled = false;
                txtDistrict.Enabled = false;
                txtBlock.Enabled = false;


            }
        }
    }
    protected void ddlccbmcdetailflt_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSocietyFlt();
    }
    protected void FillSocietyFlt()
    {
        try
        {
            ddlSocietyflt.Items.Clear();
            if (ddlccbmcdetailflt.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
                                  new string[] { "flag", "Supplyunitparant_ID" },
                                  new string[] { "16", ddlccbmcdetailflt.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSocietyflt.DataTextField = "Office_Name";
                        ddlSocietyflt.DataValueField = "Office_ID";
                        ddlSocietyflt.DataSource = ds.Tables[0];
                        ddlSocietyflt.DataBind();
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                //Response.Redirect("RMRD_Challan_EntryFromCanes.aspx", false);
            }
            ddlSocietyflt.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
  
}