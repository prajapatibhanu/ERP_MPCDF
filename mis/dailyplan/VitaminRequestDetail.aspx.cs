using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Web;
using System.Web.UI;

public partial class mis_dailyplan_VitaminRequestDetail : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string Fdate = "";
    string Tdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {

                  
                    
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    if (Session["IsSuccess"] != null)
                    {
                        if ((Boolean)Session["IsSuccess"] == true)
                        {
                            Session["IsSuccess"] = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Vitamin Dispatch Successfully !')", true);

                        }
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    FillOffice();
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }

    #region User Defined Function
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
                ddlFilterOffice.DataSource = ds.Tables[0];
                ddlFilterOffice.DataTextField = "Office_Name";
                ddlFilterOffice.DataValueField = "Office_ID";
                ddlFilterOffice.DataBind();
                ddlFilterOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlFilterOffice.SelectedValue = ViewState["Office_ID"].ToString();
                ddlFilterOffice.Enabled = false;


                ddlFilterOffice.DataSource = ds.Tables[0];
                ddlFilterOffice.DataTextField = "Office_Name";
                ddlFilterOffice.DataValueField = "Office_ID";
                ddlFilterOffice.DataBind();
                ddlFilterOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlFilterOffice.SelectedValue = ViewState["Office_ID"].ToString();
                ddlFilterOffice.Enabled = false;
                

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
    protected void FillGrid()
    {
        try
        {
            btnExport.Visible = false;

            if (txtFromDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            }
            if (txtFromDate.Text != "")
            {
                Tdate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            }

            GridView1.DataSource = null;
            GridView1.DataBind();
            btnExport.Visible = false;
            btnShowntoplant.Visible = false;
            ds = null;
            ds = objdb.ByProcedure("Usp_Production_VitaminRequest"
            , new string[] { "flag", "Office_Id", "FromDate", "ToDate" }
            , new string[] { "2", ViewState["Office_ID"].ToString(), Fdate, Tdate }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds;
                        GridView1.DataBind();

                        btnExport.Visible = true;
                        btnShowntoplant.Visible = true;

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
    #endregion

  
    #region Row Command Event
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow Row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblVitaminReceivedQuantity = (Label)Row.FindControl("lblVitaminReceivedQuantity");
            TextBox txtVitaminReceivedQuantity = (TextBox)Row.FindControl("txtVitaminReceivedQuantity");
            LinkButton lnkbtnDispatch = (LinkButton)Row.FindControl("lnkbtnDispatch");
            LinkButton lnkSave = (LinkButton)Row.FindControl("lnkSave");
            string VitaminRequest_ID = e.CommandArgument.ToString();
            if(e.CommandName== "Dispatch")
            {
                lblVitaminReceivedQuantity.Visible = false;
                txtVitaminReceivedQuantity.Visible = true;
                lnkbtnDispatch.Visible = false;
                lnkSave.Visible = true;
            }
            else if(e.CommandName== "Save")
            {
                ds = objdb.ByProcedure("Usp_Production_VitaminRequest", new string[] { "flag", "VitaminRequest_ID", "VitaminReceivedQuantity", "Request_Status", "CreatedBy", "CreatedBy_IP" }, new string[] { "3", VitaminRequest_ID, txtVitaminReceivedQuantity.Text,"Dispatch",objdb.createdBy(),objdb.GetLocalIPAddress() }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            Session["IsSuccess"] = true;
                            Response.Redirect("VitaminRequestDetail.aspx", false);


                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                            if (error == "Already Exists.")
                            {
                                Session["IsSuccess"] = false;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Testing Request already Send.');", true);

                            }
                            else
                            {
                                Session["IsSuccess"] = false;
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
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
    #endregion

    #region Button Click Event
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
		    GridView1.Columns[13].Visible = false;
            GridView1.Columns[14].Visible = false;
            GridView1.Columns[8].Visible = false;
            GridView1.Columns[9].Visible = true;
            lblMsg.Text = "";
            string FileName = Session["Office_Name"].ToString() + "_" + "VitaminRequestDetails_";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView1.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            GridView1.Columns[12].Visible = true;
            Response.End();
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   
    protected void btnShowntoplant_Click(object sender, EventArgs e)
    {
        try
        {
            string ShownToPlant = "0";
            foreach(GridViewRow row in GridView1.Rows)
            {

                CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                if(chkselect.Checked == true)
                {
                    ShownToPlant = "1";
                }
                else
                {
                    ShownToPlant = "0";
                }
                objdb.ByProcedure("Usp_Production_VitaminRequest", new string[] { "flag", "VitaminRequest_ID", "ShownToPlant" }, new string[] { "4", chkselect.ToolTip.ToString(), ShownToPlant }, "dataset");
            }
            FillGrid();
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", "Record Updated Successfully");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    #endregion
}