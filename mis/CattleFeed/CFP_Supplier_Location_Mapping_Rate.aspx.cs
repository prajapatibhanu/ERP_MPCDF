using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_CattleFeed_CFP_Supplier_Location_Mapping_Rate : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillRbtn();
        fillSupplier();
        fillLocType();
        fillGrd();
    }
    private void fillRbtn()
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_CFP_Rate_Types", new string[] { "flag" }, new string[] { "0" }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            rbtnRateType.DataSource = ds;
            rbtnRateType.DataTextField = "RateType";
            rbtnRateType.DataValueField = "RateTypeID";
            rbtnRateType.DataBind();

        }
        rbtnRateType.SelectedValue = "1";
        GC.SuppressFinalize(objdb);

    }
    private void fillSupplier()
    {
        ddlSupplier.Items.Clear();
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_CFP_Supplier_Registration_ByOfficeID_List", new string[] { "flag", "OfficeID" }, new string[] { "0", objdb.Office_ID() }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlSupplier.DataSource = ds;
            ddlSupplier.DataTextField = "SupplierName";
            ddlSupplier.DataValueField = "SupplierRegistrationID";
            ddlSupplier.DataBind();

        }
        ddlSupplier.Items.Insert(0, new ListItem("-- Select --", "0"));
        GC.SuppressFinalize(objdb);

    }
    private void fillLocType()
    {
        ddllocationtype.Items.Clear();
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_CFPAdminOfficeType", new string[] { "flag" }, new string[] { "0" }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddllocationtype.DataSource = ds;
            ddllocationtype.DataValueField = "OfficeType_ID";
            ddllocationtype.DataTextField = "OfficeTypeName";
            ddllocationtype.DataBind();

        }
        ddllocationtype.Items.Insert(0, new ListItem("-- Select --", "0"));
        GC.SuppressFinalize(objdb);

    }
    private void fillLoc()
    {
        ddlLocation.Items.Clear();
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_CFP_AdminOffice_ByType", new string[] { "flag", "OfficeTypeID" }, new string[] { "0", ddllocationtype.SelectedValue }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlLocation.DataSource = ds;
            ddlLocation.DataValueField = "Office_ID";
            ddlLocation.DataTextField = "Office_Name";
            ddlLocation.DataBind();

        }
        ddlLocation.Items.Insert(0, new ListItem("-- Select --", "0"));
        GC.SuppressFinalize(objdb);

    }
    protected void ddllocationtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillLoc();
    }
    private void fillGrd()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_Supplier_Location_List", new string[] { "flag", "OfficeID" }, new string[] { "0", objdb.Office_ID() }, "dataset");
            grdCatlist.DataSource = ds;
            grdCatlist.DataBind();
            grdCatlist.HeaderRow.TableSection = TableRowSection.TableHeader;
            grdCatlist.UseAccessibleHeader = true;
            
        }
        catch (Exception ex) { }
        finally
        {
            GC.SuppressFinalize(ds);
            GC.SuppressFinalize(objdb);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                ds = new DataSet();
                string flag = "0";
                if (Convert.ToInt32(hdnvalue.Value) > 0)
                {
                    flag = "1";
                }
                ds = objdb.ByProcedure("SP_CFP_Supplier_Location_Insert_Update_Delete",
                    new string[] { "flag", "Location_Type_ID", "Location_ID", "Rate_Type_ID", "Rate", "TotalDistance", "InsertedBY", "IPAddress", "OfficeID", "SupplierID", "SupplierLocationID" },
                    new string[] { flag, ddllocationtype.SelectedValue, ddlLocation.SelectedValue, rbtnRateType.SelectedValue, txtRate.Text.Trim(), txtDistance.Text.Trim(), objdb.createdBy(), Request.UserHostAddress, objdb.Office_ID(), ddlSupplier.SelectedValue, hdnvalue.Value }, "TableSave");
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
                {
                    fillGrd();
                    Clear();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
    }

    private void Clear()
    {
        ddlSupplier.SelectedValue = "0";
        ddllocationtype.SelectedValue = "0";
        ddlLocation.SelectedValue = "0";
        rbtnRateType.SelectedValue = "1";
        txtRate.Text = string.Empty;
        txtDistance.Text = string.Empty;
        hdnvalue.Value = "0";
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Clear();
        lblMsg.Text = string.Empty;
    }

    protected void grdCatlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        lblMsg.Text = string.Empty;
        switch (e.CommandName)
        {
            case "RecordUpdate":
                DataSet ds1 = new DataSet();
                ds1 = objdb.ByProcedure("SP_CFP_Supplier_Location_By_ID", new string[] { "flag", "SupplierLocationID" }, new string[] { "0", hdnvalue.Value }, "dataset");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddllocationtype.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["LocationTypeID"]);
                    fillLoc();
                    ddlLocation.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["LocationID"]);
                    rbtnRateType.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["RateTypeID"]);
                    txtRate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Rate"]);
                    txtDistance.Text = Convert.ToString(ds1.Tables[0].Rows[0]["TotalDistance"]);
                    ddlSupplier.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["SupplierID"]);
                    btnSave.Text = "Edit";
                }
                GC.SuppressFinalize(objdb);
                GC.SuppressFinalize(ds1);
                break;
            case "RecordDelete":
                ds = new DataSet();
                ds = objdb.ByProcedure("SP_CFP_Supplier_Location_Insert_Update_Delete",
                    new string[] { "flag", "Location_Type_ID", "Location_ID", "Rate_Type_ID", "Rate", "TotalDistance", "InsertedBY", "IPAddress", "OfficeID", "SupplierLocationID" },
                    new string[] { "2", ddllocationtype.SelectedValue, ddlLocation.SelectedValue, rbtnRateType.SelectedValue, txtRate.Text.Trim(), txtDistance.Text.Trim(), objdb.createdBy(), Request.UserHostAddress, objdb.Office_ID(), hdnvalue.Value }, "TableSave");
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
                {
                    fillGrd();
                    Clear();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
                }
                GC.SuppressFinalize(objdb);
                GC.SuppressFinalize(ds);
                break;
            default:
                break;
        }
    }
    protected void grdCatlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCatlist.PageIndex = e.NewPageIndex;
        fillGrd();
    }
}