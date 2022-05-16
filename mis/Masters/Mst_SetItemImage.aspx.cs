using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class mis_Masters_Mst_SetItemImage : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
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
    #region=======================user defined function========================


    private void Clear()
    {



        GridView1.SelectedIndex = -1;
    }
    private void GetItemDetails()
    {
        try
        {
            if (ddlItemCategory.SelectedValue != "0")
            {
                GridView1.DataSource = objdb.ByProcedure("USP_SetItemImage",
                   new string[] { "flag", "ItemCat_id", "Office_ID" },
                   new string[] { "1", ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");
                string ofid = objdb.Office_ID();
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void UpdateImage()
    {
        string strFileName = "", strExtension = "", path2 = "";
        try
        {
            if (Page.IsValid)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (fuImage.HasFile)
                    {
                        if (!String.IsNullOrEmpty(ViewState["IImgOriginal"].ToString()))
                        {
                            string pathalready1 = Path.Combine(Server.MapPath("../image/Org_ItemImg/"), ViewState["IImgOriginal"].ToString());
                            if (File.Exists(pathalready1))
                            {
                                File.Delete(pathalready1);

                            }
                        }
                        if (!String.IsNullOrEmpty(ViewState["IItemImgThumb"].ToString()))
                        {
                            string pathalready2 = Path.Combine(Server.MapPath("../image/Thumb_ItemImg/"), ViewState["IItemImgThumb"].ToString());
                            if (File.Exists(pathalready2))
                            {
                                File.Delete(pathalready2);

                            }
                        }
                        strFileName = fuImage.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        string strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        strFileName = ViewState["IName"].ToString().Replace(" ", "");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../image/Org_ItemImg/"), strFileName);
                        fuImage.PostedFile.SaveAs(path);

                        Stream imgStream = fuImage.PostedFile.InputStream;
                        System.Drawing.Bitmap bmThumb = new System.Drawing.Bitmap(imgStream);
                        System.Drawing.Image im = bmThumb.GetThumbnailImage(100, 100, null, IntPtr.Zero);
                        im.Save(Server.MapPath("../image/Thumb_ItemImg/").ToString() + "\\ThumbNail_" + strFileName);

                        path2 = "ThumbNail_" + strFileName;
                        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];

                        ds1 = objdb.ByProcedure("USP_SetItemImage",
                      new string[] { "Flag", "ItemOffice_ID", "Item_id", "Office_ID", "ItemImg_Original", "ItemImg_Thumb", "CreatedByIP", "CreatedBy", "ItemAdvCardRate" },
                       new string[] { "2", ViewState["rowid"].ToString(), ViewState["Item_id"].ToString(), objdb.Office_ID(), strFileName, path2, IPAddress, objdb.createdBy(),txtAdvCardRate.Text.Trim() }, "dataset");
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            GetItemDetails();
                            strFileName = string.Empty;
                            path2 = string.Empty;

                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);

                        }
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    }
                }
                else
                {
                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Select Image");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

        }
        catch (Exception ex)
        {
            string path5 = Path.Combine(Server.MapPath("../image/Org_ItemImg/"), strFileName);
            if (File.Exists(path5))
            {
                File.Delete(path5);

            }
            string path6 = Path.Combine(Server.MapPath("../image/Thumb_ItemImg/"), path2);
            if (File.Exists(path6))
            {
                File.Delete(path6);

            }
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    #endregion====================================end of user defined function

    #region=============== changed event for controls =================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetItemDetails();
        }
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
                    lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItemImgOriginal = (Label)row.FindControl("lblItemImgOriginal");
                    Label lblItemImgThumb = (Label)row.FindControl("lblItemImgThumb");
                    Label lblItemCatName = (Label)row.FindControl("lblItemCatName");
                    Label lblItemName = (Label)row.FindControl("lblItemName");
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");
                    Label lblItemAdvCardRate = (Label)row.FindControl("lblItemAdvCardRate");

                    modelcategory.InnerHtml = lblItemCatName.Text;
                    modelitem.InnerHtml = lblItemName.Text;

                    ViewState["rowid"] = e.CommandArgument;
                    ViewState["Item_id"] = lblItem_id.Text;
                    ViewState["IName"] = lblItemName.Text;
                    ViewState["IImgOriginal"] = lblItemImgOriginal.Text;
                    ViewState["IItemImgThumb"] = lblItemImgThumb.Text;
                    txtAdvCardRate.Text = lblItemAdvCardRate.Text;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                }
            }
            else if (e.CommandName == "RecordAdvCard")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItemCatName = (Label)row.FindControl("lblItemCatName");
                    Label lblItemName = (Label)row.FindControl("lblItemName");
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");
                    Label lblItemAdvCardRate = (Label)row.FindControl("lblItemAdvCardRate");

                    modelcategory1.InnerHtml = lblItemCatName.Text;
                    modelitem1.InnerHtml = lblItemName.Text;

                    ViewState["rowid"] = e.CommandArgument;
                    ViewState["Item_id"] = lblItem_id.Text;
                    txtAdvCardRate.Text = lblItemAdvCardRate.Text;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal1()", true);
                }
            }
            else if (e.CommandName == "ChangeStatus")
            {
                DataSet dsStatus = new DataSet();
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblModalMsg.Text = string.Empty;
      
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    HiddenField HF_Status = (HiddenField)row.FindControl("HFItemAdvCart_Status");
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");
                    Int16 sts = 0;
                   
                     if (HF_Status.Value == "True" || HF_Status.Value == "")
                    {
                        sts = 0;
                    }
                    else
                    {
                        sts = 1;
                    }
                     ds1 = objdb.ByProcedure("USP_SetItemImage",
                           new string[] { "Flag","ItemOffice_ID", "Item_id", "Office_ID", "ItemAdvCart_Status" },
                            new string[] {"4", e.CommandArgument.ToString(), lblItem_id.Text, objdb.Office_ID(), Convert.ToString(sts) }, "dataset");
                     if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                     {

                         string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                         lblModalMsg1.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                         GetItemDetails();

                     }
                     else
                     {
                         string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                         lblModalMsg1.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);

                     }


                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion============ end of changed event for controls===========

    #region============ init,selectedindexchanged , button click event ============================
    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetCategory();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (btnSubmit.Text == "Update")
            {
                UpdateImage();
            }
        }
    }
    #endregion=============end of button click funciton==================
    protected void btnUpdateRate_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                ds1 = objdb.ByProcedure("USP_SetItemImage",
                          new string[] { "Flag", "ItemOffice_ID", "Item_id", "ItemAdvCardRate", "Office_ID", "CreatedByIP", "CreatedBy" },
                           new string[] { "3", ViewState["rowid"].ToString(), ViewState["Item_id"].ToString(), txtAdvCardRate.Text.Trim(), objdb.Office_ID(), IPAddress, objdb.createdBy() }, "dataset");
                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {

                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblModalMsg1.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    GetItemDetails();


                }
                else
                {
                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblModalMsg1.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);

                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal1()", true);
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
    }
}