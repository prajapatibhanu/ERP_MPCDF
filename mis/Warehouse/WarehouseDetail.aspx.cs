using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Warehouse_WarehouseDetail : System.Web.UI.Page
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
            BindOpeningStockItem(); //Call function for fill gridview
        }
    }

    private void BindOpeningStockItem()
    {
        try
        {
            lblError.Text = "";
            ds = objdb.ByProcedure("Proc_tblSpItemStock",
                new string[] { "flag", "Warehouse_id" },
                new string[] { "9", objdb.Decrypt(Request.QueryString["id"].ToString()) }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                GVWarehouseItemDetail.DataSource = ds;
                GVWarehouseItemDetail.DataBind();
                //GVDivAuditProcess.DataSource = ds;
                //GVDivAuditProcess.DataBind();
            }
            else
            {
                GVWarehouseItemDetail.DataSource = null;
                GVWarehouseItemDetail.DataBind();
                //GVDivAuditProcess.DataSource = null;
                //GVDivAuditProcess.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 1). " + ex.Message.ToString());
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
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 10). " + ex.Message.ToString());
        }
    }
}