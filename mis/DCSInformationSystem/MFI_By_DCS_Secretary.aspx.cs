using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformationSystem_MFI_By_DCS_Secretary : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtTotal_Profit_Loss.Text = "0";
                getyear();
                int Employee_id = int.Parse(Session["Emp_ID"].ToString());
                string Officeid = objdb.Office_ID();
                GetCCDetails();
               // getdata();
               // DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_Finanacial_info_byDCS",
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

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Finanacial_info_byDCS",
               new string[] { "flag", "Office_ID", "CreatedBy" },
                 new string[] { "3", objdb.Office_ID(), Session["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvMFI_detail.DataSource = ds;
                    gvMFI_detail.DataBind();
                    gvMFI_detail.Visible = true;
                }
                else
                {
                    gvMFI_detail.DataSource = null;
                    gvMFI_detail.DataBind();
                }
            }
            else
            {
                gvMFI_detail.DataSource = null;
                gvMFI_detail.DataBind();
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

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Finanacial_info_byDCS",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year","CC_Id" },
                 new string[] { "6", ddlSocietyflt.SelectedValue, Session["Emp_ID"].ToString(),DDlMonth2.SelectedValue
                           , ddlyear2.SelectedItem.Text,ddlccbmcdetailflt.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvMFI_detail.DataSource = ds;
                    gvMFI_detail.DataBind();
                    gvMFI_detail.Visible = true;
                    gvMFI_detail.Rows[gvMFI_detail.Rows.Count - 1].Focus();
                }
                else
                {
                    gvMFI_detail.DataSource = null;
                    gvMFI_detail.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                    lblMsg.Focus();
                }
            }
            else
            {
                gvMFI_detail.DataSource = null;
                gvMFI_detail.DataBind();
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
            lblMFI_DCS_ID.Text = "";
             txtDCScode.Text = "";
             txtDCScode.Enabled = true;
            txtOI_Milk_amount.Text = "0";
            txtOI_DCS_Commition.Text = "0";
            txtOI_Ghee_Commition.Text = "0";
            txtOI_Cattle_feed_Commition.Text = "0";
            txtOI_Miniral_mixture_Commition.Text = "0";
            txtOI_Head_load.Text = "0";
            txtOI_BMC_Chilling_charges.Text = "0";
            txtOI_Local_milk_sale_amount.Text = "0";
            txtOI_Sample_milk_sale_amount.Text = "0";
            txtOI_Other.Text = "";

            txtOE_Payment_to_producer.Text = "0";
            txtOE_Head_load.Text = "";
            txtOE_Camical_detergent.Text = "0";
            txtOE_Traveling.Text = "0";
            txtOE_Stationary.Text = "0";
            txtOE_Computer_expense.Text = "0";
            txtOE_Office_expense.Text = "0";
            txtOE_General_body_meeting.Text = "0";
            txtOE_STS_Secretary.Text = "0";
            txtOE_STS_Tester_helper.Text = "0";
            txtOE_STS_AHC_AIworker.Text = "0";
            txtOE_Other.Text = "0";


            lblTotal_operating_income.Text = "0";
            lblTotal_operating_Expense.Text = "0";
            txtTotal_Profit_Loss.Text = "0";

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
    protected void DDlMonth_SelectedIndexChanged(object sender, EventArgs e)
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
                //  string Registered_As_Women = "";
                //if (chkrw.Checked == true)
                //{
                //    Registered_As_Women = "Yes";
                //}
                //else
                //{
                //    Registered_As_Women = "No";
                //}
                lblTotal_operating_income.Text =Convert.ToString( double.Parse(txtOI_Milk_amount.Text) + double.Parse(txtOI_DCS_Commition.Text) + double.Parse(txtOI_Ghee_Commition.Text) + double.Parse(txtOI_Cattle_feed_Commition.Text) + double.Parse(txtOI_Miniral_mixture_Commition.Text) + double.Parse(txtOI_Head_load.Text) + double.Parse(txtOI_BMC_Chilling_charges.Text) + double.Parse(txtOI_Local_milk_sale_amount.Text) + double.Parse(txtOI_Sample_milk_sale_amount.Text) + double.Parse(txtOI_Other.Text));
                lblTotal_operating_Expense.Text = Convert.ToString(double.Parse(txtOE_Payment_to_producer.Text) + double.Parse(txtOE_Head_load.Text) + double.Parse(txtOE_Camical_detergent.Text) + double.Parse(txtOE_Traveling.Text) + double.Parse(txtOE_Stationary.Text) + double.Parse(txtOE_Computer_expense.Text) + double.Parse(txtOE_Office_expense.Text) + double.Parse(txtOE_General_body_meeting.Text) + double.Parse(txtOE_STS_Secretary.Text) + double.Parse(txtOE_STS_Tester_helper.Text) + double.Parse(txtOE_STS_AHC_AIworker.Text) + double.Parse(txtOE_Other.Text));
                txtTotal_Profit_Loss.Text = Convert.ToString(double.Parse(lblTotal_operating_income.Text) - double.Parse(lblTotal_operating_Expense.Text));
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Finanacial_info_byDCS",
                    new string[] { "flag", "Office_ID", "DCS_Code" ,"OI_Milk_amount","OI_DCS_Commition","OI_Ghee_Commition","OI_Cattle_feed_Commition","OI_Miniral_mixture_Commition","OI_Head_load","OI_BMC_Chilling_charges","OI_Local_milk_sale_amount","OI_Sample_milk_sale_amount","OI_Other","OE_Payment_to_producer","OE_Head_load","OE_Camical_detergent","OE_Traveling","OE_Stationary","OE_Computer_expense","OE_Office_expense","OE_General_body_meeting","OE_STS_Secretary","OE_STS_Tester_helper","OE_STS_AHC_AIworker","OE_Other","Total_operating_income","Total_operating_Expense","Total_Profit_Loss", "Month",
                                     "Year", "CreatedBy","CC_Id","DCS_Id" },
                      new string[] { "0", objdb.Office_ID(),txtDCScode.Text,txtOI_Milk_amount.Text,txtOI_DCS_Commition.Text,txtOI_Ghee_Commition.Text,txtOI_Cattle_feed_Commition.Text,txtOI_Miniral_mixture_Commition.Text,txtOI_Head_load.Text,txtOI_BMC_Chilling_charges.Text,txtOI_Local_milk_sale_amount.Text,txtOI_Sample_milk_sale_amount.Text,txtOI_Other.Text,txtOE_Payment_to_producer.Text,txtOE_Head_load.Text,txtOE_Camical_detergent.Text,txtOE_Traveling.Text,txtOE_Stationary.Text,txtOE_Computer_expense.Text,txtOE_Office_expense.Text,txtOE_General_body_meeting.Text,txtOE_STS_Secretary.Text,txtOE_STS_Tester_helper.Text,txtOE_STS_AHC_AIworker.Text,txtOE_Other.Text,lblTotal_operating_income.Text,lblTotal_operating_Expense.Text,txtTotal_Profit_Loss.Text,  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text,Session["Emp_ID"].ToString(),ddlccbmcdetail.SelectedValue,ddlSociety.SelectedValue }, "dataset");


                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
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
    protected void gvMFI_detail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "select")
            {
                int MFI_DCS_ID = Convert.ToInt32(e.CommandArgument.ToString());
                lblMFI_DCS_ID.Text = e.CommandArgument.ToString();
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Finanacial_info_byDCS",
              new string[] { "flag", "Office_ID", "CreatedBy", "MFI_DCS_ID" },
                new string[] { "4", objdb.Office_ID(), Session["Emp_ID"].ToString(), MFI_DCS_ID.ToString() }, "dataset");
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

                        txtOI_Milk_amount.Text = ds.Tables[0].Rows[0]["OI_Milk_amount"].ToString();
                        txtOI_DCS_Commition.Text = ds.Tables[0].Rows[0]["OI_DCS_Commition"].ToString();
                        txtOI_Ghee_Commition.Text = ds.Tables[0].Rows[0]["OI_Ghee_Commition"].ToString();
                        txtOI_Cattle_feed_Commition.Text = ds.Tables[0].Rows[0]["OI_Cattle_feed_Commition"].ToString();
                        txtOI_Miniral_mixture_Commition.Text = ds.Tables[0].Rows[0]["OI_Miniral_mixture_Commition"].ToString();
                        txtOI_Head_load.Text = ds.Tables[0].Rows[0]["OI_Head_load"].ToString();
                        txtOI_BMC_Chilling_charges.Text = ds.Tables[0].Rows[0]["OI_BMC_Chilling_charges"].ToString();
                        txtOI_Local_milk_sale_amount.Text = ds.Tables[0].Rows[0]["OI_Local_milk_sale_amount"].ToString();
                        txtOI_Sample_milk_sale_amount.Text = ds.Tables[0].Rows[0]["OI_Sample_milk_sale_amount"].ToString();
                        txtOI_Other.Text = ds.Tables[0].Rows[0]["OI_Other"].ToString();

                       
                        txtOE_Payment_to_producer.Text = ds.Tables[0].Rows[0]["OE_Payment_to_producer"].ToString();
                        txtOE_Head_load.Text = ds.Tables[0].Rows[0]["OE_Head_load"].ToString();
                        txtOE_Camical_detergent.Text = ds.Tables[0].Rows[0]["OE_Camical_detergent"].ToString();
                        txtOE_Traveling.Text = ds.Tables[0].Rows[0]["OE_Traveling"].ToString();
                        txtOE_Stationary.Text = ds.Tables[0].Rows[0]["OE_Stationary"].ToString();
                        txtOE_Computer_expense.Text = ds.Tables[0].Rows[0]["OE_Computer_expense"].ToString();
                        txtOE_Office_expense.Text = ds.Tables[0].Rows[0]["OE_Office_expense"].ToString();
                        txtOE_General_body_meeting.Text = ds.Tables[0].Rows[0]["OE_General_body_meeting"].ToString();
                        txtOE_STS_Secretary.Text = ds.Tables[0].Rows[0]["OE_STS_Secretary"].ToString();
                        txtOE_STS_Tester_helper.Text = ds.Tables[0].Rows[0]["OE_STS_Tester_helper"].ToString();
                        txtOE_STS_AHC_AIworker.Text = ds.Tables[0].Rows[0]["OE_STS_AHC_AIworker"].ToString();
                        txtOE_Other.Text = ds.Tables[0].Rows[0]["OE_Other"].ToString();

                        lblTotal_operating_income.Text = ds.Tables[0].Rows[0]["Total_operating_income"].ToString();
                        lblTotal_operating_Expense.Text = ds.Tables[0].Rows[0]["Total_operating_Expense"].ToString();
                        txtTotal_Profit_Loss.Text = ds.Tables[0].Rows[0]["Total_Profit_Loss"].ToString();
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
                lblTotal_operating_income.Text = Convert.ToString(double.Parse(txtOI_Milk_amount.Text) + double.Parse(txtOI_DCS_Commition.Text) + double.Parse(txtOI_Ghee_Commition.Text) + double.Parse(txtOI_Cattle_feed_Commition.Text) + double.Parse(txtOI_Miniral_mixture_Commition.Text) + double.Parse(txtOI_Head_load.Text) + double.Parse(txtOI_BMC_Chilling_charges.Text) + double.Parse(txtOI_Local_milk_sale_amount.Text) + double.Parse(txtOI_Sample_milk_sale_amount.Text) + double.Parse(txtOI_Other.Text));
                lblTotal_operating_Expense.Text = Convert.ToString(double.Parse(txtOE_Payment_to_producer.Text) + double.Parse(txtOE_Head_load.Text) + double.Parse(txtOE_Camical_detergent.Text) + double.Parse(txtOE_Traveling.Text) + double.Parse(txtOE_Stationary.Text) + double.Parse(txtOE_Computer_expense.Text) + double.Parse(txtOE_Office_expense.Text) + double.Parse(txtOE_General_body_meeting.Text) + double.Parse(txtOE_STS_Secretary.Text) + double.Parse(txtOE_STS_Tester_helper.Text) + double.Parse(txtOE_STS_AHC_AIworker.Text) + double.Parse(txtOE_Other.Text));
                txtTotal_Profit_Loss.Text = Convert.ToString(double.Parse(lblTotal_operating_income.Text) - double.Parse(lblTotal_operating_Expense.Text));
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Finanacial_info_byDCS",
                    new string[] { "flag","MFI_DCS_ID" ,"Office_ID", "DCS_Code" ,"OI_Milk_amount","OI_DCS_Commition","OI_Ghee_Commition","OI_Cattle_feed_Commition","OI_Miniral_mixture_Commition","OI_Head_load","OI_BMC_Chilling_charges","OI_Local_milk_sale_amount","OI_Sample_milk_sale_amount","OI_Other","OE_Payment_to_producer","OE_Head_load","OE_Camical_detergent","OE_Traveling","OE_Stationary","OE_Computer_expense","OE_Office_expense","OE_General_body_meeting","OE_STS_Secretary","OE_STS_Tester_helper","OE_STS_AHC_AIworker","OE_Other","Total_operating_income","Total_operating_Expense","Total_Profit_Loss", "Month",
                                     "Year", "CreatedBy","CC_Id","DCS_Id" },
                      new string[] { "5",lblMFI_DCS_ID.Text, objdb.Office_ID(),txtDCScode.Text,txtOI_Milk_amount.Text,txtOI_DCS_Commition.Text,txtOI_Ghee_Commition.Text,txtOI_Cattle_feed_Commition.Text,txtOI_Miniral_mixture_Commition.Text,txtOI_Head_load.Text,txtOI_BMC_Chilling_charges.Text,txtOI_Local_milk_sale_amount.Text,txtOI_Sample_milk_sale_amount.Text,txtOI_Other.Text,txtOE_Payment_to_producer.Text,txtOE_Head_load.Text,txtOE_Camical_detergent.Text,txtOE_Traveling.Text,txtOE_Stationary.Text,txtOE_Computer_expense.Text,txtOE_Office_expense.Text,txtOE_General_body_meeting.Text,txtOE_STS_Secretary.Text,txtOE_STS_Tester_helper.Text,txtOE_STS_AHC_AIworker.Text,txtOE_Other.Text,lblTotal_operating_income.Text,lblTotal_operating_Expense.Text,txtTotal_Profit_Loss.Text,  DDlMonth.SelectedValue
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

    protected void calculation(string OI_Milk_amount, string OI_DCS_Commition, string OI_Ghee_Commition, string OI_Cattle_feed_Commition, string OI_Miniral_mixture_Commition, string OI_Head_load, string OI_BMC_Chilling_charges, string OI_Local_milk_sale_amount, string OI_Sample_milk_sale_amount, string OI_Other, string OE_Payment_to_producer, string OE_Head_load, string OE_Camical_detergent, string OE_Traveling, string OE_Stationary, string OE_Computer_expense, string OE_Office_expense, string OE_General_body_meeting, string OE_STS_Secretary, string OE_STS_Tester_helper, string OE_STS_AHC_AIworker, string OE_Other)
    {
        try
        {

            double OE = 0, OI = 0;
           // string O_pl = "";
           // string Of = txtOI_Milk_amount.Text;
           // OE = double.Parse(txtOI_Milk_amount.Text) + double.Parse(txtOI_DCS_Commition.Text) + double.Parse(txtOI_Ghee_Commition.Text) + double.Parse(txtOI_Cattle_feed_Commition.Text) + double.Parse(txtOI_Miniral_mixture_Commition.Text) + double.Parse(txtOI_Head_load.Text) + double.Parse(txtOI_BMC_Chilling_charges.Text) + double.Parse(txtOI_Local_milk_sale_amount.Text) + double.Parse(txtOI_Sample_milk_sale_amount.Text) + double.Parse(txtOI_Other.Text) ;
            OI = double.Parse(OI_Milk_amount) + double.Parse(OI_DCS_Commition) + double.Parse(OI_Ghee_Commition) + double.Parse(OI_Cattle_feed_Commition) + double.Parse(OI_Miniral_mixture_Commition) + double.Parse(OI_Head_load) + double.Parse(OI_BMC_Chilling_charges) + double.Parse(OI_Local_milk_sale_amount) + double.Parse(OI_Sample_milk_sale_amount) + double.Parse(OI_Other) ; 
           // OI = double.Parse(txtOE_Payment_to_producer.Text) + double.Parse(txtOE_Head_load.Text) + double.Parse(txtOE_Camical_detergent.Text) + double.Parse(txtOE_Traveling.Text) + double.Parse(txtOE_Stationary.Text) + double.Parse(txtOE_Computer_expense.Text) + double.Parse(txtOE_Office_expense.Text) + double.Parse(txtOE_General_body_meeting.Text) + double.Parse(txtOE_STS_Secretary.Text) + double.Parse(txtOE_STS_Tester_helper.Text) + double.Parse(txtOE_STS_AHC_AIworker.Text) + double.Parse(txtOE_Other.Text);
            OE = double.Parse(OE_Payment_to_producer) + double.Parse(OE_Head_load) + double.Parse(OE_Camical_detergent ) + double.Parse(OE_Traveling) + double.Parse(OE_Stationary ) + double.Parse( OE_Computer_expense ) + double.Parse( OE_Office_expense ) + double.Parse( OE_General_body_meeting ) + double.Parse( OE_STS_Secretary ) + double.Parse( OE_STS_Tester_helper ) + double.Parse( OE_STS_AHC_AIworker ) + double.Parse( OE_Other);
            O_pl = (OI - OE).ToString();
            //lblTotal_operating_income.Text = Convert.ToString( OI);
           // lblTotal_operating_Expense.Text = Convert.ToString(OE);
           // txtTotal_Profit_Loss.Text = Convert.ToString(O_pl);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string Getcal(string OI_Milk_amount, string OI_DCS_Commition, string OI_Ghee_Commition, string OI_Cattle_feed_Commition, string OI_Miniral_mixture_Commition, string OI_Head_load, string OI_BMC_Chilling_charges, string OI_Local_milk_sale_amount, string OI_Sample_milk_sale_amount, string OI_Other, string OE_Payment_to_producer, string OE_Head_load, string OE_Camical_detergent, string OE_Traveling, string OE_Stationary, string OE_Computer_expense, string OE_Office_expense, string OE_General_body_meeting, string OE_STS_Secretary, string OE_STS_Tester_helper, string OE_STS_AHC_AIworker, string OE_Other)
    {
        if (HttpContext.Current != null)
        {
            Page page = (Page)HttpContext.Current.Handler as Page;
            TextBox txtTotal_Profit_Loss = (TextBox)page.FindControl("txtTotal_Profit_Loss");

            mis_DCSInformationSystem_MFI_By_DCS_Secretary obj = new mis_DCSInformationSystem_MFI_By_DCS_Secretary();
            obj.calculation( OI_Milk_amount ,OI_DCS_Commition ,OI_Ghee_Commition ,OI_Cattle_feed_Commition ,OI_Miniral_mixture_Commition ,OI_Head_load ,OI_BMC_Chilling_charges ,OI_Local_milk_sale_amount ,OI_Sample_milk_sale_amount ,OI_Other ,OE_Payment_to_producer ,OE_Head_load  ,OE_Camical_detergent ,OE_Traveling ,OE_Stationary  ,OE_Computer_expense ,OE_Office_expense ,OE_General_body_meeting ,OE_STS_Secretary  ,OE_STS_Tester_helper ,OE_STS_AHC_AIworker ,OE_Other  );
            // obj.Bind_DOABarGraph2(GUB, CostCentre, Location, Category, EVgroup, DOA, order, ltSc.Text);
           // txtTotal_Profit_Loss.Text = O_pl.ToString();
        }
        return O_pl;
        //  return html1;

    }

    public static string O_pl { get; set; }
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

        DataSet dsdetail = objdb.ByProcedure("usp_insertupdate_monthly_Finanacial_info_byDCS",
       new string[] { "flag", "Office_ID" },
         new string[] { "2", ddlSociety.SelectedValue.ToString() }, "dataset");
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
               // Response.Redirect("RMRD_Challan_EntryFromCanes.aspx", false);
            }
            ddlSocietyflt.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}