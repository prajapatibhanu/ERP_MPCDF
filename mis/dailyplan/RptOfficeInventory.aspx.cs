using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Drawing;
using System.IO;

public partial class mis_dailyplan_RptOfficeInventory : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();
                FillDropdown();
                //ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                //ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("ReadOnly", "ReadOnly");
                txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtToDate.Attributes.Add("ReadOnly", "ReadOnly");
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpItemCategory",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = ds;
                ddlCategory.DataTextField = "ItemCatName";
                ddlCategory.DataValueField = "ItemCat_id";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
                ddlCategory.SelectedValue = "1";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        FillGrid();
    }

    protected void ddlDS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblSeletedInfo.Text = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlProductSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblSeletedInfo.Text = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            lblSeletedInfo.Text = "";
            ViewState["SelectedOffice"] = ddlDS.SelectedValue.ToString();
            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy-MM-dd");
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy-MM-dd");
            ViewState["ddlCategory"] = ddlCategory.SelectedValue.ToString();



            ds = objdb.ByProcedure("spProductionItemStock", new string[] { "flag", "ItemCat_id", "Office_ID", "FromDate", "ToDate" }, new string[] { "15", ViewState["ddlCategory"].ToString(), ddlDS.SelectedValue.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;

            }
            if (ds != null)
            {
                //btnSave.Visible = true;
            }
            /********************************/

            lblSeletedInfo.Text = "<b>Dugdh Sangh :</b> " + ddlDS.SelectedItem.ToString() + " </p> <p><b>From Date :</b> " + txtFromDate.Text.ToString() + " </p> <p><b>To Date :</b> " + txtToDate.Text.ToString() + " </p><p> <b>Category :</b>   " + ddlCategory.SelectedItem.ToString() + "</p> ";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}