using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.IO;

public partial class mis_Garage_GrgVehicleReportDateWise : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    txtFromDate.Attributes.Add("readonly", "readonly");

                    txtToDate.Attributes.Add("readonly", "readonly");

                    FillVehicle();
                }

            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillVehicle()
    {
        try
        {
            ddlVehicleNo.Items.Clear();
            ds = objdb.ByProcedure("spGrgVehicleMaster", new string[] { "flag", "VehicleOwnedType", "Office_ID" },
                new string[] { "12", ddlVehicleOwnedType.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlVehicleNo.DataSource = ds.Tables[0];
                ddlVehicleNo.DataTextField = "VehicleNo";
                ddlVehicleNo.DataValueField = "VehicleID";
                ddlVehicleNo.DataBind();
                ddlVehicleNo.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlVehicleNo.Items.Insert(0, new ListItem("All", "0"));
            }
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
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                ds = objdb.ByProcedure("spGrgVehicleMaster", new string[] { "flag", "Office_ID", "FromDate", "ToDate", "VehicleOwnedType", "VehicleID" },
                    new string[] { "5", ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlVehicleOwnedType.SelectedValue.ToString(), ddlVehicleNo.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";


            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("spGrgVehicleMaster",
                      new string[] { "flag", "VehicleID" },
                      new string[] { "6", id.ToString() }, "dataset");
            FillGrid();
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted");


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1): " + ex.Message.ToString());
        }
    }
    protected void ddlVehicleOwnedType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillVehicle();
    }
}
