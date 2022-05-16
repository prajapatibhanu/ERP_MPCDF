using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpPromotionAllHistory : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillOffice();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
        //try
        //{
        //    if (!IsPostBack)
        //    {

        //        if (Session["Emp_ID"] != null)
        //        {
        //            lblMsg.Text = "";
        //            ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
        //            FillGrid();
        //        }
        //        else
        //        {
        //            Response.Redirect("~/mis/Login.aspx");
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        //}
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOldOffice.DataSource = ds;
                ddlOldOffice.DataTextField = "Office_Name";
                ddlOldOffice.DataValueField = "Office_ID";
                ddlOldOffice.DataBind();
                ddlOldOffice.Items.Insert(0, new ListItem("Select Office", "0"));
            }
            ddlOldOffice.SelectedValue = ViewState["Office_ID"].ToString();
            if (ViewState["Office_ID"].ToString() == "1")
            {

                ddlOldOffice.Enabled = true;
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
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHREmpPromotion", new string[] { "flag", "Office_ID", "FromOrder_Date", "ToOrder_Date" }, new string[] { "10", ddlOldOffice.SelectedValue.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count >= 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GetDatatableHeaderDesign();
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    //private void GetPromotionList()
    //{
    //    try
    //    {
    //        lblError.Text = "";

    //        ds = objdb.ByProcedure("Sp_tblEmpPromotionHistory",
    //                 new string[] { "flag" },
    //                 new string[] { "0" }, "dataset");

    //        if (ds.Tables[0].Rows.Count != 0)
    //        {
    //            GridView1.DataSource = ds.Tables[0];
    //            GridView1.DataBind();
    //            GetDatatableHeaderDesign();
    //        }
    //        else
    //        {
    //            ds.Clear();
    //            GridView1.DataSource = ds;
    //            GridView1.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
    //    }
    //}
}