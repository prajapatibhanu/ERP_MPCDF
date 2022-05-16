using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_filetracking_FTFileMovementCustom : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                //FillGrid();
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
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            DataSet ds1 = objdb.ByProcedure("SpFTForwardFile", new string[] { "flag", "FromDate", "ToDate" }, new string[] { "14", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
				GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }

            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
}