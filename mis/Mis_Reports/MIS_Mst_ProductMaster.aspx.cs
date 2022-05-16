using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class mis_Mis_Reports_MIS_Mst_ProductMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetProductName();

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
    #region=======================user defined function========================
    protected void GetProductName()
    {
        try
        {
            ddlProductName.DataTextField = "ProductName";
            ddlProductName.DataValueField = "ProductId";
            ddlProductName.DataSource = objdb.ByProcedure("USP_MIS_Mst_Product",
                 new string[] { "Flag", "Office_ID" },
                   new string[] { "0", objdb.Office_ID() }, "dataset");
            ddlProductName.DataBind();
            ddlProductName.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void Clear()
    {

        txtProductName.Text = string.Empty;
        btnSave.Text = "Save";
        GridView1.SelectedIndex = -1;
    }
    private void GetProductNameMasterDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_MIS_Mst_Product",
                 new string[] { "Flag", "Office_ID", "ProductId" },
                   new string[] { "0", objdb.Office_ID(),ddlProductName.SelectedValue }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertorUpdateProductMaster()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    if (btnSave.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_MIS_Mst_Product",
                            new string[] { "Flag", "ProductName", "Office_ID", "CreatedBy", "CreatedByIP" },
                            new string[] { "1", txtProductName.Text.Trim(), objdb.Office_ID(), objdb.createdBy(),
                                            IPAddress + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetProductNameMasterDetails();
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Product Head " + txtProductName.Text + " " + error + " Exist.");
                                GetDatatableHeaderDesign();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }
                    if (btnSave.Text == "Update")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_MIS_Mst_Product",
                            new string[] { "Flag", "ProductId", "ProductName", "Office_ID", "CreatedBy", "CreatedByIP" },
                            new string[] { "2", ViewState["rowid"].ToString(),txtProductName.Text.Trim(), objdb.Office_ID(),objdb.OfficeType_ID()
                                    , objdb.createdBy(), IPAddress + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetProductNameMasterDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Product Name " + txtProductName.Text + " " + error + " Exist.");
                                GetDatatableHeaderDesign();
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
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Product Name");
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

    #region=============== changed event for controls =================
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
                    Label lblProductName = (Label)row.FindControl("lblProductName");
                    txtProductName.Text = lblProductName.Text;                   
                    ViewState["rowid"] = e.CommandArgument;
                    btnSave.Text = "Update";

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                    GetDatatableHeaderDesign();
                }

            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds = objdb.ByProcedure("USP_MIS_Mst_Product",
                                new string[] { "Flag", "ProductId", "CreatedBy", "CreatedByIP" },
                                new string[] { "3",  e.CommandArgument.ToString(), objdb.createdBy()
                                    , IPAddress + ":" + objdb.GetMACAddress()
                                   }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetProductNameMasterDetails();
                        GetProductName();
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
    }
    #endregion============ end of changed event for controls===========

    #region============ button click event ============================
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateProductMaster();
    }
    #endregion=============end of button click funciton==================

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
        GetDatatableHeaderDesign();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetProductNameMasterDetails();
        }
    }
}