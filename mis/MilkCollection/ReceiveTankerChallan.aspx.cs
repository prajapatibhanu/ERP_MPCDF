using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_MilkCollection_ReceiveTankerChallan : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure apiprocedure = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (apiprocedure.createdBy() != null)
        {
            if (!IsPostBack)
            {
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtfilterdate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                FillGrid();
                //txtDispatchTime.Text = DateTime.Now.ToString("hh:mm tt");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtArrivalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                GetReferenceInfo();
				Timer1_Tick(sender, e);
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }


    private void GetReferenceInfo()
    {

        DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                                  new string[] { "flag", "I_OfficeID", "V_EntryType" },
                                  new string[] { "8", apiprocedure.Office_ID(), "Out" }, "dataset");
        if (ds1.Tables[0].Rows.Count != 0)
        { 
            ddlReferenceNo.DataSource = ds1;
            ddlReferenceNo.DataTextField = "C_ReferenceNo";
            ddlReferenceNo.DataValueField = "BI_MilkInOutRefID";
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

    protected void ddlReferenceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ViewState["InsertRecord"] = null;
            //ViewState["InsertRecord2"] = null;
            //gv_MilkQualityDetail.DataSource = string.Empty;
            //gv_MilkQualityDetail.DataBind();
            //gv_SampleNoDetails.DataSource = string.Empty;
            //gv_SampleNoDetails.DataBind();

            ViewState["InsertRecord"] = null;
            ViewState["InsertRecord2"] = null;

            gv_MilkQualityDetail.DataSource = string.Empty;
            gv_MilkQualityDetail.DataBind();

            gv_SampleNoDetails.DataSource = string.Empty;
            gv_SampleNoDetails.DataBind();
            dv_MilkSampleDetails.Visible = false;

            gvmilkAdulterationtestdetail.DataSource = string.Empty;
            gvmilkAdulterationtestdetail.DataBind();
            milktestdetail.Visible = false;
            btnAddQualityDetails.Enabled = true;

            ddlCompartmentType.ClearSelection();
            ddlMilkQuality.ClearSelection();
            txtTemprature.Text = "";
            txtAcidity.Text = "";
            ddlCOB.ClearSelection();
            txtFat.Text = "";
            txtCLR.Text = "";
            txtSNF.Text = "";
            txtMBRT.Text = "";
            ddlAlcohol.ClearSelection();
            txtRepresentativeName.Text = "";
            txtRepresentativeMobileNo.Text = "";
            txtClosingBalanceAfterDispatch.Text = "";
            txtNextTankerRequiredDate.Text = "";
            ddlNextTankerShift.ClearSelection();
            txtTankerCapacity.Text = "";
            txtTankerCount.Text = "";

            txtAlcoholperc.Text = "";
            ddlAlcohol.ClearSelection();
            DivAlcoholper.Visible = false;


            if (ddlReferenceNo.SelectedValue != "0")
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                               new string[] { "flag", "BI_MilkInOutRefID", "V_EntryType" },
                               new string[] { "9", ddlReferenceNo.SelectedValue, "Out" }, "dataset");

                if (ds1.Tables[0].Rows.Count != 0)
                {

                    ddlchallanno.DataSource = ds1;
                    ddlchallanno.DataTextField = "Challanno";
                    ddlchallanno.DataValueField = "I_EntryID";
                    ddlchallanno.DataBind();
                    ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));


                }
                else
                {
                    ddlchallanno.DataSource = string.Empty;
                    ddlchallanno.DataBind();
                    ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlchallanno.DataSource = string.Empty;
                ddlchallanno.DataBind();
                ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));

            }

            ddlchallanno_SelectedIndexChanged(sender, e);

        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    protected void ddlchallanno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["InsertRecord"] = null;
            ViewState["InsertRecord2"] = null;

            gv_MilkQualityDetail.DataSource = string.Empty;
            gv_MilkQualityDetail.DataBind();

            gv_SampleNoDetails.DataSource = string.Empty;
            gv_SampleNoDetails.DataBind();
            dv_MilkSampleDetails.Visible = false;

            gvmilkAdulterationtestdetail.DataSource = string.Empty;
            gvmilkAdulterationtestdetail.DataBind();
            milktestdetail.Visible = false;
            btnAddQualityDetails.Enabled = true;
            ddlCompartmentType.ClearSelection();
            ddlMilkQuality.ClearSelection();
            txtTemprature.Text = "";
            txtAcidity.Text = "";
            ddlCOB.ClearSelection();
            txtFat.Text = "";
            txtCLR.Text = "";
            txtSNF.Text = "";
            txtMBRT.Text = "";
            ddlAlcohol.ClearSelection();
            txtRepresentativeName.Text = "";
            txtRepresentativeMobileNo.Text = "";
            txtClosingBalanceAfterDispatch.Text = "";
            txtNextTankerRequiredDate.Text = "";
            ddlNextTankerShift.ClearSelection();
            txtTankerCapacity.Text = "";
            txtTankerCount.Text = "";


            if (ddlReferenceNo.SelectedValue != "0" && ddlchallanno.SelectedValue != "0")
            {

                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                               new string[] { "flag", "BI_MilkInOutRefID", "I_EntryID" },
                               new string[] { "10", ddlReferenceNo.SelectedValue, ddlchallanno.SelectedValue }, "dataset");

                if (ds1.Tables[0].Rows.Count != 0)
                {
                    ddlUnitName.SelectedValue = ds1.Tables[0].Rows[0]["I_OfficeID"].ToString();
                    txtDriverName.Text = ds1.Tables[0].Rows[0]["V_DriverName"].ToString();
                    txtDriverMobileNo.Text = ds1.Tables[0].Rows[0]["V_DriverMobileNo"].ToString();
                    txtVehicleNo.Text = ds1.Tables[0].Rows[0]["V_VehicleNo"].ToString();

                    dv_TankerType.Visible = true;
                    ddlTankerType.SelectedValue = ds1.Tables[0].Rows[0]["V_TankerType"].ToString();
                    ddlTankerType_SelectedIndexChanged(sender, e);

                    txtRepresentativeName.Text = ds1.Tables[0].Rows[0]["V_RepresentativeName"].ToString();
                    txtRepresentativeMobileNo.Text = ds1.Tables[0].Rows[0]["V_RepresentativeMobileNo"].ToString();

                    txtClosingBalanceAfterDispatch.Text = ds1.Tables[0].Rows[0]["D_ClosingBalance"].ToString();
                    if (ds1.Tables[0].Rows[0]["DT_NextTankerDate"] != null && ds1.Tables[0].Rows[0]["DT_NextTankerDate"].ToString() != "")
                    {
                        txtNextTankerRequiredDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["DT_NextTankerDate"].ToString(), cult).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtNextTankerRequiredDate.Text = "";
                    }
                    txtTankerCapacity.Text = ds1.Tables[0].Rows[0]["D_TankerCapacity"].ToString();
                    txtTankerCount.Text = ds1.Tables[0].Rows[0]["I_TankerCount"].ToString();
                    ddlNextTankerShift.SelectedValue = ds1.Tables[0].Rows[0]["V_Shift"].ToString();

                    ddlReferenceNo.Enabled = false;

                }
                else
                {
                    ddlUnitName.ClearSelection();
                    txtDriverName.Text = "";
                    txtDriverMobileNo.Text = "";
                    txtVehicleNo.Text = "";

                    dv_TankerType.Visible = true;
                    ddlTankerType.ClearSelection();
                    ddlTankerType_SelectedIndexChanged(sender, e);

                    txtRepresentativeName.Text = "";
                    txtRepresentativeMobileNo.Text = "";

                    txtClosingBalanceAfterDispatch.Text = "";
                    txtNextTankerRequiredDate.Text = "";
                    revTankerCapacity.Text = "";
                    txtTankerCount.Text = "";
                    ddlNextTankerShift.ClearSelection();
                }
            }
            else
            {
                ddlUnitName.ClearSelection();
                txtDriverName.Text = "";
                txtDriverMobileNo.Text = "";
                txtVehicleNo.Text = "";

                dv_TankerType.Visible = true;
                ddlTankerType.ClearSelection();
                ddlTankerType_SelectedIndexChanged(sender, e);

                txtRepresentativeName.Text = "";
                txtRepresentativeMobileNo.Text = "";

                txtClosingBalanceAfterDispatch.Text = "";
                txtNextTankerRequiredDate.Text = "";
                revTankerCapacity.Text = "";
                txtTankerCount.Text = "";
                ddlNextTankerShift.ClearSelection();
                ddlReferenceNo.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            //btnAdd.Visible = false;
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }

        //if (ddlReferenceNo.SelectedIndex != 0)
        //{
        //    ds = null;
        //    ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
        //                    new string[] { "flag", "BI_MilkInOutRefID" },
        //                    new string[] { "5", ddlReferenceNo.SelectedValue }, "dataset");

        //    if (ds.Tables.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            txtDriverName.Text = ds.Tables[0].Rows[0]["V_DriverName"].ToString();
        //            txtDriverMobileNo.Text = ds.Tables[0].Rows[0]["V_DriverMobileNo"].ToString();
        //            txtVehicleNo.Text = ds.Tables[0].Rows[0]["V_VehicleNo"].ToString();

        //            ddlTankerType.SelectedValue = ds.Tables[1].Rows[0]["V_VehicleType"].ToString();
        //            ddlTankerType_SelectedIndexChanged(sender, e);

        //            txtDriverName.Enabled = false;
        //            txtDriverMobileNo.Enabled = false;
        //            txtVehicleNo.Enabled = false;

        //        }
        //        else
        //        {
        //            txtDriverName.Text = "";
        //            txtDriverMobileNo.Text = "";
        //            txtVehicleNo.Text = "";

        //            txtDriverName.Enabled = false;
        //            txtDriverMobileNo.Enabled = false;
        //            txtVehicleNo.Enabled = false;
        //        }
        //    }
        //}
        //else
        //{

        //}
    }

    //protected void ddlReferenceNo_Init(object sender, EventArgs e)
    //{

    //    DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
    //                           new string[] { "flag", "I_OfficeID", "V_EntryType" },
    //                           new string[] { "8", apiprocedure.Office_ID(), "Out" }, "dataset");
    //    if (ds1.Tables[0].Rows.Count != 0)
    //    {

    //        ddlReferenceNo.DataSource = ds1;
    //        ddlReferenceNo.DataTextField = "C_ReferenceNo";
    //        ddlReferenceNo.DataValueField = "BI_MilkInOutRefID";
    //        ddlReferenceNo.DataBind();
    //        ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    else
    //    {
    //        ddlReferenceNo.DataSource = string.Empty;
    //        ddlReferenceNo.DataBind();
    //        ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //}

    public int SendMsg(string MobileNo, string msg)
    {
        int status = 0;
        try
        {
            //Your authentication key
            string authKey = "3597C1493C124F";

            //Sender ID
            string senderId = "SANCHI";

            string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=" + authKey + "&type=unicode&routeid=288&contacts=" + MobileNo + "&senderid=" + senderId + "&msg=" + Server.UrlEncode(msg);
            HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();

            //End Sending OTP SMS
            status = 1;
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        return status;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    private void FillGrid()
    {
        try
        {
            string date = "";

            if (txtfilterdate.Text != "")
            {
                date = Convert.ToDateTime(txtfilterdate.Text, cult).ToString("yyyy/MM/dd");
            }

            gvReceivedEntry.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "Office_ID", "V_EntryType", "Filter_Date" },
                               new string[] { "25", apiprocedure.Office_ID(), "In", date }, "dataset");
            gvReceivedEntry.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
    }

    protected void txtfilterdate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    if (txtTotalSeals.Text != "")
    //    {
    //        StringBuilder html = new StringBuilder();

    //        int sealCount = Convert.ToInt32(txtTotalSeals.Text);
    //        int j = sealCount;

    //        if (ddlTankerType.SelectedValue == "S")
    //        {
    //            for (int i = 0; i < sealCount; i++)
    //            {
    //                html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(i + 1) + "' name='SealNo" + Convert.ToString(i + 1) + "'");
    //                html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(i + 1) + "' name='SealLocation" + Convert.ToString(i + 1) + "'");
    //                html.Append(" readonly='readonly'><option Value='S'>S</option></select></td>");
    //                html.Append("<td><input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(i + 1) + "' name='SealRemark" + Convert.ToString(i + 1) + "'");
    //                html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
    //            }

    //            //html.Append("<tr><th colspan='3' style='text-align:center;'>Valve Box</th></tr><tr><th>Seal No.</th><th>Seal Location</th><th>Seal Remark</th></tr>");

    //            //for (int i = 0; i < 2; i++)
    //            //{
    //            //    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(j + 1) + "' name='SealNo" + Convert.ToString(j + 1) + "'");
    //            //    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(j + 1) + "' name='SealLocation" + Convert.ToString(j + 1) + "'");
    //            //    html.Append(" readonly='readonly'><option Value='VB'>VB</option></select></td>");
    //            //    html.Append("<td><input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(j + 1) + "' name='SealRemark" + Convert.ToString(j + 1) + "'");
    //            //    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
    //            //    j++;
    //            //}

    //            //Enable Required Validations
    //            rfvCompartmentType_S.Enabled = true;
    //            rfvMilkQuality_S.Enabled = true;
    //            //rfvMilkQuantity_S.Enabled = true;
    //            rfvTemprature_S.Enabled = true;
    //            rfvAcidity_S.Enabled = true;
    //            rfvCOB_S.Enabled = true;
    //            rfvFat_S.Enabled = true;
    //            rfvSNF_S.Enabled = true;
    //            rfvCLR_S.Enabled = true;
    //            //rfvAlcohol_S.Enabled = true;
    //            //End

    //            //Enable regular expression Validations
    //            //revMilkQuantity_S.Enabled = true;
    //            revTemprature_S.Enabled = true;
    //            revAcidity_S.Enabled = true;
    //            revFat_S.Enabled = true;
    //            revSNF_S.Enabled = true;
    //            revCLR_S.Enabled = true;
    //            revMBRT_S.Enabled = true;
    //            //End

    //            rowSealDetails.Style.Add("display", "block");
    //            btnSubmit.Visible = true;
    //        }
    //        else if (ddlTankerType.SelectedValue == "D")
    //        {
    //            //for (int i = 0; i < sealCount; i++)
    //            //{
    //            //    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(i + 1) + "' name='SealNo" + Convert.ToString(i + 1) + "'");
    //            //    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(i + 1) + "' name='SealLocation" + Convert.ToString(i + 1) + "'");
    //            //    html.Append("><option Value='F'>F</option><option Value='R'>R</option></select></td>");
    //            //    html.Append("<td><input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(i + 1) + "' name='SealRemark" + Convert.ToString(i + 1) + "'");
    //            //    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
    //            //}

    //            for (int i = 0; i < sealCount; i++)
    //            {
    //                html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(i + 1) + "' name='SealNo" + Convert.ToString(i + 1) + "'");
    //                html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(i + 1) + "' name='SealLocation" + Convert.ToString(i + 1) + "' readonly='readonly'>");

    //                if (((DataTable)ViewState["InsertRecord"]).Rows[0]["V_SealLocation"].ToString() == "F")
    //                {
    //                    html.Append("<option Value='F'>F</option></select></td>");
    //                }
    //                else if (((DataTable)ViewState["InsertRecord"]).Rows[0]["V_SealLocation"].ToString() == "R")
    //                {
    //                    html.Append("<option Value='R'>R</option></select></td>");
    //                }

    //                html.Append("<td><input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(i + 1) + "' name='SealRemark" + Convert.ToString(i + 1) + "'");
    //                html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
    //            }

    //            if (ViewState["InsertRecord"] != null)
    //            {
    //                if (((DataTable)ViewState["InsertRecord"]).Rows.Count == 2)
    //                {
    //                    //html.Append("<tr><th colspan='3' style='text-align:center;'>Valve Box</th></tr><tr><th>Seal No.</th><th>Seal Location</th><th>Seal Remark</th></tr>");

    //                    //for (int i = 0; i < 2; i++)
    //                    //{
    //                    //    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(j + 1) + "' name='SealNo" + Convert.ToString(j + 1) + "'");
    //                    //    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(j + 1) + "' name='SealLocation" + Convert.ToString(j + 1) + "'");
    //                    //    html.Append(" readonly='readonly'><option Value='VB'>VB</option></select></td>");
    //                    //    html.Append("<td><input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(j + 1) + "' name='SealRemark" + Convert.ToString(j + 1) + "'");
    //                    //    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
    //                    //    j++;
    //                    //}
    //                }
    //            }

    //            //Enable Required Validations
    //            rfvCompartmentType_S.Enabled = false;
    //            rfvMilkQuality_S.Enabled = false;
    //            //rfvMilkQuantity_S.Enabled = false;
    //            rfvTemprature_S.Enabled = false;
    //            rfvAcidity_S.Enabled = false;
    //            rfvCOB_S.Enabled = false;
    //            rfvFat_S.Enabled = false;
    //            rfvSNF_S.Enabled = false;
    //            rfvCLR_S.Enabled = false;
    //            //rfvAlcohol_S.Enabled = false;
    //            //End

    //            //Enable regular expression Validations
    //            //revMilkQuantity_S.Enabled = false;
    //            revTemprature_S.Enabled = false;
    //            revAcidity_S.Enabled = false;
    //            revFat_S.Enabled = false;
    //            revSNF_S.Enabled = false;
    //            revCLR_S.Enabled = false;
    //            revMBRT_S.Enabled = false;
    //            //End

    //            rowSealDetails.Style.Add("display", "block");
    //            btnSubmit.Visible = true;
    //        }
    //        else
    //        {
    //            lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Select Tanker Type first!");
    //            ddlTankerType.Focus();

    //            //Enable Required Validations
    //            rfvCompartmentType_S.Enabled = false;
    //            rfvMilkQuality_S.Enabled = false;
    //            //rfvMilkQuantity_S.Enabled = false;
    //            rfvTemprature_S.Enabled = false;
    //            rfvAcidity_S.Enabled = false;
    //            rfvCOB_S.Enabled = false;
    //            rfvFat_S.Enabled = false;
    //            rfvSNF_S.Enabled = false;
    //            rfvCLR_S.Enabled = false;
    //            //rfvAlcohol_S.Enabled = false;
    //            //End

    //            //Enable regular expression Validations
    //            //revMilkQuantity_S.Enabled = false;
    //            revTemprature_S.Enabled = false;
    //            revAcidity_S.Enabled = false;
    //            revFat_S.Enabled = false;
    //            revSNF_S.Enabled = false;
    //            revCLR_S.Enabled = false;
    //            revMBRT_S.Enabled = false;
    //            //End
    //        }

    //        dvSealDetails.InnerHtml = html.ToString();
    //    }
    //    else
    //    {
    //        lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "Enter No. Of Seal first!");
    //    }
    //}

    private void CheckQCDifference(string ReferenceCode, string MobileNo, string CCName, string TankerNo)
    {
        try
        {
            DataSet ds2 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "V_ReferenceCode" },
                               new string[] { "15", ReferenceCode }, "dataset");

            if (ds2.Tables[0].Rows.Count > 0)
            {
                string msg = "CC Name : " + CCName + "\nTanker No. : " + TankerNo + "\n";

                //Fat - F, R, S Compartment
                if (ds2.Tables[0].Select("V_SealLocation = 'F'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'F'")[0]["Diff_FAT"].ToString()) >= Convert.ToDecimal("0.2"))
                    {
                        msg += "FAT diff. of Front chamber : " + ds2.Tables[0].Select("V_SealLocation = 'F'")[0]["Diff_FAT"].ToString() + "\n";
                    }
                }

                if (ds2.Tables[0].Select("V_SealLocation = 'R'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'R'")[0]["Diff_FAT"].ToString()) >= Convert.ToDecimal("0.2"))
                    {
                        msg += "FAT diff. of Rear chamber : " + ds2.Tables[0].Select("V_SealLocation = 'R'")[0]["Diff_FAT"].ToString() + "\n";
                    }
                }

                if (ds2.Tables[0].Select("V_SealLocation = 'S'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'S'")[0]["Diff_FAT"].ToString()) >= Convert.ToDecimal("0.2"))
                    {
                        msg += "FAT diff. of Single chamber : " + ds2.Tables[0].Select("V_SealLocation = 'S'")[0]["Diff_FAT"].ToString() + "\n";
                    }
                }
                //End of Fat - F, R, S Compartment

                //SNF - F, R, S Compartment
                if (ds2.Tables[0].Select("V_SealLocation = 'F'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'F'")[0]["Diff_SNF"].ToString()) >= Convert.ToDecimal("0.25"))
                    {
                        msg += "SNF diff. of Front chamber : " + ds2.Tables[0].Select("V_SealLocation = 'F'")[0]["Diff_SNF"].ToString() + "\n";
                    }
                }

                if (ds2.Tables[0].Select("V_SealLocation = 'R'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'R'")[0]["Diff_SNF"].ToString()) >= Convert.ToDecimal("0.25"))
                    {
                        msg += "SNF diff. of Rear chamber : " + ds2.Tables[0].Select("V_SealLocation = 'R'")[0]["Diff_SNF"].ToString() + "\n";
                    }
                }

                if (ds2.Tables[0].Select("V_SealLocation = 'S'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'S'")[0]["Diff_SNF"].ToString()) >= Convert.ToDecimal("0.25"))
                    {
                        msg += "SNF diff. of Single chamber : " + ds2.Tables[0].Select("V_SealLocation = 'S'")[0]["Diff_SNF"].ToString() + "\n";
                    }
                }
                //End of SNF - F, R, S Compartment

                //int i = SendMsg(Session["MobileNo"].ToString(), msg);
                int i = SendMsg(MobileNo, msg);
                if (i == 1)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "<br>&nbsp;<b>Diffrence Report sent to your registered Mobile No.</b><br />";
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                DataTable dt = ViewState["InsertRecord"] as DataTable;

                if (ViewState["InsertRecord2"] != null)
                {
                    DataRow[] rows = ((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = '" + dt.Rows[row.RowIndex]["V_SealLocation"] + "'");
                    foreach (DataRow ro in rows)
                    {
                        ((DataTable)ViewState["InsertRecord2"]).Rows.Remove(ro);
                    }
                }

                gv_SampleNoDetails.DataSource = ((DataTable)ViewState["InsertRecord2"]);
                gv_SampleNoDetails.DataBind();

                dt.Rows.Remove(dt.Rows[row.RowIndex]);
                ViewState["InsertRecord"] = dt;
                gv_MilkQualityDetail.DataSource = dt;
                gv_MilkQualityDetail.DataBind();

                //Fill Chamber Dropdownlist of Milk Sample details 
                ddlChamberOfSampleNo.DataSource = ((DataTable)ViewState["InsertRecord"]);
                ddlChamberOfSampleNo.DataTextField = "V_SealLocationAlias";
                ddlChamberOfSampleNo.DataValueField = "V_SealLocation";
                ddlChamberOfSampleNo.DataBind();

                if (((DataTable)ViewState["InsertRecord"]).Rows.Count < 0)
                {
                    ddlChamberOfSampleNo.Items.Insert(0, new ListItem("Select", "0"));
                }

                gvmilkAdulterationtestdetail.DataSource = null;
                gvmilkAdulterationtestdetail.DataBind();

                if (((DataTable)ViewState["InsertRecord"]).Select("V_SealLocation = 'F'").Length > 0 && ((DataTable)ViewState["InsertRecord"]).Select("V_SealLocation = 'R'").Length > 0)
                {
                    btnAddQualityDetails.Enabled = false;
                }
                else
                {
                    btnAddQualityDetails.Enabled = true;
                }

                BindAdulterationTestGrid();

                //For clear record for add child record
                //txtMilkQuantity.Text = "";
                ddlCompartmentType.ClearSelection();
                txtFat.Text = "";
                txtSNF.Text = "";
                txtCLR.Text = "";
                txtTemprature.Text = "";
                txtAcidity.Text = "";
                ddlCOB.ClearSelection();
                txtMBRT.Text = "";
                txtAlcoholperc.Text = "";
                ddlAlcohol.ClearSelection();
                DivAlcoholper.Visible = false;

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }

    protected void ddlTankerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        //dvSealDetails.InnerHtml = string.Empty;
        //rowSealDetails.Style.Add("display", "none");
        btnSubmit.Visible = false;

        if (ddlTankerType.SelectedIndex != 0)
        {
            if (ddlTankerType.SelectedValue == "S")
            {
                //txtTotalSeals.Text = "1";

                //Range validator setting
                //rvTotalSeals.MinimumValue = "1";


                ddlCompartmentType.Items.Clear();
                ddlCompartmentType.Items.Add(new ListItem("Single", "S"));
                ddlCompartmentType.Enabled = false;

                //Hide Milk Quality detail add multi time
                dv_gvMilkQualityDeailsAddButton.Visible = false;
                dv_gvMilkQualityDeails.Visible = false;

                BindAdulterationTestGrid();

                //Adulteration section Visible=true when change Tanker type
                //dv_CompartmentTypeforAdulteration.Visible = true;
                //dv_AdulterationType.Visible = true;
                //dv_AdulterationValue.Visible = true;
                //dv_btnAdulterationTest.Visible = true;
                //dv_AdulterationHeaderSection.Visible = true;
                //dv_AdulterationTestGridVIew.Visible = true;

                //show Add generate seal entry control when Single Compartment tanker
                //btnAdd.Visible = false;

                //Disable required field validator in add quality details setction
                rfvMilkQuality_S.Enabled = true;
                //rfvMilkQuantity_S.Enabled = true;
                rfvTemprature_S.Enabled = true;
                rfvAcidity_S.Enabled = true;
                rfvCOB_S.Enabled = true;
                rfvFat_S.Enabled = true;
                rfvSNF_S.Enabled = true;
                rfvCLR_S.Enabled = true;

                //Disable regular expression validator in add quality details setction
                //revMilkQuantity_S.Enabled = true;
                revTemprature_S.Enabled = true;
                revAcidity_S.Enabled = true;
                revFat_S.Enabled = true;
                revSNF_S.Enabled = true;
                revCLR_S.Enabled = true;
                revMBRT_S.Enabled = true;

                //Disable required field validator in add quality details setction
                rfvMilkQuality.Enabled = false;
                //rfvMilkQuantity.Enabled = false;
                rfvTemprature.Enabled = false;
                rfvAcidity.Enabled = false;
                rfvCOB.Enabled = false;
                rfvFat.Enabled = false;
                rfvSNF.Enabled = false;
                rfvCLR.Enabled = false;

                //Disable regular expression validator in add quality details setction
                //revMilkQuantity.Enabled = false;
                revTemprature.Enabled = false;
                revAcidity.Enabled = false;
                revFat.Enabled = false;
                revSNF.Enabled = false;
                revCLR.Enabled = false;
                revMBRT.Enabled = false;

                //Milk Sample Details
                dv_MilkSampleDetails.Visible = true;
                dv_ChamberOfSampleDetails.Visible = true;
                dv_SampleNo.Visible = true;
                dv_SampleRemark.Visible = true;
                dv_SampleNoGrid.Visible = true;
                dv_gv_sampleNoDetails.Visible = true;

            }
            else if (ddlTankerType.SelectedValue == "D")
            {
                //txtTotalSeals.Text = "2";

                ////Range validator setting
                //rvTotalSeals.MinimumValue = "2";

                ddlCompartmentType.Items.Clear();

                ddlCompartmentType.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                                           new string[] { "flag", "V_ReferenceCode" },
                                                           new string[] { "20", ddlchallanno.SelectedItem.Text }, "dataset");
                ddlCompartmentType.DataTextField = "SealLocation";
                ddlCompartmentType.DataValueField = "Value";
                ddlCompartmentType.DataBind();
                //ddlCompartmentType.Items.Insert(0, new ListItem("Select", "0"));

                //ddlCompartmentType.Items.Add(new ListItem("Front", "F"));
                //ddlCompartmentType.Items.Add(new ListItem("Rear", "R"));
                ddlCompartmentType.Enabled = true;

                //Show Milk Quality detail add multi time
                dv_gvMilkQualityDeailsAddButton.Visible = true;
                dv_gvMilkQualityDeails.Visible = true;

                //Adulteration section Visible=false when change Tanker type
                //dv_CompartmentTypeforAdulteration.Visible = false;
                //dv_AdulterationType.Visible = false;
                //dv_AdulterationValue.Visible = false;
                //dv_btnAdulterationTest.Visible = false;
                //dv_AdulterationHeaderSection.Visible = false;
                //dv_AdulterationTestGridVIew.Visible = false;

                //hide Add generate seal entry control when Single Compartment tanker
                //btnAdd.Visible = false;

                //Disable required field validator in add quality details setction
                rfvMilkQuality_S.Enabled = false;
                //rfvMilkQuantity_S.Enabled = false;
                rfvTemprature_S.Enabled = false;
                rfvAcidity_S.Enabled = false;
                rfvCOB_S.Enabled = false;
                rfvFat_S.Enabled = false;
                rfvSNF_S.Enabled = false;
                rfvCLR_S.Enabled = false;

                //Disable regular expression validator in add quality details setction
                //revMilkQuantity_S.Enabled = false;
                revTemprature_S.Enabled = false;
                revAcidity_S.Enabled = false;
                revFat_S.Enabled = false;
                revSNF_S.Enabled = false;
                revCLR_S.Enabled = false;
                revMBRT_S.Enabled = false;

                //Disable required field validator in add quality details setction
                rfvMilkQuality.Enabled = true;
                //rfvMilkQuantity.Enabled = true;
                rfvTemprature.Enabled = true;
                rfvAcidity.Enabled = true;
                rfvCOB.Enabled = true;
                rfvFat.Enabled = true;
                rfvSNF.Enabled = true;
                rfvCLR.Enabled = true;

                //Disable regular expression validator in add quality details setction
                //revMilkQuantity.Enabled = true;
                revTemprature.Enabled = true;
                revAcidity.Enabled = true;
                revFat.Enabled = true;
                revSNF.Enabled = true;
                revCLR.Enabled = true;
                revMBRT.Enabled = true;

                //Milk Sample Details
                dv_MilkSampleDetails.Visible = false;
                dv_ChamberOfSampleDetails.Visible = false;
                dv_SampleNo.Visible = false;
                dv_SampleRemark.Visible = false;
                dv_SampleNoGrid.Visible = false;
                dv_gv_sampleNoDetails.Visible = false;
            }
        }
        else
        {
            //txtTotalSeals.Text = "2";

            //Range validator setting
            //rvTotalSeals.MinimumValue = "2";

            ddlCompartmentType.Items.Clear();
            ddlCompartmentType.Items.Insert(0, new ListItem("Select", "0"));
            ddlCompartmentType.Enabled = true;

            //Hide Milk Quality detail add multi time
            dv_gvMilkQualityDeailsAddButton.Visible = false;
            dv_gvMilkQualityDeails.Visible = false;

            //Adulteration section Visible=false when change Tanker type
            //dv_CompartmentTypeforAdulteration.Visible = false;
            //dv_AdulterationType.Visible = false;
            //dv_AdulterationValue.Visible = false;
            //dv_btnAdulterationTest.Visible = false;
            //dv_AdulterationHeaderSection.Visible = false;
            //dv_AdulterationTestGridVIew.Visible = false;

            //hide Add generate seal entry control when selected index = 0
            //btnAdd.Visible = false;

            //Disable required field validator in add quality details setction
            rfvMilkQuality_S.Enabled = true;
            //rfvMilkQuantity_S.Enabled = true;
            rfvTemprature_S.Enabled = true;
            rfvAcidity_S.Enabled = true;
            rfvCOB_S.Enabled = true;
            rfvFat_S.Enabled = true;
            rfvSNF_S.Enabled = true;
            rfvCLR_S.Enabled = true;

            //Disable regular expression validator in add quality details setction
            //revMilkQuantity_S.Enabled = true;
            revTemprature_S.Enabled = true;
            revAcidity_S.Enabled = true;
            revFat_S.Enabled = true;
            revSNF_S.Enabled = true;
            revCLR_S.Enabled = true;
            revMBRT_S.Enabled = true;

            //Disable required field validator in add quality details setction
            rfvMilkQuality.Enabled = false;
            //rfvMilkQuantity.Enabled = false;
            rfvTemprature.Enabled = false;
            rfvAcidity.Enabled = false;
            rfvCOB.Enabled = false;
            rfvFat.Enabled = false;
            rfvSNF.Enabled = false;
            rfvCLR.Enabled = false;

            //Disable regular expression validator in add quality details setction
            //revMilkQuantity.Enabled = false;
            revTemprature.Enabled = false;
            revAcidity.Enabled = false;
            revFat.Enabled = false;
            revSNF.Enabled = false;
            revCLR.Enabled = false;
            revMBRT.Enabled = false;

            //Milk Sample Details
            dv_MilkSampleDetails.Visible = false;
            dv_ChamberOfSampleDetails.Visible = false;
            dv_SampleNo.Visible = false;
            dv_SampleRemark.Visible = false;
            dv_SampleNoGrid.Visible = false;
            dv_gv_sampleNoDetails.Visible = false;
        }
    }

    protected void btnAddQualityDetails_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddQualityDetails();
        ddlCompartmentType.Focus();
    }

    private DataTable GetMilkQualityDetails()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(int)));
        dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dt.Columns.Add(new DataColumn("D_FAT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("D_SNF", typeof(decimal)));
        dt.Columns.Add(new DataColumn("D_CLR", typeof(decimal)));
        dt.Columns.Add(new DataColumn("V_Temp", typeof(string)));
        dt.Columns.Add(new DataColumn("V_Acidity", typeof(string)));
        dt.Columns.Add(new DataColumn("V_COB", typeof(string)));
        dt.Columns.Add(new DataColumn("V_Alcohol", typeof(string)));
        dt.Columns.Add(new DataColumn("V_MBRT", typeof(string)));
        dt.Columns.Add(new DataColumn("V_MilkQuality", typeof(string)));

        foreach (GridViewRow row in gv_MilkQualityDetail.Rows)
        {
            //Label lblMilkQuantity = (Label)row.FindControl("lblMilkQuantity");
            Label lblSealLocation = (Label)row.FindControl("lblSealLocation");
            Label lblFAT = (Label)row.FindControl("lblFAT");
            Label lblSNF = (Label)row.FindControl("lblSNF");
            Label lblCLR = (Label)row.FindControl("lblCLR");
            Label lblTemp = (Label)row.FindControl("lblTemp");
            Label lblAcidity = (Label)row.FindControl("lblAcidity");
            Label lblCOB = (Label)row.FindControl("lblCOB");
            Label lblAlcohol = (Label)row.FindControl("lblAlcohol");
            Label lblMBRT = (Label)row.FindControl("lblMBRT");
            Label lblMilkQuality = (Label)row.FindControl("lblMilkQuality");

            dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = lblSealLocation.Text;
            dr[2] = lblFAT.Text;
            dr[3] = lblSNF.Text;
            dr[4] = lblCLR.Text;
            dr[5] = lblTemp.Text;
            dr[6] = lblAcidity.Text;
            dr[7] = lblCOB.Text;
            dr[8] = lblAlcohol.Text;
            dr[9] = lblMBRT.Text;
            dr[10] = lblMilkQuality.Text;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void AddQualityDetails()
    {
        try
        {
            int CompartmentType = 0;

            string alcoholePer = "";

            if (ddlAlcohol.SelectedValue == "Negative")
            {
                alcoholePer = ddlAlcohol.SelectedValue + "(" + txtAlcoholperc.Text + "%)";
            }
            else if (ddlAlcohol.SelectedValue == "Positive")
            {
                alcoholePer = "Positive";
            }
            else
            {
                alcoholePer = "0";
            }


            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                //dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(int)));
                dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dt.Columns.Add(new DataColumn("D_FAT", typeof(decimal)));
                dt.Columns.Add(new DataColumn("D_SNF", typeof(decimal)));
                dt.Columns.Add(new DataColumn("D_CLR", typeof(decimal)));
                dt.Columns.Add(new DataColumn("V_Temp", typeof(string)));
                dt.Columns.Add(new DataColumn("V_Acidity", typeof(string)));
                dt.Columns.Add(new DataColumn("V_Alcohol", typeof(string)));
                dt.Columns.Add(new DataColumn("V_COB", typeof(string)));
                dt.Columns.Add(new DataColumn("V_MBRT", typeof(string)));
                dt.Columns.Add(new DataColumn("V_MilkQuality", typeof(string)));

                dr = dt.NewRow();
                dr[0] = 1;
                //dr[1] = txtMilkQuantity.Text;
                dr[1] = ddlCompartmentType.SelectedValue;
                dr[2] = ddlCompartmentType.SelectedItem.Text;
                dr[3] = txtFat.Text;
                dr[4] = txtSNF.Text;
                dr[5] = txtCLR.Text;
                dr[6] = txtTemprature.Text;
                dr[7] = txtAcidity.Text;
                dr[8] = alcoholePer; //ddlAlcohol.SelectedValue;
                dr[9] = ddlCOB.SelectedValue;
                dr[10] = txtMBRT.Text == "" ? "0" : txtMBRT.Text;
                dr[11] = ddlMilkQuality.SelectedValue;
                dt.Rows.Add(dr);

                ViewState["InsertRecord"] = dt;
                gv_MilkQualityDetail.DataSource = dt;
                gv_MilkQualityDetail.DataBind();

                //ddlCompartmentTypeforQuality.DataSource = dt;
                //ddlCompartmentTypeforQuality.DataTextField = "V_SealLocationAlias";
                //ddlCompartmentTypeforQuality.DataValueField = "V_SealLocation";
                //ddlCompartmentTypeforQuality.DataBind();
                //ddlCompartmentTypeforQuality.Items.Insert(0, new ListItem("Select", "0"));


                //Adulteration Test details
                //dv_CompartmentTypeforAdulteration.Visible = true;
                //dv_AdulterationType.Visible = true;
                //dv_AdulterationValue.Visible = true;
                //dv_btnAdulterationTest.Visible = true;
                //dv_AdulterationHeaderSection.Visible = true;
                //dv_AdulterationTestGridVIew.Visible = true;


                ddlChamberOfSampleNo.DataSource = dt;
                ddlChamberOfSampleNo.DataTextField = "V_SealLocationAlias";
                ddlChamberOfSampleNo.DataValueField = "V_SealLocation";
                ddlChamberOfSampleNo.DataBind();
                //ddlChamberOfSampleNo.Items.Insert(0, new ListItem("Select", "0"));

                //Milk Sample Details
                dv_MilkSampleDetails.Visible = true;
                dv_ChamberOfSampleDetails.Visible = true;
                dv_SampleNo.Visible = true;
                dv_SampleRemark.Visible = true;
                dv_SampleNoGrid.Visible = true;
                dv_gv_sampleNoDetails.Visible = true;
            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                //dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(int)));
                dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dt.Columns.Add(new DataColumn("D_FAT", typeof(decimal)));
                dt.Columns.Add(new DataColumn("D_SNF", typeof(decimal)));
                dt.Columns.Add(new DataColumn("D_CLR", typeof(decimal)));
                dt.Columns.Add(new DataColumn("V_Temp", typeof(string)));
                dt.Columns.Add(new DataColumn("V_Acidity", typeof(string)));
                dt.Columns.Add(new DataColumn("V_Alcohol", typeof(string)));
                dt.Columns.Add(new DataColumn("V_COB", typeof(string)));
                dt.Columns.Add(new DataColumn("V_MBRT", typeof(string)));
                dt.Columns.Add(new DataColumn("V_MilkQuality", typeof(string)));

                DT = (DataTable)ViewState["InsertRecord"];
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlCompartmentType.SelectedValue == DT.Rows[i]["V_SealLocation"].ToString())
                    {
                        CompartmentType = 1;
                    }
                }
                if (CompartmentType == 1)
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Compartment of \"" + ddlCompartmentType.SelectedItem.Text + "\" already exist.");
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = 1;
                    //dr[1] = txtMilkQuantity.Text;
                    dr[1] = ddlCompartmentType.SelectedValue;
                    dr[2] = ddlCompartmentType.SelectedItem.Text;
                    dr[3] = txtFat.Text;
                    dr[4] = txtSNF.Text;
                    dr[5] = txtCLR.Text;
                    dr[6] = txtTemprature.Text;
                    dr[7] = txtAcidity.Text;
                    dr[8] = alcoholePer; //ddlAlcohol.SelectedValue;
                    dr[9] = ddlCOB.SelectedValue;
                    dr[10] = txtMBRT.Text == "" ? "0" : txtMBRT.Text;
                    dr[11] = ddlMilkQuality.SelectedValue;
                    dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord"] = dt;
                gv_MilkQualityDetail.DataSource = dt;
                gv_MilkQualityDetail.DataBind();

                //Adelteration Test Details
                //ddlCompartmentTypeforQuality.DataSource = dt;
                //ddlCompartmentTypeforQuality.DataTextField = "V_SealLocationAlias";
                //ddlCompartmentTypeforQuality.DataValueField = "V_SealLocation";
                //ddlCompartmentTypeforQuality.DataBind();
                //ddlCompartmentTypeforQuality.Items.Insert(0, new ListItem("Select", "0"));

                //dv_CompartmentTypeforAdulteration.Visible = true;
                //dv_AdulterationType.Visible = true;
                //dv_AdulterationValue.Visible = true;
                //dv_btnAdulterationTest.Visible = true;
                //dv_AdulterationHeaderSection.Visible = true;
                //dv_AdulterationTestGridVIew.Visible = true;

                //Milk Sample Details
                ddlChamberOfSampleNo.DataSource = dt;
                ddlChamberOfSampleNo.DataTextField = "V_SealLocationAlias";
                ddlChamberOfSampleNo.DataValueField = "V_SealLocation";
                ddlChamberOfSampleNo.DataBind();
                //ddlChamberOfSampleNo.Items.Insert(0, new ListItem("Select", "0"));

                dv_MilkSampleDetails.Visible = true;
                dv_ChamberOfSampleDetails.Visible = true;
                dv_SampleNo.Visible = true;
                dv_SampleRemark.Visible = true;
                dv_SampleNoGrid.Visible = true;
                dv_gv_sampleNoDetails.Visible = true;
            }

            BindAdulterationTestGrid();

            txtAlcoholperc.Text = "";
            ddlAlcohol.ClearSelection();
            DivAlcoholper.Visible = false;


            //Clear Record
            //txtMilkQuantity.Text = "";
            ddlCompartmentType.ClearSelection();
            ddlMilkQuality.ClearSelection();
            txtFat.Text = "";
            txtSNF.Text = "";
            txtCLR.Text = "";
            txtTemprature.Text = "";
            txtAcidity.Text = "";
            ddlCOB.ClearSelection();
            txtMBRT.Text = ""; 

            if (((DataTable)ViewState["InsertRecord"]).Select("V_SealLocation = 'F'").Length > 0 && ((DataTable)ViewState["InsertRecord"]).Select("V_SealLocation = 'R'").Length > 0)
            {
                btnAddQualityDetails.Enabled = false;
            }
            else
            {
                btnAddQualityDetails.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
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

    //private void AddAdulterationTestDetails()
    //{
    //    try
    //    {
    //        int CompartmentType = 0;

    //        if (Convert.ToString(ViewState["InsertRecord1"]) == null || Convert.ToString(ViewState["InsertRecord1"]) == "")
    //        {
    //            DataTable dt = new DataTable();
    //            DataRow dr;
    //            dt.Columns.Add(new DataColumn("S.No", typeof(int)));
    //            dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
    //            dt.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
    //            dt.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
    //            dt.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));

    //            dr = dt.NewRow();
    //            dr[0] = 1;
    //            dr[1] = ddlCompartmentTypeforQuality.SelectedValue;
    //            dr[2] = ddlCompartmentTypeforQuality.SelectedItem.Text;
    //            dr[3] = ddlAdulterationType.SelectedValue;
    //            dr[4] = ddlAdulterationValue.SelectedValue;
    //            dt.Rows.Add(dr);

    //            ViewState["InsertRecord1"] = dt;
    //            gv_AdulterationTestDetails.DataSource = dt;
    //            gv_AdulterationTestDetails.DataBind();

    //        }
    //        else
    //        {
    //            DataTable dt = new DataTable();
    //            DataTable DT = new DataTable();
    //            DataRow dr;
    //            dt.Columns.Add(new DataColumn("S.No", typeof(int)));
    //            dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
    //            dt.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
    //            dt.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
    //            dt.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));

    //            DT = (DataTable)ViewState["InsertRecord1"];
    //            for (int i = 0; i < DT.Rows.Count; i++)
    //            {
    //                if (ddlCompartmentTypeforQuality.SelectedValue == DT.Rows[i]["V_SealLocation"].ToString() && ddlAdulterationType.SelectedValue == DT.Rows[i]["V_AdulterationType"].ToString())
    //                {
    //                    CompartmentType = 1;
    //                }
    //            }
    //            if (CompartmentType == 1)
    //            {
    //                lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Compartment of \"" + ddlCompartmentTypeforQuality.SelectedItem.Text + "\" Adulteration Type and " + ddlAdulterationType.SelectedValue + " already exist.");
    //            }
    //            else
    //            {
    //                dr = dt.NewRow();
    //                dr[0] = dt.Rows.Count + 1;
    //                dr[1] = ddlCompartmentTypeforQuality.SelectedValue;
    //                dr[2] = ddlCompartmentTypeforQuality.SelectedItem.Text;
    //                dr[3] = ddlAdulterationType.SelectedValue;
    //                dr[4] = ddlAdulterationValue.SelectedValue;
    //                dt.Rows.Add(dr);
    //            }

    //            foreach (DataRow tr in DT.Rows)
    //            {
    //                dt.Rows.Add(tr.ItemArray);
    //            }
    //            ViewState["InsertRecord1"] = dt;
    //            gv_AdulterationTestDetails.DataSource = dt;
    //            gv_AdulterationTestDetails.DataBind();

    //        }

    //        //Clear Record
    //        ddlCompartmentTypeforQuality.ClearSelection();
    //        ddlAdulterationType.ClearSelection();
    //        ddlAdulterationValue.ClearSelection();

    //        if ((((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'F'").Length > ddlAdulterationType.Items.Count - 2 || ((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'R'").Length > ddlAdulterationType.Items.Count - 2 && ddlTankerType.SelectedValue == "D") || (((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'S'").Length > ddlAdulterationType.Items.Count - 2 && ddlTankerType.SelectedValue == "S"))
    //        {
    //            btnSubmit.Visible = true;
    //        }
    //        else
    //        {
    //            btnSubmit.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
    //    }
    //}

    protected void btnAdulterationTest_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        //AddAdulterationTestDetails();
        //ddlCompartmentTypeforQuality.Focus();
    }

    //protected void lnkAdulterationDelete_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";

    //        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
    //        DataTable dt = ViewState["InsertRecord1"] as DataTable;
    //        dt.Rows.Remove(dt.Rows[row.RowIndex]);
    //        ViewState["InsertRecord1"] = dt;
    //        gv_AdulterationTestDetails.DataSource = dt;
    //        gv_AdulterationTestDetails.DataBind();

    //        //For clear record for add child record
    //        ddlCompartmentTypeforQuality.ClearSelection();
    //        ddlAdulterationType.ClearSelection();
    //        ddlAdulterationValue.ClearSelection();

    //        if ((((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'F'").Length > ddlAdulterationType.Items.Count - 2 || ((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'R'").Length > ddlAdulterationType.Items.Count - 2 && ddlTankerType.SelectedValue == "D") || (((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'S'").Length > ddlAdulterationType.Items.Count - 2 && ddlTankerType.SelectedValue == "S"))
    //        {
    //            btnSubmit.Visible = true;
    //        }
    //        else
    //        {
    //            btnSubmit.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
    //    }
    //}

    protected void ddlUnitName_Init(object sender, EventArgs e)
    {
        ddlUnitName.DataSource = apiprocedure.ByProcedure("Sp_CommonTables",
                               new string[] { "flag", "Office_ID" },
                               new string[] { "11", apiprocedure.Office_ID() }, "dataset");
        ddlUnitName.DataTextField = "Office_Name";
        ddlUnitName.DataValueField = "Office_ID";
        ddlUnitName.DataBind();
        ddlUnitName.Items.Insert(0, new ListItem("Select", "0"));
    }


    protected void txtCLR_TextChanged(object sender, EventArgs e)
    {
        txtSNF.Text = GetSNF().ToString();

        if (txtCLR.Text == "" || txtFat.Text == "")
        { txtSNF.Text = ""; }

        if (txtCLR.Text == "")
        {
            txtCLR.Focus();
        }

        if (txtFat.Text == "")
        {
            txtFat.Focus();
        }

        if (txtCLR.Text != "" && txtFat.Text != "")
        {
            txtMBRT.Focus();
        }
    }

    private decimal GetSNF()
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (txtCLR.Text != "")
            { clr = Convert.ToDecimal(txtCLR.Text); }
            if (txtFat.Text != "")
            { fat = Convert.ToDecimal(txtFat.Text); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 2);
    }

    protected void lnkbtnSampleNoDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["InsertRecord2"] as DataTable;
            dt.Rows.Remove(dt.Rows[row.RowIndex]);
            ViewState["InsertRecord2"] = dt;
            gv_SampleNoDetails.DataSource = dt;
            gv_SampleNoDetails.DataBind();

            if ((((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'F'").Length > 0 || ((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'R'").Length > 0 && ddlTankerType.SelectedValue == "D") || (((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'S'").Length > 0 && ddlTankerType.SelectedValue == "S"))
            {
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
            }

            //For clear record for add child record
            ddlChamberOfSampleNo.ClearSelection();
            txtSampleNo.Text = "";
            txtSampleRemark.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }

    private DataTable GetMilkSampleDetails()
    {

        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dt.Columns.Add(new DataColumn("V_SampleNo", typeof(string)));
        dt.Columns.Add(new DataColumn("V_SampleRemark", typeof(string)));

        foreach (GridViewRow row in gv_SampleNoDetails.Rows)
        {
            Label lblSealLocation = (Label)row.FindControl("lblSealLocation");
            Label lblSampleNo = (Label)row.FindControl("lblSampleNo");
            Label lblSampleRemark = (Label)row.FindControl("lblSampleRemark");

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
            dr[1] = lblSampleNo.Text;
            dr[2] = lblSampleRemark.Text;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    protected void BtnAddSample_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddSampleNoDetails();
        ddlChamberOfSampleNo.Focus();
    }

    private void AddSampleNoDetails()
    {
        try
        {
            int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord2"]) == null || Convert.ToString(ViewState["InsertRecord2"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SampleNo", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SampleRemark", typeof(string)));

                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlChamberOfSampleNo.SelectedValue;
                dr[2] = ddlChamberOfSampleNo.SelectedItem.Text;
                dr[3] = txtSampleNo.Text;
                dr[4] = txtSampleRemark.Text;
                dt.Rows.Add(dr);

                ViewState["InsertRecord2"] = dt;
                gv_SampleNoDetails.DataSource = dt;
                gv_SampleNoDetails.DataBind();

            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SampleNo", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SampleRemark", typeof(string)));

                DT = (DataTable)ViewState["InsertRecord2"];
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlChamberOfSampleNo.SelectedValue == DT.Rows[i]["V_SealLocation"].ToString() && txtSampleNo.Text == DT.Rows[i]["V_SampleNo"].ToString())
                    {
                        CompartmentType = 1;
                    }

                    if (txtSampleNo.Text == DT.Rows[i]["V_SampleNo"].ToString())
                    {
                        CompartmentType = 2;
                    }
                }
                if (CompartmentType == 1)
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Compartment of \"" + ddlChamberOfSampleNo.SelectedItem.Text + "\" in Sample No. " + txtSampleNo.Text + " already exist.");
                }
                else if (CompartmentType == 2)
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Sample No. " + txtSampleNo.Text + " already exist.");
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = dt.Rows.Count + 1;
                    dr[1] = ddlChamberOfSampleNo.SelectedValue;
                    dr[2] = ddlChamberOfSampleNo.SelectedItem.Text;
                    dr[3] = txtSampleNo.Text;
                    dr[4] = txtSampleRemark.Text;
                    dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord2"] = dt;
                gv_SampleNoDetails.DataSource = dt;
                gv_SampleNoDetails.DataBind();

            }

            //Clear Record
            ddlChamberOfSampleNo.ClearSelection();
            txtSampleNo.Text = "";
            txtSampleRemark.Text = "";

            if ((((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'F'").Length > 0 || ((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'R'").Length > 0 && ddlTankerType.SelectedValue == "D") || (((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'S'").Length > 0 && ddlTankerType.SelectedValue == "S"))
            {
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //btnAdd.Visible = false;
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    //private void BindAdulterationTestGrid()
    //{
    //    try
    //    {
    //        //Start Logic Here
    //        DataTable dtTL = new DataTable();
    //        DataRow drTL;

    //        DataSet DSAT = apiprocedure.ByProcedure("USP_Mst_AdulterationTestList",
    //                               new string[] { "flag" },
    //                               new string[] { "1" }, "dataset");

    //        if (DSAT.Tables[0].Rows.Count != 0)
    //        {
    //            dtTL.Columns.Add(new DataColumn("S.No", typeof(int)));
    //            dtTL.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
    //            dtTL.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
    //            dtTL.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));

    //            foreach (DataTable table in DSAT.Tables)
    //            {
    //                for (int i = 0; i < table.Rows.Count; i++)
    //                {
    //                    drTL = dtTL.NewRow();
    //                    drTL[0] = dtTL.Rows.Count + 1;
    //                    drTL[1] = ddlCompartmentType.SelectedValue;
    //                    drTL[2] = ddlCompartmentType.SelectedItem.Text;
    //                    drTL[3] = DSAT.Tables[0].Rows[i]["V_AdulteratioTName"].ToString();
    //                    dtTL.Rows.Add(drTL);
    //                }
    //            }
    //        }

    //        gvmilkAdulterationtestdetail.DataSource = dtTL;
    //        gvmilkAdulterationtestdetail.DataBind();

    //        milktestdetail.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
    //    }
    //}


    private void BindAdulterationTestGrid()
    {
        try
        {
            //Start Logic Here
            DataTable dtTL = new DataTable();
            DataRow drTL;

            DataSet DSAT = apiprocedure.ByProcedure("USP_Mst_AdulterationTestList",
                                   new string[] { "flag" },
                                   new string[] { "1" }, "dataset");

            if (DSAT.Tables[0].Rows.Count != 0)
            {
                dtTL.Columns.Add(new DataColumn("S.No", typeof(int)));
                dtTL.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dtTL.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dtTL.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));

                if (ddlTankerType.SelectedValue == "D")
                {
                    for (int i = 0; i < ((DataTable)ViewState["InsertRecord"]).Rows.Count; i++)
                    {
                        for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                        {
                            drTL = dtTL.NewRow();
                            drTL[0] = dtTL.Rows.Count + 1;
                            drTL[1] = ((DataTable)ViewState["InsertRecord"]).Rows[i]["V_SealLocation"].ToString();
                            drTL[2] = ((DataTable)ViewState["InsertRecord"]).Rows[i]["V_SealLocationAlias"].ToString();
                            drTL[3] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                            dtTL.Rows.Add(drTL);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < ddlCompartmentType.Items.Count; i++)
                    {
                        for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                        {
                            drTL = dtTL.NewRow();
                            drTL[0] = dtTL.Rows.Count + 1;
                            drTL[1] = ddlCompartmentType.SelectedValue;
                            drTL[2] = ddlCompartmentType.SelectedItem.Text.ToString();
                            drTL[3] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                            dtTL.Rows.Add(drTL);
                        }
                    }
                }
            }

            gvmilkAdulterationtestdetail.DataSource = dtTL;
            gvmilkAdulterationtestdetail.DataBind();

            milktestdetail.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }

    //protected void ddlAdulterationType_Init(object sender, EventArgs e)
    //{
    //    ddlAdulterationType.DataSource = apiprocedure.ByProcedure("USP_Mst_AdulterationTestList",
    //                            new string[] { "flag" },
    //                            new string[] { "1" }, "dataset");
    //    ddlAdulterationType.DataValueField = "V_AdulteratioTName";
    //    ddlAdulterationType.DataTextField = "V_AdulteratioTName";
    //    ddlAdulterationType.DataBind();
    //    ddlAdulterationType.Items.Insert(0, new ListItem("Select", "0"));
    //}

    protected void ddlMilkQuality_Init(object sender, EventArgs e)
    {
        ddlMilkQuality.DataSource = apiprocedure.ByProcedure("USP_Mst_MilkQualityList",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");
        ddlMilkQuality.DataValueField = "V_MilkQualityList";
        ddlMilkQuality.DataTextField = "V_MilkQualityList";
        ddlMilkQuality.DataBind();
        ddlMilkQuality.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        mv_FormWizard.ActiveViewIndex += 1;

        if (mv_FormWizard.ActiveViewIndex > 0)
        {
            btnPrevious.Visible = true;
        }
        else
        {
            btnPrevious.Visible = false;
        }

        if (mv_FormWizard.ActiveViewIndex < 1)
        {
            btnNext.Visible = true;
        }
        else
        {
            btnNext.Visible = false;
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        mv_FormWizard.ActiveViewIndex -= 1;

        if (mv_FormWizard.ActiveViewIndex > 0)
        {
            btnPrevious.Visible = true;
        }
        else
        {
            btnPrevious.Visible = false;
        }

        if (mv_FormWizard.ActiveViewIndex < 1)
        {
            btnNext.Visible = true;
        }
        else
        {
            btnNext.Visible = false;
        }
    }

    private int GetMQualityEntry()
    {
        int MQDCOUNT = 0;

        try
        {
            DataSet GetQualityEntry = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                      new string[] { "flag", "V_ReferenceCode" },
                                      new string[] { "34", ddlchallanno.SelectedItem.Text }, "dataset");

            if (GetQualityEntry != null)
            {
                if (GetQualityEntry.Tables.Count > 0)
                {
                    if (GetQualityEntry.Tables[0].Rows.Count > 0)
                    {
                        MQDCOUNT = GetQualityEntry.Tables[0].Rows.Count;
                    }
                    else
                    {
                        return MQDCOUNT;
                    }
                }
                else
                {
                    return MQDCOUNT;
                }

            }

            return MQDCOUNT;

        }
        catch (Exception)
        {

            return MQDCOUNT;
        }

    }

    public string GetChallanStatus()
    {
        string strtankerlivestatus = "0";

        try
        {
            DataSet dscheckTanker = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "BI_MilkInOutRefID", "V_ReferenceCode", "V_EntryType" },
                               new string[] { "26", ddlReferenceNo.SelectedValue, ddlchallanno.SelectedItem.Text, "In" }, "dataset");

            if (dscheckTanker != null)
            {
                if (dscheckTanker.Tables.Count > 0)
                {
                    if (dscheckTanker.Tables[0].Rows.Count > 0)
                    {
                        strtankerlivestatus = dscheckTanker.Tables[0].Rows[0]["Status"].ToString();

                        return strtankerlivestatus;
                    }
                    else
                    {
                        return strtankerlivestatus;
                    }
                }
                else
                {
                    return strtankerlivestatus;
                }
            }
            else
            {
                return strtankerlivestatus;
            }

        }
        catch (Exception ex)
        {

            return strtankerlivestatus;
        }


    }

    public string MCMS_ChallanRunTime_GrossWeight_Validation()
    {
        string ReferenceGrossWeight = "0";

        try
        {

            DataSet dsReferenceGrossWeight = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "BI_MilkInOutRefID" },
                               new string[] { "29", ddlReferenceNo.SelectedValue }, "dataset");

            if (dsReferenceGrossWeight != null)
            {
                if (dsReferenceGrossWeight.Tables.Count > 0)
                {
                    if (dsReferenceGrossWeight.Tables[0].Rows.Count > 0)
                    {
                        ReferenceGrossWeight = dsReferenceGrossWeight.Tables[0].Rows[0]["Status"].ToString();

                        return ReferenceGrossWeight;
                    }
                    else
                    {
                        return ReferenceGrossWeight;
                    }
                }
                else
                {
                    return ReferenceGrossWeight;
                }
            }
            else
            {
                return ReferenceGrossWeight;
            }

        }
        catch (Exception)
        {

            return ReferenceGrossWeight;
        }
    }

    public string MCMS_RuntimeChallan_In_QC_NetWeight_Validation()
    {
        string ChallanNetWeight = "0";

        try
        {

            DataSet dschallanNetWeight = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "V_ReferenceCode", },
                               new string[] { "32", ddlchallanno.SelectedItem.Text }, "dataset");

            if (dschallanNetWeight != null)
            {
                if (dschallanNetWeight.Tables.Count > 0)
                {
                    if (dschallanNetWeight.Tables[0].Rows.Count > 0)
                    {
                        ChallanNetWeight = dschallanNetWeight.Tables[0].Rows[0]["Status"].ToString();

                        return ChallanNetWeight;
                    }
                    else
                    {
                        return ChallanNetWeight;
                    }
                }
                else
                {
                    return ChallanNetWeight;
                }
            }
            else
            {
                return ChallanNetWeight;
            }

        }
        catch (Exception)
        {

            return ChallanNetWeight;
        }
    }

    private string MCMS_RuntimeChallan_In_QC_ChamberLocation_Validation(string Location)
    {
        string GetChamberLocationStatus = "0";

        try
        {

            DataSet dsChamberLocation = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "V_ReferenceCode", "V_SealLocation" },
                               new string[] { "33", ddlchallanno.SelectedItem.Text, Location }, "dataset");

            if (dsChamberLocation != null)
            {
                if (dsChamberLocation.Tables.Count > 0)
                {
                    if (dsChamberLocation.Tables[0].Rows.Count > 0)
                    {
                        GetChamberLocationStatus = dsChamberLocation.Tables[0].Rows[0]["Status"].ToString();

                        return GetChamberLocationStatus;
                    }
                    else
                    {
                        return GetChamberLocationStatus;
                    }
                }
                else
                {
                    return GetChamberLocationStatus;
                }
            }
            else
            {
                return GetChamberLocationStatus;
            }

        }
        catch (Exception)
        {

            return GetChamberLocationStatus;
        }

    }
      
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";


                // Check Runtime Challan Info

                if (ddlchallanno.SelectedIndex > 0)
                {
                    string strtS = GetChallanStatus();

                    if (strtS == "0")
                    {

                    }
                    else
                    {

                        GetReferenceInfo();
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Challan No - " + ddlchallanno.SelectedItem.Text + " QC Entry already exists!");
                        //ddlReferenceNo_SelectedIndexChanged(sender, e);
                        FillGrid();
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Please select Challan Number.!");
                    return;
                }

                // Check Gross Weight Enter Or Not

                if (ddlReferenceNo.SelectedIndex > 0)
                {
                    string strtR = MCMS_ChallanRunTime_GrossWeight_Validation();

                    if (strtR == "1")
                    {

                    }
                    else
                    {

                        GetReferenceInfo();
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Reference No. " + ddlReferenceNo.SelectedItem.Text + " Gross Weight Not Updated!");
                        //ddlReferenceNo_SelectedIndexChanged(sender, e);
                        FillGrid();
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Please select Reference Number.!");
                    return;
                }

                // Check Net Weight Enter Or Not

                if (ddlReferenceNo.SelectedIndex > 0)
                {
                    string strtR = MCMS_RuntimeChallan_In_QC_NetWeight_Validation();

                    if (strtR == "0")
                    {

                    }
                    else
                    {

                        GetReferenceInfo();
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Challan No. " + ddlchallanno.SelectedItem.Text + " Net Weight Updated SO QC Entry Not a Process!");
                        //ddlReferenceNo_SelectedIndexChanged(sender, e);
                        FillGrid();
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Please select Reference Number.!");
                    return;
                }



                //if (txtTotalSeals.Text != "")
                //{
                //Milk Quality Details
                DataTable dt1 = new DataTable();

                if (ddlTankerType.SelectedValue == "S") // S Mean Single Compartment
                {
                    string alcoholePerS = "";

                    if (ddlAlcohol.SelectedValue == "Negative")
                    {
                        alcoholePerS = ddlAlcohol.SelectedValue + "(" + txtAlcoholperc.Text + "%)";
                    }
                    else if (ddlAlcohol.SelectedValue == "Positive")
                    {
                        alcoholePerS = "Positive";
                    }
                    else
                    {
                        alcoholePerS = "0";
                    }

                    DataRow dr1;
                    dt1.Columns.Add(new DataColumn("I_MilkQuantity", typeof(int)));
                    dt1.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                    dt1.Columns.Add(new DataColumn("D_FAT", typeof(decimal)));
                    dt1.Columns.Add(new DataColumn("D_SNF", typeof(decimal)));
                    dt1.Columns.Add(new DataColumn("D_CLR", typeof(decimal)));
                    dt1.Columns.Add(new DataColumn("V_Temp", typeof(string)));
                    dt1.Columns.Add(new DataColumn("V_Acidity", typeof(string)));
                    dt1.Columns.Add(new DataColumn("V_COB", typeof(string)));
                    dt1.Columns.Add(new DataColumn("V_Alcohol", typeof(string)));
                    dt1.Columns.Add(new DataColumn("V_MBRT", typeof(string)));
                    dt1.Columns.Add(new DataColumn("V_MilkQuality", typeof(string)));

                    dr1 = dt1.NewRow();
                    dr1[0] = "0";
                    dr1[1] = ddlCompartmentType.SelectedValue;
                    dr1[2] = txtFat.Text;
                    dr1[3] = txtSNF.Text;
                    dr1[4] = txtCLR.Text;
                    dr1[5] = txtTemprature.Text;
                    dr1[6] = txtAcidity.Text;
                    dr1[7] = ddlCOB.SelectedValue;
                    dr1[8] = alcoholePerS; //ddlAlcohol.SelectedValue;
                    dr1[9] = txtMBRT.Text == "" ? "0" : txtMBRT.Text;
                    dr1[10] = ddlMilkQuality.SelectedValue;
                    dt1.Rows.Add(dr1);

                }
                else if (ddlTankerType.SelectedValue == "D") // D mean Dual Compartment
                {
                    dt1 = GetMilkQualityDetails();

                    int GetMQStatus = GetMQualityEntry();
                    int GetCEntryMQS = dt1.Rows.Count;
                    if (GetMQStatus != GetCEntryMQS)
                    {
                        lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Challan No -" + ddlchallanno.SelectedItem.Text + " Contain Milk in Front & Rear Chamber. So Kindly Fill Both Chamber Details.");
                        return;
                    }

                }
                //End of Milk Quality Details

                //Begin of Adulteration Details (adulteration fill both compartment cases)

                DataTable dt2 = new DataTable();
                dt2 = GetAdulterationTestDetails();

                //End


                //Check chamber Location For Dual Chamber Existance

                if (dt1.Rows.Count == 2)
                {
                    string srtCHL_F = MCMS_RuntimeChallan_In_QC_ChamberLocation_Validation("F");
                    string srtCHL_R = MCMS_RuntimeChallan_In_QC_ChamberLocation_Validation("R");

                    if (srtCHL_F == "0")
                    {

                    }
                    else
                    {
                        GetReferenceInfo();
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Chamber Location Front Entry Already Exist In dataBase For Challan No - " + ddlchallanno.SelectedItem.Text);
                        //ddlReferenceNo_SelectedIndexChanged(sender, e);
                        FillGrid();
                        return;
                    }

                    if (srtCHL_R == "0")
                    {

                    }
                    else
                    {
                        GetReferenceInfo();
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Chamber Location Rear Entry Already Exist In dataBase For Challan No - " + ddlchallanno.SelectedItem.Text);
                        //ddlReferenceNo_SelectedIndexChanged(sender, e);
                        FillGrid();
                        return;
                    }

                }
                else if (dt1.Rows.Count == 1)
                {

                    string srtCHL = MCMS_RuntimeChallan_In_QC_ChamberLocation_Validation(ddlCompartmentType.SelectedValue);

                    if (srtCHL == "0")
                    {

                    }
                    else
                    {
                        GetReferenceInfo();
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Chamber Location Front Entry Already Exist In dataBase For Challan No - " + ddlchallanno.SelectedItem.Text);
                        //ddlReferenceNo_SelectedIndexChanged(sender, e);
                        FillGrid();
                        return;
                    }

                }

                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Something Went Wrong Please Try Again Later");
                    return;
                }

                //return;


                //Seal Details
                //int sealCount = Convert.ToInt32(txtTotalSeals.Text);

                //DataTable dt = new DataTable();
                //DataRow dr;

                //dt.Columns.Add(new DataColumn("SealNo", typeof(string)));
                //dt.Columns.Add(new DataColumn("SealLocation", typeof(string)));
                //dt.Columns.Add(new DataColumn("SealRemark", typeof(string)));

                //if (ddlTankerType.SelectedValue == "D")
                //{
                //    if (dt1.Rows.Count == 2)
                //    {
                //        sealCount += 2;
                //    }
                //}
                //else if (ddlTankerType.SelectedValue == "S")
                //{
                //    sealCount += 2;
                //}

                //for (int i = 0; i < sealCount; i++)
                //{
                //    if (Request.Form["SealNo" + Convert.ToString(i + 1)] != "")
                //    {
                //        dr = dt.NewRow();
                //        dr[0] = Request.Form["SealNo" + Convert.ToString(i + 1)];
                //        dr[1] = Request.Form["SealLocation" + Convert.ToString(i + 1)];
                //        dr[2] = Request.Form["SealRemark" + Convert.ToString(i + 1)];
                //        dt.Rows.Add(dr);
                //    }
                //}
                //End of Seal Details

                //Milk Sample Details


                DataTable dt3 = new DataTable();
                dt3 = GetMilkSampleDetails();
                //End of Milk Sample

                if (dt1.Rows.Count > 0) //Check quality details filled atleast 1 row
                {
                    if (dt3.Rows.Count > 0) //Check Milk Sample details filled atleast 1 row
                    {
                        int AdType = 9;
                        if (ddlTankerType.SelectedValue == "D")
                        {
                            if (ViewState["InsertRecord"] != null)
                            {
                                if (((DataTable)ViewState["InsertRecord"]).Select("V_SealLocation = 'F'").Length > 0 && ((DataTable)ViewState["InsertRecord"]).Select("V_SealLocation = 'R'").Length > 0)
                                {
                                    AdType = (AdType * 2);
                                }
                            }
                        }

                        DateTime ADate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(txtArrivalDate.Text, cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(txtArrivalTime.Text, cult).ToString("hh:mm:ss tt")), cult);

                        if (dt2.Rows.Count == AdType) //Check adulteration details filled all row
                        {
                            //if ((dt.Select("SealLocation = 'S'").Length > 0 && ddlTankerType.SelectedValue == "S")
                            //         ||
                            //         (dt.Select("SealLocation = 'F'").Length > 0 || dt.Select("SealLocation = 'R'").Length > 0 && ddlTankerType.SelectedValue == "D")) //Check seal details filled atleast 8 row
                            //{

                            ds = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                    new string[] { "Flag"
                                                ,"Office_ID"
                                                ,"OfficeType_ID"
                                                ,"BI_MilkInOutRefID"
                                                ,"DT_Date"
                                                ,"V_ReferenceCode"
                                                ,"V_TankerType"
                                                ,"V_VehicleNo"
                                                ,"V_EntryType"
                                                ,"DT_ArrivalDateTime"
                                                ,"DT_DispatchDateTime"
                                                ,"V_DriverName"
                                                ,"V_DriverMobileNo"
                                                ,"V_RepresentativeName"
                                                ,"V_RepresentativeMobileNo"
                                                ,"V_MilkType"
                                                ,"I_CreatedByEmpID"
                                                ,"V_Remark"
                                                ,"D_ClosingBalance"
                                                ,"DT_NextTankerDate"
                                                ,"V_EntryFrom"
                                                ,"D_TankerCapacity"
                                                ,"I_TankerCount"
                                                ,"V_Shift"
                                    },
                                    new string[] { "2" ,
                                                apiprocedure.Office_ID(),
                                                apiprocedure.OfficeType_ID(),
                                                ddlReferenceNo.SelectedValue,
                                                Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"),
                                                ddlchallanno.SelectedItem.Text,
                                                ddlTankerType.SelectedValue,
                                                txtVehicleNo.Text,
                                                "In",
                                                ADate.ToString("yyyy/MM/dd hh:mm:ss tt"),
                                                "",
                                                txtDriverName.Text,
                                                txtDriverMobileNo.Text,
                                                txtRepresentativeName.Text,
                                                txtRepresentativeMobileNo.Text,
                                                "Raw",
                                                apiprocedure.createdBy(),
                                                "Tanker Received Entry from WEB"
                                                ,txtClosingBalanceAfterDispatch.Text
                                                ,txtNextTankerRequiredDate.Text == "" ? "" : Convert.ToDateTime(txtNextTankerRequiredDate.Text, cult).ToString("yyyy/MM/dd")
                                                ,"Web"
                                                ,txtTankerCapacity.Text == "" ? "0.00" : txtTankerCapacity.Text
                                                ,txtTankerCount.Text
                                                ,ddlNextTankerShift.SelectedValue
                                    },
                                   new string[] { "type_Trn_MilkQualityDetails", "type_Trn_tblAdulterationTest", "type_Trn_MilkSampleDetail" },
                                   new DataTable[] { dt1, dt2, dt3 }, "TableSave");

                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                Session["IsSuccess"] = true;

                                //CheckQCDifference(ds.Tables[0].Rows[0]["ReferenceCode"].ToString(), Session["MobileNo"].ToString(), ddlUnitName.SelectedItem.Text, txtVehicleNo.Text);
                                //Session["ReferenceCode"] = ds.Tables[0].Rows[0]["ReferenceCode"].ToString();
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                                Response.Redirect("ReceiveTankerChallan.aspx", false);
                                //lblMsg.Text = apiprocedure.Alert("fa-check", "alert-success", "Thank You!", "Record Inserted Successfully");
                                //FillGrid();
                            }
                            else
                            {
                                lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                Session["IsSuccess"] = false;
                            }
                            //}
                            //else
                            //{
                            //    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : First to fill atleast 1 compartment seals and 1 valve seal details!");
                            //    Session["IsSuccess"] = false;
                            //}
                        }
                        else
                        {
                            lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : Required All Adulteration Test!");
                            Session["IsSuccess"] = false;
                        }
                    }
                    else
                    {
                        lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : First to fill atleast 1 milk sample detail!");
                        Session["IsSuccess"] = false;
                    }
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : First to fill atleast 1 milk quality detail!");
                    Session["IsSuccess"] = false;
                }
                //}
                //else
                //{
                //    lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "Kindly enter total no. of seals first.");
                //    Session["IsSuccess"] = false;
                //}

                //Get After run code token for denied page refresh activity
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
            Session["IsSuccess"] = false;
        }
    }


    protected void ddlAlcohol_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtAlcoholperc.Text = "";

            if (ddlAlcohol.SelectedValue == "0")
            {
                DivAlcoholper.Visible = false;
            }

            if (ddlAlcohol.SelectedValue == "Positive")
            {
                DivAlcoholper.Visible = false;
            }

            if (ddlAlcohol.SelectedValue == "Negative")
            {
                DivAlcoholper.Visible = true;
            }
        }
        catch (Exception ex)
        {
            DivAlcoholper.Visible = false;
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
	
	protected void Timer1_Tick(object sender, EventArgs e)
    {
        try
        {
			  
            DataSet NotificationDS = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                                      new string[] { "flag", "I_OfficeID", "V_EntryType" },
                                      new string[] { "8", apiprocedure.Office_ID(), "Out" }, "dataset");

            if (NotificationDS.Tables[0].Rows.Count != 0)
            {

                if (NotificationDS != null)
                {
					
                    lblNotificationCount_Top.Text = NotificationDS.Tables[0].Rows.Count.ToString();
                    lblNotificationCount.Text = NotificationDS.Tables[0].Rows.Count.ToString();
                    Repeater1.DataSource = NotificationDS;
                    Repeater1.DataBind();
                }
                else
                {
                    lblNotificationCount_Top.Text = "0";
                    lblNotificationCount.Text = "0";
                    Repeater1.DataSource = string.Empty;
                    Repeater1.DataBind();
                }

            }
            else
            {
                lblNotificationCount_Top.Text = "0";
                lblNotificationCount.Text = "0";
                Repeater1.DataSource = string.Empty;
                Repeater1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


}