using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RTI_RTIApplicantsForms_RTIDetails : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["App_ID"] != null && Request.QueryString["RTI_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ReasonForFirstAppeal.Visible = false;
                    chkForFirstAppeal.Checked = false;
                    ViewState["App_ID"] = Session["App_ID"];
                    ViewState["RTI_ID"] = Request.QueryString["RTI_ID"];
                    ViewState["RTI_RequestType"] = "";
                    ReasonForFirstAppeal.Visible = false;
                    DivManage();
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
            ds = objdb.ByProcedure("SpRtiReqDetail", new string[] { "flag", "RTI_ID" }, new string[] { "9", ViewState["RTI_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";

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

                DetailsView3.DataSource = ds;
                DetailsView3.DataBind();
                
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkForFirstAppeal_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkForFirstAppeal.Checked == true)
            {
                ReasonForFirstAppeal.Visible = true;
            }
            else
            {
                ReasonForFirstAppeal.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        
    }
    protected void btnSendRequest_Click(object sender, EventArgs e)
    {
        //from Database
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlRTI_FAGroundFor.SelectedIndex <= 0)
            {
                msg += "Select Ground For Appeal <br/>";
            }
            if(msg == "")
            {
                string RTI_FARequestDoc = "";

                if (fuRTI_FARequestDoc.HasFile)
                {
                    RTI_FARequestDoc = Guid.NewGuid() + "-" + fuRTI_FARequestDoc.FileName;
                }
                else
                {
                    RTI_FARequestDoc = "";
                }
                objdb.ByProcedure("SpRtiReqDetail",
                       new string[] { "flag", "RTI_ID", "RTI_FAGroundFor", "RTI_FAComment", "RTI_FARequestDoc" },
                            new string[] { "10", ViewState["RTI_ID"].ToString(), ddlRTI_FAGroundFor.SelectedValue, txtRTI_FAComment.Text.Trim(), RTI_FARequestDoc }, "dataset");

                ds = objdb.ByProcedure("SpApplicantDetail",
                       new string[] { "flag", "App_ID" },
                            new string[] { "10", ViewState["App_ID"].ToString() }, "dataset");

                string App_PLStatus = ds.Tables[0].Rows[0]["App_PLStatus"].ToString();
                if (App_PLStatus == "Yes (हाँ)")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Alert!", "First Appeal Request has been sent to Mp Agro Department.");
                    FillDetail();
                    divFirstAppeal.Visible = true;
                    divRequestFirstAppeal.Visible = false;
                }
                if (App_PLStatus == "No (नहीं)")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Alert!", "First Appeal Request has been sent to Mp Agro Department.");
                    FillDetail();
                    divFirstAppeal.Visible = true;
                    divRequestFirstAppeal.Visible = false;
                    //Response.Redirect("RTIPaymentForm.aspx");
                }
                if (fuRTI_FARequestDoc.HasFile) 
                {
                    fuRTI_FARequestDoc.PostedFile.SaveAs(Server.MapPath("~/mis/RTI/RTI_Docs/" + RTI_FARequestDoc));
                }
                else
                {
                    //SupportingDoc = "";
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                divRequestFirstAppeal.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void DivManage()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpRtiReqDetail", new string[] { "flag", "RTI_ID" }, new string[] { "8", ViewState["RTI_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["RTI_RequestType"] = ds.Tables[0].Rows[0]["RTI_RequestType"].ToString();
                if (ViewState["RTI_RequestType"].ToString() != "")
                {
                    if (ViewState["RTI_RequestType"].ToString() == "RTI Request")
                    {
                        divFirstAppeal.Visible = false;
                       // FirstAppealChat.Visible = false;
                    }
                    else if (ViewState["RTI_RequestType"].ToString() == "First Appeal Request")
                    {
                        divRequestFirstAppeal.Visible = false;
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
            StringBuilder sb = new StringBuilder();
            ds = null;
            ds1 = null;
            ds1 = objdb.ByProcedure("SpRtiReplyDetail", new string[] { "flag", "RTI_ID" }, new string[] { "9", ViewState["RTI_ID"].ToString() }, "dataset");
            int NoOfType = ds1.Tables[0].Rows.Count;
            if (ds1 != null && NoOfType > 0)
            {
                for (int j = 0; j < NoOfType; j++)
                {
                    string RequestType = ds1.Tables[0].Rows[j]["Rply_RTIRequestType"].ToString();
                sb.Append("<fieldset><legend>" + RequestType + "</legend><br />");
                ds = objdb.ByProcedure("SpRtiReplyDetail", new string[] { "flag", "RTI_ID", "Rply_RTIRequestType" }, new string[] { "8", ViewState["RTI_ID"].ToString(), RequestType }, "dataset");
                    int NoOfRecords = ds.Tables[0].Rows.Count;
                    if (ds != null && NoOfRecords > 0)
                    {
                        lblMsg.Text = "";
                        lblDepartmentRecord.Text = "";
                       
                        for (int i = 0; i < NoOfRecords; i++)
                        {
                            sb.Append(" <div class='direct-chat-msg' style='min-height:100px;'>");
                            sb.Append("<div class='direct-chat-info clearfix'>");
                            sb.Append("<span class='direct-chat-name pull-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</span><br/>");
                            sb.Append("<span class='direct-chat-name pull-left'>[" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + " / " + ds.Tables[0].Rows[i]["Department_Name"].ToString() + "]</span>");
                            sb.Append("<span class='direct-chat-timestamp pull-right'>" + ds.Tables[0].Rows[i]["Rply_UpdatedOn"].ToString() + " , " + ds.Tables[0].Rows[i]["Rply_UpdatedTimeOn"].ToString() + "</span>");     //16 Aug 2:00 pm
                            sb.Append("</div>");
                            //.direct-chat-info -->
                            sb.Append("<img class='direct-chat-img' src='../image/User1.png' alt='message user image'/>");
                            //.direct-chat-img -->
                            sb.Append("<div class='direct-chat-text form-group' style='word-wrap:break-word'>" + ds.Tables[0].Rows[i]["Rply_RTIRemark"].ToString());
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
                                sb.Append(" <a href='RTI_Docs/" + Rply_RTIDoc2 + "' target='blank' style='word-wrap:break-word'>Attachment 2</a>");
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
                    dvChat.InnerHtml = sb.ToString();
                }
            }
            else
            {
                lblDepartmentRecord.Text = "MP Agro Department will Reply Soon.";
                lblDepartmentRecord.Style.Add("color", "Red");
            }

       
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}