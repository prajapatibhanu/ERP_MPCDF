using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;


public partial class mis_Demand_MilkOrProductOrderByCitizen : System.Web.UI.Page
{
    DataSet ds,ds1,ds11,ds12 = new DataSet();
    DataTable dt;
    APIProcedure objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("en-IN", true);
    int sumQ = 0;
    int cellIndex = 4;
    int cellIndexbooth = 4;
    protected void Page_Load(object sender, EventArgs e)
        {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                
                lblMsg.Text = string.Empty;
                GetState();
                ItemCategory();
               
            
               
          
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
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

    protected void ItemCategory()
    {
        try
        {
            ddlItemCategory.DataSource = objdb.ByProcedure("SpItemCategory",
                     new string[] { "flag" },
                     new string[] { "1" }, "dataset");
            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCat_id";
            ddlItemCategory.DataBind();
            ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }


    protected void GetItem()
    {

        try
        {
            ddlItem.DataSource = objdb.ByProcedure("SpItemType",
                     new string[] { "flag", "ItemCat_id", "Office_ID" },
                     new string[] { "6", ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");
            ddlItem.DataTextField = "ItemTypeName";
            ddlItem.DataValueField = "ItemType_id";
            ddlItem.DataBind();
            ddlItem.Items.Insert(0, new ListItem("Select", "0"));
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GetShift()
    {
        try
        {
           
                ddlShift.DataSource=objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
       
    }

    protected void GetState()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminState",
                     new string[] { "flag" },
                       new string[] { "10" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlState.DataTextField = "State_Name";
                ddlState.DataValueField = "State_ID";
                ddlState.DataSource = ds.Tables[0];
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select", "0"));
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

    protected void GetCity()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminDivision",
                     new string[] { "flag" },
                       new string[] { "10"}, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlCity.DataTextField = "Division_Name";
                ddlCity.DataValueField = "Division_ID";
                ddlCity.DataSource = ds.Tables[0];
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("Select", "0"));
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



    protected void GetVariantDetails()
    {

        try
        {

            ddlItemName.DataSource = objdb.ByProcedure("USP_Mst_AdminVariant",
                            new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID" },
                            new string[] { "6", ddlItemCategory.SelectedValue, ddlItem.SelectedValue, objdb.Office_ID() }, "dataset");
            ddlItemName.DataTextField = "ItemName";
            ddlItemName.DataValueField = "Item_id";
            ddlItemName.DataBind();
            //ddlItemName.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

        //finally
        //{
        //    if (ds != null) { ds.Dispose(); }
        //}
    }


    protected void GetRate()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                     new string[] { "flag", "ItemCat_id", "ItemType_id", "Item_id", "Office_ID" },
                       new string[] { "2", ddlItemCategory.SelectedValue,ddlItem.SelectedValue, ddlItemName.SelectedValue,objdb.Office_ID() }, "dataset");
            if (ds.Tables[0].Rows.Count >0)
            {
               txtRate.Text=ds.Tables[0].Rows[0]["ItemRate"].ToString();
 
                //ddlCity.DataSource = ds.Tables[0];
                //ddlCity.DataBind();
                //ddlCity.Items.Insert(0, new ListItem("Select", "0"));
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


    protected void GetTotalRate()
    {
        try
        {
            if (txtQty.Text != "0" && txtRate.Text != "0")
            {
                decimal salerate = Convert.ToDecimal(txtQty.Text.Trim()) * Convert.ToDecimal(txtRate.Text.Trim());
                txtTotalRate.Text = salerate.ToString();

            }
            else
            {
                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", "Error :");
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    //protected void GetQtyCitizenWise()
    //{
    //    try
    //    {
    //        ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
    //                 new string[] { "flag", "MobileNo","Office_ID" },
    //                   new string[] { "7",txtMobileNo.Text , objdb.Office_ID() }, "dataset");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txtxCitizenwiseQty.Text = ds.Tables[0].Rows[0]["ItemRate"].ToString();

    //            //ddlCity.DataSource = ds.Tables[0];
    //            //ddlCity.DataBind();
    //            //ddlCity.Items.Insert(0, new ListItem("Select", "0"));
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds != null) { ds.Dispose(); }
    //    }
    //}

    protected void GetTotalQty()
    {
        try
        {

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                     new string[] { "flag", "MobileNo", "Office_ID" },
                       new string[] { "7", txtMobileNo.Text, objdb.Office_ID() }, "dataset");


            if (txtQty.Text != "0" && txtRate.Text != "0")
            {
                decimal salerate = Convert.ToDecimal(txtQty.Text.Trim()) * Convert.ToDecimal(txtRate.Text.Trim());
                txtTotalRate.Text = salerate.ToString();

            }
            else
            {
                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", "Error :");
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ClearCart()
    {
        GetShift();
        ddlItemCategory.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
       // GetVariantDetails();
        ddlItemName.SelectedIndex = 0;
        txtQty.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtTotalRate.Text = string.Empty;
        pnlBtnAddCart.Visible = true;
        pnlBtnAddMoreCart.Visible = false;
        

       

    }


    protected void AddCitizenInfo()
    {


        try
        {
            DateTime date3 = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            lblMsg.Text = string.Empty;
          

                
                lblMsg.Text = "";
                ds12 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                    new string[] { "flag"
                                ,"CAddrress1"
                                ,"CCity"
                               // ,"CPincode"
                        },

                    new string[] { "14"
                                
                                ,txtAddress1.Text.Trim()
                                ,ddlCity.SelectedValue
                                
                              //  ,txtPincode.Text
                                
                             
                                }, "dataset");
                if (ds12.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    Clear();

                    GetCartInfo();
                    pnlCart.Visible = true;
                    string success = ds12.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    ClearCart();

                    lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :" + success);
                }
                else
                {
                    string error = ds12.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    if (error == "Already Exists.")
                    {
                        lblMsg.Text = objdb.Alert("fa fa-warning", "alert-warning", "Warning!", " or "
                             + " " + error);
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                }
                ds12.Clear();
            



        }
        catch(Exception ex)
        
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void AddMoreItem()
    {

        
            try
            {

                DateTime date3 = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {

                    lblMsg.Text = "";

                    ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                        new string[] { "flag"
                                ,"CitizenName"
                                ,"MobileNo"
                                ,"OTP"
                                ,"DeliveryShift_id"
                                , "Delivery_Date"
                                ,"ItemCat_id","ItemType_id","Item_id"
                                ,"QtyInNo"
                                , "CAddrress1"
                                , "CState"
                                ,"CCity"
                                ,"Office_ID"
                                ,"CreatedBy"
                                , "CreatedByIP"
                                ,"CDemand_Status"
                                 , "CtotalAmount"},
                        new string[] { "3",txtName.Text.Trim(),txtMobileNo.Text.Trim()
                                ,"123"
                               , ddlShift.SelectedValue
                               ,odat.ToString()                                                                
                                , ddlItemCategory.SelectedValue
                                , ddlItem.SelectedValue
                                 , ddlItemName.SelectedValue 
                                ,txtQty.Text.Trim()
                                ,txtAddress1.Text.Trim()
                                ,ddlState.SelectedValue
                                ,ddlCity.SelectedValue
                              ,objdb.Office_ID()
                               , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                               ,"1",txtTotalRate.Text.Trim()}, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        // GetDatatableHeaderDesign();
                        // Clear();
                        pnlCart.Visible = true;
                        GetCartInfo();
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
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                        }
                    }



                    lblMsg.Text = string.Empty;

                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }


            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }


            }



        
    }
    
    protected void GetCartInfo()
     {

         try
         {
             ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                            new string[] { "flag", "MobileNo", "Office_ID" },
                            new string[] { "1", txtMobileNo.Text, objdb.Office_ID() }, "dataset");

             if (ds1.Tables[0].Rows.Count != 0)
             {
                 dt = ds1.Tables[0];

                 foreach (DataRow drow in dt.Rows)
                 {
                     foreach (DataColumn column in dt.Columns)
                     {
                         if (column.ToString() == "QtyInNo"|| column.ToString() == "CTotalAmount")
                         {

                             if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                             {
                                 drow[column] = 0;
                             }

                        }

                     }
                 }

                 GridView1.DataSource = dt;
                 GridView1.DataBind();

                 GridView1.FooterRow.Cells[3].Text = "Total";
                 GridView1.FooterRow.Cells[3].Font.Bold = true;
                 foreach (DataColumn column in dt.Columns)
                 {
                     if (column.ToString() == "QtyInNo" || column.ToString()=="CTotalAmount")
                     {

                         sumQ = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                         GridView1.FooterRow.Cells[cellIndex].Text = sumQ.ToString();
                         GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                         cellIndex = cellIndex + 1;
                     }
                 }

                 foreach (DataColumn column in dt.Columns)
                 {
                     if (column.ToString() == "Total")
                     {

                         sumQ = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));
                       int QTYSum =sumQ;
                         GridView1.FooterRow.Cells[cellIndex].Text = sumQ.ToString("N2");
                         GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                         cellIndex = cellIndex + 1;
                     }
                 }

                 int rowcount = GridView1.FooterRow.Cells.Count - 3;
                 GridView1.FooterRow.Cells[rowcount].Visible = false;
                 GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
                 GridView1.FooterRow.Cells[rowcount + 2].Visible = false;

             }

             else
             {

                 GridView1.DataSource = null;
                 GridView1.DataBind();
             }



         }
         catch (Exception ex)
         {
             lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
         }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
    private void Clear()
    {
        lblMsg.Text = string.Empty;
        txtName.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        pnlOTP.Visible = false;
        btnOTP.Visible = true;
        btnOK.Visible = false;
        txtQty.Text = string.Empty;
        txtAddress1.Text = string.Empty;
        txtDeliveryDate.Text = string.Empty;
        ddlCity.SelectedIndex = 0;
        ddlShift.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
        ddlItemName.SelectedIndex = 0;
        txtPincode.Text = string.Empty;
        ddlItemName.SelectedIndex = 0;
        txtRate.Text = string.Empty;
        txtTotalRate.Text = string.Empty;

    }
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
    private void InsertCitizenDemand()
    {

            try
            {


                DateTime date3 = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";

                        ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                            new string[] { "flag"
                                ,"CitizenName"
                                ,"MobileNo"
                                
                                ,"DeliveryShift_id"
                                , "Delivery_Date"
                                ,"ItemCat_id","ItemType_id","Item_id"
                                ,"QtyInNo"
                                //, "CAddrress1"
                                //, "CState"
                                //,"CCity"
                                ,"Office_ID"
                                ,"CreatedBy"
                                , "CreatedByIP"
                                ,"CDemand_Status"
                                 , "CtotalAmount"},
                            new string[] { "4",txtName.Text.Trim(),txtMobileNo.Text.Trim()
                                
                               , ddlShift.SelectedValue
                               ,odat.ToString()                                                                
                                , ddlItemCategory.SelectedValue
                                , ddlItem.SelectedValue
                                 , ddlItemName.SelectedValue 
                                ,txtQty.Text.Trim()
                                //,txtAddress1.Text.Trim()
                                //,ddlState.SelectedValue
                                //,ddlCity.SelectedValue
                              ,objdb.Office_ID()
                               , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                               ,"1",txtTotalRate.Text.Trim()}, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                            // GetDatatableHeaderDesign();
                           // Clear();
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
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                            }
                        }

                    }

                    lblMsg.Text = string.Empty;
                    if (btnSubmit.Text == "Update")
                    {
                        GetRate();
                        GetTotalRate();
                        lblMsg.Text = "";
                        ds11 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandCitizen",
                            new string[] { "flag","MilkOrProductDemandCitizenId"
                                ,"CAddrress1"
                                ,"CCity"
                                
                        ,"Item_id"
                        , "ItemType_id"
                        ,"ItemCat_id"
                        
                        ,"QtyInNo" 

                            ,"CTotalAmount"},


                            new string[] { "5"
                                , ViewState["rowid"].ToString()
                                ,txtAddress1.Text.Trim()
                                ,ddlCity.SelectedValue
                                
                                , ddlItemName.SelectedValue
                                , ddlItem.SelectedValue
                                , ddlItemCategory.SelectedValue
                                
                                ,txtQty.Text.Trim()
                                ,txtTotalRate.Text.Trim()
                                
                                   
                               //,ddlShift.SelectedValue                               
                               // ,odat.ToString()
                              //    ,objdb.Office_ID()
                              //, objdb.createdBy()
                              //,objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                           
                                }, "dataset");
                        if (ds11.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            Clear();

                            GetCartInfo();
                            pnlCart.Visible = true;
                            string success = ds11.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            ClearCart();

                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds11.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa fa-warning", "alert-warning", "Warning!", " or "
                                     + " " + error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds11.Clear();
                    }
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }


            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }


            }
        

    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {

        GetVariantDetails();
        GetRate();

    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItem();
    }
    protected void ddlShift_Init(object sender, EventArgs e)
    {
        GetShift();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        btnOTP.Visible = false;
        btnOK.Visible = false;
        pnlOTP.Visible = false;
        txtOTP.Enabled = false;
        pnlitemInfo.Visible = true;
        pnlBtnAddCart.Visible = true;
    }
    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ClearCart();
        //InsertCitizenDemand();
        pnlCart.Visible = true;
        //pnlCitizenInfo.Visible = true;
        pnlOTP.Visible = false;
        btnOK.Visible = false;
        btnOTP.Enabled = false;
        btnAddToCart.Enabled = false;
        //GetTotalRate();

        //InsertCitizenDemand();
        pnlCart.Visible = true;
        GetCartInfo();
        pnlBtnAddMoreCart.Visible = true;

        //AddMoreItem();
        btnAddMore.Enabled = true;
       


        

    }
    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        pnlOTP.Visible = false;
        btnOK.Visible = false;
        btnOTP.Enabled = false;
        btnAddToCart.Enabled = false;
        GetTotalRate();
      
        InsertCitizenDemand();
        pnlCart.Visible = true;
        GetCartInfo();
        pnlBtnAddMoreCart.Visible = true;

        //AddMoreItem();
        btnAddMore.Enabled = true;
        pnlContinue.Visible = true;
       

    }

    protected void btnCitizenInfo_Click(object sender, EventArgs e)
    {

        pnlBtnAddCart.Visible=false;
        pnlBtnAddMoreCart.Visible = false;
        pnlCitizenInfo.Visible = true;
       // InsertCitizenDemand();
        pnlCart.Visible = true;
        GetCartInfo();
        //btnPay.Enabled = true;
        txtOTP.Visible = false; 
        btnOTP.Visible = false;

      
       
    }
    protected void btnPay_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
            pnlCart.Visible = false;
            pnlCitizenInfo.Visible = false;
            Response.Redirect("MilkOrProductOrderByCitizen.aspx");
           
           
        
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Payment Successfull :" );
        
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCity();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         
        try
        {
            if (e.CommandName == "RecordUpdate")
            {
                btnSubmit.Visible = true;
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    pnlOTP.Visible = false;
                    Label lblItemCatName = (Label)row.FindControl("lblItemCatName");
                    Label lblCitizenName = (Label)row.FindControl("lblCitizenName");
                    Label lblMobileNo = (Label)row.FindControl("lblMobileNo");
                    Label lblCState = (Label)row.FindControl("lblCState");
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");
                    Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                    Label lblUnit = (Label)row.FindControl("lblUnit");
                    Label lblItemType_id = (Label)row.FindControl("lblItemType_id");
                    Label lblItemTypeName = (Label)row.FindControl("lblItemTypeName");
                    Label lblItemVarName = (Label)row.FindControl("lblItemVarName");
                    Label lblItemQuantity = (Label)row.FindControl("lblItemQuantity");
                    Label lblItemRate = (Label)row.FindControl("lblItemRate");
                    Label lblDD = (Label)row.FindControl("lblDD");
                    Label lblInvoiceNo = (Label)row.FindControl("lblInvoiceNo");
                    Label lblCCity = (Label)row.FindControl("lblCCity");
                    Label lblCAddrress1 = (Label)row.FindControl("lblCAddrress1");
                    Label lblDeliveryShift_id = (Label)row.FindControl("lblDeliveryShift_id");

                   
                    ViewState["rowid"] = e.CommandArgument;
                    txtOTP.Visible = false;
                    btnOK.Visible = false;
                    txtName.Enabled = false;
                    txtName.Text = lblCitizenName.Text;
                    txtMobileNo.Enabled = false;
                    txtMobileNo.Text = lblMobileNo.Text;

                    GetShift();
                    ddlShift.SelectedValue = lblDeliveryShift_id.Text;
                    ddlItemCategory.SelectedValue = lblItemCat_id.Text;
                    GetItem();
                    ddlItem.SelectedValue = lblItemType_id.Text;
                    GetVariantDetails();
                    ddlItemName.SelectedValue = lblItem_id.Text;
                    txtQty.Text = lblItemQuantity.Text;
                    txtDeliveryDate.Text = lblDD.Text;
                    txtAddress1.Text = lblCAddrress1.Text;
                    GetState();
                   GetCity();
                   ddlCity.SelectedValue = lblCCity.Text;
                   GetShift();
                 //   ddlShift.SelectedValue=lblS
                   ddlState.SelectedValue = lblCState.Text;
                   GetItem();
                   GetVariantDetails();
                   GetRate();
                   GetTotalRate();
                
                    //ddlUnit.SelectedValue = lblUnit.Text;
                   pnlSubmit.Visible = true;
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
                }
                    
            }

            else if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    ds = objdb.ByProcedure("SpItemStock",
                                new string[] { "flag", "ItmStock_id", "CreatedBy"
                                    , "PageName" },
                                new string[] { "9", ViewState["rowid"].ToString(), objdb.createdBy()
                                   
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                     }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        GetCartInfo();
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

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }


    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        GetTotalRate();
        btnAddMore.Enabled = false;
        btnAddToCart.Enabled = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetTotalRate();
        InsertCitizenDemand();
        pnlCart.Visible = true;
        GetCartInfo();
        btnSubmit.Visible = false;
        btnCitizenInfo.Enabled = false;
        btnAddMore.Enabled = false;
    }


    protected void btnOTP_Click (object sender, EventArgs e)
    {
        pnlOTP.Visible = true;
        btnOTP.Enabled = false;
        pnlOk.Visible = true;
        
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType== DataControlRowType.DataRow)
        {
            sumQ = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem,"QtyInNo"));            
        }
        else if(e.Row.RowType== DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = String.Format("",sumQ);
        }
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        AddCitizenInfo();
        lblMsg.Text = string.Empty;
        btnPay.Enabled = true;
    }
}