using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_RMRDReportViaCanes : System.Web.UI.Page
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
                txtFromDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");
                FillBMCRoot();
                             
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void FillGrid()
    {
        try
        {
          
            string FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            string ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ds = objdb.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes", new string[] { "flag", "FromDate", "ToDate", "BMCTankerRoot_Id", "I_OfficeID","I_OfficeTypeID" }, new string[] { "9", FromDate, ToDate, ddlBMCTankerRootName.SelectedValue, objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    gv_MilkCollectionChallanEntryDetails.DataSource = ds;
                    gv_MilkCollectionChallanEntryDetails.DataBind();
                    decimal TotalMilkQuantity = 0;

                    decimal TotalFATInKg = 0;
                    decimal TotalSnfInKg = 0;

                    TotalMilkQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("I_MilkQuantity"));
                    TotalFATInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    TotalSnfInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[6].Text = "<b>Grand Total : </b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[7].Text = "<b>" + TotalMilkQuantity.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[11].Text = "<b>" + TotalFATInKg.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[12].Text = "<b>" + TotalSnfInKg.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Left;
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Left;
                    GetDatatableHeaderDesign();
                    GetDatatableFooterDesign();

                }
                else
                {
                    gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
                    gv_MilkCollectionChallanEntryDetails.DataBind();
                }
            }
            else
            {
                gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
                gv_MilkCollectionChallanEntryDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    private void GetDatatableFooterDesign()
    {
        try
        {
            if (gv_MilkCollectionChallanEntryDetails.Rows.Count > 0)
            {
                gv_MilkCollectionChallanEntryDetails.FooterRow.TableSection = TableRowSection.TableFooter;
                //gv_MilkCollectionChallanEntryDetails.foo = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    public void FillBMCRoot()
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                       new string[] { "flag", "Office_ID", "OfficeType_ID" },
                       new string[] { "11", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");


            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
                ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
                ddlBMCTankerRootName.DataSource = ds;
                ddlBMCTankerRootName.DataBind();
                ddlBMCTankerRootName.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (gv_MilkCollectionChallanEntryDetails.Rows.Count > 0)
            {
                gv_MilkCollectionChallanEntryDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                gv_MilkCollectionChallanEntryDetails.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

   
}