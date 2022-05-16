using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;


public partial class mis_Masters_DSTankerShifting : System.Web.UI.Page
{
    DataSet ds, ds1, ds2, ds3, ds4;
    static DataSet ds5;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            lblMsg.Text = "";

            if (!Page.IsPostBack)
            {

                FillOffice();
                FillTanker();
                FillGrid();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlFromds.DataSource = ds.Tables[0];
                ddlFromds.DataTextField = "Office_Name";
                ddlFromds.DataValueField = "Office_ID";
                ddlFromds.DataBind();
                ddlFromds.Items.Insert(0, new ListItem("Select", "0"));
                ddlFromds.SelectedValue = objdb.Office_ID();
                ddlFromds.Enabled = false;

                ddlTods.DataSource = ds.Tables[0];
                ddlTods.DataTextField = "Office_Name";
                ddlTods.DataValueField = "Office_ID";
                ddlTods.DataBind();
                ddlTods.Items.Insert(0, new ListItem("Select", "0"));
                
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillTanker()
    {
        try
        {
           // lblMsg.Text = "";
            ds = objdb.ByProcedure("Usp_TankerDetail",
                new string[] { "flag", "I_OfficeID" },
                new string[] { "8", objdb.Office_ID() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlV_VehicleNo.DataSource = ds.Tables[0];
                ddlV_VehicleNo.DataTextField = "V_VehicleNo";
                ddlV_VehicleNo.DataValueField = "I_TankerID";
                ddlV_VehicleNo.DataBind();
                ddlV_VehicleNo.Items.Insert(0, new ListItem("Select", "0"));
               

            }
            else
            {

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
            ds = objdb.ByProcedure("Usp_TankerDetail", new string[] { "flag", "I_TankerID", "I_OfficeID", "CreatedBy" }, new string[] {"9",ddlV_VehicleNo.SelectedValue,ddlTods.SelectedValue,objdb.createdBy() }, "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    
                    string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());
                    ddlTods.ClearSelection();
                    FillTanker();
                    FillGrid();
                   
                }
                else
                {
                    string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Success.ToString());
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
            gvTankerHistory.DataSource = string.Empty;
            gvTankerHistory.DataBind();
            ds = objdb.ByProcedure("Usp_TankerDetail", new string[] { "flag", "I_OfficeID" }, new string[] { "10", objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvTankerHistory.DataSource = ds;
                    gvTankerHistory.DataBind();
                }
                
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string ExcelName = "Tanker Transfer List";

            string FileName = ExcelName + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            if (gvTankerHistory.Rows.Count > 0)
            {
                gvTankerHistory.GridLines = GridLines.Both;
                gvTankerHistory.HeaderStyle.Font.Bold = true;
                gvTankerHistory.RenderControl(htmltextwrtter);
            }


            Response.Write(strwritter.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
}