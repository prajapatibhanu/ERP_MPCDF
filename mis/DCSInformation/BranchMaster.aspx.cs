using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformation_BranchMaster : System.Web.UI.Page
{
    DataSet ds, ds1;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillBankDetail();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    protected void FillGrid()
    {
        try
        {
            gvBanchDetail.DataSource = null;
            gvBanchDetail.DataBind();
            ds = objdb.ByProcedure("USP_TblMasterBranch", new string[] { "flag", "BankID" }, new string[] { "4", ddlBankName.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvBanchDetail.DataSource = ds;
                gvBanchDetail.DataBind();
                gvBanchDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvBanchDetail.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillBankDetail()
    {
        try
        {
            ds = objdb.ByProcedure("USP_TblBankMaster", new string[] { "flag" }, new string[] { "4" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlBankName.DataTextField = "BankName";
                ddlBankName.DataValueField = "BankId";
                ddlBankName.DataSource = ds;
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnSave.Text == "Save")
            {
                ds = objdb.ByProcedure("USP_TblMasterBranch", new string[] { "flag", "BankID", "BranchCode", "BranchName", "IsActive", "IFSCCode", "CreatedBy" },
               new string[] { "1", ddlBankName.SelectedValue, txtIFSCCode.Text, txtBranchName.Text, "1", txtIFSCCode.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Record Saved Successfully.");
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Record Already Exists.");
                    }
                }
            }
            if (btnSave.Text == "Update")
            {
                ds = objdb.ByProcedure("USP_TblMasterBranch", new string[] { "flag", "ID", "BankID", "BranchCode", "BranchName", "IFSCCode" },
              new string[] { "2", ViewState["Id"].ToString(), ddlBankName.SelectedValue, txtIFSCCode.Text, txtBranchName.Text, txtIFSCCode.Text }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Record Updated Successfully.");
                        btnSave.Text = "Save";
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Record Already Exists.");
                    }
                }
            }
            //ddlBankName.ClearSelection();
            txtBranchName.Text = "";
            txtbranchCode.Text = "";
            txtIFSCCode.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvBanchDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["Id"] = e.CommandArgument.ToString();
            if (e.CommandName == "RecordUpdate")
            {
                ds = objdb.ByProcedure("USP_TblMasterBranch", new string[] { "flag", "ID" },
                new string[] { "3", e.CommandArgument.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlBankName.ClearSelection();
                    ddlBankName.Items.FindByValue(ds.Tables[0].Rows[0]["BankId"].ToString()).Selected = true;
                    txtIFSCCode.Text = ds.Tables[0].Rows[0]["BranchCode"].ToString();
                    txtBranchName.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    //txtIFSCCode.Text = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                    btnSave.Text = "Update";
                }
            }
            if (e.CommandName == "RecordDelete")
            {
                ds = objdb.ByProcedure("USP_TblMasterBranch", new string[] { "flag", "ID" },
               new string[] { "5", e.CommandArgument.ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Deleted Successfully.");
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlBankName.SelectedIndex > 0)
            {
                FillGrid();
                btnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}