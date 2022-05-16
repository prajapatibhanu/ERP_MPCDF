using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_RTI_RTIDashboardCommentsDetail : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1, ds2;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Emp_Name"] != null && Request.QueryString["RTI_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    lblRTIReply.Text = "";
                    lblCommentRecord.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"];
                    ViewState["Emp_Name"] = Session["Emp_Name"];
                    ViewState["App_ID"] = "";
                    ViewState["RTI_ID"] = objdb.Decrypt(Request.QueryString["RTI_ID"]);
                    ViewState["Rply_RTIRequestType"] = "RTI Request";
                    FillDetail();
                    FillRelpy();
                    FillRelpy2();
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
            ds = null;
            ds = objdb.ByProcedure("SpRtiReplyDetail", new string[] { "flag", "RTI_ID" }, new string[] { "11", ViewState["RTI_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                // lblMsg.Text = "";

                // For RTI DetailView
                DetailsView1.DataSource = ds;
                DetailsView1.DataBind();
                RTIDetails.Text = ds.Tables[0].Rows[0]["RTI_Request"].ToString();
                // RTIDetails.InnerHtml = ds.Tables[0].Rows[0]["RTI_Request"].ToString();
                string RTI_RequestDoc = ds.Tables[0].Rows[0]["RTI_RequestDoc"].ToString();
                if (RTI_RequestDoc != "")
                {
                    hyprRTI_RequestDoc.Visible = true;
                    hyprRTI_RequestDoc.NavigateUrl = "~/mis/RTI/RTI_Docs/" + RTI_RequestDoc;
                }
                else
                {
                    hyprRTI_RequestDoc.Visible = false;
                    hyprRTI_RequestDoc.NavigateUrl = "";
                }

                // For First Appeal DetailView
                DetailsView2.DataSource = ds;
                DetailsView2.DataBind();
                string BPLStatus = ds.Tables[0].Rows[0]["App_PLStatus"].ToString();
                if (BPLStatus == "Yes (हाँ)")
                {
                    DetailsView3.DataSource = ds;
                    DetailsView3.DataBind();
                    divPaymentDetail.Visible = false;
                }
                if (BPLStatus == "No (नहीं)")
                {
                    DetailsView3.DataSource = ds;
                    DetailsView3.DataBind();
                    divPaymentDetail.Visible = true;
                }


                string closesatus = ds.Tables[0].Rows[0]["ShowRTIStatus"].ToString();
            }
            else { }
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
            ds = null;
            ds = objdb.ByProcedure("SpRtiReplyDetail", new string[] { "flag", "RTI_ID", "Rply_RTIRequestType" }, new string[] { "8", ViewState["RTI_ID"].ToString(), ViewState["Rply_RTIRequestType"].ToString() }, "dataset");
            int NoOfRecords = ds.Tables[0].Rows.Count;
            if (ds != null && NoOfRecords > 0)
            {
                //lblMsg.Text = "";
                dvRTIRplyComment.Visible = true;
                lblRTIReply.Text = "";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < NoOfRecords; i++)
                {
                    sb.Append(" <div class='direct-chat-msg'>");
                    sb.Append("<div class='direct-chat-info clearfix'>");
                    sb.Append("<span class='direct-chat-name pull-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</span><br/>");
                    sb.Append("<span class='direct-chat-name pull-left'>[" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + " / " + ds.Tables[0].Rows[i]["Department_Name"].ToString() + "]</span>");
                    sb.Append("<span class='direct-chat-timestamp pull-right'>" + ds.Tables[0].Rows[i]["Rply_UpdatedOn"].ToString() + " , " + ds.Tables[0].Rows[i]["Rply_UpdatedTimeOn"].ToString() + "</span>");     //16 Aug 2:00 pm
                    sb.Append("</div>");
                    //.direct-chat-info -->
                    sb.Append("<img class='direct-chat-img' src='../image/User1.png' alt='message user image'/>");
                    //.direct-chat-img -->
                    sb.Append("<div class='direct-chat-text form-group' style='word-wrap:break-word; min-height:80px;'>" + ds.Tables[0].Rows[i]["Rply_RTIRemark"].ToString());
                    //sb.Append("RTI Act has been made by legislation of Parliament of India on 15 June 2005.");

                    sb.Append("<div class='attachment text-right''><br />");
                    string Rply_RTIDoc1 = ds.Tables[0].Rows[i]["Rply_RTIDoc1"].ToString();
                    if (Rply_RTIDoc1 != null && Rply_RTIDoc1 != "")
                    {
                        sb.Append(" <a href='RTI_Docs/" + Rply_RTIDoc1 + "' target='blank'>Attachment 1</a>");
                    }
                    string Rply_RTIDoc2 = ds.Tables[0].Rows[i]["Rply_RTIDoc2"].ToString();
                    if (Rply_RTIDoc2 != null && Rply_RTIDoc2 != "")
                    {
                        sb.Append(" <a href='RTI_Docs/" + Rply_RTIDoc2 + "' target='blank' style='word-wrap:break-word'>/ Attachment 2</a>");
                    }
                    sb.Append("</div></div></div>");
                    dvChat.InnerHtml = sb.ToString();
                    // Div2.InnerHtml = sb.ToString();
                }

            }
            else if (ds != null && NoOfRecords == 0)
            {
                dvRTIRplyComment.Visible = false;
                lblRTIReply.Text = "No Comments.";
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillRelpy2()
    {
        try
        {
            ds = null;
            ds1 = null;
            StringBuilder sb = new StringBuilder();
            ds1 = objdb.ByProcedure("SpRtiAddChat", new string[] { "flag", "RTI_ID" }, new string[] { "11", ViewState["RTI_ID"].ToString() }, "dataset");
            int NoOfType = ds1.Tables[0].Rows.Count;
            if (ds1 != null && NoOfType > 0)
            {
                for (int j = 0; j < NoOfType; j++)
                {
                    divInternalDiscussion.Visible = true;
                    string RequestType = ds1.Tables[0].Rows[j]["RTI_RequestType"].ToString();
                    sb.Append("<fieldset><legend>Comments For " + RequestType + "</legend><br />");
                    ds = objdb.ByProcedure("SpRtiAddChat", new string[] { "flag", "RTI_ID", "RTI_RequestType" }, new string[] { "8", ViewState["RTI_ID"].ToString(), RequestType }, "dataset");
                    int NoOfRecords = ds.Tables[0].Rows.Count;
                    if (ds != null && NoOfRecords > 0)
                    {
                        lblCommentRecord.Text = "";
                        for (int i = 0; i < NoOfRecords; i++)
                        {
                            sb.Append(" <div class='direct-chat-msg' style='min-height:100px;'>");
                            sb.Append("<div class='direct-chat-info clearfix'>");
                            sb.Append("<span class='direct-chat-name pull-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</span><br/>");
                            sb.Append("<span class='direct-chat-name pull-left'>[" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + " / " + ds.Tables[0].Rows[i]["Department_Name"].ToString() + "]</span>");
                            sb.Append("<span class='direct-chat-timestamp pull-right'>" + ds.Tables[0].Rows[i]["Chat_UpdatedOn"].ToString() + " , " + ds.Tables[0].Rows[i]["Chat_UpdatedOnTime"].ToString() + "</span>");     //16 Aug 2:00 pm
                            sb.Append("</div>");
                            //.direct-chat-info -->
                            sb.Append("<img class='direct-chat-img' src='../image/User1.png' alt='message user image'/>");
                            //.direct-chat-img -->
                            sb.Append("<div class='direct-chat-text form-group' style='word-wrap:break-word; min-height:80px;'>" + ds.Tables[0].Rows[i]["Chat_Remark"].ToString());
                            //sb.Append("RTI Act has been made by legislation of Parliament of India on 15 June 2005.");

                            sb.Append("<div class='attachment text-right''><br />");
                            string Chat_Doc1 = ds.Tables[0].Rows[i]["Chat_Doc1"].ToString();
                            if (Chat_Doc1 != null && Chat_Doc1 != "")
                            {
                                sb.Append(" <a href='RTI_Docs/" + Chat_Doc1 + "' target='blank'>Attachment 1</a>");
                            }
                            string Chat_Doc2 = ds.Tables[0].Rows[i]["Chat_Doc2"].ToString();
                            if (Chat_Doc2 != null && Chat_Doc2 != "")
                            {
                                sb.Append(" <a href='RTI_Docs/" + Chat_Doc2 + "' target='blank' style='word-wrap:break-word'>/ Attachment 2</a>");
                            }
                            sb.Append("</div></div></div>");
                        }
                    }
                    else
                    {
                        // lblDepartmentRecord.Text = "Please Wait! Reply is on waiting.";
                        // lblDepartmentRecord.Style.Add("color", "Red");
                    }
                    sb.Append("</fieldset>");
                    divchat.InnerHtml = sb.ToString();
                }
            }
            else if (ds1 != null && NoOfType == 0)
            {
                divInternalDiscussion.Visible = false;
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