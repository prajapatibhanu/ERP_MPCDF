using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_CattleFeed_CFP_Suppliers_Registration : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objapi = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        Fillgrd();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (hdnvalue.Value == "0")
        {
            if (!FillCheckGSTrecord()) { InsertUpdate(); }
            else { lblMsg.Text = objapi.Alert("fa-check", "alert-error", "Thank You!", "Fail : GSTN already exist."); }
        }
        else InsertUpdate();

    }
    private bool FillCheckGSTrecord()
    {
        bool result = true;
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Check_GSTN",
                                         new string[] { "flag", "GSTN" },
                                         new string[] { "0", txtGSTN.Text }, "dataset");


            result = Convert.ToBoolean(ds.Tables[0].Rows[0]["iExist"]);


        }
        catch (Exception)
        {

        }
        finally
        {
            ds.Dispose();
            GC.SuppressFinalize(objapi);
        }
        return result;
    }
    private bool GetCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtfromDt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtto.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate < tdate)
            {
                res = true;

            }
            else
            {
                res = false;
                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private void InsertUpdate()
    {
        lblMsg.Text = "";
        if (GetCompareDate())
        {
            StringBuilder sb = new StringBuilder();
            if (txtGSTN.Text != string.Empty)
            {
                if (txtGSTN.Text.Length < 15)
                {
                    sb.Append("Enter valid GST No.\\n");
                }
            }
            if (txtmobile.Text != string.Empty)
            {
                if (txtmobile.Text.Length < 10)
                {
                    sb.Append("Enter valid Mobile No.\\n");
                }
            }
            if (sb.ToString() == string.Empty)
            {
                ds = new DataSet();
                string flag = "0";
                if (Convert.ToInt32(hdnvalue.Value) > 0)
                {
                    flag = "1";
                }
                string ChkTDS = "0";
                if (chkTDS.Checked == true)
                {
                    ChkTDS = "1";
                }
                ds = objapi.ByProcedure("SP_CFP_Supplier_Registration_Insert_Update_Delete",
                                          new string[] { "flag", "SupplierName", "MobileNo", "GSTN", "Email", "ContractFrom", "ContractTo","IsTdsApplicable", "SupplierAddress",
                                  "OfficeID","InsertedBy","IPAddress","SupplierID"},
                                          new string[] { flag,txtName.Text,txtmobile.Text,txtGSTN.Text,txtemail.Text,txtfromDt.Text,txtto.Text, ChkTDS, txtAddress.Text,
                                      objapi.Office_ID(),objapi.createdBy(),Request.UserHostAddress,hdnvalue.Value }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["ErrorMSG"].ToString() == "SUCCESS")
                        {
                            Fillgrd();
                            Clear();
                            lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
                        }
                    }
                }

            }
            else
            {
                lblMsg.Text = objapi.Alert("fa-check", "alert-info", "Fail!", sb.ToString());

            }

        }
        else
        {
            lblMsg.Text = objapi.Alert("fa-check", "alert-info", "Fail!", "From date should be less than To date.");

        }

    }

    private void Fillgrd()
    {
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Supplier_Registration_ByOfficeID_List",
                                         new string[] { "flag", "OfficeID" },
                                         new string[] { "0", objapi.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdlist.DataSource = ds;
                grdlist.DataBind();
                grdlist.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdlist.UseAccessibleHeader = true;
            }

        }
        catch (Exception)
        {

        }
        finally
        {
            ds.Dispose();
            GC.SuppressFinalize(objapi);
        }

    }
    private void Fillrecord()
    {
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Supplier_Registration_ByRegistrationID_List",
                                         new string[] { "flag", "SupplierRegistrationID" },
                                         new string[] { "0", hdnvalue.Value }, "dataset");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    txtName.Text = Convert.ToString(ds.Tables[0].Rows[0]["SupplierName"]);
                    txtmobile.Text = Convert.ToString(ds.Tables[0].Rows[0]["MobileNo"]);
                    txtGSTN.Text = Convert.ToString(ds.Tables[0].Rows[0]["GSTN"]);
                    txtGSTN.Enabled = false;
                    txtemail.Text = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                    txtfromDt.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContractFrom"]);
                    txtto.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContractTo"]);
                    if (ds.Tables[0].Rows[0]["IsTdsApplicable"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[0]["IsTdsApplicable"].ToString() == "1")
                        {
                            chkTDS.Checked = true;
                        }
                    }
                    else
                    {
                        chkTDS.Checked = false;
                    }
                    chkTDS.Checked = true;
                    txtAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["SupplierADDRESS"]);
                    btnSave.Text = "Edit";
                }
            }
        }
        catch (Exception)
        {

        }
        finally
        {
            ds.Dispose();
            GC.SuppressFinalize(objapi);
        }

    }
    private void Clear()
    {
        txtName.Text = string.Empty;
        txtmobile.Text = string.Empty;
        txtGSTN.Text = string.Empty;
        txtemail.Text = string.Empty;
        txtfromDt.Text = string.Empty;
        txtto.Text = string.Empty;
        txtAddress.Text = string.Empty;
        hdnvalue.Value = "0";
        txtGSTN.Enabled = true;
        btnSave.Text = "Save";
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    protected void grdlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        lblMsg.Text = string.Empty;
        switch (e.CommandName)
        {
            case "RecordUpdate":
                Fillrecord();
                break;
            case "RecordDelete":
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_CFP_Supplier_Registration_Insert_Update_Delete",
                                 new string[] { "flag", "SupplierName", "MobileNo", "GSTN", "Email", "ContractFrom", "ContractTo", "SupplierAddress",
                                  "OfficeID","InsertedBy","IPAddress","SupplierID"},
                                 new string[] { "2",txtName.Text,txtmobile.Text,txtGSTN.Text,txtemail.Text,txtfromDt.Text,txtto.Text,txtAddress.Text,
                                      objapi.Office_ID(),objapi.createdBy(),Request.UserHostAddress,hdnvalue.Value }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["ErrorMSG"].ToString() == "SUCCESS")
                        {
                            Fillgrd();
                            Clear();
                            lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
                        }
                    }
                }
                break;
            default:
                break;
        }
    }
    protected void grdlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdlist.PageIndex = e.NewPageIndex;
        Fillgrd();

    }
}