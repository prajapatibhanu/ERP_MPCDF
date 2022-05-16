using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Web.UI.HtmlControls;

public partial class mis_Payroll_SetDA_OfficeWise : System.Web.UI.Page
{
    DataSet ds1, ds2 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtApplicableDate.Text = Date;
                txtApplicableDate.Attributes.Add("readonly", "readonly");
                GetDAData();
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
    private void GetDAData()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_ParollDA",
                     new string[] { "Flag", "Office_ID" },
                     new string[] { "1", objdb.Office_ID() }, "dataset");

            Repeater1.DataSource = null;
            Repeater1.DataBind();

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataSource = ds1.Tables[0];
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();

                Repeater1.DataSource = ds1.Tables[1];
                Repeater1.DataBind();
            }


        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void Clear()
    {
        txtApplicableDate.Text = string.Empty;
        txtDA.Text = string.Empty;
        btnSubmit.Text = "Save";
        string Date = DateTime.Now.ToString("dd/MM/yyyy");
        txtApplicableDate.Text = Date;

        foreach (RepeaterItem itemEquipment in Repeater1.Items)
        {
            HtmlTableRow tr = (HtmlTableRow)itemEquipment.FindControl("trID");
            tr.Attributes.Remove("style");
        }
    }
    private void InsertandUpdate()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DateTime apdate = DateTime.ParseExact(txtApplicableDate.Text, "dd/MM/yyyy", culture);
                string apdat = apdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string IPAddress = Request.UserHostAddress;

                if (btnSubmit.Text == "Save")
                {
                    ds2 = objdb.ByProcedure("USP_Mst_ParollDA",
                    new string[] { "Flag", "ParollDA_Per", "EffectiveDate", "Office_ID"
                                  , "LastUpdatedBy", "LastUpdatedByIP" },
                    new string[] { "2", txtDA.Text.Trim(), apdat.ToString(), objdb.Office_ID(),
                                      objdb.createdBy(), IPAddress.ToString() }, "dataset");

                    if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        ds2.Dispose();
                        GetDAData();
                        Clear();
                    }
                    else if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string already = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning", "Warning :" + already);
                    }
                    else
                    {
                        string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", "Sorry :" + error);
                    }
                }
                else if (btnSubmit.Text == "Update")
                {
                    if(hfrowid.Value!="")
                    {
                        ds2 = objdb.ByProcedure("USP_Mst_ParollDA",
                          new string[] { "Flag","ParollDA_Id", "ParollDA_Per", "EffectiveDate", "Office_ID"
                                          , "LastUpdatedBy", "LastUpdatedByIP" },
                          new string[] { "3",hfrowid.Value, txtDA.Text.Trim(), apdat.ToString(), objdb.Office_ID(),
                                              objdb.createdBy(), IPAddress.ToString() }, "dataset");
                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            ds2.Dispose();
                            GetDAData();
                            Clear();
                        }
                        else if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "DateError")
                        {
                            string DateError = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning", "Warning :" + DateError);
                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", "Sorry :" + error);
                        }
                    }
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertandUpdate();
        }

    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (Repeater1.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                lblFooter.Visible = true;
            }
        }
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        try
        {
            if (e.CommandName == "RecordUpdate")
            {
                Label lblParollDA_Per = e.Item.FindControl("lblParollDA_Per") as Label;
                Label lblEffectiveDate = e.Item.FindControl("lblEffectiveDate") as Label;
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trID");

                txtDA.Text = lblParollDA_Per.Text;
                txtApplicableDate.Text = lblEffectiveDate.Text;
                hfrowid.Value = e.CommandArgument.ToString();
                btnSubmit.Text = "Update";
                tr.Attributes.Add("style", "background-color:#999999;color:#FFFFFF;");
            }
            else if (e.CommandName == "RecordView")
            {
                lblMsg.Text = string.Empty;
                  ds2 = objdb.ByProcedure("USP_Mst_ParollDA",
                          new string[] { "Flag","ParollDA_Id" },
                          new string[] { "4", e.CommandArgument.ToString() }, "dataset");

                if(ds2.Tables[0].Rows.Count>0)
                {
                    Repeater2.DataSource = ds2.Tables[0];
                    Repeater2.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                }
                ds2.Dispose();
            }

        }

        catch (Exception ex)
        {
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
       
    }
}