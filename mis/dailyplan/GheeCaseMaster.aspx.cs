using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_dailyplan_GheeCaseMaster : System.Web.UI.Page
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
               
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();
                FillGrid();
                FillVariant();
                
               


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
            ddlVariant.Items.Clear();
            ds = objdb.ByProcedure("Sp_tblGheeCaseMaster", new string[] { "flag", "Office_ID", "ProductSection_ID" }, new string[] {"6",objdb.Office_ID(),"2" }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    ddlVariant.DataSource = ds;
                    ddlVariant.DataTextField = "ItemName";
                    ddlVariant.DataValueField = "Item_id";
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
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                
                
                    if (btnSave.Text == "Save")
                    {
                        ds = objdb.ByProcedure("Sp_tblGheeCaseMaster",
                                           new string[] 
                                   {"flag",
                                    "Office_Id", 
			                        "Item_Id",
			                        "CasesSize", 
			                        "Isactive", 
			                        "CreatedBy", 
			                        "CreatedByIP"
                                   },
                                           new string[] 
                                   {"1",
                                    objdb.Office_ID(),
                                    ddlVariant.SelectedValue,
                                    txtCasesSize.Text,
                                    "1",
                                    objdb.createdBy(),
                                    objdb.GetLocalIPAddress()
                                   }, "dataset");
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
                        ds = objdb.ByProcedure("Sp_tblGheeCaseMaster",
                                           new string[] 
                                   {"flag",
                                    "GheeCase_Id", 
			                        "Item_Id",
                                    "CasesSize",
			                        "CreatedBy",  
			                        "CreatedByIP"
                                   },
                                           new string[] 
                                   {"2",
                                    ViewState["GheeCase_Id"].ToString(),
                                    ddlVariant.SelectedValue,
                                    txtCasesSize.Text,
                                    objdb.createdBy(),
                                    objdb.GetLocalIPAddress()
                                   } 
                                  , "dataset");
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
                    ddlVariant.ClearSelection();
                    txtCasesSize.Text = "";
                    btnSave.Text = "Save";
                    ViewState["Case_ID"] = "";
                    FillGrid();
                    
               
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
            //lblMsg.Text = "";
            gvcasedetail.DataSource = string.Empty;
            gvcasedetail.DataBind();

            ds = objdb.ByProcedure("Sp_tblGheeCaseMaster", new string[] { "flag", "Office_Id" }, new string[] {"5",objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gvcasedetail.DataSource = ds;
                    gvcasedetail.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvcasedetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if(e.CommandName == "EditRecord")
            {
                string GheeCase_Id = e.CommandArgument.ToString();
                ViewState["GheeCase_Id"] = GheeCase_Id.ToString();
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblItem_ID =(Label)gvr.FindControl("lblItem_ID");
                Label lblCasesSize =(Label)gvr.FindControl("lblCasesSize");
                
                ddlVariant.ClearSelection();
                ddlVariant.Items.FindByValue(lblItem_ID.Text).Selected = true;
                txtCasesSize.Text = lblCasesSize.Text;
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}