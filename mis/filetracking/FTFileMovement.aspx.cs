using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_filetracking_SearchFile : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    divfiledetail.Visible = false;
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
    protected void btnSearch_Click(object sender, EventArgs e)
     {
        try
        {
            lblMsg2.Text = "";
            lblMsg.Text = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpFTForwardFile",
                new string[] { "flag", "File_No", "QRCode", "Office_ID" },
                new string[] { "13", txtFileNumber.Text, txtFileNumber.Text, ViewState["Office_ID"].ToString() }, "dataset");      
            if (ds.Tables[0].Rows.Count != 0)
            {
                divfiledetail.Visible = true;
                Gvfiledetail.DataSource = ds.Tables[0];
                Gvfiledetail.DataBind();

                GridView1.DataSource = ds.Tables[1];
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                divfiledetail.Visible = false;
                lblMsg2.Text = "No File/Letter No. is On Desk and forwarded.";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
}