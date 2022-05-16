using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Grievance_GrvDashboardCommentsDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    if (Request.QueryString["Application_ID"] != null)
                    {
                        ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                        ViewState["Application_ID"] = objdb.Decrypt(Request.QueryString["Application_ID"].ToString());
                        FillGrid();
                        FillRelpy();
                        FillOfficerReply();
                    }
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
    protected void FillRelpy()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            ds = null;
            ds = objdb.ByProcedure("SpGrvReplyDetail", new string[] { "flag", "Application_ID" }, new string[] { "1", ViewState["Application_ID"].ToString() }, "dataset");
            int NoOfRecords = ds.Tables[0].Rows.Count;
            if (ds != null && NoOfRecords > 0)
            {
                lblMsg.Text = "";
                lblDepartmentRecord.Text = "";

                for (int i = 0; i < NoOfRecords; i++)
                {
                    sb.Append(" <div class='direct-chat-msg'>");
                    sb.Append("<div class='direct-chat-info clearfix'>");
                    sb.Append("<span class='direct-chat-name pull-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</span><br/>");
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

            }
            else
            {
                lblDepartmentRecord.Text =  "Reply awaiting for feedback...";
                lblDepartmentRecord.Style.Add("color", "Red");
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