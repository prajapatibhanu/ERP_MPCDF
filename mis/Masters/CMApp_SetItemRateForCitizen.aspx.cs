using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Data;


public partial class mis_Masters_CMApp_SetItemRateForCitizen : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2 = new DataSet();
    string effectivedate = ""; Int32 totalqty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
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
    
    
    
    private void GetItemSaleDetails()
    {
        try
        {

            ds1 = objdb.ByProcedure("USP_CMApp_Mst_SetItemRateForCitizen",
                     new string[] { "Flag", "Office_ID", "ItemCat_id" },
                     new string[] { "1", objdb.Office_ID(), ddlItemCategory.SelectedValue }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();

                lblMsg.Text = string.Empty;
                pnlbtn.Visible = true;
                pnlproduct.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                pnlproduct.Visible = false;
                pnlbtn.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Item Not Exist For Category - " + ddlItemCategory.SelectedItem.Text);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2: ", ex.Message.ToString());
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

                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {


                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");

                        if (CheckBox1.Checked == true)
                        {
                            Checkboxstatus = 1;

                            TextBox txtItemRate = (TextBox)row.FindControl("txtItemRate");


                            if (txtItemRate.Text == "" || txtItemRate.Text == "0" || txtItemRate.Text == "0.00")
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
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Checked Item Rate Can't Empty / 0 / 0.00");
                        return;
                    }




                    if (GridView1.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");

                            Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                            Label lblItem_id = (Label)row.FindControl("lblItem_id");

                            TextBox txtItemRate = (TextBox)row.FindControl("txtItemRate");
                            TextBox txtEffectiveDate = (TextBox)row.FindControl("txtEffectiveDate");



                            if (CheckBox1.Checked == true)
                            {
                                if (txtItemRate.Text != "" && txtItemRate.Text != "0" && txtItemRate.Text != "0.00" && txtEffectiveDate.Text != "")
                                {
                                    
                                    DateTime date3 = DateTime.ParseExact(txtEffectiveDate.Text, "dd/MM/yyyy", culture);
                                    effectivedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                                    ds2 = objdb.ByProcedure("USP_CMApp_Mst_SetItemRateForCitizen",
                                               new string[] { "Flag", "ItemCat_id", "Item_id" ,"Office_ID", "ItemRate", "EffectiveDate", "CreatedBy", "CreatedByIP" },
                                               new string[] { "2", lblItemCat_id.Text, lblItem_id.Text, objdb.Office_ID(), txtItemRate.Text.Trim(), effectivedate.ToString(), objdb.createdBy(), "" }, "dataset");

                                }
                                //else
                                //{
                                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Effective Date or Item Rate ");
                                //    return;

                                //}
                            }

                        }

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetItemSaleDetails();

                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetItemSaleDetails();


                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                       
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  3 : " + ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }

    }
    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetCategory();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetItemSaleDetails();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertRate();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlproduct.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        pnlbtn.Visible = false;
        ddlItemCategory.SelectedIndex = 0;
    }
}