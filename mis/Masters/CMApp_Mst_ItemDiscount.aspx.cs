using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Globalization;

public partial class mis_Masters_CMApp_Mst_ItemDiscount : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds4 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    string currentdate = "", currrentime = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetDiscountDetails();
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



    #region==========Item Citizen Mapping code========================

    #region=======================user defined function========================
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView2.Rows.Count > 0)
            {
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView2.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }
    private void Clear()
    {

        ddlItem.SelectedIndex = 0;
        txtDiscount.Text = string.Empty;

    }
    private void GetDiscountDetails()
    {
        try
        {
            GridView2.DataSource = objdb.ByProcedure("USP_CMApp_Mst_ItemRateDiscount",
                        new string[] { "Flag", "Office_ID" },
                        new string[] { "2", objdb.Office_ID() }, "TableSave");
            GridView2.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2: " + ex.Message.ToString());
        }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtEffectiveFromDate.Text;
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtEffectiveToDate.Text;
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                InsertDiscount();
            }
            else
            {
                txtEffectiveToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must less than or equal to FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error D: ", ex.Message.ToString());
        }
    }
    protected void GetItemCategory()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
                  new string[] { "flag" },
                 new string[] { "1" }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds1.Tables[0];
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3:  ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void GetItem()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_CMApp_ItemDetails",
                       new string[] { "flag", "ItemCat_id", "Office_ID" },
                       new string[] { "1", ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "Item_id";
                ddlItem.DataSource = ds1.Tables[0];
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4:", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void InsertDiscount()
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DateTime date3 = DateTime.ParseExact(txtEffectiveFromDate.Text, "dd/MM/yyyy", culture);
                DateTime date4 = DateTime.ParseExact(txtEffectiveToDate.Text, "dd/MM/yyyy", culture);
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];




                if (btnMSubmit.Text == "Save")
                {
                    lblMsg.Text = "";
                    ds1 = objdb.ByProcedure("USP_CMApp_Mst_ItemRateDiscount",
                        new string[] { "Flag", "ItemCat_id", "Item_id", "Office_ID","Discount_InPer",
                                "EffectiveFromDate","EffectiveToDate", "CreatedBy", "CreatedByIP" },

                        new string[] { "3", ddlItemCategory.SelectedValue,ddlItem.SelectedValue, objdb.Office_ID()
                                ,txtDiscount.Text.Trim(),date3.ToString("yyyy/MM/dd"),date4.ToString("yyyy/MM/dd")
                                , objdb.createdBy(),  IPAddress }, "TableSave");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Clear();
                        GetDiscountDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string msg = ds1.Tables[0].Rows[0]["Msg"].ToString();
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (msg == "Already")
                        {
                            GetDatatableHeaderDesign();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error + "  for " + ddlItem.SelectedItem.Text + "  Date : " +  txtEffectiveToDate.Text.Trim());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + error);
                        }
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Select Citizen Name from List");
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6:", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    #endregion=============== end user defined function
    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetItemCategory();

    }

    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItem();
        GetDatatableHeaderDesign();
    }
    protected void btnMSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void btnMClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ddlItemCategory.SelectedIndex = 0;       
        txtEffectiveFromDate.Text = string.Empty;
        txtEffectiveToDate.Text = string.Empty;
        Clear();
        GetDatatableHeaderDesign();
    }
    #endregion========================================================
    
}