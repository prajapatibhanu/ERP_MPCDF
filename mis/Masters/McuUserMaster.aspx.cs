using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;


public partial class mis_Masters_McuUserMaster : System.Web.UI.Page
{

    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    CultureInfo cult = new CultureInfo("en-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!Page.IsPostBack)
            {

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('New MCMS User Created!')", true);

                    }
                }

                FillDropdown();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void ddlOfficeType_Init(object sender, EventArgs e)
    {
        //GetOfficeType();
    }


    protected void ddlOfficeType_Title_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOffice_ID.Items.Clear();
            ddlOffice_ID.Items.Insert(0, new ListItem("Select", "0"));

            if (ddlOfficeType_Title.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpAdminOfficeType", new string[] { "flag", "OfficeType_ID","Office_Parant_ID" }, new string[] { "11", ddlOfficeType_Title.SelectedValue,objdb.Office_ID()}, "dataset");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice_ID.DataSource = ds;
                    ddlOffice_ID.DataTextField = "Office_Name";
                    ddlOffice_ID.DataValueField = "Office_ID";
                    ddlOffice_ID.DataBind();
                    ddlOffice_ID.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            ddlOfficeType_Title.Focus();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void FillDropdown()
    {
        try
        {
            // Office Type Title
            ds = null;
            ds = objdb.ByProcedure("SpAdminOfficeType", new string[] { "flag" }, new string[] { "7" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeType_Title.DataSource = ds;
                ddlOfficeType_Title.DataTextField = "OfficeTypeName";
                ddlOfficeType_Title.DataValueField = "OfficeType_ID";
                ddlOfficeType_Title.DataBind();
                ddlOfficeType_Title.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlOfficeType_Title.Items.Insert(0, new ListItem("Select", "0"));
            }

            ddlOffice_ID.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlBank_Init(object sender, EventArgs e)
    {
        GetBank();
    }

    private void GetBank()
    {
        try
        {
            ddlBank.DataSource = objdb.ByProcedure("sp_tblPUBankMaster",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
            ddlBank.DataTextField = "BankName";
            ddlBank.DataValueField = "Bank_id";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = "";
                ds = null;
                int ISActive = 1;

                if (chkIsActive.Checked)
                {
                    ISActive = 1;
                }
                else
                {
                    ISActive = 0;
                }

                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {

                        ds = objdb.ByProcedure("usp_Mst_McuUserMaster",
                               new string[] { "flag","OfficeType_ID", "Office_ID", "UserTypeId", "Name"
                            , "Mobile", "Email", "Bank_id","Branch_Name","IFSC","AccountNo","IsActive","CreatedBy","CreatedBy_IP"},
                               new string[] { "3", ddlOfficeType_Title.SelectedValue, ddlOffice_ID.SelectedValue,"13",txtOfficerName.Text,
                                   txtofficermobileNo.Text,txtOffice_Email.Text,ddlBank.SelectedValue,txtBranchName.Text,txtIFSCCode.Text,
                                   txtBankAccountNo.Text,ISActive.ToString(),objdb.createdBy(),objdb.GetLocalIPAddress() }, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            Session["IsSuccess"] = true;
                            Response.Redirect("McuUserMaster.aspx", false);
                        }
                    }
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
}