﻿using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Masters_HSNMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Emp_ID"] = "1";
        try
        {
            if (objdb.createdBy() != null)
            {
                txtHSNEffectiveDate.Text = objdb.getdate();
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();

                    txtCGST.Attributes.Add("readonly", "readonly");
                    txtSGST.Attributes.Add("readonly", "readonly");
                    ddlIntegratedTax.Enabled = true;
                    FillGrid();
                }
            }
            else
            {
                objdb.redirectToHome();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #region -- User Defined Function --

    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinHSNMaster",
                 new string[] { "flag", "HSN_TypeOfSupply" },
                 new string[] { "9", ddlTypeOfSupply.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
            }
            else
            {
                GridView1.DataSource = new string[] { };
            }
            GridView1.DataBind();
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillHSNGrid()
    {
        try
        {
            lblMsg.Text = "";
            DataSet ds1 = objdb.ByProcedure("SpFinHSNMaster",
                new string[] { "flag", "HSN_ID" },
                new string[] { "8", ViewState["HSN_ID"].ToString() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds1;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = ds1;
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        try
        {
            txtHSNCode.Text = "";
            ddlIntegratedTax.ClearSelection();
            txtCGST.Text = "";
            txtSGST.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    #endregion

    #region -- User Defined Function --

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
            lblmodalmsg.Text = "";
            string msg = "";
            if (ddl__IntegratedTax.SelectedIndex == 0)
            {
                msg += "Select Integrated Tax. <br />";
            }
            //if (txt_HSNEffectiveDate.Text == "")
            //{
            //    msg += "Enter HSN/SAC Effective Date.";
            //}
            if (msg == "")
            {
                ds = objdb.ByProcedure("SpFinHSNMaster",
                    new string[] { "flag", "HSN_ID", "HSN_IntegratedTax", "HSN_CGST", "HSN_SGST", "HSN_UpdatedBy" },
                    new string[] { "4", ViewState["HSN_ID"].ToString(), ddl__IntegratedTax.SelectedValue, txt_CGST.Text, txt_SGST.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows[0]["status"].ToString() == "true")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CallModal();", true);
                    lblmodalmsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillHSNGrid();
                    FillGrid();
                    txt_HSNEffectiveDate.Text = "";
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CallModal();", true);
                    //lblmodalmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Update Integrate Tax or HSN_EffectiveDate  for changes");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = "";
                string msg = "";
                string HSN_IsActive = "1";
                if (ddlTypeOfSupply.SelectedIndex == 0)
                {
                    msg = "Select Type of Supply <br/>";
                }
                if (txtHSNEffectiveDate.Text == "")
                {
                    msg += "Enter HSN/SAC Effective Date. <br />";
                }
                if (txtHSNCode.Text == "")
                {
                    msg += "Enter HSN Code. <br />";
                }
                if (ddlIntegratedTax.SelectedIndex == 0)
                {
                    msg += "Select Integrated Tax. <br />";
                }
                if (msg.Trim() == "")
                {
                    int Status = 0;
                    DataSet ds11 = objdb.ByProcedure("SpFinHSNMaster", new string[] { "flag", "HSN_Code" }, new string[] { "7", txtHSNCode.Text }, "dataset");
                    if (ds11.Tables[0].Rows.Count > 0)
                    {
                        Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());
                    }
                    if (btnSave.Text == "Save" && Status == 0)
                    {
                        objdb.ByProcedure("SpFinHSNMaster",
                            new string[] { "flag", "HSN_IsActive", "HSN_TypeOfSupply", "HSN_Code", "HSN_IntegratedTax", "HSN_CGST", "HSN_SGST", "HSN_UpdatedBy" },
                            new string[] { "0", HSN_IsActive, ddlTypeOfSupply.SelectedValue, txtHSNCode.Text, ddlIntegratedTax.SelectedValue, txtCGST.Text, txtSGST.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        FillGrid();
                        ClearText();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('HSN Code is already exist.');", true);
                        ClearText();
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
    }

    #endregion

    #region -- Change Event --

    protected void ddlIntegratedTax_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlIntegratedTax.SelectedIndex > 0)
            {
                decimal IntegratedTax = Convert.ToDecimal(ddlIntegratedTax.SelectedValue);
                decimal CGST = (IntegratedTax) / 2;
                decimal SGST = (IntegratedTax) / 2;
                txtCGST.Text = CGST.ToString();
                txtSGST.Text = SGST.ToString();
            }
            else
            {
                txtCGST.Text = "";
                txtSGST.Text = "";
            }
            //FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddl__IntegratedTax_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CallModal();", true);
            if (ddl__IntegratedTax.SelectedIndex > 0)
            {
                decimal IntegratedTax = Convert.ToDecimal(ddl__IntegratedTax.SelectedValue);
                decimal CGST = (IntegratedTax) / 2;
                decimal SGST = (IntegratedTax) / 2;
                txt_CGST.Text = CGST.ToString();
                txt_SGST.Text = SGST.ToString();
            }
            else
            {
                txt_CGST.Text = "";
                txt_SGST.Text = "";
            }
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlTypeOfSupply_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
            lblMsg.Text = "";
            lblmodalmsg.Text = "";
            txt_HSNEffectiveDate.Text = objdb.getdate();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CallModal();", true);
            string HSN_ID = GridView1.SelectedDataKey.Value.ToString();
            ViewState["HSN_ID"] = HSN_ID;
            ds = objdb.ByProcedure("SpFinHSNMaster", new string[] { "flag", "HSN_ID" }, new string[] { "6", HSN_ID }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txt_HSNCode.Text = ds.Tables[0].Rows[0]["HSN_Code"].ToString();
                ddl__IntegratedTax.ClearSelection();
                ddl__IntegratedTax.Items.FindByValue(ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString()).Selected = true;
                // txt_IntegratedTax.Text = ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString();
                txt_SGST.Text = ds.Tables[0].Rows[0]["HSN_SGST"].ToString();
                txt_CGST.Text = ds.Tables[0].Rows[0]["HSN_CGST"].ToString();
                //string texable = ds.Tables[0].Rows[0]["Texiblity"].ToString();
                //if (texable == "Taxable")
                //{
                //    ddlTaxabilityEdit.SelectedValue = "1";
                //}
                //else if (texable == "Exempt")
                //{
                //    ddlTaxabilityEdit.SelectedValue = "2";
                //}
                //else if (texable == "Nil Rated")
                //{
                //    ddlTaxabilityEdit.SelectedValue = "3";
                //}
                //else
                //{
                //    ddlTaxabilityEdit.SelectedValue = "0";
                //}
                //txt_HSNEffectiveDate.Text = ds.Tables[0].Rows[0]["HSN_EffectiveDate"].ToString();
                ViewState["HSNEffectiveDate"] = ds.Tables[0].Rows[0]["HSN_EffectiveDate"].ToString();
                FillHSNGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    #endregion
}