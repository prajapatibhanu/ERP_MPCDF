using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;


public partial class mis_Demand_Rpt_MilkOrProductOrderByDistributor : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2 = new DataSet();
    string orderdate = "";
    Int32 totalqty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"].ToString() != null && Session["Office_ID"].ToString() != null)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["createdBy"] = Session["Emp_ID"].ToString();
                //GetCategory();
                //GetShift();
                GetDetails();
                GetRouteIDByDistributor();
                GetRetailer();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = Date;
                txtToDate.Attributes.Add("readonly", "readonly");
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
    #region=======================user defined function========================
    private void GetDetails()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Trn_DistributorDemandPage",
                            new string[] { "Flag", "Office_ID", },
                            new string[] { "1", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds2.Tables[0];
                ddlItemCategory.DataBind();

                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds2.Tables[1];
                ddlShift.DataBind();


            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void GetRouteIDByDistributor()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "DistributorId", "ItemCat_id" },
                       new string[] { "4", ViewState["Office_ID"].ToString(), ViewState["createdBy"].ToString(),ddlItemCategory.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ViewState["RouteId"] = ds.Tables[0].Rows[0]["RouteId"].ToString();
            }
            else
            {
                ViewState["RouteId"] = "0";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error route ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    //protected void GetShift()
    //{
    //    try
    //    {
            
    //            ddlShift.DataTextField = "ShiftName";
    //            ddlShift.DataValueField = "Shift_id";
    //            ddlShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
    //                 new string[] { "flag" },
    //                   new string[] { "1" }, "dataset");
    //            ddlShift.DataBind();
    //            ddlShift.Items.Insert(0, new ListItem("All", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
    //    }
    //}
    //protected void GetCategory()
    //{
    //    try
    //    {
    //        ddlItemCategory.DataTextField = "ItemCatName";
    //        ddlItemCategory.DataValueField = "ItemCat_id";
    //        ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
    //             new string[] { "flag" },
    //               new string[] { "1" }, "dataset");
    //        ddlItemCategory.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
    //    }
    //}
    private void GetRetailer()
    {
        try
        {
            ddlRetailer.Items.Clear();
            //ds = objdb.ByProcedure("USP_Mst_DistributorParlourMapping",
            //         new string[] { "flag", "DistributorId" },
            //           new string[] { "6", ViewState["createdBy"].ToString() }, "dataset");
            ds = objdb.ByProcedure("USP_Mst_BoothReg",
                    new string[] { "flag", "RouteId", "ItemCat_id" },
                      new string[] { "12", ViewState["RouteId"].ToString(),ddlItemCategory.SelectedValue.ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlRetailer.DataTextField = "BoothName";
                ddlRetailer.DataValueField = "BoothId";
                ddlRetailer.DataSource = ds;
                ddlRetailer.DataBind();
                ddlRetailer.Items.Insert(0, new ListItem("All", "0"));
                
            }
            else
            {
                ddlRetailer.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }

    }
    private void GetItemDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                new string[] { "flag", "FromDate", "ToDate", "Shift_id", "ItemCat_id", "BoothId", "DistributorId" },
                       new string[] { "11", fromdat.ToString(), todat.ToString(), ddlShift.SelectedValue,ddlItemCategory.SelectedValue,ddlRetailer.SelectedValue,ViewState["createdBy"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {

                pnldata.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GridView4.DataSource = ds.Tables[0];
                GridView4.DataBind();
            }
            else
            {
                pnldata.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView4.DataSource = null;
                GridView4.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtToDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                GetItemDetails();
            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6: ", ex.Message.ToString());
        }
    }
    #endregion====================================end of user defined function

    #region=============== init or changed event for controls =================
    
    
    #endregion============ end of changed event for controls===========


    #region============ button click event & GridView Event ============================
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
   
    #endregion=============end of button click funciton==================

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ItemOrdered")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblBoothName = (Label)row.FindControl("lblBoothName");
                    Label lblDemandDate = (Label)row.FindControl("lblDemandDate");
                    Label lblShiftName = (Label)row.FindControl("lblShiftName");
                    Label lblItemCatid = (Label)row.FindControl("lblItemCatid");

                    modalBoothName.InnerHtml = lblBoothName.Text;
                    modaldate.InnerHtml = lblDemandDate.Text;
                    modelShift.InnerHtml = lblShiftName.Text;

                    GridView2.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                   new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                     new string[] { "9", e.CommandArgument.ToString(), lblItemCatid.Text, ViewState["Office_ID"].ToString() }, "dataset");
                    GridView2.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                }
            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  7: " + ex.Message.ToString());
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblItemCatName = e.Row.FindControl("lblItemCatName") as Label;

                if (lblItemCatName.Text == "Milk")
                {
                    e.Row.CssClass = "columnmilk";
                }
                else
                {
                    e.Row.CssClass = "columnproduct";
                }

            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 8: " + ex.Message.ToString());
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRetailer();
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblMilkOrProductDemandId = e.Row.FindControl("lblMilkOrProductDemandId") as Label;
                Label lblItemCatid = e.Row.FindControl("lblItemCatid") as Label;
                GridView gv = (GridView)e.Row.FindControl("GridView3");
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                   new string[] { "9", lblMilkOrProductDemandId.Text, lblItemCatid.Text, ViewState["Office_ID"].ToString() }, "dataset");

                if (ds1 != null && ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {

                        gv.DataSource = ds1.Tables[0];
                        gv.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "child table error : " + ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView4.Visible = true;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "ViewOrderReport" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView4.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}