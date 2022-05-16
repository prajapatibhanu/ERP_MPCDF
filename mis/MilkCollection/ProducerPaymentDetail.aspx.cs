using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.Net;

public partial class mis_MilkCollection_ProducerPaymentDetail : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString(); 
                txtFdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                txtTdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");

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

    protected void FillSociety()
    {
        try
        {
            if (ddlMilkCollectionUnit.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                                  new string[] { "10", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[2];
                        ddlSociety.DataBind();
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
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
            else
            {
                Response.Redirect("ProducerPaymentDetail.aspx", false);
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

            divIteminfo.Visible = false;


            if (txtFdt.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
            }

            if (txtTdt.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = null;
            ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                        new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID" },
                        new string[] { "0", Fdate, Tdate, ddlSociety.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        divIteminfo.Visible = true;

                        gvItemDetails.DataSource = ds.Tables[1];
                        gvItemDetails.DataBind();
                        gvItemDetails_Cash.DataSource = ds.Tables[1];
                        gvItemDetails_Cash.DataBind();

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

            // View Payment Hsitory

            DataSet DsPPH = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                        new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID" },
                        new string[] { "3", Fdate, Tdate, ddlSociety.SelectedValue }, "dataset");

            if (DsPPH != null)
            {
                if (DsPPH.Tables.Count > 0)
                {
                    if (DsPPH.Tables[0].Rows.Count > 0)
                    {
                        GV_PPHistory.DataSource = DsPPH.Tables[0];
                        GV_PPHistory.DataBind();

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
            ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
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

        //Your authentication key
        string authKey = "3597C1493C124F";
        //Sender ID
        string senderId = "MPSCDF";

        string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=" + authKey + "&type=unicode&routeid=2&contacts=" + MobileNo + "&senderid=" + senderId + "&msg=" + Server.UrlEncode(SMSText);
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
                            TextBox txtpaymentDate = (TextBox)row.FindControl("txtpaymentDate");

                            if (txtPayableAmount.Text == "" || txtPayableAmount.Text == "0" || txtPayableAmount.Text == "0.00"
                                && txtpaymentDate.Text == "")
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

                    foreach (GridViewRow row in gvItemDetails.Rows)
                    {
                        CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");

                        Label lblProducerId = (Label)row.FindControl("lblProducerId");
                        Label lblMilkValue = (Label)row.FindControl("lblMilkValue");
                        Label lblSaleValue = (Label)row.FindControl("lblSaleValue");
                        Label lblProducerName = (Label)row.FindControl("lblProducerName");

                        Label lblMobile = (Label)row.FindControl("lblMobile");

                        TextBox txtPayableAmount = (TextBox)row.FindControl("txtPayableAmount");
                        TextBox txtPaidAmount = (TextBox)row.FindControl("txtPaidAmount");
                        TextBox txtpaymentDate = (TextBox)row.FindControl("txtpaymentDate");

                        TextBox txtUTRNo = (TextBox)row.FindControl("txtUTRNo");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

                        TextBox txtBank_Name = (TextBox)row.FindControl("txtBank_Name");
                        TextBox txtAccountNo = (TextBox)row.FindControl("txtAccountNo");

                        if (CheckBox1.Checked == true)
                        {
                            if (txtpaymentDate.Text == "" || txtpaymentDate.Text == "0")
                            {
                                DateTime.ParseExact(System.DateTime.Now.ToString(), "dd/MM/yyyy", cult);
                            }
                            else
                            {
                                DateTime date3 = DateTime.ParseExact(txtpaymentDate.Text, "dd/MM/yyyy", cult);

                                DateTime Fdt = DateTime.ParseExact(txtFdt.Text, "dd/MM/yyyy", cult);
                                DateTime Tdt = DateTime.ParseExact(txtTdt.Text, "dd/MM/yyyy", cult);

                                smsstatus = "1";

                                smsstext = "Dugdh Praday, your Milk Payment " + txtPaidAmount.Text + " has been processed till date " + txtFdt.Text + " to " + txtTdt.Text + " with your bank -" + txtBank_Name.Text + " and account no - " + txtAccountNo.Text;

                                //SendSMS("7489250319", smsstext);

                                ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                                           new string[] { "flag", "Office_ID", "PaymentFromDt", "PaymentToDt", "ProducerId", "MilkValue", "SaleValue",
                                               "PayableAmount", "PaidAmount", "UTRNo", "Bank_Name", "PaymentDt", "Remark", "SmsStatus", "SmsText", "CreatedBy","AccountNo" },
                                           new string[] { "2", objdb.Office_ID(), Fdt.ToString(), Tdt.ToString(),lblProducerId.Text,lblMilkValue.Text,lblSaleValue.Text,
                                              txtPayableAmount.Text,txtPaidAmount.Text,txtUTRNo.Text,txtBank_Name.Text,date3.ToString(),txtRemark.Text,smsstatus,smsstext, objdb.createdBy(),txtAccountNo.Text }, "dataset");

                            }
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
            if (txtFdt.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
            }

            if (txtTdt.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = null;
            ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                        new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID" },
                        new string[] { "4", Fdate, Tdate, ddlSociety.SelectedValue }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        WriteNew(dt, "BSD31RBI", ds.Tables[1].Rows[0]["V_FileNo"].ToString());
                        //code changes by mohini on 3oct2020
                        foreach (GridViewRow rows in GV_PPHistory.Rows)
                        {
                            Label lblProducerPayment_ID = (Label)rows.FindControl("lblProducerPayment_ID");
                            Label lblPayableAmount = (Label)rows.FindControl("lblPayableAmount");
                            if (decimal.Parse(lblPayableAmount.Text) > 0)
                            {
                                objdb.ByProcedure("USP_Trn_ProducerPaymentDetails_New", new string[] { "flag", "ProducerPayment_ID", "H2HGeneratedStatus" }, new string[] { "8", lblProducerPayment_ID.Text, "1" }, "dataset");
                            }
                        }

                        //code changes by mohini on 3oct2020
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Producer Data Not Available In Data For This Billing Cycle");
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Producer Data Not Available In Data For This Billing Cycle");
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Producer Data Not Available In Data For This Billing Cycle");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }
    }

    protected void WriteNew(DataTable dt, string ClientCode, string strFileCount)
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
       string fileDirPath = @"C:\inetpub\wwwroot\MPCDFERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005"; ;
        //string fileDirPath = @"F:\MPCDF_ERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005"; 
        FileStream fs1 = new FileStream(fileDirPath, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter writer = new StreamWriter(fs1);
        writer.Write(txt); 
        objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                         new string[] { "flag", "H2HClientCode", "FilePath" },
                         new string[] { "5", ClientCode, "~/H2H/" + GetFPath }, "dataset");
        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "File Successfully Saved - " + fileDirPath);
        //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "File Successfully Saved - <a href='" + "E:/CDF_09082020/H2H/" + GetFPath + "' Target='_blank'>View File</a>");
        writer.Close();

    }


}