using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_Masters_Mst_ProductRateType : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetProductRateType();
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
    private void Clear()
    {

        txtProductRateTypeName.Text = string.Empty;
        btnSubmit.Text = "Save";
        GridView1.SelectedIndex = -1;
    }
    private void GetProductRateType()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_ProductRateType",
                    new string[] { "Flag", "Office_ID", },
                    new string[] { "1", objdb.Office_ID() }, "dataset");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertorUpdateProductRateType()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = "";
                    string chkval = "";
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    if (chkIsActive.Checked == true)
                    {
                        chkval = "1";
                    }
                    else
                    {
                        chkval = "0";
                    }
                    if (btnSubmit.Text == "Save")
                    {
                       
                        ds1 = objdb.ByProcedure("USP_Mst_ProductRateType",
                            new string[] { "Flag", "ProductRateTypeName", "IsActive", "Office_ID", "CreatedBy", "CreatedByIP", },
                            new string[] { "2", txtProductRateTypeName.Text.Trim() ,chkval.ToString(), objdb.Office_ID(),
                                               objdb.createdBy(),IPAddress }, "dataset");

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetProductRateType();
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Product Rate Type " + txtProductRateTypeName.Text.Trim() + " " + error + " Exist.");
                                GetProductRateType();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds1.Clear();
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        lblMsg.Text = "";
                        ds1 = objdb.ByProcedure("USP_Mst_ProductRateType",
                            new string[] { "Flag","ProductRateTypeId", "ProductRateTypeName", "IsActive", "Office_ID", "CreatedBy", "CreatedByIP", },
                            new string[] { "3",ViewState["rowid"].ToString(), txtProductRateTypeName.Text.Trim() ,chkval.ToString(), objdb.Office_ID(),
                                               objdb.createdBy(),IPAddress }, "dataset");

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Product Rate Type " + txtProductRateTypeName.Text.Trim() + " " + success + " Exists.");
                            GetProductRateType();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Product Rate Type " + txtProductRateTypeName.Text.Trim() + " " + error + " Exists.");
                                GetProductRateType();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds1.Clear();
                    }

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Route Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertorUpdateProductRateType();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "EditRequest")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblProductRateTypeName = (Label)row.FindControl("lblProductRateTypeName");
                    Label lblIsActive = (Label)row.FindControl("lblIsActive");

                    txtProductRateTypeName.Text = lblProductRateTypeName.Text;
                    if (lblIsActive.Text=="True")
                    {
                        chkIsActive.Checked = true;
                    }
                    else
                    {
                        chkIsActive.Checked = false;
                    }
                    btnSubmit.Text = "Update";
                    ViewState["rowid"] = e.CommandArgument.ToString();
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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
}