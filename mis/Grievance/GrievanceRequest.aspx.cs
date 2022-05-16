using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Web;

public partial class Grievance_GrievanceRequest : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    string Attachment1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null && Session["Emp_Name"] != null)
            {
                if (Request.QueryString["Application_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"];
                    ViewState["Emp_Name"] = Session["Emp_Name"];
                    ViewState["Designation_ID"] = Session["Designation_ID"];
                    ViewState["Application_ID"] = objdb.Decrypt(Request.QueryString["Application_ID"].ToString());
                    DivReply.Visible = true;
                    DivDepartmentReply.Visible = true;
                    DivInternal.Visible = true;
                    FillGrid();
                    FillRelpy();
                    //FillDepartment();
                    FillOfficerReply();
                    FillEmployee();
                }
                else
                {
                    Response.Redirect("~/mis/Grievance/ViewGrievance_Details.aspx");
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
    }
    protected void SendMail(string ApplicantID)
    {
        try
        {
            if (ViewState["EmailID"].ToString() != "")
            {
                string Id = ApplicantID;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.EnableSsl = false;
                mail.From = new MailAddress("carempcdf@gmail.com");
                mail.To.Add(ViewState["EmailID"].ToString());
                mail.Subject = "Regarding Grievance Complaint";
                mail.IsBodyHtml = true;
                string htmlBody;
                string url = HttpUtility.HtmlEncode("http://erpdairy.com/ComplaintsFeedback.aspx?Id=" + objdb.Encrypt(Id));
                htmlBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet'></head><body style='font-family:open sans, sans-serif;'><p>प्रिय उपभोक्ता,</p> <br/><p>आपकी शिकायत क्रमांक : <b>" + ViewState["Application_RefNo"].ToString() + "</b> का निराकरण सफलता पूर्वक कर दिया गया है|</p><br /><br /><br /><p style='margin: 0 0 10px 0;font-size: 14px;'>कृपया अपनी प्रतिक्रिया दें </p><p style='margin: 0 0 8px 0;font-size: 10px;'><span style='font-size:10px' class='btn btn-primary'><a style='padding: 8px;text-decoration: none;background-color: #0a5d0a; color: white;border-radius: 8px;' href=" + url + HttpUtility.HtmlEncode("&Status=" + objdb.Encrypt("Satisfied")) + ">Satisfied</a><a style=' margin-left:30px;     padding: 8px;     text-decoration: none;     background-color: #ca2717;     color: white;     border-radius: 8px;' href=" + url + HttpUtility.HtmlEncode("&Status=" + objdb.Encrypt("Dis-Satisfied")) + ">Dis-Satisfied</a></span></body></html>";
                mail.Body = htmlBody;
                SmtpServer.Credentials = new System.Net.NetworkCredential("carempcdf@gmail.com", "sfa@2365");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
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
            DetailsView1.DataSource = null;
            DetailsView1.DataBind();

            ds = objdb.ByProcedure("SpGrvApplicantDetail", new string[] { "flag", "Application_ID" }, new string[] { "8", ViewState["Application_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                DetailsView1.DataSource = ds;
                DetailsView1.DataBind();
                ViewState["MobileNo"] = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                ViewState["EmailID"] = ds.Tables[0].Rows[0]["EmailID"].ToString();
                ViewState["Application_RefNo"] = ds.Tables[0].Rows[0]["Application_RefNo"].ToString();
                ViewState["Application_GrievanceType"] = ds.Tables[0].Rows[0]["Application_GrievanceType"].ToString();
                Application_Descritption.Text = ds.Tables[0].Rows[0]["Application_Descritption"].ToString();
                string Application_Doc1 = ds.Tables[0].Rows[0]["Application_Doc1"].ToString();
                string Application_Doc2 = ds.Tables[0].Rows[0]["Application_Doc2"].ToString();
                if (Application_Doc1 != "")
                {
                    hyprApplication_Doc1.Visible = true;
                    hyprApplication_Doc1.NavigateUrl = "~/mis/Grievance/Upload_Doc/" + Application_Doc1;
                }
                else
                {
                    hyprApplication_Doc1.Visible = false;
                    hyprApplication_Doc1.NavigateUrl = "";
                }
                if (Application_Doc2 != "")
                {
                    hyprApplication_Doc2.Visible = true;
                    hyprApplication_Doc2.NavigateUrl = "~/mis/Grievance/Upload_Doc/" + Application_Doc2;
                }
                else
                {
                    hyprApplication_Doc2.Visible = false;
                    hyprApplication_Doc2.NavigateUrl = "";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void BtnReply_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlStatus.SelectedIndex <= 0)
            {
                msg = "Please Status Type.\\n";
            }
            if (FUReply_SuppDoc.HasFile)
            {
                Attachment1 = Guid.NewGuid() + "-" + FUReply_SuppDoc.FileName;
                FUReply_SuppDoc.PostedFile.SaveAs(Server.MapPath("~/mis/Grievance/Upload_Doc/" + Attachment1));
            }
            else
            {
                Attachment1 = "";
            }
            if (txtRemark.Text.Trim() == "")
            {
                msg += "Enter Remark. \\n";
            }
            if (msg.Trim() == "")
            {

                if (BtnReply.Text == "Reply")
                {
                    if (ddlStatus.Text == "Open")
                    {
                        DivReply.Visible = true;
                        objdb.ByProcedure("SpGrvReplyDetail",
                            new string[] { "flag", "Emp_ID", "Application_ID", "Designation_ID", "Emp_Name", "Application_GrvStatus", "Reply_SuppDoc", "Reply_Remark", "Reply_UpdatedBy" },
                            new string[] { "2", ViewState["Emp_ID"].ToString(), ViewState["Application_ID"].ToString(), ViewState["Designation_ID"].ToString(), ViewState["Emp_Name"].ToString(), ddlStatus.SelectedItem.Text, Attachment1, txtRemark.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    }
                    if (ddlStatus.Text == "Close")
                    {
                        DivReply.Visible = false;
                        objdb.ByProcedure("SpGrvReplyDetail",
                            new string[] { "flag", "Emp_ID", "Application_ID", "Designation_ID", "Emp_Name", "Application_GrvStatus", "Reply_SuppDoc", "Reply_Remark", "Reply_UpdatedBy" },
                            new string[] { "0", ViewState["Emp_ID"].ToString(), ViewState["Application_ID"].ToString(), ViewState["Designation_ID"].ToString(), ViewState["Emp_Name"].ToString(), ddlStatus.SelectedItem.Text, Attachment1, txtRemark.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                        objdb.ByProcedure("SpGrvApplicantDetail", new string[] { "flag", "Application_ID", "Application_GrvStatus" }, new string[] { "9", ViewState["Application_ID"].ToString(), ddlStatus.SelectedItem.Text }, "dataset");
                        SendMail(ViewState["Application_ID"].ToString());
                        SendSMS();
                    }

                    ddlStatus.ClearSelection();
                    FUReply_SuppDoc.Dispose();
                    txtRemark.Text = "";
                    FillRelpy();
                    FillGrid();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Reply given to citizen.');", true);

                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void SendSMS()
    {
        string MobileNo = ViewState["MobileNo"].ToString();
        string ToUserName = string.Empty;
        string SMS = "शिकायत निराकृत |";
        ServicePointManager.Expect100Continue = true;
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        string link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + MobileNo + "&text=" + Server.UrlEncode(SMS) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407164387736707323&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=2&camp_name=mpmilk_user";
        HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        Stream stream = response.GetResponseStream();
    }
    protected void FillRelpy()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpGrvReplyDetail", new string[] { "flag", "Application_ID" }, new string[] { "1", ViewState["Application_ID"].ToString() }, "dataset");
            int NoOfRecords = ds.Tables[0].Rows.Count;
            if (ds != null && NoOfRecords > 0)
            {
                lblMsg.Text = "";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < NoOfRecords; i++)
                {
                    sb.Append(" <div class='direct-chat-msg'>");
                    sb.Append("<div class='direct-chat-info clearfix'>");
                    sb.Append("<span class='direct-chat-name pull-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + " [" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + "]</span><br/>");

                    sb.Append("<span class='direct-chat-timestamp pull-right'>" + ds.Tables[0].Rows[i]["Reply_UpdatedOn"].ToString() + "</span>");     //16 Aug 2:00 pm
                    sb.Append("</div>");
                    //.direct-chat-info -->
                    sb.Append("<img class='direct-chat-img' src='../image/User1.png' alt='message user image'/>");
                    //.direct-chat-img -->
                    sb.Append("<div class='direct-chat-text form-group' style='word-wrap:break-word; min-height:80px;'>" + ds.Tables[0].Rows[i]["Reply_Remark"].ToString());
                    //sb.Append("RTI Act has been made by legislation of Parliament of India on 15 June 2005.");

                    sb.Append("<div class='attachment text-right''><br />");
                    string Reply_SuppDoc = ds.Tables[0].Rows[i]["Reply_SuppDoc"].ToString();
                    if (Reply_SuppDoc != null && Reply_SuppDoc != "")
                    {
                        sb.Append(" <a href='Upload_Doc/" + Reply_SuppDoc + "' target='blank'>Attachment1</a>");
                    }
                    sb.Append("</div></div></div>");
                    dvChat.InnerHtml = sb.ToString();
                }
                if (ds.Tables[0].Rows[0]["FinalStatus"].ToString() == "Open")
                {
                    DivReply.Visible = true;
                    DivDepartmentReply.Visible = true;
                    DivInternal.Visible = true;
                }
                else
                {
                    DivReply.Visible = false;
                    DivDepartmentReply.Visible = false;
                    DivInternal.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // For Internal Discussioin

    protected void BtnAddOfficer_Click(object sender, EventArgs e)
    {
        try
        {
            Label4.Text = "";
            FillAddDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDepartment()
    {
        try
        {
            Label4.Text = "";
            ds = null;
            ds = objdb.ByProcedure("SpAdminDepartment", new string[] { "flag", "Office_ID" }, new string[] { "6", Session["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlFrwd_Department.DataSource = ds;
                ddlFrwd_Department.DataTextField = "Department_Name";
                ddlFrwd_Department.DataValueField = "Department_ID";
                ddlFrwd_Department.DataBind();
                ddlFrwd_Department.Items.Insert(0, new ListItem("Select", "0"));
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmployee()
    {
        try
        {
            ddlFrwd_OfficerName.Items.Clear();
            ds = null;
            int DepartmentId = 0;
            if (ViewState["Application_GrievanceType"].ToString() == "उत्पाद की गुणवत्ता के बारे में")
            {
                DepartmentId = 36;
            }
            else if (ViewState["Application_GrievanceType"].ToString() == "उत्पाद की उपलब्धता के बारे में" || ViewState["Application_GrievanceType"].ToString() == "वितरक / परिवहनकर्ता सम्बन्धित शिकायत" || ViewState["Application_GrievanceType"].ToString() == "एजेंसी / बूथ / पार्लर / डीपो सम्बन्धित शिकायत ")
            {
                DepartmentId = 47;
            }
            else if (ViewState["Application_GrievanceType"].ToString() == "समिति भुगतान के सम्बन्ध में" || ViewState["Application_GrievanceType"].ToString() == "दूध उत्पादक समिति से सम्बंधित")
            {
                DepartmentId = 46;
            }
            else
            {
                DepartmentId = 0;
            }
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Department_ID", "Emp_ID", "Office_ID" }, new string[] { "44", DepartmentId.ToString(), ViewState["Emp_ID"].ToString(), Session["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlFrwd_OfficerName.DataSource = ds;
                ddlFrwd_OfficerName.DataTextField = "Emp_Name";
                ddlFrwd_OfficerName.DataValueField = "Emp_ID";
                ddlFrwd_OfficerName.DataBind();
                ddlFrwd_OfficerName.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
            }
            // }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillAddDetail()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpGrvInternalDiscussion", new string[] { "flag", "Application_ID" }, new string[] { "2", ViewState["Application_ID"].ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblGridRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblGridRecord.Text = "File is not forwarded yet.";
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlFrwd_Department_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            Label4.Text = "";
            FillEmployee();
            FillAddDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Label4.Text = "";
            string msg = "";
            if (ddlFrwd_Department.SelectedIndex <= 0)
            {
                msg += "Select Department<br/>";
            }
            if (ddlFrwd_OfficerName.SelectedIndex <= 0)
            {
                msg += "Select Officer Name<br/>";
            }
            if (msg == "")
            {
                if (btnAdd.Text == "Add")
                {
                    Label4.Text = "";
                    string Frwd_IsActive = "1";
                    ds = null;
                    ds = objdb.ByProcedure("SpGrvInternalDiscussion", new string[] { "flag", "Application_ID", "Emp_ID" }, new string[] { "1", ViewState["Application_ID"].ToString(), ddlFrwd_OfficerName.SelectedValue }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count == 0)
                    {
                        Label4.Text = "";
                        objdb.ByProcedure("SpGrvInternalDiscussion",
                        new string[] { "flag", "Emp_ID", "Frwd_IsActive", "Application_ID", "Department_ID", "Forward_UpdatedBy" },
                        new string[] { "0", ddlFrwd_OfficerName.SelectedValue, Frwd_IsActive, ViewState["Application_ID"].ToString(), ddlFrwd_Department.SelectedValue, ViewState["Emp_ID"].ToString() }, "dataset");
                        Label4.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        ddlFrwd_Department.ClearSelection();
                        ddlFrwd_OfficerName.ClearSelection();

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    }
                    else if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        Label4.Text = objdb.Alert("fa-ban", "alert-info", "Sorry!", "Grievance is Already Forwarded to This Officer.");
                    }
                    else
                    { }
                    if (GridView1.Rows.Count == 0)
                    {
                        string MobileNo = ViewState["MobileNo"].ToString();
                        string ToUserName = string.Empty;
                        string SMS = "शिकायत वरिष्ठ अधिकारी को प्रेषित |";
                        ServicePointManager.Expect100Continue = true;
                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        string link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + MobileNo + "&text=" + Server.UrlEncode(SMS) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407164387741433654&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=2&camp_name=mpmilk_user";
                        HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                        Stream stream = response.GetResponseStream();
                    }
                    FillAddDetail();
                    ddlFrwd_OfficerName.ClearSelection();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
                }
            }
            else
            {
                Label4.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label4.Text = "";
        string Forward_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
        ds = objdb.ByProcedure("SpGrvInternalDiscussion", new string[] { "flag", "Forward_ID" }, new string[] { "3", Forward_ID.ToString() }, "dataset");
        Label4.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        FillAddDetail();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);

        ddlFrwd_Department.ClearSelection();
        ddlFrwd_OfficerName.ClearSelection();
    }
    protected void btnInternalDiscussion_Click(object sender, EventArgs e)
    {
        try
        {
            Label3.Text = "";
            string msg = "";
            if (txtChat_Remark.Text.Trim() == "")
            {
                msg += "Enter Remark<br/>";
            }
            if (msg == "")
            {
                string Chat_SuppDoc1 = "";
                string Chat_SuppDoc2 = "";

                if (fuChat_Doc1.HasFile)
                {
                    Chat_SuppDoc1 = Guid.NewGuid() + "-" + fuChat_Doc1.FileName;
                }
                else
                {
                    Chat_SuppDoc1 = "";
                }
                if (fuChat_Doc2.HasFile)
                {
                    Chat_SuppDoc2 = Guid.NewGuid() + "-" + fuChat_Doc2.FileName;
                }
                else
                {
                    Chat_SuppDoc2 = "";
                }

                string Chat_IsActive = "1";
                objdb.ByProcedure("SpGrvChat",
                                new string[] { "flag", "Application_ID", "Chat_IsActive", "Chat_EmpID", "Chat_Remark", "Chat_SuppDoc1", "Chat_SuppDoc2" },
                                     new string[] { "0", ViewState["Application_ID"].ToString(), Chat_IsActive, ViewState["Emp_ID"].ToString(), txtChat_Remark.Text.Trim(), Chat_SuppDoc1, Chat_SuppDoc2 }, "dataset");


                if (fuChat_Doc1.HasFile)
                {
                    fuChat_Doc1.PostedFile.SaveAs(Server.MapPath("~/mis/Grievance/Upload_Doc/" + Chat_SuppDoc1));
                }
                else { }
                if (fuChat_Doc2.HasFile)
                {
                    fuChat_Doc2.PostedFile.SaveAs(Server.MapPath("~/mis/Grievance/Upload_Doc/" + Chat_SuppDoc2));
                }
                else { }
                Label3.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                txtChat_Remark.Text = "";
                FillOfficerReply();
            }
            else
            {
                Label3.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOfficerReply()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpGrvChat", new string[] { "flag", "Application_ID" }, new string[] { "2", ViewState["Application_ID"].ToString() }, "dataset");
            int NoOfRecords = ds.Tables[0].Rows.Count;
            if (ds != null && NoOfRecords > 0)
            {
                lblCommentRecord.Text = "";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < NoOfRecords; i++)
                {
                    sb.Append(" <div class='direct-chat-msg'>");
                    sb.Append("<div class='direct-chat-info clearfix'>");
                    sb.Append("<span class='direct-chat-name pull-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</span><br/>");
                    sb.Append("<span class='direct-chat-name pull-left'>[" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + " / " + ds.Tables[0].Rows[i]["Department_Name"].ToString() + "]</span>");
                    sb.Append("<span class='direct-chat-timestamp pull-right'>" + ds.Tables[0].Rows[i]["Chat_UpdatedOn"].ToString() + " , " + ds.Tables[0].Rows[i]["Chat_UpdatedOnTime"].ToString() + "</span>");     //16 Aug 2:00 pm
                    sb.Append("</div>");
                    //.direct-chat-info -->
                    sb.Append("<img class='direct-chat-img' src='../image/User1.png' alt='message user image'/>");
                    //.direct-chat-img -->
                    sb.Append("<div class='direct-chat-text form-group' style='word-wrap:break-word; min-height:80px;'>" + ds.Tables[0].Rows[i]["Chat_Remark"].ToString());


                    sb.Append("<div class='attachment text-right''><br />");
                    string Chat_SuppDoc1 = ds.Tables[0].Rows[i]["Chat_SuppDoc1"].ToString();
                    if (Chat_SuppDoc1 != null && Chat_SuppDoc1 != "")
                    {
                        sb.Append(" <a href='Upload_Doc/" + Chat_SuppDoc1 + "' target='blank'>Attachment 1</a>");
                    }
                    string Chat_SuppDoc2 = ds.Tables[0].Rows[i]["Chat_SuppDoc2"].ToString();
                    if (Chat_SuppDoc2 != null && Chat_SuppDoc2 != "")
                    {
                        sb.Append(" <a href='Upload_Doc/" + Chat_SuppDoc2 + "' target='blank' style='word-wrap:break-word'>/ Attachment 2</a>");
                    }
                    sb.Append("</div></div></div>");
                    divchat.InnerHtml = sb.ToString();


                }
            }
            else if (ds != null && NoOfRecords == 0)
            {
                lblCommentRecord.Text = "No Comments.";
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}