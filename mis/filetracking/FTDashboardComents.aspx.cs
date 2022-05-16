using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_filetracking_FTDashboardComents : System.Web.UI.Page
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
                if (Request.QueryString["File_ID"] != null)
                {
                    commentsDetail.Visible = true;
                    OutwardDetail.Visible = false;
                    ViewState["File_ID"] = objdb.Decrypt(Request.QueryString["File_ID"].ToString());
                    CommentsFromOfficers();
                    DivForward.Visible = false;
                    ds = objdb.ByProcedure("SpFTDashboard",
                            new string[] { "flag", "File_ID" },
                            new string[] { "6", ViewState["File_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        DetailsView1.DataSource = ds;
                        DetailsView1.DataBind();
                        if (ds.Tables[0].Rows[0]["File_Type"].ToString() == "पत्र")
                        {
                            lblDescription1.InnerText = "पत्र विवरण";
                            FileDescription1.InnerHtml = ds.Tables[0].Rows[0]["File_Description"].ToString();
                            FileDescription.Attributes.Add("style", "background-color:#f4f4f4;padding: 10px;min-height: 150px;");
                            lblDescription.InnerText = "पत्र पर टिप्पणी";
                            txtForwarded_Description.Attributes.Add("style", "background-color:#f4f4f4;padding: 10px;min-height: 150px;");
                        }
                        else
                        {
                            lblDescription1.InnerText = "नोट शीट विवरण";
                            FileDescription1.InnerHtml = ds.Tables[0].Rows[0]["File_Description"].ToString();
                            FileDescription.Attributes.Add("style", "background-color:#bff2d3;padding: 10px;min-height: 150px;");
                            lblDescription.InnerText = "नोट शीट पर टिप्पणी  ";
                            txtForwarded_Description.Attributes.Add("style", "background-color:#bff2d3;padding: 10px;min-height: 150px;");
                        }
                    }
                }
                else if (Request.QueryString["Outward_ID"] != null)
                {
                    ViewState["Outward_ID"] = objdb.Decrypt(Request.QueryString["Outward_ID"].ToString());
                    commentsDetail.Visible = false;
                    OutwardDetail.Visible = true;
                    ds = objdb.ByProcedure("SpFTDashboard",
                            new string[] { "flag", "Outward_ID" },
                            new string[] { "10", ViewState["Outward_ID"].ToString() }, "dataset");
                    if (ds != null && ds.Tables.Count != 0)
                    {
                        DetailsView2.DataSource = ds;
                        DetailsView2.DataBind();

                        GridView1.DataSource = ds.Tables[1];
                        GridView1.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void CommentsFromOfficers()
    {
        if (ViewState["File_ID"] != null || ViewState["SearchFileId"] != null)
        {
            if (ViewState["File_ID"] != null)
            {
                ViewState["File_ID"] = ViewState["File_ID"].ToString();
            }
            else
            {
                ViewState["File_ID"] = ViewState["SearchFileId"].ToString();
            }
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
                }
                else if (Session["Emp_ID"].ToString() != ds.Tables[0].Rows[i]["Emp_ID"].ToString())
                {
                    sb.Append("<span class='direct-chat-name pull-left' style='color:red;'>" + "Forwarded to - " + ds.Tables[0].Rows[i]["Forwarded_Officer"].ToString() + "</span><br/>");
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

                sb.Append("</div></div></div>");
                DivOfficerChat.InnerHtml = sb.ToString();
            }
        }
    }
}