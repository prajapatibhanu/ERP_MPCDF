using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Warehouse_WarehouseUpdate : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string Attachment = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //check session
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    //ds = objdb.ByProcedure("SpAdminOffice",
                    //         new string[] { "flag" },
                    //         new string[] { "1" }, "dataset");
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
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + ex.Message.ToString());
                }
                autofilldata();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    private void autofilldata()
    {
        try
        {
            ds = objdb.ByProcedure("SpWarehouseMaster",
                         new string[] { "flag", "WrId" },
                         new string[] { "2", objdb.Decrypt(Request.QueryString["id"].ToString()) }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtWarehouseName.Text = ds.Tables[0].Rows[0]["WarehouseName"].ToString();
                txtArea.Text = ds.Tables[0].Rows[0]["Area"].ToString();
                txtCapacity.Text = ds.Tables[0].Rows[0]["WarehouseCapacity"].ToString();
                txt_Address.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtInchageName.Text = ds.Tables[0].Rows[0]["InchargeName"].ToString();
                txtMobileNo.Text = ds.Tables[0].Rows[0]["InchargeMobile"].ToString();
                txtemail.Text = ds.Tables[0].Rows[0]["InchargeEmail"].ToString();
                ddloffice.SelectedValue = ds.Tables[0].Rows[0]["OfficeId"].ToString();
                ddlOwnedrented.SelectedValue = ds.Tables[0].Rows[0]["TypeOfWarehouse"].ToString();
                if (ds.Tables[0].Rows[0]["TypeOfWarehouse"].ToString() == "Owned")
                {
                    txtOccupancyForm.Enabled = false;
                    txtOccupancyTo.Enabled = false;
                    FUAgreement.Enabled = false;
                    txtRent.Enabled = false;
                    hypAttach.Visible = false;
                }
                else if (ds.Tables[0].Rows[0]["TypeOfWarehouse"].ToString() == "Rented")
                {
                    txtOccupancyForm.Enabled = true;
                    if (ds.Tables[0].Rows[0]["TypeOfWarehouse"].ToString() != "Owned")
                    {
                        IFormatProvider culture = new CultureInfo("en-US", true);
                        txtOccupancyForm.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Occupancy_Form"].ToString(), culture).ToString("MM/dd/yyyy");
                        txtOccupancyTo.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Occupancy_To"].ToString(), culture).ToString("MM/dd/yyyy");
                        txtRent.Text = ds.Tables[0].Rows[0]["MonthlyRent"].ToString();
                        Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();
                        txtPeriod.Text = ds.Tables[0].Rows[0]["AgreementPeriod"].ToString();
                    }
                    else
                    {
                        txtOccupancyForm.Text = "";
                        txtOccupancyTo.Text = "";
                        txtPeriod.Text = "";
                        txtRent.Text = "";
                        Attachment = "";
                    }
                    txtRent.Enabled = true;
                    FUAgreement.Enabled = true;
                    hypAttach.NavigateUrl = Attachment;

                    if (hypAttach.NavigateUrl != "")
                    {
                        hypAttach.Visible = true;
                    }
                    else
                    {
                        hypAttach.Visible = false;
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Werehouse data not fetch.");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error:" + ex.Message.ToString());
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            string DocumentPath = "";
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
                    else if (size <= 10)
                    {
                        msg += "Uploaded Image should be less than 10 mb.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                    }
                    else
                    {
                        DocumentPath += "~/mis/Warehouse/WrAgrements/" + Guid.NewGuid() + FUAgreement.FileName;
                        FUAgreement.SaveAs(Server.MapPath(DocumentPath));
                    }
                }
                else if (hypAttach.NavigateUrl == "" && Attachment == "" && ddlOwnedrented.SelectedValue == "Rented")
                {
                    msg += "Attach Agreement";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                    SetFocus(FUAgreement);
                }
                else if (hypAttach.NavigateUrl != "" && Attachment == "" && ddlOwnedrented.SelectedValue == "Rented")
                {
                    DocumentPath = hypAttach.NavigateUrl;
                }
                else if (hypAttach.NavigateUrl == "" && Attachment != "" && ddlOwnedrented.SelectedValue == "Rented")
                {
                    DocumentPath = Attachment;
                }
            }

            if (msg == "" && Page.IsValid)
            {
                string Fromdate = "", Todate = "";
                decimal rent = 0;
                if (txtOccupancyForm.Text != "" && txtOccupancyTo.Text != "")
                {
                    IFormatProvider culture = new CultureInfo("en-US", true);
                    DateTime date3 = DateTime.ParseExact(txtOccupancyForm.Text, "MM/dd/yyyy", culture);
                    Fromdate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                    DateTime date4 = DateTime.ParseExact(txtOccupancyTo.Text, "MM/dd/yyyy", culture);
                    Todate = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                }
                if (txtRent.Text != "")
                {
                    rent = Convert.ToDecimal(txtRent.Text.ToString());
                }
                try
                {
                    ds = objdb.ByProcedure("SpWarehouseMaster",
                        new string[] { "flag", "WrId", "WrName", "Area", "WrCapacity", "Address", "InchargeName", "InchargeMobile", "InchargeEmail", "OfficeId", "TypeOfWr", "Occupancy_from", "Occupancy_To", "AgreementPeriod", "Attachment", "MonthlyRent" },
                        new string[] { "3", objdb.Decrypt(Request.QueryString["id"].ToString()), txtWarehouseName.Text, txtArea.Text, txtCapacity.Text, txt_Address.Text, txtInchageName.Text, txtMobileNo.Text, txtemail.Text.ToLower(), ddloffice.SelectedValue, ddlOwnedrented.SelectedValue, Fromdate, Todate, txtPeriod.Text, DocumentPath, rent.ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    //ClearData();
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error:" + ex.Message.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error:" + ex.Message.ToString());
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            string id = objdb.Decrypt(Request.QueryString["id"].ToString());
            Response.Redirect("~/mis/Warehouse/WarehouseList.aspx?id=" + objdb.Encrypt(id) + "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error:" + ex.Message.ToString());
        }
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
        FUAgreement.Enabled = false;
        txtRent.Enabled = false;
        hypAttach.NavigateUrl = "";
        hypAttach.Text = "";
        SetFocus(txtWarehouseName);
    }

    protected void ddlOwnedrented_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOwnedrented.SelectedIndex != 0)
            {
                if (ddlOwnedrented.SelectedValue == "Owned")
                {
                    txtOccupancyForm.Text = "";
                    txtOccupancyForm.Enabled = false;
                    txtOccupancyTo.Text = "";
                    txtOccupancyTo.Enabled = false;
                    txtPeriod.Text = "";
                    FUAgreement.Enabled = false;
                    hypAttach.Visible = false;
                    txtRent.Text = "";
                    txtRent.Enabled = false;
                    lblMsg.Text = "";
                }
                else if (ddlOwnedrented.SelectedValue == "Rented")
                {
                    ds = objdb.ByProcedure("SpWarehouseMaster",
                                  new string[] { "flag", "WrId" },
                                  new string[] { "2", objdb.Decrypt(Request.QueryString["id"].ToString()) }, "dataset");

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        if (ds.Tables[0].Rows[0]["TypeOfWarehouse"].ToString() != "Owned")
                        {
                            IFormatProvider culture = new CultureInfo("en-US", true);
                            txtOccupancyForm.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Occupancy_Form"].ToString(), culture).ToString("MM/dd/yyyy");
                            txtOccupancyTo.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Occupancy_To"].ToString(), culture).ToString("MM/dd/yyyy");
                            txtRent.Text = ds.Tables[0].Rows[0]["MonthlyRent"].ToString();
                            Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();
                            txtPeriod.Text = ds.Tables[0].Rows[0]["AgreementPeriod"].ToString();
                        }
                        else
                        {
                            txtOccupancyForm.Text = "";
                            txtOccupancyTo.Text = "";
                            txtPeriod.Text = "";
                            txtRent.Text = "";
                            Attachment = "";
                        }

                        txtRent.Enabled = true;
                        FUAgreement.Enabled = true;
                        txtOccupancyForm.Enabled = true;
                        txtOccupancyTo.Enabled = true;
                        hypAttach.NavigateUrl = Attachment;

                        if (hypAttach.NavigateUrl != "")
                        {
                            hypAttach.Visible = true;
                        }
                        else
                        {
                            hypAttach.Visible = false;
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Werehouse data not fetch.");
                    }
                    lblMsg.Text = "";
                }
            }
            else
            {
                txtOccupancyForm.Enabled = false;
                txtOccupancyForm.Text = "";
                txtOccupancyTo.Enabled = false;
                txtOccupancyTo.Text = "";
                txtPeriod.Text = "";
                FUAgreement.Enabled = false;
                hypAttach.Visible = false;
                txtRent.Enabled = false;
                txtRent.Text = "";
                lblMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + ex.Message.ToString());
        }
    }

    protected void txtOccupancyForm_TextChanged(object sender, EventArgs e)
    {
        if (txtOccupancyForm.Text != "" && txtOccupancyTo.Text != "")
        {
            txtPeriod.Text = calculateYMD(txtOccupancyForm.Text, "MM/dd/yyyy", txtOccupancyTo.Text, "MM/dd/yyyy");
        }
        else
        {
            SetFocus(txtOccupancyTo);
        }
    }

    protected void txtOccupancyTo_TextChanged(object sender, EventArgs e)
    {
        if (txtOccupancyForm.Text != "" && txtOccupancyTo.Text != "")
        {
            txtPeriod.Text = calculateYMD(txtOccupancyForm.Text, "MM/dd/yyyy", txtOccupancyTo.Text, "MM/dd/yyyy");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select Occupancy from date.');", true);
            SetFocus(txtOccupancyForm);
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
                    if (Math.Abs(totaldays + 1) != 0)
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
                return "Wrong date format!!";
            }
        }
        catch (Exception ex)
        {
            return "Exception Error :" + ex.Message.ToString();
        }
    }

    public string DocumentPath { get; set; }

    public string FileImg { get; set; }
}