using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_ProductionQCMaster : System.Web.UI.Page
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
                    ViewState["ProductSection_ID"] = "0";
                    FillGrid();

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
            ds = objdb.ByProcedure("spProduction_LabTesting",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                chkOffice.DataSource = ds.Tables[0];
                chkOffice.DataTextField = "Product_Name";
                chkOffice.DataValueField = "Product_ID";
                chkOffice.DataBind();
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
            string ProductSection_IsActive = "1";
            string SectionMapping_IsActive = "1";

            if (msg.Trim() == "")
            {
                int Status = 0;
                DataSet ds11 = objdb.ByProcedure("spProduction_LabTesting",
                    new string[] { "flag", "QCParameterName", "QCParameterID" },
                    new string[] { "10", txtProdSecName.Text, ViewState["ProductSection_ID"].ToString() }, "dataset");
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());

                }
                if (btnSubmit.Text == "Submit" && ViewState["ProductSection_ID"].ToString() == "0" && Status == 0)
                {
                    ds = objdb.ByProcedure("spProduction_LabTesting",
                        new string[] { "flag", "QCParameterName", "IsActive" },
                                new string[] { "7", txtProdSecName.Text,ProductSection_IsActive }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ProductSection_ID = ds.Tables[0].Rows[0]["ProductSection_ID"].ToString();
                        if (ProductSection_ID != "")
                        {
                            foreach (ListItem item in chkOffice.Items)
                            {
                                if (item.Selected == true)
                                {
                                    objdb.ByProcedure("spProduction_LabTesting",
                                        new string[] { "flag", "QCParameterID", "ProductID", "IsActive" },
                                        new string[] { "8", ProductSection_ID, item.Value, SectionMapping_IsActive }, "dataset");
                                }
                            }
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                            ClearData();
                            FillGrid();
                        }
                    }
                }
                else if (btnSubmit.Text == "Update" && ViewState["ProductSection_ID"].ToString() != "0" && Status == 0)
                {

                    objdb.ByProcedure("spProduction_LabTesting",
                              new string[] { "flag", "QCParameterID", "QCParameterName" },
                                      new string[] { "11", ViewState["ProductSection_ID"].ToString(), txtProdSecName.Text}, "dataset");

                    foreach (ListItem item in chkOffice.Items)
                    {

                        if (item.Selected == true)
                        {
                            objdb.ByProcedure("spProduction_LabTesting",
                                new string[] { "flag", "QCParameterID", "ProductID", "IsActive" },
                                new string[] { "13", ViewState["ProductSection_ID"].ToString(),item.Value,"1"}, "dataset");
                        }
                        else
                        {
                            objdb.ByProcedure("spProduction_LabTesting",
                                new string[] { "flag", "QCParameterID", "ProductID", "IsActive" },
                                new string[] { "13", ViewState["ProductSection_ID"].ToString(),item.Value,"0"}, "dataset");
                        }
                    }
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearData();                                      
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Product Section Name is already exist.');", true);
                    //ClearData();

                }
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
            txtProdSecName.Text = "";
            chkDugdhSangh.Checked = false;
            chkOffice.ClearSelection();
            btnSubmit.Text = "Submit";
            ViewState["ProductSection_ID"] = "0";
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
            ds = objdb.ByProcedure("spProduction_LabTesting", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
            }
            GridView1.DataBind();
            //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            //GridView1.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string ProductSection_ID = GridView1.SelectedDataKey.Value.ToString();
            ViewState["ProductSection_ID"] = ProductSection_ID.ToString();
            ds = objdb.ByProcedure("spProduction_LabTesting", new string[] { "flag", "QCParameterID" }, new string[] { "14", ProductSection_ID }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtProdSecName.Text = ds.Tables[0].Rows[0]["QCParameterName"].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    chkOffice.ClearSelection();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        string Value = ds.Tables[1].Rows[i]["ProductID"].ToString();
                        foreach (ListItem item in chkOffice.Items)
                        {
                            if (item.Value == Value)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }

                btnSubmit.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //try
        //{
        //    lblMsg.Text = "";
        //    string ProductSection_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
        //    objdb.ByProcedure("SPProductionSectionMaster", new string[] { "flag", "ProductSection_ID", "ProductSection_IsActive", "ProductSection_UpdatedBy" }, new string[] { "5", ProductSection_ID, "0", ViewState["Emp_ID"].ToString() }, "dataset");
        //    FillGrid();
        //    ClearData();

        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        //}
    }
}