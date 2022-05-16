using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;


public partial class mis_Masters_Mst_ItemSaleRate : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2,ds3 = new DataSet();
    string effectivedate = "",effectivedate2=""; Int32 totalqty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetCategory();
            GetLocation();
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
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds.Tables[0];
                ddlItemCategory.DataBind();
              //  ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
                ddlItemCategory.SelectedValue = objdb.GetMilkCatId();
            }
            else
            {
                ddlItemCategory.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void GetLocation()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_Area",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlLocation.DataTextField = "AreaName";
                ddlLocation.DataValueField = "AreaId";
                ddlLocation.DataSource = ds.Tables[0];
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetRoute()
    {
        try
        {
            ddlRoute.Items.Clear();
            //ds = objdb.ByProcedure("USP_Mst_Route",
            //         new string[] { "flag", "Office_ID", "AreaId" },
            //         new string[] { "6", objdb.Office_ID(), ddlLocation.SelectedValue }, "dataset");
           ds3= objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
                     new string[] { "7", objdb.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue }, "dataset");
           if (ds3.Tables[0].Rows.Count != 0)
            {
                ddlRoute.DataSource = ds3.Tables[0];
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    //private void GetRoute()
    //{
    //    try
    //    {
    //        ddlRoute.Items.Clear();
    //        if (ddlItemCategory.SelectedValue != "0")
    //        {
    //            ds = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
    //                     new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
    //                     new string[] { "7", objdb.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue }, "dataset");
    //            if (ds.Tables[0].Rows.Count != 0)
    //            {
    //                ddlRoute.DataSource = ds.Tables[0];
    //                ddlRoute.DataTextField = "RName";
    //                ddlRoute.DataValueField = "RouteId";
    //                ddlRoute.DataBind();
    //                ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
    //            }
    //            else
    //            {
    //                ddlRoute.Items.Insert(0, new ListItem("No Record Found", "0"));
    //            }
    //        }
    //        else
    //        {
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! Error 3: ", "First Select Categorty");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds != null) { ds.Dispose(); }
    //    }
    //}
    protected void GetInstitution()
    {
        try
        {
            //ds = objdb.ByProcedure("USP_Mst_Organization",
            //        new string[] { "flag", "RetailerTypeID", "Office_ID" },
            //          new string[] { "7", "3", objdb.Office_ID() }, "dataset");

            //if (ds.Tables[0].Rows.Count != 0)
            //{
            //    ddlIntitution.DataTextField = "InstName";
            //    ddlIntitution.DataValueField = "OrganizationId";
            //    ddlIntitution.DataSource = ds;
            //    ddlIntitution.DataBind();
            //    ddlIntitution.Items.Insert(0, new ListItem("Select", "0"));
            //}
            //else
            //{
            //    ddlIntitution.Items.Insert(0, new ListItem("Select", "0"));
            //}
            //ds = objdb.ByProcedure("USP_Mst_BoothReg",
            //        new string[] { "flag", "RetailerTypeID", "Office_ID" },
            //          new string[] { "15", objdb.GetInstRetailerTypeId()
            //              , objdb.Office_ID() }, "dataset");

            if(ddlRoute.SelectedValue!="0")
            {
                ds = objdb.ByProcedure("USP_Mst_BoothReg",
                 new string[] { "flag", "Office_ID", "RouteId" },
                 new string[] { "16", objdb.Office_ID(), ddlRoute.SelectedValue }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    ddlIntitution.DataTextField = "BName";
                    ddlIntitution.DataValueField = "BoothId";
                    ddlIntitution.DataSource = ds;
                    ddlIntitution.DataBind();
                    ddlIntitution.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlIntitution.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            
           

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetItemSaleDetails()
    {
        try
        {
            string iiyesNo = "";
            if (ddlForInstitution.SelectedValue == "1")
            {
                iiyesNo = ddlIntitution.SelectedValue;
            }
            else
            {
                iiyesNo = "0";
            }
            ds1 = objdb.ByProcedure("USP_Mst_MilkOrProductSaleRate",
                     new string[] { "Flag", "Office_ID", "ItemCat_id", "RouteId", "OrganizationId" },
                     new string[] { "1", objdb.Office_ID(), ddlItemCategory.SelectedValue, ddlRoute.SelectedValue, iiyesNo }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0 && ddlItemCategory.SelectedValue==objdb.GetMilkCatId())
            {
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();

                foreach (GridViewRow row in GridView1.Rows)
                {

                    TextBox txtDistOrSSRate = (TextBox)row.FindControl("txtDistOrSSRate");
                    TextBox txtRetailerRate = (TextBox)row.FindControl("txtRetailerRate");
                    TextBox txtSSRate = (TextBox)row.FindControl("txtSSRate");
                    txtDistOrSSRate.Attributes.Add("readonly", "readonly");
                    txtRetailerRate.Attributes.Add("readonly", "readonly");
                    txtSSRate.Attributes.Add("readonly", "readonly");
					 // txtDistOrSSRate.Attributes.Add("onKeyDown", "doCheck();");
                    // txtRetailerRate.Attributes.Add("onKeyDown", "doCheck();");
                    // txtSSRate.Attributes.Add("onKeyDown", "doCheck();");
                }

                GridView1.Visible = true;
                GridView2.Visible = false;
                lblMsg.Text = string.Empty;
                pnlbtn.Visible = true;
                pnlproduct.Visible = true;
            }
            else if (ds1.Tables[0].Rows.Count != 0 && ddlItemCategory.SelectedValue == objdb.GetProductCatId())
            {
                
                GridView2.DataSource = ds1.Tables[0];
                GridView2.DataBind();

                foreach (GridViewRow row in GridView2.Rows)
                {

                    TextBox txtConsumerRate2 = (TextBox)row.FindControl("txtConsumerRate2");
                    TextBox txtRetailerRate2 = (TextBox)row.FindControl("txtRetailerRate2");
                    txtRetailerRate2.Attributes.Add("readonly", "readonly");
                    txtConsumerRate2.Attributes.Add("readonly", "readonly");
                }

                GridView1.Visible = false;
                GridView2.Visible = true;
                lblMsg.Text = string.Empty;
                pnlbtn.Visible = true;
                pnlproduct.Visible = true;
            }
            else
            {
                GridView1.Visible = false;
                GridView2.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView2.DataSource = null;
                GridView2.DataBind();
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

                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {


                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");

                        if (CheckBox1.Checked == true)
                        {
                            Checkboxstatus = 1;

                            TextBox txtDistOrSSRate = (TextBox)row.FindControl("txtDistOrSSRate");
                            TextBox txtConsumerRate = (TextBox)row.FindControl("txtConsumerRate");


                            if (txtDistOrSSRate.Text == "" || txtDistOrSSRate.Text == "0" || txtDistOrSSRate.Text == "0.000"
                                && txtConsumerRate.Text == "" || txtConsumerRate.Text == "0" || txtConsumerRate.Text == "0.000")
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
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Checked Item Consumer Rate Can't Empty / 0 / 0.00");
                        return;
                    }




                    if (GridView1.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            CheckBox CheckBox1 = (CheckBox)row.FindControl("CheckBox1");

                            Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                            Label lblItem_id = (Label)row.FindControl("lblItem_id");

                            TextBox txtDistOrSSRate = (TextBox)row.FindControl("txtDistOrSSRate");
                            TextBox txtDistOrSSComm = (TextBox)row.FindControl("txtDistOrSSComm");
                            TextBox txtTransComm = (TextBox)row.FindControl("txtTransComm");
                            TextBox txtRetailerRate = (TextBox)row.FindControl("txtRetailerRate");
                            TextBox txtRetailerComm = (TextBox)row.FindControl("txtRetailerComm");
                            TextBox txtConsumerRate = (TextBox)row.FindControl("txtConsumerRate");
                            TextBox txtAdvCardRebateComm = (TextBox)row.FindControl("txtAdvCardRebateComm");
                            TextBox SpecialRebateMargin = (TextBox)row.FindControl("SpecialRebateMargin");

                            TextBox txtSSTransMargin = (TextBox)row.FindControl("txtSSTransMargin");
                            TextBox txtSSMargin = (TextBox)row.FindControl("txtSSMargin");
                            TextBox txtSSRate = (TextBox)row.FindControl("txtSSRate");



                         
                           

                            TextBox txtEffectiveDate = (TextBox)row.FindControl("txtEffectiveDate");



                            if (CheckBox1.Checked == true)
                            {
                                if (txtDistOrSSRate.Text != "" && txtDistOrSSRate.Text != "0"
                                    && txtConsumerRate.Text != "" && txtConsumerRate.Text != "0"
                                    && txtEffectiveDate.Text != "" && txtRetailerRate.Text != "0" && txtSSTransMargin.Text!=""
                                    && txtSSMargin.Text != "" && txtSSRate.Text!="")                              {
                                    string iiyesNo = "";
                                    if (ddlForInstitution.SelectedValue == "1")
                                    {
                                        iiyesNo = ddlIntitution.SelectedValue;
                                    }
                                    else
                                    {
                                        iiyesNo = "0";
                                    }
                                    DateTime date3 = DateTime.ParseExact(txtEffectiveDate.Text, "dd/MM/yyyy", culture);
                                    effectivedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                                    ds = objdb.ByProcedure("USP_Mst_MilkOrProductSaleRate",
                                               new string[] { "Flag", "AreaId", "ItemCat_id", "Item_id", "RouteId", "OrganizationId", "Office_ID", "DistOrSSRate", "DistOrSSComm", "TransComm", "RetailerRate", "RetailerComm", "ConsumerRate", "EffectiveDate", "CreatedBy", "CreatedByIP", "AdvCardRebateComm", "SpecialRebateMargin", "SSTransMargin", "SSMargin", "SSRate" },
                                               new string[] { "2", ddlLocation.SelectedValue, lblItemCat_id.Text, lblItem_id.Text, ddlRoute.SelectedValue, iiyesNo, objdb.Office_ID(), txtDistOrSSRate.Text.Trim(), txtDistOrSSComm.Text.Trim(), txtTransComm.Text.Trim(), txtRetailerRate.Text.Trim(), txtRetailerComm.Text.Trim(), txtConsumerRate.Text.Trim(), effectivedate.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), txtAdvCardRebateComm.Text, SpecialRebateMargin.Text, txtSSTransMargin.Text, txtSSMargin.Text, txtSSRate.Text }, "dataset");
                                   
                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Distributor/Supersokist or Effective Date or Retailer Rate ");
                                    return;

                                }
                            }

                        }

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetItemSaleDetails();
                           
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                           string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                           GetItemSaleDetails();
                          
                          
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        ds.Clear();
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

    private void InsertRate2()
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


                    foreach (GridViewRow row in GridView2.Rows)
                    {
                        CheckBox CheckBox3 = (CheckBox)row.FindControl("CheckBox3");

                        if (CheckBox3.Checked == true)
                        {
                            Checkboxstatus = 1;

                            TextBox txtDistOrSSRate2 = (TextBox)row.FindControl("txtDistOrSSRate2");
                            TextBox txtConsumerRate2 = (TextBox)row.FindControl("txtConsumerRate2");


                            if (txtDistOrSSRate2.Text == "" || txtDistOrSSRate2.Text == "0" || txtDistOrSSRate2.Text == "0.000"
                                && txtConsumerRate2.Text == "" || txtConsumerRate2.Text == "0" || txtConsumerRate2.Text == "0.000")
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
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Checked Item Consumer Rate Can't Empty / 0 / 0.00");
                        return;
                    }




                    if (GridView2.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in GridView2.Rows)
                        {
                            CheckBox CheckBox3 = (CheckBox)row.FindControl("CheckBox3");

                            Label lblItemCat_id2 = (Label)row.FindControl("lblItemCat_id2");
                            Label lblItem_id2 = (Label)row.FindControl("lblItem_id2");

                            TextBox txtDistOrSSRate2 = (TextBox)row.FindControl("txtDistOrSSRate2");
                            TextBox txtDistOrSSComm2 = (TextBox)row.FindControl("txtDistOrSSComm2");
                            TextBox txtTransComm2 = (TextBox)row.FindControl("txtTransComm2");
                            TextBox txtRetailerRate2 = (TextBox)row.FindControl("txtRetailerRate2");
                            TextBox txtRetailerComm2 = (TextBox)row.FindControl("txtRetailerComm2");
                            TextBox txtConsumerRate2 = (TextBox)row.FindControl("txtConsumerRate2");






                            TextBox txtEffectiveDate2 = (TextBox)row.FindControl("txtEffectiveDate2");



                            if (CheckBox3.Checked == true)
                            {
                                if (txtDistOrSSRate2.Text != "" && txtDistOrSSRate2.Text != "0"
                                    && txtConsumerRate2.Text != "" && txtConsumerRate2.Text != "0"
                                    && txtEffectiveDate2.Text != "" && txtRetailerRate2.Text != "0")
                                {
                                    string iiyesNo = "";
                                    if (ddlForInstitution.SelectedValue == "1")
                                    {
                                        iiyesNo = ddlIntitution.SelectedValue;
                                    }
                                    else
                                    {
                                        iiyesNo = "0";
                                    }
                                    DateTime date3 = DateTime.ParseExact(txtEffectiveDate2.Text, "dd/MM/yyyy", culture);
                                    effectivedate2 = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                                    ds = objdb.ByProcedure("USP_Mst_MilkOrProductSaleRate",
                                               new string[] { "Flag", "AreaId", "ItemCat_id", "Item_id", "RouteId", "OrganizationId", "Office_ID", "DistOrSSRate", "DistOrSSComm", "TransComm", "RetailerRate", "RetailerComm", "ConsumerRate", "EffectiveDate", "CreatedBy", "CreatedByIP" },
                                               new string[] { "2", ddlLocation.SelectedValue, lblItemCat_id2.Text, lblItem_id2.Text, ddlRoute.SelectedValue, iiyesNo, objdb.Office_ID(), txtDistOrSSRate2.Text.Trim(), txtDistOrSSComm2.Text.Trim(), txtTransComm2.Text.Trim(), txtRetailerRate2.Text.Trim(), txtRetailerComm2.Text.Trim(), txtConsumerRate2.Text.Trim(), effectivedate2.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Distributor/Supersokist or Effective Date or Retailer Rate ");
                                    return;

                                }
                            }

                        }

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetItemSaleDetails();

                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetItemSaleDetails();


                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        ds.Clear();
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
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetRoute();
    }
    //protected void ddlIntitution_Init(object sender, EventArgs e)
    //{
    // GetInstitution();
   // }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetItemSaleDetails();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
            {
                InsertRate();
            }
            else
            {
                InsertRate2();
            }
        }
       
    }
    //protected void txtDistOrSSRate_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        lblMsg.Text = "";

    //        foreach (GridViewRow row in GridView1.Rows)
    //        {
    //            TextBox txtDistOrSSRate = (TextBox)row.FindControl("txtDistOrSSRate");
    //            TextBox txtDistOrSSComm = (TextBox)row.FindControl("txtDistOrSSComm");
    //            TextBox txtTransComm = (TextBox)row.FindControl("txtTransComm");
    //            TextBox txtRetailerRate = (TextBox)row.FindControl("txtRetailerRate");
    //            TextBox txtRetailerComm = (TextBox)row.FindControl("txtRetailerComm");
    //            TextBox txtConsumerRate = (TextBox)row.FindControl("txtConsumerRate");

    //            if (txtDistOrSSRate.Text == "" || txtDistOrSSComm.Text == "")
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
    //            }
    //            else
    //            {
    //                txtRetailerRate.Text = (Convert.ToDecimal(txtDistOrSSRate.Text) + Convert.ToDecimal(txtDistOrSSComm.Text) + Convert.ToDecimal(txtTransComm.Text)).ToString();
    //            }


    //            if (txtRetailerRate.Text == "" || txtRetailerComm.Text == "")
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
    //            }
    //            else
    //            {
    //                txtConsumerRate.Text = (Convert.ToDecimal(txtRetailerRate.Text) + Convert.ToDecimal(txtRetailerComm.Text)).ToString();
    //            }

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    // protected void txtConsumerRate_TextChanged(object sender, EventArgs e)
    // {
        // try
        // {

            // lblMsg.Text = "";

            // foreach (GridViewRow row in GridView1.Rows)
            // {
                // TextBox txtConsumerRate = (TextBox)row.FindControl("txtConsumerRate");
                // TextBox txtRetailerComm = (TextBox)row.FindControl("txtRetailerComm");
                // TextBox txtRetailerRate = (TextBox)row.FindControl("txtRetailerRate");
                // TextBox txtTransComm = (TextBox)row.FindControl("txtTransComm");
                // TextBox txtDistOrSSComm = (TextBox)row.FindControl("txtDistOrSSComm");
                // TextBox txtDistOrSSRate = (TextBox)row.FindControl("txtDistOrSSRate");

                // if (txtConsumerRate.Text == "" || txtRetailerComm.Text == "")
                // {
                    // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Consumer Rate / Retailer comm.");
                // }
                // else
                // {
                    // txtRetailerRate.Text = (Convert.ToDecimal(txtConsumerRate.Text) - Convert.ToDecimal(txtRetailerComm.Text) + Convert.ToDecimal(txtTransComm.Text)).ToString();
                // }


                // if (txtTransComm.Text == "" || txtDistOrSSComm.Text == "")
                // {
                    // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Transport / comm.");
                // }
                // else
                // {
                    // txtDistOrSSRate.Text = (Convert.ToDecimal(txtRetailerRate.Text) - Convert.ToDecimal(txtTransComm.Text) - Convert.ToDecimal(txtDistOrSSComm.Text)).ToString();
                // }

            // }

        // }
        // catch (Exception ex)
        // {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        // }
    // }
    // protected void txtRetailerComm_TextChanged(object sender, EventArgs e)
    // {
        // try
        // {

            // lblMsg.Text = "";

            // foreach (GridViewRow row in GridView1.Rows)
            // {
                // TextBox txtConsumerRate = (TextBox)row.FindControl("txtConsumerRate");
                // TextBox txtRetailerComm = (TextBox)row.FindControl("txtRetailerComm");
                // TextBox txtRetailerRate = (TextBox)row.FindControl("txtRetailerRate");
                // TextBox txtTransComm = (TextBox)row.FindControl("txtTransComm");
                // TextBox txtDistOrSSComm = (TextBox)row.FindControl("txtDistOrSSComm");
                // TextBox txtDistOrSSRate = (TextBox)row.FindControl("txtDistOrSSRate");

                // if (txtConsumerRate.Text == "" || txtRetailerComm.Text == "")
                // {
                    // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                // }
                // else
                // {
                    // txtRetailerRate.Text = (Convert.ToDecimal(txtConsumerRate.Text) - Convert.ToDecimal(txtRetailerComm.Text) + Convert.ToDecimal(txtTransComm.Text)).ToString();
                // }


                // if (txtTransComm.Text == "" || txtDistOrSSComm.Text == "")
                // {
                    // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                // }
                // else
                // {
                    // txtDistOrSSRate.Text = (Convert.ToDecimal(txtRetailerRate.Text) - Convert.ToDecimal(txtTransComm.Text) - Convert.ToDecimal(txtDistOrSSComm.Text)).ToString();
                // }

            // }

        // }
        // catch (Exception ex)
        // {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        // }
    // }
    // protected void txtTransComm_TextChanged(object sender, EventArgs e)
    // {
        // try
        // {

            // lblMsg.Text = "";

            // foreach (GridViewRow row in GridView1.Rows)
            // {
                // TextBox txtConsumerRate = (TextBox)row.FindControl("txtConsumerRate");
                // TextBox txtRetailerComm = (TextBox)row.FindControl("txtRetailerComm");
                // TextBox txtRetailerRate = (TextBox)row.FindControl("txtRetailerRate");
                // TextBox txtTransComm = (TextBox)row.FindControl("txtTransComm");
                // TextBox txtDistOrSSComm = (TextBox)row.FindControl("txtDistOrSSComm");
                // TextBox txtDistOrSSRate = (TextBox)row.FindControl("txtDistOrSSRate");

                // if (txtConsumerRate.Text == "" || txtRetailerComm.Text == "")
                // {
                    // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                // }
                // else
                // {
                    // txtRetailerRate.Text = (Convert.ToDecimal(txtConsumerRate.Text) - Convert.ToDecimal(txtRetailerComm.Text)).ToString();
                // }


                // if (txtTransComm.Text == "" || txtDistOrSSComm.Text == "")
                // {
                    // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                // }
                // else
                // {
                    // txtDistOrSSRate.Text = (Convert.ToDecimal(txtRetailerRate.Text) - Convert.ToDecimal(txtTransComm.Text) - Convert.ToDecimal(txtDistOrSSComm.Text)).ToString();
                // }

            // }

        // }
        // catch (Exception ex)
        // {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        // }
    // }
    // protected void txtDistOrSSComm_TextChanged(object sender, EventArgs e)
    // {
        // try
        // {

            // lblMsg.Text = "";

            // foreach (GridViewRow row in GridView1.Rows)
            // {
                // TextBox txtConsumerRate = (TextBox)row.FindControl("txtConsumerRate");
                // TextBox txtRetailerComm = (TextBox)row.FindControl("txtRetailerComm");
                // TextBox txtRetailerRate = (TextBox)row.FindControl("txtRetailerRate");
                // TextBox txtTransComm = (TextBox)row.FindControl("txtTransComm");
                // TextBox txtDistOrSSComm = (TextBox)row.FindControl("txtDistOrSSComm");
                // TextBox txtDistOrSSRate = (TextBox)row.FindControl("txtDistOrSSRate");

                // if (txtConsumerRate.Text == "" || txtRetailerComm.Text == "")
                // {
                    // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                // }
                // else
                // {
                    // txtRetailerRate.Text = (Convert.ToDecimal(txtConsumerRate.Text) - Convert.ToDecimal(txtRetailerComm.Text)).ToString();
                // }


                // if (txtTransComm.Text == "" || txtDistOrSSComm.Text == "")
                // {
                    // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                // }
                // else
                // {
                    // txtDistOrSSRate.Text = (Convert.ToDecimal(txtRetailerRate.Text) - Convert.ToDecimal(txtTransComm.Text) - Convert.ToDecimal(txtDistOrSSComm.Text)).ToString();
                // }

            // }

        // }
        // catch (Exception ex)
        // {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        // }
    // }
    
    // for Product  

    protected void txtDistOrSSRate2_TextChanged(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";

            foreach (GridViewRow row in GridView2.Rows)
            {
                TextBox txtDistOrSSRate2 = (TextBox)row.FindControl("txtDistOrSSRate2");
                TextBox txtDistOrSSComm2 = (TextBox)row.FindControl("txtDistOrSSComm2");
                TextBox txtTransComm2 = (TextBox)row.FindControl("txtTransComm2");
                TextBox txtRetailerRate2 = (TextBox)row.FindControl("txtRetailerRate2");
                TextBox txtRetailerComm2 = (TextBox)row.FindControl("txtRetailerComm2");
                TextBox txtConsumerRate2 = (TextBox)row.FindControl("txtConsumerRate2");

                if (txtDistOrSSRate2.Text == "" || txtDistOrSSComm2.Text == "")
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                }
                else
                {
                    txtRetailerRate2.Text = (Convert.ToDecimal(txtDistOrSSRate2.Text) + Convert.ToDecimal(txtDistOrSSComm2.Text) + Convert.ToDecimal(txtTransComm2.Text)).ToString();
                }


                if (txtRetailerRate2.Text == "" || txtRetailerComm2.Text == "")
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                }
                else
                {
                    txtConsumerRate2.Text = (Convert.ToDecimal(txtRetailerRate2.Text) + Convert.ToDecimal(txtRetailerComm2.Text)).ToString();
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtDistOrSSComm2_TextChanged(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";

            foreach (GridViewRow row in GridView2.Rows)
            {
                TextBox txtDistOrSSRate2 = (TextBox)row.FindControl("txtDistOrSSRate2");
                TextBox txtDistOrSSComm2 = (TextBox)row.FindControl("txtDistOrSSComm2");
                TextBox txtTransComm2 = (TextBox)row.FindControl("txtTransComm2");
                TextBox txtRetailerRate2 = (TextBox)row.FindControl("txtRetailerRate2");
                TextBox txtRetailerComm2 = (TextBox)row.FindControl("txtRetailerComm2");
                TextBox txtConsumerRate2 = (TextBox)row.FindControl("txtConsumerRate2");

                if (txtDistOrSSRate2.Text == "" || txtDistOrSSComm2.Text == "")
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                }
                else
                {
                    txtRetailerRate2.Text = (Convert.ToDecimal(txtDistOrSSRate2.Text) + Convert.ToDecimal(txtDistOrSSComm2.Text) + Convert.ToDecimal(txtTransComm2.Text)).ToString();
                }


                if (txtRetailerRate2.Text == "" || txtRetailerComm2.Text == "")
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                }
                else
                {
                    txtConsumerRate2.Text = (Convert.ToDecimal(txtRetailerRate2.Text) + Convert.ToDecimal(txtRetailerComm2.Text)).ToString();
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtTransComm2_TextChanged(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";

            foreach (GridViewRow row in GridView2.Rows)
            {
                TextBox txtDistOrSSRate2 = (TextBox)row.FindControl("txtDistOrSSRate2");
                TextBox txtDistOrSSComm2 = (TextBox)row.FindControl("txtDistOrSSComm2");
                TextBox txtTransComm2 = (TextBox)row.FindControl("txtTransComm2");
                TextBox txtRetailerRate2 = (TextBox)row.FindControl("txtRetailerRate2");
                TextBox txtRetailerComm2 = (TextBox)row.FindControl("txtRetailerComm2");
                TextBox txtConsumerRate2 = (TextBox)row.FindControl("txtConsumerRate2");

                if (txtDistOrSSRate2.Text == "" || txtDistOrSSComm2.Text == "")
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                }
                else
                {
                    txtRetailerRate2.Text = (Convert.ToDecimal(txtDistOrSSRate2.Text) + Convert.ToDecimal(txtDistOrSSComm2.Text) + Convert.ToDecimal(txtTransComm2.Text)).ToString();
                }


                if (txtRetailerRate2.Text == "" || txtRetailerComm2.Text == "")
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                }
                else
                {
                    txtConsumerRate2.Text = (Convert.ToDecimal(txtRetailerRate2.Text) + Convert.ToDecimal(txtRetailerComm2.Text)).ToString();
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtRetailerComm2_TextChanged(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";

            foreach (GridViewRow row in GridView2.Rows)
            {
                TextBox txtDistOrSSRate2 = (TextBox)row.FindControl("txtDistOrSSRate2");
                TextBox txtDistOrSSComm2 = (TextBox)row.FindControl("txtDistOrSSComm2");
                TextBox txtTransComm2 = (TextBox)row.FindControl("txtTransComm2");
                TextBox txtRetailerRate2 = (TextBox)row.FindControl("txtRetailerRate2");
                TextBox txtRetailerComm2 = (TextBox)row.FindControl("txtRetailerComm2");
                TextBox txtConsumerRate2 = (TextBox)row.FindControl("txtConsumerRate2");

                if (txtDistOrSSRate2.Text == "" || txtDistOrSSComm2.Text == "")
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                }
                else
                {
                    txtRetailerRate2.Text = (Convert.ToDecimal(txtDistOrSSRate2.Text) + Convert.ToDecimal(txtDistOrSSComm2.Text) + Convert.ToDecimal(txtTransComm2.Text)).ToString();
                }


                if (txtRetailerRate2.Text == "" || txtRetailerComm2.Text == "")
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invalid Rate / comm.");
                }
                else
                {
                    txtConsumerRate2.Text = (Convert.ToDecimal(txtRetailerRate2.Text) + Convert.ToDecimal(txtRetailerComm2.Text)).ToString();
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // end for product
   
    protected void ddlForInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (ddlForInstitution.SelectedValue == "1")
        {
            pnlIist.Visible = true;
            GetInstitution();
        }
        
        else
        {
            pnlIist.Visible = false;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ddlRoute.SelectedIndex = 0;
        ddlForInstitution.SelectedIndex = 0;
       // ddlIntitution.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        pnlproduct.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        pnlIist.Visible = false;
        pnlbtn.Visible = false;
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
    }
    
}