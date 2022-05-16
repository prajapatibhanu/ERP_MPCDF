using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_dailyplan_ProductFatSnfFactorMaster : System.Web.UI.Page
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
                ddlProductSection_SelectedIndexChanged(sender, e);
                FillGrid();
                //GetItemCategory(sender, e);

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
                    //txtDate.Attributes.Add("readonly", "readonly");
                    //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

                ddlProductSection.SelectedValue = "2";
                ddlProductSection.Enabled = false;
                

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
            lblMsg.Text = "";
            try
            {
               

                if (ddlProductSection.SelectedValue != "0")
                {
                    lblMsg.Text = "";
                    ddlProduct.Items.Clear();
                    ds = null;
                    ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                        new string[] { "flag", "Office_ID", "ProductSection_ID" },
                        new string[] { "4", objdb.Office_ID(), ddlProductSection.SelectedValue.ToString() }, "dataset");

                   

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if(btnSave.Text == "Save")
            {
                ds = objdb.ByProcedure("Usp_Mst_ProdFatSnfFactor", new string[] { "flag",
                                                                              "Office_ID",
                                                                              "ProductSection_ID", 
                                                                              "ItemType_id",
                                                                              "Fat",
                                                                              "Snf",
                                                                              "IsActive",
                                                                              "CreatedAt",
                                                                              "CreatedBy",
                                                                              "CreatedByIP",},
                                                                             new string[] 
                                                                              { "1",
                                                                                objdb.Office_ID(),
                                                                                ddlProductSection.SelectedValue,
                                                                                ddlProduct.SelectedValue,
                                                                                txtFat.Text,
                                                                                txtSnf.Text,
                                                                                "1",
                                                                                objdb.Office_ID(),
                                                                                objdb.createdBy(),
                                                                                objdb.GetLocalIPAddress()
                                                                              }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-danger", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        FillGrid();
                        
                        ddlProduct.ClearSelection();
                        txtFat.Text = "";
                        txtSnf.Text = "";
                    }
                }   
            }
            else if(btnSave.Text =="Update")
            {
                ds = objdb.ByProcedure("Usp_Mst_ProdFatSnfFactor", new string[] { "flag",
                                                                              "ProdFatSnfFactID",
                                                                              "ItemType_id",
                                                                              "Fat",
                                                                              "Snf",
                                                                              "CreatedAt",
                                                                              "CreatedBy",
                                                                              "CreatedByIP",},
                                                                             new string[] 
                                                                              { "3",
                                                                                ViewState["ProdFatSnfFactID"].ToString(),
                                                                                ddlProduct.SelectedValue,
                                                                                txtFat.Text,
                                                                                txtSnf.Text,
                                                                                objdb.Office_ID(),
                                                                                objdb.createdBy(),
                                                                                objdb.GetLocalIPAddress()
                                                                              }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-danger", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        FillGrid();
                        
                        ddlProduct.ClearSelection();
                        txtFat.Text = "";
                        txtSnf.Text = "";
                        btnSave.Text = "Save";
                    }
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
            gvDetail.DataSource = string.Empty;
            gvDetail.DataBind();
            ds = objdb.ByProcedure("Usp_Mst_ProdFatSnfFactor", new string[] { "flag", "Office_ID"}, new string[] {"2",objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gvDetail.DataSource = ds;
                    gvDetail.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditRecord")
            {
                string ProdFatSnfFactID = e.CommandArgument.ToString();
                ViewState["ProdFatSnfFactID"] = ProdFatSnfFactID.ToString();
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblItemType_id = (Label)gvr.FindControl("lblItemType_id");
                Label lblFat = (Label)gvr.FindControl("lblFat");
                Label lblSnf = (Label)gvr.FindControl("lblSnf");
                ddlProduct.ClearSelection();
                ddlProduct.Items.FindByValue(lblItemType_id.Text).Selected = true;
                txtFat.Text = lblFat.Text;
                txtSnf.Text = lblSnf.Text;
                btnSave.Text = "Update";



            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}