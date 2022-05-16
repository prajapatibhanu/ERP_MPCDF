using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Mis_Reports_FRM_MilkCosume_ProductMarketing : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (objdb.createdBy() != null && objdb.Office_ID() != null)
            {
                if (!Page.IsPostBack)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    Year();


                }
            }
            else
            {
                objdb.redirectToHome();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Year()
    {
        try
        {
            for (int i = 2020; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }
            ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
        ds.Clear();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("USP_MIS_Trn_MilkConsumedInIdigneosProductMaking",
                        new string[] { "flag", "Year", "Month" },
                         new string[] { "2", ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;


                    decimal BSDS = Convert.ToDecimal(ds.Tables[0].Rows[0]["BSDS"].ToString());
                    decimal GSDS = Convert.ToDecimal(ds.Tables[0].Rows[0]["GSDS"].ToString());
                    decimal ISDS = Convert.ToDecimal(ds.Tables[0].Rows[0]["ISDS"].ToString());
                    decimal JSDS = Convert.ToDecimal(ds.Tables[0].Rows[0]["JSDS"].ToString());
                    decimal USDS = Convert.ToDecimal(ds.Tables[0].Rows[0]["USDS"].ToString());
                    decimal BKDS = Convert.ToDecimal(ds.Tables[0].Rows[0]["BKDS"].ToString());
                    decimal Total = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total"].ToString());

                    BSDS = ds.Tables[0].AsEnumerable().Sum(row => BSDS);
                    GSDS = ds.Tables[0].AsEnumerable().Sum(row => GSDS);
                    ISDS = ds.Tables[0].AsEnumerable().Sum(row => ISDS);
                    JSDS = ds.Tables[0].AsEnumerable().Sum(row => JSDS);
                    USDS = ds.Tables[0].AsEnumerable().Sum(row => USDS);
                    BKDS = ds.Tables[0].AsEnumerable().Sum(row => BKDS);
                    Total = ds.Tables[0].AsEnumerable().Sum(row => Total);

                    GridView1.FooterRow.Cells[0].Text = "<b> TOTAL </b>";
                    GridView1.FooterRow.Cells[1].Text = "<b>" + BSDS.ToString() + "</b>";
                    GridView1.FooterRow.Cells[2].Text = "<b>" + GSDS.ToString() + "</b>";
                    GridView1.FooterRow.Cells[3].Text = "<b>" + ISDS.ToString() + "</b>";
                    GridView1.FooterRow.Cells[4].Text = "<b>" + JSDS.ToString() + "</b>";
                    GridView1.FooterRow.Cells[5].Text = "<b>" + USDS.ToString() + "</b>";
                    GridView1.FooterRow.Cells[6].Text = "<b>" + BKDS.ToString() + "</b>";
                    GridView1.FooterRow.Cells[7].Text = "<b>" + Total.ToString() + "</b>";

                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        ds.Clear();
    }

}

