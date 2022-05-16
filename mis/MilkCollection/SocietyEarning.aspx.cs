using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;


public partial class mis_MilkCollection_SocietyEarning : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        txtDate.Text = Session["date"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }
                if (Session["UpdateSuccess"] != null)
                {
                    if ((Boolean)Session["UpdateSuccess"] == true)
                    {
                        Session["UpdateSuccess"] = false;
                        txtDate.Text = Session["date"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Updated Successfully');", true);
                    }
                }
                if (Session["DeleteSuccess"] != null)
                {
                    if ((Boolean)Session["DeleteSuccess"] == true)
                    {
                        Session["DeleteSuccess"] = false;
                        txtDate.Text = Session["date"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Deleted Successfully');", true);
                    }
                }
                
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillSociety();
                FillProducerName();
                txtDateF.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                //GetViewINVDetails(txtDateF.Text);
                txtDateF.Attributes.Remove("readonly");
                FillGrid();


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
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtsocietyName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    
                }

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
    protected void FillProducerName()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_Id" },
                              new string[] { "3", objdb.Office_ID() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlFarmer.DataTextField = "ProducerName";
                    ddlFarmer.DataValueField = "ProducerId";
                    ddlFarmer.DataSource = ds;
                    ddlFarmer.DataBind();
                    ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                
                lblMsg.Text = "";
               
                    if (btnSave.Text == "Save")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("USP_Trn_LocalEarningDetail_Dcs",
                                                  new string[] { "flag" 
				                                ,"EntryDt"
				                                ,"Office_ID"
				                                ,"ProducerId"
				                                ,"NetAmount"
				                                ,"Remark"
				                                ,"CreatedBy"
				                                ,"CreatedBy_IP"                                               
                                    },
                                                  new string[] { "1"
                                              ,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")
                                              ,objdb.Office_ID()                                             
                                              ,ddlFarmer.SelectedValue
                                              ,txttotalamount.Text
                                              ,txtRemark.Text
                                              ,ViewState["Emp_ID"].ToString()
                                             ,objdb.GetLocalIPAddress()
                                              
                                    },"dataset");
                                               

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            Session["IsSuccess"] = true;
                            Session["date"] = txtDate.Text;
                            Response.Redirect("SocietyEarning.aspx", false);

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            Session["IsSuccess"] = false;
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("USP_Trn_LocalEarningDetail_Dcs",
                                                  new string[] { "flag"
                                                ,"DcsLocalEarning_Id"
				                                ,"EntryDt"
				                                ,"Office_ID"
				                                ,"ProducerId"
				                                ,"NetAmount"
				                                ,"Remark"
				                                ,"CreatedBy"
				                                ,"CreatedBy_IP"                                               
                                    },
                                                  new string[] { "5"
                                              ,ViewState["DcsLocalEarningDetail_Id"].ToString()
                                              ,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")
                                              ,objdb.Office_ID()                                             
                                              ,ddlFarmer.SelectedValue
                                              ,txttotalamount.Text
                                              ,txtRemark.Text
                                              ,ViewState["Emp_ID"].ToString()
                                             ,objdb.GetLocalIPAddress()
                                              
                                    },"dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            Session["UpdateSuccess"] = true;
                            Session["date"] = txtDate.Text;
                            Response.Redirect("SocietyEarning.aspx", false);

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            Session["UpdateSuccess"] = false;
                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                }
               
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    protected void FillGrid()
    {
        try
        {
            gv_EarningDetail.DataSource = string.Empty;
            gv_EarningDetail.DataBind();
            ds = objdb.ByProcedure("USP_Trn_LocalEarningDetail_Dcs", new string[] { "flag", "EntryDt", "Office_ID" }, new string[] {"4",Convert.ToDateTime(txtDateF.Text,cult).ToString("yyyy/MM/dd"),objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gv_EarningDetail.DataSource = ds;
                    gv_EarningDetail.DataBind();
                }
                
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void txtDateF_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void gv_EarningDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            string DcsLocalEarningDetail_Id = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblEntryDt = (Label)row.FindControl("lblEntryDt");
            Label lblProducerId = (Label)row.FindControl("lblProducerId");
            Label lblNetAmount = (Label)row.FindControl("lblNetAmount");
            Label lblRemark = (Label)row.FindControl("lblRemark");
            ViewState["DcsLocalEarningDetail_Id"] = DcsLocalEarningDetail_Id;
            if (e.CommandName == "EditRecord")
            {
                txtDate.Text = lblEntryDt.Text;
                ddlFarmer.ClearSelection();
                ddlFarmer.SelectedValue = lblProducerId.Text;
                txttotalamount.Text = lblNetAmount.Text;
                txtRemark.Text = lblRemark.Text;
                btnSave.Text = "Update";
            }
            if (e.CommandName == "DeleteRecord")
            {
                objdb.ByProcedure("USP_Trn_LocalEarningDetail_Dcs", new string[] { "flag", "DcsLocalEarning_Id" }, new string[] { "6", DcsLocalEarningDetail_Id }, "dataset");           
                Session["DeleteSuccess"] = true;
                Session["date"] = txtDate.Text;
                Response.Redirect("SocietyEarning.aspx", false);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
}