using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Demand_ViewOrdersCitizen : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataTable dt;
    int sumQ = 0;
    int sum11 = 0;
    int cellIndex = 4;
    int cellIndexbooth = 4;
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                pnldata.Visible = true;
                GetDatatableHeaderDesign();
                GetCitizenOrderDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void GetCartInfo()
    {

        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "1", objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                dt = ds1.Tables[0];

                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() == "QtyInNo" || column.ToString() == "CTotalAmount")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();

                GridView1.FooterRow.Cells[3].Text = "Total";
                GridView1.FooterRow.Cells[3].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "QtyInNo" || column.ToString() == "CTotalAmount")
                    {

                        sumQ = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndex].Text = sumQ.ToString();
                        GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Total")
                    {

                        sumQ = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));
                        int QTYSum = sumQ;
                        GridView1.FooterRow.Cells[cellIndex].Text = sumQ.ToString("N2");
                        GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }

                int rowcount = GridView1.FooterRow.Cells.Count - 3;
                GridView1.FooterRow.Cells[rowcount].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 2].Visible = false;

            }

            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
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

    private void GetInVoiceWiseDemand()
    {
        try
        {
            lblMsg.Text = "";
            //DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            //string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                     new string[] { "flag", "Office_ID" },
                       new string[] { "6", objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() == "InVoiceNo")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                //dt.Columns.Remove("RouteId");
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Routewise demand Report ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void GetCitizenOrderDetails()
    {
        try
        {

            //DateTime date3 = DateTime.ParseExact(txtDel.Text, "dd/MM/yyyy", culture);
            //DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            //string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            //string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                                new string[] { "flag", "Office_ID" },
                                new string[] { "6", objdb.Office_ID() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }

            if (ds.Tables[0].Rows.Count != 0)
            {

                pnldata.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                pnldata.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "ItemOrdered")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {


                    lblMsg.Text = string.Empty;
                    lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblCitizenName = (Label)row.FindControl("lblCitizenName");
                    Label lblQtyInNo = (Label)row.FindControl("lblQtyInNo");
                    Label lblCTotalAmount = (Label)row.FindControl("lblCTotalAmount");
                    Label lblItemCatid = (Label)row.FindControl("lblItemCatid");
                    Label lblMobileNo = (Label)row.FindControl("lblMobileNo");
                    Label lblDemand_Date = (Label)row.FindControl("lblDemand_Date");
                    Label lblDeliveryShift_id = (Label)row.FindControl("lblDeliveryShift_id");
                    Label lblDelivery_Date = (Label)row.FindControl("lblDelivery_Date");
                    Label lblShiftName = (Label)row.FindControl("lblShiftName");
                    // Label lblDelivery_Date = (Label)row.FindControl("lblDelivery_Date");
                    //GetCartInfo();

                    modalBoothName.InnerHtml = lblCitizenName.Text;
                    // modaldate.InnerHtml = lblMobileNo.Text;
                    // modelShift.InnerHtml = lblDelivery_Date.Text;

                    GridView2.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                                           new string[] { "flag", "MobileNo", "Office_ID" },
                                           new string[] { "7", e.CommandArgument.ToString(), objdb.Office_ID() }, "dataset");
                    GridView2.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

                }
            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblItemCatName = e.Row.FindControl("lblItemCatName") as Label;

                if (lblItemCatName.Text == "Milk")
                {
                    e.Row.CssClass = "columnmilk";
                }
                else
                {
                    e.Row.CssClass = "columnproduct";
                }

            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
}