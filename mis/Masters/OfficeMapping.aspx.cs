using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;

public partial class mis_Masters_OfficeMapping : System.Web.UI.Page
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

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }

    protected void FillSociety()
    {
        try
        {
            divsupplyunit.Visible = false;
            chkMappedSupplyunit.Items.Clear();
            
           
            if (ddlMilkCollectionUnit.SelectedIndex > 0)
            {
                ds = null;
                ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                                  new string[] { "10", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[2];
                        ddlSociety.DataBind();
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
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
            else
            {
                Response.Redirect("OfficeMapping.aspx", false);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlSociety_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divsupplyunit.Visible = false;
            chkMappedSupplyunit.Items.Clear();
           
            if(ddlSociety.SelectedIndex > 0)
            {
                if(ddlMilkCollectionUnit.SelectedValue == "5")
                {
                    FillMappedSupplyunit();
                   // FillUnMappedSupplyunit();
                }
                else
                {
                    divsupplyunit.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillMappedSupplyunit()
    {
        try
        {
            lblMsg.Text = "";
            string flag = "";
            btnSave.Visible = false;
            if(ddlMilkCollectionUnit.SelectedValue == "5")
            {
                flag = "48";
            }
            else
            {
                if(ddlSupplyUnit.SelectedValue == "1")
                {
                    flag = "48";
                }
                else if (ddlSupplyUnit.SelectedValue == "2")
                {
                    flag = "49";
                }
                else
                {
                    flag = "52";
                }
            }
            string TooltipText = "";
            string ChkStatus = "";
            chkMappedSupplyunit.Items.Clear();

            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID", "Office_Parant_ID" }, new string[] { flag, ddlSociety.SelectedValue, objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnSave.Visible = true;
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    DataView dv = new DataView();
                    DataSet ds6 = ds;
                    dv = ds6.Tables[0].DefaultView;
                    dv.RowFilter = "IsActive = '1'";
                    DataTable dt1 = dv.ToTable();
                    ViewState["dt1"] = dt1;
                    chkMappedSupplyunit.DataSource = ds;
                    chkMappedSupplyunit.DataTextField = "Office_Name";
                    chkMappedSupplyunit.DataValueField = "Office_ID";
                   
                   
                   
                    chkMappedSupplyunit.DataBind();
                    foreach (ListItem listItem in chkMappedSupplyunit.Items)
                    {
                        DataRow[] dRows = dt.Select("Office_ID = " + listItem.Value);
                        if (dRows != null)
                        {
                            if (dRows.Length > 0)
                            {
                                TooltipText = dRows[0]["IsActive"].ToString();
                                ChkStatus = dRows[0]["ChkStatus"].ToString();
                            }
                        }

                        if (TooltipText == "1")
                        {
                            listItem.Selected = true;
                            listItem.Enabled = true;
                            
                            
                            
                            
                        }
                        if (ChkStatus == "Disabled")
                        {
                            
                            listItem.Enabled = false;
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
    
    protected void ddlSupplyUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlSupplyUnit.SelectedIndex > 0)
            {
                
                    FillMappedSupplyunit();
                    
                
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
            string flag = "";
            if(ddlMilkCollectionUnit.SelectedValue == "5")
            {
                flag = "50";
            }
            else
            {
                if(ddlSupplyUnit.SelectedValue == "1")
                {
                    flag = "50";
                }
                 else if (ddlSupplyUnit.SelectedValue == "2")
                {
                    flag = "51";
                }
                else
                {
                    flag = "53";
                }
                
            }
            lblMsg.Text = "";
            int Chkcount = 0;
            DataTable dtMap = new DataTable();
            dtMap.Columns.Add("Office_ID", typeof(string));

            foreach (ListItem listItem in chkMappedSupplyunit.Items)
            {
                if (listItem.Selected == true)
                {
                    if (listItem.Enabled == true)
                    {
                        dtMap.Rows.Add(listItem.Value);


                    }
                }
            }
            DataTable dtUnMap = new DataTable();
            dtUnMap.Columns.Add("Office_ID", typeof(string));
            DataTable dt = (DataTable)ViewState["dt1"];
            foreach (ListItem listItem in chkMappedSupplyunit.Items)
            {
                
                    if (listItem.Selected == false)
                    {
                        DataRow[] dRows = dt.Select("Office_ID = " + listItem.Value);
                        if (dRows != null)
                        {
                            if (dRows.Length > 0)
                            {
                                dtUnMap.Rows.Add(listItem.Value);
                            }
                        }
                    }                
            }
            //if (dtMap.Rows.Count > 0)
            //{
                //if (dtMap.Rows.Count <=2)
               // {

                    ds = objdb.ByProcedure("SpAdminOffice", new string[]
                                                      { "flag", 
                                                        "Office_ID", 
                                                        "CreatedBy", 
                                                        "CreatedBy_IP" },
                                                             new string[] 
                                                       { flag, 
                                                         ddlSociety.SelectedValue,                                                        
                                                         objdb.createdBy(), 
                                                         objdb.GetLocalIPAddress() 
                                                       }, new string[] {
                                                       "type_OfficeMapping"  
                                                       ,"type_OfficeUnMapping"},
                                                         new DataTable[] {
                                                       dtMap, 
                                                       dtUnMap
                                                        }, "TableSave");
                    
                    chkMappedSupplyunit.Items.Clear();
                    btnSave.Visible = false;
                    ddlMilkCollectionUnit.ClearSelection();
                    ddlMilkCollectionUnit_SelectedIndexChanged(sender, e);
                    ddlSociety_SelectedIndexChanged(sender, e);
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou", "Mapping Done Sucessfully");
               // }
               // else
                //{
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please select only 2 checkbox')", true);
                //}
            //}
            //else
            //{
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please select atleast one checkbox')", true);
            //}
           
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}