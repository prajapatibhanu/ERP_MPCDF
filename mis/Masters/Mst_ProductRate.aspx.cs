using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;


public partial class mis_Masters_Mst_ProductRate : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2 = new DataSet();
    string effectivedate = "", effectivedate2 = ""; Int32 totalqty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetCategory();
            GetItemByCategory();
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
                ddlItemCategory.SelectedValue = objdb.GetProductCatId();
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

            ds2 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                     new string[] { "flag", "Office_ID", "ItemCat_id" },
                       new string[] { "2", objdb.Office_ID(), ddlItemCategory.SelectedValue }, "dataset");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "Item_id";
                ddlItemName.DataSource = ds2.Tables[0];
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlItemName.Items.Insert(0, new ListItem("No Record Found", "0"));
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
    private void GetProductSaleDetails()
    {
        try
        {

            ds1 = objdb.ByProcedure("USP_Mst_DSProductRate",
                     new string[] { "Flag", "Office_ID", "ItemCat_id", "Item_id" },
                     new string[] { "1", objdb.Office_ID(), ddlItemCategory.SelectedValue,ddlItemName.SelectedValue }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0 && ddlItemName.SelectedValue != "0" && ddlItemCategory.SelectedValue==objdb.GetProductCatId())
            {
                GridView1.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
                lblMsg.Text = string.Empty;
                pnlbtn.Visible = true;
                pnlproduct.Visible = true;
            }
            else
            {
                GridView1.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                pnlproduct.Visible = false;
                pnlbtn.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Item Not Exist For Category - " + ddlItemCategory.SelectedItem.Text);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void InsertRate()
    {
        try
        {
            if (Page.IsValid)
            {
                int Checkboxstatus = 0;
                int CheckBlankVal = 0;
                ds2 = null;
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {


                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");

                        if (CheckBox1.Checked == true)
                        {
                            Checkboxstatus = 1;

                            TextBox txtIncludingGSTRate = (TextBox)row.FindControl("txtIncludingGSTRate");
                            TextBox txtEffectiveDate = (TextBox)row.FindControl("txtEffectiveDate");

                            if (txtIncludingGSTRate.Text == "" || txtEffectiveDate.Text=="")
                            {
                                CheckBlankVal = 1;
                            }
                        }
                    }

                    if (Checkboxstatus == 0)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Please Select At Least One CheckBox Row");
                        return;
                    }
                    if (CheckBlankVal == 1)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Checked Poduct Rate Can't Empty / 0 / 0.00 and Effective Date");
                        return;
                    }


                    if (GridView1.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");
                                                      
                            Label lblProductRateTypeId = (Label)row.FindControl("lblProductRateTypeId");
                            TextBox txtIncludingGSTRate = (TextBox)row.FindControl("txtIncludingGSTRate");
                            TextBox txtEffectiveDate = (TextBox)row.FindControl("txtEffectiveDate");

                            if (CheckBox1.Checked == true)
                            {
                                if (txtIncludingGSTRate.Text != "" && txtEffectiveDate.Text != "")
                                {
                                    
                                    DateTime date3 = DateTime.ParseExact(txtEffectiveDate.Text, "dd/MM/yyyy", culture);
                                    effectivedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                                    ds1 = objdb.ByProcedure("USP_Mst_DSProductRate",
                                               new string[] { "Flag", "ItemCat_id", "Item_id", "ProductRateTypeId", "RateIncludingGST", "EffectiveDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                                               new string[] { "2", ddlItemCategory.SelectedValue, ddlItemName.SelectedValue,lblProductRateTypeId.Text, txtIncludingGSTRate.Text.Trim(), effectivedate.ToString(), objdb.Office_ID(), objdb.createdBy(), IPAddress }, "dataset");

                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Distributor/Supersokist or Effective Date or Retailer Rate ");
                                    return;

                                }
                            }

                        }

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetProductSaleDetails();

                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetProductSaleDetails();


                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        ds1.Clear();
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! .", "Please Enter Date");
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            GetProductSaleDetails();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (ddlItemCategory.SelectedValue !="0" && ddlItemName.SelectedValue!="0")
            {
                InsertRate();
            }
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
}