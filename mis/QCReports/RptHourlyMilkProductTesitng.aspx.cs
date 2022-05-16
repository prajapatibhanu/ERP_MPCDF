using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Web;
using System.Web.UI;

public partial class mis_QCReports_RptHourlyMilkProductTesitng : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string Fdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {

                    if (Session["IsSuccess"] != null)
                    {
                        if ((Boolean)Session["IsSuccess"] == true)
                        {
                            Session["IsSuccess"] = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Hourly Testing Details Successfully saved !')", true);

                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["QCParameterID"] = "0";
                    ViewState["TestRequest_ID"] = "0";
                    
                    FillOffice();
                    
                    txtFilterDT_TextChanged(sender, e);

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
                

                ddlFilterOffice.DataSource = ds.Tables[0];
                ddlFilterOffice.DataTextField = "Office_Name";
                ddlFilterOffice.DataValueField = "Office_ID";
                ddlFilterOffice.DataBind();
                ddlFilterOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlFilterOffice.SelectedValue = ViewState["Office_ID"].ToString();
                ddlFilterOffice.Enabled = false;
                txtFilterDT.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

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
    protected void txtFilterDT_TextChanged(object sender, EventArgs e)
    {
        try
        {
            btnExport.Visible = false;
            //btnPrint.Visible = false;
            if (txtFilterDT.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFilterDT.Text, cult).ToString("yyyy/MM/dd");
            }


            GridView3.DataSource = null;
            GridView3.DataBind();
            btnExport.Visible = false;
            //btnPrint.Visible = false;
            ds = null;
            ds = objdb.ByProcedure("spProductionMilkContainerMaster"
            , new string[] { "flag", "Office_Id", "TestRequest_DT" }
            , new string[] { "23", ViewState["Office_ID"].ToString(), Fdate }, "dataset");

            if (ds != null)
            {
                if(ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        GridView3.DataSource = ds;
                        GridView3.DataBind();
                        GridView5.DataSource = ds;
                        GridView5.DataBind();
                        btnExport.Visible = true;
                        //btnPrint.Visible = true;
                    }
                    else
                    {
                        GridView3.DataSource = string.Empty;
                        GridView3.DataBind();
                        GridView5.DataSource = string.Empty;
                        GridView5.DataBind();
                    }
                }
                else
                {
                    GridView3.DataSource = string.Empty;
                    GridView3.DataBind();
                    GridView5.DataSource = string.Empty;
                    GridView5.DataBind();
                }
                
            }
            else
            {
                GridView3.DataSource = string.Empty;
                GridView3.DataBind();
                GridView5.DataSource = string.Empty;
                GridView5.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView5.Visible = true;
            // GridView3.Columns[9].Visible = false;
            //  GridView3.Columns[10].Visible = false;
            //  GridView3.Columns[11].Visible = false;
            //  GridView3.Columns[12].Visible = false;
            //foreach (GridViewRow rows in GridView3.Rows)
            //{

            //    Label Label1 = (Label)rows.FindControl("Label1");
            //    Label Label7 = (Label)rows.FindControl("Label7");


            //    Label1.Visible = false;
            //    Label7.Visible = true;
            //}
            string FileName = "MilkTestingRequestToQCHourlyTesting";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView5.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            GridView5.Visible = false;
            // GridView3.Columns[9].Visible = true;
            //GridView3.Columns[10].Visible = true;
            //GridView3.Columns[11].Visible = true;
            //GridView3.Columns[12].Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
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
            , new string[] { "24", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");

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


                DataSet ds1 = objdb.ByProcedure("spProductionMilkContainerMaster"
                , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
                , new string[] { "25", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");
                if (ds1 != null)
                {
                    GridView2.DataSource = ds1;
                    GridView2.DataBind();
					txtRR.Text = ds1.Tables[0].Rows[0]["TestRequest_Remark"].ToString();

                }
                else
                {
                    GridView2.DataSource = string.Empty;
                    GridView2.DataBind();
                }

            }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewResultParameterModal()", true);

    }
    protected void lblHeadFilterReq_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        Label lblRowNumber = (Label)GridView3.Rows[selRowIndex].FindControl("lblRowNumber");
        Label lblTestRequest_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequest_No");
        Label lblTestRequestFor = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequestFor");
        Label lblSampleBatch_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblSampleBatch_No");
        Label lblSampleLot_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblSampleLot_No");

        Label8.Text = lblTestRequest_No.Text;
        Label8.ToolTip = lblRowNumber.ToolTip.ToString();
        Label9.Text = lblTestRequestFor.Text;
        Label10.Text = lblSampleBatch_No.Text;
        Label11.Text = lblSampleLot_No.Text;

        Label lblTestRequestFor_ID = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequestFor_ID");


        ds = objdb.ByProcedure("spProductionMilkContainerMaster"
        , new string[] { "flag", "TestRequest_ID", }
        , new string[] { "21", lblRowNumber.ToolTip.ToString() }, "dataset");

        if (ds != null)
        {
            GridView4.DataSource = ds;
            GridView4.DataBind();

        }
        else
        {
            GridView4.DataSource = string.Empty;
            GridView4.DataBind();
        }


        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewHeadDetailModal()", true);
    }
    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        //ddlTestRequestType_SelectedIndexChanged(sender, e);
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
    protected void GvResultDetail_RowDataBound(object sender, GridViewRowEventArgs e)
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
                lblValueRang.Text = "( " + lblMinValue.Text + "  -  " + lblMaxValue.Text + " )";
            }

            if (lblCalculationMethod.Text == "Adulteration Test" || lblCalculationMethod.Text == "Manufacturing Date" || lblCalculationMethod.Text == "OT")
            {
                lblValueRang.Text = lblOptionDetails.Text;
            }

        }
    }
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRowNumber = (Label)e.Row.FindControl("lblRowNumber");
            Label lblTestRequestType = (Label)e.Row.FindControl("lblTestRequestType");
            Label lblTestRequestFor_ID = (Label)e.Row.FindControl("lblTestRequestFor_ID");
            GridView GvResultDetail = (GridView)e.Row.FindControl("GvResultDetail");
            GridView GvHeadDetail = (GridView)e.Row.FindControl("GvHeadDetail");
            DataSet ds1 = null;
            DataSet ds2 = null;
            if (lblTestRequestType.Text == "Milk" || lblTestRequestType.Text == "Product")
            {

                ds1 = objdb.ByProcedure("spProductionMilkContainerMaster"
                , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
                , new string[] { "24", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");

                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[1].Rows.Count > 0)
                        {
                            GvHeadDetail.DataSource = ds1.Tables[1];
                            GvHeadDetail.DataBind();
                        }
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            GvResultDetail.DataSource = ds1.Tables[0];
                            GvResultDetail.DataBind();
                        }

                    }



                }
                else
                {
                    GvResultDetail.DataSource = string.Empty;
                    GvResultDetail.DataBind();
                    GvHeadDetail.DataSource = string.Empty;
                    GvHeadDetail.DataBind();
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


                ds1 = objdb.ByProcedure("spProductionMilkContainerMaster"
                , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
                , new string[] { "25", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");
                if (ds1 != null)
                {
                    GvResultDetail.DataSource = ds1;
                    GvResultDetail.DataBind();

                }
                else
                {
                    GvResultDetail.DataSource = string.Empty;
                    GvResultDetail.DataBind();
                }

            }



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
}