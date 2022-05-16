using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_filetracking_FTComposeFileList : System.Web.UI.Page
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
                    ds = objdb.ByProcedure("SpFTForwardFile",
                        new string[] { "flag", "Emp_ID" },
                        new string[] { "3", ViewState["Emp_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                        GridView1.UseAccessibleHeader = true;
                    }
                    else
                    {
                        lblMsg2.Text = "There is no File on my Desk...";
                    }
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string File_ID = GridView1.SelectedValue.ToString();
        ds = objdb.ByProcedure("SpFTForwardFile",
                        new string[] { "flag", "File_ID" },
                        new string[] { "5", File_ID }, "dataset");
        string Forwarded_ID = ds.Tables[0].Rows[0]["Forwarded_ID"].ToString();
        Response.Redirect("ViewFileStatus.aspx?Forwarded_ID=" + objdb.Encrypt(Forwarded_ID) + "&File_ID=" + objdb.Encrypt(File_ID));
    }
}