using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_dailyplan_IngredientsMaster : System.Web.UI.Page
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
                FillProduct();
                FillIngredient();
                ViewState["dt"] = "";
                

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
                    txtsocietyName.Text = Session["Office_Name"].ToString();
                    txtDate.Attributes.Add("readonly", "readonly");
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillProduct()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ddlProduct.Items.Clear();
            ddlProd_flt.Items.Clear();
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                new string[] { "flag", "Office_ID", "ProductSection_ID" },
                new string[] { "4", objdb.Office_ID(), "2" }, "dataset");

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
                ddlProd_flt.DataSource = ds.Tables[0];
                ddlProd_flt.DataTextField = "ItemTypeName";
                ddlProd_flt.DataValueField = "ItemType_id";
                ddlProd_flt.DataBind();

            }
            ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
            ddlProd_flt.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillIngredient()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("Usp_MstIngredientsMaster",
                  new string[] { "flag", "Office_ID" },
                  new string[] { "1",objdb.Office_ID() }, "dataset");
            ddlIngredients.Items.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlIngredients.DataSource = ds.Tables[0];
                    ddlIngredients.DataTextField = "ItemName";
                    ddlIngredients.DataValueField = "Item_id";
                    ddlIngredients.DataBind();

                }

            }
            ddlIngredients.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Status = "0";
            foreach(GridViewRow rows in gvIngredientDetail.Rows)
            {
                Label lblItem_id = (Label)rows.FindControl("lblItem_id");
                if(lblItem_id.Text == ddlIngredients.SelectedValue)
                {
                    Status = "1";
                    break;
                }
            }
            if(Status == "0")
            {
                if (ViewState["dt"].ToString() == null || ViewState["dt"].ToString() == "")
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Item_id", typeof(int));
                    dt.Columns.Add("ItemName", typeof(string));
                    dt.Columns.Add("CalculationMethod", typeof(string));
                    dt.Columns.Add("Value", typeof(string));
                    dt.Columns.Add("FatFactor", typeof(decimal));
                    dt.Columns.Add("SnfFactor", typeof(decimal));
                    dt.Rows.Add(ddlIngredients.SelectedValue, ddlIngredients.SelectedItem.Text, ddlCalculationMethod.SelectedItem.Text, txtValue.Text, txtFatFactor.Text, txtSnfFactor.Text);
                    ViewState["dt"] = dt;
                    gvIngredientDetail.DataSource = dt;
                    gvIngredientDetail.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["dt"];
                    dt.Rows.Add(ddlIngredients.SelectedValue, ddlIngredients.SelectedItem.Text, ddlCalculationMethod.SelectedItem.Text, txtValue.Text, txtFatFactor.Text, txtSnfFactor.Text);
                    ViewState["dt"] = dt;
                    gvIngredientDetail.DataSource = dt;
                    gvIngredientDetail.DataBind();
                }

                ddlIngredients.ClearSelection();
                ddlCalculationMethod.ClearSelection();
                txtFatFactor.Text = "";
                txtSnfFactor.Text = "";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ingredients already Exists');", true);
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DataTable dtGetIngredientDetail = new DataTable();
                dtGetIngredientDetail = GetIngredientDetail();
                if (dtGetIngredientDetail.Rows.Count > 0)
                {
                    if (btnSave.Text == "Save")
                    {
                        ds = objdb.ByProcedure("Usp_MstIngredientsMaster",
                                           new string[] 
                                   {"flag",
                                    "Office_ID", 
			                        "ItemType_ID",
			                        "EffectiveDate", 
			                        "IsActive", 
			                        "CreatedBy", 
			                        "CreatedAt",  
			                        "CreatedByIP"
                                   },
                                           new string[] 
                                   {"2",
                                    objdb.Office_ID(),
                                    ddlProduct.SelectedValue,
                                    Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                    "1",
                                    objdb.createdBy(),
                                    objdb.Office_ID(),
                                    objdb.GetLocalIPAddress()
                                   }, new string[] 
                                   {
                                    "type_ProdIngredientMasterChild",
                                   },
                                           new DataTable[]
                                   {
                                    dtGetIngredientDetail 
                                   }, "TableSave");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                                {
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                            }
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        ds = objdb.ByProcedure("Usp_MstIngredientsMaster",
                                           new string[] 
                                   {"flag",
                                    "ProdIngredient_ID", 			                        
			                        "CreatedBy", 
			                        "CreatedAt",  
			                        "CreatedByIP"
                                   },
                                           new string[] 
                                   {"4",
                                    ViewState["ProdIngredient_ID"].ToString(),
                                    objdb.createdBy(),
                                    objdb.Office_ID(),
                                    objdb.GetLocalIPAddress()
                                   }, new string[] 
                                   {
                                    "type_ProdIngredientMasterChild",
                                   },
                                           new DataTable[]
                                   {
                                    dtGetIngredientDetail 
                                   }, "TableSave");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                                {
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                            }
                        }
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["dt"] = "";
                    gvIngredientDetail.DataSource = string.Empty;
                    gvIngredientDetail.DataBind();
                    ddlProduct.ClearSelection();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please add atleast one ingredient');", true);
                }
            }
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private DataTable GetIngredientDetail()
    {
        
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("Item_id", typeof(int));
        dt.Columns.Add("CalculationMethod", typeof(string));
        dt.Columns.Add("Value", typeof(string));
        dt.Columns.Add("FatFactor", typeof(decimal));
        dt.Columns.Add("SnfFactor", typeof(decimal));

        foreach(GridViewRow row in gvIngredientDetail.Rows)
        {
            Label lblItem_id = (Label)row.FindControl("lblItem_id");
            Label lblCalculationMethod = (Label)row.FindControl("lblCalculationMethod");
            Label lblValue = (Label)row.FindControl("lblValue");
            Label lblFatFactor = (Label)row.FindControl("lblFatFactor");
            Label lblSnfFactor = (Label)row.FindControl("lblSnfFactor");
            dt.Rows.Add(lblItem_id.Text, lblCalculationMethod.Text, lblValue.Text, lblFatFactor.Text, lblSnfFactor.Text);
        }

        return dt;
    }
    protected void ddlProd_flt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView1.DataSource = string.Empty;
            GridView1.DataBind();
            if(ddlProd_flt.SelectedIndex > 0)
            {
               
                ds = objdb.ByProcedure("Usp_MstIngredientsMaster", new string[] { "flag", "Office_ID", "ItemType_ID" }, new string[] { "3", objdb.Office_ID(), ddlProd_flt.SelectedValue }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds;
                        GridView1.DataBind();

                    }
                }
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
            lblMsg.Text = "";
            ViewState["dt"] = "";
            ds = objdb.ByProcedure("Usp_MstIngredientsMaster", new string[] { "flag", "Office_ID", "ItemType_ID" }, new string[] { "3", objdb.Office_ID(), ddlProduct.SelectedValue }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Item_id", typeof(int));
                    dt.Columns.Add("ItemName", typeof(string));
                    dt.Columns.Add("CalculationMethod", typeof(string));
                    dt.Columns.Add("Value", typeof(string));
                    dt.Columns.Add("FatFactor", typeof(decimal));
                    dt.Columns.Add("SnfFactor", typeof(decimal));
                    dt = ds.Tables[0];
                    ViewState["dt"] = dt;
                    gvIngredientDetail.DataSource = dt;
                    gvIngredientDetail.DataBind();
                    btnSave.Text = "Update";


                    ViewState["ProdIngredient_ID"] = ds.Tables[1].Rows[0]["ProdIngredient_ID"].ToString();

                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("IngredientsMaster.aspx", false);
    }
    protected void gvIngredientDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DeleteRecord")
            {
                string ID = e.CommandArgument.ToString();
                DataTable dt = (DataTable)ViewState["dt"];
                int Count = dt.Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["Item_id"].ToString() == ID.ToString())
                    {

                        dr.Delete();
                        break;
                    }
                }
                dt.AcceptChanges();
                ViewState["dt"] = dt;

                gvIngredientDetail.DataSource = dt;
                gvIngredientDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}