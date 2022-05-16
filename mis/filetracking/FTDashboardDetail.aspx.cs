using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_filetracking_FTDashboardDetailNew : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

                if (Request.QueryString["Parameter1"] != null)
                {
                    ViewState["Parameter1"] = Request.QueryString["Parameter1"].ToString();
                    FillDetail();
                    ViewState["FillGrid"] = "1";
                }
                else if (Request.QueryString["Parameter"] != null)
                {
                    ViewState["Emp_ID"] = objdb.Decrypt(Request.QueryString["Emp_ID"].ToString());
                    ViewState["Parameter"] = Request.QueryString["Parameter"].ToString();
                    dvSearchForm.Visible = false;
                    FillGridDropdown();
                    FillGrid();
                    ViewState["FillGrid"] = "2";
                }
                else
                {
                    Response.Redirect("FTDashboard.aspx");
                }
            }
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
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            if (ViewState["Parameter1"].ToString() == "TotalFiles")
            {
                FileDetail.Visible = true;
                OutwardDetail.Visible = false;
                InwardDetail.Visible = false;
                ds = objdb.ByProcedure("SpFTDashboardNew", new string[] { "flag", "FromDate", "ToDate" }, new string[] { "15", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "CREATED FILES";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "CREATED FILES";
                }
            }
            else if (ViewState["Parameter1"].ToString() == "InwardLetter")
            {
                FileDetail.Visible = false;
                OutwardDetail.Visible = false;
                InwardDetail.Visible = true;
                ds = objdb.ByProcedure("SpFTDashboardNew", new string[] { "flag", "FromDate", "ToDate" }, new string[] { "16", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView3.DataSource = ds;
                    GridView3.DataBind();
                    GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView3.UseAccessibleHeader = true;
                    lblInwardmsg.Text = "INWARD LETTER";
                }
                else
                {
                    GridView3.DataSource = new string[] { };
                    GridView3.DataBind();
                    GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView3.UseAccessibleHeader = true;
                    lblInwardmsg.Text = "INWARD LETTER";
                }
            }
            else if (ViewState["Parameter1"].ToString() == "OutwardLetter")
            {
                FileDetail.Visible = false;
                OutwardDetail.Visible = true;
                InwardDetail.Visible = false;
                ds = objdb.ByProcedure("SpFTDashboardNew", new string[] { "flag", "FromDate", "ToDate" }, new string[] { "17", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView2.UseAccessibleHeader = true;
                    lblOutwardmsg.Text = "OUTWARD LETTER";
                }
                else
                {
                    GridView2.DataSource = new string[] { };
                    GridView2.DataBind();
                    GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView2.UseAccessibleHeader = true;
                    lblOutwardmsg.Text = "OUTWARD LETTER";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFTDashboardNew", new string[] { "flag", "Emp_ID" }, new string[] { "0", ViewState["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ViewState["Emp_Name"] = ds.Tables[0].Rows[0]["Emp_Name"].ToString();
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
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();

            if (ViewState["Parameter"].ToString() == "Inward")
            {
                FileDetail.Visible = false;
                OutwardDetail.Visible = false;
                InwardDetail.Visible = true;
                ds = objdb.ByProcedure("SpFTDashboardNew", new string[] { "flag", "File_UpdatedBy"}, new string[] { "18", ViewState["Emp_ID"].ToString()}, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView3.DataSource = ds;
                    GridView3.DataBind();
                    GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView3.UseAccessibleHeader = true;
                    lblInwardmsg.Text = "INWARD FILES BY [ " + ViewState["Emp_Name"].ToString() + " ]";
                }
                else
                {
                    GridView3.DataSource = new string[] { };
                    GridView3.DataBind();
                    GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView3.UseAccessibleHeader = true;
                    lblInwardmsg.Text = "INWARD FILES BY [ " + ViewState["Emp_Name"].ToString() + " ]";
                }
            }
            else if (ViewState["Parameter"].ToString() == "Create")
            {
                FileDetail.Visible = true;
                OutwardDetail.Visible = false;
                InwardDetail.Visible = false;
                ds = objdb.ByProcedure("SpFTDashboardNew", new string[] { "flag", "File_UpdatedBy"}, new string[] { "19", ViewState["Emp_ID"].ToString()}, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "CREATE FILES BY [ " + ViewState["Emp_Name"].ToString() + " ]";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "CREATE FILES BY [ " + ViewState["Emp_Name"].ToString() + " ]";
                }
            }
            else if (ViewState["Parameter"].ToString() == "OnMyDesk")
            {
                FileDetail.Visible = true;
                OutwardDetail.Visible = false;
                InwardDetail.Visible = false;
                ds = objdb.ByProcedure("SpFTDashboardNew", new string[] { "flag", "Emp_ID"}, new string[] { "20", ViewState["Emp_ID"].ToString()}, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "FILES ON DESK [ " + ViewState["Emp_Name"].ToString() + " ]";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "FILES ON DESK [ " + ViewState["Emp_Name"].ToString() + " ]";
                }
            }
            else if (ViewState["Parameter"].ToString() == "Outward")
            {
                FileDetail.Visible = false;
                OutwardDetail.Visible = true;
                InwardDetail.Visible = false;
                ds = objdb.ByProcedure("SpFTDashboardNew", new string[] { "flag", "Outward_Updatedby"}, new string[] { "21", ViewState["Emp_ID"].ToString()}, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView2.UseAccessibleHeader = true;
                    lblOutwardmsg.Text = "OUTWARD LETTER BY [ " + ViewState["Emp_Name"].ToString() + " ]";
                }
                else
                {
                    GridView2.DataSource = new string[] { };
                    GridView2.DataBind();
                    GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView2.UseAccessibleHeader = true;
                    lblOutwardmsg.Text = "OUTWARD LETTER BY [ " + ViewState["Emp_Name"].ToString() + " ]";
                }
            }
            else
            {
                GridView2.DataSource = new string[] { };
                GridView2.DataBind();
                lblMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        FillDetail();
    }
}