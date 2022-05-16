using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Warehouse_WarehouseAuditHistory : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        //check session
        if (Session["Emp_ID"] == null)
        {
            Response.Redirect("~/mis/Login.aspx");
        }

        if (!IsPostBack)
        {
            fillAuditQty();
        }
    }

    private void fillAuditQty()
    {
        try
        {
            ds = objdb.ByProcedure("SptblWarehouseAudit", 
                           new string[] { "flag", "Warehouse_id", "OfficeId" },
                           new string[] { "3", objdb.Decrypt(Request.QueryString["id"].ToString()), objdb.Office_ID() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GVDivAuditProcess.DataSource = ds;
                GVDivAuditProcess.DataBind();
            }
            else
            {
                GVDivAuditProcess.DataSource = null;
                GVDivAuditProcess.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 1). " + ex.Message.ToString());
        }
    }

    protected void GVDivAuditProcess_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            try
            {
                ds = objdb.ByProcedure("SptblWarehouseAudit",
                    new string[] { "flag", "Warehouse_id", "AuditDate"},
                    new string[] { "4", objdb.Decrypt(Request.QueryString["id"].ToString()), GVDivAuditProcess.SelectedDataKey.Value.ToString() }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    GVAuditHistory.DataSource = ds;
                    GVAuditHistory.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModel();", true);
                }
                else
                {
                    GVAuditHistory.DataSource = null;
                    GVAuditHistory.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 2). " + ex.Message.ToString());
            }
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/mis/Warehouse/WarehouseList.aspx?id=" + Request.QueryString["id"].ToString() + "");
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 3). " + ex.Message.ToString());
        }
    }
}