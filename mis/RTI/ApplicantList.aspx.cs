using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_RTI_ApplicantList : System.Web.UI.Page
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
    private void Fillgrid()
    {
        try
        {
            GridView1.DataSource = new string[]{};
            ds = objdb.ByProcedure("SpApplicantDetail"
                , new string[] { "flag", "RTI_ByOfficeID" }
                , new string[] { "18", ViewState["Office_ID"].ToString()}, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
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
}