using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_MilkCollection_rpt_SocietywiseMilkCollection : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {
            try
            {
                if (rbMilkCollection.SelectedValue == "1")
                {
                    ds = null;
                    ds = objdb.ByProcedure("Sp_RptDCSMilkCollection",
                                         new string[] { "flag", "OfficeId", "FromDate", "ToDate" },
                                         new string[] { "2", ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtFromDate.Text.Trim(), cult).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtToDate.Text.Trim(), cult).ToString("dd/MM/yyyy") }, "dataset");
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            grdTotalMilk.DataSource = ds.Tables[0];
                            grdTotalMilk.DataBind();
                            pnlTotal.Visible = true;
                            pnlproducer.Visible = false;                            
                        }
                    }
                }
                else if (rbMilkCollection.SelectedValue == "2")
                {
                    ds = null;
                    ds = objdb.ByProcedure("Sp_RptDCSMilkCollection",
                                         new string[] { "flag", "OfficeId", "FromDate" },
                                         new string[] { "1", ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtCollectionDate.Text.Trim(), cult).ToString("dd/MM/yyyy") }, "dataset");
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gvMilkCollection.DataSource = ds.Tables[0];
                            gvMilkCollection.DataBind();
                            pnlTotal.Visible = false;
                            pnlproducer.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }

    protected void rbMilkCollection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbMilkCollection.SelectedValue == "1")
        {
            pnlSamiti.Visible = true;
            pnlUtpadak.Visible = false;
            ds = null;
            grdTotalMilk.DataSource = ds;
            grdTotalMilk.DataBind();
            gvMilkCollection.DataSource = ds;
            gvMilkCollection.DataBind();
            pnlproducer.Visible = false;
            pnlTotal.Visible = false;
            txtCollectionDate.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
        }
        else
        {
            pnlSamiti.Visible = false;
            pnlUtpadak.Visible = true;
            grdTotalMilk.DataSource = ds;
            grdTotalMilk.DataBind();
            gvMilkCollection.DataSource = ds;
            gvMilkCollection.DataBind();
            pnlproducer.Visible = false;
            pnlTotal.Visible = false;
            txtCollectionDate.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
        }
    }

}