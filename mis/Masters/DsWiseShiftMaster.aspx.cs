using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Masters_DsWiseShiftMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                try
                {
                    lblMsg.Text = "";

                    if (!IsPostBack)
                    {
                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        ViewState["Office_ID"] = Session["Office_ID"].ToString();
                        ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                        FillOffice();
                        FillDetail();
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
                ddlDs.DataSource = ds.Tables[0];
                ddlDs.DataTextField = "Office_Name";
                ddlDs.DataValueField = "Office_ID";
                ddlDs.DataBind();
                ddlDs.Items.Insert(0, new ListItem("Select", "0"));
                ddlDs.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDs.Enabled = false;
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
    protected void FillDetail()
    {
        try
        {
           
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("Usp_DSWiseProductionShiftMaster",
                 new string[] { "flag", "Office_Id" },
                 new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (btnSave.Text == "Save")
            {
                if (ddlDs.SelectedIndex == 0)
                {
                    msg += "Select Dugdh Sangh.";
                }
                if (ddlShift.SelectedIndex == 0)
                {
                    msg += "Select Shift.";
                }
                if (msg == "")
                {
                    string[] startwords = txtShiftStartTime.Text.Split(':');
                    string[] startwords2 = startwords[1].Split(' ');
                    if (txtShiftStartTime.Text.Contains("PM") && startwords[0] == "12")
                    {
                        ViewState["starthour"] = startwords[0];
                    }
                    if (txtShiftStartTime.Text.Contains("PM") && startwords[0] != "12")
                    {
                        ViewState["starthour"] = (12 + Int32.Parse(startwords[0])).ToString();
                    }
                    if (txtShiftStartTime.Text.Contains("AM") && startwords[0] == "12")
                    {
                        ViewState["starthour"] = "0";
                    }
                    if (txtShiftStartTime.Text.Contains("AM") && startwords[0] != "12")
                    {
                        ViewState["starthour"] = startwords[0];
                    }

                    string[] endwords = txtShiftEndTime.Text.Split(':');
                    string[] endwords2 = endwords[1].Split(' ');
                    if (txtShiftEndTime.Text.Contains("PM") && endwords[0] == "12")
                    {
                        ViewState["endhour"] = endwords[0];
                    }
                    if (txtShiftEndTime.Text.Contains("PM") && endwords[0] != "12")
                    {
                        ViewState["endhour"] = (12 + Int32.Parse(endwords[0])).ToString();
                    }
                    if (txtShiftEndTime.Text.Contains("AM") && endwords[0] == "12")
                    {
                        ViewState["endhour"] = "0";
                    }
                    if (txtShiftEndTime.Text.Contains("AM") && endwords[0] != "12")
                    {
                        ViewState["endhour"] = endwords[0];
                    }
                    string starttime = ViewState["starthour"].ToString() + ":" + startwords2[0];
                    string endtime = ViewState["endhour"].ToString() + ":" + endwords2[0];
                    TimeSpan t1 = TimeSpan.Parse(starttime);
                    TimeSpan t2 = TimeSpan.Parse(endtime);
                    TimeSpan t3 = t2 - t1;
                    ds = objdb.ByProcedure("Usp_DSWiseProductionShiftMaster",
                            new string[] { "flag", "Office_Id", "Name","Shift_Id", "StartTime", "EndTime", "Status" },
                            new string[] { "1", ddlDs.SelectedValue, ddlShift.SelectedItem.Text,ddlShift.SelectedValue.ToString(), t1.ToString(), t2.ToString(), "1" }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }

                    }
                    FillDetail();
                    ddlShift.ClearSelection();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void ChkStatus_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("ChkStatus");
            string DSWiseProdShift_Id = chk.ToolTip.ToString();
            ds = objdb.ByProcedure("Usp_DSWiseProductionShiftMaster",
                                      new string[] { "flag", "DSWiseProdShift_Id", "Status" },
                                      new string[] { "4", DSWiseProdShift_Id, "0" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
}