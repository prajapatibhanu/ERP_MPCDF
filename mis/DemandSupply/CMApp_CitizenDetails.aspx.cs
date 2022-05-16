using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class mis_DemandSupply_CMApp_CitizenDetails : System.Web.UI.Page
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
                GetCitizen();
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
    protected void GetCitizen()
    {
        try
        {
            ddlCitizen.DataTextField = "CName";
            ddlCitizen.DataValueField = "CitizenId";
            ddlCitizen.DataSource = objdb.ByProcedure("USP_CMApp_CitizenOrderRpt",
                 new string[] { "Flag" },
                   new string[] { "2" }, "dataset");
            ddlCitizen.DataBind();
            ddlCitizen.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Citizen", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            GetCitizenOrderReport();
          
        }
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
    private void GetCitizenOrderReport()
    {
        try
        {


            ds1 = objdb.ByProcedure("USP_CMApp_Mst_CitizenReg",
                     new string[] { "Flag", "CitizenId", "CitizenStatus" },
                       new string[] { "13",ddlCitizen.SelectedValue,ddlStatus.SelectedValue }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0 && ds1 != null)
            {
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
                pnlsearch.Visible = true;
            }
            else
            {
                pnlsearch.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! ", "Record Not Found.");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2: ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ddlCitizen.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        pnlsearch.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (e.CommandName == "CmdIsActive")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                    HiddenField HFIsActive = (HiddenField)row.FindControl("HFIsActive");

                    if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                    {
                        lblMsg.Text = string.Empty;
                        string CS_Status = "";
                        if (HFIsActive.Value == "True")
                        {
                            CS_Status = "False";
                        }
                        else if (HFIsActive.Value == "False")
                        {
                            CS_Status = "True";
                        }
                        else
                        {
                            CS_Status = "True";
                        }

                        ds1 = objdb.ByProcedure("USP_CMApp_Mst_CitizenReg",
                                    new string[] { "Flag", "CitizenId", "IsActive" },
                                    new string[] { "11", e.CommandArgument.ToString(), CS_Status }, "TableSave");

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetCitizenOrderReport();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        ds1.Dispose();
                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}