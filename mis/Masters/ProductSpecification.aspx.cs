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


public partial class mis_Masters_ProductSpecification : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3, ds4, ds5;
    //string orderdate = "", demanddate = "", currentdate = "", currrentime = "", deliverydat = ""; Int32 totalqty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);


    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetProdSpecificationDetails();
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

    


    protected void GetIngredient()
    {

        try
        {
            ds = objdb.ByProcedure("USP_Mst_Ingredients",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlNameofIngredients.DataTextField = "IngredientsName";
                ddlNameofIngredients.DataValueField = "IngredientsId";
                ddlNameofIngredients.DataSource = ds.Tables[0];
                ddlNameofIngredients.DataBind();
                ddlNameofIngredients.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlNameofIngredients.Items.Insert(0, new ListItem("Select", "0"));
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
            if (ds != null) { ds.Dispose(); }
        }
    }


   
    protected void GetItem()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_AdvanceCard",
                       new string[] { "flag","ItemCat_id","Office_ID" },
                       new string[] { "12", "2", objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlProductName.DataTextField = "Abbreviation";
                ddlProductName.DataValueField = "ItemType_id";
                ddlProductName.DataSource = ds1.Tables[0];
                ddlProductName.DataBind();
                ddlProductName.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }



    protected void GetUnit()    
    {
        try
        {
            ds4 = objdb.ByProcedure("SpUnit",
                       new string[] { "flag",  },
                       new string[] { "1", }, "dataset");

            if (ds4.Tables[0].Rows.Count != 0)
            {
                ddlItemRatio.DataTextField = "UnitName";
                ddlItemRatio.DataValueField = "Unit_id";
                ddlItemRatio.DataSource = ds4.Tables[0];
                ddlItemRatio.DataBind();
                ddlItemRatio.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }
    }
     protected void GetProdSpecificationDetails()
    {
        try
        {

            ds3 = objdb.ByProcedure("USP_Mst_ProductSpecification",
                    new string[] { "flag", "IngredientsId" },
                    new string[] { "1", ddlNameofIngredients.SelectedValue}, "dataset");
            GridView1.DataSource = ds3.Tables[0];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }


    }
    private void InsertorUpdateIngredients()
        {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";

                        ds2 = objdb.ByProcedure("USP_Mst_ProductSpecification",
                    new string[] { "flag", "IngredientsId", "Prod_Id", "Office_ID", "Unit_ID","CreatedBy", "CreatedBy_IP" },
                    new string[] { "2", ddlNameofIngredients.SelectedValue , ddlProductName.SelectedValue,
                        objdb.Office_ID(), ddlItemRatio.SelectedValue,
                        objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            GetProdSpecificationDetails();
                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Specifications Already Exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Already exists!", error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds2.Clear();
                    
                }

                    if (btnSubmit.Text == "Update")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_ProductSpecification",
                            new string[] { "flag","ProdSpec_id","IngredientsId","Prod_Id","Unit_ID"
                               ,"CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                            new string[] { "3", ViewState["rowid"].ToString(),ddlNameofIngredients.SelectedValue,ddlProductName.SelectedValue
                                ,ddlItemRatio.SelectedValue
                                , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),  Path.GetFileName(Request.Url.AbsolutePath), "Citizen registration record deleted"
                           }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetProdSpecificationDetails();
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
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }


               
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
        }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
            finally
            {
                if (ds != null) { ds.Dispose(); }
            }
        }
    }





    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

              
                    Label lblIngredients_Name = (Label)row.FindControl("lblIngredients_Name");
                    Label lblProduct_Name = (Label)row.FindControl("lblProduct_Name");
                    Label lblUnitName = (Label)row.FindControl("lblUnitName");
                    Label lblProd_Id = (Label)row.FindControl("lblProd_Id");
                    Label lblIngredientsId = (Label)row.FindControl("lblIngredientsId");
                    Label lblUnit_ID = (Label)row.FindControl("lblUnit_ID");

                    ddlItemRatio.SelectedValue = lblUnit_ID.Text;
                    ddlNameofIngredients.SelectedValue = lblIngredientsId.Text;
                    ddlProductName.SelectedValue = lblProd_Id.Text;

                    ViewState["rowid"] = e.CommandArgument;

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

            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    ds = objdb.ByProcedure("USP_Mst_ProductSpecification",
                                new string[] { "flag", "ProdSpec_id", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", e.CommandArgument.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Citizen registration record deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetProdSpecificationDetails();
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void Clear()
    {
        ddlItemRatio.SelectedIndex = 0;
        ddlNameofIngredients.SelectedIndex = 0;
        ddlProductName.SelectedIndex = 0;
        lblMsg.Text = string.Empty;
        GridView1.SelectedIndex = -1;
        btnSubmit.Text = "Save";

    }





    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetCategory();
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlItemCategory.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            pnlItem.Visible = true;
            GetItem();

        }
        else
        {

            ddlItemCategory.SelectedIndex = 0;

            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "Please Select Date & Shift & Retailer");
        }
    }


    protected void ddlNameofIngredients_Init(object sender, EventArgs e)
    {

        GetIngredient();
    }
    protected void ddlProductName_Init(object sender, EventArgs e)
    {
        GetItem();
    }

    protected void ddlItemRatio_Init(object sender, EventArgs e)
    {
        GetUnit();
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateIngredients();

    }



}