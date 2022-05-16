using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_ThirdPartyUnionMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {

                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();                  
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

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string IsActive = "1";
            if(txtThirdPartyUnion_Name.Text == "")
            {
                msg += "Enter Third Party Union Name.";
            }
            if(msg == "")
            {
                if(btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("SpThirdPartyUnion",new string[]{"flag","ThirdPartyUnion_Name", "ThirdPartyUnion_Address","Office_ID", "IsActive","CreatedBy", "CreatedByIP","TPGSTno"},
                                                               new string[]{"0",txtThirdPartyUnion_Name.Text,txtThirdPartyUnion_Address.Text,objdb.Office_ID(),IsActive,objdb.createdBy(),objdb.GetLocalIPAddress(),txtGST.Text},"dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                FillGrid();
                                ClearText();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            }
                            else
                            {
                                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error :" + error);
                            }
                        }
                    }
                }
                if (btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("SpThirdPartyUnion", new string[] { "flag", "ThirdPartyUnion_Id", "ThirdPartyUnion_Name", "ThirdPartyUnion_Address", "CreatedBy", "CreatedByIP", "TPGSTno" },
                                                                new string[] { "3", ViewState["ThirdPartyUnion_Id"].ToString(), txtThirdPartyUnion_Name.Text, txtThirdPartyUnion_Address.Text, objdb.createdBy(), objdb.GetLocalIPAddress(), txtGST.Text }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                FillGrid();
                                ClearText();
                                btnSave.Text = "Save";                               
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            }
                            else
                            {
                                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error :" + error);
                            }
                        }
                    }

                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),"CallMyFunction","alert('"+ msg +"')",true);
            }
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
            Gridview1.DataSource = new string[] { };
            Gridview1.DataBind();
            ds = objdb.ByProcedure("SpThirdPartyUnion", new string[] { "flag","Office_ID" }, new string[] { "1",objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Gridview1.DataSource = ds;
                Gridview1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        txtThirdPartyUnion_Address.Text = "";
        txtThirdPartyUnion_Name.Text = "";
        txtGST.Text = "";

    }
    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditDetail")
            {
                string ThirdPartyUnion_Id = e.CommandArgument.ToString();
                ViewState["ThirdPartyUnion_Id"] = ThirdPartyUnion_Id.ToString();
                ds = null;
                ds = objdb.ByProcedure("SpThirdPartyUnion", new string[] { "flag", "ThirdPartyUnion_Id" }, new string[] { "2", ThirdPartyUnion_Id }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        txtThirdPartyUnion_Name.Text = ds.Tables[0].Rows[0]["ThirdPartyUnion_Name"].ToString();
                        txtThirdPartyUnion_Address.Text = ds.Tables[0].Rows[0]["ThirdPartyUnion_Address"].ToString();
                        txtGST.Text = ds.Tables[0].Rows[0]["TPGSTno"].ToString();
                    
                        btnSave.Text = "Update";
                    }
                    else
                    {

                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            Gridview1.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}