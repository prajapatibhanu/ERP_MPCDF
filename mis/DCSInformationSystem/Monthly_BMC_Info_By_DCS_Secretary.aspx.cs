using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformationSystem_Monthly_BMC_Info_By_DCS_Secretary_aspx : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                getyear();
                int Employee_id = int.Parse(Session["Emp_ID"].ToString());
                string Officeid = objdb.Office_ID();
                GetCCDetails();
              //  getdata();
               // DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_BMC_Information_system",
               //new string[] { "flag", "Office_ID" },
               //  new string[] { "2", Officeid.ToString() }, "dataset");
               // if (dsdetail.Tables.Count > 0)
               // {
               //     if (dsdetail.Tables[0].Rows.Count > 0)
               //     {
               //         txtDCScode.Text = dsdetail.Tables[0].Rows[0]["DCS_CODE"].ToString();
                      
               //         txtDCScode.Enabled = false;
                       
               //     }
               // }
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

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_BMC_Information_system",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year","CC_Id" },
                 new string[] { "6", ddlSocietyflt.SelectedValue, Session["Emp_ID"].ToString(),DDlMonth2.SelectedValue
                           , ddlyear2.SelectedItem.Text,ddlccbmcdetailflt.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvBMC_detail.DataSource = ds;
                    gvBMC_detail.DataBind();
                    gvBMC_detail.Visible = true;
                    gvBMC_detail.Rows[gvBMC_detail.Rows.Count - 1].Focus();
                }
                else
                {
                    gvBMC_detail.DataSource = null;
                    gvBMC_detail.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                    lblMsg.Focus();
                }
            }
            else
            {
                gvBMC_detail.DataSource = null;
                gvBMC_detail.DataBind();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                lblMsg.Focus();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void DDlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvBMC_detail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "select")
            {
                int BMC_Info_ID = Convert.ToInt32(e.CommandArgument.ToString());
                lblBMC_Info_ID.Text = e.CommandArgument.ToString();
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_BMC_Information_system",
              new string[] { "flag", "Office_ID", "CreatedBy", "BMC_Info_ID" },
                new string[] { "4", objdb.Office_ID(), Session["Emp_ID"].ToString(), BMC_Info_ID.ToString() }, "dataset");
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
                        txtDCScode.Text = ds.Tables[0].Rows[0]["DCS_Code"].ToString();
                        txtMilk_handeled_during_month.Text = ds.Tables[0].Rows[0]["Milk_handeled_during_month"].ToString();
                        txtBMC_running_hours.Text = ds.Tables[0].Rows[0]["BMC_running_hours"].ToString();
                        txtElectricity_unit.Text = ds.Tables[0].Rows[0]["Electricity_unit"].ToString();
                        txtElectricity_Amount.Text = ds.Tables[0].Rows[0]["Electricity_Amount"].ToString();
                        txtDiesel_consumed.Text = ds.Tables[0].Rows[0]["Diesel_consumed"].ToString();
                        txtDiesel_Amount.Text = ds.Tables[0].Rows[0]["Diesel_Amount"].ToString();
                        txtOil_consumed.Text = ds.Tables[0].Rows[0]["Oil_consumed"].ToString();
                        txtOil_Amount.Text = ds.Tables[0].Rows[0]["Oil_Amount"].ToString();

                        txtCleaning_agent_consumed.Text = ds.Tables[0].Rows[0]["Cleaning_agent_consumed"].ToString();
                        txtCleaning_agent_Amount.Text = ds.Tables[0].Rows[0]["Cleaning_agent_Amount"].ToString();

                        txtRepair_mantenance_expence.Text = ds.Tables[0].Rows[0]["Repair_mantenance_expence"].ToString();
                        txtAdditional_M_P_deploy_num.Text = ds.Tables[0].Rows[0]["Additional_M_P_deploy_num"].ToString();
                        txtAdditional_M_P_deploy_Amount_salary.Text = ds.Tables[0].Rows[0]["Additional_M_P_deploy_Amount_salary"].ToString();
                        // PDM_rowcount.ToString()


                        //DataTable dt = new DataTable();
                        //dt = ds.Tables[1];
                        //ViewState["PurchaseDuringMonth"] = dt;
                        //gvBMC_detail.DataSource = dt;
                        //gvBMC_detail.DataBind();
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (DDlMonth.SelectedIndex > 0)
            {
                string Officeid = objdb.Office_ID();
              //  string Registered_As_Women = "";
                //if (chkrw.Checked == true)
                //{
                //    Registered_As_Women = "Yes";
                //}
                //else
                //{
                //    Registered_As_Women = "No";
                //}


                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_BMC_Information_system",
                    new string[] { "flag", "Office_ID", "DCS_Code" , "Milk_handeled_during_month" ,"BMC_running_hours" ,"Electricity_unit","Electricity_Amount" ,"Diesel_consumed" ,"Diesel_Amount" ,"Oil_consumed" ,"Oil_Amount" ,"Cleaning_agent_consumed" ,"Cleaning_agent_Amount" ,"Repair_mantenance_expence" ,"Additional_M_P_deploy_num","Additional_M_P_deploy_Amount_salary", "Month",
                                     "Year", "CreatedBy","CC_Id","DCS_Id" },
                      new string[] { "0", objdb.Office_ID(),txtDCScode.Text,txtMilk_handeled_during_month.Text,txtBMC_running_hours.Text,txtElectricity_unit.Text,txtElectricity_Amount.Text,txtDiesel_consumed.Text,txtDiesel_Amount.Text,txtOil_consumed.Text,txtOil_Amount.Text,txtCleaning_agent_consumed.Text,txtCleaning_agent_Amount.Text,txtRepair_mantenance_expence.Text,txtAdditional_M_P_deploy_num.Text,txtAdditional_M_P_deploy_Amount_salary.Text,  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString(),ddlccbmcdetail.SelectedValue,ddlSociety.SelectedValue }, "dataset");


                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
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
    public void getdata()
    {
        try
        {

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_BMC_Information_system",
               new string[] { "flag", "Office_ID", "CreatedBy" },
                 new string[] { "3", objdb.Office_ID(), Session["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvBMC_detail.DataSource = ds;
                    gvBMC_detail.DataBind();
                    gvBMC_detail.Visible = true;
                }
                else
                {
                    gvBMC_detail.DataSource = null;
                    gvBMC_detail.DataBind();
                }
            }
            else
            {
                gvBMC_detail.DataSource = null;
                gvBMC_detail.DataBind();
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
            lblBMC_Info_ID.Text = "";
            txtDCScode.Text = "";
            txtDCScode.Enabled = true;
            txtMilk_handeled_during_month.Text = "";
            txtBMC_running_hours.Text = "";
            txtElectricity_unit.Text = "";
            txtElectricity_Amount.Text = "";
            txtDiesel_consumed.Text = "";
            txtDiesel_Amount.Text = "";
            txtOil_consumed.Text = "";
            txtOil_Amount.Text = "";
           
            txtCleaning_agent_consumed.Text = "";
            txtCleaning_agent_Amount.Text = "";
           
            txtRepair_mantenance_expence.Text = "";
            txtAdditional_M_P_deploy_num.Text = "";
            txtAdditional_M_P_deploy_Amount_salary.Text = "";
           
            DDlMonth.SelectedIndex = 0;
            btnSubmit.Visible = true;
            btnupdate.Visible = false;
            ddlccbmcdetail.ClearSelection();
            FillSociety();
            getyear();
            

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


                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_BMC_Information_system",
                    new string[] { "flag","BMC_Info_ID", "Office_ID", "DCS_Code" , "Milk_handeled_during_month" ,"BMC_running_hours" ,"Electricity_unit","Electricity_Amount" ,"Diesel_consumed" ,"Diesel_Amount" ,"Oil_consumed" ,"Oil_Amount" ,"Cleaning_agent_consumed" ,"Cleaning_agent_Amount" ,"Repair_mantenance_expence" ,"Additional_M_P_deploy_num","Additional_M_P_deploy_Amount_salary", "Month",
                                     "Year", "CreatedBy","CC_Id","DCS_Id" },
                      new string[] { "5",lblBMC_Info_ID.Text ,objdb.Office_ID(),txtDCScode.Text,txtMilk_handeled_during_month.Text,txtBMC_running_hours.Text,txtElectricity_unit.Text,txtElectricity_Amount.Text,txtDiesel_consumed.Text,txtDiesel_Amount.Text,txtOil_consumed.Text,txtOil_Amount.Text,txtCleaning_agent_consumed.Text,txtCleaning_agent_Amount.Text,txtRepair_mantenance_expence.Text,txtAdditional_M_P_deploy_num.Text,txtAdditional_M_P_deploy_Amount_salary.Text,  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString(),ddlccbmcdetail.SelectedValue,ddlSociety.SelectedValue }, "dataset");


                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Updated Successfully");
                        clear();
                        //getdata();
                        lblBMC_Info_ID.Text = "";
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
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                            ddlccbmcdetailflt.Items.Insert(0, new ListItem("Select", "0"));
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
                ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                ddlccbmcdetailflt.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlccbmcdetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDCScode.Text = "";

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
    protected void ddlSociety_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDCScode.Text = "";

        DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_BMC_Information_system",
               new string[] { "flag", "Office_ID" },
                 new string[] { "2", ddlSociety.SelectedValue }, "dataset");
        if (dsdetail.Tables.Count > 0)
        {
            if (dsdetail.Tables[0].Rows.Count > 0)
            {
                txtDCScode.Text = dsdetail.Tables[0].Rows[0]["DCS_CODE"].ToString();

                txtDCScode.Enabled = false;

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