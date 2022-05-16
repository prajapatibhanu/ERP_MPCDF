using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;

public partial class mis_Masters_UpdateBMCDCS : System.Web.UI.Page
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
                GetOfficeDetails();
                
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void ddlOfficeType_Init(object sender, EventArgs e)
    {
        GetOfficeType();
    }
    private void GetOfficeType()
    {
        try
        {
            ddlOfficeType.DataSource = objddl.OfficeTypeFill();
            ddlOfficeType.DataTextField = "OfficeType_Title";
            ddlOfficeType.DataValueField = "OfficeType_ID";
            ddlOfficeType.DataBind();

            if (objdb.OfficeType_ID() == objdb.GetHOType_Id().ToString())
            {
                for (int i = ddlOfficeType.Items.Count - 1; i >= 0; i--)
                {
                    if (ddlOfficeType.Items[i].Value != objdb.GetDSType_Id().ToString())// for show only dugdh sangh
                    {
                        ddlOfficeType.Items.RemoveAt(i);
                    }
                }
            }
            else if (objdb.OfficeType_ID() == objdb.GetDSType_Id().ToString())
            {
                for (int i = ddlOfficeType.Items.Count - 1; i >= 0; i--)
                {
                    if (ddlOfficeType.Items[i].Value == objdb.GetHOType_Id().ToString() || ddlOfficeType.Items[i].Value == objdb.GetDSType_Id().ToString())// for show only dugdh sangh
                    {
                        ddlOfficeType.Items.RemoveAt(i);
                    }
                }
            }
            ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));

            // if(objdb.Office_ID)
            //ddlOfficeType.Items.Remove(ddlOfficeType.Items.FindByValue("1"));
            //ddlOfficeType.Items.Remove(ddlOfficeType.Items.FindByValue(objdb.OfficeType_ID()));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetOfficeDetails()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice",
                    new string[] { "flag", "Office_Parant_ID" },
                    new string[] { "34",objdb.Office_ID()}, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
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
            lblMsg.Text = "";
            if (e.CommandName == "RecordUpdate")
            {
                DataSet ds1 = new DataSet();
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    string officeid = "";
                    officeid = e.CommandArgument.ToString();
                    ds = null;

                    ds1 = objdb.ByProcedure("SpAdminOffice",
                          new string[] { "flag", "Office_ID" },
                          new string[] { "16", officeid }, "dataset");
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {

                        txtOfficeName.Text = ds1.Tables[0].Rows[0]["Office_Name"].ToString();
                        txtOffice_Name_E.Text = ds1.Tables[0].Rows[0]["Office_Name_E"].ToString();
                        ddlOfficeType.ClearSelection();
                        ddlOfficeType.Items.FindByValue(ds1.Tables[0].Rows[0]["OfficeType_ID"].ToString()).Selected = true;
                        
                        btnUpdate.Enabled = true;

                    }
                    ViewState["rowid"] = e.CommandArgument;
                   
                    
                }


            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID", "Office_Name", "Office_Name_E", "OfficeType_ID" }, new string[] { "35", ViewState["rowid"].ToString(),txtOfficeName.Text,txtOffice_Name_E.Text, ddlOfficeType.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
            {
               
                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());
                ddlOfficeType.ClearSelection();
                txtOfficeName.Text = "";
                txtOffice_Name_E.Text = "";
                btnUpdate.Enabled = false;
                GetOfficeDetails();
            }
           
            else
            {
                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Success.ToString());
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}