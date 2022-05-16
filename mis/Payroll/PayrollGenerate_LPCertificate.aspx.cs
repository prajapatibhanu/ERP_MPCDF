using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Payroll_PayrollGenerate_LPCertificate : System.Web.UI.Page
{
    DataSet ds1, ds2 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetOffice();
                FillEmployee();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    private void GetOffice()
    {
        try
        {
            ds1 = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {

                ddlOfficeName.DataSource = ds1.Tables[0];
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
                ddlOfficeName.SelectedValue = objdb.Office_ID();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }

    }

    protected void FillEmployee()
    {
        try
        {

            ddlEmployee.Items.Clear();
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ds2 = objdb.ByProcedure("USP_Payroll_Generate_LPC", new string[] { "flag", "Office_ID" }, new string[] { "1", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds2.Tables.Count > 0)
            {
                ddlEmployee.DataSource = ds2;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }

    private void GetEmployeeDetailsByEmpId()
    {
        ds2 = objdb.ByProcedure("USP_Payroll_Generate_LPC",
            new string[] { "flag", "Emp_ID" },
            new string[] { "2", ddlEmployee.SelectedValue }, "dataset");
        if (ds2.Tables.Count > 0)
        {
            txtOrderNo.Text = ds2.Tables[0].Rows[0]["OrderNo"].ToString();
            txtOrderDate.Text = ds2.Tables[0].Rows[0]["OrderDate"].ToString();
            txtTransferOffice.Text = ds2.Tables[0].Rows[0]["OldOffice_Name"].ToString();
            txtProceedingTo.Text = ds2.Tables[0].Rows[0]["NewOffice_Name"].ToString();
            txtRelievingDate.Text = ds2.Tables[0].Rows[0]["RelievingDate"].ToString();
            txtDesignation.Text = ds2.Tables[0].Rows[0]["OldDesignation_Name"].ToString();
            txtLevel.Text = ds2.Tables[0].Rows[0]["Level_Name"].ToString();

        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ddlEmployee.SelectedIndex > 0)
            {
                GetEmployeeDetailsByEmpId();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!  Error 3", ex.Message.ToString());
        }
    }
    protected void BindGrid()
    {
        GridView1.DataSource = ViewState["EDDetails"] as DataTable;
        GridView1.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            if (GridView1.Rows.Count > 0)
            {
                DataTable dt = (DataTable)ViewState["EDDetails"];
                dt.Rows.Add(txtEarningHead.Text.Trim().ToUpper(), txtEarningAmount.Text.Trim()
                            , txtDeductionHead.Text.Trim().ToUpper(), txtDeductionAmount.Text.Trim()
                            );
                ViewState["EDDetails"] = dt;
                this.BindGrid();
                txtEarningHead.Text = string.Empty;
                txtEarningAmount.Text = string.Empty;
                txtDeductionHead.Text = string.Empty;
                txtDeductionAmount.Text = string.Empty;
                if (dt != null) { dt.Dispose(); }
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("EarningHead"), new DataColumn("EarningAmount") 
                , new DataColumn("DeductionHead") , new DataColumn("DeductionAmount")
                });

                dt.Rows.Add(txtEarningHead.Text.Trim().ToUpper(), txtEarningAmount.Text.Trim()
                             , txtDeductionHead.Text.Trim().ToUpper(), txtDeductionAmount.Text.Trim()
                             );
                ViewState["EDDetails"] = dt;
                this.BindGrid();
                txtEarningHead.Text = string.Empty;
                txtEarningAmount.Text = string.Empty;
                txtDeductionHead.Text = string.Empty;
                txtDeductionAmount.Text = string.Empty;
                if (dt != null) { dt.Dispose(); }
            }

        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordDelete")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    int RowIndex = row.RowIndex;
                    int index = Convert.ToInt32(RowIndex);
                    DataTable dt = ViewState["EDDetails"] as DataTable;
                    dt.Rows[index].Delete();
                    ViewState["EDDetails"] = dt;
                    BindGrid();
                    if (dt != null) { dt.Dispose(); }

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 5 : ", ex.Message.ToString());
        }
    }

    public void printLPC(DataSet ds6)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class='content' style='border: 1px solid black'>");
        sb.Append("<table class='table1' style='width:100%; height:100%'>");
        sb.Append("<tr>");
        sb.Append("<td class='text-center' colspan='3'><b>" + txtTransferOffice.Text.Trim() + "</b></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-center' colspan='3'><u><b>//LAST PAY CERTIFICATE//</b></u></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='1'>1-Name :</td>");
        sb.Append("<td class='text-left' colspan='2'>" + ddlEmployee.SelectedItem.Text + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='1'>2-Designation :</td>");
        sb.Append("<td class='text-left' colspan='2'>" + txtDesignation.Text.Trim() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='1'>3-Pay Scale :</td>");
        sb.Append("<td class='text-left' colspan='2'>" + txtLevel.Text.Trim() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='1'>4-Transfer Order No. :</td>");
        sb.Append("<td class='text-left' colspan='2'>" +txtTransferOffice.Text.Trim() + "</br>" + txtOrderNo.Text + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='1'>5-Relieving Order No. :</td>");
        sb.Append("<td class='text-left' colspan='2'>" + txtReliveingOrderNo.Text.Trim() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='1'>6-Proceding To :</td>");
        sb.Append("<td class='text-left' colspan='2'>" + txtProceedingTo.Text.Trim() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='1'>7-Relieving Date from The H.Q :</td>");
        sb.Append("<td class='text-left' colspan='2'>" + txtRelievingDate.Text.Trim() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='1'>8- SALARY PAID UPTO :</td>");
        sb.Append("<td class='text-left' colspan='2'>" + txtRelievingDate.Text.Trim() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");

        sb.Append("<tr>");
        sb.Append("<td></td>");
        sb.Append("<td></td>");
        sb.Append("<td></td>");
        sb.Append("</tr>");

        sb.Append("</table>");
        sb.Append("<table class='table table1-bordered'>");
        int Count = ds6.Tables[0].Rows.Count;
        //int ColCount = ds6.Tables[0].Columns.Count;
        sb.Append("<thead>");
        sb.Append("<td>S No.</td>");
        sb.Append("<td>Earning</td>");
        sb.Append("<td>Amount</td>");
        sb.Append("<td>Deduction</td>");
        sb.Append("<td>Amount</td>");
        sb.Append("</thead>");
        decimal et = 0;
        for (int i = 0; i < Count; i++)
        {

            sb.Append("<tr>");
            sb.Append("<td>" + (i+1) + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["EarningHead"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["EarningAmount"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["DeductionHead"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["DeductionAmount"] + "</td>");
            sb.Append("</tr>");

            if(ds6.Tables[0].Rows[i]["EarningAmount"]!="")
            {
                decimal dd=  Convert.ToDecimal(ds6.Tables[0].Rows[i]["EarningAmount"]);
                et = et + dd;
            }

        }
        sb.Append("<tr>");

       
        sb.Append("<td class='text-center' colspan='2'><b>Total<b></td>");
        sb.Append("<td style='text-align: center;border:1px solid black !important;'><b>" + et.ToString("0.00") + "</b></td>");
        sb.Append("<td></td>");
        sb.Append("<td></td>");
        sb.Append("</tr>");
        
        sb.Append("</table>");
        sb.Append("<table class='table1' style='width:100%; height:100%'>");


        sb.Append("<tr>");

        sb.Append("<td class='text-left' colspan='2'>1 . " + TextBox1.Text + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='2'>2 . " + TextBox2.Text + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='2'>3 . " + TextBox3.Text + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='2'>4 . " + TextBox4.Text + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='2'>5 . " + TextBox5.Text + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='2'>6 . " + TextBox6.Text + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='2'>7 . " + TextBox7.Text + "</td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='1'>Date :" + txtCurrentDate.Text + "</td>");
        sb.Append("<td class='text-right' colspan='1'>Manager (A&P)<br/>" + txtTransferOffice.Text + "<br/></td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        sb.Append("</div>");
        Print.InnerHtml = sb.ToString();
        ds6.Dispose();
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
       
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        DataTable dtInsertChild = (DataTable)ViewState["EDDetails"];

         if (dtInsertChild.Rows.Count > 0)
         {
             DataSet ds3 = new DataSet();
             ds3.Merge(dtInsertChild);
             printLPC(ds3);
             ds3.Dispose();
         }
         else
         {
             lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! ", "Please Enter Earning & Deduction Details");
         }
    }
}