using System;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;


public partial class mis_Warehouse_WarehouseDetail : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();


                    //ds = objdb.ByProcedure("SpAdminOffice",
                    //       new string[] { "flag" },
                    //       new string[] { "1" }, "dataset");
                    ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "54" }, "dataset");
                    ddloffice.DataSource = ds;
                    ddloffice.DataTextField = "Office_Name";
                    ddloffice.DataValueField = "Office_ID";
                    ddloffice.DataBind();
                    ddloffice.Items.Insert(0, new ListItem("All", "0"));
                    ddloffice.SelectedValue = ViewState["Office_ID"].ToString();
                    ddloffice.Enabled = false;

                    //if (objdb.Office_ID().ToString() == objdb.GetHOId().ToString())
                    //{
                    //    ddloffice.Enabled = true;
                    //}
                    //else
                    //{
                    //    ddloffice.Enabled = false;
                    //}

                    if (ddloffice.SelectedIndex == 0)
                    {
                        Allfillgrid();
                    }
                    else
                    {
                        fillgrid();
                    }

                    if (Request.QueryString["id"] != null)
                    {
                        ds = objdb.ByProcedure("SpWarehouseMaster",
                                 new string[] { "flag", "WrId" },
                                 new string[] { "4", objdb.Decrypt(Request.QueryString["id"].ToString()) }, "dataset");

                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            ddloffice.SelectedValue = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                            fillgrid();
                        }
                    }
                
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GVWarehouseList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string id = GVWarehouseList.SelectedDataKey.Value.ToString();

            if (id != "")
            {
                Response.Redirect("~/mis/Warehouse/WarehouseUpdate.aspx?id=" + objdb.Encrypt(id.ToString()) + "");
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Selected Data Key not found.");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    public void fillgrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpWarehouseMaster",
                         new string[] { "flag", "OfficeId" },
                         new string[] { "0", ddloffice.SelectedValue }, "dataset");
            GVWarehouseList.DataSource = ds;
            GVWarehouseList.DataBind();
            GVWarehouseList.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVWarehouseList.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void Allfillgrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpWarehouseMaster",
                         new string[] { "flag" },
                         new string[] { "5" }, "dataset");
            GVWarehouseList.DataSource = ds;
            GVWarehouseList.DataBind();
            GVWarehouseList.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVWarehouseList.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddloffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddloffice.SelectedIndex != 0)
        {
            fillgrid();
        }
        else
        {
            Allfillgrid();
        }
    }
}