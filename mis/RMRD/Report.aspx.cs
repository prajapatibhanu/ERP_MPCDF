using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;

public partial class mis_RMRD_Report : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    IFormatProvider cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            lblMsg.Text = "";

            if (!Page.IsPostBack)
            {
                txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                FillBMCRoot();
               
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    public void FillBMCRoot()
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                      new string[] { "flag" },
                      new string[] { "1"}, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
                ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
                ddlBMCTankerRootName.DataSource = ds;
                ddlBMCTankerRootName.DataBind();
                //ddlBMCTankerRootName.Items.Insert(0, "Select");
                ddlBMCTankerRootName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            divreport.InnerHtml = "";
            lblMsg.Text = "";
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD", new string[] { "flag","Date","BMCTankerRoot_Id" }, new string[] {"18",Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),ddlBMCTankerRootName.SelectedValue }, "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table class='table table-bordered'>");
                sb.Append("<tr>");
                sb.Append("<td colspan ='9'><p><b>BMC&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ddlBMCTankerRootName.SelectedItem.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tan.No.&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["V_VehicleNo"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Time - " + ds.Tables[0].Rows[0]["DT_TankerArrivalTime"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date - " + ds.Tables[0].Rows[0]["DT_TankerArrivalDate"].ToString() + "</b></p>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>S.No</th>");
                sb.Append("<th>Name</th>");
                sb.Append("<th>Temp</th>");
                sb.Append("<th>Qty</th>");
                sb.Append("<th>Fat %</th>");
                sb.Append("<th>CLR</th>");
                sb.Append("<th>SNF %</th>");
                sb.Append("<th>Kg Fat</th>");
                sb.Append("<th>Kg SNF</th>");
                sb.Append("</tr>");
                int count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
			    {
                    sb.Append("<tr>");
                    sb.Append("<td>"+(i + 1)+"</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["V_Temp"].ToString() + "° C</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["I_MilkQuantity"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["D_FAT"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["D_CLR"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["D_SNF"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["FATInKg"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["SNFInKg"].ToString() + "</td>");
                    sb.Append("</tr>");
			    }
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td><b>Received</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td><b>" + ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("I_MilkQuantity")) + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td><b>" + ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("D_SNF")) + "</b></td>");
                sb.Append("<td><b>" + ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FATInKg")) + "</b></td>");
                sb.Append("<td><b>" + ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SNFInKg")) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                divreport.InnerHtml = sb.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
}