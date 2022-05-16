using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Dashboard_QCDetailReports : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string Fdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        if (Request.QueryString["Rid"] != null)
        {
            if (objdb.Decrypt(Request.QueryString["Rid"]) != null)
            {
                string Rid = objdb.Decrypt(Request.QueryString["Rid"].ToString());
                txtDate.Text = Rid;
                Filldata();
            }
            else
                txtDate.Text = string.Empty;
        }
        else
            txtDate.Text = string.Empty;
       
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Filldata();
    }
    private void Filldata()
    {
        try
        {
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }
            GridView3.DataSource = null;
            GridView3.DataBind();
            ds = null;
            ds = objdb.ByProcedure("spProductionMilkContainerMaster"
            , new string[] { "flag", "Office_Id", "TestRequest_DT" }
            , new string[] { "9", objdb.Office_ID(), Fdate }, "dataset");
            if (ds != null)
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            else
            {
                GridView3.DataSource = string.Empty;
                GridView3.DataBind();
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
            , new string[] { "8",objdb.Office_ID(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");

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
            , new string[] { "11", objdb.Office_ID(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");
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
}