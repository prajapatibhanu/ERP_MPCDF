using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_CattleFeed_Rpt_Fin_CFP_PO : System.Web.UI.Page
{
    DataSet ds, ds1 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtToDate.Text = Date;
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["Office_FinAddress"] = Session["Office_FinAddress"].ToString();
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }


    }
    private void FillGrid()
    {
        try
        {
            Gridview1.DataSource = null;
            Gridview1.DataBind();
            DateTime fmonth = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime tmonth = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            DateTime enddate = tmonth.AddDays(1);
            string FromDate = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string ToDate = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_Order",
                                  new string[] { "Flag", "Office_ID", "FromDate", "ToDate" },
                                  new string[] { "6", ViewState["Office_ID"].ToString()
                                      , FromDate.ToString(),ToDate.ToString() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                Gridview1.DataSource = ds1.Tables[0];
                Gridview1.DataBind();
                
            }
            else
            {
                Gridview1.DataSource = null;
                Gridview1.DataBind();
            }
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string fdat = fdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string myStringtodate = txtToDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string tdat = tdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                FillGrid();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            GetCompareDate();
        }
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (Gridview1.Rows.Count > 0)
            {
                Gridview1.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridview1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordView")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_OrderChild",
                 new string[] { "Flag", "CFP_Purchase_Order_ID" },
                new string[] { "0", e.CommandArgument.ToString() }, "dataset");
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        GridView2.DataSource = ds1.Tables[0];
                        GridView2.DataBind();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "myItemDetailsModal();", true);
                            
                    }
                }
            }
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds1.Dispose(); GC.SuppressFinalize(objdb); }
    }
}