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
public partial class mis_Garage_GrgVehicleMaintenanceReport : System.Web.UI.Page
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
                    FillYearMonth();
                   // GridView1.Visible = false;
                    GridViewOwned.Visible = false;
                    GridViewHired.Visible = false;
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
                new string[] { "12", rbtVehicleOwnedType.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
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
    protected void FillYearMonth()
    {
        try
        {

            ds = objdb.ByProcedure("spGrgVehicleMaster", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds.Tables[0];
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, "Select");

            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                ddlMonth.DataSource = ds.Tables[1];
                ddlMonth.DataTextField = "MonthName";
                ddlMonth.DataValueField = "MonthID";
                ddlMonth.DataBind();
                ddlMonth.Items.Insert(0, "Select");

            }
            //if (rbtVehicleOwnedType.SelectedValue.ToString() == "Hired")
            //{
            //    ds = objdb.ByProcedure("spGrgVehicleMaster", new string[] { "flag", "VehicleOwnedType" }, new string[] { "7", rbtVehicleOwnedType.SelectedValue.ToString() }, "dataset");
            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        ddlAgency.DataSource = ds.Tables[0];
            //        ddlAgency.DataTextField = "Agency";
            //        ddlAgency.DataValueField = "Agency";
            //        ddlAgency.DataBind();
            //        // ddlAgency.Items.Insert(0, "Select");

            //    }
            //}
            //ddlAgency.Items.Insert(0, new ListItem("All", "0"));

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
            GridViewOwned.DataSource = null;
            GridViewOwned.DataBind();

            GridViewHired.DataSource = null;
            GridViewHired.DataBind();

            if (rbtVehicleOwnedType.SelectedValue.ToString() == "Owned")
            {
                GridViewOwned.Visible = true;
            }
            else
            {
                GridViewHired.Visible = true;
            }
            
            //GridView1.Visible = true;
            //GridView1.DataSource = null;
            //GridView1.DataBind();

            //GridView1.Columns[1].Visible = true;
            //GridView1.Columns[2].Visible = true;
            //GridView1.Columns[3].Visible = true;
            //GridView1.Columns[4].Visible = true;
            //GridView1.Columns[5].Visible = true;
            //GridView1.Columns[6].Visible = true;
            //GridView1.Columns[7].Visible = true;
            //GridView1.Columns[8].Visible = true;
            //GridView1.Columns[9].Visible = true;
            //GridView1.Columns[10].Visible = true;
            //GridView1.Columns[11].Visible = true;
            //GridView1.Columns[12].Visible = true;
            //GridView1.Columns[13].Visible = true;
            //GridView1.Columns[14].Visible = true;
            //GridView1.Columns[15].Visible = true;
            //GridView1.Columns[16].Visible = true;
            if (ddlYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("spGrgVehicleMaster", new string[] { "flag", "Office_ID", "Year", "MonthNo", "VehicleOwnedType", "VehicleID" },
                    new string[] { "8", ViewState["Office_ID"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), rbtVehicleOwnedType.SelectedValue.ToString(), ddlVehicleNo.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    if (rbtVehicleOwnedType.SelectedValue.ToString() == "Owned")
                    {
                        GridViewOwned.DataSource = ds.Tables[0];
                        GridViewOwned.DataBind();
                        GridViewOwned.HeaderRow.TableSection = TableRowSection.TableHeader;
                        GridViewOwned.UseAccessibleHeader = true;
                    }
                    else
                    {
                        GridViewHired.DataSource = ds.Tables[0];
                        GridViewHired.DataBind();
                        GridViewHired.HeaderRow.TableSection = TableRowSection.TableHeader;
                        GridViewHired.UseAccessibleHeader = true;
                    }


                    //GridView1.DataSource = ds.Tables[0];
                    //GridView1.DataBind();
                    //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //GridView1.UseAccessibleHeader = true;

                    //if (rbtVehicleOwnedType.SelectedValue.ToString() == "Hired")
                    //{
                    //    //GridView1.Columns[1].Visible = false;
                    //    //GridView1.Columns[2].Visible = false;

                    //    GridView1.Columns[4].Visible = false;
                    //    GridView1.Columns[5].Visible = false;
                    //    GridView1.Columns[6].Visible = false;
                    //    GridView1.Columns[7].Visible = false;
                    //    GridView1.Columns[8].Visible = false;
                    //    GridView1.Columns[9].Visible = false;
                    //    GridView1.Columns[10].Visible = false;
                    //    GridView1.Columns[11].Visible = false;
                    //    GridView1.Columns[12].Visible = false;
                    //    GridView1.Columns[13].Visible = false;
                    //}
                    //else
                    //{
                    //    GridView1.Columns[3].Visible = false;
                    //    GridView1.Columns[14].Visible = false;
                    //    GridView1.Columns[15].Visible = false;
                    //    GridView1.Columns[16].Visible = false;

                    //}
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

   
    protected void rbtVehicleOwnedType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //ddlAgency.Items.Clear();
            //divAgency.Visible = false;
            
            
            //GridView1.DataSource = null;
            //GridView1.DataBind();
            //GridView1.Visible = false;

            GridViewOwned.DataSource = null;
            GridViewOwned.DataBind();
            GridViewOwned.Visible = false;

            GridViewHired.DataSource = null;
            GridViewHired.DataBind();
            GridViewHired.Visible = false;

            //if (rbtVehicleOwnedType.SelectedValue.ToString() == "Hired")
            //{
            //    divAgency.Visible = true;
            //    ds = objdb.ByProcedure("spGrgVehicleMaster", new string[] { "flag", "VehicleOwnedType" }, new string[] { "7", rbtVehicleOwnedType.SelectedValue.ToString() }, "dataset");
            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        ddlAgency.DataSource = ds.Tables[0];
            //        ddlAgency.DataTextField = "Agency";
            //        ddlAgency.DataValueField = "Agency";
            //        ddlAgency.DataBind();
            //        // ddlAgency.Items.Insert(0, "Select");

            //    }

            //}
            //ddlAgency.Items.Insert(0, new ListItem("All", "0"));

            FillVehicle();
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridView1.DataSource = null;
        //GridView1.DataBind();
        //GridView1.Visible = false;

        GridViewOwned.DataSource = null;
        GridViewOwned.DataBind();
        GridViewOwned.Visible = false;

        GridViewHired.DataSource = null;
        GridViewHired.DataBind();
        GridViewHired.Visible = false;
    }
    //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        string MaintenanceID = GridView1.DataKeys[e.RowIndex].Value.ToString();
    //        objdb.ByProcedure("spGrgVehicleMaster",
    //                  new string[] { "flag", "MaintenanceID" },
    //                  new string[] { "9", MaintenanceID.ToString() }, "dataset");
    //        FillGrid();
    //        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted");


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1): " + ex.Message.ToString());
    //    }
    //}
    protected void GridViewOwned_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MaintenanceID = GridViewOwned.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("spGrgVehicleMaster",
                      new string[] { "flag", "MaintenanceID" },
                      new string[] { "9", MaintenanceID.ToString() }, "dataset");
            FillGrid();
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted");


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1): " + ex.Message.ToString());
        }
    }
    protected void GridViewHired_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MaintenanceID = GridViewHired.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("spGrgVehicleMaster",
                      new string[] { "flag", "MaintenanceID" },
                      new string[] { "9", MaintenanceID.ToString() }, "dataset");
            FillGrid();
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted");


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1): " + ex.Message.ToString());
        }
    }
}
