using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using System.Text;
using System.Globalization;
using System.Data.Odbc;
using System.Linq;


public partial class mis_MilkCollection_DeleteCCWiseDbfEntry : System.Web.UI.Page
{
    DataSet dsreturn, ds = new DataSet();    
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();

                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        //ddlOffice.Enabled = true;
                    }
                    ds = objdb.ByProcedure("SpAdminOffice",
                          new string[] { "flag", "OfficeType_ID", "Office_ID" },
                          new string[] { "41", objdb.OfficeType_ID(), objdb.Office_ID() }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlOffice.DataSource = ds;
                            ddlOffice.DataTextField = "Office_Name";
                            ddlOffice.DataValueField = "Office_ID";
                            ddlOffice.DataBind();
                            ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                            ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                        }
                        else
                        {
                            ddlOffice.DataSource = ds;
                            ddlOffice.DataTextField = "Office_Name";
                            ddlOffice.DataValueField = "Office_ID";
                            ddlOffice.DataBind();

                        }

                    }
                    GetCCDetails();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "EntryDate", "CC_Id" }, new string[] { "14",  Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), ddlccbmcdetail.SelectedValue }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
    protected void FillGrid()
    {
        try
        {
            lblMsg.Text = "";
            gvEntryList.DataSource = string.Empty;
            gvEntryList.DataBind();
           
            btnDelete.Visible = false;
            ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "EntryDate", "CC_Id" }, new string[] { "13", Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), ddlccbmcdetail.SelectedValue }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                
                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Status"].ToString() == "0")
                        {
                            btnDelete.Visible = true;
                        }
                    }
                    gvEntryList.DataSource = ds.Tables[1];
                    gvEntryList.DataBind();
                    gvEntryList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvEntryList.UseAccessibleHeader = true;
                    decimal TotalMilkQty = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
                    gvEntryList.FooterRow.Cells[7].Text = "<b>Total : </b>";
                    gvEntryList.FooterRow.Cells[8].Text = "<b>" + TotalMilkQty.ToString() + "</b>";
                }
                              
                //GetDatatableHeaderDesign();
            }
            else
            {
                gvEntryList.DataSource = string.Empty;
                gvEntryList.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
}