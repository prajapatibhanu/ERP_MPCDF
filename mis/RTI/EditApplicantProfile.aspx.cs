using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_RTI_EditApplicantProfile : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Request.QueryString["App_ID"] != null && Request.QueryString["App_ID"] != "" && Request.QueryString["TaskID"] != "" && Request.QueryString["ShowHide"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["App_ID"] = objdb.Decrypt(Request.QueryString["App_ID"]);
                    ViewState["TaskID"] = objdb.Decrypt(Request.QueryString["TaskID"]);
                    FillState();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["ShowHide"] = objdb.Decrypt(Request.QueryString["ShowHide"]);
                   
                    FillDetails();
                    EnableDisable();
                   // ddlApp_State.Attributes.Add("disabled", "disabled");
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
    protected void FillDetails()
    {
        try
        {
            DataSet dsRecord = objdb.ByProcedure("SpApplicantDetail",
                         new string[] { "flag", "App_ID" },
                              new string[] { "19", ViewState["App_ID"].ToString() }, "dataset");

            if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
            {
                string name = dsRecord.Tables[0].Rows[0]["App_Name"].ToString();

                if (name != "")
                {
                    txtApp_Name.Text = dsRecord.Tables[0].Rows[0]["App_Name"].ToString();
                    txtApp_MobileNo.Text = dsRecord.Tables[0].Rows[0]["App_MobileNo"].ToString();
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
                }
                else{}
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
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
            if (rbtnlApp_PLStatus.SelectedIndex == 1)
            {
                txtApp_BPLCardNo.Text = "";
                txtApp_YearOfIssue.Text= "";
                txtApp_IssuingAuthority.Text = "";
            }
            if (msg == "")
            {
                if (btnUpdate.Text.Trim() == "Update")
                {
                    DataSet dsRecord = objdb.ByProcedure("SpApplicantDetail",
                         new string[] { "flag", "App_MobileNo", "App_ID" },
                              new string[] { "20", txtApp_MobileNo.Text.Trim(), ViewState["App_ID"].ToString() }, "dataset");

                    if (dsRecord != null && dsRecord.Tables[0].Rows.Count == 0)
                    {
                        // Insert Applicant Detail
                        string App_IsActive = "1";
                        objdb.ByProcedure("SpApplicantDetail",
                            new string[] { "flag", "App_ID", "App_IsActive", "App_Name", "App_MobileNo", "App_Email", "App_UserType", "App_Gender", "App_Address", "App_Pincode", "App_Block", "App_District", "App_State", "App_PLStatus", "App_BPLCardNo", "App_YearOfIssue", "App_IssuingAuthority", "App_UpdatedBy" },
                                      new string[] { "4", ViewState["App_ID"].ToString(), App_IsActive, txtApp_Name.Text.Trim(), txtApp_MobileNo.Text.Trim(), txtApp_Email.Text.Trim(), ddlApp_UserType.SelectedItem.ToString(), rbtnlApp_Gender.SelectedValue, txtApp_Address.Text.Trim(), txtApp_Pincode.Text.Trim(), ddlApp_Block.SelectedValue, ddlApp_District.SelectedValue, ddlApp_State.SelectedValue, rbtnlApp_PLStatus.SelectedItem.ToString(), txtApp_BPLCardNo.Text.Trim(), txtApp_YearOfIssue.Text.Trim().Trim(), txtApp_IssuingAuthority.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Alert!", "Data Saved Successfully");
                        ViewState["TaskID"] = "0";
                        EnableDisable();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This Mobile Number is Already Registered.')", true);
                        txtApp_MobileNo.Text = "";
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

    protected void EnableDisable()
    {
        try
        {
            if (ViewState["TaskID"].ToString() == "0")
            {
                btnUpdate.Visible = false;
                Panel1.Enabled = false;
            }
            else
            {
                btnUpdate.Visible = true;
                Panel1.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void img1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["ShowHide"].ToString() == "Show")
            {
                Response.Redirect("ApplicantList.aspx");
            }
            else if (ViewState["ShowHide"].ToString() == "Hide")
            {
                Response.Redirect("/mis/RTI/ListOfAllApplicants.aspx");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
       
    }
}