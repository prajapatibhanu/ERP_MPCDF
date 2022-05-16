using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

public partial class mis_HR_HREmpLeaveRequests : System.Web.UI.Page
{
    DataSet ds, ds2;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();

                    txtReason.Attributes.Add("readonly", "readonly");
                    FillDropdown();
                    FillGrid();
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select Year", "0"));
            }
            ddlFinancialYear.SelectedValue = DateTime.Now.Year.ToString(); ;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            lblMsg2.Text = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Office_ID", "LeaveApproveAuthority", "Year", "Financial_Year" },
                  new string[] { "10", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), ddlFinancialYear.SelectedValue.ToString(), ddlFinancialYear.SelectedValue.ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                // lblMsg2.Text = "No Record Found...";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["LeaveId"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "LeaveId", "Year", "Financial_Year" },
                  new string[] { "11", ViewState["LeaveId"].ToString(), ddlFinancialYear.SelectedValue.ToString(), ddlFinancialYear.SelectedValue.ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                DetailsView1.DataSource = ds.Tables[0];
                DetailsView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;




                txtReason.Text = ds.Tables[0].Rows[0]["LeaveRemark"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
                ViewState["LeaveType"] = ds.Tables[0].Rows[0]["LeaveType"].ToString();
                ViewState["EmpID"] = ds.Tables[0].Rows[0]["Emp_ID"].ToString();

                GridView2.DataSource = new string[] {};
                GridView2.DataBind();
                ds2 = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Emp_ID", "LeaveId" }, new string[] { "23", ViewState["Emp_ID"].ToString(), ViewState["LeaveId"].ToString() }, "dataset");
                if (ds2.Tables[0].Rows.Count != 0)
                {
                    GridView2.DataSource = ds2.Tables[0];
                    GridView2.DataBind();
                }
                if (ds2.Tables[1].Rows.Count != 0)
                {
                    ReplyBoX.Visible = true;
                    btnApprove.Visible = true;
                }
                else
                {
                    ReplyBoX.Visible = false;
                    btnApprove.Visible = false;
                }
                


                ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Office_ID", "Emp_ID", "Approval_Auth_Id" }, new string[] { "22", ViewState["Office_ID"].ToString(), ViewState["EmpID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlForwardedToName.DataSource = ds;
                    ddlForwardedToName.DataTextField = "Emp_Name";
                    ddlForwardedToName.DataValueField = "Emp_ID";
                    ddlForwardedToName.DataBind();
                    //ddlForwardedToName.Items.Insert(0, new ListItem("Select", "0"));
                }

                ddlStatus.ClearSelection();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue.ToString() == "Forwarded")
        {
            ForwardLeave();
        }
        else
        {
            ApproveRejectLeave();
        }
    }



    protected void ApproveRejectLeave()
    {
        try
        {
            DataSet ds1;
            string DocPath = "";
            string OrderDate = "";
            lblMsg.Text = "";
            string msg = "";
            string TakenLeave = "";
            int DayCount = 0;
            decimal TotalTakenLeave = 0;
            if (ddlStatus.SelectedIndex == 0)
            {
                msg += "Select Status. <br/>";
            }
            if (txtRemark.Text == "")
            {
                msg += "Enter Remark. <br/>";
            }
            if (txtOrderDate.Text != "")
            {
                OrderDate = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                OrderDate = "";
            }
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveType_ID", "Financial_Year", "Emp_ID" },
            new string[] { "38", ViewState["LeaveType"].ToString(), ddlFinancialYear.SelectedValue.ToString(), ViewState["EmpID"].ToString() }, "datatset");

            string financialYear = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
            string LeaveDay1 = ds.Tables[0].Rows[0]["Leave_Days"].ToString();

            if (ddlStatus.SelectedValue.ToString() == "Approved")
            {
                ds1 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
            new string[] { "8", ViewState["LeaveId"].ToString() }, "datatset");
                if (ds1 != null && ds1.Tables.Count != 0)
                {
                    if (ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "First Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Second Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Half Day")
                    {
                        TakenLeave = "0.5";
                        TotalTakenLeave = Convert.ToDecimal(TakenLeave.ToString());
                    }
                    else
                    {
                        int count = ds1.Tables[1].Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            string DayName = ds1.Tables[1].Rows[i]["DayName"].ToString();
                            string Leave_Date = ds1.Tables[1].Rows[i]["LeaveDate"].ToString();
                            if (DayName == "Sunday")
                            {
                                DayCount = DayCount;
                            }
                            else
                            {
                                int status = 1;
                                DataSet ds2 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
             new string[] { "21", ViewState["LeaveId"].ToString() }, "datatset");
                                if (ds1 != null)
                                {
                                    int leaveCount = ds2.Tables[0].Rows.Count;
                                    if (ds2 != null && ds2.Tables[0].Rows.Count != 0)
                                    {
                                        for (int j = 0; j < leaveCount; j++)
                                        {
                                            string Holiday_Date = ds2.Tables[0].Rows[j]["Holiday_Date"].ToString();
                                            if (Leave_Date == Holiday_Date)
                                            {
                                                status = 0;
                                                break;
                                            }
                                            else
                                            {
                                                status = 1;
                                            }
                                        }
                                    }
                                    if (status == 1)
                                    {
                                        DayCount++;
                                    }
                                    else
                                    {
                                        DayCount = DayCount;
                                    }
                                }
                            }
                        }
                        TotalTakenLeave = DayCount;
                        //TotalTakenLeave = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TakenLeave"].ToString());
                    }
                }

            }
            else
            {
                TotalTakenLeave = 0;
            }
            objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "LeaveType", "TotalAllowedLeave", "TotalTakenLeave", "FinancialYear" },
            new string[] { "2", ViewState["EmpID"].ToString(), ViewState["LeaveType"].ToString(), LeaveDay1, TotalTakenLeave.ToString(), financialYear }, "datatset");
            if (ApprovalDoc.HasFile)
            {
                DocPath = "../HR/UploadDoc/LeaveApproveDoc/" + Guid.NewGuid() + "-" + ApprovalDoc.FileName;
                ApprovalDoc.PostedFile.SaveAs(Server.MapPath(DocPath));
            }
            objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId", "LeaveStatus", "RemarkByApprovalAuth", "LeaveApprovalOrderNo", "LeaveApprovalOrderDate", "LeaveApprovalOrderFile" },
            new string[] { "9", ViewState["LeaveId"].ToString(), ddlStatus.SelectedValue.ToString(), txtRemark.Text, txtOrderNo.Text, OrderDate, DocPath }, "datatset");


            /***********SEND SMS TO EMPLOYEE****************/
            DataSet ds3;
            ds3 = objdb.ByProcedure("SpHRLeaveApplication",
                new string[] { "flag", "LeaveId" },
                new string[] { "31", ViewState["LeaveId"].ToString() }, "datatset");
            if (ds3.Tables[0].Rows.Count > 0)
            {
                string EmpMobileNo = ds3.Tables[0].Rows[0]["Emp_MobileNo"].ToString();
                string EmpName = ds3.Tables[0].Rows[0]["Emp_Name"].ToString();
                string Leavedaycount = ds3.Tables[0].Rows[0]["Leavedaycount"].ToString()+" days";
                string LeaveType = ds3.Tables[0].Rows[0]["Leave_Type"].ToString();
                string LeaveFromDate = ds3.Tables[0].Rows[0]["LeaveFromDate"].ToString();
                string LeaveToDate = ds3.Tables[0].Rows[0]["LeaveToDate"].ToString();
                if (EmpMobileNo != "9893098930" && EmpMobileNo != "0000000000" && EmpMobileNo != null)
                {
                    string Empmessage = "";
					string link="";
					ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    if (TotalTakenLeave.ToString() == "0.5")
                    {
                        //Leave Sanction
                        Empmessage = "Your Leave request for Half Day of " + LeaveFromDate + " has been " + ddlStatus.SelectedValue.ToString()  ;
						
                        //Empmessage = "Your leave request for half day of" + LeaveFromDate + " has been " + ddlStatus.SelectedValue.ToString() + ".";
                        link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + EmpMobileNo + "&text=" + Server.UrlEncode(Empmessage) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162399208592688&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=0&camp_name=mpmilk_user";
                    }
                    else
                    {
                        //Leave Status
                        Empmessage = "Your Leave request for " + Leavedaycount + " " + LeaveType + " from " + LeaveFromDate + " to " + LeaveToDate + " has been " + ddlStatus.SelectedValue.ToString();
                        //Empmessage = "Your leave request from " + LeaveFromDate + " to " + LeaveToDate + " has been " + ddlStatus.SelectedValue.ToString() + ".";
                        link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + EmpMobileNo + "&text=" + Server.UrlEncode(Empmessage) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162399176722438&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=0&camp_name=mpmilk_user";
                    }
                   // Empmessage = "Your leave request from " + LeaveFromDate + " to " + LeaveToDate + " has been " + ddlStatus.SelectedItem.Text + ".";
                   // string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=3597C1493C124F&type=unicode&routeid=2&contacts=" + EmpMobileNo + "&senderid=MPSCDF&msg=" + Empmessage;
                    HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    Stream stream = response.GetResponseStream();
                }

            }
            /***********************************************/


            FillGrid();



            lblMsg.Text = "<div class='alert alert-success alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><h4><i class='icon fa fa-check'></i> Success</h4>Leave has been " + ddlStatus.SelectedValue.ToString() + " successfully. <a href='HRLeaveAppliedByStaff.aspx'>Click Here</a> to view " + ddlStatus.SelectedValue.ToString() + " leave requests..</div>";
            ddlStatus.ClearSelection();
            txtRemark.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ForwardLeave()
    {
        try
        {
            lblMsg.Text = "";

            objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId", "LeaveStatus", "LeaveApproveAuthority", "RemarkByApprovalAuth", "Emp_ID" },
                        new string[] { "40", ViewState["LeaveId"].ToString(), ddlStatus.SelectedValue.ToString(), ddlForwardedToName.SelectedValue.ToString(), txtRemark.Text, ViewState["Emp_ID"].ToString() }, "datatset");

            lblMsg.Text = "<div class='alert alert-success alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><h4><i class='icon fa fa-check'></i> Success</h4>Leave has been " + ddlStatus.SelectedValue.ToString() + " successfully. <a href='HRLeaveAppliedByStaff.aspx'>Click Here</a> to view " + ddlStatus.SelectedValue.ToString() + " leave requests..</div>";
            ddlStatus.ClearSelection();
            txtRemark.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}