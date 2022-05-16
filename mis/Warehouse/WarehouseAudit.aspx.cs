using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Warehouse_WarehouseAudit : System.Web.UI.Page
{
    DataSet ds, ds3;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        //check session
        if (Session["Emp_ID"] == null)
        {
            Response.Redirect("~/mis/Login.aspx");
        }

        if (!IsPostBack)
        {
            BindOpeningStockItem();
            // Disable textbox control in gridview if Date not selected
            TextBox txtAuditQty = new TextBox();
            foreach (GridViewRow row in GVDivAuditProcess.Rows)
            {
                GridViewRow selectedRow = row;
                txtAuditQty = (TextBox)selectedRow.FindControl("txtAuditQty");
                txtAuditQty.Text = "";
                txtAuditQty.Enabled = false;
            }
        }
    }

    protected void txtAuditDate_TextChanged(object sender, EventArgs e)
    {
        if (txtAuditDate.Text != "")
        {
            lblError.Text = "";
            TextBox txtAuditQty = new TextBox();
            foreach (GridViewRow row in GVDivAuditProcess.Rows)
            {
                GridViewRow selectedRow = row;
                txtAuditQty = (TextBox)selectedRow.FindControl("txtAuditQty");
                txtAuditQty.Text = "";
                txtAuditQty.Enabled = true;
            }
            fillAuditQty();
        }
        else
        {
            lblError.Text = "";
            TextBox txtAuditQty = new TextBox();
            foreach (GridViewRow row in GVDivAuditProcess.Rows)
            {
                GridViewRow selectedRow = row;
                txtAuditQty = (TextBox)selectedRow.FindControl("txtAuditQty");
                txtAuditQty.Text = "";
                txtAuditQty.Enabled = false;
            }
            cleargrid();
        }
    }

    private void cleargrid()
    {
        TextBox txtAuditQty = new TextBox();
        foreach (GridViewRow row in GVDivAuditProcess.Rows)
        {
            GridViewRow selectedRow = row;
            txtAuditQty = (TextBox)selectedRow.FindControl("txtAuditQty");
            txtAuditQty.Text = "";
            btnSave.Enabled = false;
        }
    }

    private void fillAuditQty()
    {
        if (Page.IsValid)
        {
            try
            {
                cleargrid(); // clear record
                ds = objdb.ByProcedure("SptblWarehouseAudit",
                    new string[] { "flag", "Emp_Id", "AuditDate", "Warehouse_id", "OfficeId" },
                    new string[] { "1", Session["Emp_ID"].ToString(), 
                               Convert.ToDateTime(txtAuditDate.Text, cult).ToString("yyyy/MM/dd"), 
                               objdb.Decrypt(Request.QueryString["id"].ToString()), 
                               objdb.Office_ID() }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    TextBox txtAuditQty = new TextBox();
                    foreach (GridViewRow row in GVDivAuditProcess.Rows)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (row.Cells[1].Text == ds.Tables[0].Rows[i]["Item_id"].ToString())
                            {
                                GridViewRow selectedRow = row;
                                txtAuditQty = (TextBox)selectedRow.FindControl("txtAuditQty");
                                txtAuditQty.Text = ds.Tables[0].Rows[i]["AuditQuantity"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 1). " + ex.Message.ToString());
            }
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
                GVDivAuditProcess.DataSource = ds;
                GVDivAuditProcess.DataBind();
                txtAuditDate.Visible = true;
                btnSave.Visible = true;
            }
            else
            {
                GVDivAuditProcess.DataSource = null;
                GVDivAuditProcess.DataBind();
                DvAuditDate.Visible = false;
                btnSave.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 2). " + ex.Message.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                ds3 = objdb.ByProcedure("Proc_tblSpItemStock",
                          new string[] { "flag", "Warehouse_id" },
                          new string[] { "9", objdb.Decrypt(Request.QueryString["id"].ToString()) }, "dataset");

                for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                {
                    TextBox txtAuditQty = new TextBox();
                    foreach (GridViewRow row in GVDivAuditProcess.Rows)
                    {
                        if (row.Cells[1].Text == ds3.Tables[0].Rows[i]["Item_id"].ToString())
                        {
                            GridViewRow selectedRow = row;
                            txtAuditQty = (TextBox)selectedRow.FindControl("txtAuditQty");
                            if (txtAuditQty.Text != "")
                            {
                                ds = objdb.ByProcedure("SptblWarehouseAudit",
                                    new string[] { "flag", "Item_id", "Emp_Id", "Warehouse_id", "AuditQuantity", "AuditDate", "OfficeId" },
                                    new string[] { "2", ds3.Tables[0].Rows[i]["Item_id"].ToString(), 
                                            Session["Emp_ID"].ToString(), 
                                            objdb.Decrypt(Request.QueryString["id"].ToString()), 
                                            txtAuditQty.Text, 
                                            Convert.ToDateTime(txtAuditDate.Text, cult).ToString("yyyy/MM/dd"), 
                                            Session["Office_ID"].ToString() }, "dataset");
                            }
                        }
                    }
                }

                lblError.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Process Successfully Completed!");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 3). " + ex.Message.ToString());
        }
    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModel();", true);
        }
    }

    protected void txtAuditQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            float a = 0, b = 0;
            ds = objdb.ByProcedure("Proc_tblSpItemStock",
                               new string[] { "flag", "Warehouse_id" },
                               new string[] { "13", objdb.Decrypt(Request.QueryString["id"].ToString()) }, "dataset");

            TextBox txtAuditQty = new TextBox();
            foreach (GridViewRow row in GVDivAuditProcess.Rows)
            {
                GridViewRow selectedRow = row;
                txtAuditQty = (TextBox)selectedRow.FindControl("txtAuditQty");

                if (txtAuditQty.Text != "")
                {
                    a = float.Parse(txtAuditQty.Text.ToString());
                    b = float.Parse(ds.Tables[0].Rows[selectedRow.RowIndex]["Cr"].ToString());
                    if (a <= b)
                    {
                        lblError.Text = "";
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        lblError.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Audit Quantity should not grater then stock quantity. <br /> आडिट मात्रा स्टॉक मात्रा से अधिक नहीं होनी चाहिए।");
                        btnSave.Enabled = false;
                        break;
                    }
                }
                else
                {
                    //lblError.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 4). " + ex.Message.ToString());
        }
    }

    protected void GVDivAuditProcess_Load(object sender, EventArgs e)
    {
        TextBox txtAuditQty = new TextBox();
        foreach (GridViewRow row in GVDivAuditProcess.Rows)
        {
            GridViewRow selectedRow = row;
            txtAuditQty = (TextBox)selectedRow.FindControl("txtAuditQty");
            if (txtAuditQty.Text != "")
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        string id = objdb.Decrypt(Request.QueryString["id"].ToString());
        Response.Redirect("~/mis/Warehouse/WarehouseList.aspx?id=" + objdb.Encrypt(id) + "");
    }
}
