using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class mis_Masters_Mst_MorPItemSubSection : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    string IsActiveStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCategory();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetItemSectionMasterDetails();
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
        txtSubSectionName.Text = string.Empty;
        ddlSection.SelectedIndex = 0;
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
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
    protected void GetSection()
    {
        try
        {
            if (ddlItemCategory.SelectedValue != "0")
            {


                ddlSection.DataTextField = "SectionName";
                ddlSection.DataValueField = "MOrPSection_id";
                ddlSection.DataSource = objdb.ByProcedure("USP_Mst_MilkOrProductSectionName",
                     new string[] { "Flag", "Office_ID", "ItemCat_id" },
                       new string[] { "5", objdb.Office_ID(), ddlItemCategory.SelectedValue }, "dataset");
                ddlSection.DataBind();
                ddlSection.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    private void GetItemSectionMasterDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_MilkOrProductSubSectionName",
                            new string[] { "Flag", "Office_ID", },
                            new string[] { "3", objdb.Office_ID() }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertOrUpdateSubSection()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    if (chkIsActive.Checked == true)
                    {
                        IsActiveStatus = "1";
                    }
                    else
                    {
                        IsActiveStatus = "0";
                    }
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_MilkOrProductSubSectionName",
                            new string[] { "flag", "MOrPSection_id", "SubSectionName", "IsActive", "CreatedBy", "CreatedByIP", "Office_ID", "ItemCat_id" },
                            new string[] { "1",ddlSection.SelectedValue, txtSubSectionName.Text.Trim(), IsActiveStatus, objdb.createdBy()
                                , IPAddress, objdb.Office_ID(),ddlItemCategory.SelectedValue }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetItemSectionMasterDetails();
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :");
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["Msg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa fa-warning", "alert-warning", "Warning!", txtSubSectionName.Text + " " + error);
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
                        ds = objdb.ByProcedure("USP_Mst_MilkOrProductSubSectionName",
                            new string[] { "flag", "MOrPSubSection_id", "MOrPSection_id", "SubSectionName", "IsActive", "CreatedBy", "CreatedByIP", "Office_ID", "ItemCat_id" },
                            new string[] { "2", ViewState["rowid"].ToString(),ddlSection.SelectedValue, txtSubSectionName.Text.Trim(), IsActiveStatus, 
                                objdb.createdBy(), IPAddress, objdb.Office_ID(),ddlItemCategory.SelectedValue }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            ddlItemCategory.SelectedIndex = 0;
                            GetItemSectionMasterDetails();
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["Msg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa fa-warning", "alert-warning", "Warning!", txtSubSectionName.Text + " " + error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
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
                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

        }

    }
    #endregion====================================end of user defined function==========

    #region============ button click and gridview events event ============================
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemCategory.SelectedValue != "0")
        {
            GetSection();
            GetDatatableHeaderDesign();
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertOrUpdateSubSection();
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ddlSection.SelectedIndex = 0;
        Clear();
        GridView1.SelectedIndex = -1;
        ddlItemCategory.SelectedIndex = 0;
        GetDatatableHeaderDesign();
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
                    Label lblIsActive = (Label)row.FindControl("lblIsActive");
                    Label lblSubSectionName = (Label)row.FindControl("lblSubSectionName");
                    Label lblMOrPSection_id = (Label)row.FindControl("lblMOrPSection_id");
                    Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                    ViewState["rowid"] = e.CommandArgument;
                    if (lblIsActive.Text == "True")
                    {
                        chkIsActive.Checked = true;
                    }
                    else
                    {
                        chkIsActive.Checked = false;
                    }
                    if (lblItemCat_id.Text == "")
                    {
                        ddlItemCategory.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlItemCategory.SelectedValue = lblItemCat_id.Text;
                    }
                    txtSubSectionName.Text = lblSubSectionName.Text;
                    GetSection();
                    ddlSection.SelectedValue = lblMOrPSection_id.Text;
                   
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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    #endregion=============end of button click funciton==================
}