using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_RTI_RTIOfficerForms_RequestedRTI : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["App_ID"] = "";
                    ViewState["RequestType"] = "RTI Request";
                    ViewState["RTI_ID"] = "";
                    ViewState["RTI_RequestDoc"] = "";
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                   //DetailDiv.Visible = false;
                    Fillgrid();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
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
            GridView1.DataSource = new string[] { };
            string fromDate = "", ToDate = "";
            if (txtFromDate.Text != "")
            {
                fromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy-MM-dd");
            }
            if (txtToDate.Text != "")
            {
                ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy-MM-dd");
            }
            ds = objdb.ByProcedure("SpRtiReplyDetail",
                 new string[] { "flag", "RTI_RequestType", "RTI_ByOfficeID", "startDate", "endDate" },
                 new string[] { "10", ViewState["RequestType"].ToString(), ViewState["Office_ID"].ToString(), fromDate, ToDate }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                
            }
            //else
            //{
            //    lblMsg.Text = "There Is No RTI Detail Available.";
            //    lblMsg.Style.Add("color", "Red");
            //    lblMsg.Style.Add("font-size", "16px");
                

            //}
            GridView1.DataBind();
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
            foreach (GridViewRow row in GridView1.Rows)
            {
                LinkButton lnk = (LinkButton)row.FindControl("lnkEditModal");
                Label lbl = (Label)row.FindControl("lblCntBeEdit");
                string RTI_ID = lnk.ToolTip.ToString();
                DataSet dscheck = objdb.ByProcedure("SpRtiReqDetail", new string[] { "flag", "RTI_ID" }, new string[] { "19", RTI_ID }, "dataset");
                if(dscheck != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (dscheck.Tables[0].Rows[0]["ReplyStatus"].ToString() == "False")
                    {
                        lnk.Visible = false;
                        lbl.Visible = true;
                    }
                    else
                    {
                        lnk.Visible = true;
                        lbl.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkEditModal_Click(object sender, EventArgs e)
    {
        lblModal.Text = "";
        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        LinkButton lnk = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkEditModal");
        ViewState["RTI_ID"] = lnk.ToolTip.ToString(); 
        ds = objdb.ByProcedure("SpRtiReqDetail",
                           new string[] { "flag", "RTI_ID" },
                                new string[] { "18", ViewState["RTI_ID"].ToString() }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtRTI_Subject.Text = ds.Tables[0].Rows[0]["RTI_Subject"].ToString();
            txtRTI_Request.Text = ds.Tables[0].Rows[0]["RTI_Request"].ToString();
            string RTI_RequestDoc = ds.Tables[0].Rows[0]["RTI_RequestDoc"].ToString();
            ViewState["RTI_RequestDoc"] = RTI_RequestDoc;
            if (RTI_RequestDoc != "")
            {
                hyprRTI_RequestDoc.Visible = true;
                hyprRTI_RequestDoc.NavigateUrl = "~/mis/RTI/RTI_Docs/" + RTI_RequestDoc;
            }
            else
            {
                hyprRTI_RequestDoc.Visible = false;
                hyprRTI_RequestDoc.NavigateUrl = "";
            }
            
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);
        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        GridView1.UseAccessibleHeader = true;
    }
    protected void btnEditRTI_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtRTI_Subject.Text.Trim() == "")
            {
                msg += "Enter Subject for RTI Request<br/>";
            }
            if (txtRTI_Request.Text.Trim() == "")
            {
                msg += "Enter RTI Request<br/>";
            }
            if (msg == "")
            {
                string SupportingDoc = "";

                if (fuRTI_RequestDoc.HasFile)
                {
                    SupportingDoc = Guid.NewGuid() + "-" + fuRTI_RequestDoc.FileName;
                }
                else if (ViewState["RTI_RequestDoc"] !="")
                {
                    SupportingDoc = ViewState["RTI_RequestDoc"].ToString();
                }
                else
                {
                    SupportingDoc = "";
                }
                objdb.ByProcedure("SpRtiReqDetail",
                               new string[] { "flag", "RTI_ID", "RTI_Subject", "RTI_Request", "RTI_RequestDoc" },
                                    new string[] { "17", ViewState["RTI_ID"].ToString(), txtRTI_Subject.Text.Trim(), txtRTI_Request.Text.Trim(), SupportingDoc }, "dataset");

                if (fuRTI_RequestDoc.HasFile)
                {
                    fuRTI_RequestDoc.PostedFile.SaveAs(Server.MapPath("~/mis/RTI/RTI_Docs/" + SupportingDoc));
                }
                lblModal.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                Fillgrid();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);
            }
            
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void FillEditDetailOfRTI()
    {
        try
        {

        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["RTI_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpRtiReqDetail",
                               new string[] { "flag", "RTI_ID" },
                                    new string[] { "18", ViewState["RTI_ID"].ToString() }, "dataset");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            Fillgrid();
        }
        catch (Exception)
        {

            throw;
        }
    }
}