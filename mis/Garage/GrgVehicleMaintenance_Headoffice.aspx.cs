using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Garage_GrgVehicleMaintenance_Headoffice : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                lblMsg.Text = "";
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();


                    txtOwnedServicingDate.Attributes.Add("readonly", "readonly");
                    txtOwnedNextServicingDueDate.Attributes.Add("readonly", "readonly");


                    FillYearMonth();
                    FillVehicle();
                   // pnlOwned.Visible = false;
                    pnlInCaseOfServicing.Visible = false;
                   // pnlHired.Visible = true;
                    rbtVehicleOwnedType_SelectedIndexChanged(sender, e);


                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillYearMonth()
    {
        try
        {

            ds = objdb.ByProcedure("spGrgVehicleMaster", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds.Tables[0];
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, "Select");

            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                ddlMonth.DataSource = ds.Tables[1];
                ddlMonth.DataTextField = "MonthName";
                ddlMonth.DataValueField = "MonthID";
                ddlMonth.DataBind();
                ddlMonth.Items.Insert(0, "Select");

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillVehicle()
    {
        try
        {
            ddlVehicleNo.Items.Clear();
            ds = objdb.ByProcedure("spGrgVehicleMaster", new string[] { "flag", "VehicleOwnedType", "Office_ID" }, new string[] { "3", "0", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlVehicleNo.DataSource = ds.Tables[0];
                ddlVehicleNo.DataTextField = "VehicleNo";
                ddlVehicleNo.DataValueField = "VehicleID";
                ddlVehicleNo.DataBind();
                ddlVehicleNo.Items.Insert(0, "Select");
            }
            else
            {
                ddlVehicleNo.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month. \\n";
            }
            if (ddlVehicleNo.SelectedIndex == 0)
            {
                msg += "Select Vehicle No. \\n";
            }

            if (rbtVehicleOwnedType.SelectedValue == "Owned")
            {
                if (txtOwnedTotRunupMonthly.Text == "")
                {
                    msg += "Enter Total Run-up Monthly. \\n";
                }
                if (txtOwnedFuelConsumption.Text == "")
                {
                    msg += "Enter Fuel Consumption. \\n";
                }
                if (txtOwnedFuelRate.Text == "")
                {
                    msg += "Enter Fuel Rate. \\n";
                }

                if (txtOwnedFuelExpences.Text == "")
                {
                    msg += "Enter Fuel Expences. \\n";
                }
                if (txtOwnedOtherExpensesOnVehicle.Text == "")
                {
                    msg += "Enter Other Expenses On Vehicle. \\n";
                }
                if (txtOwnedExpensesDetails.Text == "")
                {
                    msg += "Enter  Expenses Details. \\n";
                }

                if (chkOwnedInCaseOfServicing.Checked == true)
                {
                    if (txtOwnedServicingDate.Text == "")
                    {
                        msg += "Enter Servicing Date. \\n";
                    }
                    if (txtOwnedTotalKM.Text == "")
                    {
                        msg += "Enter Total KM. \\n";
                    }
                    if (txtOwnedTotalExpensesInServicing.Text == "")
                    {
                        msg += "Enter Total Expenses In Servicing. \\n";
                    }
                    if (txtOwnedExpensesBrief.Text == "")
                    {
                        msg += "Enter Expenses Brief. \\n";
                    }
                    if (txtOwnedNextServicingDueDate.Text == "")
                    {
                        msg += "Enter Next Servicing Due Date. \\n";
                    }
                    if (txtOwnedOtherInfo.Text == "")
                    {
                        msg += "Enter Other Info. \\n";
                    }

                }
            }
            //else
            //{
            //    if (txtHiredTotalRunupMonthly.Text == "")
            //    {
            //        msg += "Enter Total Run-up Monthly. \\n";
            //    }
            //    if (txtBillNo.Text == "")
            //    {
            //        msg += "Enter Bill No. \\n";
            //    }
            //    if (txtBillAmount.Text == "")
            //    {
            //        msg += "Enter Bill Amount. \\n";
            //    }
            //}


            if (msg.Trim() == "")
            {

                if (rbtVehicleOwnedType.SelectedValue == "Owned")
                {
                    string OwnedInCaseOfServicing = "No";
                    if (chkOwnedInCaseOfServicing.Checked == true)
                        OwnedInCaseOfServicing = "Yes";

                    string OwnedServicingDate = null;
                    if (txtOwnedServicingDate.Text != "")
                        OwnedServicingDate = Convert.ToDateTime(txtOwnedServicingDate.Text, cult).ToString("yyyy/MM/dd");


                    string OwnedNextServicingDueDate = null;
                    if (txtOwnedNextServicingDueDate.Text != "")
                        OwnedNextServicingDueDate = Convert.ToDateTime(txtOwnedNextServicingDueDate.Text, cult).ToString("yyyy/MM/dd");

                    string OwnedTotalKM = "0";
                    if (txtOwnedTotalKM.Text != "")
                        OwnedTotalKM = txtOwnedTotalKM.Text;

                    string OwnedTotalExpensesInServicing = "0";
                    if (txtOwnedTotalExpensesInServicing.Text != "")
                        OwnedTotalExpensesInServicing = txtOwnedTotalExpensesInServicing.Text;


                    if (chkOwnedInCaseOfServicing.Checked == true)
                    {
                        objdb.ByProcedure("spGrgVehicleMaster",
                        new string[] { "flag", "Office_ID", "Year", "MonthNo"
                            , "MonthName", "VehicleOwnedType"
                            , "VehicleID", "OwnedTotRunupMonthly", "OwnedFuelExpences", "OwnedOtherExpensesOnVehicle"
                            , "OwnedExpensesDetails", "OwnedInCaseOfServicing", "OwnedServicingDate", "OwnedTotalKM"
                            , "OwnedTotalExpensesInServicing", "OwnedExpensesBrief", "OwnedNextServicingDueDate", "OwnedOtherInfo",  "UpdatedBy" 
                            ,"OwnedFuelConsumption","OwnedFuelRate"
                        },
                        new string[] { "4", ViewState["Office_ID"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString()
                        , ddlMonth.SelectedItem.ToString()  , rbtVehicleOwnedType.SelectedValue.ToString()
                        , ddlVehicleNo.SelectedValue.ToString(), txtOwnedTotRunupMonthly.Text, txtOwnedFuelExpences.Text, txtOwnedOtherExpensesOnVehicle.Text
                        , txtOwnedExpensesDetails.Text, OwnedInCaseOfServicing,OwnedServicingDate, OwnedTotalKM
                        , OwnedTotalExpensesInServicing, txtOwnedExpensesBrief.Text,  
                        OwnedNextServicingDueDate, txtOwnedOtherInfo.Text, ViewState["Emp_ID"].ToString() 
                        ,txtOwnedFuelConsumption.Text,txtOwnedFuelRate.Text
                        }, "dataset");
                    }
                    else
                    {
                        objdb.ByProcedure("spGrgVehicleMaster",
                    new string[] { "flag", "Office_ID", "Year", "MonthNo"
                            , "MonthName", "VehicleOwnedType"
                            , "VehicleID", "OwnedTotRunupMonthly", "OwnedFuelExpences", "OwnedOtherExpensesOnVehicle"
                            , "OwnedExpensesDetails", "OwnedInCaseOfServicing",  "UpdatedBy" 
                            ,"OwnedFuelConsumption","OwnedFuelRate"
                    },
                    new string[] { "4", ViewState["Office_ID"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString()
                        , ddlMonth.SelectedItem.ToString()  , rbtVehicleOwnedType.SelectedValue.ToString()
                        , ddlVehicleNo.SelectedValue.ToString(), txtOwnedTotRunupMonthly.Text, txtOwnedFuelExpences.Text, txtOwnedOtherExpensesOnVehicle.Text
                        , txtOwnedExpensesDetails.Text, OwnedInCaseOfServicing, ViewState["Emp_ID"].ToString() 
                        ,txtOwnedFuelConsumption.Text,txtOwnedFuelRate.Text
                        }, "dataset");
                    }
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Saved.");
                    ClearText();

                }
                //else
                //{
                //    objdb.ByProcedure("spGrgVehicleMaster",
                //    new string[] { "flag", "Office_ID", "Year", "MonthNo", "MonthName", "VehicleOwnedType", "VehicleID", "HiredTotalRunupMonthly", "BillNo", "BillAmount", "UpdatedBy" },
                //    new string[] { "4", ViewState["Office_ID"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlMonth.SelectedItem.ToString()
                //        , rbtVehicleOwnedType.SelectedValue.ToString(), ddlVehicleNo.SelectedValue.ToString(),
                //        txtHiredTotalRunupMonthly.Text,txtBillNo.Text,txtBillAmount.Text,ViewState["Emp_ID"].ToString() }, "dataset");

                //    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Saved.");
                //    ClearText();
                //}


            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void rbtVehicleOwnedType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillVehicle();
            //chkOwnedInCaseOfServicing.Checked = false;
            //pnlOwnedInCaseOfServicing.Enabled = false;
            if (rbtVehicleOwnedType.SelectedValue.ToString() == "Owned")
            {
                //pnlOwned.Visible = true;
               // pnlHired.Visible = false;
                pnlInCaseOfServicing.Visible = false;
                txtOwnedServicingDate.Text = "";
                txtOwnedTotalKM.Text = "";
                txtOwnedTotalExpensesInServicing.Text = "";
                txtOwnedExpensesBrief.Text = "";
                txtOwnedNextServicingDueDate.Text = "";
                txtOwnedOtherInfo.Text = "";
                if (chkOwnedInCaseOfServicing.Checked == true)
                {
                    pnlInCaseOfServicing.Visible = true;
                    txtOwnedServicingDate.Text = "";
                    txtOwnedTotalKM.Text = "";
                    txtOwnedTotalExpensesInServicing.Text = "";
                    txtOwnedExpensesBrief.Text = "";
                    txtOwnedNextServicingDueDate.Text = "";
                    txtOwnedOtherInfo.Text = "";
                }


            }
            else
            {
               // pnlOwned.Visible = false;
                pnlInCaseOfServicing.Visible = false;
                txtOwnedServicingDate.Text = "";
                txtOwnedTotalKM.Text = "";
                txtOwnedTotalExpensesInServicing.Text = "";
                txtOwnedExpensesBrief.Text = "";
                txtOwnedNextServicingDueDate.Text = "";
                txtOwnedOtherInfo.Text = "";

                //pnlHired.Visible = true;

            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ClearText()
    {
        try
        {
            ddlYear.ClearSelection();
            ddlMonth.ClearSelection();
            //rbtVehicleOwnedType.SelectedValue = "Hired";
           // pnlOwned.Visible = false;
            //pnlHired.Visible = true;
            FillVehicle();
            txtOwnedTotRunupMonthly.Text = "";
            txtOwnedFuelConsumption.Text = "";
            txtOwnedFuelRate.Text = "";
            txtOwnedFuelExpences.Text = "";
            txtOwnedOtherExpensesOnVehicle.Text = "";
            txtOwnedExpensesDetails.Text = "";
            chkOwnedInCaseOfServicing.Checked = false;
            txtOwnedServicingDate.Text = "";
            txtOwnedTotalKM.Text = "";
            txtOwnedTotalExpensesInServicing.Text = "";
            txtOwnedExpensesBrief.Text = "";
            txtOwnedNextServicingDueDate.Text = "";
            txtOwnedOtherInfo.Text = "";
            txtHiredTotalRunupMonthly.Text = "";
            txtBillNo.Text = "";
            txtBillAmount.Text = "";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void chkOwnedInCaseOfServicing_CheckedChanged(object sender, EventArgs e)
    {
        txtOwnedServicingDate.Text = "";
        txtOwnedTotalKM.Text = "";
        txtOwnedTotalExpensesInServicing.Text = "";
        txtOwnedExpensesBrief.Text = "";
        txtOwnedNextServicingDueDate.Text = "";
        txtOwnedOtherInfo.Text = "";
        pnlInCaseOfServicing.Visible = false;
        if (chkOwnedInCaseOfServicing.Checked == true)
        {
            pnlInCaseOfServicing.Visible = true;

        }
    }
}