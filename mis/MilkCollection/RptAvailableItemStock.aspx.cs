using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_RptAvailableItemStock : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillSociety();
                GetItemCategory();
                //txtFdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");


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

    protected void FillSociety()
    {
        try
        {

            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSociety.DataTextField = "Office_Name";
                    ddlSociety.DataValueField = "Office_ID";
                    ddlSociety.DataSource = ds;
                    ddlSociety.DataBind();
                    ddlSociety.Enabled = false;

                }
                else
                {
                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillGrid();

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
            btnExport.Visible = false;
            btnPrint.Visible = false;
            gvReport.DataSource = string.Empty;
            gvReport.DataBind();

            ds = objdb.ByProcedure("USP_Trn_LocalSaleDetail_Dcs", new string[] { "flag", "ItemType_id", "ItemCat_id", "Office_ID" }, new string[] { "10", ddlitemtype.SelectedValue, ddlitemcategory.SelectedValue, ddlSociety.SelectedValue }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    btnExport.Visible = true;
                    btnPrint.Visible = true;
                    gvReport.DataSource = ds;
                    gvReport.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetItemCategory()
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_MstLocalSale",
                         new string[] { "flag" },
                         new string[] { "4" }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlitemcategory.DataTextField = "ItemCatName";
                        ddlitemcategory.DataValueField = "ItemCat_id";
                        ddlitemcategory.DataSource = ds;
                        ddlitemcategory.DataBind();
                        ddlitemcategory.Items.Insert(0, new ListItem("All", "0"));
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void ddlitemcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlitemcategory.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Sp_MstLocalSale",
                             new string[] { "flag", "ItemCat_id" },
                             new string[] { "5", ddlitemcategory.SelectedValue }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlitemtype.DataTextField = "ItemTypeName";
                            ddlitemtype.DataValueField = "ItemType_id";
                            ddlitemtype.DataSource = ds;
                            ddlitemtype.DataBind();
                            ddlitemtype.Items.Insert(0, new ListItem("All", "0"));
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Type Not Exist For Category - " + ddlitemcategory.SelectedItem.Text);
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Type Not Exist For Category - " + ddlitemcategory.SelectedItem.Text);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Item Type Not Exist For Category - " + ddlitemcategory.SelectedItem.Text);
                }
            }
            else
            {

                ddlitemtype.Items.Clear();
                ddlitemtype.Items.Insert(0, new ListItem("All", "0"));
               

            }
            Session["event_control"] = ddlitemtype;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlitemcategory_Init(object sender, EventArgs e)
    {
        try
        {
            ddlitemcategory.Items.Add(new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlitemtype_Init(object sender, EventArgs e)
    {
        try
        {
            ddlitemtype.Items.Add(new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = ddlSociety.SelectedItem.Text + "_" + "AvailableItem_Report";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvReport.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
   
}