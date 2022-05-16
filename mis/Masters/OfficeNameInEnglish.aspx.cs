using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;

public partial class mis_Masters_OfficeNameInEnglish : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            lblMsg.Text = "";

            if (!Page.IsPostBack)
            {
                GetOfficeType();
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


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOfficeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            OfficeName();
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID", "Office_Name_E", "Office_Code", "SocietyCode", "CreatedBy", "CreatedBy_IP" }, new string[] { "31", ddlOfficeName.SelectedValue, txtOffice_Name_E.Text, txtSocietyCode.Text,txtBillingCode.Text, objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                txtOffice_Name_E.Text = "";
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {

                    string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());
                    txtOffice_Name_E.Text = "";
                    txtSocietyCode.Text = "";
                    txtBillingCode.Text = "";
                    OfficeName();
                   

                }

                else
                {
                    string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Success.ToString());
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtSocietyCode.Text = "";
            txtOffice_Name_E.Text = "";
            if(ddlOfficeName.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID" }, new string[] { "30", ddlOfficeName.SelectedValue }, "dataset");
                if(ds != null && ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        txtSocietyCode.Text = ds.Tables[0].Rows[0]["Office_Code"].ToString();
                        txtOffice_Name_E.Text = ds.Tables[0].Rows[0]["Office_Name_E"].ToString();
                        txtBillingCode.Text = ds.Tables[0].Rows[0]["SocietyCode"].ToString();

                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void OfficeName()
    {
        try
        {
            ddlOfficeName.Items.Clear();
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "OfficeType_ID", "Office_Parant_ID" }, new string[] { "32", ddlOfficeType.SelectedValue, objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();

            }
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
}