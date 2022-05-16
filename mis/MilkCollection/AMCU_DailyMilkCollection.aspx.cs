using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Web;

public partial class mis_MilkCollection_AMCU_DailyMilkCollection : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                ViewState["Mobile"] = null;
                ViewState["ProducerNameEnglish"] = null;
                ViewState["ProducerCardNo"] = null;
                ViewState["RPLTR"] = null;
                ViewState["AMOUNT"] = null;
                ViewState["I_CollectionID"] = null;
                ViewState["TextMsg"] = null;
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                
                FillProducerName();
                FillSociety();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlShift_SelectedIndexChanged(sender, e);
                SetFocus(txtDate);
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
            // lblMsg.Text = "";
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSociatyName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    txtBlock.Text = ds.Tables[0].Rows[0]["Block_Name"].ToString();
                    lblblockname.Text = ds.Tables[0].Rows[0]["Block_Name"].ToString();
                    lblDcsname.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    lbldate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    shift.Text = ddlShift.SelectedValue;

                    txtBlock.Enabled = false;
                }
                else
                {
                    ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillProducerName()
    {
        try
        {
            // lblMsg.Text = "";
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_Id" },
                              new string[] { "3", objdb.Office_ID() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlFarmer.DataTextField = "ProducerName";
                    ddlFarmer.DataValueField = "ProducerId";
                    ddlFarmer.DataSource = ds;
                    ddlFarmer.DataBind();
                    ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlFarmer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFarmer.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                             new string[] { "flag", "ProducerId" },
                             new string[] { "4", ddlFarmer.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    txtProducerId.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                    ViewState["Mobile"] = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    //ViewState["Mobile"] = "8878112333";
                    ViewState["ProducerNameEnglish"] = ds.Tables[0].Rows[0]["ProducerNameEnglish"].ToString();
                    ViewState["ProducerCardNo"] = ds.Tables[0].Rows[0]["ProducerCardNo"].ToString();

                }
            }

            if (btnSubmit.Text == "Update")
            {

            }
            else
            {
                //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //ddlMilkType.ClearSelection();
                //ddlQuality.ClearSelection();
                txtI_MilkQuantity.Text = "";
                txtNetFat.Text = "";
                txtNetCLR.Text = "";
                txtnetsnf.Text = "";
                //txtsnfinkg.Text = "";
                //txtfatinkg.Text = "";
                txtRemark.Text = "";
            }
            SetFocus(ddlMilkType);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void txtSociatyName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "DCSMaster_ID" },
                              new string[] { "5", "1" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //txtCode.Text = ds.Tables[0].Rows[0]["DCS_Code"].ToString();
                //txtAssembly.Text = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                //txtTown.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private decimal GetCLR()
    {
        decimal CLR = 0, SNF = 0, fat = 0;
        try
        {
            if (txtnetsnf.Text != "") { SNF = Convert.ToDecimal(txtnetsnf.Text); }

            if (txtNetFat.Text != "") { fat = Convert.ToDecimal(txtNetFat.Text); }

            CLR = Obj_MC.GetCLR_DCS(fat, SNF);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error" + ex.Message.ToString());
        }
        return Math.Round(CLR, 1);
    }

    private decimal GetSNF_InKG()
    {
        decimal clr = 0, fat = 0, snf_Per = 0, MilkQty = 0, SNF_InKG = 0;

        try
        {
            if (txtI_MilkQuantity.Text == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(txtI_MilkQuantity.Text); }

            if (txtNetFat.Text == "") { fat = 0; } else { fat = Convert.ToDecimal(txtNetFat.Text); }

            if (txtNetCLR.Text == "") { clr = 0; } else { clr = Convert.ToDecimal(txtNetCLR.Text); }

            snf_Per = Obj_MC.GetSNFPer_DCS(fat, clr);

            SNF_InKG = Obj_MC.GetSNFInKg(MilkQty, snf_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error" + ex.Message.ToString());
        }

        return Math.Round(SNF_InKG, 3);
    }

    private decimal GetFAT_InKG()
    {
        decimal fat_Per = 0, MilkQty = 0, FAT_InKG = 0;

        try
        {
            if (txtI_MilkQuantity.Text == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(txtI_MilkQuantity.Text); }

            if (txtNetFat.Text == "") { fat_Per = 0; } else { fat_Per = Convert.ToDecimal(txtNetFat.Text); }

            FAT_InKG = Obj_MC.GetSNFInKg(MilkQty, fat_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error" + ex.Message.ToString());
        }

        return Math.Round(FAT_InKG, 3);
    }

    protected void txtI_MilkQuantity_TextChanged(object sender, EventArgs e)
    {
        txtNetCLR.Text = GetCLR().ToString();
        //txtfatinkg.Text = GetFAT_InKG().ToString();
        //txtsnfinkg.Text = GetSNF_InKG().ToString();

        if (btnSubmit.Text == "Update")
        {
            if (txtI_MilkQuantity.Text != "" && txtNetFat.Text != "" && txtnetsnf.Text != "")
            {
                GetRateAndAmount();
            }
        }

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }

        if (txtI_MilkQuantity.Text == "" || txtI_MilkQuantity.Text == "0" || txtI_MilkQuantity.Text == "0.0" || txtI_MilkQuantity.Text == "0.00")
        {
            txtI_MilkQuantity.Text = "";
            txtNetFat.Text = "";
            txtNetCLR.Text = "";
            txtnetsnf.Text = "";
            //txtsnfinkg.Text = "";
            //txtfatinkg.Text = "";
            txtRemark.Text = "";
        }

        if (txtI_MilkQuantity.Text != "" && txtNetFat.Text != "" && txtnetsnf.Text != "")
        {
            GetRateAndAmount();
        }



    }

    protected void txtNetFat_TextChanged(object sender, EventArgs e)
    {
        txtNetCLR.Text = GetCLR().ToString();
        //txtfatinkg.Text = GetFAT_InKG().ToString();
        //txtsnfinkg.Text = GetSNF_InKG().ToString();

        if (btnSubmit.Text == "Update")
        {
            if (txtI_MilkQuantity.Text != "" && txtNetFat.Text != "" && txtnetsnf.Text != "")
            {
                GetRateAndAmount();
            }
        }

        if (txtnetsnf.Text == "")
        {
            txtnetsnf.Focus();
        }

        if (txtNetFat.Text == "") { txtNetFat.Text = "0"; }

        if (Convert.ToDecimal(txtNetFat.Text) > Convert.ToDecimal(5.5))
        {
            ddlMilkType.ClearSelection();
            ddlMilkType.SelectedValue = "Buffalo";
        }
        else
        {
            ddlMilkType.ClearSelection();
            ddlMilkType.SelectedValue = "Cow";
        }

        if (txtI_MilkQuantity.Text != "" && txtNetFat.Text != "" && txtnetsnf.Text != "")
        {
            GetRateAndAmount();
        }
  
    }

    protected void txtnetsnf_TextChanged(object sender, EventArgs e)
    {
        txtNetCLR.Text = GetCLR().ToString();
        //txtfatinkg.Text = GetFAT_InKG().ToString();
        //txtsnfinkg.Text = GetSNF_InKG().ToString();

        if (txtI_MilkQuantity.Text != "" && txtNetFat.Text != "" && txtnetsnf.Text != "")
        {
            GetRateAndAmount();
        }
        SetFocus(btnSubmit);
    }

    private void GetRateAndAmount()
    {

        decimal FAT = 0, CLR = 0, MilkQty = 0, SNF_Per = 0; string RatePerLtr = ""; string TotalAmount = "";
        ViewState["RPLTR"] = null;
        ViewState["AMOUNT"] = null;
        ViewState["TextMsg"] = null;

        try
        {
            if (txtNetFat.Text == "") { FAT = 0; } else { FAT = Convert.ToDecimal(txtNetFat.Text); }

            if (txtNetCLR.Text == "") { CLR = 0; } else { CLR = Convert.ToDecimal(txtNetCLR.Text); }

            if (txtI_MilkQuantity.Text == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(txtI_MilkQuantity.Text); }

            RatePerLtr = Obj_MC.GetRatePerLtrOrKg(FAT, CLR, objdb.Office_ID());
			 if (ddlQuality.SelectedValue == "Sour")
            {
                RatePerLtr = (Math.Round(decimal.Parse(RatePerLtr) * 50 / 100, 2)).ToString();
            }
            if (ddlQuality.SelectedValue == "Curd")
            {
                RatePerLtr = (Math.Round(decimal.Parse(RatePerLtr) * 30 / 100, 2)).ToString();
            }
            ViewState["RPLTR"] = RatePerLtr;
            TotalAmount = Math.Round((MilkQty) * (Convert.ToDecimal(RatePerLtr)), 3).ToString();
            ViewState["AMOUNT"] = TotalAmount;
            txtRemark.Text = TotalAmount + " @ " + RatePerLtr + " Per Ltr";
             
            string ShiftName = ""; string MilkType = ""; string ProducerNameCode = "";;string MilkQtyMsg = "";
			
			MilkQtyMsg = MilkQty.ToString() + " लीटर";
            if (ddlShift.SelectedValue == "Morning")
            {
                ShiftName = "M";
            }
            else
            {
                ShiftName = "E";
            }

            if (ddlMilkType.SelectedValue == "Buffalo")
            {
                MilkType = "B";
            }
            else if (ddlMilkType.SelectedValue == "Cow")
            {
                MilkType = "C";
            }
            else
            {
                MilkType = "M";
            }
            if (txtnetsnf.Text == "") { SNF_Per = 0; } else { SNF_Per = Convert.ToDecimal(txtnetsnf.Text); }


            if (ViewState["ProducerNameEnglish"] != null)
            {
                ProducerNameCode = ViewState["ProducerNameEnglish"].ToString();
            }

            if (ViewState["ProducerCardNo"] != null)
            {
                ProducerNameCode = ProducerNameCode + "(" + ViewState["ProducerCardNo"].ToString() + ")";
            }
            if(btnSubmit.Text == "Submit")
            {
				//ViewState["TextMsg"] = "Date "  + Convert.ToDateTime(txtDate.Text, cult).ToString("dd/MM/yyyy") +  " shift "  +ShiftName + " FAT " +FAT + " SNF "+ " " + SNF_Per +  " qty " + MilkQty + " Ltr " + ", Thanks !" ;
				//ViewState["TextMsg"] = "दिनांक  " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd/MM/yyyy") + " शिफ्ट  " + ShiftName + " फेट  " + FAT + " एसएनएफ  "  + //SNF_Per + " मात्रा " + MilkQtyMsg + ", धन्यवाद !";
				 ViewState["TextMsg"] = "दिनांक  " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd/MM/yyyy") + " शिफ्ट  " + ShiftName + " फेट  " + FAT + " एसएनएफ  " + SNF_Per + " मात्रा " + MilkQtyMsg + " कुल राशि " + TotalAmount;
                //ViewState["TextMsg ] = "D:" + Convert.ToDateTime(txtDate.Text, cult).ToString("dd/MM/yyyy")
                //+ " Shift:" + ShiftName
                //+ " " + ProducerNameCode
                //+ " Milk Type:" + MilkType
                //+ " Qty:" + MilkQty
                //+ " Fat:" + FAT
                //+ " SNF:" + SNF_Per
                //+ " CLR:" + CLR
                //+ " RTPL:" + RatePerLtr
                //+ " Amount:" + TotalAmount;
            }
            else
            {
                //ViewState["TextMsg"] = "जानकारी सुधार की गयी" +"D:" + Convert.ToDateTime(txtDate.Text, cult).ToString("dd/MM/yyyy")
                //+ " Shift:" + ShiftName
                //+ " " + ProducerNameCode
                //+ " Milk Type:" + MilkType
                //+ " Qty:" + MilkQty
                //+ " Fat:" + FAT
                //+ " SNF:" + SNF_Per
                //+ " CLR:" + CLR
                //+ " RTPL:" + RatePerLtr
                //+ " Amount:" + TotalAmount;
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error:" + ex.Message.ToString());
        }
    }

    private void SendSMS(string MobileNo, string SMSText)
    {
		try
		{
		ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //Your authentication key
        //string authKey = "3597C1493C124F";
        //Sender ID
        //string senderId = "MPSCDF";

        //string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=" + authKey + "&type=unicode&routeid=288&contacts=" + MobileNo + "&senderid=" + senderId + "&msg=" + Server.UrlEncode(SMSText);
		string link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + MobileNo + "&text=" + Server.UrlEncode(SMSText) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162589782117741&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=2&camp_name=mpmilk_user";
        HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        Stream stream = response.GetResponseStream();
		}
		catch(Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString()+ "SMS Error");
        }

    }

    private string GetDispatchDetail()
    {
        string strDispatchStatus = "0";

        try
        {
            ds = null;

            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                     new string[] { "flag", "DT_Date", "V_Shift", "I_OfficeID" },
                     new string[] { "15", Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"), cult).ToString("yyyy-MM-dd"), ddlShift.SelectedValue.ToString(), objdb.Office_ID() }, "dataset");

            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strDispatchStatus = ds.Tables[0].Rows[0]["DispatchStatus"].ToString();

                    return strDispatchStatus;
                }
                else
                {
                    return strDispatchStatus;
                }
            }
            else
            {
                return strDispatchStatus;
            }
        }
        catch (Exception)
        {

            return strDispatchStatus;
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                string DispatchDetail = GetDispatchDetail();

                if (DispatchDetail == "0")
                {

                }
                else
                {
                    string nextshift = "";

                    if (ddlShift.SelectedValue == "Morning")
                    {
                        nextshift = "Evening";
                    }
                    if (ddlShift.SelectedValue == "Evening")
                    {
                        nextshift = "next day Morning";
                    }

                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", ddlShift.SelectedItem.Text + " shift milk dispatch has been done. Now milk receiving will be done on " + nextshift + " shift.");
                    FillGrid();
                    return;

                }


                if (btnSubmit.Text == "Submit")
                {

                    ds = null;
                    ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                    new string[] { "Flag"
                     ,"Office_Id"
                     ,"V_SocietyName"
                     ,"Dt_Date"
                     ,"V_Shift"
                     ,"I_Producer_ID"
                     ,"I_MilkSupplyQty"
                     ,"V_MilkType"
                     ,"V_Review"
                     ,"Quality"
                     ,"Fat"
                     ,"CLR"
                     ,"SNF"
                     ,"Rate"
                     ,"Amount"
                     ,"TotalSNFInKg"
                     ,"TotalFatInKg"
                     ,"CreatedBy"
                     ,"CreatedBy_IP"
                     ,"Remark" 
                     ,"EntryMode"
                    },
                  new string[] { "16"
                      ,ViewState["Office_ID"].ToString()
                      ,txtSociatyName.Text
                      ,Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                      ,ddlShift.SelectedValue.ToString()
                      ,ddlFarmer.SelectedValue.ToString()
                      ,txtI_MilkQuantity.Text
                      ,ddlMilkType.SelectedValue.ToString()
                      ,ViewState["TextMsg"].ToString()
                      ,ddlQuality.SelectedValue.ToString()
                      ,txtNetFat.Text
                      ,txtNetCLR.Text
                      ,txtnetsnf.Text
                      ,ViewState["RPLTR"].ToString()
                      ,ViewState["AMOUNT"].ToString()
                      ,"0.0"
                      ,"0.0"
                      ,ViewState["Emp_ID"].ToString()
                      ,objdb.GetLocalIPAddress() 
                      ,txtRemark.Text
                      ,"AMCU"

                  }, "dataset");


                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            //if (ViewState["Mobile"] != null)
                            //{
                            //    if (ViewState["TextMsg"] != null)
                            //    {
                            //        //SendSMS(ViewState["Mobile"].ToString(), ViewState["TextMsg"].ToString());

                            //        //if (ddlFarmer.SelectedValue.ToString() == "90256")
                            //        // {
                            //        // SendSMS(ViewState["Mobile"].ToString(), ViewState["TextMsg"].ToString());
                            //        // }

                            //    }
                            //}


                            if (ViewState["Mobile"] != null)
                            {
                                if (ViewState["TextMsg"] != null)
                                {
                                    
									 SendSMS(ViewState["Mobile"].ToString(), ViewState["TextMsg"].ToString());
                                }
                            }

                            ////if (Convert.ToDecimal(txtI_MilkQuantity.Text) > 100)
                            ////{
                            ////    string smsstext = "In society Titora Producer No " + txtProducerId.Text + " has provided Milk " + txtI_MilkQuantity.Text + " Liter More than capacity for " + Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd") + " & " + ddlShift.SelectedItem + ".";
                            ////    SendSMS("7489250319", smsstext);
                            ////}

                            FillGrid();
                            Cleartext();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Record Saved Successfully");
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", ds.Tables[0].Rows[0]["Msg"].ToString());

                        }
                    }
                }

                if (btnSubmit.Text == "Update")
                {
                    ddlFarmer_SelectedIndexChanged(sender, e);
                    ds = null;
                    ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                    new string[] { "Flag"
                     ,"Office_Id"
                     ,"V_SocietyName"
                     ,"Dt_Date"
                     ,"V_Shift"
                     ,"I_Producer_ID"
                     ,"I_MilkSupplyQty"
                     ,"V_MilkType"
                     //,"V_Review"
                     ,"Quality"
                     ,"Fat"
                     ,"CLR"
                     ,"SNF"
                     ,"Rate"
                     ,"Amount"
                     ,"TotalSNFInKg"
                     ,"TotalFatInKg"
                     ,"CreatedBy"
                     ,"CreatedBy_IP"
                     ,"I_CollectionID"
                     ,"Remark"
                    },
                  new string[] { "17"
                      ,ViewState["Office_ID"].ToString()
                      ,txtSociatyName.Text
                      ,Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                      ,ddlShift.SelectedValue.ToString()
                      ,ddlFarmer.SelectedValue.ToString()
                      ,txtI_MilkQuantity.Text
                      ,ddlMilkType.SelectedValue.ToString()
                      //,ViewState["TextMsg"].ToString()
                      ,ddlQuality.SelectedValue.ToString()
                      ,txtNetFat.Text
                      ,txtNetCLR.Text
                      ,txtnetsnf.Text
                      ,ViewState["RPLTR"].ToString()
                      ,ViewState["AMOUNT"].ToString()
                      ,"0.0"
                      ,"0.0"
                      ,ViewState["Emp_ID"].ToString()
                      ,objdb.GetLocalIPAddress() 
                      ,ViewState["I_CollectionID"].ToString()
                      ,txtRemark.Text
                  }, "dataset");


                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            //if (ViewState["Mobile"] != null)
                            //{
                            //    if (ViewState["TextMsg"] != null)
                            //    {
                            //        SendSMS(ViewState["Mobile"].ToString(), ViewState["TextMsg"].ToString());
                            //    }
                            //}
                            txtDate.Enabled = true;
                            ddlShift.Enabled = true;
                            ddlFarmer.Enabled = true;
                            FillGrid();
                            Cleartext();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Record Saved Successfully");
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", ds.Tables[0].Rows[0]["Msg"].ToString());

                        }
                    }

                    btnSubmit.Text = "Submit";

                }
                  
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
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

            //ddlShift.Enabled = false;


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void FillGrid()
    {
        try
        {

            ds = null;

            string strdt = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd");

            if (ddlShift.SelectedValue.ToString() == "0")
            {
                gvMilkCollection.DataSource = ds;
                gvMilkCollection.DataBind();
                gvMilkCollection.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvMilkCollection.UseAccessibleHeader = true;

            }

            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "FDT", "V_Shift", "Office_Id" },
                              new string[] { "6", strdt, ddlShift.SelectedValue.ToString(), objdb.Office_ID() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                div_milkdetails.Visible = true;
                gvMilkCollection.DataSource = ds;
                gvMilkCollection.DataBind();
                gvMilkCollection.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvMilkCollection.UseAccessibleHeader = true;

                if (ds.Tables[1].Rows.Count > 0)
                {
                    lbltotalmilkqty.Text = ds.Tables[1].Rows[0]["Net_I_MilkSupplyQty"].ToString();

                    Label lblF_Qty = (gvMilkCollection.FooterRow.FindControl("lblF_Qty") as Label);
                    Label lblF_Fat = (gvMilkCollection.FooterRow.FindControl("lblF_Fat") as Label);
                    Label lblF_SNF = (gvMilkCollection.FooterRow.FindControl("lblF_SNF") as Label);
                    Label lblF_CLR = (gvMilkCollection.FooterRow.FindControl("lblF_CLR") as Label);
                    Label lblF_Rate = (gvMilkCollection.FooterRow.FindControl("lblF_Rate") as Label);
                    Label lblF_Amount = (gvMilkCollection.FooterRow.FindControl("lblF_Amount") as Label);

                    lblF_Qty.Text = ds.Tables[1].Rows[0]["Net_I_MilkSupplyQty"].ToString();
                    lblF_Fat.Text = ds.Tables[1].Rows[0]["Fat_Avg"].ToString();
                    lblF_SNF.Text = ds.Tables[1].Rows[0]["SNF_Avg"].ToString();
                    lblF_CLR.Text = ds.Tables[1].Rows[0]["CLR_Avg"].ToString();
                    lblF_Rate.Text = ds.Tables[1].Rows[0]["Rate_Avg"].ToString();
                    lblF_Amount.Text = ds.Tables[1].Rows[0]["Amount_Net"].ToString();
                }


                //Label lblF_Qty = (gvMilkCollection.FooterRow.FindControl("lblF_Qty") as Label);
                //Label lblF_Fat = (gvMilkCollection.FooterRow.FindControl("lblF_Fat") as Label);
                //Label lblF_SNF = (gvMilkCollection.FooterRow.FindControl("lblF_SNF") as Label);
                //Label lblF_CLR = (gvMilkCollection.FooterRow.FindControl("lblF_CLR") as Label);
                //Label lblF_FatInKg = (gvMilkCollection.FooterRow.FindControl("lblF_FatInKg") as Label);
                //Label lblF_SNFInKg = (gvMilkCollection.FooterRow.FindControl("lblF_SNFInKg") as Label);
                //Label lblF_Amount = (gvMilkCollection.FooterRow.FindControl("lblF_Amount") as Label);


                //decimal F_Qty = 0, F_Fat = 0, F_SNF = 0, F_CLR = 0, F_FatInKg = 0, F_SNFInKg = 0, F_Amount = 0;

                //foreach (GridViewRow row in gvMilkCollection.Rows)
                //{

                //    Label txtI_MilkSupplyQty = (Label)row.FindControl("txtI_MilkSupplyQty");
                //    if (txtI_MilkSupplyQty.Text != "")
                //    {
                //        F_Qty += Convert.ToDecimal(txtI_MilkSupplyQty.Text);
                //    }

                //    Label lblFat = (Label)row.FindControl("lblFat");
                //    if (lblFat.Text != "")
                //    {
                //        F_Fat += Convert.ToDecimal(lblFat.Text);
                //    }

                //    Label lblsnf = (Label)row.FindControl("lblsnf");
                //    if (lblsnf.Text != "")
                //    {
                //        F_SNF += Convert.ToDecimal(lblsnf.Text);
                //    }

                //    Label lblCLR = (Label)row.FindControl("lblCLR");
                //    if (lblCLR.Text != "")
                //    {
                //        F_CLR += Convert.ToDecimal(txtI_MilkSupplyQty.Text);
                //    }

                //    Label lblTotalFatInKg = (Label)row.FindControl("lblTotalFatInKg");
                //    if (lblTotalFatInKg.Text != "")
                //    {
                //        F_FatInKg += Convert.ToDecimal(lblTotalFatInKg.Text);
                //    }

                //    Label lblTotalSNFInKg = (Label)row.FindControl("lblTotalSNFInKg");
                //    if (lblTotalSNFInKg.Text != "")
                //    {
                //        F_SNFInKg += Convert.ToDecimal(lblTotalSNFInKg.Text);
                //    }

                //    Label lblAmount = (Label)row.FindControl("lblAmount");
                //    if (lblAmount.Text != "")
                //    {
                //        F_Amount += Convert.ToDecimal(lblAmount.Text);
                //    }

                //}

                //lblF_Qty.Text = F_Qty.ToString();
                //lblF_Fat.Text = F_Fat.ToString();
                //lblF_SNF.Text = F_SNF.ToString();
                //lblF_CLR.Text = F_CLR.ToString();
                //lblF_FatInKg.Text = F_FatInKg.ToString();
                //lblF_SNFInKg.Text = F_SNFInKg.ToString();
                //lblF_Amount.Text = F_Amount.ToString();

            }
            else
            {
                div_milkdetails.Visible = false;
                gvMilkCollection.DataSource = null;
                gvMilkCollection.DataBind();
            }

            // Total



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void txtI_MilkSupplyQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                int selRowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
                Label lblgvI_CollectionID = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblgvI_CollectionID");
                TextBox txtI_MilkSupplyQty = (TextBox)gvMilkCollection.Rows[selRowIndex].FindControl("txtI_MilkSupplyQty");

                if (txtI_MilkSupplyQty.Text != "")
                {
                    objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                            new string[] { "flag", "I_CollectionID", "I_MilkSupplyQty", "UpdatedBy", "UpdatedBy_IP" },
                            new string[] { "11", lblgvI_CollectionID.Text, txtI_MilkSupplyQty.Text, ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Record Updated Successfully");

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Opps!", "Enter Valid Milk Quty");

                }

            }

            Cleartext();
            FillGrid();
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void lbledit_Click(object sender, EventArgs e)
    {
        try
        {

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkbtnVN = (LinkButton)gvMilkCollection.Rows[selRowIndex].FindControl("lbledit");
            Label lblDt_Date = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblDt_Date");
            Label lblgvV_Shift = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblgvV_Shift");
            Label lblgvV_Code = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblgvV_Code");
            Label lblgvI_MilkType = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblgvI_MilkType");
            Label lblQuality = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblQuality");
            Label txtI_MilkSupplyQty = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("txtI_MilkSupplyQty");
            Label lblFat = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblFat");
            Label lblsnf = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblsnf");
            Label lblCLR = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblCLR");
            Label lblTotalFatInKg = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblTotalFatInKg");
            Label lblTotalSNFInKg = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblTotalSNFInKg");
            Label lblRowNo = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblRowNo");

            ViewState["RPLTR"] = null;
            ViewState["AMOUNT"] = null;
            ViewState["TextMsg"] = null;

            Label lblRate = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblRate");
            Label lblAmount = (Label)gvMilkCollection.Rows[selRowIndex].FindControl("lblAmount");
            ViewState["RPLTR"] = lblRate.Text;
            ViewState["AMOUNT"] = lblAmount.Text;
            ViewState["TextMsg"] = lblRowNo.ToolTip.ToString();


            ViewState["I_CollectionID"] = lnkbtnVN.CommandArgument;
            txtDate.Text = lblDt_Date.Text;
            ddlShift.SelectedValue = lblgvV_Shift.Text;
            txtProducerId.Text = lblgvV_Code.Text;
            ddlFarmer.SelectedValue = lblgvV_Code.ToolTip.ToString();
            ddlMilkType.SelectedValue = lblgvI_MilkType.Text;
            ddlQuality.SelectedValue = lblQuality.Text;
            txtI_MilkQuantity.Text = txtI_MilkSupplyQty.Text;
            txtNetFat.Text = lblFat.Text;
            txtnetsnf.Text = lblsnf.Text;
            txtNetCLR.Text = lblCLR.Text;
            //txtfatinkg.Text = lblTotalFatInKg.Text;
            //txtsnfinkg.Text = lblTotalSNFInKg.Text;
            txtRemark.Text = lblRowNo.ToolTip.ToString();
            btnSubmit.Text = "Update";


            txtDate.Enabled = false;
            ddlShift.Enabled = false;
            ddlFarmer.Enabled = false;

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void Cleartext()
    {

        //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ddlFarmer.ClearSelection();
        //ddlMilkType.ClearSelection();
        //ddlQuality.ClearSelection();
        txtI_MilkQuantity.Text = "";
        txtNetFat.Text = "";
        txtNetCLR.Text = "";
        txtnetsnf.Text = "";
        //txtsnfinkg.Text = "";
        //txtfatinkg.Text = "";
        txtRemark.Text = "";
        txtProducerId.Text = "";
        ViewState["TextMsg"] = null;

    }

    protected void PrintSlip(string I_CollectionID)
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection", new string[] { "flag", "I_CollectionID" }, new string[] { "20", I_CollectionID }, "dataset");
            if(ds != null)
            {
                if(ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                       // sb.Append("<div style='border:1px dashed black; width:300px; height:100%; margin:20px 0 0 80px; padding:10px !important'>");
                        sb.Append("<div style='border:1px dashed black; margin-top:-20px; width:220px;'>");
                        sb.Append("<table style='font-size:13px;;  margin-left:8px; font-family:monospace;'>");
                        sb.Append("<tr>");
                        sb.Append("<td colspan='2' style='text-align:center'>दुग्ध समिति " + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>दिनांक : " + ds.Tables[0].Rows[0]["Dt_Date"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["V_Shift"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["V_MilkType"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["V_Shift"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["V_MilkType"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td><span id='spn'>" + ds.Tables[0].Rows[0]["ProducerCardNo"].ToString() + "</span> :&nbsp;  " + ds.Tables[0].Rows[0]["ProducerName"].ToString() + "</td>");
                       
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td><span id='spn'>लीटर</span> :&nbsp; " + ds.Tables[0].Rows[0]["I_MilkSupplyQty"].ToString() + "</td>");
                       
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td ><span id='spn'>फैट</span> :&nbsp; " + ds.Tables[0].Rows[0]["Fat"].ToString() + "</td>");
                       
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td ><span id='spn'>सी.अल.आर.</span> :&nbsp; " + ds.Tables[0].Rows[0]["CLR"].ToString() + "</td>");
                       
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td ><span id='spn'>रेट / ली.</span> :&nbsp; " + ds.Tables[0].Rows[0]["Rate"].ToString() + "</td>");
                       
                        sb.Append("<tr>");
                        sb.Append("<td ><span id='spn'>कुल कि.</span> :&nbsp; " + ds.Tables[0].Rows[0]["Amount"].ToString() + "</td>");
                       
                        sb.Append("</tr>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("</div>");
                        Print.InnerHtml = sb.ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType() ,"CallMyFunction","window.print();",true);
                        //StringBuilder sb1 = new StringBuilder();
                        //sb.Append("<script type = 'text/javascript'>");
                        //sb.Append("window.Print()");                    
                        //sb.Append("</script>");
                        //ClientScript.RegisterStartupScript(this.GetType(), "script", sb1.ToString());

                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lblPrint_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lblPrint = (LinkButton)gvMilkCollection.Rows[selRowIndex].FindControl("lblPrint");
            string I_CollectionID = lblPrint.CommandArgument.ToString();
            PrintSlip(I_CollectionID);
    }
}