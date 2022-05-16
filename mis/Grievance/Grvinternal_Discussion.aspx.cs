using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Grievance_Grvinternal_Discussion : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null && Session["Emp_Name"] != null)
            {
                if (Request.QueryString["Application_ID"] != null )
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"];
                    ViewState["Emp_Name"] = Session["Emp_Name"];
                    ViewState["Designation_ID"] = Session["Designation_ID"];
                    ViewState["Application_ID"] = objdb.Decrypt(Request.QueryString["Application_ID"].ToString());

                    FillGrid();
                
                    FillOfficerReply();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
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
                Application_Descritption.InnerHtml = ds.Tables[0].Rows[0]["Application_Descritption"].ToString();
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
   
    protected void btnInternalDiscussion_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
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
                                new string[] { "flag",  "Application_ID", "Chat_IsActive", "Chat_EmpID", "Chat_Remark", "Chat_SuppDoc1", "Chat_SuppDoc2" },
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
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                txtChat_Remark.Text = "";
                FillOfficerReply();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
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
                    divChat.InnerHtml = sb.ToString();


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