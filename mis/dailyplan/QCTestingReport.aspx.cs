using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using ClosedXML.Excel;
using System.IO;

public partial class mis_dailyplan_QCTestingReport : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {

                    btnPrint.Visible = false;
                    btnExcel.Visible = false;
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["QCParameterID"] = "0";
                    ViewState["TestRequest_ID"] = "0";
                    FillShift();                  
                    FillOffice();
                    GetSectionView();
                    
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void FillShift()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                 new string[] { "flag" },
                 new string[] { "2" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataTextField = "Name";
                ddlShift.DataValueField = "Shift_Id";
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("All", "0"));
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
               
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDS.Enabled = false;            

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetSectionView()
    {

        try
        {

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID" },
                  new string[] { "6", ddlDS.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.Items.Insert(0, new ListItem("All", "0"));

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCalculationMethod = e.Row.FindControl("lblCalculationMethod") as Label;
            Label lblMinValue = e.Row.FindControl("lblMinValue") as Label;
            Label lblMaxValue = e.Row.FindControl("lblMaxValue") as Label;
            Label lblOptionDetails = e.Row.FindControl("lblOptionDetails") as Label;
            Label lblValueRang = e.Row.FindControl("lblValueRang") as Label;

            if (lblCalculationMethod.Text == "Value" || lblCalculationMethod.Text == "Use By Days")
            {
                lblValueRang.Text = lblMinValue.Text + " - " + lblMaxValue.Text;
            }

            if (lblCalculationMethod.Text == "Adulteration Test" || lblCalculationMethod.Text == "Manufacturing Date" || lblCalculationMethod.Text == "OT")
            {
                lblValueRang.Text = lblOptionDetails.Text;
            }

        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            btnPrint.Visible = false;
            btnExcel.Visible = false;
            string FromDate = Convert.ToDateTime(txtFromDate.Text,cult).ToString("yyyy/MM/dd");
            string ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            GridView3.DataSource = null;
            GridView3.DataBind();

            ds = null;
            ds = objdb.ByProcedure("SpProductionReports", new string[] { "flag", "Office_Id", "FromDate", "ToDate", "Shift_ID", "ProductSection_ID", "TestRequest_Result_Status" }, new string[] { "0", ddlDS.SelectedValue, FromDate, ToDate,ddlShift.SelectedValue,ddlPSection.SelectedValue, ddlResultStatus.SelectedValue }, "dataset");
            if(ds != null)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    GridView3.DataSource = ds;
                    GridView3.DataBind();
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;
                }
                else
                {
                    GridView3.DataSource = string.Empty;
                    GridView3.DataBind();
                    btnPrint.Visible = false;
                    btnExcel.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void lbFilterReq_Click(object sender, EventArgs e)
    {
        
        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        Label lblRowNumber = (Label)GridView3.Rows[selRowIndex].FindControl("lblRowNumber");
        Label lblTestRequest_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequest_No");
        Label lblTestRequestFor = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequestFor");
        Label lblSampleBatch_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblSampleBatch_No");
        Label lblSampleLot_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblSampleLot_No");

        Label2.Text = lblTestRequest_No.Text;
        Label2.ToolTip = lblRowNumber.ToolTip.ToString();
        Label3.Text = lblTestRequestFor.Text;
        Label4.Text = lblSampleBatch_No.Text;
        Label5.Text = lblSampleLot_No.Text;
        Label lblTestRequestType = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequestType");
        Label lblTestRequestFor_ID = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequestFor_ID");

        if (lblTestRequestType.Text == "Milk" || lblTestRequestType.Text == "Product")
        {

            ds = objdb.ByProcedure("spProductionMilkContainerMaster"
            , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
            , new string[] { "8", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");

            if (ds != null)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
                txtRR.Text = ds.Tables[0].Rows[0]["TestRequest_Remark"].ToString();
                // lblmpR.Text = objdb.Alert("fa-check", "alert-success", ds.Tables[0].Rows[0]["TestRequest_Result_Status"].ToString(), "");
            }
            else
            {
                GridView2.DataSource = string.Empty;
                GridView2.DataBind();
            }
        }

        else
        {
            if (lblTestRequestType.Text == "Silo")
            {
                lblTestRequestFor_ID.Text = "-1";
            }
            if (lblTestRequestType.Text == "PMT")
            {
                lblTestRequestFor_ID.Text = "-2";
            }
            if (lblTestRequestType.Text == "RMT")
            {
                lblTestRequestFor_ID.Text = "-3";
            }

            if (lblTestRequestType.Text == "Tank")
            {
                lblTestRequestFor_ID.Text = "-4";
            }

            ds = objdb.ByProcedure("spProductionMilkContainerMaster"
            , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
            , new string[] { "11", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");
            if (ds != null)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
                txtRR.Text = ds.Tables[0].Rows[0]["TestRequest_Remark"].ToString();
                // lblmpR.Text = objdb.Alert("fa-check", "alert-success", ds.Tables[0].Rows[0]["TestRequest_Result_Status"].ToString(), "");
            }
            else
            {
                GridView2.DataSource = string.Empty;
                GridView2.DataBind();
            }

        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewResultParameterModal()", true);

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    private void ExportGridToExcel()
    {
        try
        {
            string FromDate = "";
            string ToDate = "";
            if (txtFromDate.Text != "")
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            }

            if (txtToDate.Text != "")
            {
                ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            }


            ds = null;
            ds = objdb.ByProcedure("SpProductionReports", new string[] { "flag", "Office_Id", "FromDate", "ToDate", "Shift_ID", "ProductSection_ID", "TestRequest_Result_Status" }, new string[] { "1", ddlDS.SelectedValue, FromDate, ToDate, ddlShift.SelectedValue, ddlPSection.SelectedValue, ddlResultStatus.SelectedValue }, "dataset");

            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + "QCTestingReport" + DateTime.Now + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
}