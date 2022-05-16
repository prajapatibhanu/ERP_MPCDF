using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;

public partial class mis_MilkCollection_BMCChillingCostMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds2;
    IFormatProvider cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!IsPostBack)
            {
                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                //FillSociety();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");
                FillGrid();
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

    protected void FillSociety()
    {
        try
        {
            lblMsg.Text = "";
            ddLBMC.Items.Clear();
            string code = ddlMilkCollectionUnit.SelectedValue;
            if(ddlTemp.SelectedIndex > 0)
            {
                ds = null;
                ds = objdb.ByProcedure("SpAdminOffice",
                                            new string[] { "flag", "OfficeType_ID", "Office_Parant_ID" },
                                            new string[] { "32", code, objdb.Office_ID() }, "TableSave");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddLBMC.DataTextField = "Office_Name";
                        ddLBMC.DataValueField = "Office_ID";
                        ddLBMC.DataSource = ds.Tables[0];
                        ddLBMC.DataBind();
                        //ddLBMC.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        //ddLBMC.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    //ddLBMC.Items.Insert(0, new ListItem("Select", "0"));
                }
                DataSet ds1 = objdb.ByProcedure("Sp_Mst_BMCChillingCost", new string[] { "flag", "OfficeType_ID", "Temp", "EffectiveDate" }, new string[] { "4", objdb.OfficeType_ID(), ddlTemp.SelectedValue,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds1 != null && ds1.Tables.Count > 0)
                {
                    int Count = ds1.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        int OfficeID = int.Parse(ds1.Tables[0].Rows[i]["Office_ID"].ToString());
                        foreach (ListItem item in ddLBMC.Items)
                        {
                            if (item.Value == OfficeID.ToString())
                            {
                                item.Selected = true;
                                item.Enabled = false;
                            }
                        }
                    }
                }
            }
            else
            {
                ddlMilkCollectionUnit.ClearSelection();
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Please Select Temperature First");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally { if (ds != null) { ds.Dispose(); } }

    }
    private DataTable GetDetail()
    {
         lblMsg.Text = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("Office_ID",typeof(string));
           
            foreach (ListItem item in ddLBMC.Items)
            {
                if (item.Selected)
                {
                    if(item.Enabled == true)
                    {
                        dt.Rows.Add(item.Value);
                    }
                }
            }
        
        return dt;
        
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string IsActive = "1";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                
                    DataTable dt = GetDetail();
                    if(dt.Rows.Count > 0)
                    {
                        ds = objdb.ByProcedure("Sp_Mst_BMCChillingCost",
                     new string[] { "flag", "MilkCollectionUnit", "OfficeType_ID", "EffectiveDate", "Temp", "ChillingCost", "IsActive", "CreatedBy", "CreatedAt", "CreatedByIp" },
                     new string[] { "0", 
                      ddlMilkCollectionUnit.SelectedItem.Text,
                      objdb.OfficeType_ID(),
                      Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                      ddlTemp.SelectedValue,
                      txtChillingCost.Text,
                      IsActive,
                      objdb.createdBy(),
                      objdb.Office_ID(),
                      objdb.GetLocalIPAddress() }
                     , new string[] { "type_AddSocietyForBMCChillingCost"  },
                                              new DataTable[] { dt}, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());

                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning.ToString());

                        }
                        else
                        {
                            string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Danger.ToString());
                        }
                        ClearText();
                        FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Please Select At least one society");
                    }
                    
               
              
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally { if (ds != null) { ds.Dispose(); } }
    }
    protected void FillGrid()
    {
        try
        {
           // lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_Mst_BMCChillingCost", new string[] { "flag", "OfficeType_ID" }, new string[] {"1",objdb.OfficeType_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gvReport.DataSource = ds;
                    gvReport.DataBind();
                }
                else
                {
                    gvReport.DataSource = string.Empty;
                    gvReport.DataBind();
                }
            }
            else
            {
                gvReport.DataSource = string.Empty;
                gvReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally { if (ds != null) { ds.Dispose(); } }
        

    }
   
    protected void ClearText()
    {
        ddLBMC.ClearSelection();
        txtChillingCost.Text = "";
        ddlTemp.ClearSelection();
        btnSave.Text = "Save";
        
    }
    protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
    protected void ddlTemp_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
}

