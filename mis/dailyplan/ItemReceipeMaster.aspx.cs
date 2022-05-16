using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_ItemReceipeMaster : System.Web.UI.Page
{
    DataSet ds, ds1, ds2, ds3, ds4;
    static DataSet ds5;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    FillItem();
                    FillProductSection();
                    FillMappingItem();
                    FillUnit();
                    ddlUnit.Attributes.Add("readonly", "readonly");
                    ddlUnit.Enabled = false;
                    ViewState["Recepie_ID"] = "0";
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
    protected void FillProductSection()
    {
        try
        {

            lblMsg.Text = "";
            ddlProductSection.Items.Clear();
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "0",ddlDS.SelectedValue.ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlProductSection.DataSource = ds.Tables[0];
                ddlProductSection.DataTextField = "ProductSection_Name";
                ddlProductSection.DataValueField = "ProductSection_ID";
                ddlProductSection.DataBind();
               
            }
            ddlProductSection.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillItem()
    {
        try
        {
            lblMsg.Text = "";
            ddlItem.Items.Clear();
            ds = objdb.ByProcedure("SpProductionRecepie_Master",
                 new string[] { "flag" },
                 new string[] { "6" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlItem.DataSource = ds.Tables[0];
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "Item_id";
                ddlItem.DataBind();
            }
            ddlItem.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
         
         lblMsg.Text = "";
         string msg = "";
         string Recepie_IsActive = "1";
         if(ddlDS.SelectedIndex == 0)
         {
             msg = "Select Dugdh Sangh";
         }
         if (ddlProductSection.SelectedIndex == 0)
         {
             msg = "Select Product Section";
         }
         if (ddlProduct.SelectedIndex == 0)
         {
             msg = "Select Product/Item";
         }
         if (ddlItem.SelectedIndex == 0)
         {
             msg = "Select Item";
         }
         if (txtQuantity.Text == "")
         {
             msg = "Enter Quantity";
         }
         if (msg.Trim() == "")
         {
             int Status = 0;
             ds = objdb.ByProcedure("SpProductionRecepie_Master", new string[] { "flag", "Product_ID", "Office_ID", "ProductSection_ID", "Item_ID", "Recepie_ID" }, new string[] { "8", ddlProduct.SelectedValue.ToString(), ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ddlItem.SelectedItem.Value.ToString(), ViewState["Recepie_ID"].ToString() }, "dataset");
             if(ds != null && ds.Tables[0].Rows.Count > 0)
             {
                 Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
             }
             if (btnSubmit.Text == "Submit" && ViewState["Recepie_ID"].ToString() == "0" && Status == 0)
             {

                 objdb.ByProcedure("SpProductionRecepie_Master", 
                                    new string[] {"flag", "Product_ID", "Office_ID", "ProductSection_ID", "Item_ID", "Item_Quantity", "Item_Quantity_Unit", "Recepie_IsActive", "Recepie_UpdatedBy" },
                                    new string[] { "0", ddlProduct.SelectedValue.ToString(), ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ddlItem.SelectedValue.ToString(), txtQuantity.Text, ddlUnit.SelectedValue.ToString(), Recepie_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");         
               lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
               ClearData();
               FillGrid();
                  
               
             }  
             else if(btnSubmit.Text == "Update" && ViewState["Recepie_ID"].ToString() != "0" && Status == 0)
             {
                 objdb.ByProcedure("SpProductionRecepie_Master",
                                   new string[] { "flag", "Recepie_ID", "Product_ID", "Office_ID", "ProductSection_ID", "Item_ID", "Item_Quantity", "Item_Quantity_Unit", "Recepie_UpdatedBy" },
                                   new string[] { "3", ViewState["Recepie_ID"].ToString(), ddlProduct.SelectedValue.ToString(), ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ddlItem.SelectedValue.ToString(), txtQuantity.Text, ddlUnit.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                 lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                 btnSubmit.Text = "Submit";
                 ClearData();
                 FillGrid();
             }
             else
             {
                 Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item Reciepe is already exist.');", true);
                 //ClearData();

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
    private void ClearData()
    {
        try
        {
          ddlItem.ClearSelection();
          FillUnit();
          txtQuantity.Text = "";
          ViewState["Recepie_ID"] = "0";         
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillGrid()
    {
        try
        {
            
            GridView1.DataSource = null;
            ds = objdb.ByProcedure("SpProductionRecepie_Master", new string[] { "flag","Product_ID", "Office_ID", "ProductSection_ID" }, new string[] { "5",ddlProduct.SelectedValue.ToString(),ddlDS.SelectedValue.ToString(),ddlProductSection.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
            }
            GridView1.DataBind();
            //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            //GridView1.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlDS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillProductSection();
            FillMappingItem();
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillMappingItem()
    {
        try
        {
            lblMsg.Text = "";
            ddlProduct.Items.Clear();
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping", new string[] { "flag", "Office_ID", "ProductSection_ID" }, new string[] {"4",ddlDS.SelectedValue.ToString(),ddlProductSection.SelectedValue.ToString() }, "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {

                ddlProduct.DataSource = ds.Tables[0];
                ddlProduct.DataTextField = "Product_Name";
                ddlProduct.DataValueField = "Product_ID";
                ddlProduct.DataBind();

            }
            ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlProductSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {            
            FillMappingItem();
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillUnit();
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
            //lblMsg.Text = "";
            ddlUnit.Items.Clear();
            ds = objdb.ByProcedure("SpProductionRecepie_Master", new string[] { "flag", "Item_id" }, new string[] { "7", ddlItem.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                ddlUnit.DataSource = ds.Tables[0];
                ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataValueField = "Unit_id";
                ddlUnit.DataBind();

            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Recepie_ID = GridView1.SelectedDataKey.Value.ToString();
            ViewState["Recepie_ID"] = Recepie_ID.ToString();
            DataSet ds11 = objdb.ByProcedure("SpProductionRecepie_Master", new string[] { "flag", "Recepie_ID" }, new string[] { "2", Recepie_ID }, "dataset");
            if (ds11 != null)
            {
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    ddlDS.ClearSelection();
                    ddlDS.Items.FindByValue(ds11.Tables[0].Rows[0]["Office_ID"].ToString()).Selected = true;
                    FillProductSection();
                    ddlProductSection.ClearSelection();
                    ddlProductSection.Items.FindByValue(ds11.Tables[0].Rows[0]["ProductSection_ID"].ToString()).Selected = true;
                    FillMappingItem();
                    ddlProduct.ClearSelection();
                    ddlProduct.Items.FindByValue(ds11.Tables[0].Rows[0]["Product_ID"].ToString()).Selected = true;
                    ddlItem.ClearSelection();
                    ddlItem.Items.FindByValue(ds11.Tables[0].Rows[0]["Item_ID"].ToString()).Selected = true;
                    FillUnit();
                    ddlUnit.ClearSelection();
                    ddlUnit.Items.FindByValue(ds11.Tables[0].Rows[0]["Item_Quantity_Unit"].ToString()).Selected = true;
                    txtQuantity.Text = ds11.Tables[0].Rows[0]["Item_Quantity"].ToString();
                }      
                btnSubmit.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Recepie_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpProductionRecepie_Master", new string[] { "flag", "Recepie_ID", "Recepie_IsActive", "Recepie_UpdatedBy" }, new string[] { "4", Recepie_ID, "0", ViewState["Emp_ID"].ToString() }, "dataset");
            FillGrid();
            ClearData();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
}