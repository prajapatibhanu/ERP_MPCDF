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

public partial class mis_Masters_DivisionMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (!Page.IsPostBack)
        {
            Session["EMP"] = "1";
            if (Session["EMP"] != null)
            {
                FillDivision();
            }
            else
            {

            }
        }
    }
    protected void FillDivision()
    {
        try
        {
            GridDetails.DataSource = objdb.ByProcedure("Usp_Division",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
            GridDetails.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Page")
            {
                lblMsg.Text = "";
                //ClearText();
                string Division_ID = Convert.ToString(e.CommandArgument.ToString());
                ViewState["Division_ID"] = Division_ID;
                ds = objdb.ByProcedure("Usp_Division", new string[] { "flag", "Division_ID" }, new string[] { "3", Division_ID }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtdivision.Text = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                    if (ds.Tables[0].Rows[0]["Division_IsActive"].ToString() == "1")
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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridDetails.PageIndex = e.NewPageIndex;
        FillDivision();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (txtdivision.Text.Trim() == "")
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "", "Please Enter Division Name");
                }
                else
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

                        ds = objdb.ByProcedure("Usp_Division",
                                                new string[] { "flag", "Division_Name", "Division_IsActive", "Division_UpdatedBy" },
                                                new string[] { "2", txtdivision.Text.Trim(), isactive.ToString(), Session["EMP"].ToString() }, "dataset");
                        if (ds.Tables.Count > 0)
                        {
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Division Saved Successfully.", "");
                            FillDivision();
                            Clear();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Division Already Exist", "");
                        }
                    }
                    if (btnSave.Text == "Update")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("Usp_Division",
                                                new string[] { "flag", "Division_Name", "Division_IsActive", "Division_UpdatedBy", "Division_Id" },
                                                new string[] { "4", txtdivision.Text.Trim(), isactive.ToString(), Session["EMP"].ToString(), ViewState["Division_ID"].ToString() }, "dataset");
                        if (ds.Tables.Count > 0)
                        {
                            btnSave.Text = "SUBMIT";
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Division Updated Successfully.", "");
                            FillDivision();
                            Clear();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Division Already Exist", "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    protected void Clear()
    {
        txtdivision.Text = "";
        chkIsActive.Checked = true;
        btnSave.Text = "Submit";
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