using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformationSystem_MPR_Of_TS_Contractual_Labour : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    getyear();
                  //  getdata();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    public void getyear()
    {
        try
        {

            DataSet dsyear = new DataSet();
            dsyear = objdb.ByProcedure("Get_year_and_month"
                   , new string[] { "flag" }
                   , new string[] { "0" }, "dataset");
            if (dsyear != null)
            {
                if (dsyear.Tables.Count > 0)
                {
                    if (dsyear.Tables[0].Rows.Count > 0)
                    {
                        ddlyear.Items.Clear();
                        ddlyear.DataSource = dsyear.Tables[0];
                        ddlyear.DataTextField = "Year_sel";
                        ddlyear.DataValueField = "Year_sel";
                        ddlyear.DataBind();
                        //CODE CHANGES STARTED BY AJAY ON 13-JUN-2019
                        //ddlDOA.Items.Insert(0, "--Select DOA--");
                        //  ddlyear.Items.Insert(0, DateTime.Now.Year.ToString());
                        ddlyear.Enabled = true;

                        ddlyear2.Items.Clear();
                        ddlyear2.DataSource = dsyear.Tables[0];
                        ddlyear2.DataTextField = "Year_sel";
                        ddlyear2.DataValueField = "Year_sel";
                        ddlyear2.DataBind();
                        //CODE CHANGES STARTED BY AJAY ON 13-JUN-2019
                        //ddlDOA.Items.Insert(0, "--Select DOA--");
                        //  ddlyear.Items.Insert(0, DateTime.Now.Year.ToString());
                        ddlyear.Enabled = true;
                    }

                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                        lblMsg.Focus();
                        //panelasset.Visible = false;

                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void DDlMonth2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (DDlMonth2.SelectedIndex > 0)
            {
                getdata_byfilter();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Select Month for search");
                lblMsg.Focus();
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
            DDlMonth2_SelectedIndexChanged(sender, e);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void getdata_byfilter()
    {
        try
        {

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Contractual_labour",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year" },
                 new string[] { "4", objdb.Office_ID(), Session["Emp_ID"].ToString(),DDlMonth2.SelectedValue
                           , ddlyear2.SelectedItem.Text }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvCLdetail.DataSource = ds;
                    gvCLdetail.DataBind();
                    gvCLdetail.Visible = true;
                    gvCLdetail.Rows[gvCLdetail.Rows.Count - 1].Focus();
                }
                else
                {
                    gvCLdetail.DataSource = null;
                    gvCLdetail.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                    lblMsg.Focus();
                }
            }
            else
            {
                gvCLdetail.DataSource = null;
                gvCLdetail.DataBind();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                lblMsg.Focus();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void getdata()
    {
        try
        {

            DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Contractual_labour",
               new string[] { "flag", "Office_ID", "CreatedBy" },
                 new string[] { "1", objdb.Office_ID(), Session["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvCLdetail.DataSource = ds;
                    gvCLdetail.DataBind();
                    gvCLdetail.Visible = true;
                }
                else
                {
                    gvCLdetail.DataSource = null;
                    gvCLdetail.DataBind();
                }
            }
            else
            {
                gvCLdetail.DataSource = null;
                gvCLdetail.DataBind();
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
            if (DDlMonth.SelectedIndex > 0)
            {
                int count = 0;
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Contractual_labour",
                    new string[] { "flag", "Office_ID", "Month",
                                     "Year", "Num_unskilled", "Num_semi_skilled","Num_skilled","WBA_unskilled","WBA_semi_skilled","WBA_skilled", "CreatedBy" },
                      new string[] { "0", objdb.Office_ID(),  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text ,txtnmUn_Skilled.Text,txtnmSemi_Skilled.Text,txtnmSkilled.Text,txtWBAUn_Skilled.Text,txtWBASemi_Skilled.Text,txtWBASkilled.Text,Session["Emp_ID"].ToString()}, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        count++;
                        if (count > 0)
                        {

                            lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "Saved Successfully");

                        }
                    }
                }


            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Select Month");
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void gvCLdetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "select")
            {
                int MCL_Id = Convert.ToInt32(e.CommandArgument.ToString());
                lblMCL_ID.Text = e.CommandArgument.ToString();
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Contractual_labour",
              new string[] { "flag", "Office_ID", "CreatedBy", "MCL_Id" },
                new string[] { "2", objdb.Office_ID(), Session["Emp_ID"].ToString(), MCL_Id.ToString() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DDlMonth.SelectedValue = ds.Tables[0].Rows[0]["Month"].ToString();
                        getyear();
                        ddlyear.SelectedValue = ds.Tables[0].Rows[0]["Year"].ToString();
                        txtnmUn_Skilled.Text = ds.Tables[0].Rows[0]["Num_unskilled"].ToString();
                        txtnmSemi_Skilled.Text = ds.Tables[0].Rows[0]["Num_semi_skilled"].ToString();
                        txtnmSkilled.Text = ds.Tables[0].Rows[0]["Num_skilled"].ToString();
                        txtWBAUn_Skilled.Text = ds.Tables[0].Rows[0]["WBA_unskilled"].ToString();
                        txtWBASemi_Skilled.Text = ds.Tables[0].Rows[0]["WBA_semi_skilled"].ToString();
                        txtWBASkilled.Text = ds.Tables[0].Rows[0]["WBA_skilled"].ToString();
                        btnSubmit.Visible = false;
                        btnupdate.Visible = true;
                        // txtnmUn_Skilled.Text = ds.Tables[0].Rows[0]["Year"].ToString();

                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvCLdetail_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (DDlMonth.SelectedIndex > 0)
            {
                int count = 0;
                DataSet ds = objdb.ByProcedure("usp_insertupdate_monthly_Contractual_labour",
                    new string[] { "flag", "Office_ID", "Month",
                                     "Year", "Num_unskilled", "Num_semi_skilled","Num_skilled","WBA_unskilled","WBA_semi_skilled","WBA_skilled", "CreatedBy","MCL_Id" },
                      new string[] { "3", objdb.Office_ID(),  DDlMonth.SelectedValue
                           , ddlyear.SelectedItem.Text ,txtnmUn_Skilled.Text,txtnmSemi_Skilled.Text,txtnmSkilled.Text,txtWBAUn_Skilled.Text,txtWBASemi_Skilled.Text,txtWBASkilled.Text,Session["Emp_ID"].ToString(),lblMCL_ID.Text}, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        count++;
                        if (count > 0)
                        {

                            lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success!", ds.Tables[0].Rows[0]["msg"].ToString());

                        }
                    }
                }


            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Select Month");
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
}