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

public partial class mis_MilkCollection_CCWiseSocietywithincentiveamount : System.Web.UI.Page
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

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Society Milk Invoice Successfully Generated');", true);
                    }
                }
                
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
               
				txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");
                
                GetCCDetails();
				if (objdb.Office_ID() == "2")
                {
                    ddlBillingCycle.SelectedValue = "5 days";
                }
                else
                {
                    ddlBillingCycle.SelectedValue = "10 days";
                }
                txtDate_TextChanged(sender, e);


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
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            btnExport.Visible = false;
            btnprint.Visible = false;
            GridView1.DataSource = string.Empty;
            GridView1.DataBind();
            DataSet dsFillGrid = objdb.ByProcedure("Usp_MilkCollectionBillingReports",
                       new string[] { "flag", "CCID", "FromDate", "ToDate", },
                       new string[] { "43", ddlccbmcdetail.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"),"0" }, "dataset");
           
            
            if (dsFillGrid != null && dsFillGrid.Tables.Count > 0)
            {
                if (dsFillGrid.Tables[1].Rows.Count > 0)
                {
                    btnExport.Visible = true;
                    btnprint.Visible = true;
                    spn.InnerHtml =Session["Office_Name"] + "<br/>" + "Society with Incentive Amount" + "<br/>  CC:" + ddlccbmcdetail.SelectedItem.Text + " ( " + txtFdt.Text + " - " + txtTdt.Text + " )";
                    GridView1.DataSource = dsFillGrid.Tables[1];
                    GridView1.DataBind();
                    string TotalQuantity = "0";
                    string TotalAmount = "0";
                    TotalQuantity = dsFillGrid.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Quantity")).ToString();
                    TotalAmount = dsFillGrid.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Amount")).ToString();
                    GridView1.FooterRow.Cells[2].Text = "<b>Total : </b>";
                    GridView1.FooterRow.Cells[3].Text = "<b>" + TotalQuantity.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + TotalAmount.ToString() + "</b>";
                   
                  
                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

	protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtDate.Text != "")
            {
                string BillingCycle = ddlBillingCycle.SelectedItem.Text;
                string[] DatePart = txtDate.Text.Split('/');
                int Day = int.Parse(DatePart[0]);
                int Month = int.Parse(DatePart[1]);
                int Year = int.Parse(DatePart[2]);
                string SelectedFromDate = "";
                string SelectedToDate = "";
                if (BillingCycle == "5 days")
                {
                    if (Day >= 1 && Day < 6)
                    {
                        SelectedFromDate = "01";
                        SelectedToDate = "05";
                    }
                    else if (Day > 5 && Day < 11)
                    {
                        SelectedFromDate = "6";
                        SelectedToDate = "10";
                    }
                    else if (Day > 10 && Day < 16)
                    {
                        SelectedFromDate = "11";
                        SelectedToDate = "15";
                    }
                    else if (Day > 15 && Day < 21)
                    {
                        SelectedFromDate = "16";
                        SelectedToDate = "20";
                    }
                    else if (Day > 20 && Day < 26)
                    {
                        SelectedFromDate = "21";
                        SelectedToDate = "25";
                    }
                    else if (Day > 25 && Day <= 31)
                    {
                        SelectedFromDate = "26";
                        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
                        {
                            SelectedToDate = "31";
                        }
                        else if (Month == 2)
                        {
                            SelectedToDate = "28";
                        }
                        else
                        {
                            SelectedToDate = "30";
                        }

                    }
                    SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                    SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;

                }
                else
                {
                    if (Day >= 1 && Day < 11)
                    {
                        SelectedFromDate = "01";
                        SelectedToDate = "10";
                    }
                    else if (Day > 10 && Day < 21)
                    {
                        SelectedFromDate = "11";
                        SelectedToDate = "20";
                    }
                    else if (Day > 20 && Day <= 31)
                    {
                        SelectedFromDate = "21";
                        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
                        {
                            SelectedToDate = "31";
                        }
                        else if (Month == 2)
                        {
                            SelectedToDate = "28";
                        }
                        else
                        {
                            SelectedToDate = "30";
                        }
                    }

                    SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                    SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;
                }
                txtFdt.Text = Convert.ToDateTime(SelectedFromDate, cult).ToString("dd/MM/yyyy");
                txtTdt.Text = Convert.ToDateTime(SelectedToDate, cult).ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void ddlBillingCycle_TextChanged(object sender, EventArgs e)
    {
        txtDate_TextChanged(sender, e);
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
           

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
 	
    }
   
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView1.Columns[7].Visible = true;
            GridView1.Columns[6].Visible = false;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "SocietywithIncentiveAmount" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            div.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "View")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = gvr.RowIndex;
            GridViewRow row1 = GridView1.Rows[index];
            Label lblOfc = (Label)row1.FindControl("lblOffice_Name_E");
            spnofc.InnerHtml = "[" +lblOfc.Text + "]";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowDetail();", true);
            gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
            gv_SocietyMilkDispatchDetail.DataBind();
            string Office_ID = e.CommandArgument.ToString();
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "CCID", "Office_ID", "FromDate", "ToDate" }, new string[] { "44", ddlccbmcdetail.SelectedValue, Office_ID, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gv_SocietyMilkDispatchDetail.DataSource = ds.Tables[0];
                    gv_SocietyMilkDispatchDetail.DataBind();

                    decimal TotalQuantity = 0;
                    decimal TotalFatKg = 0;
                    decimal TotalSnfKg = 0;
                    decimal TotalSnf = 0;
                    TotalQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
                    TotalFatKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    TotalSnfKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    TotalSnf = Math.Round((TotalSnfKg * 100) / TotalQuantity,2);
                    gv_SocietyMilkDispatchDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
                    gv_SocietyMilkDispatchDetail.FooterRow.Cells[5].Text = "<b>" + TotalQuantity.ToString() + "</b>";
                    gv_SocietyMilkDispatchDetail.FooterRow.Cells[6].Text = "<b>" + TotalFatKg.ToString() + "</b>";
                    gv_SocietyMilkDispatchDetail.FooterRow.Cells[7].Text = "<b>" + TotalSnfKg.ToString() + "</b>";
                    gv_SocietyMilkDispatchDetail.FooterRow.Cells[4].Text = "<b>" + TotalSnf.ToString() + "</b>";
                }
            }
        }
    }
}