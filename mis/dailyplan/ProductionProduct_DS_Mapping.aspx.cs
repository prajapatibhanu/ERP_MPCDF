using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_ProductionProduct_DS_Mapping : System.Web.UI.Page
{
    DataSet ds, ds1, ds2, ds3, ds4;
    static DataSet ds5;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    FillItem();
                    FillProductSection();
                    ViewState["ProductDSMapping_ID"] = "0";
                    //FillGrid();

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDS.Enabled = false;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillProductSection()
    {
        try
        {

            //lblMsg.Text = "";
            ddlProductSection.Items.Clear();
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "0", ddlDS.SelectedValue.ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlProductSection.DataSource = ds.Tables[0];
                ddlProductSection.DataTextField = "ProductSection_Name";
                ddlProductSection.DataValueField = "ProductSection_ID";
                ddlProductSection.DataBind();

            }
            ddlProductSection.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillItem()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                 new string[] { "flag","Office_ID" },
                 new string[] { "1",ViewState["Office_ID"].ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                chkItem.DataSource = ds.Tables[0];
                chkItem.DataTextField = "ItemName";
                chkItem.DataValueField = "Item_id";
                chkItem.DataBind();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (msg.Trim() == "")
            {

                if (btnSubmit.Text == "Submit")
                {

                    foreach (ListItem item in chkItem.Items)
                    {
                        if (item.Selected == true)
                        {
                            string ProductDSMapping_IsActive = "1";
                            objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                                new string[] { "flag", "Product_ID", "Office_ID", "ProductSection_ID", "ProductDSMapping_IsActive", "ProductDSMapping_UpdatedBy" },
                                new string[] { "2", item.Value, ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ProductDSMapping_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
                        }
                        else
                        {
                            string ProductDSMapping_IsActive = "0";
                            objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                                new string[] { "flag", "Product_ID", "Office_ID", "ProductSection_ID", "ProductDSMapping_IsActive", "ProductDSMapping_UpdatedBy" },
                                new string[] { "2", item.Value, ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ProductDSMapping_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
                        }
                    }
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearData();
                    //FillGrid();


                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Product Section Name is already exist.');", true);
                    //ClearData(); 
                }
				
				FillOffice();
            FillItem();
            FillProductSection();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void ClearData()
    {
        try
        {
            ddlDS.ClearSelection();
            FillProductSection();
            chkItem.ClearSelection();
            chkAllItem.Checked = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillGrid()
    {
        try
        {

            GridView1.DataSource = null;
            ds = objdb.ByProcedure("SPProductionSectionMaster", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
            }
            GridView1.DataBind();
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlDS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillProductSection();
            FillMappingItem();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillMappingItem()
    {
        try
        {
            lblMsg.Text = "";
            chkItem.ClearSelection();
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping", new string[] { "flag", "Office_ID", "ProductSection_ID" }, new string[] { "3", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    string Value = ds.Tables[0].Rows[i]["Product_ID"].ToString();
                    foreach (ListItem item in chkItem.Items)
                    {
                        if (item.Value == Value)
                        {
                            
                            if (ds.Tables[0].Rows[i]["ProductSection_ID"].ToString() == ddlProductSection.SelectedValue.ToString())
                            {
                                item.Selected = true;
                                item.Enabled = true;
                            }
                            else if (ds.Tables[0].Rows[i]["ProductSection_ID"].ToString() != ddlProductSection.SelectedValue.ToString())
                            {
                                item.Enabled = false;
                                //item.Text = ds.Tables[0].Rows[i]["ItemName"].ToString() + "  <small style='color:blue'><i>" + ds.Tables[0].Rows[i]["ProductSection_Name"].ToString() + "</i></small>";
                            }
                            else
                            {
                                item.Enabled = true;
                            }
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
    protected void ddlProductSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            FillMappingItem();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}