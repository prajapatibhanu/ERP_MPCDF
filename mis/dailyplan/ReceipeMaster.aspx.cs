using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_dailyplan_ReceipeMaster : System.Web.UI.Page
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
                FillOffice();
                GetItemCategory(sender, e);

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
            ds = objdb.ByProcedure("SpAdminOffice",
                  new string[] { "flag" },
                  new string[] { "12" }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtsocietyName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    txtDate.Attributes.Add("readonly", "readonly");
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    FillProductSection();
                }

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
                 new string[] { "0", objdb.Office_ID() }, "dataset");

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

    protected void ddlProductSection_Init(object sender, EventArgs e)
    {
        try
        {
            ddlProductSection.Items.Add(new ListItem("Select", "0"));
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
            DivDispalyRecipe.Visible = false;
            lblLossInPer.Text = "";
            lblOutcomeInPer.Text = "";
            GV_Recipe_Info.DataSource = string.Empty;
            GV_Recipe_Info.DataBind();
            GV_Recipe_Info_Pkg.DataSource = string.Empty;
            GV_Recipe_Info_Pkg.DataBind();


            if (ddlProductSection.SelectedValue != "0")
            {
                lblMsg.Text = "";
                ddlProduct.Items.Clear();
                ds = null;
                ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                    new string[] { "flag", "Office_ID", "ProductSection_ID" },
                    new string[] { "4", objdb.Office_ID(), ddlProductSection.SelectedValue.ToString() }, "dataset");

                //ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                //new string[] { "flag", "Office_ID", "SkimmedMilk", "WholeMilk", "ProductSection_ID" },
                //new string[] { "5", objdb.Office_ID(), 
                //     objdb.SkimmedMilkItemId_ID(), objdb.WholeMilkItemId_ID(),ddlProductSection.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    ddlProduct.DataSource = ds.Tables[0];
                    ddlProduct.DataTextField = "ItemTypeName";
                    ddlProduct.DataValueField = "ItemType_id";
                    ddlProduct.DataBind();

                }
                ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlProduct.Items.Clear();
                ddlProduct_Init(sender, e);
            }

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
            DivDispalyRecipe.Visible = false;
            lblLossInPer.Text = "";
            lblOutcomeInPer.Text = "";
            GV_Recipe_Info.DataSource = string.Empty;
            GV_Recipe_Info.DataBind();
            GV_Recipe_Info_Pkg.DataSource = string.Empty;
            GV_Recipe_Info_Pkg.DataBind();



            DataSet dscheckRecipe = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                            new string[] { "flag", "ProductSection_ID", "Product_ID", "Office_ID" },
                            new string[] { "11", ddlProductSection.SelectedValue, ddlProduct.SelectedValue, objdb.Office_ID() }, "dataset");

            if (dscheckRecipe != null)
            {
                if (dscheckRecipe.Tables.Count > 0)
                {
                    if (dscheckRecipe.Tables[0].Rows.Count > 0)
                    {
                        string strRecipeStstus = dscheckRecipe.Tables[0].Rows[0]["Status"].ToString();

                        if (strRecipeStstus == "0")
                        {
                            lblMsg.Text = "";
                            ds = null;
                            ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster", new string[] { "flag", "Office_ID", "ProductSection_ID", "ItemType_id" },
                                new string[] { "8", objdb.Office_ID(), ddlProductSection.SelectedValue, ddlProduct.SelectedValue }, "dataset");

                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                ddlmappedproduct.DataSource = ds.Tables[0];
                                ddlmappedproduct.DataTextField = "ItemName";
                                ddlmappedproduct.DataValueField = "Product_ID";
                                ddlmappedproduct.DataBind();
                                ddlmappedproduct.Items.Insert(0, new ListItem("Select", "0"));

                                GetItemvariants();
                            }
                            else
                            {
                                ddlmappedproduct.Items.Clear();
                                ddlmappedproduct.Items.Insert(0, new ListItem("Select", "0"));
                            }
                        }
                        else
                        {

                            
                           
                            DataSet dsRecipedisplay = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                              new string[] { "flag", "ProductSection_ID", "Product_ID", "Office_ID" },
                              new string[] { "15", ddlProductSection.SelectedValue, ddlProduct.SelectedValue, objdb.Office_ID() }, "dataset");

                            if (dsRecipedisplay != null)
                            {
                                if (dsRecipedisplay.Tables.Count > 0)
                                {
                                    if (dsRecipedisplay.Tables[0].Rows.Count > 0)
                                    {
                                        DivDispalyRecipe.Visible = true;
                                        lblLossInPer.Text = dsRecipedisplay.Tables[0].Rows[0]["LossInPer"].ToString();
                                        lblOutcomeInPer.Text = dsRecipedisplay.Tables[0].Rows[0]["OutcomeInPer"].ToString(); 
                                        GV_Recipe_Info.DataSource = dsRecipedisplay.Tables[0];
                                        GV_Recipe_Info.DataBind();
                                        GV_Recipe_Info_Pkg.DataSource = dsRecipedisplay.Tables[1];
                                        GV_Recipe_Info_Pkg.DataBind();

                                    }
                                    else
                                    {
                                        DivDispalyRecipe.Visible = false;
                                        lblLossInPer.Text = "";
                                        lblOutcomeInPer.Text = "";
                                        GV_Recipe_Info.DataSource = string.Empty;
                                        GV_Recipe_Info.DataBind();
                                        GV_Recipe_Info_Pkg.DataSource = string.Empty;
                                        GV_Recipe_Info_Pkg.DataBind();
                                    }
                                }
                                else
                                {
                                    DivDispalyRecipe.Visible = false;
                                    lblLossInPer.Text = "";
                                    lblOutcomeInPer.Text = "";
                                    GV_Recipe_Info.DataSource = string.Empty;
                                    GV_Recipe_Info.DataBind();
                                    GV_Recipe_Info_Pkg.DataSource = string.Empty;
                                    GV_Recipe_Info_Pkg.DataBind();
                                }
                            }
                            else
                            {
                                DivDispalyRecipe.Visible = false;
                                lblLossInPer.Text = "";
                                lblOutcomeInPer.Text = "";
                                GV_Recipe_Info.DataSource = string.Empty;
                                GV_Recipe_Info.DataBind();
                                GV_Recipe_Info_Pkg.DataSource = string.Empty;
                                GV_Recipe_Info_Pkg.DataBind();
                            }


                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", ddlProduct.SelectedItem.Text + " - Recipe Already Exist In DataBase.Please See Below Recipe Info.....");
                            return;
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

    protected void ddlProduct_Init(object sender, EventArgs e)
    {
        try
        {
            ddlProduct.Items.Add(new ListItem("Select", "0"));
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

    protected void ddlmappedproduct_Init(object sender, EventArgs e)
    {
        try
        {
            ddlmappedproduct.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceipeMaster.aspx", false);
    }

    private void GetItemCategory(object sender, EventArgs e)
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                         new string[] { "flag" },
                         new string[] { "0" }, "dataset");
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
                        ddlitemcategory.SelectedValue = objdb.ProductionRawMaterialCategoryId_ID();
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
                ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                             new string[] { "flag", "ItemCat_id" },
                             new string[] { "2", ddlitemcategory.SelectedValue }, "dataset");
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
                            ddlitemtype.SelectedValue = objdb.ProductionRawMaterialType_ID();
                            ddlitemtype.Enabled = false;
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
                ddlitemname.Items.Clear();
                ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
                ddlmappedproduct.Items.Clear();
                ddlmappedproduct.Items.Insert(0, new ListItem("Select", "0"));

                txtItemUnit.Text = "";
                txtitemqty.Text = "";

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


                ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                             new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID", "WholeMilk", "SkimmedMilk", "ChakkeP" },
                             new string[] { "3", ddlitemcategory.SelectedValue, ddlitemtype.SelectedValue, objdb.Office_ID(),
                              objdb.SkimmedMilkItemId_ID(), objdb.WholeMilkItemId_ID(),objdb.ChakkeMilkItemId_ID()}, "dataset");
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

                            // For packaging Section 

                            ddlItemNamepackaging.DataTextField = "ItemName";
                            ddlItemNamepackaging.DataValueField = "Item_id";
                            ddlItemNamepackaging.DataSource = ds;
                            ddlItemNamepackaging.DataBind();
                            ddlItemNamepackaging.Items.Insert(0, new ListItem("Select", "0"));


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


                ddlitemname.Items.Clear();
                ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
                ddlmappedproduct.Items.Clear();
                ddlmappedproduct.Items.Insert(0, new ListItem("Select", "0"));

                txtItemUnit.Text = "";
                txtitemqty.Text = "";

            }

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

                ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                            new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID", "Item_id" },
                            new string[] { "4", ddlitemcategory.SelectedValue, ddlitemtype.SelectedValue, objdb.Office_ID(), ddlitemname.SelectedValue }, "dataset");


                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtItemUnit.Text = ds.Tables[0].Rows[0]["UnitName"].ToString();
                            txtItemUnit.ToolTip = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                            txtitemqty.Enabled = true;

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Something went wrong");
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
                txtitemqty.Enabled = false;
                txtItemUnit.Text = "";
                txtitemqty.Text = "";
                ddlmappedproduct.Items.Clear();
                ddlmappedproduct.Items.Insert(0, new ListItem("Select", "0"));

            }

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
        }


    }

    private void AddItemDetails()
    {
        try
        {

            int ItemId = 0;

            string ItemNameId = "";
            string CatId = "";
            string TypeId = "";

            if (ddlitemname.SelectedValue == objdb.WholeMilkItemId_ID())
            {
                CatId = objdb.WholeMilkItemCategoryId_ID();
                TypeId = objdb.WholeMilkItemTypeId_ID();
                ItemNameId = objdb.WholeMilkItemId_ID();

            }
            else if (ddlitemname.SelectedValue == objdb.SkimmedMilkItemId_ID())
            {
                CatId = objdb.SkimmedMilkItemCategoryId_ID();
                TypeId = objdb.SkimmedMilkItemTypeId_ID();
                ItemNameId = objdb.SkimmedMilkItemId_ID();
            }

            else if (ddlitemname.SelectedValue == objdb.ChakkeMilkItemId_ID())
            {
                CatId = objdb.ChakkeMilkItemCategoryId_ID();
                TypeId = objdb.ChakkeMilkItemTypeId_ID();
                ItemNameId = objdb.ChakkeMilkItemId_ID();
            }
            else
            {
                CatId = ddlitemcategory.SelectedValue;
                TypeId = ddlitemtype.SelectedValue;
                ItemNameId = ddlitemname.SelectedValue;
            }

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
                dt.Columns.Add(new DataColumn("MappingProductId", typeof(int)));
                dt.Columns.Add(new DataColumn("MappingProductName", typeof(string)));

                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = CatId;
                dr[2] = ddlitemcategory.SelectedItem.Text;
                dr[3] = TypeId;
                dr[4] = ddlitemtype.SelectedItem.Text;
                dr[5] = ItemNameId;
                dr[6] = ddlitemname.SelectedItem.Text;
                dr[7] = txtItemUnit.ToolTip.ToString();
                dr[8] = txtItemUnit.Text;
                dr[9] = txtitemqty.Text;
                dr[10] = ddlmappedproduct.SelectedValue;
                dr[11] = ddlmappedproduct.SelectedItem.Text;

                dt.Rows.Add(dr);

                ViewState["InsertRecord"] = dt;
                gv_SealInfo.DataSource = dt;
                gv_SealInfo.DataBind();

            }
            else
            {

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
                dt.Columns.Add(new DataColumn("MappingProductId", typeof(int)));
                dt.Columns.Add(new DataColumn("MappingProductName", typeof(string)));

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
                    dr[1] = CatId;
                    dr[2] = ddlitemcategory.SelectedItem.Text;
                    dr[3] = TypeId;
                    dr[4] = ddlitemtype.SelectedItem.Text;
                    dr[5] = ItemNameId;
                    dr[6] = ddlitemname.SelectedItem.Text;
                    dr[7] = txtItemUnit.ToolTip.ToString();
                    dr[8] = txtItemUnit.Text;
                    dr[9] = txtitemqty.Text;
                    dr[10] = ddlmappedproduct.SelectedValue;
                    dr[11] = ddlmappedproduct.SelectedItem.Text;
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
                //ddlitemcategory.Enabled = true;
                //ddlitemname.Enabled = true;
                //ddlitemtype.Enabled = true;
            }

            //ddlitemcategory.ClearSelection();
            //ddlitemtype.Items.Clear();
            //ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
            //ddlitemname.Items.Clear();
            //ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
            ddlitemname.ClearSelection();

            txtItemUnit.Text = "";
            txtitemqty.Text = "";
            ddlmappedproduct.ClearSelection();

            txtitemqty.Enabled = false;
            btnSubmit.Enabled = true;

            ddlProductSection.Enabled = false;
            ddlProduct.Enabled = false;

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

            //ddlitemcategory.ClearSelection();
            //ddlitemtype.Items.Clear();
            //ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
            //ddlitemname.Items.Clear();
            //ddlitemname.Items.Insert(0, new ListItem("Select", "0"));
            txtItemUnit.Text = "";
            txtitemqty.Text = "";
            txtitemqty.Enabled = false;
            ddlmappedproduct.Items.Clear();
            ddlmappedproduct.Items.Insert(0, new ListItem("Select", "0"));

            DataTable dtdeletecc = ViewState["InsertRecord"] as DataTable;

            if (dtdeletecc.Rows.Count == 0)
            {

                btnAddItemInfo.Enabled = true;
                btnSubmit.Enabled = false;
                //ddlitemcategory.Enabled = true;
                //ddlitemtype.Enabled = true;
                //ddlitemname.Enabled = true;

                ViewState["InsertRecord"] = null;

                ddlProductSection.Enabled = true;
                ddlProduct.Enabled = true;

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
        dtID.Columns.Add(new DataColumn("ItemRatioPer", typeof(decimal)));
        dtID.Columns.Add(new DataColumn("ItemOfProductId", typeof(int)));


        foreach (GridViewRow rowcc in gv_SealInfo.Rows)
        {
            Label lblItemCat_id = (Label)rowcc.FindControl("lblItemCat_id");
            Label lblItemType_id = (Label)rowcc.FindControl("lblItemType_id");
            Label lblItem_id = (Label)rowcc.FindControl("lblItem_id");
            Label lblUnit_id = (Label)rowcc.FindControl("lblUnit_id");
            Label lblI_Quantity = (Label)rowcc.FindControl("lblI_Quantity");
            Label lblMappingProductId = (Label)rowcc.FindControl("lblMappingProductId");

            drID = dtID.NewRow();
            drID[0] = lblItemCat_id.Text;
            drID[1] = lblItemType_id.Text;
            drID[2] = lblItem_id.Text;
            drID[3] = lblUnit_id.Text;
            drID[4] = lblI_Quantity.Text;
            drID[5] = lblMappingProductId.Text;
            dtID.Rows.Add(drID);
        }
        return dtID;
    }

    protected void ddlvariantsName_Init(object sender, EventArgs e)
    {
        try
        {
            ddlvariantsName.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    public void GetItemvariants()
    {


        try
        {
            if (ddlProduct.SelectedValue != "0")
            {
                DataSet DSFILLvariants = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                            new string[] { "flag", "Office_ID", "ItemType_id" },
                            new string[] { "12", objdb.Office_ID(), ddlProduct.SelectedValue }, "dataset");

                if (DSFILLvariants != null)
                {
                    if (DSFILLvariants.Tables.Count > 0)
                    {
                        if (DSFILLvariants.Tables[0].Rows.Count > 0)
                        {
                            ddlvariantsName.DataSource = DSFILLvariants.Tables[0];
                            ddlvariantsName.DataTextField = "ItemName";
                            ddlvariantsName.DataValueField = "Item_id";
                            ddlvariantsName.DataBind();
                            ddlvariantsName.Items.Insert(0, new ListItem("Select", "0"));
                        }
                    }
                }
                else
                {
                    ddlvariantsName.Items.Clear();
                    ddlvariantsName.Items.Add(new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlvariantsName.Items.Clear();
                ddlvariantsName.Items.Add(new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void ddlItemNamepackaging_Init(object sender, EventArgs e)
    {
        try
        {
            ddlItemNamepackaging.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlItemNamepackaging_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            txtitemqty.Text = "";

            if (ddlItemNamepackaging.SelectedValue != "0")
            {

                ds = null;

                ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                            new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID", "Item_id" },
                            new string[] { "4", ddlitemcategory.SelectedValue, ddlitemtype.SelectedValue, objdb.Office_ID(), ddlItemNamepackaging.SelectedValue }, "dataset");


                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txttotalpkts.Enabled = true;
                            lblUnitN.Text = "(In 1 " + ds.Tables[0].Rows[0]["UnitName"].ToString() + ")";
                            lblUnitN.ToolTip = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Something went wrong");
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
                txttotalpkts.Enabled = false;
                txttotalpkts.Text = "";
                lblUnitN.Text = "";
                ddlItemNamepackaging.ClearSelection();
                ddlItemNamepackaging.Items.Insert(0, new ListItem("Select", "0"));

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnaddpackaging_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (txttotalpkts.Text == "" || txttotalpkts.Text == "0")
        {
            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Invalid Packet Quantity 0 or Blank");
            return;
        }
        else
        {
            AddItemDetailspackaging();
        }

    }

    private void AddItemDetailspackaging()
    {
        try
        {

            int ItemId = 0;

            string ItemNameId = "";
            string CatId = "";
            string TypeId = "";

            CatId = ddlitemcategory.SelectedValue;
            TypeId = ddlitemtype.SelectedValue;
            ItemNameId = ddlItemNamepackaging.SelectedValue;

            if (Convert.ToString(ViewState["InsertRecordP"]) == null || Convert.ToString(ViewState["InsertRecordP"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("Variant_ID", typeof(int)));
                dt.Columns.Add(new DataColumn("Variant_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("Unit_id", typeof(int)));
                dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
                dt.Columns.Add(new DataColumn("I_PktQty", typeof(decimal)));

                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlvariantsName.SelectedValue;
                dr[2] = ddlvariantsName.SelectedItem.Text;
                dr[3] = ddlItemNamepackaging.SelectedValue;
                dr[4] = ddlItemNamepackaging.SelectedItem.Text;
                dr[5] = lblUnitN.ToolTip;
                dr[6] = lblUnitN.Text;
                dr[7] = txttotalpkts.Text;

                dt.Rows.Add(dr);
                ViewState["InsertRecordP"] = dt;
                gbpackaging.DataSource = dt;
                gbpackaging.DataBind();

            }
            else
            {

                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("Variant_ID", typeof(int)));
                dt.Columns.Add(new DataColumn("Variant_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("Unit_id", typeof(int)));
                dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
                dt.Columns.Add(new DataColumn("I_PktQty", typeof(decimal)));

                DT = (DataTable)ViewState["InsertRecordP"];


                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlItemNamepackaging.SelectedValue == DT.Rows[i]["Item_id"].ToString())
                    {
                        ItemId = 1;
                    }

                }

                if (ItemId == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Item Name \"" + ddlItemNamepackaging.SelectedItem.Text + "\" already exist.");
                }

                else
                {
                    dr = dt.NewRow();
                    dr[0] = 1;
                    dr[1] = ddlvariantsName.SelectedValue;
                    dr[2] = ddlvariantsName.SelectedItem.Text;
                    dr[3] = ddlItemNamepackaging.SelectedValue;
                    dr[4] = ddlItemNamepackaging.SelectedItem.Text;
                    dr[5] = lblUnitN.ToolTip;
                    dr[6] = lblUnitN.Text;
                    dr[7] = txttotalpkts.Text;
                    dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecordP"] = dt;
                gbpackaging.DataSource = dt;
                gbpackaging.DataBind();
            }

            ddlvariantsName.ClearSelection();
            ddlItemNamepackaging.ClearSelection();
            lblUnitN.Text = "";
            txttotalpkts.Text = "";
            txttotalpkts.Enabled = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void lnkDeleteCCP_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt3 = ViewState["InsertRecordP"] as DataTable;
            dt3.Rows.Remove(dt3.Rows[row1.RowIndex]);
            ViewState["InsertRecordP"] = dt3;
            gbpackaging.DataSource = dt3;
            gbpackaging.DataBind();

            ddlvariantsName.ClearSelection();
            ddlItemNamepackaging.ClearSelection();
            lblUnitN.Text = "";
            txttotalpkts.Text = "";
            txttotalpkts.Enabled = false;
            DataTable dtdeletecc = ViewState["InsertRecordP"] as DataTable;


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    private DataTable GetpackagingDetail()
    {
        DataTable dtIDP = new DataTable();
        DataRow drID;

        dtIDP.Columns.Add(new DataColumn("Variant_ID", typeof(int)));
        dtIDP.Columns.Add(new DataColumn("Item_id", typeof(int)));
        dtIDP.Columns.Add(new DataColumn("Unit_id", typeof(int)));
        dtIDP.Columns.Add(new DataColumn("I_PktQty", typeof(decimal)));

        foreach (GridViewRow rowP in gbpackaging.Rows)
        {
            Label lblVariant_ID = (Label)rowP.FindControl("lblVariant_ID");
            Label lblItem_id = (Label)rowP.FindControl("lblItem_id");
            Label lblUnit_id = (Label)rowP.FindControl("lblUnit_id");
            Label lblI_PktQty = (Label)rowP.FindControl("lblI_PktQty");

            drID = dtIDP.NewRow();
            drID[0] = lblVariant_ID.Text;
            drID[1] = lblItem_id.Text;
            drID[2] = lblUnit_id.Text;
            drID[3] = lblI_PktQty.Text;

            dtIDP.Rows.Add(drID);
        }
        return dtIDP;
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

        try
        {

            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                lblMsg.Text = "";
                DataTable dtIDF = new DataTable();
                dtIDF = GetItemDetail();

                DataTable dtIDP = new DataTable();
                dtIDP = GetpackagingDetail();

                if (dtIDF.Rows.Count > 0 && dtIDP.Rows.Count > 0)
                {
                    ds = null;
                    ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                                              new string[] { "Flag" 
				                                ,"Office_ID"
				                                ,"ProductSection_ID" 
				                                ,"Product_ID" 
				                                ,"LossInPer"
				                                ,"OutcomeInPer"
				                                ,"Recepie_UpdatedBy"
				                              
                                    },
                                              new string[] { "9"
                                              ,objdb.Office_ID() 
                                              ,ddlProductSection.SelectedValue
                                              ,ddlProduct.SelectedValue
                                              ,txtLossInPer.Text
                                              ,txtOutcome.Text
                                              ,ViewState["Emp_ID"].ToString() 
                                    },
                                             new string[] { "type_ProductionRecipe_Master", "type_ProductionRecipe_MasterPackaging" },
                                             new DataTable[] { dtIDF, dtIDP }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        Session["IsSuccess"] = true;
                        Response.Redirect("ReceipeMaster.aspx", false);

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        Session["IsSuccess"] = false;
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "First To Fill At Least One Ingredient / Packaging Details");
                }

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }



}