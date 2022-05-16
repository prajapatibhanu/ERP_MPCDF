using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Security.Cryptography;

public partial class mis_RTI_FrwdRTIs : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["App_ID"] = "";
                   // ViewState["RequestType"] = "RTI Request";
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
            ds = objdb.ByProcedure("SpRtiAddChat", new string[] { "flag", "Emp_ID" }, new string[] { "9", ViewState["Emp_ID"].ToString() }, "dataset");

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
}