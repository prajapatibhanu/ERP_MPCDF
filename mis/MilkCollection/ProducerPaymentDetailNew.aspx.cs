using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public partial class mis_MilkCollection_ProducerPaymentDetailNew : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);

    string Fdate = "";
    string Tdate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";
            lblRecordMsg.Text = "";

            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtFdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                txtTdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                GetCCDetails();
				 FillSociety();
				ViewState["ProducerPayment_ID"] = "";
				
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
                        }
                        

                      


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //protected void FillSociety()
    //{
    //    try
    //    {
    //        if (ddlMilkCollectionUnit.SelectedValue != "0")
    //        {
    //            ds = null;
    //            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
    //                              new string[] { "flag", "Office_ID", "OfficeType_ID" },
    //                              new string[] { "10", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");

    //            if (ds.Tables.Count > 0)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlSociety.DataTextField = "Office_Name";
    //                    ddlSociety.DataValueField = "Office_ID";
    //                    ddlSociety.DataSource = ds.Tables[2];
    //                    ddlSociety.DataBind();
    //                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
    //                }
    //                else
    //                {
    //                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
    //                }
    //            }
    //            else
    //            {
    //                ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
    //            }
    //        }
    //        else
    //        {
    //            Response.Redirect("ProducerPaymentDetail.aspx", false);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void FillSociety()
    {
        try
        {
            ddlSociety.ClearSelection();
            ddlSociety.Items.Clear();
            if (ddlccbmcdetail.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry",
                         new string[] { "flag", "Office_ID", "Office_Parant_ID", "OfficeType_ID" },
                         new string[] { "8", ddlccbmcdetail.SelectedValue.ToString(), objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[0];
                        ddlSociety.DataBind();
                        // ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));

                    }
                }
                else
                {
                    //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                // Response.Redirect("SocietywiseMilkCollectionInvoiceDetails.aspx", false);
            }
            //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));

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

            divIteminfo.Visible = false;
            lblRecordMsg.Text = "";

            if (txtFdt.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
            }

            if (txtTdt.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = null;
            string Society = "";

            foreach (ListItem item in ddlSociety.Items)
            {
                if (item.Selected)
                {
                    Society += item.Value + ",";
                }
            }
            ds = null;
            //ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New",
            //            new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID" },
            //            new string[] { "0", Fdate, Tdate, ddlSociety.SelectedValue }, "dataset");
			ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New",
                       new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID_mlt" },
                       new string[] { "0", Fdate, Tdate, Society }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        divIteminfo.Visible = true;

                        gvItemDetails.DataSource = ds.Tables[1];
                        gvItemDetails.DataBind();
                        gvItemDetails_Cash.DataSource = ds.Tables[1];
                        gvItemDetails_Cash.DataBind();
						decimal TotalMilkQuanity = 0;
                        decimal TotalMV = 0;
                        decimal TotalSV = 0;
						decimal TotalEV = 0;
                        decimal TotalAdjustAmount = 0;
                        decimal TotalPayableAmount = 0;
						TotalMilkQuanity = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalLtr_MilkQty"));
                        TotalMV = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkValue"));
                        TotalSV = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SaleValue"));
						TotalEV = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("EarningValue"));
                        TotalAdjustAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("AdjustAmount"));
                        TotalPayableAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("PayableAmount"));
								
								
                        gvItemDetails.FooterRow.Cells[2].Text = "<b>Total : </b>";
                        gvItemDetails.FooterRow.Cells[3].Text = "<b>" + TotalMilkQuanity.ToString() + "</b>";
                        gvItemDetails.FooterRow.Cells[6].Text = "<b>" + TotalMV.ToString() + "</b>";
                        gvItemDetails.FooterRow.Cells[7].Text = "<b>" + TotalEV.ToString() + "</b>";
                        gvItemDetails.FooterRow.Cells[8].Text = "<b>" + TotalSV.ToString() + "</b>";
                        gvItemDetails.FooterRow.Cells[9].Text = "<b>" + TotalAdjustAmount.ToString() + "</b>";
                        gvItemDetails.FooterRow.Cells[10].Text = "<b>" + TotalPayableAmount.ToString() + "</b>";

                    }
                    else
                    {
                        gvItemDetails.DataSource = string.Empty;
                        gvItemDetails.DataBind();
                        divIteminfo.Visible = false;

                    }
                }

                else
                {
                    gvItemDetails.DataSource = string.Empty;
                    gvItemDetails.DataBind();
                    divIteminfo.Visible = false;

                }

            }
            else
            {
                gvItemDetails.DataSource = string.Empty;
                gvItemDetails.DataBind();
                divIteminfo.Visible = false;

            }
            FillPaymentHistory();
            // View Payment Hsitory



            rbppaymentMethod_SelectedIndexChanged(sender, e);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }

    private void ExportGridToExcel()
    {
        try
        {

            if (txtFdt.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
            }

            if (txtTdt.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
            }


            ds = null;
            ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New",
                        new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID" },
                        new string[] { "0", Fdate, Tdate, ddlSociety.SelectedValue }, "dataset");

            DataTable dt = new DataTable();
            dt = ds.Tables[2];

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + "ProducerPaymentProcess" + DateTime.Now + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    private void SendSMS(string MobileNo, string SMSText)
    {
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //Your authentication key
        //string authKey = "3597C1493C124F";
        //Sender ID
        //string senderId = "MPSCDF";

        //string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=" + authKey + "&type=unicode&routeid=288&contacts=" + //MobileNo + "&senderid=" + senderId + "&msg=" + Server.UrlEncode(SMSText);
        string link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + MobileNo + "&text=" + Server.UrlEncode(SMSText) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162468489054757&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=2&camp_name=mpmilk_user";
        HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        Stream stream = response.GetResponseStream();

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string smsstatus = "";
                string smsstext = "";
                string AdjustmentStatus = "0";
                decimal AdjustAmount = 0;
                int Checkboxstatus = 0;
                int CheckBlankVal = 0;
                ds = null;

                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {


                    foreach (GridViewRow row in gvItemDetails.Rows)
                    {
                        CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");

                        if (CheckBox1.Checked == true)
                        {
                            Checkboxstatus = 1;
                            TextBox txtPayableAmount = (TextBox)row.FindControl("txtPayableAmount");
                            //TextBox txtpaymentDate = (TextBox)row.FindControl("txtpaymentDate");

                            if (txtPayableAmount.Text == "" || txtPayableAmount.Text == "0" || txtPayableAmount.Text == "0.00"
                                )
                            {
                                CheckBlankVal = 1;
                            }

                        }
                    }

                    if (Checkboxstatus == 0)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Select At Least One CheckBox Row");
                        return;
                    }


                    if (CheckBlankVal == 1)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Checked Producer Paid Amount / Payment Date Can't Empty / 0 / 0.00");
                        return;
                    }
                    int PaymentStatus = 1;
                    foreach (GridViewRow row in gvItemDetails.Rows)
                    {
                        CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");

                        Label lblProducerId = (Label)row.FindControl("lblProducerId");
                        Label lblMilkValue = (Label)row.FindControl("lblMilkValue");
						Label lblEarningValue = (Label)row.FindControl("lblEarningValue");
                        Label lblSaleValue = (Label)row.FindControl("lblSaleValue");
                        Label lblProducerName = (Label)row.FindControl("lblProducerName");

                        Label lblMobile = (Label)row.FindControl("lblMobile");

                        TextBox txtPayableAmount = (TextBox)row.FindControl("txtPayableAmount");
                        Label lblAdjustAmount = (Label)row.FindControl("lblAdjustAmount");
                      if(txtPayableAmount.Text.Contains("-"))
                      {
                          AdjustmentStatus = "0";
                          //PaymentStatus = 0;
                      }
                      else
                      {
                          AdjustmentStatus = "1";
                         // PaymentStatus = 1;

                      }
                      

                        TextBox txtBank_Name = (TextBox)row.FindControl("txtBank_Name");
                        TextBox txtAccountNo = (TextBox)row.FindControl("txtAccountNo");

                        if (CheckBox1.Checked == true)
                        {
                            //if (txtpaymentDate.Text == "" || txtpaymentDate.Text == "0")
                            //{
                            //    DateTime.ParseExact(System.DateTime.Now.ToString(), "dd/MM/yyyy", cult);
                            //}
                            //else
                            //{
                            //    DateTime date3 = DateTime.ParseExact(txtpaymentDate.Text, "dd/MM/yyyy", cult);

                            //    DateTime Fdt = DateTime.ParseExact(txtFdt.Text, "dd/MM/yyyy", cult);
                            //    DateTime Tdt = DateTime.ParseExact(txtTdt.Text, "dd/MM/yyyy", cult);

                            //    smsstatus = "1";

                            //    smsstext = "Dugdh Praday, your Milk Payment " + txtPaidAmount.Text + " has been processed till date " + txtFdt.Text + " to " + txtTdt.Text + " with your bank -" + txtBank_Name.Text + " and account no - " + txtAccountNo.Text;

                            //    //SendSMS("7489250319", smsstext);

                            //    ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New",
                            //               new string[] { "flag", "Office_ID", "PaymentFromDt", "PaymentToDt", "ProducerId", "MilkValue", "SaleValue",
                            //                   "PayableAmount", "PaidAmount", "UTRNo", "Bank_Name", "PaymentDt", "Remark", "SmsStatus", "SmsText", "CreatedBy","AccountNo" },
                            //               new string[] { "2", objdb.Office_ID(), Fdt.ToString(), Tdt.ToString(),lblProducerId.Text,lblMilkValue.Text,lblSaleValue.Text,
                            //                  txtPayableAmount.Text,txtPaidAmount.Text,txtUTRNo.Text,txtBank_Name.Text,date3.ToString(),txtRemark.Text,smsstatus,smsstext, objdb.createdBy(),txtAccountNo.Text }, "dataset");

                            //}


                            DateTime Fdt = DateTime.ParseExact(txtFdt.Text, "dd/MM/yyyy", cult);
                            DateTime Tdt = DateTime.ParseExact(txtTdt.Text, "dd/MM/yyyy", cult);

                            smsstatus = "1";

                            smsstext = "Dugdh Praday, your Milk Payment " + txtPayableAmount.Text + " has been processed till date " + txtFdt.Text + " to " + txtTdt.Text + " with your bank -" + txtBank_Name.Text + " and account no - " + txtAccountNo.Text;

                            //SendSMS("7489250319", smsstext);

                            ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New",
                                       new string[] { "flag", "Office_ID", "PaymentFromDt", "PaymentToDt", "ProducerId", "MilkValue", "SaleValue","EarningValue",
                                               "PayableAmount","Bank_Name", "SmsStatus", "SmsText", "CreatedBy","AccountNo","AdjustAmount","PaymentStatus","AdjustmentStatus"},
                                       new string[] { "2",ddlSociety.SelectedValue, Fdt.ToString(), Tdt.ToString(),lblProducerId.Text,lblMilkValue.Text,lblSaleValue.Text,lblEarningValue.Text,
                                              txtPayableAmount.Text,txtBank_Name.Text,smsstatus,smsstext, objdb.createdBy(),txtAccountNo.Text,lblAdjustAmount.Text,PaymentStatus.ToString(),AdjustmentStatus.ToString()}, "dataset");


                        }

                    }

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        gvItemDetails.DataSource = string.Empty;
                        gvItemDetails.DataBind();

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        gvItemDetails.DataSource = string.Empty;
                        gvItemDetails.DataBind();

                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }

                btnSubmit_Click(sender, e);

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void rbppaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rbppaymentMethod.SelectedValue == "1")
        {
            gvItemDetails.Visible = true;
            gvItemDetails_Cash.Visible = false;
        }
        if (rbppaymentMethod.SelectedValue == "2")
        {
            gvItemDetails.Visible = false;
            gvItemDetails_Cash.Visible = true;
        }

    }

    protected void btnh2hFile_Click(object sender, EventArgs e)
    {

        try
        {
            string Status = "0";
			string flag = "";
            string ClientCode;
            ViewState["ProducerPayment_ID"] = "";
            if(objdb.Office_ID() == "5")
            {
                
                flag = "10";
                ClientCode = "JSDS31IDBI";
            }
            else if(objdb.Office_ID() == "4")
            {
                flag = "11";
                ClientCode = "ISDS31AXIS";
            }
            else if(objdb.Office_ID() == "7")
            {

                flag = "12";
                ClientCode = "BKSDS31ICICI";
            }
            else if (objdb.Office_ID() == "6")
            {
                flag = "9";
                ClientCode = "USDSMCX";
            }
			else if (objdb.Office_ID() == "3")
            {
                flag = "9";
                
                ClientCode = "GSDSMCX";
            }
            else
            {
                flag = "9";
                ClientCode = "BSD31RBI";
            }
            foreach (GridViewRow rows in GV_PPHistory.Rows)
            {
                CheckBox chk = (CheckBox)rows.FindControl("CheckBox1");
                if(chk.Checked == true)
                {
                    Status = "1";
                    break;
                }
            }
            lblRecordMsg.Text = "";
            lblMsg.Text = "";
            if (txtFdt.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
            }

            if (txtTdt.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
            }
            if (Status == "1")
            {
                ds = null;
                foreach (GridViewRow rows in GV_PPHistory.Rows)
                {
                    CheckBox chk = (CheckBox)rows.FindControl("CheckBox1");
                    Label lblProducerPayment_ID = (Label)rows.FindControl("lblProducerPayment_ID");
                    if (chk.Checked)
                    {
                        ViewState["ProducerPayment_ID"] += lblProducerPayment_ID.Text + ",";
                       
                    }
                }

                //ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New",
                //            new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID", "OfficeID"},
                //            new string[] { flag, Fdate, Tdate, ddlSociety.SelectedValue, objdb.Office_ID()}, "dataset");
                ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New",
                           new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID", "OfficeID", "ProducerPayment_ID_Mlt" },
                           new string[] { flag, Fdate, Tdate, ddlSociety.SelectedValue, objdb.Office_ID(), ViewState["ProducerPayment_ID"].ToString() }, "dataset");

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ViewState["ProducerId"] = "0";
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                           
                            foreach (GridViewRow rows in GV_PPHistory.Rows)
                            {
                                Label lblProducerPayment_ID = (Label)rows.FindControl("lblProducerPayment_ID");
                                Label lblPayableAmount = (Label)rows.FindControl("lblPayableAmount");
                                CheckBox chk = (CheckBox)rows.FindControl("CheckBox1");
                                if (chk.Checked == true)
                                {
                                    if (decimal.Parse(lblPayableAmount.Text) > 0)
                                    {
                                        objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New", new string[] { "flag", "ProducerPayment_ID", "H2HGeneratedStatus" }, new string[] { "8", lblProducerPayment_ID.Text, "1" }, "dataset");
                                    }
                                }
                               
                            }
							if(ds.Tables[2].Rows.Count > 0)
                            {
                                int Count = ds.Tables[2].Rows.Count;
                                for (int i = 0; i < Count; i++)
                                {

                                    if (ds.Tables[2].Rows[i]["Mobile"].ToString() != "")
                                    {
                                        ViewState["Mobile"] = ds.Tables[2].Rows[i]["Mobile"].ToString();
                                        //ViewState["TextMsg"] = "Amount for period" + Convert.ToDateTime(txtFdt.Text, cult).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(txtTdt.Text, cult).ToString("dd/MM/yyyy") +   " released to society, kindly contact, Thanks !";
                                        ViewState["TextMsg"] = "दिनांक " + Convert.ToDateTime(txtFdt.Text, cult).ToString("dd/MM/yyyy")+ " से " + Convert.ToDateTime(txtTdt.Text, cult).ToString("dd/MM/yyyy")+ " समिति को भुगतान हो गया है, कृपया तीन दिवस में संपर्क करे, धन्यवाद !";
                                        SendSMS(ViewState["Mobile"].ToString(), ViewState["TextMsg"].ToString());
                                    }
                                    
                                }
                            }
                            //WriteNew(dt, "BSD31RBI", ds.Tables[1].Rows[0]["V_FileNo"].ToString());
							if (objdb.Office_ID() == "2" || objdb.Office_ID() == "5" || objdb.Office_ID() == "7" || objdb.Office_ID() == "4")
                            {

                                WriteNewforExcel(dt, ClientCode, ds.Tables[1].Rows[0]["V_FileNo"].ToString());
                            }
                            else
                            {
                                WriteNew(dt, ClientCode, ds.Tables[1].Rows[0]["V_FileNo"].ToString());
                            }
							
                        }
                        else
                        {
                            lblRecordMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Producer Data Not Available In Data For This Billing Cycle");
                        }
                    }
                    else
                    {
                        lblRecordMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Producer Data Not Available In Data For This Billing Cycle");
                    }
                }
                else
                {
                    lblRecordMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Producer Data Not Available In Data For This Billing Cycle");
                }
            }
            else
            {
                lblRecordMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Select At Least One CheckBox Row");
            }

        }
        catch (Exception ex)
        {
            lblRecordMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }
    }

    protected void WriteNew(DataTable dt, string ClientCode, string strFileCount)
    {
        try
        {
            string txt = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                int i = 1;
                int ColCount = dt.Columns.Count;
                foreach (DataColumn column in dt.Columns)
                {

                    txt += row[column.ColumnName].ToString();
                    if (i < ColCount)
                    {
                        txt += ",";
                    }
                    i++;
                }

                txt += "\r\n";
            }

            string GetFPath = ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".txt";
            string fileDirPath = @"C:\inetpub\wwwroot\MPCDF_ERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005";
             //string fileDirPath = @"C:\inetpub\wwwroot\MPCDFERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005"; ;
           // string fileDirPath = @"F:\MPCDFERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005";
            FileStream fs1 = new FileStream(fileDirPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs1);
            writer.Write(txt);
            objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New",
                             new string[] { "flag", "H2HClientCode", "FilePath", "MilkCollectionInvoice_ID", "Office_ID" },
                             new string[] { "5", ClientCode, "~/H2H/" + GetFPath, ViewState["ProducerId"].ToString(),objdb.Office_ID()  }, "dataset");
            lblRecordMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "File Successfully Saved - " + fileDirPath);
            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "File Successfully Saved - <a href='" + "E:/CDF_09082020/H2H/" + GetFPath + "' Target='_blank'>View File</a>");
            writer.Close();

            string fName = fileDirPath;

            System.IO.FileStream fs = System.IO.File.Open(fName,
            System.IO.FileMode.Open);
            byte[] btFile = new byte[fs.Length];
            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + GetFPath.Replace("txt", "005")); //hdfNombreArchivo: Nombre del archivo .txt a generarse Ejm: Log_21_08_2018.txt
            EnableViewState = false;
            Response.ContentType = "text";
            Response.BinaryWrite(btFile);
            Response.End();    
        }
        catch (Exception ex)
        {
            lblRecordMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }

    }
    protected void WriteNewforExcel(DataTable dt, string ClientCode, string strFileCount)
    {
        try
        {
            string txt = string.Empty;

           foreach (DataRow row in dt.Rows)
           {
               int i = 1;
               int ColCount = dt.Columns.Count;
               foreach (DataColumn column in dt.Columns)
               {

                   txt += row[column.ColumnName].ToString();
                   if (i < ColCount)
                   {
                       txt += "\t";
                   }
                   i++;
               }

               txt += "\r\n";
           }

            string GetFPath = ClientCode + DateTime.Now.ToString("ddMM") + "-" + strFileCount + ".xls";
            
            string fileDirPath = @"C:\inetpub\wwwroot\MPCDF_ERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "-" + strFileCount + ".xls"; 
            // string fileDirPath = @"F:\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "-" + strFileCount + ".xls";
            FileStream fs1 = new FileStream(fileDirPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs1);
            writer.Write(txt);
            objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New",
                             new string[] { "flag", "H2HClientCode", "FilePath", "MilkCollectionInvoice_ID","Office_ID" },
                             new string[] { "5", ClientCode, "~/H2H/" + GetFPath, ViewState["ProducerId"].ToString(),objdb.Office_ID() }, "dataset");
            lblRecordMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "File Successfully Saved - " + fileDirPath);
            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "File Successfully Saved - <a href='" + "E:/CDF_09082020/H2H/" + GetFPath + "' Target='_blank'>View File</a>");
            writer.Close();

            string attachment = "attachment; filename=" + GetFPath;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
             using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
				if(objdb.Office_ID() == "4")
                {
                    gvh2h.ShowHeader = false;
                }
                else
                {
                    gvh2h.ShowHeader = true;
                }
                gvh2h.DataSource = dt;
                gvh2h.DataBind();


                foreach (GridViewRow row in gvh2h.Rows)
                {

                    foreach (TableCell cell in row.Cells)
                    {

                        cell.CssClass = "textmode";
                    }
                }

                gvh2h.RenderControl(hw);

                //style to format numbers to string


                string style = @"<style> .textmode {mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                //string tab = "";
                //foreach (DataColumn dc in dt.Columns)
                //{
                //    Response.Write(tab + dc.ColumnName);
                //    tab = "\t";
                //}
                //Response.Write("\n");
                //int j;
                //foreach (DataRow dr in dt.Rows)
                //{
                //    tab = "";
                //    for (j = 0; j < dt.Columns.Count; j++)
                //    {

                //        Response.Write(tab + dr[j].ToString());
                //        tab = "\t";

                //    }

                //    Response.Write("\n");

                //}
                ////Response.Write("td { mso-number-format:\@; } ");
                //Response.End();
			}
        }
        catch (Exception ex)
        {
            lblRecordMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }

    }
    protected void FillPaymentHistory()
    {
        try
        {
            lblMsg.Text = "";
            btnUpdate.Visible = false;
            btnh2hFile.Visible = false;
            if (txtFdt.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
            }

            if (txtTdt.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
            }
			string Society = "";

            int SerialNo = 0;
            int totalListItem = ddlSociety.Items.Count;
            foreach (ListItem item in ddlSociety.Items)
            {
                if (item.Selected)
                {
                    SerialNo++;
                    Society += item.Value + ",";

                }
            }
            DataSet DsPPH = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New",
                        new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID_mlt" },
                        new string[] { "3", Fdate, Tdate, Society }, "dataset");

            if (DsPPH != null)
            {
                if (DsPPH.Tables.Count > 0)
                {
                    if (DsPPH.Tables[0].Rows.Count > 0)
                    {
                        btnUpdate.Visible = true;
                        btnh2hFile.Visible = true;
                        GV_PPHistory.DataSource = DsPPH.Tables[0];
                        GV_PPHistory.DataBind();

                        decimal Total = 0;
                        
                        Total = DsPPH.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PayableAmount"));

                        GV_PPHistory.FooterRow.Cells[2].Text = "<b>Total : </b>";
                        GV_PPHistory.FooterRow.Cells[3].Text = "<b>" + Total.ToString() + "</b>";
                    }
                    else
                    {
                        GV_PPHistory.DataSource = string.Empty;
                        GV_PPHistory.DataBind();
                    }
                }
                else
                {
                    GV_PPHistory.DataSource = string.Empty;
                    GV_PPHistory.DataBind();
                }
            }
            else
            {
                GV_PPHistory.DataSource = string.Empty;
                GV_PPHistory.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblRecordMsg.Text = "";
            if (Page.IsValid)
            {
               
                int Checkboxstatus = 0;
                int CheckBlankVal = 0;
                ds = null;

                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {


                    foreach (GridViewRow row in GV_PPHistory.Rows)
                    {
                        CheckBox Checkbox1 = (CheckBox)row.FindControl("Checkbox1");

                        if (Checkbox1.Checked == true)
                        {
                            Checkboxstatus = 1;
                            TextBox txtPaidAmount = (TextBox)row.FindControl("txtPaidAmount");
                            TextBox txtpaymentDate = (TextBox)row.FindControl("txtpaymentDate");
                            TextBox txtUTRNo = (TextBox)row.FindControl("txtUTRNo");
                            if (txtPaidAmount.Text == "" || txtPaidAmount.Text == "0" || txtPaidAmount.Text == "0.00"
                                || txtpaymentDate.Text == "" || txtUTRNo.Text == "")
                            {
                                CheckBlankVal = 1;
                            }
                           

                        }
                    }

                    if (Checkboxstatus == 0)
                    {
                        lblRecordMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Select At Least One CheckBox Row");
                        return;
                    }


                    if (CheckBlankVal == 1)
                    {
                        lblRecordMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Checked Producer Paid Amount / Payment Date /UTRNo Can't Empty /  0 / 0.00");
                        return;
                    }

                    foreach (GridViewRow row in GV_PPHistory.Rows)
                    {
                        CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");
                        TextBox txtPaidAmount = (TextBox)row.FindControl("txtPaidAmount");
                        TextBox txtpaymentDate = (TextBox)row.FindControl("txtpaymentDate");

                        TextBox txtUTRNo = (TextBox)row.FindControl("txtUTRNo");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        Label lblProducerPayment_ID = (Label)row.FindControl("lblProducerPayment_ID");


                        if (CheckBox1.Checked == true)
                        {
                            if (txtpaymentDate.Text == "" || txtpaymentDate.Text == "0")
                            {
                                DateTime.ParseExact(System.DateTime.Now.ToString(), "dd/MM/yyyy", cult);
                            }
                            else
                            {
                                DateTime date3 = DateTime.ParseExact(txtpaymentDate.Text, "dd/MM/yyyy", cult);
                                ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New",
                                           new string[] { "flag", "ProducerPayment_ID", "PaidAmount", "PaymentDt", "UTRNo", "Remark" },
                                           new string[] { "7", lblProducerPayment_ID.Text,txtPaidAmount.Text, date3.ToString(),txtUTRNo.Text,txtRemark.Text
                                             }, "dataset");

                            }

                        }

                    }

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblRecordMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblRecordMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }

                FillPaymentHistory();

            }

        }
        catch (Exception ex)
        {
            lblRecordMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }
    }
    protected void ddlccbmcdetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
}