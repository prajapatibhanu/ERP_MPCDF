using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_RTI_Rply : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1, ds2;
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Emp_Name"] != null && Request.QueryString["RTI_ID"] != null && Request.QueryString["ShowHide"] != null && Session["Office_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lnkApplyForFirstAppeal.Visible = false;
                    lblMsg.Text = "";
                    lblCommentRecord.Text = "";
                    lblGridRecord.Text = "";
                    lblModal.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"];
                    ViewState["Emp_Name"] = Session["Emp_Name"];
                    ViewState["App_ID"] = "";
                    ViewState["RTI_ID"] = objdb.Decrypt(Request.QueryString["RTI_ID"]);
                    ViewState["ShowHide"] = objdb.Decrypt(Request.QueryString["ShowHide"]);
                    ViewState["Rply_RTIRequestType"] = "RTI Request";
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    // ShowHideFAButton();
                    //ReasonForFirstAppeal.Visible = false;
                    FillDetail();
                    FillRelpy();
                    FillDepartment();
                    FillRelpy2();
                    ddlFrwd_OfficerName.Items.Insert(0, new ListItem("- No Record Found -", "0"));
                    rtidate.Visible = false;
                    txtRTICloseDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                RTIDetails.Text = ds.Tables[0].Rows[0]["RTI_Request"].ToString() ;
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
                    hdnApp_Status.Value="Yes";
                    divFill_FirstPaymentDetail.Visible = false;
                }
                if (BPLStatus == "No (नहीं)")
                {
                    DetailsView3.DataSource = ds;
                    DetailsView3.DataBind();
                    divPaymentDetail.Visible = true;
                    hdnApp_Status.Value = "No";
                    divFill_FirstPaymentDetail.Visible = true;
                }

                
                string closesatus = ds.Tables[0].Rows[0]["ShowRTIStatus"].ToString();
                
                if (ViewState["ShowHide"].ToString() == "Hide")
                {
                    divRply.Visible = false;
                    divIntlRply.Visible = false;
                    lnkAddOfficer.Visible = false;
                    btnInternalDiscussion.Visible = false;
                    if (dvChat.InnerHtml == "")
                    {
                        lblShowNoComOnHide.Visible = true;
                    }
                    else
                    {
                        lblShowNoComOnHide.Visible = false;
                    }
                    
                }
                else if (ViewState["ShowHide"].ToString() == "Show")
                {
                    lblShowNoComOnHide.Visible = false;
                    if (closesatus == "Close")
                    {
                        divRply.Visible = false;
                        lnkAddOfficer.Visible = false;
                        divIntlRply.Visible = false;
                        btnInternalDiscussion.Visible = false;
                        divIntlRply.Visible = false;
                    }
                    else
                    {
                        divRply.Visible = true;
                        lnkAddOfficer.Visible = true;
                        divIntlRply.Visible = true;
                        btnInternalDiscussion.Visible = true;
                        divIntlRply.Visible = true;
                    }
                }
               
            }
            
            if(ds != null && ds.Tables[1].Rows.Count > 0)
            {
                string Office_ID = ds.Tables[0].Rows[0]["RTI_ByOfficeID"].ToString();
                if (Office_ID.ToString() == ViewState["Office_ID"].ToString())
                {
                    if(ds.Tables[1].Rows[0]["Rply_UpdatedOn"].ToString() != "")
                {
                    var dateAndTime = DateTime.Now;
                    var date = dateAndTime.ToString("dd/MM/yyyy");
                    string d11 = Convert.ToDateTime(date, cult).ToString("yyyy/MM/dd");
                    string d22 = Convert.ToDateTime(ds.Tables[1].Rows[0]["Rply_UpdatedOn"].ToString(), cult).ToString("yyyy/MM/dd");
                    DateTime d1 = DateTime.Parse(d11);
                    DateTime d2 = DateTime.Parse(d22);
                    d2 = d2.AddDays(30);
                    if (d1 >= d2)
                    {
                        lnkApplyForFirstAppeal.Visible = false;
                    }
                    else
                    {
                        lnkApplyForFirstAppeal.Visible = true;
                    }
                }
                }

                
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnReply_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblCommentRecord.Text = "";
            lblGridRecord.Text = "";
            lblModal.Text = "";
            string msg = "";
            if (txtRply_RTIRemark.Text.Trim() == "")
            {
                msg += "Enter Remark<br/>";
            }
            if (ddlRply_Status.SelectedIndex <= 0)
            {
                msg += "Select Status<br/>";
            }
            if (txtRTICloseDate.Text == "")
            {
                msg += "Enter RTI Close Date<br/>";
            }
            if (msg == "")
            {
                string Rply_RTIDoc1 = "";
                string Rply_RTIDoc2 = "";

                if (fuRply_RTIDoc1.HasFile)
                {
                    Rply_RTIDoc1 = Guid.NewGuid() + "-" + fuRply_RTIDoc1.FileName;
                }
                else
                {
                    Rply_RTIDoc1 = "";
                }
                if (fuRply_RTIDoc2.HasFile)
                {
                    Rply_RTIDoc2 = Guid.NewGuid() + "-" + fuRply_RTIDoc2.FileName;
                }
                else
                {
                    Rply_RTIDoc2 = "";
                }

                string Rply_IsActive = "1";

                objdb.ByProcedure("SpRtiReplyDetail",
                                new string[] { "flag", "RTI_ID", "Rply_IsActive", "Rply_RTIRequestType", "Rply_RTIRemark", "Rply_RTIDoc1", "Rply_RTIDoc2", "Rply_Status", "Rply_UpdatedBy", "Rply_UpdatedOn" },
                                     new string[] { "0", ViewState["RTI_ID"].ToString(), Rply_IsActive, ViewState["Rply_RTIRequestType"].ToString(), txtRply_RTIRemark.Text.Trim(), Rply_RTIDoc1, Rply_RTIDoc2, ddlRply_Status.SelectedValue, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtRTICloseDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");


                if (fuRply_RTIDoc1.HasFile)
                {
                    fuRply_RTIDoc1.PostedFile.SaveAs(Server.MapPath("~/mis/RTI/RTI_Docs/" + Rply_RTIDoc1));
                }
                else { }
                if (fuRply_RTIDoc2.HasFile)
                {
                    fuRply_RTIDoc2.PostedFile.SaveAs(Server.MapPath("~/mis/RTI/RTI_Docs/" + Rply_RTIDoc2));
                }
                else { }
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideLabel();", true);
                // ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                txtRply_RTIRemark.Text = "";
                ddlRply_Status.ClearSelection();
                FillDetail();
                FillRelpy();
                ShowHideFAButton();
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
                lblCommentRecord.Text = "No Comments.";
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    // For Internal Discussioin

    protected void lnkAddOfficer_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblCommentRecord.Text = "";
            lblGridRecord.Text = "";
            lblModal.Text = "";
            ddlAdd_Department.ClearSelection();
            ddlFrwd_OfficerName.Items.Clear();
            ddlFrwd_OfficerName.Items.Insert(0, new ListItem("- No Record Found -", "0"));
            FillGridIntrlD();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDepartment()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpAdminDepartment", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlAdd_Department.DataSource = ds;
                ddlAdd_Department.DataTextField = "Department_Name";
                ddlAdd_Department.DataValueField = "Department_ID";
                ddlAdd_Department.DataBind();
                ddlAdd_Department.Items.Insert(0, new ListItem("Select", "0"));
            }
            else { }
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
            ddlFrwd_OfficerName.Items.Clear();

            if (ddlAdd_Department.SelectedIndex > 0)
            {
                ds = null;
                //ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Department_ID", "Emp_ID" }, new string[] { "15", ddlAdd_Department.SelectedValue, ViewState["Emp_ID"].ToString() }, "dataset");
                ds = objdb.ByProcedure("SpRtiAddChat", new string[] { "flag", "Department_ID", "Emp_ID", "Office_ID" }, new string[] { "13", ddlAdd_Department.SelectedValue, ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlFrwd_OfficerName.DataSource = ds;
                    ddlFrwd_OfficerName.DataTextField = "Emp_Name";
                    ddlFrwd_OfficerName.DataValueField = "Emp_ID";
                    ddlFrwd_OfficerName.DataBind();
                    ddlFrwd_OfficerName.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlFrwd_OfficerName.Items.Insert(0, new ListItem("- No Record Found -", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridIntrlD()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpRtiAddChat", new string[] { "flag", "RTI_ID" }, new string[] { "4", ViewState["RTI_ID"].ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblGridRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblGridRecord.Text = "File is not forwarded yet.";
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlAdd_Department_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblCommentRecord.Text = "";
            lblGridRecord.Text = "";
            lblModal.Text = "";
            FillEmployee();
            FillGridIntrlD();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblCommentRecord.Text = "";
            lblGridRecord.Text = "";
            lblModal.Text = "";
            string msg = "";
            if (ddlAdd_Department.SelectedIndex <= 0)
            {
                msg += "Select Department<br/>";
            }
            if (ddlFrwd_OfficerName.SelectedIndex <= 0)
            {
                msg += "Select Status<br/>";
            }
            if (msg == "")
            {
                if (btnAdd.Text == "Add Officer")
                {
                    lblModal.Text = "";
                    string Add_IsActive = "1";
                    ds = null;
                    ds = objdb.ByProcedure("SpRtiAddChat", new string[] { "flag", "RTI_ID", "Emp_ID" }, new string[] { "5", ViewState["RTI_ID"].ToString(), ddlFrwd_OfficerName.SelectedValue }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count == 0)
                    {
                        lblModal.Text = "";
                        objdb.ByProcedure("SpRtiAddChat",
                        new string[] { "flag", "Emp_ID", "RTI_ID", "Add_IsActive", "Add_DepartmentID", "Add_UpdatedBy" },
                        new string[] { "0", ddlFrwd_OfficerName.SelectedValue, ViewState["RTI_ID"].ToString(), Add_IsActive, ddlAdd_Department.SelectedValue, ViewState["Emp_ID"].ToString() }, "dataset");
                        lblModal.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Successfully RTI Forward to Officer.");
                        //ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideLabel();", true);
                    }
                    else if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        lblModal.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "RTI is Already Forwarded to This Officer.");
                    }
                    else
                    { }
                    FillGridIntrlD();
                    ddlFrwd_OfficerName.ClearSelection();
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
                }
            }
            else
            {
                lblModal.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string Add_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();

        ds = objdb.ByProcedure("SpRtiAddChat", new string[] { "flag", "Add_ID" }, new string[] { "6", Add_ID.ToString() }, "dataset");
        lblModal.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Officer Name Removed Successfully.");
        ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideLabel();", true);
        FillGridIntrlD();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
    }
    protected void btnInternalDiscussion_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblCommentRecord.Text = "";
            lblGridRecord.Text = "";
            lblModal.Text = "";
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
                // Check this RTI is forwarded or not
                ds = null;
                ds = objdb.ByProcedure("SpRtiAddChat", new string[] { "flag", "RTI_ID" }, new string[] { "14", ViewState["RTI_ID"].ToString() }, "dataset");
                string AbleStatus = "";
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    AbleStatus = ds.Tables[0].Rows[0]["Status"].ToString();
                }

                if (AbleStatus == "Able")
                {
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
                    //lblCommentRecord.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Thank You! Send Remark Successfully');", true);

                    //ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideLabel();", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Alert! Please Firstly Add Officer For Internal Discussion.');", true);
                }


                txtChat_Remark.Text = "";
                // FillDetail();
                FillRelpy2();
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
    protected void FillRelpy2()
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
                    divchat.InnerHtml = sb.ToString();
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

    // Request For First Appeal

    //protected void chkForFirstAppeal_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (chkForFirstAppeal.Checked == true)
    //        {
    //            ReasonForFirstAppeal.Visible = true;
    //        }
    //        else
    //        {
    //            ReasonForFirstAppeal.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }

    //}

    protected void btnSendRequest_Click(object sender, EventArgs e)
    {
        //from Database
        try
        {
            lblMsg.Text = "";
            lblCommentRecord.Text = "";
            lblGridRecord.Text = "";
            lblModal.Text = "";
            string msg = "";
            if (ddlRTI_FAGroundFor.SelectedIndex <= 0)
            {
                msg += "Select Ground For Appeal <br/>";
            }
            //if (ddlRTI_FAGroundFor.SelectedIndex <= 0)
            //{
            //    msg += "Select Ground For Appeal <br/>";
            //}
            //if (ddlRTI_FAGroundFor.SelectedIndex <= 0)
            //{
            //    msg += "Select Ground For Appeal <br/>";
            //}
            //if (ddlRTI_FAGroundFor.SelectedIndex <= 0)
            //{
            //    msg += "Select Ground For Appeal <br/>";
            //}
            if (msg == "")
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

                string FAPaymentMode = "";
                string FAReceiptNo = "";
                string FAAmount = "0.00";
                DataSet ds2 = objdb.ByProcedure("SpRtiReqDetail",
                       new string[] { "flag", "RTI_ID" },
                            new string[] { "21", ViewState["RTI_ID"].ToString() }, "dataset");

                if (ds2.Tables[0].Rows[0]["App_ID"].ToString() != "")
                {
                    ViewState["App_ID"] = ds2.Tables[0].Rows[0]["App_ID"].ToString();

                    ds = objdb.ByProcedure("SpApplicantDetail",
                           new string[] { "flag", "App_ID" },
                                new string[] { "10", ViewState["App_ID"].ToString() }, "dataset");

                    string App_PLStatus = ds.Tables[0].Rows[0]["App_PLStatus"].ToString();
                    if (App_PLStatus == "Yes (हाँ)")
                    {
                        FAPaymentMode = ""; // ddlRTI_PaymentMode.SelectedValue;
                        FAReceiptNo = txtRTI_POReceiptNo.Text;
                        FAAmount = "0.00"; // txtRTI_Amount.Text;
                    }
                    else
                    {
                        FAPaymentMode = ddlRTI_PaymentMode.SelectedValue;
                        FAReceiptNo = txtRTI_POReceiptNo.Text;
                        FAAmount = txtRTI_Amount.Text;
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
                DataSet ds1 = objdb.ByProcedure("SpRtiReqDetail",
                       new string[] { "flag", "RTI_ID", "RTI_FAGroundFor", "RTI_FAComment", "RTI_FARequestDoc", "RTI_FAPaymentMode", "RTI_FAPOReceiptNo", "RTI_FAAmount" },
                            new string[] { "10", ViewState["RTI_ID"].ToString(), ddlRTI_FAGroundFor.SelectedValue, txtRTI_FAComment.Text.Trim(), RTI_FARequestDoc, FAPaymentMode, FAReceiptNo, FAAmount }, "dataset");
                FillDetail();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Alert! First Appeal Request has been sent to Mp Agro Department.');", true);

                divRply.Visible = false;
                lnkAddOfficer.Visible = false;
                divIntlRply.Visible = false;
                lnkApplyForFirstAppeal.Visible = false;
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                // divRequestFirstAppeal.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkApplyForFirstAppeal_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblCommentRecord.Text = "";
            lblGridRecord.Text = "";
            lblModal.Text = "";
            ddlRTI_PaymentMode.ClearSelection();
            txtRTI_POReceiptNo.Text = "";
            txtRTI_Amount.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ShowHideFAButton()
    {
        try
        {
            ds2 = null;
            //ds2 = objdb.ByProcedure("SpRtiReqDetail",
            //           new string[] { "flag", "RTI_ID"},
            //                new string[] { "16", ViewState["RTI_ID"].ToString() }, "dataset");
            //if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            //{
            //    string d11 = Convert.ToDateTime(ds2.Tables[0].Rows[0]["RTI_UpdatedOn"].ToString(), cult).ToString("yyyy/MM/dd");
            //    //int daydiff = DateTime.Parse(Convert.ToDateTime(d11).ToShortDateString()).Subtract(DateTime.Parse(Convert.ToDateTime(DateTime.Now).ToShortDateString())).Days;
            //    int daydiff = DateTime.Parse(Convert.ToDateTime(DateTime.Now).ToShortDateString()).Subtract(DateTime.Parse(Convert.ToDateTime(d11).ToShortDateString())).Days;

            //    string RTI_RequestType = ds2.Tables[0].Rows[0]["RTI_RequestType"].ToString();
            //    string status = ds2.Tables[0].Rows[0]["RTI_Status"].ToString();
            //    if (daydiff > 30 && RTI_RequestType == ViewState["Rply_RTIRequestType"].ToString() && status == "Open")
            //    {
            //        lnkApplyForFirstAppeal.Visible = true;
            //    }
            //    else if (RTI_RequestType == ViewState["Rply_RTIRequestType"].ToString() && status == "Close")
            //    {
            //        lnkApplyForFirstAppeal.Visible = true;
            //    }
            //    else
            //    {
            //        lnkApplyForFirstAppeal.Visible = false;
            //    }
            //}


            //-------------------Only Comment For testing--------------------------------------------//
            ds2 = objdb.ByProcedure("SpRtiReqDetail",
                      new string[] { "flag", "RTI_ID" },
                           new string[] { "22", ViewState["RTI_ID"].ToString() }, "dataset");
            {
                if (ds2.Tables[0].Rows.Count > 0 || ds2.Tables[1].Rows.Count > 0)
                {
                    if (ds2.Tables[0].Rows[0]["STATUS"].ToString() == "Able" || ds2.Tables[1].Rows[0]["STATUS"].ToString() == "Able" || ds2.Tables[2].Rows[0]["STATUS"].ToString() == "Able")
                    {
                        lnkApplyForFirstAppeal.Visible = true;
                    }
                    else
                    {
                        lnkApplyForFirstAppeal.Visible = false;
                    }
                }
                else
                {
                    lnkApplyForFirstAppeal.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["ShowHide"].ToString() == "Show")
            {
                Response.Redirect("/mis/RTI/RequestedRTI.aspx");
            }
            else if (ViewState["ShowHide"].ToString() == "Hide")
            {
                Response.Redirect("/mis/RTI/ListOfAllRTIs.aspx");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkViewIntrlDis_Click(object sender, EventArgs e)
    {
        lnkViewRply.Focus();
    }
    protected void lnkViewRply_Click(object sender, EventArgs e)
    {
        lnkViewIntrlDis.Focus();
    }
    protected void ddlRply_Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            rtidate.Visible = false;
            lblMsg.Text = "";
            if (ddlRply_Status.SelectedValue == "Close")
            {
                rtidate.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}