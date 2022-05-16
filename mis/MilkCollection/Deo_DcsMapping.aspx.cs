using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_MilkCollection_Deo_DcsMapping : System.Web.UI.Page
{

    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {

        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                BindDeoEmp();

            }
        }
        else
        {
            objdb.redirectToHome();
        }

    }

    private void BindDeoEmp()
    {

        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("Usp_DeoDetail",
                              new string[] { "flag" },
                              new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddldeoemp.DataTextField = "DeoName";
                    ddldeoemp.DataValueField = "Deo_Id";
                    ddldeoemp.DataSource = ds;
                    ddldeoemp.DataBind();
                    ddldeoemp.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddldeoemp.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddldeoemp.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    private void BindGrid()
    {

        try
        {
            // lblMsg.Text = "";
            ds = objdb.ByProcedure("Usp_DeoDetail",
                              new string[] { "flag", "OfficeType_ID" },
                              new string[] { "5", ddltask.SelectedValue }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDeoDetail.DataSource = ds;
                    gvDeoDetail.DataBind();


                    foreach (GridViewRow row in gvDeoDetail.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");

                        if (chkSelect.Checked == true)
                        {
                            chkSelect.Enabled = false;
                        }
                        else
                        {
                            chkSelect.Enabled = true;
                        }

                    }

                }
                else
                {
                    gvDeoDetail.DataSource = string.Empty;
                    gvDeoDetail.DataBind();
                }
            }
            else
            {
                gvDeoDetail.DataSource = string.Empty;
                gvDeoDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void ddltask_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int Checkboxstatus = 0;

            foreach (GridViewRow row in gvDeoDetail.Rows)
            {
                Label lblRowNumber = (Label)row.FindControl("lblRowNumber");
                Label lblDeo_Id = (Label)row.FindControl("lblDeo_Id");
                Label lblOffice_Id = (Label)row.FindControl("lblOffice_Id");


                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");

                if (chkSelect.Checked == true && chkSelect.Enabled == true)
                {
                    Checkboxstatus = 1;

                    objdb.ByProcedure("Usp_DeoDetail",
                    new string[] { "flag", "Deo_Id", "Office_Id", "TaskAssignBy", "MappingStatus", "OfficeType_ID" },
                    new string[] { "6", ddldeoemp.SelectedValue, lblRowNumber.ToolTip.ToString(), 
                        txtTaskAssignBy.Text, Checkboxstatus.ToString(),ddltask.SelectedValue }, "dataset");

                }


            }
            BindGrid();
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Record Saved Successfully ");
            ddltask.ClearSelection();
            ddldeoemp.ClearSelection();
            txtTaskAssignBy.Text = "";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



}