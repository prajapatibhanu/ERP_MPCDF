using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_MilkIssueForProduction : System.Web.UI.Page
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
                    GetSectionView();
                    FillShift();
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
            lblMsg.Text = "";
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

    private void GetSectionView()
    {

        try
        {

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID" },
                  new string[] { "6", ddlDS.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));

                ddlPS.DataSource = ds.Tables[0];
                ddlPS.DataTextField = "ProductSection_Name";
                ddlPS.DataValueField = "ProductSection_ID";
                ddlPS.DataBind();
                ddlPS.Items.Insert(0, new ListItem("All", "0"));


            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
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
            lblMsg.Text = "";
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

    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {
            GetOpeningStock();
            GetSectionDetail();
        }
        else
        {
            gvReceivedMilkDetail.DataSource = string.Empty;
            gvReceivedMilkDetail.DataBind();
            gvmttos.DataSource = string.Empty;
            gvmttos.DataBind();

        }

    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {
            GetOpeningStock();
            GetSectionDetail();
        }
        else
        {
            gvReceivedMilkDetail.DataSource = string.Empty;
            gvReceivedMilkDetail.DataBind();
            gvmttos.DataSource = string.Empty;
            gvmttos.DataBind();

        }
    }

    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {
            GetOpeningStock();
            GetSectionDetail();
        }
        else
        {
            gvReceivedMilkDetail.DataSource = string.Empty;
            gvReceivedMilkDetail.DataBind();
            gvmttos.DataSource = string.Empty;
            gvmttos.DataBind();

        }
    }

    private void GetOpeningStock()
    {

        try
        {
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            lblMsg.Text = "";

            DataSet dsGV = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "FDate", "ProductSection_ID" },
                  new string[] { "7", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue }, "dataset");

            if (dsGV != null && dsGV.Tables[0].Rows.Count > 0)
            {
                gvReceivedMilkDetail.DataSource = dsGV;
                gvReceivedMilkDetail.DataBind();
                ViewState["totalMQty"] = dsGV.Tables[0].Rows[0]["AvailableMilkInStock"].ToString();
                ViewState["totalFat"] = dsGV.Tables[0].Rows[0]["AvailableFATInStock"].ToString();
                ViewState["totalSnf"] = dsGV.Tables[0].Rows[0]["AvailableSNFInStock"].ToString();

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

            ds = null;

            int strcat_id = 0;

            if (ddlPSection.SelectedValue == "1")
            {
                strcat_id = 3;
            }
            if (ddlPSection.SelectedValue == "2")
            {
                strcat_id = 2;
            }

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }


            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "ProductSection_ID", "Date", "ItemCat_id" },
                  new string[] { "8", ddlDS.SelectedValue, ddlShift.SelectedValue, ddlPSection.SelectedValue, Fdate, strcat_id.ToString() }, "dataset");


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvmttos.DataSource = ds;
                gvmttos.DataBind();

                decimal MilkQty_InKG = 0;
                decimal MilkFAT_InKG = 0;
                decimal MilkSNF_InKG = 0;

                decimal PrvD_InPkt = 0;
                decimal PrvD_InLtr = 0;
                decimal CurD_InPkt = 0;
                decimal CurD_InLtr = 0;

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

                    if (txtgv_mqty.Text == "0.00")
                    {
                        txtgv_mqty.Text = "";
                    }

                    // Milk FAT 
                    TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
                    if (txtgvV_Fat.Text != "")
                    {
                        MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
                        Label lblTFQty = (gvmttos.FooterRow.FindControl("lblTFQty") as Label);
                        lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
                    }

                    if (txtgvV_Fat.Text == "0.000")
                    {
                        txtgvV_Fat.Text = "";
                    }

                    // Milk SNF 
                    TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");
                    if (txtgvV_Snf.Text != "")
                    {
                        MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
                        Label lblTSQty = (gvmttos.FooterRow.FindControl("lblTSQty") as Label);
                        lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
                    }

                    if (txtgvV_Snf.Text == "0.000")
                    {
                        txtgvV_Snf.Text = "";
                    }

                    Label lblPrev_Demand_InPkt = (Label)row.FindControl("lblPrev_Demand_InPkt");
                    Label Prev_Demand_InPkt_F = (gvmttos.FooterRow.FindControl("Prev_Demand_InPkt_F") as Label);

                    if (lblPrev_Demand_InPkt.Text != "")
                    {
                        PrvD_InPkt += Convert.ToDecimal(lblPrev_Demand_InPkt.Text);
                        Prev_Demand_InPkt_F.Text = PrvD_InPkt.ToString("0.00");
                    }


                    Label lblPrev_DemandInLtr = (Label)row.FindControl("lblPrev_DemandInLtr");
                    Label lblPrev_DemandInLtr_F = (gvmttos.FooterRow.FindControl("lblPrev_DemandInLtr_F") as Label);


                    if (lblPrev_DemandInLtr.Text != "")
                    {
                        PrvD_InLtr += Convert.ToDecimal(lblPrev_DemandInLtr.Text);
                        lblPrev_DemandInLtr_F.Text = PrvD_InLtr.ToString("0.00");
                    }

                    Label lblCurrent_Demand_InPkt = (Label)row.FindControl("lblCurrent_Demand_InPkt");
                    Label lblCurrent_Demand_InPkt_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_InPkt_F") as Label);


                    if (lblCurrent_Demand_InPkt.Text != "")
                    {
                        CurD_InPkt += Convert.ToDecimal(lblCurrent_Demand_InPkt.Text);
                        lblCurrent_Demand_InPkt_F.Text = CurD_InPkt.ToString("0.00");
                    }

                    Label lblCurrent_Demand_InLtr = (Label)row.FindControl("lblCurrent_Demand_InLtr");
                    Label lblCurrent_Demand_InLtr_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_InLtr_F") as Label);


                    if (lblCurrent_Demand_InLtr.Text != "")
                    {
                        CurD_InLtr += Convert.ToDecimal(lblCurrent_Demand_InLtr.Text);
                        lblCurrent_Demand_InLtr_F.Text = CurD_InLtr.ToString("0.00");
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

    protected void txtgv_mqty_TextChanged(object sender, EventArgs e)
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
                    TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
                    TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");
                    Label lblItem_FAT_RatioPer = (Label)row.FindControl("lblItem_FAT_RatioPer");
                    Label lblItem_SNF_RatioPer = (Label)row.FindControl("lblItem_SNF_RatioPer");

                    if (txtgv_mqty.Text != "")
                    {
                        MilkQty_InKG += Convert.ToDecimal(txtgv_mqty.Text);
                        Label lblTMQty = (gvmttos.FooterRow.FindControl("lblTMQty") as Label);
                        lblTMQty.Text = MilkQty_InKG.ToString("0.00");

                    }

                    if (txtgv_mqty.Text != "" && lblItem_FAT_RatioPer.Text != "")
                    {
                        MilkFAT_InKG = (Convert.ToDecimal(txtgv_mqty.Text) * Convert.ToDecimal(lblItem_FAT_RatioPer.Text)) / 100;
                        txtgvV_Fat.Text = MilkFAT_InKG.ToString("0.000");

                        MilkSNF_InKG = (Convert.ToDecimal(txtgv_mqty.Text) * Convert.ToDecimal(lblItem_SNF_RatioPer.Text)) / 100;
                        txtgvV_Snf.Text = MilkSNF_InKG.ToString("0.000");
                    }
                    else
                    {
                        txtgvV_Fat.Text = "";
                        txtgvV_Snf.Text = "";
                    }


                    //// Milk FAT 
                    //TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
                    //if (txtgvV_Fat.Text != "")
                    //{
                    //    MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
                    //    Label lblTFQty = (gvmttos.FooterRow.FindControl("lblTFQty") as Label);
                    //    lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
                    //}

                    //// Milk SNF 
                    //TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");
                    //if (txtgvV_Snf.Text != "")
                    //{
                    //    MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
                    //    Label lblTSQty = (gvmttos.FooterRow.FindControl("lblTSQty") as Label);
                    //    lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
                    //}

                }


                if (MilkQty_InKG > Convert.ToDecimal(ViewState["totalMQty"].ToString()))
                {

                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued Milk Qty Can't Greter Then Available Milk Qty.");
                }

                //if (MilkFAT_InKG > Convert.ToDecimal(ViewState["totalFat"].ToString()))
                //{
                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued FAT Qty Can't Greter Then Available FAT Qty.");
                //}

                //if (MilkSNF_InKG > Convert.ToDecimal(ViewState["totalSnf"].ToString()))
                //{
                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued SNF Qty Can't Greter Then Available SNF Qty.");
                //}


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

    protected void txtgvV_Fat_TextChanged(object sender, EventArgs e)
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


                if (MilkQty_InKG > Convert.ToDecimal(ViewState["totalMQty"].ToString()))
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued Milk Qty Can't Greter Then Available Milk Qty.");
                }

                if (MilkFAT_InKG > Convert.ToDecimal(ViewState["totalFat"].ToString()))
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued FAT Qty Can't Greter Then Available FAT Qty.");
                }

                if (MilkSNF_InKG > Convert.ToDecimal(ViewState["totalSnf"].ToString()))
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued SNF Qty Can't Greter Then Available SNF Qty.");
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

    protected void txtgvV_Snf_TextChanged(object sender, EventArgs e)
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


                if (MilkQty_InKG > Convert.ToDecimal(ViewState["totalMQty"].ToString()))
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued Milk Qty Can't Greter Then Available Milk Qty.");
                }

                if (MilkFAT_InKG > Convert.ToDecimal(ViewState["totalFat"].ToString()))
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued FAT Qty Can't Greter Then Available FAT Qty.");
                }

                if (MilkSNF_InKG > Convert.ToDecimal(ViewState["totalSnf"].ToString()))
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued SNF Qty Can't Greter Then Available SNF Qty.");
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

    private DataTable GetMilkProcess()
    {
        decimal TFAT1 = 0;
        decimal MQTY1 = 0;
        decimal TSNF1 = 0;


        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ProductSection_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("Cr", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Fat", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Snf", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));


        foreach (GridViewRow row in gvmttos.Rows)
        {
            Label lblItemType_id = (Label)row.FindControl("lblItemType_id");
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
                dr[0] = ddlPSection.SelectedValue;
                dr[1] = MQTY1;
                dr[2] = TFAT1;
                dr[3] = TSNF1;
                dr[4] = lblItemType_id.Text;
                dt.Rows.Add(dr);
            }


        }

        return dt;

    }

    protected void lbgettotal_Click(object sender, EventArgs e)
    {
        try
        {
            decimal MilkQty_InKG = 0;
            decimal MilkFAT_InKG = 0;
            decimal MilkSNF_InKG = 0;
            decimal PrvD_InPkt = 0;
            decimal PrvD_InLtr = 0;
            decimal CurD_InPkt = 0;
            decimal CurD_InLtr = 0;

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


                    Label lblPrev_Demand_InPkt = (Label)row.FindControl("lblPrev_Demand_InPkt");
                    Label Prev_Demand_InPkt_F = (gvmttos.FooterRow.FindControl("Prev_Demand_InPkt_F") as Label);

                    if (lblPrev_Demand_InPkt.Text != "")
                    {
                        PrvD_InPkt += Convert.ToDecimal(lblPrev_Demand_InPkt.Text);
                        Prev_Demand_InPkt_F.Text = PrvD_InPkt.ToString("0.00");
                    }


                    Label lblPrev_DemandInLtr = (Label)row.FindControl("lblPrev_DemandInLtr");
                    Label lblPrev_DemandInLtr_F = (gvmttos.FooterRow.FindControl("lblPrev_DemandInLtr_F") as Label);


                    if (lblPrev_DemandInLtr.Text != "")
                    {
                        PrvD_InLtr += Convert.ToDecimal(lblPrev_DemandInLtr.Text);
                        lblPrev_DemandInLtr_F.Text = PrvD_InLtr.ToString("0.00");
                    }

                    Label lblCurrent_Demand_InPkt = (Label)row.FindControl("lblCurrent_Demand_InPkt");
                    Label lblCurrent_Demand_InPkt_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_InPkt_F") as Label);


                    if (lblCurrent_Demand_InPkt.Text != "")
                    {
                        CurD_InPkt += Convert.ToDecimal(lblCurrent_Demand_InPkt.Text);
                        lblCurrent_Demand_InPkt_F.Text = CurD_InPkt.ToString("0.00");
                    }

                    Label lblCurrent_Demand_InLtr = (Label)row.FindControl("lblCurrent_Demand_InLtr");
                    Label lblCurrent_Demand_InLtr_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_InLtr_F") as Label);


                    if (lblCurrent_Demand_InLtr.Text != "")
                    {
                        CurD_InLtr += Convert.ToDecimal(lblCurrent_Demand_InLtr.Text);
                        lblCurrent_Demand_InLtr_F.Text = CurD_InLtr.ToString("0.00");
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

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                int MILKQTY = 0;
                int TFAT = 0;
                int TSNF = 0;

                foreach (GridViewRow row in gvmttos.Rows)
                {

                    TextBox txtgv_mqty = (TextBox)row.FindControl("txtgv_mqty");
                    TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
                    TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");

                    if (txtgv_mqty.Text == "0" || txtgv_mqty.Text == "0.00" || txtgv_mqty.Text == "0.0")
                    {
                        MILKQTY = 1;
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

                if (MILKQTY == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Milk Qty Can't Blank or '0' or '0.00' or '0.0'");
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

                        //// Milk FAT 
                        //TextBox txtgvV_Fat = (TextBox)row.FindControl("txtgvV_Fat");
                        //if (txtgvV_Fat.Text != "")
                        //{
                        //    MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
                        //    Label lblTFQty = (gvmttos.FooterRow.FindControl("lblTFQty") as Label);
                        //    lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
                        //}

                        //// Milk SNF 
                        //TextBox txtgvV_Snf = (TextBox)row.FindControl("txtgvV_Snf");
                        //if (txtgvV_Snf.Text != "")
                        //{
                        //    MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
                        //    Label lblTSQty = (gvmttos.FooterRow.FindControl("lblTSQty") as Label);
                        //    lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
                        //}

                    }

                }


                if (MilkQty_InKG > Convert.ToDecimal(ViewState["totalMQty"].ToString()))
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued Milk Qty Can't Greter Then Available Milk Qty.");
                    return;
                }

                //if (MilkFAT_InKG > Convert.ToDecimal(ViewState["totalFat"].ToString()))
                //{
                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued FAT Qty Can't Greter Then Available FAT Qty.");
                //    return;
                //}

                //if (MilkSNF_InKG > Convert.ToDecimal(ViewState["totalSnf"].ToString()))
                //{
                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Issued SNF Qty Can't Greter Then Available SNF Qty.");
                //    return;
                //}


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
                                              new string[] { "9"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                              , ddlShift.SelectedValue.ToString()
                                              , ViewState["Emp_ID"].ToString() 
                                    },
                                             new string[] { "type_Production_Milk_InOut_Child" },
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


                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

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
                  new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID" },
                  new string[] { "18", ddlDS.SelectedValue, ddlshiftFilter.SelectedValue, Fdate, ddlPS.SelectedValue.ToString() }, "dataset");

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

    protected void ddlPS_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSectionDetailHistory();
    }
    protected void txtdateFilter_TextChanged(object sender, EventArgs e)
    {
        GetSectionDetailHistory();
    }
    protected void ddlshiftFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSectionDetailHistory();
    }

}