using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;


public partial class mis_MilkCollection_SocietywiseMilkCollectionPassbook : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillSociety();
                txtFdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                txtTdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");


                ddlItemBillingHead_Type.ClearSelection();
                ddlHeaddetails.Items.Clear();
                ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
                gv_HeadDetails.DataSource = string.Empty;
                gv_HeadDetails.DataBind();


            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void FillSociety()
    {
        try
        {

            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSociety.DataTextField = "Office_Name";
                    ddlSociety.DataValueField = "Office_ID";
                    ddlSociety.DataSource = ds;
                    ddlSociety.DataBind();
                    ddlSociety.Enabled = false;

                }
                else
                {
                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                lblmsgshow.Visible = false;
                string Fdate = "";
                string Tdate = "";

                if (txtFdt.Text != "")
                {
                    Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                }

                if (txtTdt.Text != "")
                {
                    Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                }

                ds = null;

                if (objdb.OfficeType_ID() == "5")
                {
                    ds = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                           new string[] { "flag", "FDT", "TDT", "OfficeId" },
                           new string[] { "19", Fdate, Tdate, ddlSociety.SelectedValue }, "dataset");
                }
                if (objdb.OfficeType_ID() == "6")
                {
                    ds = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                           new string[] { "flag", "FDT", "TDT", "OfficeId" },
                           new string[] { "14", Fdate, Tdate, ddlSociety.SelectedValue }, "dataset");
                }



                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            lblSociety.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString() + " / " + ds.Tables[0].Rows[0]["Office_Code"].ToString();
                            lblbankInfo.Text = ds.Tables[0].Rows[0]["IFSC"].ToString() + " / " + ds.Tables[0].Rows[0]["BankName"].ToString() + " / " + ds.Tables[0].Rows[0]["BankAccountNo"].ToString();
                            lblOfficename.Text = ds.Tables[0].Rows[0]["AttachUnitName"].ToString() + " / " + ds.Tables[0].Rows[0]["AttachUnitCode"].ToString();
                            lblBillingPeriod.Text = ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString();

                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + lblSociety.Text + "');", true);

                            if (ds.Tables[1].Rows.Count != 0)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {

                                    FS_DailyReport.Visible = true;
                                    gv_SocietyMilkDispatchDetail.DataSource = ds.Tables[1];
                                    gv_SocietyMilkDispatchDetail.DataBind();

                                    decimal MilkQty_InKG = 0;
                                    decimal FAT_IN_KG = 0;
                                    decimal SNF_IN_KG = 0;
                                    decimal Value = 0;

                                    decimal MilkValue = 0;

                                    foreach (GridViewRow row in gv_SocietyMilkDispatchDetail.Rows)
                                    {
                                        Label lblI_MilkSupplyQty = (Label)row.FindControl("lblI_MilkSupplyQty");

                                        if (lblI_MilkSupplyQty.Text != "")
                                        {
                                            MilkQty_InKG += Convert.ToDecimal(lblI_MilkSupplyQty.Text);
                                            Label lblI_MilkSupplyQtyTotal = (gv_SocietyMilkDispatchDetail.FooterRow.FindControl("lblI_MilkSupplyQtyTotal") as Label);
                                            lblI_MilkSupplyQtyTotal.Text = MilkQty_InKG.ToString("0.00");
                                        }

                                        Label lblFAT_IN_KG = (Label)row.FindControl("lblFAT_IN_KG");

                                        if (lblFAT_IN_KG.Text != "")
                                        {
                                            FAT_IN_KG += Convert.ToDecimal(lblFAT_IN_KG.Text);
                                            Label lblTotal_FAT_IN_KG = (gv_SocietyMilkDispatchDetail.FooterRow.FindControl("lblTotal_FAT_IN_KG") as Label);
                                            lblTotal_FAT_IN_KG.Text = FAT_IN_KG.ToString("0.000");
                                        }



                                        Label lblSNF_IN_KG = (Label)row.FindControl("lblSNF_IN_KG");

                                        if (lblSNF_IN_KG.Text != "")
                                        {
                                            SNF_IN_KG += Convert.ToDecimal(lblSNF_IN_KG.Text);
                                            Label lblTotal_SNF_IN_KG = (gv_SocietyMilkDispatchDetail.FooterRow.FindControl("lblTotal_SNF_IN_KG") as Label);
                                            lblTotal_SNF_IN_KG.Text = SNF_IN_KG.ToString("0.00");
                                        }

                                        Label lblValue = (Label)row.FindControl("lblValue");

                                        if (lblValue.Text != "")
                                        {
                                            Value += Convert.ToDecimal(lblValue.Text);
                                            MilkValue += Convert.ToDecimal(lblValue.Text);
                                            Label lblNetValue = (gv_SocietyMilkDispatchDetail.FooterRow.FindControl("lblNetValue") as Label);
                                            lblNetValue.Text = Value.ToString("0.00");
                                        }

                                    }


                                    lblCommission.Text = (FAT_IN_KG * 7).ToString();
                                    lblMilkValue.Text = MilkValue.ToString("0.00");
                                    lblGrossEarning.Text = (MilkValue + Convert.ToDecimal(lblCommission.Text)).ToString();

                                     if (ViewState["InsertRecord"] != null)
                                    {
                                    DataTable dtadhead = ViewState["InsertRecord"] as DataTable;

                                    DataRow dr; 
                                    dr = dtadhead.NewRow();
                                    dr[0] = dtadhead.Rows.Count + 1;
                                    dr[1] = "DEDUCTION";
                                    dr[2] = "0";
                                    dr[3] = "N.R.D.";
                                    dr[4] = (FAT_IN_KG * 1).ToString();
                                    dtadhead.Rows.Add(dr);

                                     

                                    grhradsdetails.DataSource = dtadhead;
                                    grhradsdetails.DataBind(); 
                                    decimal Prodvalue = 0;
                                    Label lblGrandTotals = (grhradsdetails.FooterRow.FindControl("lblGrandTotal") as Label);

                                    foreach (GridViewRow rowcc in grhradsdetails.Rows)
                                    {
                                        Label lblTotalPrice = (Label)rowcc.FindControl("lblTotalPrice");
                                        Label lblItemBillingHead_Type = (Label)rowcc.FindControl("lblItemBillingHead_Type");

                                        if (lblTotalPrice.Text != "")
                                        {
                                            if (lblItemBillingHead_Type.Text == "ADDITION")
                                            {
                                                Prodvalue += Decimal.Parse(lblTotalPrice.Text);
                                            }
                                            if (lblItemBillingHead_Type.Text == "DEDUCTION")
                                            {
                                                Prodvalue -= Decimal.Parse(lblTotalPrice.Text);
                                            }
                                        }

                                    }
                                    lblGrandTotals.Text = Prodvalue.ToString("N2");


                                    lbldeductionadditionValue.Text = Prodvalue.ToString("N2");

                                    lblnetamount.Text = (Convert.ToDecimal(lblGrossEarning.Text) + (Prodvalue)).ToString();

									}
                                    else
                                    {

                                        DataTable dt = new DataTable();
                                        DataRow dr;
                                        dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                                        dt.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                                        dt.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                                        dt.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                                        dt.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));


                                        dr = dt.NewRow();
                                        dr[0] = 1;
                                        dr[1] = "DEDUCTION";
                                        dr[2] = "0";
                                        dr[3] = "N.R.D.";
                                        dr[4] = (FAT_IN_KG * 1).ToString();
                                        dt.Rows.Add(dr);

                                        grhradsdetails.DataSource = dt;
                                        grhradsdetails.DataBind();
                                        decimal Prodvalue = 0;
                                        Label lblGrandTotals = (grhradsdetails.FooterRow.FindControl("lblGrandTotal") as Label);

                                        foreach (GridViewRow rowcc in grhradsdetails.Rows)
                                        {
                                            Label lblTotalPrice = (Label)rowcc.FindControl("lblTotalPrice");
                                            Label lblItemBillingHead_Type = (Label)rowcc.FindControl("lblItemBillingHead_Type");

                                            if (lblTotalPrice.Text != "")
                                            {
                                                if (lblItemBillingHead_Type.Text == "ADDITION")
                                                {
                                                    Prodvalue += Decimal.Parse(lblTotalPrice.Text);
                                                }
                                                if (lblItemBillingHead_Type.Text == "DEDUCTION")
                                                {
                                                    Prodvalue -= Decimal.Parse(lblTotalPrice.Text);
                                                }
                                            }

                                        }

                                        lblGrandTotals.Text = Prodvalue.ToString("N2");

                                        lbldeductionadditionValue.Text = Prodvalue.ToString("N2");

                                        lblnetamount.Text = (Convert.ToDecimal(lblGrossEarning.Text) + (Prodvalue)).ToString();
                                    }


                                    //if (ds.Tables[5].Rows.Count != 0)
                                    //{
                                    //    if (ds.Tables[5].Rows.Count > 0)
                                    //    {
                                    //        lblProductSaleValue.Text = ds.Tables[5].Rows[0]["NetPurchaseAmount"].ToString();
                                    //    }
                                    //    else
                                    //    {
                                    //        lblProductSaleValue.Text = "0.00";
                                    //    }

                                    //}
                                    //else
                                    //{
                                    //    lblProductSaleValue.Text = "0.00";
                                    //}

                                    //lblGrossEarning.Text = (Convert.ToDecimal(lblMilkValue.Text) - Convert.ToDecimal(lblProductSaleValue.Text)).ToString();

                                    if (ds.Tables[2].Rows.Count != 0)
                                    {
                                        if (ds.Tables[2].Rows.Count > 0)
                                        {
                                            FS_DailyReport_Shift.Visible = true;
                                            gv_SocietyMorningData.DataSource = ds.Tables[2];
                                            gv_SocietyMorningData.DataBind();

                                        }

                                        else
                                        {
                                           // FS_DailyReport_Shift.Visible = false;
                                            gv_SocietyMorningData.DataSource = string.Empty;
                                            gv_SocietyMorningData.DataBind();
                                        }
                                    }

                                    else
                                    {
                                        //FS_DailyReport_Shift.Visible = false;
                                        gv_SocietyMorningData.DataSource = string.Empty;
                                        gv_SocietyMorningData.DataBind();
                                    }


                                    if (ds.Tables[3].Rows.Count != 0)
                                    {
                                        if (ds.Tables[3].Rows.Count > 0)
                                        {
                                            FS_DailyReport_Shift.Visible = true;
                                            gv_SocietyEveningData.DataSource = ds.Tables[3];
                                            gv_SocietyEveningData.DataBind();
                                        }
                                        else
                                        {
                                            //FS_DailyReport_Shift.Visible = false;
                                            gv_SocietyEveningData.DataSource = string.Empty;
                                            gv_SocietyEveningData.DataBind();
                                        }
                                    }
                                    else
                                    {
                                       // FS_DailyReport_Shift.Visible = false;
                                        gv_SocietyEveningData.DataSource = string.Empty;
                                        gv_SocietyEveningData.DataBind();
                                    }

                                }

                                else
                                {
                                    lblmsgshow.Visible = true;
                                    lblmsgshow.Text = "No Record Found";
                                    FS_DailyReport_Shift.Visible = false;
                                    FS_DailyReport.Visible = false;
                                    gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                                    gv_SocietyMilkDispatchDetail.DataBind();
                                }
                            }

                            else
                            {
                                lblmsgshow.Visible = true;
                                lblmsgshow.Text = "No Record Found";
                                FS_DailyReport_Shift.Visible = false;
                                FS_DailyReport.Visible = false;
                                gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                                gv_SocietyMilkDispatchDetail.DataBind();
                            }

                        }
                        else
                        {
                            lblmsgshow.Visible = true;
                            lblmsgshow.Text = "No Record Found";
                            FS_DailyReport_Shift.Visible = false;
                            FS_DailyReport.Visible = false;
                            gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                            gv_SocietyMilkDispatchDetail.DataBind();
                        }
                    }

                    else
                    {
                        lblmsgshow.Visible = true;
                        lblmsgshow.Text = "No Record Found";
                        FS_DailyReport_Shift.Visible = false;
                        FS_DailyReport.Visible = false;
                        gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                        gv_SocietyMilkDispatchDetail.DataBind();
                    }


                }
                else
                {
                    lblmsgshow.Visible = true;
                    lblmsgshow.Text = "No Record Found";
                    lblSociety.Text = "";
                    lblbankInfo.Text = "";
                    lblOfficename.Text = "";
                    lblBillingPeriod.Text = "";
                    FS_DailyReport_Shift.Visible = false;
                    FS_DailyReport.Visible = false;

                }

            } 

            gv_HeadDetails.DataSource = string.Empty;
            gv_HeadDetails.DataBind();
            ViewState["InsertRecord"] = null;
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void gv_SocietyMorningData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int RowSpan = 3;
        for (int i = gv_SocietyMorningData.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = gv_SocietyMorningData.Rows[i];
            GridViewRow prevRow = gv_SocietyMorningData.Rows[i + 1];
            if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
    }

    protected void gv_SocietyEveningData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int RowSpan = 3;
        for (int i = gv_SocietyEveningData.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = gv_SocietyEveningData.Rows[i];
            GridViewRow prevRow = gv_SocietyEveningData.Rows[i + 1];
            if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
    }


    protected void btnaddhead_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("SocietywiseMilkCollectionPassbook.aspx", false);
    }

    protected void btnAddHeadsDetails_Click(object sender, EventArgs e)
    {
        lblPopupMsg.Text = "";
        AddMCUDetails();
    }

    private void AddMCUDetails()
    {
        try
        {
            int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));

                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlItemBillingHead_Type.SelectedValue;
                dr[2] = ddlHeaddetails.SelectedValue;
                dr[3] = ddlHeaddetails.SelectedItem.Text;
                dr[4] = txtHeadAmount.Text;
                dt.Rows.Add(dr);
                ViewState["InsertRecord"] = dt;
                gv_HeadDetails.DataSource = dt;
                gv_HeadDetails.DataBind();

            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));

                DT = (DataTable)ViewState["InsertRecord"];

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlHeaddetails.SelectedValue == DT.Rows[i]["ItemBillingHead_ID"].ToString())
                    {
                        CompartmentType = 1;
                    }
                }

                if (CompartmentType == 1)
                {
                    lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Head \"" + ddlHeaddetails.SelectedItem.Text + "\" already exist.");
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = 1;
                    dr[1] = ddlItemBillingHead_Type.SelectedValue;
                    dr[2] = ddlHeaddetails.SelectedValue;
                    dr[3] = ddlHeaddetails.SelectedItem.Text;
                    dr[4] = txtHeadAmount.Text;

                    dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord"] = dt;
                gv_HeadDetails.DataSource = dt;
                gv_HeadDetails.DataBind();
            }

            ddlItemBillingHead_Type.ClearSelection();
            ddlHeaddetails.Items.Clear();
            ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
            txtHeadAmount.Text = "";
            GetAmountTotal();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

        }

        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }

    }

    protected void lnkDeleteHead_Click(object sender, EventArgs e)
    {
        try
        {
            lblPopupMsg.Text = "";
            GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt3 = ViewState["InsertRecord"] as DataTable;
            dt3.Rows.Remove(dt3.Rows[row1.RowIndex]);
            ViewState["InsertRecord"] = dt3;
            gv_HeadDetails.DataSource = dt3;
            gv_HeadDetails.DataBind();
            ddlItemBillingHead_Type.ClearSelection();
            ddlHeaddetails.Items.Clear();
            ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
            txtHeadAmount.Text = "";
            GetAmountTotal();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
    }

    private void GetAmountTotal()
    {

        try
        {
            decimal dPageTotal = 0;

            Label lblGrandTotal = (gv_HeadDetails.FooterRow.FindControl("lblGrandTotal") as Label);


            foreach (GridViewRow rowcc in gv_HeadDetails.Rows)
            {
                Label lblTotalPrice = (Label)rowcc.FindControl("lblTotalPrice");
                Label lblItemBillingHead_Type = (Label)rowcc.FindControl("lblItemBillingHead_Type");

                if (lblTotalPrice.Text != "")
                {
                    if (lblItemBillingHead_Type.Text == "ADDITION")
                    {
                        dPageTotal += Decimal.Parse(lblTotalPrice.Text);
                    }
                    if (lblItemBillingHead_Type.Text == "DEDUCTION")
                    {
                        dPageTotal -= Decimal.Parse(lblTotalPrice.Text);
                    }
                }

            }

            lblGrandTotal.Text = dPageTotal.ToString("N2");

        }
        catch (Exception)
        {

            throw;
        }


    }

    protected void ddlHeaddetails_Init(object sender, EventArgs e)
    {
        try
        {
            ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
    }

    protected void ddlItemBillingHead_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblPopupMsg.Text = "";

            if (ddlItemBillingHead_Type.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
               new string[] { "flag", "ItemBillingHead_Type" },
               new string[] { "8", ddlItemBillingHead_Type.SelectedValue }, "dataset");

                ddlHeaddetails.DataTextField = "ItemBillingHead_Name";
                ddlHeaddetails.DataValueField = "ItemBillingHead_ID";
                ddlHeaddetails.DataSource = ds;
                ddlHeaddetails.DataBind();
                ddlHeaddetails.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlItemBillingHead_Type.ClearSelection();
                ddlHeaddetails.Items.Clear();
                ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }


    }

    protected void btnSearchHeadDetails_Click(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
    }
}