using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;

public partial class mis_Grievance_Dashboard : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["Office_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    Session["ds2"] = "";
                    FillDetail();
                }
            }
            //ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag" }, new string[] { "0" }, "dataset");
            //if (ds.Tables.Count != 0)
            //{
            //    lblOpenGrievance.Text = ds.Tables[0].Rows[0]["OpenGrv"].ToString();
            //    lblCloseGrievance.Text = ds.Tables[1].Rows[0]["CloseGrv"].ToString();
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDetail()
    {
        try
        {
            grv1.DataSource = null;
            grv1.DataBind();
            grv2.DataSource = null;
            grv2.DataBind();
            grv3.DataSource = null;
            grv3.DataBind();
            grv4.DataSource = null;
            grv4.DataBind();
            grv5.DataSource = null;
            grv5.DataBind();
            grv6.DataSource = null;
            grv6.DataBind();
            //grv7.DataSource = null;
            //grv7.DataBind();
            //grv8.DataSource = null;
            //grv8.DataBind();
            grv9.DataSource = null;
            grv9.DataBind();
            grv10.DataSource = null;
            grv10.DataBind();
			GrvOfficeWisePendingComplaints.DataSource = null;
            GrvOfficeWisePendingComplaints.DataBind();
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag" }, new string[] { "13" }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                GrvOfficeWisePendingComplaints.DataSource = ds.Tables[0];
                GrvOfficeWisePendingComplaints.DataBind();
				int ProductionQualityTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("ProductionQualityTotal"));
                int ProductionAvlTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("ProductionAvlTotal"));
                int SocietyPaymentTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("SocietyPaymentTotal"));
                int ParlourRelatedTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("ParlourRelatedTotal"));
                int OtherInformationTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OtherInformationTotal"));
                int MilkProductionTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("MilkProductionTotal"));
                int OtherSuggestionTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OtherSuggestionTotal"));
                GrvOfficeWisePendingComplaints.FooterRow.Cells[1].Text = "<b>| TOTAL |</b>";
                GrvOfficeWisePendingComplaints.FooterRow.Cells[2].Text = "<b>" + ProductionQualityTotal.ToString() + "</b>";
                GrvOfficeWisePendingComplaints.FooterRow.Cells[3].Text = "<b>" + ProductionAvlTotal.ToString() + "</b>";
                GrvOfficeWisePendingComplaints.FooterRow.Cells[4].Text = "<b>" + SocietyPaymentTotal.ToString() + "</b>";
                GrvOfficeWisePendingComplaints.FooterRow.Cells[5].Text = "<b>" + ParlourRelatedTotal.ToString() + "</b>";
                GrvOfficeWisePendingComplaints.FooterRow.Cells[6].Text = "<b>" + OtherInformationTotal.ToString() + "</b>";
                GrvOfficeWisePendingComplaints.FooterRow.Cells[7].Text = "<b>" + MilkProductionTotal.ToString() + "</b>";
                GrvOfficeWisePendingComplaints.FooterRow.Cells[8].Text = "<b>" + OtherSuggestionTotal.ToString() + "</b>";
            }
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag" }, new string[] { "7" }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grv1.DataSource = ds.Tables[0];
                    grv1.DataBind();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    grv2.DataSource = ds.Tables[1];
                    grv2.DataBind();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    grv3.DataSource = ds.Tables[2];
                    grv3.DataBind();
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    grv4.DataSource = ds.Tables[3];
                    grv4.DataBind();
                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    grv5.DataSource = ds.Tables[4];
                    grv5.DataBind();
                }
                if (ds.Tables[5].Rows.Count > 0)
                {
                    grv6.DataSource = ds.Tables[5];
                    grv6.DataBind();
                }
                //if (ds.Tables[6].Rows.Count > 0)
                //{
                //    grv7.DataSource = ds.Tables[6];
                //    grv7.DataBind();
                //}
                //if (ds.Tables[7].Rows.Count > 0)
                //{
                //    grv8.DataSource = ds.Tables[7];
                //    grv8.DataBind();
                //}
                if (ds.Tables[8].Rows.Count > 0)
                {
                    grv9.DataSource = ds.Tables[8];
                    grv9.DataBind();
                }
                if (ds.Tables[9].Rows.Count > 0)
                {
                    grv10.DataSource = ds.Tables[9];
                    grv10.DataBind();
                }
            }
            if (ds.Tables[10].Rows.Count > 0)
            {
                spntotal.InnerText = ds.Tables[10].Rows[0]["Total"].ToString();
                spnclosed.InnerText = ds.Tables[10].Rows[0]["Closed"].ToString();
                spnpending.InnerText = ds.Tables[10].Rows[0]["Opened"].ToString();
                spn15Dayspending.InnerText = ds.Tables[10].Rows[0]["15DaysPending"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkPending_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Status"] = "Open";
            ViewState["Days"] = "15";
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                int Pendingsum = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                    int Pending = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("Count"));
                    GridView2.FooterRow.Cells[1].Text = "| TOTAL | ";
                    GridView2.FooterRow.Cells[2].Text = "<b>" + Pending.ToString() + "</b>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert();", true);
                    lbl.InnerHtml = "15 दिनों से अधिक लंबित शिकायतें :";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnktotal_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Status"] = "Total";
            ViewState["Days"] = "0";
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag" }, new string[] { "11" }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                int Totalsum = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                    int Total = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("Count"));
                    GridView2.FooterRow.Cells[1].Text = "| TOTAL | ";
                    GridView2.FooterRow.Cells[2].Text = "<b>" + Total.ToString() + "</b>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert();", true);
                    lbl.InnerHtml = "अब तक की कुल शिकायत :";

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkclosed_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Status"] = "Close";
            ViewState["Days"] = "0";
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag" }, new string[] { "10" }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                int Closesum = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                    int closed = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("Count"));
                    GridView2.FooterRow.Cells[1].Text = "| TOTAL | ";
                    GridView2.FooterRow.Cells[2].Text = "<b>" + closed.ToString() + "</b>";
                    lbl.InnerHtml = "अब तक हल की गई कुल शिकायत :";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert();", true);

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void lnkopen_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Status"] = "Open";
            ViewState["Days"] = "0";
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                int OpenSum = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                    int Open = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("Count"));
                    GridView2.FooterRow.Cells[1].Text = "| TOTAL | ";
                    GridView2.FooterRow.Cells[2].Text = "<b>" + Open.ToString() + "</b>";
                    lbl.InnerHtml = "लंबित शिकायतें :";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert();", true);

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                string Status = "";
                GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblOffice_Name = (Label)gvRow.FindControl("lblOffice_Name");
                GridView1.DataSource = null;
                GridView1.DataBind();

                ds = objdb.ByProcedure("SpGrvApplicantDetail", new string[] { "flag", "Office_ID", "Application_GrvStatus", "Days" },
                    new string[] { "15", e.CommandArgument.ToString(), ViewState["Status"].ToString(), ViewState["Days"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    ViewState["dirState"] = dt;
                    ViewState["sortdr"] = "Asc";
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
                lblOfficeName.InnerText = lblOffice_Name.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert1();", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert();", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtrslt = (DataTable)ViewState["dirState"];
        if (dtrslt.Rows.Count > 0)
        {
            if (Convert.ToString(ViewState["sortdr"]) == "Asc")
            {
                dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                ViewState["sortdr"] = "Desc";
            }
            else
            {
                dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                ViewState["sortdr"] = "Asc";
            }
            GridView1.DataSource = dtrslt;
            GridView1.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert1();", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert();", true);						


        }

    }
    protected void grv1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Status = e.CommandName.ToString();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblOffice_Name = (Label)gvRow.FindControl("lblOffice_Name");
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType", "Application_GrvStatus" },
                new string[] { "12", e.CommandArgument.ToString(), "उत्पाद की गुणवत्ता के बारे में", Status }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeName1.InnerText = lblOffice_Name.Text;
                lblComplaintType.InnerText = ds.Tables[0].Rows[0]["Application_Subject"].ToString();
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert2();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void grv2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Status = e.CommandName.ToString();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblOffice_Name = (Label)gvRow.FindControl("lblOffice_Name");
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType", "Application_GrvStatus" },
                new string[] { "12", e.CommandArgument.ToString(), "उत्पाद की उपलब्धता के बारे में", Status }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeName1.InnerText = lblOffice_Name.Text;
                lblComplaintType.InnerText = ds.Tables[0].Rows[0]["Application_Subject"].ToString();
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert2();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void grv3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Status = e.CommandName.ToString();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblOffice_Name = (Label)gvRow.FindControl("lblOffice_Name");
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType", "Application_GrvStatus" },
                new string[] { "12", e.CommandArgument.ToString(), "समिति भुगतान के सम्बन्ध में", Status }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeName1.InnerText = lblOffice_Name.Text;
                lblComplaintType.InnerText = ds.Tables[0].Rows[0]["Application_Subject"].ToString();
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert2();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void grv4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Status = e.CommandName.ToString();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblOffice_Name = (Label)gvRow.FindControl("lblOffice_Name");
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType", "Application_GrvStatus" },
                new string[] { "12", e.CommandArgument.ToString(), "एजेंसी / बूथ / पार्लर / डीपो सम्बन्धित शिकायत", Status }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeName1.InnerText = lblOffice_Name.Text;
                lblComplaintType.InnerText = ds.Tables[0].Rows[0]["Application_Subject"].ToString();
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert2();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void grv5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Status = e.CommandName.ToString();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblOffice_Name = (Label)gvRow.FindControl("lblOffice_Name");
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType", "Application_GrvStatus" },
                new string[] { "12", e.CommandArgument.ToString(), "वितरक / परिवहनकर्ता सम्बन्धित शिकायत", Status }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeName1.InnerText = lblOffice_Name.Text;
                lblComplaintType.InnerText = ds.Tables[0].Rows[0]["Application_Subject"].ToString();
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert2();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void grv6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Status = e.CommandName.ToString();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblOffice_Name = (Label)gvRow.FindControl("lblOffice_Name");
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType", "Application_GrvStatus" },
                new string[] { "12", e.CommandArgument.ToString(), "दूध उत्पादक समिति से सम्बंधित", Status }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeName1.InnerText = lblOffice_Name.Text;
                lblComplaintType.InnerText = ds.Tables[0].Rows[0]["Application_Subject"].ToString();
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert2();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void grv7_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Status = e.CommandName.ToString();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblOffice_Name = (Label)gvRow.FindControl("lblOffice_Name");
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType", "Application_GrvStatus" },
                new string[] { "12", e.CommandArgument.ToString(), "क्रय सामग्री प्रदायक द्वारा शिकायत", Status }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeName1.InnerText = lblOffice_Name.Text;
                lblComplaintType.InnerText = ds.Tables[0].Rows[0]["Application_Subject"].ToString();
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert2();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void grv8_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Status = e.CommandName.ToString();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblOffice_Name = (Label)gvRow.FindControl("lblOffice_Name");
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType", "Application_GrvStatus" },
                new string[] { "12", e.CommandArgument.ToString(), "सामग्री प्रदाय से सम्बंधित", Status }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeName1.InnerText = lblOffice_Name.Text;
                lblComplaintType.InnerText = ds.Tables[0].Rows[0]["Application_Subject"].ToString();
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert2();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void grv9_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Status = e.CommandName.ToString();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblOffice_Name = (Label)gvRow.FindControl("lblOffice_Name");
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType", "Application_GrvStatus" },
                new string[] { "12", e.CommandArgument.ToString(), "अन्य सुझाव", Status }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeName1.InnerText = lblOffice_Name.Text;
                lblComplaintType.InnerText = ds.Tables[0].Rows[0]["Application_Subject"].ToString();
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert2();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void grv10_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Status = e.CommandName.ToString();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblOffice_Name = (Label)gvRow.FindControl("lblOffice_Name");
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType", "Application_GrvStatus" },
                new string[] { "12", e.CommandArgument.ToString(), "अन्य (जानकारी प्राप्त करने से सम्बंधित)", Status }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeName1.InnerText = lblOffice_Name.Text;
                lblComplaintType.InnerText = ds.Tables[0].Rows[0]["Application_Subject"].ToString();
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert2();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Application_ID = GridView3.SelectedDataKey.Value.ToString();
            string url = "GrievanceRequest.aspx?Application_ID=" + objdb.Encrypt(Application_ID);

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            //Response.Redirect("GrievanceRequest.aspx?Application_ID=" + objdb.Encrypt(Application_ID));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}