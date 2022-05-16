using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_CCDS_MilkInwardEntry : System.Web.UI.Page
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

                FillGrid();
                //txtDispatchTime.Text = DateTime.Now.ToString("hh:mm tt");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtArrivalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }

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
            gvReceivedEntry.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "Office_ID", "V_EntryType" },
                               new string[] { "7", apiprocedure.Office_ID(), "In" }, "dataset");
            gvReceivedEntry.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtTotalSeals.Text != "")
        {
            StringBuilder html = new StringBuilder();

            int sealCount = Convert.ToInt32(txtTotalSeals.Text);
            int j = sealCount;

            if (ddlTankerType.SelectedValue == "S")
            {
                for (int i = 0; i < sealCount; i++)
                {
                    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(i + 1) + "' name='SealNo" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(i + 1) + "' name='SealLocation" + Convert.ToString(i + 1) + "'");
                    html.Append(" readonly='readonly'><option Value='S'>S</option></select></td>");
                    html.Append("<td><input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(i + 1) + "' name='SealRemark" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                }

                html.Append("<tr><th colspan='3' style='text-align:center;'>Valve Box</th></tr><tr><th>Seal No.</th><th>Seal Location</th><th>Seal Remark</th></tr>");

                for (int i = 0; i < 2; i++)
                {
                    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(j + 1) + "' name='SealNo" + Convert.ToString(j + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(j + 1) + "' name='SealLocation" + Convert.ToString(j + 1) + "'");
                    html.Append(" readonly='readonly'><option Value='VB'>VB</option></select></td>");
                    html.Append("<td><input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(j + 1) + "' name='SealRemark" + Convert.ToString(j + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                    j++;
                }

                //Enable Required Validations
                rfvCompartmentType_S.Enabled = true;
                rfvMilkQuality_S.Enabled = true;
                rfvMilkQuantity_S.Enabled = true;
                rfvTemprature_S.Enabled = true;
                rfvAcidity_S.Enabled = true;
                rfvCOB_S.Enabled = true;
                rfvFat_S.Enabled = true;
                rfvSNF_S.Enabled = true;
                rfvCLR_S.Enabled = true;
                //End

                //Enable regular expression Validations
                revMilkQuantity_S.Enabled = true;
                revTemprature_S.Enabled = true;
                revAcidity_S.Enabled = true;
                revFat_S.Enabled = true;
                revSNF_S.Enabled = true;
                revCLR_S.Enabled = true;
                revMBRT_S.Enabled = true;
                //End

                rowSealDetails.Style.Add("display", "block");
                btnSubmit.Visible = true;
            }
            else if (ddlTankerType.SelectedValue == "D")
            {
                for (int i = 0; i < sealCount; i++)
                {
                    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(i + 1) + "' name='SealNo" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(i + 1) + "' name='SealLocation" + Convert.ToString(i + 1) + "'");
                    html.Append("><option Value='F'>F</option><option Value='R'>R</option></select></td>");
                    html.Append("<td><input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(i + 1) + "' name='SealRemark" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                }

                if (ViewState["InsertRecord"] != null)
                {
                    if (((DataTable)ViewState["InsertRecord"]).Rows.Count == 2)
                    {
                        html.Append("<tr><th colspan='3' style='text-align:center;'>Valve Box</th></tr><tr><th>Seal No.</th><th>Seal Location</th><th>Seal Remark</th></tr>");

                        for (int i = 0; i < 2; i++)
                        {
                            html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(j + 1) + "' name='SealNo" + Convert.ToString(j + 1) + "'");
                            html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(j + 1) + "' name='SealLocation" + Convert.ToString(j + 1) + "'");
                            html.Append(" readonly='readonly'><option Value='VB'>VB</option></select></td>");
                            html.Append("<td><input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(j + 1) + "' name='SealRemark" + Convert.ToString(j + 1) + "'");
                            html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                            j++;
                        }
                    }
                }

                //Enable Required Validations
                rfvCompartmentType_S.Enabled = false;
                rfvMilkQuality_S.Enabled = false;
                rfvMilkQuantity_S.Enabled = false;
                rfvTemprature_S.Enabled = false;
                rfvAcidity_S.Enabled = false;
                rfvCOB_S.Enabled = false;
                rfvFat_S.Enabled = false;
                rfvSNF_S.Enabled = false;
                rfvCLR_S.Enabled = false;
                //End

                //Enable regular expression Validations
                revMilkQuantity_S.Enabled = false;
                revTemprature_S.Enabled = false;
                revAcidity_S.Enabled = false;
                revFat_S.Enabled = false;
                revSNF_S.Enabled = false;
                revCLR_S.Enabled = false;
                revMBRT_S.Enabled = false;
                //End

                rowSealDetails.Style.Add("display", "block");
                btnSubmit.Visible = true;
            }
            else
            {
                lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Select Tanker Type first!");
                ddlTankerType.Focus();

                //Enable Required Validations
                rfvCompartmentType_S.Enabled = false;
                rfvMilkQuality_S.Enabled = false;
                rfvMilkQuantity_S.Enabled = false;
                rfvTemprature_S.Enabled = false;
                rfvAcidity_S.Enabled = false;
                rfvCOB_S.Enabled = false;
                rfvFat_S.Enabled = false;
                rfvSNF_S.Enabled = false;
                rfvCLR_S.Enabled = false;
                //End

                //Enable regular expression Validations
                revMilkQuantity_S.Enabled = false;
                revTemprature_S.Enabled = false;
                revAcidity_S.Enabled = false;
                revFat_S.Enabled = false;
                revSNF_S.Enabled = false;
                revCLR_S.Enabled = false;
                revMBRT_S.Enabled = false;
                //End
            }

            dvSealDetails.InnerHtml = html.ToString();
        }
        else
        {
            lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "Enter No. Of Seal first!");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";
                if (txtTotalSeals.Text != "")
                {
                    //Milk Quality Details
                    DataTable dt1 = new DataTable();

                    if (ddlTankerType.SelectedValue == "S") // S Mean Single Compartment
                    {
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
                        dr1[0] = txtMilkQuantity.Text;
                        dr1[1] = ddlCompartmentType.SelectedValue;
                        dr1[2] = txtFat.Text;
                        dr1[3] = txtSNF.Text;
                        dr1[4] = txtCLR.Text;
                        dr1[5] = txtTemprature.Text;
                        dr1[6] = txtAcidity.Text;
                        dr1[7] = ddlCOB.SelectedValue;
                        dr1[8] = txtAlcohol.Text == "" ? "0" : txtAlcohol.Text;
                        dr1[9] = txtMBRT.Text == "" ? "0" : txtMBRT.Text;
                        dr1[10] = ddlMilkQuality.SelectedValue;
                        dt1.Rows.Add(dr1);
                    }
                    else if (ddlTankerType.SelectedValue == "D") // D mean Dual Compartment
                    {
                        dt1 = GetMilkQualityDetails();
                    }
                    //End of Milk Quality Details

                    //Begin of Adulteration Details (adulteration fill both compartment cases)

                    DataTable dt2 = new DataTable();
                    dt2 = GetAdulterationTestDetails();

                    //End

                    //Seal Details
                    int sealCount = Convert.ToInt32(txtTotalSeals.Text);

                    DataTable dt = new DataTable();
                    DataRow dr;

                    dt.Columns.Add(new DataColumn("SealNo", typeof(string)));
                    dt.Columns.Add(new DataColumn("SealLocation", typeof(string)));
                    dt.Columns.Add(new DataColumn("SealRemark", typeof(string)));

                    if (ddlTankerType.SelectedValue == "D")
                    {
                        if (dt1.Rows.Count == 2)
                        {
                            sealCount += 2;
                        }
                    }
                    else if (ddlTankerType.SelectedValue == "S")
                    {
                        sealCount += 2;
                    }

                    for (int i = 0; i < sealCount; i++)
                    {
                        if (Request.Form["SealNo" + Convert.ToString(i + 1)] != "")
                        {
                            dr = dt.NewRow();
                            dr[0] = Request.Form["SealNo" + Convert.ToString(i + 1)];
                            dr[1] = Request.Form["SealLocation" + Convert.ToString(i + 1)];
                            dr[2] = Request.Form["SealRemark" + Convert.ToString(i + 1)];
                            dt.Rows.Add(dr);
                        }
                    }
                    //End of Seal Details

                    //Milk Sample Details
                    DataTable dt3 = new DataTable();
                    dt3 = GetMilkSampleDetails();
                    //End of Milk Sample

                    if (dt1.Rows.Count > 0) //Check quality details filled atleast 1 row
                    {
                        if (dt3.Rows.Count > 0) //Check Milk Sample details filled atleast 1 row
                        {
                            int AdType = 0;
                            if (ddlTankerType.SelectedValue == "S")
                            {
                                AdType = ddlAdulterationType.Items.Count - 1;
                            }
                            else if (ddlTankerType.SelectedValue == "D")
                            {
                                AdType = (ddlAdulterationType.Items.Count * 2) - 2;
                            }

                            DateTime ADate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(txtArrivalDate.Text, cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(txtArrivalTime.Text, cult).ToString("hh:mm:ss tt")), cult);

                            if (dt2.Rows.Count == AdType) //Check adulteration details filled all row
                            {
                                if ((dt.Select("SealLocation = 'S'").Length > 0 && dt.Select("SealLocation = 'VB'").Length > 0 && ddlTankerType.SelectedValue == "S")
                                    ||
                                    (dt.Select("SealLocation = 'F'").Length > 0 && dt.Select("SealLocation = 'R'").Length > 0 && dt.Select("SealLocation = 'VB'").Length > 0 && ddlTankerType.SelectedValue == "D"))
                                {

                                    ds = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                            new string[] { "Flag"
                                                ,"Office_ID"
                                                ,"OfficeType_ID"
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
                                                Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"),
                                                ddlReferenceNo.SelectedValue,
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
                                           new string[] { "type_Trn_TankerSealDetails", 
                                        "type_Trn_MilkQualityDetails", "type_Trn_tblAdulterationTest", "type_Trn_MilkSampleDetail" },
                                           new DataTable[] { dt, 
                                        dt1, dt2, dt3  }, "TableSave");

                                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                    {
                                        Session["IsSuccess"] = true;

                                        //CheckQCDifference(ds.Tables[0].Rows[0]["ReferenceCode"].ToString(), Session["MobileNo"].ToString(), ddlUnitName.SelectedItem.Text, txtVehicleNo.Text);
                                        //Session["ReferenceCode"] = ds.Tables[0].Rows[0]["ReferenceCode"].ToString();
                                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                                        Response.Redirect("MilkInwardEntry.aspx", false);
                                        //lblMsg.Text = apiprocedure.Alert("fa-check", "alert-success", "Thank You!", "Record Inserted Successfully");
                                        //FillGrid();
                                    }
                                    else
                                    {
                                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                        Session["IsSuccess"] = false;
                                    }
                                }
                                else
                                {
                                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : First to fill atleast 1 compartment seals and 1 valve seal details!");
                                    Session["IsSuccess"] = false;
                                }
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
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "Kindly enter total no. of seals first.");
                    Session["IsSuccess"] = false;
                }

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

            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["InsertRecord"] as DataTable;
            dt.Rows.Remove(dt.Rows[row.RowIndex]);
            ViewState["InsertRecord"] = dt;
            gv_MilkQualityDetail.DataSource = dt;
            gv_MilkQualityDetail.DataBind();

            //For clear record for add child record
            txtMilkQuantity.Text = "";
            ddlCompartmentType.ClearSelection();
            txtFat.Text = "";
            txtSNF.Text = "";
            txtCLR.Text = "";
            txtTemprature.Text = "";
            txtAcidity.Text = "";
            ddlCOB.ClearSelection();
            txtMBRT.Text = "";
            txtMilkQuantity.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }

    protected void ddlTankerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        dvSealDetails.InnerHtml = string.Empty;
        rowSealDetails.Style.Add("display", "none");
        btnSubmit.Visible = false;

        if (ddlTankerType.SelectedIndex != 0)
        {
            if (ddlTankerType.SelectedValue == "S")
            {
                txtTotalSeals.Text = "1";

                //Range validator setting
                rvTotalSeals.MinimumValue = "1";

                ddlCompartmentType.Items.Clear();
                ddlCompartmentType.Items.Add(new ListItem("Single", "S"));
                ddlCompartmentType.Enabled = false;

                //Hide Milk Quality detail add multi time
                dv_gvMilkQualityDeailsAddButton.Visible = false;
                dv_gvMilkQualityDeails.Visible = false;

                //Adulteration section Visible=true when change Tanker type
                dv_CompartmentTypeforAdulteration.Visible = true;
                dv_AdulterationType.Visible = true;
                dv_AdulterationValue.Visible = true;
                dv_btnAdulterationTest.Visible = true;
                dv_AdulterationHeaderSection.Visible = true;
                dv_AdulterationTestGridVIew.Visible = true;

                //show Add generate seal entry control when Single Compartment tanker
                btnAdd.Visible = false;

                //Disable required field validator in add quality details setction
                rfvMilkQuality_S.Enabled = true;
                rfvMilkQuantity_S.Enabled = true;
                rfvTemprature_S.Enabled = true;
                rfvAcidity_S.Enabled = true;
                rfvCOB_S.Enabled = true;
                rfvFat_S.Enabled = true;
                rfvSNF_S.Enabled = true;
                rfvCLR_S.Enabled = true;

                //Disable regular expression validator in add quality details setction
                revMilkQuantity_S.Enabled = true;
                revTemprature_S.Enabled = true;
                revAcidity_S.Enabled = true;
                revFat_S.Enabled = true;
                revSNF_S.Enabled = true;
                revCLR_S.Enabled = true;
                revMBRT_S.Enabled = true;

                //Disable required field validator in add quality details setction
                rfvMilkQuality.Enabled = false;
                rfvMilkQuantity.Enabled = false;
                rfvTemprature.Enabled = false;
                rfvAcidity.Enabled = false;
                rfvCOB.Enabled = false;
                rfvFat.Enabled = false;
                rfvSNF.Enabled = false;
                rfvCLR.Enabled = false;

                //Disable regular expression validator in add quality details setction
                revMilkQuantity.Enabled = false;
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
                txtTotalSeals.Text = "2";

                //Range validator setting
                rvTotalSeals.MinimumValue = "2";

                ddlCompartmentType.Items.Clear();
                ddlCompartmentType.Items.Insert(0, new ListItem("Select", "0"));
                ddlCompartmentType.Items.Add(new ListItem("Front", "F"));
                ddlCompartmentType.Items.Add(new ListItem("Rear", "R"));
                ddlCompartmentType.Enabled = true;

                //Show Milk Quality detail add multi time
                dv_gvMilkQualityDeailsAddButton.Visible = true;
                dv_gvMilkQualityDeails.Visible = true;

                //Adulteration section Visible=false when change Tanker type
                dv_CompartmentTypeforAdulteration.Visible = false;
                dv_AdulterationType.Visible = false;
                dv_AdulterationValue.Visible = false;
                dv_btnAdulterationTest.Visible = false;
                dv_AdulterationHeaderSection.Visible = false;
                dv_AdulterationTestGridVIew.Visible = false;

                //hide Add generate seal entry control when Single Compartment tanker
                btnAdd.Visible = false;

                //Disable required field validator in add quality details setction
                rfvMilkQuality_S.Enabled = false;
                rfvMilkQuantity_S.Enabled = false;
                rfvTemprature_S.Enabled = false;
                rfvAcidity_S.Enabled = false;
                rfvCOB_S.Enabled = false;
                rfvFat_S.Enabled = false;
                rfvSNF_S.Enabled = false;
                rfvCLR_S.Enabled = false;

                //Disable regular expression validator in add quality details setction
                revMilkQuantity_S.Enabled = false;
                revTemprature_S.Enabled = false;
                revAcidity_S.Enabled = false;
                revFat_S.Enabled = false;
                revSNF_S.Enabled = false;
                revCLR_S.Enabled = false;
                revMBRT_S.Enabled = false;

                //Disable required field validator in add quality details setction
                rfvMilkQuality.Enabled = true;
                rfvMilkQuantity.Enabled = true;
                rfvTemprature.Enabled = true;
                rfvAcidity.Enabled = true;
                rfvCOB.Enabled = true;
                rfvFat.Enabled = true;
                rfvSNF.Enabled = true;
                rfvCLR.Enabled = true;

                //Disable regular expression validator in add quality details setction
                revMilkQuantity.Enabled = true;
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
            txtTotalSeals.Text = "2";

            //Range validator setting
            rvTotalSeals.MinimumValue = "2";

            ddlCompartmentType.Items.Clear();
            ddlCompartmentType.Items.Insert(0, new ListItem("Select", "0"));
            ddlCompartmentType.Enabled = true;

            //Hide Milk Quality detail add multi time
            dv_gvMilkQualityDeailsAddButton.Visible = false;
            dv_gvMilkQualityDeails.Visible = false;

            //Adulteration section Visible=false when change Tanker type
            dv_CompartmentTypeforAdulteration.Visible = false;
            dv_AdulterationType.Visible = false;
            dv_AdulterationValue.Visible = false;
            dv_btnAdulterationTest.Visible = false;
            dv_AdulterationHeaderSection.Visible = false;
            dv_AdulterationTestGridVIew.Visible = false;

            //hide Add generate seal entry control when selected index = 0
            btnAdd.Visible = false;

            //Disable required field validator in add quality details setction
            rfvMilkQuality_S.Enabled = true;
            rfvMilkQuantity_S.Enabled = true;
            rfvTemprature_S.Enabled = true;
            rfvAcidity_S.Enabled = true;
            rfvCOB_S.Enabled = true;
            rfvFat_S.Enabled = true;
            rfvSNF_S.Enabled = true;
            rfvCLR_S.Enabled = true;

            //Disable regular expression validator in add quality details setction
            revMilkQuantity_S.Enabled = true;
            revTemprature_S.Enabled = true;
            revAcidity_S.Enabled = true;
            revFat_S.Enabled = true;
            revSNF_S.Enabled = true;
            revCLR_S.Enabled = true;
            revMBRT_S.Enabled = true;

            //Disable required field validator in add quality details setction
            rfvMilkQuality.Enabled = false;
            rfvMilkQuantity.Enabled = false;
            rfvTemprature.Enabled = false;
            rfvAcidity.Enabled = false;
            rfvCOB.Enabled = false;
            rfvFat.Enabled = false;
            rfvSNF.Enabled = false;
            rfvCLR.Enabled = false;

            //Disable regular expression validator in add quality details setction
            revMilkQuantity.Enabled = false;
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
            Label lblMilkQuantity = (Label)row.FindControl("lblMilkQuantity");
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
            dr[0] = Convert.ToInt32(lblMilkQuantity.Text);
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

            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(int)));
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
                dr[1] = txtMilkQuantity.Text;
                dr[2] = ddlCompartmentType.SelectedValue;
                dr[3] = ddlCompartmentType.SelectedItem.Text;
                dr[4] = txtFat.Text;
                dr[5] = txtSNF.Text;
                dr[6] = txtCLR.Text;
                dr[7] = txtTemprature.Text;
                dr[8] = txtAcidity.Text;
                dr[9] = txtAlcohol.Text == "" ? "0" : txtAlcohol.Text;
                dr[10] = ddlCOB.SelectedValue;
                dr[11] = txtMBRT.Text == "" ? "0" : txtMBRT.Text;
                dr[12] = ddlMilkQuality.SelectedValue;
                dt.Rows.Add(dr);

                ViewState["InsertRecord"] = dt;
                gv_MilkQualityDetail.DataSource = dt;
                gv_MilkQualityDetail.DataBind();

                ddlCompartmentTypeforQuality.DataSource = dt;
                ddlCompartmentTypeforQuality.DataTextField = "V_SealLocationAlias";
                ddlCompartmentTypeforQuality.DataValueField = "V_SealLocation";
                ddlCompartmentTypeforQuality.DataBind();
                ddlCompartmentTypeforQuality.Items.Insert(0, new ListItem("Select", "0"));


                //Adulteration Test details
                dv_CompartmentTypeforAdulteration.Visible = true;
                dv_AdulterationType.Visible = true;
                dv_AdulterationValue.Visible = true;
                dv_btnAdulterationTest.Visible = true;
                dv_AdulterationHeaderSection.Visible = true;
                dv_AdulterationTestGridVIew.Visible = true;


                ddlChamberOfSampleNo.DataSource = dt;
                ddlChamberOfSampleNo.DataTextField = "V_SealLocationAlias";
                ddlChamberOfSampleNo.DataValueField = "V_SealLocation";
                ddlChamberOfSampleNo.DataBind();
                ddlChamberOfSampleNo.Items.Insert(0, new ListItem("Select", "0"));

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
                dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(int)));
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
                    dr[0] = dt.Rows.Count + 1;
                    dr[1] = txtMilkQuantity.Text;
                    dr[2] = ddlCompartmentType.SelectedValue;
                    dr[3] = ddlCompartmentType.SelectedItem.Text;
                    dr[4] = txtFat.Text;
                    dr[5] = txtSNF.Text;
                    dr[6] = txtCLR.Text;
                    dr[7] = txtTemprature.Text;
                    dr[8] = txtAcidity.Text;
                    dr[9] = txtAlcohol.Text == "" ? "0" : txtAlcohol.Text;
                    dr[10] = ddlCOB.SelectedValue;
                    dr[11] = txtMBRT.Text == "" ? "0" : txtMBRT.Text;
                    dr[12] = ddlMilkQuality.SelectedValue;
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
                ddlCompartmentTypeforQuality.DataSource = dt;
                ddlCompartmentTypeforQuality.DataTextField = "V_SealLocationAlias";
                ddlCompartmentTypeforQuality.DataValueField = "V_SealLocation";
                ddlCompartmentTypeforQuality.DataBind();
                ddlCompartmentTypeforQuality.Items.Insert(0, new ListItem("Select", "0"));

                dv_CompartmentTypeforAdulteration.Visible = true;
                dv_AdulterationType.Visible = true;
                dv_AdulterationValue.Visible = true;
                dv_btnAdulterationTest.Visible = true;
                dv_AdulterationHeaderSection.Visible = true;
                dv_AdulterationTestGridVIew.Visible = true;

                //Milk Sample Details
                ddlChamberOfSampleNo.DataSource = dt;
                ddlChamberOfSampleNo.DataTextField = "V_SealLocationAlias";
                ddlChamberOfSampleNo.DataValueField = "V_SealLocation";
                ddlChamberOfSampleNo.DataBind();
                ddlChamberOfSampleNo.Items.Insert(0, new ListItem("Select", "0"));

                dv_MilkSampleDetails.Visible = true;
                dv_ChamberOfSampleDetails.Visible = true;
                dv_SampleNo.Visible = true;
                dv_SampleRemark.Visible = true;
                dv_SampleNoGrid.Visible = true;
                dv_gv_sampleNoDetails.Visible = true;
            }

            //Clear Record
            txtMilkQuantity.Text = "";
            ddlCompartmentType.ClearSelection();
            ddlMilkQuality.ClearSelection();
            txtFat.Text = "";
            txtSNF.Text = "";
            txtCLR.Text = "";
            txtTemprature.Text = "";
            txtAcidity.Text = "";
            ddlCOB.ClearSelection();
            txtMBRT.Text = "";
            txtMilkQuantity.Text = "";
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

        foreach (GridViewRow row in gv_AdulterationTestDetails.Rows)
        {
            Label lblSealLocation = (Label)row.FindControl("lblSealLocation");
            Label lblAdulterationType = (Label)row.FindControl("lblAdulterationType");
            Label lblAdulterationValue = (Label)row.FindControl("lblAdulterationValue");

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
            dr[2] = lblAdulterationValue.Text;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void AddAdulterationTestDetails()
    {
        try
        {
            int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord1"]) == null || Convert.ToString(ViewState["InsertRecord1"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dt.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
                dt.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));

                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlCompartmentTypeforQuality.SelectedValue;
                dr[2] = ddlCompartmentTypeforQuality.SelectedItem.Text;
                dr[3] = ddlAdulterationType.SelectedValue;
                dr[4] = ddlAdulterationValue.SelectedValue;
                dt.Rows.Add(dr);

                ViewState["InsertRecord1"] = dt;
                gv_AdulterationTestDetails.DataSource = dt;
                gv_AdulterationTestDetails.DataBind();

            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dt.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
                dt.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));

                DT = (DataTable)ViewState["InsertRecord1"];
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlCompartmentTypeforQuality.SelectedValue == DT.Rows[i]["V_SealLocation"].ToString() && ddlAdulterationType.SelectedValue == DT.Rows[i]["V_AdulterationType"].ToString())
                    {
                        CompartmentType = 1;
                    }
                }
                if (CompartmentType == 1)
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Compartment of \"" + ddlCompartmentTypeforQuality.SelectedItem.Text + "\" Adulteration Type and " + ddlAdulterationType.SelectedValue + " already exist.");
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = dt.Rows.Count + 1;
                    dr[1] = ddlCompartmentTypeforQuality.SelectedValue;
                    dr[2] = ddlCompartmentTypeforQuality.SelectedItem.Text;
                    dr[3] = ddlAdulterationType.SelectedValue;
                    dr[4] = ddlAdulterationValue.SelectedValue;
                    dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord1"] = dt;
                gv_AdulterationTestDetails.DataSource = dt;
                gv_AdulterationTestDetails.DataBind();

            }

            //Clear Record
            ddlCompartmentTypeforQuality.ClearSelection();
            ddlAdulterationType.ClearSelection();
            ddlAdulterationValue.ClearSelection();

            if ((((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'F'").Length > 0 && ((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'R'").Length > 0 && ddlTankerType.SelectedValue == "D") || (((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'S'").Length > 0 && ddlTankerType.SelectedValue == "S"))
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    protected void btnAdulterationTest_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddAdulterationTestDetails();
        ddlCompartmentTypeforQuality.Focus();
    }

    protected void lnkAdulterationDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["InsertRecord1"] as DataTable;
            dt.Rows.Remove(dt.Rows[row.RowIndex]);
            ViewState["InsertRecord1"] = dt;
            gv_AdulterationTestDetails.DataSource = dt;
            gv_AdulterationTestDetails.DataBind();

            //For clear record for add child record
            ddlCompartmentTypeforQuality.ClearSelection();
            ddlAdulterationType.ClearSelection();
            ddlAdulterationValue.ClearSelection();

            if ((((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'F'").Length > 0 && ((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'R'").Length > 0 && ddlTankerType.SelectedValue == "D") || (((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'S'").Length > 0 && ddlTankerType.SelectedValue == "S"))
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }

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

    protected void ddlReferenceNo_Init(object sender, EventArgs e)
    {
        ddlReferenceNo.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "Office_ID" },
                               new string[] { "11", apiprocedure.Office_ID() }, "dataset");
        ddlReferenceNo.DataTextField = "ReferenceCodeUnitName";
        ddlReferenceNo.DataValueField = "V_ReferenceCode";
        ddlReferenceNo.DataBind();
        ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void ddlReferenceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReferenceNo.SelectedIndex != 0)
            {
                ds = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "V_ReferenceCode", "Office_ID" },
                               new string[] { "10", ddlReferenceNo.SelectedValue, apiprocedure.Office_ID() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        dv_TankerType.Visible = true;

                        ddlUnitName.SelectedValue = ds.Tables[0].Rows[0]["I_OfficeID"].ToString();
                        txtClosingBalanceAfterDispatch.Text = ds.Tables[0].Rows[0]["D_ClosingBalance"].ToString();
                        if (ds.Tables[0].Rows[0]["DT_NextTankerDate"] != System.DBNull.Value)
                        {
                            txtNextTankerRequiredDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_NextTankerDate"].ToString(), cult).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            txtNextTankerRequiredDate.Text = "";
                        }
                        txtVehicleNo.Text = ds.Tables[0].Rows[0]["V_VehicleNo"].ToString();
                        txtDriverName.Text = ds.Tables[0].Rows[0]["V_DriverName"].ToString();
                        txtDriverMobileNo.Text = ds.Tables[0].Rows[0]["V_DriverMobileNo"].ToString();
                        txtRepresentativeName.Text = ds.Tables[0].Rows[0]["V_RepresentativeName"].ToString();
                        txtRepresentativeMobileNo.Text = ds.Tables[0].Rows[0]["V_RepresentativeMobileNo"].ToString();
                        ddlTankerType.SelectedValue = ds.Tables[0].Rows[0]["V_TankerType"].ToString();

                        txtTankerCapacity.Text = ds.Tables[0].Rows[0]["D_TankerCapacity"].ToString();
                        txtTankerCount.Text = ds.Tables[0].Rows[0]["I_TankerCount"].ToString();

                        if (ds.Tables[0].Rows[0]["V_Shift"] != System.DBNull.Value)
                        {
                            if (ds.Tables[0].Rows[0]["V_Shift"].ToString() != "" && ds.Tables[0].Rows[0]["V_Shift"].ToString() != "0" && ds.Tables[0].Rows[0]["V_Shift"].ToString() != "0.00")
                            {
                                ddlNextTankerShift.SelectedValue = ds.Tables[0].Rows[0]["V_Shift"].ToString();
                            }
                            else
                            {
                                ddlNextTankerShift.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            ddlNextTankerShift.SelectedIndex = 0;
                        }

                        ddlTankerType_SelectedIndexChanged(sender, e);

                        //ddlUnitName.Enabled = false;
                        //txtVehicleNo.Enabled = false;
                        //txtDriverName.Enabled = false;
                        //txtDriverMobileNo.Enabled = false;
                        //txtRepresentativeName.Enabled = false;
                        //txtRepresentativeMobileNo.Enabled = false;
                        ddlTankerType.Enabled = false;
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Exists")
                    {
                        lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());


                        txtVehicleNo.Text = "";
                        ddlUnitName.ClearSelection();
                        txtDriverName.Text = "";
                        txtDriverMobileNo.Text = "";
                        txtRepresentativeName.Text = "";
                        txtRepresentativeMobileNo.Text = "";
                        txtClosingBalanceAfterDispatch.Text = "";
                        txtNextTankerRequiredDate.Text = "";
                        ddlTankerType.ClearSelection();
                        ddlTankerType_SelectedIndexChanged(sender, e);
                        //ddlUnitName.Enabled = true;
                        //txtVehicleNo.Enabled = true;
                        //txtDriverName.Enabled = true;
                        //txtDriverMobileNo.Enabled = true;
                        //txtRepresentativeName.Enabled = true;
                        //txtRepresentativeMobileNo.Enabled = true;
                        //ddlTankerType.Enabled = true;
                        //dv_TankerType.Visible = false;

                        btnAddQualityDetails.Visible = false;
                        ddlReferenceNo.Focus();
                    }
                    else
                    {
                        lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());

                        //ddlUnitName.Enabled = true;
                        //txtVehicleNo.Enabled = true;
                        //txtDriverName.Enabled = true;
                        //txtDriverMobileNo.Enabled = true;
                        //txtRepresentativeName.Enabled = true;
                        //txtRepresentativeMobileNo.Enabled = true;
                        //ddlTankerType.Enabled = true;
                        //dv_TankerType.Visible = false;

                        txtVehicleNo.Text = "";
                        ddlUnitName.ClearSelection();
                        txtDriverName.Text = "";
                        txtDriverMobileNo.Text = "";
                        txtRepresentativeName.Text = "";
                        txtRepresentativeMobileNo.Text = "";
                        txtClosingBalanceAfterDispatch.Text = "";
                        txtNextTankerRequiredDate.Text = "";
                        ddlTankerType.ClearSelection();
                        ddlTankerType_SelectedIndexChanged(sender, e);

                        btnAddQualityDetails.Visible = false;
                        ddlReferenceNo.Focus();
                    }
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "No Data Found!");

                    //ddlUnitName.Enabled = true;
                    //txtVehicleNo.Enabled = true;
                    //txtDriverName.Enabled = true;
                    //txtDriverMobileNo.Enabled = true;
                    //txtRepresentativeName.Enabled = true;
                    //txtRepresentativeMobileNo.Enabled = true;
                    //ddlTankerType.Enabled = true;
                    //dv_TankerType.Visible = false;

                    txtVehicleNo.Text = "";
                    ddlUnitName.ClearSelection();
                    txtDriverName.Text = "";
                    txtDriverMobileNo.Text = "";
                    txtRepresentativeName.Text = "";
                    txtRepresentativeMobileNo.Text = "";
                    txtClosingBalanceAfterDispatch.Text = "";
                    txtNextTankerRequiredDate.Text = "";
                    ddlTankerType.ClearSelection();
                    ddlTankerType_SelectedIndexChanged(sender, e);

                    btnAddQualityDetails.Visible = false;
                    ddlReferenceNo.Focus();
                }
            }
            else
            {
                //ddlUnitName.Enabled = true;
                //txtVehicleNo.Enabled = true;
                //txtDriverName.Enabled = true;
                //txtDriverMobileNo.Enabled = true;
                //txtRepresentativeName.Enabled = true;
                //txtRepresentativeMobileNo.Enabled = true;
                //ddlTankerType.Enabled = true;
                //dv_TankerType.Visible = false;

                txtVehicleNo.Text = "";
                ddlUnitName.ClearSelection();
                txtDriverName.Text = "";
                txtDriverMobileNo.Text = "";
                txtRepresentativeName.Text = "";
                txtRepresentativeMobileNo.Text = "";
                txtClosingBalanceAfterDispatch.Text = "";
                txtNextTankerRequiredDate.Text = "";
                ddlTankerType.ClearSelection();
                ddlTankerType_SelectedIndexChanged(sender, e);

                btnAddQualityDetails.Visible = false;
                ddlReferenceNo.Focus();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
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

            if ((((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'F'").Length > 0 && ((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'R'").Length > 0 && ddlTankerType.SelectedValue == "D") || (((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'S'").Length > 0 && ddlTankerType.SelectedValue == "S"))
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
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
                }
                if (CompartmentType == 1)
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Compartment of \"" + ddlChamberOfSampleNo.SelectedItem.Text + "\" in Sample No. " + txtSampleNo.Text + " already exist.");
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

            if ((((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'F'").Length > 0 && ((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'R'").Length > 0 && ddlTankerType.SelectedValue == "D") || (((DataTable)ViewState["InsertRecord2"]).Select("V_SealLocation = 'S'").Length > 0 && ddlTankerType.SelectedValue == "S"))
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
        }
        catch (Exception ex)
        {
            btnAdd.Visible = false;
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
}