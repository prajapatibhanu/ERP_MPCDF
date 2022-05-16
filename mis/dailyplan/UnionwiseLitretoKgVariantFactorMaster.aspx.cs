using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_dailyplan_UnionwiseLitretoKgVariantFactorMaster : System.Web.UI.Page
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
                FillVariant();
                FillGrid();

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
                    

                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillVariant()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New",
                  new string[] { "flag" },
                  new string[] { "10" }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVariant.DataSource = ds;
                    ddlVariant.DataTextField = "ItemTypeName";
                    ddlVariant.DataValueField = "ItemType_id";
                    ddlVariant.DataBind();
                    

                }

            }
            ddlVariant.Items.Insert(0, new ListItem("Select", "0"));

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
            lblMsg.Text = "";
            if(btnSave.Text == "Save")
            {
                ds = objdb.ByProcedure("Usp_Mst_LitretoKgConversionFactor",
                                    new string[] 
                                    {"flag",
                                     "Office_ID",
                                     "ItemType_id",
                                     "LtrtoKgFactor",
                                     "FatFactor",
                                     "SnfFactor",
                                     "IsActive",
                                     "CreatedBy",
                                     "CreatedAt",
                                     "CreatedByIP"

                                    },
                                    new string[] 
                                    {"1",
                                      objdb.Office_ID(),
                                      ddlVariant.SelectedValue,
                                      txtLitretoKgConversionFactor.Text,
                                      txtFatFactor.Text,
                                      txtSnffactor.Text,
                                      "1",
                                      objdb.createdBy(),
                                      objdb.Office_ID(),
                                      objdb.GetLocalIPAddress()
                                    }, "dataset");
            }
            else if(btnSave.Text == "Update")
            {
                ds = objdb.ByProcedure("Usp_Mst_LitretoKgConversionFactor",
                                    new string[] 
                                    {"flag",
                                     "LtrtoKgFactor_ID",
                                     "ItemType_id",
                                     "LtrtoKgFactor",
                                     "FatFactor",
                                     "SnfFactor",
                                     "CreatedBy",
                                     "CreatedAt",
                                     "CreatedByIP"

                                    },
                                    new string[] 
                                    {"4",
                                      ViewState["LtrtoKgFactor_ID"].ToString(),
                                      ddlVariant.SelectedValue,
                                      txtLitretoKgConversionFactor.Text,
                                      txtFatFactor.Text,
                                      txtSnffactor.Text,
                                      objdb.createdBy(),
                                      objdb.Office_ID(),
                                      objdb.GetLocalIPAddress()
                                    }, "dataset");
            }
            if(ds !=null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-danger", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    FillGrid();
                    ddlVariant.ClearSelection();
                    txtLitretoKgConversionFactor.Text = "";
                    txtFatFactor.Text = "";
                    txtSnffactor.Text = "";
                    btnSave.Text = "Save";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = string.Empty;
            GridView1.DataBind();
            ds = objdb.ByProcedure("Usp_Mst_LitretoKgConversionFactor", new string[] { "flag", "Office_ID" }, new string[] {"2",objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "EditRecord")
        {
            string LtrtoKgFactor_ID = e.CommandArgument.ToString();
            ViewState["LtrtoKgFactor_ID"] = LtrtoKgFactor_ID;
            ds = objdb.ByProcedure("Usp_Mst_LitretoKgConversionFactor", new string[] { "flag", "LtrtoKgFactor_ID" }, new string[] { "3", LtrtoKgFactor_ID }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    ddlVariant.ClearSelection();
                    ddlVariant.Items.FindByValue(ds.Tables[0].Rows[0]["ItemType_id"].ToString()).Selected = true;
                    txtLitretoKgConversionFactor.Text = ds.Tables[0].Rows[0]["LtrtoKgFactor"].ToString();
                    txtFatFactor.Text = ds.Tables[0].Rows[0]["FatFactor"].ToString();
                    txtSnffactor.Text = ds.Tables[0].Rows[0]["SnfFactor"].ToString();
                    btnSave.Text = "Update";
                }
            }
        }
    }
}