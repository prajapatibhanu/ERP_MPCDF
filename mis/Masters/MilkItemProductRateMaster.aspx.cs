using System;
using System.IO;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Masters_MilkItemProductRateMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    IFormatProvider culture = new CultureInfo("en-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetProductMasterDetails();
            }
        }
        else
        {
            //objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    #region=======================user defined function========================

    private void Clear()
    {
        ddlProductName.SelectedIndex = 0;
        ddlProductName.ClearSelection();
        ddlProductCategory.SelectedIndex = 0;
        //ddlUnit.SelectedIndex = 0;
        txtPacketSize.Text = string.Empty;
        txtEffectiveDate.Text = string.Empty;
        txtMRPRate.Text = string.Empty;
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
        ddlProductCategory.Enabled = true;
        ddlProductName.Enabled = true;
        //ddlUnit.Enabled = true;
        txtPacketSize.Enabled = true;
        GetProductMasterDetails();
    }
    private void GetOfficeType()
    {
        try
        {
            ddlOfficeType.DataSource = objddl.OfficeTypeFill().Tables[1];
            ddlOfficeType.DataTextField = "OfficeTypeName";
            ddlOfficeType.DataValueField = "OfficeType_ID";
            ddlOfficeType.DataBind();

            if (objdb.OfficeType_ID() == objdb.GetHOType_Id().ToString())
            {
                for (int i = ddlOfficeType.Items.Count - 1; i >= 0; i--)
                {
                    if (ddlOfficeType.Items[i].Value != objdb.GetDSType_Id().ToString()) //for show only dugdh sangh
                    {
                        ddlOfficeType.Items.RemoveAt(i);
                    }
                }
            }
            else if (objdb.OfficeType_ID() == objdb.GetDSType_Id().ToString())
            {
                for (int i = ddlOfficeType.Items.Count - 1; i >= 0; i--)
                {
                    if (ddlOfficeType.Items[i].Value != objdb.OfficeType_ID().ToString()) //for show only dugdh sangh
                    {
                        ddlOfficeType.Items.RemoveAt(i);
                    }
                }
            }

            ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));

            //if(objdb.Office_ID)
            //ddlOfficeType.Items.Remove(ddlOfficeType.Items.FindByValue("1"));
            //ddlOfficeType.Items.Remove(ddlOfficeType.Items.FindByValue(objdb.OfficeType_ID()));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GetDivision()
    {
        try
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_ID";
            ddlDivision.DataSource = objdb.ByProcedure("SpAdminDivision",
                    new string[] { "flag", "State_ID" },
                    new string[] { "7", "12" }, "dataset");
            ddlDivision.DataBind();
            ddlDivision.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetOfficeTypeCode()
    {
        try
        {
            if (ds != null)
            {
                ds.Clear();
            }
            ds = objdb.ByProcedure("SpAdminOfficeType",
                           new string[] { "flag", "OfficeType_ID" },
                           new string[] { "1", ddlOfficeType.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["OfficeTypeCode"] = ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString();
                GetOffice();
            }
            else
            {
                GetOffice();
            }
            ds.Clear();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void GetOffice()
    {
        if (ddlOfficeType.SelectedIndex > 0)
        {
            ddlOffice.Enabled = true;
            if (ds != null)
            {
                ds.Clear();
            }
            ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag", "OfficeType_ID" },
                           new string[] { "5", ddlOfficeType.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ds.Clear();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        else
        {
            ddlOffice.ClearSelection();
            ddlOffice.Enabled = false;
        }
    }

    protected void GetProductCategory()
    {
        try
        {
            ddlProductCategory.DataSource = objdb.ByProcedure("ProcCommTablesFill",
                   new string[] { "type" },
                   new string[] { "3" }, "dataset");
            ddlProductCategory.DataTextField = "ItemCatName";
            ddlProductCategory.DataValueField = "ItemCat_id";
            ddlProductCategory.DataBind();
            ddlProductCategory.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error: 1 " + ex.Message.ToString());
        }
    }
    protected void GetProduct()
    {
        try
        {
            if (ddlProductCategory.SelectedValue != "0")
            {
                ddlProductName.DataSource = objdb.ByProcedure("ProcCommTablesFill",
                  new string[] { "type", "ItemCat_id" },
                  new string[] { "4", ddlProductCategory.SelectedValue }, "dataset");
                ddlProductName.DataTextField = "ItemTypeName";
                ddlProductName.DataValueField = "ItemType_id";
                ddlProductName.DataBind();
                ddlProductName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error: 2 " + ex.Message.ToString());
        }
    }

    //protected void GetUnit()
    //{
    //    try
    //    {
    //        ddlUnit.DataSource = objddl.UnitFill();
    //        ddlUnit.DataTextField = "UQCCode";
    //        ddlUnit.DataValueField = "Unit_id";
    //        ddlUnit.DataBind();
    //        ddlUnit.Items.Insert(0, new ListItem("Select", "0"));

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error: 3 " + ex.Message.ToString());
    //    }
    //}
    //private void GetDatatableHeaderDesign()
    //{
    //    try
    //    {
    //        if (GridView1.Rows.Count > 0)
    //        {
    //            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
    //            GridView1.UseAccessibleHeader = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
    //    }
    //}
    private void GetProductMasterDetails()
    {
        try
        {

            GridView1.DataSource = objdb.ByProcedure("SptblPUProductVariant",
                    new string[] { "flag", "Office_ID" },
                    new string[] { "1", objdb.Office_ID() }, "dataset");
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                //GetDatatableHeaderDesign();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error: 4 " + ex.Message.ToString());
        }
    }
    private void InsertOrUpdateProduct()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string date3 = Convert.ToDateTime(txtEffectiveDate.Text.Trim(), culture).ToString("yyyy/MM/dd");
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("SptblPUProductVariant",
                            new string[] { "flag", "Cat_id", "ProductId"
                                        , "PacketSize", "MRPRate","Commission","SaleRate"
					                        ,"EffectiveDate"
                                        , "CreatedBy", "Office_ID", "CreatedBy_IP" },
                            new string[] { "2", ddlProductCategory.SelectedValue, ddlProductName.SelectedValue
                                , txtPacketSize.Text.Trim(), txtMRPRate.Text.Trim()
                                ,date3.ToString(), objdb.createdBy(), objdb.Office_ID()
                                ,objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetProductMasterDetails();
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error: 5 " + error);
                            }
                        }
                    }
                    else if (btnSubmit.Text == "Update")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("SptblPUProductVariant",
                                 new string[] { "flag", "VariantId", "MRPRate", "Commission", "SaleRate"
                                     , "EffectiveDate", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                 new string[] { "3", ViewState["rowid"].ToString(), txtMRPRate.Text.Trim(), date3.ToString(), objdb.createdBy()
                                , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Product Variant Record Updated" }, "dataset");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetProductMasterDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", ddlProductName.Text + " or " + txtPacketSize.Text.Trim() + " " + error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error: 6 " + error);
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Bank Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error: 7" + ex.Message.ToString());
            }
        }
    }
    #endregion =========end of user defined function==========

    #region=============== Init or changed event for controls =================

    protected void ddlProductCategory_Init(object sender, EventArgs e)
    {
        GetProductCategory();
    }
    //protected void ddlUnit_Init(object sender, EventArgs e)
    //{
    //    GetUnit();
    //}
    protected void ddlProductCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetProduct();
        //GetDatatableHeaderDesign();
    }
    protected void ddlOfficeType_Init(object sender, EventArgs e)
    {
        GetOfficeType();
    }
    protected void ddlOfficeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetOfficeTypeCode();
    }
    #endregion============ end of changed event for controls===========

    #region============ button click and gridview events event =========

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertOrUpdateProduct();
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
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblCat_id = (Label)row.FindControl("lblCat_id");
                    Label lblProductId = (Label)row.FindControl("lblProductId");
                    Label lblUnit = (Label)row.FindControl("lblUnit");
                    Label lblPacketSize = (Label)row.FindControl("lblPacketSize");
                    Label lblMRPRate = (Label)row.FindControl("lblMRPRate");
                    Label lblEffectiveDate = (Label)row.FindControl("lblEffectiveDate");
                    ViewState["rowid"] = e.CommandArgument;
                    ddlProductCategory.Enabled = false;
                    ddlProductName.Enabled = false;
                    //ddlUnit.Enabled = false;
                    txtPacketSize.Enabled = false;
                    ddlProductCategory.SelectedValue = lblCat_id.Text;
                    GetProduct();
                    ddlProductName.SelectedValue = lblProductId.Text;
                    //ddlUnit.SelectedValue = lblUnit.Text;
                    txtPacketSize.Text = lblPacketSize.Text;
                    txtMRPRate.Text = lblMRPRate.Text;
                    txtEffectiveDate.Text = lblEffectiveDate.Text;
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
                    // GetDatatableHeaderDesign();
                }
            }
            else if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    ds = objdb.ByProcedure("SptblPUProductVariant",
                                new string[] { "flag", "VariantId", "CreatedBy", "CreatedBy_IP"
                                    , "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Product Variant Record Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Clear();
                        GetProductMasterDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    ds.Clear();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
            else if (e.CommandName == "RecordView")
            {
                lblViewDetailHeader.Text = "Rate History";
                ds = objdb.ByProcedure("SptblPUProductVariant",
                         new string[] { "flag", "VariantId" },
                         new string[] { "6", e.CommandArgument.ToString() }, "dataset");

                gvRateHistroy.DataSource = ds;
                gvRateHistroy.DataBind();

                foreach (GridViewRow gvRow in GridView1.Rows)
                {
                    if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                    {
                        GridView1.SelectedIndex = gvRow.DataItemIndex;
                        GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                        break;
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewDetailPopup();", true);
                // GetDatatableHeaderDesign();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error: 8" + ex.Message.ToString());
        }
    }
    #endregion=============end of button click funciton==================

    protected void ddlDivision_Init(object sender, EventArgs e)
    {
        GetDivision();
    }
}