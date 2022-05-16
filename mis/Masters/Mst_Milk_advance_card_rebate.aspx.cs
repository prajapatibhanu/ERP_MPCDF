using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Masters_Mst_Milk_advance_card_rebate : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2 = new DataSet();
    string effectivedate = "", effectivedate2 = ""; Int32 totalqty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCategory();
                // GetItemByCategory();
                GetLocation();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetLocation()
    {
        try
        {
            ddlLocation.DataTextField = "AreaName";
            ddlLocation.DataValueField = "AreaId";
            ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
           
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
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds1.Tables[0];
                ddlItemCategory.DataBind();
                ddlItemCategory.SelectedValue = objdb.GetMilkCatId();
                ddlItemCategory.Enabled = false;
            }
            else
            {
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void GetItemByCategory()
    {
        try
        {

          
            ds2 = objdb.ByProcedure("USP_Mst_SetItem_Rebate",
                     new string[] { "flag", "Office_ID", "ItemCat_id" },
                     new string[] { "2", objdb.Office_ID(), ddlItemCategory.SelectedValue }, "dataset");
            if (ds2.Tables.Count > 1)
            {
                if (ds2.Tables[0].Rows.Count > 0 && ds2.Tables[1].Rows.Count > 0)
                {
                    GridView1.Visible = true;
                    GridView1.DataSource = ds2;
                    GridView1.DataBind();
                    pnlproduct.Visible = true;
                    pnlbtn.Visible = true;
                    btnSubmit.Text = ds2.Tables[1].Rows[0]["Msg"].ToString();
                }
                else
                {
                    GridView1.Visible = false;
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Item Not Exist For Category - " + ddlItemCategory.SelectedItem.Text);
                }
            }
            else
            {
                GridView1.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Item Not Exist For Category - " + ddlItemCategory.SelectedItem.Text);
            }
               
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetItemByCategory();

            //GetProductSaleDetails();
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
         try
        {
        btnSubmit_Click(sender, e);
        }
         catch (Exception ex)
         {
             lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
         }
         finally
         {
             if (ds2 != null) { ds2.Dispose(); }
         }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlproduct.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        pnlbtn.Visible = false;


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (btnSubmit.Text == "Save")
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");
                    TextBox txtRebate_Amount = (TextBox)row.FindControl("txtRebate_Amount");
                    TextBox txtEffectiveDate = (TextBox)row.FindControl("txtEffectiveDate");
                    DateTime date2 = DateTime.ParseExact(txtEffectiveDate.Text, "dd/MM/yyyy", culture);
                    string EffectiveDate = date2.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    ds2 = objdb.ByProcedure("USP_Mst_SetItem_Rebate",
                             new string[] { "flag", "Office_ID", "ItemCat_id", "Item_id", "Rebate_Amount", "EffectiveDate", "CreatedBy" },
                             new string[] { "3", objdb.Office_ID(), ddlItemCategory.SelectedValue, lblItem_id.Text, txtRebate_Amount.Text, EffectiveDate, objdb.createdBy() }, "dataset");

                   
                }
                if (int.Parse(ds2.Tables[0].Rows[0]["record"].ToString()) == GridView1.Rows.Count)
                {
                    btnSearch_Click(sender, e);
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-success","Success", "Record Inserted Successfully");

                }

            }
            else if (btnSubmit.Text == "Update")
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label lblItemRebate_id = (Label)row.FindControl("lblItemRebate_id");
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");
                    TextBox txtRebate_Amount = (TextBox)row.FindControl("txtRebate_Amount");
                    TextBox txtEffectiveDate = (TextBox)row.FindControl("txtEffectiveDate");

                    DateTime date2 = DateTime.ParseExact(txtEffectiveDate.Text, "dd/MM/yyyy", culture);
                    string EffectiveDate = date2.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    ds2 = objdb.ByProcedure("USP_Mst_SetItem_Rebate",
                             new string[] { "flag", "Office_ID", "ItemCat_id", "MorP_Advancedcard_Rebate_Id", "Item_id", "Rebate_Amount", "EffectiveDate", "CreatedBy" },
                             new string[] { "4", objdb.Office_ID(), ddlItemCategory.SelectedValue, lblItemRebate_id.Text, lblItem_id.Text, txtRebate_Amount.Text, EffectiveDate, objdb.createdBy() }, "dataset");
                }
                if (int.Parse(ds2.Tables[0].Rows[0]["record"].ToString()) == GridView1.Rows.Count)
                {
                    btnSearch_Click(sender, e);
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success", "Record Updated Successfully");

                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
}