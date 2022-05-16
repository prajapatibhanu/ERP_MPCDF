using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Web.UI;


public partial class mis_MilkCollection_MCRateChartMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();

                GetItemCategory(sender, e);

                FillOffice();
                txtTransactionDt.Attributes.Add("readonly", "readonly");
                txtTransactionDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BindSaleRate();

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

    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtsocietyName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();

                }

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

    protected void ddlitemcategory_Init(object sender, EventArgs e)
    {
        try
        {
            ddlitemcategory.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlitemtype_Init(object sender, EventArgs e)
    {
        try
        {
            ddlitemtype.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("MCRateChartMaster.aspx", false);
    }

    private void GetItemCategory(object sender, EventArgs e)
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_MstLocalSale",
                         new string[] { "flag" },
                         new string[] { "4" }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlitemcategory.DataTextField = "ItemCatName";
                        ddlitemcategory.DataValueField = "ItemCat_id";
                        ddlitemcategory.DataSource = ds;
                        ddlitemcategory.DataBind();
                        ddlitemcategory.Items.Insert(0, new ListItem("Select", "0"));
                        ddlitemcategory.SelectedValue = objdb.DcsRawMilkItemCategoryId_ID();
                        ddlitemcategory.Enabled = false;
                        ddlitemcategory_SelectedIndexChanged(sender, e);

                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void ddlitemcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlitemcategory.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Sp_MstLocalSale",
                             new string[] { "flag", "ItemCat_id" },
                             new string[] { "5", ddlitemcategory.SelectedValue }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlitemtype.DataTextField = "ItemTypeName";
                            ddlitemtype.DataValueField = "ItemType_id";
                            ddlitemtype.DataSource = ds;
                            ddlitemtype.DataBind();
                            ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
                             ddlitemtype.SelectedValue = objdb.DcsRawMilkItemTypeId_ID();
                           // ddlitemtype.SelectedValue = "10001";
                            ddlitemtype.Enabled = false;
                            gvItemDetails.DataSource = string.Empty;
                            gvItemDetails.DataBind();
                            divIteminfo.Visible = false;
                            divpageAction.Visible = false;
                            ddlitemtype_SelectedIndexChanged(sender, e);
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Type Not Exist For Category - " + ddlitemcategory.SelectedItem.Text);
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Type Not Exist For Category - " + ddlitemcategory.SelectedItem.Text);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Type Not Exist For Category - " + ddlitemcategory.SelectedItem.Text);
                }
            }
            else
            {

                ddlitemtype.Items.Clear();
                ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));

                gvItemDetails.DataSource = string.Empty;
                gvItemDetails.DataBind();
                divIteminfo.Visible = false;
                divpageAction.Visible = false;


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlitemcategory.SelectedValue != "0" && ddlitemtype.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("USP_Mst_MCRateChartMaster",
                             new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID" },
                             new string[] { "3", ddlitemcategory.SelectedValue, ddlitemtype.SelectedValue, objdb.Office_ID() }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            divIteminfo.Visible = true;
                            divpageAction.Visible = true;

                            gvItemDetails.DataSource = ds;
                            gvItemDetails.DataBind();

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text);
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text);
                }
            }
            else
            {
                gvItemDetails.DataSource = string.Empty;
                gvItemDetails.DataBind();
                divIteminfo.Visible = false;
                divpageAction.Visible = false;

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    private void Clear()
    {
        lblMsg.Text = string.Empty;
        ddlitemtype.Items.Clear();
        ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
        ddlitemtype.SelectedIndex = 0;
        ddlitemcategory.SelectedIndex = 0;
        btnSubmit.Text = "Save";


    }

    private void BindSaleRate()
    {
        try
        {
            lblMsg.Text = "";

            ds = null;
            ds = objdb.ByProcedure("USP_Mst_MCRateChartMaster",
                                        new string[] { "flag", "Office_Id" },
                                        new string[] { "4", objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                gvItemSaleRate.DataSource = ds;
                gvItemSaleRate.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                gvItemSaleRate.DataSource = null;
                gvItemSaleRate.DataBind();
            }
            ds.Clear();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (gvItemSaleRate.Rows.Count > 0)
            {
                gvItemSaleRate.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvItemSaleRate.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }


    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                int Checkboxstatus = 0;
                int CheckBlankVal = 0;
                ds = null;

                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {


                    foreach (GridViewRow row in gvItemDetails.Rows)
                    {
                        CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");

                        if (CheckBox1.Checked == true)
                        {
                            Checkboxstatus = 1;
                            TextBox txtDeductionRate1 = (TextBox)row.FindControl("txtDeductionRate1");
                            TextBox txtDeductionRate2 = (TextBox)row.FindControl("txtDeductionRate2");
                            TextBox txtPurchaseRate = (TextBox)row.FindControl("txtPurchaseRate");

                            if (txtDeductionRate1.Text == "" || txtDeductionRate1.Text == "0" || txtDeductionRate1.Text == "0.00"
                                && txtDeductionRate2.Text == "" || txtDeductionRate2.Text == "0" || txtDeductionRate2.Text == "0.00"
                                && txtPurchaseRate.Text == "" || txtPurchaseRate.Text == "0" || txtPurchaseRate.Text == "0.00")
                            {
                                CheckBlankVal = 1;
                            }

                        }
                    }

                    if (Checkboxstatus == 0)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Select At Least One CheckBox Row");
                        return;
                    }


                    if (CheckBlankVal == 1)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Checked Item MRP Rate Can't Empty / 0 / 0.00");
                        return;
                    }




                    if (gvItemDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvItemDetails.Rows)
                        {
                            CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");

                            Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                            Label lblItemType_id = (Label)row.FindControl("lblItemType_id");
                            Label lblItem_id = (Label)row.FindControl("lblItem_id");
                            Label lblUnit_id = (Label)row.FindControl("lblUnit_id");

                            TextBox txtPurchaseRate = (TextBox)row.FindControl("txtPurchaseRate");
                            TextBox txtFactorRate = (TextBox)row.FindControl("txtFactorRate");
                            TextBox txtCowRate = (TextBox)row.FindControl("txtCowRate");
                            TextBox txtDeductionRate1 = (TextBox)row.FindControl("txtDeductionRate1");
                            TextBox txtDeductionRate2 = (TextBox)row.FindControl("txtDeductionRate2");


                            TextBox txtEffectiveDate = (TextBox)row.FindControl("txtEffectiveDate");



                            if (CheckBox1.Checked == true)
                            {
                                if (txtPurchaseRate.Text == "" || txtPurchaseRate.Text == "0"
                                    && txtEffectiveDate.Text == "" || txtEffectiveDate.Text == "0")
                                {

                                }
                                else
                                {
                                    DateTime date3 = DateTime.ParseExact(txtEffectiveDate.Text, "dd/MM/yyyy", cult);

                                    ds = objdb.ByProcedure("USP_Mst_MCRateChartMaster",
                                               new string[] { "flag", "ItemCat_id", "ItemType_id", "Item_id", "Unit_id", "Office_ID", "PurchaseRate", "FactorforCowRate", "CowRate", "DeductionRate1", "DeductionRate2", "EffectiveDate", "CreatedBy" },
                                               new string[] { "0", lblItemCat_id.Text, lblItemType_id.Text, lblItem_id.Text, lblUnit_id.Text, objdb.Office_ID(), txtPurchaseRate.Text,txtFactorRate.Text,txtCowRate.Text, txtDeductionRate1.Text, txtDeductionRate2.Text, date3.ToString(), objdb.createdBy() }, "dataset");

                                }
                            }

                        }

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString(); 
                            Clear(); 
                            gvItemDetails.DataSource = string.Empty;
                            gvItemDetails.DataBind();
                            divIteminfo.Visible = false;
                            divpageAction.Visible = false;
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            BindSaleRate();
                            GetItemCategory(sender, e);
                            Clear();
                            ddlitemtype.Items.Clear();
                            ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
                            gvItemDetails.DataSource = string.Empty;
                            gvItemDetails.DataBind();
                            divIteminfo.Visible = false;
                            divpageAction.Visible = false;
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        ds.Clear();
                        GetItemCategory(sender, e);
                        BindSaleRate(); 
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





}