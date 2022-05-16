using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
public partial class mis_dailyplan_BulkMileSaletoUnionandThirdParty : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {

                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    
                    string date = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate.Text = date.ToString();
                    HideshowUnionOrThirdParty();
                    txtDate.Attributes.Add("readonly", "readonly");
                    FillGrid();
                }
            }
            catch (Exception ex)
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
                ddlDS.Items.Clear();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.DataBind();
            }
            DataSet ds1 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "1",objdb.Office_ID() }, "dataset");

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ddlUnion.DataSource = ds1.Tables[0];
                ddlUnion.DataTextField = "Office_Name";
                ddlUnion.DataValueField = "Office_ID";
                ddlUnion.DataBind();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));



            }
            else
            {
                ddlUnion.Items.Clear();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));
                ddlUnion.DataBind();
            }
            DataSet ds2 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                 new string[] { "flag","Office_ID"},
                 new string[] { "2", objdb.Office_ID() }, "dataset");

            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ddlThirdparty.DataSource = ds2.Tables[0];
                ddlThirdparty.DataTextField = "ThirdPartyUnion_Name";
                ddlThirdparty.DataValueField = "ThirdPartyUnion_Id";
                ddlThirdparty.DataBind();
                ddlThirdparty.Items.Insert(0, new ListItem("Select", "0"));



            }
            else
            {
                ddlThirdparty.Items.Clear();
                ddlThirdparty.Items.Insert(0, new ListItem("Select", "0"));
                ddlThirdparty.DataBind();
            }
            DataSet ds3 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                 new string[] { "flag" },
                 new string[] { "4" }, "dataset");

            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ddlMDP.DataSource = ds3.Tables[0];
                ddlMDP.DataTextField = "Office_Name";
                ddlMDP.DataValueField = "Office_ID";
                ddlMDP.DataBind();
                ddlMDP.Items.Insert(0, new ListItem("Select", "0"));



            }
            else
            {
                ddlMDP.Items.Clear();
                ddlMDP.Items.Insert(0, new ListItem("Select", "0"));
                ddlMDP.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void HideshowUnionOrThirdParty()
    {
        try
        {
            ddlThirdparty.ClearSelection();
            ddlUnion.ClearSelection();
            ddlMDP.ClearSelection();
            lblMsg.Text = "";
            if(rbtnTransferType.SelectedIndex == 0)
            {
                union.Visible = true;
                thirdparty.Visible = false;
                MDP.Visible = false;

            }
            else if (rbtnTransferType.SelectedIndex == 1)
            {
                MDP.Visible = false;
                union.Visible = false;
                thirdparty.Visible = true;
            }
            else
            {
                MDP.Visible = true;
                union.Visible = false;
                thirdparty.Visible = false;
            }
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void rbtnsaleto_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideshowUnionOrThirdParty();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string IsActive = "1";
            string msg = "";
            if (rbtnTransferType.SelectedIndex == 0)
            {
                if (ddlUnion.SelectedIndex == 0)
                {
                    msg += "Select Union.\\n";
                }

            }
            else if (rbtnTransferType.SelectedIndex == 1)
            {
                if (ddlThirdparty.SelectedIndex == 0)
                {
                    msg += "Select Third Party.\\n";
                }
            }
            else
            {
                if (ddlMDP.SelectedIndex == 0)
                {
                    msg += "Select MDP.\\n";
                }
            }
            if(txtQuantity.Text.Trim() == "")
            {
                msg += "Enter Quantity.\\n";
            }
            if (txtSNF.Text.Trim() == "")
            {
                msg += "Enter SNF.\\n";
            }
            if (txtFAT.Text.Trim() == "")
            {
                msg += "Enter FAT.\\n";
            }
            string SaleToOffice_Id =  "";
            if (rbtnTransferType.SelectedIndex == 0)
            {
                SaleToOffice_Id = ddlUnion.SelectedValue;
            }
            else if (rbtnTransferType.SelectedIndex == 1)
            {
                SaleToOffice_Id = ddlThirdparty.SelectedValue;
            }
            else
            {
                SaleToOffice_Id = ddlMDP.SelectedValue;
            }
            if(msg == "")
            {
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty", new string[] { "flag", "Date", "SaleFromOffice_Id", "SaleToOffice_Id", "MilkTrasferType", "Quantity", "FAT", "SNF", "Remark", "IsActive", "CreatedBy", "CreatedByIP" },
                                                                                new string[] { "0", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ddlDS.SelectedValue, SaleToOffice_Id, rbtnTransferType.SelectedValue, txtQuantity.Text, txtFAT.Text, txtSNF.Text,txtRemark.Text, IsActive, objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
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
                    FillGrid();

                }
                else if(btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty", new string[] { "flag", "BilkMilkSale_Id", "Date", "Quantity", "FAT", "SNF", "Remark", "IsActive", "CreatedBy", "CreatedByIP" },
                                                                                new string[] { "7",ViewState["BilkMilkSale_Id"].ToString(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), txtQuantity.Text, txtFAT.Text, txtSNF.Text, txtRemark.Text, IsActive, objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
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
                    ClearText();
                    btnSave.Text = "Save";
                    ddlUnion.Enabled = true;
                    ddlMDP.Enabled = true;
                    ddlThirdparty.Enabled = true;
                    rbtnTransferType.Enabled = true;
                    FillGrid();
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction()", "alert('" + msg + "')", true);
            }
       
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        txtFAT.Text = "";
        txtSNF.Text = "";
        ddlUnion.ClearSelection();
        ddlThirdparty.ClearSelection();
        ddlMDP.ClearSelection();
        txtQuantity.Text = "";
        txtRemark.Text = "";
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            string TransferToffice_ID = "";
            string MilkTransferTypeID = rbtnTransferType.SelectedValue;
            string TransferFromOffice_ID = ViewState["Office_ID"].ToString();
            if (MilkTransferTypeID == "1")
            {
                TransferToffice_ID = ddlUnion.SelectedValue;
            }
            else if (MilkTransferTypeID == "2")
            {
                TransferToffice_ID = ddlThirdparty.SelectedValue;
            }
            else
            {
                TransferToffice_ID = ddlMDP.SelectedValue;
            }


            ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                new string[] { "flag", "SaleFromOffice_Id ", "MilkTrasferType", "Date"},
                new string[] { "5", TransferFromOffice_ID,  MilkTransferTypeID, Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")}, "dataset");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditRecord")
            {
                string BilkMilkSale_Id = e.CommandArgument.ToString();
                ViewState["BilkMilkSale_Id"] = BilkMilkSale_Id.ToString();
                
                ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty", new string[] { "flag", "BilkMilkSale_Id" }, new string[] { "6", BilkMilkSale_Id }, "dataset");
                if(ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    rbtnTransferType.Enabled = false;
                    txtDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                    string MilkTrasferType = ds.Tables[0].Rows[0]["MilkTrasferType"].ToString();
                    if(MilkTrasferType == "1")
                    {
                        ddlUnion.ClearSelection();
                        ddlUnion.Items.FindByValue(ds.Tables[0].Rows[0]["SaleToOffice_Id"].ToString()).Selected = true;
                        ddlUnion.Enabled = false;
                    }
                    else if (MilkTrasferType == "2")
                    {
                        ddlThirdparty.ClearSelection();
                        ddlThirdparty.Items.FindByValue(ds.Tables[0].Rows[0]["SaleToOffice_Id"].ToString()).Selected = true;
                        ddlThirdparty.Enabled = false;
                    }
                    else
                    {
                        ddlMDP.ClearSelection();
                        ddlMDP.Items.FindByValue(ds.Tables[0].Rows[0]["SaleToOffice_Id"].ToString()).Selected = true;
                        ddlMDP.Enabled = false;
                    }
                    txtQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    txtFAT.Text = ds.Tables[0].Rows[0]["FAT"].ToString();
                    txtSNF.Text = ds.Tables[0].Rows[0]["SNF"].ToString();
                    txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                    
                    btnSave.Text = "Update";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}