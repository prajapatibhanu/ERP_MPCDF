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
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpFTComposeFile",
                new string[] { "flag", "Emp_ID", "File_No", "QRCode", "Office_ID" },
                new string[] { "7", ViewState["Emp_ID"].ToString(), txtFileNumber.Text, txtFileNumber.Text, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblMsg2.Text = "No File/Letter No. is On My Desk and forwarded.";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string SearchFileId = GridView1.SelectedValue.ToString();
        Response.Redirect("ViewFileStatus.aspx?SearchFileId=" + objdb.Encrypt(SearchFileId));
    }
}