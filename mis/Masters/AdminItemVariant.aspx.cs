using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class mis_Masters_AdminItemVariant : System.Web.UI.Page
{

    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                ItemCategory();
                GetUnit();
                GetVariantDetails();
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

    protected void ItemCategory()
    {
       try{

      

        ddlItemCategory.DataSource = objdb.ByProcedure("SpItemCategory",
                 new string[] { "flag" },
                 new string[] { "1" }, "dataset");
        ddlItemCategory.DataTextField = "ItemCatName";
        ddlItemCategory.DataValueField = "ItemCat_id";
        ddlItemCategory.DataBind();
        ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
       }
         catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
        
    }


    protected void GetItem()
    {

        try
        {
      

        ddlItem.DataSource = objdb.ByProcedure("SpItemType",
                 new string[] { "flag", "ItemCat_id", "Office_ID" },
                 new string[] { "6", ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");
        ddlItem.DataTextField = "ItemTypeName";
        ddlItem.DataValueField = "ItemType_id";
        ddlItem.DataBind();
        ddlItem.Items.Insert(0, new ListItem("Select", "0"));
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void GetPackagingMode()
    {

        try
        {


            ds = objdb.ByProcedure("SpItemType",
                new string[] { "flag" }, 
                new string[] { "8" }, "dataset");
            ddlPackMode.DataSource = ds.Tables[0];
            ddlPackMode.DataTextField = "PackagingModeName";
            ddlPackMode.DataValueField = "PackagingModeId";
            ddlPackMode.DataBind();
            ddlPackMode.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }

    }

    private void InsertOrUpdateVariant()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_AdminVariant",
                            new string[] { "flag", "ItemCat_id", "ItemType_id", "Unit_id", "ItemName", "PackagingSize","Packaging_Mode"
                                , "HSNCode", "CreatedBy" },
                            new string[] { "2", ddlItemCategory.SelectedValue, ddlItem.SelectedValue, ddlUnit.SelectedValue
                                , txtItemVariantName.Text.Trim(), txtPackSize.Text.Trim(),ddlPackMode.SelectedValue
                                ,ddlHsnCode.SelectedValue
                                , objdb.createdBy() }, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetVariantDetails();
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :Record added Successfully");
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa fa-warning", "alert-warning", "Warning!", ddlItemCategory.SelectedItem.Text+" " + txtItemVariantName.Text+" "
                                    + txtPackSize.Text.Trim() + " " + error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }

                    if (btnSubmit.Text == "Update")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_AdminVariant",
                            new string[] { "flag", "Item_id", "ItemCat_id", "ItemType_id", "Unit_id", "ItemName" ,"PackagingSize"
                                ,"Packaging_Mode"
                                , "HSNCode", "CreatedBy" },
                            new string[] { "3", ViewState["rowid"].ToString(), ddlItemCategory.SelectedValue
                                , ddlItem.SelectedValue,ddlUnit.SelectedValue,txtItemVariantName.Text.Trim()
                                 ,txtPackSize.Text.Trim(),ddlPackMode.SelectedValue
                                ,ddlHsnCode.SelectedValue
                                , objdb.createdBy() }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            Clear();
                            GetVariantDetails();
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                          
                            
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa fa-warning", "alert-warning", "Warning!", txtItemVariantName.Text + " or "
                                    + txtPackSize.Text.Trim() + " " + error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }

                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }


            }
        }
    }

    private void Clear()
    {
        ddlHsnCode.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
      //  ddlPackMode.SelectedIndex = 0;
        ddlUnit.SelectedIndex = 0;
        txtPackSize.Text = string.Empty;
       
        txtItemVariantName.Text = string.Empty;
        // txtDescription.Text = string.Empty;
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
        lblMsg.Text = "";
        
    }
    protected void GetVariantDetails()
    {

        try
        {

            GridView1.DataSource = objdb.ByProcedure("USP_Mst_AdminVariant",
                            new string[] { "flag", "Office_ID" },
                            new string[] { "1",objdb.Office_ID() }, "dataset");
            GridView1.DataBind();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

        //finally
        //{
        //    if (ds != null) { ds.Dispose(); }
        //}
    }


    protected void GetUnit()
    {
        try
        {
            ddlUnit.DataTextField = "UQCCode";
                ddlUnit.DataValueField = "Unit_id";
                ddlUnit.DataSource = objdb.ByProcedure("SpUnit",
                       new string[] { "flag", },
                       new string[] { "1", }, "dataset");
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
           

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
 
    private void GetProductMasterDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("SpItemType",
                            new string[] { "flag", "Office_ID", },
                            new string[] { "5", objdb.Office_ID() }, "dataset");
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
  
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItem();
    }


  
    
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void txtPacketSize_TextChanged(object sender, EventArgs e)
    {

    }
   
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        
    }

    protected void ddlPackMode_Init(object sender, EventArgs e)
    {
        GetPackagingMode();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertOrUpdateVariant();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");
                    Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                    Label lblUnit_id = (Label)row.FindControl("lblUnit_id");
                    Label lblItemType_id = (Label)row.FindControl("lblItemType_id");
                    Label lblItemName = (Label)row.FindControl("lblItemName");
                    Label lblPackagingSize = (Label)row.FindControl("lblPackagingSize");
                    Label lblPackMode = (Label)row.FindControl("lblPackMode");
                     Label lblHSNCode = (Label)row.FindControl("lblHSNCode");
                    
                    ViewState["rowid"] = e.CommandArgument;
                    ddlItemCategory.SelectedValue = lblItemCat_id.Text;
                    GetItem();
                    ddlItem.SelectedValue = lblItemType_id.Text;

                    txtItemVariantName.Text = lblItemName.Text;

                    txtPackSize.Text = lblPackagingSize.Text;
                    ddlUnit.SelectedValue = lblUnit_id.Text;
                    ddlPackMode.SelectedValue = lblPackMode.Text;
                    ddlHsnCode.SelectedValue = lblHSNCode.Text;
                    // txtDescription.Text = lblDescription.Text;
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
                }

            }
        

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
}