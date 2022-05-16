using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_RTI_FrwdRTIRply : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Emp_Name"] != null && Request.QueryString["RTI_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"];
                    ViewState["Emp_Name"] = Session["Emp_Name"];
                    ViewState["RTI_ID"] = objdb.Decrypt(Request.QueryString["RTI_ID"]);
                    ViewState["Rply_RTIRequestType"] = "RTI Request";
                    FillDetail();
                    FillRelpy();
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
            DataSet dscheck = objdb.ByProcedure("SpRtiAddChat", new string[] { "flag", "RTI_ID" }, new string[] { "10", ViewState["RTI_ID"].ToString() }, "dataset");
            if (dscheck != null && dscheck.Tables[0].Rows.Count > 0)
            {
                string RTI_Status = dscheck.Tables[0].Rows[0]["RTI_Status"].ToString();
                string RTI_RequestType = dscheck.Tables[0].Rows[0]["RTI_RequestType"].ToString();
                string flag = "";
                if (RTI_Status == "Open")
                {
                    divIntlRply.Visible = true;
                }
                else if (RTI_Status == "Close")
                {
                    divIntlRply.Visible = false;
                }
                else
                { }
                if (RTI_RequestType == "RTI Request")
                {
                    flag = "11";
                    divFirstAppeal.Visible = false;
                }
                else if (RTI_RequestType == "First Appeal Request")
                {
                    flag = "7";
                    divFirstAppeal.Visible = true;
                }
                else
                { }
                if (flag != null && flag != "")
                {
                    ds = objdb.ByProcedure("SpRtiReplyDetail", new string[] { "flag", "RTI_ID" }, new string[] { flag, ViewState["RTI_ID"].ToString() }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        // lblMsg.Text = "";

                        DetailsView3.DataSource = ds;
                        DetailsView3.DataBind();
                        RTIFADetails.InnerHtml = ds.Tables[0].Rows[0]["RTI_FAComment"].ToString();
                        string RTI_FARequestDoc = ds.Tables[0].Rows[0]["RTI_FARequestDoc"].ToString();
                        if (RTI_FARequestDoc != "")
                        {
                            hyprRTI_FARequestDoc.Visible = true;
                            hyprRTI_FARequestDoc.NavigateUrl = "~/mis/RTI/RTI_Docs/" + RTI_FARequestDoc;
                        }
                        else
                        {
                            hyprRTI_FARequestDoc.Visible = false;
                            hyprRTI_FARequestDoc.NavigateUrl = "";
                        }
                        // For RTI DetailView
                        DetailsView1.DataSource = ds;
                        DetailsView1.DataBind();
                        RTIDetails.InnerHtml = ds.Tables[0].Rows[0]["RTI_Request"].ToString();
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

                    }
                    else { }
                }
                else { }
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
            ds1 = null;
            StringBuilder sb = new StringBuilder();
            //111111111111111
            ds1 = objdb.ByProcedure("SpRtiAddChat", new string[] { "flag", "RTI_ID" }, new string[] { "11", ViewState["RTI_ID"].ToString() }, "dataset");
            int NoOfType = ds1.Tables[0].Rows.Count;
            if (ds1 != null && NoOfType > 0)
            {
                for (int j = 0; j < NoOfType; j++)
                {
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
                    divChat.InnerHtml = sb.ToString();
                }
            }
            else if (ds1 != null && NoOfType == 0)
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
                string Chat_Doc1 = "";
                string Chat_Doc2 = "";

                if (fuChat_Doc1.HasFile)
                {
                    Chat_Doc1 = Guid.NewGuid() + "-" + fuChat_Doc1.FileName;
                }
                else
                {
                    Chat_Doc1 = "";
                }
                if (fuChat_Doc2.HasFile)
                {
                    Chat_Doc2 = Guid.NewGuid() + "-" + fuChat_Doc2.FileName;
                }
                else
                {
                    Chat_Doc2 = "";
                }

                string Chat_IsActive = "1";
                objdb.ByProcedure("SpRtiAddChat",
                                new string[] { "flag", "RTI_ID", "Chat_IsActive", "Chat_EmpID", "Chat_Remark", "Chat_Doc1", "Chat_Doc2" },
                                     new string[] { "7", ViewState["RTI_ID"].ToString(), Chat_IsActive, ViewState["Emp_ID"].ToString(), txtChat_Remark.Text.Trim(), Chat_Doc1, Chat_Doc2 }, "dataset");


                if (fuChat_Doc1.HasFile)
                {
                    fuChat_Doc1.PostedFile.SaveAs(Server.MapPath("~/mis/RTI/RTI_Docs/" + Chat_Doc1));
                }
                else { }
                if (fuChat_Doc2.HasFile)
                {
                    fuChat_Doc2.PostedFile.SaveAs(Server.MapPath("~/mis/RTI/RTI_Docs/" + Chat_Doc2));
                }
                else { }
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                txtChat_Remark.Text = "";
                // FillDetail();
                FillRelpy();
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
}