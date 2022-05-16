using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;
public partial class mis_dailyplan_ETPTesting : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }
                lblMsg.Text = "";
                lblRecordMsg.Text = "";
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtEntryDate.Attributes.Add("readonly", "readonly");
                FillGrid();
               
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblRecordMsg.Text = "";
            string IsActive = "1";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                if(btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("Usp_ProductionETPTesting", new string[] 
                                           {"flag",
                                           "EntryDate",  
                                           "Office_ID",  
                                           "Source",  
                                           "Ph",  
                                           "SuspendedSolid",  
                                           "OilandGrease",  
                                           "COD",                                             
                                           "Remark",  
                                           "IsActive",  
                                           "CreatedAt",  
                                           "CreatedBy",  
                                           "CreatedByIP"
                                            },
                                            new string[]
                                            {"0",
                                             Convert.ToDateTime(txtEntryDate.Text,cult).ToString("yyyy/MM/dd"),
                                             objdb.Office_ID(),
                                             txtSource.Text,
                                             txtPh.Text,
                                             txtSuspendedSolid.Text,
                                             txtOilandGrease.Text,
                                             txtCOD.Text,
                                             txtRemark.Text,
                                             IsActive,
                                             objdb.Office_ID(),
                                             objdb.createdBy(),
                                             objdb.GetLocalIPAddress()
                                            }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());

                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                           
                        }


                    }
                    
                }
                else if (btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("Usp_ProductionETPTesting", new string[] 
                                           {"flag",
                                           "ETPTestingID",   
                                           "Source",  
                                           "Ph",  
                                           "SuspendedSolid",  
                                           "OilandGrease",  
                                           "COD",
                                           "InitialDO",
                                           "FinalDO",
                                           "BOD",
                                           "Remark",   
                                           "CreatedAt",  
                                           "CreatedBy",  
                                           "CreatedByIP"
                                            },
                                            new string[]
                                            {"4",                                            
                                             ViewState["ETPTestingID"].ToString(),
                                             txtSource.Text,
                                             txtPh.Text,
                                             txtSuspendedSolid.Text,
                                             txtOilandGrease.Text,
                                             txtCOD.Text,
                                             txtinitialDO.Text,
                                             txtFinalDO.Text,
                                             txtBOD.Text,
                                             txtRemark.Text,
                                             objdb.Office_ID(),
                                             objdb.createdBy(),
                                             objdb.GetLocalIPAddress()
                                            }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());

                        }                       
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());

                        }


                    }

                }
                rfvFinalDO.Enabled = false;
                rfvinitialDO.Enabled = false;
                rfvBOD.Enabled = false;
                FillGrid();
                ClearText();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        
        txtEntryDate.Enabled = false;
        txtSource.Text = "";
        txtPh.Text = "";
        txtSuspendedSolid.Text = "";
        txtOilandGrease.Text = "";
        txtCOD.Text = "";
        txtinitialDO.Text = "";
        txtFinalDO.Text = "";
        txtBOD.Text = "";
        txtRemark.Text = "";
        txtEntryDate.Enabled = true;
        txtinitialDO.Enabled = false;
        txtFinalDO.Enabled = false;
        txtBOD.Enabled = false;
    }
    protected void FillGrid()
    {
        try
        {
            lblRecordMsg.Text = "";
            btnExcel.Visible = false;
            //ds = objdb.ByProcedure("Usp_ProductionETPTesting", new string[] { "flag","EntryDate","Office_ID" }, new string[] {"2",Convert.ToDateTime(txtEntryDate.Text,cult).ToString("yyyy/MM/dd"),objdb.Office_ID() }, "dataset");
            ds = objdb.ByProcedure("Usp_ProductionETPTesting", new string[] { "flag","Office_ID" }, new string[] { "2", objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    btnExcel.Visible = true;
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                }
                else
                {
                    gvDetails.DataSource = string.Empty;
                    gvDetails.DataBind();
                }
            }
            else
            {
                gvDetails.DataSource = string.Empty;
                gvDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblRecordMsg.Text = "";
            string ETPTestingID = e.CommandArgument.ToString();
            ViewState["ETPTestingID"] = ETPTestingID;
            if(e.CommandName == "EditRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblDate = (Label)row.FindControl("lblDate");
                Label lblSource = (Label)row.FindControl("lblSource");
                Label lblPh = (Label)row.FindControl("lblPh");
                Label lblSuspendedSolid = (Label)row.FindControl("lblSuspendedSolid");
                Label lblOilandGrease = (Label)row.FindControl("lblOilandGrease");
                Label lblCOD = (Label)row.FindControl("lblCOD");
                Label lblInitialDO = (Label)row.FindControl("lblInitialDO");
                Label lblFinalDO = (Label)row.FindControl("lblFinalDO");
                Label lblBOD = (Label)row.FindControl("lblBOD");
                Label lblRemark = (Label)row.FindControl("lblRemark");
                Label lblDateDiff = (Label)row.FindControl("lblDateDiff");
                txtEntryDate.Text = lblDate.Text;
                txtEntryDate.Enabled = false;
                txtSource.Text = lblSource.Text;
                txtPh.Text = lblPh.Text;
                txtSuspendedSolid.Text = lblSuspendedSolid.Text;
                txtOilandGrease.Text = lblOilandGrease.Text;
                txtCOD.Text = lblCOD.Text;
                txtinitialDO.Text = lblInitialDO.Text;
                txtFinalDO.Text = lblFinalDO.Text;
                txtBOD.Text = lblBOD.Text;
                txtRemark.Text = lblRemark.Text;
                int DateDiff = int.Parse(lblDateDiff.Text);
                if (DateDiff > 2)
                {
                    txtinitialDO.Enabled = true;
                    txtFinalDO.Enabled = true;
                    txtBOD.Enabled = true;
                    rfvFinalDO.Enabled = true;
                    rfvinitialDO.Enabled = true;
                    rfvBOD.Enabled = true;
                }
                else
                {
                    txtinitialDO.Enabled = false;
                    txtFinalDO.Enabled = false;
                    txtBOD.Enabled = false;
                    rfvFinalDO.Enabled = false;
                    rfvinitialDO.Enabled = false;
                    rfvBOD.Enabled = false;
                }
                btnSave.Text = "Update";

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            gvDetails.Columns[11].Visible = false;
            string FileName = "ETPTestingReport";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvDetails.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            gvDetails.Columns[11].Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
}