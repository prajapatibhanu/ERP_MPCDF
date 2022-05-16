using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_SocietyBillRemarkMaster : System.Web.UI.Page
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
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                
                GetCCDetails();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");

                if (objdb.Office_ID() == "2")
                {
                    ddlBillingCycle.SelectedValue = "5 days";
                }
                else
                {
                    ddlBillingCycle.SelectedValue = "10 days";
                }

                txtDate_TextChanged(sender, e);
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
    //protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillSociety();
    //}
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
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
                        }
                        ddlccbmcdetail_flt.DataTextField = "Office_Name";
                        ddlccbmcdetail_flt.DataValueField = "Office_ID";


                        ddlccbmcdetail_flt.DataSource = ds;
                        ddlccbmcdetail_flt.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail_flt.Items.Insert(0, new ListItem("All", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
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
    protected void FillSociety()
    {
        try
        {
            ddlSociety.ClearSelection();
            ddlSociety.Items.Clear();
            if (ddlccbmcdetail.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry",
                         new string[] { "flag", "Office_ID", "Office_Parant_ID", "OfficeType_ID" },
                         new string[] { "8", ddlccbmcdetail.SelectedValue.ToString(), objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[0];
                        ddlSociety.DataBind();
                       // ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));

                    }
                }
                else
                {
                    //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                // Response.Redirect("SocietywiseMilkCollectionInvoiceDetails.aspx", false);
            }
            ddlSociety.Items.Insert(0, new ListItem("Select", "0"));

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
        if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
        {
            lblMsg.Text = "";
            string IsActive = "1";
          
            if(btnSubmit.Text == "Save")
            {
                ds = objdb.ByProcedure("Usp_Mst_SocietyBillRemark",
                                      new string[] {"flag",
                                                 "EntryDate", 
                                                 "Office_ID",
                                                 "CC_ID",
                                                 "BillingFromDate" ,
                                                 "BillingToDate",
                                                 "Remark",                                                 
                                                 "CreatedAt", 
                                                 "CreatedBy", 
                                                 "CreatedByIP", 
                                                 "IsActive"
                                               },
                                     new string[] {"1"
                                              ,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")
                                              ,ddlSociety.SelectedValue
                                              ,ddlccbmcdetail.SelectedValue
                                              ,Convert.ToDateTime(txtBillingCycleFromDate.Text,cult).ToString("yyyy/MM/dd")
                                              ,Convert.ToDateTime(txtBillingCycleToDate.Text,cult).ToString("yyyy/MM/dd")
                                              ,txtRemark.Text
                                              ,objdb.Office_ID()
                                              ,objdb.createdBy()
                                              ,objdb.GetLocalIPAddress()
                                              ,IsActive                                     
                                 }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Success);
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning);
                        }
                        else
                        {
                            string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Danger!", Danger);
                        }

                    }
                }
            }
            //else if (btnSubmit.Text == "Update")
            //{
            //    ds = objdb.ByProcedure("Usp_AdditionalChargesMaster",
            //                          new string[] {"flag",
            //                                     "AdditionalCharges_ID",
            //                                     "EntryDate", 
            //                                     "Office_ID", 
            //                                     "Office_Parant_ID",
            //                                     "Rateperkg", 
            //                                     "CreatedAt", 
            //                                     "CreatedBy", 
            //                                     "CreatedByIP", 
                                               
            //                                   },
            //                         new string[] {"3"
            //                                  ,ViewState["AdditionalCharges_ID"].ToString()
            //                                  ,Convert.ToDateTime(txtEffectiveDate.Text,cult).ToString("yyyy/MM/dd")
            //                                  ,ddlSociety.SelectedValue
            //                                  ,objdb.Office_ID()
            //                                  ,txtRateperkg.Text
            //                                  ,objdb.Office_ID()
            //                                  ,objdb.createdBy()
            //                                  ,objdb.GetLocalIPAddress()
                                                                                 
            //                     }, "dataset");
            //    if (ds != null && ds.Tables.Count > 0)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
            //            {
            //                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
            //                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Success);
            //            }
            //            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
            //            {
            //                string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
            //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning);
            //            }
            //            else
            //            {
            //                string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
            //                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Danger!", Danger);
            //            }

            //        }
            //    }
            //}
           //FillGrid();
            btnSubmit.Text = "Save";
          
            ddlSociety.Enabled = true;
            ddlccbmcdetail.Enabled = true;
            ddlccbmcdetail.ClearSelection();
            ddlccbmcdetail_SelectedIndexChanged(sender, e);
          
            ddlSociety.ClearSelection();

            txtRemark.Text = "";
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtDate.Text != "")
            {
                string BillingCycle = ddlBillingCycle.SelectedItem.Text;
                string[] DatePart = txtDate.Text.Split('/');
                int Day = int.Parse(DatePart[0]);
                int Month = int.Parse(DatePart[1]);
                int Year = int.Parse(DatePart[2]);
                string SelectedFromDate = "";
                string SelectedToDate = "";
                if (BillingCycle == "5 days")
                {
                    if (Day >= 1 && Day < 6)
                    {
                        SelectedFromDate = "01";
                        SelectedToDate = "05";
                    }
                    else if (Day > 5 && Day < 11)
                    {
                        SelectedFromDate = "6";
                        SelectedToDate = "10";
                    }
                    else if (Day > 10 && Day < 16)
                    {
                        SelectedFromDate = "11";
                        SelectedToDate = "15";
                    }
                    else if (Day > 15 && Day < 21)
                    {
                        SelectedFromDate = "16";
                        SelectedToDate = "20";
                    }
                    else if (Day > 20 && Day < 26)
                    {
                        SelectedFromDate = "21";
                        SelectedToDate = "25";
                    }
                    else if (Day > 25 && Day <= 31)
                    {
                        SelectedFromDate = "26";
                        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
                        {
                            SelectedToDate = "31";
                        }
                        else if (Month == 2)
                        {
                            SelectedToDate = "28";
                        }
                        else
                        {
                            SelectedToDate = "30";
                        }

                    }
                    SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                    SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;

                }
                else
                {
                    if (Day >= 1 && Day < 11)
                    {
                        SelectedFromDate = "01";
                        SelectedToDate = "10";
                    }
                    else if (Day > 10 && Day < 21)
                    {
                        SelectedFromDate = "11";
                        SelectedToDate = "20";
                    }
                    else if (Day > 20 && Day <= 31)
                    {
                        SelectedFromDate = "21";
                        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
                        {
                            SelectedToDate = "31";
                        }
                        else if (Month == 2)
                        {
                            SelectedToDate = "28";
                        }
                        else
                        {
                            SelectedToDate = "30";
                        }
                    }

                    SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                    SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;
                }
                txtBillingCycleFromDate.Text = Convert.ToDateTime(SelectedFromDate, cult).ToString("dd/MM/yyyy");
                txtBillingCycleToDate.Text = Convert.ToDateTime(SelectedToDate, cult).ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void ddlBillingCycle_TextChanged(object sender, EventArgs e)
    {
        txtDate_TextChanged(sender, e);
    }
    
    protected void ddlccbmcdetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
   
}