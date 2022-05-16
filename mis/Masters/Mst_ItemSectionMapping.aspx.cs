using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Xml;


public partial class mis_Masters_Mst_ItemSectionMapping : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    int already = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
               
                GetCategory();
                GetItemSectionMappingDetails();
                ViewState["MappingData"] = "";
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetSectionSearch();
                GetSubSectionUpdate();
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
    private void GetItemSectionMappingDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_MilkOrProductItemSectionMapping",
                            new string[] { "Flag", "Office_ID", "MOrPSection_id" },
                            new string[] { "4", objdb.Office_ID(),ddlSectionSearch.SelectedValue }, "dataset");
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetSection()
    {
        try
        {
            if(ddlItemCategory.SelectedValue!="0")
            {

            
            ddlSection.DataTextField = "SectionName";
            ddlSection.DataValueField = "MOrPSection_id";
            ddlSection.DataSource = objdb.ByProcedure("USP_Mst_MilkOrProductSectionName",
                 new string[] { "Flag", "Office_ID", "ItemCat_id" },
                   new string[] { "5", objdb.Office_ID(),ddlItemCategory.SelectedValue }, "dataset");
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetSubSection()
    {
        try
        {
            if (ddlSection.SelectedValue != "0")
            {


                ddlSubSection.DataTextField = "SubSectionName";
                ddlSubSection.DataValueField = "MOrPSubSection_id";
                ddlSubSection.DataSource = objdb.ByProcedure("USP_Mst_MilkOrProductSubSectionName",
                     new string[] { "Flag", "Office_ID", "MOrPSection_id" },
                       new string[] { "4", objdb.Office_ID(), ddlSection.SelectedValue }, "dataset");
                ddlSubSection.DataBind();
                ddlSubSection.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetSubSectionUpdate()
    {
        try
        {

                ddlSubSectionUpdate.DataTextField = "SubSectionName";
                ddlSubSectionUpdate.DataValueField = "MOrPSubSection_id";
                ddlSubSectionUpdate.DataSource = objdb.ByProcedure("USP_Mst_MilkOrProductSubSectionName",
                     new string[] { "Flag", "Office_ID" },
                       new string[] { "5", objdb.Office_ID() }, "dataset");
                ddlSubSectionUpdate.DataBind();
                ddlSubSectionUpdate.Items.Insert(0, new ListItem("Select", "0"));
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetSectionSearch()
    {
        try
        {
            ddlSectionSearch.DataTextField = "SectionName";
            ddlSectionSearch.DataValueField = "MOrPSection_id";
            ddlSectionSearch.DataSource = objdb.ByProcedure("USP_Mst_MilkOrProductSectionName",
                 new string[] { "Flag", "Office_ID" },
                   new string[] { "4", objdb.Office_ID() }, "dataset");
            ddlSectionSearch.DataBind();
            ddlSectionSearch.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetItem()
    {
        try
        {
            ddlItemName.DataTextField = "ItemName";
            ddlItemName.DataValueField = "Item_id";
            ddlItemName.DataSource = objdb.ByProcedure("USP_Mst_MilkOrProductItemSectionMapping",
                 new string[] { "Flag", "ItemCat_id", "Office_ID" },
                   new string[] { "1", ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");
            ddlItemName.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    protected void GetCategory()
    {
        try
        {
            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCat_id";
            ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlItemCategory.DataBind();
            ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }

    #endregion========================================================
    #region=========== init or changed even===========================



    #endregion===========================
    #region=========== click event for grdiview row bound event===========================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertItemData();
        }
    }
    private void InsertItemData()
    {
        try
        {
            lblMsg.Text = string.Empty;
            string ItemId = "";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                foreach (ListItem item in ddlItemName.Items)
                {
                    if (item.Selected)
                    {
                        ItemId = item.Value;

                        if (ItemId != "0")
                        {
                            InsertSectionMapping(ItemId);
                        }
                    }
                }
                if (already > 0)
                {
                    ViewState["MappingData"] = "";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success : Record Inserted Successfully");
                    already = 0;
					 GetItem();
                    GetItemSectionMappingDetails();
                }
                else
                {

                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Updated");
                    return;
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa fa-warning", "alert-warning", "Warning!", " Enter Bank Name");
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
    }
    #endregion===========================
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemCategory.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetItem();
            GetSection();
        }

    }
    #region=============== changed event for controls =================
    private void InsertSectionMapping(string I_itemId)
    {
        try
        {

            string IPAddress1 = Request.ServerVariables["REMOTE_ADDR"];

            if (btnSubmit.Text == "Save")
            {
                lblMsg.Text = "";
                ds1 = objdb.ByProcedure("USP_Mst_MilkOrProductItemSectionMapping",
                    new string[] { "Flag", "MOrPSection_id", "MOrPSubSection_id", "Item_id", "Office_ID", "CreatedBy", "CreatedByIP" },
                    new string[] { "2", ddlSection.SelectedValue,ddlSubSection.SelectedValue, I_itemId, objdb.Office_ID(), objdb.createdBy(), IPAddress1 }, "TableSave");

                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    ++already;
                    ViewState["MappingData"] = "1";
                }
                else
                {
                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    string msg = ds1.Tables[0].Rows[0]["Msg"].ToString();
                    if (msg == "Already")
                    {
                        already--;
                        ViewState["MappingData"] = "2";
                    }
                    else
                    {
                        ViewState["MappingData"] = "0";
                    }

                }
                ds1.Dispose();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (e.CommandName == "RecordDelete")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblIsActive = (Label)row.FindControl("lblIsActive");
                    string status = "";
                    if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                    {
                        lblMsg.Text = string.Empty;

                        if (lblIsActive.Text == "True")
                        {
                            status = "False";
                        }
                        else
                        {
                            status = "True";
                        }
                        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        ds1 = objdb.ByProcedure("USP_Mst_MilkOrProductItemSectionMapping",
                                    new string[] { "Flag", "ItemSectionMapping_Id", "IsActive", "CreatedBy", "CreatedByIP" },
                                    new string[] { "3", e.CommandArgument.ToString(), status, objdb.createdBy(), IPAddress }, "TableSave");

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :");
                            GetItemSectionMappingDetails();
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["Msg"].ToString();
                            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        ds1.Clear();
                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                }

            }
            if (e.CommandName == "RecordEdit")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblIName = (Label)row.FindControl("lblItemName");
                    txtItemName.Text = lblIName.Text;
                    ViewState["ItemSectionMapping_Id"] = e.CommandArgument.ToString();
                    ddlSubSectionUpdate.SelectedIndex = 0;
                    lblModalMsg1.Text = string.Empty;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    #endregion============ end of changed event for controls===========
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetItemSectionMappingDetails();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetSubSection();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            ds1 = objdb.ByProcedure("USP_Mst_MilkOrProductItemSectionMapping",
                                    new string[] { "Flag", "ItemSectionMapping_Id", "MOrPSubSection_id" },
                                    new string[] { "5", ViewState["ItemSectionMapping_Id"].ToString(),ddlSubSectionUpdate.SelectedValue }, "TableSave");

            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
            {
                string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                lblModalMsg1.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :");
                ddlSubSectionUpdate.SelectedIndex = 0;
                GetItemSectionMappingDetails();
            }
            else
            {
                string error = ds1.Tables[0].Rows[0]["Msg"].ToString();
                lblModalMsg1.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", "Error :" + error);
            }
            ds1.Clear();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
    }
}