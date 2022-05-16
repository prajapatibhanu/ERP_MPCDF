using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_MilkCollection_Deo_EntryDetail : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["Deo_Id"] = "0";
                FillGrid();
                BindDeoEmp();
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

    protected void FillGrid()
    {
        try
        {

            ds = null;
            ds = objdb.ByProcedure("Usp_DeoDetail",
                              new string[] { "flag" },
                              new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {

                gvDeoDetail.DataSource = ds;
                gvDeoDetail.DataBind();

            }
            else
            {

                gvDeoDetail.DataSource = null;
                gvDeoDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";

                int isactive = 1;
                if (chkIsActive.Checked)
                {
                    isactive = 1;
                }
                else
                {
                    isactive = 0;
                }

                if (btnSubmit.Text == "Submit")
                {

                    DataSet DsExitsance = objdb.ByProcedure("Usp_DeoDetail",
                                                   new string[] { "flag"  
				                                ,"DeoMobile" },
                                                   new string[] { "7",
                                                txtDeoMobile.Text }, "dataset");

                    if (DsExitsance.Tables[0].Rows.Count != 0)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", "DEO Detail (Mobile No) Already Exists");
                        return;
                    }
                    else
                    {
                        objdb.ByProcedure("Usp_DeoDetail",
                                                   new string[] { "flag", 
                                                 "DeoName"
				                                ,"DeoMobile" 
				                                ,"Status" },
                                                   new string[] { "0",
                                                txtDeoName.Text,txtDeoMobile.Text,isactive.ToString() }, "dataset");

                        txtDeoName.Text = "";
                        txtDeoMobile.Text = "";
                        FillGrid();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Successfully Saved");
                    }





                }
                if (btnSubmit.Text == "Update")
                {
                    objdb.ByProcedure("Usp_DeoDetail",
                                                new string[] { "flag", 
                                                 "DeoName"
				                                ,"DeoMobile" 
				                                ,"Status"
                                                ,"Deo_Id"},
                                                new string[] { "4",
                                                txtDeoName.Text,txtDeoMobile.Text,isactive.ToString(),ViewState["Deo_Id"].ToString() }, "dataset");

                    txtDeoName.Text = "";
                    txtDeoMobile.Text = "";
                    FillGrid();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Successfully Saved");

                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)gvDeoDetail.Rows[selRowIndex].FindControl("chkSelect");
            string Deo_Id = chk.ToolTip.ToString();
            string Deo_IsActive = "0";
            if (chk != null & chk.Checked)
            {
                Deo_IsActive = "1";
            }

            objdb.ByProcedure("Usp_DeoDetail",
                       new string[] { "flag", "Status", "Deo_Id" },
                       new string[] { "1", Deo_IsActive, Deo_Id }, "dataset");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void gvDeoDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["Deo_Id"] = gvDeoDetail.SelectedValue.ToString();
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("Usp_DeoDetail",
                       new string[] { "flag", "Deo_Id" },
                       new string[] { "3", ViewState["Deo_Id"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {

                txtDeoName.Text = ds.Tables[0].Rows[0]["DeoName"].ToString();
                txtDeoMobile.Text = ds.Tables[0].Rows[0]["DeoMobile"].ToString();

                string strIsactive = ds.Tables[0].Rows[0]["Status"].ToString();

                if (strIsactive == "1")
                {
                    chkIsActive.Checked = true;
                }
                else
                {
                    chkIsActive.Checked = false;
                }

                btnSubmit.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void BindDeoEmp()
    {

        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("Usp_DeoDetail",
                              new string[] { "flag" },
                              new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddldeoemp.DataTextField = "DeoName";
                    ddldeoemp.DataValueField = "Deo_Id";
                    ddldeoemp.DataSource = ds;
                    ddldeoemp.DataBind();
                    ddldeoemp.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddldeoemp.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddldeoemp.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void ddldeoemp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("Usp_DeoDetail",
                              new string[] { "flag", "Deo_Id" },
                              new string[] { "9", ddldeoemp.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = string.Empty;
                    GridView1.DataBind();
                }
            }
            else
            {
                GridView1.DataSource = string.Empty;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}