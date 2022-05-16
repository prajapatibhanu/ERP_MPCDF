using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformationSystem_DCS_semifixed_nature_info : System.Web.UI.Page
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
                //getdata();
               // DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_SEmi_fixed_nature_info_byDCS",
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
    public void getdata()
    {
        try
        {

            DataSet ds = objdb.ByProcedure("usp_insertupdate_SEmi_fixed_nature_info_byDCS",
               new string[] { "flag", "Office_ID", "CreatedBy" },
                 new string[] { "3", objdb.Office_ID(), Session["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvSFNI_detail.DataSource = ds;
                    gvSFNI_detail.DataBind();
                    gvSFNI_detail.Visible = true;
                }
                else
                {
                    gvSFNI_detail.DataSource = null;
                    gvSFNI_detail.DataBind();
                }
            }
            else
            {
                gvSFNI_detail.DataSource = null;
                gvSFNI_detail.DataBind();
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

            DataSet ds = objdb.ByProcedure("usp_insertupdate_SEmi_fixed_nature_info_byDCS",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year","CC_Id" },
                 new string[] { "6", ddlSocietyflt.SelectedValue, Session["Emp_ID"].ToString(),DDlMonth2.SelectedValue
                           , ddlyear2.SelectedItem.Text,ddlccbmcdetailflt.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvSFNI_detail.DataSource = ds;
                    gvSFNI_detail.DataBind();
                    gvSFNI_detail.Visible = true;
                    gvSFNI_detail.Rows[gvSFNI_detail.Rows.Count - 1].Focus();
                }
                else
                {
                    gvSFNI_detail.DataSource = null;
                    gvSFNI_detail.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                    lblMsg.Focus();
                }
            }
            else
            {
                gvSFNI_detail.DataSource = null;
                gvSFNI_detail.DataBind();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                lblMsg.Focus();
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
            lblSFN_DCS_ID.Text = "";
            txtDCScode.Text = "";
            txtTNM_GC_LL.Text = "0";
            txtTNM_GC_MF.Text = "0";
            txtTNM_GC_SF.Text = "0";
            txtTNM_GC_LFO.Text = "0";
            txtTNM_SC_LL.Text = "0";
            txtTNM_SC_MF.Text = "0";
            txtTNM_SC_SF.Text = "0";
            txtTNM_SC_LFO.Text = "0";
            txtTNM_ST_LL.Text = "0";
            txtTNM_ST_MF.Text = "0";
            txtTNM_ST_SF.Text = "0";
            txtTNM_ST_LFO.Text = "0";
            txtTNM_OBC_LL.Text = "0";
            txtTNM_OBC_MF.Text = "0";
            txtTNM_OBC_SF.Text = "0";
            txtTNM_OBC_LFO.Text = "0";

            txtSCA_LL.Text = "0";
            txtSCA_MF.Text = "0";
            txtSCA_SF.Text = "0";
            txtSCA_LFO.Text = "0";

            txtWM_GC_LL.Text = "0";
            txtWM_GC_MF.Text = "0";
            txtWM_GC_SF.Text = "0";
            txtWM_GC_LFO.Text = "0";
            txtWM_SC_LL.Text = "0";
            txtWM_SC_MF.Text = "0";
            txtWM_SC_SF.Text = "0";
            txtWM_SC_LFO.Text = "0";
            txtWM_ST_LL.Text = "0";
            txtWM_ST_MF.Text = "0";
            txtWM_ST_SF.Text = "0";
            txtWM_ST_LFO.Text = "0";
            txtWM_OBC_LL.Text = "0";
            txtWM_OBC_MF.Text = "0";
            txtWM_OBC_SF.Text = "0";
            txtWM_OBC_LFO.Text = "0";
           

            DDlMonth.SelectedIndex = 0;
            ddlccbmcdetail.ClearSelection();
            FillSociety();
            btnSubmit.Visible = true;
            btnupdate.Visible = false;

            getyear();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void DDlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnupdate_Click(object sender, EventArgs e)
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

                DataSet ds = objdb.ByProcedure("usp_insertupdate_SEmi_fixed_nature_info_byDCS",
                    new string[] { "flag","SFN_DCS_ID", "Office_ID", "DCS_Code", "TNM_GC_LL", "TNM_GC_MF", "TNM_GC_SF", "TNM_GC_LFO", "TNM_SC_LL", "TNM_SC_MF", "TNM_SC_SF", "TNM_SC_LFO", "TNM_ST_LL", "TNM_ST_MF", "TNM_ST_SF", "TNM_ST_LFO", "TNM_OBC_LL", "TNM_OBC_MF", "TNM_OBC_SF", "TNM_OBC_LFO", "SCA_LL", "SCA_MF", "SCA_SF", "SCA_LFO", "WM_GC_LL", "WM_GC_MF", "WM_GC_SF", "WM_GC_LFO", "WM_SC_LL", "WM_SC_MF", "WM_SC_SF", "WM_SC_LFO", "WM_ST_LL", "WM_ST_MF", "WM_ST_SF", "WM_ST_LFO", "WM_OBC_LL", "WM_OBC_MF", "WM_OBC_SF", "WM_OBC_LFO", "Month",
                                     "Year", "CreatedBy","CC_Id","DCS_Id" },
                      new string[] { "5",lblSFN_DCS_ID.Text, objdb.Office_ID(),txtDCScode.Text,txtTNM_GC_LL.Text ,txtTNM_GC_MF.Text ,txtTNM_GC_SF.Text ,txtTNM_GC_LFO.Text ,txtTNM_SC_LL.Text ,txtTNM_SC_MF.Text ,txtTNM_SC_SF.Text ,txtTNM_SC_LFO.Text ,txtTNM_ST_LL.Text ,txtTNM_ST_MF.Text ,txtTNM_ST_SF.Text ,txtTNM_ST_LFO.Text ,txtTNM_OBC_LL.Text ,txtTNM_OBC_MF.Text ,txtTNM_OBC_SF.Text ,txtTNM_OBC_LFO.Text ,txtSCA_LL.Text ,txtSCA_MF.Text ,txtSCA_SF.Text ,txtSCA_LFO.Text ,txtWM_GC_LL.Text ,txtWM_GC_MF.Text ,txtWM_GC_SF.Text ,txtWM_GC_LFO.Text ,txtWM_SC_LL.Text ,txtWM_SC_MF.Text ,txtWM_SC_SF.Text ,txtWM_SC_LFO.Text ,txtWM_ST_LL.Text ,txtWM_ST_MF.Text ,txtWM_ST_SF.Text ,txtWM_ST_LFO.Text ,txtWM_OBC_LL.Text ,txtWM_OBC_MF.Text ,txtWM_OBC_SF.Text ,txtWM_OBC_LFO.Text ,  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString(),ddlccbmcdetail.SelectedValue,ddlSociety.SelectedValue  }, "dataset");


                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
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

                DataSet ds = objdb.ByProcedure("usp_insertupdate_SEmi_fixed_nature_info_byDCS",
                    new string[] { "flag", "Office_ID", "DCS_Code", "TNM_GC_LL", "TNM_GC_MF", "TNM_GC_SF", "TNM_GC_LFO", "TNM_SC_LL", "TNM_SC_MF", "TNM_SC_SF", "TNM_SC_LFO", "TNM_ST_LL", "TNM_ST_MF", "TNM_ST_SF", "TNM_ST_LFO", "TNM_OBC_LL", "TNM_OBC_MF", "TNM_OBC_SF", "TNM_OBC_LFO", "SCA_LL", "SCA_MF", "SCA_SF", "SCA_LFO", "WM_GC_LL", "WM_GC_MF", "WM_GC_SF", "WM_GC_LFO", "WM_SC_LL", "WM_SC_MF", "WM_SC_SF", "WM_SC_LFO", "WM_ST_LL", "WM_ST_MF", "WM_ST_SF", "WM_ST_LFO", "WM_OBC_LL", "WM_OBC_MF", "WM_OBC_SF", "WM_OBC_LFO", "Month",
                                     "Year", "CreatedBy","CC_Id","DCS_Id" },
                      new string[] { "0", objdb.Office_ID(),txtDCScode.Text,txtTNM_GC_LL.Text ,txtTNM_GC_MF.Text ,txtTNM_GC_SF.Text ,txtTNM_GC_LFO.Text ,txtTNM_SC_LL.Text ,txtTNM_SC_MF.Text ,txtTNM_SC_SF.Text ,txtTNM_SC_LFO.Text ,txtTNM_ST_LL.Text ,txtTNM_ST_MF.Text ,txtTNM_ST_SF.Text ,txtTNM_ST_LFO.Text ,txtTNM_OBC_LL.Text ,txtTNM_OBC_MF.Text ,txtTNM_OBC_SF.Text ,txtTNM_OBC_LFO.Text ,txtSCA_LL.Text ,txtSCA_MF.Text ,txtSCA_SF.Text ,txtSCA_LFO.Text ,txtWM_GC_LL.Text ,txtWM_GC_MF.Text ,txtWM_GC_SF.Text ,txtWM_GC_LFO.Text ,txtWM_SC_LL.Text ,txtWM_SC_MF.Text ,txtWM_SC_SF.Text ,txtWM_SC_LFO.Text ,txtWM_ST_LL.Text ,txtWM_ST_MF.Text ,txtWM_ST_SF.Text ,txtWM_ST_LFO.Text ,txtWM_OBC_LL.Text ,txtWM_OBC_MF.Text ,txtWM_OBC_SF.Text ,txtWM_OBC_LFO.Text ,  DDlMonth.SelectedValue
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
    protected void gvSFNI_detail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "select")
            {
                int SFN_DCS_ID = Convert.ToInt32(e.CommandArgument.ToString());
                lblSFN_DCS_ID.Text = e.CommandArgument.ToString();
                DataSet ds = objdb.ByProcedure("usp_insertupdate_SEmi_fixed_nature_info_byDCS",
              new string[] { "flag", "Office_ID", "CreatedBy", "SFN_DCS_ID" },
                new string[] { "4", objdb.Office_ID(), Session["Emp_ID"].ToString(), SFN_DCS_ID.ToString() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DDlMonth.SelectedValue = ds.Tables[0].Rows[0]["Month"].ToString();
                        getyear();
                        ddlyear.SelectedValue = ds.Tables[0].Rows[0]["Year"].ToString();
                        ddlccbmcdetail.ClearSelection();
                        ddlccbmcdetail.Items.FindByValue(ds.Tables[0].Rows[0]["CC_Id"].ToString()).Selected = true;
                        ddlccbmcdetail_SelectedIndexChanged(sender, e);
                        ddlSociety.ClearSelection();
                        ddlSociety.Items.FindByValue(ds.Tables[0].Rows[0]["DCS_Id"].ToString()).Selected = true;
                        txtDCScode.Text = ds.Tables[0].Rows[0]["DCS_Code"].ToString();

                        txtTNM_GC_LL.Text = ds.Tables[0].Rows[0]["TNM_GC_LL"].ToString();
                        txtTNM_GC_MF.Text = ds.Tables[0].Rows[0]["TNM_GC_MF"].ToString();
                        txtTNM_GC_SF.Text = ds.Tables[0].Rows[0]["TNM_GC_SF"].ToString();
                        txtTNM_GC_LFO.Text = ds.Tables[0].Rows[0]["TNM_GC_LFO"].ToString();
                        txtTNM_SC_LL.Text = ds.Tables[0].Rows[0]["TNM_SC_LL"].ToString();
                        txtTNM_SC_MF.Text = ds.Tables[0].Rows[0]["TNM_SC_MF"].ToString();
                        txtTNM_SC_SF.Text = ds.Tables[0].Rows[0]["TNM_SC_SF"].ToString();
                        txtTNM_SC_LFO.Text = ds.Tables[0].Rows[0]["TNM_SC_LFO"].ToString();
                        txtTNM_ST_LL.Text = ds.Tables[0].Rows[0]["TNM_ST_LL"].ToString();
                        txtTNM_ST_MF.Text = ds.Tables[0].Rows[0]["TNM_ST_MF"].ToString();
                        txtTNM_ST_SF.Text = ds.Tables[0].Rows[0]["TNM_ST_SF"].ToString();
                        txtTNM_ST_LFO.Text = ds.Tables[0].Rows[0]["TNM_ST_LFO"].ToString();
                        txtTNM_OBC_LL.Text = ds.Tables[0].Rows[0]["TNM_OBC_LL"].ToString();
                        txtTNM_OBC_MF.Text = ds.Tables[0].Rows[0]["TNM_OBC_MF"].ToString();
                        txtTNM_OBC_SF.Text = ds.Tables[0].Rows[0]["TNM_OBC_SF"].ToString();
                        txtTNM_OBC_LFO.Text = ds.Tables[0].Rows[0]["TNM_OBC_LFO"].ToString();

                        txtSCA_LL.Text = ds.Tables[0].Rows[0]["SCA_LL"].ToString();
                        txtSCA_MF.Text = ds.Tables[0].Rows[0]["SCA_MF"].ToString();
                        txtSCA_SF.Text = ds.Tables[0].Rows[0]["SCA_SF"].ToString();
                        txtSCA_LFO.Text = ds.Tables[0].Rows[0]["SCA_LFO"].ToString();

                        txtWM_GC_LL.Text = ds.Tables[0].Rows[0]["WM_GC_LL"].ToString();
                        txtWM_GC_MF.Text = ds.Tables[0].Rows[0]["WM_GC_MF"].ToString();
                        txtWM_GC_SF.Text = ds.Tables[0].Rows[0]["WM_GC_SF"].ToString();
                        txtWM_GC_LFO.Text = ds.Tables[0].Rows[0]["WM_GC_LFO"].ToString();
                        txtWM_SC_LL.Text = ds.Tables[0].Rows[0]["WM_SC_LL"].ToString();
                        txtWM_SC_MF.Text = ds.Tables[0].Rows[0]["WM_SC_MF"].ToString();
                        txtWM_SC_SF.Text = ds.Tables[0].Rows[0]["WM_SC_SF"].ToString();
                        txtWM_SC_LFO.Text = ds.Tables[0].Rows[0]["WM_SC_LFO"].ToString();
                        txtWM_ST_LL.Text = ds.Tables[0].Rows[0]["WM_ST_LL"].ToString();
                        txtWM_ST_MF.Text = ds.Tables[0].Rows[0]["WM_ST_MF"].ToString();
                        txtWM_ST_SF.Text = ds.Tables[0].Rows[0]["WM_ST_SF"].ToString();
                        txtWM_ST_LFO.Text = ds.Tables[0].Rows[0]["WM_ST_LFO"].ToString();
                        txtWM_OBC_LL.Text = ds.Tables[0].Rows[0]["WM_OBC_LL"].ToString();
                        txtWM_OBC_MF.Text = ds.Tables[0].Rows[0]["WM_OBC_MF"].ToString();
                        txtWM_OBC_SF.Text = ds.Tables[0].Rows[0]["WM_OBC_SF"].ToString();
                        txtWM_OBC_LFO.Text = ds.Tables[0].Rows[0]["WM_OBC_LFO"].ToString();

                        
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
                DataSet  ds1 = objdb.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
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
        DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_SEmi_fixed_nature_info_byDCS",
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
                Response.Redirect("RMRD_Challan_EntryFromCanes.aspx", false);
            }
            ddlSocietyflt.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}