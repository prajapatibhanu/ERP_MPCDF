using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_RTI_ListOfAllFirstApp : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["App_ID"] = "";
                    ViewState["RequestType"] = "First Appeal Request";
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    Fillgrid();
                    //DetailDiv.Visible = false;
                }
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
            ds = objdb.ByProcedure("SpRtiReqDetail", new string[] { "flag" }, new string[] { "20" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeID.DataSource = ds;
                ddlOfficeID.DataTextField = "Office_Name";
                ddlOfficeID.DataValueField = "Office_ID";
                ddlOfficeID.DataBind();
                ddlOfficeID.Items.Insert(0, new ListItem("All", "0"));
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlOfficeID.SelectedValue = ViewState["Office_ID"].ToString();
                    ddlOfficeID.Enabled = true;
                }
                else
                {
                    ddlOfficeID.SelectedValue = ViewState["Office_ID"].ToString();
                    ddlOfficeID.Enabled = false;
                }
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Fillgrid()
    {
        try
        {
            string fromDate = "", ToDate = "";
            if (txtFromDate.Text != "")
            {
                fromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy-MM-dd");
            }
            if (txtToDate.Text != "")
            {
                ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy-MM-dd");
            }
            string flag = "";
            if (ddlOfficeID.SelectedValue.ToString() == "0")
            {
                flag = "15";
            }
            else
            {
                flag = "6";
            }
            GridView1.DataSource = new string[]{};
            ds = objdb.ByProcedure("SpRtiReplyDetail"
              , new string[] { "flag", "RTI_RequestType", "RTI_ByOfficeID", "startDate", "endDate" }
              , new string[] { flag, ViewState["RequestType"].ToString(), ddlOfficeID.SelectedValue, fromDate, ToDate }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                
            }
            //else
            //{
            //    lblMsg.Text = "There Is No First Appeal RTI Detail Available.";
            //    lblMsg.Style.Add("color", "Red");
            //    lblMsg.Style.Add("font-size", "16px");
            //    GridView1.DataSource = null;
            //    GridView1.DataBind();
            //}
            GridView1.DataBind();
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void ddlOfficeID_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Fillgrid();
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            Fillgrid();
        }
        catch (Exception)
        {

            throw;
        }
    }

}