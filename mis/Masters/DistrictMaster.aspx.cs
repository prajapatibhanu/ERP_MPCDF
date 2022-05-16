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

public partial class mis_Masters_DistrictMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            Session["EMP"] = "1";
            FillDivision();
            GetData();
        }
    }
    protected void GetData()
    {
        try
        {
            GridDetails.DataSource = objdb.ByProcedure("Usp_District",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
            GridDetails.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDivision()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Usp_District",
                            new string[] { "flag" },
                            new string[] { "6" }, "dataset");
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_ID";
            ddlDivision.DataSource = ds;
            ddlDivision.DataBind();
            ddlDivision.Items.Insert(0, new ListItem("--Select Division--", "0"));
            ddlDivision.SelectedIndex = 0;
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
                string District_ID = Convert.ToString(e.CommandArgument.ToString());
                ViewState["District_ID"] = District_ID;
                ds = objdb.ByProcedure("Usp_District", new string[] { "flag", "District_ID" }, new string[] { "3", District_ID }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDivision.SelectedValue = ds.Tables[0].Rows[0]["Division_ID"].ToString();
                    txtdistrict.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                    if (ds.Tables[0].Rows[0]["District_IsActive"].ToString() == "1")
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
    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell statusCell = e.Row.Cells[3];
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
    protected void GridDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridDetails.PageIndex = e.NewPageIndex;
        GetData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (ddlDivision.SelectedIndex == 0)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Please Select Division", "");
                }
                else if (txtdistrict.Text.Trim() == "")
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Please Enter District Name", "");
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

                        ds = objdb.ByProcedure("Usp_District",
                                                new string[] { "flag", "Division_ID", "District_Name", "District_IsActive", "District_UpdatedBy" },
                                                new string[] { "2", ddlDivision.SelectedValue, txtdistrict.Text.Trim(), isactive.ToString(), Session["EMP"].ToString() }, "dataset");
                        if (ds.Tables.Count > 0)
                        {
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-warning", "Data Saved Successfully", "");
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-Success", "Data Already Exists.", "");
                        }
                    }
                    if (btnSave.Text == "Update")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("Usp_District",
                                                new string[] { "flag", "District_ID", "District_Name", "District_IsActive", "District_UpdatedBy", "Division_Id" },
                                                new string[] { "4", ViewState["District_ID"].ToString(), txtdistrict.Text.Trim(), isactive.ToString(), Session["EMP"].ToString(), ddlDivision.SelectedValue }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-warning", "Data Updated Successfully", "");
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", ds.Tables[0].Rows[0]["Msg"].ToString(), "");
                            btnSave.Text = "SUBMIT";
                        }
                    }
                    FillDivision();
                    GetData();
                    Clear();
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
        txtdistrict.Text = "";
        chkIsActive.Checked = true;        
    }
}