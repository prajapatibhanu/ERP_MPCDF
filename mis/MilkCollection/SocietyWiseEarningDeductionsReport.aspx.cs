using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

public partial class mis_MilkCollection_SocietyWiseEarningDeductionsReport : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);

    string Fdate = "";
    string Tdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";
           

            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtFdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                txtTdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                GetCCDetails();
				hfvalue.Value = Session["Office_Name"].ToString();

            }
        }
        else
        {
            objdb.redirectToHome();
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
                            ddlccbmcdetail.Items.Insert(0, new ListItem("All", "0"));
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
    //protected void FillSociety()
    //{
    //    try
    //    {
    //        if (ddlMilkCollectionUnit.SelectedValue != "0")
    //        {
    //            ds = null;
    //            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
    //                              new string[] { "flag", "Office_ID", "OfficeType_ID" },
    //                              new string[] { "10", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");

    //            if (ds.Tables.Count > 0)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlSociety.DataTextField = "Office_Name";
    //                    ddlSociety.DataValueField = "Office_ID";
    //                    ddlSociety.DataSource = ds.Tables[2];
    //                    ddlSociety.DataBind();
    //                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
    //                }
    //                else
    //                {
    //                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
    //                }
    //            }
    //            else
    //            {
    //                ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
    //            }
    //        }
    //        else
    //        {
    //            Response.Redirect("ProducerPaymentDetail.aspx", false);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    #endregion

    #region Changed Event

    //protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillSociety();
    //}
    #endregion

    #region Button Click Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            btnExport.Visible = false;
            Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
            Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
            ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry", new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Office_Parant_ID","OfficeType_ID" }, new string[] { "5", Fdate, Tdate, ddlccbmcdetail.SelectedValue,objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
					DataTable dt = new DataTable();
					dt = ds.Tables[0];
					ViewState["dirState"] = dt;  
					ViewState["sortdr"] = "Asc"; 
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    //btnExport.Visible = true;
                    decimal TotalCurntCycleAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CycleAmount"));
                    decimal TotalPreviousAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PrevoiusAmount"));
                    decimal TotalDeductionAmount = TotalCurntCycleAmount + TotalPreviousAmount; 
                    decimal TotalDeducted = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("DeductedAmount"));
                    decimal TotalBalance = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BalanceAmount"));
                    GridView1.FooterRow.Cells[5].Text = "<b>Total : </b>";
                    //GridView1.FooterRow.Cells[5].Text = "<b>" + TotalCurntCycleAmount.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalPreviousAmount.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalDeductionAmount.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalDeducted.ToString() + "</b>";
                    GridView1.FooterRow.Cells[6].Text = "<b>" + TotalBalance.ToString() + "</b>";
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }
    }
    
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
           
            lblMsg.Text = "";
            string FileName = Session["Office_Name"].ToString() + "_" + "CCWiseEarningDeductionReport_" + ddlccbmcdetail.SelectedItem.Text + "_";
            Response.Clear();
           
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView1.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
	protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)  
        {  
            DataTable dtrslt= (DataTable)ViewState["dirState"];  
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
					GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;	
					decimal TotalCurntCycleAmount = dtrslt.AsEnumerable().Sum(row => row.Field<decimal>("CycleAmount"));
                    decimal TotalPreviousAmount = dtrslt.AsEnumerable().Sum(row => row.Field<decimal>("PrevoiusAmount"));
                    decimal TotalDeductionAmount = TotalCurntCycleAmount + TotalPreviousAmount; 
                    decimal TotalDeducted = dtrslt.AsEnumerable().Sum(row => row.Field<decimal>("DeductedAmount"));
                    decimal TotalBalance = dtrslt.AsEnumerable().Sum(row => row.Field<decimal>("BalanceAmount"));
                    GridView1.FooterRow.Cells[5].Text = "<b>Total : </b>";
                    //GridView1.FooterRow.Cells[5].Text = "<b>" + TotalCurntCycleAmount.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalPreviousAmount.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalDeductionAmount.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalDeducted.ToString() + "</b>";
                    GridView1.FooterRow.Cells[6].Text = "<b>" + TotalBalance.ToString() + "</b>";					
              
                 
            }  
  
        } 
    #endregion

}