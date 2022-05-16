using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
 
public partial class mis_MilkCollection_DCSMilkDispatch : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass obj_MC = new MilkCalculationClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Attributes.Add("readonly", "readonly");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                FillSociety(sender, e);
                MilkDispatchDetail();

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        lblMsg.Text = "";
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Milk Dispatch Process Successfully Completed');", true);
                    }
                }

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

    protected void ddlShift_Init(object sender, EventArgs e)
    {
        try
        {
            DataSet dsct = objdb.ByProcedure("USP_GetServerDatetime",
                             new string[] { "flag" },
                             new string[] { "1" }, "dataset");

            string currrentime = dsct.Tables[0].Rows[0]["currentTime"].ToString();

            string[] s = currrentime.Split(':');

            if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 12 && ((Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 12 && Convert.ToInt32(s[1]) <= 59))))
            {
                ddlShift.SelectedValue = "Morning";
            }
            else
            {
                ddlShift.SelectedValue = "Evening";
            }

            ddlShift.Enabled = false;


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void FillSociety(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtsocietyName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    txtBlock.Text = ds.Tables[0].Rows[0]["Block_Name"].ToString();
                    txtBlock.Enabled = false;

                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    ddlDCS.DataTextField = "Office_Name";
                    ddlDCS.DataValueField = "Office_ID";
                    ddlDCS.DataSource = ds.Tables[1];
                    ddlDCS.DataBind();

                }

                DataSet dsctfs = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                 new string[] { "flag", "EntryDate", "EntryShift", "OfficeId", "FilterDate", "Item_id" },
                 new string[] { "6", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd"), ddlShift.SelectedValue, objdb.Office_ID(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd"), objdb.DcsRawMilkItemId_ID() }, "dataset");

                if (dsctfs.Tables.Count != 0)
                {


                    if (dsctfs.Tables[0].Rows.Count > 0)
                    {
                        txtFAT.Text = dsctfs.Tables[0].Rows[0]["FAT_IN_Per"].ToString();
                        txtSNF.Text = dsctfs.Tables[0].Rows[0]["SNF_IN_Per"].ToString();
                        txtCLR.Text = dsctfs.Tables[0].Rows[0]["CLR_IN_Per"].ToString();
                        txtQuantity.Text = dsctfs.Tables[0].Rows[0]["I_MilkSupplyQty"].ToString();
                        MC_txtfatinkg.Text = dsctfs.Tables[0].Rows[0]["FAT_IN_KG"].ToString();
                        MC_snfinkg.Text = dsctfs.Tables[0].Rows[0]["SNF_IN_KG"].ToString();
                    }


                    if (dsctfs.Tables[1].Rows.Count > 0)
                    {
                        txtSaleMilkQuantity.Text = dsctfs.Tables[1].Rows[0]["SaleMilkQuantity"].ToString();
                        txtNetAmount.Text = dsctfs.Tables[1].Rows[0]["NetAmount"].ToString();
                        txtMRPRate.Text = dsctfs.Tables[1].Rows[0]["MRP"].ToString();

                    }

                }


            }
            else
            {

            }

            txtSaleMilkQuantity_TextChanged(sender, e);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void txtSaleMilkQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";

            ddlMilkDispatchtype.SelectedIndex = -1;

            decimal netmilkqty = 0;

            if (txtSaleMilkQuantity.Text == "" || txtSaleMilkQuantity.Text == "0.00" || txtSaleMilkQuantity.Text == "0")
            {
                txtSaleMilkQuantity.Text = "0";
                txtNetAmount.Text = "0";
                txtMRPRate.Text = "0";
            }

            txtNetFat.Text = txtFAT.Text;
            txtNetCLR.Text = txtCLR.Text;
            txtnetsnf.Text = txtSNF.Text;
            netmilkqty = (Convert.ToDecimal(txtQuantity.Text) - Convert.ToDecimal(txtSaleMilkQuantity.Text));
            txtNetMilkQty.Text = netmilkqty.ToString();

            txtNetMilkQtyInKG.Text = obj_MC.GetLtrToKg(Convert.ToDecimal(txtNetCLR.Text), Convert.ToDecimal(netmilkqty)).ToString();
            txtfatinkg.Text = obj_MC.GetFATInKg(Convert.ToDecimal(txtNetMilkQtyInKG.Text), Convert.ToDecimal(txtNetFat.Text)).ToString();
            txtsnfinkg.Text = obj_MC.GetSNFInKg(Convert.ToDecimal(txtNetMilkQtyInKG.Text), Convert.ToDecimal(txtnetsnf.Text)).ToString();

            string MilkRate = obj_MC.GetRatePerLtrOrKg(Math.Round(Convert.ToDecimal(txtNetFat.Text), 1), Math.Round(Convert.ToDecimal(txtNetCLR.Text), 0), objdb.Office_ID());
            string MilkAmount = Math.Round((Convert.ToDecimal(txtNetMilkQtyInKG.Text)) * (Convert.ToDecimal(MilkRate)), 3).ToString();
            txtNetMilkQtyInKG.ToolTip = MilkAmount + " @ " + MilkRate + " Per KG" + " (" + Math.Round(Convert.ToDecimal(txtNetFat.Text), 1) + " , " + Math.Round(Convert.ToDecimal(txtNetCLR.Text), 0) + ")";

            txtrate.Text = MilkRate;
            txtamount.Text = MilkAmount;

            ddlMilkDispatchtype.SelectedValue = "Cans";
            //ddlMilkDispatchtype.Enabled = false;
            ddlMilkDispatchtype_SelectedIndexChanged(sender, e);
            ddlAdulterationTest_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ClearAll()
    {
        ddlDCS.SelectedIndex = 0;
        txtQuantity.Text = "";
        txtFAT.Text = "";
        txtSNF.Text = "";
    }

    protected void ddlMilkDispatchtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            txttemperature.Text = "";
            txttotalcans.Text = "";
            txtV_DriverMobileNo.Text = "";
            txtV_DriverName.Text = "";
            txtV_VehicleNo.Text = "";
            ddlMilkQuality.ClearSelection();
            gv_SealInfo.DataSource = string.Empty;
            gv_SealInfo.DataBind();
            ViewState["InsertRecord1"] = null;
            ddlSealtype.ClearSelection();
            ddlChamberType.ClearSelection();
            ddlSealColor.ClearSelection();
            txtV_SealNo.Text = "";
            txtV_SealRemark.Text = "";

            if (ddlMilkDispatchtype.SelectedValue == "Cans")
            {
                divcandetail.Visible = true;
                divMilkDype.Visible = true;
                RequiredFieldValidator8.Enabled = true;
                RegularExpressionValidator7.Enabled = true;
                divsealdetail.Visible = false;

            }
            else if (ddlMilkDispatchtype.SelectedValue == "Tanker")
            {
                divcandetail.Visible = false;
                RequiredFieldValidator8.Enabled = false;
                RegularExpressionValidator7.Enabled = false;
                divsealdetail.Visible = false;
                divsealdetail.Visible = true;
                divMilkDype.Visible = true;
            }


            else
            {
                divcandetail.Visible = false;
                RequiredFieldValidator8.Enabled = false;
                RegularExpressionValidator7.Enabled = false;
                divsealdetail.Visible = false;
                divMilkDype.Visible = false;
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
            Session["IsSuccess"] = false;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("DCSMilkDispatch.aspx", false);
    }

    protected void MilkDispatchDetail()
    {
        try
        {


            //if (ddlShift.SelectedIndex > 0)
            //{ }
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                     new string[] { "flag", "DT_Date", "V_Shift", "I_OfficeID" },
                     new string[] { "3", Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"), cult).ToString("yyyy-MM-dd"), ddlShift.SelectedValue.ToString(), objdb.Office_ID() }, "dataset");

            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    div_milkdetails.Visible = true;
                    GrdDispatchDetails.DataSource = ds.Tables[0];
                    GrdDispatchDetails.DataBind();
                    btnSubmit.Enabled = false;
                    btnSubmit.ToolTip = "Already Generated Challan For Today " + ddlShift.SelectedValue + " Shift";

                    txtQuantity.Text = "";
                    txtFAT.Text = "";
                    txtSNF.Text = "";
                    txtCLR.Text = "";
                    MC_txtfatinkg.Text = "";
                    MC_snfinkg.Text = "";
                    txtNetMilkQty.Text = "";
                    txtNetFat.Text = "";
                    txtnetsnf.Text = "";
                    txtNetCLR.Text = "";
                    txtNetMilkQtyInKG.Text = "";
                    txtfatinkg.Text = "";
                    txtsnfinkg.Text = "";
                }
                else
                {
                    div_milkdetails.Visible = false;
                    btnSubmit.Enabled = true;

                }


            }

            if (txtNetMilkQty.Text != "")
            {
                if (Convert.ToDecimal(txtNetMilkQty.Text) <= 0)
                {
                    btnTankerSealDetails.Enabled = false;
                    btnTankerSealDetails.ToolTip = "At Present Milk Quantity Showing is Negative Value So Can't Dispatch Milk!";
                    btnSubmit.Enabled = false;
                    btnSubmit.ToolTip = "At Present Milk Quantity Showing is Negative Value So Can't Dispatch Milk!";
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Present Milk Quantity Showing is Negative Value So Can't Dispatch Milk!");

                }
                else
                {
                    lblMsg.Text = "";
                    btnTankerSealDetails.Enabled = true;
                }
            }
            else
            {
                lblMsg.Text = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Present Milk Quantity Showing is 0 Value So Can't Dispatch Milk!");
                btnSubmit.Enabled = false;

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlSealColor_Init(object sender, EventArgs e)
    {
        DataSet ds1 = objdb.ByProcedure("USP_Mst_SealColor",
                              new string[] { "flag" },
                              new string[] { "1" }, "dataset");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            ddlSealColor.DataSource = ds1;
            ddlSealColor.DataTextField = "V_SealColor";
            ddlSealColor.DataValueField = "I_SealColorID";
            ddlSealColor.DataBind();
            ddlSealColor.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlSealColor.DataSource = string.Empty;
            ddlSealColor.DataBind();
            ddlSealColor.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void btnTankerSealDetails_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddSealDetails();
    }

    private void AddSealDetails()
    {
        try
        {
            int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord1"]) == null || Convert.ToString(ViewState["InsertRecord1"]) == "")
            {
                DataTable dt1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt1.Columns.Add(new DataColumn("Sealtype", typeof(string)));
                dt1.Columns.Add(new DataColumn("ChamberType", typeof(string)));
                dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

                dr1 = dt1.NewRow();
                dr1[0] = 1;
                dr1[1] = ddlSealtype.SelectedValue;
                dr1[2] = ddlChamberType.SelectedValue;
                dr1[3] = ddlSealColor.SelectedValue;
                dr1[4] = ddlSealColor.SelectedItem;
                dr1[5] = txtV_SealNo.Text;
                dr1[6] = txtV_SealRemark.Text;
                dt1.Rows.Add(dr1);

                ViewState["InsertRecord1"] = dt1;
                gv_SealInfo.DataSource = dt1;
                gv_SealInfo.DataBind();
            }
            else
            {
                DataTable dt1 = new DataTable();
                DataTable DT1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt1.Columns.Add(new DataColumn("Sealtype", typeof(string)));
                dt1.Columns.Add(new DataColumn("ChamberType", typeof(string)));
                dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

                DT1 = (DataTable)ViewState["InsertRecord1"];

                if (DT1.Rows.Count > 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Only 2 Seal Allow in One Tanker");
                    return;
                }


                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    if (txtV_SealNo.Text == DT1.Rows[i]["V_SealNo"].ToString())
                    {
                        CompartmentType = 1;
                    }
                }
                if (CompartmentType == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Seal of \"" + txtV_SealNo.Text + "\" already exist.");

                }
                else
                {
                    dr1 = dt1.NewRow();
                    dr1[0] = 1;
                    dr1[1] = ddlSealtype.SelectedValue;
                    dr1[2] = ddlChamberType.SelectedValue;
                    dr1[3] = ddlSealColor.SelectedValue;
                    dr1[4] = ddlSealColor.SelectedItem;
                    dr1[5] = txtV_SealNo.Text;
                    dr1[6] = txtV_SealRemark.Text;
                    dt1.Rows.Add(dr1);
                }

                foreach (DataRow tr in DT1.Rows)
                {
                    dt1.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord1"] = dt1;
                gv_SealInfo.DataSource = dt1;
                gv_SealInfo.DataBind();
            }


            //Clear Record
            ddlSealtype.SelectedIndex = -1;
            ddlChamberType.SelectedIndex = -1;
            ddlSealColor.SelectedIndex = -1;
            txtV_SealNo.Text = "";
            txtV_SealRemark.Text = "";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt2 = ViewState["InsertRecord1"] as DataTable;
            dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
            ViewState["InsertRecord1"] = dt2;
            gv_SealInfo.DataSource = dt2;
            gv_SealInfo.DataBind();


            ddlSealtype.SelectedIndex = -1;
            ddlChamberType.SelectedIndex = -1;
            ddlSealColor.SelectedIndex = -1;
            txtV_SealNo.Text = "";
            txtV_SealRemark.Text = "";


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    private void BindAdulterationTestGrid()
    {
        try
        {
            //Start Logic Here
            DataTable dtTL = new DataTable();
            DataRow drTL;

            DataSet DSAT = objdb.ByProcedure("USP_Mst_AdulterationTestList",
                                   new string[] { "flag" },
                                   new string[] { "1" }, "dataset");

            if (DSAT.Tables[0].Rows.Count != 0)
            {
                dtTL.Columns.Add(new DataColumn("S.No", typeof(int)));
                dtTL.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dtTL.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dtTL.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));

                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                    {
                        drTL = dtTL.NewRow();
                        drTL[0] = dtTL.Rows.Count + 1;
                        drTL[1] = "F";
                        drTL[2] = "Singal Chamber";
                        drTL[3] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                        dtTL.Rows.Add(drTL);
                    }
                }
            }

            gvmilkAdulterationtestdetail.DataSource = dtTL;
            gvmilkAdulterationtestdetail.DataBind();

            milktestdetail.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }

    protected void ddlAdulterationTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdulterationTest.SelectedValue == "Yes")
            {
                milktestdetail.Visible = true;
                BindAdulterationTestGrid();

            }
            else
            {
                milktestdetail.Visible = false;
                gvmilkAdulterationtestdetail.DataSource = string.Empty;
                gvmilkAdulterationtestdetail.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }

    private DataTable GetSealGridvalue()
    {

        DataTable dtseal = new DataTable();
        DataRow drseal;

        dtseal.Columns.Add(new DataColumn("Sealtype", typeof(string)));
        dtseal.Columns.Add(new DataColumn("ChamberType", typeof(string)));
        dtseal.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
        dtseal.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
        dtseal.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

        foreach (GridViewRow rowseal in gv_SealInfo.Rows)
        {
            Label lblSealtype = (Label)rowseal.FindControl("lblSealtype");
            Label lblChamberType = (Label)rowseal.FindControl("lblChamberType");
            Label lblTI_SealColor = (Label)rowseal.FindControl("lblTI_SealColor");
            Label lblV_SealNo = (Label)rowseal.FindControl("lblV_SealNo");
            Label lblV_SealRemark = (Label)rowseal.FindControl("lblV_SealRemark");

            drseal = dtseal.NewRow();
            drseal[0] = lblSealtype.Text;
            drseal[1] = lblChamberType.Text;
            drseal[2] = lblTI_SealColor.Text;
            drseal[3] = lblV_SealNo.Text;
            drseal[4] = lblV_SealRemark.Text;

            dtseal.Rows.Add(drseal);
        }
        return dtseal;
    }

    private DataTable GetAdulterationTestDetails()
    {

        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dt.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
        dt.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));

        foreach (GridViewRow row in gvmilkAdulterationtestdetail.Rows)
        {
            Label lblSealLocation = (Label)row.FindControl("lblSealLocation");
            Label lblAdulterationType = (Label)row.FindControl("lblAdulterationType");
            DropDownList ddlAdelterationTestValue = (DropDownList)row.FindControl("ddlAdelterationTestValue");

            string SealLocation = "";
            switch (lblSealLocation.Text)
            {
                case "Single":
                    SealLocation = "S";
                    break;
                case "Rear":
                    SealLocation = "R";
                    break;
                case "Front":
                    SealLocation = "F";
                    break;
                default:
                    break;
            }

            dr = dt.NewRow();
            dr[0] = SealLocation;
            dr[1] = lblAdulterationType.Text;
            dr[2] = ddlAdelterationTestValue.SelectedValue;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";
                int I_TotalCans = 0;
                if (txttotalcans.Text != "")
                {
                    I_TotalCans = Convert.ToInt32(txttotalcans.Text);
                }

                if (txtSaleMilkQuantity.Text == "")
                {
                    txtSaleMilkQuantity.Text = "0.00";
                }
                if (txtMRPRate.Text == "")
                {
                    txtMRPRate.Text = "0.00";
                }
                if (txtNetAmount.Text == "")
                {
                    txtNetAmount.Text = "0.00";
                }

                DataTable dtAdultration = new DataTable(); 

                if (ddlAdulterationTest.SelectedValue == "Yes")
                {
                    dtAdultration = GetAdulterationTestDetails();

                    if (dtAdultration.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "In Adulteration Test Yes - The All Adulteration Field Is Mandatory.");
                        return;
                    }
                }
                else
                { 
                    dtAdultration.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                    dtAdultration.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
                    dtAdultration.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));
                }
                 
                DataTable dtsealF = new DataTable();
                dtsealF = GetSealGridvalue();

                if (ddlMilkDispatchtype.SelectedValue == "Tanker")
                {
                    if (dtsealF.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "In Case Of Tanker - First To Fill At Least One Tanker Valve Seal Details");
                        return;
                    }
                }


                if (ddlMilkDispatchtype.SelectedValue == "Can")
                {
                    if (txttotalcans.Text != "" || txttotalcans.Text != "0" || txttotalcans.Text != "0.00")
                    {

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "In Case Of Can - Please Entered Valid Total Cans");
                        return;
                    }
                }

                // Runtime Validation

                DataSet dsValidationRuntime = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                     new string[] { "flag", "DT_Date", "V_Shift", "I_OfficeID" },
                     new string[] { "3", Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"), cult).ToString("yyyy-MM-dd"), ddlShift.SelectedValue.ToString(), objdb.Office_ID() }, "dataset");

                if (dsValidationRuntime.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Only 1 time Dispatch Alow For Every Day & Shift");
                    return;

                }
                else
                {

                    ds = null;
                    ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                            new string[] { "flag", 
				                                "I_OfficeID",
				                                "I_OfficeTypeID",
				                                "AttachedToCC",
				                                "V_MilkDispatchType",
                                                "I_TotalCans",
				                                "V_VehicleNo",
				                                "V_DriverName",
				                                "V_DriverMobileNo",
				                                "V_EntryType",
				                                "D_MilkQuality",
				                                "V_Shift",
				                                "D_MilkQuantity",
				                                "FAT",
				                                "CLR",
				                                "SNF", 
				                                "I_CreatedByEmpID",
				                                "V_Remark",
				                                "V_EntryFrom",
                                                "V_Temp",
                                                "MilkSaleQty",
                                                "MilkSaleRatePerLtr",
                                                "MilkSaleAmount",
                                                "NetMilkQtyInKG",
                                                "NetFATInKG",
                                                "NetSNFInKG",
                                                "Rate",
                                                "Amount",
                                                "AdulterationTestStatus"
                                                 },

                                                new string[] { "2",  
                                                objdb.Office_ID(),
                                                objdb.OfficeType_ID(), 
                                                ddlDCS.SelectedValue,
                                                ddlMilkDispatchtype.SelectedValue,
                                                I_TotalCans.ToString(),
                                                txtV_VehicleNo.Text,
                                                txtV_DriverName.Text,
                                                txtV_DriverMobileNo.Text,
                                                "Out",
                                                ddlMilkQuality.SelectedItem.Text,
                                                ddlShift.SelectedValue,
                                                txtNetMilkQty.Text,
                                                txtNetFat.Text,
                                                txtNetCLR.Text,
                                                txtnetsnf.Text, 
                                                objdb.createdBy(),
                                                "",
                                                "Web",
                                                txttemperature.Text,
                                                txtSaleMilkQuantity.Text,
                                                txtMRPRate.Text,
                                                txtNetAmount.Text,
                                                txtNetMilkQtyInKG.Text,
                                                txtfatinkg.Text,
                                                txtsnfinkg.Text,
                                                txtrate.Text,
                                                txtamount.Text,
                                                ddlAdulterationTest.SelectedItem.Text
                                                },
                                             new string[] { "type_Trn_TankerSealDetails_Dcs", "type_Trn_tblAdulterationTest_Dcs" },
                                             new DataTable[] { dtsealF, dtAdultration }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        Session["IsSuccess"] = true;
                        Response.Redirect("DCSMilkDispatch.aspx", false);

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        Session["IsSuccess"] = false;
                    }
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }

    }


}