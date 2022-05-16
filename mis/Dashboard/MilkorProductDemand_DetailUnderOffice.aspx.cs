using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Dashboard_MilkorProductDemand_DetailUnderOffice : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        string Pid = string.Empty;
        if (Request.QueryString["Mid"] != null)
        {
            string Rid = objdb.Decrypt(Request.QueryString["Mid"].ToString());
            
            
            if (Rid == "N.A") { Rid = string.Empty; }
          
            lblmilksearchdate.Text = Rid;
           
            FillDetail();

        }
        if (Request.QueryString["PID"] != null)
        {
            Pid = objdb.Decrypt(Request.QueryString["PID"].ToString());
            if (Pid == "N.A") { Pid = string.Empty; }
            lblProductsearchdate.Text = Pid;
            FillProdDetail();
        }

    }
    protected void FillDetail()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Trn_MilkOrProductDemand_BY_DateOffice_List", new string[] { "flag", "OfficeID", "CurrentDate", "ProductCurrentDate" }, new string[] { "0", objdb.Office_ID(), lblmilksearchdate.Text, lblProductsearchdate.Text }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                grdMilk.DataSource = ds;
                grdMilk.DataBind();
                

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void FillProdDetail()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Trn_MilkOrProductDemand_BY_DateOffice_List", new string[] { "flag", "OfficeID", "CurrentDate", "ProductCurrentDate" }, new string[] { "1", objdb.Office_ID(), lblmilksearchdate.Text, lblProductsearchdate.Text }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                
                grdProduct.DataSource = ds;
                grdProduct.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdMilk_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdMilk.PageIndex = e.NewPageIndex;
        FillDetail();
    }
    protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdMilk.PageIndex = e.NewPageIndex;
        FillProdDetail();
    }
}