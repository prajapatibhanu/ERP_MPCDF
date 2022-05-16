using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_LooseProductProductionEntry : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["Emp_ID"] != null)
        {
            try
            {
               
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    GetSectionView();
                    GetSectionDetail();
                    FillUnit();
                    string date = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate.Text = date.ToString();
                    txtDate.Attributes.Add("readonly", "readonly");
                    FillGrid();

                }
            }
            catch(Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDS.Enabled = false;


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
    protected void FillUnit()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpLooseProductionProductEntry",
                 new string[] { "flag" },
                 new string[] { "6" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlUnit.DataSource = ds.Tables[0];
                ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataValueField = "Unit_id";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
                


            }
            else
            {
                ddlUnit.Items.Clear();
                ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
                ddlUnit.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetSectionView()
    {

        try
        {

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID" },
                  new string[] { "6", ddlDS.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.SelectedValue = "2";
                ddlPSection.Enabled = false;

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

   
    protected void ClearText()
    {

        ddlUnit.ClearSelection();
        txtRemark.Text = "";
        ddlVariant.ClearSelection();
        txtQuantity.Text = "";
        txtBatchNo.Text = "";
        txtLotNo.Text = "";
    }
    protected void FillGrid()
    {
        try
        {
            Gridview1.DataSource = new string[] { };
            Gridview1.DataBind();
            ds = objdb.ByProcedure("SpLooseProductionProductEntry", new string[] { "flag", "Date", "Office_ID" }, new string[] { "3", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"),ddlDS.SelectedValue }, "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Gridview1.DataSource = ds;
                Gridview1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSectionDetail();
    }
    private void GetSectionDetail()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpLooseProductionProductEntry", new string[] { "flag", "ProductSection_ID", "Office_ID" }, new string[] {"4",ddlPSection.SelectedValue,ddlDS.SelectedValue }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    ddlVariant.DataSource = ds.Tables[0];
                    ddlVariant.DataTextField = "ItemTypeName";
                    ddlVariant.DataValueField = "ItemType_id";
                    ddlVariant.DataBind();
                    ddlVariant.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlVariant.Items.Clear();
                    ddlVariant.Items.Insert(0, new ListItem("Select", "0"));
                    ddlVariant.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditDetail")
            {
                string LoosePP_Id = e.CommandArgument.ToString();
                ViewState["LoosePP_Id"] = LoosePP_Id.ToString();
                ds = null;
                ds = objdb.ByProcedure("SpLooseProductionProductEntry", new string[] { "flag", "LoosePP_Id" }, new string[] { "2", LoosePP_Id }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtDate.Enabled = false;
                        ddlVariant.ClearSelection();
                        ddlVariant.Items.FindByValue(ds.Tables[0].Rows[0]["ItemType_id"].ToString()).Selected = true;
                        ddlVariant.Enabled = false;
                        ddlUnit.ClearSelection();
                        ddlUnit.Items.FindByValue(ds.Tables[0].Rows[0]["Unit_id"].ToString()).Selected = true;
                        txtQuantity.Text = ds.Tables[0].Rows[0]["Cr"].ToString();
                        txtBatchNo.Text = ds.Tables[0].Rows[0]["Batch_No"].ToString();
                        txtLotNo.Text = ds.Tables[0].Rows[0]["Lot_No"].ToString();
                        txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                        btnSave.Text = "Update";
                    }
                    else
                    {
                        
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if(ddlVariant.SelectedIndex == 0)
            {
                msg += "Select Product.\\n";
            }
            if (txtQuantity.Text == "")
            {
                msg += "Enter Quantity";
            }
            if (ddlUnit.SelectedIndex == 0)
            {
                msg += "Select Unit.\\n";
            }
            string IsActive = "1";
            if(msg != "0")
            {
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("SpLooseProductionProductEntry", new string[] { "flag", "Office_ID", "ProductSection_ID", "Date", "Cr", "Dr", "Unit_id", "ItemType_id", "Batch_No", "Lot_No", "Remark", "CreatedBy", "CreatedBy_IP", "IsActive" }, new string[] { "0", ddlDS.SelectedValue, ddlPSection.SelectedValue, Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), txtQuantity.Text, "0",ddlUnit.SelectedValue, ddlVariant.SelectedValue, txtBatchNo.Text, txtLotNo.Text,txtRemark.Text, objdb.createdBy(), "", IsActive }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                FillGrid();
                                ClearText();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            }
                            else
                            {
                                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error :" + error);
                            }
                        }
                    }

                }
                if (btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("SpLooseProductionProductEntry", new string[] { "flag", "LoosePP_Id", "Cr", "Unit_id", "Batch_No", "Lot_No", "Remark", "CreatedBy", "CreatedBy_IP" }, new string[] { "5", ViewState["LoosePP_Id"].ToString(), txtQuantity.Text,ddlUnit.SelectedValue, txtBatchNo.Text, txtLotNo.Text,txtRemark.Text, objdb.createdBy(), "" }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                FillGrid();
                                ClearText();
                                btnSave.Text = "Save";
                                ddlVariant.Enabled = true;
                                txtDate.Enabled = true;
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            }
                            else
                            {
                                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error :" + error);
                            }
                        }
                    }

                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
            



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            Gridview1.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}