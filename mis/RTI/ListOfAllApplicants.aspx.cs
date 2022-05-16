using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_RTI_ListOfAllApplicants : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["App_ID"] = "";
                    ViewState["RequestType"] = "RTI Request";
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    Fillgrid();
                    //DetailDiv.Visible = false;
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
            string flag = "";
            if (ddlOfficeID.SelectedValue.ToString() == "0")
            {
                flag = "23";
            }
            else
            {
                flag = "18";
            }
            GridView1.DataSource = new string[]{};
            ds = objdb.ByProcedure("SpApplicantDetail"
                , new string[] { "flag", "RTI_ByOfficeID" }
                , new string[] { flag, ddlOfficeID.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                
            }
            //else
            //{
            //    lblMsg.Text = "There Is No RTI Detail Available.";
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
    protected void ddlOfficeID_SelectedIndexChanged(object sender, EventArgs e)
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