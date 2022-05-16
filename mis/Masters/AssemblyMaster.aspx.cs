using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Text;

public partial class mis_Masters_AssemblyMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["EMP"] = "1";
            if (Session["EMP"] != null)
            {
                FillAssembly();
            }
            else
            {

            }
        }
    }
    protected void FillAssembly()
    {
        try
        {
            GridDetails.DataSource = objdb.ByProcedure("Usp_Assembly",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
            GridDetails.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (txtAssembly.Text.Trim() == "")
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "", "Please Enter Assembly");
            }
            else
            {
                try
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
                    if (btnSave.Text == "SUBMIT")
                    {
                        ds = null;

                        ds = objdb.ByProcedure("Usp_Assembly",
                                                new string[] { "flag", "Assembly_Name", "Assembly_IsActive", "Assembly_UpdatedBy" },
                                                new string[] { "2", txtAssembly.Text.Trim(), isactive.ToString(), Session["EMP"].ToString() }, "dataset");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-danger", "", "Assembly Saved Successfully.");
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "", "Assembly Already Exists.");
                        }
                    }
                    if (btnSave.Text == "Update")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("Usp_Assembly",
                                                new string[] { "flag", "Assembly_Name", "Assembly_IsActive", "Assembly_UpdatedBy", "Assembly_Id" },
                                                new string[] { "4", txtAssembly.Text.Trim(), isactive.ToString(), Session["EMP"].ToString(), ViewState["Assembly_ID"].ToString() }, "dataset");
                        btnSave.Text = "SUBMIT";
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-danger", "", "Assembly Saved Successfully.");
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "", "Assembly Already Exists.");
                        }
                    }
                    FillAssembly();
                    Clear();
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
                }
            }
        }
    }
    protected void Clear()
    {
        txtAssembly.Text = "";
        chkIsActive.Checked = true;
    }
    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            Clear();
            string Assembly_ID = Convert.ToString(e.CommandArgument.ToString());
            ViewState["Assembly_ID"] = Assembly_ID;
            ds = objdb.ByProcedure("Usp_Assembly", new string[] { "flag", "Assembly_ID" }, new string[] { "3", Assembly_ID }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAssembly.Text = ds.Tables[0].Rows[0]["Assembly_Name"].ToString();
                if (ds.Tables[0].Rows[0]["Assembly_IsActive"].ToString() == "1")
                {
                    chkIsActive.Checked = true;
                }
                else
                {
                    chkIsActive.Checked = false;
                }
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridDetails.PageIndex = e.NewPageIndex;
        FillAssembly();
    }
    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell statusCell = e.Row.Cells[2];
            if (statusCell.Text == "1")
            {
                statusCell.Text = "Yes";
            }
            if (statusCell.Text == "0")
            {
                statusCell.Text = "No";
            }
        }
    }
}