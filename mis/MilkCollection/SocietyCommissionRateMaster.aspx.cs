using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;

public partial class mis_MilkCollection_SocietyCommissionRateMaster : System.Web.UI.Page
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

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Society Milk Invoice Successfully Generated');", true);
                    }
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtEffectiveDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
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
    protected void FillGrid()
    {
        try
        {
            
            ds = objdb.ByProcedure("Usp_Mst_SocietyCommissionRateMaster", new string[] { "flag", "Office_ID"}, new string[] {"2",objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gvDetail.DataSource = ds;
                    gvDetail.DataBind();
                }
                else
                {
                    gvDetail.DataSource = string.Empty;
                    gvDetail.DataBind();
                }
            }
            else
            {
                gvDetail.DataSource = string.Empty;
                gvDetail.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string IsActive = "1";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                if(btnSubmit.Text == "Save")
                {
                    ds = objdb.ByProcedure("Usp_Mst_SocietyCommissionRateMaster", new string[] { "flag", "Rate", "MilkType", "EffectiveDate", "Office_ID", "CreatedBy", "CreatedBy_IP", "IsActive" }, new string[] { "0", txtRate.Text,ddlMilkType.SelectedValue, Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy/MM/dd"), objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress(), IsActive }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Msg = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Msg.ToString());

                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                string Msg = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "warning!", Msg.ToString());

                            }
                            else
                            {
                                string Msg = ds.Tables[0].Rows[0]["Msg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Msg.ToString());
                            }
                            txtRate.Text = "";
                            FillGrid();
                        }
                    }
                }
                else if(btnSubmit.Text == "Update")
                {
                    ds = objdb.ByProcedure("Usp_Mst_SocietyCommissionRateMaster", new string[] { "flag", "Rate", "MilkType", "EffectiveDate", "SocCommissionRate_Id", "CreatedBy", "CreatedBy_IP" }, new string[] { "3", txtRate.Text,ddlMilkType.SelectedValue, Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["SocCommissionRate_Id"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)



                        {

                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Msg = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Msg.ToString());

                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                string Msg = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "warning!", Msg.ToString());

                            }
                            else
                            {
                                string Msg = ds.Tables[0].Rows[0]["Msg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Msg.ToString());
                            }
                            txtRate.Text = "";
                            btnSubmit.Text = "Save";
                            FillGrid();
                        }
                    }
                }
               
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string ID = e.CommandArgument.ToString();
            ViewState["SocCommissionRate_Id"] = ID.ToString();
            ds = objdb.ByProcedure("Usp_Mst_SocietyCommissionRateMaster", new string[] { "flag", "SocCommissionRate_Id" }, new string[] { "4", ID }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    txtEffectiveDate.Text = ds.Tables[0].Rows[0]["EffectiveDate"].ToString();
                    txtRate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                    ddlMilkType.ClearSelection();
                    ddlMilkType.Items.FindByValue(ds.Tables[0].Rows[0]["MilkType"].ToString()).Selected = true;
                    btnSubmit.Text = "Update";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
}