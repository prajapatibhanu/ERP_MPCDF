using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Web.UI;


public partial class mis_MilkCollection_InwardItem : System.Web.UI.Page
{ 
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillSociety();
                GetItemCategory();
                txtTransactionDt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                TxtFdate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                BindOpeningStockItem();

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

    protected void FillSociety()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtsocietyName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    txtBlock.Text = ds.Tables[0].Rows[0]["Block_Name"].ToString();
                    txtBlock.Enabled = false;

                }

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

    protected void ddlitemcategory_Init(object sender, EventArgs e)
    {
        try
        {
            ddlitemcategory.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlitemtype_Init(object sender, EventArgs e)
    {
        try
        {
            ddlitemtype.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlitemname_Init(object sender, EventArgs e)
    {
        try
        {
            ddlitemname.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("InwardItem.aspx", false);
    }

    private void GetItemCategory()
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_MstLocalSale",
                         new string[] { "flag" },
                         new string[] { "4" }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlitemcategory.DataTextField = "ItemCatName";
                        ddlitemcategory.DataValueField = "ItemCat_id";
                        ddlitemcategory.DataSource = ds;
                        ddlitemcategory.DataBind();
                        ddlitemcategory.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void ddlitemcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlitemcategory.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Sp_MstLocalSale",
                             new string[] { "flag", "ItemCat_id" },
                             new string[] { "5", ddlitemcategory.SelectedValue }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlitemtype.DataTextField = "ItemTypeName";
                            ddlitemtype.DataValueField = "ItemType_id";
                            ddlitemtype.DataSource = ds;
                            ddlitemtype.DataBind();
                            ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Type Not Exist For Category - " + ddlitemcategory.SelectedItem.Text);
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Type Not Exist For Category - " + ddlitemcategory.SelectedItem.Text);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Type Not Exist For Category - " + ddlitemcategory.SelectedItem.Text);
                }
            }
            else
            {

                ddlitemtype.Items.Clear();
                ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
                ddlitemname.Items.Clear();
                ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
                lblunitnameH.Text = "";
                txtitemqty.Text = "";

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlitemcategory.SelectedValue != "0" && ddlitemtype.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Sp_MstLocalSale",
                             new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID" },
                             new string[] { "6", ddlitemcategory.SelectedValue, ddlitemtype.SelectedValue, objdb.Office_ID() }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlitemname.DataTextField = "ItemName";
                            ddlitemname.DataValueField = "Item_id";
                            ddlitemname.DataSource = ds;
                            ddlitemname.DataBind();
                            ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text);
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text);
                }
            }
            else
            {


                ddlitemname.Items.Clear();
                ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
                lblunitnameH.Text = "";
                txtitemqty.Text = "";

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void ddlitemname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtitemqty.Text = "";
			txtitemqty.Enabled = true;
            if (ddlitemcategory.SelectedValue != "0" && ddlitemtype.SelectedValue != "0" && ddlitemname.SelectedValue != "0")
            {

                ds = null;
                ds = objdb.ByProcedure("Sp_MstLocalSale",
                             new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID", "Item_id" },
                             new string[] { "7", ddlitemcategory.SelectedValue, ddlitemtype.SelectedValue, objdb.Office_ID(), ddlitemname.SelectedValue }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {


                            txtitemqty.Enabled = true;
                            lblunitnameH.Text = "( In " + ds.Tables[0].Rows[0]["UnitName"].ToString() + " )";
                            lblunitnameH.ToolTip = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                            HFsalerate.Value = ds.Tables[0].Rows[0]["MRP"].ToString();


                            if (lblunitnameH.ToolTip == "10")
                            {
                                RegularExpressionValidator5.ValidationExpression = @"^[0-9]*$";
                                RegularExpressionValidator5.Text = "<i class='fa fa-exclamation-circle' title='Enter Valid Quantity(This Item Unit Allow Only Digits)!'></i>";

                            }

                            else
                            {
                                RegularExpressionValidator5.ValidationExpression = @"^[0-9]\d*(\.\d{1,3})?$";
                                RegularExpressionValidator5.Text = "<i class='fa fa-exclamation-circle' title='Enter Valid Quantity(This Item Unit Allow Only Digits/Decimal as well as 3 Decimal Place!'></i>";
                            }

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text + " & Item Name - " + ddlitemname.SelectedItem.Text);
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text + " & Item Name - " + ddlitemname.SelectedItem.Text);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text + " & Item Name - " + ddlitemname.SelectedItem.Text);
                }
            }
            else
            {
                txtitemqty.Enabled = false;
                lblunitnameH.Text = "";
                txtitemqty.Text = "";

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {


                if (txtitemqty.Text == "" || txtitemqty.Text == "0")
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Invalid Item Quantity : - " + txtitemqty.Text);
                    return;
                }
                else
                {
                    HFFinalAmount.Value = (Convert.ToDecimal(HFsalerate.Value) * Convert.ToDecimal(txtitemqty.Text)).ToString();
                }

                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    DateTime date3 = DateTime.ParseExact(txtTransactionDt.Text, "dd/MM/yyyy", cult);
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("Sp_tblSpItemStock",
                                                new string[] { "flag", "ItemCat_id", "ItemType_id", "Item_id", "Cr", "Dr", "Rate", "Warehouse_id", "Depart_Id", "TranDt", "Office_Id", "CreatedBy", "PageName", "Remark", "ipaddress", "Amount" },
                                                new string[] { "5", ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitemname.SelectedItem.Value, txtitemqty.Text, "0", HFsalerate.Value, "0", "0", date3.ToString(), objdb.Office_ID(), objdb.createdBy(), Path.GetFileName(Request.Url.AbsolutePath), txtremarks.Text, objdb.GetLocalIPAddress(), HFFinalAmount.Value }, "dataset");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            BindOpeningStockItem();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            BindOpeningStockItem();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        ds.Clear();

                    }
                    if (btnSubmit.Text == "Update")
                    {

                        ds = objdb.ByProcedure("Sp_tblSpItemStock",
                                                new string[] { "flag", "ItemCat_id", "ItemType_id", "Item_id", "Cr", "Dr", "Rate", "Warehouse_id", "Depart_Id", "TranDt", "Office_Id", "ItmStock_id", "CreatedBy", "PageName", "Remark", "ipaddress", "Amount" },
                                                new string[] { "7", ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitemname.SelectedItem.Value, txtitemqty.Text, "0", HFsalerate.Value, "0", "0", date3.ToString(), objdb.Office_ID(), ViewState["rid"].ToString(), objdb.createdBy(), Path.GetFileName(Request.Url.AbsolutePath), txtremarks.Text, objdb.GetLocalIPAddress(), HFFinalAmount.Value }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            BindOpeningStockItem();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            BindOpeningStockItem();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        ds.Clear();
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! .", "Please Enter Date");
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }

    }

    private void BindOpeningStockItem()
    {
        try
        {
            lblMsg.Text = "";

            string date = "";

            if (TxtFdate.Text != "")
            {
                date = Convert.ToDateTime(TxtFdate.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Opps!", "Invalid Date Formate");
                return;
            }

            ds = null;
            ds = objdb.ByProcedure("Sp_tblSpItemStock",
                                        new string[] { "flag", "Office_Id", "Warehouse_id", "TranDt" },
                                        new string[] { "6", objdb.Office_ID(), "0", date }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                gvOpeningStock.DataSource = ds;
                gvOpeningStock.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                gvOpeningStock.DataSource = null;
                gvOpeningStock.DataBind();
            }
            ds.Clear();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

    protected void gvOpeningStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTransactionFrom = e.Row.FindControl("lblTransactionFrom") as Label;
                LinkButton lnkUpdate = e.Row.FindControl("lnkUpdate") as LinkButton;
                LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;

                if (lblTransactionFrom.Text == "Purchase Order Received")
                {
                    lnkDelete.Visible = false;
                    lnkUpdate.Visible = false;
                }
                else
                {
                    lnkDelete.Visible = false;
                    lnkUpdate.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

    protected void gvOpeningStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblWarehouse_id = (Label)row.FindControl("lblWarehouse_id");
                    Label lblTranDt = (Label)row.FindControl("lblTranDt");
                    Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                    Label lblItemType_id = (Label)row.FindControl("lblItemType_id");
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");
                    Label lblProducUnit_id = (Label)row.FindControl("lblProducUnit_id");
                    Label lblCr = (Label)row.FindControl("lblCr");
                    Label lblRate = (Label)row.FindControl("lblRate");
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    Label lblremark = (Label)row.FindControl("lblremark");

                    


                    txtTransactionDt.Text = lblTranDt.Text;
                    ViewState["rid"] = e.CommandArgument.ToString();
                    GetItemCategory();
                    ddlitemcategory.SelectedValue = lblItemCat_id.Text;
                    ddlitemcategory_SelectedIndexChanged(sender, e);
                    ddlitemtype.SelectedValue = lblItemType_id.Text;
                    ddlitemtype_SelectedIndexChanged(sender, e);
                    ddlitemname.SelectedValue = lblItem_id.Text;
                    ddlitemname_SelectedIndexChanged(sender, e);

                    foreach (GridViewRow gvRow in gvOpeningStock.Rows)
                    {
                        if (gvOpeningStock.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            gvOpeningStock.SelectedIndex = gvRow.DataItemIndex;
                            gvOpeningStock.SelectedRowStyle.BackColor = System.Drawing.Color.LemonChiffon;
                            break;
                        }
                    }
                    GetDatatableHeaderDesign();

                    btnSubmit.Text = "Update";
                    txtitemqty.Text = lblCr.Text;
                    txtremarks.Text = lblremark.Text;
                    HFsalerate.Value = lblRate.Text;
                }

            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    ViewState["rid"] = e.CommandArgument.ToString();
                    ds = objdb.ByProcedure("Sp_tblSpItemStock",
                                                new string[] { "flag", "ItmStock_id", "CreatedBy", "PageName", "Remark", "ipaddress" },
                                                new string[] { "8", ViewState["rid"].ToString(), objdb.createdBy(), Path.GetFileName(Request.Url.AbsolutePath), "Opening Stock Record Deleted", objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds.Clear();
                        BindOpeningStockItem();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds.Clear();
                        BindOpeningStockItem();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (gvOpeningStock.Rows.Count > 0)
            {
                gvOpeningStock.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvOpeningStock.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

    private void Clear()
    {
        lblMsg.Text = string.Empty;
        ddlitemtype.Items.Clear();
        ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
        ddlitemtype.SelectedIndex = 0;
        ddlitemcategory.SelectedIndex = 0;
        ddlitemname.Items.Clear();
        ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
        lblunitnameH.Text = "";
        txtitemqty.Text = string.Empty;
        txtremarks.Text = string.Empty;
        btnSubmit.Text = "Save";
        gvOpeningStock.SelectedIndex = -1;
        GetDatatableHeaderDesign();
    }

    protected void TxtFdate_TextChanged(object sender, EventArgs e)
    {
        if (TxtFdate.Text != "")
        {
            BindOpeningStockItem();
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Opps!", "Invalid Date Formate");
            return;
        }
    }

}