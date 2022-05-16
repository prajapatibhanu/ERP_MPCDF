using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RTI_UFileRTI : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["App_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["App_ID"] = Session["App_ID"].ToString();
                    Fillgrid();
                    //DetailDiv.Visible = false;
                    Session["RequestType"] = "";

                }
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
            ds = objdb.ByProcedure("SpRtiReqDetail", new string[] { "flag", "App_ID" }, new string[] { "7", ViewState["App_ID"].ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                lblMsg.Text = "No RTI Filed.";
                lblMsg.Style.Add("color", "Red");
                lblMsg.Style.Add("font-size", "16px");
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string RTI_ID = GridView1.SelectedDataKey.Value.ToString();

    //        Response.Redirect("RTIDetails.aspx ?RTI_ID=" + RTI_ID);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
}