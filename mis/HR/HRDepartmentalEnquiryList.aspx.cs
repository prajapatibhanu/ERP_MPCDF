using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_HR_HRDepartmentalEnquiryList : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Office_Name"] = Session["Office_Name"].ToString();
                    ViewState["Emp_Name"] = Session["Emp_Name"].ToString();
                    txtOrderDate.Attributes.Add("readonly", "readonly");
                    txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    FillGrid();
                    FillDropdown();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlPresentingOfficer.DataSource = ds;
                ddlPresentingOfficer.DataTextField = "Emp_Name";
                ddlPresentingOfficer.DataValueField = "Emp_ID";
                ddlPresentingOfficer.DataBind();
                ddlPresentingOfficer.Items.Insert(0, new ListItem("Select Presenting Officer", "0"));


                ddlEnquiryOfficer.DataSource = ds;
                ddlEnquiryOfficer.DataTextField = "Emp_Name";
                ddlEnquiryOfficer.DataValueField = "Emp_ID";
                ddlEnquiryOfficer.DataBind();
                ddlEnquiryOfficer.Items.Insert(0, new ListItem("Select Enquiry Officer", "0"));
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
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds != null && ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid2()
    {
        try
        {
            GridView2.DataSource = new string[] { };
            GridView2.DataBind();
            ds = objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag", "DepartmentEnquiry_ID" }, new string[] { "5", ViewState["DepartmentEnquiry_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                    foreach (GridViewRow row in GridView2.Rows)
                    {
                        Label Status = (Label)row.FindControl("lblStatus");
                        if (Status.ToolTip.ToString() == "Close")
                        {
                            btnSave.Enabled = false;
                        }
                        else
                        {
                            btnSave.Enabled = true;
                        }
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
                    lblMsg.Text = "";
                }

                if (ds.Tables[1].Rows.Count != 0)
                {
                    GridView3.DataSource = ds.Tables[1];
                    GridView3.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
                    lblMsg.Text = "";
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["DepartmentEnquiry_ID"] = GridView1.SelectedDataKey.Value.ToString();
            //GridViewRow row = GridView1.SelectedRow.Cells[1].Text;
            ds = objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag", "DepartmentEnquiry_ID" }, new string[] { "3", ViewState["DepartmentEnquiry_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                txtOrderNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
            }
            FillGrid2();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        try
        {
            ddlEnquiryOfficer.ClearSelection();
            ddlPresentingOfficer.ClearSelection();
            txtENQ_Remark.Text = "";
            txtWitnessMob.Text = "";
            txtWitnessName.Text = "";
            txtEmailId.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (txtOrderNo.Text == "")
            {
                msg = msg + "Enter Order No.//n";
            }
            if (txtOrderDate.Text == "")
            {
                msg = msg + "Enter Order Date.//n";
            }
            if (chkEO.Checked == true)
            {
                if (ddlEnquiryOfficer.SelectedIndex == 0)
                {
                    msg = msg + "Select Enquiry Officer.//n";
                }
            }
            else
            {
                if (txtOtherEnquiryOfficer.Text == "")
                {
                    msg = msg + "Enter Other Enquiry Officer.//n";
                }
            }

            if (chkPO.Checked == true)
            {
                if (ddlPresentingOfficer.SelectedIndex == 0)
                {
                    msg = msg + "Select Enquiry Officer.//n";
                }
            }
            else
            {
                if (txtOtherPresentingOfficer.Text == "")
                {
                    msg = msg + "Enter Other Presenting Officer.//n";
                }
            }
            if (RblStatus.SelectedItem.Text == "Close")
            {
                if (!Fu_UploadDoc.HasFile)
                {
                    msg = msg + "Upload File.\n";
                }
            }
            if (msg == "")
            {
                string UploadDoc = "";
                if (Fu_UploadDoc.HasFile)
                {
                    UploadDoc = "../HR/EnquiryDoc/" + Guid.NewGuid() + "-" + Fu_UploadDoc.FileName;
                    Fu_UploadDoc.PostedFile.SaveAs(Server.MapPath(UploadDoc));
                }

                objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag", "DepartmentEnquiry_ID", "Status", "ENQ_OrderNo", "ENQ_OrderDate", "EnquiryOfficer", "PresentingOfficer", "OtherEnquiryOfficer", "OtherPresentingOfficer", "CloseAttachedDoc", "ENQ_Remark", "UpdatedBy" },
      new string[] { "4", ViewState["DepartmentEnquiry_ID"].ToString(), RblStatus.SelectedItem.Text, txtOrderNo.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd"), ddlEnquiryOfficer.SelectedItem.Text, ddlPresentingOfficer.SelectedItem.Text, txtOtherEnquiryOfficer.Text, txtOtherPresentingOfficer.Text, UploadDoc, txtENQ_Remark.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                ClearText();
                FillGrid2();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSaveWitness_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (txtWitnessName.Text == "")
            {
                msg = msg + "Enter Witness Name.//n";
            }
            if (txtWitnessMob.Text == "")
            {
                msg = msg + "Enter Witness Mobile.//n";
            }
            if (txtEmailId.Text == "")
            {
                msg = msg + "Enter Witness Email Id.//n";
            }
            if (msg == "")
            {
                objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag", "DepartmentEnquiry_ID", "Witness_IsActive", "Witness_Name", "Witness_Mobile", "Witness_Email", "UpdatedBy" },
      new string[] { "7", ViewState["DepartmentEnquiry_ID"].ToString(), "1", txtWitnessName.Text, txtWitnessMob.Text, txtEmailId.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                SendMail();
                ClearText();
                FillGrid2();
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
    protected void SendMail()
    {
        string ToEmail = string.Empty;
        string ToUserName = string.Empty, ShowToMainPage = "";
        if (txtEmailId.Text.Trim() != "")
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("carempcdf@gmail.com");
            mail.ReplyTo = new MailAddress("carempcdf@gmail.com");
            mail.To.Add(txtEmailId.Text.Trim());
            mail.Subject = "Regarding Departmental Enquiry";
            mail.IsBodyHtml = true;
            string Body;
            Body = "Hello " + txtWitnessName.Text + "</b></b> You are the witness of the Departmental enquiry Order No : <b>" + txtOrderNo.Text + "</b> .";
            mail.Body = Body;
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.Credentials = new System.Net.NetworkCredential("carempcdf@gmail.com", "sfa@2365");

            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
    }
    protected void RblStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (RblStatus.SelectedItem.Text == "Close")
            {
                UploadDoc.Visible = true;
            }
            else
            {
                UploadDoc.Visible = false;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkEO_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEO.Checked == true)
        {
            ddlEnquiryOfficer.Enabled = true;
            DivOtherEO.Style.Add("display", "none");
        }
        else
        {
            ddlEnquiryOfficer.Enabled = false;
            DivOtherEO.Style.Add("display", "block");
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
    }
    protected void chkPO_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPO.Checked == true)
        {
            ddlPresentingOfficer.Enabled = true;
            DivOtherPO.Style.Add("display", "none");
        }
        else
        {
            ddlPresentingOfficer.Enabled = false;
            DivOtherPO.Style.Add("display", "block");
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
    }
}