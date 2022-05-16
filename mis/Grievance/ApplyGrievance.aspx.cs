using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class mis_Grievance_ApplyGrievance : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    string Attachment1, Attachment2;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["OfficeType_ID"] = Session["OfficeType_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["Application_ID"] = "0";
                ddlDSName.Enabled = true;
                FillDistrict();
                FillDSName();
                //FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillDistrict()
    {
        try
        {
            ddlDistrict.ClearSelection();
            ds = objdb.ByProcedure("SpAdminDistrict", new string[] { "flag" }, new string[] { "16" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataSource = ds;
                ddlDistrict.DataBind();
            }
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDSName()
    {
        try
        {
            ddlDSName.ClearSelection();
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" },
                 new string[] { "57" }, "datatset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDSName.DataTextField = "Office_Name";
                ddlDSName.DataValueField = "Office_ID";
                ddlDSName.DataSource = ds;
                ddlDSName.DataBind();
            }
            ddlDSName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
			lblMsg.Text ="";
            string msg = "";
            string Applicantion_IsActive = "1";
            if (ddlGrvType.SelectedIndex == 0)
            {
                msg += "Please Select Grievance Type.\\n";
            }
            if (ddlDistrict.SelectedIndex == 0)
            {
                msg += "Please Select District.\\n";
            }
            if (txtLocation.Text == "")
            {
                msg += " Please Enter Location.\\n";
            }
            if (txtGrvDescription.Text == "")
            {
                msg += "Please Enter Description.\\n";
            }
            if (txtSuppDoc1.HasFile)
            {
                Attachment1 = Guid.NewGuid() + "-" + txtSuppDoc1.FileName;
                txtSuppDoc1.PostedFile.SaveAs(Server.MapPath("~/mis/Grievance/Upload_Doc/" + Attachment1));
            }
            else
            {
                Attachment1 = "";
            }
            if (txtSuppDoc2.HasFile)
            {
                Attachment2 = Guid.NewGuid() + "-" + txtSuppDoc2.FileName;
                txtSuppDoc2.PostedFile.SaveAs(Server.MapPath("~/mis/Grievance/Upload_Doc/" + Attachment2));
            }
            else
            {
                Attachment2 = "";
            }
            if (msg == "")
            {
                string AppRefNo = "";
                string AppGrvType = "";
                if (BtnSubmit.Text == "Submit")
                {

                    ds = objdb.ByProcedure("SpGrvApplicantDetail",
                            new string[] { "flag", "Emp_ID", "Office_ID", "Complaint_Name", "ContactNo", "Applicantion_IsActive", "Application_GrvStatus", "DistrictID", "EmailID", "ComplaintNo", "Location", "Application_Subject", "Application_GrievanceType", "Application_Descritption", "Application_Doc1", "Application_Doc2", "Application_UpdatedBy" },
                            new string[] { "0", ViewState["Emp_ID"].ToString(), ddlDSName.SelectedValue.ToString(), txtComplaintName.Text.Trim(), txtContactNo.Text, Applicantion_IsActive, "Open", ddlDistrict.SelectedValue.ToString(), txtEmail.Text.Trim(), txtComplaintNo.Text.Trim(), txtLocation.Text, txtSubject.Text, ddlGrvType.SelectedItem.Text, txtGrvDescription.Text, Attachment1, Attachment2, ViewState["Emp_ID"].ToString() }, "Dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        AppRefNo = ds.Tables[0].Rows[0]["Application_RefNo"].ToString();
                        AppGrvType = ds.Tables[0].Rows[0]["Application_GrievanceType"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed, Your Refrence No is " + AppRefNo + ".");
                    }
                    if (txtEmail.Text.Trim() != "" && ddlGrvType.SelectedValue.ToString() != "10")
                    {
                        SendMail(AppRefNo, AppGrvType);
                    }
                    if (txtContactNo.Text != "" && ddlGrvType.SelectedValue.ToString() != "10")
                    {
                        string URL = "http://erpdairy.com/MPCDF_Webservice.asmx/SendComplaintMsg";
                        URL = URL + "?key=SFA_MPCDF-ERP" +
                        "&MobileNo=" + txtContactNo.Text +
                        "&AppRefNo=" + AppRefNo;
                        var request = (HttpWebRequest)WebRequest.Create(URL);
                        request.Method = "GET";
                        var response = (HttpWebResponse)request.GetResponse();
                        string jsonString = string.Empty;
                        using (var stream = response.GetResponseStream())
                        {
                            using (var sr = new StreamReader(stream))
                            {
                                jsonString = sr.ReadToEnd();
                            }
                        }
                    }
                    ClearText();
                    //FillGrid();
                }
                else if (BtnSubmit.Text == "Edit")
                {
                    objdb.ByProcedure("SpGrvApplicantDetail",
                           new string[] { "flag", "Application_ID", "Emp_ID", "Complaint_Name", "ContactNo", "Applicantion_IsActive", "Application_GrvStatus", "DistrictID", "EmailID", "ComplaintNo", "Location", "Application_Subject", "Application_GrievanceType", "Application_Descritption", "Application_Doc1", "Application_Doc2", "Application_UpdatedBy" },
                           new string[] { "1", ViewState["Application_ID"].ToString(), ViewState["Emp_ID"].ToString(), txtComplaintName.Text.Trim(), txtContactNo.Text, Applicantion_IsActive, "Open", ddlDistrict.SelectedValue.ToString(), txtEmail.Text.Trim(), txtComplaintNo.Text.Trim(), txtLocation.Text, txtSubject.Text, ddlGrvType.SelectedItem.Text, txtGrvDescription.Text, Attachment1, Attachment2, ViewState["Emp_ID"].ToString() }, "Dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    BtnSubmit.Text = "Submit";
                    ClearText();
                    //FillGrid();

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
    protected void SendMail(string AppRefNo, string AppGrvType)
    {
        try
        {
            if (txtEmail.Text.Trim() != "")
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("carempcdf@gmail.com");
                mail.ReplyTo = new MailAddress("carempcdf@gmail.com");
                mail.To.Add(txtEmail.Text.Trim());
                mail.Subject = "Regarding Grievance Complaint";
                mail.IsBodyHtml = true;
                string Body;
                Body = "<h3>प्रिय उपभोक्ता, </h3></br><p>आपकी शिकायत विषय : <b>" + AppGrvType + "</b>,शिकायत क्रमांक : <b>" + AppRefNo + "</b> सफलता पूर्वक दर्ज कर दी गई है, इस शिकायत का निराकरण जल्द ही किया जावेगा |</br></br></br></br><h3>धन्यवाद ,</h3></br><h3>साँची सेवा</h3></p>";
                mail.Body = Body;
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.UseDefaultCredentials = true;
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
    protected void ClearText()
    {
        txtSubject.Text = "";
        txtComplaintName.Text = "";
        txtContactNo.Text = "";
        txtComplaintNo.Text = "";
        txtGrvDescription.Text = "";
        txtSuppDoc1.Dispose();
        txtSuppDoc2.Dispose();
        txtLocation.Text = "";
        ddlDistrict.ClearSelection();
		ddlDSName.ClearSelection();
        ddlGrvType.ClearSelection();
    }
    //protected void FillGrid()
    //{
    //    try
    //    {
    //        GridView1.DataSource = null;
    //        GridView1.DataBind();
    //        ds = objdb.ByProcedure("SpGrvApplicantDetail", new string[] { "flag", "Emp_ID" }, new string[] { "11", ViewState["Emp_ID"].ToString() }, "dataset");
    //        if (ds.Tables[0].Rows.Count != 0)
    //        {
    //            GridView1.DataSource = ds;
    //            GridView1.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        ClearText();
    //        ViewState["Application_ID"] = GridView1.SelectedDataKey.Value.ToString();
    //        ds = objdb.ByProcedure("SpGrvApplicantDetail", new string[] { "flag", "Application_ID" }, new string[] { "3", ViewState["Application_ID"].ToString() }, "dataset");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txtSubject.Text = ds.Tables[0].Rows[0]["Application_Subject"].ToString();

    //            ddlGrvType.Items.FindByValue(ds.Tables[0].Rows[0]["Application_GrievanceType"].ToString()).Selected = true;
    //            txtGrvDescription.Text = ds.Tables[0].Rows[0]["Application_Descritption"].ToString();
    //            BtnSubmit.Text = "Edit";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        ClearText();
    //        GridView1.PageIndex = e.NewPageIndex;
    //        FillGrid();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
			lblMsg.Text ="";
            ddlDSName.ClearSelection();
            if (ddlDistrict.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpAdminDistrict", new string[] { "flag", "District_ID" },
                                  new string[] { "17", ddlDistrict.SelectedValue.ToString() }, "datatset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDSName.SelectedValue = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                    ddlDSName.Enabled = false;
                }
                ddlDSName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}