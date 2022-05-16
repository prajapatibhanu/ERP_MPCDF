using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_Demand_UpdateApprovedDemandChild : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds4, ds5, ds6 = new DataSet();
    double sum1 = 0;
    int sum11 = 0;
    int dsum11 = 0;
    int csum11 = 0;
    int cellIndexbooth = 3;
    IFormatProvider culture = new CultureInfo("en-US", true);
    string success = "", error = "";
    int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && Session["DDate"].ToString() != "" && Session["DShift"].ToString() != "" && Session["DCategory"].ToString() != "" && Session["DRDIType"].ToString() != "")
        {

            if (!Page.IsPostBack)
            {
                GetBoothandOrganizationDetails();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    private void GetBoothandOrganizationDetails()
    {
        try
        {
            lblMsg.Text = "";
            spanRDIName.InnerHtml = Session["D_RDIName"].ToString();
            SpanDate.InnerHtml = Session["DDate"].ToString();
            spanShift.InnerHtml = Session["DShiftName"].ToString();
            spanCategory.InnerHtml = Session["DCategoryName"].ToString();

            DateTime odate = DateTime.ParseExact(Session["DDate"].ToString(), "dd/MM/yyyy", culture);
            string orderedate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductApprovedDemandRDIwise",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date",
                                     "RouteId", "DistributorId", "OrganizationId" },
                       new string[] { "4", objdb.Office_ID(), Session["DShift"].ToString(), Session["DCategory"].ToString(), orderedate
                           , Session["D_RouteId"].ToString(), Session["D_DistributorId"].ToString(), Session["D_OrganizationId"].ToString() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                DataTable dt, dt1 = new DataTable();
                dt = ds1.Tables[0];
                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName" && column.ToString() != "Total Demand" && column.ToString() != "Total Crate" && column.ToString() != "Total Demand in Litre")
                        if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName" && column.ToString() != "Total Demand")
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

                GridView1.FooterRow.Cells[2].Text = "Total Demand";
                GridView1.FooterRow.Cells[2].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {
                    //if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName" && column.ToString() != "Total Demand" && column.ToString() != "Total Crate" && column.ToString() != "Total Demand in Litre")
                    if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName" && column.ToString() != "Total Demand")
                    {

                        sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndexbooth].Text = sum11.ToString();
                        GridView1.FooterRow.Cells[cellIndexbooth].Font.Bold = true;
                        cellIndexbooth = cellIndexbooth + 1;
                    }
                }

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Total Demand")
                    {

                        dsum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndexbooth].Text = dsum11.ToString();
                        GridView1.FooterRow.Cells[cellIndexbooth].Font.Bold = true;
                        cellIndexbooth = cellIndexbooth + 1;
                    }
                }
                //if (Session["DCategory"].ToString() != "2") // for milk category
                //{

                //    foreach (DataColumn column in dt.Columns)
                //    {
                //        if (column.ToString() == "Total Demand in Litre")
                //        {

                //            sum1 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

                //            GridView1.FooterRow.Cells[cellIndexbooth].Text = sum1.ToString("N2");
                //            GridView1.FooterRow.Cells[cellIndexbooth].Font.Bold = true;
                //            cellIndexbooth = cellIndexbooth + 1;
                //        }
                //    }
                //}

               
              //  if (Session["DCategory"].ToString() != "2")
               // {
                    int rowcount = GridView1.FooterRow.Cells.Count - 3;
                    GridView1.FooterRow.Cells[rowcount].Visible = false;
                    GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
                    GridView1.FooterRow.Cells[rowcount + 2].Visible = false;
              //  }
                //else
                //{
                //    int rowcount = GridView1.FooterRow.Cells.Count - 5;
                //    GridView1.FooterRow.Cells[rowcount].Visible = false;
                //    GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
                //    GridView1.FooterRow.Cells[rowcount + 2].Visible = false;
                //    GridView1.FooterRow.Cells[rowcount + 3].Visible = false;
                //    GridView1.FooterRow.Cells[rowcount + 4].Visible = false;
                //}
                ViewState["RoutOrDisOrInstWiseTable"] = "";
                if (dt1 != null) { dt1.Dispose(); }
            }
            else
            {
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
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

              //  string demandid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();

                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;

               // int maxrowcell1 = e.Row.Cells.Count - 1;
                //if (Session["DCategory"].ToString() != "3")
                //{
                //    e.Row.Cells[maxrowcell1].Visible = false;
                //    e.Row.Cells[maxrowcell1 + 1].Visible = false;
                //}
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {

                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;

                //int maxheadercell1 = e.Row.Cells.Count - 1;
                //if (Session["DCategory"].ToString() != "3")
                //{
                //    e.Row.Cells[maxheadercell1].Visible = false;
                //    e.Row.Cells[maxheadercell1 + 1].Visible = false;
                //}
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ItemOrdered")
        {
            Control ctrl = e.CommandSource as Control;
            if (ctrl != null)
            {
                lblMsg.Text = string.Empty;
                lblModalMsg.Text = string.Empty;
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                Label lblBandOName = (Label)row.FindControl("lblBandOName");
                ViewState["rowid"] = e.CommandArgument.ToString();
                GetItemDetailByDemandID();
                modalBoothName.InnerHtml = lblBandOName.Text;
                modaldate.InnerHtml = Session["DDate"].ToString();
                modalshift.InnerHtml = Session["DShiftName"].ToString();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);


            }
        }
    }
    private void GetItemDetailByDemandID()
    {
        try
        {
            ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                  new string[] { "13", ViewState["rowid"].ToString(), Session["DCategory"].ToString(), objdb.Office_ID() }, "dataset");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                GridView4.DataSource = ds5.Tables[0];
                GridView4.DataBind();
            }
            else
            {
                GridView4.DataSource = null;
                GridView4.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 3 : " + ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordEdit")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    lblModalMsg.Text = string.Empty;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblTotalQty = (Label)row.FindControl("lblTotalQty");
                    Label lblFTQRemark = (Label)row.FindControl("lblFTQRemark");
                    TextBox txtTotalQty = (TextBox)row.FindControl("txtTotalQty");
                    TextBox txtTotalQtyRemark = (TextBox)row.FindControl("txtTotalQtyRemark");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RequiredFieldValidator rf2 = row.FindControl("rfv2") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;
                    CustomValidator cv1 = row.FindControl("CustomValidator1") as CustomValidator;

                    foreach (GridViewRow gvRow in GridView4.Rows)
                    {
                        Label HlblTotalQty = (Label)gvRow.FindControl("lblTotalQty");
                        Label HlblFTQRemark = (Label)gvRow.FindControl("lblFTQRemark");
                        TextBox HtxtTotalQty = (TextBox)gvRow.FindControl("txtTotalQty");
                        TextBox HtxtTotalQtyRemark = (TextBox)gvRow.FindControl("txtTotalQtyRemark");
                        LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
                        LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
                        LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
                        RequiredFieldValidator Hrfv = gvRow.FindControl("rfv1") as RequiredFieldValidator;
                        RequiredFieldValidator Hrfv2 = gvRow.FindControl("rfv2") as RequiredFieldValidator;
                        RegularExpressionValidator Hrev1 = gvRow.FindControl("rev1") as RegularExpressionValidator;
                        CustomValidator Hcv1 = row.FindControl("CustomValidator1") as CustomValidator;

                        HlblTotalQty.Visible = true;
                        HlblFTQRemark.Visible = true;
                        HtxtTotalQty.Visible = false;
                        HtxtTotalQtyRemark.Visible = false;
                        if (HlblFTQRemark.Text == "")
                        {
                            HlnkEdit.Visible = true;
                        }
                       else
                        {
                            HlnkEdit.Visible = false;
                        }
                        HlnkUpdate.Visible = false;
                        HlnkReset.Visible = false;
                        Hrfv.Enabled = false;
                        Hrfv2.Enabled = false;
                        Hrev1.Enabled = false;
                        Hcv1.Enabled = false;

                    }
                    txtTotalQty.Text = "";
                    txtTotalQty.Text = lblTotalQty.Text;
                    txtTotalQtyRemark.Text = "";
                    lblTotalQty.Visible = false;
                    lblFTQRemark.Visible = false;
                    lnkEdit.Visible = false;

                    lnkUpdate.Visible = true;
                    lnkReset.Visible = true;
                    rfv.Enabled = true;
                    rf2.Enabled = true;
                    rev1.Enabled = true;
                    cv1.Enabled = true;
                    txtTotalQty.Visible = true;
                    txtTotalQtyRemark.Visible = true;
                }

            }
            if (e.CommandName == "RecordReset")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblTotalQty = (Label)row.FindControl("lblTotalQty");
                    Label lblFTQRemark = (Label)row.FindControl("lblFTQRemark");
                    TextBox txtTotalQty = (TextBox)row.FindControl("txtTotalQty");
                    TextBox txtTotalQtyRemark = (TextBox)row.FindControl("txtTotalQtyRemark");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RequiredFieldValidator rfv2 = row.FindControl("rfv2") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;
                    CustomValidator cv1 = row.FindControl("CustomValidator1") as CustomValidator;

                    lblTotalQty.Visible = true;
                    lblFTQRemark.Visible = true;
                    lnkEdit.Visible = true;


                    lnkUpdate.Visible = false;
                    lnkReset.Visible = false;
                    rfv.Enabled = false;
                    rfv2.Enabled = false;
                    rev1.Enabled = false;
                    cv1.Enabled = false;
                    txtTotalQty.Visible = false;
                    txtTotalQtyRemark.Visible = false;
                }

            }
            if (e.CommandName == "RecordUpdate")
            {
                if (Page.IsValid)
                {
                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                        lblMsg.Text = string.Empty;
                        GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                        Label lblTotalQty = (Label)row.FindControl("lblTotalQty");
                        TextBox txtTotalQty = (TextBox)row.FindControl("txtTotalQty");
                        TextBox txtTotalQtyRemark = (TextBox)row.FindControl("txtTotalQtyRemark");
                        LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                        LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                        LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");


                        ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                         new string[] { "flag", "MilkOrProductDemandChildId", "TotalQty", "PreviousTotalQty", "FinalTotalQtyRemark", "CreatedBy", "CreatedByIP" },
                         new string[] { "14", e.CommandArgument.ToString(), txtTotalQty.Text.Trim(), lblTotalQty.Text, txtTotalQtyRemark.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");

                        if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            GetItemDetailByDemandID();
                            GetBoothandOrganizationDetails();
                        }
                        else
                        {
                            string error = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 4:" + error);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5: " + ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }

    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
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
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 6 : " + ex.Message.ToString());
        }
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        GridViewRow gvr = (GridViewRow)cv.NamingContainer;
        TextBox txtTotalQty = (TextBox)gvr.FindControl("txtTotalQty");
        Label lblSupplyTotalQty = (Label)gvr.FindControl("lblSupplyTotalQty");
        if (Convert.ToInt32(txtTotalQty.Text) == Convert.ToInt32(lblSupplyTotalQty.Text))
        {
            args.IsValid = true;
        }
        else
            args.IsValid = false;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
    }
}