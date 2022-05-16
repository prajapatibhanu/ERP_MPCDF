using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;

public partial class mis_Masters_InactiveDuplicateEntry : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds2;
    IFormatProvider cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {

            if (!Page.IsPostBack)
            {
                
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillGrid();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void FillGrid()
    {
        try
        {
            //lblMsg.Text = "";
            GridView1.DataSource = string.Empty;
            GridView1.DataBind();
            ds = objdb.ByProcedure("Usp_InactiveOfficeMasterData", new string[] { "flag", "Office_Parant_ID" }, new string[] { "1", objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
            string Office_ID = e.CommandArgument.ToString();
            if(e.CommandName == "DeleteRecord")
            {
                objdb.ByProcedure("Usp_InactiveOfficeMasterData", new string[] { "flag", "Office_ID", "DeletedBy", "DeletedBy_IP" }, new string[] { "2", Office_ID,objdb.createdBy(),objdb.GetLocalIPAddress() }, "dataset");
               
                lblMsg.Text = objdb.Alert("fa-success", "alert-success", "Thankyou!", "Record Deleted Successfully");
                FillGrid();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}