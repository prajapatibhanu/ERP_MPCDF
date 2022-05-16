using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RTI_RegistrationForm : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["App_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["App_ID"] = Session["App_ID"].ToString();
                    // HideonPageLoad();
                    FillState();
                    // FillDistrict();
                    ViewState["App_MobileNo"] = "";
                    ViewState["RTI_RequestDocPath"] = "";
                    ViewState["RTI_RequestDocPath"] = "";
                    ViewState["RTI_RegistrationNo"] = "";
                    FillMobileNo();
                    hyprRTI_RequestDoc.Visible = false;
                    if (Request.QueryString["RTI_ID"] != null && Request.QueryString["RTI_ID"] != "")
                    {
                        ViewState["RTI_ID"] = Request.QueryString["RTI_ID"].ToString();

                    }
                    else { }
                    FillDetails();
                }
            }
            else
            {
                Response.Redirect("~/ApplyRti.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillState()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpAdminState", new string[] { "flag" }, new string[] { "7" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlApp_State.DataSource = ds;
                ddlApp_State.DataTextField = "State_Name";
                ddlApp_State.DataValueField = "State_ID";
                ddlApp_State.DataBind();
                ddlApp_State.Items.Insert(0, new ListItem("Select", "0"));
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDistrict()
    {
        try
        {
            ddlApp_District.Items.Clear();

            if (ddlApp_State.SelectedIndex > 0)
            {
                ds = null;
                ds = objdb.ByProcedure("SpAdminDistrict", new string[] { "flag", "State_ID" }, new string[] { "12", ddlApp_State.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlApp_District.DataSource = ds;
                    ddlApp_District.DataTextField = "District_Name";
                    ddlApp_District.DataValueField = "District_ID";
                    ddlApp_District.DataBind();
                    ddlApp_District.Items.Insert(0, new ListItem("Select", "0"));
                }
                else { }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillBlock()
    {
        try
        {
            ddlApp_Block.Items.Clear();

            if (ddlApp_State.SelectedIndex > 0 && ddlApp_District.SelectedIndex > 0)
            {
                ds = null;
                ds = objdb.ByProcedure("SpAdminBlock", new string[] { "flag", "District_ID" }, new string[] { "6", ddlApp_District.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlApp_Block.DataSource = ds;
                    ddlApp_Block.DataTextField = "Block_Name";
                    ddlApp_Block.DataValueField = "Block_ID";
                    ddlApp_Block.DataBind();
                    ddlApp_Block.Items.Insert(0, new ListItem("Select", "0"));
                }
                else { }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillMobileNo()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpApplicantDetail",
                           new string[] { "flag", "App_ID" },
                                new string[] { "7", ViewState["App_ID"].ToString() }, "dataset");

            if (ds != null)
            {
                txtApp_MobileNo.Text = ds.Tables[0].Rows[0]["App_MobileNo"].ToString();
                ViewState["App_MobileNo"] = txtApp_MobileNo.Text;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDetails()
    {
        try
        {
            DataSet dsRecord = objdb.ByProcedure("SpApplicantDetail",
                         new string[] { "flag", "App_MobileNo", "App_ID" },
                              new string[] { "9", ViewState["App_MobileNo"].ToString(), ViewState["App_ID"].ToString() }, "dataset");

            if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
            {
                string name = dsRecord.Tables[0].Rows[0]["App_Name"].ToString();

                if (name != "")
                {
                    txtApp_Name.Text = dsRecord.Tables[0].Rows[0]["App_Name"].ToString();
                    txtApp_Email.Text = dsRecord.Tables[0].Rows[0]["App_Email"].ToString();
                    ddlApp_UserType.ClearSelection();
                    ddlApp_UserType.Items.FindByValue(dsRecord.Tables[0].Rows[0]["App_UserType"].ToString()).Selected = true;
                    rbtnlApp_Gender.ClearSelection();
                    rbtnlApp_Gender.Items.FindByText(dsRecord.Tables[0].Rows[0]["App_Gender"].ToString()).Selected = true;
                    txtApp_Address.Text = dsRecord.Tables[0].Rows[0]["App_Address"].ToString();
                    txtApp_Pincode.Text = dsRecord.Tables[0].Rows[0]["App_Pincode"].ToString();
                    ddlApp_State.ClearSelection();
                    ddlApp_State.Items.FindByValue(dsRecord.Tables[0].Rows[0]["App_State"].ToString()).Selected = true;
                    FillDistrict();
                    ddlApp_District.ClearSelection();
                    ddlApp_District.Items.FindByValue(dsRecord.Tables[0].Rows[0]["App_District"].ToString()).Selected = true;
                    FillBlock();
                    ddlApp_Block.ClearSelection();
                    ddlApp_Block.Items.FindByValue(dsRecord.Tables[0].Rows[0]["App_Block"].ToString()).Selected = true;
                    // ddlApp_Block.Text = dsRecord.Tables[0].Rows[0]["App_Block"].ToString();
                    rbtnlApp_PLStatus.ClearSelection();
                    rbtnlApp_PLStatus.Items.FindByText(dsRecord.Tables[0].Rows[0]["App_PLStatus"].ToString()).Selected = true;
                   // BPLBlock();
                    txtApp_BPLCardNo.Text = dsRecord.Tables[0].Rows[0]["App_BPLCardNo"].ToString();
                    txtApp_YearOfIssue.Text = dsRecord.Tables[0].Rows[0]["App_YearOfIssue"].ToString();
                    txtApp_IssuingAuthority.Text = dsRecord.Tables[0].Rows[0]["App_IssuingAuthority"].ToString();


                    //txtAddress.Enabled = false;
                    //txtBPLCardNo.Enabled = false;
                    //txtEmail.Enabled = false;
                    //txtIssuingAuthority.Enabled = false;
                    //txtName.Enabled = false;
                    //txtPincode.Enabled = false;
                    //txtBlock.Enabled = false;
                    //txtYearOfIssue.Enabled = false;
                    //ddlApp_District.Enabled = false;
                    //rbtnlApp_PLStatus.Enabled = false;
                    //ddlApp_State.Enabled = false;
                    //ddlUserType.Enabled = false;
                    //rbtnlGender.Enabled = false;

                    // btnSubmit.Text = "Modify";

                    if (ViewState["RTI_ID"] != "" && ViewState["RTI_ID"] != null)
                    {
                        DataSet dsRTIRecord = objdb.ByProcedure("SpRtiReqDetail",
                           new string[] { "flag", "RTI_ID" },
                                new string[] { "3", ViewState["RTI_ID"].ToString() }, "dataset");

                        if (dsRTIRecord != null && dsRTIRecord.Tables[0].Rows.Count > 0)
                        {
                            txtRTI_Subject.Text = dsRTIRecord.Tables[0].Rows[0]["RTI_Subject"].ToString();
                            txtRTI_Request.Text = dsRTIRecord.Tables[0].Rows[0]["RTI_Request"].ToString();
                            ViewState["RTI_RequestDocPath"] = dsRTIRecord.Tables[0].Rows[0]["RTI_RequestDoc"].ToString();
                            if (dsRTIRecord.Tables[0].Rows[0]["RTI_RequestDoc"].ToString() != "")
                            {
                                hyprRTI_RequestDoc.NavigateUrl = "~/mis/RTI/RTI_Docs/" + ViewState["RTI_RequestDocPath"].ToString();
                                hyprRTI_RequestDoc.Visible = true;
                            }
                            else
                            {
                                hyprRTI_RequestDoc.Visible = false;
                            }
                        }
                        else { }
                    }
                    else { }
                }
                else
                {
                    btnSubmit.Text = "Submit";
                }
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtApp_Name.Text.Trim() == "")
            {
                msg += "Enter Name<br/>";
            }
            if (txtApp_Email.Text.Trim() == "")
            {
                msg += "Enter Email<br/>";
            }
            if (ddlApp_UserType.SelectedIndex <= 0)
            {
                msg += "Select User Type<br/>";
            }
            if (txtApp_Address.Text.Trim() == "")
            {
                msg += "Enter Address<br/>";
            }
            if (txtApp_Pincode.Text.Trim() == "")
            {
                msg += "Enter Pincode<br/>";
            }
            if (ddlApp_Block.SelectedIndex <= 0)
            {
                msg += "Select Block<br/>";
            }
            if (ddlApp_District.SelectedIndex <= 0)
            {
                msg += "Select District<br/>";
            }
            if (ddlApp_State.SelectedIndex <= 0)
            {
                msg += "Select State<br/>";
            }
            if (rbtnlApp_PLStatus.SelectedIndex < 0)
            {
                msg += "Select Povert Status<br/>";
            }
            if (rbtnlApp_PLStatus.SelectedIndex != 1)
            {
                if (txtApp_BPLCardNo.Text.Trim() == "")
                {
                    msg += "Enter BPL Card No<br/>";
                }
                if (txtApp_YearOfIssue.Text.Trim() == "")
                {
                    msg += "Enter Year Of Issue<br/>";
                }
                if (txtApp_IssuingAuthority.Text.Trim() == "")
                {
                    msg += "Enter Issuing Authority<br/>";
                }
            }
            if (txtRTI_Request.Text.Trim() == "")
            {
                msg += "Enter RTI Request<br/>";
            }
            if (txtRTI_Subject.Text.Trim() == "")
            {
                msg += "Enter Subject for RTI Request<br/>";
            }
            //if (txtSecurityCode.Text.Trim() == "")
            //{
            //    lblMsg.Text += "Enter Security Code <br/>";
            //}
            if (msg == "")
            {
                if (btnSubmit.Text.Trim() == "Submit")
                {

                    // Insert Applicant Detail
                    string App_IsActive = "1";
                    objdb.ByProcedure("SpApplicantDetail",
                        new string[] { "flag", "App_ID", "App_IsActive", "App_Name", "App_MobileNo", "App_Email", "App_UserType", "App_Gender", "App_Address", "App_Pincode", "App_Block", "App_District", "App_State", "App_PLStatus", "App_BPLCardNo", "App_YearOfIssue", "App_IssuingAuthority", "App_UpdatedBy" },
                                  new string[] { "4", ViewState["App_ID"].ToString(), App_IsActive, txtApp_Name.Text.Trim(), txtApp_MobileNo.Text.Trim(), txtApp_Email.Text.Trim(), ddlApp_UserType.SelectedItem.ToString(), rbtnlApp_Gender.SelectedValue, txtApp_Address.Text.Trim(), txtApp_Pincode.Text.Trim(), ddlApp_Block.SelectedValue, ddlApp_District.SelectedValue, ddlApp_State.SelectedValue, rbtnlApp_PLStatus.SelectedItem.ToString(), txtApp_BPLCardNo.Text.Trim(), txtApp_YearOfIssue.Text.Trim().Trim(), txtApp_IssuingAuthority.Text.Trim(), ViewState["App_ID"].ToString() }, "dataset");


                    //Insert RTI Application on the basis of App_ID

                    string SupportingDoc = "";

                    if (fuRTI_RequestDoc.HasFile)
                    {
                        SupportingDoc = Guid.NewGuid() + "-" + fuRTI_RequestDoc.FileName;
                    }
                    else if (ViewState["RTI_RequestDocPath"] != "")
                    {
                        SupportingDoc = ViewState["RTI_RequestDocPath"].ToString();
                    }
                    else
                    {
                        SupportingDoc = "";
                    }
                    ds = null;
                    string RTI_IsActive = "1";
                    if (ViewState["RTI_ID"] == null || ViewState["RTI_ID"] == "")
                    {
                        ds = objdb.ByProcedure("SpRtiReqDetail",
                                 new string[] { "flag", "App_ID", "RTI_IsActive", "RTI_Subject", "RTI_Request", "RTI_RequestDoc" },
                                      new string[] { "0", ViewState["App_ID"].ToString(), RTI_IsActive, txtRTI_Subject.Text.Trim(), txtRTI_Request.Text.Trim(), SupportingDoc }, "dataset");

                        ViewState["RTI_RegistrationNo"] = ds.Tables[0].Rows[0]["RTI_RegistrationNo"].ToString();
                        ViewState["RTI_ID"] = ds.Tables[0].Rows[0]["RTI_ID"].ToString();
                    }
                    else
                    {
                        ds = objdb.ByProcedure("SpRtiReqDetail",
                                new string[] { "flag", "RTI_ID", "App_ID", "RTI_IsActive", "RTI_Subject", "RTI_Request", "RTI_RequestDoc" },
                                     new string[] { "13", ViewState["RTI_ID"].ToString(), ViewState["App_ID"].ToString(), RTI_IsActive, txtRTI_Subject.Text.Trim(), txtRTI_Request.Text.Trim(), SupportingDoc }, "dataset");

                        ViewState["RTI_RegistrationNo"] = ds.Tables[0].Rows[0]["RTI_RegistrationNo"].ToString();
                        ViewState["RTI_ID"] = ds.Tables[0].Rows[0]["RTI_ID"].ToString();
                    }


                    if (fuRTI_RequestDoc.HasFile)
                    {
                        fuRTI_RequestDoc.PostedFile.SaveAs(Server.MapPath("~/mis/RTI/RTI_Docs/" + SupportingDoc));
                    }
                    else
                    {
                        //SupportingDoc = "";
                    }
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Alert!", "Data Saved Successfully");
                    //if (ViewState["RTI_RegistrationNo"] != null && ViewState["RTI_RegistrationNo"] != "")
                    //{
                    //    string Frwd_IsActive = "1";
                    //    objdb.ByProcedure("SpRtiFrwdDetail",
                    //        new string[] { "flag", "RTI_ID", "Frwd_IsActive", "Frwd_EmpID", "Frwd_OfficerID" },
                    //        new string[] { "8", ViewState["RTI_ID"].ToString(), Frwd_IsActive, ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    //    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    //}
                    string BPLStatus = rbtnlApp_PLStatus.SelectedValue;
                    if (BPLStatus == "Yes (हाँ)")
                    {
                        Response.Redirect("RegistrationNo.aspx");
                    }
                    if (BPLStatus == "No (नहीं)")
                    {
                        Response.Redirect("RTIPaymentForm.aspx");
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlApp_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDistrict();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BPLBlock()
    {
        try
        {
            string BPLStatus = rbtnlApp_PLStatus.SelectedItem.ToString();
            if (BPLStatus == "Yes (हाँ)")
            {
                BPLCardNo.Visible = true;
                YearOfIssue.Visible = true;
                IssuingAuthority.Visible = true;
            }
            if (BPLStatus == "No (नहीं)")
            {
                HideonPageLoad();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void HideonPageLoad()
    {
        try
        {
            BPLCardNo.Visible = false;
            YearOfIssue.Visible = false;
            IssuingAuthority.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlApp_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillBlock();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtApp_Name.Text.Trim() == "")
            {
                msg += "Enter Name<br/>";
            }
            if (txtApp_Email.Text.Trim() == "")
            {
                msg += "Enter Email<br/>";
            }
            if (ddlApp_UserType.SelectedIndex <= 0)
            {
                msg += "Select User Type<br/>";
            }
            if (txtApp_Address.Text.Trim() == "")
            {
                msg += "Enter Address<br/>";
            }
            if (txtApp_Pincode.Text.Trim() == "")
            {
                msg += "Enter Pincode<br/>";
            }
            if (ddlApp_Block.SelectedIndex <= 0)
            {
                msg += "Select Block<br/>";
            }
            if (ddlApp_District.SelectedIndex <= 0)
            {
                msg += "Select District<br/>";
            }
            if (ddlApp_State.SelectedIndex <= 0)
            {
                msg += "Select State<br/>";
            }
            if (rbtnlApp_PLStatus.SelectedIndex < 0)
            {
                msg += "Select Povert Status<br/>";
            }
            if (rbtnlApp_PLStatus.SelectedIndex != 1)
            {
                if (txtApp_BPLCardNo.Text.Trim() == "")
                {
                    msg += "Enter BPL Card No<br/>";
                }
                if (txtApp_YearOfIssue.Text.Trim() == "")
                {
                    msg += "Enter Year Of Issue<br/>";
                }
                if (txtApp_YearOfIssue.Text.Trim() != "")
                {
                    if (txtApp_YearOfIssue.Text.Length != 4)
                    {
                        msg += "Enter Valid Year Of Issue<br/>";
                    }
                }
                if (txtApp_IssuingAuthority.Text.Trim() == "")
                {
                    msg += "Enter Issuing Authority<br/>";
                }
            }
            if (txtRTI_Request.Text.Trim() == "")
            {
                msg += "Enter RTI Request<br/>";
            }
            if (txtRTI_Subject.Text.Trim() == "")
            {
                msg += "Enter Subject for RTI Request<br/>";
            }
            //if (txtSecurityCode.Text.Trim() == "")
            //{
            //    lblMsg.Text += "Enter Security Code <br/>";
            //}
            if (msg == "")
            {
                if (btnSaveAsDraft.Text == "Save As Draft")
                {

                    // Insert Applicant Detail
                    string App_IsActive = "0";
                    objdb.ByProcedure("SpApplicantDetail",
                        new string[] { "flag", "App_ID", "App_IsActive", "App_Name", "App_MobileNo", "App_Email", "App_UserType", "App_Gender", "App_Address", "App_Pincode", "App_Block", "App_District", "App_State", "App_PLStatus", "App_BPLCardNo", "App_YearOfIssue", "App_IssuingAuthority" },
                                  new string[] { "4", ViewState["App_ID"].ToString(), App_IsActive, txtApp_Name.Text.Trim(), txtApp_MobileNo.Text.Trim(), txtApp_Email.Text.Trim(), ddlApp_UserType.SelectedItem.ToString(), rbtnlApp_Gender.SelectedValue, txtApp_Address.Text.Trim(), txtApp_Pincode.Text.Trim(), ddlApp_Block.SelectedValue, ddlApp_District.SelectedValue, ddlApp_State.SelectedValue, rbtnlApp_PLStatus.SelectedItem.ToString(), txtApp_BPLCardNo.Text.Trim(), txtApp_YearOfIssue.Text.Trim().Trim(), txtApp_IssuingAuthority.Text.Trim() }, "dataset");


                    //Insert RTI Application on the basis of App_ID

                    string SupportingDoc = "";

                    if (fuRTI_RequestDoc.HasFile)
                    {
                        SupportingDoc = Guid.NewGuid() + "-" + fuRTI_RequestDoc.FileName;
                    }
                    else if (ViewState["RTI_RequestDocPath"] != "")
                    {
                        SupportingDoc = ViewState["RTI_RequestDocPath"].ToString();
                    }
                    else
                    {
                        SupportingDoc = "";
                    }
                    ds = null;
                    string RTI_IsActive = "0";
                    if (ViewState["RTI_ID"] == null)
                    {
                        objdb.ByProcedure("SpRtiReqDetail",
                               new string[] { "flag", "App_ID", "RTI_IsActive", "RTI_Subject", "RTI_Request", "RTI_RequestDoc" },
                                    new string[] { "11", ViewState["App_ID"].ToString(), RTI_IsActive, txtRTI_Subject.Text.Trim(), txtRTI_Request.Text.Trim(), SupportingDoc }, "dataset");

                    }
                    else
                    {
                        objdb.ByProcedure("SpRtiReqDetail",
                                new string[] { "flag", "RTI_ID", "App_ID", "RTI_IsActive", "RTI_Subject", "RTI_Request", "RTI_RequestDoc" },
                                     new string[] { "12", ViewState["RTI_ID"].ToString(), ViewState["App_ID"].ToString(), RTI_IsActive, txtRTI_Subject.Text.Trim(), txtRTI_Request.Text.Trim(), SupportingDoc }, "dataset");
                    }
                    if (fuRTI_RequestDoc.HasFile)
                    {
                        fuRTI_RequestDoc.PostedFile.SaveAs(Server.MapPath("~/mis/RTI/RTI_Docs/" + SupportingDoc));
                    }
                    else
                    {
                        //SupportingDoc = "";
                    }
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Alert!", "Data Saved Successfully");
                }
                ViewState["RTI_RequestDocPath"] = "";
               // Response.Redirect("RegistrationForm.aspx");
                txtRTI_Subject.Text = "";
                txtRTI_Request.Text = "";
                hyprRTI_RequestDoc.Visible = false;
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
}