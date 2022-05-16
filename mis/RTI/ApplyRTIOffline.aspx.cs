using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_RTI_ApplyRTIOffline : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    FillState();
                    ViewState["App_MobileNo"] = "";
                    ViewState["App_ID"] = "";
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    //HideonPageLoad();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "abc('Yes')", true);
                    //ddlApp_State.Attributes.Add("disabled", "disabled");
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
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
                //ddlApp_State.Items.FindByValue("12").Selected = true;
                //FillDistrict();
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
    protected void ddlApp_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDistrict();
            if (rbn1.Checked == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "CallMyFunction", "abc()", true);//script used for calling js function
            }
            if (rbtn2.Checked == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "CallMyFunction", "abc1()", true);//script used for calling js function
            }
            
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "abc()", true);
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "abc()", true);
            //ClientScript.RegisterStartupScript(this.GetType(), "abc()", script, true);
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
            if (rbn1.Checked == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "CallMyFunction", "abc()", true);//script used for calling js function
            }
            if (rbtn2.Checked == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "CallMyFunction", "abc1()", true);//script used for calling js function
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDetails()
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsRecord = objdb.ByProcedure("SpApplicantDetail",
                         new string[] { "flag", "App_MobileNo", "App_ID" },
                              new string[] { "9", ViewState["App_MobileNo"].ToString(), ViewState["App_ID"].ToString() }, "dataset");

            if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
            {
                string name = dsRecord.Tables[0].Rows[0]["App_Name"].ToString();

                if (name != "")
                {
                    ViewState["App_ID"] = dsRecord.Tables[0].Rows[0]["App_ID"].ToString();
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
                    //rbtnlApp_PLStatus.ClearSelection();
                    //rbtnlApp_PLStatus.Items.FindByText(dsRecord.Tables[0].Rows[0]["App_PLStatus"].ToString()).Selected = true;
                    if (dsRecord.Tables[0].Rows[0]["App_PLStatus"].ToString() == "Yes (हाँ)")
                    {
                       // rbn1.Checked = true;
                       //rbtn2.Checked = false;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "CallMyFunction", "abc('Yes')", true);//script used for calling js function
                    }
                    else
                    {
                        //rbtn2.Checked = true;
                        //rbn1.Checked = false;

                       
                        ScriptManager.RegisterStartupScript(Page, GetType(), "CallMyFunction", "abc1('No')", true);//script used for calling js function
                    }
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

                //    if (ViewState["RTI_ID"] != "" && ViewState["RTI_ID"] != null)
                //    {
                //        DataSet dsRTIRecord = objdb.ByProcedure("SpRtiReqDetail",
                //           new string[] { "flag", "RTI_ID" },
                //                new string[] { "3", ViewState["RTI_ID"].ToString() }, "dataset");

                //        if (dsRTIRecord != null && dsRTIRecord.Tables[0].Rows.Count > 0)
                //        {
                //            txtRTI_Subject.Text = dsRTIRecord.Tables[0].Rows[0]["RTI_Subject"].ToString();
                //            txtRTI_Request.Text = dsRTIRecord.Tables[0].Rows[0]["RTI_Request"].ToString();
                //            //ViewState["RTI_RequestDocPath"] = dsRTIRecord.Tables[0].Rows[0]["RTI_RequestDoc"].ToString();
                //            if (dsRTIRecord.Tables[0].Rows[0]["RTI_RequestDoc"].ToString() != "")
                //            {
                //                hyprRTI_RequestDoc.NavigateUrl = "~/mis/RTI/RTI_Docs/" + ViewState["RTI_RequestDocPath"].ToString();
                //                hyprRTI_RequestDoc.Visible = true;
                //            }
                //            else
                //            {
                //                hyprRTI_RequestDoc.Visible = false;
                //            }
                //        }
                //        else { }
                //    }
                //    else { }
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

    protected void BPLBlock()
    {
        try
        {
            //string BPLStatus = rbtnlApp_PLStatus.SelectedItem.ToString();
            string BPLStatus = "";
            if (rbn1.Checked == true)
            {
                BPLStatus = rbn1.Text;
            }
            if (rbtn2.Checked == true)
            {
                BPLStatus = rbtn2.Text;
            }
            if (BPLStatus == "Yes (हाँ)")
            {
                BPLCardNo.Visible = true;
                YearOfIssue.Visible = true;
                IssuingAuthority.Visible = true;
                //divForOffLine.Visible = false;
            }
            if (BPLStatus == "No (नहीं)")
            {
                BPLCardNo.Visible = false;
                YearOfIssue.Visible = false;
                IssuingAuthority.Visible = false;
                //divForOffLine.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtApp_MobileNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            
            lblMsg.Text = "";
            string msg ="";
            if (txtApp_MobileNo.Text.Trim() == "")
            {
                msg += "Enter Mobile No.<br/>";
            }
            else
            {
                if (txtApp_MobileNo.Text.Length != 10)
                {
                    msg += "Enter Valid Mobile No.<br/>";
                }
                else
                {
                    int firstdigit = Int32.Parse(txtApp_MobileNo.Text.Substring(0, 1));
                    if (firstdigit < 6)
                    {
                        msg += "Enter Valid Mobile No.<br/>";
                    }
                }
            }
            if(msg == "")
            {
                ViewState["App_MobileNo"] = txtApp_MobileNo.Text;
                string App_IsActive = "1";
                // Insert Mobile No

                {
                    ds = objdb.ByProcedure("SpApplicantDetail", new string[] { "flag", "App_MobileNo" }, new string[] { "22", ViewState["App_MobileNo"].ToString() }, "Dataset");
                    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["App_ID"] = ds.Tables[0].Rows[0]["App_ID"].ToString();
                        FillDetails();
                       
                         //txtRTI_Subject.Text = "";
                        // txtRTI_Request.Text = "";
                    }
                    if (ds != null && ds.Tables[0].Rows.Count == 0)
                    {
                        DataSet dsMob = new DataSet();
                        ClearField();
                        FillState();
                        dsMob = objdb.ByProcedure("SpApplicantDetail", new string[] { "flag", "App_IsActive", "App_MobileNo" }, new string[] { "0", App_IsActive, ViewState["App_MobileNo"].ToString() }, "Dataset");
                        ViewState["App_ID"] = dsMob.Tables[0].Rows[0]["App_ID"].ToString();

                        if (rbn1.Checked == true)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "CallMyFunction", "abc('Yes')", true);//script used for calling js 
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "CallMyFunction", "abc1('No')", true);//script used for calling js 
                        }
                        
                    }
                    if (ds == null)
                    {
                        Response.Redirect("ApplyRTi.aspx");
                        
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!",msg);
                ClearField();
                ScriptManager.RegisterStartupScript(Page, GetType(), "CallMyFunction", "checkMobileField()", true);
            }
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
            //if (txtApp_Email.Text.Trim() == "")
            //{
            //    msg += "Enter Email<br/>";
            //}
            if (ddlApp_UserType.SelectedIndex <= 0)
            {
                msg += "Select User Type<br/>";
            }
            if (txtApp_Address.Text.Trim() == "")
            {
                msg += "Enter Address<br/>";
            }
            //if (txtApp_Pincode.Text.Trim() == "")
            //{
                //msg += "Enter Pincode<br/>";
           // }
           // if (ddlApp_Block.SelectedIndex <= 0)
            //{
                //msg += "Select Block<br/>";
          //  }
            if (ddlApp_District.SelectedIndex <= 0)
            {
                msg += "Select District<br/>";
            }
            if (ddlApp_State.SelectedIndex <= 0)
            {
                msg += "Select State<br/>";
            }
            if (txtRTIFileDate.Text == "")
            {
                msg += "Enter RTI File Date<br/>";
            }
            //if (rbtnlApp_PLStatus.SelectedIndex < 0)
            //{
            //    msg += "Select Povert Status<br/>";
            //}
            if (rbn1.Checked == true)
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
            //if (rbtnlApp_PLStatus.SelectedIndex != 1)
            //{
            //    if (txtApp_BPLCardNo.Text.Trim() == "")
            //    {
            //        msg += "Enter BPL Card No<br/>";
            //    }
            //    if (txtApp_YearOfIssue.Text.Trim() == "")
            //    {
            //        msg += "Enter Year Of Issue<br/>";
            //    }
            //    if (txtApp_IssuingAuthority.Text.Trim() == "")
            //    {
            //        msg += "Enter Issuing Authority<br/>";
            //    }
            //}

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
            if (rbtn2.Checked == true)
            {
                if (ddlRTI_PaymentMode.SelectedIndex <= 0)
                {
                    msg += "Select Payment Mode<br/>";
                }
                if (txtRTI_POReceiptNo.Text.Trim() == "")
                {
                    msg += "Enter PO/ Receipt No<br/>";
                }
                if (txtRTI_Amount.Text.Trim() == "")
                {
                    msg += "Enter Ammount<br/>";
                }
            }
            
            if (msg == "")
            {
                if (btnSubmit.Text.Trim() == "Submit")
                {
                    // Insert Applicant Detail
                    string App_IsActive = "1"; string rbtnBPL = "";
                    if (rbn1.Checked == true)
                    {
                        rbtnBPL = rbn1.Text;
                    }
                    if (rbtn2.Checked == true)
                    {
                        rbtnBPL = rbtn2.Text;
                    }
                    //objdb.ByProcedure("SpApplicantDetail",
                    //    new string[] { "flag", "App_ID", "App_IsActive", "App_Name", "App_MobileNo", "App_Email", "App_UserType", "App_Gender", "App_Address", "App_Pincode", "App_Block", "App_District", "App_State", "App_PLStatus", "App_BPLCardNo", "App_YearOfIssue", "App_IssuingAuthority" },
                    //              new string[] { "4", ViewState["App_ID"].ToString(), App_IsActive, txtApp_Name.Text.Trim(), txtApp_MobileNo.Text.Trim(), txtApp_Email.Text.Trim(), ddlApp_UserType.SelectedItem.ToString(), rbtnlApp_Gender.SelectedValue, txtApp_Address.Text.Trim(), txtApp_Pincode.Text.Trim(), ddlApp_Block.SelectedValue, ddlApp_District.SelectedValue, ddlApp_State.SelectedValue, rbtnlApp_PLStatus.SelectedItem.ToString(), txtApp_BPLCardNo.Text.Trim(), txtApp_YearOfIssue.Text.Trim().Trim(), txtApp_IssuingAuthority.Text.Trim() }, "dataset");
                    objdb.ByProcedure("SpApplicantDetail",
                        new string[] { "flag", "App_ID", "App_IsActive", "App_Name", "App_MobileNo", "App_Email", "App_UserType", "App_Gender", "App_Address", "App_Pincode", "App_Block", "App_District", "App_State", "App_PLStatus", "App_BPLCardNo", "App_YearOfIssue", "App_IssuingAuthority" },
                                  new string[] { "4", ViewState["App_ID"].ToString(), App_IsActive, txtApp_Name.Text.Trim(), txtApp_MobileNo.Text.Trim(), txtApp_Email.Text.Trim(), ddlApp_UserType.SelectedItem.ToString(), rbtnlApp_Gender.SelectedValue, txtApp_Address.Text.Trim(), txtApp_Pincode.Text.Trim(), ddlApp_Block.SelectedValue, ddlApp_District.SelectedValue, ddlApp_State.SelectedValue, rbtnBPL, txtApp_BPLCardNo.Text.Trim(), txtApp_YearOfIssue.Text.Trim().Trim(), txtApp_IssuingAuthority.Text.Trim() }, "dataset");

                    //Insert RTI Application on the basis of App_ID

                    string SupportingDoc = "";

                    if (fuRTI_RequestDoc.HasFile)
                    {
                        SupportingDoc = Guid.NewGuid() + "-" + fuRTI_RequestDoc.FileName;
                    }
                    else
                    {
                        SupportingDoc = "";
                    }
                    ds = null;
                    string RTI_IsActive = "1";
                    string amount = "0.00";
                    if (txtRTI_Amount.Text.Trim() != "")
                    {
                        amount = txtRTI_Amount.Text;
                    }
                    ds = objdb.ByProcedure("SpRtiReqDetail",
                                new string[] { "flag", "App_ID", "RTI_IsActive", "RTI_Subject", "RTI_Request", "RTI_RequestDoc", "RTI_PaymentMode", "RTI_POReceiptNo", "RTI_Amount", "RTI_ByOfficeID", "RTI_UpdatedOn" },
                                     new string[] { "0", ViewState["App_ID"].ToString(), RTI_IsActive, txtRTI_Subject.Text.Trim(), txtRTI_Request.Text.Trim(), SupportingDoc, ddlRTI_PaymentMode.SelectedValue, txtRTI_POReceiptNo.Text.Trim(), amount, ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtRTIFileDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");

                    //Session["RTI_RegistrationNo"] = ds.Tables[0].Rows[0]["RTI_RegistrationNo"].ToString();
                    Session["RTI_RegistrationNo"] = ds.Tables[0].Rows[0]["RTI_RegistrationNo"].ToString();
                    if (fuRTI_RequestDoc.HasFile)
                    {
                        fuRTI_RequestDoc.PostedFile.SaveAs(Server.MapPath("~/mis/RTI/RTI_Docs/" + SupportingDoc));
                    }
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    txtApp_MobileNo.Text = "";
                    txtRTI_Subject.Text = "";
                    txtRTI_Request.Text = "";
                    ClearField();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "abc('Yes')", true);
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearField()
    {
        try
        {
            //txtRTI_Subject.Text = "";
            //txtRTI_Request.Text = "";
           // txtApp_MobileNo.Text = "";
            txtApp_Name.Text = "";
            ddlApp_UserType.ClearSelection();
            txtApp_Email.Text = "";
            rbtnlApp_Gender.SelectedIndex = 0;
            ddlApp_State.ClearSelection();
            ddlApp_District.Items.Clear();
            ddlApp_District.ClearSelection();
            ddlApp_Block.Items.Clear();
            ddlApp_Block.ClearSelection();
            txtApp_Pincode.Text = "";
            txtApp_Address.Text = "";
            rbn1.Checked = true;
            //rbtnlApp_PLStatus.SelectedIndex = 0;
            txtApp_BPLCardNo.Text = "";
            txtApp_YearOfIssue.Text = "";
            txtApp_IssuingAuthority.Text = "";
            ddlRTI_PaymentMode.ClearSelection();
            txtRTI_Amount.Text = "";
            txtRTI_POReceiptNo.Text = "";
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}