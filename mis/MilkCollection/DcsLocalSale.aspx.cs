using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;


public partial class mis_MilkCollection_DcsLocalSale : System.Web.UI.Page
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
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        txtDate.Text = Session["date"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }
                if (Session["DeleteIsSuccess"] != null)
                {
                    if ((Boolean)Session["DeleteIsSuccess"] == true)
                    {
                        Session["DeleteIsSuccess"] = false;
                        txtDate.Text = Session["date"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Deleted Successfully');", true);
                    }
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillSociety();
                //txtDate.Attributes.Add("readonly", "readonly");
                
                rblpt_SelectedIndexChanged(sender, e);
                GetItemCategory();
                txtDateF.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                GetViewINVDetails(txtDateF.Text);
                txtDateF.Attributes.Remove("readonly");
                //txtDate.Focus();
                // Session["event_control"] = null;


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
        // if (Session["event_control"] != null)
        // {
            // if (Session["event_control"] is TextBox)
            // {
                // TextBox control = (TextBox)Session["event_control"];
                // control.Focus();
            // }
            // else if (Session["event_control"] is DropDownList)
            // {
                // DropDownList control = (DropDownList)Session["event_control"];
                // control.Focus();
            // }
            // else if (Session["event_control"] is Button)
            // {
                // Button control = (Button)Session["event_control"];
                // control.Focus();
            // }
        // }
    }

    protected void FillSociety()
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
                    txtBlock.Text = ds.Tables[0].Rows[0]["Block_Name"].ToString();
                    txtBlock.Enabled = false;

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

    protected void rblpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblpt.SelectedValue == "1")
            {
                DivPname.Visible = false;
                DivProducerID.Visible = true;
                DivProducerCode.Visible = true;
                FillProducerName();
                rfvProducer.Enabled = true;
                rfvPName.Enabled = false;
            }
            else
            {
                DivPname.Visible = true;
                DivProducerID.Visible = false;
                DivProducerCode.Visible = false;
                rfvProducer.Enabled = false;
                rfvPName.Enabled = true;
                txtProducerCode.Text = "";
                txtProducerName.Text = "";
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillProducerName()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_Id" },
                              new string[] { "3", objdb.Office_ID() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlFarmer.DataTextField = "ProducerName";
                    ddlFarmer.DataValueField = "ProducerId";
                    ddlFarmer.DataSource = ds;
                    ddlFarmer.DataBind();
                    ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlFarmer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFarmer.SelectedIndex > 0)
            {
                ds = null;

                ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                             new string[] { "flag", "ProducerId" },
                             new string[] { "4", ddlFarmer.SelectedValue.ToString() }, "dataset");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtProducerCode.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                    DivPname.Visible = false;
                    txtProducerName.Text = ds.Tables[0].Rows[0]["ProducerName"].ToString();
                }


            }
            else
            {
                DivPname.Visible = true;
                txtProducerCode.Text = "";
                txtProducerName.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        //Session["event_control"] = ddlitemcategory;
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

    protected void ddlitemname_Init(object sender, EventArgs e)
    {
        try
        {
            ddlitemname.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("DcsLocalSale.aspx", false);
    }

    private void GetItemCategory()
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
                ddlitemname.Items.Clear();
                ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
                txtItemUnit.Text = "";
                txtitemqty.Text = "";
                txtMRP.Text = "";
                txttotalamount.Text = "";
            }
            //Session["event_control"] = ddlitemtype;
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
                ds = objdb.ByProcedure("Sp_MstLocalSale",
                             new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID" },
                             new string[] { "6", ddlitemcategory.SelectedValue, ddlitemtype.SelectedValue, objdb.Office_ID() }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlitemname.DataTextField = "ItemName";
                            ddlitemname.DataValueField = "Item_id";
                            ddlitemname.DataSource = ds;
                            ddlitemname.DataBind();
                            ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlitemname.Items.Clear();
                            ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text);
                        }
                    }
                    else
                    {
                        ddlitemname.Items.Clear();
                        ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text);
                    }
                }
                else
                {
                    ddlitemname.Items.Clear();
                    ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text);
                }
            }
            else
            {


                ddlitemname.Items.Clear();
                ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
                txtItemUnit.Text = "";
                txtitemqty.Text = "";
                txtMRP.Text = "";
                txttotalamount.Text = "";
            }
            //Session["event_control"] = ddlitemname;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void ddlitemname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtitemqty.Text = "";

            if (ddlitemcategory.SelectedValue != "0" && ddlitemtype.SelectedValue != "0" && ddlitemname.SelectedValue != "0")
            {

                ds = null;

                if (ddlitemname.SelectedValue != objdb.DcsRawMilkItemId_ID())
                {
                    ds = objdb.ByProcedure("Sp_MstLocalSale",
                             new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID", "Item_id" },
                             new string[] { "7", ddlitemcategory.SelectedValue, ddlitemtype.SelectedValue, objdb.Office_ID(), ddlitemname.SelectedValue }, "dataset");
                }
                else
                {
                    ds = objdb.ByProcedure("Sp_MstLocalSale",
                             new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID", "Item_id" },
                             new string[] { "8", ddlitemcategory.SelectedValue, ddlitemtype.SelectedValue, objdb.Office_ID(), ddlitemname.SelectedValue }, "dataset");
                }


                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            txtItemUnit.Text = ds.Tables[0].Rows[0]["UnitName"].ToString();
                            txtItemUnit.ToolTip = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                            txtMRP.Text = ds.Tables[0].Rows[0]["MRP"].ToString();

                            //txtAvailableStock.Text = "500";

                            if (txtMRP.Text == "" || txtMRP.Text == "0")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "First To Fill Item Sale Rate");
                                return;
                            }
                            txtMRP.ToolTip = "DCS Price - " + ds.Tables[0].Rows[0]["DistributorDCSPrice"].ToString() + " & DCS Margine - " + ds.Tables[0].Rows[0]["DCSMargine"].ToString() + " & Secretary Margine - " + ds.Tables[0].Rows[0]["SecretaryMargine"].ToString();

                            lblDistributorDCSPrice.Text = ds.Tables[0].Rows[0]["DistributorDCSPrice"].ToString();
                            lblDCSMargine.Text = ds.Tables[0].Rows[0]["DCSMargine"].ToString();
                            lblSecretaryPrice.Text = ds.Tables[0].Rows[0]["SecretaryPrice"].ToString();
                            lblSecretaryMargine.Text = ds.Tables[0].Rows[0]["SecretaryMargine"].ToString();


                            if (txtItemUnit.ToolTip == "10")
                            {
                                RegularExpressionValidator5.ValidationExpression = @"^[0-9]*$";
                                RegularExpressionValidator5.Text = "<i class='fa fa-exclamation-circle' title='Enter Valid Quantity(This Item Unit Allow Only Digits)!'></i>";

                            }

                            else
                            {
                                RegularExpressionValidator5.ValidationExpression = @"^[0-9]\d*(\.\d{1,3})?$";
                                RegularExpressionValidator5.Text = "<i class='fa fa-exclamation-circle' title='Enter Valid Quantity(This Item Unit Allow Only Digits/Decimal as well as 3 Decimal Place!'></i>";
                            }

                            // Available Stock

                            if (ddlitemname.SelectedValue == objdb.DcsRawMilkItemId_ID())
                            {
                                string stravsM = GetAvailableRowMilkStock();
                                txtAvailableStock.Text = Convert.ToDecimal(stravsM).ToString("0.00");

                                if (txtAvailableStock.Text == "" || txtAvailableStock.Text == "0" || txtAvailableStock.Text == "0.0" || txtAvailableStock.Text == "0.00" || txtAvailableStock.Text == "0.000")
                                {
                                    txtitemqty.Enabled = true;
                                }
                                else
                                {
                                    txtitemqty.Enabled = true;
                                }

                            }
                            else
                            {
                                string stravs = GetAvailableItemStock();
                                txtAvailableStock.Text = Convert.ToDecimal(stravs).ToString("0.00");

                                if (txtAvailableStock.Text == "" || txtAvailableStock.Text == "0" || txtAvailableStock.Text == "0.0" || txtAvailableStock.Text == "0.00" || txtAvailableStock.Text == "0.000")
                                {
                                    txtitemqty.Enabled = true;
                                }
                                else
                                {
                                    txtitemqty.Enabled = true;
                                }
                            }

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item / Milk Rate Not By Admin.");
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text + " & Item Name - " + ddlitemname.SelectedItem.Text);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Not Exist For Category - " + ddlitemcategory.SelectedItem.Text + " & Item Type - " + ddlitemtype.SelectedItem.Text + " & Item Name - " + ddlitemname.SelectedItem.Text);
                }
            }
            else
            {
                txtitemqty.Enabled = true;
                txtItemUnit.Text = "";
                txtitemqty.Text = "";
                txtMRP.Text = "";
                txttotalamount.Text = "";
            }
            //Session["event_control"] = txtitemqty;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    private string GetShift()
    {
        string shiftname = "0";

        try
        {
            DataSet dsct = objdb.ByProcedure("USP_GetServerDatetime",
                             new string[] { "flag" },
                             new string[] { "1" }, "dataset");

            if (dsct.Tables[0].Rows.Count != 0)
            {
                string currrentime = dsct.Tables[0].Rows[0]["currentTime"].ToString();

                string[] s = currrentime.Split(':');

                if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 12 && ((Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 12 && Convert.ToInt32(s[1]) <= 59))))
                {
                    shiftname = "Morning";
                    return shiftname;
                }
                else
                {
                    shiftname = "Evening";
                    return shiftname;
                }

            }
            else
            {
                return shiftname;
            }


        }
        catch (Exception)
        {
            return shiftname;
        }


    }

    private string GetAvailableRowMilkStock()
    {
        string ItemAvailableStock = "0";

        try
        {

            if (GetShift() != "0")
            {
                ds = null;

                if (objdb.OfficeType_ID()=="5")
                {
                    ds = objdb.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                        new string[] { "flag", "EntryDate", "EntryShift", "Office_Id", "ItemCat_id", "ItemType_id", "Item_id", "I_OfficeTypeID" },
                        new string[] { "6", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd"), GetShift(), objdb.Office_ID(), ddlitemcategory.SelectedItem.Value, 
                             ddlitemtype.SelectedItem.Value, ddlitemname.SelectedItem.Value, objdb.OfficeType_ID() }, "dataset");

                }
                else
                {

                    ds = objdb.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                        new string[] { "flag", "EntryDate", "EntryShift", "Office_Id", "ItemCat_id", "ItemType_id", "Item_id", "I_OfficeTypeID" },
                        new string[] { "4", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd"), GetShift(), objdb.Office_ID(), ddlitemcategory.SelectedItem.Value, 
                             ddlitemtype.SelectedItem.Value, ddlitemname.SelectedItem.Value, objdb.OfficeType_ID() }, "dataset");

                }

               
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ItemAvailableStock = ds.Tables[0].Rows[0]["TotalMilkAvl"].ToString();
                    return ItemAvailableStock;
                }
                else
                {
                    return ItemAvailableStock;
                }
            }

            else
            {
                return ItemAvailableStock;
            }

        }
        catch (Exception ex)
        {
            return ItemAvailableStock;
        }
    }

    private string GetAvailableItemStock()
    {
        string ItemAvailableStock = "0";

        try
        {

            if (ddlitemcategory.SelectedValue != "0" && ddlitemtype.SelectedValue != "0" && ddlitemname.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                                        new string[] { "flag", "Office_Id", "ItemCat_id", "ItemType_id", "Item_id" },
                                        new string[] { "5", objdb.Office_ID(), ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitemname.SelectedItem.Value }, "Dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    ItemAvailableStock = ds.Tables[0].Rows[0]["AvailableStock"].ToString();
                    return ItemAvailableStock;
                }
                else
                {
                    return ItemAvailableStock;
                }

            }
            else
            {
                return ItemAvailableStock;
            }

        }
        catch (Exception ex)
        {
            return ItemAvailableStock;
        }
    }

    protected void txtitemqty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal Itq = 0;
            decimal Avs = 0;

            if (txtitemqty.Text != "" && txtAvailableStock.Text != "" && txtAvailableStock.Text != "0" && txtAvailableStock.Text != "0.00" && txtAvailableStock.Text != "0.0" && txtAvailableStock.Text != "0.000")
            {
                Itq = Convert.ToDecimal(txtitemqty.Text);
                Avs = Convert.ToDecimal(txtAvailableStock.Text);

                if (Itq > Avs)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Entred Item/Milk Quantity Can't Greter Then Available Stock");
                    btnAddItemInfo.Enabled = false;

                }
                else
                {
                    btnAddItemInfo.Enabled = true;
                    txttotalamount.Text = (Convert.ToDecimal(txtitemqty.Text) * Convert.ToDecimal(txtMRP.Text)).ToString("0.00");
                }

            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Invalid Item Quantity");
            }
            //Session["event_control"] = btnAddItemInfo;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
       
    }

    protected void btnAddItemInfo_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (txtitemqty.Text == "" || txtitemqty.Text == "0")
        {
            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Invalid Item Quantity 0 or Blank");
            return;
           
        }
        else
        {
            AddItemDetails();
            //Session["event_control"] = null;
        }
        ddlitemcategory.Focus();

    }

    private void AddItemDetails()
    {
        try
        {

            int ItemId = 0;

            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCatName", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemTypeName", typeof(string)));
                dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("Unit_id", typeof(int)));
                dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
                dt.Columns.Add(new DataColumn("I_Quantity", typeof(decimal)));

                dt.Columns.Add(new DataColumn("DistributorDCSPrice", typeof(decimal)));
                dt.Columns.Add(new DataColumn("DCSMargine", typeof(decimal)));
                dt.Columns.Add(new DataColumn("SecretaryPrice", typeof(decimal)));
                dt.Columns.Add(new DataColumn("SecretaryMargine", typeof(decimal)));
                dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));



                dt.Columns.Add(new DataColumn("NetAmount", typeof(decimal)));


                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlitemcategory.SelectedValue;
                dr[2] = ddlitemcategory.SelectedItem.Text;
                dr[3] = ddlitemtype.SelectedValue;
                dr[4] = ddlitemtype.SelectedItem.Text;
                dr[5] = ddlitemname.SelectedValue;
                dr[6] = ddlitemname.SelectedItem.Text;
                dr[7] = txtItemUnit.ToolTip.ToString();
                dr[8] = txtItemUnit.Text;
                dr[9] = txtitemqty.Text;
                dr[10] = lblDistributorDCSPrice.Text;
                dr[11] = lblDCSMargine.Text;
                dr[12] = lblSecretaryPrice.Text;
                dr[13] = lblSecretaryMargine.Text;
                dr[14] = txtMRP.Text;
                dr[15] = txttotalamount.Text;


                dt.Rows.Add(dr);

                ViewState["InsertRecord"] = dt;
                gv_SealInfo.DataSource = dt;
                gv_SealInfo.DataBind();

            }
            else
            {
                if (ddlitemname.SelectedValue == objdb.DcsRawMilkItemId_ID())
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Can't sale at a time milk or items");
                    return;
                }

                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCatName", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemTypeName", typeof(string)));
                dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("Unit_id", typeof(int)));
                dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
                dt.Columns.Add(new DataColumn("I_Quantity", typeof(decimal)));

                dt.Columns.Add(new DataColumn("DistributorDCSPrice", typeof(decimal)));
                dt.Columns.Add(new DataColumn("DCSMargine", typeof(decimal)));
                dt.Columns.Add(new DataColumn("SecretaryPrice", typeof(decimal)));
                dt.Columns.Add(new DataColumn("SecretaryMargine", typeof(decimal)));
                dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));

                dt.Columns.Add(new DataColumn("NetAmount", typeof(decimal)));
                DT = (DataTable)ViewState["InsertRecord"];


                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlitemname.SelectedValue == DT.Rows[i]["Item_id"].ToString())
                    {
                        ItemId = 1;
                    }

                }

                if (ItemId == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Item Name \"" + ddlitemname.SelectedItem.Text + "\" already exist.");
                }

                else
                {
                    dr = dt.NewRow();
                    dr[0] = 1;
                    dr[1] = ddlitemcategory.SelectedValue;
                    dr[2] = ddlitemcategory.SelectedItem.Text;
                    dr[3] = ddlitemtype.SelectedValue;
                    dr[4] = ddlitemtype.SelectedItem.Text;
                    dr[5] = ddlitemname.SelectedValue;
                    dr[6] = ddlitemname.SelectedItem.Text;
                    dr[7] = txtItemUnit.ToolTip.ToString();
                    dr[8] = txtItemUnit.Text;
                    dr[9] = txtitemqty.Text;
                    dr[10] = lblDistributorDCSPrice.Text;
                    dr[11] = lblDCSMargine.Text;
                    dr[12] = lblSecretaryPrice.Text;
                    dr[13] = lblSecretaryMargine.Text;
                    dr[14] = txtMRP.Text;
                    dr[15] = txttotalamount.Text;


                    dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord"] = dt;
                gv_SealInfo.DataSource = dt;
                gv_SealInfo.DataBind();
            }

            //Clear Record

            if (ddlitemname.SelectedValue == objdb.DcsRawMilkItemId_ID())
            {
                btnAddItemInfo.Enabled = false;
                ddlitemcategory.Enabled = false;
                ddlitemname.Enabled = false;
                ddlitemtype.Enabled = false;
            }
            else
            {
                btnAddItemInfo.Enabled = true;
                ddlitemcategory.Enabled = true;
                ddlitemname.Enabled = true;
                ddlitemtype.Enabled = true;
            }

            ddlitemcategory.ClearSelection();
            ddlitemtype.Items.Clear();
            ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
            ddlitemname.Items.Clear();
            ddlitemname.Items.Insert(0, new ListItem("Select", "0"));

            txtItemUnit.Text = "";
            txtitemqty.Text = "";
            txtitemqty.Enabled = false;
            txtMRP.Text = "";
            txttotalamount.Text = "";
            txtAvailableStock.Text = "";
            txtAvailableStock.Enabled = false;
            rblpt.Enabled = false;
            ddlFarmer.Enabled = false;
            btnSubmit.Enabled = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void lnkDeleteCC_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt3 = ViewState["InsertRecord"] as DataTable;
            dt3.Rows.Remove(dt3.Rows[row1.RowIndex]);
            ViewState["InsertRecord"] = dt3;
            gv_SealInfo.DataSource = dt3;
            gv_SealInfo.DataBind();

            ddlitemcategory.ClearSelection();
            ddlitemtype.Items.Clear();
            ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
            ddlitemname.Items.Clear();
            ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
            txtItemUnit.Text = "";
            txtitemqty.Text = "";
            txtitemqty.Enabled = false;
            txtMRP.Text = "";
            txttotalamount.Text = "";
            txtAvailableStock.Text = "";


            DataTable dtdeletecc = ViewState["InsertRecord"] as DataTable;

            if (dtdeletecc.Rows.Count == 0)
            {
                rblpt.Enabled = true;
                ddlFarmer.Enabled = true;
                btnAddItemInfo.Enabled = true;
                btnSubmit.Enabled = false;
                ddlitemcategory.Enabled = true;
                ddlitemname.Enabled = true;
                ddlitemtype.Enabled = true;
                //ddlFarmer.ClearSelection();
                ViewState["InsertRecord"] = null;
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    private DataTable GetItemDetail()
    {
        DataTable dtID = new DataTable();
        DataRow drID;

        dtID.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dtID.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dtID.Columns.Add(new DataColumn("Item_id", typeof(int)));
        dtID.Columns.Add(new DataColumn("Unit_id", typeof(int)));
        dtID.Columns.Add(new DataColumn("I_Quantity", typeof(decimal)));

        dtID.Columns.Add(new DataColumn("DistributorDCSPrice", typeof(decimal)));
        dtID.Columns.Add(new DataColumn("DCSMargine", typeof(decimal)));
        dtID.Columns.Add(new DataColumn("SecretaryPrice", typeof(decimal)));
        dtID.Columns.Add(new DataColumn("SecretaryMargine", typeof(decimal)));
        dtID.Columns.Add(new DataColumn("MRP", typeof(decimal)));


        dtID.Columns.Add(new DataColumn("NetAmount", typeof(decimal)));

        foreach (GridViewRow rowcc in gv_SealInfo.Rows)
        {
            Label lblItemCat_id = (Label)rowcc.FindControl("lblItemCat_id");
            Label lblItemType_id = (Label)rowcc.FindControl("lblItemType_id");
            Label lblItem_id = (Label)rowcc.FindControl("lblItem_id");
            Label lblUnit_id = (Label)rowcc.FindControl("lblUnit_id");
            Label lblI_Quantity = (Label)rowcc.FindControl("lblI_Quantity");

            Label lblDistributorDCSPrice = (Label)rowcc.FindControl("lblDistributorDCSPrice");
            Label lblDCSMargine = (Label)rowcc.FindControl("lblDCSMargine");
            Label lblSecretaryPrice = (Label)rowcc.FindControl("lblSecretaryPrice");
            Label lblSecretaryMargine = (Label)rowcc.FindControl("lblSecretaryMargine");
            Label lblMRP = (Label)rowcc.FindControl("lblMRP");

            Label lblNetAmount = (Label)rowcc.FindControl("lblNetAmount");

            drID = dtID.NewRow();
            drID[0] = lblItemCat_id.Text;
            drID[1] = lblItemType_id.Text;
            drID[2] = lblItem_id.Text;
            drID[3] = lblUnit_id.Text;
            drID[4] = lblI_Quantity.Text;
            drID[5] = lblDistributorDCSPrice.Text;
            drID[6] = lblDCSMargine.Text;
            drID[7] = lblSecretaryPrice.Text;
            drID[8] = lblSecretaryMargine.Text;
            drID[9] = lblMRP.Text;
            drID[10] = lblNetAmount.Text;

            dtID.Rows.Add(drID);
        }
        return dtID;
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

        try
        {

            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                if (rblpt.SelectedValue == "1")
                {
                    if (ddlFarmer.SelectedValue == "0")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Select Producer");
                        return;
                    }

                }

                lblMsg.Text = "";
                DataTable dtIDF = new DataTable();
                dtIDF = GetItemDetail();

                if (dtIDF.Rows.Count > 0)
                {
                    if(btnSubmit.Text == "Save")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                                                  new string[] { "Flag" 
				                                ,"Office_ID"
				                                ,"Producer_Type" 
				                                ,"ProducerId" 
				                                ,"UserName"
				                                ,"ProducerName"
				                                ,"NetAmount"
				                                ,"CreatedBy" 
				                                ,"CreatedBy_IP"
                                                ,"InvoiceDt"
                                    },
                                                  new string[] { "0"
                                              ,objdb.Office_ID()
                                              ,rblpt.SelectedValue
                                              ,ddlFarmer.SelectedValue
                                              ,txtProducerCode.Text
                                              ,txtProducerName.Text
                                              ,"0.00"
                                              ,ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                    },
                                                 new string[] { "type_Trn_LocalSaleDetailChild_Dcs" },
                                                 new DataTable[] { dtIDF }, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            Session["IsSuccess"] = true;
                            Session["date"] = txtDate.Text;
                            Response.Redirect("DcsLocalSale.aspx", false);

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            Session["IsSuccess"] = false;
                        }
                    }
                    else if (btnSubmit.Text == "Update")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                                                  new string[] { "Flag" 
				                                ,"DcsLocalSale_Id"
				                                ,"Producer_Type" 
				                                ,"ProducerId" 
				                                ,"UserName"
				                                ,"ProducerName"
				                                ,"NetAmount"
				                                ,"CreatedBy" 
				                                ,"CreatedBy_IP"  
                                                ,"InvoiceDt"
                                    },
                                                  new string[] { "8"
                                              ,ViewState["DcsLocalSale_Id"].ToString()
                                              ,rblpt.SelectedValue
                                              ,ddlFarmer.SelectedValue
                                              ,txtProducerCode.Text
                                              ,txtProducerName.Text
                                              ,"0.00"
                                              ,ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()   
                                              ,Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                    },
                                                 new string[] { "type_Trn_LocalSaleDetailChild_Dcs" },
                                                 new DataTable[] { dtIDF }, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            Session["IsSuccess"] = true;
                            Session["date"] = txtDate.Text;
                            Response.Redirect("DcsLocalSale.aspx", false);

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            Session["IsSuccess"] = false;
                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "First To Fill At Least One Sale Item Details");
                }

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }

    protected void GetViewINVDetails(string Fdate)
    {
        try
        {
            ds = null;

            string date = "";

            date = Convert.ToDateTime(Fdate, cult).ToString("yyyy/MM/dd");

            ds = objdb.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                     new string[] { "flag", "Office_ID", "InvoiceDt" },
                     new string[] { "1", objdb.Office_ID(), date }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_localsaleINV.DataSource = ds;
                        gv_localsaleINV.DataBind();

                    }
                    else
                    {
                        gv_localsaleINV.DataSource = null;
                        gv_localsaleINV.DataBind();
                    }
                }
                else
                {
                    gv_localsaleINV.DataSource = null;
                    gv_localsaleINV.DataBind();
                }
            }
            else
            {
                gv_localsaleINV.DataSource = null;
                gv_localsaleINV.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void txtDateF_TextChanged(object sender, EventArgs e)
    {

        if (txtDate.Text != "")
        {
            GetViewINVDetails(txtDateF.Text);
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Opps!", "Invalid Date Formate");
            return;
        }


    }

    protected void gv_localsaleINV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            foreach (GridViewRow row1 in gv_localsaleINV.Rows)
            {
                Label lblShiftTime = (Label)row1.FindControl("lblShiftTime");
                Label lblV_Shift = (Label)row1.FindControl("lblV_Shift");
                //string currrentime = Convert.ToDateTime(lblInvoiceDt.Text, cult).ToString("HH:mm");
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + currrentime + "');", true);
                //string currrentime = ADate.ToString();

                string[] s = lblShiftTime.Text.Split(':');

                if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 12 && ((Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 12 && Convert.ToInt32(s[1]) <= 59))))
                {
                    lblV_Shift.Text = "Morning";

                }
                else
                {
                    lblV_Shift.Text = "Evening";
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
        ddlitemname_SelectedIndexChanged(sender, e);
    }
    protected void gv_localsaleINV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string DcsLocalSale_Id = e.CommandArgument.ToString();
            ViewState["DcsLocalSale_Id"] = DcsLocalSale_Id.ToString();
            if(e.CommandName == "EditRecord")
            {
               DataSet dsRecord = objdb.ByProcedure("USP_Trn_LocalSaleDetail_Dcs", new string[] { "flag", "DcsLocalSale_Id" }, new string[] { "7", DcsLocalSale_Id }, "dataset");
               if (dsRecord != null && dsRecord.Tables.Count > 0)
                {
                    if (dsRecord.Tables[0].Rows.Count > 0)
                    {
                        txtDate.Text = dsRecord.Tables[0].Rows[0]["InvoiceDt"].ToString();
                        rblpt.ClearSelection();
                        rblpt.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Producer_Type"].ToString()).Selected = true;
                        rblpt_SelectedIndexChanged(sender, e);
                        if (dsRecord.Tables[0].Rows[0]["Producer_Type"].ToString() == "1")
                        {
                            ddlFarmer.ClearSelection();
                            ddlFarmer.Items.FindByValue(dsRecord.Tables[0].Rows[0]["ProducerId"].ToString()).Selected = true;
                        }
                        else if (dsRecord.Tables[0].Rows[0]["Producer_Type"].ToString() == "2")
                        {
                            txtProducerName.Text = dsRecord.Tables[0].Rows[0]["ProducerName"].ToString();
                        }
                        ViewState["InsertRecord"] = dsRecord.Tables[1];
                        gv_SealInfo.DataSource = dsRecord.Tables[1];
                        gv_SealInfo.DataBind();
                        btnSubmit.Text = "Update";
                        btnSubmit.Enabled = true;
                    }
                }
            }
            else if (e.CommandName == "DeleteRecord")
            {
                DataSet dsRecord = objdb.ByProcedure("USP_Trn_LocalSaleDetail_Dcs", new string[] { "flag", "DcsLocalSale_Id" }, new string[] { "9", DcsLocalSale_Id }, "dataset");
                if (dsRecord != null && dsRecord.Tables.Count > 0)
                {
                    if (dsRecord.Tables[0].Rows.Count > 0)
                    {
                        if (dsRecord.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            Session["DeleteIsSuccess"] = true;
                            Session["date"] = txtDate.Text;
                            Response.Redirect("DcsLocalSale.aspx", false);

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + dsRecord.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            Session["DeleteIsSuccess"] = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}