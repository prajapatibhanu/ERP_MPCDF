using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;


public partial class mis_DemandSupply_CarriageInwardEntryAtSupply : System.Web.UI.Page
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
                CrateStockDetails();
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
        ddlCrateColor.SelectedIndex = 0;
        btnSubmit.Text = "Save";
        GetDatatableHeaderDesign();
        GridView1.SelectedIndex = -1;
        txtQtyPerCarriageType.Text = string.Empty;
        txtSpecification.Text = string.Empty;
        txtDate.Text = string.Empty;
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
    private void CrateStockDetails()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Trn_CarriageInwardAtSupply",
                     new string[] { "flag", "Office_ID" },
                       new string[] { "1", objdb.Office_ID() }, "dataset");
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
               // ddlCarriageMode.Items.Insert(0, new ListItem("Select", "0"));
                ddlCarriageMode.Items.Remove(ddlCarriageMode.Items.FindByText("Box"));
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
               
                if (btnSubmit.Text == "Save")
                {
                    lblMsg.Text = "";
                    DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                    string openingdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    ds1 = objdb.ByProcedure("USP_Trn_CarriageInwardAtSupply",
                        new string[] { "flag","CarriageModeID", "CrateColorID","OpeningDate","NoOfCarriageQty"
                                , "Specification","Office_ID","CreatedBy", "CreatedByIP" },
                        new string[] { "2",ddlCarriageMode.SelectedValue,ddlCrateColor.SelectedValue,openingdat,txtQtyPerCarriageType.Text.Trim()
                               ,txtSpecification.Text.Trim(),objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        CrateStockDetails();
                        Clear();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (error == "Retailer Code Already Exists")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  8:" + error);
                        }
                    }
                }
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
   
    protected void ddlCarriageMode_Init(object sender, EventArgs e)
    {
        GetCarriageMode();
    }
    protected void ddlCrateColor_Init(object sender, EventArgs e)
    {
        GetCrateColor();
    }
    #endregion=====================end of control======================

    #region=============== changed event for controls =================
   
    protected void ddlFilterOfficeNane_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDatatableHeaderDesign();
        CrateStockDetails();
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
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    string isactive = "";
                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {

                        ds1 = objdb.ByProcedure("USP_Trn_CarriageInwardAtSupply",
                            new string[] { "flag", "CarriageInwardId", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                    new string[] { "3",  e.CommandArgument.ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "CarriageInwardEntryAtSupply Details Deleted" }, "TableSave");

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            CrateStockDetails();
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