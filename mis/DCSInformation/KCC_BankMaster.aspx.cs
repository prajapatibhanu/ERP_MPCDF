using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformation_KCC_BankMaster : System.Web.UI.Page
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
                    FillGrid();
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
            ds = objdb.ByProcedure("USP_TblBankMaster", new string[] { "flag" }, new string[] { "4" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvbankDetail.DataSource = ds;
                gvbankDetail.DataBind();
                gvbankDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvbankDetail.UseAccessibleHeader = true;
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
                ds = objdb.ByProcedure("USP_TblBankMaster", new string[] { "flag", "BankName", "IsActive", "CreatedBy" },
               new string[] { "1", txtBankName.Text, "1", ViewState["Emp_ID"].ToString() }, "dataset");
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
                ds = objdb.ByProcedure("USP_TblBankMaster", new string[] { "flag", "BankId", "BankName" },
              new string[] { "2", ViewState["BankId"].ToString(), txtBankName.Text }, "dataset");
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
            txtBankName.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvbankDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["BankId"] = e.CommandArgument.ToString();
            if (e.CommandName == "RecordUpdate")
            {
                ds = objdb.ByProcedure("USP_TblBankMaster", new string[] { "flag", "BankId" },
                new string[] { "3", ViewState["BankId"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtBankName.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                    btnSave.Text = "Update";
                }
            }
            if (e.CommandName == "RecordDelete")
            {
                ds = objdb.ByProcedure("USP_TblBankMaster", new string[] { "flag", "BankId" },
               new string[] { "5", ViewState["BankId"].ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Deleted Successfully.");
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}