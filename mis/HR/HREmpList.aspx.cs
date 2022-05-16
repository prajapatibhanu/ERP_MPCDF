using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class mis_HR_HREmpList : System.Web.UI.Page
{
    DataSet ds;
    string flag;
    // AbstApiDBApi objdb = new APIProcedure();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    flag = "22";
                    FillList(flag);
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
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlAdminOffice_ID.DataSource = ds;
                ddlAdminOffice_ID.DataTextField = "Office_Name";
                ddlAdminOffice_ID.DataValueField = "Office_ID";
                ddlAdminOffice_ID.DataBind();
                //ddlAdminOffice_ID.Items.Insert(0, new ListItem("All", "0"));
                ddlAdminOffice_ID.SelectedValue = ViewState["Office_ID"].ToString();
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlAdminOffice_ID.Enabled = true;
                }
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillList(string flag)
    {
        try
        {
            //Repeater1.DataSource = null;
            //Repeater1.DataBind();
            ds = null;
            //ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] {flag, ddlAdminOffice_ID.SelectedValue }, "dataset");
            //int rowcount = ds.Tables[0].Rows.Count;
            //lblNoOfEmployee.Text = rowcount.ToString();
            //if(ds != null &&  rowcount> 0)
            //{
            //    Repeater1.DataSource = ds;
            //    Repeater1.DataBind();
            //}
            //else 
            //{
            //    Repeater1.DataSource = null;
            //    Repeater1.DataBind();
            //    lblMsg.Text = "No Record Found.";
            //}

            /***************/
            ds = null;
            StringBuilder sb = new StringBuilder();
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { flag, ddlAdminOffice_ID.SelectedValue }, "dataset");
            int newrowcount = ds.Tables[0].Rows.Count;
            lblNoOfEmployee.Text = newrowcount.ToString();
            if (ds != null && newrowcount > 0)
            {
                for (int i = 0; i < newrowcount; i++)
                {
                    if (i % 6 == 0 || i==0)
                    {
                        sb.Append("<div class='row'>");
                    }
                    //string Emp_id_enc = objdb.Encrypt(ds.Tables[0].Rows[i]["Emp_ID"].ToString());
                    sb.Append("<div class='col-md-2 col-sm-6'><div class='form-group'><div class='users'>");
                    sb.Append("<img src='" + ds.Tables[0].Rows[i]["Emp_ProfileImage"].ToString() + "' alt='User Image' style='height: 75px; width: 75px;' />");

                    sb.Append("<a class='users-list-name' href='HREmpDetailView.aspx?Emp_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Emp_ID"].ToString()) + "' target='_blank'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</a>");

                    sb.Append("<span class='users-list-date clearfix' style='color: CadetBlue;'>" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + "</span>");

                    sb.Append("<span class='clearfix' Style='color: CornflowerBlue;' ToolTip='" + ds.Tables[0].Rows[i]["Dept"].ToString() + "'>" + ds.Tables[0].Rows[i]["Department_Name"].ToString() + "</span>");

                    sb.Append("<small><span class='clearfix' Style='color: tomato;' ToolTip='" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</span></small>");

                    sb.Append("<a class='users-list-date clearfix' style='color: CornflowerBlue;' href='HREmpServiceBookDetail.aspx?Emp_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Emp_ID"].ToString()) + "' target='_blank'>[Service Book]</a>");

                    sb.Append("</div></div></div>");
                    if (i % 6 == 5)
                    {
                        sb.Append("</div>");
                    }

                }

                divFillList.InnerHtml = sb.ToString();
            }
            /**************/




        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void ddlAdminOffice_ID_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        flag = "22";
    //        FillList(flag);
    //        lnkViewAllEmployee.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void lnkViewAllEmployee_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            flag = "23";
            FillList(flag);
            lnkViewAllEmployee.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            flag = "22";
            FillList(flag);
            lnkViewAllEmployee.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}