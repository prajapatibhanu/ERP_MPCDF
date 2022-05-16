using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

public partial class mis_MilkCollection_NPDWMShiftingMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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
                
                GetCCDetails();
               // ddlType.Enabled = false;
               

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
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTransferfromCC.DataTextField = "Office_Name";
                        ddlTransferfromCC.DataValueField = "Office_ID";
                        ddlTransfertoCC.DataTextField = "Office_Name";
                        ddlTransfertoCC.DataValueField = "Office_ID";

                        ddlTransferfromCC.DataSource = ds;
                        ddlTransferfromCC.DataBind();
                        ddlTransfertoCC.DataSource = ds;
                        ddlTransfertoCC.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlTransferfromCC.Items.Insert(0, new ListItem("Select", "0"));
                            ddlTransfertoCC.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlTransfertoCC.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlTransfertoCC.Enabled = false;
                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
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
            btnUpdate.Visible = false;
            GridView1.DataSource = string.Empty;
            GridView1.DataBind();
            ds = objdb.ByProcedure("Sp_NPDWMShifting", 
                new string[] { "flag", "CC_Id", "BillingPeriodFromDate", "BillingPeriodToDate" },
                new string[] { "1", ddlTransferfromCC.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                btnUpdate.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();

            }
            else
            {
                GridView1.DataSource = string.Empty;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DataTable dt = new DataTable();
				DataTable dt1 = new DataTable();
                dt = GetData();
				dt1 = GetTransferData();
				string FDT =Convert.ToDateTime(txtFdt.Text,cult).ToString("yyyy/MM/dd");
                string TDT = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                //DateTime F1 = Convert.ToDateTime(FDT).AddDays(1);
				DateTime F1 = Convert.ToDateTime(TDT).AddDays(1);
                DateTime F2 = Convert.ToDateTime(TDT).AddDays(10);
                //DateTime F1 = Convert.ToDateTime(txtFdt.Text).AddDays(10);
                //DateTime F2 = Convert.ToDateTime(txtTdt.Text).AddDays(10);
                if (dt.Rows.Count > 0)
                {
                    if (ddlType.SelectedValue == "1")
                    {
                        ds = objdb.ByProcedure("Sp_NPDWMShifting",
                                               new string[] 
                                           {"flag",
										    "BillingPeriodFromDate",
                                            "BillingPeriodToDate",
                                            "CreatedBy",
                                            "CreatedAt",
                                            "CreatedByIP"
                                           },
                                               new string[] 
                                           {"4", 
											Convert.ToDateTime(txtFdt.Text,cult).ToString("yyyy/MM/dd"),
                                            Convert.ToDateTime(txtTdt.Text,cult).ToString("yyyy/MM/dd"),										   
                                            objdb.createdBy(),
                                            objdb.Office_ID(),
                                            objdb.GetLocalIPAddress()
                                           },
                                               new string[]
                                           {"type_UpdateDWMNP"
                                           },
                                                new DataTable[] 
                                            {dt 
                                            }, "TableSave");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                                {
                                    lblMsg.Text = objdb.Alert("fa-danger", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                            }
                        }
                    }
                    else if (ddlType.SelectedValue == "2")
                    {
                        ds = objdb.ByProcedure("Sp_NPDWMShifting",
                                               new string[] 
                                           {"flag",
                                            "CC_Id",
                                            "BillingPeriodFromDate",
                                            "BillingPeriodToDate",
                                            "CreatedBy",
                                            "CreatedAt",
                                            "CreatedByIP"
                                           },
                                               new string[] 
                                           {"5",
                                            ddlTransfertoCC.SelectedValue,
                                            Convert.ToDateTime(F1,cult).ToString("yyyy/MM/dd"),
                                            Convert.ToDateTime(F2,cult).ToString("yyyy/MM/dd"),	
                                            objdb.Office_ID(),
                                            objdb.createdBy(),
                                            objdb.Office_ID(),
                                            objdb.GetLocalIPAddress()
                                           },
                                               new string[]
                                           {"type_UpdateDWMNP",
                                            "type_TransferDWMNP"
                                           },
                                                new DataTable[] 
                                            {dt,
                                             dt1 
                                            }, "TableSave");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                                {
                                    lblMsg.Text = objdb.Alert("fa-danger", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                            }
                        }
                    }

                    ddlTransferfromCC.ClearSelection();
                    ddlTransfertoCC.ClearSelection();
                    ddlTransferfromCC_SelectedIndexChanged(sender, e);
                    ddlType_SelectedIndexChanged(sender, e);
                    btnSearch_Click(sender, e);
                    txtFdt.Text = "";
                    txtTdt.Text = "";
                    btnUpdate.Visible = false;
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select at least one society')", true);
                }
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected DataTable GetData()
    {
        DataTable dt = new DataTable();
        //dt.Columns.Add("NPDetails_ID", typeof(int));
        dt.Columns.Add("AddtionsDeducEntry_ID", typeof(int));
        dt.Columns.Add("HeadAmount", typeof(decimal));
        dt.Columns.Add("HeadRemark", typeof(string));

        foreach(GridViewRow rows in GridView1.Rows)
        {
            CheckBox chk = (CheckBox)rows.FindControl("CheckBox1");
            //Label NPDetails_ID = (Label)rows.FindControl("lblNPDetails_ID");
            Label AddtionsDeducEntry_ID = (Label)rows.FindControl("lblAddtionsDeducEntry_ID");
            TextBox HeadAmount = (TextBox)rows.FindControl("txtUpdateAmount");
            TextBox HeadRemark = (TextBox)rows.FindControl("txtUpdateHeadRemark");
            if(chk.Checked == true)
            {
                //dt.Rows.Add(NPDetails_ID.Text, AddtionsDeducEntry_ID.Text, HeadAmount.Text, HeadRemark.Text);
                dt.Rows.Add(AddtionsDeducEntry_ID.Text, HeadAmount.Text, HeadRemark.Text);
            }
        }
        return dt;
    }
	protected DataTable GetTransferData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("NPDetails_ID", typeof(int));
       

        foreach (GridViewRow rows in GridView1.Rows)
        {
            CheckBox chk = (CheckBox)rows.FindControl("CheckBox1");
            Label NPDetails_ID = (Label)rows.FindControl("lblNPDetails_ID");
           
            if (chk.Checked == true)
            {

                dt.Rows.Add(NPDetails_ID.Text);
            }
        }
        return dt;
    }
    protected void GetBillingPeriod()
    {
        try
        {
            //lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_NPDWMShifting", new string[] { "flag", "CC_Id" }, new string[] {"3",ddlTransferfromCC.SelectedValue }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    txtFdt.Text = ds.Tables[0].Rows[0]["BillingCycleFromDate"].ToString();
                    txtTdt.Text = ds.Tables[0].Rows[0]["BillingCycleToDate"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlTransferfromCC_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillingPeriod();
        btnUpdate.Visible = false;
        GridView1.DataSource = string.Empty;
        GridView1.DataBind();

    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        divTransfertto.Visible = false;
        if(ddlType.SelectedIndex > 0)
        {
            if(ddlType.SelectedValue == "1")
            {
                divTransfertto.Visible = false;
            }
            else
            {
                divTransfertto.Visible = true;
            }
        }
    }
}