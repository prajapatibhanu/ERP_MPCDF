using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Masters_BankBranchMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Emp_ID"] = "1";
        Session["Office_ID"] = "2";
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetBankBranchDetails();
                GetBank();
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

    #region=======================user defined function========================

    private void Clear()
    {
        txtBranchName.Text = string.Empty;
        ddlBank.SelectedIndex = 0;
        txtBranchCode.Text = string.Empty;
        txtIFSCCode.Text = string.Empty;
        btnSubmit.Text = "Submit";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
        chkIsActive.Checked = true;
    }
    private void GetBankBranchDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("sp_tblPUBankBranchMaster",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertOrUpdateBank()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    int isactive = 1;
                    if (chkIsActive.Checked)
                    {
                        isactive = 1;
                    }
                    else
                    {
                        isactive = 0;
                    }
                    if (btnSubmit.Text == "Submit")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("sp_tblPUBankBranchMaster",
                            new string[] { "flag", "Bank_id", "BranchName", "BranchCode", "IFSC", "CreatedBy", "Office_ID", "IsActive" },
                            new string[] { "2", ddlBank.SelectedValue, txtBranchName.Text.Trim(), txtBranchCode.Text.Trim(), txtIFSCCode.Text.Trim().ToUpper(), objdb.createdBy(), objdb.Office_ID(), isactive.ToString() }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetBankBranchDetails();
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Branch Already Exist", "");
                        }
                       // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#myModal').modal('show')", true);
                        ds.Clear();
                        Clear();
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("sp_tblPUBankBranchMaster",
                            new string[] { "flag", "Branch_id", "Bank_id", "BranchName", "BranchCode", "IFSC", "CreatedBy", "IsActive" },
                            new string[] { "3", ViewState["Branch_Id"].ToString(), ddlBank.SelectedValue, txtBranchName.Text.Trim(), txtBranchCode.Text.Trim(), txtIFSCCode.Text.Trim().ToUpper(), objdb.createdBy(),isactive.ToString() }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetBankBranchDetails();
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {

                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Branch Already Exist", "");
                        }
                        ds.Clear();
                        Clear();
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-danger", "Warning!", " Enter Bank Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    protected void GetBank()
    {
        try
        {
            ddlBank.DataSource = objdb.ByProcedure("sp_tblPUBankBranchMaster",
                   new string[] { "flag" },
                   new string[] { "7" }, "dataset");
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
    #endregion====================================end of user defined function==========



    #region=============== init or changed event for controls =================


    #endregion============ end of changed event for controls===========

    #region============ button click and gridview events event ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertOrUpdateBank();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            try
            {
                lblMsg.Text = "";
                //ClearText();
                string Branch_Id = Convert.ToString(e.CommandArgument.ToString());
                ViewState["Branch_Id"] = Branch_Id;
                ds = objdb.ByProcedure("sp_tblPUBankBranchMaster", new string[] { "flag", "Branch_id" }, new string[] { "8", Branch_Id }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlBank.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["Bank_id"].ToString());
                    txtBranchName.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    txtBranchCode.Text = ds.Tables[0].Rows[0]["BranchCode"].ToString();
                    txtIFSCCode.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();

                    if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "True" || ds.Tables[0].Rows[0]["IsActive"].ToString() == "1")
                    {
                        chkIsActive.Checked = true;
                    }
                    else
                    {
                        chkIsActive.Checked = false;
                    }
                    btnSubmit.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    #endregion=============end of button click funciton==================
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell statusCell = e.Row.Cells[5];
            if (statusCell.Text == "1" || statusCell.Text == "True")
            {
                statusCell.Text = "Yes";
            }
            if (statusCell.Text == "0" || statusCell.Text == "False")
            {
                statusCell.Text = "No";
            }
        }
    }
}