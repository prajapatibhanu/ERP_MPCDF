 using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_RMRD_ReceivedTankerOutBySecurity_RMRD : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    CultureInfo cult = new CultureInfo("en-IN", true);
    decimal Totalmilkqty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            txtArrivalDate.Text = System.DateTime.Now.ToString();

            if (!IsPostBack)
            {
                txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }
                fs_action.Visible = false;
                btnSubmit.Visible = false;
                GetReferenceInfo();

                GetViewReceivedTankerDetails();

                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    #region User Defined Function
    private void GetReferenceInfo()
    {

        DataSet ds1 = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                                  new string[] { "flag", "I_OfficeID" },
                                  new string[] { "20", objdb.Office_ID() }, "dataset");
        if (ds1 != null && ds1.Tables[0].Rows.Count != 0)
        {

            ddlReferenceNo.DataSource = ds1;
            ddlReferenceNo.DataTextField = "C_ReferenceNo";
            ddlReferenceNo.DataValueField = "BI_MilkInOutRMRDRefID";
            ddlReferenceNo.DataBind();
            ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlReferenceNo.DataSource = string.Empty;
            ddlReferenceNo.DataBind();
            ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
        }

    }
    protected void ViewSampleInfo()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                                      new string[] { "flag", "BI_MilkInOutRMRDRefID" },
                                      new string[] { "9", ddlReferenceNo.SelectedValue }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_ViewChallanDetail.Visible = true;
                        gv_ViewChallanDetail.DataSource = ds;
                        gv_ViewChallanDetail.DataBind();
                        fs_action.Visible = true;
                        btnSubmit.Visible = true;

                    }

                    else
                    {
                        gv_ViewChallanDetail.Visible = false;
                        gv_ViewChallanDetail.DataSource = string.Empty;
                        gv_ViewChallanDetail.DataBind();
                        fs_action.Visible = false;
                        btnSubmit.Visible = false;
                    }

                }
                else
                {
                    gv_ViewChallanDetail.Visible = false;
                    gv_ViewChallanDetail.DataSource = string.Empty;
                    gv_ViewChallanDetail.DataBind();
                    fs_action.Visible = false;
                    btnSubmit.Visible = false;
                }
            }
            else
            {
                gv_ViewChallanDetail.Visible = false;
                gv_ViewChallanDetail.DataSource = string.Empty;
                gv_ViewChallanDetail.DataBind();
                fs_action.Visible = false;
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    private DataTable GetMilkQualityDetails()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("I_EntryID", typeof(string)));
        dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(decimal)));

        foreach (GridViewRow row in gv_ViewChallanDetail.Rows)
        {
            Label lblI_EntryID = (Label)row.FindControl("lblI_EntryID");
            Label lblV_SealLocation = (Label)row.FindControl("lblV_SealLocation");
            TextBox lblI_MilkQuantity = (TextBox)row.FindControl("txtI_MilkQuantity");

            dr = dt.NewRow();
            dr[0] = lblI_EntryID.Text;
            dr[1] = lblV_SealLocation.Text;
            dr[2] = lblI_MilkQuantity.Text;
            dt.Rows.Add(dr);
        }
        return dt;
    }
    protected void GetViewReceivedTankerDetails()
    {
        try
        {
            ds = null;
            string date = "";

            if (txtDate.Text != "")
            {
                date = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                     new string[] { "flag", "I_OfficeID", "D_Date" },
                     new string[] { "15", objdb.Office_ID(), date }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_TodayReceivedTankerDetails.DataSource = ds;
                        gv_TodayReceivedTankerDetails.DataBind();

                    }
                    else
                    {
                        gv_TodayReceivedTankerDetails.DataSource = null;
                        gv_TodayReceivedTankerDetails.DataBind();
                    }
                }
                else
                {
                    gv_TodayReceivedTankerDetails.DataSource = null;
                    gv_TodayReceivedTankerDetails.DataBind();
                }
            }
            else
            {
                gv_TodayReceivedTankerDetails.DataSource = null;
                gv_TodayReceivedTankerDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    public string EntryAtQCValidations()
    {
        string Status = "1";
        try
        {
            ds = null;

            string Count1 = "0";
            string Count2 = "0";


            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                     new string[] { "flag", "I_EntryID" },
                     new string[] { "11", ddlReferenceNo.SelectedValue }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Count1 = ds.Tables[0].Rows[0]["MCCOUNT"].ToString();

                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        Count2 = ds.Tables[1].Rows[0]["MQCOUNT"].ToString();

                    }
                }

            }
            if (Count1 != "0" && Count2 != "0")
            {
                if (Count1 == Count2)
                {
                    Status = "0";
                }
                else
                {
                    Status = "1";
                }
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
        return Status;
    }
    protected void GetViewRefDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                     new string[] { "flag", "I_OfficeID", "BI_MilkInOutRefID" },
                     new string[] { "12", objdb.Office_ID(), ddlReferenceNo.SelectedValue }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_viewreferenceno.Visible = false;
                        gv_viewreferenceno.DataSource = null;
                        gv_viewreferenceno.DataBind();

                    }

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    #endregion

    #region change Event
    protected void ddlReferenceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            
            if (ddlReferenceNo.SelectedValue != "0")
            {
                string Strvalid = EntryAtQCValidations();
                if (Strvalid == "0")
                {
                    DataSet ds1 = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                              new string[] { "flag", "BI_MilkInOutRMRDRefID" },
                              new string[] { "8", ddlReferenceNo.SelectedValue }, "dataset");

                    if (ds1.Tables[0].Rows.Count != 0)
                    {
                        fs_action.Visible = true;
                        btnSubmit.Visible = true;
                        txtD_GrossWeight.Text = ds1.Tables[0].Rows[0]["D_GrossWeight"].ToString();
                        txtD_GrossWeight.Enabled = false;
                        txtReceiptNo.Text = ds1.Tables[0].Rows[0]["WeightReceiptNo"].ToString();
                        ddlTankerType.SelectedValue = ds1.Tables[0].Rows[0]["V_VehicleType"].ToString();
                        txtArrivalDate.Text = ds1.Tables[0].Rows[0]["DT_TankerArrivalDate"].ToString();
                        div_SealVerification_Single_Challan.Visible = true;
                        //if (ds1.Tables[0].Rows.Count == 2) //In Case 2 Challan For Front and Rear Chamber Case
                        //{
                        //    div_SealVerification_Single_Challan.Visible = true;

                        //}
                        //else if (ds1.Tables[0].Rows.Count == 1) //In Case 1 Challan For Front/Rear/Single Chamber Case
                        //{
                        //    div_SealVerification_Single_Challan.Visible = true;
                        //}
                        //else
                        //{
                        //    div_SealVerification_Single_Challan.Visible = false;
                        //}


                        //int CCMQCOUNT = 0; int DSMQCOUNT = 0; string SL = "";

                        //DataSet GetMQualityINOut = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                        //                 new string[] { "flag", "BI_MilkInOutRefID" },
                        //                 new string[] { "9", ddlReferenceNo.SelectedValue }, "dataset");

                        //if (GetMQualityINOut != null)
                        //{
                        //    if (GetMQualityINOut.Tables.Count > 0)
                        //    {
                        //        if (GetMQualityINOut.Tables[0].Rows.Count > 0)
                        //        {
                        //            CCMQCOUNT = GetMQualityINOut.Tables[0].Rows.Count;
                        //        }

                        //        if (GetMQualityINOut.Tables[1].Rows.Count > 0)
                        //        {
                        //            DSMQCOUNT = GetMQualityINOut.Tables[1].Rows.Count;

                        //        }

                        //        if (CCMQCOUNT != DSMQCOUNT)
                        //        {

                        //            SL = GetMQualityINOut.Tables[1].Rows[0]["V_SealLocation"].ToString();

                        //            if (SL == "F")
                        //            {
                        //                SL = "Rear";
                        //            }
                        //            else if (SL == "R")
                        //            {
                        //                SL = "Front";
                        //            }
                        //            else
                        //            {
                        //                SL = "Singal";
                        //            }

                        //            gv_ViewChallanDetail.DataSource = string.Empty;
                        //            gv_ViewChallanDetail.DataBind();
                        //            div_SealVerification_Single_Challan.Visible = false;
                        //            fs_action.Visible = false;
                        //            btnSubmit.Visible = false;
                        //            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Reference No - " + ddlReferenceNo.SelectedItem.Text + " QC Entry is Still Pending (" + SL + ") Chamber !");
                        //            return;

                        //        }



                        //    }

                        //}







                    }
                    else
                    {
                        gv_ViewChallanDetail.DataSource = string.Empty;
                        gv_ViewChallanDetail.DataBind();
                        div_SealVerification_Single_Challan.Visible = false;
                        fs_action.Visible = false;
                        btnSubmit.Visible = false;
                    }

                    //GetViewRefDetails();
                    ViewSampleInfo();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "QC Entry is not completed yet");
                }
            }
            else
            {
                gv_ViewChallanDetail.DataSource = string.Empty;
                gv_ViewChallanDetail.DataBind();
                div_SealVerification_Single_Challan.Visible = false;
                fs_action.Visible = false;
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }   
    protected void txtI_MilkQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            foreach (GridViewRow row in gv_ViewChallanDetail.Rows)
            {
                TextBox txtI_MilkQuantity = (TextBox)row.FindControl("txtI_MilkQuantity");

                if (txtI_MilkQuantity.Text != "")
                {
                    Totalmilkqty += Convert.ToDecimal(txtI_MilkQuantity.Text);

                    Label lblTotal_I_MilkQuantity = (gv_ViewChallanDetail.FooterRow.FindControl("lblTotal_I_MilkQuantity") as Label);

                    lblTotal_I_MilkQuantity.Text = Totalmilkqty.ToString("0.00");
                    ViewState["Total_I_MilkQuantity"] = Totalmilkqty.ToString("0.00");

                    if (Convert.ToDecimal(txtD_GrossWeight.Text) <= Totalmilkqty)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Net Weight : \"" + Totalmilkqty + "\" Should not be greater then Gross Weight  \"" + txtD_GrossWeight.Text + "\".");
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "InValid Milk Quantity (In KG)");
                    return;
                }


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 17:" + ex.Message.ToString());
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        GetViewReceivedTankerDetails();
    }
    #endregion

    #region Button Event
    protected void btnYes_Click(object sender, EventArgs e)
    {

        try
        {
            if (ddlReferenceNo.SelectedIndex > 0)
            {
                lblMsg.Text = "";

                foreach (GridViewRow row in gv_ViewChallanDetail.Rows)
                {
                    TextBox lblI_MilkQuantity = (TextBox)row.FindControl("txtI_MilkQuantity");

                    if (lblI_MilkQuantity.Text == "" || lblI_MilkQuantity.Text == "0.00" || lblI_MilkQuantity.Text == "0"|| lblI_MilkQuantity.Text == "00")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Net Weight Can't Acceptable Blank , 0 , 0.00. Please Enter Valid Milk Quantity (In KG)");
                        return;
                    }

                }

                DataTable dt = new DataTable();
                dt = GetMilkQualityDetails();


                if (btnSubmit.Text == "Update")
                {

                    if (dt.Rows.Count > 0)
                    {
                        
                        
                           
                            int Count = dt.Rows.Count;
                            for (int i = 0; i < Count; i++)
                            {
                                DataRow dr = dt.Rows[i];
                                string I_MilkQuantity = dr["I_MilkQuantity"].ToString();
                                string I_EntryID = dr["I_EntryID"].ToString();
                                string V_SealLocation = dr["V_SealLocation"].ToString();
                               objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD", new string[] { "flag", "I_EntryID", "I_MilkQuantity", "V_SealLocation" }, new string[] { "12", I_EntryID, I_MilkQuantity, V_SealLocation }, "dataset");
                            }
                            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                            new string[] { "flag", "BI_MilkInOutRMRDRefID", "D_NetWeight", "I_OfficeID" },
                            new string[] { "13", ddlReferenceNo.SelectedValue, ViewState["Total_I_MilkQuantity"].ToString(),objdb.Office_ID() },
                            "dataset");

                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                Session["IsSuccess"] = true;
                                Response.Redirect("ReceivedTankerOutBySecurity_RMRD.aspx", false);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                        
                       
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Net Weight Can't Acceptable Blank , 0 , 0.00. Please Enter Valid Milk Quantity (In KG)");
                        return;
                    }


                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Please select Reference Number.!");
                return;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceivedTankerOutBySecurity.aspx");
    }
    #endregion

}