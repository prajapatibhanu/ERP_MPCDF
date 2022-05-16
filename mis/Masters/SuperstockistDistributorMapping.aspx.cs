using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;


public partial class mis_Masters_SuperstockistDistributorMapping : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, dsr, ds3, ds4,ds5, dsd = new DataSet();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetSuperStockist();
                GetCategory();
                GetLocation();
                GetSearchCategory();
                GetSearchLocation();
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
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void GetSuperStockist()
    {
        try
        {

            ddlSuperStockistName.DataTextField = "SSCName";
            ddlSuperStockistName.DataValueField = "SuperStockistId";
            ddlSuperStockistName.DataSource = objdb.ByProcedure("USP_Mst_SuperStockistReg",
                 new string[] { "flag", "Office_ID" },
                   new string[] { "1",objdb.Office_ID() }, "dataset");
            ddlSuperStockistName.DataBind();
            ddlSuperStockistName.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetCategory()
    {
        try
        {
           
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetSearchCategory()
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds3.Tables[0].Rows.Count != 0)
            {
                ddlSearchItemCategory.DataTextField = "ItemCatName";
                ddlSearchItemCategory.DataValueField = "ItemCat_id";
                ddlSearchItemCategory.DataSource = ds3.Tables[0];
                ddlSearchItemCategory.DataBind();
                ddlSearchItemCategory.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlSearchItemCategory.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    protected void GetLocation()
    {
        try
        {
                ddlLocation.DataTextField = "AreaName";
                ddlLocation.DataValueField = "AreaId";
                ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetSearchLocation()
    {
        try
        {
            ds4 = objdb.ByProcedure("USP_Mst_Area",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds4.Tables[0].Rows.Count != 0)
            {
                ddlSearchLocation.DataTextField = "AreaName";
                ddlSearchLocation.DataValueField = "AreaId";
                ddlSearchLocation.DataSource = ds4.Tables[0];
                ddlSearchLocation.DataBind();
                ddlSearchLocation.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlLocation.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }
    }
    private void GetDistributor()
    {
        try
        {
            ddlDistributor.Items.Clear();
            ddlDistributor.DataSource = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                    new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
                    new string[] { "1", objdb.Office_ID(),ddlItemCategory.SelectedValue, ddlLocation.SelectedValue }, "dataset");
            ddlDistributor.DataTextField = "DRName";
            ddlDistributor.DataValueField = "DistributorId";
            ddlDistributor.DataBind();
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
        }
        finally
        {
            if (dsr != null) { dsr.Dispose(); }
        }
    }
    protected void SuperStockistDistributorMappingDetails()
    {
        try
        {

            GridView1.DataSource = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                         new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                        new string[] { "3", objdb.Office_ID(), ddlSearchLocation.SelectedValue, ddlSearchItemCategory.SelectedValue }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();
          
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4:", ex.Message.ToString());
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
            GetDistributor();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {
                        GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                        Label lblIsActive = (Label)row.FindControl("lblIsActive");

                        string isactive = "";
                        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        if (lblIsActive.Text == "True")
                        {
                            isactive = "0";
                        }
                        else
                        {
                            isactive = "1";
                        }
                        lblMsg.Text = string.Empty;
                        dsd = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                                    new string[] { "flag", "SuperStockistDistributorMapping_Id", "CreatedBy", "CreatedByIP", "IsActive" },
                                    new string[] { "4", e.CommandArgument.ToString(), objdb.createdBy() ,IPAddress,isactive }, "TableSave");


                        if (dsd.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = dsd.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            SuperStockistDistributorMappingDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = dsd.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        dsd.Dispose();
                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  delete record: " + ex.Message.ToString());
        }
        finally
        {
            if (dsd != null) { dsd.Dispose(); }
        }
    }
    private void InsertSSDistMapping()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DataRow dr;
                DataTable dtdistid = new DataTable();
                dtdistid.Columns.Add("DistributorId", typeof(string));


                dr = dtdistid.NewRow();
                foreach (ListItem item in ddlDistributor.Items)
                {
                    if (item.Selected)
                    {
                        dr[0] = item.Value;
                        dtdistid.Rows.Add(dr.ItemArray);
                    }
                }

                if (dtdistid.Rows.Count > 0)
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds5 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                              new string[] { "flag", "SuperStockistId", "ItemCat_id", "AreaId", "Office_ID", "CreatedBy", "CreatedByIP" },
                             new string[] { "2", ddlSuperStockistName.SelectedValue, ddlItemCategory.SelectedValue, 
                             ddlLocation.SelectedValue,objdb.Office_ID(), objdb.createdBy(),IPAddress },
                               new string[] { "type_SuperStockistDistributorMapping" },
                               new DataTable[] { dtdistid }, "dataset");

                    if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        GetDistributor();
                    }
                    else
                    {
                        string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Data not found ");
                }

                if (dtdistid != null)
                {
                    dtdistid.Dispose();
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  insert record: " + ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertSSDistMapping();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            SuperStockistDistributorMappingDetails();
        }
    }
}