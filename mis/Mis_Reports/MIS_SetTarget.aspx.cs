using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Mis_Reports_MIS_SetTarget : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetFinancialYear_and_OfficeDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetFinancialYear_and_OfficeDetails()
    {
        try
        {

            ds1 = objdb.ByProcedure("USP_MIS_Trn_SetTargetMilkProcurementOrSaleDSwise",
                 new string[] { "Flag" },
                   new string[] { "1" }, "dataset");
            if (ds1 != null)
            {
                ddlFiancialYear.DataTextField = "FinancialYear";
                ddlFiancialYear.DataValueField = "Phase_id";
                ddlFiancialYear.DataSource = ds1.Tables[0];
                ddlFiancialYear.DataBind();
                ddlFiancialYear.Items.Insert(0, new ListItem("Select", "0"));

                gvSetMilkProcurementOrSale.DataSource = ds1.Tables[1];
                gvSetMilkProcurementOrSale.DataBind();

                DataTable dt1 = new DataTable();
                dt1 = ds1.Tables[0];
                ViewState["ddFinancialYear"] = dt1;

                DataTable dt2 = new DataTable();
                dt2 = ds1.Tables[1];
                ViewState["gvSetMilkProcurementOrSale"] = dt2;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void gvSetMilkProcurementOrSale_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Set Target of Milk Procurement & Sale";
            HeaderCell.ColumnSpan = gvSetMilkProcurementOrSale.Columns.Count;
            HeaderGridRow.Cells.Add(HeaderCell);
            gvSetMilkProcurementOrSale.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertMilkProcurementOrsale();
        }

    }
    private void InsertMilkProcurementOrsale()
    {
        try
        {
            if (gvSetMilkProcurementOrSale.Rows.Count > 0)
            {
                int qty = 0;
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add(new DataColumn("Office_ID", typeof(int)));
                dt.Columns.Add(new DataColumn("MilkProcurement", typeof(decimal)));
                dt.Columns.Add(new DataColumn("MilkSale", typeof(decimal)));
                dr = dt.NewRow();
                foreach (GridViewRow row in gvSetMilkProcurementOrSale.Rows)
                {


                    GridViewRow selectedrow = row;
                    TextBox txtMilkProcurement = (TextBox)selectedrow.FindControl("txtMilkProcurement");
                    TextBox txtMilkSale = (TextBox)selectedrow.FindControl("txtMilkSale");
                    Label lblOffice_ID = (Label)row.FindControl("lblOffice_ID");
                    if (txtMilkProcurement.Text != "" && txtMilkSale.Text != "")
                    {
                        qty++;
                        dr[0] = lblOffice_ID.Text;
                        dr[1] = txtMilkProcurement.Text;
                        dr[2] = txtMilkSale.Text;

                        dt.Rows.Add(dr.ItemArray);
                    }

                }

                if (qty > 0)
                {


                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds1 = objdb.ByProcedure("USP_MIS_Trn_SetTargetMilkProcurementOrSaleDSwise",
                         new string[] { "Flag", "FYId", "TMonth", "CreatedBy", "CreatedByIP" },
                         new string[] { "2", ddlFiancialYear.SelectedValue, ddlMonth.SelectedValue, objdb.createdBy(), IPAddress },
                    new string[] { "type_MIS_Trn_SetTargetMilkProcurementOrSaleDSwise" },
                           new DataTable[] { dt }, "dataset");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        DataTable dt1 = (DataTable)ViewState["gvSetMilkProcurementOrSale"];
                        gvSetMilkProcurementOrSale.DataSource = dt1;
                        gvSetMilkProcurementOrSale.DataBind();
                        dt1.Dispose();
                        ddlMonth.SelectedIndex = 0;
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);


                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error + "Fiancial Year : " + ddlFiancialYear.SelectedItem.Text + " Month : " + ddlMonth.SelectedItem.Text);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  Milk Procurement or Sale Details:" + error);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warninng !", "Select atleast Milk Procurement or Sale Details");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Own Procurement", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void FillData()
    {
        try
        {
            lblMsg.Text = "";
            GridView1.DataSource = null;
            ds1 = objdb.ByProcedure("USP_MIS_Trn_SetTargetMilkProcurementOrSaleDSwise",
                        new string[] { "Flag", "FYId", "TMonth" },
                        new string[] { "4", ddlFiancialYear.SelectedValue, ddlMonth.SelectedValue }, "dataset");
            if (ds1 != null && ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds1;
                }
            }
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlFiancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFiancialYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                FillData();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFiancialYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                FillData();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}