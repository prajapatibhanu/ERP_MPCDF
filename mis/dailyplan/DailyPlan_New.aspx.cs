using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_DailyPlan_New : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    string Fdate = "";

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
                    txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                    FillOffice();
                    FillShift();
                    GetSectionView();
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
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));

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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            int strcat_id = 0;

            if (ddlPSection.SelectedValue == "1")
            {
                strcat_id = 3;
            }
            if (ddlPSection.SelectedValue == "2")
            {
                strcat_id = 2;
            }

            lblMsg.Text = "";

            DataSet dsGV = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                  new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ItemCat_id", "ProductSection_ID" },
                  new string[] { "13", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, strcat_id.ToString(), ddlPSection.SelectedValue }, "dataset");

            if (dsGV != null && dsGV.Tables[0].Rows.Count > 0)
            {
                gvReceivedMilkDetail.DataSource = dsGV.Tables[0];
                gvReceivedMilkDetail.DataBind();
                FSDEMANDDETAIl.Visible = true; 

                decimal PrvD_InPkt = 0;
                decimal PrvD_InLtr = 0;
                decimal CurD_InPkt = 0;
                decimal CurD_InLtr = 0;
                decimal CurD_InLtrtenpersurplus = 0;

                foreach (GridViewRow row in gvReceivedMilkDetail.Rows)
                {

                    Label lblPrev_Demand_InPkt = (Label)row.FindControl("lblPrev_Demand_InPkt");
                    Label Prev_Demand_InPkt_F = (gvReceivedMilkDetail.FooterRow.FindControl("Prev_Demand_InPkt_F") as Label);

                    if (lblPrev_Demand_InPkt.Text != "")
                    {
                        PrvD_InPkt += Convert.ToDecimal(lblPrev_Demand_InPkt.Text);
                        Prev_Demand_InPkt_F.Text = PrvD_InPkt.ToString("0.00");
                    }


                    Label lblPrev_DemandInLtr = (Label)row.FindControl("lblPrev_DemandInLtr");
                    Label lblPrev_DemandInLtr_F = (gvReceivedMilkDetail.FooterRow.FindControl("lblPrev_DemandInLtr_F") as Label);


                    if (lblPrev_DemandInLtr.Text != "")
                    {
                        PrvD_InLtr += Convert.ToDecimal(lblPrev_DemandInLtr.Text);
                        lblPrev_DemandInLtr_F.Text = PrvD_InLtr.ToString("0.00");
                    }

                    Label lblCurrent_Demand_InPkt = (Label)row.FindControl("lblCurrent_Demand_InPkt");
                    Label lblCurrent_Demand_InPkt_F = (gvReceivedMilkDetail.FooterRow.FindControl("lblCurrent_Demand_InPkt_F") as Label);


                    if (lblCurrent_Demand_InPkt.Text != "")
                    {
                        CurD_InPkt += Convert.ToDecimal(lblCurrent_Demand_InPkt.Text);
                        lblCurrent_Demand_InPkt_F.Text = CurD_InPkt.ToString("0.00");
                    }

                    Label lblCurrent_Demand_InLtr = (Label)row.FindControl("lblCurrent_Demand_InLtr");
                    Label lblCurrent_Demand_InLtr_F = (gvReceivedMilkDetail.FooterRow.FindControl("lblCurrent_Demand_InLtr_F") as Label);


                    if (lblCurrent_Demand_InLtr.Text != "")
                    {
                        CurD_InLtr += Convert.ToDecimal(lblCurrent_Demand_InLtr.Text);
                        lblCurrent_Demand_InLtr_F.Text = CurD_InLtr.ToString("0.00");
                    }


                    Label lblTenPerSurPlusQty = (Label)row.FindControl("lblTenPerSurPlusQty");
                    Label lblTenPerSurPlusQty_F = (gvReceivedMilkDetail.FooterRow.FindControl("lblTenPerSurPlusQty_F") as Label);


                    if (lblTenPerSurPlusQty.Text != "")
                    {
                        CurD_InLtrtenpersurplus += Convert.ToDecimal(lblTenPerSurPlusQty.Text);
                        lblTenPerSurPlusQty_F.Text = CurD_InLtrtenpersurplus.ToString("0.00");
                    }



                }

            }
            else
            {
                FSDEMANDDETAIl.Visible = false;
                gvReceivedMilkDetail.DataSource = string.Empty;
                gvReceivedMilkDetail.DataBind();
            }

            ViewRequiredInventory();
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        try
        {

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkbtnVN = (LinkButton)gvReceivedMilkDetail.Rows[selRowIndex].FindControl("LinkButton3");

            Label lblTenPerSurPlusQty = (Label)gvReceivedMilkDetail.Rows[selRowIndex].FindControl("lblTenPerSurPlusQty");
            Label lblItem_id = (Label)gvReceivedMilkDetail.Rows[selRowIndex].FindControl("lblItem_id");
            Label lblCurrent_Demand_InPkt = (Label)gvReceivedMilkDetail.Rows[selRowIndex].FindControl("lblCurrent_Demand_InPkt");

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster", new string[] { "flag", "Office_ID", "ProductQty", "Product_ID", "ProductSection_ID", "Item_id", "TotalDemandInPacket" },
                new string[] { "10", objdb.Office_ID(), lblTenPerSurPlusQty.Text, lblItem_id.Text, ddlPSection.SelectedValue, lblItem_id.Text, lblCurrent_Demand_InPkt.Text }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gv_RecipeInfo.DataSource = ds.Tables[0];
                gv_RecipeInfo.DataBind();

                gvpackagingMaterial.DataSource = ds.Tables[1];
                gvpackagingMaterial.DataBind();


            }
            else
            {
                gv_RecipeInfo.DataSource = string.Empty;
                gv_RecipeInfo.DataBind();

                gvpackagingMaterial.DataSource = string.Empty;
                gvpackagingMaterial.DataBind();

            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CCModelF()", true);

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
    }

    private DataTable GetItemDemandDetail()
    {

        decimal D_Qty = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("Item_Id", typeof(int)));
        dt.Columns.Add(new DataColumn("PD_Pkt", typeof(int)));
        dt.Columns.Add(new DataColumn("PD_Ltr", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CD_Pkt", typeof(int)));
        dt.Columns.Add(new DataColumn("CD_Ltr", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CD_Ltr10PerSurplus", typeof(decimal)));

        foreach (GridViewRow row in gvReceivedMilkDetail.Rows)
        {
            Label lblItem_id = (Label)row.FindControl("lblItem_id");
            Label lblPrev_Demand_InPkt = (Label)row.FindControl("lblPrev_Demand_InPkt");
            Label lblPrev_DemandInLtr = (Label)row.FindControl("lblPrev_DemandInLtr");
            Label lblCurrent_Demand_InPkt = (Label)row.FindControl("lblCurrent_Demand_InPkt");
            Label lblCurrent_Demand_InLtr = (Label)row.FindControl("lblCurrent_Demand_InLtr");
            Label lblTenPerSurPlusQty = (Label)row.FindControl("lblTenPerSurPlusQty");

            if (lblTenPerSurPlusQty.Text != "")
            {
                D_Qty = Convert.ToDecimal(lblTenPerSurPlusQty.Text);

                if (D_Qty > 0)
                {
                    dr = dt.NewRow();
                    dr[0] = lblItem_id.Text;
                    dr[1] = lblPrev_Demand_InPkt.Text;
                    dr[2] = lblPrev_DemandInLtr.Text;
                    dr[3] = lblCurrent_Demand_InPkt.Text;
                    dr[4] = lblCurrent_Demand_InLtr.Text;
                    dr[5] = lblTenPerSurPlusQty.Text;
                    dt.Rows.Add(dr);
                }
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


                DataTable dtDD = new DataTable();
                dtDD = GetItemDemandDetail();

                DataTable dtID = new DataTable();
                DataTable dtIDFinal = new DataTable();
                dtIDFinal.Columns.Add(new DataColumn("Variant_Id", typeof(int)));
                dtIDFinal.Columns.Add(new DataColumn("Varient_Qty", typeof(decimal)));
                dtIDFinal.Columns.Add(new DataColumn("VarientUnit_Id", typeof(int)));
                dtIDFinal.Columns.Add(new DataColumn("VarientInventoryType", typeof(int)));
                DataRow dr1;

                if (dtDD.Rows.Count > 0)
                {

                    foreach (DataRow row in dtDD.Rows)
                    {
                        ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster",

                        new string[] { "flag", "Office_ID", "ProductQty", "Product_ID",
                            "ProductSection_ID", "Item_id", "TotalDemandInPacket" },
                        new string[] { "10", objdb.Office_ID(), 
                            row["CD_Ltr10PerSurplus"].ToString(), 
                            row["Item_Id"].ToString(),
                            ddlPSection.SelectedValue, row["Item_Id"].ToString(),
                            row["CD_Pkt"].ToString() }, "dataset");

                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            dtID = null;
                            dtID = ds.Tables[0];
                            foreach (DataRow row1 in dtID.Rows)
                            {
                                dr1 = dtIDFinal.NewRow();
                                dr1[0] = row1["Item_id"].ToString();
                                dr1[1] = row1["TotalQty"].ToString();
                                dr1[2] = row1["Unit_id"].ToString();
                                dr1[3] = 1;
                                dtIDFinal.Rows.Add(dr1);
                            }

                        }

                        if (ds != null && ds.Tables[1].Rows.Count > 0)
                        {
                            dtID = null;
                            dtID = ds.Tables[1];
                            foreach (DataRow row1 in dtID.Rows)
                            {
                                dr1 = dtIDFinal.NewRow();
                                dr1[0] = row1["Item_id"].ToString();
                                dr1[1] = row1["QtyInKg"].ToString();
                                dr1[2] = row1["Unit_id"].ToString();
                                dr1[3] = 2;
                                dtIDFinal.Rows.Add(dr1);
                            }

                        }

                    }
                }

                if (dtDD.Rows.Count > 0 && dtIDFinal.Rows.Count > 0)
                {

                    ds = null;
                    ds = objdb.ByProcedure("SpProductionDailyPlan",
                                              new string[] { "flag" 
				                                ,"Office_ID"
                                                ,"ProductSection_ID"
				                                ,"Date" 
				                                ,"Shift_Id" 
				                                ,"CreatedBy"
                                                ,"CreatedBy_IP"
                                    },
                                              new string[] { "0"
                                              ,objdb.Office_ID()
                                              ,ddlPSection.SelectedValue
                                              ,Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                              ,ddlShift.SelectedValue.ToString()
                                              , ViewState["Emp_ID"].ToString() 
                                              ,objdb.GetLocalIPAddress()
                                    },
                                             new string[] { "Type_tbl_Production_Milk_DailyPlanMasterChild", "Type_tbl_Production_Milk_DailyPlanMasterChildInventory" },
                                             new DataTable[] { dtDD, dtIDFinal }, "TableSave");

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you! Record Save Successfully", "");
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "EXISTS")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Daily Production Plan Already Generated For This Date Or Shift Please See The Report", "");
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Record already Exist.", "");
                        }
                    }

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Data Can't Insert Because No Demand For This Date & Shift Also Recipe No Generate!");
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

    private void ViewRequiredInventory()
    {

        try
        {
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            DataSet dsPPRD = objdb.ByProcedure("SpProductionDailyPlan",
                  new string[] { "flag", "Office_ID", "ProductSection_ID", "Date", "Shift_Id" },
                  new string[] { "1", ddlDS.SelectedValue, ddlPSection.SelectedValue, Fdate, ddlShift.SelectedValue },
                  "dataset");

            if (dsPPRD != null && dsPPRD.Tables[0].Rows.Count > 0)
            {
                reportdiv.Visible = true; 
                gvRID.DataSource = dsPPRD;
                gvRID.DataBind();

                lblDcsname.Text = ddlDS.SelectedItem.Text;
                lbldate.Text = Convert.ToDateTime(txtDate.Text, cult).ToString("dd/MM/yyyy");
                lblshift.Text = ddlShift.SelectedItem.Text;
                lblProductionSection.Text = ddlPSection.SelectedItem.Text;

            }
            else
            {
                reportdiv.Visible = false;
                gvRID.DataSource = string.Empty;
                gvRID.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }



}