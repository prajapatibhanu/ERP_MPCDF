using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Sendsms : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
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
					ddlOffice.Enabled = false;
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlOffice.Enabled = true;
                }
                    FillEmployee();
                    FillDetail();
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
    protected void FillDetail()
    {
        try
        {
            gvDetails.DataSource = null;
            gvDetails.DataBind();
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" },
                    new string[] { "25", ddlOffice.SelectedValue.ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmployee()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("All", "0"));
				ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtsms.Text.Trim() != "")
            {
                string MobileNo = string.Empty;
                string ToUserName = string.Empty; 
                foreach (GridViewRow gvrow in gvDetails.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    if (chk != null & chk.Checked)
                    {
                        MobileNo += gvrow.Cells[3].Text + ",";
                        ToUserName += gvrow.Cells[2].Text + ",";
                    }
                }
                MobileNo += txtrecipient.Text;
                ToUserName += txtrecipient.Text;
                if (MobileNo != "")
                {
					 ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    //string link = "http://sms.mssinfotech.in/api/mt/SendSMS?APIKey=m22P25e3z0200zV7doXm9B&senderid=MPAGRO&channel=trans&DCS=0&flashsms=0&number=" + MobileNo + "&text=" + txtsms.Text + "&route=8";


                  //  string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=3597C1493C124F&type=unicode&routeid=288&contacts=" + MobileNo + "&senderid=MPSCDF&msg=" + Server.UrlEncode(txtsms.Text);

					string link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + MobileNo + "&text=" + txtsms.Text + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162323408894504&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=0&camp_name=mpmilk_user";

                    //string link = "http://sms.mssinfotech.in/api/mt/SendSMS?APIKey=m22P25e3z0200zV7doXm9B&senderid=MPAGRO&channel=trans&DCS=0&flashsms=0&number=" + MobileNo + "&text=" + txtsms.Text + "&route=8";
                    HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    Stream stream = response.GetResponseStream();

                    /*******************/

                    ds = objdb.ByProcedure("SpSMSBuilder",
                           new string[] { "flag", "SmsSentTo", "SmsSentToList", "SmsType", "SmsContent", "SmsSentBy" },
                           new string[] { "1", MobileNo, ToUserName, "SMSBUILDER", txtsms.Text.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    /*******************/


                    txtrecipient.Text = "";
                    txtsms.Text = "";
                    //FillDetail();
                   
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "SMS Sent Successfully.");
                }
                else
                {
                    string msg = "Please Enter atleast one Mobile No or Select one Bank from below Table!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert(''" + msg + ");", true);
                }

            }
            else
            {
                string msg = "Please Enter a Message then click on Send SMS !";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Sendsms.aspx");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}