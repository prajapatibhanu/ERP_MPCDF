using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DutyChart_GenrateDutyChart : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ddlSearchYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlSearchMonth.SelectedValue = DateTime.Now.Month.ToString();               
                FillDetail();
                if(objdb.Office_ID() == "2206")
                {
                    lblHeading.Text = "दुग्ध शीत केंद्र बैतूल और मुलताई बी.एम.सी";
                }
                else
                {
                    lblHeading.Text = Session["Office_Name"].ToString();
                }
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void FillDetail()
    {
		
        GvDetail.DataSource = null;
        GvDetail.DataBind();
        DivData.Visible = false;
        ds = objdb.ByProcedure("Sp_tblDutyChartMapping", new string[] { "flag", "OfficeId", "Year", "Month" },
            new string[] { "2", objdb.Office_ID(), ddlSearchYear.SelectedValue.ToString(), ddlSearchMonth.SelectedValue.ToString() }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
			lblMonth.InnerText = ddlSearchMonth.SelectedItem.Text;
            lblMonth2.InnerText = ddlSearchMonth.SelectedItem.Text;
            DivData.Visible = true;
            GvDetail.DataSource = ds;
            GvDetail.DataBind();
            lblDate.InnerHtml = ds.Tables[0].Rows[0]["CreatedOn"].ToString();
            GvDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GvDetail.UseAccessibleHeader = true;
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //ds = objdb.ByProcedure("Sp_tblDutyChartMapping", new string[] { "flag", "Year", "Month", "FromDate", "ToDate", "OfficeId", "CreatedBy", "CreatedIP" },
            //    new string[] { "1", 
            //        ddlYear.SelectedValue.ToString(), 
            //        ddlMonth.SelectedValue.ToString(), 
            //        Convert.ToDateTime(( "1" + "/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()), cult).ToString("yyyy/MM/dd"), 
            //        Convert.ToDateTime(( "30" + "/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()), cult).ToString("yyyy/MM/dd")
            //        , objdb.Office_ID()
            //        , objdb.createdBy()
            //        , objdb.GetLocalIPAddress()
            //    }, "dataset");
            ds = objdb.ByProcedure("Sp_tblDutyChartMapping", new string[] { "flag", "Year", "Month", "OfficeId", "CreatedBy", "CreatedIP" },
                new string[] { "1", 
                    ddlYear.SelectedValue.ToString(), 
                    ddlMonth.SelectedValue.ToString()
                    , objdb.Office_ID()
                    , objdb.createdBy()
                    , objdb.GetLocalIPAddress()
                }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["msg"].ToString() == "Ok")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                }
            }
            FillDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlSearchMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
		lblMsg.Text = "";
        FillDetail();
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
		lblMsg.Text = "";
        FillDetail();
    }
}