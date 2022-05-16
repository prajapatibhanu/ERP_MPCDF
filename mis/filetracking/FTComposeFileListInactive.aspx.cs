using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_filetracking_FTComposeFileListInactive : System.Web.UI.Page
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
                    FillGrid();
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
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpFTForwardFile",
                        new string[] { "flag", "Emp_ID" },
                        new string[] { "16", ViewState["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                Visibility();
            }
            else
            {
                lblMsg2.Text = "There is no File on my Desk...";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Visibility()
    {
        try
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                LinkButton lnkEdit = (LinkButton)row.FindControl("LinkButton2");
                string File_ID = lnkEdit.ToolTip.ToString();
                ds = objdb.ByProcedure("SpFTComposeFile",
                      new string[] { "flag", "File_ID" },
                      new string[] { "11", File_ID }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0]["ForwardCount"].ToString() == "0")
                    {
                        lnkEdit.Visible = true;
                    }
                    else
                    {
                        lnkEdit.Visible = false;
                    }
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
        try
        {
            string File_ID = GridView1.SelectedValue.ToString();
            if (ViewState["Edit"] == "")
            {
                ds = objdb.ByProcedure("SpFTComposeFile",
                       new string[] { "flag", "File_ID" },
                       new string[] { "12", File_ID }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0]["StatusOfFile"].ToString() == "Inward")
                    {
                        Response.Redirect("FTInward.aspx?File_ID=" + objdb.Encrypt(File_ID));
                    }
                    else
                    {
                        Response.Redirect("FTComposeFile.aspx?File_ID=" + objdb.Encrypt(File_ID));
                    }
                }
            }
            else
            {
                ds = objdb.ByProcedure("SpFTForwardFile",
                            new string[] { "flag", "File_ID" },
                            new string[] { "5", File_ID }, "dataset");
                string Forwarded_ID = ds.Tables[0].Rows[0]["Forwarded_ID"].ToString();
                Response.Redirect("ViewFileStatus.aspx?Forwarded_ID=" + objdb.Encrypt(Forwarded_ID) + "&File_ID=" + objdb.Encrypt(File_ID));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Edit"] = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}