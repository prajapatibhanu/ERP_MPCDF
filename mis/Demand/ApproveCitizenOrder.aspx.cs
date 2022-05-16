using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Demand_ApproveCitizenOrder : System.Web.UI.Page
{
    DataSet ds, ds1, ds5, ds6 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("en-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCategory();

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

    protected void GetCategory()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds;
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("All", "0"));
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





    protected void GetInvoice()
    {
        try
        {

            //DateTime date3 = DateTime.ParseExact(txtDel.Text, "dd/MM/yyyy", culture);
            //DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            //string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            //string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                                new string[] { "flag", "Delivery_Date", "ItemCat_id", "Office_ID" },
                                new string[] { "10", odat.ToString(), ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {

                //  pnldata.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                // GetDatatableHeaderDesign();
            }
            else
            {
                //  pnldata.Visible = true;
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

    protected void SetStatus()
    {
        try
        {

            ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                               new string[] { "flag" },
                               new string[] { "12" }, "TableSave");

            if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
            {
                string success = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                // GetOrderDetails();
            }
            else
            {
                string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }

    protected void SetStatus1()
    {
        try
        {

            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                               new string[] { "flag" },
                               new string[] { "13" }, "TableSave");

            if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
            {
                string success = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                // GetOrderDetails();
            }
            else
            {
                string error = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
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

                //  pnldata.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                // GetDatatableHeaderDesign();
            }
            else
            {
                //  pnldata.Visible = true;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        GetInvoice();

    }
    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetCategory();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        //  pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        // Clear();
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
                    // Label lblInVoiceNo = (Label)row.FindControl("lblInVoiceNo");

                    modalBoothName.InnerHtml = lblCitizenName.Text;
                    //modaldate.InnerHtml = lblMobileNo.Text;
                    //modelShift.InnerHtml = lblDelivery_Date.Text;

                    GridView4.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                                         new string[] { "flag", "MobileNo", "Office_ID" },
                                         new string[] { "7", e.CommandArgument.ToString(), objdb.Office_ID() }, "dataset");
                    GridView4.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                }
            }
        }
        catch (Exception ex1)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex1.Message.ToString());
        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string demandid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                //GridView gridView2 = e.Row.FindControl("GridView2") as GridView;

                //ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                // new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                //   new string[] { "2", demandid.ToString(), Session["DCategory"].ToString(), objdb.Office_ID() }, "dataset");
                //gridView2.DataSource = ds2.Tables[0];
                //gridView2.DataBind();


                //e.Row.Cells[4].Visible = false;
                //e.Row.Cells[5].Visible = false;
                //e.Row.Cells[6].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.Cells[4].Visible = false;
                //e.Row.Cells[5].Visible = false;
                //e.Row.Cells[6].Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {

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
                    Label lblItemQty = (Label)row.FindControl("lblItemQty");
                    TextBox txtItemQty = (TextBox)row.FindControl("txtItemQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

                    foreach (GridViewRow gvRow in GridView4.Rows)
                    {
                        Label HlblItemQty = (Label)gvRow.FindControl("lblItemQty");
                        TextBox HtxtItemQty = (TextBox)gvRow.FindControl("txtItemQty");
                        LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
                        LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
                        LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
                        RequiredFieldValidator Hrfv = gvRow.FindControl("rfv1") as RequiredFieldValidator;
                        RegularExpressionValidator Hrev1 = gvRow.FindControl("rev1") as RegularExpressionValidator;
                        HlblItemQty.Visible = true;
                        HtxtItemQty.Visible = false;
                        HlnkEdit.Visible = true;
                        HlnkUpdate.Visible = false;
                        HlnkReset.Visible = false;
                        Hrfv.Enabled = false;
                        Hrev1.Enabled = false;

                    }

                    lblItemQty.Visible = false;
                    lnkEdit.Visible = false;

                    lnkUpdate.Visible = true;
                    lnkReset.Visible = true;
                    rfv.Enabled = true;
                    rev1.Enabled = true;
                    txtItemQty.Visible = true;
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
                    Label lblItemQty = (Label)row.FindControl("lblItemQty");
                    Label lblAdvCard = (Label)row.FindControl("lblAdvCard");
                    TextBox txtItemQty = (TextBox)row.FindControl("txtItemQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

                    lblItemQty.Visible = true;
                    lblAdvCard.Visible = true;
                    lnkEdit.Visible = true;


                    lnkUpdate.Visible = false;
                    lnkReset.Visible = false;
                    rfv.Enabled = false;
                    rev1.Enabled = false;
                    txtItemQty.Visible = false;
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
                        Label lblItemQty = (Label)row.FindControl("lblItemQty");
                        Label lblAdvCard = (Label)row.FindControl("lblAdvCard");
                        TextBox txtItemQty = (TextBox)row.FindControl("txtItemQty");
                        LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                        LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                        LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                        int totalqty = 0;
                        totalqty = Convert.ToInt32(txtItemQty.Text) + Convert.ToInt32(lblAdvCard.Text);


                        ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                         new string[] { "flag", "MilkOrProductDemandChildId", "ItemQty", "TotalQty", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                         new string[] { "5", e.CommandArgument.ToString(), txtItemQty.Text.Trim(), totalqty.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Item Qty Updated from Modify MilkorProduct Page(Parlour) " }, "TableSave");

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            //GetItemDetailByDemandID();
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                    }
                }
            }
            if (e.CommandName == "RecordDelete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                lblMsg.Text = string.Empty;
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                            new string[] { "flag", "MilkOrProductDemandChildId", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                            new string[] { "7", e.CommandArgument.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "ItemQty Deleted from MilkorProduct Page(Parlour)" }, "TableSave");

                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    // GetItemDetailByDemandID();
                }
                else
                {
                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                }
            }

        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
    //protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            Label lblItemCatName = e.Row.FindControl("lblItemCatName") as Label;
    //            Label lblDemandStatus = e.Row.FindControl("lblDemandStatus") as Label;

    //            if (lblDemandStatus.Text == "2")
    //            {
    //                e.Row.Cells[6].Visible = false;
    //                GridView4.HeaderRow.Cells[6].Visible = false;
    //                btnApproved.Visible = false;
    //                btnReject.Visible = false;
    //            }
    //            else if (lblDemandStatus.Text == "3")
    //            {
    //                e.Row.Cells[6].Visible = false;
    //                GridView4.HeaderRow.Cells[6].Visible = false;
    //                btnApproved.Visible = false;
    //                btnReject.Visible = false;
    //            }
    //            else
    //            {
    //                GridView4.HeaderRow.Cells[6].Visible = true;
    //                e.Row.Cells[6].Visible = true;
    //                btnApproved.Visible = true;
    //                btnReject.Visible = true;
    //            }

    //            if (lblItemCatName.Text == "Milk")
    //            {
    //                e.Row.CssClass = "columnmilk";
    //            }
    //            else
    //            {
    //                e.Row.CssClass = "columnproduct";
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 20 : " + ex.Message.ToString());
    //    }
    //}





    private void ApprovedOrRejectedOrderd(string status)
    {
        ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                           new string[] { "flag", "CDemand_Status" },
                           new string[] { "11", status.ToString() }, "TableSave");

        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
        {
            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
            // GetOrderDetails();
            // SetStatus();
            GetInvoice();
        }
        else
        {
            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            ApprovedOrRejectedOrderd("2");
          //  SetStatus1();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 21 : " + ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void btnApproved_Click(object sender, EventArgs e)
    {
        try
        {
            ApprovedOrRejectedOrderd("3");
           // SetStatus();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 22 : " + ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }








    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
}