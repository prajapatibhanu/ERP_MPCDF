using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_ApprovalofSupplyatDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds4, ds5, ds6, ds7, ds8, ds9 = new DataSet();
    double sum1 = 0;
    int sum11 = 0;
    int dsum11 = 0;
    int csum11 = 0;
    int cellIndexbooth = 4;
    int i_Qty = 0, i_NaQty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    string success = "", error = "";
    int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["DDate"].ToString() != "" && Request.QueryString["DShift"].ToString() != "" && Request.QueryString["DRDIType"].ToString() != "" && Request.QueryString["DLocation"].ToString() != "")
                {
                    GetBoothandOrganizationDetails();
                }
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
            string retailertypeid = "";
            spanRDIName.InnerHtml = objdb.Decrypt(Request.QueryString["D_RDIName"].ToString());
            SpanDate.InnerHtml = objdb.Decrypt(Request.QueryString["DDate"].ToString());
            spanShift.InnerHtml = objdb.Decrypt(Request.QueryString["DShiftName"].ToString());
            spanCategory.InnerHtml = objdb.Decrypt(Request.QueryString["DCategoryName"].ToString());

            if (objdb.Decrypt(Request.QueryString["D_OrganizationId"].ToString()) != "0")
            {
                retailertypeid = objdb.GetInstRetailerTypeId();
            }
            else
            {
                retailertypeid = "0";
            }

            DateTime odate = DateTime.ParseExact(objdb.Decrypt(Request.QueryString["DDate"].ToString()), "dd/MM/yyyy", culture);
            string orderedate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDmandAprovedListForSuply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date",
                                     "RouteId", "DistributorId", "OrganizationId","RetailerTypeId","AreaId" },
                       new string[] { "4", objdb.Office_ID(), objdb.Decrypt(Request.QueryString["DShift"].ToString()),
                           objdb.GetMilkCatId(), orderedate
                           , objdb.Decrypt(Request.QueryString["D_RouteId"].ToString()), 
                           objdb.Decrypt(Request.QueryString["D_DistributorId"].ToString()),
                           objdb.Decrypt(Request.QueryString["D_OrganizationId"].ToString()),retailertypeid,
                           objdb.Decrypt(Request.QueryString["DLocation"].ToString()) }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                btnSubmit.Visible = true;
                DataTable dt, dt1 = new DataTable();
                dt = ds1.Tables[0];
                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName")
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

                GridView1.FooterRow.Cells[3].Text = "Total Demand";
                GridView1.FooterRow.Cells[3].Font.Bold = true;

                DataTable dtcrate = new DataTable();// create dt for Crate total
                DataRow drcrate;

                dtcrate.Columns.Add("ItemName", typeof(string));
                dtcrate.Columns.Add("CrateQty", typeof(int));
                dtcrate.Columns.Add("CratePacketQty", typeof(String));
                drcrate = dtcrate.NewRow();
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName")
                    {

                        sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndexbooth].Text = sum11.ToString();
                        GridView1.FooterRow.Cells[cellIndexbooth].Font.Bold = true;
                        cellIndexbooth = cellIndexbooth + 1;

                        if (objdb.GetMilkCatId() == "3")
                        {
                            ds8 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                                new string[] { "Flag", "ItemCat_id", "Itemname", "Office_ID", "EffectiveDate" },
                                new string[] { "7", objdb.GetMilkCatId(), column.ToString(), objdb.Office_ID(), orderedate }, "dataset");

                            if (ds8.Tables[0].Rows.Count > 0)
                            {
                                i_Qty = Convert.ToInt32(ds8.Tables[0].Rows[0]["ItemQtyByCarriageMode"].ToString());
                                i_NaQty = Convert.ToInt32(ds8.Tables[0].Rows[0]["NotIssueQty"].ToString());
                            }
                            else
                            {
                                i_Qty = 0;
                                i_NaQty = 0;
                            }
                            if (ds8 != null) { ds8.Dispose(); }

                            if (i_Qty != 0 && i_NaQty != 0)
                            {
                                int Actualcrate = 0, remenderCrate = 0, FinalCrate = 0;
                                string Extrapacket = "0";
                                Actualcrate = sum11 / i_Qty;
                                remenderCrate = sum11 % i_Qty;

                                if (remenderCrate <= i_NaQty)
                                {
                                    FinalCrate = Actualcrate;
                                    Extrapacket = remenderCrate.ToString();

                                }
                                else
                                {
                                    FinalCrate = Actualcrate + 1;
                                    Extrapacket = "-" + (i_Qty - remenderCrate);
                                }
                                drcrate[0] = column.ToString();
                                drcrate[1] = FinalCrate;
                                drcrate[2] = Extrapacket;
                            }
                            else
                            {
                                drcrate[0] = column.ToString();
                                drcrate[1] = "0";
                                drcrate[2] = "0";
                            }

                            dtcrate.Rows.Add(drcrate.ItemArray);

                            //  end code for crate calculation
                        }
                    }
                }
              
                if (objdb.GetMilkCatId() == "3")
                {
                    //  sum and bind data in string  builder
                    int cratetotal = Convert.ToInt32(dtcrate.Compute("SUM([" + "CrateQty" + "])", string.Empty));
                    int Rowcount = dtcrate.Rows.Count;
                    StringBuilder sbtable = new StringBuilder();
                    sbtable.Append("<div style='margin-left:450px; margin-right:450px; margin-bottom:10px; text-align:center;'>CRATE SUMMARY</div>");
                    sbtable.Append("<table class='table table-striped table-bordered' style='width:100%; height:100%'>");
                    sbtable.Append("<tr>");
                    sbtable.Append("<td style='text-align:right;'>");
                    sbtable.Append("</td>");

                    for (int i = 0; i < Rowcount; i++)
                    {
                        sbtable.Append("<td>" + dtcrate.Rows[i]["ItemName"].ToString() + "");



                    }
                    sbtable.Append("<td style='text-align:center;' colspan='" + (Rowcount + 1) + "'>TOTAL");
                    sbtable.Append("</td>");
                    sbtable.Append("</tr>");

                    sbtable.Append("<tr>");
                    sbtable.Append("<td style='text-align:right;'> CRATE DETAILS");
                    sbtable.Append("</td>");
                    for (int i = 0; i < Rowcount; i++)
                    {
                        sbtable.Append("<td>" + dtcrate.Rows[i]["CrateQty"].ToString() + "");


                    }
                    sbtable.Append("<td style='text-align:center;' colspan='" + (Rowcount + 1) + "'>" + cratetotal.ToString());

                    sbtable.Append("</tr>");
                    sbtable.Append("<tr>");
                    sbtable.Append("<td style='text-align:right;'> EXTRA PKT(+/-)");
                    sbtable.Append("</td>");

                    for (int i = 0; i < Rowcount; i++)
                    {
                        sbtable.Append("<td>" + dtcrate.Rows[i]["CratePacketQty"].ToString() + "");


                    }
                    sbtable.Append("<td style='text-align:center;' colspan='" + (Rowcount + 1) + "'>");
                    sbtable.Append("</tr>");
                    sbtable.Append("</table>");
                    divtable.InnerHtml = sbtable.ToString();
                    if (dtcrate != null) { dtcrate.Dispose(); }
                    // end  of crate binding and
                }
              
                int rowcount = GridView1.FooterRow.Cells.Count - 3;
                GridView1.FooterRow.Cells[rowcount].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 2].Visible = false;
                ViewState["RoutOrDisOrInstWiseTable"] = "";
                if (dt1 != null) { dt1.Dispose(); }
            }
            else
            {
                btnSubmit.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                divtable.InnerHtml = "";
                divtable.Visible = false;
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

                string demandid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {

                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
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
    private void ChallanCreated()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    string demandid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    if (chkSelect.Checked == true)
                    {
                        ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                               new string[] { "flag", "MilkOrProductDemandId", "CreatedBy", "CreatedByIP" },
                               new string[] { "4", demandid, objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");
                        if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            success = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();

                            if (i == 0)
                            {
                                i = 1;
                            }
                            else
                            {
                                i = i + 1;
                            }
                        }
                        else
                        {
                            i = 0;
                            error = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        }
                    }

                }
                if (i > 0)
                {
                    GetBoothandOrganizationDetails();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 3:" + error);
                }
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 4 : " + ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ChallanCreated();
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
                modaldate.InnerHtml = objdb.Decrypt(Request.QueryString["DDate"].ToString());
                modalshift.InnerHtml = objdb.Decrypt(Request.QueryString["DShiftName"].ToString());

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
                  new string[] { "2", ViewState["rowid"].ToString(), objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5 : " + ex.Message.ToString());
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
                    Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
                    Label lblRemarkAtSupply = (Label)row.FindControl("lblRemarkAtSupply");
                    TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
                    TextBox txtRemarkAtSupply = (TextBox)row.FindControl("txtRemarkAtSupply");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

                    foreach (GridViewRow gvRow in GridView4.Rows)
                    {
                        Label HlblSupplyTotalQty = (Label)gvRow.FindControl("lblSupplyTotalQty");
                        Label HlblRemarkAtSupply = (Label)gvRow.FindControl("lblRemarkAtSupply");
                        TextBox HtxtSupplyTotalQty = (TextBox)gvRow.FindControl("txtSupplyTotalQty");
                        TextBox HtxtRemarkAtSupply = (TextBox)gvRow.FindControl("txtRemarkAtSupply");
                        LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
                        LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
                        LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
                        RequiredFieldValidator Hrfv = gvRow.FindControl("rfv1") as RequiredFieldValidator;
                        RegularExpressionValidator Hrev1 = gvRow.FindControl("rev1") as RegularExpressionValidator;
                        HlblSupplyTotalQty.Visible = true;
                        HlblRemarkAtSupply.Visible = true;
                        HtxtSupplyTotalQty.Visible = false;
                        HtxtRemarkAtSupply.Visible = false;
                        HlnkEdit.Visible = true;
                        HlnkUpdate.Visible = false;
                        HlnkReset.Visible = false;
                        Hrfv.Enabled = false;
                        Hrev1.Enabled = false;

                    }
                    txtSupplyTotalQty.Text = "";
                    txtSupplyTotalQty.Text = lblSupplyTotalQty.Text;
                    lblSupplyTotalQty.Visible = false;
                    lblRemarkAtSupply.Visible = false;
                    lnkEdit.Visible = false;

                    lnkUpdate.Visible = true;
                    lnkReset.Visible = true;
                    rfv.Enabled = true;
                    rev1.Enabled = true;
                    txtSupplyTotalQty.Visible = true;
                    txtRemarkAtSupply.Visible = true;
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
                    Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
                    Label lblRemarkAtSupply = (Label)row.FindControl("lblRemarkAtSupply");
                    TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
                    TextBox txtRemarkAtSupply = (TextBox)row.FindControl("txtRemarkAtSupply");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

                    lblModalMsg.Text = string.Empty;

                    lblSupplyTotalQty.Visible = true;
                    lblRemarkAtSupply.Visible = true;
                    lnkEdit.Visible = true;


                    lnkUpdate.Visible = false;
                    lnkReset.Visible = false;
                    rfv.Enabled = false;
                    rev1.Enabled = false;
                    txtSupplyTotalQty.Visible = false;
                    txtRemarkAtSupply.Visible = false;
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
                        Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
                        TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
                        TextBox txtRemarkAtSupply = (TextBox)row.FindControl("txtRemarkAtSupply");
                        LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                        LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                        LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");

                        if (txtSupplyTotalQty.Text == "0" && txtRemarkAtSupply.Text == "")
                        {
                            lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Remark");
                            return;
                        }
                        else
                        {
                            ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                             new string[] { "flag", "MilkOrProductDemandChildId", "SupplTotalQty", "RemarkAtSupply", "CreatedBy", "CreatedByIP" },
                             new string[] { "3", e.CommandArgument.ToString(), txtSupplyTotalQty.Text.Trim(), txtRemarkAtSupply.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");

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
                                lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 6: " + ex.Message.ToString());
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
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7 : " + ex.Message.ToString());
        }
    }
    protected void lnkbtnback_Click(object sender, EventArgs e)
    {

        Response.Redirect("SupplyListRouteOrDistWise.aspx?DDate=" + Request.QueryString["DDate"] 
            + "&DShift=" + Request.QueryString["DShift"]
           + "&DCategory=" + Request.QueryString["DCategory"]
           + "&DRDIType=" + Request.QueryString["DRDIType"]
              + "&DLocation=" + Request.QueryString["DLocation"]);
    }
}