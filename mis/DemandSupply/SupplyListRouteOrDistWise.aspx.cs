using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;

public partial class mis_DemandSupply_SupplyListRouteOrDistWise : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3, ds4, ds5, ds6, ds8, ds9 = new DataSet();
    int cellIndexbooth = 4;
    int i_Qty = 0, i_NaQty = 0;
    int sum11 = 0, i = 0;
    string success = "", error = "";
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtOrderDate.Text = Date;
                txtOrderDate.Attributes.Add("readonly", "true");
                GetLocation();
                GetShift();
                GetRoute();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void GetLocation()
    {
        try
        {
            ddlLocation.DataTextField = "AreaName";
            ddlLocation.DataValueField = "AreaId";
            ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocation.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetShift()
    {
        try
        {
            ddlShift.DataTextField = "ShiftName";
            ddlShift.DataValueField = "Shift_id";
            ddlShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlShift.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    private void GetRoute()
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductDmandAprovedListForSuply",
                  new string[] { "flag", "AreaId", "Shift_id", "ItemCat_id", "Demand_Date", "Office_ID" },
                    new string[] { "5", ddlLocation.SelectedValue, ddlShift.SelectedValue, objdb.GetMilkCatId(), orderedate.ToString(), objdb.Office_ID() }, "dataset");
            ddlRoute.Items.Clear();
            if (ds6.Tables[0].Rows.Count > 0)
            {
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataSource = ds6.Tables[0];
                ddlRoute.DataBind();
            }
            else
            {
                ddlRoute.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Route ", ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }
    //private void GetApprovedDemandRouteWise()
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
    //        string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //        ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDmandAprovedListForSuply",
    //                 new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date", "AreaId" },
    //                   new string[] { "1", objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetMilkCatId(), orderedate, ddlLocation.SelectedValue }, "dataset");

    //        if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
    //        {
    //            pnlData.Visible = true;
    //            GridViewRoutwise.DataSource = ds1.Tables[0];
    //            GridViewRoutwise.DataBind();
    //        }
    //        else
    //        {
    //            pnlData.Visible = false;
    //            GridViewRoutwise.DataSource = null;
    //            GridViewRoutwise.DataBind();
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Routewise Approved demand ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds1 != null) { ds1.Dispose(); }
    //    }
    //}
    #endregion========================================================



    //protected void GridViewRoutwise_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Cells[2].Visible = false;
    //        e.Row.Cells[3].Visible = false;

    //    }
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        e.Row.Cells[2].Visible = false;
    //        e.Row.Cells[3].Visible = false;
    //    }
    //}
    //protected void GridViewRoutwise_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "RoutwiseBooth")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                lblMsg.Text = string.Empty;
    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
    //                LinkButton lnkbtnRoute = (LinkButton)row.FindControl("lnkbtnRoute");
    //                lblroutename.Text = lnkbtnRoute.Text;
    //                ViewState["RouteId"] = e.CommandArgument.ToString();
    //                GetBoothandOrganizationDetails(Convert.ToInt32(e.CommandArgument));
    //                pnlData.Visible = false;
    //                pnlparlourdetails.Visible = true;


    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
    //    }
    //}


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int routeid = Convert.ToInt32(ddlRoute.SelectedValue);
            lblroutename.Text = ddlRoute.SelectedItem.Text;
            GetBoothandOrganizationDetails(routeid);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        // pnlData.Visible = false;

        //GridViewRoutwise.DataSource = null;
        //GridViewRoutwise.DataBind();
        GridView1.DataSource = null;
        GridView1.DataBind();
        divtable.InnerHtml = "";
        pnlparlourdetails.Visible = false;
    }

    #region======================Code for Final approved====================

    private void GetBoothandOrganizationDetails(int routeid)
    {
        try
        {
            lblMsg.Text = "";
            DateTime odate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDmandAprovedListForSuply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date",
                                     "RouteId", "DistributorId", "OrganizationId","RetailerTypeId","AreaId" },
                       new string[] { "4", objdb.Office_ID(), ddlShift.SelectedValue,objdb.GetMilkCatId(), orderedate,
                                    routeid.ToString(),"0","0","0",ddlLocation.SelectedValue}, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                pnlparlourdetails.Visible = true;

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
                pnlparlourdetails.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                divtable.InnerHtml = "";

            }
        }
        catch (Exception ex)
        {
            lblReportMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Retailer Details ", ex.Message.ToString());
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
            lblReportMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
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
                DataTable dtInsertChild = new DataTable();
                DataRow drIC;
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];

                dtInsertChild.Columns.Add("MilkOrProductDemandId", typeof(int));
                dtInsertChild.Columns.Add("Demand_UpdatedBy", typeof(int));
                dtInsertChild.Columns.Add("Demand_UpdatedByIP", typeof(string));
                drIC = dtInsertChild.NewRow();
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    string demandid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    if (chkSelect.Checked == true)
                    {
                        drIC[0] = demandid;
                        drIC[1] = objdb.createdBy();
                        drIC[2] = IPAddress;


                        dtInsertChild.Rows.Add(drIC.ItemArray);
                        //ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                        //       new string[] { "flag", "MilkOrProductDemandId", "CreatedBy", "CreatedByIP" },
                        //       new string[] { "4", demandid, objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");
                        //if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        //{
                        //    success = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        //    if (i == 0)
                        //    {
                        //        i = 1;
                        //    }
                        //    else
                        //    {
                        //        i = i + 1;
                        //    }
                        //}
                        //else
                        //{
                        //    i = 0;
                        //    error = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        //}
                    }

                }
                if (dtInsertChild.Rows.Count > 0)
                {
                    ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand_UpdateStatusOrChallan",
                                                 new string[] { "flag" },
                                                 new string[] { "2" }, "type_Trn_MilkOrProductDemand_ChallanCreation", dtInsertChild, "TableSave");

                    if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        dtInsertChild.Dispose();
                        GetRoute();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        pnlparlourdetails.Visible = false;
                    }
                    else
                    {
                        string msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error:" + msg);
                    }
                }
                //if (i > 0)
                //{
                //    GetBoothandOrganizationDetails(Convert.ToInt32(ViewState["RouteId"]));
                //    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                //}
                //else
                //{
                //    lblReportMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 3:" + error);
                //}
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
                modaldate.InnerHtml = txtOrderDate.Text;
                modalshift.InnerHtml = ddlShift.SelectedItem.Text;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);


            }
        }
    }
    private void GetItemDetailByDemandID()
    {
        try
        {
            lblMilkOrProductDemandId.Text = ViewState["rowid"].ToString();
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
            lblReportMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5 : " + ex.Message.ToString());
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
                    // LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

                    Label lblLeakageQty = (Label)row.FindControl("lblLeakageQty");
                    TextBox txtLeakageQty = (TextBox)row.FindControl("txtLeakageQty");
                    RegularExpressionValidator releakqty = row.FindControl("releakqty") as RegularExpressionValidator;


                    foreach (GridViewRow gvRow in GridView4.Rows)
                    {
                        Label HlblSupplyTotalQty = (Label)gvRow.FindControl("lblSupplyTotalQty");
                        Label HlblRemarkAtSupply = (Label)gvRow.FindControl("lblRemarkAtSupply");
                        TextBox HtxtSupplyTotalQty = (TextBox)gvRow.FindControl("txtSupplyTotalQty");
                        TextBox HtxtRemarkAtSupply = (TextBox)gvRow.FindControl("txtRemarkAtSupply");
                        LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
                        LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
                        //LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
                        RequiredFieldValidator Hrfv = gvRow.FindControl("rfv1") as RequiredFieldValidator;
                        RegularExpressionValidator Hrev1 = gvRow.FindControl("rev1") as RegularExpressionValidator;
                        if (objdb.Office_ID() == "5")
                        {
                            Label HlblLeakageQty = (Label)row.FindControl("lblLeakageQty");
                            TextBox HtxtLeakageQty = (TextBox)row.FindControl("txtLeakageQty");
                            RegularExpressionValidator Hreleakqty = row.FindControl("releakqty") as RegularExpressionValidator;

                            HlblLeakageQty.Visible = true;
                            HtxtLeakageQty.Visible = false;
                            Hreleakqty.Enabled = false;
                        }
                        HlblSupplyTotalQty.Visible = true;
                        HlblRemarkAtSupply.Visible = true;
                        HtxtSupplyTotalQty.Visible = false;
                        HtxtRemarkAtSupply.Visible = false;
                        HlnkEdit.Visible = true;
                        HlnkUpdate.Visible = false;
                        //HlnkReset.Visible = false;
                        Hrfv.Enabled = false;
                        Hrev1.Enabled = false;

                    }
                    txtSupplyTotalQty.Text = "";
                    txtSupplyTotalQty.Text = lblSupplyTotalQty.Text;
                    lblSupplyTotalQty.Visible = false;
                    if (objdb.Office_ID() == "5")
                    {
                        txtLeakageQty.Text = string.Empty;
                        txtLeakageQty.Text = lblLeakageQty.Text;
                        lblLeakageQty.Visible = false;
                    }
                    lblRemarkAtSupply.Visible = false;
                    lnkEdit.Visible = false;

                    lnkUpdate.Visible = true;
                    // lnkReset.Visible = true;
                    rfv.Enabled = true;
                    rev1.Enabled = true;
                    releakqty.Enabled = true;
                    txtSupplyTotalQty.Visible = true;
                    txtRemarkAtSupply.Visible = true;
                    txtLeakageQty.Visible = true;
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
                    // LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;
                    Label lblLeakageQty = (Label)row.FindControl("lblLeakageQty");
                    TextBox txtLeakageQty = (TextBox)row.FindControl("txtLeakageQty");
                    RegularExpressionValidator releakqty = row.FindControl("releakqty") as RegularExpressionValidator;



                    lblModalMsg.Text = string.Empty;

                    lblSupplyTotalQty.Visible = true;
                    lblLeakageQty.Visible = true;
                    lblRemarkAtSupply.Visible = true;
                    lnkEdit.Visible = true;


                    lnkUpdate.Visible = false;
                    //lnkReset.Visible = false;
                    rfv.Enabled = false;
                    rev1.Enabled = false;
                    releakqty.Enabled = false;
                    txtSupplyTotalQty.Visible = false;
                    txtRemarkAtSupply.Visible = false;
                    txtLeakageQty.Visible = false;
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
                        TextBox txtLeakageQty = (TextBox)row.FindControl("txtLeakageQty");
                        TextBox txtRemarkAtSupply = (TextBox)row.FindControl("txtRemarkAtSupply");
                        LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                        LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                        //LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");

                        if (txtSupplyTotalQty.Text == "0" && txtRemarkAtSupply.Text == "")
                        {
                            lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Remark");
                            return;
                        }
                        else
                        {
                            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                            string tmpleakageqty = "";
                            ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                             new string[] { "flag", "MilkOrProductDemandChildId", "SupplTotalQty", "LeakageQty", "RemarkAtSupply", "CreatedBy", "CreatedByIP" },
                             new string[] { "3", e.CommandArgument.ToString(), txtSupplyTotalQty.Text.Trim(), txtLeakageQty.Text.Trim(), txtRemarkAtSupply.Text.Trim(), objdb.createdBy(), IPAddress + ":" + objdb.GetMACAddress() }, "TableSave");

                            if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                GetItemDetailByDemandID();
                                GetBoothandOrganizationDetails(Convert.ToInt32(ddlRoute.SelectedValue));
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
                if (objdb.Office_ID() == "5")
                {
                    GridView4.HeaderRow.Cells[5].Visible = true;
                    e.Row.Cells[5].Visible = true;
                }
                else
                {
                    GridView4.HeaderRow.Cells[5].Visible = false;
                    e.Row.Cells[5].Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7 : " + ex.Message.ToString());
        }
    }
    #endregion==============================================================
    protected void txtOrderDate_TextChanged(object sender, EventArgs e)
    {
        GetRoute();
    }
    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRoute();
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRoute();
    }
    protected void chkedit_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            int checkcount = 0;


            lblMsg.Text = string.Empty;
            lblModalMsg.Text = string.Empty;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

            foreach (GridViewRow row in GridView4.Rows)
            {
                CheckBox chkedit = (CheckBox)row.FindControl("chkedit");

                Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
                Label lblRemarkAtSupply = (Label)row.FindControl("lblRemarkAtSupply");
                TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
                TextBox txtRemarkAtSupply = (TextBox)row.FindControl("txtRemarkAtSupply");
                LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                // LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                //LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

                Label lblLeakageQty = (Label)row.FindControl("lblLeakageQty");
                TextBox txtLeakageQty = (TextBox)row.FindControl("txtLeakageQty");
                RegularExpressionValidator releakqty = row.FindControl("releakqty") as RegularExpressionValidator;



                Label HlblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
                Label HlblRemarkAtSupply = (Label)row.FindControl("lblRemarkAtSupply");
                TextBox HtxtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
                TextBox HtxtRemarkAtSupply = (TextBox)row.FindControl("txtRemarkAtSupply");
                LinkButton HlnkEdit = (LinkButton)row.FindControl("lnkEdit");
                //  LinkButton HlnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                // LinkButton HlnkReset = (LinkButton)row.FindControl("lnkReset");
                RequiredFieldValidator Hrfv = row.FindControl("rfv1") as RequiredFieldValidator;
                RegularExpressionValidator Hrev1 = row.FindControl("rev1") as RegularExpressionValidator;
                if (chkedit.Checked == true)
                {
                    if (objdb.Office_ID() == "5")
                    {
                        Label HlblLeakageQty = (Label)row.FindControl("lblLeakageQty");
                        TextBox HtxtLeakageQty = (TextBox)row.FindControl("txtLeakageQty");
                        RegularExpressionValidator Hreleakqty = row.FindControl("releakqty") as RegularExpressionValidator;

                        HlblLeakageQty.Visible = true;
                        HtxtLeakageQty.Visible = false;
                        Hreleakqty.Enabled = false;
                    }
                    HlblSupplyTotalQty.Visible = true;
                    HlblRemarkAtSupply.Visible = true;
                    HtxtSupplyTotalQty.Visible = false;
                    HtxtRemarkAtSupply.Visible = false;
                    HlnkEdit.Visible = true;
                    // HlnkUpdate.Visible = false;
                    //HlnkReset.Visible = false;
                    Hrfv.Enabled = false;
                    Hrev1.Enabled = false;


                    txtSupplyTotalQty.Text = "";
                    txtSupplyTotalQty.Text = lblSupplyTotalQty.Text;
                    lblSupplyTotalQty.Visible = false;
                    if (objdb.Office_ID() == "5")
                    {
                        txtLeakageQty.Text = string.Empty;
                        txtLeakageQty.Text = lblLeakageQty.Text;
                        lblLeakageQty.Visible = false;
                    }
                    lblRemarkAtSupply.Visible = false;
                    lnkEdit.Visible = false;

                    // lnkUpdate.Visible = true;
                    // lnkReset.Visible = true;
                    rfv.Enabled = true;
                    rev1.Enabled = true;
                    releakqty.Enabled = true;
                    txtSupplyTotalQty.Visible = true;
                    txtRemarkAtSupply.Visible = true;
                    txtLeakageQty.Visible = true;
                    checkcount += 1;
                }
                else
                {
                    HlblSupplyTotalQty.Visible = false;
                    HlblRemarkAtSupply.Visible = false;
                    HtxtSupplyTotalQty.Visible = true;
                    HtxtRemarkAtSupply.Visible = true;
                    HlnkEdit.Visible = false;
                    // HlnkUpdate.Visible = false;
                    //HlnkReset.Visible = false;
                    Hrfv.Enabled = true;
                    Hrev1.Enabled = true;


                   // txtSupplyTotalQty.Text = "";
                   // txtSupplyTotalQty.Text = lblSupplyTotalQty.Text;
                    lblSupplyTotalQty.Visible = true;
                    lblRemarkAtSupply.Visible = true;
                    lnkEdit.Visible = true;

                    // lnkUpdate.Visible = true;
                    // lnkReset.Visible = true;
                    rfv.Enabled = false;
                    rev1.Enabled = false;
                    releakqty.Enabled = false;
                    txtSupplyTotalQty.Visible = false;
                    txtRemarkAtSupply.Visible = false;
                    txtLeakageQty.Visible = false;
                   // checkcount += 1;
                }

            }
            lblcheckcount.Text = checkcount.ToString();
            if (checkcount > 0)
            {
                lnkupdate.Visible = true;
                //lnkFinalSubmit.Visible = false;
            }
            else
            {
                lnkupdate.Visible = false;
                //lnkFinalSubmit.Visible = true;
            }


            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "select  : " + ex.Message.ToString());
        }
    }
    protected void lnkupdate_Click(object sender, EventArgs e)
    {
        try
        {


            if (Page.IsValid)
            {
                int checkcount = 0;
                DateTime odate = DateTime.ParseExact(modaldate.InnerHtml, "dd/MM/yyyy", culture);
                string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                lblMsg.Text = "";
                DataTable dtInsertChild = new DataTable();
                DataRow drIC;

                dtInsertChild.Columns.Add("MilkOrProductDemandChildId", typeof(int));
                //dtInsertChild.Columns.Add("TotalQty", typeof(int));
                dtInsertChild.Columns.Add("SupplyTotalQty", typeof(int));
                dtInsertChild.Columns.Add("LeakageQty", typeof(int));
                dtInsertChild.Columns.Add("RemarkAtSupply", typeof(string));


                drIC = dtInsertChild.NewRow();
                foreach (GridViewRow row in GridView4.Rows)
                {

                    CheckBox chkedit = (CheckBox)row.FindControl("chkedit");
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    lblMsg.Text = string.Empty;
                    // GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblMilkOrProductDemandChildId = (Label)row.FindControl("lblMilkOrProductDemandChildId");
                    Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
                    TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
                    TextBox txtLeakageQty = (TextBox)row.FindControl("txtLeakageQty");
                    TextBox txtRemarkAtSupply = (TextBox)row.FindControl("txtRemarkAtSupply");
                    // LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    //  LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    if (chkedit.Checked == true)
                    {
						if (int.Parse(lblSupplyTotalQty.Text)>0 && txtSupplyTotalQty.Text == "0" && txtRemarkAtSupply.Text == "")
                         {
                        // if (txtSupplyTotalQty.Text == "0" && txtRemarkAtSupply.Text == "")
                        // {
                            lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Remark");
                            return;
                        }
                        if (txtLeakageQty.Text == "")
                        {
                            txtLeakageQty.Text = "0";
                        }
                        drIC[0] = lblMilkOrProductDemandChildId.Text;
                        // drIC[1] = txtSupplyTotalQty.Text;
                        drIC[1] = txtSupplyTotalQty.Text;
                        drIC[2] = txtLeakageQty.Text;
                        drIC[3] = txtRemarkAtSupply.Text;


                        dtInsertChild.Rows.Add(drIC.ItemArray);
                        checkcount += 1;
                    }
                }
                //else
                //{
                if (checkcount > 0)
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    string tmpleakageqty = "";
                    ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                     new string[] { "flag", "MilkOrProductDemandId", "CreatedBy", "CreatedByIP" },
                     new string[] { "21", lblMilkOrProductDemandId.Text, objdb.createdBy(), IPAddress + ":" + objdb.GetMACAddress() }, "type_Trn_MilkOrProductDemandChildUpdate", dtInsertChild, "TableSave");

                    if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        GetItemDetailByDemandID();
                        GetBoothandOrganizationDetails(Convert.ToInt32(ddlRoute.SelectedValue));
                    }
                    else
                    {
                        string error = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                }

            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "replace save : " + ex.Message.ToString());
        }
    }
}