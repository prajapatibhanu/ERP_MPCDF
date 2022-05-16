using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_CitizenItemMapping : System.Web.UI.Page
{


    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                CitizenItemMappingDetails();
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

    #region=======================user defined function========================

    protected void GetItemCategory()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_CitizenItemMapping",
                  new string[] { "flag" },
                 new string[] { "6" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds;
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));

            }
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
            ds = objdb.ByProcedure("USP_Mst_CitizenItemMapping",
                       new string[] { "flag", "Office_ID" },
                       new string[] { "7", objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "Item_id";
                ddlItem.DataSource = ds;
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void GetCitizen()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_CitizenItemMapping",
                      new string[] { "flag", "Office_ID" },
                       new string[] { "1", objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlCitizen.DataTextField = "CitizenId";
                ddlCitizen.DataValueField = "CitizenId";
                ddlCitizen.DataSource = ds;
                ddlCitizen.DataBind();
                ddlCitizen.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void CitizenItemMappingDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_DistributorParlourMapping",
                        new string[] { "flag", "Office_ID" },
                       new string[] { "1", objdb.Office_ID() }, "dataset");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Clear()
    {
        ddlCitizen.SelectedIndex = -1;
        ddlItem.SelectedIndex = -1;

        ddlItemCategory.SelectedIndex = -1;
       
        panelDist.Visible = false;
        panelSubDist.Visible = false;

        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
    }
    private void InsertorUpdateCitizenItemMapping()
    {
        lblMsg.Text = "";
        string Is_Active = "1";
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        ds = objdb.ByProcedure("USP_Mst_CitizenItemMapping",
                            new string[] { "flag", "CitizenId", "ItemCat_id", "Item_id", "Office_ID", "CreatedBy", "CreatedByIP" },
                            new string[] { "2", ddlCitizen.SelectedValue, ddlItemCategory.SelectedValue, ddlItem.SelectedValue, objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            CitizenItemMappingDetails();
                            Clear();
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

                    }
                    if (btnSubmit.Text == "Update")
                    {
                        ds = objdb.ByProcedure("USP_Mst_DistributorParlourMapping",
                             new string[] { "flag", "Mst_CitizenItemMappingId", "CitizenId", "ItemCat_id", "Item_id", "Office_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                             new string[] { "3",ViewState["rowid"].ToString(), ddlCitizen.SelectedValue, ddlItemCategory.SelectedValue, ddlItem.SelectedValue, objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                             Path.GetFileName(Request.Url.AbsolutePath), "Distributor/Superstockist Details Updated"}, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            CitizenItemMappingDetails();
                            Clear();
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
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Bank Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    #endregion====================================end of user defined function





    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItemCategory();
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItem();
    }

    protected void ddlCitizen_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCitizen();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        InsertorUpdateCitizenItemMapping();

    }
}
