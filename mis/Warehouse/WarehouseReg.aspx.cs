using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Threading;

public partial class mis_Warehouse_WarehouseReg : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        //check session
        if (Session["Emp_ID"] == null)
        {
            Response.Redirect("~/mis/Login.aspx");
        }

        try
        {
            if (!Page.IsPostBack)
            {
                //ds = objdb.ByProcedure("SpAdminOffice",
                //             new string[] { "flag" },
                //             new string[] { "23" }, "dataset");
                ds = objdb.ByProcedure("SpAdminOffice",
                            new string[] { "flag" },
                            new string[] { "54" }, "dataset");

                ddloffice.DataSource = ds;
                ddloffice.DataTextField = "Office_Name";
                ddloffice.DataValueField = "Office_ID";
                ddloffice.DataBind();
                ddloffice.Items.Insert(0, new ListItem("Select", "0"));
                ddloffice.SelectedValue = objdb.Office_ID();
                if (objdb.Office_ID().ToString() == objdb.GetHOId().ToString())
                {
                    ddloffice.Enabled = true;
                }
                else
                {
                    ddloffice.Enabled = false;
                }
                SetFocus(txtWarehouseName);

                //Desabled Controls
                txtOccupancyForm.Enabled = false;
                txtOccupancyTo.Enabled = false;
                txtPeriod.Enabled = false;
                FUAgreement.Enabled = false;
                txtRent.Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Close", "CloseDialog();", true);
                string DocumentPath = "";
                string msg = "";
                if (FUAgreement.Enabled == true)
                {
                    if (FUAgreement.HasFile)
                    {
                        decimal size = Math.Round(((decimal)FUAgreement.PostedFile.ContentLength / (decimal)1024), 2);
                        FileImg = System.IO.Path.GetExtension(FUAgreement.FileName);
                        if (FileImg != ".pdf" && FileImg != ".doc" && FileImg != ".docx")
                        {
                            msg += "Only document formats (*.pdf, *.doc & *.docx) are accepted.";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                        }
                        else if (size <= 5)
                        {
                            msg += "Uploaded Image should be less than 5 mb.";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                        }
                        else
                        {
                            DocumentPath += "~/mis/Warehouse/WrAgrements/" + Guid.NewGuid() + FUAgreement.FileName;
                            FUAgreement.SaveAs(Server.MapPath(DocumentPath));
                        }
                    }
                    else
                    {
                        msg += "Attach Agreement";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                    }
                }

                if (msg == "")
                {
                    IFormatProvider culture = new CultureInfo("en-US", true);
                    System.Threading.Thread.SpinWait(1);
                    string date = "", date1 = "";
                    decimal rent = 0;
                    if (txtOccupancyForm.Text != "")
                    {
                        DateTime date3 = DateTime.ParseExact(txtOccupancyForm.Text, "MM/dd/yyyy", culture);
                        date = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    }

                    if (txtOccupancyTo.Text != "")
                    {
                        DateTime date4 = DateTime.ParseExact(txtOccupancyTo.Text, "MM/dd/yyyy", culture);
                        date1 = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    }

                    if (txtRent.Text != "")
                    {
                        rent = Convert.ToDecimal(txtRent.Text.ToString());
                    }

                    try
                    {
                        ds = objdb.ByProcedure("SpWarehouseMaster",
                            new string[] { "flag", "WrName", "Area", "WrCapacity", "Address", "InchargeName", "InchargeMobile", "InchargeEmail", "OfficeId", "TypeOfWr", "Occupancy_from", "Occupancy_To", "AgreementPeriod", "Attachment", "MonthlyRent", "CreatedBy" },
                            new string[] { "1", txtWarehouseName.Text, txtArea.Text, txtCapacity.Text, txt_Address.Text, txtInchageName.Text, txtMobileNo.Text, txtemail.Text.ToLower(), ddloffice.SelectedValue, ddlOwnedrented.SelectedValue, date, date1, txtPeriod.Text, DocumentPath, rent.ToString(), Session["Emp_ID"].ToString() }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        ClearData();
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
        lblMsg.Text = "";
    }

    private void ClearData()
    {
        txtWarehouseName.Text = "";
        txtArea.Text = "";
        txtCapacity.Text = "";
        txt_Address.Text = "";
        txtInchageName.Text = "";
        txtMobileNo.Text = "";
        txtemail.Text = "";
        ddloffice.SelectedIndex = 0;
        ddlOwnedrented.SelectedIndex = 0;
        txtPeriod.Text = "";
        txtOccupancyForm.Text = "";
        txtOccupancyTo.Text = "";
        txtRent.Text = "";
        txtOccupancyForm.Enabled = false;
        txtOccupancyTo.Enabled = false;
        txtPeriod.Enabled = false;
        FUAgreement.Enabled = false;
        txtRent.Enabled = false;
        SetFocus(txtWarehouseName);
    }

    protected void ddlOwnedrented_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOwnedrented.SelectedIndex != 0)
        {
            if (ddlOwnedrented.SelectedValue == "Owned")
            {
                txtOccupancyForm.Enabled = false;
                txtOccupancyForm.Text = "";
                txtOccupancyTo.Enabled = false;
                txtOccupancyTo.Text = "";
                txtPeriod.Enabled = false;
                txtPeriod.Text = "";
                FUAgreement.Enabled = false;
                txtRent.Enabled = false;
                txtRent.Text = "";
                lblMsg.Text = "";
            }
            else if (ddlOwnedrented.SelectedValue == "Rented")
            {
                txtOccupancyForm.Enabled = true;
                txtOccupancyTo.Enabled = true;
                txtPeriod.Enabled = true;
                FUAgreement.Enabled = true;
                txtRent.Enabled = true;
                lblMsg.Text = "";
            }
        }
        else
        {
            txtOccupancyForm.Enabled = false;
            txtOccupancyTo.Enabled = false;
            txtOccupancyForm.Text = "";
            txtOccupancyTo.Text = "";
            txtPeriod.Enabled = false;
            txtPeriod.Text = "";
            FUAgreement.Enabled = false;
            txtRent.Enabled = false;
            txtRent.Text = "";
            lblMsg.Text = "";
        }
    }

    protected void txtOccupancyForm_TextChanged(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (txtOccupancyForm.Text != "" && txtOccupancyTo.Text != "")
            {
                string period = calculateYMD(txtOccupancyForm.Text, "MM/dd/yyyy", txtOccupancyTo.Text, "MM/dd/yyyy");
                if (period != "ex")
                {
                    if (period != "0")
                    {
                        txtPeriod.Text = period;
                    }
                    else
                    {
                        txtPeriod.Text = "Invalid date range!!";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Invalid Occupancy date range!!');", true);
                        txtOccupancyForm.Text = "";
                        SetFocus(txtOccupancyForm);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Invalid date and format!!!!');", true);
                    txtOccupancyForm.Text = "";
                    SetFocus(txtOccupancyForm);
                }
            }
            else
            {
                SetFocus(txtOccupancyTo);
            }
        }
    }

    protected void txtOccupancyTo_TextChanged(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (txtOccupancyForm.Text != "" && txtOccupancyTo.Text != "")
            {
                string period = calculateYMD(txtOccupancyForm.Text, "MM/dd/yyyy", txtOccupancyTo.Text, "MM/dd/yyyy");

                if (period != "ex")
                {
                    if (period != "0")
                    {
                        txtPeriod.Text = period;
                    }
                    else
                    {
                        txtPeriod.Text = "Invalid date range!!";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Invalid Occupancy Date range!!');", true);
                        txtOccupancyTo.Text = "";
                        SetFocus(txtOccupancyTo);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Invalid date and format!!!!');", true);
                    txtOccupancyTo.Text = "";
                    SetFocus(txtOccupancyTo);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select Occupancy from date.');", true);
                SetFocus(txtOccupancyForm);
            }
        }
    }

    public string calculateYMD(string FromDate, string FromDateformat, string ToDate, string ToDateFormat)
    {
        try
        {
            int totaldays = 0;
            string year = "", month = "", day = "";
            IFormatProvider cultur = new CultureInfo("en-US", true);

            DateTime d = DateTime.ParseExact(FromDate, FromDateformat, cultur);
            string Date1 = d.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime d1 = DateTime.ParseExact(ToDate, ToDateFormat, cultur);
            string Date2 = d1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (d <= d1)
            {
                DateTime compareTo = DateTime.Parse(Date1);
                DateTime now = DateTime.Parse(Date2);

                int Year = now.Year - compareTo.Year;
                int mont = now.Month - compareTo.Month;
                int months = Year * 12 + mont;
                int CountDays = now.Day - compareTo.Day;
                months += now.Day < compareTo.Day ? -1 : 0;

                int totalYear = months / 12;
                int totalmonth = months % 12;
                int Days = now.Subtract(compareTo.AddMonths(months)).Days;

                if (Days > 31) { totaldays = (Days + 1) % 30; } else { totaldays = Days; }

                if (totalYear > 1) { if (Math.Abs(totalYear) != 0) { year = Math.Abs(totalYear) + " Years "; } else { year = ""; } }
                else { if (Math.Abs(totalYear) != 0) { year = Math.Abs(totalYear) + " Year "; } else { year = ""; } }

                if (totalmonth > 1) { if (Math.Abs(totalmonth) != 0) { month = Math.Abs(totalmonth) + " Months "; } else { month = ""; } }
                else { if (Math.Abs(totalmonth) != 0) { month = Math.Abs(totalmonth) + " Month "; } else { month = ""; } }

                if (totaldays > 1) { if (Math.Abs(totaldays) != 0) { day = Math.Abs(totaldays + 1) + " Days"; } else { day = ""; } }
                else
                {
                    if (Math.Abs(totaldays) >= 0)
                    {
                        if ((totaldays + 1) == 1) { day = Math.Abs(totaldays + 1) + " Day"; }
                        else { day = Math.Abs(totaldays + 1) + " Days"; }
                    }
                    else { day = ""; }
                }

                //return value in string as format like {d} Year {d} Months {d} days
                return year + month + day;
            }
            else
            {
                return "0"; //Invalid date Range (Start to End)..!!
            }
        }
        catch (Exception)
        {
            return "ex"; //Invalid date format..!!
        }
    }

    public string DocumentPath { get; set; }

    public string FileImg { get; set; }
}