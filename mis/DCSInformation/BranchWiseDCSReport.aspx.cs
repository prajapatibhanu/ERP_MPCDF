using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_DCSInformation_BranchWiseDCSReport : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";                   
                    FillBank();
                    FillBranch();
                    
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        
    }
    protected void FillBank()
    {
        try
        {
            ddlBank.Items.Clear();
            ds = objdb.ByProcedure("SpKCCDCSWiseInformation", new string[] { "flag" }, new string[] { "5" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlBank.DataSource = ds;
                ddlBank.DataValueField = "BankId";
                ddlBank.DataTextField = "BankName";
                ddlBank.DataBind();

            }
            ddlBank.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    protected void btnSearch_Click(object sender, System.EventArgs e)
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
            lblMsg.Text = "";
            gvDetails.DataSource = new string[] { };
            gvDetails.DataBind();
            ds = objdb.ByProcedure("SpKCCReports", new string[] { "flag", "FromDate", "ToDate", "Bank_ID", "Branch_ID", "Office_ID" }, new string[] { "4", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlBank.SelectedValue.ToString(),ddlBranchName.SelectedValue.ToString(), Session["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int TotalofNoofMalesformsSubmittedbyDCS = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("NoofMalesformsSubmittedbyDCS"));
                int TotalofNoofFemalesformsSubmittedbyDCS = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("NoofFemalesformsSubmittedbyDCS"));
                int TotalofTotalNoofformsSubmittedbyDCS = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalNoofformsSubmittedbyDCS"));
                int TotalofTotalofNoofMalesformsSubmittedforincreaseinlimit = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OutofwhichNoofMalesformsSubmittedforincreaseinlimit"));
                int TotalofTotalofNoofFemalesformsSubmittedforincreaseinlimit = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OutofwhichNoofFemalesformsSubmittedforincreaseinlimit"));
                int TotalofTotalofTotalNoofformsSubmittedforincreaseinlimit = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OutofwhichTotalNoofformsSubmittedforincreaseinlimit"));
                int TotalofMaleApplicationforNewKCChavingLand = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("MaleApplicationforNewKCChavingLand"));
                int TotalofFemaleApplicationforNewKCChavingLand = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("FemaleApplicationforNewKCChavingLand"));
                int TotalofTotalApplicationforNewKCChavingLand = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalApplicationforNewKCChavingLand"));
                int TotalofMaleApplicationforNewKCChavingnoLand = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("MaleApplicationforNewKCChavingnoLand"));
                int TotalofFemaleApplicationforNewKCChavingnoLand = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("FemaleApplicationforNewKCChavingnoLand"));
                int TotalofTotalApplicationforNewKCChavingnoLand = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalApplicationforNewKCChavingnoLand"));
                int TotalofAnyOthersByMale = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("AnyOthersByMale"));
                int TotalofAnyOthersByFemale = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("AnyOthersByFemale"));
                int TotalofAnyOthersByTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("AnyOthersByTotal"));
                int TotalofNoofMaleformsSubmittedbyUniontotheBank = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("NoofMaleformsSubmittedbyUniontotheBank"));
                int TotalofNoofFemaleformsSubmittedbyUniontotheBank = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("NoofFemaleformsSubmittedbyUniontotheBank"));
                int TotalofTotalNoofformsSubmittedbyUniontotheBank = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalNoofformsSubmittedbyUniontotheBank"));
                int TotalofNoofCardIssuedByMale = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("NoofCardIssuedByMale"));
                int TotalofNoofCardIssuedByFemale = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("NoofCardIssuedByFemale"));
                int TotalofTotalNoofCardIssued = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalNoofCardIssued"));


                gvDetails.FooterRow.Cells[3].Text = "<b>Total : </b>";
                gvDetails.FooterRow.Cells[4].Text = "<b>" + TotalofNoofMalesformsSubmittedbyDCS.ToString() + "</b>";
                gvDetails.FooterRow.Cells[5].Text = "<b>" + TotalofNoofFemalesformsSubmittedbyDCS.ToString() + "</b>";
                gvDetails.FooterRow.Cells[6].Text = "<b>" + TotalofTotalNoofformsSubmittedbyDCS.ToString() + "</b>";
                gvDetails.FooterRow.Cells[7].Text = "<b>" + TotalofTotalofNoofMalesformsSubmittedforincreaseinlimit.ToString() + "</b>";
                gvDetails.FooterRow.Cells[8].Text = "<b>" + TotalofTotalofNoofFemalesformsSubmittedforincreaseinlimit.ToString() + "</b>";
                gvDetails.FooterRow.Cells[9].Text = "<b>" + TotalofTotalofTotalNoofformsSubmittedforincreaseinlimit.ToString() + "</b>";
                gvDetails.FooterRow.Cells[10].Text= "<b>" + TotalofMaleApplicationforNewKCChavingLand.ToString() + "</b>";
                gvDetails.FooterRow.Cells[11].Text = "<b>" + TotalofFemaleApplicationforNewKCChavingLand.ToString() + "</b>";
                gvDetails.FooterRow.Cells[12].Text = "<b>" + TotalofTotalApplicationforNewKCChavingLand.ToString() + "</b>";
                gvDetails.FooterRow.Cells[13].Text = "<b>" + TotalofMaleApplicationforNewKCChavingnoLand.ToString() + "</b>";
                gvDetails.FooterRow.Cells[14].Text = "<b>" + TotalofFemaleApplicationforNewKCChavingnoLand.ToString() + "</b>";
                gvDetails.FooterRow.Cells[15].Text = "<b>" + TotalofTotalApplicationforNewKCChavingnoLand.ToString() + "</b>";
                gvDetails.FooterRow.Cells[16].Text = "<b>" + TotalofAnyOthersByMale.ToString() + "</b>";
                gvDetails.FooterRow.Cells[17].Text = "<b>" + TotalofAnyOthersByFemale.ToString() + "</b>";
                gvDetails.FooterRow.Cells[18].Text = "<b>" + TotalofAnyOthersByTotal.ToString() + "</b>";
                gvDetails.FooterRow.Cells[19].Text = "<b>" + TotalofNoofMaleformsSubmittedbyUniontotheBank.ToString() + "</b>";
                gvDetails.FooterRow.Cells[20].Text = "<b>" + TotalofNoofFemaleformsSubmittedbyUniontotheBank.ToString() + "</b>";
                gvDetails.FooterRow.Cells[21].Text = "<b>" + TotalofTotalNoofformsSubmittedbyUniontotheBank.ToString() + "</b>";
                gvDetails.FooterRow.Cells[22].Text = "<b>" + TotalofNoofCardIssuedByMale.ToString() + "</b>";
                gvDetails.FooterRow.Cells[23].Text = "<b>" + TotalofNoofCardIssuedByFemale.ToString() + "</b>";
                gvDetails.FooterRow.Cells[24].Text = "<b>" + TotalofTotalNoofCardIssued.ToString() + "</b>";

                gvDetails.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[15].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[16].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[17].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[18].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[19].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[20].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[21].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[22].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[23].HorizontalAlign = HorizontalAlign.Left;
                gvDetails.FooterRow.Cells[24].HorizontalAlign = HorizontalAlign.Left;
            }
            
           
           
            
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillBranch();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillBranch()
    {
        try
        {
            ddlBranchName.Items.Clear();
           
            ds = objdb.ByProcedure("SpKCCDCSWiseInformation", new string[] { "flag", "BankID" }, new string[] { "9", ddlBank.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlBranchName.DataSource = ds;
                ddlBranchName.DataValueField = "ID";
                ddlBranchName.DataTextField = "BranchName";
                ddlBranchName.DataBind();

            }
            ddlBranchName.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}