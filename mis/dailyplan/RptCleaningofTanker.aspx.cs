using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;
using System.Text;

public partial class mis_dailyplan_RptCleaningofTanker : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }
                lblMsg.Text = "";
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Attributes.Add("readonly", "readonly");
                txtTankerCleanedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTankerCleanedDate.Attributes.Add("readonly", "readonly");
                FillOffice();

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string CleaningOfTanker_ID = e.CommandArgument.ToString();
            ViewState["CleaningOfTanker_ID"] = CleaningOfTanker_ID.ToString();
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblStatus = (Label)row.FindControl("lblStatus");
            Label lblTankerCleaningRequest_No = (Label)row.FindControl("lblTankerCleaningRequest_No");
            Label lblRemark = (Label)row.FindControl("lblRemark");
            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
            LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
            LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");

            Label lblTankerNo = (Label)row.FindControl("lblTankerNo");
            if (e.CommandName == "EditRecord")
            {
                //lblStatus.Visible = false;
                //lblRemark.Visible = false;
                //txtRemark.Visible = true;
                //ddlStatus.Visible = true;
                //ddlStatus.ClearSelection();
                //ddlStatus.Items.FindByText(lblStatus.Text).Selected = true;
                //lnkEdit.Visible = false;
                //lnkUpdate.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTankerCleaningModal();", true);
                spnOfcName.InnerHtml = Session["Office_Name"].ToString();
                txtTankerNo.Text = lblTankerNo.Text;
                spnRequestNo.InnerHtml = "क्रं" + "&nbsp;&nbsp;    " + lblTankerCleaningRequest_No.Text;

            }
            else if (e.CommandName == "ViewRecord")
            {
                FillViewDetail(CleaningOfTanker_ID);
                
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
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
            //lblMsg.Text = "";
            btnShowntoplant.Visible = false;
            ds = objdb.ByProcedure("Usp_tbl_Production_CleaningofTanker", new string[] { "flag", "I_OfficeID", "FromDate", "ToDate" }, new string[] { "6", objdb.Office_ID(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
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
            foreach (GridViewRow row in GridView1.Rows)
            {

                CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                if (chkselect.Checked == true)
                {
                    ShownToPlant = "1";
                }
                else
                {
                    ShownToPlant = "0";
                }
                objdb.ByProcedure("Usp_tbl_Production_CleaningofTanker", new string[] { "flag", "CleaningOfTanker_ID", "ShownToPlant" }, new string[] { "7", chkselect.ToolTip.ToString(), ShownToPlant }, "dataset");
            }
            FillGrid();
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", "Record Updated Successfully");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Usp_tbl_Production_CleaningofTanker", new string[] { "flag", 
                                                                                         "CleaningOfTanker_ID",
                                                                                         "TankerCleanedDate",
                                                                                         "TankerCleanedTime",
                                                                                         "Tank_RMT_PMT", 
                                                                                         "MainHole_GasKit", 
                                                                                         "AirBent", 
                                                                                         "UnLoadingValve", 
                                                                                         "InnerShell", 
                                                                                         "CleanedRemark", 
                                                                                         "CreatedBy", 
                                                                                         "CreatedAt",
                                                                                         "CreatedByIP"}
                                                                         , new string[]{ "9", 
                                                                                         ViewState["CleaningOfTanker_ID"].ToString(),
                                                                                         Convert.ToDateTime(txtTankerCleanedDate.Text,cult).ToString("yyyy/MM/dd"),
                                                                                         txtTankerCleanedTime.Text,
                                                                                         txtTank_RMT_PMT.Text, 
                                                                                         txtMainHole_GasKit.Text, 
                                                                                         txtAirBent.Text, 
                                                                                         txtUnLoadingValve.Text, 
                                                                                         txtInnerShell.Text, 
                                                                                         txtCleanedRemark.Text, 
                                                                                         objdb.createdBy(), 
                                                                                         objdb.Office_ID(),
                                                                                         objdb.GetLocalIPAddress()},
                                                                                        "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                }
                FillGrid();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillViewDetail(string CleaningOfTanker_ID)
    {
        ds = objdb.ByProcedure("Usp_tbl_Production_CleaningofTanker", new string[] { "flag", "CleaningOfTanker_ID" }, new string[] { "10", CleaningOfTanker_ID }, "dataset");
        if(ds != null && ds.Tables.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            if(ds.Tables[0].Rows.Count > 0)
            {
                
                sb.Append("<table class='table'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:center;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:center;'><b>गुण नियंत्रण शाखा</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:center;'><b>Tanker/Tank/Silo क्लीनिंग रिपोर्ट</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>दिनांक</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["TankerCleanedDate"].ToString() + "</td>");
                sb.Append("<td>समय</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["TankerCleanedTime"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>क्रं</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["TankerCleaningRequest_No"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>टैंकर क्रमांक</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["V_VehicleNo"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>टैंक/आर.एम.टी./पी.एम.टी.नं.</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["Tank_RMT_PMT"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>मेन होल/गैसकिट</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["MainHole_GasKit"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>एयर बेंट</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["AirBent"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>अनलोडिंग वाल्व</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["UnLoadingValve"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>इनर शैल</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["InnerShell"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>रिमार्क</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["CleanedRemark"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
               
                sb.Append("</tr>");
                sb.Append("<tr >");
                sb.Append("<td style='padding-top:70px;'><b>उत्पादन शाखा</b></td>");
                sb.Append("<td colspan='3' style='text-align:right; padding-top:70px;'><b>जांचकर्ता</b></td>");
                sb.Append("</tr>");
                sb.Append("</table");
                divPrint.InnerHtml = sb.ToString();
                //divViewDetail.InnerHtml = sb.ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "window.print();", true);
                
            }
        }
        

    }
}