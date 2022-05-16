using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_SendMail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ddlOffice.Enabled = false;
                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        ddlOffice.Enabled = true;
                    }
                    FillEmployee();
                    FillDetail();
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDetail()
    {
        try
        {
            gvDetails.DataSource = null;
            gvDetails.DataBind();
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" },
                    new string[] { "25", ddlOffice.SelectedValue.ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
            }
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
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblMsg.Text = "";
            if (txtSubject.Text == "")
            {
                msg += "Enter Email Subject. \\n";
            }
            if (txtMessage.InnerText == "")
            {
                msg += "Enter Email Message. \\n";
            }
            if (msg == "")
            {
                string ToEmail = string.Empty;
                string ToUserName = string.Empty, ShowToMainPage = "";
                foreach (GridViewRow gvrow in gvDetails.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    if (chk != null & chk.Checked)
                    {
                        ToEmail += gvrow.Cells[3].Text + ",";
                        ToUserName += gvrow.Cells[2].Text + ",";
                    }
                }
                ToEmail += txtEmail.Text;
                ToUserName += txtEmail.Text;
                if (ToEmail.Trim() != "")
                {
                    MailMessage mail = new MailMessage();
                    //mail.From = new MailAddress("keralaslbc@gmail.com");
                    //mail.From = new MailAddress("carempagro@gmail.com");
                    mail.From = new MailAddress("carempcdf@gmail.com");
                    mail.ReplyTo = new MailAddress("carempcdf@gmail.com");
                    string[] Emails = ToEmail.Split(',');
                   foreach (string mail1 in Emails)
                   {
                       if (mail1 != "")
                           mail.To.Add(mail1);
                   }
				  // mail.To.Add(mail1);
                    //mail.To.Add("deshmukhhimani26@gmail.com");
                    mail.Subject = txtSubject.Text;

                    mail.IsBodyHtml = true;
                    string Body;

                    // Body = txtMessage.Text;
                    Body = txtMessage.InnerText;
                    mail.Body = Body;

                    string fileName = "", filepath = "";
                    if (FU_Attachment.HasFile)
                    {
                        filepath = "../Circular/" + Guid.NewGuid() + "-" + FU_Attachment.FileName;
                        FU_Attachment.PostedFile.SaveAs(Server.MapPath(filepath));

                        fileName = Path.GetFileName(FU_Attachment.PostedFile.FileName);
                        mail.Attachments.Add(new Attachment(FU_Attachment.PostedFile.InputStream, fileName));
                    }

                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                    //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 25);
                    //SmtpServer.EnableSsl = false;
                    //SmtpServer.Port = 25;
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("carempagro@gmail.com", "mpagro@1235");//ADD 5 IN PASSWORD
                    SmtpServer.Credentials = new System.Net.NetworkCredential("carempcdf@gmail.com", "sfa@2365");

                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    /*******************/
                    if (chkbox.Checked)
                    {
                        ShowToMainPage = "1";
                    }
                    else
                    {
                        ShowToMainPage = "0";
                    }
                    ds = objdb.ByProcedure("SpEmailBuilder",
                           new string[] { "flag", "EmailBody", "EmailSubject", "EmailSentTo", "EmailAttachment", "EmailSentBy", "EmailType", "EmailSentToList", "ShowToMainPage" },
                           new string[] { "1", txtMessage.InnerText.ToString(), txtSubject.Text.ToString(), ToEmail, filepath, ViewState["Emp_ID"].ToString(), "EMAILBUILDER", ToUserName, ShowToMainPage }, "dataset");

                    /*******************/
                    txtEmail.Text = "";
                    txtSubject.Text = "";
                    txtMessage.InnerText = "";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Email / Circular sent Successfully.");
                }
                else
                {
                    msg = "Please Enter atleast one Email address or Select one Employee from below Table!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("SendMail.aspx");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}