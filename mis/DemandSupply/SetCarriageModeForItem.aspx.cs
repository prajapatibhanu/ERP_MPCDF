using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;

public partial class mis_DemandSupply_SetCarriageModeForItem : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds1 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {

            if (!Page.IsPostBack)
            {
                GetCarriageModeForItemDetails();
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
    #region=======================user defined function========================
    private void Clear()
    {
        txtQtyPerCarriageType.Text = string.Empty;
        ddlCarriageMode.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
        ddlItemName.SelectedIndex = 0;
        ddlCarriageMode.SelectedIndex = 0;
        ddlCrateColor.SelectedIndex = 0;
        txtNotIssueQty.Text = string.Empty;
        btnSubmit.Text = "Save";
        pnlcratecolor.Visible = false;
        GetDatatableHeaderDesign();
        GridView1.SelectedIndex = -1;
        rfvcratecolour.Enabled = false;
        txtEffectiveDate.Text = string.Empty;
        txtCratePerThaapi.Text = string.Empty;
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 1 " + ex.Message.ToString());
        }
    }
    private void GetCarriageModeForItemDetails()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                     new string[] { "flag", "Office_ID" },
                       new string[] { "3", objdb.Office_ID() }, "dataset");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds2.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
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
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void GetCategory()
    {
        try
        {

            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds2.Tables[0];
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void GetItemByCategory()
    {
        try
        {

            ds2 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                     new string[] { "flag", "Office_ID", "ItemCat_id" },
                       new string[] { "2", objdb.Office_ID(), ddlItemCategory.SelectedValue }, "dataset");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "Item_id";
                ddlItemName.DataSource = ds2.Tables[0];
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlItemName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void GetCarriageMode()
    {
        try
        {

            ds2 = objdb.ByProcedure("USP_Mst_CarriageMode",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlCarriageMode.DataTextField = "CarriageModeName";
                ddlCarriageMode.DataValueField = "CarriageModeID";
                ddlCarriageMode.DataSource = ds2.Tables[0];
                ddlCarriageMode.DataBind();
                ddlCarriageMode.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCarriageMode.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    private void GetCrateColor()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                         new string[] { "flag" },
                           new string[] { "1" }, "dataset");
                    if (ds2.Tables[0].Rows.Count > 0)
                    {


                        ddlCrateColor.DataTextField = "V_SealColor";
                        ddlCrateColor.DataValueField = "I_SealColorID";
                        ddlCrateColor.DataSource = ds2.Tables[0];
                        ddlCrateColor.DataBind();
                        ddlCrateColor.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        ddlCrateColor.Items.Insert(0, new ListItem("Select", "0"));
                    }
        }
        catch (Exception)
        {
            
            throw;
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    private void InsertorUpdateSetCarriageModeForItem()
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                string isactive = "";
                if (chkIsActive.Checked == true)
                {
                    isactive = "1";
                }
                else
                {
                    isactive = "0";
                }
                DateTime date3 = DateTime.ParseExact(txtEffectiveDate.Text, "dd/MM/yyyy", culture);
                string EffectiveDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                if (btnSubmit.Text == "Save")
                {
                    lblMsg.Text = "";                   
                    ds1 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                        new string[] { "flag","ItemCat_id","Item_id","CarriageModeID", "ItemQtyByCarriageMode"
                                , "CrateColorID","Office_ID", "IsActive","CreatedBy", "CreatedByIP","NotIssueQty","EffectiveDate","CratePerThaapi" },
                        new string[] { "4",ddlItemCategory.SelectedValue,ddlItemName.SelectedValue,ddlCarriageMode.SelectedValue,txtQtyPerCarriageType.Text.Trim()
                               ,ddlCrateColor.SelectedValue,objdb.Office_ID(),isactive, objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),txtNotIssueQty.Text.Trim(),EffectiveDate,txtCratePerThaapi.Text.Trim() }, "TableSave");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetCarriageModeForItemDetails();
                        Clear();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (error == "Already Exists")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Item already exists.");
                            GetCarriageModeForItemDetails();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  8:" + error);
                        }
                    }
                }
                if (btnSubmit.Text == "Update")
                {
                    lblMsg.Text = "";
                    ds1 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                        new string[] { "flag","ItemCarriageModeId","ItemCat_id","Item_id","CarriageModeID", "ItemQtyByCarriageMode"
                                , "CrateColorID","Office_ID", "IsActive","CreatedBy", "CreatedByIP","PageName","Remark","NotIssueQty","EffectiveDate","CratePerThaapi" },
                        new string[] { "5", ViewState["rowid"].ToString(),ddlItemCategory.SelectedValue,ddlItemName.SelectedValue,ddlCarriageMode.SelectedValue,
                               txtQtyPerCarriageType.Text.Trim(),ddlCrateColor.SelectedValue,objdb.Office_ID(),isactive, objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                             Path.GetFileName(Request.Url.AbsolutePath), "Set Carriage Mode for Item Updated",txtNotIssueQty.Text.Trim(),EffectiveDate,txtCratePerThaapi.Text.Trim()}, "dataset");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Clear();
                        GetCarriageModeForItemDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (error == "Already Exists")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                            GetCarriageModeForItemDetails();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 9:" + error);
                        }
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Select Dugdh Sang");
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    #endregion================================================================

    #region=====================Init event for controls===========================
    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetCategory();
    }
    protected void ddlCarriageMode_Init(object sender, EventArgs e)
    {
        GetCarriageMode();
    }
    #endregion=====================end of control======================

    #region=============== changed event for controls =================
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemCategory.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetItemByCategory();
            GetDatatableHeaderDesign();
        }
    }
    protected void ddlCarriageMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCarriageMode.SelectedValue != "0")
            {
                spanqtydetail.InnerHtml = "Per " + ddlCarriageMode.SelectedItem.Text;
                if (ddlCarriageMode.SelectedItem.Text == "Crate")
                {
                    pnlcratecolor.Visible = true;
                    rfvcratecolour.Enabled = true;
                    GetCrateColor();
                }
                else
                {
                    ddlCrateColor.Items.Insert(0, new ListItem("Select", "0"));
                    pnlcratecolor.Visible = false;
                    rfvcratecolour.Enabled = false;

                }
            }
            else
            {
                spanqtydetail.InnerHtml = "";
            }
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }

        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }

    protected void ddlFilterOfficeNane_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDatatableHeaderDesign();
        GetCarriageModeForItemDetails();
    }
    #endregion============ end of changed event for controls===========

    #region============ button click event ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertorUpdateSetCarriageModeForItem();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                    Label lblItemCatid = (Label)row.FindControl("lblItemCat_id");
                    Label lblItemid = (Label)row.FindControl("lblItem_id");
                    Label lblCarriageModeID = (Label)row.FindControl("lblCarriageModeID");
                    Label lblOfficeID = (Label)row.FindControl("lblOffice_ID");
                    Label lblCrateColorID = (Label)row.FindControl("lblCrateColorID");
                    Label lblItemQty = (Label)row.FindControl("lblItemQty");
                    Label lblIsActive = (Label)row.FindControl("lblIsActive");
                    Label lblQty = (Label)row.FindControl("lblQty");
                    Label lblNotIssueQty = (Label)row.FindControl("lblNotIssueQty");
                    Label lblEffectiveDate = (Label)row.FindControl("lblEffectiveDate");
                    Label lblCratePerThaapi = (Label)row.FindControl("lblCratePerThaapi");
                    
                    
                    ddlItemCategory.SelectedValue = lblItemCatid.Text;
                    GetItemByCategory();
                    ddlItemName.SelectedValue = lblItemid.Text;
                    ddlCarriageMode.SelectedValue = lblCarriageModeID.Text;
                    chkIsActive.Checked = Convert.ToBoolean(lblIsActive.Text);
                    if (lblCarriageModeID.Text == "1")
                    {
                        pnlcratecolor.Visible = true;
                        GetCrateColor();
                        ddlCrateColor.SelectedValue = lblCrateColorID.Text;
                    }
                    else
                    {
                        if (lblCrateColorID.Text=="")
                        {
                            pnlcratecolor.Visible = false;
                            ddlCrateColor.SelectedIndex = 0;
                        }
                        else
                        {
                            pnlcratecolor.Visible = false;
                            GetCrateColor();
                            ddlCrateColor.SelectedValue = lblCrateColorID.Text;
                        }
                        //pnlcratecolor.Visible = false;
                        //GetCrateColor();
                        //ddlCrateColor.SelectedValue = lblCrateColorID.Text;
                    }
                    txtQtyPerCarriageType.Text = lblQty.Text;
                    txtNotIssueQty.Text = lblNotIssueQty.Text;
                    txtEffectiveDate.Text = lblEffectiveDate.Text;
                    txtCratePerThaapi.Text = lblCratePerThaapi.Text;
                    ViewState["rowid"] = e.CommandArgument;

                    btnSubmit.Text = "Update";

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                    GetDatatableHeaderDesign();
                }

            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    string isactive = "";
                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {
                        GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                        Label lblIsActive = (Label)row.FindControl("lblIsActive");
                        if (lblIsActive.Text == "True")
                        {
                            isactive = "0";
                        }
                        else
                        {
                            isactive = "1";
                        }
                        ds1 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                            new string[] { "flag", "ItemCarriageModeId", "IsActive", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                    new string[] { "6",  e.CommandArgument.ToString(),isactive, objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "CarriageModeForItem Details Active Or DeActive" }, "TableSave");

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetCarriageModeForItemDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }

                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    #endregion=============end of button click function==================
    
}