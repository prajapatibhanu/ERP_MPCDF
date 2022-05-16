using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


public partial class mis_dailyplan_MilkTransferToSections : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    string Fdate = "";
    string Tdate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    FillShift();
                    GetOpeningStock();
                    GetSectionDetail();
                    GetSectionDetailHistory();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void FillOffice()
    {
        try
        {
            //lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDS.Enabled = false;

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillShift()
    {
        try
        {
            //lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                 new string[] { "flag" },
                 new string[] { "2" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataTextField = "Name";
                ddlShift.DataValueField = "Shift_Id";
                ddlShift.DataBind();
                ddlShift.SelectedValue = ds.Tables[1].Rows[0]["Shift_Id"].ToString();
                txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");
                //ddlShift.Enabled = false;
                //txtDate.Enabled = false;

                ddlshiftFilter.DataSource = ds.Tables[0];
                ddlshiftFilter.DataTextField = "Name";
                ddlshiftFilter.DataValueField = "Shift_Id";
                ddlshiftFilter.DataBind();
                ddlshiftFilter.Items.Insert(0, new ListItem("All", "0"));
                txtdateFilter.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        GetOpeningStock();
        GetSectionDetail();
    }

    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOpeningStock();
        GetSectionDetail();
    }

    private void GetOpeningStock()
    {

        try
        {
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            //lblMsg.Text = "";

            DataSet dsGV = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "FDate" },
                  new string[] { "3", objdb.Office_ID(), ddlShift.SelectedValue, Fdate }, "dataset");

            if (dsGV != null && dsGV.Tables[0].Rows.Count > 0)
            {
                gvReceivedMilkDetail.DataSource = dsGV;
                gvReceivedMilkDetail.DataBind();

                ViewState["totalMQty"] = dsGV.Tables[0].Rows[0]["AvailableMilkInStock"].ToString();
                ViewState["totalFat"] = dsGV.Tables[0].Rows[0]["AvailableFATInStock"].ToString();
                ViewState["totalSnf"] = dsGV.Tables[0].Rows[0]["AvailableSNFInStock"].ToString();

                decimal MilkQty_InKG = 0;

            }
            else
            {
                gvReceivedMilkDetail.DataSource = string.Empty;
                gvReceivedMilkDetail.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }





    private void GetSectionDetail()
    {

        try
        {

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }


            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "Date" },
                  new string[] { "4", ddlDS.SelectedValue, ddlShift.SelectedValue, Fdate }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvmttos.DataSource = ds;
                gvmttos.DataBind();

                decimal MilkQty_InKG = 0;
                decimal MilkFAT_InKG = 0;
                decimal MilkSNF_InKG = 0;

                foreach (GridViewRow row in gvmttos.Rows)
                {
                    // Milk QTY  
                    TextBox txtgv_mqty = (TextBox)row.FindControl("txtgv_mqty");
                    if (txtgv_mqty.Text != "")
                    {
                        MilkQty_InKG += Convert.ToDecimal(txtgv_mqty.Text);
                        Label lblTMQty = (gvmttos.FooterRow.FindControl("lblTMQty") as Label);
                        lblTMQty.Text = MilkQty_InKG.ToString("0.00");
                    }

                    // Milk FAT 
                    TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
                    if (txtgvV_Fat.Text != "")
                    {
                        MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
                        Label lblTFQty = (gvmttos.FooterRow.FindControl("lblTFQty") as Label);
                        lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
                    }

                    // Milk SNF 
                    TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgv_mqty");
                    if (txtgvV_Snf.Text != "")
                    {
                        MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
                        Label lblTSQty = (gvmttos.FooterRow.FindControl("lblTSQty") as Label);
                        lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
                    }

                }



            }
            else
            {
                gvmttos.DataSource = string.Empty;
                gvmttos.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    //protected void txtgv_mqty_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        decimal MilkQty_InKG = 0;
    //        decimal MilkFAT_InKG = 0;
    //        decimal MilkSNF_InKG = 0;


    //        if (ViewState["totalMQty"] != null && ViewState["totalFat"] != null && ViewState["totalSnf"] != null)
    //        {

    //            foreach (GridViewRow row in gvmttos.Rows)
    //            {
    //                // Milk QTY  
    //                TextBox txtgv_mqty = (TextBox)row.FindControl("txtgv_mqty");
    //                if (txtgv_mqty.Text != "")
    //                {
    //                    MilkQty_InKG += Convert.ToDecimal(txtgv_mqty.Text);
    //                    Label lblTMQty = (gvmttos.FooterRow.FindControl("lblTMQty") as Label);
    //                    lblTMQty.Text = MilkQty_InKG.ToString("0.00");
    //                }

    //                // Milk FAT 
    //                TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
    //                if (txtgvV_Fat.Text != "")
    //                {
    //                    MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
    //                    Label lblTFQty = (gvmttos.FooterRow.FindControl("lblTFQty") as Label);
    //                    lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
    //                }

    //                // Milk SNF 
    //                TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");
    //                if (txtgvV_Snf.Text != "")
    //                {
    //                    MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
    //                    Label lblTSQty = (gvmttos.FooterRow.FindControl("lblTSQty") as Label);
    //                    lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
    //                }

    //            }


    //            if (MilkQty_InKG > Convert.ToDecimal(ViewState["totalMQty"].ToString()))
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer Milk Qty To Section Can't Greter Then Available Milk Qty.");
    //            }

    //            if (MilkFAT_InKG > Convert.ToDecimal(ViewState["totalFat"].ToString()))
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer FAT Qty To Section Can't Greter Then Available FAT Qty.");
    //            }

    //            if (MilkSNF_InKG > Convert.ToDecimal(ViewState["totalSnf"].ToString()))
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer SNF Qty To Section Can't Greter Then Available SNF Qty.");
    //            }


    //        }

    //        else
    //        {
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Something went wrong");
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }

    //}

    //protected void txtgvV_Fat_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        decimal MilkQty_InKG = 0;
    //        decimal MilkFAT_InKG = 0;
    //        decimal MilkSNF_InKG = 0;


    //        if (ViewState["totalMQty"] != null && ViewState["totalFat"] != null && ViewState["totalSnf"] != null)
    //        {

    //            foreach (GridViewRow row in gvmttos.Rows)
    //            {
    //                // Milk QTY  
    //                TextBox txtgv_mqty = (TextBox)row.FindControl("txtgv_mqty");
    //                if (txtgv_mqty.Text != "")
    //                {
    //                    MilkQty_InKG += Convert.ToDecimal(txtgv_mqty.Text);
    //                    Label lblTMQty = (gvmttos.FooterRow.FindControl("lblTMQty") as Label);
    //                    lblTMQty.Text = MilkQty_InKG.ToString("0.00");
    //                }

    //                // Milk FAT 
    //                TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
    //                if (txtgvV_Fat.Text != "")
    //                {
    //                    MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
    //                    Label lblTFQty = (gvmttos.FooterRow.FindControl("lblTFQty") as Label);
    //                    lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
    //                }

    //                // Milk SNF 
    //                TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");
    //                if (txtgvV_Snf.Text != "")
    //                {
    //                    MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
    //                    Label lblTSQty = (gvmttos.FooterRow.FindControl("lblTSQty") as Label);
    //                    lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
    //                }

    //            }


    //            if (MilkQty_InKG > Convert.ToDecimal(ViewState["totalMQty"].ToString()))
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer Milk Qty To Section Can't Greter Then Available Milk Qty.");
    //            }

    //            if (MilkFAT_InKG > Convert.ToDecimal(ViewState["totalFat"].ToString()))
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer FAT Qty To Section Can't Greter Then Available FAT Qty.");
    //            }

    //            if (MilkSNF_InKG > Convert.ToDecimal(ViewState["totalSnf"].ToString()))
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer SNF Qty To Section Can't Greter Then Available SNF Qty.");
    //            }

    //        }

    //        else
    //        {
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Something went wrong");
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void txtgvV_Snf_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        decimal MilkQty_InKG = 0;
    //        decimal MilkFAT_InKG = 0;
    //        decimal MilkSNF_InKG = 0;


    //        if (ViewState["totalMQty"] != null && ViewState["totalFat"] != null && ViewState["totalSnf"] != null)
    //        {



    //            foreach (GridViewRow row in gvmttos.Rows)
    //            {
    //                // Milk QTY  
    //                TextBox txtgv_mqty = (TextBox)row.FindControl("txtgv_mqty");
    //                if (txtgv_mqty.Text != "")
    //                {
    //                    MilkQty_InKG += Convert.ToDecimal(txtgv_mqty.Text);
    //                    Label lblTMQty = (gvmttos.FooterRow.FindControl("lblTMQty") as Label);
    //                    lblTMQty.Text = MilkQty_InKG.ToString("0.00");
    //                }

    //                // Milk FAT 
    //                TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
    //                if (txtgvV_Fat.Text != "")
    //                {
    //                    MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
    //                    Label lblTFQty = (gvmttos.FooterRow.FindControl("lblTFQty") as Label);
    //                    lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
    //                }

    //                // Milk SNF 
    //                TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");
    //                if (txtgvV_Snf.Text != "")
    //                {
    //                    MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
    //                    Label lblTSQty = (gvmttos.FooterRow.FindControl("lblTSQty") as Label);
    //                    lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
    //                }

    //            }


    //            if (MilkQty_InKG > Convert.ToDecimal(ViewState["totalMQty"].ToString()))
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer Milk Qty To Section Can't Greter Then Available Milk Qty.");
    //            }

    //            if (MilkFAT_InKG > Convert.ToDecimal(ViewState["totalFat"].ToString()))
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer FAT Qty To Section Can't Greter Then Available FAT Qty.");
    //            }

    //            if (MilkSNF_InKG > Convert.ToDecimal(ViewState["totalSnf"].ToString()))
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer SNF Qty To Section Can't Greter Then Available SNF Qty.");
    //            }

    //        }

    //        else
    //        {
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Something went wrong");
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    private DataTable GetMilkProcess()
    {
        decimal TFAT1 = 0;
        decimal MQTY1 = 0;
        decimal TSNF1 = 0;


        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ProductSection_ID", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Dr", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Fat_Dr", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Snf_Dr", typeof(decimal)));


        foreach (GridViewRow row in gvmttos.Rows)
        {
            Label lblProductSection_ID = (Label)row.FindControl("lblProductSection_ID");
            TextBox txtgv_mqty = (TextBox)row.FindControl("txtgv_mqty");
            TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
            TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");

            if (txtgv_mqty.Text != "0" && txtgv_mqty.Text != "0.00" && txtgv_mqty.Text != "0.0" && txtgv_mqty.Text != "")
            {
                MQTY1 = Convert.ToDecimal(txtgv_mqty.Text);
            }
            else
            {
                MQTY1 = 0;
            }

            if (txtgvV_Fat.Text != "0" && txtgvV_Fat.Text != "0.00" && txtgvV_Fat.Text != "0.0" && txtgvV_Fat.Text != "")
            {
                TFAT1 = Convert.ToDecimal(txtgvV_Fat.Text);
            }
            else
            {
                TFAT1 = 0;
            }

            if (txtgvV_Snf.Text != "0" && txtgvV_Snf.Text != "0.00" && txtgvV_Snf.Text != "0.0" && txtgvV_Snf.Text != "0.000" && txtgvV_Snf.Text != "")
            {
                TSNF1 = Convert.ToDecimal(txtgvV_Snf.Text);
            }
            else
            {
                TSNF1 = 0;
            }

            if (MQTY1 != 0)
            {
                dr = dt.NewRow();
                dr[0] = lblProductSection_ID.Text;
                dr[1] = MQTY1;
                dr[2] = TFAT1;
                dr[3] = TSNF1;
                dt.Rows.Add(dr);
            }


        }

        return dt;

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                int MQTY = 0;
                int TFAT = 0; 
                int TSNF = 0;

                foreach (GridViewRow row in gvmttos.Rows)
                {

                    TextBox txtgv_mqty = (TextBox)row.FindControl("txtgv_mqty");
                    TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
                    TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");

                    if (txtgv_mqty.Text == "0" || txtgv_mqty.Text == "0.00" || txtgv_mqty.Text == "0.0")
                    {
                        MQTY = 1;
                    }

                    //if (txtgvV_Fat.Text == "0" || txtgvV_Fat.Text == "0.00" || txtgvV_Fat.Text == "0.0")
                    //{
                    //    TFAT = 1;
                    //}

                    //if (txtgvV_Snf.Text == "0" || txtgvV_Snf.Text == "0.00" || txtgvV_Snf.Text == "0.0")
                    //{
                    //    TSNF = 1;
                    //}

                }

                if (MQTY == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "	Milk Qty Can't Blank or '0' or '0.00' or '0.0'");
                    return;
                }

                //if (TFAT == 1)
                //{
                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "FAT Can't Blank or '0' or '0.00' or '0.0'");
                //    return;
                //}
                 
                //if (TSNF == 1)
                //{
                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "SNF Can't Blank or '0' or '0.00' or '0.0'");
                //    return;
                //}


                DataTable dtIDF = new DataTable();
                dtIDF = GetMilkProcess();



                decimal MilkQty_InKG = 0;
                decimal MilkFAT_InKG = 0;
                decimal MilkSNF_InKG = 0;


                if (ViewState["totalMQty"] != null && ViewState["totalFat"] != null && ViewState["totalSnf"] != null)
                {

                    foreach (GridViewRow row in gvmttos.Rows)
                    {
                        // Milk QTY  
                        TextBox txtgv_mqty = (TextBox)row.FindControl("txtgv_mqty");
                        if (txtgv_mqty.Text != "")
                        {
                            MilkQty_InKG += Convert.ToDecimal(txtgv_mqty.Text);
                            Label lblTMQty = (gvmttos.FooterRow.FindControl("lblTMQty") as Label);
                            lblTMQty.Text = MilkQty_InKG.ToString("0.00");
                        }

                        // Milk FAT 
                        TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
                        if (txtgvV_Fat.Text != "")
                        {
                            MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
                            Label lblTFQty = (gvmttos.FooterRow.FindControl("lblTFQty") as Label);
                            lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
                        }

                        // Milk SNF 
                        TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");
                        if (txtgvV_Snf.Text != "")
                        {
                            MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
                            Label lblTSQty = (gvmttos.FooterRow.FindControl("lblTSQty") as Label);
                            lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
                        }

                    }

                }

                if (MilkQty_InKG > Convert.ToDecimal(ViewState["totalMQty"].ToString()))
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer Milk Qty To Section Can't Greter Then Available Milk Qty.");
                    return;
                }

                if (MilkFAT_InKG > Convert.ToDecimal(ViewState["totalFat"].ToString()))
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer FAT Qty To Section Can't Greter Then Available FAT Qty.");
                    return;
                }

                if (MilkSNF_InKG > Convert.ToDecimal(ViewState["totalSnf"].ToString()))
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Transfer SNF Qty To Section Can't Greter Then Available SNF Qty.");
                    return;
                }


                if (dtIDF.Rows.Count > 0)
                {

                    ds = null;
                    ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                                              new string[] { "Flag" 
				                                ,"Office_ID"
				                                ,"Date" 
				                                ,"Shift_Id" 
				                                ,"CreatedBy" 
                                    },
                                              new string[] { "5"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                              , ddlShift.SelectedValue.ToString()
                                              , ViewState["Emp_ID"].ToString() 
                                    },
                                             new string[] { "type_Production_Milk_InOut" },
                                             new DataTable[] { dtIDF }, "TableSave");

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you! Record Save Successfully", "");
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Record already Exist.", "");
                        }
                    }


                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Data Can't Insert Without Any Value");
                    return;
                }


                FillOffice();
                FillShift();
                GetOpeningStock();
                GetSectionDetail();

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }


    protected void lbgettotal_Click(object sender, EventArgs e)
    {
        try
        {
            decimal MilkQty_InKG = 0;
            decimal MilkFAT_InKG = 0;
            decimal MilkSNF_InKG = 0;


            if (ViewState["totalMQty"] != null && ViewState["totalFat"] != null && ViewState["totalSnf"] != null)
            {

                foreach (GridViewRow row in gvmttos.Rows)
                {
                    // Milk QTY  
                    TextBox txtgv_mqty = (TextBox)row.FindControl("txtgv_mqty");
                    if (txtgv_mqty.Text != "")
                    {
                        MilkQty_InKG += Convert.ToDecimal(txtgv_mqty.Text);
                        Label lblTMQty = (gvmttos.FooterRow.FindControl("lblTMQty") as Label);
                        lblTMQty.Text = MilkQty_InKG.ToString("0.00");
                    }

                    // Milk FAT 
                    TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
                    if (txtgvV_Fat.Text != "")
                    {
                        MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
                        Label lblTFQty = (gvmttos.FooterRow.FindControl("lblTFQty") as Label);
                        lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
                    }

                    // Milk SNF 
                    TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");
                    if (txtgvV_Snf.Text != "")
                    {
                        MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
                        Label lblTSQty = (gvmttos.FooterRow.FindControl("lblTSQty") as Label);
                        lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
                    }

                }

            }

            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Something went wrong");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    private void GetSectionDetailHistory()
    {

        try
        {

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtdateFilter.Text, cult).ToString("yyyy/MM/dd");
            }


            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "Date" },
                  new string[] { "15", ddlDS.SelectedValue, ddlshiftFilter.SelectedValue, Fdate }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GVMTHistory.DataSource = ds;
                GVMTHistory.DataBind();

                decimal MilkQty_InKG = 0;
                decimal MilkFAT_InKG = 0;
                decimal MilkSNF_InKG = 0;

                foreach (GridViewRow row in GVMTHistory.Rows)
                {
                    // Milk QTY  
                    Label txtgv_mqty = (Label)row.FindControl("txtgv_mqty");
                    if (txtgv_mqty.Text != "")
                    {
                        MilkQty_InKG += Convert.ToDecimal(txtgv_mqty.Text);
                        Label lblTMQty = (GVMTHistory.FooterRow.FindControl("lblTMQty") as Label);
                        lblTMQty.Text = MilkQty_InKG.ToString("0.00");
                    }

                    // Milk FAT 
                    Label txtgvV_Fat = (Label)row.FindControl("txtgvV_Fat");
                    if (txtgvV_Fat.Text != "")
                    {
                        MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
                        Label lblTFQty = (GVMTHistory.FooterRow.FindControl("lblTFQty") as Label);
                        lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
                    }

                    // Milk SNF 
                    Label txtgvV_Snf = (Label)row.FindControl("txtgv_mqty");
                    if (txtgvV_Snf.Text != "")
                    {
                        MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
                        Label lblTSQty = (GVMTHistory.FooterRow.FindControl("lblTSQty") as Label);
                        lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
                    }

                }



            }
            else
            {
                GVMTHistory.DataSource = string.Empty;
                GVMTHistory.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }


    protected void ddlshiftFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSectionDetailHistory();
    }
    protected void txtdateFilter_TextChanged(object sender, EventArgs e)
    {
        GetSectionDetailHistory();
    }


    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            lblMsg.Text = "";

            DataSet dsGV1 = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "FDate" },
                  new string[] { "19", objdb.Office_ID(), ddlShift.SelectedValue, Fdate }, "dataset");

            if (dsGV1.Tables.Count > 0)
            {
                GVCC.DataSource = dsGV1.Tables[0];
                GVCC.DataBind();

                decimal MilkQty_InKG = 0;
                decimal MilkFAT_InKG = 0;
                decimal MilkSNF_InKG = 0;

                decimal MilkQty_InKGDr = 0;
                decimal MilkFAT_InKGDr = 0;
                decimal MilkSNF_InKGDr = 0;

                foreach (GridViewRow row in GVCC.Rows)
                {
                    // Milk QTY  
                    Label txtgv_mqty = (Label)row.FindControl("lblCr");
                    if (txtgv_mqty.Text != "")
                    {
                        MilkQty_InKG += Convert.ToDecimal(txtgv_mqty.Text);
                        Label lblTMQty = (GVCC.FooterRow.FindControl("lblTMQty") as Label);
                        lblTMQty.Text = MilkQty_InKG.ToString("0.00");
                    }

                    // Milk FAT 
                    Label txtgvV_Fat = (Label)row.FindControl("lblFat");
                    if (txtgvV_Fat.Text != "")
                    {
                        MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
                        Label lblTFQty = (GVCC.FooterRow.FindControl("lblTFQty") as Label);
                        lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
                    }

                    // Milk SNF 
                    Label txtgvV_Snf = (Label)row.FindControl("lblSnf");
                    if (txtgvV_Snf.Text != "")
                    {
                        MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
                        Label lblTSQty = (GVCC.FooterRow.FindControl("lblTSQty") as Label);
                        lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
                    }


                    // For Dr 


                    Label lblDr = (Label)row.FindControl("lblDr");
                    if (lblDr.Text != "")
                    {
                        MilkQty_InKGDr += Convert.ToDecimal(lblDr.Text);
                        Label lblTMQtyDr = (GVCC.FooterRow.FindControl("lblTMQtyDr") as Label);
                        lblTMQtyDr.Text = MilkQty_InKGDr.ToString("0.00");
                    }

                    // Milk FAT 
                    Label lblFat_Dr = (Label)row.FindControl("lblFat_Dr");
                    if (lblFat_Dr.Text != "")
                    {
                        MilkFAT_InKGDr += Convert.ToDecimal(lblFat_Dr.Text);
                        Label lblTFat_Dr = (GVCC.FooterRow.FindControl("lblTFat_Dr") as Label);
                        lblTFat_Dr.Text = MilkFAT_InKGDr.ToString("0.000");
                    }

                    // Milk SNF 
                    Label lblSnf_Dr = (Label)row.FindControl("lblSnf_Dr");
                    if (lblSnf_Dr.Text != "")
                    {
                        MilkSNF_InKGDr += Convert.ToDecimal(lblSnf_Dr.Text);
                        Label lblTSnf_Dr = (GVCC.FooterRow.FindControl("lblTSnf_Dr") as Label);
                        lblTSnf_Dr.Text = MilkSNF_InKGDr.ToString("0.000");
                    }

                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CCModelF()", true);
            }


        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
         
    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MilkTransferToSections.aspx", false);
    }

}