using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;


public partial class mis_dailyplan_GenerateReceipe : System.Web.UI.Page
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
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();

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
            if (ddlProductSection.SelectedValue != "0")
            {
                lblMsg.Text = "";
                ddlProduct.Items.Clear();
                ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping", new string[] { "flag", "Office_ID", "ProductSection_ID" }, new string[] { "4", objdb.Office_ID(), ddlProductSection.SelectedValue.ToString() }, "dataset");
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
        GetReipiInfo();
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

    private void GetReipiInfo()
    {
        try
        {

            if (ddlProduct.SelectedValue != "0" && ddlProduct.SelectedValue != "0" && txtQtyF.Text != "")
            {

                if (txtQtyF.Text == "")
                {
                    txtQtyF.Text = "0";
                }

                ds = null;
                ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster", new string[] { "flag", "Office_ID", "ProductQty", "Product_ID", "ProductSection_ID" },
                    new string[] { "10", objdb.Office_ID(), txtQtyF.Text, ddlProduct.SelectedValue, ddlProduct.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    gv_RecipeInfo.DataSource = ds.Tables[0];
                    gv_RecipeInfo.DataBind();


                }
                else
                {
                    gv_RecipeInfo.DataSource = string.Empty;
                    gv_RecipeInfo.DataBind();

                }
            }

            else
            {
                gv_RecipeInfo.DataSource = string.Empty;
                gv_RecipeInfo.DataBind();

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void txtQtyF_TextChanged(object sender, EventArgs e)
    {
        GetReipiInfo();
    }



}