using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using System.IO;
public partial class mis_dailyplan_MachineMaster : System.Web.UI.Page
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
                lblMsg.Text = "";
                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                HeadDataTable();
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
    protected void HeadDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn RowNo = dt.Columns.Add("RowNo", typeof(int));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        dt.Columns.Add(new DataColumn("HeadName", typeof(string)));

        
        ViewState["InsertRecord"] = dt;
        gvHeads.DataSource = dt;
        gvHeads.DataBind();
    }
    protected void FillGrid()
    {
        try
        {
            
            lblReportMsg.Text = "";
            ds = objdb.ByProcedure("Usp_Mst_Machine", new string[] { "flag", "Office_ID" }, new string[] {"6",objdb.Office_ID()}, "dataset");
            if(ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvMachineDetail.DataSource = ds.Tables[0];
                        gvMachineDetail.DataBind();
                    }
                    else
                    {
                        gvMachineDetail.DataSource = string.Empty;
                        gvMachineDetail.DataBind();
                    }
                }
                else
                {
                    gvMachineDetail.DataSource = string.Empty;
                    gvMachineDetail.DataBind();
                }
            }
            else
            {
                gvMachineDetail.DataSource = string.Empty;
                gvMachineDetail.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            
            DataTable dt = (DataTable)ViewState["InsertRecord"];
            dt.Rows.Add(null, txtHeadName.Text);
            ViewState["InsertRecord"] = dt;
            gvHeads.DataSource = dt;
            gvHeads.DataBind();
           
        }
        catch(Exception ex)
        {

        }
    }
    protected void gvHeads_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if(e.CommandName == "DeleteRow")
            {
                string Id = e.CommandArgument.ToString();
                DataTable dt = (DataTable)ViewState["InsertRecord"];
                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["RowNo"].ToString() == Id.ToString())
                    {
                        dr.Delete();
                    }
                }
                dt.AcceptChanges();
                ViewState["InsertRecord"] = dt;
                gvHeads.DataSource = dt;
                gvHeads.DataBind();
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
    }
    private DataTable GetMachineHead()
    {

        DataTable dt = new DataTable();
        DataRow dr; 
        dt.Columns.Add(new DataColumn("HeadName", typeof(string)));
       foreach (GridViewRow row in gvHeads.Rows)
        {

            Label lblHeadName = (Label)row.FindControl("lblHeadName");           
            dr = dt.NewRow();
            dr[0] = lblHeadName.Text;            
            dt.Rows.Add(dr);
        }
        return dt;

    }
    protected void btnSave_Click(object sender, EventArgs e)
     {
        try
        {
            lblMsg.Text = "";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";
                string IsActive = "1";
                DataTable dt = new DataTable();
                dt = GetMachineHead();
                if (dt.Rows.Count > 0)
                {
                    ds = objdb.ByProcedure("Usp_Mst_Machine", new string[]
                                         {"flag"
                                          ,"Machine_Name"                                        
                                          ,"Office_ID"
                                          ,"IsActive"
                                          ,"CreatedBy"
                                          ,"CreatedByIP"
                                         }
                                         , new string[]
                                         {"0"                                          
                                          ,txtMachineName.Text
                                          ,objdb.Office_ID()
                                          ,IsActive
                                          ,objdb.createdBy()
                                          ,objdb.GetLocalIPAddress()

                                         }
                                         , new string[] { 
                                              "type_Mst_MachineHeads"                                           
                                          },
                                      new DataTable[] {
                                              dt 
                                          },
                                      "TableSave");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());
                            txtHeadName.Text = "";
                            txtMachineName.Text = "";
                            HeadDataTable();
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {

                            string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "oops!", Warning.ToString());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            Session["IsSuccess"] = false;
                        }

                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Add At Least One Head");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
    }
    protected void gvMachineDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblMachine_ID = (Label)row.FindControl("lblMachine_ID");
            LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
            LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
            LinkButton lnkAdd = (LinkButton)row.FindControl("lnkAdd");
            LinkButton lnkSave = (LinkButton)row.FindControl("lnkSave");
            Label lblHead_ID = (Label)row.FindControl("lblHead_ID");
            Label lblMachineName = (Label)row.FindControl("lblMachineName");
            Label lblHeadName = (Label)row.FindControl("lblHeadName");
            TextBox txt_HeadName = (TextBox)row.FindControl("txt_HeadName");
            TextBox txtAddHeadName = (TextBox)row.FindControl("txtAddHeadName");
            TextBox txt_MachineName = (TextBox)row.FindControl("txt_MachineName");
            if (e.CommandName == "EditRecord")
            {
                lblMachineName.Visible = false;
                lblHeadName.Visible = false;
                txt_HeadName.Visible = true;
                txt_MachineName.Visible = true;
                lnkEdit.Visible = false;
                lnkAdd.Visible = false;
                lnkSave.Visible = false;
                lnkUpdate.Visible = true;
                txtAddHeadName.Visible = false;

            }
            if (e.CommandName == "AddRecord")
            {
                lblMachineName.Visible = true;
                lblHeadName.Visible = true;
                txt_HeadName.Visible = false;
                txtAddHeadName.Visible = true;
                txt_MachineName.Visible = false;
                lnkEdit.Visible = false;
                lnkUpdate.Visible = false;
                lnkAdd.Visible = false;
                lnkSave.Visible = true;


            }
            if(e.CommandName == "UpdateRecord")
            {
                ds = objdb.ByProcedure("Usp_Mst_Machine", new string[] { "flag", "Machine_ID", "Machine_Name", "Head_ID", "Head_Name" }, new string[] { "7", lblMachine_ID.Text, txt_MachineName.Text, lblHead_ID.Text, txt_HeadName.Text }, "dataset");
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {

                    string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());
                    FillGrid();
                    
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                {

                    string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "oops!", Warning.ToString());
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    Session["IsSuccess"] = false;
                }
                
                
            }
            if (e.CommandName == "SaveRecord")
            {
                ds = objdb.ByProcedure("Usp_Mst_Machine", new string[] { "flag", "Machine_ID", "Head_Name", "IsActive", "CreatedBy", "CreatedByIP" }, new string[] { "8", lblMachine_ID.Text, txtAddHeadName.Text, "1", objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {

                    string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());
                    FillGrid();

                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                {

                    string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "oops!", Warning.ToString());
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    Session["IsSuccess"] = false;
                }


            }
            
        }
        catch (Exception ex)
        {


        }
    }
    
}