using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;

public partial class mis_filetracking_FTForwardedByOperator : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
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
                    txtDepartment.Attributes.Add("readonly", "readonly");
                    txtForwardDate.Attributes.Add("readonly", "readonly");
                    //FillEmp();
                    DivForward.Visible = false;
                    divDetail.Visible = false;
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
    protected void FillDetailView()
    {
        try
        {
            lblgridMsg.Text = "";
            divDetail.Visible = true;
            ds = objdb.ByProcedure("SpFTComposeFile",
                                new string[] { "flag", "File_ID" },
                                new string[] { "6", ViewState["File_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                DetailsView1.DataSource = ds.Tables[0];
                DetailsView1.DataBind();
                if (ds.Tables[0].Rows[0]["File_Type"].ToString() == "पत्र")
                {
                    if (ds.Tables[0].Rows[0]["File_Status"].ToString() == "Available")
                    {
                        DivForward.Visible = true;
                        lblDescription1.InnerText = "पत्र विवरण";
                        FileDescription1.InnerHtml = ds.Tables[0].Rows[0]["File_Description"].ToString();
                        FileDescription.Attributes.Add("style", "background-color:#f4f4f4;padding: 10px;min-height: 150px;");
                        lblDescription.InnerText = "पत्र पर टिप्पणी";
                        txtForwarded_Description.Attributes.Add("style", "background-color:#f4f4f4;padding: 10px;min-height: 150px;");
                    }
                    else
                    {
                        DivForward.Visible = false;
                        lblDescription1.InnerText = "पत्र विवरण";
                        FileDescription1.InnerHtml = ds.Tables[0].Rows[0]["File_Description"].ToString();
                        FileDescription.Attributes.Add("style", "background-color:#f4f4f4;padding: 10px;min-height: 150px;");
                        lblDescription.InnerText = "पत्र पर टिप्पणी";
                        txtForwarded_Description.Attributes.Add("style", "background-color:#f4f4f4;padding: 10px;min-height: 150px;");
                    }
                }

                else
                {
                    if (ds.Tables[0].Rows[0]["File_Status"].ToString() == "Available")
                    {
                        DivForward.Visible = true;
                        lblDescription1.InnerText = "नोट शीट विवरण";
                        FileDescription1.InnerHtml = ds.Tables[0].Rows[0]["File_Description"].ToString();
                        FileDescription.Attributes.Add("style", "background-color:#bff2d3;padding: 10px;min-height: 150px;");
                        lblDescription.InnerText = "नोट शीट पर टिप्पणी  ";
                        txtForwarded_Description.Attributes.Add("style", "background-color:#bff2d3;padding: 10px;min-height: 150px;");
                    }
                    else
                    {
                        DivForward.Visible = false;
                        lblDescription1.InnerText = "नोट शीट विवरण";
                        FileDescription1.InnerHtml = ds.Tables[0].Rows[0]["File_Description"].ToString();
                        FileDescription.Attributes.Add("style", "background-color:#bff2d3;padding: 10px;min-height: 150px;");
                        lblDescription.InnerText = "नोट शीट पर टिप्पणी  ";
                        txtForwarded_Description.Attributes.Add("style", "background-color:#bff2d3;padding: 10px;min-height: 150px;");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmp()
    {
        try
        {
            ds = objdb.ByProcedure("SpFTComposeFile", new string[] { "flag", "Office_ID", "Emp_ID" }, new string[] { "18", ViewState["Office_ID"].ToString(), ViewState["EmpID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlForwarded_Officer.DataTextField = "Emp_Name";
                ddlForwarded_Officer.DataValueField = "Emp_ID";
                ddlForwarded_Officer.DataSource = ds;
                ddlForwarded_Officer.DataBind();
                ddlForwarded_Officer.Items.Insert(0, new ListItem("चुने", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void CommentsFromOfficers()
    {
        if (ViewState["File_ID"] != null)
        {
            divDetail.Visible = true;
            ds = objdb.ByProcedure("SpFTForwardFile",
              new string[] { "flag", "File_ID" },
              new string[] { "1", ViewState["File_ID"].ToString() }, "dataset");
            int NoOfRow = ds.Tables[0].Rows.Count - 1;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= NoOfRow; i++)
            {
                divComments.Visible = true;
                sb.Append(" <div class='direct-chat-msg'>");
                sb.Append("<div class='direct-chat-info clearfix'>");
                if (ds.Tables[0].Rows[i]["Forwarded_UpdatedOn"].ToString() != null && ds.Tables[0].Rows[i]["Forwarded_UpdatedOn"].ToString() != "")
                {
                    sb.Append("<span class='direct-chat-name pull-left'>" + ds.Tables[0].Rows[i]["Forwarded_Officer"].ToString() + "</span><br/>");
                    sb.Append("<span class='direct-chat-timestamp pull-right'>" + ds.Tables[0].Rows[i]["Forwarded_UpdatedOn"].ToString() + "</span>");    //16 Aug 2:00 pm
                    sb.Append("</div>");
                    sb.Append("<img class='direct-chat-img' src='../image/User1.png' alt='message user image'/>");
                    //.direct-chat-img -->
                    sb.Append("<div class='direct-chat-text form-group' style='word-wrap:break-word'>" + ds.Tables[0].Rows[i]["Forwarded_Description"].ToString());
                    sb.Append("</div>");
                    sb.Append("<span class='direct-chat-timestamp pull-right' style='color:red;font-size:12px;'>" + "Forwarded by - " + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</span><br/>");
                }
                else if (ViewState["Emp_ID"].ToString() != ds.Tables[0].Rows[i]["Emp_ID"].ToString())
                {
                    sb.Append("<span class='direct-chat-name pull-left' style='color:red;'>" + "Available - " + ds.Tables[0].Rows[i]["Forwarded_Officer"].ToString() + "</span><br/>");
                    ViewState["EmpID"] = ds.Tables[0].Rows[i]["Emp_ID"].ToString();
                }
                else if (ViewState["Emp_ID"].ToString() == ds.Tables[0].Rows[i]["Emp_ID"].ToString())
                {
                    sb.Append("<span class='direct-chat-name pull-left' style='color:red;'>" + "Available - " + ds.Tables[0].Rows[i]["Forwarded_Officer"].ToString() + "</span><br/>");
                    ViewState["EmpID"] = ds.Tables[0].Rows[i]["Emp_ID"].ToString();
                }
                sb.Append("<div class='attachment text-right''><br />");
                string Attachment1 = ds.Tables[0].Rows[i]["Document_Upload1"].ToString();
                if (Attachment1 != null && Attachment1 != "")
                {
                    sb.Append(" <a href='../Uploads/" + Attachment1 + "' target='blank'>Attachment 1 </a>");
                }

                string Attachment2 = ds.Tables[0].Rows[i]["Document_Upload2"].ToString();
                if (Attachment2 != null && Attachment2 != "")
                {
                    sb.Append(" <a href='../Uploads/" + Attachment2 + "' target='blank'>Attachment 2</a>");
                }
                sb.Append("</div>");
                DivOfficerChat.InnerHtml = sb.ToString();
            }
            FillEmp();
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            //ViewState["OTP"] = "112233";
            //string GenrateOTP = txtOTP.Text;
            //if (GenrateOTP == ViewState["OTP"].ToString())
            //{
            string msg = "";
            string Forwardedmsg = "";
            string Forwarded_IsActive = "0";
            string ProfileImagePath1 = "";
            string ProfileImagePath2 = "";
            if (txtForwarded_Description.Text == "")
            {
                txtForwarded_Description.Text = "NA";
                // msg += "Enter Forward Discription. \n";
            }
            if (txtDepartment.Text == "")
            {
                msg += "Enter Forward Officer. \n";
            }
            if (ddlForwarded_Officer.SelectedIndex == 0)
            {
                msg += "Select Forward Officer. \n";
            }
            if (txtForwardDate.Text == "")
            {
                msg += "Select Forward Date. \n";
            }
            if (Document_Upload1.HasFile)
            {
                ProfileImagePath1 = "../filetracking/Uploads/" + Guid.NewGuid() + "-" + Document_Upload1.FileName;
                Document_Upload1.PostedFile.SaveAs(Server.MapPath(ProfileImagePath1));
            }
            if (Document_Upload2.HasFile)
            {
                ProfileImagePath2 = "../filetracking/Uploads/" + Guid.NewGuid() + Document_Upload2.FileName;
                Document_Upload2.SaveAs(Server.MapPath(ProfileImagePath2));
            }
            if (msg == "")
            {
                ds = objdb.ByProcedure("SpFTForwardFile",
                           new string[] { "flag", "File_ID" },
                           new string[] { "11", ViewState["File_ID"].ToString() }, "dataset");
                string message = ds.Tables[0].Rows[0]["File_Type"].ToString();
                if (message.ToString() == "फ़ाइल / नोट शीट")
                {
                    Forwardedmsg = "File / Note Sheet";
                }
                else
                {
                    Forwardedmsg = "Letter";
                }
                string message1 = "This " + Forwardedmsg + " has been successfully Forwarded.";
                if (ViewState["File_ID"].ToString() != "")
                {
                    ds = objdb.ByProcedure("SpFTComposeFile",
                          new string[] { "flag", "File_ID" },
                          new string[] { "16", ViewState["File_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        ViewState["Forwarded_ID"] = ds.Tables[0].Rows[0]["Forwarded_ID"].ToString();
                    }
                    objdb.ByProcedure("SpFTForwardFile",
                     new string[] { "flag", "Forwarded_ID", "Forwarded_IsActive", "Forwarded_Description", "Forwarded_Department", "Forwarded_Officer", "Document_Upload1", "Document_Upload2", "File_Status", "FileForwardedByOpt", "Forwarded_UpdatedOn" },
                     new string[] { "10", ViewState["Forwarded_ID"].ToString(), Forwarded_IsActive, txtForwarded_Description.Text, txtDepartment.Text, ddlForwarded_Officer.SelectedItem.Text, ProfileImagePath1, ProfileImagePath2, "Not Available", ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtForwardDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");


                    objdb.ByProcedure("SpFTForwardFile",
                               new string[] { "flag", "Forwarded_IsActive", "File_ID", "Emp_ID", "File_Status", "Forwarded_UpdatedOn" },
                               new string[] { "12", "1", ViewState["File_ID"].ToString(), ddlForwarded_Officer.SelectedValue.ToString(), "Available", Convert.ToDateTime(txtForwardDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + message1 + "');", true);
                    CommentsFromOfficers();
                    txtForwarded_Description.Text = "";
                    txtDepartment.Text = "";
                    ddlForwarded_Officer.ClearSelection();
                    txtForwardDate.Text = "";
                    //txtOTP.Text = "";
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
            //}
            //else
            //{
            //    lblModal.Text = "कृपया सही OTP प्रविष्ट करे";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
            //    txtOTP.Text = "";
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpFTComposeFile",
                               new string[] { "flag", "File_No", "QRCode", "Office_ID" },
                               new string[] { "15", txtFileNo.Text, txtFileNo.Text, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ViewState["File_ID"] = ds.Tables[0].Rows[0]["File_ID"].ToString();
                FillDetailView();
                CommentsFromOfficers();
                txtDepartment.Text = "";
                txtForwardDate.Text = "";
            }
            else
            {
                lblgridMsg.Text = "There is No File / Letter / Bar Code Exists...";
                divDetail.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlForwarded_Officer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlForwarded_Officer.SelectedIndex > 0)
            {
                txtDepartment.Focus();
                ds = objdb.ByProcedure("SpFTComposeFile",
                        new string[] { "flag", "Emp_ID" },
                        new string[] { "17", ddlForwarded_Officer.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtDepartment.Text = ds.Tables[0].Rows[0]["Department_Name"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}