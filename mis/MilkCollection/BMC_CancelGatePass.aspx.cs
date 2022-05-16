using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_MilkCollection_BMC_CancelGatePass : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            lblMsg.Text = "";

            if (!Page.IsPostBack)
            {
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                GetViewRefDetails();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
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

    protected void GetViewRefDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID" },
                     new string[] { "26", objdb.Office_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_viewreferenceno.DataSource = ds;
                        gv_viewreferenceno.DataBind();
                        gv_viewreferenceno.HeaderRow.TableSection = TableRowSection.TableHeader;
                        gv_viewreferenceno.UseAccessibleHeader = true;
                         
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("BMC_CancelGatePass.aspx", false);

        ViewState["BI_MilkInOutRefID"] = null;
        ViewState["I_TankerID"] = null;

    }

    protected void btnRefCancel_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        Label lblBI_MilkInOutRefID = (Label)gv_viewreferenceno.Rows[selRowIndex].FindControl("lblBI_MilkInOutRefID");
        Label lblI_TankerID = (Label)gv_viewreferenceno.Rows[selRowIndex].FindControl("lblI_TankerID");

        ViewState["BI_MilkInOutRefID"] = lblBI_MilkInOutRefID.Text;
        ViewState["I_TankerID"] = lblI_TankerID.Text;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);



    }


    public string GetTankerCurrentRStatus(string RefNo)
    {
        string strRefelivestatus = "0";

        try
        {
            DataSet dscheckRs = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "BI_MilkInOutRefID" },
                     new string[] { "32", RefNo }, "dataset");


            if (dscheckRs != null)
            {
                if (dscheckRs.Tables.Count > 0)
                {
                    if (dscheckRs.Tables[0].Rows.Count > 0)
                    {
                        strRefelivestatus = dscheckRs.Tables[0].Rows[0]["RefAvailable"].ToString();

                        return strRefelivestatus;
                    }
                    else
                    {
                        return strRefelivestatus;
                    }
                }
                else
                {
                    return strRefelivestatus;
                }
            }
            else
            {
                return strRefelivestatus;
            }

        }
        catch (Exception)
        {

            return strRefelivestatus;
        }


    }

    public string GetTankerCacelStatus(string RefNo)
    {
        string strRefeCancelstatus = "0";

        try
        {
            DataSet dscheckRsC = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                     new string[] { "flag", "BI_MilkInOutRefID" },
                     new string[] { "17", RefNo }, "dataset");

            if (dscheckRsC != null)
            {
                if (dscheckRsC.Tables.Count > 0)
                {
                    if (dscheckRsC.Tables[0].Rows.Count > 0)
                    {
                        strRefeCancelstatus = dscheckRsC.Tables[0].Rows[0]["Status"].ToString();

                        return strRefeCancelstatus;
                    }
                    else
                    {
                        return strRefeCancelstatus;
                    }
                }
                else
                {
                    return strRefeCancelstatus;
                }
            }
            else
            {
                return strRefeCancelstatus;
            }

        }
        catch (Exception)
        {

            return strRefeCancelstatus;
        }

    }


    protected void btnYesT_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                if (ViewState["BI_MilkInOutRefID"] != null && ViewState["I_TankerID"] != null)
                {

                    string strtCancelRS = GetTankerCurrentRStatus(ViewState["BI_MilkInOutRefID"].ToString());

                    if (strtCancelRS == "0")
                    {

                    }
                    else
                    {
                        lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Oops! " + "This Reference Number - " + ViewState["BI_MilkInOutRefID"].ToString() + " In-Process / Challan Generated");
                        GetViewRefDetails();
                        return;
                    }

                    string strtCancelStatus = GetTankerCacelStatus(ViewState["BI_MilkInOutRefID"].ToString());

                    if (strtCancelStatus == "0")
                    {

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Oops! " + "This Reference Number - " + ViewState["BI_MilkInOutRefID"].ToString() + " Already Canceled");
                        GetViewRefDetails();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", false);
                        return;
                    }


                    ds = null;
                    ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU", new string[] { "flag", "RefCancelRemark"
				                                ,"BI_MilkInOutRefID"
				                                ,"I_TankerID"
                                                ,"RefCancelBy" },
                                                       new string[] { "27", txtRefCancelRemark.Text,
                                                ViewState["BI_MilkInOutRefID"].ToString(),
                                                ViewState["I_TankerID"].ToString(),
                                                objdb.createdBy() }, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = "Reference Successfully canceled";

                        lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", success);
                        btnSaveTankerDetails.Text = "Submit";
                        btnSaveTankerDetails.Enabled = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

                        //  string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        //  lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        // btnSaveTankerDetails.Text = "Save";
                        // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        if (error == "Already Exists.")
                        {
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Vehicle No Already Exists");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                        }
                        else
                        {
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    txtRefCancelRemark.Text = "";
                    GetViewRefDetails();
                }

            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "First To Fill At Least One Chilling Centre Details");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    //protected void gv_viewreferenceno_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        foreach (GridViewRow row1 in gv_viewreferenceno.Rows)
    //        {
    //            Label lblRefCancelStatusF = (Label)row1.FindControl("lblRefCancelStatusF");
    //            LinkButton btnRefCancel = (LinkButton)row1.FindControl("btnRefCancel");
    //            Label lblBI_MilkInOutRefID = (Label)row1.FindControl("lblBI_MilkInOutRefID");
    //            Label Challan_Validation = (Label)row1.FindControl("lblChallan_Validation");


    //            if (lblRefCancelStatusF.Text == "True" || Challan_Validation.Text != "")
    //            {
    //                btnRefCancel.Visible = false;
    //            }
    //            else
    //            {
    //                btnRefCancel.Visible = true;

    //            }

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }

    //}
}