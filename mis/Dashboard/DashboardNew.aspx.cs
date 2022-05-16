using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;


public partial class mis_Dashboard_DashboardNew : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillCatalog();

                //HL_DCS.HRef = "DSOfficeDetails.aspx?OfficeTypeI=" + objdb.Encrypt("6") + "&OfficeType=" + objdb.Encrypt("DCS");
                //HL_BMC.HRef = "DSOfficeDetails.aspx?OfficeTypeI=" + objdb.Encrypt("5") + "&OfficeType=" + objdb.Encrypt("BMC");
                //HL_MDP.HRef = "DSOfficeDetails.aspx?OfficeTypeI=" + objdb.Encrypt("3") + "&OfficeType=" + objdb.Encrypt("MDP");
                //HL_CC.HRef = "DSOfficeDetails.aspx?OfficeTypeI=" + objdb.Encrypt("4") + "&OfficeType=" + objdb.Encrypt("CC");

                FillShift();

                txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtOrderDate.Attributes.Add("ReadOnly", "ReadOnly");
                //FillProduction();
                //FillQualityControl();
                FillChart();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void FillShift()
    {
        try
        {
            ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds;
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillCatalog()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "1", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //lblDCS.Text = ds.Tables[0].Rows[0]["DCS"].ToString();
                //lblBMC.Text = ds.Tables[0].Rows[0]["BMC"].ToString();
                //lblMDP.Text = ds.Tables[0].Rows[0]["MDP"].ToString();
                //lblCC.Text = ds.Tables[0].Rows[0]["CC"].ToString();
                //lblProducer.Text = ds.Tables[0].Rows[0]["Producer"].ToString();
                //lblDistributor.Text = ds.Tables[0].Rows[0]["Distributer"].ToString();
                //lblParlour.Text = ds.Tables[0].Rows[0]["Parlour"].ToString();
                //lblCitizenCardHolder.Text = ds.Tables[0].Rows[0]["CitizenCardHolder"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void txtOrderDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtOrderDate.Text == "")
            {
                txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtOrderDate.Attributes.Add("ReadOnly", "ReadOnly");
            }
            //FillQualityControl();
            //FillProduction();
            FillChart();
            
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtOrderDate.Text == "")
            {
                txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtOrderDate.Attributes.Add("ReadOnly", "ReadOnly");
            }
            //FillQualityControl();
            //FillProduction();
            FillChart();
           
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    protected void FillChart()
    {
        try
        {
            string QC_PassCases = "0";
            string QC_FailCases = "0";
            string Prod_MilkQuantity = "0";
            string Prod_ProductQuantity = "0";
            string D_MilkQuantity = "0";
            string D_ProductQuantity = "0"; 
            string CL_Society = "0";
            string CL_BMC = "0";
            string CL_CC = "0";
            string CL_MDP = "0"; 
            string Sup_MilkQuantity = "0";
            string Sup_ProductQuantity = "0"; 
            string ActualSale = "0";
            string Ret_Quantity = "0";

            // 1 MILK COLLECTION

            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID", "ShiftName", "Date" }, new string[] { "5", ViewState["Office_ID"].ToString(), ddlShift.SelectedItem.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblCL_MilkQuantity.Text = ds.Tables[0].Rows[0]["CL_MilkQuantity"].ToString();
                lblCL_MilkFat.Text = ds.Tables[0].Rows[0]["CL_MilkFat"].ToString();
                lblCL_MilkSnf.Text = ds.Tables[0].Rows[0]["CL_MilkSnf"].ToString(); 
                CL_Society = ds.Tables[0].Rows[0]["CL_Society"].ToString();
                CL_BMC = ds.Tables[0].Rows[0]["CL_BMC"].ToString();
                CL_CC = ds.Tables[0].Rows[0]["CL_CC"].ToString();
                CL_MDP = ds.Tables[0].Rows[0]["CL_MDP"].ToString();

            }


            // 2 DEMAND

            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID", "Shift_id", "Date" }, new string[] { "4", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblD_MilkQuantity.Text = ds.Tables[0].Rows[0]["D_MilkQuantity"].ToString();
                //lblD_MilkFat.Text = ds.Tables[0].Rows[0]["D_MilkFat"].ToString();
                //lblD_MilkSnf.Text = ds.Tables[0].Rows[0]["D_MilkSnf"].ToString();
                lblD_ProductQuantity.Text = ds.Tables[0].Rows[0]["D_ProductQuantity"].ToString();
                //lblD_ProductFat.Text = ds.Tables[0].Rows[0]["D_ProductFat"].ToString();

                D_MilkQuantity = ds.Tables[0].Rows[0]["D_MilkQuantity"].ToString();
                D_ProductQuantity = ds.Tables[0].Rows[0]["D_ProductQuantity"].ToString();
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FunProduction('" + ds.Tables[0].Rows[0]["MilkQuantity"].ToString() + "', '" + ds.Tables[0].Rows[0]["ProductQuantity"].ToString() + "');", true);
            }

            // 3 For PRODUCTION
             
            //ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID", "Shift_id", "Date" }, new string[] { "3", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    lblMilkQuantity.Text = ds.Tables[0].Rows[0]["MilkQuantity"].ToString();
            //    lblMilkFat.Text = ds.Tables[0].Rows[0]["MilkFat"].ToString();
            //    lblMilkSnf.Text = ds.Tables[0].Rows[0]["MilkSnf"].ToString();
            //    lblProductQuantity.Text = ds.Tables[0].Rows[0]["ProductQuantity"].ToString();
            //    lblProductFat.Text = ds.Tables[0].Rows[0]["ProductFat"].ToString();

            //    Prod_MilkQuantity = ds.Tables[0].Rows[0]["MilkQuantity"].ToString();
            //    Prod_ProductQuantity = ds.Tables[0].Rows[0]["ProductQuantity"].ToString();
            //    // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FunProduction('" + ds.Tables[0].Rows[0]["MilkQuantity"].ToString() + "', '" + ds.Tables[0].Rows[0]["ProductQuantity"].ToString() + "');", true);
            //}
             

            //ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID", "Shift_id", "Date" }, new string[] { "2", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    lblTestCases.Text = ds.Tables[0].Rows[0]["TestCases"].ToString();
            //    lblPassCases.Text = ds.Tables[0].Rows[0]["PassCases"].ToString();
            //    lblFailCases.Text = ds.Tables[0].Rows[0]["FailCases"].ToString();

            //    QC_PassCases = ds.Tables[0].Rows[0]["PassCases"].ToString();
            //    QC_FailCases = ds.Tables[0].Rows[0]["FailCases"].ToString();

            //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FunQualityControl('" + ds.Tables[0].Rows[0]["PassCases"].ToString() + "', '" + ds.Tables[0].Rows[0]["FailCases"].ToString() + "');", true);
            //}

             


            //ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID", "Shift_id", "Date" }, new string[] { "6", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    lblSup_MilkQuantity.Text = ds.Tables[0].Rows[0]["Sup_MilkQuantity"].ToString();
            //    lblSup_MilkFat.Text = ds.Tables[0].Rows[0]["Sup_MilkFat"].ToString();
            //    lblSup_MilkSnf.Text = ds.Tables[0].Rows[0]["Sup_MilkSnf"].ToString();
            //    lblSup_ProductQuantity.Text = ds.Tables[0].Rows[0]["Sup_ProductQuantity"].ToString();
            //    lblSup_ProductFat.Text = ds.Tables[0].Rows[0]["Sup_ProductFat"].ToString();

            //    Sup_MilkQuantity = ds.Tables[0].Rows[0]["Sup_MilkQuantity"].ToString();
            //    Sup_ProductQuantity = ds.Tables[0].Rows[0]["Sup_ProductQuantity"].ToString();
            //    // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FunProduction('" + ds.Tables[0].Rows[0]["MilkQuantity"].ToString() + "', '" + ds.Tables[0].Rows[0]["ProductQuantity"].ToString() + "');", true);
            //}
             
            //ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID", "Shift_id", "Date" }, new string[] { "7", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    lblSale_Quantity.Text = ds.Tables[0].Rows[0]["Sale_Quantity"].ToString();
            //    lblSale_Milk.Text = ds.Tables[0].Rows[0]["Sale_Milk"].ToString();
            //    lblSale_Product.Text = ds.Tables[0].Rows[0]["Sale_Product"].ToString();
            //    lblRet_Quantity.Text = ds.Tables[0].Rows[0]["Ret_Quantity"].ToString();
            //    lblRet_Milk.Text = ds.Tables[0].Rows[0]["Ret_Milk"].ToString();
            //    lblRet_Product.Text = ds.Tables[0].Rows[0]["Ret_Product"].ToString();

            //    ActualSale = ds.Tables[0].Rows[0]["ActualSale"].ToString();
            //    Ret_Quantity = ds.Tables[0].Rows[0]["Ret_Quantity"].ToString();

            //    // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FunProduction('" + ds.Tables[0].Rows[0]["MilkQuantity"].ToString() + "', '" + ds.Tables[0].Rows[0]["ProductQuantity"].ToString() + "');", true);
            //}
             


            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FunChart('" +
               Prod_MilkQuantity + "', '" + Prod_ProductQuantity +
                 "', '" + QC_PassCases + "', '" + QC_FailCases +
                  "', '" + D_MilkQuantity + "', '" + D_ProductQuantity +
                   "', '" + CL_Society + "', '" + CL_BMC + "', '" + CL_CC + "', '" + CL_MDP +
                    "', '" + Sup_MilkQuantity + "', '" + Sup_ProductQuantity +
                    "', '" + ActualSale + "', '" + Ret_Quantity +
                 "');", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillQualityControl()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID", "Shift_id", "Date" }, new string[] { "2", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblTestCases.Text = ds.Tables[0].Rows[0]["TestCases"].ToString();
                lblPassCases.Text = ds.Tables[0].Rows[0]["PassCases"].ToString();
                lblFailCases.Text = ds.Tables[0].Rows[0]["FailCases"].ToString();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FunQualityControl('" + ds.Tables[0].Rows[0]["PassCases"].ToString() + "', '" + ds.Tables[0].Rows[0]["FailCases"].ToString() + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillProduction()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID", "Shift_id", "Date" }, new string[] { "3", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblMilkQuantity.Text = ds.Tables[0].Rows[0]["MilkQuantity"].ToString();
                lblMilkFat.Text = ds.Tables[0].Rows[0]["MilkFat"].ToString();
                lblMilkSnf.Text = ds.Tables[0].Rows[0]["MilkSnf"].ToString();
                lblProductQuantity.Text = ds.Tables[0].Rows[0]["ProductQuantity"].ToString();
                lblProductFat.Text = ds.Tables[0].Rows[0]["ProductFat"].ToString();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FunProduction('" + ds.Tables[0].Rows[0]["MilkQuantity"].ToString() + "', '" + ds.Tables[0].Rows[0]["ProductQuantity"].ToString() + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}