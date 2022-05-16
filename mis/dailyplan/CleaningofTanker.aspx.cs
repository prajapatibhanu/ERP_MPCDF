using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using System.Text;

public partial class mis_dailyplan_CleaningofTanker : System.Web.UI.Page
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
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtEntryDate.Attributes.Add("readonly", "readonly");
                    txtFilterDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFilterDate.Attributes.Add("readonly", "readonly");
                    FillTankerNo();
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

    #region User Defined Function
    protected void FillTankerNo()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Usp_tbl_Production_CleaningofTanker",
                    new string[] { "flag", "I_OfficeID"},
                    new string[] { "1", objdb.Office_ID()}, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    ddlTankerNo.DataSource = ds;
                    ddlTankerNo.DataTextField = "V_VehicleNo";
                    ddlTankerNo.DataValueField = "I_TankerID";
                    ddlTankerNo.DataBind();
                    
                }
                else
                {
                    ddlTankerNo.Items.Clear();
                }
            }
            else
            {
                ddlTankerNo.Items.Clear();
            }
            ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
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

            ds = objdb.ByProcedure("Usp_tbl_Production_CleaningofTanker", new string[] { "flag", "I_OfficeID", "RequestDate" }, new string[] { "3", objdb.Office_ID(), Convert.ToDateTime(txtFilterDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
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
    #endregion

    #region Button Click Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string IsActive = "1";
            ds = objdb.ByProcedure("Usp_tbl_Production_CleaningofTanker", new string[] {"flag",
                                                                                     "I_TankerID",
                                                                                     "I_OfficeID", 
                                                                                     "RequestDate", 
                                                                                     "Status",
                                                                                     "IsActive",
                                                                                     "CreatedBy",
                                                                                     "CreatedAt",
                                                                                     "CreatedByIP",
                                                                                    }
                                                                    , new string[] {"2",
                                                                                    ddlTankerNo.SelectedValue.ToString(),
                                                                                    objdb.Office_ID(),
                                                                                    Convert.ToDateTime(txtEntryDate.Text,cult).ToString("yyyy/MM/dd"),
                                                                                    "Pending",
                                                                                     IsActive,
                                                                                     objdb.createdBy(),
                                                                                     objdb.Office_ID(),
                                                                                     objdb.GetLocalIPAddress()                                                                     
                                                                                     }
                                                                                     , "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check","alert-success","Thank You!",ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else  if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-danger", "alert-danger", "Danger", ds.Tables[0].Rows[0]["ErrrorMsg"].ToString());
                    }
                }
            }
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion

    #region TextChangedEvent
    protected void txtFilterDate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    #endregion

    protected void FillViewDetail(string CleaningOfTanker_ID)
    {
        ds = objdb.ByProcedure("Usp_tbl_Production_CleaningofTanker", new string[] { "flag", "CleaningOfTanker_ID" }, new string[] { "10", CleaningOfTanker_ID }, "dataset");
        if (ds != null && ds.Tables.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0)
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string CleaningOfTanker_ID = e.CommandArgument.ToString();
        if (e.CommandName == "ViewRecord")
        {
            FillViewDetail(CleaningOfTanker_ID);

        }
    }
}