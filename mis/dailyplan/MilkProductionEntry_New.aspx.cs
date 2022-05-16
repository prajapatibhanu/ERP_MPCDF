using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_MilkProductionEntry_New : System.Web.UI.Page
{
	static DataSet ds5, ds6;
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
                    txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
					FillBMCRoot();
                    GetSectionView();
                    SetInFlowInitialRow();
                    SetInFlowGoatMilkInitialRow();
                    SetInFlowCansInitialRow();
                    SetOutFlowIcecreamInitialRow();
                    SetOutFlowInitialRow();
                    SetOutFlowIssuetoOtherInitialRow();
                    SetBMCDCSCollectionInitialRow();
                    SetRecvdFromOtherUnionInitialRow();
                    SetForPowderConversionInitialRow();
                    SetIssuetoPowderPlantInitialRow();
                    FillShift();
                    FillSheet();
                    FillRRData();
                    GetSectionDetail();
                    btnOpeningGetTotal_Click(sender, e);
                    btnProcessGetTotal_Click(sender, e);
                    btnReturnGetTotal_Click(sender, e);
                    btnBMCDCSCollectionGetTotal_Click(sender, e);
                    btnCCWiseProcurementGetTotal_Click(sender, e);
                    btnCanesCollectionGetTotal_Click(sender, e);
                    btnrcvdfrmothrUnionGetTotal_Click(sender, e);
                    btnForPowderConversionGetTotal_Click(sender, e);
                    btnInFlowOtherGetTotal_Click(sender, e);
                    btnPaticularsGetTotal_Click(sender, e);
                    btnMilkToIPGetTotal_Click(sender, e);
                    btnIssuetoMDPOrCCGetTotal_Click(sender, e);
                    btnIssuetootherGetTotal_Click(sender, e);
                    btnIssuetoPowderPlantGetTotal_Click(sender, e);
                    btnIssuetoCreamGetTotal_Click(sender, e);
                    btnIssuetoIceCreamGetTotal_Click(sender, e);
                    btnOutflowOtherGetTotal_Click(sender, e);
                    btnClosingBalancesGetTotal_Click(sender, e);
                    btnIsuuetoOtherPartyGetTotal_Click(sender, e);
                    btnColdRoomBalancesGetTotal_Click(sender, e);
                    btnCCWiseGoatMilkProcurementGetTotal_Click(sender, e);


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

    #region User Defined Function

    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New",
                 new string[] { "flag" },
                 new string[] { "11" }, "dataset");

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
            ds = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                 new string[] { "flag", "Office_Id", "OfficeType_ID" },
                 new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataTextField = "Name";
                ddlShift.DataValueField = "Shift_Id";
                ddlShift.DataBind();
                ddlShift.SelectedValue = ds.Tables[0].Rows[0]["Shift_Id"].ToString();
                // txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");
                //ddlShift.Enabled = false;
                //txtDate.Enabled = false;
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
                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                  new string[] { "21", ddlDS.SelectedValue, objdb.OfficeType_ID() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.SelectedValue = "1";
                ddlPSection.Enabled = false;

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

                decimal PrvD_500InPkt = 0;
                decimal PrvD_200InPkt = 0;
                decimal PrvD_1InPkt = 0;
                decimal PrvD_500InLtr = 0;
                decimal PrvD_200InLtr = 0;
                decimal PrvD_1InLtr = 0;
                decimal CurD_500InPkt = 0;
                decimal CurD_200InPkt = 0;
                decimal CurD_1InPkt = 0;
                decimal CurD_500InLtr = 0;
                decimal CurD_200InLtr = 0;
                decimal CurD_1InLtr = 0;

                foreach (GridViewRow row in gvmttos.Rows)
                {


                    Label lblPrev_Demand_500InPkt = (Label)row.FindControl("lblPrev_Demand_500InPkt");
                    Label Prev_Demand_500InPkt_F = (gvmttos.FooterRow.FindControl("Prev_Demand_500InPkt_F") as Label);

                    if (lblPrev_Demand_500InPkt.Text != "")
                    {
                        PrvD_500InPkt += Convert.ToDecimal(lblPrev_Demand_500InPkt.Text);
                        Prev_Demand_500InPkt_F.Text = PrvD_500InPkt.ToString("0.00");
                    }


                    Label lblPrev_Demand_200InPkt = (Label)row.FindControl("lblPrev_Demand_200InPkt");
                    Label Prev_Demand_200InPkt_F = (gvmttos.FooterRow.FindControl("Prev_Demand_200InPkt_F") as Label);


                    if (lblPrev_Demand_200InPkt.Text != "")
                    {
                        PrvD_200InPkt += Convert.ToDecimal(lblPrev_Demand_200InPkt.Text);
                        Prev_Demand_200InPkt_F.Text = PrvD_200InPkt.ToString("0.00");
                    }

                    Label lblPrev_Demand_1InPkt = (Label)row.FindControl("lblPrev_Demand_1InPkt");
                    Label Prev_Demand_1InPkt_F = (gvmttos.FooterRow.FindControl("Prev_Demand_1InPkt_F") as Label);


                    if (lblPrev_Demand_1InPkt.Text != "")
                    {
                        PrvD_1InPkt += Convert.ToDecimal(lblPrev_Demand_1InPkt.Text);
                        Prev_Demand_1InPkt_F.Text = PrvD_1InPkt.ToString("0.00");
                    }

                    //Label lblPrev_Demand500InLtr = (Label)row.FindControl("lblPrev_Demand500InLtr");
                    //Label lblPrev_Demand500InLtr_F = (gvmttos.FooterRow.FindControl("lblPrev_Demand500InLtr_F") as Label);


                    //if (lblPrev_Demand500InLtr.Text != "")
                    //{
                    //    PrvD_500InLtr += Convert.ToDecimal(lblPrev_Demand500InLtr.Text);
                    //    lblPrev_Demand500InLtr_F.Text = PrvD_500InLtr.ToString("0.00");
                    //}
                    //Label lblPrev_Demand200InLtr = (Label)row.FindControl("lblPrev_Demand200InLtr");
                    //Label lblPrev_Demand200InLtr_F = (gvmttos.FooterRow.FindControl("lblPrev_Demand200InLtr_F") as Label);


                    //if (lblPrev_Demand200InLtr.Text != "")
                    //{
                    //    PrvD_200InLtr += Convert.ToDecimal(lblPrev_Demand200InLtr.Text);
                    //    lblPrev_Demand200InLtr_F.Text = PrvD_200InLtr.ToString("0.00");
                    //}
                    Label lblPrev_DemandInLtr = (Label)row.FindControl("lblPrev_DemandInLtr");
                    Label lblPrev_Demand1InLtr_F = (gvmttos.FooterRow.FindControl("lblPrev_Demand1InLtr_F") as Label);


                    if (lblPrev_DemandInLtr.Text != "")
                    {
                        PrvD_1InLtr += Convert.ToDecimal(lblPrev_DemandInLtr.Text);
                        lblPrev_Demand1InLtr_F.Text = PrvD_1InLtr.ToString("0.00");
                    }

                    Label lblCurrent_Demand_500InPkt = (Label)row.FindControl("lblCurrent_Demand_500InPkt");
                    Label lblCurrent_Demand_500InPkt_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_500InPkt_F") as Label);


                    if (lblCurrent_Demand_500InPkt.Text != "")
                    {
                        CurD_500InPkt += Convert.ToDecimal(lblCurrent_Demand_500InPkt.Text);
                        lblCurrent_Demand_500InPkt_F.Text = CurD_500InPkt.ToString("0.00");
                    }
                    Label lblCurrent_Demand_200InPkt = (Label)row.FindControl("lblCurrent_Demand_200InPkt");
                    Label lblCurrent_Demand_200InPkt_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_200InPkt_F") as Label);


                    if (lblCurrent_Demand_200InPkt.Text != "")
                    {
                        CurD_200InPkt += Convert.ToDecimal(lblCurrent_Demand_200InPkt.Text);
                        lblCurrent_Demand_200InPkt_F.Text = CurD_200InPkt.ToString("0.00");
                    }
                    Label lblCurrent_Demand_1InPkt = (Label)row.FindControl("lblCurrent_Demand_1InPkt");
                    Label lblCurrent_Demand_1InPkt_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_1InPkt_F") as Label);


                    if (lblCurrent_Demand_1InPkt.Text != "")
                    {
                        CurD_1InPkt += Convert.ToDecimal(lblCurrent_Demand_1InPkt.Text);
                        lblCurrent_Demand_1InPkt_F.Text = CurD_1InPkt.ToString("0.00");
                    }
                    //Label lblCurrent_Demand_500InLtr = (Label)row.FindControl("lblCurrent_Demand_500InLtr");
                    //Label lblCurrent_Demand_500InLtr_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_500InLtr_F") as Label);


                    //if (lblCurrent_Demand_500InLtr.Text != "")
                    //{
                    //    CurD_500InLtr += Convert.ToDecimal(lblCurrent_Demand_500InLtr.Text);
                    //    lblCurrent_Demand_500InLtr_F.Text = CurD_500InLtr.ToString("0.00");
                    //}
                    //Label lblCurrent_Demand_200InLtr = (Label)row.FindControl("lblCurrent_Demand_200InLtr");
                    //Label lblCurrent_Demand_200InLtr_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_200InLtr_F") as Label);


                    //if (lblCurrent_Demand_200InLtr.Text != "")
                    //{
                    //    CurD_200InLtr += Convert.ToDecimal(lblCurrent_Demand_200InLtr.Text);
                    //    lblCurrent_Demand_200InLtr_F.Text = CurD_200InLtr.ToString("0.00");
                    //}
                    Label lblCurrent_Demand_1InLtr = (Label)row.FindControl("lblCurrent_Demand_1InLtr");
                    Label lblCurrent_Demand_1InLtr_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_1InLtr_F") as Label);


                    if (lblCurrent_Demand_1InLtr.Text != "")
                    {
                        CurD_1InLtr += Convert.ToDecimal(lblCurrent_Demand_1InLtr.Text);
                        lblCurrent_Demand_1InLtr_F.Text = CurD_1InLtr.ToString("0.00");
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
    protected void FillSheet()
    {

        //lblMsg.Text = "";
        ViewState["MilkProductionProcess_Id"] = "";
        gvOpening.DataSource = string.Empty;
        gvOpening.DataBind();
        gvProcess.DataSource = string.Empty;
        gvProcess.DataBind();
        gvReturn.DataSource = string.Empty;
        gvReturn.DataBind();
        SetInFlowInitialRow();
        SetInFlowGoatMilkInitialRow();
        SetInFlowCansInitialRow();
        SetOutFlowIcecreamInitialRow();
        SetOutFlowInitialRow();
        SetOutFlowIssuetoOtherInitialRow();
        SetBMCDCSCollectionInitialRow();
        SetRecvdFromOtherUnionInitialRow();
        SetForPowderConversionInitialRow();
        SetIssuetoPowderPlantInitialRow();
        btnSave.Text = "Save";
        DataSet dsRecord = objdb.ByProcedure("USP_MilkProductionEntry_New", new string[] { "flag", "Office_ID", "Date", "Shift_Id", "OfficeType_ID" }, new string[] { "1", objdb.Office_ID(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ddlShift.SelectedValue, objdb.OfficeType_ID() }, "dataset");
        if (dsRecord != null && dsRecord.Tables.Count > 0)
        {
            if (dsRecord.Tables[0].Rows.Count > 0)
            {
                btnSave.Text = "Update";
                ViewState["MilkProductionProcess_Id"] = dsRecord.Tables[0].Rows[0]["MilkProductionProcess_Id"].ToString();

            }
            if (dsRecord.Tables[1].Rows.Count > 0)
            {
                gvOpening.DataSource = dsRecord.Tables[1];
                gvOpening.DataBind();




            }
            if (dsRecord.Tables[2].Rows.Count > 0)
            {
                gvProcess.DataSource = dsRecord.Tables[2];
                gvProcess.DataBind();


            }
            if (dsRecord.Tables[3].Rows.Count > 0)
            {
                gvReturn.DataSource = dsRecord.Tables[3];
                gvReturn.DataBind();


            }
            if (dsRecord.Tables[4].Rows.Count > 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["InFlowCurrentTable"];
                dtCurrentTable = dsRecord.Tables[4];
                ViewState["InFlowCurrentTable"] = dtCurrentTable;

                gvCCWiseProcurement.DataSource = dtCurrentTable;
                gvCCWiseProcurement.DataBind();
                foreach (GridViewRow rows in gvCCWiseProcurement.Rows)
                {
                    Label lblOffice_ID = (Label)rows.FindControl("lblOffice_ID");
                    DropDownList ddlCC = (DropDownList)rows.FindControl("ddlCC");
                    DropDownList ddlCCMilkQuality = (DropDownList)rows.FindControl("ddlCCMilkQuality");
                   
                    FillCC(ddlCC);
                    ddlCC.SelectedValue = lblOffice_ID.Text;

                    Label lblCCMilkQuality = (Label)rows.FindControl("lblCCMilkQuality");
                    Label lblCCI_TankerID = (Label)rows.FindControl("lblCCI_TankerID");
                    DropDownList ddlCCTankerNo = (DropDownList)rows.FindControl("ddlCCTankerNo");
                    FillTanker(ddlCCTankerNo);
                    ddlCCTankerNo.SelectedValue = lblCCI_TankerID.Text;
                    ddlCCMilkQuality.SelectedValue = lblCCMilkQuality.Text;

                }

            }
            if (dsRecord.Tables[5].Rows.Count > 0)
            {
                gvPaticulars.DataSource = dsRecord.Tables[5];
                gvPaticulars.DataBind();


            }
            if (dsRecord.Tables[6].Rows.Count > 0)
            {
                gvColdRoomBalances.DataSource = dsRecord.Tables[6];
                gvColdRoomBalances.DataBind();


            }
            if (dsRecord.Tables[7].Rows.Count > 0)
            {
                gvClosingBalances.DataSource = dsRecord.Tables[7];
                gvClosingBalances.DataBind();


            }
            if (dsRecord.Tables[8].Rows.Count > 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                dtCurrentTable = dsRecord.Tables[8];
                ViewState["CurrentTable"] = dtCurrentTable;

                gvIssuetoMDPOrCC.DataSource = dtCurrentTable;
                gvIssuetoMDPOrCC.DataBind();
                foreach (GridViewRow rows in gvIssuetoMDPOrCC.Rows)
                {
                    Label lblOffice_ID = (Label)rows.FindControl("lblOffice_ID");
                    DropDownList ddlMDPCC = (DropDownList)rows.FindControl("ddlMDPCC");
                    Label lblMDPCCI_TankerID = (Label)rows.FindControl("lblMDPCCI_TankerID");
                    DropDownList ddlMDPCCTankerNo = (DropDownList)rows.FindControl("ddlMDPCCTankerNo");
                    Label lblMDPCCMilkQuality = (Label)rows.FindControl("lblMDPCCMilkQuality");
                    DropDownList ddlMDPCCMilkQuality = (DropDownList)rows.FindControl("ddlMDPCCMilkQuality");
                    FillMDPORCC(ddlMDPCC);
                    FillTanker(ddlMDPCCTankerNo);
                    ddlMDPCC.SelectedValue = lblOffice_ID.Text;
                    ddlMDPCCTankerNo.SelectedValue = lblMDPCCI_TankerID.Text;
                    ddlMDPCCMilkQuality.SelectedValue = lblMDPCCMilkQuality.Text;

                }

            }
            if (dsRecord.Tables[9].Rows.Count > 0)
            {
                gvMilkToIP.DataSource = dsRecord.Tables[9];
                gvMilkToIP.DataBind();
                foreach (GridViewRow rows in gvMilkToIP.Rows)
                {
                    Label lblMilkType = (Label)rows.FindControl("lblMilkType");
                    DropDownList ddlMilkType = (DropDownList)rows.FindControl("ddlMilkType");

                    ddlMilkType.SelectedValue = lblMilkType.Text;

                }



            }
            if (dsRecord.Tables[10].Rows.Count > 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["ITOCurrentTable"];
                dtCurrentTable = dsRecord.Tables[10];
                ViewState["ITOCurrentTable"] = dtCurrentTable;

                gvIsuuetoOtherParty.DataSource = dtCurrentTable;
                gvIsuuetoOtherParty.DataBind();
                foreach (GridViewRow rows in gvIsuuetoOtherParty.Rows)
                {
                    Label lblOffice_ID = (Label)rows.FindControl("lblOffice_ID");
                    DropDownList ddlThirdUnion = (DropDownList)rows.FindControl("ddlThirdUnion");
                    Label lblThirdUnionI_TankerID = (Label)rows.FindControl("lblThirdUnionI_TankerID");
                    DropDownList ddlThirdUnionTankerNo = (DropDownList)rows.FindControl("ddlThirdUnionTankerNo");
                    Label lblThirdUnionMilkQuality = (Label)rows.FindControl("lblThirdUnionMilkQuality");
                    DropDownList ddlThirdUnionMilkQuality = (DropDownList)rows.FindControl("ddlThirdUnionMilkQuality");
                    FillIssuetoother(ddlThirdUnion);
                    ddlThirdUnion.SelectedValue = lblOffice_ID.Text;
                    FillTanker(ddlThirdUnionTankerNo);
                    ddlThirdUnionTankerNo.SelectedValue = lblThirdUnionI_TankerID.Text;
                    ddlThirdUnionMilkQuality.SelectedValue = lblThirdUnionMilkQuality.Text;

                }


            }

            if (dsRecord.Tables[11].Rows.Count > 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["BMCDCSCollection"];
                dtCurrentTable = dsRecord.Tables[11];
                ViewState["BMCDCSCollection"] = dtCurrentTable;

                gvBMCDCSCollection.DataSource = dtCurrentTable;
                gvBMCDCSCollection.DataBind();
                foreach (GridViewRow rows in gvBMCDCSCollection.Rows)
                {
                    Label lblI_TankerID = (Label)rows.FindControl("lblI_TankerID");
                    //Label lblBMCTankerRoot_Id = (Label)rows.FindControl("lblBMCTankerRoot_Id");
                    Label lblBMCDCSCollectionMilkQuality = (Label)rows.FindControl("lblBMCDCSCollectionMilkQuality");
                    DropDownList ddlTanker = (DropDownList)rows.FindControl("ddlTanker");
                    DropDownList ddlBMCDCSCollectionMilkQuality = (DropDownList)rows.FindControl("ddlBMCDCSCollectionMilkQuality");
                    //DropDownList ddlBMCTankerRoot = (DropDownList)rows.FindControl("ddlBMCTankerRoot");
                    FillTanker(ddlTanker);
                    ddlTanker.SelectedValue = lblI_TankerID.Text;
                    ddlBMCDCSCollectionMilkQuality.SelectedValue = lblBMCDCSCollectionMilkQuality.Text;
                    //FillBMCRoot(ddlBMCTankerRoot);
                    //ddlBMCTankerRoot.SelectedValue = lblBMCTankerRoot_Id.Text;

                }


            }
            if (dsRecord.Tables[12].Rows.Count > 0)
            {
                gvinflowother.DataSource = dsRecord.Tables[12];
                gvinflowother.DataBind();


            }
            if (dsRecord.Tables[13].Rows.Count > 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["InFlowRecvdfromothrunion"];
                dtCurrentTable = dsRecord.Tables[13];
                ViewState["InFlowRecvdfromothrunion"] = dtCurrentTable;

                gvrcvdfrmothrUnion.DataSource = dtCurrentTable;
                gvrcvdfrmothrUnion.DataBind();
                foreach (GridViewRow rows in gvrcvdfrmothrUnion.Rows)
                {
                    Label lblOffice_ID = (Label)rows.FindControl("lblOffice_ID");
                    Label lblMilkTypeID = (Label)rows.FindControl("lblMilkTypeID");
                    DropDownList ddlrcvdfrmothrUnion = (DropDownList)rows.FindControl("ddlrcvdfrmothrUnion");

                    DropDownList ddl2 = (DropDownList)rows.FindControl("ddlrcvdfrmothrUnionMilkType");
                    FillUnion(ddlrcvdfrmothrUnion);
                    FillMilkType(ddl2);
                    ddlrcvdfrmothrUnion.SelectedValue = lblOffice_ID.Text;
                    ddl2.SelectedValue = lblMilkTypeID.Text;


                }

            }
            if (dsRecord.Tables[14].Rows.Count > 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["InFlowforPowderConversion"];
                dtCurrentTable = dsRecord.Tables[14];
                ViewState["InFlowforPowderConversion"] = dtCurrentTable;

                gvForPowderConversion.DataSource = dtCurrentTable;
                gvForPowderConversion.DataBind();
                foreach (GridViewRow rows in gvForPowderConversion.Rows)
                {
                    Label lblOffice_ID = (Label)rows.FindControl("lblOffice_ID");

                    DropDownList ddlForPowderConversion = (DropDownList)rows.FindControl("ddlForPowderConversion");
                    Label lblForPowderConversionMilkTypeID = (Label)rows.FindControl("lblForPowderConversionMilkTypeID");
                    DropDownList ddl2 = (DropDownList)rows.FindControl("ddlForPowderConversionMilkType");
                    FillUnion(ddlForPowderConversion);
                    FillMilkType(ddl2);
                    ddlForPowderConversion.SelectedValue = lblOffice_ID.Text;
                    ddl2.SelectedValue = lblForPowderConversionMilkTypeID.Text;


                }


            }
            if (dsRecord.Tables[15].Rows.Count > 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["OutFlowIssuetoPowderPlant"];
                dtCurrentTable = dsRecord.Tables[15];
                ViewState["OutFlowIssuetoPowderPlant"] = dtCurrentTable;
                GVIssuetoPowderPlant.DataSource = dsRecord.Tables[15];
                GVIssuetoPowderPlant.DataBind();
                foreach (GridViewRow rows in GVIssuetoPowderPlant.Rows)
                {
                    Label lblContainer = (Label)rows.FindControl("lblContainer");
                    Label lblVariant = (Label)rows.FindControl("lblVariant");
                    DropDownList ddlContainer = (DropDownList)rows.FindControl("ddlContainer");
                    DropDownList ddlVariant = (DropDownList)rows.FindControl("ddlVariant");

                    FillContainer(ddlContainer);
                    ddlContainer.SelectedValue = lblContainer.Text;
                    ddlVariant.SelectedValue = lblVariant.Text;
                    //ddlVariant.Items.FindByText(lblVariant.Text).Selected = true;



                }


            }
            if (dsRecord.Tables[16].Rows.Count > 0)
            {
                gvIssuetoCream.DataSource = dsRecord.Tables[16];
                gvIssuetoCream.DataBind();


            }

            if (dsRecord.Tables[17].Rows.Count > 0)
            {
                gvIssuetoother.DataSource = dsRecord.Tables[17];
                gvIssuetoother.DataBind();


            }
            if (dsRecord.Tables[18].Rows.Count > 0)
            {
                gvOther.DataSource = dsRecord.Tables[18];
                gvOther.DataBind();


            }
            if (dsRecord.Tables[19].Rows.Count > 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["InFlowCansCurrentTable"];
                dtCurrentTable = dsRecord.Tables[19];
                ViewState["InFlowCansCurrentTable"] = dtCurrentTable;
                gvCanesCollection.DataSource = dsRecord.Tables[19];
                gvCanesCollection.DataBind();
                foreach (GridViewRow rows in gvCanesCollection.Rows)
                {

                    Label lblCanesCollectionMilkQuality = (Label)rows.FindControl("lblCanesCollectionMilkQuality");
                    DropDownList ddlCanesCollectionMilkQuality = (DropDownList)rows.FindControl("ddlCanesCollectionMilkQuality");

                    ddlCanesCollectionMilkQuality.SelectedValue = lblCanesCollectionMilkQuality.Text;

                    //ddlVariant.Items.FindByText(lblVariant.Text).Selected = true;



                }


            }
            if (dsRecord.Tables[20].Rows.Count > 0)
            {
                gvIssuetoIceCream.DataSource = dsRecord.Tables[20];
                gvIssuetoIceCream.DataBind();
                DataTable dtCurrentTable = (DataTable)ViewState["OutFlowIcecreamCurrentTable"];
                dtCurrentTable = dsRecord.Tables[20];
                ViewState["OutFlowIcecreamCurrentTable"] = dtCurrentTable;


            }
            if (dsRecord.Tables[21].Rows.Count > 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["InFlowGoatMilkCurrentTable"];
                dtCurrentTable = dsRecord.Tables[21];
                ViewState["InFlowGoatMilkCurrentTable"] = dtCurrentTable;

                gvCCWiseGoatMilkProcurement.DataSource = dtCurrentTable;
                gvCCWiseGoatMilkProcurement.DataBind();
                foreach (GridViewRow rows in gvCCWiseGoatMilkProcurement.Rows)
                {
                    Label lblOffice_ID = (Label)rows.FindControl("lblCCGoatMilkOffice_ID");
                    DropDownList ddlCC = (DropDownList)rows.FindControl("ddlCCGoatMilk");
                    DropDownList ddlCCMilkQuality = (DropDownList)rows.FindControl("ddlCCGoatMilkQuality");
                    FillCC(ddlCC);
                    ddlCC.SelectedValue = lblOffice_ID.Text;

                    Label lblCCMilkQuality = (Label)rows.FindControl("lblCCGoatMilkMilkQuality");
                    Label lblCCI_TankerID = (Label)rows.FindControl("lblCCGoatMilkI_TankerID");
                    DropDownList ddlCCTankerNo = (DropDownList)rows.FindControl("ddlCCGoatMilkTankerNo");
                    FillTanker(ddlCCTankerNo);
                    ddlCCTankerNo.SelectedValue = lblCCI_TankerID.Text;
                    ddlCCMilkQuality.SelectedValue = lblCCMilkQuality.Text;

                }

            }
            //if (dsRecord.Tables[22].Rows.Count > 0)
            //{
            //    GVIssueofgoatmilk.DataSource = dsRecord.Tables[22];
            //    GVIssueofgoatmilk.DataBind();
            //}
        }
    }
    protected void FillRRData()
    {
        Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
        DataSet dsRR_Child = objdb.ByProcedure("SpProductionProduct_Milk_InOut_RRSheetNew",
            new string[] { "flag", "Office_ID", "Date", "Shift_Id", "ProductSection_ID", "Ghee_RR", "Cream_RR", "Butter_RR", "SMP_RR" },
            new string[] { "1", objdb.Office_ID(), Fdate, ddlShift.SelectedValue, "2", objdb.LooseGheeItemTypeId_ID(), objdb.LooseCreamItemTypeId_ID(), objdb.LooseButterItemTypeId_ID(), objdb.LooseSMPItemTypeId_ID() }, "dataset");

        if (dsRR_Child != null && dsRR_Child.Tables[0].Rows.Count > 0)
        {
            GVRRSheet.DataSource = dsRR_Child.Tables[1];
            GVRRSheet.DataBind();

        }

        else
        {
            GVRRSheet.DataSource = string.Empty;
            GVRRSheet.DataBind();
            btnGetTotal.Enabled = true;
        }
    }
    //public void FillBMCRoot(DropDownList ddl)
    //{
    //    try
    //    {
    //        ds = null;
    //        ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
    //                  new string[] { "flag", "OfficeType_ID", "Office_ID" },
    //                  new string[] { "6", objdb.OfficeType_ID(), objdb.Office_ID() }, "dataset");

    //        if (ds.Tables[0].Rows.Count != 0)
    //        {
    //            ddl.DataTextField = "BMCTankerRootName";
    //            ddl.DataValueField = "BMCTankerRoot_Id";
    //            ddl.DataSource = ds;
    //            ddl.DataBind();
    //            //ddlBMCTankerRootName.Items.Insert(0, "Select");
    //            ddl.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }

    //}
    public void FillBMCRoot()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                      new string[] { "flag", "Office_ID"},
                      new string[] { "33", ddlDS.SelectedValue}, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
               ds6 = ds;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    public void FillContainer(DropDownList ddl)
    {
        try
        {
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New", new string[] { "flag", "Office_ID", "OfficeType_ID" },
                      new string[] { "9", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = "V_MCName";
            ddl.DataValueField = "I_MCID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    protected void FillCC(DropDownList ddl)
    {
        try
        {
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New", new string[] { "flag", "Office_ID", "OfficeType_ID" },
                      new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = "Office_Name";
            ddl.DataValueField = "Office_ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillUnion(DropDownList ddl)
    {
        try
        {
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New", new string[] { "flag", "Office_ID", "OfficeType_ID" },
                      new string[] { "6", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = "Office_Name";
            ddl.DataValueField = "Office_ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillMilkType(DropDownList ddl)
    {
        try
        {
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New", new string[] { "flag" },
                      new string[] { "8" }, "dataset");

            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = "ItemName";
            ddl.DataValueField = "Item_id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillTanker(DropDownList ddl)
    {
        try
        {
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New", new string[] { "flag", "Office_ID", "OfficeType_ID" },
                      new string[] { "7", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = "V_VehicleNo";
            ddl.DataValueField = "I_TankerID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillIssuetoother(DropDownList ddl)
    {
        try
        {
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New", new string[] { "flag", "Office_ID", "OfficeType_ID" },
                      new string[] { "5", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = "Office_Name";
            ddl.DataValueField = "Office_ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillMDPORCC(DropDownList ddl)
    {
        try
        {
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New", new string[] { "flag", "Office_ID" },
                      new string[] { "4", objdb.Office_ID() }, "dataset");

            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = "Office_Name";
            ddl.DataValueField = "Office_ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    //RR Sheet
    private DataTable GetRRSheetData()
    {

        decimal RR_OpeningBalance = 0;
        decimal RR_Obtained = 0;
        decimal RR_Total = 0;
        decimal RR_Toning = 0;
        decimal RR_MaintainingSNF = 0;
        decimal RR_IssuedForProductSection = 0;
        decimal RR_TotalIssued = 0;
        decimal RR_ClosingBalance = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("RR_OpeningBalance", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_Obtained", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_Total", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_Toning", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_MaintainingSNF", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_IssuedForProductSection", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_TotalIssued", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_ClosingBalance", typeof(decimal)));

        foreach (GridViewRow row in GVRRSheet.Rows)
        {

            Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
            Label lblItemType_id = (Label)row.FindControl("lblItemType_id");

            TextBox txtBalance_BFRR = (TextBox)row.FindControl("txtBalance_BFRR");
            TextBox txtRRObtained = (TextBox)row.FindControl("txtRRObtained");
            TextBox txtRRTotal = (TextBox)row.FindControl("txtRRTotal");

            TextBox txtRRToning = (TextBox)row.FindControl("txtRRToning");
            TextBox txtRRMaintainingSNF = (TextBox)row.FindControl("txtRRMaintainingSNF");
            TextBox txtRRIssueforproductionsection = (TextBox)row.FindControl("txtRRIssueforproductionsection");
            TextBox txtRRTotalIssued = (TextBox)row.FindControl("txtRRTotalIssued");
            TextBox txtRRClosingBalance = (TextBox)row.FindControl("txtRRClosingBalance");


            if (txtBalance_BFRR.Text != "0" && txtBalance_BFRR.Text != "0.00")
            {
                RR_OpeningBalance = Convert.ToDecimal(txtBalance_BFRR.Text);
            }
            else
            {
                RR_OpeningBalance = 0;
            }

            if (txtRRObtained.Text != "0" && txtRRObtained.Text != "0.00")
            {
                RR_Obtained = Convert.ToDecimal(txtRRObtained.Text);
            }
            else
            {
                RR_Obtained = 0;
            }

            if (txtRRTotal.Text != "0" && txtRRTotal.Text != "0.00")
            {
                RR_Total = Convert.ToDecimal(txtRRTotal.Text);
            }
            else
            {
                RR_Total = 0;
            }

            if (txtRRToning.Text != "0" && txtRRToning.Text != "0.00")
            {
                RR_Toning = Convert.ToDecimal(txtRRToning.Text);
            }
            else
            {
                RR_Toning = 0;
            }

            if (txtRRMaintainingSNF.Text != "0" && txtRRMaintainingSNF.Text != "0.00")
            {
                RR_MaintainingSNF = Convert.ToDecimal(txtRRMaintainingSNF.Text);
            }
            else
            {
                RR_MaintainingSNF = 0;
            }


            if (txtRRIssueforproductionsection.Text != "0" && txtRRIssueforproductionsection.Text != "0.00")
            {
                RR_IssuedForProductSection = Convert.ToDecimal(txtRRIssueforproductionsection.Text);
            }
            else
            {
                RR_IssuedForProductSection = 0;
            }


            if (txtRRTotalIssued.Text != "0" && txtRRTotalIssued.Text != "0.00")
            {
                RR_TotalIssued = Convert.ToDecimal(txtRRTotalIssued.Text);
            }
            else
            {
                RR_TotalIssued = 0;
            }

            if (txtRRClosingBalance.Text != "0" && txtRRClosingBalance.Text != "0.00")
            {
                RR_ClosingBalance = Convert.ToDecimal(txtRRClosingBalance.Text);
            }
            else
            {
                RR_ClosingBalance = 0;
            }

            dr = dt.NewRow();
            dr[0] = lblItemCat_id.Text;
            dr[1] = lblItemType_id.Text;
            dr[2] = RR_OpeningBalance;
            dr[3] = RR_Obtained;
            dr[4] = RR_Total;
            dr[5] = RR_Toning;
            dr[6] = RR_MaintainingSNF;
            dr[7] = RR_IssuedForProductSection;
            dr[8] = RR_TotalIssued;
            dr[9] = RR_ClosingBalance;
            dt.Rows.Add(dr);

        }

        return dt;
    }

    #endregion
     #region InFlow

    #region Opening Balance

    protected void gvOpening_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Opening Balance";
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvOpening.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private DataTable GetOpeningData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("I_MCID", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(decimal));
        dt.Columns.Add("QtyInKg", typeof(decimal));
        dt.Columns.Add("Fat_Per", typeof(decimal));
        dt.Columns.Add("Snf_Per", typeof(decimal));
        dt.Columns.Add("KgFat", typeof(decimal));
        dt.Columns.Add("KgSnf", typeof(decimal));

        foreach (GridViewRow row in gvOpening.Rows)
        {
            Label lblOpeningI_MCID = (Label)row.FindControl("lblOpeningI_MCID");
            TextBox txtOpeningQuantityInLtr = (TextBox)row.FindControl("txtOpeningQuantityInLtr");
            TextBox txtOpeningQuantityInKg = (TextBox)row.FindControl("txtOpeningQuantityInKg");
            TextBox txtOpeningFat_Per = (TextBox)row.FindControl("txtOpeningFat_Per");
            TextBox txtOpeningSnf_Per = (TextBox)row.FindControl("txtOpeningSnf_Per");
            TextBox txtOpeningFATInKg = (TextBox)row.FindControl("txtOpeningFATInKg");
            TextBox txtOpeningSNFInKg = (TextBox)row.FindControl("txtOpeningSNFInKg");
            if (txtOpeningQuantityInLtr.Text != "" && txtOpeningQuantityInKg.Text != "" && txtOpeningQuantityInLtr.Text != "0.00" && txtOpeningQuantityInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblOpeningI_MCID.Text;
                dr[1] = txtOpeningQuantityInLtr.Text;
                dr[2] = txtOpeningQuantityInKg.Text;
                dr[3] = txtOpeningFat_Per.Text;
                dr[4] = txtOpeningSnf_Per.Text;
                dr[5] = txtOpeningFATInKg.Text;
                dr[6] = txtOpeningSNFInKg.Text;
                dt.Rows.Add(dr);

            }

        }
        return dt;
    }
    protected void btnOpening_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DataTable dtOpening = new DataTable();
                dtOpening = GetOpeningData();
                if (dtOpening.Rows.Count > 0)
                {
                    if (btnOpening.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { "type_Production_Milk_InProcess_Opening" ,
                                                       
                                                     },
                                            new DataTable[] { dtOpening, 
                                                          
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnOpening.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"23",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { "type_Production_Milk_InProcess_Opening" 
                                                         
                                                     },
                                            new DataTable[] { dtOpening, 
                                                           
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }

                //Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnOpeningGetTotal_Click(object sender, EventArgs e)
    {
        btnOpening.Enabled = false;
        decimal OpeningTQtyLtr = 0;
        decimal OpeningTQtyKg = 0;
        decimal OpeningTFatKg = 0;
        decimal OpeningTSnfKg = 0;
        foreach (GridViewRow rows in gvOpening.Rows)
        {
            TextBox txtOpeningQuantityInLtr = (TextBox)rows.FindControl("txtOpeningQuantityInLtr");
            TextBox txtOpeningQuantityInKg = (TextBox)rows.FindControl("txtOpeningQuantityInKg");
            TextBox txtOpeningFATInKg = (TextBox)rows.FindControl("txtOpeningFATInKg");
            TextBox txtOpeningSNFInKg = (TextBox)rows.FindControl("txtOpeningSNFInKg");
            if (txtOpeningQuantityInLtr.Text != "")
            {
                OpeningTQtyLtr += decimal.Parse(txtOpeningQuantityInLtr.Text);
            }
            if (txtOpeningQuantityInKg.Text != "")
            {
                OpeningTQtyKg += decimal.Parse(txtOpeningQuantityInKg.Text);
            }
            if (txtOpeningFATInKg.Text != "")
            {
                OpeningTFatKg += decimal.Parse(txtOpeningFATInKg.Text);
            }
            if (txtOpeningSNFInKg.Text != "")
            {
                OpeningTSnfKg += decimal.Parse(txtOpeningSNFInKg.Text);
            }
        }

        gvOpening.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvOpening.FooterRow.Cells[2].Text = "<b>" + OpeningTQtyLtr.ToString() + "</b>";
        gvOpening.FooterRow.Cells[1].Text = "<b>" + OpeningTQtyKg.ToString() + "</b>";
        gvOpening.FooterRow.Cells[5].Text = "<b>" + OpeningTFatKg.ToString() + "</b>";
        gvOpening.FooterRow.Cells[6].Text = "<b>" + OpeningTSnfKg.ToString() + "</b>";
        if (OpeningTQtyLtr > 0)
        {
            btnOpening.Enabled = true;
        }
    }
    #endregion

    #region PROCESS
    protected void gvProcess_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "PROCESS";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvProcess.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }

    }
    private DataTable GetProcessData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Item_id", typeof(string));
        dt.Columns.Add("NoOfPackets", typeof(string));
        dt.Columns.Add("PackedQtyInLtr", typeof(string));
        dt.Columns.Add("PackedQtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvProcess.Rows)
        {
            Label lblProcessItem_id = (Label)row.FindControl("lblProcessItem_id");
            TextBox txtProcessnofpackets = (TextBox)row.FindControl("txtProcessnofpackets");
            TextBox txtProcessPackedInLtr = (TextBox)row.FindControl("txtProcessPackedInLtr");
            TextBox txtProcessPackedInKg = (TextBox)row.FindControl("txtProcessPackedInKg");
            TextBox txtProcessFat_Per = (TextBox)row.FindControl("txtProcessFat_Per");
            TextBox txtProcessSnf_Per = (TextBox)row.FindControl("txtProcessSnf_Per");
            TextBox txtProcessFATInKg = (TextBox)row.FindControl("txtProcessFATInKg");
            TextBox txtProcessSNFInKg = (TextBox)row.FindControl("txtProcessSNFInKg");
            if (txtProcessPackedInLtr.Text != "" && txtProcessPackedInKg.Text != "" && txtProcessPackedInLtr.Text != "0.00" && txtProcessPackedInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblProcessItem_id.Text;
                dr[1] = txtProcessnofpackets.Text;
                dr[2] = txtProcessPackedInLtr.Text;
                dr[3] = txtProcessPackedInKg.Text;

                dr[4] = txtProcessFat_Per.Text;
                dr[5] = txtProcessSnf_Per.Text;
                dr[6] = txtProcessFATInKg.Text;
                dr[7] = txtProcessSNFInKg.Text;
                dt.Rows.Add(dr);

            }

        }
        return dt;
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtProcess = new DataTable();
                dtProcess = GetProcessData();


                if (dtProcess.Rows.Count > 0)
                {


                    if (btnProcess.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_InProcess_Process"
                                                     
                                                     },
                                            new DataTable[] { 
                                                          dtProcess 
                                                         
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnProcess.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"24",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] {
                                                         "type_Production_Milk_InProcess_Process"
                                                        
                                                     },
                                            new DataTable[] {
                                                            dtProcess
                                                            
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnProcessGetTotal_Click(object sender, EventArgs e)
    {
        btnProcess.Enabled = false;
        decimal ProcessLooseTQtyLtr = 0;
        decimal ProcessLooseTQtyKg = 0;
        decimal ProcessPacketTQtyLtr = 0;
        decimal ProcessPacketTQtyKg = 0;
        decimal ProcessTFatKg = 0;
        decimal ProcessTSnfKg = 0;
        foreach (GridViewRow rows in gvProcess.Rows)
        {

            TextBox txtProcessPackedInLtr = (TextBox)rows.FindControl("txtProcessPackedInLtr");
            TextBox txtProcessPackedInKg = (TextBox)rows.FindControl("txtProcessPackedInKg");
            TextBox txtProcessFATInKg = (TextBox)rows.FindControl("txtProcessFATInKg");
            TextBox txtProcessSNFInKg = (TextBox)rows.FindControl("txtProcessSNFInKg");

            if (txtProcessPackedInLtr.Text != "")
            {
                ProcessPacketTQtyLtr += decimal.Parse(txtProcessPackedInLtr.Text);
            }
            if (txtProcessPackedInKg.Text != "")
            {
                ProcessPacketTQtyKg += decimal.Parse(txtProcessPackedInKg.Text);
            }
            if (txtProcessFATInKg.Text != "")
            {
                ProcessTFatKg += decimal.Parse(txtProcessFATInKg.Text);
            }
            if (txtProcessSNFInKg.Text != "")
            {
                ProcessTSnfKg += decimal.Parse(txtProcessSNFInKg.Text);
            }
        }
        if (gvProcess.Rows.Count > 0)
        {
            gvProcess.FooterRow.Cells[0].Text = "<b>Total : </b>";
            gvProcess.FooterRow.Cells[3].Text = "<b>" + ProcessPacketTQtyKg.ToString() + "</b>";
            gvProcess.FooterRow.Cells[2].Text = "<b>" + ProcessPacketTQtyLtr.ToString() + "</b>";

            gvProcess.FooterRow.Cells[6].Text = "<b>" + ProcessTFatKg.ToString() + "</b>";
            gvProcess.FooterRow.Cells[7].Text = "<b>" + ProcessTSnfKg.ToString() + "</b>";

        }
        if (ProcessPacketTQtyKg > 0)
        {
            btnProcess.Enabled = true;
        }

    }
    #endregion

    #region RETURN
    protected void gvReturn_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "RETURN";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvReturn.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private DataTable GetReturnData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Item_id", typeof(string));
        dt.Columns.Add("NoOfPackets", typeof(string));
        dt.Columns.Add("PackedQtyInLtr", typeof(string));
        dt.Columns.Add("PackedQtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvReturn.Rows)
        {
            Label lblReturnItem_id = (Label)row.FindControl("lblReturnItem_id");
            TextBox txtReturnPackedInLtr = (TextBox)row.FindControl("txtReturnPackedInLtr");
            TextBox txtReturnnofpackets = (TextBox)row.FindControl("txtReturnnofpackets");
            TextBox txtReturnPackedInKg = (TextBox)row.FindControl("txtReturnPackedInKg");
            TextBox txtReturnFat_Per = (TextBox)row.FindControl("txtReturnFat_Per");
            TextBox txtReturnSnf_Per = (TextBox)row.FindControl("txtReturnSnf_Per");
            TextBox txtReturnFATInKg = (TextBox)row.FindControl("txtReturnFATInKg");
            TextBox txtReturnSNFInKg = (TextBox)row.FindControl("txtReturnSNFInKg");
            if (txtReturnPackedInLtr.Text != "" && txtReturnPackedInKg.Text != "" && txtReturnPackedInLtr.Text != "0.00" && txtReturnPackedInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblReturnItem_id.Text;
                dr[1] = txtReturnnofpackets.Text;
                dr[2] = txtReturnPackedInLtr.Text;
                dr[3] = txtReturnPackedInKg.Text;
                dr[4] = txtReturnFat_Per.Text;
                dr[5] = txtReturnSnf_Per.Text;
                dr[6] = txtReturnFATInKg.Text;
                dr[7] = txtReturnSNFInKg.Text;
                dt.Rows.Add(dr);

            }

        }
        return dt;
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtReturn = new DataTable();
                dtReturn = GetReturnData();


                if (dtReturn.Rows.Count > 0)
                {


                    if (btnReturn.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] {  
                                                       "type_Production_Milk_InProcess_Return"
                                                       
                                                     },
                                            new DataTable[] { 
                                                          dtReturn
                                                          
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnReturn.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"5",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                         "type_Production_Milk_InProcess_Return"
                                                     },
                                            new DataTable[] {
                                                            dtReturn
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnReturnGetTotal_Click(object sender, EventArgs e)
    {
        btnReturn.Enabled = false;
        decimal ReturnPacketTQtyLtr = 0;
        decimal ReturnPacketTQtyKg = 0;
        decimal ReturnTFatKg = 0;
        decimal ReturnTSnfKg = 0;
        foreach (GridViewRow rows in gvReturn.Rows)
        {

            TextBox txtReturnPackedInLtr = (TextBox)rows.FindControl("txtReturnPackedInLtr");
            TextBox txtReturnPackedInKg = (TextBox)rows.FindControl("txtReturnPackedInKg");
            TextBox txtReturnFatInKg = (TextBox)rows.FindControl("txtReturnFatInKg");
            TextBox txtReturnSnfInKg = (TextBox)rows.FindControl("txtReturnSnfInKg");

            if (txtReturnPackedInLtr.Text != "")
            {
                ReturnPacketTQtyLtr += decimal.Parse(txtReturnPackedInLtr.Text);
            }
            if (txtReturnPackedInKg.Text != "")
            {
                ReturnPacketTQtyKg += decimal.Parse(txtReturnPackedInKg.Text);
            }
            if (txtReturnFatInKg.Text != "")
            {
                ReturnTFatKg += decimal.Parse(txtReturnFatInKg.Text);
            }
            if (txtReturnSnfInKg.Text != "")
            {
                ReturnTSnfKg += decimal.Parse(txtReturnSnfInKg.Text);
            }
        }
        if (gvReturn.Rows.Count > 0)
        {
            gvReturn.FooterRow.Cells[0].Text = "<b>Total : </b>";

            gvReturn.FooterRow.Cells[1].Text = "<b>" + ReturnPacketTQtyKg.ToString() + "</b>";
            gvReturn.FooterRow.Cells[2].Text = "<b>" + ReturnPacketTQtyLtr.ToString() + "</b>";
            gvReturn.FooterRow.Cells[6].Text = "<b>" + ReturnTFatKg.ToString() + "</b>";
            gvReturn.FooterRow.Cells[7].Text = "<b>" + ReturnTSnfKg.ToString() + "</b>";
        }
        if (ReturnPacketTQtyKg > 0)
        {
            btnReturn.Enabled = true;
        }
    }
    #endregion

    #region MDP/CC WISE RECEIPT
    protected void gvCCWiseProcurement_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "MDP/CC WISE RECEIPT";
            HeaderCell.ColumnSpan = 11;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvCCWiseProcurement.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private void SetInFlowInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("Office_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
        dt.Columns.Add(new DataColumn("I_TankerID", typeof(string)));
        dt.Columns.Add(new DataColumn("V_VehicleNo", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("FAT", typeof(string)));
        dt.Columns.Add(new DataColumn("SNF", typeof(string)));
        dt.Columns.Add(new DataColumn("FatInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("SnfInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkQuality", typeof(string)));
        dt.Columns.Add(new DataColumn("ChallanNo", typeof(string)));

        dr = dt.NewRow();


        dr["Office_ID"] = string.Empty;
        dr["Office_Name"] = string.Empty;
        dr["I_TankerID"] = string.Empty;
        dr["V_VehicleNo"] = string.Empty;
        dr["QtyInLtr"] = string.Empty;
        dr["QtyInKg"] = string.Empty;
        dr["FAT"] = string.Empty;
        dr["SNF"] = string.Empty;
        dr["FatInKg"] = string.Empty;
        dr["SnfInKg"] = string.Empty;
        dr["MilkQuality"] = string.Empty;
        dr["ChallanNo"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["InFlowCurrentTable"] = dt;


        gvCCWiseProcurement.DataSource = dt;
        gvCCWiseProcurement.DataBind();



        DropDownList ddl1 = (DropDownList)gvCCWiseProcurement.Rows[0].Cells[1].FindControl("ddlCC");
        DropDownList ddl2 = (DropDownList)gvCCWiseProcurement.Rows[0].Cells[2].FindControl("ddlCCTankerNo");


        //DropDownList ddl2 = (DropDownList)Gridview1.Rows[0].Cells[2].FindControl("ddlStyle");
        FillCC(ddl1);
        FillTanker(ddl2);
    }
    private void AddInFlowNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["InFlowCurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["InFlowCurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlCC = (DropDownList)gvCCWiseProcurement.Rows[rowIndex].Cells[1].FindControl("ddlCC");
                    DropDownList ddlCCTankerNo = (DropDownList)gvCCWiseProcurement.Rows[rowIndex].Cells[2].FindControl("ddlCCTankerNo");
                    DropDownList ddlCCMilkQuality = (DropDownList)gvCCWiseProcurement.Rows[rowIndex].Cells[3].FindControl("ddlCCMilkQuality");
                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");
                    if (ddlCC.SelectedIndex != 0)
                    {
                        TextBox QtyInLtr = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[4].FindControl("txtCCQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[5].FindControl("txtCCQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[6].FindControl("txtCCFat_Per");
                        TextBox SNF_Per = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[7].FindControl("txtCCSnf_Per");
                        TextBox FatInKg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[8].FindControl("txtCCFATInKg");
                        TextBox SNFInkg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[9].FindControl("txtCCSNFInKg");
                        TextBox CCChallanNo = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[10].FindControl("txtCCChallanNo");


                        drCurrentRow = dtCurrentTable.NewRow();


                        dtCurrentTable.Rows[i - 1]["Office_ID"] = ddlCC.SelectedValue;
                        //dtCurrentTable.Rows[i - 1]["Column2"] = ddlSt.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["Office_Name"] = ddlCC.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["I_TankerID"] = ddlCCTankerNo.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["V_VehicleNo"] = ddlCCTankerNo.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInLtr"] = QtyInLtr.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInKg"] = QtyInKg.Text;
                        dtCurrentTable.Rows[i - 1]["FAT"] = FAT_Per.Text;
                        dtCurrentTable.Rows[i - 1]["SNF"] = SNF_Per.Text;
                        dtCurrentTable.Rows[i - 1]["FatInKg"] = FatInKg.Text;
                        dtCurrentTable.Rows[i - 1]["SnfInKg"] = SNFInkg.Text;
                        dtCurrentTable.Rows[i - 1]["MilkQuality"] = ddlCCMilkQuality.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["ChallanNo"] = CCChallanNo.Text;

                        rowIndex++;

                    }
                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["InFlowCurrentTable"] = dtCurrentTable;

                    gvCCWiseProcurement.DataSource = dtCurrentTable;
                    gvCCWiseProcurement.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["InFlowCurrentTable"] = null;
        }

        //Set Previous Data on Postbacks


        SetInFlowPreviousData();
    }
    private void SetInFlowPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["InFlowCurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["InFlowCurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlCC = (DropDownList)gvCCWiseProcurement.Rows[rowIndex].Cells[1].FindControl("ddlCC");
                        DropDownList ddlCCTankerNo = (DropDownList)gvCCWiseProcurement.Rows[rowIndex].Cells[2].FindControl("ddlCCTankerNo");
                        DropDownList ddlCCMilkQuality = (DropDownList)gvCCWiseProcurement.Rows[rowIndex].Cells[3].FindControl("ddlCCMilkQuality");
                        TextBox QtyInLtr = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[4].FindControl("txtCCQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[5].FindControl("txtCCQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[6].FindControl("txtCCFat_Per");
                        TextBox SNF_Per = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[7].FindControl("txtCCSnf_Per");
                        TextBox FatInKg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[8].FindControl("txtCCFATInKg");
                        TextBox SNFInkg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[9].FindControl("txtCCSNFInKg");
                        TextBox CCChallanNo = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[10].FindControl("txtCCChallanNo");

                        FillCC(ddlCC);
                        FillTanker(ddlCCTankerNo);
                        if (i < dt.Rows.Count - 1)
                        {
                            ddlCC.ClearSelection();
                            ddlCC.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;

                            ddlCCTankerNo.ClearSelection();
                            ddlCCTankerNo.Items.FindByValue(dt.Rows[i]["I_TankerID"].ToString()).Selected = true;

                            ddlCCMilkQuality.ClearSelection();
                            ddlCCMilkQuality.Items.FindByValue(dt.Rows[i]["MilkQuality"].ToString()).Selected = true;


                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();
                        CCChallanNo.Text = dt.Rows[i]["ChallanNo"].ToString();
                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void SetInFlowOnRemove()
    {
        int rowIndex = 0;
        double Total = 0;
        if (ViewState["InFlowCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["InFlowCurrentTable"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlCC = (DropDownList)gvCCWiseProcurement.Rows[rowIndex].Cells[1].FindControl("ddlCC");
                    DropDownList ddlCCTankerNo = (DropDownList)gvCCWiseProcurement.Rows[rowIndex].Cells[2].FindControl("ddlCCTankerNo");
                    DropDownList ddlCCMilkQuality = (DropDownList)gvCCWiseProcurement.Rows[rowIndex].Cells[3].FindControl("ddlCCMilkQuality");
                    TextBox QtyInLtr = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[4].FindControl("txtCCQuantityInLtr");
                    TextBox QtyInKg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[5].FindControl("txtCCQuantityInKg");
                    TextBox FAT_Per = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[6].FindControl("txtCCFat_Per");
                    TextBox SNF_Per = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[7].FindControl("txtCCSnf_Per");
                    TextBox FatInKg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[8].FindControl("txtCCFATInKg");
                    TextBox SNFInkg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[9].FindControl("txtCCSNFInKg");

                    TextBox CCChallanNo = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[10].FindControl("txtCCChallanNo");
                    FillCC(ddlCC);
                    FillTanker(ddlCCTankerNo);
                    if (i < dt.Rows.Count)
                    {
                        if (dt.Rows[i]["Office_ID"].ToString() != "")
                        {
                            ddlCC.ClearSelection();
                            ddlCC.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;
                            ddlCCTankerNo.ClearSelection();
                            ddlCCTankerNo.Items.FindByValue(dt.Rows[i]["I_TankerID"].ToString()).Selected = true;

                            ddlCCMilkQuality.ClearSelection();
                            ddlCCMilkQuality.Items.FindByValue(dt.Rows[i]["MilkQuality"].ToString()).Selected = true;


                        }
                    }

                    ddlCC.SelectedValue = dt.Rows[i]["Office_ID"].ToString();
                    QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                    QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                    FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                    SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                    FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                    SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();
                    CCChallanNo.Text = dt.Rows[i]["ChallanNo"].ToString();
                    rowIndex++;


                }

            }

        }
    }
    protected void SetInFlowPreviousOnRemove()
    {
        int rowIndex = 0;
        double Total = 0;
        if (ViewState["InFlowCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["InFlowCurrentTable"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlCC = (DropDownList)gvCCWiseProcurement.Rows[rowIndex].Cells[1].FindControl("ddlCC");
                    TextBox QtyInLtr = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[2].FindControl("txtCCQuantityInLtr");
                    TextBox QtyInKg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[3].FindControl("txtCCQuantityInKg");
                    TextBox FAT_Per = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[4].FindControl("txtCCFat_Per");
                    TextBox SNF_Per = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[5].FindControl("txtCCSnf_Per");
                    TextBox FatInKg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[6].FindControl("txtCCFATInKg");
                    TextBox SNFInkg = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[7].FindControl("txtCCSNFInKg");
                    TextBox CCChallanNo = (TextBox)gvCCWiseProcurement.Rows[rowIndex].Cells[10].FindControl("txtCCChallanNo");

                    FillCC(ddlCC);

                    if (i < dt.Rows.Count)
                    {
                        if (dt.Rows[i]["Office_ID"].ToString() != "")
                        {
                            ddlCC.ClearSelection();
                            ddlCC.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;


                        }
                    }
                    ddlCC.SelectedValue = dt.Rows[i]["Office_ID"].ToString();
                    QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                    QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                    FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                    SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                    FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                    SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();
                    CCChallanNo.Text = dt.Rows[i]["ChallanNo"].ToString();
                    rowIndex++;


                }

            }

        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["InFlowrowID"] = rowID.ToString();
        if (ViewState["InFlowCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["InFlowCurrentTable"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["InFlowCurrentTable"] = dt;

            //Re bind the GridView for the updated data  
            gvCCWiseProcurement.DataSource = dt;
            gvCCWiseProcurement.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetInFlowOnRemove();

    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        ViewState["InFlowrowID"] = "";
        //BindGrid();
        AddInFlowNewRowToGrid();
    }
    private DataTable GetCCWiseProcuremetData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Office_Id", typeof(string));
        dt.Columns.Add("I_TankerID", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        dt.Columns.Add("MilkQuality", typeof(string));
        dt.Columns.Add("ChallanNo", typeof(string));
        foreach (GridViewRow row in gvCCWiseProcurement.Rows)
        {
            DropDownList ddlCC = (DropDownList)row.FindControl("ddlCC");
            DropDownList ddlCCTankerNo = (DropDownList)row.FindControl("ddlCCTankerNo");
            DropDownList ddlCCMilkQuality = (DropDownList)row.FindControl("ddlCCMilkQuality");
            TextBox txtCCQuantityInLtr = (TextBox)row.FindControl("txtCCQuantityInLtr");
            TextBox txtCCQuantityInKg = (TextBox)row.FindControl("txtCCQuantityInKg");
            TextBox txtCCFat_Per = (TextBox)row.FindControl("txtCCFat_Per");
            TextBox txtCCSnf_Per = (TextBox)row.FindControl("txtCCSnf_Per");
            TextBox txtCCFATInKg = (TextBox)row.FindControl("txtCCFATInKg");
            TextBox txtCCSNFInKg = (TextBox)row.FindControl("txtCCSNFInKg");
            TextBox txtCCChallanNo = (TextBox)row.FindControl("txtCCChallanNo");
            if (txtCCQuantityInLtr.Text != "" && txtCCQuantityInKg.Text != "" && txtCCQuantityInLtr.Text != "0.00" && txtCCQuantityInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = ddlCC.SelectedValue;
                dr[1] = ddlCCTankerNo.SelectedValue;
                dr[2] = txtCCQuantityInLtr.Text;
                dr[3] = txtCCQuantityInKg.Text;
                dr[4] = txtCCFat_Per.Text;
                dr[5] = txtCCSnf_Per.Text;
                dr[6] = txtCCFATInKg.Text;
                dr[7] = txtCCSNFInKg.Text;
                dr[8] = ddlCCMilkQuality.SelectedValue;
                dr[9] = txtCCChallanNo.Text;
                dt.Rows.Add(dr);

            }
        }
        return dt;

    }
    protected void btnCCWiseProcurement_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtCCWP = new DataTable();
                dtCCWP = GetCCWiseProcuremetData();


                if (dtCCWP.Rows.Count > 0)
                {


                    if (btnCCWiseProcurement.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_InProcess_CCWiseProcurement"
                                                     },
                                            new DataTable[] {
                                                          dtCCWP
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnCCWiseProcurement.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"6",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                         "type_Production_Milk_InProcess_CCWiseProcurement"
                                                     },
                                            new DataTable[] { 
                                                            dtCCWP
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnCCWiseProcurementGetTotal_Click(object sender, EventArgs e)
    {
        btnCCWiseProcurement.Enabled = false;
        decimal CCWiseTQtyLtr = 0;
        decimal CCWiseTQtyKg = 0;
        decimal CCWiseTFatKg = 0;
        decimal CCWiseTSnfKg = 0;
        foreach (GridViewRow rows in gvCCWiseProcurement.Rows)
        {
            TextBox txtCCQuantityInLtr = (TextBox)rows.FindControl("txtCCQuantityInLtr");
            TextBox txtCCQuantityInKg = (TextBox)rows.FindControl("txtCCQuantityInKg");
            TextBox txtCCFATInKg = (TextBox)rows.FindControl("txtCCFATInKg");
            TextBox txtCCSnfInKg = (TextBox)rows.FindControl("txtCCSnfInKg");
            if (txtCCQuantityInLtr.Text != "")
            {
                CCWiseTQtyLtr += decimal.Parse(txtCCQuantityInLtr.Text);
            }
            if (txtCCQuantityInKg.Text != "")
            {
                CCWiseTQtyKg += decimal.Parse(txtCCQuantityInKg.Text);
            }
            if (txtCCFATInKg.Text != "")
            {
                CCWiseTFatKg += decimal.Parse(txtCCFATInKg.Text);
            }
            if (txtCCSnfInKg.Text != "")
            {
                CCWiseTSnfKg += decimal.Parse(txtCCSnfInKg.Text);
            }
        }

        gvCCWiseProcurement.FooterRow.Cells[3].Text = "<b>Total : </b>";
        gvCCWiseProcurement.FooterRow.Cells[4].Text = "<b>" + CCWiseTQtyKg.ToString() + "</b>";
        gvCCWiseProcurement.FooterRow.Cells[5].Text = "<b>" + CCWiseTQtyLtr.ToString() + "</b>";

        gvCCWiseProcurement.FooterRow.Cells[8].Text = "<b>" + CCWiseTFatKg.ToString() + "</b>";
        gvCCWiseProcurement.FooterRow.Cells[9].Text = "<b>" + CCWiseTSnfKg.ToString() + "</b>";

        if (CCWiseTQtyKg > 0)
        {
            btnCCWiseProcurement.Enabled = true;
        }
    }
    #endregion

    #region MDP/CC WISE GOAT MILK RECEIPT
    protected void gvCCWiseGoatMilkProcurement_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "MDP/CC WISE GOAT MILK RECEIPT";
            HeaderCell.ColumnSpan = 11;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvCCWiseGoatMilkProcurement.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private void SetInFlowGoatMilkInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("Office_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
        dt.Columns.Add(new DataColumn("I_TankerID", typeof(string)));
        dt.Columns.Add(new DataColumn("V_VehicleNo", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("FAT", typeof(string)));
        dt.Columns.Add(new DataColumn("SNF", typeof(string)));
        dt.Columns.Add(new DataColumn("FatInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("SnfInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkQuality", typeof(string)));
        dt.Columns.Add(new DataColumn("ChallanNo", typeof(string)));

        dr = dt.NewRow();


        dr["Office_ID"] = string.Empty;
        dr["Office_Name"] = string.Empty;
        dr["I_TankerID"] = string.Empty;
        dr["V_VehicleNo"] = string.Empty;
        dr["QtyInLtr"] = string.Empty;
        dr["QtyInKg"] = string.Empty;
        dr["FAT"] = string.Empty;
        dr["SNF"] = string.Empty;
        dr["FatInKg"] = string.Empty;
        dr["SnfInKg"] = string.Empty;
        dr["MilkQuality"] = string.Empty;
        dr["ChallanNo"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["InFlowGoatMilkCurrentTable"] = dt;


        gvCCWiseGoatMilkProcurement.DataSource = dt;
        gvCCWiseGoatMilkProcurement.DataBind();



        DropDownList ddl1 = (DropDownList)gvCCWiseGoatMilkProcurement.Rows[0].Cells[1].FindControl("ddlCCGoatMilk");
        DropDownList ddl2 = (DropDownList)gvCCWiseGoatMilkProcurement.Rows[0].Cells[2].FindControl("ddlCCGoatMilkTankerNo");


        //DropDownList ddl2 = (DropDownList)Gridview1.Rows[0].Cells[2].FindControl("ddlStyle");
        FillCC(ddl1);
        FillTanker(ddl2);
    }
    private void AddInFlowGoatMilkNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["InFlowGoatMilkCurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["InFlowGoatMilkCurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlCCGoatMilk = (DropDownList)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[1].FindControl("ddlCCGoatMilk");
                    DropDownList ddlCCGoatMilkTankerNo = (DropDownList)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[2].FindControl("ddlCCGoatMilkTankerNo");
                    TextBox txtCCGoatMilkChallanNo = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[3].FindControl("txtCCGoatMilkChallanNo");
                    DropDownList ddlCCGoatMilkQuality = (DropDownList)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[4].FindControl("ddlCCGoatMilkQuality");
                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");
                    if (ddlCCGoatMilk.SelectedIndex != 0)
                    {
                        TextBox QtyInLtr = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[5].FindControl("txtCCGoatMilkQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[6].FindControl("txtCCGoatMilkQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[7].FindControl("txtCCGoatMilkFat_Per");
                        TextBox SNF_Per = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[8].FindControl("txtCCGoatMilkSnf_Per");
                        TextBox FatInKg = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[9].FindControl("txtCCGoatMilkFATInKg");
                        TextBox SNFInkg = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[10].FindControl("txtCCGoatMilkSnfInKg");



                        drCurrentRow = dtCurrentTable.NewRow();


                        dtCurrentTable.Rows[i - 1]["Office_ID"] = ddlCCGoatMilk.SelectedValue;
                        //dtCurrentTable.Rows[i - 1]["Column2"] = ddlSt.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["Office_Name"] = ddlCCGoatMilk.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["I_TankerID"] = ddlCCGoatMilkTankerNo.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["V_VehicleNo"] = ddlCCGoatMilkTankerNo.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["ChallanNo"] = txtCCGoatMilkChallanNo.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInLtr"] = QtyInLtr.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInKg"] = QtyInKg.Text;
                        dtCurrentTable.Rows[i - 1]["FAT"] = FAT_Per.Text;
                        dtCurrentTable.Rows[i - 1]["SNF"] = SNF_Per.Text;
                        dtCurrentTable.Rows[i - 1]["FatInKg"] = FatInKg.Text;
                        dtCurrentTable.Rows[i - 1]["SnfInKg"] = SNFInkg.Text;
                        dtCurrentTable.Rows[i - 1]["MilkQuality"] = ddlCCGoatMilkQuality.SelectedItem.Text;

                        rowIndex++;

                    }
                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["InFlowGoatMilkCurrentTable"] = dtCurrentTable;

                    gvCCWiseGoatMilkProcurement.DataSource = dtCurrentTable;
                    gvCCWiseGoatMilkProcurement.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["InFlowGoatMilkCurrentTable"] = null;
        }

        //Set Previous Data on Postbacks


        SetInFlowGoatMilkPreviousData();
    }

    private void SetInFlowGoatMilkPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["InFlowGoatMilkCurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["InFlowGoatMilkCurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlCCGoatMilk = (DropDownList)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[1].FindControl("ddlCCGoatMilk");
                        DropDownList ddlCCGoatMilkTankerNo = (DropDownList)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[2].FindControl("ddlCCGoatMilkTankerNo");
                        TextBox txtCCGoatMilkChallanNo = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[3].FindControl("txtCCGoatMilkChallanNo");
                        DropDownList ddlCCGoatMilkQuality = (DropDownList)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[4].FindControl("ddlCCGoatMilkQuality");

                        TextBox QtyInLtr = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[5].FindControl("txtCCGoatMilkQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[6].FindControl("txtCCGoatMilkQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[7].FindControl("txtCCGoatMilkFat_Per");
                        TextBox SNF_Per = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[8].FindControl("txtCCGoatMilkSnf_Per");
                        TextBox FatInKg = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[9].FindControl("txtCCGoatMilkFATInKg");
                        TextBox SNFInkg = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[10].FindControl("txtCCGoatMilkSnfInKg");


                        FillCC(ddlCCGoatMilk);
                        FillTanker(ddlCCGoatMilkTankerNo);
                        if (i < dt.Rows.Count - 1)
                        {
                            ddlCCGoatMilk.ClearSelection();
                            ddlCCGoatMilk.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;

                            ddlCCGoatMilkTankerNo.ClearSelection();
                            ddlCCGoatMilkTankerNo.Items.FindByValue(dt.Rows[i]["I_TankerID"].ToString()).Selected = true;

                            ddlCCGoatMilkQuality.ClearSelection();
                            ddlCCGoatMilkQuality.Items.FindByValue(dt.Rows[i]["MilkQuality"].ToString()).Selected = true;


                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        txtCCGoatMilkChallanNo.Text = dt.Rows[i]["ChallanNo"].ToString();
                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();

                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void SetInFlowGoatMilkOnRemove()
    {
        int rowIndex = 0;
        double Total = 0;
        if (ViewState["InFlowGoatMilkCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["InFlowGoatMilkCurrentTable"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlCCGoatMilk = (DropDownList)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[1].FindControl("ddlCCGoatMilk");
                    DropDownList ddlCCGoatMilkTankerNo = (DropDownList)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[2].FindControl("ddlCCGoatMilkTankerNo");
                    TextBox txtCCGoatMilkChallanNo = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[3].FindControl("txtCCGoatMilkChallanNo");
                    DropDownList ddlCCGoatMilkQuality = (DropDownList)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[4].FindControl("ddlCCGoatMilkQuality");
                    TextBox QtyInLtr = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[5].FindControl("txtCCGoatMilkQuantityInLtr");
                    TextBox QtyInKg = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[6].FindControl("txtCCGoatMilkQuantityInKg");
                    TextBox FAT_Per = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[7].FindControl("txtCCGoatMilkFat_Per");
                    TextBox SNF_Per = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[8].FindControl("txtCCGoatMilkSnf_Per");
                    TextBox FatInKg = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[9].FindControl("txtCCGoatMilkFATInKg");
                    TextBox SNFInkg = (TextBox)gvCCWiseGoatMilkProcurement.Rows[rowIndex].Cells[10].FindControl("txtCCGoatMilkSnfInKg");

                    FillCC(ddlCCGoatMilk);
                    FillTanker(ddlCCGoatMilkTankerNo);
                    if (i < dt.Rows.Count)
                    {
                        if (dt.Rows[i]["Office_ID"].ToString() != "")
                        {
                            ddlCCGoatMilk.ClearSelection();
                            ddlCCGoatMilk.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;
                            ddlCCGoatMilkTankerNo.ClearSelection();
                            ddlCCGoatMilkTankerNo.Items.FindByValue(dt.Rows[i]["I_TankerID"].ToString()).Selected = true;

                            ddlCCGoatMilkQuality.ClearSelection();
                            ddlCCGoatMilkQuality.Items.FindByValue(dt.Rows[i]["MilkQuality"].ToString()).Selected = true;


                        }
                    }

                    ddlCCGoatMilk.SelectedValue = dt.Rows[i]["Office_ID"].ToString();
                    txtCCGoatMilkChallanNo.Text = dt.Rows[i]["ChallanNo"].ToString();
                    QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                    QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                    FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                    SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                    FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                    SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();

                    rowIndex++;

                }

            }

        }
    }
    protected void btnCCGoatMilkRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["InFlowGoatMilkrowID"] = rowID.ToString();
        if (ViewState["InFlowGoatMilkCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["InFlowGoatMilkCurrentTable"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["InFlowGoatMilkCurrentTable"] = dt;

            //Re bind the GridView for the updated data  
            gvCCWiseGoatMilkProcurement.DataSource = dt;
            gvCCWiseGoatMilkProcurement.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetInFlowGoatMilkOnRemove();
    }
    protected void btnCCGoatMilkAdd_Click(object sender, EventArgs e)
    {
        ViewState["InFlowGoatMilkrowID"] = "";
        //BindGrid();
        AddInFlowGoatMilkNewRowToGrid();
    }
    private DataTable GetCCWiseGoatMilkProcuremetData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Office_Id", typeof(string));
        dt.Columns.Add("I_TankerID", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        dt.Columns.Add("MilkQuality", typeof(string));
        dt.Columns.Add("ChallanNo", typeof(string));
        foreach (GridViewRow row in gvCCWiseGoatMilkProcurement.Rows)
        {
            DropDownList ddlCC = (DropDownList)row.FindControl("ddlCCGoatMilk");
            DropDownList ddlCCTankerNo = (DropDownList)row.FindControl("ddlCCGoatMilkTankerNo");
            DropDownList ddlCCMilkQuality = (DropDownList)row.FindControl("ddlCCGoatMilkQuality");
            TextBox txtCCQuantityInLtr = (TextBox)row.FindControl("txtCCGoatMilkQuantityInLtr");
            TextBox txtCCGoatMilkChallanNo = (TextBox)row.FindControl("txtCCGoatMilkChallanNo");
            TextBox txtCCQuantityInKg = (TextBox)row.FindControl("txtCCGoatMilkQuantityInKg");
            TextBox txtCCFat_Per = (TextBox)row.FindControl("txtCCGoatMilkFat_Per");
            TextBox txtCCSnf_Per = (TextBox)row.FindControl("txtCCGoatMilkSnf_Per");
            TextBox txtCCFATInKg = (TextBox)row.FindControl("txtCCGoatMilkFATInKg");
            TextBox txtCCSNFInKg = (TextBox)row.FindControl("txtCCGoatMilkSnfInKg");
            if (txtCCQuantityInLtr.Text != "" && txtCCQuantityInKg.Text != "" && txtCCQuantityInLtr.Text != "0.00" && txtCCQuantityInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = ddlCC.SelectedValue;
                dr[1] = ddlCCTankerNo.SelectedValue;
                dr[2] = txtCCQuantityInLtr.Text;
                dr[3] = txtCCQuantityInKg.Text;
                dr[4] = txtCCFat_Per.Text;
                dr[5] = txtCCSnf_Per.Text;
                dr[6] = txtCCFATInKg.Text;
                dr[7] = txtCCSNFInKg.Text;
                dr[8] = ddlCCMilkQuality.SelectedValue;
                dr[9] = txtCCGoatMilkChallanNo.Text;
                dt.Rows.Add(dr);

            }
        }
        return dt;

    }
    protected void btnCCWiseGoatMilkProcurement_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtCCWGMP = new DataTable();
                dtCCWGMP = GetCCWiseGoatMilkProcuremetData();


                if (dtCCWGMP.Rows.Count > 0)
                {


                    if (btnCCWiseGoatMilkProcurement.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_InProcess_CCWiseGoatMilkProcurement"
                                                     },
                                            new DataTable[] {
                                                          dtCCWGMP
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnCCWiseGoatMilkProcurement.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"30",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                         "type_Production_Milk_InProcess_CCWiseGoatMilkProcurement"
                                                     },
                                            new DataTable[] { 
                                                            dtCCWGMP
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnCCWiseGoatMilkProcurementGetTotal_Click(object sender, EventArgs e)
    {
        btnCCWiseGoatMilkProcurement.Enabled = false;
        decimal CCWiseGoatMilkTQtyLtr = 0;
        decimal CCWiseGoatMilkTQtyKg = 0;
        decimal CCWiseGoatMilkTFatKg = 0;
        decimal CCWiseGoatMilkTSnfKg = 0;
        foreach (GridViewRow rows in gvCCWiseGoatMilkProcurement.Rows)
        {
            TextBox txtCCGoatMilkQuantityInLtr = (TextBox)rows.FindControl("txtCCGoatMilkQuantityInLtr");
            TextBox txtCCGoatMilkQuantityInKg = (TextBox)rows.FindControl("txtCCGoatMilkQuantityInKg");
            TextBox txtCCGoatMilkFATInKg = (TextBox)rows.FindControl("txtCCGoatMilkFATInKg");
            TextBox txtCCGoatMilkSnfInKg = (TextBox)rows.FindControl("txtCCGoatMilkSnfInKg");
            if (txtCCGoatMilkQuantityInLtr.Text != "")
            {
                CCWiseGoatMilkTQtyLtr += decimal.Parse(txtCCGoatMilkQuantityInLtr.Text);
            }
            if (txtCCGoatMilkQuantityInKg.Text != "")
            {
                CCWiseGoatMilkTQtyKg += decimal.Parse(txtCCGoatMilkQuantityInKg.Text);
            }
            if (txtCCGoatMilkFATInKg.Text != "")
            {
                CCWiseGoatMilkTFatKg += decimal.Parse(txtCCGoatMilkFATInKg.Text);
            }
            if (txtCCGoatMilkSnfInKg.Text != "")
            {
                CCWiseGoatMilkTSnfKg += decimal.Parse(txtCCGoatMilkSnfInKg.Text);
            }
        }

        gvCCWiseGoatMilkProcurement.FooterRow.Cells[3].Text = "<b>Total : </b>";
        gvCCWiseGoatMilkProcurement.FooterRow.Cells[4].Text = "<b>" + CCWiseGoatMilkTQtyKg.ToString() + "</b>";
        gvCCWiseGoatMilkProcurement.FooterRow.Cells[5].Text = "<b>" + CCWiseGoatMilkTQtyLtr.ToString() + "</b>";

        gvCCWiseGoatMilkProcurement.FooterRow.Cells[8].Text = "<b>" + CCWiseGoatMilkTFatKg.ToString() + "</b>";
        gvCCWiseGoatMilkProcurement.FooterRow.Cells[9].Text = "<b>" + CCWiseGoatMilkTSnfKg.ToString() + "</b>";

        if (CCWiseGoatMilkTQtyKg > 0)
        {
            btnCCWiseGoatMilkProcurement.Enabled = true;
        }
    }
    #endregion

    #region Received From Other Union

    protected void gvrcvdfrmothrUnion_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Received From Other Union";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvrcvdfrmothrUnion.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private void SetRecvdFromOtherUnionInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("Office_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkTypeID", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkTypeName", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("FAT", typeof(string)));
        dt.Columns.Add(new DataColumn("SNF", typeof(string)));
        dt.Columns.Add(new DataColumn("FatInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("SnfInKg", typeof(string)));


        dr = dt.NewRow();


        dr["Office_ID"] = string.Empty;
        dr["Office_Name"] = string.Empty;
        dr["MilkTypeID"] = string.Empty;
        dr["MilkTypeName"] = string.Empty;
        dr["QtyInLtr"] = string.Empty;
        dr["QtyInKg"] = string.Empty;
        dr["FAT"] = string.Empty;
        dr["SNF"] = string.Empty;
        dr["FatInKg"] = string.Empty;
        dr["SnfInKg"] = string.Empty;

        dt.Rows.Add(dr);


        ViewState["InFlowRecvdfromothrunion"] = dt;


        gvrcvdfrmothrUnion.DataSource = dt;
        gvrcvdfrmothrUnion.DataBind();



        DropDownList ddl1 = (DropDownList)gvrcvdfrmothrUnion.Rows[0].Cells[1].FindControl("ddlrcvdfrmothrUnion");

        DropDownList ddl2 = (DropDownList)gvrcvdfrmothrUnion.Rows[0].Cells[2].FindControl("ddlrcvdfrmothrUnionMilkType");
        //DropDownList ddl2 = (DropDownList)Gridview1.Rows[0].Cells[2].FindControl("ddlStyle");
        FillMilkType(ddl2);
        FillUnion(ddl1);

    }
    private void AddRecvdFromOtherUnionNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["InFlowRecvdfromothrunion"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["InFlowRecvdfromothrunion"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlrcvdfrmothrUnion = (DropDownList)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[1].FindControl("ddlrcvdfrmothrUnion");
                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");
                    if (ddlrcvdfrmothrUnion.SelectedIndex != 0)
                    {
                        DropDownList ddlrcvdfrmothrUnionMilkType = (DropDownList)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[2].FindControl("ddlrcvdfrmothrUnionMilkType");
                        TextBox QtyInLtr = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[4].FindControl("txtrcvdfrmothrUnionQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[3].FindControl("txtrcvdfrmothrUnionQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[5].FindControl("txtrcvdfrmothrUnionFat_Per");
                        TextBox SNF_Per = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[6].FindControl("txtrcvdfrmothrUnionSnf_Per");
                        TextBox FatInKg = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[7].FindControl("txtrcvdfrmothrUnionFATInKg");
                        TextBox SNFInkg = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[8].FindControl("txtrcvdfrmothrUnionSNFInKg");



                        drCurrentRow = dtCurrentTable.NewRow();


                        dtCurrentTable.Rows[i - 1]["Office_ID"] = ddlrcvdfrmothrUnion.SelectedValue;
                        //dtCurrentTable.Rows[i - 1]["Column2"] = ddlSt.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["Office_Name"] = ddlrcvdfrmothrUnion.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["MilkTypeID"] = ddlrcvdfrmothrUnionMilkType.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["MilkTypeName"] = ddlrcvdfrmothrUnionMilkType.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInLtr"] = QtyInLtr.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInKg"] = QtyInKg.Text;
                        dtCurrentTable.Rows[i - 1]["FAT"] = FAT_Per.Text;
                        dtCurrentTable.Rows[i - 1]["SNF"] = SNF_Per.Text;
                        dtCurrentTable.Rows[i - 1]["FatInKg"] = FatInKg.Text;
                        dtCurrentTable.Rows[i - 1]["SnfInKg"] = SNFInkg.Text;


                        rowIndex++;

                    }
                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["InFlowRecvdfromothrunion"] = dtCurrentTable;

                    gvrcvdfrmothrUnion.DataSource = dtCurrentTable;
                    gvrcvdfrmothrUnion.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["InFlowRecvdfromothrunion"] = null;
        }

        //Set Previous Data on Postbacks


        SetRecvdFromOtherUnionPreviousData();
    }

    private void SetRecvdFromOtherUnionPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["InFlowRecvdfromothrunion"] != null)
            {

                DataTable dt = (DataTable)ViewState["InFlowRecvdfromothrunion"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlrcvdfrmothrUnion = (DropDownList)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[1].FindControl("ddlrcvdfrmothrUnion");
                        DropDownList ddlrcvdfrmothrUnionMilkType = (DropDownList)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[2].FindControl("ddlrcvdfrmothrUnionMilkType");
                        TextBox QtyInLtr = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[4].FindControl("txtrcvdfrmothrUnionQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[3].FindControl("txtrcvdfrmothrUnionQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[5].FindControl("txtrcvdfrmothrUnionFat_Per");
                        TextBox SNF_Per = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[6].FindControl("txtrcvdfrmothrUnionSnf_Per");
                        TextBox FatInKg = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[7].FindControl("txtrcvdfrmothrUnionFATInKg");
                        TextBox SNFInkg = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[8].FindControl("txtrcvdfrmothrUnionSNFInKg");


                        FillUnion(ddlrcvdfrmothrUnion);
                        FillMilkType(ddlrcvdfrmothrUnionMilkType);
                        if (i < dt.Rows.Count - 1)
                        {
                            ddlrcvdfrmothrUnion.ClearSelection();
                            ddlrcvdfrmothrUnion.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;

                            ddlrcvdfrmothrUnionMilkType.ClearSelection();
                            ddlrcvdfrmothrUnionMilkType.Items.FindByValue(dt.Rows[i]["MilkTypeID"].ToString()).Selected = true;


                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();

                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();

                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    private void SetRecvdFromOtherUnionOnRemove()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["InFlowRecvdfromothrunion"] != null)
            {

                DataTable dt = (DataTable)ViewState["InFlowRecvdfromothrunion"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlrcvdfrmothrUnion = (DropDownList)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[1].FindControl("ddlrcvdfrmothrUnion");
                        DropDownList ddlrcvdfrmothrUnionMilkType = (DropDownList)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[2].FindControl("ddlrcvdfrmothrUnionMilkType");
                        TextBox QtyInLtr = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[4].FindControl("txtrcvdfrmothrUnionQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[3].FindControl("txtrcvdfrmothrUnionQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[5].FindControl("txtrcvdfrmothrUnionFat_Per");
                        TextBox SNF_Per = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[6].FindControl("txtrcvdfrmothrUnionSnf_Per");
                        TextBox FatInKg = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[7].FindControl("txtrcvdfrmothrUnionFATInKg");
                        TextBox SNFInkg = (TextBox)gvrcvdfrmothrUnion.Rows[rowIndex].Cells[8].FindControl("txtrcvdfrmothrUnionSNFInKg");


                        FillUnion(ddlrcvdfrmothrUnion);
                        FillMilkType(ddlrcvdfrmothrUnionMilkType);
                        if (i < dt.Rows.Count)
                        {
                            ddlrcvdfrmothrUnion.ClearSelection();
                            ddlrcvdfrmothrUnion.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;

                            ddlrcvdfrmothrUnionMilkType.ClearSelection();
                            ddlrcvdfrmothrUnionMilkType.Items.FindByValue(dt.Rows[i]["MilkTypeID"].ToString()).Selected = true;


                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();

                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();

                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    private DataTable GetRecvdFromOtherUnionData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Office_Id", typeof(string));
        dt.Columns.Add("MilkTypeID", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvrcvdfrmothrUnion.Rows)
        {
            DropDownList ddlrcvdfrmothrUnion = (DropDownList)row.FindControl("ddlrcvdfrmothrUnion");
            DropDownList ddlrcvdfrmothrUnionMilkType = (DropDownList)row.FindControl("ddlrcvdfrmothrUnionMilkType");
            TextBox txtrcvdfrmothrUnionQuantityInLtr = (TextBox)row.FindControl("txtrcvdfrmothrUnionQuantityInLtr");
            TextBox txtrcvdfrmothrUnionQuantityInKg = (TextBox)row.FindControl("txtrcvdfrmothrUnionQuantityInKg");
            TextBox txtrcvdfrmothrUnionFat_Per = (TextBox)row.FindControl("txtrcvdfrmothrUnionFat_Per");
            TextBox txtrcvdfrmothrUnionSnf_Per = (TextBox)row.FindControl("txtrcvdfrmothrUnionSnf_Per");
            TextBox txtrcvdfrmothrUnionFATInKg = (TextBox)row.FindControl("txtrcvdfrmothrUnionFATInKg");
            TextBox txtrcvdfrmothrUnionSnfInKg = (TextBox)row.FindControl("txtrcvdfrmothrUnionSnfInKg");
            if (txtrcvdfrmothrUnionQuantityInLtr.Text != "" && txtrcvdfrmothrUnionQuantityInKg.Text != "" && txtrcvdfrmothrUnionQuantityInLtr.Text != "0.00" && txtrcvdfrmothrUnionQuantityInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = ddlrcvdfrmothrUnion.SelectedValue;
                dr[1] = ddlrcvdfrmothrUnionMilkType.SelectedValue;
                dr[2] = txtrcvdfrmothrUnionQuantityInLtr.Text;
                dr[3] = txtrcvdfrmothrUnionQuantityInKg.Text;
                dr[4] = txtrcvdfrmothrUnionFat_Per.Text;
                dr[5] = txtrcvdfrmothrUnionSnf_Per.Text;
                dr[6] = txtrcvdfrmothrUnionFATInKg.Text;
                dr[7] = txtrcvdfrmothrUnionSnfInKg.Text;

                dt.Rows.Add(dr);

            }
        }
        return dt;

    }
    protected void btnrcvdfrmothrUnionRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["RecvdfromothrunionrowID"] = rowID.ToString();
        if (ViewState["InFlowRecvdfromothrunion"] != null)
        {
            DataTable dt = (DataTable)ViewState["InFlowRecvdfromothrunion"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["InFlowRecvdfromothrunion"] = dt;

            //Re bind the GridView for the updated data  
            gvrcvdfrmothrUnion.DataSource = dt;
            gvrcvdfrmothrUnion.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetRecvdFromOtherUnionOnRemove();
    }
    protected void btnrcvdfrmothrUnionAdd_Click(object sender, EventArgs e)
    {
        ViewState["RecvdfromothrunionrowID"] = "";
        //BindGrid();
        AddRecvdFromOtherUnionNewRowToGrid();
    }
    protected void btnrcvdfrmothrUnion_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtrcvdfromothrunion = new DataTable();
                dtrcvdfromothrunion = GetRecvdFromOtherUnionData();


                //if (dtrcvdfromothrunion.Rows.Count > 0)
                //{


                if (btnrcvdfrmothrUnion.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                {
                    DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                        new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                        new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                        new string[] { 
                                                       "type_Production_Milk_InProcess_RecvdfromotherUnion"
                                                     },
                                        new DataTable[] { 
                                                          dtrcvdfromothrunion
                                          },
                                                          "TableSave");
                    if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        txtDate_TextChanged(sender, e);
                    }

                    else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                    {
                        string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                    }

                    else
                    {
                        string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                    }
                }
                else if (btnrcvdfrmothrUnion.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                {
                    DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                        new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                        new string[]{"7",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                        new string[] { 
                                                          "type_Production_Milk_InProcess_RecvdfromotherUnion"
                                                     },
                                        new DataTable[] { 
                                                            dtrcvdfromothrunion
                                          },
                                                          "TableSave");
                    if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        txtDate_TextChanged(sender, e);

                    }

                    else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                    {
                        string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                    }

                    else
                    {
                        string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                    }
                }
                //}
                //else
                //{
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                //}
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnrcvdfrmothrUnionGetTotal_Click(object sender, EventArgs e)
    {
        btnrcvdfrmothrUnion.Enabled = false;
        decimal RcvdFromothrUnionTQtyLtr = 0;
        decimal RcvdFromothrUnionTQtyKg = 0;
        decimal RcvdFromothrUnionTFatKg = 0;
        decimal RcvdFromothrUnionTSnfKg = 0;
        foreach (GridViewRow rows in gvrcvdfrmothrUnion.Rows)
        {
            TextBox txtrcvdfrmothrUnionQuantityInLtr = (TextBox)rows.FindControl("txtrcvdfrmothrUnionQuantityInLtr");
            TextBox txtrcvdfrmothrUnionQuantityInKg = (TextBox)rows.FindControl("txtrcvdfrmothrUnionQuantityInKg");
            TextBox txtrcvdfrmothrUnionFATInKg = (TextBox)rows.FindControl("txtrcvdfrmothrUnionFATInKg");
            TextBox txtrcvdfrmothrUnionSnfInKg = (TextBox)rows.FindControl("txtrcvdfrmothrUnionSnfInKg");
            if (txtrcvdfrmothrUnionQuantityInLtr.Text != "")
            {
                RcvdFromothrUnionTQtyLtr += decimal.Parse(txtrcvdfrmothrUnionQuantityInLtr.Text);
            }
            if (txtrcvdfrmothrUnionQuantityInKg.Text != "")
            {
                RcvdFromothrUnionTQtyKg += decimal.Parse(txtrcvdfrmothrUnionQuantityInKg.Text);
            }
            if (txtrcvdfrmothrUnionFATInKg.Text != "")
            {
                RcvdFromothrUnionTFatKg += decimal.Parse(txtrcvdfrmothrUnionFATInKg.Text);
            }
            if (txtrcvdfrmothrUnionSnfInKg.Text != "")
            {
                RcvdFromothrUnionTSnfKg += decimal.Parse(txtrcvdfrmothrUnionSnfInKg.Text);
            }
        }

        gvrcvdfrmothrUnion.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvrcvdfrmothrUnion.FooterRow.Cells[2].Text = "<b>" + RcvdFromothrUnionTQtyKg.ToString() + "</b>";
        gvrcvdfrmothrUnion.FooterRow.Cells[3].Text = "<b>" + RcvdFromothrUnionTQtyLtr.ToString() + "</b>";

        gvrcvdfrmothrUnion.FooterRow.Cells[6].Text = "<b>" + RcvdFromothrUnionTFatKg.ToString() + "</b>";
        gvrcvdfrmothrUnion.FooterRow.Cells[7].Text = "<b>" + RcvdFromothrUnionTSnfKg.ToString() + "</b>";
        if (RcvdFromothrUnionTQtyKg > 0)
        {
            btnrcvdfrmothrUnion.Enabled = true;
        }
    }
    #endregion

    #region For Powder Conversion
    protected void gvForPowderConversion_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "For Powder Conversion";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvForPowderConversion.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private void SetForPowderConversionInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("Office_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkTypeID", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkTypeName", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("FAT", typeof(string)));
        dt.Columns.Add(new DataColumn("SNF", typeof(string)));
        dt.Columns.Add(new DataColumn("FatInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("SnfInKg", typeof(string)));


        dr = dt.NewRow();


        dr["Office_ID"] = string.Empty;
        dr["Office_Name"] = string.Empty;
        dr["MilkTypeID"] = string.Empty;
        dr["MilkTypeName"] = string.Empty;
        dr["QtyInLtr"] = string.Empty;
        dr["QtyInKg"] = string.Empty;
        dr["FAT"] = string.Empty;
        dr["SNF"] = string.Empty;
        dr["FatInKg"] = string.Empty;
        dr["SnfInKg"] = string.Empty;

        dt.Rows.Add(dr);


        ViewState["InFlowforPowderConversion"] = dt;


        gvForPowderConversion.DataSource = dt;
        gvForPowderConversion.DataBind();



        DropDownList ddl1 = (DropDownList)gvForPowderConversion.Rows[0].Cells[1].FindControl("ddlForPowderConversion");
        DropDownList ddl2 = (DropDownList)gvForPowderConversion.Rows[0].Cells[2].FindControl("ddlForPowderConversionMilkType");

        //DropDownList ddl2 = (DropDownList)Gridview1.Rows[0].Cells[2].FindControl("ddlStyle");
        FillUnion(ddl1);
        FillMilkType(ddl2);
    }
    private void AddForPowderConversionNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["InFlowforPowderConversion"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["InFlowforPowderConversion"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlForPowderConversion = (DropDownList)gvForPowderConversion.Rows[rowIndex].Cells[1].FindControl("ddlForPowderConversion");
                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");
                    if (ddlForPowderConversion.SelectedIndex != 0)
                    {
                        DropDownList ddlForPowderConversionMilkType = (DropDownList)gvForPowderConversion.Rows[rowIndex].Cells[2].FindControl("ddlForPowderConversionMilkType");
                        TextBox QtyInLtr = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[4].FindControl("txtForPowderConversionQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[3].FindControl("txtForPowderConversionQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[5].FindControl("txtForPowderConversionFat_Per");
                        TextBox SNF_Per = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[6].FindControl("txtForPowderConversionSnf_Per");
                        TextBox FatInKg = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[7].FindControl("txtForPowderConversionFATInKg");
                        TextBox SNFInkg = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[8].FindControl("txtForPowderConversionSNFInKg");



                        drCurrentRow = dtCurrentTable.NewRow();


                        dtCurrentTable.Rows[i - 1]["Office_ID"] = ddlForPowderConversion.SelectedValue;
                        //dtCurrentTable.Rows[i - 1]["Column2"] = ddlSt.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["Office_Name"] = ddlForPowderConversion.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["MilkTypeID"] = ddlForPowderConversionMilkType.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["MilkTypeName"] = ddlForPowderConversionMilkType.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInLtr"] = QtyInLtr.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInKg"] = QtyInKg.Text;
                        dtCurrentTable.Rows[i - 1]["FAT"] = FAT_Per.Text;
                        dtCurrentTable.Rows[i - 1]["SNF"] = SNF_Per.Text;
                        dtCurrentTable.Rows[i - 1]["FatInKg"] = FatInKg.Text;
                        dtCurrentTable.Rows[i - 1]["SnfInKg"] = SNFInkg.Text;


                        rowIndex++;

                    }
                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["InFlowforPowderConversion"] = dtCurrentTable;

                    gvForPowderConversion.DataSource = dtCurrentTable;
                    gvForPowderConversion.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["InFlowforPowderConversion"] = null;
        }

        //Set Previous Data on Postbacks


        SetForPowderConversionPreviousData();
    }
    private void SetForPowderConversionPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["InFlowforPowderConversion"] != null)
            {

                DataTable dt = (DataTable)ViewState["InFlowforPowderConversion"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlForPowderConversion = (DropDownList)gvForPowderConversion.Rows[rowIndex].Cells[1].FindControl("ddlForPowderConversion");
                        DropDownList ddlForPowderConversionMilkType = (DropDownList)gvForPowderConversion.Rows[rowIndex].Cells[2].FindControl("ddlForPowderConversionMilkType");
                        TextBox QtyInLtr = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[4].FindControl("txtForPowderConversionQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[3].FindControl("txtForPowderConversionQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[5].FindControl("txtForPowderConversionFat_Per");
                        TextBox SNF_Per = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[6].FindControl("txtForPowderConversionSnf_Per");
                        TextBox FatInKg = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[7].FindControl("txtForPowderConversionFATInKg");
                        TextBox SNFInkg = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[8].FindControl("txtForPowderConversionSNFInKg");



                        FillUnion(ddlForPowderConversion);
                        FillMilkType(ddlForPowderConversionMilkType);
                        if (i < dt.Rows.Count - 1)
                        {
                            ddlForPowderConversion.ClearSelection();
                            ddlForPowderConversion.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;

                            ddlForPowderConversionMilkType.ClearSelection();
                            ddlForPowderConversionMilkType.Items.FindByValue(dt.Rows[i]["MilkTypeID"].ToString()).Selected = true;


                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();

                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();

                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    private void SetForPowderConversionOnRemove()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["InFlowforPowderConversion"] != null)
            {

                DataTable dt = (DataTable)ViewState["InFlowforPowderConversion"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlForPowderConversion = (DropDownList)gvForPowderConversion.Rows[rowIndex].Cells[1].FindControl("ddlForPowderConversion");
                        DropDownList ddlForPowderConversionMilkType = (DropDownList)gvForPowderConversion.Rows[rowIndex].Cells[2].FindControl("ddlForPowderConversionMilkType");
                        TextBox QtyInLtr = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[4].FindControl("txtForPowderConversionQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[3].FindControl("txtForPowderConversionQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[5].FindControl("txtForPowderConversionFat_Per");
                        TextBox SNF_Per = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[6].FindControl("txtForPowderConversionSnf_Per");
                        TextBox FatInKg = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[7].FindControl("txtForPowderConversionFATInKg");
                        TextBox SNFInkg = (TextBox)gvForPowderConversion.Rows[rowIndex].Cells[8].FindControl("txtForPowderConversionSNFInKg");



                        FillUnion(ddlForPowderConversion);
                        FillMilkType(ddlForPowderConversionMilkType);
                        if (i < dt.Rows.Count)
                        {
                            ddlForPowderConversion.ClearSelection();
                            ddlForPowderConversion.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;

                            ddlForPowderConversionMilkType.ClearSelection();
                            ddlForPowderConversionMilkType.Items.FindByValue(dt.Rows[i]["MilkTypeID"].ToString()).Selected = true;


                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();

                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();

                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnForPowderConversionRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["ForPowderConversionrowID"] = rowID.ToString();
        if (ViewState["InFlowforPowderConversion"] != null)
        {
            DataTable dt = (DataTable)ViewState["InFlowforPowderConversion"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["InFlowforPowderConversion"] = dt;

            //Re bind the GridView for the updated data  
            gvForPowderConversion.DataSource = dt;
            gvForPowderConversion.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetForPowderConversionOnRemove();
    }
    protected void btnForPowderConversion_Click(object sender, EventArgs e)
    {
        ViewState["ForPowderConversionrowID"] = "";
        //BindGrid();
        AddForPowderConversionNewRowToGrid();
    }
    private DataTable GetForPowderConversionData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Office_Id", typeof(string));
        dt.Columns.Add("MilkTypeID", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvForPowderConversion.Rows)
        {
            DropDownList ddlForPowderConversion = (DropDownList)row.FindControl("ddlForPowderConversion");
            DropDownList ddlForPowderConversionMilkType = (DropDownList)row.FindControl("ddlForPowderConversionMilkType");
            TextBox txtForPowderConversionQuantityInLtr = (TextBox)row.FindControl("txtForPowderConversionQuantityInLtr");
            TextBox txtForPowderConversionQuantityInKg = (TextBox)row.FindControl("txtForPowderConversionQuantityInKg");
            TextBox txtForPowderConversionFat_Per = (TextBox)row.FindControl("txtForPowderConversionFat_Per");
            TextBox txtForPowderConversionSnf_Per = (TextBox)row.FindControl("txtForPowderConversionSnf_Per");
            TextBox txtForPowderConversionFATInKg = (TextBox)row.FindControl("txtForPowderConversionFATInKg");
            TextBox txtForPowderConversionSnfInKg = (TextBox)row.FindControl("txtForPowderConversionSnfInKg");
            if (txtForPowderConversionQuantityInLtr.Text != "" && txtForPowderConversionQuantityInKg.Text != "" && txtForPowderConversionQuantityInLtr.Text != "0.00" && txtForPowderConversionQuantityInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = ddlForPowderConversion.SelectedValue;
                dr[1] = ddlForPowderConversionMilkType.SelectedValue;
                dr[2] = txtForPowderConversionQuantityInLtr.Text;
                dr[3] = txtForPowderConversionQuantityInKg.Text;
                dr[4] = txtForPowderConversionFat_Per.Text;
                dr[5] = txtForPowderConversionSnf_Per.Text;
                dr[6] = txtForPowderConversionFATInKg.Text;
                dr[7] = txtForPowderConversionSnfInKg.Text;

                dt.Rows.Add(dr);

            }
        }
        return dt;

    }
    protected void btnSaveForPowderConversion_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtforpowdrconv = new DataTable();
                dtforpowdrconv = GetForPowderConversionData();


                if (dtforpowdrconv.Rows.Count > 0)
                {


                    if (btnSaveForPowderConversion.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_InProcess_ForPowderConversion"
                                                      
                                                     },
                                            new DataTable[] { 
                                                          dtforpowdrconv
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnSaveForPowderConversion.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"8",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                          "type_Production_Milk_InProcess_ForPowderConversion"
                                                     },
                                            new DataTable[] { 
                                                            dtforpowdrconv
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnForPowderConversionGetTotal_Click(object sender, EventArgs e)
    {
        btnSaveForPowderConversion.Enabled = false;
        decimal ForPowderConversionTQtyLtr = 0;
        decimal ForPowderConversionTQtyKg = 0;
        decimal ForPowderConversionTFatKg = 0;
        decimal ForPowderConversionTSnfKg = 0;
        foreach (GridViewRow rows in gvForPowderConversion.Rows)
        {
            TextBox txtForPowderConversionQuantityInLtr = (TextBox)rows.FindControl("txtForPowderConversionQuantityInLtr");
            TextBox txtForPowderConversionQuantityInKg = (TextBox)rows.FindControl("txtForPowderConversionQuantityInKg");
            TextBox txtForPowderConversionFATInKg = (TextBox)rows.FindControl("txtForPowderConversionFATInKg");
            TextBox txtForPowderConversionSnfInKg = (TextBox)rows.FindControl("txtForPowderConversionSnfInKg");
            if (txtForPowderConversionQuantityInLtr.Text != "")
            {
                ForPowderConversionTQtyLtr += decimal.Parse(txtForPowderConversionQuantityInLtr.Text);
            }
            if (txtForPowderConversionQuantityInKg.Text != "")
            {
                ForPowderConversionTQtyKg += decimal.Parse(txtForPowderConversionQuantityInKg.Text);
            }
            if (txtForPowderConversionFATInKg.Text != "")
            {
                ForPowderConversionTFatKg += decimal.Parse(txtForPowderConversionFATInKg.Text);
            }
            if (txtForPowderConversionSnfInKg.Text != "")
            {
                ForPowderConversionTSnfKg += decimal.Parse(txtForPowderConversionSnfInKg.Text);
            }
        }

        gvForPowderConversion.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvForPowderConversion.FooterRow.Cells[2].Text = "<b>" + ForPowderConversionTQtyKg.ToString() + "</b>";
        gvForPowderConversion.FooterRow.Cells[3].Text = "<b>" + ForPowderConversionTQtyLtr.ToString() + "</b>";

        gvForPowderConversion.FooterRow.Cells[6].Text = "<b>" + ForPowderConversionTFatKg.ToString() + "</b>";
        gvForPowderConversion.FooterRow.Cells[7].Text = "<b>" + ForPowderConversionTSnfKg.ToString() + "</b>";
        if (ForPowderConversionTQtyKg > 0)
        {
            btnSaveForPowderConversion.Enabled = true;
        }
    }
    #endregion

    #region BMC/DCS Collection
    protected void gvBMCDCSCollection_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "BMC/DCS Collection";
            HeaderCell.ColumnSpan = 10;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvBMCDCSCollection.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private void SetBMCDCSCollectionInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("I_TankerID", typeof(string)));
        dt.Columns.Add(new DataColumn("V_VehicleNo", typeof(string)));
        //dt.Columns.Add(new DataColumn("BMCTankerRoot_Id", typeof(string)));
        dt.Columns.Add(new DataColumn("BMCTankerRootName", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkQuality", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("FAT", typeof(string)));
        dt.Columns.Add(new DataColumn("SNF", typeof(string)));
        dt.Columns.Add(new DataColumn("FatInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("SnfInKg", typeof(string)));


        dr = dt.NewRow();


        dr["I_TankerID"] = string.Empty;
        dr["V_VehicleNo"] = string.Empty;
        //dr["BMCTankerRoot_Id"] = string.Empty;
        dr["BMCTankerRootName"] = string.Empty;
        dr["MilkQuality"] = string.Empty;
        dr["QtyInLtr"] = string.Empty;
        dr["QtyInKg"] = string.Empty;
        dr["FAT"] = string.Empty;
        dr["SNF"] = string.Empty;
        dr["FatInKg"] = string.Empty;
        dr["SnfInKg"] = string.Empty;

        dt.Rows.Add(dr);


        ViewState["BMCDCSCollection"] = dt;


        gvBMCDCSCollection.DataSource = dt;
        gvBMCDCSCollection.DataBind();


        DropDownList ddl1 = (DropDownList)gvBMCDCSCollection.Rows[0].Cells[1].FindControl("ddlTanker");
        //DropDownList ddl2 = (DropDownList)gvBMCDCSCollection.Rows[0].Cells[1].FindControl("ddlBMCTankerRoot");

        FillTanker(ddl1);
        //FillBMCRoot(ddl2);

    }
    private void AddBMCDCSCollectionNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["BMCDCSCollection"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["BMCDCSCollection"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlTanker = (DropDownList)gvBMCDCSCollection.Rows[rowIndex].Cells[1].FindControl("ddlTanker");
                    // DropDownList ddlBMCTankerRoot = (DropDownList)gvBMCDCSCollection.Rows[rowIndex].Cells[2].FindControl("ddlBMCTankerRoot");

                    if (ddlTanker.SelectedIndex != 0)
                    {
                        TextBox BMCTankerRootName = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[4].FindControl("txtBMCRootName");
                        DropDownList MilkQuality = (DropDownList)gvBMCDCSCollection.Rows[rowIndex].Cells[3].FindControl("ddlBMCDCSCollectionMilkQuality");
                        //TextBox QtyInLtr = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[4].FindControl("txtBMCDCSCollectionQtyInLtr");
                        TextBox QtyInKg = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[4].FindControl("txtBMCDCSCollectionQtyInKg");
                        TextBox FAT_Per = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[5].FindControl("txtBMCDCSCollectionFat_Per");
                        TextBox SNF_Per = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[6].FindControl("txtBMCDCSCollectionSnf_Per");
                        TextBox FatInKg = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[7].FindControl("txtBMCDCSCollectionFATInKg");
                        TextBox SNFInkg = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[8].FindControl("txtBMCDCSCollectionSNFInKg");



                        drCurrentRow = dtCurrentTable.NewRow();


                        dtCurrentTable.Rows[i - 1]["I_TankerID"] = ddlTanker.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["V_VehicleNo"] = ddlTanker.SelectedItem.Text;
                        //dtCurrentTable.Rows[i - 1]["BMCTankerRoot_Id"] = ddlBMCTankerRoot.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["BMCTankerRootName"] = BMCTankerRootName.Text;
                        dtCurrentTable.Rows[i - 1]["MilkQuality"] = MilkQuality.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInLtr"] = "0";
                        dtCurrentTable.Rows[i - 1]["QtyInKg"] = QtyInKg.Text;
                        dtCurrentTable.Rows[i - 1]["FAT"] = FAT_Per.Text;
                        dtCurrentTable.Rows[i - 1]["SNF"] = SNF_Per.Text;
                        dtCurrentTable.Rows[i - 1]["FatInKg"] = FatInKg.Text;
                        dtCurrentTable.Rows[i - 1]["SnfInKg"] = SNFInkg.Text;


                        rowIndex++;

                    }
                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["BMCDCSCollection"] = dtCurrentTable;

                    gvBMCDCSCollection.DataSource = dtCurrentTable;
                    gvBMCDCSCollection.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["BMCDCSCollection"] = null;
        }

        //Set Previous Data on Postbacks


        SetBMCDCSCollectionPreviousData();
    }
    private void SetBMCDCSCollectionPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["BMCDCSCollection"] != null)
            {

                DataTable dt = (DataTable)ViewState["BMCDCSCollection"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlTanker = (DropDownList)gvBMCDCSCollection.Rows[rowIndex].Cells[1].FindControl("ddlTanker");
                        // DropDownList ddlBMCTankerRoot = (DropDownList)gvBMCDCSCollection.Rows[rowIndex].Cells[2].FindControl("ddlBMCTankerRoot");
                        TextBox BMCTankerRootName = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[4].FindControl("txtBMCRootName");
                        DropDownList MilkQuality = (DropDownList)gvBMCDCSCollection.Rows[rowIndex].Cells[3].FindControl("ddlBMCDCSCollectionMilkQuality");

                        TextBox QtyInKg = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[4].FindControl("txtBMCDCSCollectionQtyInKg");
                        TextBox FAT_Per = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[5].FindControl("txtBMCDCSCollectionFat_Per");
                        TextBox SNF_Per = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[6].FindControl("txtBMCDCSCollectionSnf_Per");
                        TextBox FatInKg = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[7].FindControl("txtBMCDCSCollectionFATInKg");
                        TextBox SNFInkg = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[8].FindControl("txtBMCDCSCollectionSNFInKg");


                        FillTanker(ddlTanker);
                        // FillBMCRoot(ddlBMCTankerRoot);
                        if (i < dt.Rows.Count - 1)
                        {
                            ddlTanker.ClearSelection();
                            ddlTanker.Items.FindByValue(dt.Rows[i]["I_TankerID"].ToString()).Selected = true;

                            //ddlBMCTankerRoot.ClearSelection();
                            //ddlBMCTankerRoot.Items.FindByValue(dt.Rows[i]["BMCTankerRoot_Id"].ToString()).Selected = true;



                        }
                        BMCTankerRootName.Text = dt.Rows[i]["BMCTankerRootName"].ToString();
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        MilkQuality.Text = dt.Rows[i]["MilkQuality"].ToString();
                        //QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();

                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    private void SetBMCDCSCollectionOnRemove()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["BMCDCSCollection"] != null)
            {

                DataTable dt = (DataTable)ViewState["BMCDCSCollection"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlTanker = (DropDownList)gvBMCDCSCollection.Rows[rowIndex].Cells[1].FindControl("ddlTanker");
                        // DropDownList ddlBMCTankerRoot = (DropDownList)gvBMCDCSCollection.Rows[rowIndex].Cells[2].FindControl("ddlBMCTankerRoot");
                        TextBox BMCTankerRootName = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[4].FindControl("txtBMCRootName");
                        DropDownList MilkQuality = (DropDownList)gvBMCDCSCollection.Rows[rowIndex].Cells[3].FindControl("ddlBMCDCSCollectionMilkQuality");

                        TextBox QtyInKg = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[4].FindControl("txtBMCDCSCollectionQtyInKg");
                        TextBox FAT_Per = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[5].FindControl("txtBMCDCSCollectionFat_Per");
                        TextBox SNF_Per = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[6].FindControl("txtBMCDCSCollectionSnf_Per");
                        TextBox FatInKg = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[7].FindControl("txtBMCDCSCollectionFATInKg");
                        TextBox SNFInkg = (TextBox)gvBMCDCSCollection.Rows[rowIndex].Cells[8].FindControl("txtBMCDCSCollectionSNFInKg");


                        FillTanker(ddlTanker);
                        // FillBMCRoot(ddlBMCTankerRoot);
                        if (i < dt.Rows.Count)
                        {
                            ddlTanker.ClearSelection();
                            ddlTanker.Items.FindByValue(dt.Rows[i]["I_TankerID"].ToString()).Selected = true;

                            //ddlBMCTankerRoot.ClearSelection();
                            //ddlBMCTankerRoot.Items.FindByValue(dt.Rows[i]["BMCTankerRoot_Id"].ToString()).Selected = true;



                        }
                        BMCTankerRootName.Text = dt.Rows[i]["BMCTankerRootName"].ToString();
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        MilkQuality.Text = dt.Rows[i]["MilkQuality"].ToString();
                        //QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();

                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnBMCDCSCollectionRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["BMCDCSCollectionrowID"] = rowID.ToString();
        if (ViewState["BMCDCSCollection"] != null)
        {
            DataTable dt = (DataTable)ViewState["BMCDCSCollection"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["BMCDCSCollection"] = dt;

            //Re bind the GridView for the updated data  
            gvBMCDCSCollection.DataSource = dt;
            gvBMCDCSCollection.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetBMCDCSCollectionOnRemove();
    }
    protected void btnBMCDCSCollectionAdd_Click(object sender, EventArgs e)
    {
        ViewState["BMCDCSCollectionrowID"] = "";
        //BindGrid();
        AddBMCDCSCollectionNewRowToGrid();
    }
    private DataTable GetBMCDCSCollData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("I_TankerID", typeof(string));
        dt.Columns.Add("BMCTankerRootName", typeof(string));
        dt.Columns.Add("MilkQuality", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvBMCDCSCollection.Rows)
        {
            DropDownList ddlTanker = (DropDownList)row.FindControl("ddlTanker");
            TextBox txtBMCRootName = (TextBox)row.FindControl("txtBMCRootName");
            DropDownList ddlBMCDCSCollectionMilkQuality = (DropDownList)row.FindControl("ddlBMCDCSCollectionMilkQuality");
            //TextBox txtBMCDCSCollectionQtyInLtr = (TextBox)row.FindControl("txtBMCDCSCollectionQtyInLtr");
            TextBox txtBMCDCSCollectionQtyInKg = (TextBox)row.FindControl("txtBMCDCSCollectionQtyInKg");
            TextBox txtBMCDCSCollectionFat_per = (TextBox)row.FindControl("txtBMCDCSCollectionFat_per");
            TextBox txtBMCDCSCollectionSnf_Per = (TextBox)row.FindControl("txtBMCDCSCollectionSnf_Per");
            TextBox txtBMCDCSCollectionFatInKg = (TextBox)row.FindControl("txtBMCDCSCollectionFatInKg");
            TextBox txtBMCDCSCollectionSnfInKg = (TextBox)row.FindControl("txtBMCDCSCollectionSnfInKg");

            if (txtBMCDCSCollectionQtyInKg.Text != "" && txtBMCDCSCollectionQtyInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = ddlTanker.SelectedValue;
                dr[1] = txtBMCRootName.Text;
                dr[2] = ddlBMCDCSCollectionMilkQuality.SelectedValue;
                dr[3] = "0";
                dr[4] = txtBMCDCSCollectionQtyInKg.Text;
                dr[5] = txtBMCDCSCollectionFat_per.Text;
                dr[6] = txtBMCDCSCollectionSnf_Per.Text;
                dr[7] = txtBMCDCSCollectionFatInKg.Text;
                dr[8] = txtBMCDCSCollectionSnfInKg.Text;
                dt.Rows.Add(dr);

            }

        }
        return dt;
    }
    protected void btnBMCDCSCollection_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtBMCDCSColl = new DataTable();
                dtBMCDCSColl = GetBMCDCSCollData();


                if (dtBMCDCSColl.Rows.Count > 0)
                {


                    if (btnBMCDCSCollection.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_InProcess_BMCDCSColl"
                                                       
                                                     },
                                            new DataTable[] { 
                                                          dtBMCDCSColl
                                                         
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnBMCDCSCollection.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"9",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                         "type_Production_Milk_InProcess_BMCDCSColl"
                                                     },
                                            new DataTable[] {
                                                            dtBMCDCSColl
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnBMCDCSCollectionGetTotal_Click(object sender, EventArgs e)
    {
        btnBMCDCSCollection.Enabled = false;
        decimal BMCDCSCollTQtyLtr = 0;
        decimal BMCDCSCollTQtyKg = 0;
        decimal BMCDCSCollTFatKg = 0;
        decimal BMCDCSCollTSnfKg = 0;
        foreach (GridViewRow rows in gvBMCDCSCollection.Rows)
        {

            TextBox txtBMCDCSCollectionQtyInKg = (TextBox)rows.FindControl("txtBMCDCSCollectionQtyInKg");
            TextBox txtBMCDCSCollectionFatInKg = (TextBox)rows.FindControl("txtBMCDCSCollectionFatInKg");
            TextBox txtBMCDCSCollectionSnfInKg = (TextBox)rows.FindControl("txtBMCDCSCollectionSnfInKg");

            if (txtBMCDCSCollectionQtyInKg.Text != "")
            {
                BMCDCSCollTQtyKg += decimal.Parse(txtBMCDCSCollectionQtyInKg.Text);
            }
            if (txtBMCDCSCollectionFatInKg.Text != "")
            {
                BMCDCSCollTFatKg += decimal.Parse(txtBMCDCSCollectionFatInKg.Text);
            }
            if (txtBMCDCSCollectionSnfInKg.Text != "")
            {
                BMCDCSCollTSnfKg += decimal.Parse(txtBMCDCSCollectionSnfInKg.Text);
            }
        }
        gvBMCDCSCollection.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvBMCDCSCollection.FooterRow.Cells[3].Text = "<b>" + BMCDCSCollTQtyKg.ToString() + "</b>";


        gvBMCDCSCollection.FooterRow.Cells[6].Text = "<b>" + BMCDCSCollTFatKg.ToString() + "</b>";
        gvBMCDCSCollection.FooterRow.Cells[7].Text = "<b>" + BMCDCSCollTSnfKg.ToString() + "</b>";
        if (BMCDCSCollTQtyKg > 0)
        {
            btnBMCDCSCollection.Enabled = true;
        }
    }
    #endregion

    #region Cans Collection

    protected void gvCanesCollection_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Cans Collection";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvCanesCollection.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private void SetInFlowCansInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add("Variant", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        dt.Columns.Add("MilkQuality", typeof(string));


        dr = dt.NewRow();


        dr["Variant"] = string.Empty;        
        dr["QtyInKg"] = string.Empty;
        dr["Fat_Per"] = string.Empty;
        dr["Snf_Per"] = string.Empty;
        dr["KgFat"] = string.Empty;
        dr["KgSnf"] = string.Empty;
        dr["MilkQuality"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["InFlowCansCurrentTable"] = dt;


        gvCanesCollection.DataSource = dt;
        gvCanesCollection.DataBind();

    }
    private void AddInFlowCansNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["InFlowCansCurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["InFlowCansCurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    Label lblCanesCollectionVariant = (Label)gvCanesCollection.Rows[rowIndex].Cells[1].FindControl("lblCanesCollectionVariant");
                    DropDownList ddlCanesCollectionMilkQuality = (DropDownList)gvCanesCollection.Rows[rowIndex].Cells[2].FindControl("ddlCanesCollectionMilkQuality");
                    
                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");

                   
                        TextBox QtyInKg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[3].FindControl("txtCanesCollectionQtyInKg");
                        TextBox FAT_Per = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[4].FindControl("txtCanesCollectionFat_per");
                        TextBox SNF_Per = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[5].FindControl("txtCanesCollectionSnf_Per");
                        TextBox FatInKg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[6].FindControl("txtCanesCollectionFatInKg");
                        TextBox SNFInkg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[7].FindControl("txtCanesCollectionSnfInKg");


                        drCurrentRow = dtCurrentTable.NewRow();



                        dtCurrentTable.Rows[i - 1]["Variant"] = lblCanesCollectionVariant.Text;
             
                        dtCurrentTable.Rows[i - 1]["QtyInKg"] = QtyInKg.Text;
                        dtCurrentTable.Rows[i - 1]["Fat_Per"] = FAT_Per.Text;
                        dtCurrentTable.Rows[i - 1]["Snf_Per"] = SNF_Per.Text;
                        dtCurrentTable.Rows[i - 1]["KgFat"] = FatInKg.Text;
                        dtCurrentTable.Rows[i - 1]["KgSnf"] = SNFInkg.Text;
                        dtCurrentTable.Rows[i - 1]["MilkQuality"] = ddlCanesCollectionMilkQuality.SelectedItem.Text;

                        rowIndex++;

                   
                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                  ViewState["InFlowCansCurrentTable"] = dtCurrentTable;

                    gvCanesCollection.DataSource = dtCurrentTable;
                    gvCanesCollection.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["InFlowCansCurrentTable"] = null;
        }

        //Set Previous Data on Postbacks


        SetInFlowCansPreviousData();
    }
    private void SetInFlowCansPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["InFlowCansCurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["InFlowCansCurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Label lblCanesCollectionVariant = (Label)gvCanesCollection.Rows[rowIndex].Cells[1].FindControl("lblCanesCollectionVariant");
                        DropDownList ddlCanesCollectionMilkQuality = (DropDownList)gvCanesCollection.Rows[rowIndex].Cells[2].FindControl("ddlCanesCollectionMilkQuality");

                        //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");


                        TextBox QtyInKg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[3].FindControl("txtCanesCollectionQtyInKg");
                        TextBox FAT_Per = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[4].FindControl("txtCanesCollectionFat_per");
                        TextBox SNF_Per = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[5].FindControl("txtCanesCollectionSnf_Per");
                        TextBox FatInKg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[6].FindControl("txtCanesCollectionFatInKg");
                        TextBox SNFInkg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[7].FindControl("txtCanesCollectionSnfInKg");
                        if (i < dt.Rows.Count - 1)
                        {
                            ddlCanesCollectionMilkQuality.ClearSelection();
                            ddlCanesCollectionMilkQuality.Items.FindByValue(dt.Rows[i]["MilkQuality"].ToString()).Selected = true;
                        }
                       
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        lblCanesCollectionVariant.Text = "Via Canes";
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["Fat_Per"].ToString();
                        SNF_Per.Text = dt.Rows[i]["Snf_Per"].ToString();
                        FatInKg.Text = dt.Rows[i]["KgFat"].ToString();
                        SNFInkg.Text = dt.Rows[i]["KgSnf"].ToString();

                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void SetInFlowCansOnRemove()
    {
        int rowIndex = 0;
        double Total = 0;
        if (ViewState["InFlowCansCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["InFlowCansCurrentTable"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label lblCanesCollectionVariant = (Label)gvCanesCollection.Rows[rowIndex].Cells[1].FindControl("lblCanesCollectionVariant");
                    DropDownList ddlCanesCollectionMilkQuality = (DropDownList)gvCanesCollection.Rows[rowIndex].Cells[2].FindControl("ddlCanesCollectionMilkQuality");

                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");


                    TextBox QtyInKg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[3].FindControl("txtCanesCollectionQtyInKg");
                    TextBox FAT_Per = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[4].FindControl("txtCanesCollectionFat_per");
                    TextBox SNF_Per = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[5].FindControl("txtCanesCollectionSnf_Per");
                    TextBox FatInKg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[6].FindControl("txtCanesCollectionFatInKg");
                    TextBox SNFInkg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[7].FindControl("txtCanesCollectionSnfInKg");
                    if (i < dt.Rows.Count)
                    {
                        ddlCanesCollectionMilkQuality.ClearSelection();
                        ddlCanesCollectionMilkQuality.Items.FindByValue(dt.Rows[i]["MilkQuality"].ToString()).Selected = true;
                    }

                    lblCanesCollectionVariant.Text = dt.Rows[i]["Variant"].ToString();
                                     
                    QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                    FAT_Per.Text = dt.Rows[i]["Fat_Per"].ToString();
                    SNF_Per.Text = dt.Rows[i]["Snf_Per"].ToString();
                    FatInKg.Text = dt.Rows[i]["KgFat"].ToString();
                    SNFInkg.Text = dt.Rows[i]["KgSnf"].ToString();

                    rowIndex++;


                }

            }

        }
    }
    protected void SetInFlowCansPreviousOnRemove()
    {
        int rowIndex = 0;
        double Total = 0;
        if (ViewState["InFlowCansCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["InFlowCansCurrentTable"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label lblCanesCollectionVariant = (Label)gvCanesCollection.Rows[rowIndex].Cells[1].FindControl("lblCanesCollectionVariant");
                    DropDownList ddlCanesCollectionMilkQuality = (DropDownList)gvCanesCollection.Rows[rowIndex].Cells[2].FindControl("ddlCanesCollectionMilkQuality");

                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");


                    TextBox QtyInKg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[3].FindControl("txtCanesCollectionQtyInKg");
                    TextBox FAT_Per = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[4].FindControl("txtCanesCollectionFat_per");
                    TextBox SNF_Per = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[5].FindControl("txtCanesCollectionSnf_Per");
                    TextBox FatInKg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[6].FindControl("txtCanesCollectionFatInKg");
                    TextBox SNFInkg = (TextBox)gvCanesCollection.Rows[rowIndex].Cells[7].FindControl("txtCanesCollectionSnfInKg");




                    ddlCanesCollectionMilkQuality.ClearSelection();
                    ddlCanesCollectionMilkQuality.Items.FindByValue(dt.Rows[i]["MilkQuality"].ToString()).Selected = true;
                    QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                    FAT_Per.Text = dt.Rows[i]["Fat_Per"].ToString();
                    SNF_Per.Text = dt.Rows[i]["Snf_Per"].ToString();
                    FatInKg.Text = dt.Rows[i]["KgFat"].ToString();
                    SNFInkg.Text = dt.Rows[i]["KgSnf"].ToString();
                    lblCanesCollectionVariant.Text = dt.Rows[i]["Variant"].ToString();
                    rowIndex++;


                }

            }

        }
    }
    protected void btnCansRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["InFlowCansrowID"] = rowID.ToString();
        if (ViewState["InFlowCansCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["InFlowCansCurrentTable"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["InFlowCansCurrentTable"] = dt;

            //Re bind the GridView for the updated data  
            gvCanesCollection.DataSource = dt;
            gvCanesCollection.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetInFlowCansOnRemove();

    }
    protected void btnCansAdd_Click(object sender, EventArgs e)
    {
        ViewState["InFlowCansrowID"] = "";
        //BindGrid();
        AddInFlowCansNewRowToGrid();
    }
    private DataTable GetCanesCollData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Variant", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        dt.Columns.Add("MilkQuality", typeof(string));
        foreach (GridViewRow row in gvCanesCollection.Rows)
        {
            Label lblCanesCollectionVariant = (Label)row.FindControl("lblCanesCollectionVariant");
            DropDownList ddlCanesCollectionMilkQuality = (DropDownList)row.FindControl("ddlCanesCollectionMilkQuality");
            //TextBox txtinflowotherQtyInLtr = (TextBox)row.FindControl("txtinflowotherQtyInLtr");
            TextBox txtCanesCollectionQtyInKg = (TextBox)row.FindControl("txtCanesCollectionQtyInKg");
            TextBox txtCanesCollectionFat_per = (TextBox)row.FindControl("txtCanesCollectionFat_per");
            TextBox txtCanesCollectionSnf_Per = (TextBox)row.FindControl("txtCanesCollectionSnf_Per");
            TextBox txtCanesCollectionFatInKg = (TextBox)row.FindControl("txtCanesCollectionFatInKg");
            TextBox txtCanesCollectionSnfInKg = (TextBox)row.FindControl("txtCanesCollectionSnfInKg");

            if (txtCanesCollectionQtyInKg.Text != "" && txtCanesCollectionQtyInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblCanesCollectionVariant.Text;
                dr[1] = "0";
                dr[2] = txtCanesCollectionQtyInKg.Text;
                dr[3] = txtCanesCollectionFat_per.Text;
                dr[4] = txtCanesCollectionSnf_Per.Text;
                dr[5] = txtCanesCollectionFatInKg.Text;
                dr[6] = txtCanesCollectionSnfInKg.Text;
                dr[7] = ddlCanesCollectionMilkQuality.SelectedValue;
                dt.Rows.Add(dr);

            }

        }
        return dt;
    }
    protected void btnCanesCollection_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtCanesColl = new DataTable();
                dtCanesColl = GetCanesCollData();


                if (dtCanesColl.Rows.Count > 0)
                {


                    if (btnCanesCollection.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_InProcess_CanesCollection"
                                                      
                                                     },
                                            new DataTable[] { 
                                                          dtCanesColl
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnCanesCollection.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"10",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                          "type_Production_Milk_InProcess_CanesCollection"
                                                          
                                                     },
                                            new DataTable[] { 
                                                            dtCanesColl
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnCanesCollectionGetTotal_Click(object sender, EventArgs e)
    {
        btnCanesCollection.Enabled = false;
        decimal CanesCollTQtyLtr = 0;
        decimal CanesCollTQtyKg = 0;
        decimal CanesCollTFatKg = 0;
        decimal CanesCollTSnfKg = 0;
        foreach (GridViewRow rows in gvCanesCollection.Rows)
        {

            TextBox txtCanesCollectionQtyInKg = (TextBox)rows.FindControl("txtCanesCollectionQtyInKg");
            TextBox txtCanesCollectionFatInKg = (TextBox)rows.FindControl("txtCanesCollectionFatInKg");
            TextBox txtCanesCollectionSnfInKg = (TextBox)rows.FindControl("txtCanesCollectionSnfInKg");

            if (txtCanesCollectionQtyInKg.Text != "")
            {
                CanesCollTQtyKg += decimal.Parse(txtCanesCollectionQtyInKg.Text);
            }
            if (txtCanesCollectionFatInKg.Text != "")
            {
                CanesCollTFatKg += decimal.Parse(txtCanesCollectionFatInKg.Text);
            }
            if (txtCanesCollectionSnfInKg.Text != "")
            {
                CanesCollTSnfKg += decimal.Parse(txtCanesCollectionSnfInKg.Text);
            }
        }
        if (gvinflowother.Rows.Count > 0)
        {
            gvCanesCollection.FooterRow.Cells[0].Text = "<b>Total : </b>";

            gvCanesCollection.FooterRow.Cells[2].Text = "<b>" + CanesCollTQtyKg.ToString() + "</b>";
            gvCanesCollection.FooterRow.Cells[5].Text = "<b>" + CanesCollTFatKg.ToString() + "</b>";
            gvCanesCollection.FooterRow.Cells[6].Text = "<b>" + CanesCollTSnfKg.ToString() + "</b>";
        }
        if (CanesCollTQtyKg > 0)
        {
            btnCanesCollection.Enabled = true;
        }
    }

    #endregion

    #region Others(Used In Process)
    protected void gvinflowother_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Others(Used In Process)";
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvinflowother.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private DataTable GetInFlowOthersData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Variant", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvinflowother.Rows)
        {
            Label lblVariant = (Label)row.FindControl("lblVariant");
            //TextBox txtinflowotherQtyInLtr = (TextBox)row.FindControl("txtinflowotherQtyInLtr");
            TextBox txtinflowotherQtyInKg = (TextBox)row.FindControl("txtinflowotherQtyInKg");
            TextBox txtinflowotherFat_per = (TextBox)row.FindControl("txtinflowotherFat_per");
            TextBox txtinflowotherSnf_Per = (TextBox)row.FindControl("txtinflowotherSnf_Per");
            TextBox txtinflowotherFatInKg = (TextBox)row.FindControl("txtinflowotherFatInKg");
            TextBox txtinflowotherSnfInKg = (TextBox)row.FindControl("txtinflowotherSnfInKg");

            if (txtinflowotherQtyInKg.Text != "" && txtinflowotherQtyInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblVariant.Text;
                dr[1] = "0";
                dr[2] = txtinflowotherQtyInKg.Text;
                dr[3] = txtinflowotherFat_per.Text;
                dr[4] = txtinflowotherSnf_Per.Text;
                dr[5] = txtinflowotherFatInKg.Text;
                dr[6] = txtinflowotherSnfInKg.Text;
                dt.Rows.Add(dr);

            }

        }
        return dt;
    }
    protected void btnInFlowOther_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                DataTable dtInFlowOthers = new DataTable();
                dtInFlowOthers = GetInFlowOthersData();


                if (dtInFlowOthers.Rows.Count > 0)
                {


                    if (btnInFlowOther.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] {
                                                       "type_Production_Milk_InProcess_InflowOther"
                                                     },
                                            new DataTable[] { 
                                                          dtInFlowOthers
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnInFlowOther.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"11",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] {
                                                         "type_Production_Milk_InProcess_InflowOther"
                                                     },
                                            new DataTable[] { 
                                                            dtInFlowOthers
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnInFlowOtherGetTotal_Click(object sender, EventArgs e)
    {
        btnInFlowOther.Enabled = false;
        decimal InFlowOtherTQtyLtr = 0;
        decimal InFlowOtherTQtyKg = 0;
        decimal InFlowOtherTFatKg = 0;
        decimal InFlowOtherTSnfKg = 0;
        foreach (GridViewRow rows in gvinflowother.Rows)
        {

            TextBox txtinflowotherQtyInKg = (TextBox)rows.FindControl("txtinflowotherQtyInKg");
            TextBox txtinflowotherFatInKg = (TextBox)rows.FindControl("txtinflowotherFatInKg");
            TextBox txtinflowotherSnfInKg = (TextBox)rows.FindControl("txtinflowotherSnfInKg");

            if (txtinflowotherQtyInKg.Text != "")
            {
                InFlowOtherTQtyKg += decimal.Parse(txtinflowotherQtyInKg.Text);
            }
            if (txtinflowotherFatInKg.Text != "")
            {
                InFlowOtherTFatKg += decimal.Parse(txtinflowotherFatInKg.Text);
            }
            if (txtinflowotherSnfInKg.Text != "")
            {
                InFlowOtherTSnfKg += decimal.Parse(txtinflowotherSnfInKg.Text);
            }
        }
        if (gvinflowother.Rows.Count > 0)
        {
            gvinflowother.FooterRow.Cells[0].Text = "<b>Total : </b>";

            gvinflowother.FooterRow.Cells[1].Text = "<b>" + InFlowOtherTQtyKg.ToString() + "</b>";
            gvinflowother.FooterRow.Cells[4].Text = "<b>" + InFlowOtherTFatKg.ToString() + "</b>";
            gvinflowother.FooterRow.Cells[5].Text = "<b>" + InFlowOtherTSnfKg.ToString() + "</b>";
        }
        if (InFlowOtherTQtyKg > 0)
        {
            btnInFlowOther.Enabled = true;
        }
    }
    #endregion

    #endregion
    
    #region OutFlow

    #region PARTICULARS(Sales Figures)

    protected void gvPaticulars_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "PARTICULARS(Sales Figures)";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvPaticulars.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private DataTable GetParticularsData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Item_id", typeof(string));
        dt.Columns.Add("NoOfPackets", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvPaticulars.Rows)
        {
            Label lblPartcularItem_id = (Label)row.FindControl("lblPartcularItem_id");

            TextBox txtPaticularsNoOfPackets = (TextBox)row.FindControl("txtPaticularsNoOfPackets");
            TextBox txtPaticularsQuantityInLtr = (TextBox)row.FindControl("txtPaticularsQuantityInLtr");
            TextBox txtPaticularsQuantityInKg = (TextBox)row.FindControl("txtPaticularsQuantityInKg");
            TextBox txtParticularFat_Per = (TextBox)row.FindControl("txtParticularFat_Per");
            TextBox txtParticularSnf_Per = (TextBox)row.FindControl("txtParticularSnf_Per");
            TextBox txtParticularFATInKg = (TextBox)row.FindControl("txtParticularFATInKg");
            TextBox txtParticularSnfInKg = (TextBox)row.FindControl("txtParticularSnfInKg");

            if (txtPaticularsQuantityInLtr.Text != "" && txtPaticularsQuantityInLtr.Text != "0.00" && txtPaticularsQuantityInLtr.Text != "" && txtPaticularsQuantityInLtr.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblPartcularItem_id.Text;
                dr[1] = txtPaticularsNoOfPackets.Text;
                dr[2] = txtPaticularsQuantityInLtr.Text;
                dr[3] = txtPaticularsQuantityInKg.Text;
                dr[4] = txtParticularFat_Per.Text;
                dr[5] = txtParticularSnf_Per.Text;
                dr[6] = txtParticularFATInKg.Text;
                dr[7] = txtParticularSnfInKg.Text;
                dt.Rows.Add(dr);

            }
        }
        return dt;
    }
    protected void btnPaticulars_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtParticulars = new DataTable();
                dtParticulars = GetParticularsData();


                if (dtParticulars.Rows.Count > 0)
                {


                    if (btnPaticulars.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_OutProcess_Particular"
                                                      
                                                     },
                                            new DataTable[] { 
                                                          dtParticulars
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnPaticulars.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"12",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                         "type_Production_Milk_OutProcess_Particular"
                                                     },
                                            new DataTable[] { 
                                                            dtParticulars
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnPaticularsGetTotal_Click(object sender, EventArgs e)
    {
        btnPaticulars.Enabled = false;
        decimal ParticularPacketTQtyLtr = 0;
        decimal ParticularPacketTQtyKg = 0;
        decimal ParticularTFatKg = 0;
        decimal ParticularTSnfKg = 0;
        foreach (GridViewRow rows in gvPaticulars.Rows)
        {

            TextBox txtPaticularsQuantityInLtr = (TextBox)rows.FindControl("txtPaticularsQuantityInLtr");
            TextBox txtPaticularsQuantityInKg = (TextBox)rows.FindControl("txtPaticularsQuantityInKg");
            TextBox txtParticularFATInKg = (TextBox)rows.FindControl("txtParticularFATInKg");
            TextBox txtParticularSnfInKg = (TextBox)rows.FindControl("txtParticularSnfInKg");

            if (txtPaticularsQuantityInLtr.Text != "")
            {
                ParticularPacketTQtyLtr += decimal.Parse(txtPaticularsQuantityInLtr.Text);
            }
            if (txtPaticularsQuantityInKg.Text != "")
            {
                ParticularPacketTQtyKg += decimal.Parse(txtPaticularsQuantityInKg.Text);
            }
            if (txtParticularFATInKg.Text != "")
            {
                ParticularTFatKg += decimal.Parse(txtParticularFATInKg.Text);
            }
            if (txtParticularSnfInKg.Text != "")
            {
                ParticularTSnfKg += decimal.Parse(txtParticularSnfInKg.Text);
            }
        }
        if (gvPaticulars.Rows.Count > 0)
        {
            gvPaticulars.FooterRow.Cells[0].Text = "<b>Total : </b>";
            gvPaticulars.FooterRow.Cells[2].Text = "<b>" + ParticularPacketTQtyLtr.ToString() + "</b>";
            gvPaticulars.FooterRow.Cells[1].Text = "<b>" + ParticularPacketTQtyKg.ToString() + "</b>";
            gvPaticulars.FooterRow.Cells[6].Text = "<b>" + ParticularTFatKg.ToString() + "</b>";
            gvPaticulars.FooterRow.Cells[7].Text = "<b>" + ParticularTSnfKg.ToString() + "</b>";
        }
        if (ParticularPacketTQtyKg > 0)
        {
            btnPaticulars.Enabled = true;
        }
    }
    #endregion

    #region MILK TO INDIGENOUS PRODUCT

    protected void gvMilkToIP_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "MILK TO INDIGENOUS PRODUCT";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvMilkToIP.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private DataTable GetMIPData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("ItemType_id", typeof(string));
        dt.Columns.Add("MilkType", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvMilkToIP.Rows)
        {
            Label lblMilkToIPItemType_id = (Label)row.FindControl("lblMilkToIPItemType_id");
            DropDownList ddlMilkType = (DropDownList)row.FindControl("ddlMilkType");
            TextBox txtMilkToIPQuantityInLtr = (TextBox)row.FindControl("txtMilkToIPQuantityInLtr");
            TextBox txtMilkToIPQuantityInKg = (TextBox)row.FindControl("txtMilkToIPQuantityInKg");
            TextBox txtMilkToIPFat_Per = (TextBox)row.FindControl("txtMilkToIPFat_Per");
            TextBox txtMilkToIPSnf_Per = (TextBox)row.FindControl("txtMilkToIPSnf_Per");
            TextBox txtMilkToIPFATInKg = (TextBox)row.FindControl("txtMilkToIPFATInKg");
            TextBox txtMilkToIPSNFInKg = (TextBox)row.FindControl("txtMilkToIPSNFInKg");
            if (txtMilkToIPQuantityInLtr.Text != "" && txtMilkToIPQuantityInKg.Text != "" && txtMilkToIPQuantityInLtr.Text != "0.00" && txtMilkToIPQuantityInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblMilkToIPItemType_id.Text;
                dr[1] = ddlMilkType.SelectedItem.Text;
                dr[2] = txtMilkToIPQuantityInLtr.Text;
                dr[3] = txtMilkToIPQuantityInKg.Text;
                dr[4] = txtMilkToIPFat_Per.Text;
                dr[5] = txtMilkToIPSnf_Per.Text;
                dr[6] = txtMilkToIPFATInKg.Text;
                dr[7] = txtMilkToIPSNFInKg.Text;
                dt.Rows.Add(dr);

            }
        }
        return dt;
    }
    protected void btnMilkToIP_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtMIP = new DataTable();
                dtMIP = GetMIPData();


                if (dtMIP.Rows.Count > 0)
                {


                    if (btnMilkToIP.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_OutProcess_MIP",
                                                       
                                                     },
                                            new DataTable[] {  
                                                          dtMIP
                                                          
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnMilkToIP.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"13",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                         "type_Production_Milk_OutProcess_MIP"
                                                     },
                                            new DataTable[] {  
                                                            dtMIP
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnMilkToIPGetTotal_Click(object sender, EventArgs e)
    {
        btnMilkToIP.Enabled = false;
        decimal MIPPacketTQtyLtr = 0;
        decimal MIPPacketTQtyKg = 0;
        decimal MIPTFatKg = 0;
        decimal MIPTSnfKg = 0;
        foreach (GridViewRow rows in gvMilkToIP.Rows)
        {

            TextBox txtMilkToIPQuantityInLtr = (TextBox)rows.FindControl("txtMilkToIPQuantityInLtr");
            TextBox txtMilkToIPQuantityInKg = (TextBox)rows.FindControl("txtMilkToIPQuantityInKg");
            TextBox txtMilkToIPFATInKg = (TextBox)rows.FindControl("txtMilkToIPFATInKg");
            TextBox txtMilkToIPSnfInKg = (TextBox)rows.FindControl("txtMilkToIPSnfInKg");

            if (txtMilkToIPQuantityInLtr.Text != "")
            {
                MIPPacketTQtyLtr += decimal.Parse(txtMilkToIPQuantityInLtr.Text);
            }
            if (txtMilkToIPQuantityInKg.Text != "")
            {
                MIPPacketTQtyKg += decimal.Parse(txtMilkToIPQuantityInKg.Text);
            }
            if (txtMilkToIPFATInKg.Text != "")
            {
                MIPTFatKg += decimal.Parse(txtMilkToIPFATInKg.Text);
            }
            if (txtMilkToIPSnfInKg.Text != "")
            {
                MIPTSnfKg += decimal.Parse(txtMilkToIPSnfInKg.Text);
            }
        }

        gvMilkToIP.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvMilkToIP.FooterRow.Cells[3].Text = "<b>" + MIPPacketTQtyLtr.ToString() + "</b>";
        gvMilkToIP.FooterRow.Cells[2].Text = "<b>" + MIPPacketTQtyKg.ToString() + "</b>";
        gvMilkToIP.FooterRow.Cells[6].Text = "<b>" + MIPTFatKg.ToString() + "</b>";
        gvMilkToIP.FooterRow.Cells[7].Text = "<b>" + MIPTSnfKg.ToString() + "</b>";
        if (MIPPacketTQtyKg > 0)
        {
            btnMilkToIP.Enabled = true;
        }
    }
    #endregion

    #region ISSUE TO MDP/CC

    protected void gvIssuetoMDPOrCC_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "ISSUE TO MDP/CC";
            HeaderCell.ColumnSpan = 11;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvIssuetoMDPOrCC.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private void SetOutFlowInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("Office_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("FAT", typeof(string)));
        dt.Columns.Add(new DataColumn("SNF", typeof(string)));
        dt.Columns.Add(new DataColumn("FatInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("SnfInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("I_TankerID", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkType", typeof(string)));
        dt.Columns.Add(new DataColumn("ChallanNo", typeof(string)));

        dr = dt.NewRow();


        dr["Office_ID"] = string.Empty;
        dr["Office_Name"] = string.Empty;
        dr["QtyInLtr"] = string.Empty;
        dr["QtyInKg"] = string.Empty;
        dr["FAT"] = string.Empty;
        dr["SNF"] = string.Empty;
        dr["FatInKg"] = string.Empty;
        dr["SnfInKg"] = string.Empty;
        dr["I_TankerID"] = string.Empty;
        dr["MilkType"] = string.Empty;
        dr["ChallanNo"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;

        gvIssuetoMDPOrCC.DataSource = dt;
        gvIssuetoMDPOrCC.DataBind();

        DropDownList ddl1 = (DropDownList)gvIssuetoMDPOrCC.Rows[0].Cells[1].FindControl("ddlMDPCC");
        DropDownList ddl2 = (DropDownList)gvIssuetoMDPOrCC.Rows[0].Cells[1].FindControl("ddlMDPCCTankerNo");
        //DropDownList ddl2 = (DropDownList)Gridview1.Rows[0].Cells[2].FindControl("ddlStyle");
        FillMDPORCC(ddl1);
        FillTanker(ddl2);

    }
    private void AddOutFlowNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlMDPCC = (DropDownList)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[1].FindControl("ddlMDPCC");
                    DropDownList ddlMDPCCTankerNo = (DropDownList)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[2].FindControl("ddlMDPCCTankerNo");
                    TextBox txtMDPCCChallanNo = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[3].FindControl("txtMDPCCChallanNo");
                    DropDownList ddlMDPCCMilkQuality = (DropDownList)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[4].FindControl("ddlMDPCCMilkQuality");
                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");
                    if (ddlMDPCC.SelectedIndex != 0)
                    {
                        TextBox QtyInKg = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[5].FindControl("txtIssuetoMDPOrCCQuantityInKg");
                        TextBox QtyInLtr = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[6].FindControl("txtIssuetoMDPOrCCQuantityInLtr");                    
                        TextBox FAT_Per = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[7].FindControl("txtIssuetoMDPOrCCFat_Per");
                        TextBox SNF_Per = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[8].FindControl("txtIssuetoMDPOrCCSnf_Per");
                        TextBox FatInKg = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[9].FindControl("txtIssuetoMDPOrCCFATInKg");
                        TextBox SNFInkg = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[10].FindControl("txtIssuetoMDPOrCCSnfInKg");




                        drCurrentRow = dtCurrentTable.NewRow();


                        dtCurrentTable.Rows[i - 1]["Office_ID"] = ddlMDPCC.SelectedValue;
                        //dtCurrentTable.Rows[i - 1]["Column2"] = ddlSt.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["Office_Name"] = ddlMDPCC.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInLtr"] = QtyInLtr.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInKg"] = QtyInKg.Text;
                        dtCurrentTable.Rows[i - 1]["FAT"] = FAT_Per.Text;
                        dtCurrentTable.Rows[i - 1]["SNF"] = SNF_Per.Text;
                        dtCurrentTable.Rows[i - 1]["FatInKg"] = FatInKg.Text;
                        dtCurrentTable.Rows[i - 1]["SnfInKg"] = SNFInkg.Text;
                        dtCurrentTable.Rows[i - 1]["I_TankerID"] = ddlMDPCCTankerNo.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["MilkType"] = ddlMDPCCMilkQuality.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["ChallanNo"] = txtMDPCCChallanNo.Text;
                        rowIndex++;

                    }
                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvIssuetoMDPOrCC.DataSource = dtCurrentTable;
                    gvIssuetoMDPOrCC.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["CurrentTable"] = null;
        }

        //Set Previous Data on Postbacks


        SetOutFlowPreviousData();
    }

    private void SetOutFlowPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlMDPCC = (DropDownList)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[1].FindControl("ddlMDPCC");
                        DropDownList ddlMDPCCTankerNo = (DropDownList)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[2].FindControl("ddlMDPCCTankerNo");
                        TextBox txtMDPCCChallanNo = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[3].FindControl("txtMDPCCChallanNo");
                        DropDownList ddlMDPCCMilkQuality = (DropDownList)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[4].FindControl("ddlMDPCCMilkQuality");
                        TextBox QtyInKg = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[5].FindControl("txtIssuetoMDPOrCCQuantityInKg");
                        TextBox QtyInLtr = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[6].FindControl("txtIssuetoMDPOrCCQuantityInLtr");
                        
                        TextBox FAT_Per = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[7].FindControl("txtIssuetoMDPOrCCFat_Per");
                        TextBox SNF_Per = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[8].FindControl("txtIssuetoMDPOrCCSnf_Per");
                        TextBox FatInKg = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[9].FindControl("txtIssuetoMDPOrCCFATInKg");
                        TextBox SNFInkg = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[10].FindControl("txtIssuetoMDPOrCCSnfInKg");


                        FillMDPORCC(ddlMDPCC);
                        FillTanker(ddlMDPCCTankerNo);
                        if (i < dt.Rows.Count - 1)
                        {
                            ddlMDPCC.ClearSelection();
                            ddlMDPCC.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;
                            ddlMDPCCTankerNo.ClearSelection();
                            ddlMDPCCTankerNo.Items.FindByValue(dt.Rows[i]["I_TankerID"].ToString()).Selected = true;



                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();
                        ddlMDPCCMilkQuality.SelectedValue = dt.Rows[i]["MilkType"].ToString();
                        txtMDPCCChallanNo.Text = dt.Rows[i]["ChallanNo"].ToString();
                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    private void SetOutFlowOnRemove()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlMDPCC = (DropDownList)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[1].FindControl("ddlMDPCC");
                        DropDownList ddlMDPCCTankerNo = (DropDownList)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[2].FindControl("ddlMDPCCTankerNo");
                        TextBox txtMDPCCChallanNo = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[3].FindControl("txtMDPCCChallanNo");
                        DropDownList ddlMDPCCMilkQuality = (DropDownList)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[4].FindControl("ddlMDPCCMilkQuality");
                        TextBox QtyInKg = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[5].FindControl("txtIssuetoMDPOrCCQuantityInKg");
                        TextBox QtyInLtr = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[6].FindControl("txtIssuetoMDPOrCCQuantityInLtr");
                        
                        TextBox FAT_Per = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[7].FindControl("txtIssuetoMDPOrCCFat_Per");
                        TextBox SNF_Per = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[8].FindControl("txtIssuetoMDPOrCCSnf_Per");
                        TextBox FatInKg = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[9].FindControl("txtIssuetoMDPOrCCFATInKg");
                        TextBox SNFInkg = (TextBox)gvIssuetoMDPOrCC.Rows[rowIndex].Cells[10].FindControl("txtIssuetoMDPOrCCSnfInKg");


                        FillMDPORCC(ddlMDPCC);
                        FillTanker(ddlMDPCCTankerNo);
                        if (i < dt.Rows.Count)
                        {
                            ddlMDPCC.ClearSelection();
                            ddlMDPCC.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;
                            ddlMDPCCTankerNo.ClearSelection();
                            ddlMDPCCTankerNo.Items.FindByValue(dt.Rows[i]["I_TankerID"].ToString()).Selected = true;



                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();
                        txtMDPCCChallanNo.Text = dt.Rows[i]["ChallanNo"].ToString();
                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void btnIssuetoMDPOrCCRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["OutFlowrowID"] = rowID.ToString();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["CurrentTable"] = dt;

            //Re bind the GridView for the updated data  
            gvIssuetoMDPOrCC.DataSource = dt;
            gvIssuetoMDPOrCC.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetOutFlowOnRemove();
    }
    protected void btnIssuetoMDPOrCCAdd_Click(object sender, EventArgs e)
    {
        ViewState["OutFlowrowID"] = "";
        //BindGrid();
        AddOutFlowNewRowToGrid();
    }
    private DataTable GetIssuetoMDPCCData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Office_Id", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        dt.Columns.Add("I_TankerID", typeof(int));
        dt.Columns.Add("MilkType", typeof(string));
        dt.Columns.Add("ChallanNo", typeof(string));
        foreach (GridViewRow row in gvIssuetoMDPOrCC.Rows)
        {
            DropDownList ddlMDPCC = (DropDownList)row.FindControl("ddlMDPCC");
            DropDownList ddlMDPCCTankerNo = (DropDownList)row.FindControl("ddlMDPCCTankerNo");
            DropDownList ddlMDPCCMilkQuality = (DropDownList)row.FindControl("ddlMDPCCMilkQuality");
            TextBox txtIssuetoMDPOrCCQuantityInLtr = (TextBox)row.FindControl("txtIssuetoMDPOrCCQuantityInLtr");
            TextBox txtMDPCCChallanNo = (TextBox)row.FindControl("txtMDPCCChallanNo");
            TextBox txtIssuetoMDPOrCCQuantityInKg = (TextBox)row.FindControl("txtIssuetoMDPOrCCQuantityInKg");
            TextBox txtIssuetoMDPOrCCFat_Per = (TextBox)row.FindControl("txtIssuetoMDPOrCCFat_Per");
            TextBox txtIssuetoMDPOrCCSnf_Per = (TextBox)row.FindControl("txtIssuetoMDPOrCCSnf_Per");
            TextBox txtIssuetoMDPOrCCFATInKg = (TextBox)row.FindControl("txtIssuetoMDPOrCCFATInKg");
            TextBox txtIssuetoMDPOrCCSNFInKg = (TextBox)row.FindControl("txtIssuetoMDPOrCCSNFInKg");
            if (txtIssuetoMDPOrCCQuantityInLtr.Text != "" && txtIssuetoMDPOrCCQuantityInKg.Text != "" && txtIssuetoMDPOrCCQuantityInLtr.Text != "0.00" && txtIssuetoMDPOrCCQuantityInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = ddlMDPCC.SelectedValue;
                dr[1] = txtIssuetoMDPOrCCQuantityInLtr.Text;
                dr[2] = txtIssuetoMDPOrCCQuantityInKg.Text;
                dr[3] = txtIssuetoMDPOrCCFat_Per.Text;
                dr[4] = txtIssuetoMDPOrCCSnf_Per.Text;
                dr[5] = txtIssuetoMDPOrCCFATInKg.Text;
                dr[6] = txtIssuetoMDPOrCCSNFInKg.Text;
                dr[7] = ddlMDPCCTankerNo.SelectedValue;
                dr[8] = ddlMDPCCMilkQuality.SelectedValue;
                dr[9] = txtMDPCCChallanNo.Text;
                dt.Rows.Add(dr);

            }
        }
        return dt;

    }
    protected void btnIssuetoMDPOrCC_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                DataTable dtIssued = new DataTable();
                dtIssued = GetIssuetoMDPCCData();


                if (dtIssued.Rows.Count > 0)
                {


                    if (btnIssuetoMDPOrCC.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_OutProcess_IssuetoMDP_CC"
                                                     },
                                            new DataTable[] { 
                                                          dtIssued
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnIssuetoMDPOrCC.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"14",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                         "type_Production_Milk_OutProcess_IssuetoMDP_CC"
                                                     },
                                            new DataTable[] { 
                                                            dtIssued
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnIssuetoMDPOrCCGetTotal_Click(object sender, EventArgs e)
    {
        btnIssuetoMDPOrCC.Enabled = false;
        decimal MDPTQtyLtr = 0;
        decimal MDPTQtyKg = 0;
        decimal MDPTFatKg = 0;
        decimal MDPTSnfKg = 0;
        foreach (GridViewRow rows in gvIssuetoMDPOrCC.Rows)
        {
            TextBox txtIssuetoMDPOrCCQuantityInLtr = (TextBox)rows.FindControl("txtIssuetoMDPOrCCQuantityInLtr");
            TextBox txtIssuetoMDPOrCCQuantityInKg = (TextBox)rows.FindControl("txtIssuetoMDPOrCCQuantityInKg");
            TextBox txtIssuetoMDPOrCCFATInKg = (TextBox)rows.FindControl("txtIssuetoMDPOrCCFATInKg");
            TextBox txtIssuetoMDPOrCCSnfInKg = (TextBox)rows.FindControl("txtIssuetoMDPOrCCSnfInKg");
            if (txtIssuetoMDPOrCCQuantityInLtr.Text != "")
            {
                MDPTQtyLtr += decimal.Parse(txtIssuetoMDPOrCCQuantityInLtr.Text);
            }
            if (txtIssuetoMDPOrCCQuantityInKg.Text != "")
            {
                MDPTQtyKg += decimal.Parse(txtIssuetoMDPOrCCQuantityInKg.Text);
            }
            if (txtIssuetoMDPOrCCFATInKg.Text != "")
            {
                MDPTFatKg += decimal.Parse(txtIssuetoMDPOrCCFATInKg.Text);
            }
            if (txtIssuetoMDPOrCCSnfInKg.Text != "")
            {
                MDPTSnfKg += decimal.Parse(txtIssuetoMDPOrCCSnfInKg.Text);
            }
        }

        gvIssuetoMDPOrCC.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvIssuetoMDPOrCC.FooterRow.Cells[5].Text = "<b>" + MDPTQtyLtr.ToString() + "</b>";
        gvIssuetoMDPOrCC.FooterRow.Cells[4].Text = "<b>" + MDPTQtyKg.ToString() + "</b>";
        gvIssuetoMDPOrCC.FooterRow.Cells[8].Text = "<b>" + MDPTFatKg.ToString() + "</b>";
        gvIssuetoMDPOrCC.FooterRow.Cells[9].Text = "<b>" + MDPTSnfKg.ToString() + "</b>";
        if (MDPTQtyLtr > 0)
        {
            btnIssuetoMDPOrCC.Enabled = true;
        }
    }
    #endregion

    #region ISSUE TO OTHER PARTY

    protected void gvIsuuetoOtherParty_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "ISSUE TO OTHER PARTY";
            HeaderCell.ColumnSpan = 11;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvIsuuetoOtherParty.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private void SetOutFlowIssuetoOtherInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("Office_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("FAT", typeof(string)));
        dt.Columns.Add(new DataColumn("SNF", typeof(string)));
        dt.Columns.Add(new DataColumn("FatInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("SnfInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("I_TankerID", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkType", typeof(string)));
        dt.Columns.Add(new DataColumn("ChallanNo", typeof(string)));
        dr = dt.NewRow();


        dr["Office_ID"] = string.Empty;
        dr["Office_Name"] = string.Empty;
        dr["QtyInLtr"] = string.Empty;
        dr["QtyInKg"] = string.Empty;
        dr["FAT"] = string.Empty;
        dr["SNF"] = string.Empty;
        dr["FatInKg"] = string.Empty;
        dr["SnfInKg"] = string.Empty;
        dr["I_TankerID"] = string.Empty;
        dr["MilkType"] = string.Empty;
        dr["ChallanNo"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["ITOCurrentTable"] = dt;

        gvIsuuetoOtherParty.DataSource = dt;
        gvIsuuetoOtherParty.DataBind();

        DropDownList ddl1 = (DropDownList)gvIsuuetoOtherParty.Rows[0].Cells[1].FindControl("ddlThirdUnion");
        DropDownList ddl2 = (DropDownList)gvIsuuetoOtherParty.Rows[0].Cells[1].FindControl("ddlThirdUnionTankerNo");
        FillIssuetoother(ddl1);
        FillTanker(ddl2);

    }

    private void AddIssuetootherNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["ITOCurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["ITOCurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlThirdUnion = (DropDownList)gvIsuuetoOtherParty.Rows[rowIndex].Cells[1].FindControl("ddlThirdUnion");
                    DropDownList ddlThirdUnionTankerNo = (DropDownList)gvIsuuetoOtherParty.Rows[rowIndex].Cells[2].FindControl("ddlThirdUnionTankerNo");
                    TextBox txtThirdUnionChallanNo = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[3].FindControl("txtThirdUnionChallanNo");
                    DropDownList ddlThirdUnionMilkQuality = (DropDownList)gvIsuuetoOtherParty.Rows[rowIndex].Cells[4].FindControl("ddlThirdUnionMilkQuality");
                   
                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");
                    if (ddlThirdUnion.SelectedIndex != 0)
                    {

                        TextBox QtyInLtr = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[5].FindControl("txtIsuuetoOtherPartyQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[6].FindControl("txtIsuuetoOtherPartyQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[7].FindControl("txtIsuuetoOtherPartyFat_Per");
                        TextBox SNF_Per = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[8].FindControl("txtIsuuetoOtherPartySnf_Per");
                        TextBox FatInKg = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[9].FindControl("txtIsuuetoOtherPartyFATInKg");
                        TextBox SNFInkg = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[10].FindControl("txtIsuuetoOtherPartySnfInKg");




                        drCurrentRow = dtCurrentTable.NewRow();


                        dtCurrentTable.Rows[i - 1]["Office_ID"] = ddlThirdUnion.SelectedValue;
                        //dtCurrentTable.Rows[i - 1]["Column2"] = ddlSt.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["Office_Name"] = ddlThirdUnion.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInLtr"] = QtyInLtr.Text;
                        dtCurrentTable.Rows[i - 1]["QtyInKg"] = QtyInKg.Text;
                        dtCurrentTable.Rows[i - 1]["FAT"] = FAT_Per.Text;
                        dtCurrentTable.Rows[i - 1]["SNF"] = SNF_Per.Text;
                        dtCurrentTable.Rows[i - 1]["FatInKg"] = FatInKg.Text;
                        dtCurrentTable.Rows[i - 1]["SnfInKg"] = SNFInkg.Text;
                        dtCurrentTable.Rows[i - 1]["I_TankerID"] = ddlThirdUnionTankerNo.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["MilkType"] = ddlThirdUnionMilkQuality.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ChallanNo"] = txtThirdUnionChallanNo.Text;
                        rowIndex++;

                    }
                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["ITOCurrentTable"] = dtCurrentTable;

                    gvIsuuetoOtherParty.DataSource = dtCurrentTable;
                    gvIsuuetoOtherParty.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["CurrentTable"] = null;
        }

        //Set Previous Data on Postbacks


        SetOutFlowIssuetootherPreviousData();
    }
    private void SetOutFlowIssuetootherPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["ITOCurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["ITOCurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlThirdUnion = (DropDownList)gvIsuuetoOtherParty.Rows[rowIndex].Cells[1].FindControl("ddlThirdUnion");
                        DropDownList ddlThirdUnionTankerNo = (DropDownList)gvIsuuetoOtherParty.Rows[rowIndex].Cells[2].FindControl("ddlThirdUnionTankerNo");
                        TextBox txtThirdUnionChallanNo = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[3].FindControl("txtThirdUnionChallanNo");
                        DropDownList ddlThirdUnionMilkQuality = (DropDownList)gvIsuuetoOtherParty.Rows[rowIndex].Cells[4].FindControl("ddlThirdUnionMilkQuality");
                        TextBox QtyInLtr = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[5].FindControl("txtIsuuetoOtherPartyQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[6].FindControl("txtIsuuetoOtherPartyQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[7].FindControl("txtIsuuetoOtherPartyFat_Per");
                        TextBox SNF_Per = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[8].FindControl("txtIsuuetoOtherPartySnf_Per");
                        TextBox FatInKg = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[9].FindControl("txtIsuuetoOtherPartyFATInKg");
                        TextBox SNFInkg = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[10].FindControl("txtIsuuetoOtherPartySnfInKg");


                        FillIssuetoother(ddlThirdUnion);
                        FillTanker(ddlThirdUnionTankerNo);
                        if (i < dt.Rows.Count - 1)
                        {
                            ddlThirdUnion.ClearSelection();
                            ddlThirdUnion.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;

                            ddlThirdUnionTankerNo.ClearSelection();
                            ddlThirdUnionTankerNo.Items.FindByValue(dt.Rows[i]["I_TankerID"].ToString()).Selected = true;


                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        ddlThirdUnionMilkQuality.SelectedValue = dt.Rows[i]["MilkType"].ToString();
                        txtThirdUnionChallanNo.Text = dt.Rows[i]["ChallanNo"].ToString();
                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    private void SetOutFlowIssuetootherOnRemove()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["ITOCurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["ITOCurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlThirdUnion = (DropDownList)gvIsuuetoOtherParty.Rows[rowIndex].Cells[1].FindControl("ddlThirdUnion");
                        DropDownList ddlThirdUnionTankerNo = (DropDownList)gvIsuuetoOtherParty.Rows[rowIndex].Cells[2].FindControl("ddlThirdUnionTankerNo");
                        TextBox txtThirdUnionChallanNo = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[3].FindControl("txtThirdUnionChallanNo");
                        DropDownList ddlThirdUnionMilkQuality = (DropDownList)gvIsuuetoOtherParty.Rows[rowIndex].Cells[4].FindControl("ddlThirdUnionMilkQuality");
                        TextBox QtyInLtr = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[5].FindControl("txtIsuuetoOtherPartyQuantityInLtr");
                        TextBox QtyInKg = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[6].FindControl("txtIsuuetoOtherPartyQuantityInKg");
                        TextBox FAT_Per = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[7].FindControl("txtIsuuetoOtherPartyFat_Per");
                        TextBox SNF_Per = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[8].FindControl("txtIsuuetoOtherPartySnf_Per");
                        TextBox FatInKg = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[9].FindControl("txtIsuuetoOtherPartyFATInKg");
                        TextBox SNFInkg = (TextBox)gvIsuuetoOtherParty.Rows[rowIndex].Cells[10].FindControl("txtIsuuetoOtherPartySnfInKg");


                        FillIssuetoother(ddlThirdUnion);
                        FillTanker(ddlThirdUnionTankerNo);
                        if (i < dt.Rows.Count)
                        {
                            ddlThirdUnion.ClearSelection();
                            ddlThirdUnion.Items.FindByValue(dt.Rows[i]["Office_ID"].ToString()).Selected = true;

                            ddlThirdUnionTankerNo.ClearSelection();
                            ddlThirdUnionTankerNo.Items.FindByValue(dt.Rows[i]["I_TankerID"].ToString()).Selected = true;


                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();
                        ddlThirdUnionMilkQuality.SelectedValue = dt.Rows[i]["MilkType"].ToString();
                        txtThirdUnionChallanNo.Text = dt.Rows[i]["ChallanNo"].ToString();
                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void btnIsuuetoOtherPartyRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["OutFlowIssuetootherrowID"] = rowID.ToString();
        if (ViewState["ITOCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ITOCurrentTable"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["ITOCurrentTable"] = dt;

            //Re bind the GridView for the updated data  
            gvIsuuetoOtherParty.DataSource = dt;
            gvIsuuetoOtherParty.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetOutFlowIssuetootherOnRemove();
    }
    protected void btnIsuuetoOtherPartyAdd_Click(object sender, EventArgs e)
    {
        ViewState["OutFlowIssuetootherrowID"] = "";
        //BindGrid();
        AddIssuetootherNewRowToGrid();
    }
    private DataTable GetIssuetoOtherParty()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Office_Id", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        dt.Columns.Add("I_TankerID", typeof(int));
        dt.Columns.Add("MilkType", typeof(string));
        dt.Columns.Add("ChallanNo", typeof(string));
        foreach (GridViewRow row in gvIsuuetoOtherParty.Rows)
        {
            DropDownList ddlThirdUnion = (DropDownList)row.FindControl("ddlThirdUnion");
            DropDownList ddlThirdUnionTankerNo = (DropDownList)row.FindControl("ddlThirdUnionTankerNo");
            DropDownList ddlThirdUnionMilkQuality = (DropDownList)row.FindControl("ddlThirdUnionMilkQuality");
            TextBox txtIsuuetoOtherPartyQuantityInLtr = (TextBox)row.FindControl("txtIsuuetoOtherPartyQuantityInLtr");
            TextBox txtIsuuetoOtherPartyQuantityInKg = (TextBox)row.FindControl("txtIsuuetoOtherPartyQuantityInKg");
            TextBox txtIsuuetoOtherPartyFat_Per = (TextBox)row.FindControl("txtIsuuetoOtherPartyFat_Per");
            TextBox txtIsuuetoOtherPartySnf_Per = (TextBox)row.FindControl("txtIsuuetoOtherPartySnf_Per");
            TextBox txtIsuuetoOtherPartyFATInKg = (TextBox)row.FindControl("txtIsuuetoOtherPartyFATInKg");
            TextBox txtIsuuetoOtherPartySNFInKg = (TextBox)row.FindControl("txtIsuuetoOtherPartySNFInKg");
            TextBox txtThirdUnionChallanNo = (TextBox)row.FindControl("txtThirdUnionChallanNo");
            if (txtIsuuetoOtherPartyQuantityInLtr.Text != "" && txtIsuuetoOtherPartyQuantityInKg.Text != "" && txtIsuuetoOtherPartyQuantityInLtr.Text != "0.00" && txtIsuuetoOtherPartyQuantityInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = ddlThirdUnion.SelectedValue;
                dr[1] = txtIsuuetoOtherPartyQuantityInLtr.Text;
                dr[2] = txtIsuuetoOtherPartyQuantityInKg.Text;
                dr[3] = txtIsuuetoOtherPartyFat_Per.Text;
                dr[4] = txtIsuuetoOtherPartySnf_Per.Text;
                dr[5] = txtIsuuetoOtherPartyFATInKg.Text;
                dr[6] = txtIsuuetoOtherPartySNFInKg.Text;
                dr[7] = ddlThirdUnionTankerNo.SelectedValue;
                dr[8] = ddlThirdUnionMilkQuality.SelectedValue;
                dr[9] = txtThirdUnionChallanNo.Text;
                dt.Rows.Add(dr);

            }
        }
        return dt;

    }
    protected void btnIsuuetoOtherParty_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtIssuedOtherParty = new DataTable();
                dtIssuedOtherParty = GetIssuetoOtherParty();


                if (dtIssuedOtherParty.Rows.Count > 0)
                {


                    if (btnIsuuetoOtherParty.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_OutProcess_IssuetoOther"
                                                       
                                                     },
                                            new DataTable[] {
                                                          dtIssuedOtherParty
                                                          
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnIsuuetoOtherParty.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"15",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                         "type_Production_Milk_OutProcess_IssuetoOther"
                                                     },
                                            new DataTable[] { 
                                                            dtIssuedOtherParty
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnIsuuetoOtherPartyGetTotal_Click(object sender, EventArgs e)
    {
        btnIsuuetoOtherParty.Enabled = false;
        decimal IsuuetoOtherPartyQtyLtr = 0;
        decimal IsuuetoOtherPartyQtyKg = 0;
        decimal IsuuetoOtherPartyFatKg = 0;
        decimal IsuuetoOtherPartySnfKg = 0;
        foreach (GridViewRow rows in gvIsuuetoOtherParty.Rows)
        {
            TextBox txtIsuuetoOtherPartyQuantityInLtr = (TextBox)rows.FindControl("txtIsuuetoOtherPartyQuantityInLtr");
            TextBox txtIsuuetoOtherPartyQuantityInKg = (TextBox)rows.FindControl("txtIsuuetoOtherPartyQuantityInKg");
            TextBox txtIsuuetoOtherPartyFATInKg = (TextBox)rows.FindControl("txtIsuuetoOtherPartyFATInKg");
            TextBox txtIsuuetoOtherPartySnfInKg = (TextBox)rows.FindControl("txtIsuuetoOtherPartySnfInKg");
            if (txtIsuuetoOtherPartyQuantityInLtr.Text != "")
            {
                IsuuetoOtherPartyQtyLtr += decimal.Parse(txtIsuuetoOtherPartyQuantityInLtr.Text);
            }
            if (txtIsuuetoOtherPartyQuantityInKg.Text != "")
            {
                IsuuetoOtherPartyQtyKg += decimal.Parse(txtIsuuetoOtherPartyQuantityInKg.Text);
            }
            if (txtIsuuetoOtherPartyFATInKg.Text != "")
            {
                IsuuetoOtherPartyFatKg += decimal.Parse(txtIsuuetoOtherPartyFATInKg.Text);
            }
            if (txtIsuuetoOtherPartySnfInKg.Text != "")
            {
                IsuuetoOtherPartySnfKg += decimal.Parse(txtIsuuetoOtherPartySnfInKg.Text);
            }
        }

        gvIsuuetoOtherParty.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvIsuuetoOtherParty.FooterRow.Cells[5].Text = "<b>" + IsuuetoOtherPartyQtyLtr.ToString() + "</b>";
        gvIsuuetoOtherParty.FooterRow.Cells[4].Text = "<b>" + IsuuetoOtherPartyQtyKg.ToString() + "</b>";
        gvIsuuetoOtherParty.FooterRow.Cells[8].Text = "<b>" + IsuuetoOtherPartyFatKg.ToString() + "</b>";
        gvIsuuetoOtherParty.FooterRow.Cells[9].Text = "<b>" + IsuuetoOtherPartySnfKg.ToString() + "</b>";
        if (IsuuetoOtherPartyQtyKg > 0)
        {
            btnIsuuetoOtherParty.Enabled = true;
        }
    }
    #endregion

    #region ISSUE TO POWDER PLANT
    protected void GVIssuetoPowderPlant_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "ISSUE TO POWDER PLANT";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            GVIssuetoPowderPlant.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private void SetIssuetoPowderPlantInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("Variant", typeof(string)));
        dt.Columns.Add(new DataColumn("I_MCID", typeof(string)));
        dt.Columns.Add(new DataColumn("V_MCName", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));
        dt.Columns.Add(new DataColumn("QtyInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("FAT", typeof(string)));
        dt.Columns.Add(new DataColumn("SNF", typeof(string)));
        dt.Columns.Add(new DataColumn("FatInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("SnfInKg", typeof(string)));


        dr = dt.NewRow();


        dr["Variant"] = string.Empty;
        dr["I_MCID"] = string.Empty;
        dr["V_MCName"] = string.Empty;
        dr["QtyInLtr"] = string.Empty;
        dr["QtyInKg"] = string.Empty;
        dr["FAT"] = string.Empty;
        dr["SNF"] = string.Empty;
        dr["FatInKg"] = string.Empty;
        dr["SnfInKg"] = string.Empty;

        dt.Rows.Add(dr);


        ViewState["OutFlowIssuetoPowderPlant"] = dt;


        GVIssuetoPowderPlant.DataSource = dt;
        GVIssuetoPowderPlant.DataBind();



        DropDownList ddl1 = (DropDownList)GVIssuetoPowderPlant.Rows[0].Cells[2].FindControl("ddlContainer");


        FillContainer(ddl1);


    }
    private void AddIssuetoPowderPlantNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["OutFlowIssuetoPowderPlant"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["OutFlowIssuetoPowderPlant"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlContainer = (DropDownList)GVIssuetoPowderPlant.Rows[rowIndex].Cells[2].FindControl("ddlContainer");
                    DropDownList ddlVariant = (DropDownList)GVIssuetoPowderPlant.Rows[rowIndex].Cells[1].FindControl("ddlVariant");
                    TextBox QtyInLtr = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[4].FindControl("txtIssuetoPowderPlantQtyInLtr");
                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");
                    //if (ddlVariant.SelectedIndex != 0)
                    //{


                    TextBox QtyInKg = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[3].FindControl("txtIssuetoPowderPlantQtyInKg");
                    TextBox FAT_Per = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[5].FindControl("txtIssuetoPowderPlantFat_per");
                    TextBox SNF_Per = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[6].FindControl("txtIssuetoPowderPlantSnf_Per");
                    TextBox FatInKg = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[7].FindControl("txtIssuetoPowderPlantFatInKg");
                    TextBox SNFInkg = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[8].FindControl("txtIssuetoPowderPlantSnfInKg");



                    drCurrentRow = dtCurrentTable.NewRow();


                    dtCurrentTable.Rows[i - 1]["Variant"] = ddlVariant.SelectedItem.Text;
                    //dtCurrentTable.Rows[i - 1]["Column2"] = ddlSt.SelectedItem.Text;

                    dtCurrentTable.Rows[i - 1]["I_MCID"] = ddlContainer.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["V_MCName"] = ddlContainer.SelectedItem.Text;
                    dtCurrentTable.Rows[i - 1]["QtyInLtr"] = QtyInLtr.Text;
                    dtCurrentTable.Rows[i - 1]["QtyInKg"] = QtyInKg.Text;
                    dtCurrentTable.Rows[i - 1]["FAT"] = FAT_Per.Text;
                    dtCurrentTable.Rows[i - 1]["SNF"] = SNF_Per.Text;
                    dtCurrentTable.Rows[i - 1]["FatInKg"] = FatInKg.Text;
                    dtCurrentTable.Rows[i - 1]["SnfInKg"] = SNFInkg.Text;


                    rowIndex++;

                    // }
                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["OutFlowIssuetoPowderPlant"] = dtCurrentTable;

                    GVIssuetoPowderPlant.DataSource = dtCurrentTable;
                    GVIssuetoPowderPlant.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["OutFlowIssuetoPowderPlant"] = null;
        }

        //Set Previous Data on Postbacks


        SetIssuetoPowderPlantPreviousData();
    }

    private void SetIssuetoPowderPlantPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["OutFlowIssuetoPowderPlant"] != null)
            {

                DataTable dt = (DataTable)ViewState["OutFlowIssuetoPowderPlant"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlContainer = (DropDownList)GVIssuetoPowderPlant.Rows[rowIndex].Cells[2].FindControl("ddlContainer");
                        DropDownList ddlVariant = (DropDownList)GVIssuetoPowderPlant.Rows[rowIndex].Cells[1].FindControl("ddlVariant");
                        TextBox QtyInLtr = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[4].FindControl("txtIssuetoPowderPlantQtyInLtr");
                        TextBox QtyInKg = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[3].FindControl("txtIssuetoPowderPlantQtyInKg");
                        TextBox FAT_Per = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[5].FindControl("txtIssuetoPowderPlantFat_per");
                        TextBox SNF_Per = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[6].FindControl("txtIssuetoPowderPlantSnf_Per");
                        TextBox FatInKg = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[7].FindControl("txtIssuetoPowderPlantFatInKg");
                        TextBox SNFInkg = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[8].FindControl("txtIssuetoPowderPlantSnfInKg");


                        FillContainer(ddlContainer);

                        if (i < dt.Rows.Count - 1)
                        {
                            ddlContainer.ClearSelection();
                            ddlContainer.Items.FindByValue(dt.Rows[i]["I_MCID"].ToString()).Selected = true;




                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        ddlVariant.Text = dt.Rows[i]["Variant"].ToString();
                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();

                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    private void SetIssuetoPowderPlantOnRemove()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["OutFlowIssuetoPowderPlant"] != null)
            {

                DataTable dt = (DataTable)ViewState["OutFlowIssuetoPowderPlant"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlContainer = (DropDownList)GVIssuetoPowderPlant.Rows[rowIndex].Cells[2].FindControl("ddlContainer");
                        DropDownList ddlVariant = (DropDownList)GVIssuetoPowderPlant.Rows[rowIndex].Cells[1].FindControl("ddlVariant");
                        TextBox QtyInLtr = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[4].FindControl("txtIssuetoPowderPlantQtyInLtr");
                        TextBox QtyInKg = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[3].FindControl("txtIssuetoPowderPlantQtyInKg");
                        TextBox FAT_Per = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[5].FindControl("txtIssuetoPowderPlantFat_per");
                        TextBox SNF_Per = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[6].FindControl("txtIssuetoPowderPlantSnf_Per");
                        TextBox FatInKg = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[7].FindControl("txtIssuetoPowderPlantFatInKg");
                        TextBox SNFInkg = (TextBox)GVIssuetoPowderPlant.Rows[rowIndex].Cells[8].FindControl("txtIssuetoPowderPlantSnfInKg");


                        FillContainer(ddlContainer);

                        if (i < dt.Rows.Count)
                        {
                            ddlContainer.ClearSelection();
                            ddlContainer.Items.FindByValue(dt.Rows[i]["I_MCID"].ToString()).Selected = true;




                        }
                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        ddlVariant.Text = dt.Rows[i]["Variant"].ToString();
                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        FAT_Per.Text = dt.Rows[i]["FAT"].ToString();
                        SNF_Per.Text = dt.Rows[i]["SNF"].ToString();
                        FatInKg.Text = dt.Rows[i]["FatInKg"].ToString();
                        SNFInkg.Text = dt.Rows[i]["SnfInkg"].ToString();

                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void btnIssuetoPowderPlantRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["IssuetoPowderPlantrowID"] = rowID.ToString();
        if (ViewState["OutFlowIssuetoPowderPlant"] != null)
        {
            DataTable dt = (DataTable)ViewState["OutFlowIssuetoPowderPlant"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["OutFlowIssuetoPowderPlant"] = dt;

            //Re bind the GridView for the updated data  
            GVIssuetoPowderPlant.DataSource = dt;
            GVIssuetoPowderPlant.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetIssuetoPowderPlantOnRemove();
    }
    protected void btnIssuetoPowderPlantAdd_Click(object sender, EventArgs e)
    {
        ViewState["IssuetoPowderPlantrowID"] = "";
        //BindGrid();
        AddIssuetoPowderPlantNewRowToGrid();
    }
    private DataTable GetIssuetoPowderPlant()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Variant", typeof(string));
        dt.Columns.Add("I_MCID", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in GVIssuetoPowderPlant.Rows)
        {
            Label lblVariant = (Label)row.FindControl("lblVariant");
            DropDownList ddlVariant = (DropDownList)row.FindControl("ddlVariant");
            DropDownList ddlContainer = (DropDownList)row.FindControl("ddlContainer");
            TextBox txtIssuetoPowderPlantQtyInLtr = (TextBox)row.FindControl("txtIssuetoPowderPlantQtyInLtr");
            TextBox txtIssuetoPowderPlantQtyInKg = (TextBox)row.FindControl("txtIssuetoPowderPlantQtyInKg");
            TextBox txtIssuetoPowderPlantFat_per = (TextBox)row.FindControl("txtIssuetoPowderPlantFat_per");
            TextBox txtIssuetoPowderPlantSnf_Per = (TextBox)row.FindControl("txtIssuetoPowderPlantSnf_Per");
            TextBox txtIssuetoPowderPlantFatInKg = (TextBox)row.FindControl("txtIssuetoPowderPlantFatInKg");
            TextBox txtIssuetoPowderPlantSnfInKg = (TextBox)row.FindControl("txtIssuetoPowderPlantSnfInKg");

            if (txtIssuetoPowderPlantQtyInLtr.Text != "" && txtIssuetoPowderPlantQtyInKg.Text != "" && txtIssuetoPowderPlantQtyInLtr.Text != "0.00" && txtIssuetoPowderPlantQtyInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                //dr[0] = lblVariant.Text;
                dr[0] = ddlVariant.SelectedValue;
                dr[1] = ddlContainer.SelectedValue;
                dr[2] = txtIssuetoPowderPlantQtyInLtr.Text;
                dr[3] = txtIssuetoPowderPlantQtyInKg.Text;
                dr[4] = txtIssuetoPowderPlantFat_per.Text;
                dr[5] = txtIssuetoPowderPlantSnf_Per.Text;
                dr[6] = txtIssuetoPowderPlantFatInKg.Text;
                dr[7] = txtIssuetoPowderPlantSnfInKg.Text;
                dt.Rows.Add(dr);

            }

        }
        return dt;
    }
    protected void btnIssuetoPowderPlant_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtIssuetoPP = new DataTable();
                dtIssuetoPP = GetIssuetoPowderPlant();


                if (dtIssuetoPP.Rows.Count > 0)
                {


                    if (btnIssuetoPowderPlant.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_InProcess_IssueforPowderPlant"
                                                     },
                                            new DataTable[] { 
                                                          dtIssuetoPP
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnIssuetoPowderPlant.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"16",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] {
                                                          "type_Production_Milk_InProcess_IssueforPowderPlant"
                                                     },
                                            new DataTable[] { 
                                                            dtIssuetoPP
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnIssuetoPowderPlantGetTotal_Click(object sender, EventArgs e)
    {
        btnIssuetoPowderPlant.Enabled = false;
        decimal IsuueforPPQtyLtr = 0;
        decimal IsuueforPPQtyKg = 0;
        decimal IsuueforPPFatKg = 0;
        decimal IsuueforPPSnfKg = 0;
        foreach (GridViewRow rows in GVIssuetoPowderPlant.Rows)
        {
            TextBox txtIssuetoPowderPlantQuantityInLtr = (TextBox)rows.FindControl("txtIssuetoPowderPlantQtyInLtr");
            TextBox txtIssuetoPowderPlantQuantityInKg = (TextBox)rows.FindControl("txtIssuetoPowderPlantQtyInKg");
            TextBox txtIssuetoPowderPlantFATInKg = (TextBox)rows.FindControl("txtIssuetoPowderPlantFATInKg");
            TextBox txtIssuetoPowderPlantSnfInKg = (TextBox)rows.FindControl("txtIssuetoPowderPlantSnfInKg");
            if (txtIssuetoPowderPlantQuantityInLtr.Text != "")
            {
                IsuueforPPQtyLtr += decimal.Parse(txtIssuetoPowderPlantQuantityInLtr.Text);
            }
            if (txtIssuetoPowderPlantQuantityInKg.Text != "")
            {
                IsuueforPPQtyKg += decimal.Parse(txtIssuetoPowderPlantQuantityInKg.Text);
            }
            if (txtIssuetoPowderPlantFATInKg.Text != "")
            {
                IsuueforPPFatKg += decimal.Parse(txtIssuetoPowderPlantFATInKg.Text);
            }
            if (txtIssuetoPowderPlantSnfInKg.Text != "")
            {
                IsuueforPPSnfKg += decimal.Parse(txtIssuetoPowderPlantSnfInKg.Text);
            }
        }

        GVIssuetoPowderPlant.FooterRow.Cells[1].Text = "<b>Total : </b>";
        GVIssuetoPowderPlant.FooterRow.Cells[3].Text = "<b>" + IsuueforPPQtyLtr.ToString() + "</b>";
        GVIssuetoPowderPlant.FooterRow.Cells[2].Text = "<b>" + IsuueforPPQtyKg.ToString() + "</b>";
        GVIssuetoPowderPlant.FooterRow.Cells[6].Text = "<b>" + IsuueforPPFatKg.ToString() + "</b>";
        GVIssuetoPowderPlant.FooterRow.Cells[7].Text = "<b>" + IsuueforPPSnfKg.ToString() + "</b>";
        if (IsuueforPPQtyKg > 0)
        {
            btnIssuetoPowderPlant.Enabled = true;
        }
    }
    #endregion

    #region ISSUE TO CREAM
    protected void gvIssuetoCream_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "ISSUE TO CREAM";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvIssuetoCream.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnIssuetoCream_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtIssuetoCream = new DataTable();
                dtIssuetoCream = GetIssuetoCreamData();


                if (dtIssuetoCream.Rows.Count > 0)
                {


                    if (btnIssuetoCream.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_InProcess_IssueforCream"
                                                     },
                                            new DataTable[] { 
                                                          dtIssuetoCream
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnIssuetoCream.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"17",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                          "type_Production_Milk_InProcess_IssueforCream"
                                                     },
                                            new DataTable[] { 
                                                            dtIssuetoCream
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private DataTable GetIssuetoCreamData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Variant", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvIssuetoCream.Rows)
        {
            Label lblVariant = (Label)row.FindControl("lblVariant");
            TextBox txtIssuetoCreamQtyInLtr = (TextBox)row.FindControl("txtIssuetoCreamQtyInLtr");
            TextBox txtIssuetoCreamQtyInKg = (TextBox)row.FindControl("txtIssuetoCreamQtyInKg");
            TextBox txtIssuetoCreamFat_per = (TextBox)row.FindControl("txtIssuetoCreamFat_per");
            TextBox txtIssuetoCreamSnf_Per = (TextBox)row.FindControl("txtIssuetoCreamSnf_Per");
            TextBox txtIssuetoCreamFatInKg = (TextBox)row.FindControl("txtIssuetoCreamFatInKg");
            TextBox txtIssuetoCreamSnfInKg = (TextBox)row.FindControl("txtIssuetoCreamSnfInKg");

            if (txtIssuetoCreamQtyInLtr.Text != "" && txtIssuetoCreamQtyInKg.Text != "" && txtIssuetoCreamQtyInLtr.Text != "0.00" && txtIssuetoCreamQtyInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblVariant.Text;
                dr[1] = txtIssuetoCreamQtyInLtr.Text;
                dr[2] = txtIssuetoCreamQtyInKg.Text;
                dr[3] = txtIssuetoCreamFat_per.Text;
                dr[4] = txtIssuetoCreamSnf_Per.Text;
                dr[5] = txtIssuetoCreamFatInKg.Text;
                dr[6] = txtIssuetoCreamSnfInKg.Text;
                dt.Rows.Add(dr);

            }

        }
        return dt;
    }
    protected void btnIssuetoCreamGetTotal_Click(object sender, EventArgs e)
    {
        btnIssuetoCream.Enabled = false;
        decimal IsuuetoCreamQtyLtr = 0;
        decimal IsuuetoCreamQtyKg = 0;
        decimal IsuuetoCreamFatKg = 0;
        decimal IsuuetoCreamSnfKg = 0;
        foreach (GridViewRow rows in gvIssuetoCream.Rows)
        {
            TextBox txtIssuetoCreamQtyInLtr = (TextBox)rows.FindControl("txtIssuetoCreamQtyInLtr");
            TextBox txtIssuetoCreamQtyInKg = (TextBox)rows.FindControl("txtIssuetoCreamQtyInKg");
            TextBox txtIssuetoCreamFatInKg = (TextBox)rows.FindControl("txtIssuetoCreamFatInKg");
            TextBox txtIssuetoCreamSnfInKg = (TextBox)rows.FindControl("txtIssuetoCreamSnfInKg");
            if (txtIssuetoCreamQtyInLtr.Text != "")
            {
                IsuuetoCreamQtyLtr += decimal.Parse(txtIssuetoCreamQtyInLtr.Text);
            }
            if (txtIssuetoCreamQtyInKg.Text != "")
            {
                IsuuetoCreamQtyKg += decimal.Parse(txtIssuetoCreamQtyInKg.Text);
            }
            if (txtIssuetoCreamFatInKg.Text != "")
            {
                IsuuetoCreamFatKg += decimal.Parse(txtIssuetoCreamFatInKg.Text);
            }
            if (txtIssuetoCreamSnfInKg.Text != "")
            {
                IsuuetoCreamSnfKg += decimal.Parse(txtIssuetoCreamSnfInKg.Text);
            }
        }

        gvIssuetoCream.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvIssuetoCream.FooterRow.Cells[2].Text = "<b>" + IsuuetoCreamQtyLtr.ToString() + "</b>";
        gvIssuetoCream.FooterRow.Cells[1].Text = "<b>" + IsuuetoCreamQtyKg.ToString() + "</b>";
        gvIssuetoCream.FooterRow.Cells[5].Text = "<b>" + IsuuetoCreamFatKg.ToString() + "</b>";
        gvIssuetoCream.FooterRow.Cells[6].Text = "<b>" + IsuuetoCreamSnfKg.ToString() + "</b>";
        if (IsuuetoCreamQtyKg > 0)
        {
            btnIssuetoCream.Enabled = true;
        }
    }
    #endregion

    #region Issue To IceCream

    protected void gvIssuetoIceCream_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Issue To IceCream";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvIssuetoIceCream.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    private void SetOutFlowIcecreamInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add("Variant", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        dt.Columns.Add("Type", typeof(string));


        dr = dt.NewRow();


        dr["Variant"] = string.Empty;
        dr["QtyInLtr"] = string.Empty;
        dr["QtyInKg"] = string.Empty;
        dr["Fat_Per"] = string.Empty;
        dr["Snf_Per"] = string.Empty;
        dr["KgFat"] = string.Empty;
        dr["KgSnf"] = string.Empty;
        dr["Type"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["OutFlowIcecreamCurrentTable"] = dt;


        gvIssuetoIceCream.DataSource = dt;
        gvIssuetoIceCream.DataBind();

    }
    private void AddOutFlowIcecreamNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["OutFlowIcecreamCurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["OutFlowIcecreamCurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    Label lblVariant = (Label)gvIssuetoIceCream.Rows[rowIndex].Cells[1].FindControl("lblVariant");
                    DropDownList ddlType = (DropDownList)gvIssuetoIceCream.Rows[rowIndex].Cells[2].FindControl("ddlType");

                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");

                    TextBox QtyInKg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[3].FindControl("txtIssuetoIceCreamQtyInKg");
                    TextBox QtyInLtr = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[4].FindControl("txtIssuetoIceCreamQtyInLtr");
                    TextBox FAT_Per = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[5].FindControl("txtIssuetoIceCreamFat_per");
                    TextBox SNF_Per = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[6].FindControl("txtIssuetoIceCreamSnf_Per");
                    TextBox FatInKg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[7].FindControl("txtIssuetoIceCreamFatInKg");
                    TextBox SNFInkg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[8].FindControl("txtIssuetoIceCreamSnfInKg");


                    drCurrentRow = dtCurrentTable.NewRow();



                    dtCurrentTable.Rows[i - 1]["Variant"] = lblVariant.Text;
                    dtCurrentTable.Rows[i - 1]["QtyInLtr"] = FAT_Per.Text;
                    dtCurrentTable.Rows[i - 1]["QtyInKg"] = QtyInKg.Text;
                    dtCurrentTable.Rows[i - 1]["Fat_Per"] = FAT_Per.Text;
                    dtCurrentTable.Rows[i - 1]["Snf_Per"] = SNF_Per.Text;
                    dtCurrentTable.Rows[i - 1]["KgFat"] = FatInKg.Text;
                    dtCurrentTable.Rows[i - 1]["KgSnf"] = SNFInkg.Text;
                    dtCurrentTable.Rows[i - 1]["Type"] = ddlType.SelectedItem.Text;

                    rowIndex++;


                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["OutFlowIcecreamCurrentTable"] = dtCurrentTable;

                    gvIssuetoIceCream.DataSource = dtCurrentTable;
                    gvIssuetoIceCream.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["OutFlowIcecreamCurrentTable"] = null;
        }

        //Set Previous Data on Postbacks


        SetOutFlowIcecreamPreviousData();
    }
    private void SetOutFlowIcecreamPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["OutFlowIcecreamCurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["OutFlowIcecreamCurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Label lblVariant = (Label)gvIssuetoIceCream.Rows[rowIndex].Cells[1].FindControl("lblVariant");
                        DropDownList ddlType = (DropDownList)gvIssuetoIceCream.Rows[rowIndex].Cells[2].FindControl("ddlType");

                        //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");

                        TextBox QtyInKg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[3].FindControl("txtIssuetoIceCreamQtyInKg");
                        TextBox QtyInLtr = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[4].FindControl("txtIssuetoIceCreamQtyInLtr");
                        TextBox FAT_Per = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[5].FindControl("txtIssuetoIceCreamFat_per");
                        TextBox SNF_Per = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[6].FindControl("txtIssuetoIceCreamSnf_Per");
                        TextBox FatInKg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[7].FindControl("txtIssuetoIceCreamFatInKg");
                        TextBox SNFInkg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[8].FindControl("txtIssuetoIceCreamSnfInKg");
                        if (i < dt.Rows.Count - 1)
                        {
                            ddlType.ClearSelection();
                            ddlType.Items.FindByValue(dt.Rows[i]["Type"].ToString()).Selected = true;
                        }

                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        lblVariant.Text = "IceCream";
                        QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                        QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                        FAT_Per.Text = dt.Rows[i]["Fat_Per"].ToString();
                        SNF_Per.Text = dt.Rows[i]["Snf_Per"].ToString();
                        FatInKg.Text = dt.Rows[i]["KgFat"].ToString();
                        SNFInkg.Text = dt.Rows[i]["KgSnf"].ToString();

                        rowIndex++;

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void SetOutFlowIcecreamOnRemove()
    {
        int rowIndex = 0;
        double Total = 0;
        if (ViewState["OutFlowIcecreamCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["OutFlowIcecreamCurrentTable"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label lblVariant = (Label)gvIssuetoIceCream.Rows[rowIndex].Cells[1].FindControl("lblVariant");
                    DropDownList ddlType = (DropDownList)gvIssuetoIceCream.Rows[rowIndex].Cells[2].FindControl("ddlType");

                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");

                    TextBox QtyInKg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[3].FindControl("txtIssuetoIceCreamQtyInKg");
                    TextBox QtyInLtr = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[4].FindControl("txtIssuetoIceCreamQtyInLtr");
                    TextBox FAT_Per = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[5].FindControl("txtIssuetoIceCreamFat_per");
                    TextBox SNF_Per = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[6].FindControl("txtIssuetoIceCreamSnf_Per");
                    TextBox FatInKg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[7].FindControl("txtIssuetoIceCreamFatInKg");
                    TextBox SNFInkg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[8].FindControl("txtIssuetoIceCreamSnfInKg");
                    if (i < dt.Rows.Count)
                    {
                        ddlType.ClearSelection();
                        ddlType.Items.FindByValue(dt.Rows[i]["Type"].ToString()).Selected = true;
                    }

                    lblVariant.Text = dt.Rows[i]["Variant"].ToString();
                    QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                    QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                    FAT_Per.Text = dt.Rows[i]["Fat_Per"].ToString();
                    SNF_Per.Text = dt.Rows[i]["Snf_Per"].ToString();
                    FatInKg.Text = dt.Rows[i]["KgFat"].ToString();
                    SNFInkg.Text = dt.Rows[i]["KgSnf"].ToString();

                    rowIndex++;


                }

            }

        }
    }
    protected void SetOutFlowIcecreamPreviousOnRemove()
    {
        int rowIndex = 0;
        double Total = 0;
        if (ViewState["OutFlowIcecreamCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["OutFlowIcecreamCurrentTable"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label lblVariant = (Label)gvCanesCollection.Rows[rowIndex].Cells[1].FindControl("lblVariant");
                    DropDownList ddlType = (DropDownList)gvCanesCollection.Rows[rowIndex].Cells[2].FindControl("ddlType");

                    //  DropDownList ddlSt = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("ddlStyle");

                    TextBox QtyInKg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[3].FindControl("txtIssuetoIceCreamQtyInKg");
                    TextBox QtyInLtr = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[4].FindControl("txtIssuetoIceCreamQtyInLtr");
                    TextBox FAT_Per = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[5].FindControl("txtIssuetoIceCreamFat_per");
                    TextBox SNF_Per = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[6].FindControl("txtIssuetoIceCreamSnf_Per");
                    TextBox FatInKg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[7].FindControl("txtIssuetoIceCreamFatInKg");
                    TextBox SNFInkg = (TextBox)gvIssuetoIceCream.Rows[rowIndex].Cells[8].FindControl("txtIssuetoIceCreamSnfInKg");




                    ddlType.ClearSelection();
                    ddlType.Items.FindByValue(dt.Rows[i]["Type"].ToString()).Selected = true;
                    QtyInLtr.Text = dt.Rows[i]["QtyInLtr"].ToString();
                    QtyInKg.Text = dt.Rows[i]["QtyInKg"].ToString();
                    FAT_Per.Text = dt.Rows[i]["Fat_Per"].ToString();
                    SNF_Per.Text = dt.Rows[i]["Snf_Per"].ToString();
                    FatInKg.Text = dt.Rows[i]["KgFat"].ToString();
                    SNFInkg.Text = dt.Rows[i]["KgSnf"].ToString();
                    lblVariant.Text = dt.Rows[i]["Variant"].ToString();
                    rowIndex++;


                }

            }

        }
    }
    protected void btnIssuetoIceCreamRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["OutFlowIcecreamrowID"] = rowID.ToString();
        if (ViewState["OutFlowIcecreamCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["OutFlowIcecreamCurrentTable"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["OutFlowIcecreamCurrentTable"] = dt;

            //Re bind the GridView for the updated data  
            gvIssuetoIceCream.DataSource = dt;
            gvIssuetoIceCream.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetOutFlowIcecreamOnRemove();

    }
    protected void btnIssuetoIceCreamAdd_Click(object sender, EventArgs e)
    {
        ViewState["OutFlowIcecreamrowID"] = "";
        //BindGrid();
        AddOutFlowIcecreamNewRowToGrid();
    }
    protected void btnIssuetoIceCream_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtIssuetoIceCream = new DataTable();
                dtIssuetoIceCream = GetIssuetoIceCreamData();
                if (dtIssuetoIceCream.Rows.Count > 0)
                {


                    if (btnIssuetoIceCream.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_InProcess_IssueforIceCream"
                                                     },
                                            new DataTable[] { 
                                                          dtIssuetoIceCream
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnIssuetoIceCream.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"18",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                          "type_Production_Milk_InProcess_IssueforIceCream"
                                                     },
                                            new DataTable[] { 
                                                            dtIssuetoIceCream
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private DataTable GetIssuetoIceCreamData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Variant", typeof(string));       
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        dt.Columns.Add("Type", typeof(string));
        foreach (GridViewRow row in gvIssuetoIceCream.Rows)
        {
            Label lblVariant = (Label)row.FindControl("lblVariant");
            Label lblType = (Label)row.FindControl("lblType");
            DropDownList ddlType = (DropDownList)row.FindControl("ddlType");
            TextBox txtIssuetoIceCreamQtyInLtr = (TextBox)row.FindControl("txtIssuetoIceCreamQtyInLtr");
            TextBox txtIssuetoIceCreamQtyInKg = (TextBox)row.FindControl("txtIssuetoIceCreamQtyInKg");
            TextBox txtIssuetoIceCreamFat_per = (TextBox)row.FindControl("txtIssuetoIceCreamFat_per");
            TextBox txtIssuetoIceCreamSnf_Per = (TextBox)row.FindControl("txtIssuetoIceCreamSnf_Per");
            TextBox txtIssuetoIceCreamFatInKg = (TextBox)row.FindControl("txtIssuetoIceCreamFatInKg");
            TextBox txtIssuetoIceCreamSnfInKg = (TextBox)row.FindControl("txtIssuetoIceCreamSnfInKg");

            if (txtIssuetoIceCreamQtyInLtr.Text != "" && txtIssuetoIceCreamQtyInKg.Text != "" && txtIssuetoIceCreamQtyInLtr.Text != "0.00" && txtIssuetoIceCreamQtyInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblVariant.Text;
                dr[1] = txtIssuetoIceCreamQtyInLtr.Text;
                dr[2] = txtIssuetoIceCreamQtyInKg.Text;
                dr[3] = txtIssuetoIceCreamFat_per.Text;
                dr[4] = txtIssuetoIceCreamSnf_Per.Text;
                dr[5] = txtIssuetoIceCreamFatInKg.Text;
                dr[6] = txtIssuetoIceCreamSnfInKg.Text;
                dr[7] = ddlType.SelectedItem.Text;
                dt.Rows.Add(dr);

            }

        }
        return dt;
    }
    protected void btnIssuetoIceCreamGetTotal_Click(object sender, EventArgs e)
    {
        btnIssuetoIceCream.Enabled = false;
        decimal IsuuetoIceCreamQtyLtr = 0;
        decimal IsuuetoIceCreamQtyKg = 0;
        decimal IsuuetoIceCreamFatKg = 0;
        decimal IsuuetoIceCreamSnfKg = 0;
        foreach (GridViewRow rows in gvIssuetoIceCream.Rows)
        {
            TextBox txtIssuetoIceCreamQtyInLtr = (TextBox)rows.FindControl("txtIssuetoIceCreamQtyInLtr");
            TextBox txtIssuetoIceCreamQtyInKg = (TextBox)rows.FindControl("txtIssuetoIceCreamQtyInKg");
            TextBox txtIssuetoIceCreamFatInKg = (TextBox)rows.FindControl("txtIssuetoIceCreamFatInKg");
            TextBox txtIssuetoIceCreamSnfInKg = (TextBox)rows.FindControl("txtIssuetoIceCreamSnfInKg");
            if (txtIssuetoIceCreamQtyInLtr.Text != "")
            {
                IsuuetoIceCreamQtyLtr += decimal.Parse(txtIssuetoIceCreamQtyInLtr.Text);
            }
            if (txtIssuetoIceCreamQtyInKg.Text != "")
            {
                IsuuetoIceCreamQtyKg += decimal.Parse(txtIssuetoIceCreamQtyInKg.Text);
            }
            if (txtIssuetoIceCreamFatInKg.Text != "")
            {
                IsuuetoIceCreamFatKg += decimal.Parse(txtIssuetoIceCreamFatInKg.Text);
            }
            if (txtIssuetoIceCreamSnfInKg.Text != "")
            {
                IsuuetoIceCreamSnfKg += decimal.Parse(txtIssuetoIceCreamSnfInKg.Text);
            }
        }

        gvIssuetoIceCream.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvIssuetoIceCream.FooterRow.Cells[3].Text = "<b>" + IsuuetoIceCreamQtyLtr.ToString() + "</b>";
        gvIssuetoIceCream.FooterRow.Cells[2].Text = "<b>" + IsuuetoIceCreamQtyKg.ToString() + "</b>";
        gvIssuetoIceCream.FooterRow.Cells[6].Text = "<b>" + IsuuetoIceCreamFatKg.ToString() + "</b>";
        gvIssuetoIceCream.FooterRow.Cells[7].Text = "<b>" + IsuuetoIceCreamSnfKg.ToString() + "</b>";
        if (IsuuetoIceCreamQtyKg > 0)
        {
            btnIssuetoIceCream.Enabled = true;
        }
    }
    #endregion

    #region ISSUE TO OTHERS
    protected void gvIssuetoother_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "ISSUE TO OTHERS";
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvIssuetoother.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnIssuetoother_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtIssuetoothers = new DataTable();
                dtIssuetoothers = GetIssuetoothersData();


                if (dtIssuetoothers.Rows.Count > 0)
                {


                    if (btnIssuetoother.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_InProcess_OutflowOther"
                                                     },
                                            new DataTable[] { 
                                                          dtIssuetoothers
                                                          //dtIDF,
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnIssuetoother.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"19",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                          "type_Production_Milk_InProcess_OutflowOther"
                                                     },
                                            new DataTable[] { 
                                                            dtIssuetoothers
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private DataTable GetIssuetoothersData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Variant", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvIssuetoother.Rows)
        {
            Label lblVariant = (Label)row.FindControl("lblVariant");
            TextBox txtIssuetootherQtyInLtr = (TextBox)row.FindControl("txtIssuetootherQtyInLtr");
            TextBox txtIssuetootherQtyInKg = (TextBox)row.FindControl("txtIssuetootherQtyInKg");
            TextBox txtIssuetootherFat_per = (TextBox)row.FindControl("txtIssuetootherFat_per");
            TextBox txtIssuetootherSnf_Per = (TextBox)row.FindControl("txtIssuetootherSnf_Per");
            TextBox txtIssuetootherFatInKg = (TextBox)row.FindControl("txtIssuetootherFatInKg");
            TextBox txtIssuetootherSnfInKg = (TextBox)row.FindControl("txtIssuetootherSnfInKg");

            if (txtIssuetootherQtyInLtr.Text != "" && txtIssuetootherQtyInKg.Text != "" && txtIssuetootherQtyInLtr.Text != "0.00" && txtIssuetootherQtyInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblVariant.Text;
                dr[1] = txtIssuetootherQtyInLtr.Text;
                dr[2] = txtIssuetootherQtyInKg.Text;
                dr[3] = txtIssuetootherFat_per.Text;
                dr[4] = txtIssuetootherSnf_Per.Text;
                dr[5] = txtIssuetootherFatInKg.Text;
                dr[6] = txtIssuetootherSnfInKg.Text;
                dt.Rows.Add(dr);

            }

        }
        return dt;
    }
    protected void btnIssuetootherGetTotal_Click(object sender, EventArgs e)
    {
        btnIssuetoother.Enabled = false;
        decimal IsuuetoOtherQtyLtr = 0;
        decimal IsuuetoOtherQtyKg = 0;
        decimal IsuuetoOtherFatKg = 0;
        decimal IsuuetoOtherSnfKg = 0;
        foreach (GridViewRow rows in gvIssuetoother.Rows)
        {
            TextBox txtIssuetootherQtyInKg = (TextBox)rows.FindControl("txtIssuetootherQtyInKg");
            TextBox txtIssuetootherQtyInLtr = (TextBox)rows.FindControl("txtIssuetootherQtyInLtr");
            TextBox txtIssuetootherFatInKg = (TextBox)rows.FindControl("txtIssuetootherFatInKg");
            TextBox txtIssuetootherSnfInKg = (TextBox)rows.FindControl("txtIssuetootherSnfInKg");
            if (txtIssuetootherQtyInLtr.Text != "")
            {
                IsuuetoOtherQtyLtr += decimal.Parse(txtIssuetootherQtyInLtr.Text);
            }
            if (txtIssuetootherQtyInKg.Text != "")
            {
                IsuuetoOtherQtyKg += decimal.Parse(txtIssuetootherQtyInKg.Text);
            }
            if (txtIssuetootherFatInKg.Text != "")
            {
                IsuuetoOtherFatKg += decimal.Parse(txtIssuetootherFatInKg.Text);
            }
            if (txtIssuetootherSnfInKg.Text != "")
            {
                IsuuetoOtherSnfKg += decimal.Parse(txtIssuetootherSnfInKg.Text);
            }
        }

        gvIssuetoother.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvIssuetoother.FooterRow.Cells[2].Text = "<b>" + IsuuetoOtherQtyLtr.ToString() + "</b>";
        gvIssuetoother.FooterRow.Cells[1].Text = "<b>" + IsuuetoOtherQtyKg.ToString() + "</b>";
        gvIssuetoother.FooterRow.Cells[5].Text = "<b>" + IsuuetoOtherFatKg.ToString() + "</b>";
        gvIssuetoother.FooterRow.Cells[6].Text = "<b>" + IsuuetoOtherSnfKg.ToString() + "</b>";
        if (IsuuetoOtherQtyKg > 0)
        {
            btnIssuetoother.Enabled = true;
        }
    }
    #endregion

    #region Others
    protected void gvOther_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Others";
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvOther.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnOutflowOther_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                DataTable dtOthers = new DataTable();
                dtOthers = GetOthersData();


                if (dtOthers.Rows.Count > 0)
                {


                    if (btnOutflowOther.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_InProcess_Other",
                                                       
                                                     },
                                            new DataTable[] {
                                                          dtOthers,
                                                          
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnOutflowOther.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"20",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                         "type_Production_Milk_InProcess_Other",
                                                     },
                                            new DataTable[] { 
                                                            dtOthers,
                                                            
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private DataTable GetOthersData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Variant", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvOther.Rows)
        {
            Label lblVariant = (Label)row.FindControl("lblVariant");
            TextBox txtotherQtyInLtr = (TextBox)row.FindControl("txtotherQtyInLtr");
            TextBox txtotherQtyInKg = (TextBox)row.FindControl("txtotherQtyInKg");
            TextBox txtotherFat_per = (TextBox)row.FindControl("txtotherFat_per");
            TextBox txtotherSnf_Per = (TextBox)row.FindControl("txtotherSnf_Per");
            TextBox txtotherFatInKg = (TextBox)row.FindControl("txtotherFatInKg");
            TextBox txtotherSnfInKg = (TextBox)row.FindControl("txtotherSnfInKg");

            if (txtotherQtyInLtr.Text != "" && txtotherQtyInKg.Text != "" && txtotherQtyInLtr.Text != "0.00" && txtotherQtyInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblVariant.Text;
                dr[1] = txtotherQtyInLtr.Text;
                dr[2] = txtotherQtyInKg.Text;
                dr[3] = txtotherFat_per.Text;
                dr[4] = txtotherSnf_Per.Text;
                dr[5] = txtotherFatInKg.Text;
                dr[6] = txtotherSnfInKg.Text;
                dt.Rows.Add(dr);

            }

        }
        return dt;
    }
    protected void btnOutflowOtherGetTotal_Click(object sender, EventArgs e)
    {
        btnOutflowOther.Enabled = false;
        decimal OtherQtyLtr = 0;
        decimal OtherQtyKg = 0;
        decimal OtherFatKg = 0;
        decimal OtherSnfKg = 0;
        foreach (GridViewRow rows in gvOther.Rows)
        {
            TextBox txtotherQtyInKg = (TextBox)rows.FindControl("txtotherQtyInKg");
            TextBox txtotherQtyInLtr = (TextBox)rows.FindControl("txtotherQtyInLtr");
            TextBox txtotherFatInKg = (TextBox)rows.FindControl("txtotherFatInKg");
            TextBox txtotherSnfInKg = (TextBox)rows.FindControl("txtotherSnfInKg");
            if (txtotherQtyInKg.Text != "")
            {
                OtherQtyKg += decimal.Parse(txtotherQtyInKg.Text);
            }
            if (txtotherQtyInLtr.Text != "")
            {
                OtherQtyLtr += decimal.Parse(txtotherQtyInLtr.Text);
            }
            if (txtotherFatInKg.Text != "")
            {
                OtherFatKg += decimal.Parse(txtotherFatInKg.Text);
            }
            if (txtotherSnfInKg.Text != "")
            {
                OtherSnfKg += decimal.Parse(txtotherSnfInKg.Text);
            }
        }

        gvOther.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvOther.FooterRow.Cells[2].Text = "<b>" + OtherQtyLtr.ToString() + "</b>";
        gvOther.FooterRow.Cells[1].Text = "<b>" + OtherQtyKg.ToString() + "</b>";
        gvOther.FooterRow.Cells[5].Text = "<b>" + OtherFatKg.ToString() + "</b>";
        gvOther.FooterRow.Cells[6].Text = "<b>" + OtherSnfKg.ToString() + "</b>";
        if (OtherQtyKg > 0)
        {
            btnOutflowOther.Enabled = true;
        }
    }
    #endregion

    //#region Issue of Goat Milk
    //protected void GVIssueofgoatmilk_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        GridView HeaderGrid = (GridView)sender;
    //        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        TableCell HeaderCell = new TableCell();
    //        HeaderCell.Text = "Issue of Goat Milk";
    //        HeaderCell.ColumnSpan = 7;
    //        HeaderGridRow.Cells.Add(HeaderCell);


    //        GVIssueofgoatmilk.Controls[0].Controls.AddAt(0, HeaderGridRow);

    //    }
    //}
    //protected void btnIssueofgoatmilkSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
    //        {

    //            DataTable dtIssueofgoatmilk = new DataTable();
    //            dtIssueofgoatmilk = GetIssueofgoatmilkData();


    //            if (dtIssueofgoatmilk.Rows.Count > 0)
    //            {


    //                if (btnOutflowOther.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
    //                {
    //                    DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
    //                                        new string[] {"flag",
    //                                                   "Office_ID",
    //                                                   "ProductSection_ID",
    //                                                   "Date",
    //                                                   "Shift_Id",
    //                                                   "CreatedBy",
    //                                                   "CreatedBy_IP",
    //                                                   },
    //                                        new string[]{"0",
    //                                                   objdb.Office_ID(),
    //                                                   ddlPSection.SelectedValue,
    //                                                   Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
    //                                                   ddlShift.SelectedValue,
    //                                                   objdb.createdBy(),
    //                                                   objdb.GetLocalIPAddress()
    //                                                  },
    //                                        new string[] { 
    //                                                   "type_Production_Milk_InProcess_Issueofgoatmilk",
                                                       
    //                                                 },
    //                                        new DataTable[] {
    //                                                      dtIssueofgoatmilk,
                                                          
    //                                      },
    //                                                          "TableSave");
    //                    if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //                    {

    //                        string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //                        txtDate_TextChanged(sender, e);
    //                    }

    //                    else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
    //                    {
    //                        string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

    //                    }

    //                    else
    //                    {
    //                        string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


    //                    }
    //                }
    //                else if (btnOutflowOther.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
    //                {
    //                    DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
    //                                        new string[] {"flag",
    //                                                   "MilkProductionProcess_Id",
    //                                                   "Office_ID",
    //                                                   "ProductSection_ID",
    //                                                   "Date",
    //                                                   "Shift_Id",
    //                                                   },
    //                                        new string[]{"31",
    //                                                  ViewState["MilkProductionProcess_Id"].ToString(),
    //                                                  objdb.Office_ID(),
    //                                                  ddlPSection.SelectedValue,
    //                                                  Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
    //                                                  ddlShift.SelectedValue,
    //                                                  },
    //                                        new string[] { 
    //                                                     "type_Production_Milk_InProcess_Issueofgoatmilk",
    //                                                 },
    //                                        new DataTable[] { 
    //                                                        dtIssueofgoatmilk,
                                                            
    //                                      },
    //                                                          "TableSave");
    //                    if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //                    {

    //                        string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //                        txtDate_TextChanged(sender, e);

    //                    }

    //                    else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
    //                    {
    //                        string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

    //                    }

    //                    else
    //                    {
    //                        string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


    //                    }
    //                }
    //            }
    //            else
    //            {
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
    //            }
    //            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //private DataTable GetIssueofgoatmilkData()
    //{
    //    DataTable dt = new DataTable();
    //    DataRow dr;
    //    dt.Columns.Add("Variant", typeof(string));
    //    dt.Columns.Add("QtyInLtr", typeof(string));
    //    dt.Columns.Add("QtyInKg", typeof(string));
    //    dt.Columns.Add("Fat_Per", typeof(string));
    //    dt.Columns.Add("Snf_Per", typeof(string));
    //    dt.Columns.Add("KgFat", typeof(string));
    //    dt.Columns.Add("KgSnf", typeof(string));
    //    foreach (GridViewRow row in GVIssueofgoatmilk.Rows)
    //    {
    //        Label lblVariant = (Label)row.FindControl("lblIssueofgoatmilkVariant");
    //        TextBox txtotherQtyInLtr = (TextBox)row.FindControl("txtIssueofgoatmilkQtyInLtr");
    //        TextBox txtotherQtyInKg = (TextBox)row.FindControl("txtIssueofgoatmilkQtyInKg");
    //        TextBox txtotherFat_per = (TextBox)row.FindControl("txtIssueofgoatmilkFat_per");
    //        TextBox txtotherSnf_Per = (TextBox)row.FindControl("txtIssueofgoatmilkSnf_Per");
    //        TextBox txtotherFatInKg = (TextBox)row.FindControl("txtIssueofgoatmilkFatInKg");
    //        TextBox txtotherSnfInKg = (TextBox)row.FindControl("txtIssueofgoatmilkSnfInKg");

    //        if (txtotherQtyInLtr.Text != "" && txtotherQtyInKg.Text != "" && txtotherQtyInLtr.Text != "0.00" && txtotherQtyInKg.Text != "0.00")
    //        {
    //            dr = dt.NewRow();
    //            dr[0] = lblVariant.Text;
    //            dr[1] = txtotherQtyInLtr.Text;
    //            dr[2] = txtotherQtyInKg.Text;
    //            dr[3] = txtotherFat_per.Text;
    //            dr[4] = txtotherSnf_Per.Text;
    //            dr[5] = txtotherFatInKg.Text;
    //            dr[6] = txtotherSnfInKg.Text;
    //            dt.Rows.Add(dr);

    //        }

    //    }
    //    return dt;
    //}
    //protected void btnIssueofgoatmilk_gettotal_Click(object sender, EventArgs e)
    //{
    //    btnIssueofgoatmilkSave.Enabled = false;
    //    decimal OtherQtyLtr = 0;
    //    decimal OtherQtyKg = 0;
    //    decimal OtherFatKg = 0;
    //    decimal OtherSnfKg = 0;
    //    foreach (GridViewRow rows in GVIssueofgoatmilk.Rows)
    //    {
    //        TextBox txtotherQtyInKg = (TextBox)rows.FindControl("txtIssueofgoatmilkQtyInKg");
    //        TextBox txtotherQtyInLtr = (TextBox)rows.FindControl("txtIssueofgoatmilkQtyInLtr");
    //        TextBox txtotherFatInKg = (TextBox)rows.FindControl("txtIssueofgoatmilkFat_per");
    //        TextBox txtotherSnfInKg = (TextBox)rows.FindControl("txtIssueofgoatmilkSnf_Per");
    //        if (txtotherQtyInKg.Text != "")
    //        {
    //            OtherQtyKg += decimal.Parse(txtotherQtyInKg.Text);
    //        }
    //        if (txtotherQtyInLtr.Text != "")
    //        {
    //            OtherQtyLtr += decimal.Parse(txtotherQtyInLtr.Text);
    //        }
    //        if (txtotherFatInKg.Text != "")
    //        {
    //            OtherFatKg += decimal.Parse(txtotherFatInKg.Text);
    //        }
    //        if (txtotherSnfInKg.Text != "")
    //        {
    //            OtherSnfKg += decimal.Parse(txtotherSnfInKg.Text);
    //        }
    //    }

    //    GVIssueofgoatmilk.FooterRow.Cells[0].Text = "<b>Total : </b>";
    //    GVIssueofgoatmilk.FooterRow.Cells[2].Text = "<b>" + OtherQtyLtr.ToString() + "</b>";
    //    GVIssueofgoatmilk.FooterRow.Cells[1].Text = "<b>" + OtherQtyKg.ToString() + "</b>";
    //    GVIssueofgoatmilk.FooterRow.Cells[5].Text = "<b>" + OtherFatKg.ToString() + "</b>";
    //    GVIssueofgoatmilk.FooterRow.Cells[6].Text = "<b>" + OtherSnfKg.ToString() + "</b>";
    //    if (OtherQtyKg > 0)
    //    {
    //        btnIssueofgoatmilkSave.Enabled = true;
    //    }
    //}
    //#endregion

    #region CLOSING BALANCES

    protected void gvClosingBalances_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "CLOSING BALANCES";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvClosingBalances.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnClosingBalances_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtCB = new DataTable();
                dtCB = GetClosingBalancesData();


                if (dtCB.Rows.Count > 0)
                {


                    if (btnClosingBalances.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_OutProcess_ClosingBal"
                                                     },
                                            new DataTable[] {
                                                          dtCB
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnClosingBalances.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"21",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                         "type_Production_Milk_OutProcess_ClosingBal"
                                                     },
                                            new DataTable[] { 
                                                            dtCB
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private DataTable GetClosingBalancesData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("I_MCID", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvClosingBalances.Rows)
        {
            Label lblClosingI_MCID = (Label)row.FindControl("lblClosingI_MCID");

            TextBox txtClosingBalancesQuantityInLtr = (TextBox)row.FindControl("txtClosingBalancesQuantityInLtr");
            TextBox txtClosingBalancesQuantityInKg = (TextBox)row.FindControl("txtClosingBalancesQuantityInKg");
            TextBox txtClosingBalancesFat_Per = (TextBox)row.FindControl("txtClosingBalancesFat_Per");
            TextBox txtClosingBalancesSnf_Per = (TextBox)row.FindControl("txtClosingBalancesSnf_Per");
            TextBox txtClosingBalancesFATInKg = (TextBox)row.FindControl("txtClosingBalancesFATInKg");
            TextBox txtClosingBalancesSnfInKg = (TextBox)row.FindControl("txtClosingBalancesSnfInKg");
            if (txtClosingBalancesQuantityInKg.Text != "" && txtClosingBalancesQuantityInKg.Text != "" && txtClosingBalancesQuantityInKg.Text != "0.00" && txtClosingBalancesQuantityInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblClosingI_MCID.Text;
                dr[1] = txtClosingBalancesQuantityInLtr.Text;
                dr[2] = txtClosingBalancesQuantityInKg.Text;
                dr[3] = txtClosingBalancesFat_Per.Text;
                dr[4] = txtClosingBalancesSnf_Per.Text;
                dr[5] = txtClosingBalancesFATInKg.Text;
                dr[6] = txtClosingBalancesSnfInKg.Text;

                dt.Rows.Add(dr);

            }
        }
        return dt;
    }
    protected void btnClosingBalancesGetTotal_Click(object sender, EventArgs e)
    {
        btnClosingBalances.Enabled = false;
        decimal CBQtyLtr = 0;
        decimal CBQtyKg = 0;
        decimal CBFatKg = 0;
        decimal CBSnfKg = 0;
        foreach (GridViewRow rows in gvClosingBalances.Rows)
        {
            TextBox txtClosingBalancesQuantityInLtr = (TextBox)rows.FindControl("txtClosingBalancesQuantityInLtr");
            TextBox txtClosingBalancesQuantityInKg = (TextBox)rows.FindControl("txtClosingBalancesQuantityInKg");
            TextBox txtClosingBalancesFATInKg = (TextBox)rows.FindControl("txtClosingBalancesFATInKg");
            TextBox txtClosingBalancesSnfInKg = (TextBox)rows.FindControl("txtClosingBalancesSnfInKg");
            if (txtClosingBalancesQuantityInLtr.Text != "")
            {
                CBQtyLtr += decimal.Parse(txtClosingBalancesQuantityInLtr.Text);
            }
            if (txtClosingBalancesQuantityInKg.Text != "")
            {
                CBQtyKg += decimal.Parse(txtClosingBalancesQuantityInKg.Text);
            }
            if (txtClosingBalancesFATInKg.Text != "")
            {
                CBFatKg += decimal.Parse(txtClosingBalancesFATInKg.Text);
            }
            if (txtClosingBalancesSnfInKg.Text != "")
            {
                CBSnfKg += decimal.Parse(txtClosingBalancesSnfInKg.Text);
            }
        }

        gvClosingBalances.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvClosingBalances.FooterRow.Cells[2].Text = "<b>" + CBQtyLtr.ToString() + "</b>";
        gvClosingBalances.FooterRow.Cells[1].Text = "<b>" + CBQtyKg.ToString() + "</b>";
        gvClosingBalances.FooterRow.Cells[5].Text = "<b>" + CBFatKg.ToString() + "</b>";
        gvClosingBalances.FooterRow.Cells[6].Text = "<b>" + CBSnfKg.ToString() + "</b>";
        if (CBQtyKg > 0)
        {
            btnClosingBalances.Enabled = true;
        }
    }
    #endregion

    #region COLD ROOM BALANCES

    protected void gvColRoomBalances_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "COLD ROOM BALANCES";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvColdRoomBalances.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnColdRoomBalances_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtCRB = new DataTable();
                dtCRB = GetColdRoomBalances();


                if (dtCRB.Rows.Count > 0)
                {


                    if (btnColdRoomBalances.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() == "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                            new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                            new string[] { 
                                                       "type_Production_Milk_OutProcess_CRB"
                                                      
                                                     },
                                            new DataTable[] { 
                                                          dtCRB
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);
                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                    else if (btnColdRoomBalances.Text == "Save" && ViewState["MilkProductionProcess_Id"].ToString() != "")
                    {
                        DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                            new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                            new string[]{"22",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                            new string[] { 
                                                         "type_Production_Milk_OutProcess_CRB"
                                                     },
                                            new DataTable[] {  
                                                            dtCRB
                                          },
                                                              "TableSave");
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            txtDate_TextChanged(sender, e);

                        }

                        else if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                        {
                            string success = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                        }

                        else
                        {
                            string error = dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Please Enter at Least One Record", true);
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private DataTable GetColdRoomBalances()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Item_id", typeof(string));
        dt.Columns.Add("NoOfPackets", typeof(string));
        dt.Columns.Add("QtyInLtr", typeof(string));
        dt.Columns.Add("QtyInKg", typeof(string));
        dt.Columns.Add("Fat_Per", typeof(string));
        dt.Columns.Add("Snf_Per", typeof(string));
        dt.Columns.Add("KgFat", typeof(string));
        dt.Columns.Add("KgSnf", typeof(string));
        foreach (GridViewRow row in gvColdRoomBalances.Rows)
        {
            Label lblCRBItem_id = (Label)row.FindControl("lblCRBItem_id");
            TextBox txtColdRoomBalancesPackets = (TextBox)row.FindControl("txtColdRoomBalancesPackets");
            TextBox txtColdRoomBalancesQuantityInLtr = (TextBox)row.FindControl("txtColdRoomBalancesQuantityInLtr");
            TextBox txtColdRoomBalancesQuantityInKg = (TextBox)row.FindControl("txtColdRoomBalancesQuantityInKg");
            TextBox txtColdRoomBalancesFat_Per = (TextBox)row.FindControl("txtColdRoomBalancesFat_Per");
            TextBox txtColdRoomBalancesSnf_Per = (TextBox)row.FindControl("txtColdRoomBalancesSnf_Per");
            TextBox txtColdRoomBalancesFATInKg = (TextBox)row.FindControl("txtColdRoomBalancesFATInKg");
            TextBox txtColdRoomBalancesSNFInKg = (TextBox)row.FindControl("txtColdRoomBalancesSNFInKg");
            if (txtColdRoomBalancesQuantityInLtr.Text != "" && txtColdRoomBalancesQuantityInKg.Text != "" && txtColdRoomBalancesQuantityInLtr.Text != "0.00" && txtColdRoomBalancesQuantityInKg.Text != "0.00")
            {
                dr = dt.NewRow();
                dr[0] = lblCRBItem_id.Text;
                dr[1] = txtColdRoomBalancesPackets.Text;
                dr[2] = txtColdRoomBalancesQuantityInLtr.Text;
                dr[3] = txtColdRoomBalancesQuantityInKg.Text;
                dr[4] = txtColdRoomBalancesFat_Per.Text;
                dr[5] = txtColdRoomBalancesSnf_Per.Text;
                dr[6] = txtColdRoomBalancesFATInKg.Text;
                dr[7] = txtColdRoomBalancesSNFInKg.Text;

                dt.Rows.Add(dr);

            }
        }
        return dt;
    }
    protected void btnColdRoomBalancesGetTotal_Click(object sender, EventArgs e)
    {
        btnColdRoomBalances.Enabled = false;
        decimal CRBQtyLtr = 0;
        decimal CRBQtyKg = 0;
        decimal CRBFatKg = 0;
        decimal CRBSnfKg = 0;
        foreach (GridViewRow rows in gvColdRoomBalances.Rows)
        {
            TextBox txtColdRoomBalancesQuantityInLtr = (TextBox)rows.FindControl("txtColdRoomBalancesQuantityInLtr");
            TextBox txtColdRoomBalancesQuantityInKg = (TextBox)rows.FindControl("txtColdRoomBalancesQuantityInKg");
            TextBox txtColdRoomBalancesFATInKg = (TextBox)rows.FindControl("txtColdRoomBalancesFATInKg");
            TextBox txtColdRoomBalancesSnfInKg = (TextBox)rows.FindControl("txtColdRoomBalancesSnfInKg");
            if (txtColdRoomBalancesQuantityInLtr.Text != "")
            {
                CRBQtyLtr += decimal.Parse(txtColdRoomBalancesQuantityInLtr.Text);
            }
            if (txtColdRoomBalancesQuantityInKg.Text != "")
            {
                CRBQtyKg += decimal.Parse(txtColdRoomBalancesQuantityInKg.Text);
            }
            if (txtColdRoomBalancesFATInKg.Text != "")
            {
                CRBFatKg += decimal.Parse(txtColdRoomBalancesFATInKg.Text);
            }
            if (txtColdRoomBalancesSnfInKg.Text != "")
            {
                CRBSnfKg += decimal.Parse(txtColdRoomBalancesSnfInKg.Text);
            }
        }
        if (gvColdRoomBalances.Rows.Count > 0)
        {
            gvColdRoomBalances.FooterRow.Cells[0].Text = "<b>Total : </b>";
            gvColdRoomBalances.FooterRow.Cells[2].Text = "<b>" + CRBQtyLtr.ToString() + "</b>";
            gvColdRoomBalances.FooterRow.Cells[3].Text = "<b>" + CRBQtyKg.ToString() + "</b>";
            gvColdRoomBalances.FooterRow.Cells[6].Text = "<b>" + CRBFatKg.ToString() + "</b>";
            gvColdRoomBalances.FooterRow.Cells[7].Text = "<b>" + CRBSnfKg.ToString() + "</b>";
        }
        if (CRBQtyKg > 0)
        {
            btnColdRoomBalances.Enabled = true;
        }
    }
    #endregion
      
    #endregion

    #region Changed Event
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        SetInFlowInitialRow();
        SetOutFlowInitialRow();
        FillSheet();
        FillRRData();
        GetSectionDetail();
        btnOpeningGetTotal_Click(sender, e);
        btnProcessGetTotal_Click(sender, e);
        btnReturnGetTotal_Click(sender, e);
        btnBMCDCSCollectionGetTotal_Click(sender, e);
        btnCCWiseProcurementGetTotal_Click(sender, e);
        btnCanesCollectionGetTotal_Click(sender, e);
        btnrcvdfrmothrUnionGetTotal_Click(sender, e);
        btnForPowderConversionGetTotal_Click(sender, e);
        btnInFlowOtherGetTotal_Click(sender, e);
        btnPaticularsGetTotal_Click(sender, e);
        btnMilkToIPGetTotal_Click(sender, e);
        btnIssuetoMDPOrCCGetTotal_Click(sender, e);
        btnIssuetootherGetTotal_Click(sender, e);
        btnIssuetoPowderPlantGetTotal_Click(sender, e);
        btnIssuetoCreamGetTotal_Click(sender, e);
        btnIssuetoIceCreamGetTotal_Click(sender, e);
        btnOutflowOtherGetTotal_Click(sender, e);
        btnClosingBalancesGetTotal_Click(sender, e);
        btnIsuuetoOtherPartyGetTotal_Click(sender, e);
        btnColdRoomBalancesGetTotal_Click(sender, e);
		btnCCWiseGoatMilkProcurementGetTotal_Click(sender, e);

    }
    protected void ddlShift_TextChanged(object sender, EventArgs e)
    {
        SetInFlowInitialRow();
        SetOutFlowInitialRow();
        FillSheet();
        FillRRData();
        GetSectionDetail();
		btnOpeningGetTotal_Click(sender, e);
        btnProcessGetTotal_Click(sender, e);
        btnReturnGetTotal_Click(sender, e);
        btnBMCDCSCollectionGetTotal_Click(sender, e);
        btnCCWiseProcurementGetTotal_Click(sender, e);
        btnCanesCollectionGetTotal_Click(sender, e);
        btnrcvdfrmothrUnionGetTotal_Click(sender, e);
        btnForPowderConversionGetTotal_Click(sender, e);
        btnInFlowOtherGetTotal_Click(sender, e);
        btnPaticularsGetTotal_Click(sender, e);
        btnMilkToIPGetTotal_Click(sender, e);
        btnIssuetoMDPOrCCGetTotal_Click(sender, e);
        btnIssuetootherGetTotal_Click(sender, e);
        btnIssuetoPowderPlantGetTotal_Click(sender, e);
        btnIssuetoCreamGetTotal_Click(sender, e);
        btnIssuetoIceCreamGetTotal_Click(sender, e);
        btnOutflowOtherGetTotal_Click(sender, e);
        btnClosingBalancesGetTotal_Click(sender, e);
        btnIsuuetoOtherPartyGetTotal_Click(sender, e);
        btnColdRoomBalancesGetTotal_Click(sender, e);
		btnCCWiseGoatMilkProcurementGetTotal_Click(sender, e);
    }
    #endregion

    #region ButtonClick Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DataTable dtOpening = new DataTable();
                dtOpening = GetOpeningData();

                DataTable dtProcess = new DataTable();
                dtProcess = GetProcessData();

                DataTable dtReturn = new DataTable();
                dtReturn = GetReturnData();

                DataTable dtCCWP = new DataTable();
                dtCCWP = GetCCWiseProcuremetData();

                DataTable dtCCWGMP = new DataTable();
                dtCCWGMP = GetCCWiseGoatMilkProcuremetData();

                DataTable dtBMCDCSColl = new DataTable();
                dtBMCDCSColl = GetBMCDCSCollData();

                DataTable dtCanesColl = new DataTable();
                dtCanesColl = GetCanesCollData();

                DataTable dtrcvdfromothrunion = new DataTable();
                dtrcvdfromothrunion = GetRecvdFromOtherUnionData();

                DataTable dtforpowdrconv = new DataTable();
                dtforpowdrconv = GetForPowderConversionData();

                DataTable dtInFlowOthers = new DataTable();
                dtInFlowOthers = GetInFlowOthersData();

                DataTable dtParticulars = new DataTable();
                dtParticulars = GetParticularsData();

                DataTable dtMIP = new DataTable();
                dtMIP = GetMIPData();

                DataTable dtIssued = new DataTable();
                dtIssued = GetIssuetoMDPCCData();

                DataTable dtCB = new DataTable();
                dtCB = GetClosingBalancesData();

                DataTable dtCRB = new DataTable();
                dtCRB = GetColdRoomBalances();

                DataTable dtIssuedOtherParty = new DataTable();
                dtIssuedOtherParty = GetIssuetoOtherParty();

                DataTable dtIssuetoPP = new DataTable();
                dtIssuetoPP = GetIssuetoPowderPlant();

                DataTable dtIssuetoCream = new DataTable();
                dtIssuetoCream = GetIssuetoCreamData();

                DataTable dtIssuetoothers = new DataTable();
                dtIssuetoothers = GetIssuetoothersData();

                DataTable dtOthers = new DataTable();
                dtOthers = GetOthersData();

                //DataTable dtIssueofgoatmilk = new DataTable();
                //dtIssueofgoatmilk = GetIssueofgoatmilkData();

                DataTable dtIDF = new DataTable();
                dtIDF = GetRRSheetData();

                DataTable dtIssuetoIceCream = new DataTable();
                dtIssuetoIceCream = GetIssuetoIceCreamData();

                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                        new string[] {"flag",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       "CreatedBy",
                                                       "CreatedBy_IP",
                                                       },
                                        new string[]{"0",
                                                       objdb.Office_ID(),
                                                       ddlPSection.SelectedValue,
                                                       Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                       ddlShift.SelectedValue,
                                                       objdb.createdBy(),
                                                       objdb.GetLocalIPAddress()
                                                      },
                                        new string[] { "type_Production_Milk_InProcess_Opening" ,
                                                       "type_Production_Milk_InProcess_Process", 
                                                       "type_Production_Milk_InProcess_Return",
                                                       "type_Production_Milk_InProcess_CCWiseProcurement",
                                                       "type_Production_Milk_OutProcess_Particular",
                                                       "type_Production_Milk_OutProcess_MIP",
                                                       "type_Production_Milk_OutProcess_IssuetoMDP_CC",
                                                       "type_Production_Milk_OutProcess_ClosingBal",
                                                       "type_Production_Milk_OutProcess_CRB",
                                                       "type_Production_Milk_OutProcess_IssuetoOther",
                                                       "type_Production_Milk_InProcess_BMCDCSColl",
                                                       "type_Production_Milk_InProcess_InflowOther",
                                                       "type_Production_Milk_InProcess_RecvdfromotherUnion",
                                                       "type_Production_Milk_InProcess_ForPowderConversion",
                                                       "type_Production_Milk_InProcess_IssueforPowderPlant",
                                                       "type_Production_Milk_InProcess_IssueforCream",
                                                       "type_Production_Milk_InProcess_OutflowOther",
                                                       "type_Production_Milk_InProcess_Other",
                                                       "type_Production_Milk_InOutProcessRRSheet",
                                                       "type_Production_Milk_InProcess_CanesCollection",
                                                       "type_Production_Milk_InProcess_IssueforIceCream",
                                                       "type_Production_Milk_InProcess_CCWiseGoatMilkProcurement",
                                                       //"type_Production_Milk_InProcess_Issueofgoatmilk"
                                                     },
                                        new DataTable[] { dtOpening, 
                                                          dtProcess, 
                                                          dtReturn,
                                                          dtCCWP, 
                                                          dtParticulars, 
                                                          dtMIP,
                                                          dtIssued, 
                                                          dtCB, 
                                                          dtCRB,
                                                          dtIssuedOtherParty,
                                                          dtBMCDCSColl,
                                                          dtInFlowOthers,
                                                          dtrcvdfromothrunion,
                                                          dtforpowdrconv,
                                                          dtIssuetoPP,
                                                          dtIssuetoCream,
                                                          dtIssuetoothers,
                                                          dtOthers,
                                                          dtIDF,
                                                          dtCanesColl,
                                                          dtIssuetoIceCream,
                                                          dtCCWGMP,
                                                          //dtIssueofgoatmilk
                                          },
                                                          "TableSave");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        txtDate_TextChanged(sender, e);
                    }

                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                    }

                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                    }
                }
                else if (btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                                        new string[] {"flag",
                                                       "MilkProductionProcess_Id",
                                                       "Office_ID",
                                                       "ProductSection_ID",
                                                       "Date",
                                                       "Shift_Id",
                                                       },
                                        new string[]{"1",
                                                      ViewState["MilkProductionProcess_Id"].ToString(),
                                                      objdb.Office_ID(),
                                                      ddlPSection.SelectedValue,
                                                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                      ddlShift.SelectedValue,
                                                      },
                                        new string[] { "type_Production_Milk_InProcess_Opening" ,
                                                         "type_Production_Milk_InProcess_Process", 
                                                         "type_Production_Milk_InProcess_Return",
                                                         "type_Production_Milk_InProcess_CCWiseProcurement",
                                                         "type_Production_Milk_OutProcess_Particular",
                                                         "type_Production_Milk_OutProcess_MIP",
                                                         "type_Production_Milk_OutProcess_IssuetoMDP_CC",
                                                         "type_Production_Milk_OutProcess_ClosingBal",
                                                         "type_Production_Milk_OutProcess_CRB",
                                                         "type_Production_Milk_OutProcess_IssuetoOther",
                                                         "type_Production_Milk_InProcess_BMCDCSColl",
                                                         "type_Production_Milk_InProcess_InflowOther",
                                                          "type_Production_Milk_InProcess_RecvdfromotherUnion",
                                                          "type_Production_Milk_InProcess_ForPowderConversion",
                                                          "type_Production_Milk_InProcess_IssueforPowderPlant",
                                                          "type_Production_Milk_InProcess_IssueforCream",
                                                          "type_Production_Milk_InProcess_OutflowOther",
                                                          "type_Production_Milk_InProcess_Other",
                                                          "type_Production_Milk_InOutProcessRRSheet",
                                                          "type_Production_Milk_InProcess_CanesCollection",
                                                          "type_Production_Milk_InProcess_IssueforIceCream",
                                                          "type_Production_Milk_InProcess_CCWiseGoatMilkProcurement",
                                                          //"type_Production_Milk_InProcess_Issueofgoatmilk"
                                                     },
                                        new DataTable[] { dtOpening, 
                                                            dtProcess, 
                                                            dtReturn,
                                                            dtCCWP, 
                                                            dtParticulars, 
                                                            dtMIP,
                                                            dtIssued, 
                                                            dtCB, 
                                                            dtCRB,
                                                            dtIssuedOtherParty,
                                                            dtBMCDCSColl,
                                                            dtInFlowOthers,
                                                            dtrcvdfromothrunion,
                                                            dtforpowdrconv,
                                                            dtIssuetoPP,
                                                            dtIssuetoCream,
                                                            dtIssuetoothers,
                                                            dtOthers,
                                                            dtIDF,
                                                            dtCanesColl,
                                                            dtIssuetoIceCream,
                                                            dtCCWGMP,
                                                            //dtIssueofgoatmilk
                                          },
                                                          "TableSave");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        txtDate_TextChanged(sender, e);

                    }

                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);

                    }

                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());


                    }
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetTotal()
    {
        decimal OpeningTQtyLtr = 0;
        decimal OpeningTQtyKg = 0;
        decimal OpeningTFatKg = 0;
        decimal OpeningTSnfKg = 0;
        foreach (GridViewRow rows in gvOpening.Rows)
        {
            TextBox txtOpeningQuantityInLtr = (TextBox)rows.FindControl("txtOpeningQuantityInLtr");
            TextBox txtOpeningQuantityInKg = (TextBox)rows.FindControl("txtOpeningQuantityInKg");
            TextBox txtOpeningFATInKg = (TextBox)rows.FindControl("txtOpeningFATInKg");
            TextBox txtOpeningSNFInKg = (TextBox)rows.FindControl("txtOpeningSNFInKg");
            if (txtOpeningQuantityInLtr.Text != "")
            {
                OpeningTQtyLtr += decimal.Parse(txtOpeningQuantityInLtr.Text);
            }
            if (txtOpeningQuantityInKg.Text != "")
            {
                OpeningTQtyKg += decimal.Parse(txtOpeningQuantityInKg.Text);
            }
            if (txtOpeningFATInKg.Text != "")
            {
                OpeningTFatKg += decimal.Parse(txtOpeningFATInKg.Text);
            }
            if (txtOpeningSNFInKg.Text != "")
            {
                OpeningTSnfKg += decimal.Parse(txtOpeningSNFInKg.Text);
            }
        }

        gvOpening.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvOpening.FooterRow.Cells[2].Text = "<b>" + OpeningTQtyLtr.ToString() + "</b>";
        gvOpening.FooterRow.Cells[1].Text = "<b>" + OpeningTQtyKg.ToString() + "</b>";
        gvOpening.FooterRow.Cells[5].Text = "<b>" + OpeningTFatKg.ToString() + "</b>";
        gvOpening.FooterRow.Cells[6].Text = "<b>" + OpeningTSnfKg.ToString() + "</b>";


        decimal ProcessLooseTQtyLtr = 0;
        decimal ProcessLooseTQtyKg = 0;
        decimal ProcessPacketTQtyLtr = 0;
        decimal ProcessPacketTQtyKg = 0;
        decimal ProcessTFatKg = 0;
        decimal ProcessTSnfKg = 0;
        foreach (GridViewRow rows in gvProcess.Rows)
        {

            TextBox txtProcessPackedInLtr = (TextBox)rows.FindControl("txtProcessPackedInLtr");
            TextBox txtProcessPackedInKg = (TextBox)rows.FindControl("txtProcessPackedInKg");
            TextBox txtProcessFATInKg = (TextBox)rows.FindControl("txtProcessFATInKg");
            TextBox txtProcessSNFInKg = (TextBox)rows.FindControl("txtProcessSNFInKg");

            if (txtProcessPackedInLtr.Text != "")
            {
                ProcessPacketTQtyLtr += decimal.Parse(txtProcessPackedInLtr.Text);
            }
            if (txtProcessPackedInKg.Text != "")
            {
                ProcessPacketTQtyKg += decimal.Parse(txtProcessPackedInKg.Text);
            }
            if (txtProcessFATInKg.Text != "")
            {
                ProcessTFatKg += decimal.Parse(txtProcessFATInKg.Text);
            }
            if (txtProcessSNFInKg.Text != "")
            {
                ProcessTSnfKg += decimal.Parse(txtProcessSNFInKg.Text);
            }
        }
        if (gvProcess.Rows.Count > 0)
        {
            gvProcess.FooterRow.Cells[0].Text = "<b>Total : </b>";
            gvProcess.FooterRow.Cells[3].Text = "<b>" + ProcessPacketTQtyKg.ToString() + "</b>";
            gvProcess.FooterRow.Cells[2].Text = "<b>" + ProcessPacketTQtyLtr.ToString() + "</b>";

            gvProcess.FooterRow.Cells[6].Text = "<b>" + ProcessTFatKg.ToString() + "</b>";
            gvProcess.FooterRow.Cells[7].Text = "<b>" + ProcessTSnfKg.ToString() + "</b>";

        }

        decimal ReturnPacketTQtyLtr = 0;
        decimal ReturnPacketTQtyKg = 0;
        decimal ReturnTFatKg = 0;
        decimal ReturnTSnfKg = 0;
        foreach (GridViewRow rows in gvReturn.Rows)
        {

            TextBox txtReturnPackedInLtr = (TextBox)rows.FindControl("txtReturnPackedInLtr");
            TextBox txtReturnPackedInKg = (TextBox)rows.FindControl("txtReturnPackedInKg");
            TextBox txtReturnFatInKg = (TextBox)rows.FindControl("txtReturnFatInKg");
            TextBox txtReturnSnfInKg = (TextBox)rows.FindControl("txtReturnSnfInKg");

            if (txtReturnPackedInLtr.Text != "")
            {
                ReturnPacketTQtyLtr += decimal.Parse(txtReturnPackedInLtr.Text);
            }
            if (txtReturnPackedInKg.Text != "")
            {
                ReturnPacketTQtyKg += decimal.Parse(txtReturnPackedInKg.Text);
            }
            if (txtReturnFatInKg.Text != "")
            {
                ReturnTFatKg += decimal.Parse(txtReturnFatInKg.Text);
            }
            if (txtReturnSnfInKg.Text != "")
            {
                ReturnTSnfKg += decimal.Parse(txtReturnSnfInKg.Text);
            }
        }
        if (gvReturn.Rows.Count > 0)
        {
            gvReturn.FooterRow.Cells[0].Text = "<b>Total : </b>";

            gvReturn.FooterRow.Cells[1].Text = "<b>" + ReturnPacketTQtyKg.ToString() + "</b>";
            gvReturn.FooterRow.Cells[2].Text = "<b>" + ReturnPacketTQtyLtr.ToString() + "</b>";
            gvReturn.FooterRow.Cells[6].Text = "<b>" + ReturnTFatKg.ToString() + "</b>";
            gvReturn.FooterRow.Cells[7].Text = "<b>" + ReturnTSnfKg.ToString() + "</b>";
        }



        decimal CCWiseTQtyLtr = 0;
        decimal CCWiseTQtyKg = 0;
        decimal CCWiseTFatKg = 0;
        decimal CCWiseTSnfKg = 0;
        foreach (GridViewRow rows in gvCCWiseProcurement.Rows)
        {
            TextBox txtCCQuantityInLtr = (TextBox)rows.FindControl("txtCCQuantityInLtr");
            TextBox txtCCQuantityInKg = (TextBox)rows.FindControl("txtCCQuantityInKg");
            TextBox txtCCFATInKg = (TextBox)rows.FindControl("txtCCFATInKg");
            TextBox txtCCSnfInKg = (TextBox)rows.FindControl("txtCCSnfInKg");
            if (txtCCQuantityInLtr.Text != "")
            {
                CCWiseTQtyLtr += decimal.Parse(txtCCQuantityInLtr.Text);
            }
            if (txtCCQuantityInKg.Text != "")
            {
                CCWiseTQtyKg += decimal.Parse(txtCCQuantityInKg.Text);
            }
            if (txtCCFATInKg.Text != "")
            {
                CCWiseTFatKg += decimal.Parse(txtCCFATInKg.Text);
            }
            if (txtCCSnfInKg.Text != "")
            {
                CCWiseTSnfKg += decimal.Parse(txtCCSnfInKg.Text);
            }
        }

        gvCCWiseProcurement.FooterRow.Cells[3].Text = "<b>Total : </b>";
        gvCCWiseProcurement.FooterRow.Cells[4].Text = "<b>" + CCWiseTQtyKg.ToString() + "</b>";
        gvCCWiseProcurement.FooterRow.Cells[5].Text = "<b>" + CCWiseTQtyLtr.ToString() + "</b>";

        gvCCWiseProcurement.FooterRow.Cells[8].Text = "<b>" + CCWiseTFatKg.ToString() + "</b>";
        gvCCWiseProcurement.FooterRow.Cells[9].Text = "<b>" + CCWiseTSnfKg.ToString() + "</b>";


        decimal CCWiseGoatTQtyLtr = 0;
        decimal CCWiseGoatTQtyKg = 0;
        decimal CCWiseGoatTFatKg = 0;
        decimal CCWiseGoatTSnfKg = 0;
        foreach (GridViewRow rows in gvCCWiseGoatMilkProcurement.Rows)
        {
            TextBox txtCCGoatQuantityInLtr = (TextBox)rows.FindControl("txtCCGoatMilkQuantityInLtr");
            TextBox txtCCGoatQuantityInKg = (TextBox)rows.FindControl("txtCCGoatMilkQuantityInKg");
            TextBox txtCCGoatFATInKg = (TextBox)rows.FindControl("txtCCGoatMilkFATInKg");
            TextBox txtCCGoatSnfInKg = (TextBox)rows.FindControl("txtCCGoatMilkSnfInKg");
            if (txtCCGoatQuantityInLtr.Text != "")
            {
                CCWiseGoatTQtyLtr += decimal.Parse(txtCCGoatQuantityInLtr.Text);
            }
            if (txtCCGoatQuantityInKg.Text != "")
            {
                CCWiseGoatTQtyKg += decimal.Parse(txtCCGoatQuantityInKg.Text);
            }
            if (txtCCGoatFATInKg.Text != "")
            {
                CCWiseGoatTFatKg += decimal.Parse(txtCCGoatFATInKg.Text);
            }
            if (txtCCGoatSnfInKg.Text != "")
            {
                CCWiseGoatTSnfKg += decimal.Parse(txtCCGoatSnfInKg.Text);
            }
        }

        gvCCWiseGoatMilkProcurement.FooterRow.Cells[3].Text = "<b>Total : </b>";
        gvCCWiseGoatMilkProcurement.FooterRow.Cells[4].Text = "<b>" + CCWiseGoatTQtyKg.ToString() + "</b>";
        gvCCWiseGoatMilkProcurement.FooterRow.Cells[5].Text = "<b>" + CCWiseGoatTQtyLtr.ToString() + "</b>";

        gvCCWiseGoatMilkProcurement.FooterRow.Cells[8].Text = "<b>" + CCWiseGoatTFatKg.ToString() + "</b>";
        gvCCWiseGoatMilkProcurement.FooterRow.Cells[9].Text = "<b>" + CCWiseGoatTSnfKg.ToString() + "</b>";

        decimal RcvdFromothrUnionTQtyLtr = 0;
        decimal RcvdFromothrUnionTQtyKg = 0;
        decimal RcvdFromothrUnionTFatKg = 0;
        decimal RcvdFromothrUnionTSnfKg = 0;
        foreach (GridViewRow rows in gvrcvdfrmothrUnion.Rows)
        {
            TextBox txtrcvdfrmothrUnionQuantityInLtr = (TextBox)rows.FindControl("txtrcvdfrmothrUnionQuantityInLtr");
            TextBox txtrcvdfrmothrUnionQuantityInKg = (TextBox)rows.FindControl("txtrcvdfrmothrUnionQuantityInKg");
            TextBox txtrcvdfrmothrUnionFATInKg = (TextBox)rows.FindControl("txtrcvdfrmothrUnionFATInKg");
            TextBox txtrcvdfrmothrUnionSnfInKg = (TextBox)rows.FindControl("txtrcvdfrmothrUnionSnfInKg");
            if (txtrcvdfrmothrUnionQuantityInLtr.Text != "")
            {
                RcvdFromothrUnionTQtyLtr += decimal.Parse(txtrcvdfrmothrUnionQuantityInLtr.Text);
            }
            if (txtrcvdfrmothrUnionQuantityInKg.Text != "")
            {
                RcvdFromothrUnionTQtyKg += decimal.Parse(txtrcvdfrmothrUnionQuantityInKg.Text);
            }
            if (txtrcvdfrmothrUnionFATInKg.Text != "")
            {
                RcvdFromothrUnionTFatKg += decimal.Parse(txtrcvdfrmothrUnionFATInKg.Text);
            }
            if (txtrcvdfrmothrUnionSnfInKg.Text != "")
            {
                RcvdFromothrUnionTSnfKg += decimal.Parse(txtrcvdfrmothrUnionSnfInKg.Text);
            }
        }

        gvrcvdfrmothrUnion.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvrcvdfrmothrUnion.FooterRow.Cells[2].Text = "<b>" + RcvdFromothrUnionTQtyKg.ToString() + "</b>";
        gvrcvdfrmothrUnion.FooterRow.Cells[3].Text = "<b>" + RcvdFromothrUnionTQtyLtr.ToString() + "</b>";

        gvrcvdfrmothrUnion.FooterRow.Cells[6].Text = "<b>" + RcvdFromothrUnionTFatKg.ToString() + "</b>";
        gvrcvdfrmothrUnion.FooterRow.Cells[7].Text = "<b>" + RcvdFromothrUnionTSnfKg.ToString() + "</b>";

        decimal ForPowderConversionTQtyLtr = 0;
        decimal ForPowderConversionTQtyKg = 0;
        decimal ForPowderConversionTFatKg = 0;
        decimal ForPowderConversionTSnfKg = 0;
        foreach (GridViewRow rows in gvForPowderConversion.Rows)
        {
            TextBox txtForPowderConversionQuantityInLtr = (TextBox)rows.FindControl("txtForPowderConversionQuantityInLtr");
            TextBox txtForPowderConversionQuantityInKg = (TextBox)rows.FindControl("txtForPowderConversionQuantityInKg");
            TextBox txtForPowderConversionFATInKg = (TextBox)rows.FindControl("txtForPowderConversionFATInKg");
            TextBox txtForPowderConversionSnfInKg = (TextBox)rows.FindControl("txtForPowderConversionSnfInKg");
            if (txtForPowderConversionQuantityInLtr.Text != "")
            {
                ForPowderConversionTQtyLtr += decimal.Parse(txtForPowderConversionQuantityInLtr.Text);
            }
            if (txtForPowderConversionQuantityInKg.Text != "")
            {
                ForPowderConversionTQtyKg += decimal.Parse(txtForPowderConversionQuantityInKg.Text);
            }
            if (txtForPowderConversionFATInKg.Text != "")
            {
                ForPowderConversionTFatKg += decimal.Parse(txtForPowderConversionFATInKg.Text);
            }
            if (txtForPowderConversionSnfInKg.Text != "")
            {
                ForPowderConversionTSnfKg += decimal.Parse(txtForPowderConversionSnfInKg.Text);
            }
        }

        gvForPowderConversion.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvForPowderConversion.FooterRow.Cells[2].Text = "<b>" + ForPowderConversionTQtyKg.ToString() + "</b>";
        gvForPowderConversion.FooterRow.Cells[3].Text = "<b>" + ForPowderConversionTQtyLtr.ToString() + "</b>";

        gvForPowderConversion.FooterRow.Cells[6].Text = "<b>" + ForPowderConversionTFatKg.ToString() + "</b>";
        gvForPowderConversion.FooterRow.Cells[7].Text = "<b>" + ForPowderConversionTSnfKg.ToString() + "</b>";


        decimal BMCDCSCollTQtyLtr = 0;
        decimal BMCDCSCollTQtyKg = 0;
        decimal BMCDCSCollTFatKg = 0;
        decimal BMCDCSCollTSnfKg = 0;
        foreach (GridViewRow rows in gvBMCDCSCollection.Rows)
        {

            TextBox txtBMCDCSCollectionQtyInKg = (TextBox)rows.FindControl("txtBMCDCSCollectionQtyInKg");
            TextBox txtBMCDCSCollectionFatInKg = (TextBox)rows.FindControl("txtBMCDCSCollectionFatInKg");
            TextBox txtBMCDCSCollectionSnfInKg = (TextBox)rows.FindControl("txtBMCDCSCollectionSnfInKg");

            if (txtBMCDCSCollectionQtyInKg.Text != "")
            {
                BMCDCSCollTQtyKg += decimal.Parse(txtBMCDCSCollectionQtyInKg.Text);
            }
            if (txtBMCDCSCollectionFatInKg.Text != "")
            {
                BMCDCSCollTFatKg += decimal.Parse(txtBMCDCSCollectionFatInKg.Text);
            }
            if (txtBMCDCSCollectionSnfInKg.Text != "")
            {
                BMCDCSCollTSnfKg += decimal.Parse(txtBMCDCSCollectionSnfInKg.Text);
            }
        }
        gvBMCDCSCollection.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvBMCDCSCollection.FooterRow.Cells[3].Text = "<b>" + BMCDCSCollTQtyKg.ToString() + "</b>";


        gvBMCDCSCollection.FooterRow.Cells[6].Text = "<b>" + BMCDCSCollTFatKg.ToString() + "</b>";
        gvBMCDCSCollection.FooterRow.Cells[7].Text = "<b>" + BMCDCSCollTSnfKg.ToString() + "</b>";


        decimal InFlowOtherTQtyLtr = 0;
        decimal InFlowOtherTQtyKg = 0;
        decimal InFlowOtherTFatKg = 0;
        decimal InFlowOtherTSnfKg = 0;
        foreach (GridViewRow rows in gvinflowother.Rows)
        {

            TextBox txtinflowotherQtyInKg = (TextBox)rows.FindControl("txtinflowotherQtyInKg");
            TextBox txtinflowotherFatInKg = (TextBox)rows.FindControl("txtinflowotherFatInKg");
            TextBox txtinflowotherSnfInKg = (TextBox)rows.FindControl("txtinflowotherSnfInKg");

            if (txtinflowotherQtyInKg.Text != "")
            {
                InFlowOtherTQtyKg += decimal.Parse(txtinflowotherQtyInKg.Text);
            }
            if (txtinflowotherFatInKg.Text != "")
            {
                InFlowOtherTFatKg += decimal.Parse(txtinflowotherFatInKg.Text);
            }
            if (txtinflowotherSnfInKg.Text != "")
            {
                InFlowOtherTSnfKg += decimal.Parse(txtinflowotherSnfInKg.Text);
            }
        }
        if (gvinflowother.Rows.Count > 0)
        {
            gvinflowother.FooterRow.Cells[0].Text = "<b>Total : </b>";

            gvinflowother.FooterRow.Cells[1].Text = "<b>" + InFlowOtherTQtyKg.ToString() + "</b>";
            gvinflowother.FooterRow.Cells[4].Text = "<b>" + InFlowOtherTFatKg.ToString() + "</b>";
            gvinflowother.FooterRow.Cells[5].Text = "<b>" + InFlowOtherTSnfKg.ToString() + "</b>";
        }

        decimal CanesCollTQtyLtr = 0;
        decimal CanesCollTQtyKg = 0;
        decimal CanesCollTFatKg = 0;
        decimal CanesCollTSnfKg = 0;
        foreach (GridViewRow rows in gvCanesCollection.Rows)
        {

            TextBox txtCanesCollectionQtyInKg = (TextBox)rows.FindControl("txtCanesCollectionQtyInKg");
            TextBox txtCanesCollectionFatInKg = (TextBox)rows.FindControl("txtCanesCollectionFatInKg");
            TextBox txtCanesCollectionSnfInKg = (TextBox)rows.FindControl("txtCanesCollectionSnfInKg");

            if (txtCanesCollectionQtyInKg.Text != "")
            {
                CanesCollTQtyKg += decimal.Parse(txtCanesCollectionQtyInKg.Text);
            }
            if (txtCanesCollectionFatInKg.Text != "")
            {
                CanesCollTFatKg += decimal.Parse(txtCanesCollectionFatInKg.Text);
            }
            if (txtCanesCollectionSnfInKg.Text != "")
            {
                CanesCollTSnfKg += decimal.Parse(txtCanesCollectionSnfInKg.Text);
            }
        }
        if (gvCanesCollection.Rows.Count > 0)
        {
            gvCanesCollection.FooterRow.Cells[0].Text = "<b>Total : </b>";

            gvCanesCollection.FooterRow.Cells[2].Text = "<b>" + CanesCollTQtyKg.ToString() + "</b>";
            gvCanesCollection.FooterRow.Cells[5].Text = "<b>" + CanesCollTFatKg.ToString() + "</b>";
            gvCanesCollection.FooterRow.Cells[6].Text = "<b>" + CanesCollTSnfKg.ToString() + "</b>";
        }


        decimal ParticularPacketTQtyLtr = 0;
        decimal ParticularPacketTQtyKg = 0;
        decimal ParticularTFatKg = 0;
        decimal ParticularTSnfKg = 0;
        foreach (GridViewRow rows in gvPaticulars.Rows)
        {

            TextBox txtPaticularsQuantityInLtr = (TextBox)rows.FindControl("txtPaticularsQuantityInLtr");
            TextBox txtPaticularsQuantityInKg = (TextBox)rows.FindControl("txtPaticularsQuantityInKg");
            TextBox txtParticularFATInKg = (TextBox)rows.FindControl("txtParticularFATInKg");
            TextBox txtParticularSnfInKg = (TextBox)rows.FindControl("txtParticularSnfInKg");

            if (txtPaticularsQuantityInLtr.Text != "")
            {
                ParticularPacketTQtyLtr += decimal.Parse(txtPaticularsQuantityInLtr.Text);
            }
            if (txtPaticularsQuantityInKg.Text != "")
            {
                ParticularPacketTQtyKg += decimal.Parse(txtPaticularsQuantityInKg.Text);
            }
            if (txtParticularFATInKg.Text != "")
            {
                ParticularTFatKg += decimal.Parse(txtParticularFATInKg.Text);
            }
            if (txtParticularSnfInKg.Text != "")
            {
                ParticularTSnfKg += decimal.Parse(txtParticularSnfInKg.Text);
            }
        }
        if (gvPaticulars.Rows.Count > 0)
        {
            gvPaticulars.FooterRow.Cells[0].Text = "<b>Total : </b>";
            gvPaticulars.FooterRow.Cells[2].Text = "<b>" + ParticularPacketTQtyLtr.ToString() + "</b>";
            gvPaticulars.FooterRow.Cells[1].Text = "<b>" + ParticularPacketTQtyKg.ToString() + "</b>";
            gvPaticulars.FooterRow.Cells[6].Text = "<b>" + ParticularTFatKg.ToString() + "</b>";
            gvPaticulars.FooterRow.Cells[7].Text = "<b>" + ParticularTSnfKg.ToString() + "</b>";
        }


        decimal MIPPacketTQtyLtr = 0;
        decimal MIPPacketTQtyKg = 0;
        decimal MIPTFatKg = 0;
        decimal MIPTSnfKg = 0;
        foreach (GridViewRow rows in gvMilkToIP.Rows)
        {

            TextBox txtMilkToIPQuantityInLtr = (TextBox)rows.FindControl("txtMilkToIPQuantityInLtr");
            TextBox txtMilkToIPQuantityInKg = (TextBox)rows.FindControl("txtMilkToIPQuantityInKg");
            TextBox txtMilkToIPFATInKg = (TextBox)rows.FindControl("txtMilkToIPFATInKg");
            TextBox txtMilkToIPSnfInKg = (TextBox)rows.FindControl("txtMilkToIPSnfInKg");

            if (txtMilkToIPQuantityInLtr.Text != "")
            {
                MIPPacketTQtyLtr += decimal.Parse(txtMilkToIPQuantityInLtr.Text);
            }
            if (txtMilkToIPQuantityInKg.Text != "")
            {
                MIPPacketTQtyKg += decimal.Parse(txtMilkToIPQuantityInKg.Text);
            }
            if (txtMilkToIPFATInKg.Text != "")
            {
                MIPTFatKg += decimal.Parse(txtMilkToIPFATInKg.Text);
            }
            if (txtMilkToIPSnfInKg.Text != "")
            {
                MIPTSnfKg += decimal.Parse(txtMilkToIPSnfInKg.Text);
            }
        }

        gvMilkToIP.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvMilkToIP.FooterRow.Cells[3].Text = "<b>" + MIPPacketTQtyLtr.ToString() + "</b>";
        gvMilkToIP.FooterRow.Cells[2].Text = "<b>" + MIPPacketTQtyKg.ToString() + "</b>";
        gvMilkToIP.FooterRow.Cells[6].Text = "<b>" + MIPTFatKg.ToString() + "</b>";
        gvMilkToIP.FooterRow.Cells[7].Text = "<b>" + MIPTSnfKg.ToString() + "</b>";


        decimal MDPTQtyLtr = 0;
        decimal MDPTQtyKg = 0;
        decimal MDPTFatKg = 0;
        decimal MDPTSnfKg = 0;
        foreach (GridViewRow rows in gvIssuetoMDPOrCC.Rows)
        {
            TextBox txtIssuetoMDPOrCCQuantityInLtr = (TextBox)rows.FindControl("txtIssuetoMDPOrCCQuantityInLtr");
            TextBox txtIssuetoMDPOrCCQuantityInKg = (TextBox)rows.FindControl("txtIssuetoMDPOrCCQuantityInKg");
            TextBox txtIssuetoMDPOrCCFATInKg = (TextBox)rows.FindControl("txtIssuetoMDPOrCCFATInKg");
            TextBox txtIssuetoMDPOrCCSnfInKg = (TextBox)rows.FindControl("txtIssuetoMDPOrCCSnfInKg");
            if (txtIssuetoMDPOrCCQuantityInLtr.Text != "")
            {
                MDPTQtyLtr += decimal.Parse(txtIssuetoMDPOrCCQuantityInLtr.Text);
            }
            if (txtIssuetoMDPOrCCQuantityInKg.Text != "")
            {
                MDPTQtyKg += decimal.Parse(txtIssuetoMDPOrCCQuantityInKg.Text);
            }
            if (txtIssuetoMDPOrCCFATInKg.Text != "")
            {
                MDPTFatKg += decimal.Parse(txtIssuetoMDPOrCCFATInKg.Text);
            }
            if (txtIssuetoMDPOrCCSnfInKg.Text != "")
            {
                MDPTSnfKg += decimal.Parse(txtIssuetoMDPOrCCSnfInKg.Text);
            }
        }

        gvIssuetoMDPOrCC.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvIssuetoMDPOrCC.FooterRow.Cells[5].Text = "<b>" + MDPTQtyLtr.ToString() + "</b>";
        gvIssuetoMDPOrCC.FooterRow.Cells[4].Text = "<b>" + MDPTQtyKg.ToString() + "</b>";
        gvIssuetoMDPOrCC.FooterRow.Cells[8].Text = "<b>" + MDPTFatKg.ToString() + "</b>";
        gvIssuetoMDPOrCC.FooterRow.Cells[9].Text = "<b>" + MDPTSnfKg.ToString() + "</b>";

        decimal IsuuetoOtherPartyQtyLtr = 0;
        decimal IsuuetoOtherPartyQtyKg = 0;
        decimal IsuuetoOtherPartyFatKg = 0;
        decimal IsuuetoOtherPartySnfKg = 0;
        foreach (GridViewRow rows in gvIsuuetoOtherParty.Rows)
        {
            TextBox txtIsuuetoOtherPartyQuantityInLtr = (TextBox)rows.FindControl("txtIsuuetoOtherPartyQuantityInLtr");
            TextBox txtIsuuetoOtherPartyQuantityInKg = (TextBox)rows.FindControl("txtIsuuetoOtherPartyQuantityInKg");
            TextBox txtIsuuetoOtherPartyFATInKg = (TextBox)rows.FindControl("txtIsuuetoOtherPartyFATInKg");
            TextBox txtIsuuetoOtherPartySnfInKg = (TextBox)rows.FindControl("txtIsuuetoOtherPartySnfInKg");
            if (txtIsuuetoOtherPartyQuantityInLtr.Text != "")
            {
                IsuuetoOtherPartyQtyLtr += decimal.Parse(txtIsuuetoOtherPartyQuantityInLtr.Text);
            }
            if (txtIsuuetoOtherPartyQuantityInKg.Text != "")
            {
                IsuuetoOtherPartyQtyKg += decimal.Parse(txtIsuuetoOtherPartyQuantityInKg.Text);
            }
            if (txtIsuuetoOtherPartyFATInKg.Text != "")
            {
                IsuuetoOtherPartyFatKg += decimal.Parse(txtIsuuetoOtherPartyFATInKg.Text);
            }
            if (txtIsuuetoOtherPartySnfInKg.Text != "")
            {
                IsuuetoOtherPartySnfKg += decimal.Parse(txtIsuuetoOtherPartySnfInKg.Text);
            }
        }

        gvIsuuetoOtherParty.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvIsuuetoOtherParty.FooterRow.Cells[5].Text = "<b>" + IsuuetoOtherPartyQtyLtr.ToString() + "</b>";
        gvIsuuetoOtherParty.FooterRow.Cells[4].Text = "<b>" + IsuuetoOtherPartyQtyKg.ToString() + "</b>";
        gvIsuuetoOtherParty.FooterRow.Cells[8].Text = "<b>" + IsuuetoOtherPartyFatKg.ToString() + "</b>";
        gvIsuuetoOtherParty.FooterRow.Cells[9].Text = "<b>" + IsuuetoOtherPartySnfKg.ToString() + "</b>";


        decimal IsuueforPPQtyLtr = 0;
        decimal IsuueforPPQtyKg = 0;
        decimal IsuueforPPFatKg = 0;
        decimal IsuueforPPSnfKg = 0;
        foreach (GridViewRow rows in GVIssuetoPowderPlant.Rows)
        {
            TextBox txtIssuetoPowderPlantQuantityInLtr = (TextBox)rows.FindControl("txtIssuetoPowderPlantQtyInLtr");
            TextBox txtIssuetoPowderPlantQuantityInKg = (TextBox)rows.FindControl("txtIssuetoPowderPlantQtyInKg");
            TextBox txtIssuetoPowderPlantFATInKg = (TextBox)rows.FindControl("txtIssuetoPowderPlantFATInKg");
            TextBox txtIssuetoPowderPlantSnfInKg = (TextBox)rows.FindControl("txtIssuetoPowderPlantSnfInKg");
            if (txtIssuetoPowderPlantQuantityInLtr.Text != "")
            {
                IsuueforPPQtyLtr += decimal.Parse(txtIssuetoPowderPlantQuantityInLtr.Text);
            }
            if (txtIssuetoPowderPlantQuantityInKg.Text != "")
            {
                IsuueforPPQtyKg += decimal.Parse(txtIssuetoPowderPlantQuantityInKg.Text);
            }
            if (txtIssuetoPowderPlantFATInKg.Text != "")
            {
                IsuueforPPFatKg += decimal.Parse(txtIssuetoPowderPlantFATInKg.Text);
            }
            if (txtIssuetoPowderPlantSnfInKg.Text != "")
            {
                IsuueforPPSnfKg += decimal.Parse(txtIssuetoPowderPlantSnfInKg.Text);
            }
        }

        GVIssuetoPowderPlant.FooterRow.Cells[1].Text = "<b>Total : </b>";
        GVIssuetoPowderPlant.FooterRow.Cells[3].Text = "<b>" + IsuueforPPQtyLtr.ToString() + "</b>";
        GVIssuetoPowderPlant.FooterRow.Cells[2].Text = "<b>" + IsuueforPPQtyKg.ToString() + "</b>";
        GVIssuetoPowderPlant.FooterRow.Cells[6].Text = "<b>" + IsuueforPPFatKg.ToString() + "</b>";
        GVIssuetoPowderPlant.FooterRow.Cells[7].Text = "<b>" + IsuueforPPSnfKg.ToString() + "</b>";


        decimal IsuuetoCreamQtyLtr = 0;
        decimal IsuuetoCreamQtyKg = 0;
        decimal IsuuetoCreamFatKg = 0;
        decimal IsuuetoCreamSnfKg = 0;
        foreach (GridViewRow rows in gvIssuetoCream.Rows)
        {
            TextBox txtIssuetoCreamQtyInLtr = (TextBox)rows.FindControl("txtIssuetoCreamQtyInLtr");
            TextBox txtIssuetoCreamQtyInKg = (TextBox)rows.FindControl("txtIssuetoCreamQtyInKg");
            TextBox txtIssuetoCreamFatInKg = (TextBox)rows.FindControl("txtIssuetoCreamFatInKg");
            TextBox txtIssuetoCreamSnfInKg = (TextBox)rows.FindControl("txtIssuetoCreamSnfInKg");
            if (txtIssuetoCreamQtyInLtr.Text != "")
            {
                IsuuetoCreamQtyLtr += decimal.Parse(txtIssuetoCreamQtyInLtr.Text);
            }
            if (txtIssuetoCreamQtyInKg.Text != "")
            {
                IsuuetoCreamQtyKg += decimal.Parse(txtIssuetoCreamQtyInKg.Text);
            }
            if (txtIssuetoCreamFatInKg.Text != "")
            {
                IsuuetoCreamFatKg += decimal.Parse(txtIssuetoCreamFatInKg.Text);
            }
            if (txtIssuetoCreamSnfInKg.Text != "")
            {
                IsuuetoCreamSnfKg += decimal.Parse(txtIssuetoCreamSnfInKg.Text);
            }
        }

        gvIssuetoCream.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvIssuetoCream.FooterRow.Cells[2].Text = "<b>" + IsuuetoCreamQtyLtr.ToString() + "</b>";
        gvIssuetoCream.FooterRow.Cells[1].Text = "<b>" + IsuuetoCreamQtyKg.ToString() + "</b>";
        gvIssuetoCream.FooterRow.Cells[5].Text = "<b>" + IsuuetoCreamFatKg.ToString() + "</b>";
        gvIssuetoCream.FooterRow.Cells[6].Text = "<b>" + IsuuetoCreamSnfKg.ToString() + "</b>";

        decimal IsuuetoIceCreamQtyLtr = 0;
        decimal IsuuetoIceCreamQtyKg = 0;
        decimal IsuuetoIceCreamFatKg = 0;
        decimal IsuuetoIceCreamSnfKg = 0;
        foreach (GridViewRow rows in gvIssuetoIceCream.Rows)
        {
            TextBox txtIssuetoIceCreamQtyInLtr = (TextBox)rows.FindControl("txtIssuetoIceCreamQtyInLtr");
            TextBox txtIssuetoIceCreamQtyInKg = (TextBox)rows.FindControl("txtIssuetoIceCreamQtyInKg");
            TextBox txtIssuetoIceCreamFatInKg = (TextBox)rows.FindControl("txtIssuetoIceCreamFatInKg");
            TextBox txtIssuetoIceCreamSnfInKg = (TextBox)rows.FindControl("txtIssuetoIceCreamSnfInKg");
            if (txtIssuetoIceCreamQtyInLtr.Text != "")
            {
                IsuuetoIceCreamQtyLtr += decimal.Parse(txtIssuetoIceCreamQtyInLtr.Text);
            }
            if (txtIssuetoIceCreamQtyInKg.Text != "")
            {
                IsuuetoIceCreamQtyKg += decimal.Parse(txtIssuetoIceCreamQtyInKg.Text);
            }
            if (txtIssuetoIceCreamFatInKg.Text != "")
            {
                IsuuetoIceCreamFatKg += decimal.Parse(txtIssuetoIceCreamFatInKg.Text);
            }
            if (txtIssuetoIceCreamSnfInKg.Text != "")
            {
                IsuuetoIceCreamSnfKg += decimal.Parse(txtIssuetoIceCreamSnfInKg.Text);
            }
        }

        gvIssuetoIceCream.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvIssuetoIceCream.FooterRow.Cells[3].Text = "<b>" + IsuuetoIceCreamQtyLtr.ToString() + "</b>";
        gvIssuetoIceCream.FooterRow.Cells[2].Text = "<b>" + IsuuetoIceCreamQtyKg.ToString() + "</b>";
        gvIssuetoIceCream.FooterRow.Cells[6].Text = "<b>" + IsuuetoIceCreamFatKg.ToString() + "</b>";
        gvIssuetoIceCream.FooterRow.Cells[7].Text = "<b>" + IsuuetoIceCreamSnfKg.ToString() + "</b>";



        decimal IsuuetoOtherQtyLtr = 0;
        decimal IsuuetoOtherQtyKg = 0;
        decimal IsuuetoOtherFatKg = 0;
        decimal IsuuetoOtherSnfKg = 0;
        foreach (GridViewRow rows in gvIssuetoother.Rows)
        {
            TextBox txtIssuetootherQtyInKg = (TextBox)rows.FindControl("txtIssuetootherQtyInKg");
            TextBox txtIssuetootherQtyInLtr = (TextBox)rows.FindControl("txtIssuetootherQtyInLtr");
            TextBox txtIssuetootherFatInKg = (TextBox)rows.FindControl("txtIssuetootherFatInKg");
            TextBox txtIssuetootherSnfInKg = (TextBox)rows.FindControl("txtIssuetootherSnfInKg");
            if (txtIssuetootherQtyInLtr.Text != "")
            {
                IsuuetoOtherQtyLtr += decimal.Parse(txtIssuetootherQtyInLtr.Text);
            }
            if (txtIssuetootherQtyInKg.Text != "")
            {
                IsuuetoOtherQtyKg += decimal.Parse(txtIssuetootherQtyInKg.Text);
            }
            if (txtIssuetootherFatInKg.Text != "")
            {
                IsuuetoOtherFatKg += decimal.Parse(txtIssuetootherFatInKg.Text);
            }
            if (txtIssuetootherSnfInKg.Text != "")
            {
                IsuuetoOtherSnfKg += decimal.Parse(txtIssuetootherSnfInKg.Text);
            }
        }

        gvIssuetoother.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvIssuetoother.FooterRow.Cells[2].Text = "<b>" + IsuuetoOtherQtyLtr.ToString() + "</b>";
        gvIssuetoother.FooterRow.Cells[1].Text = "<b>" + IsuuetoOtherQtyKg.ToString() + "</b>";
        gvIssuetoother.FooterRow.Cells[5].Text = "<b>" + IsuuetoOtherFatKg.ToString() + "</b>";
        gvIssuetoother.FooterRow.Cells[6].Text = "<b>" + IsuuetoOtherSnfKg.ToString() + "</b>";



        decimal OtherQtyLtr = 0;
        decimal OtherQtyKg = 0;
        decimal OtherFatKg = 0;
        decimal OtherSnfKg = 0;
        foreach (GridViewRow rows in gvOther.Rows)
        {
            TextBox txtotherQtyInKg = (TextBox)rows.FindControl("txtotherQtyInKg");
            TextBox txtotherQtyInLtr = (TextBox)rows.FindControl("txtotherQtyInLtr");
            TextBox txtotherFatInKg = (TextBox)rows.FindControl("txtotherFatInKg");
            TextBox txtotherSnfInKg = (TextBox)rows.FindControl("txtotherSnfInKg");
            if (txtotherQtyInKg.Text != "")
            {
                OtherQtyKg += decimal.Parse(txtotherQtyInKg.Text);
            }
            if (txtotherQtyInLtr.Text != "")
            {
                OtherQtyLtr += decimal.Parse(txtotherQtyInLtr.Text);
            }
            if (txtotherFatInKg.Text != "")
            {
                OtherFatKg += decimal.Parse(txtotherFatInKg.Text);
            }
            if (txtotherSnfInKg.Text != "")
            {
                OtherSnfKg += decimal.Parse(txtotherSnfInKg.Text);
            }
        }

        gvOther.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvOther.FooterRow.Cells[2].Text = "<b>" + OtherQtyLtr.ToString() + "</b>";
        gvOther.FooterRow.Cells[1].Text = "<b>" + OtherQtyKg.ToString() + "</b>";
        gvOther.FooterRow.Cells[5].Text = "<b>" + OtherFatKg.ToString() + "</b>";
        gvOther.FooterRow.Cells[6].Text = "<b>" + OtherSnfKg.ToString() + "</b>";


        decimal CBQtyLtr = 0;
        decimal CBQtyKg = 0;
        decimal CBFatKg = 0;
        decimal CBSnfKg = 0;
        foreach (GridViewRow rows in gvClosingBalances.Rows)
        {
            TextBox txtClosingBalancesQuantityInLtr = (TextBox)rows.FindControl("txtClosingBalancesQuantityInLtr");
            TextBox txtClosingBalancesQuantityInKg = (TextBox)rows.FindControl("txtClosingBalancesQuantityInKg");
            TextBox txtClosingBalancesFATInKg = (TextBox)rows.FindControl("txtClosingBalancesFATInKg");
            TextBox txtClosingBalancesSnfInKg = (TextBox)rows.FindControl("txtClosingBalancesSnfInKg");
            if (txtClosingBalancesQuantityInLtr.Text != "")
            {
                CBQtyLtr += decimal.Parse(txtClosingBalancesQuantityInLtr.Text);
            }
            if (txtClosingBalancesQuantityInKg.Text != "")
            {
                CBQtyKg += decimal.Parse(txtClosingBalancesQuantityInKg.Text);
            }
            if (txtClosingBalancesFATInKg.Text != "")
            {
                CBFatKg += decimal.Parse(txtClosingBalancesFATInKg.Text);
            }
            if (txtClosingBalancesSnfInKg.Text != "")
            {
                CBSnfKg += decimal.Parse(txtClosingBalancesSnfInKg.Text);
            }
        }

        gvClosingBalances.FooterRow.Cells[0].Text = "<b>Total : </b>";
        gvClosingBalances.FooterRow.Cells[2].Text = "<b>" + CBQtyLtr.ToString() + "</b>";
        gvClosingBalances.FooterRow.Cells[1].Text = "<b>" + CBQtyKg.ToString() + "</b>";
        gvClosingBalances.FooterRow.Cells[5].Text = "<b>" + CBFatKg.ToString() + "</b>";
        gvClosingBalances.FooterRow.Cells[6].Text = "<b>" + CBSnfKg.ToString() + "</b>";


        decimal CRBQtyLtr = 0;
        decimal CRBQtyKg = 0;
        decimal CRBFatKg = 0;
        decimal CRBSnfKg = 0;
        foreach (GridViewRow rows in gvColdRoomBalances.Rows)
        {
            TextBox txtColdRoomBalancesQuantityInLtr = (TextBox)rows.FindControl("txtColdRoomBalancesQuantityInLtr");
            TextBox txtColdRoomBalancesQuantityInKg = (TextBox)rows.FindControl("txtColdRoomBalancesQuantityInKg");
            TextBox txtColdRoomBalancesFATInKg = (TextBox)rows.FindControl("txtColdRoomBalancesFATInKg");
            TextBox txtColdRoomBalancesSnfInKg = (TextBox)rows.FindControl("txtColdRoomBalancesSnfInKg");
            if (txtColdRoomBalancesQuantityInLtr.Text != "")
            {
                CRBQtyLtr += decimal.Parse(txtColdRoomBalancesQuantityInLtr.Text);
            }
            if (txtColdRoomBalancesQuantityInKg.Text != "")
            {
                CRBQtyKg += decimal.Parse(txtColdRoomBalancesQuantityInKg.Text);
            }
            if (txtColdRoomBalancesFATInKg.Text != "")
            {
                CRBFatKg += decimal.Parse(txtColdRoomBalancesFATInKg.Text);
            }
            if (txtColdRoomBalancesSnfInKg.Text != "")
            {
                CRBSnfKg += decimal.Parse(txtColdRoomBalancesSnfInKg.Text);
            }
        }
        foreach (GridViewRow row in GVRRSheet.Rows)
        {
            Label lblItemTypeName = (Label)row.FindControl("lblItemTypeName");
            Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
            Label lblItemType_id = (Label)row.FindControl("lblItemType_id");

            TextBox txtBalance_BFRR = (TextBox)row.FindControl("txtBalance_BFRR");
            TextBox txtRRObtained = (TextBox)row.FindControl("txtRRObtained");
            TextBox txtRRTotal = (TextBox)row.FindControl("txtRRTotal");

            TextBox txtRRToning = (TextBox)row.FindControl("txtRRToning");
            TextBox txtRRMaintainingSNF = (TextBox)row.FindControl("txtRRMaintainingSNF");
            TextBox txtRRIssueforproductionsection = (TextBox)row.FindControl("txtRRIssueforproductionsection");
            TextBox txtRRTotalIssued = (TextBox)row.FindControl("txtRRTotalIssued");

            TextBox txtRRClosingBalance = (TextBox)row.FindControl("txtRRClosingBalance");

            if (txtBalance_BFRR.Text == "")
            {
                txtBalance_BFRR.Text = "0";
            }
            if (txtRRObtained.Text == "")
            {
                txtRRObtained.Text = "0";
            }

            txtRRTotal.Text = (Convert.ToDecimal(txtBalance_BFRR.Text) + Convert.ToDecimal(txtRRObtained.Text)).ToString();


            if (txtRRToning.Text == "")
            {
                txtRRToning.Text = "0";
            }
            if (txtRRMaintainingSNF.Text == "")
            {
                txtRRMaintainingSNF.Text = "0";
            }
            if (txtRRIssueforproductionsection.Text == "")
            {
                txtRRIssueforproductionsection.Text = "0";
            }

            txtRRTotalIssued.Text = (Convert.ToDecimal(txtRRToning.Text) + Convert.ToDecimal(txtRRMaintainingSNF.Text) + Convert.ToDecimal(txtRRIssueforproductionsection.Text)).ToString();


            if (txtRRClosingBalance.Text == "")
            {
                txtRRClosingBalance.Text = "0";
            }

            txtRRClosingBalance.Text = (Convert.ToDecimal(txtRRTotal.Text) - Convert.ToDecimal(txtRRTotalIssued.Text)).ToString();
            txtRRClosingBalance.Enabled = false;

            if (txtRRTotalIssued.Text != "" || txtRRTotal.Text != "")
            {
                btnSave.Enabled = true;
            }
        }
        if (gvColdRoomBalances.Rows.Count > 0)
        {
            gvColdRoomBalances.FooterRow.Cells[0].Text = "<b>Total : </b>";
            gvColdRoomBalances.FooterRow.Cells[2].Text = "<b>" + CRBQtyLtr.ToString() + "</b>";
            gvColdRoomBalances.FooterRow.Cells[3].Text = "<b>" + CRBQtyKg.ToString() + "</b>";
            gvColdRoomBalances.FooterRow.Cells[6].Text = "<b>" + CRBFatKg.ToString() + "</b>";
            gvColdRoomBalances.FooterRow.Cells[7].Text = "<b>" + CRBSnfKg.ToString() + "</b>";
        }



    }
    protected void btnGetTotal_Click(object sender, EventArgs e)
    {
        GetTotal();
    }

    protected void btnViewDemand_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewDemand();", true);
    }
    #endregion
	[WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchBMCRootName(string BMCRootName)
    {
        List<string> customers = new List<string>();
        try
        {
            DataView dv = new DataView();
            dv = ds6.Tables[0].DefaultView;
            dv.RowFilter = "BMCTankerRootName like '%" + BMCRootName + "%'";
            DataTable dt = dv.ToTable();

            foreach (DataRow rs in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    customers.Add(rs[col].ToString());
                }
            }

        }
        catch { }
        return customers;
    }
    
}