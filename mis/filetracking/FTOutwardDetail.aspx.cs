using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_filetracking_FTOutwardDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Department_ID"] = Session["Department_ID"].ToString();
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
            ds = objdb.ByProcedure("SpFTOutwardFiles",
                new string[] { "flag", "Outward_Updatedby" },
                new string[] { "1", ViewState["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                lblMsg2.Text = "There is No Outward Letter...";
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
            string Outward_ID = GridView1.SelectedDataKey.Value.ToString();
            if (ViewState["Edit"] == "")
            {
                Response.Redirect("FTOutward.aspx?Outward_ID=" + objdb.Encrypt(Outward_ID));
            }
            else
            {
                ds = objdb.ByProcedure("SpFTOutwardFiles",
                            new string[] { "flag", "Outward_ID" },
                            new string[] { "2", Outward_ID }, "dataset");
                if (ds.Tables[0].Rows.Count != 0 && ds.Tables[1].Rows.Count != 0)
                {
                    DetailsView1.DataSource = ds;
                    DetailsView1.DataBind();

                    GridView2.DataSource = ds.Tables[1];
                    GridView2.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
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